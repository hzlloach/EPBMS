using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using FUI = FineUI;
using TU = TStar.Utility;

namespace Web.Xmdr
{
    public partial class Dxpxdr : Xmdr
    {
        #region 自定义属性
        
        public string Bll
        {
            get { return TU.Globals.GetParaValue("bll", "YjDryj"); }
        }

        #endregion

        protected override void Page_Init(object sender, EventArgs e)
        {
            Columns5Widths = new int[] { 110, 100, 60, 100, 80 };
            base.Page_Init(sender, e);
        }

        /// <summary>
        /// 依据传入参数TableName导入数据
        /// </summary>
        protected override int ImportData(DataRowView drv)
        {
            int cnt = 0;
            string[] columns = Columns;
            string xh = drv[columns[0]].ToString();
            string xm = drv[columns[1]].ToString();
            string bmbh = TStar.Web.Globals.Account.DeptPkid;
            string dzbbh = TStar.Web.Globals.Account.UserInfo.Dzbbh;
            Model.Jcgl.Jc_xs xs = BLL.Jcgl.Jc_xs.GetEntity(bmbh, dzbbh, xh, xm);
            if (string.IsNullOrEmpty(xs.Pkid)) throw new Exception("该学生信息不存在。");

            string zbbh = Djzbmc.Substring(0, 32);
            string filter = string.Format("Zbbh='{0}'", zbbh);
            string djmc = drv[columns[4]].ToString();
            string djbh = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_xmdjLocal, filter, "Djmc", "Pkid", djmc, "");
            if (djbh == "") throw new Exception(columns[4] + "不正确。");

            string fzrbh = xs.Pkid;
            string fzztdm = xs.Fzztdm.ToString();
            string xmmc = "第" + drv[columns[2]].ToString() + "期党校培训";
            string xmrq = TU.Globals.Parse2DateTime(drv[columns[3]].ToString(), "2000-01-01").ToString("yyyy-MM-dd");
            if (xmrq == "2000-01-01") throw new Exception(columns[3] + "不正确。");

            if (BLL.Xmgl.Yj_xm.Exist(Pkid, new string[] { "Fzztdm", "Zbbh", "Fzrbh", "Xmmc" }, new string[] { fzztdm, zbbh, fzrbh, xmmc }))
                throw new Exception("该党校培训成绩已导入。");

            Model.Xmgl.Yj_xm m = new Model.Xmgl.Yj_xm();
            m.Xmmc = xmmc;
            m.Fzztdm = fzztdm;
            m.Bmbh = bmbh;
            m.Zbbh = zbbh;
            m.Djbh = djbh;
            m.Fzrbh = fzrbh;
            m.Xmrq = m.Jzrq = xmrq;
            m.Bcsj = m.Shsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
            m.Shrbh = TStar.Web.Globals.Account.Pkid;
            m.Ztdm = (int)TStar.Web.Globals.SystemSetting.Status.CommitteeImported;
            if (BLL.Xmgl.Yj_xm.Save(m))
            {
                // 考核等级非不合格时写入jc_xs表的党校培训情况
                string khzt = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_xmdjLocal, "Pkid", "Djmc", djbh, "不合格");
                if (khzt != "不合格")
                {
                    string where = string.Format("Pkid='{0}'", fzrbh);
                    string khztdm = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtDm_khzt, "Mc", "Dm", khzt, "1");
                    BLL.Jcgl.Jc_xs.UpdateFields<Model.Jcgl.Jc_xs>(new string[] { "Dxkhztdm", "Dxjyrq" }, new string[] { khztdm, xmrq }, where);
                }

                cnt = 1;
                drv["Del"] = 1;
            }

            return cnt;
        }

    }
}