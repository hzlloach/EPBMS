<%@ WebHandler Language="C#" Class="VerifyCode" %>

using System;
using System.Web;

public class VerifyCode : IHttpHandler
{
    private static Random Rand = new Random();

    /// <summary>
    /// 生成6位短信验证码
    /// </summary>
    private string CreatePhoneCode()
    {
        //long year = DateTime.Now.Year;
        //long month = DateTime.Now.Month;
        //long day = DateTime.Now.Day;
        //int t = DateTime.Now.Hour;
        //long hour = t % 10 == 0 ? t + 1 : t;
        //t = DateTime.Now.Minute;
        //long minute = t % 10 == 0 ? t + 1 : t;
        //t = DateTime.Now.Second + 1;
        //long second = t % 10 == 0 ? t + 1 : t;
        //long millsec = 1;// DateTime.Now.Millisecond;
        //t = Rand.Next(1, 100);
        //long rand = 100 + (t % 10 == 0 ? t + 1 : t);
        //long code = (year + month + day) * hour * minute * second * millsec * rand % 1000000;
        //if (code % 100 == 0) code++;

        long code = Rand.Next(100001, 999999);
        return code.ToString("D6");
    }
    
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //context.Response.Write(TStar.Globals.CreatePhoneCode());

        string errmsg = "";
        string uid = context.Request.Form["uid"];
        string phone = context.Request.Form["phone"];
        string type = context.Request.Form["type"];
        string title = type == "1" ? "重置密码" : "注册";
        if (type == "1" && String.IsNullOrEmpty(uid))
        {
            errmsg += "\n请输入用户名 ！";
        }
        if (String.IsNullOrEmpty(phone))
        {
            errmsg += "\n请输入手机号码 ！";
        }
        if (errmsg.Length > 0)
        {
            context.Response.Write("[Err]" + errmsg.Substring(1));
            return;
        }

        try
        {
            // 验证手机号码
            if (type == "1" && !BLL.Account.ValidatePhone(uid, phone))
            {
                context.Response.Write("[Err]手机号码与注册时登记的号码不一致 ！");
                return;
            }

            string code = CreatePhoneCode();
            if (code.StartsWith("[Err]")) context.Response.Write(code);
            else
            {
                BLL.Account.SetCode(uid, code, 10);
                string content = string.Format("【党员培养管理系统】您申请{0}的验证码为：{1}，十分钟内有效。如非您本人操作，请及时联系学院管理员 ！", title, code);
                string msg = BLL.Globals.SendMsg(phone, content);
                if (msg == "短信成功发送") context.Response.Write(code + "验证码已发送至您手机，<br/>请注意查收 ！");
                else context.Response.Write("[Err]" + msg);
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