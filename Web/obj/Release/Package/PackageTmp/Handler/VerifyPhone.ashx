<%@ WebHandler Language="C#" Class="VerifyPhone" %>

using System;
using System.Web;
using System.Web.Services;

public class VerifyPhone : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //context.Response.Write(TStar.Globals.CreatePhoneCode());

        string errmsg = "";
        string uid = context.Request.Form["uid"];
        if (String.IsNullOrEmpty(uid))
        {
            context.Response.Write("[Err]请输入用户名 ！");
            return;
        }

        try
        {
            Model.Account.Account_user u = BLL.Account.GetEntityByUserId(uid);
            if (!String.IsNullOrEmpty(u.Pkid))
            {
                context.Response.Write(u.Mobile);
            }
            else
            {
                context.Response.Write("[Err]此用户名不存在 ！");
            }
        }
        catch (Exception err)
        {
            context.Response.Write("[Err]" + err.Message);
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}