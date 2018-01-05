using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class test : System.Web.UI.Page
    {
        public static string IPAddress
        {
            get
            {
                string userIP;  
                HttpRequest Request = HttpContext.Current.Request; // ForumContext.Current.Context.Request;  
                // 如果使用代理，获取真实IP  
                if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
                    userIP = Request.ServerVariables["REMOTE_ADDR"];
                else
                    userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (userIP == null || userIP == "")
                    userIP = Request.UserHostAddress;
                return userIP;
            }
        }  

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("您的IP地址为：" + IPAddress);
        }
    }
}