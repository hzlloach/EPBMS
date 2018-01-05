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

namespace Web.Xmgl
{
    public partial class SxhbShEdit : TStar.Web.BasePage
    {
        #region 自定义属性

        private string Hbbh 
        {
            get { return TU.Globals.GetParaValue("hbbh", ""); }
        }

        #endregion

        #region 自定义方法

        /// <summary>
        /// 获取新思想汇报的提交序号
        /// </summary>
        /// <param name="xsbh"></param>
        private void GetTjxh(string xsbh)
        {
            ;
        }

        private void BindData()
        {
            // 关闭按钮的客户端脚本
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        private void ShowUI()
        {
            Model.Xmgl.Xm_sxhbmx m = BLL.Dmgl.GetEntity<Model.Xmgl.Xm_sxhbmx>("Hbbh", Hbbh);
            if (String.IsNullOrEmpty(m.Pkid))
            {
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                return;
            }

            this.hfdHbbh.Text = m.Hbbh;
            this.tbxPyyj.Text = m.Pyyj;
            Model.Xmgl.Xm_sxhb hb = BLL.Dmgl.GetEntity<Model.Xmgl.Xm_sxhb>(Hbbh);
            this.ddlShzt.SelectedValue = hb.Ztdm.ToString();
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

        protected bool Audit()
        {
            try
            {
                string ztdm = this.ddlShzt.SelectedValue;
                Model.Xmgl.Xm_sxhbmx m = TU.Common.ConvertHelper.ConvertToEntity<Model.Xmgl.Xm_sxhbmx>();
                BLL.Xmgl.Xm_sxhb.Audit(m, TStar.Web.Globals.Account.Pkid, ztdm);

                return true;
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "评阅失败", MessageBoxIcon.Error);
                return false;
            }
        }

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            if (Audit())
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHidePostBackReference();
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("评阅成功 ！", "操作完成", MessageBoxIcon.Information, ActiveWindow.GetHidePostBackReference()));
            }
        }

        #endregion
    }
}