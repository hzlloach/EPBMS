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
    public partial class Dzbdr : Xmdr.Xmdr
    {
        public bool IsMatch(string pattern, string text)
        {
            return Regex.IsMatch(text, pattern);
        }


        #region 自定义属性
        
        public string Bll
        {
            get { return TU.Globals.GetParaValue("bll", "Drdzb"); }
        }

        #endregion

        protected override void Page_Init(object sender, EventArgs e)
        {
            Columns4Widths = new int[] { 100, 150, 100, 100 };
            base.Page_Init(sender, e);
        }

        /// <summary>
        /// 依据传入参数TableName导入数据
        /// </summary>
        protected override int ImportData(DataRowView drv)
        {
            int cnt = 0;
            string[] columns = Columns, s;
            string dm = drv[columns[0]].ToString();
            if (string.IsNullOrEmpty(dm)) throw new Exception("党支部代码为空。");

            string mc = drv[columns[1]].ToString();
            if (string.IsNullOrEmpty(mc)) throw new Exception("党支部名称为空。");

            string uid = drv[columns[2]].ToString();
            if (string.IsNullOrEmpty(uid)) throw new Exception("登录用户名为空。");

            string pwd = drv[columns[3]].ToString();
            if (string.IsNullOrEmpty(pwd)) throw new Exception("登录密码为空。");

            Model.Jcgl.Jd_dzb m = new Model.Jcgl.Jd_dzb();
            m.Bmbh = TStar.Web.Globals.Account.DeptPkid;
            m.Dzbdm = dm;
            m.Dzbmc = mc;
            m.UserID = uid;
            m.Password = pwd;
            if (BLL.Jcgl.Jd_dzb.Save(m))
            {
                cnt = 1;
                drv["Del"] = 1;
            }

            return cnt;
        }
    }
}