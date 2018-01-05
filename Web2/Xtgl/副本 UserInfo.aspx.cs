using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using TU = TStar.Utility;
using TUF = TStar.Utility.FineUI;

namespace Web.Xtgl
{
    public partial class UserInfo : TStar.Web.BasePage
    {
        #region 自定义属性

        private string OperTitle = "修改";

        #endregion

        #region 自定义方法

        private void BindData()
        {
            // 关闭按钮的客户端脚本
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

            this.lblYhm.Text = TStar.Web.Globals.Account.UserIDName;
            this.tbxXh.Text = TStar.Web.Globals.Account.UserID;
        }

        #endregion

        #region 页面及其他事件
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
            }
        }

        #endregion

        #region 按钮事件

        protected bool Save()
        {
            bool pwdOk = true;
            string errmsg = "";
            string pkid = TStar.Web.Globals.Account.Pkid;
            string pwdold = this.tbxPwdOld.Text.Trim();
            string pwd = this.tbxPwd.Text.Trim();
            string pwd2 = this.tbxPwd2.Text.Trim();
            string phone = this.hfdCode.Text.Substring(6);
            string code = this.tbxYzm.Text.Trim().ToUpper();

            if (String.IsNullOrEmpty(pwdold))
            {
                errmsg += "请输入原始密码 ！<br/>";
            }
            else
            {
                string encpwd = BLL.Globals.MD5Encrypt(pwdold);
                if (encpwd != TStar.Web.Globals.Account.Password)
                {
                    errmsg += "原始密码不正确 ！<br/>";
                }
            }
            if (pwd.Length > 0 || pwd2.Length > 0)
            {
                if (String.IsNullOrEmpty(pwd))
                {
                    pwdOk = false;
                    errmsg += "请设置新的登录密码 ！<br/>";
                }
                else if (pwd.Length < 6)
                {
                    errmsg += "密码长度至少 6 位 ！<br/>";
                }
                if (String.IsNullOrEmpty(pwd2))
                {
                    pwdOk = false;
                    errmsg += "请再次确认密码 ！<br/>";
                }
                if (pwdOk && pwd != pwd2)
                {
                    errmsg += "两次输入的密码不一致 ！<br/>";
                }
            }
            if (String.IsNullOrEmpty(phone))
            {
                errmsg += "请输入手机号码 ！<br/>";
            }
            if (String.IsNullOrEmpty(code))
            {
                errmsg += "请输入验证码 ！<br/>";
            }
            else if (code != this.hfdCode.Text.Substring(0, 6))
            {
                errmsg += "输入的验证码不正确 ！<br/>";
            }
            if (errmsg.Length > 0)
            {
                Alert.Show(errmsg, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                Model.Account_user u = BLL.Account.GetEntity(pkid);
                //if (u.Mobile.Substring(0,11) != phone)
                //{
                //    errmsg += "\n手机号码不匹配 ！";
                //}
                if (code != u.VerifyCode)
                {
                    errmsg += "\n验证码不正确 ！";
                }
                else if (DateTime.Now.Ticks > u.Timestamp)
                {
                    errmsg += "\n验证码已过期 ！";
                }
                if (errmsg.Length > 0)
                {
                    Alert.Show(errmsg, MessageBoxIcon.Error);
                    return false;
                }
                if (pwd.Length > 0)
                {
                    BLL.Account.UpdatePwd(u.Pkid, pwd);
                }
                if (phone != TStar.Web.Globals.Account.UserInfo.Mobile)
                {
                    BLL.Account.UpdateMobile(u.Pkid, phone);
                }
                return true;
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, OperTitle + "失败", MessageBoxIcon.Error);
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference(OperTitle + "成功", OperTitle + "完成", MessageBoxIcon.Information) + ActiveWindow.GetHidePostBackReference());
            }
        }

        #endregion
    }
}