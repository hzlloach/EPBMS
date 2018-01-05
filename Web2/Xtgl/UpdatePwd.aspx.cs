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
    public partial class UpdatePwd : TStar.Web.BasePage
    {
        #region 自定义属性

        private string OperTitle = "修改密码";

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
            if (errmsg.Length > 0)
            {
                Alert.Show(errmsg, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                BLL.Account.UpdatePwd(pkid, pwd);
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
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference(OperTitle + "成功 ！", OperTitle + "完成", MessageBoxIcon.Information) + ActiveWindow.GetHidePostBackReference());
            }
        }

        #endregion
    }
}