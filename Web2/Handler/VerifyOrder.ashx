<%@ WebHandler Language="C#" Class="VerifyOrder" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using TZM = TStar.Zykt.Model;

public class VerifyOrder : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //context.Response.Write(TStar.Globals.CreatePhoneCode());

        string errmsg = "";
        string uid = context.Request.Form["uid"];
        string gh = context.Request.Form["gh"];
        string ccsj = context.Request.Form["ccsj"];
        string busSID = context.Request.Form["busSID"];
        int sl = int.Parse(context.Request.Form["sl"]);
        double sum = double.Parse(context.Request.Form["sum"]);        
        bool isGwk = context.Request.Form["isGwk"] == "1";

        int seats = 0;
        Model.Yd_Order od = null;
        try
        {
            // 写预订表
            od = new Model.Yd_Order();
            od.UserID = uid;
            od.UserGh = gh;
            od.BusDate = ccsj;
            od.BusScheduleID = busSID;
            od.Quantity = sl;
            od.Total = sum;
            bool rlt = BLL.Yd_Order.Order(od, isGwk);

            try
            {
                // 刷新剩余座位数
                seats = BLL.Jc_BusSchedule.GetSeats(od.BusScheduleID);
                //this.lblSyzw.Text = seats.ToString();
            }
            catch (Exception errSw)
            {
                if (!rlt) context.Response.Write(seats + "|[Err]获取剩余座位失败 ！\n" + errSw.Message);
            }

            if (!rlt) // 预订时修改剩余座位失败
            {
                if (seats == 0)
                {
                    context.Response.Write(seats + "|该车次座位已售完 ！");
                }
                else
                {
                    context.Response.Write(seats + "|[Err]该车次剩余座位不足 ！");
                }
                return;
            }
        }
        catch (Exception err)
        {
            context.Response.Write(seats + "|[Err]" + err.Message);
            return;
        }

        // 支付  
        try
        {
            BLL.Yd_Order.Pay(od, 1, 0, TStar.Globals.Code);
            context.Response.Write(seats + "|预订成功，请准时乘车 ！");
        }
        catch (Exception errZf)
        {
            if (errZf.Message.StartsWith("[Wn]")) context.Response.Write(seats + "|[Wrn]" + errZf.Message.Substring(4));
            else context.Response.Write(seats + "|" + errZf.Message + "请尽快在预订后 " + BLL.Globals.RemainMinutes + " 分钟内完成支付 ！");
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