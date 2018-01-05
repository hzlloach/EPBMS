using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using FUI = FineUI;
using TU = TStar.Utility;

namespace Web.Xmdr
{
    public partial class Xxcjdr : Xmdr
    {
        public bool IsMatch(string pattern, string text)
        {
            return Regex.IsMatch(text, pattern);
        }


        #region 自定义属性
        
        public string Bll
        {
            get { return TU.Globals.GetParaValue("bll", "YjDryj"); }
        }

        #endregion

        protected override void Page_Init(object sender, EventArgs e)
        {
            Columns5Widths = new int[] { 110, 100, 110, 110, 100 };
            base.Page_Init(sender, e);
        }

        /// <summary>
        /// 依据传入参数TableName导入数据
        /// </summary>
        protected override int ImportData(DataRowView drv)
        {
            int cnt = 0, cjpm, zhpm, rs, bjgms;
            string[] columns = Columns, s;
            string xh = drv[columns[0]].ToString();
            string xm = drv[columns[1]].ToString();
            string bmbh = TStar.Web.Globals.Account.DeptPkid;
            string dzbbh = TStar.Web.Globals.Account.UserInfo.Dzbbh;
            Model.Jcgl.Jc_xs xs = BLL.Jcgl.Jc_xs.GetEntity(bmbh, dzbbh, xh, xm);
            if (string.IsNullOrEmpty(xs.Pkid)) throw new Exception("该学生信息不存在。");

            string zbbh = Djzbmc.Substring(0, 32);
            string xxcjpm = drv[columns[2]].ToString();
            if (!IsMatch(@"^\d+/\d+$", xxcjpm)) throw new Exception(columns[2] + "不正确。");
            else
            {
                s = xxcjpm.Split('/');
                cjpm = int.Parse(s[0]);
                rs = int.Parse(s[1]);
            }

            string zhkppm = drv[columns[3]].ToString();
            if (!IsMatch(@"^\d+/\d+$", zhkppm)) throw new Exception(columns[3] + "不正确。");
            else
            {
                s = zhkppm.Split('/');
                zhpm = int.Parse(s[0]);
                int rs2 = int.Parse(s[1]);
                if (rs2 != rs) throw new Exception(columns[2] + "与" + columns[3] + "的总人数不一致。");
            }

            string bjg = drv[columns[4]].ToString();
            if (!IsMatch(@"^\d+$", bjg)) throw new Exception(columns[4] + "不正确。");
            bjgms = int.Parse(bjg);

            string fzrbh = xs.Pkid;
            string fzztdm = xs.Fzztdm.ToString();
            string xmmc = BLL.Globals.SystemSetting.Dqxqmc + "学习成绩";
            string xmrq = BLL.Globals.SystemSetting.DxqJzsj.Substring(0,10);
            string jzrq = BLL.Globals.SystemSetting.Dqxq;
            string bz = string.Format("学习成绩排名{0}、综合考评排名{1}、不及格门数{2}", xxcjpm, zhkppm, bjg);

            if (BLL.Xmgl.Yj_xm.Exist(Pkid, new string[] { "Fzztdm", "Zbbh", "Fzrbh", "Xmmc" }, new string[] { fzztdm, zbbh, fzrbh, xmmc }))
                throw new Exception("该学习成绩已导入。");

            Model.Xmgl.Yj_xm m = new Model.Xmgl.Yj_xm();
            m.Xmmc = xmmc;
            m.Fzztdm = fzztdm;
            m.Bmbh = bmbh;
            m.Zbbh = zbbh;
            m.Fzrbh = fzrbh;
            m.Xmrq = xmrq;
            m.Jzrq = jzrq;
            m.Bz = bz;
            m.Bcsj = m.Shsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
            m.Shrbh = TStar.Web.Globals.Account.Pkid;
            m.Ztdm = (int)TStar.Web.Globals.SystemSetting.Status.CommitteeImported;
            if (BLL.Xmgl.Yj_xm.Save(m))
            {
                // 写入jc_xs表的学习成绩情况                
                string where = string.Format("Pkid='{0}'", fzrbh);
                BLL.Jcgl.Jc_xs.UpdateFields<Model.Jcgl.Jc_xs>(new string[] { "Xxcjpm", "Zhkppm", "Bjgms" }, new string[] { xxcjpm, zhkppm, bjg }, where);
                
                cnt = 1;
                drv["Del"] = 1;
            }

            return cnt;
        }
    }
}