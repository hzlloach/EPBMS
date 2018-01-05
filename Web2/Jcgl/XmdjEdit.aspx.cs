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
    public partial class XmdjEdit : TStar.Web.BasePage
    {
        #region 自定义属性

        string Title = "项目等级";

        #endregion

        #region 自定义方法

        private void BindData()
        {
            // 绑定分党委
            string filter = BLL.Globals.SystemSetting.FilterBm;
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_bm, this.ddlBmbh, "Bmmc", "Pkid", null, filter);
            TUF.Helper.BindTreeView(BLL.Globals.SystemCode.DtTree_khzbLocalCatalogBelongEdit, this.ddlZbbh, "id", "－请选择－");

            this.btnSaveNext.Hidden = !IsAdd;
            // 关闭按钮的客户端脚本
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        private void ShowUI()
        {
            if (!IsAdd)
            {
                Model.Dmgl.Jd_xmdj m = BLL.Dmgl.GetEntity<Model.Dmgl.Jd_xmdj>(Pkid);
                if (String.IsNullOrEmpty(m.Pkid))
                {
                    PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                    return;
                }
                this.hfdPkid.Text = Pkid;
                this.ddlZbbh.SelectedValue = m.Zbbh;
                this.tbxMc.Text = m.Djmc;
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
                Model.Dmgl.Jd_xmdj m = new Model.Dmgl.Jd_xmdj();
                string errMsg = "";
                string bmbh = TStar.Web.Globals.Account.DeptPkid;
                string zbbh = this.ddlZbbh.SelectedValue;
                string djmc = this.tbxMc.Text.Trim();

                if (this.ddlZbbh.SelectedIndex == 0)
                {
                    errMsg += "请选择指标名称 ！\n";
                }
                if (String.IsNullOrEmpty(djmc))
                {
                    errMsg += "请输入等级名称 ！\n";
                }
                else if (BLL.Dmgl.Exists<Model.Dmgl.Jd_xmdj>(Pkid, new string[] { "Bmbh", "Zbbh", "Djmc" }, new string[] { bmbh, zbbh, djmc }))
                {
                    errMsg += "输入的等级名称已存在 ！\n";
                }
                if (errMsg.Length > 0)
                {
                    Alert.Show(errMsg, "保存提示", MessageBoxIcon.Warning);
                    return false;
                }

                m.Pkid = Pkid;
                m.Bmbh = bmbh;
                m.Zbbh = zbbh;
                m.Djmc = djmc;
                BLL.Jcgl.Jd_xmdj.Save(m);

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