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
    public partial class XmzbEdit : TStar.Web.BasePage
    {
        #region 自定义属性

        string Title = "项目指标";

        #endregion

        #region 自定义方法

        private void BindData()
        {
            // 绑定分党委
            string filter = BLL.Globals.SystemSetting.FilterBm;
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_bm, this.ddlBmbh, "Bmmc", "Pkid", null, filter);
            TUF.Helper.BindTreeView(BLL.Globals.SystemCode.DtTree_khzbLocalCatalog, this.ddlLszb, "dm", "－顶级指标－");

            this.btnSaveNext.Hidden = !IsAdd;
            // 关闭按钮的客户端脚本
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        private void ShowUI()
        {
            if (!IsAdd)
            {
                Model.Dmgl.Jd_khzb m = BLL.Dmgl.GetEntity<Model.Dmgl.Jd_khzb>(Pkid);
                if (String.IsNullOrEmpty(m.Pkid))
                {
                    PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                    return;
                }
                this.hfdPkid.Text = Pkid;
                this.hfdOldZbdm.Text = m.Zbdm;
                this.ddlLszb.SelectedValue = GetParentDm(m.Zbdm);
                this.tbxMc.Text = m.Zbmc;
                this.rblZbqx.SelectedValue = m.Zbqx;
            }
        }

        /// <summary>
        /// 获取给定代码的父代码（最顶级返回"__"）
        /// </summary>
        private string GetParentDm(string dm)
        {
            string pdm = dm;
            int idx = pdm.IndexOf('0');
            if (idx <= 1) pdm = "__";
            else pdm = pdm.Substring(0, idx - 1) + "0".PadLeft(pdm.Length - idx + 1, '0');
            return pdm;
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
                Model.Dmgl.Jd_khzb m = new Model.Dmgl.Jd_khzb();
                string errMsg = "";
                string bmbh = TStar.Web.Globals.Account.DeptPkid;
                string lsdm = this.ddlLszb.SelectedValue;
                string zbmc = this.tbxMc.Text.Trim();
                string zbqx = this.rblZbqx.SelectedValue;
                string txsm = TU.Globals.FilterRiskChar(this.tbxTxsm.Text);

                //if (this.ddlLszb.SelectedIndex == 0)
                //{
                //    errMsg += "请选择隶属指标 ！\n";
                //}
                string nls = lsdm.Replace("0", ""); // 新隶属
                string yls = this.hfdOldZbdm.Text.Replace("0", ""); // 原隶属
                if (yls != "" && nls.IndexOf(yls) == 0)
                {
                    errMsg += "隶属指标不能属于自身 ！\n";
                }
                if (String.IsNullOrEmpty(zbmc))
                {
                    errMsg += "请输入指标名称 ！\n";
                }
                else if (BLL.Dmgl.Exists<Model.Dmgl.Jd_khzb>(Pkid, new string[] { "Bmbh", "Zbmc" }, new string[] { bmbh, zbmc }))
                {
                    errMsg += "输入的指标名称已存在 ！\n";
                }
                if (this.rblZbqx.SelectedIndex < 0)
                {
                    errMsg += "请选择指标类型 ！\n";
                }
                if (errMsg.Length > 0)
                {
                    Alert.Show(errMsg, "保存提示", MessageBoxIcon.Warning);
                    return false;
                }
               
                // 重新计算Zbdm
                if (lsdm == "__" || lsdm != GetParentDm(this.hfdOldZbdm.Text))
                    lsdm = String.Format("dbo.GetKhzbdm('{0}', '{1}')", bmbh, lsdm == "__" ? "" : lsdm.Replace("0", ""));
                else
                    lsdm = this.hfdOldZbdm.Text;

                m.Pkid = Pkid;
                m.Bmbh = bmbh;
                m.Zbdm = lsdm;
                m.Zbmc = zbmc;
                m.Zbqx = zbqx;
                m.Yxsc = this.cbxYxsc.Checked;
                m.Sfqy = true;
                m.Txsm = txsm;
                BLL.Jcgl.Jd_khzb.Save(m);

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
                bool isnew = this.rblZbqx.SelectedValue == "P";
                this.SimpleForm1.Reset();
                if (isnew) TUF.Helper.BindTreeView(BLL.Globals.SystemCode.DtTree_khzbLocalCatalog, this.ddlLszb, "dm", "－请选择－");
                this.btnClose.OnClientClick = ActiveWindow.GetHidePostBackReference();
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("保存成功 ！", "操作完成", MessageBoxIcon.Information));
            }
        }

        #endregion
    }
}