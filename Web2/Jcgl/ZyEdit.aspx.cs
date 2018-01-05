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
    public partial class ZyEdit : TStar.Web.BasePage
    {
        #region 自定义属性

        string Title = "专业";

        #endregion

        #region 自定义方法

        private void BindData()
        {
            // 绑定分党委
            this.lblBmmc.Text = TStar.Web.Globals.Account.DeptName;
            string filter = BLL.Globals.SystemSetting.FilterBm;
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_bm, this.ddlBmbh, "Bmmc", "Pkid", null, filter);

            filter = string.Format("Bmbh IN ('__', '{0}') AND {1}", this.ddlBmbh.SelectedValue, BLL.Globals.SystemSetting.FilterDzb);
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_dzb, this.ddlDzbbh, "Dzbmc", "Pkid", null, filter);
            
            this.btnSaveNext.Hidden = !IsAdd;
            // 关闭按钮的客户端脚本
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        private void ShowUI()
        {
            if (!IsAdd)
            {
                Model.Jcgl.Jd_zy m = BLL.Dmgl.GetEntity<Model.Jcgl.Jd_zy>(Pkid);
                if (String.IsNullOrEmpty(m.Pkid))
                {
                    PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                    return;
                }
                this.hfdPkid.Text = Pkid;
                this.ddlDzbbh.SelectedValue = m.Dzbbh;
                this.tbxZymc.Text = m.Zymc;
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
                string dzbbh = this.ddlDzbbh.SelectedValue;
                string zymc = this.tbxZymc.Text.Trim();
                string errMsg = "";

                if (this.ddlDzbbh.SelectedIndex == 0)
                {
                    errMsg += "请选择党支部名称 ！\n";
                }
                if (String.IsNullOrEmpty(zymc))
                {
                    errMsg += "请输入" + Title + "名称 ！\n";
                }
                if (errMsg.Length > 0)
                {
                    Alert.Show(errMsg, "保存提示", MessageBoxIcon.Warning);
                    return false;
                }

                Model.Jcgl.Jd_zy m = TU.Common.ConvertHelper.ConvertToEntity<Model.Jcgl.Jd_zy>();
                if (BLL.Jcgl.Jd_zy.Save(m)) BLL.Globals.SystemCode.RefreshDtJd_zy();
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