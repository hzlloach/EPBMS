using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TU = TStar.Utility;

namespace Web
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TStar.Web.Globals.Account.RemoveSession();

            string p = TU.Globals.GetParaValue("t", "");
            string lpage = System.Configuration.ConfigurationManager.AppSettings["LoginPage"];
            string path = TStar.Web.Globals.GetAbsolutePagePath(lpage);
            StringBuilder js = new StringBuilder();
            js.Append("<script language='JavaScript'>");
            if (p == "") js.Append("alert('您尚未登录 或 登录已过期 ！\\n请重新登录 ！');");
            js.Append("window.top.location.href='" + path + "';");
            js.Append("</script>");
            Page.Response.Write(js.ToString());
            Page.Response.End();
        }
    }
}