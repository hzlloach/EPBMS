using System;
using System.Data;
using System.Text;
using TG = TStar.Web.Globals;
using TU = TStar.Utility;

namespace BLL.Xmgl
{
    /// <summary>
    /// 思想汇报Xm_sxhb
    /// </summary>
    public class Xm_sxhb : BLL.Global.Base
    {
        /// <summary>
        /// 是否可以新撰写
        /// </summary>
        public static bool CanAdd(string xsbh)
        {
            // 新增：没有未提交的思想汇报（10-13）
            string where = string.Format("Fzrbh='{0}' AND Ztdm Between {1} AND {2}", xsbh, (int)TG.SystemSetting.Status.Draft, (int)TG.SystemSetting.Status.Revoked);
            return GetList<Model.Xmgl.Xm_sxhb>(where, null).Rows.Count == 0;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">如果是新增，则Hbbh保存的是学生Pkid和姓名</param>
        /// <param name="isSubmit">是否提交</param>
        public static void Save(Model.Xmgl.Xm_sxhbmx mx, string xsbh, string ztdm, bool isSubmit)
        {
            switch (TU.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.Status>(ztdm))
            {
                default:
                case TG.SystemSetting.Status.Draft:
                    //mx.Hbnr = mx.Hbnr.Replace("  ", "").Replace("\r", "").Replace("\n", "\n　　");
                    //if (!mx.Hbnr.StartsWith("　")) mx.Hbnr = "　　" + mx.Hbnr;
                    if (string.IsNullOrEmpty(mx.Hbbh))
                        Add(mx, xsbh, isSubmit);
                    else
                        Modify(mx, xsbh, isSubmit);
                    break;
                case TG.SystemSetting.Status.ToBeModified:
                    InModify(mx, xsbh, isSubmit);
                    break;
                case TG.SystemSetting.Status.ToBeRewritten:
                    InRewrite(mx, xsbh, isSubmit);
                    break;
            }
        }

        /// <summary>
        /// 提交
        /// </summary>
        public static void Submit(string xsbh, string hbbh)
        {
            // 判断汇报内容长度
            Model.Xmgl.Xm_sxhbmx mx = GetEntity<Model.Xmgl.Xm_sxhbmx>("Hbbh", hbbh);
            if (mx.Hbnr.Length < 800 || mx.Hbnr.Length > 1500)
            {
                throw new Exception(string.Format("汇报内容必须 800 ～ 1500 个汉字【当前 <span style='color:red'><b>{0}</b></span> 字】 ！", mx.Hbnr.Length));
            }

            // 获取V_xm_sxhb
            Model.Xmgl.V_xm_sxhb vtx = GetEntity<Model.Xmgl.V_xm_sxhb>(hbbh);
            string xm = vtx.Xm;
            string fzztdm = vtx.Fzztdm;
            string lxr = vtx.Lxrxm;
            string sjhm = vtx.Lxrsjhm;
            DateTime dtTjjzrq = DateTime.Parse(vtx.Tjjzsj); // 显示的是1号，实际为上月末(即前一秒)
            TG.SystemSetting.Status ztdm = TU.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.Status>(vtx.Ztdm.ToString());
            int month = (fzztdm == "5" || fzztdm == "6") ? 3 : 2;

            // 保存Xm_sxhb
            string czsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Model.Xmgl.Xm_sxhb hb = new Model.Xmgl.Xm_sxhb();
            hb.Pkid = hbbh;
            hb.Tjsj = czsj;
            hb.Pyjzsj = DateTime.Now.AddDays(14).ToString("yyyy-MM-dd 23:59:59");
            hb.Pytxsj = DateTime.Now.AddDays(9).ToString("yyyy-MM-dd 23:59:59");
            hb.Ztdm = (int)TG.SystemSetting.Status.Submitted;
            string[] fields = new string[] { "Tjsj", "Pyjzsj", "Pytxsj", "Ztdm" };
            if (ztdm == TG.SystemSetting.Status.Draft)
            {
                hb.Yf = (DateTime.Now < dtTjjzrq ? DateTime.Now : dtTjjzrq.AddDays(-1)).ToString("yyyy年MM月");
                fields = new string[] { "Yf", "Tjsj", "Pyjzsj", "Pytxsj", "Ztdm" };
            }
            UpdateFields<Model.Xmgl.Xm_sxhb>(hb, fields);

            // 修改Xm_sxhbtx
            if (ztdm == TG.SystemSetting.Status.Draft) // 新思想汇报且提交截止时间>=下一次提交截止时间时才修改
            {
                string where = string.Format("Pkid='{0}' AND Tjjzsj<='{1}'", xsbh, dtTjjzrq.ToString("yyyy-MM-dd HH:mm:ss"));
                dtTjjzrq = dtTjjzrq.AddMonths(month);

                fields = new string[] { "Tjjzsj", "Tjtxsj" };
                string[] keys = { dtTjjzrq.ToString("yyyy-MM-dd HH:mm:ss"), dtTjjzrq.AddDays(-16).ToString("yyyy-MM-dd 23:59:59") };
                UpdateFields<Model.Xmgl.Xm_sxhbtx>(fields, keys, where);
            }

            string title = "提交了修改";
            switch (ztdm)
            {
                case TG.SystemSetting.Status.Draft:
                    title = "提交了新";
                    break;
                case TG.SystemSetting.Status.InModify:
                    title = "提交了退回修改后";
                    break;
                case TG.SystemSetting.Status.InRewrite:
                    title = "提交了退回重写后";
                    break;
            }

            // 提交时生成短信提醒记录
            Model.Xtgl.Xt_dxfs dx = new Model.Xtgl.Xt_dxfs();
            dx.Dxlb = "02";
            dx.Jsr = lxr;
            dx.Sjhm = sjhm.Substring(0, 11);
            dx.Nr = string.Format("【{0}】联系人，您好！【{1}】刚{2}的思想汇报，请您于{3}前完成评阅 ！", dx.Jsr, xm, title, hb.Pyjzsj);
            dx.Fssj = dx.Bcsj = czsj;
            dx.Ztdm = "0";

            try
            {
                string s = Globals.WsSms.SendMsg(dx.Sjhm, dx.Nr);
                dx.Ztdm = "2";
            }
            catch (Exception err)
            {
                dx.Ztdm = "1";
                dx.Sbyy = err.Message;
            }
            finally
            {
                Insert<Model.Xtgl.Xt_dxfs>(dx);
            }
        }

        /// <summary>
        /// 审核
        /// </summary>
        public static void Audit(Model.Xmgl.Xm_sxhbmx mx, string shrbh, string ztdm)
        {
            // 判断评阅意见长度
            if (mx.Pyyj.Length < 20 || mx.Pyyj.Length > 100)
            {
                throw new Exception(string.Format("评阅意见必须 20 ～ 100 个汉字【当前 <span style='color:red'><b>{0}</b></span> 字】 ！", mx.Pyyj.Length));
            }

            // 保存Xm_sxhbmx
            string where = string.Format("Hbbh='{0}'", mx.Hbbh);
            UpdateFields<Model.Xmgl.Xm_sxhbmx>("Pyyj", mx.Pyyj, where);

            // 保存Xm_sxhb
            bool isBack = false;
            string title = "";
            string czsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            switch (TU.Common.ConvertHelper.EnumParse<TG.SystemSetting.Status>(ztdm))
            {
                case TG.SystemSetting.Status.ToBeModified:
                    isBack = true;
                    title = "修改";
                    break;
                case TG.SystemSetting.Status.ToBeRewritten:
                    isBack = true;
                    title = "重写";
                    break;
            }

            Model.Xmgl.V_xm_sxhb_cur hb = isBack ? GetEntity<Model.Xmgl.V_xm_sxhb_cur>(mx.Hbbh) : null;
            where = string.Format("Pkid='{0}'", mx.Hbbh);
            UpdateFields<Model.Xmgl.Xm_sxhb>(new string[] { "Shrbh", "Pysj", "Ztdm" }, new string[] { shrbh, czsj, ztdm }, where);

            // 退回时生成短信提醒记录
            if (isBack)
            {
                Model.Xtgl.Xt_dxfs dx = new Model.Xtgl.Xt_dxfs();
                dx.Dxlb = "02";
                dx.Jsr = hb.Xm;
                dx.Sjhm = hb.Sjhm.Substring(0, 11);
                dx.Nr = string.Format("【{0}】，您好！您有思想汇报被退回要求{1}，请尽快处理 ！", dx.Jsr, title);
                dx.Fssj = dx.Bcsj = czsj;
                dx.Ztdm = "0";

                try
                {
                    WSSmsService.SmsServiceSoapClient WsSms = new WSSmsService.SmsServiceSoapClient();
                    string s = WsSms.SendMsg(dx.Sjhm, dx.Nr);
                    dx.Ztdm = "2";
                }
                catch (Exception err)
                {
                    dx.Ztdm = "1";
                    dx.Sbyy = err.Message;
                }
                finally
                {
                    Insert<Model.Xtgl.Xt_dxfs>(dx);
                }
            }
        }
        
        /// <summary>
        /// 计算提交序号、提交截止时间及月份
        /// </summary>
        /// <param name="qsrq"></param>
        /// <param name="month"></param>
        private static void CalculateTjxhsj(string qsrq, int month, out int tjxh, out DateTime tjjzsj)
        {
            int no = 1;
            qsrq = qsrq.Replace(".", "");
            string y = qsrq.Substring(0, 4);
            string m = qsrq.Substring(4, 2);
            string d = qsrq.Substring(6, 2);
            DateTime dtNow = DateTime.Now;
            DateTime dt = DateTime.Parse(y + "." + m + ".01");
            if (d.CompareTo("15") <= 0) dt = dt.AddMonths(month);
            else dt = dt.AddMonths(month + 1);

            for (; dt < dtNow; no++)
            {
                dt = dt.AddMonths(month);
            }

            tjxh = no;
            tjjzsj = dt;
        }

        /// <summary>
        /// 新撰写
        /// </summary>
        private static void Add(Model.Xmgl.Xm_sxhbmx mx, string xsbh, bool isSubmit)
        {
            // 计算提交序号Tjxh和提交截止时间Tjjzrq
            Model.Jcgl.Jc_xs xs = GetEntity<Model.Jcgl.Jc_xs>(xsbh);
            int fzztdm = int.Parse(xs.Fzztdm);
            string qsrq = "";
            int tjxh = 1, month = 2;
            DateTime dtTjjzrq;
            switch (fzztdm)
            {
                default:
                case 2: // 积极分子                    
                    qsrq = xs.Jjfzrq;
                    break;
                case 5: // 预备党员
                    qsrq = xs.Rdrq;
                    month = 3;
                    break;
                case 6: // 正式党员
                    qsrq = xs.Zzrq;
                    month = 3;
                    break;
            }
            CalculateTjxhsj(qsrq, month, out tjxh, out dtTjjzrq);

            // 新增Xm_sxhb
            string czsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Model.Xmgl.Xm_sxhb hb = new Model.Xmgl.Xm_sxhb();
            hb.Fzrbh = xsbh;
            hb.Fzztdm = fzztdm.ToString();
            hb.Tjxh = tjxh;
            hb.Yf = (DateTime.Now < dtTjjzrq ? DateTime.Now : dtTjjzrq.AddDays(-1)).ToString("yyyy年MM月");
            hb.Tbsj = czsj;
            hb.Tjjzsj = dtTjjzrq.ToString("yyyy-MM-dd 00:00:00"); // 显示的是1号，实际为上月末(即前一秒)
            hb.Tjtxsj = dtTjjzrq.AddDays(-16).ToString("yyyy-MM-dd 23:59:59");
            hb.Ztdm = (int)TG.SystemSetting.Status.Draft;
            Insert<Model.Xmgl.Xm_sxhb>(hb);

            // 新增Xm_sxhbmx
            mx.Hbbh = hb.Pkid;
            Insert<Model.Xmgl.Xm_sxhbmx>(mx);

            // 提交
            if (isSubmit) Submit(xsbh, hb.Pkid);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private static void Modify(Model.Xmgl.Xm_sxhbmx mx, string xsbh, bool isSubmit)
        {
            // 保存Xm_sxhbmx
            string where = string.Format("Hbbh='{0}'", mx.Hbbh);
            UpdateFields<Model.Xmgl.Xm_sxhbmx>("Hbnr", mx.Hbnr, where);

            // 提交
            if (isSubmit) Submit(xsbh, mx.Hbbh);
        }

        /// <summary>
        /// 退回重写（mx.Hbbh为原sxhb记录的Pkid，保存后原记录Ztdm改为已重写，新记录Ztdm设为重写中、Tjxh和Tjjzsj不变）
        /// </summary>
        private static void InRewrite(Model.Xmgl.Xm_sxhbmx mx, string xsbh, bool isSubmit)
        {
            // 获取原sxhb记录
            Model.Xmgl.Xm_sxhb yhb = GetEntity<Model.Xmgl.Xm_sxhb>(mx.Hbbh);
            string fzztdm = yhb.Fzztdm;
            int tjxh = yhb.Tjxh;
            string tjjzsj = yhb.Tjjzsj;
            string yf = yhb.Yf;

            // 新增Xm_sxhb
            string czsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Model.Xmgl.Xm_sxhb hb = new Model.Xmgl.Xm_sxhb();
            hb.Fzrbh = xsbh;
            hb.Fzztdm = fzztdm;
            hb.Tjxh = tjxh;
            hb.Yf = yf;
            hb.Tbsj = czsj;
            hb.Tjjzsj = tjjzsj;
            hb.Tjtxsj = DateTime.Parse(tjjzsj).AddDays(-16).ToString("yyyy-MM-dd 23:59:59");
            hb.Ztdm = (int)TG.SystemSetting.Status.InRewrite;
            Insert<Model.Xmgl.Xm_sxhb>(hb);

            // 新增Xm_sxhbmx
            mx.Hbbh = hb.Pkid;
            Insert<Model.Xmgl.Xm_sxhbmx>(mx);

            // 修改原Xm_sxhb记录的Ztdm
            string where = string.Format("Pkid='{0}'", yhb.Pkid);
            UpdateFields<Model.Xmgl.Xm_sxhb>("Ztdm", (int)TG.SystemSetting.Status.ReWritten, where);

            // 提交
            if (isSubmit) Submit(xsbh, hb.Pkid);
        }
        
        /// <summary>
        /// 退回修改（mx.Hbbh为原sxhb记录的Pkid，保存后原记录Ztdm改为已修改，新记录Ztdm设为修改中、Tjxh和Tjjzsj不变）
        /// </summary>
        private static void InModify(Model.Xmgl.Xm_sxhbmx mx, string xsbh, bool isSubmit)
        {
            // 获取原sxhb记录
            Model.Xmgl.Xm_sxhb yhb = GetEntity<Model.Xmgl.Xm_sxhb>(mx.Hbbh);
            string fzztdm = yhb.Fzztdm;
            int tjxh = yhb.Tjxh;
            string tjjzsj = yhb.Tjjzsj;
            string yf = yhb.Yf;

            // 新增Xm_sxhb
            string czsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Model.Xmgl.Xm_sxhb hb = new Model.Xmgl.Xm_sxhb();
            hb.Fzrbh = xsbh;
            hb.Fzztdm = fzztdm;
            hb.Tjxh = tjxh;
            hb.Yf = yf;
            hb.Tbsj = czsj;
            hb.Tjjzsj = tjjzsj;
            hb.Tjtxsj = DateTime.Parse(tjjzsj).AddDays(-16).ToString("yyyy-MM-dd 23:59:59");
            hb.Ztdm = (int)TG.SystemSetting.Status.InModify;
            Insert<Model.Xmgl.Xm_sxhb>(hb);

            // 新增Xm_sxhbmx
            mx.Hbbh = hb.Pkid;
            Insert<Model.Xmgl.Xm_sxhbmx>(mx);

            // 修改原Xm_sxhb记录的Ztdm
            string where = string.Format("Pkid='{0}'", yhb.Pkid);
            UpdateFields<Model.Xmgl.Xm_sxhb>("Ztdm", (int)TG.SystemSetting.Status.Modified, where);

            // 提交
            if (isSubmit) Submit(xsbh, hb.Pkid);
        }

        /// <summary>
        /// 获取已评阅的思想汇报
        /// </summary>
        /// <param name="xsbh"></param>
        /// <returns></returns>
        public static DataTable GetAudittedList(string where)
        {
            return BLL.Xmgl.Xm_sxhb.GetList<Model.Xmgl.V_xm_sxhbmx_cur>(where, "Dzbmc,Xh,Yf,Tjsj");
        }

        static Random R = new Random();
        public static void Moni()
        {
            DataView dvXs = GetList<Model.Jcgl.Jc_xs>(null, "Xh").DefaultView;
            int cntDzb = dvXs.Count;
            int count = 30;

            foreach (DataRowView drv in dvXs)
            {
                for (int i = 0; i < count; i++)
                {
                    Model.Xmgl.Xm_sxhb m = new Model.Xmgl.Xm_sxhb();
                    m.Fzrbh = drv["Pkid"].ToString();
                    m.Fzztdm = drv["Fzztdm"].ToString();
                    m.Tjxh = (i + 1);
                    DateTime dt = DateTime.Parse("2016-01-01");
                    dt = dt.AddMonths(i);
                    m.Tbsj = dt.ToString("yyyy-MM-dd");
                    m.Tjjzsj = dt.AddDays(10).ToString("yyyy-MM-dd 23:59:59");
                    int r = R.Next(7);
                    switch (r)
                    {
                        case 0: // 未提交
                            m.Ztdm = 10;
                            break;
                        case 1: // 准时提交，未评阅
                            m.Tjsj = dt.AddDays(8).ToString("yyyy-MM-dd 23:59:59");
                            m.Pyjzsj = dt.AddDays(23).ToString("yyyy-MM-dd 23:59:59");
                            m.Ztdm = 14;
                            break;
                        case 2: // 准时提交，准时评阅
                            m.Tjsj = dt.AddDays(8).ToString("yyyy-MM-dd 23:59:59");
                            m.Pyjzsj = dt.AddDays(23).ToString("yyyy-MM-dd 23:59:59");
                            m.Pysj = dt.AddDays(20).ToString("yyyy-MM-dd 23:59:59");
                            m.Ztdm = 16;
                            break;
                        case 3: // 准时提交，未准时评阅
                            m.Tjsj = dt.AddDays(8).ToString("yyyy-MM-dd 23:59:59");
                            m.Pyjzsj = dt.AddDays(23).ToString("yyyy-MM-dd 23:59:59");
                            m.Pysj = dt.AddDays(25).ToString("yyyy-MM-dd 23:59:59");
                            m.Ztdm = 16;
                            break;
                        case 4: // 未准时提交，未评阅
                            m.Tjsj = dt.AddDays(12).ToString("yyyy-MM-dd 23:59:59");
                            m.Pyjzsj = dt.AddDays(27).ToString("yyyy-MM-dd 23:59:59");
                            m.Ztdm = 14;
                            break;
                        case 5: // 未准时提交，准时评阅
                            m.Tjsj = dt.AddDays(12).ToString("yyyy-MM-dd 23:59:59");
                            m.Pyjzsj = dt.AddDays(27).ToString("yyyy-MM-dd 23:59:59");
                            m.Pysj = dt.AddDays(25).ToString("yyyy-MM-dd 23:59:59");
                            m.Ztdm = 16;
                            break;
                        case 6: // 未准时提交，未准时评阅
                            m.Tjsj = dt.AddDays(12).ToString("yyyy-MM-dd 23:59:59");
                            m.Pyjzsj = dt.AddDays(27).ToString("yyyy-MM-dd 23:59:59");
                            m.Pysj = dt.AddDays(29).ToString("yyyy-MM-dd 23:59:59");
                            m.Ztdm = 16;
                            break;
                    }
                    Insert(m);
                }
            }
        }
    }
}
