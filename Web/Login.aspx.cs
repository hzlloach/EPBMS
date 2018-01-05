using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                btnReg.OnClientClick = wndReg.GetShowReference();
                btnForget.OnClientClick = wndPwd.GetShowReference();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            TStar.Web.Globals.Account.RemoveSession();

            string uid = this.tbxUserID.Text.Trim();
            string pwd = this.tbxPassword.Text.Trim();
            string errMsg = "";

            if (String.IsNullOrEmpty(uid))
            {
                errMsg  += "请输入用户名 ！\n";
            }
            if (String.IsNullOrEmpty(pwd))
            {
                errMsg += "请输入密码 ！\n";
            }
            if (errMsg.Length > 0)
            {
                Alert.Show(errMsg, "提示", MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Model.Account_user user = BLL.Account.Validate(uid, pwd);
                if (user == null)
                {
                    Alert.Show("用户名或密码不正确 ！", "提示", MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    TStar.Web.Globals.Account.SaveSession(user);
                    Response.Redirect("Frame.aspx");
                }
            }
            catch (Exception err)
            {
            }
        }
    }
}