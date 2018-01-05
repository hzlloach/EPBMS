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
    public partial class Reg : System.Web.UI.Page
    {
        #region 自定义属性

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
            string name = this.tbxXm.Text.Trim();
            string pwd = this.tbxPwd.Text.Trim();
            string pwd2 = this.tbxPwd2.Text.Trim();
            string sfzh = this.tbxSfzh.Text.Trim().ToUpper();
            string phone = this.tbxSjhm.Text.Trim();
            string code = this.tbxYzm.Text.Trim().ToUpper();

            if (String.IsNullOrEmpty(uid))
            {
                errmsg += "请输入用户名 ！<br/>";
            }
            if (String.IsNullOrEmpty(name))
            {
                errmsg += "请输入姓名 ！<br/>";
            }
            if (String.IsNullOrEmpty(sfzh))
            {
                errmsg += "请输入身份证号 ！<br/>";
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

            Model.Jcgl.V_jc_xs u = BLL.Jcgl.Jc_xs.GetEntity<Model.Jcgl.V_jc_xs>(new string[] { "UserID", "UserName" }, new string[] { uid, name });
            if(string.IsNullOrEmpty(u.Pkid))
            {
                Alert.Show("此学号与姓名的用户不存在，\n请与学院管理员联系 ！", MessageBoxIcon.Error);
                return false;
            }
            else if(u.Sfzh != sfzh)
            {
                Alert.Show("身份证号不匹配 ！", MessageBoxIcon.Error);
                return false;
            }
            else if(u.Sjhm.Substring(0,11) != phone)
            {
                Alert.Show("手机号码不匹配 ！", MessageBoxIcon.Error);
                return false;
            }

            //TZM.AccountInfo ai = null;
            //try
            //{
            //    ai = BLL.Zykt.GetAccInfo(uid, 4);
            //    if (ai == null) ShowErr("该统一帐户不存在 ！");
            //}
            //catch (Exception errykt)
            //{
            //    ShowErr("获取统一帐户信息失败 ！");
            //    return;
            //}

            //if (ai.PerCode.ToUpper() != uid || ai.AccName != name)
            //{
            //    errmsg += "用户名与姓名不匹配 ！<br/>";
            //}
            //if (ai.CertCode.ToUpper() != sfzh)
            //{
            //    errmsg += "身份证号不匹配 ！<br/>";
            //}
            //if (errmsg.Length > 0)
            //{
            //    ShowWarn(errmsg);
            //    return;
            //}

            //string[] bms = ai.FullPath.Split('|');
            //string bm = bms.Length >= 3 ? bms[2] : ai.DepName;
            //try
            //{
            //    if (!BLL.Jcgl.Jd_Bm.Exists(bm))
            //    {
            //        Model.Jcgl.Jd_Bm b = new Model.Jcgl.Jd_Bm();
            //        b.Pkid = "YKTBM";
            //        b.Dm = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            //        b.Mc = bm;
            //        BLL.Jcgl.Jd_Bm.Insert(b);
            //        bm = b.Pkid;
            //    }
            //}
            //catch (Exception errbm)
            //{
            //    ShowErr("同步部门信息失败 ！");
            //    return;
            //}

            try
            {
                BLL.Account.UpdatePwd(u.Pkid, pwd);
                return true;
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "注册失败", MessageBoxIcon.Error);
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("注册成功", "注册完成", MessageBoxIcon.Information) + ActiveWindow.GetHidePostBackReference());
            }
        }

        #endregion

        protected void btnSend_Click(object sender, EventArgs e)
        {

        }
    }
}