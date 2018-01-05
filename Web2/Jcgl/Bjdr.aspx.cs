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

namespace Web.Jcgl
{
    public partial class Bjdr : Xmdr.Xmdr
    {
        public bool IsMatch(string pattern, string text)
        {
            return Regex.IsMatch(text, pattern);
        }


        #region 自定义属性
        
        public string Bll
        {
            get { return TU.Globals.GetParaValue("bll", "Drbj"); }
        }

        #endregion

        protected override void Page_Init(object sender, EventArgs e)
        {
            Columns2Widths = new int[] { 200, 250 };
            base.Page_Init(sender, e);
        }

        /// <summary>
        /// 依据传入参数TableName导入数据
        /// </summary>
        protected override int ImportData(DataRowView drv)
        {
            int cnt = 0;
            string[] columns = Columns, s;
            string bmbh = TStar.Web.Globals.Account.DeptPkid;
            string zymc = drv[columns[0]].ToString();
            if (string.IsNullOrEmpty(zymc)) throw new Exception("专业名称为空。");

            string zybh = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_zy, "Bmbh='" + bmbh + "'", "Zymc", "Pkid", zymc, "");
            //Model.Jcgl.Jd_dzb dzb = BLL.Jcgl.Jd_dzb.GetEntity(bmbh, dzbmc);
            if (string.IsNullOrEmpty(zybh)) throw new Exception("该专业信息不存在。");

            string dzbbh = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_zy, "Bmbh='" + bmbh + "'", "Pkid", "Dzbbh", zybh, "");

            string bjmc = drv[columns[1]].ToString();
            if (string.IsNullOrEmpty(bjmc)) throw new Exception("班级名称为空。");

            Model.Jcgl.Jd_bj m = new Model.Jcgl.Jd_bj();
            m.Bmbh = bmbh;
            m.Dzbbh = dzbbh;
            m.Zybh = zybh;
            m.Bjmc = bjmc;
            if (BLL.Jcgl.Jd_bj.Save(m))
            {
                cnt = 1;
                drv["Del"] = 1;
            }

            return cnt;
        }
    }
}