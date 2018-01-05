<%@ WebHandler Language="C#" Class="VerifyUser" %>

using System;
using System.Web;
using System.Web.Services;
using TZM = TStar.Zykt.Model;

public class VerifyUser : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //context.Response.Write(TStar.Globals.CreatePhoneCode());

        string errmsg = "";
        string uid = context.Request.Form["uid"];
        if (String.IsNullOrEmpty(uid))
        {
            //context.Response.Write("[Err]请输入用户名 ！");
            return;
        }

        try
        {
            //TZM.AccountInfo u = BLL.Zykt.GetAccInfo(uid, 4);
            //if (!String.IsNullOrEmpty(u.PerCode))
            //{
            //    context.Response.Write(u.AccName);
            //}
            Model.Account.Account_user u = BLL.Account.GetEntityByUserId(uid);
            if (!string.IsNullOrEmpty(u.Pkid))
            {
                context.Response.Write(string.Format("{0}|{1}|{2}", u.Pkid, u.UserID, u.UserName));
            }
            else
            {
                context.Response.Write("[Err]查无此人");
            }
        }
        catch (Exception err)
        {
            //if (err.Message == "") context.Response.Write("[Err]查无此人 ！");
            //else context.Response.Write("[Err]" + err.Message);
            context.Response.Write("[Err]查无此人");
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }

    //[WebMethod]
    //public static string sayHi()
    //{
    //    return "Hi,Welcome to China!";
    //}
}