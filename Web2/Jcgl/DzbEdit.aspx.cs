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
    public partial class DzbEdit : TStar.Web.BasePage
    {
        #region 自定义属性

        string Title = "党支部";

        //private string Pkid
        //{
        //    get{ return TU.Globals.GetParaValue("pkid", ""); }
        //}
        //private bool IsAdd
        //{
        //    get { return String.IsNullOrEmpty(Pkid); }
        //}

        #endregion

        #region 自定义方法

        private void BindData()
        {
            // 绑定分党委
            this.lblBmmc.Text = TStar.Web.Globals.Account.DeptName;
            string filter = BLL.Globals.SystemSetting.FilterBm;
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_bm, this.ddlBmbh, "Bmmc", "Pkid", null, filter);
            
            this.btnSaveNext.Hidden = !IsAdd;
            // 关闭按钮的客户端脚本
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        private void ShowUI()
        {
            if (!IsAdd)
            {
                Model.Jcgl.Jd_dzb m = BLL.Dmgl.GetEntity<Model.Jcgl.Jd_dzb>(Pkid);
                if (String.IsNullOrEmpty(m.Pkid))
                {
                    PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                    return;
                }
                this.hfdPkid.Text = Pkid;
                this.tbxDzbdm.Text = m.Dzbdm;
                this.tbxDzbmc.Text = m.Dzbmc;
            }
            else this.tbxUid.Hidden = this.tbxPwd.Hidden = this.tbxPwd2.Hidden = false;
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
                string dzbdm = this.tbxDzbdm.Text.Trim();
                string dzbmc = this.tbxDzbmc.Text.Trim();
                string uid = this.tbxUid.Text.Trim();
                string pwd = this.tbxPwd.Text.Trim();
                string pwd2 = this.tbxPwd2.Text.Trim();
                string errMsg = "";

                if (String.IsNullOrEmpty(dzbdm))
                {
                    errMsg += "请输入" + Title + "代码 ！\n";
                }
                if (String.IsNullOrEmpty(dzbmc))
                {
                    errMsg += "请输入" + Title + "名称 ！\n";
                }
                //if (BLL.Dmgl.Exists<Model.Jcgl.Jd_dzb>(Pkid, new string[] { "bmbh", "Dzbmc" }, new string[] { m.Bmbh, m.Dzbmc }))
                //{
                //    errMsg += "输入的" + Title + "名称已存在 ！\n";
                //}
                if (!this.tbxUid.Hidden)
                {
                    if (String.IsNullOrEmpty(uid))
                    {
                        errMsg += "请输入登录帐号名 ！\n";
                    }
                    if (String.IsNullOrEmpty(pwd))
                    {
                        errMsg += "请输入登录密码 ！\n";
                    }
                    if(String.IsNullOrEmpty(pwd2))
                    {
                        errMsg += "请确认登录密码 ！\n";
                    }
                    else if (pwd != pwd2)
                    {
                        errMsg += "两次输入的登录密码不一致 ！\n";
                    }
                }
                if (errMsg.Length > 0)
                {
                    Alert.Show(errMsg, "保存提示", MessageBoxIcon.Warning);
                    return false;
                }

                Model.Jcgl.Jd_dzb m = TU.Common.ConvertHelper.ConvertToEntity<Model.Jcgl.Jd_dzb>();
                m.UserID = uid;
                m.Password = pwd;
                if (BLL.Jcgl.Jd_dzb.Save(m)) BLL.Globals.SystemCode.RefreshDtJd_dzb();
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

        protected void btnSaveNext_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                this.SimpleForm1.Reset();
                this.btnClose.OnClientClick = ActiveWindow.GetHidePostBackReference();
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("保存成功 ！", "操作完成", MessageBoxIcon.Information));
            }
        }

        #endregion
    }
}