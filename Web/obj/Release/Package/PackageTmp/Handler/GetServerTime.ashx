<%@ WebHandler Language="C#" Class="GetServerTime" %>

using System;
using System.Web;
using System.Web.Services;
using TZM = TStar.Zykt.Model;

public class GetServerTime : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Write(DateTime.Now.ToString("HH:mm:ss"));
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