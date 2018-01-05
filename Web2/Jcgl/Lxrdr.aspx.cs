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
    public partial class Lxrdr : Xmdr.Xmdr
    {
        public bool IsMatch(string pattern, string text)
        {
            return Regex.IsMatch(text, pattern);
        }

        #region 自定义属性
        
        public string Bll
        {
            get { return TU.Globals.GetParaValue("bll", "Drzy"); }
        }

        #endregion

        protected override void Page_Init(object sender, EventArgs e)
        {
            Columns6Widths = new int[] { 200, 120, 100, 120, 80, 120 };
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
            string dzbmc = drv[columns[0]].ToString(); 
            if (string.IsNullOrEmpty(dzbmc)) throw new Exception("党支部名称为空。");

            string dzbbh = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_dzb, "Bmbh='" + bmbh + "'", "Dzbmc", "Pkid", dzbmc, "");
            //Model.Jcgl.Jd_dzb dzb = BLL.Jcgl.Jd_dzb.GetEntity(bmbh, dzbmc);
            if (string.IsNullOrEmpty(dzbbh)) throw new Exception("该党支部信息不存在。");

            string gh = drv[columns[1]].ToString();
            if (string.IsNullOrEmpty(gh)) throw new Exception("工号为空。");

            string xm = drv[columns[2]].ToString();
            if (string.IsNullOrEmpty(xm)) throw new Exception("姓名为空。");

            string sjhm = drv[columns[3]].ToString();
            if (string.IsNullOrEmpty(sjhm)) throw new Exception("手机号码为空。");
            else if (sjhm.Length < 11) throw new Exception("手机号码不正确。");

            string lb = drv[columns[4]].ToString();
            if (string.IsNullOrEmpty(lb)) throw new Exception("角色类别为空。");
            else if (lb != "教师" && lb != "学生") throw new Exception("角色类别不正确。");

            string sj = drv[columns[5]].ToString();
            if (!string.IsNullOrEmpty(sj) && sj != "是") throw new Exception("支部书记不正确。");

            Model.Jcgl.Jc_lxr m = new Model.Jcgl.Jc_lxr();
            m.Bmbh = bmbh;
            m.Dzbbh = dzbbh;
            m.Gh = gh;
            m.Xm = xm;
            m.Sjhm = sjhm;
            m.Lbdm = lb == "教师" ? "1" : "2";
            m.Issj = sj == "是";
            if (BLL.Jcgl.Jc_lxr.Save(m))
            {
                cnt = 1;
                drv["Del"] = 1;
            }

            return cnt;
        }
    }
}