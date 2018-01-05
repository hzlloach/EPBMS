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

namespace Web.Jcgl
{
    public partial class DzbyhEdit : TStar.Web.BasePage
    {
        #region 自定义属性

        string Title = "党支部用户";

        //string UID
        //{
        //    get { return TU.Globals.GetParaValue("id", ""); }
        //}

        bool isNew
        {
            get { return string.IsNullOrEmpty(Pkid); }
        }

        #endregion

        #region 自定义方法

        private void BindData()
        {
            // 绑定分党委
            this.lblBmmc.Text = TStar.Web.Globals.Account.DeptName;
            string filter = BLL.Globals.SystemSetting.FilterBm;
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_bm, this.ddlBmbh, "Bmmc", "Pkid", null, filter);

            filter = string.Format("Bmbh IN ('__', '{0}')", this.ddlBmbh.SelectedValue);
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_dzb, this.ddlDzbbh, "Dzbmc", "Pkid", null, filter);
            this.ddlDzbbh.SelectedValue = Pkid;
            this.lblDzbmc.Text = this.ddlDzbbh.SelectedItem.Text;

            // 关闭按钮的客户端脚本
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        private void ShowUI()
        {
            if (!isNew)
            {
                Model.Account_user u = BLL.Account.GetEntity(Pkid);
                if (String.IsNullOrEmpty(u.Pkid))
                {
                    PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                    return;
                }
                this.hfdPkid.Text = u.Pkid;
                this.tbxUid.Text = u.UserID;
            }
        }

        #endregion

        #region 页面及其他事件
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
                this.ShowUI();
            }
        }

        #endregion

        #region 按钮事件

        protected bool Save()
        {
            try
            {
                string bmbh = TStar.Web.Globals.Account.DeptPkid;
                string dzbbh = Pkid;
                string uid = this.tbxUid.Text.Trim();
                string pwd = this.tbxPwd.Text.Trim();
                string pwd2 = this.tbxPwd2.Text.Trim();
                string errMsg = "";

                if (String.IsNullOrEmpty(uid))
                {
                    errMsg += "请输入登录用户名 ！\n";
                }
                if (isNew)
                {
                    if (String.IsNullOrEmpty(pwd) || String.IsNullOrEmpty(pwd2))
                    {
                        errMsg += "请输入登录密码并确认 ！\n";
                    }
                    else if (pwd != pwd2)
                    {
                        errMsg += "两次输入的密码不一致 ！\n";
                    }
                }
                else if ((!String.IsNullOrEmpty(pwd) || !String.IsNullOrEmpty(pwd2)) && pwd != pwd2)
                {
                    errMsg += "两次输入的密码不一致 ！\n";
                }
                if (errMsg.Length > 0)
                {
                    Alert.Show(errMsg, "保存提示", MessageBoxIcon.Warning);
                    return false;
                }

                Model.Jcgl.Jc_lxr m = new Model.Jcgl.Jc_lxr();
                m.Pkid = Pkid;// this.hfdPkid.Text;
                m.Bmbh = bmbh;
                m.Dzbbh = this.ddlDzbbh.SelectedValue;
                m.Gh = uid;
                m.Xm = this.ddlDzbbh.SelectedItem.Text;
                m.Sjhm = pwd;
                m.Lbdm = "0";
                BLL.Jcgl.Jc_lxr.Save(m);
                //ShowNotify("保存成功 ！");

                return true;
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "保存失败", MessageBoxIcon.Error);
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("保存成功 ！", "操作完成", MessageBoxIcon.Information) + ActiveWindow.GetHidePostBackReference());
            }
        }

        #endregion
    }
}