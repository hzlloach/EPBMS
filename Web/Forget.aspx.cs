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

namespace Web
{
    public partial class Forget : System.Web.UI.Page
    {
        #region 自定义属性

        private string OperTitle = "重置";

        #endregion

        #region 自定义方法

        private void BindData()
        {
            // 关闭按钮的客户端脚本
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
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
            string uid = this.tbxXh.Text.Trim().ToUpper();
            string pwd = this.tbxPwd.Text.Trim();
            string pwd2 = this.tbxPwd2.Text.Trim();
            string phone = this.tbxSjhm.Text.Trim();
            string code = this.tbxYzm.Text.Trim().ToUpper();

            if (String.IsNullOrEmpty(uid))
            {
                errmsg += "请输入用户名 ！<br/>";
            }
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
            if (String.IsNullOrEmpty(phone))
            {
                errmsg += "请输入手机号码 ！<br/>";
            }
            if (String.IsNullOrEmpty(code))
            {
                errmsg += "请输入验证码 ！<br/>";
            }
            else if (code != this.hfdCode.Text.ToUpper())
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
                Model.Account_user u = BLL.Account.GetEntityByUserId(uid); 
                if (string.IsNullOrEmpty(u.Pkid))
                {
                    Alert.Show("此用户不存在，请与学院管理员联系 ！", MessageBoxIcon.Error);
                    return false;
                }
                if (u.Mobile.Substring(0,11) != phone)
                {
                    errmsg += "\n手机号码不匹配 ！";
                }
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

                BLL.Account.UpdatePwd(u.Pkid, pwd);
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