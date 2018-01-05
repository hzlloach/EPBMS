using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using FineUI;
using TU = TStar.Utility;
using TUF = TStar.Utility.FineUI;

namespace Web.Xmgl
{
    public partial class XmsbView : TStar.Web.BasePage
    {
        #region 自定义属性

        #endregion

        #region 自定义方法

        private void BindData()
        {
            btnAudit.OnClientClick = wndEdit.GetShowReference("ShEdit.aspx?pkid=" + Pkid, "弹出窗－审核") + "return false;";

            // 关闭按钮的客户端脚本
            //this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        private void ShowUI()
        {
            Model.Xmgl.V_yj_xm m = BLL.Xmgl.Yj_xm.GetEntity<Model.Xmgl.V_yj_xm>("Pkid", Pkid);
            if (String.IsNullOrEmpty(m.Pkid))
            {
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                return;
            }

            this.tbrAudit.Hidden = !(m.Ztdm == (int)TStar.Web.Globals.SystemSetting.Status.Submitted && m.Lxrbh == TStar.Web.Globals.Account.Pkid);

            // 基本信息
            this.lblXm.Text = m.Xm;
            this.lblXmlb.Text = m.Zbmc + (!string.IsNullOrEmpty(m.Djbh) ? ("【" + m.Djmc + "】") : "");
            this.lblXmrq.Text = m.Xmrq;
            this.lblXmmc.Text = m.Xmmc;
            this.lblBz.Text = m.Bz;
            this.pnlBz.Hidden = m.Bz.Length == 0;

            // 证明材料
            if (m.Fjsl == 0) this.lblHintZm.Text = "无";
            else
            {
                this.gplZmcl.MinHeight = 280;
                DataTable dtCl = BLL.Xmgl.Yj_xm.GetList<Model.Yjgl.Yj_xmzm>("Xmbh", Pkid, "aid");
                if (dtCl.Rows.Count > 0)
                {
                    this.rptZmcl.DataSource = dtCl.DefaultView;
                    this.rptZmcl.DataBind();
                }
            }

            // 审核意见
            if (m.Ztdm <= (int)TStar.Web.Globals.SystemSetting.Status.Submitted && m.Ztdm != (int)TStar.Web.Globals.SystemSetting.Status.AuditRefused) return;
            this.gplAudit.Hidden = false;
            this.lblShzt.Text = m.Ztxsmc;
            this.lblShr.Text = m.Shrxm;
            this.lblShsj.Text = m.Shsj;
            this.lblShyj.Text = m.Shyj;
            this.pnlShyj.Hidden = m.Ztdm == (int)TStar.Web.Globals.SystemSetting.Status.AuditAccepted;
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

        protected void Window_Close(object sender, WindowCloseEventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #endregion
    }
}