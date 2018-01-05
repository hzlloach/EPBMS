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
    public partial class SxhbShView : TStar.Web.BasePage
    {
        #region 自定义属性

        //protected string Pkid
        //{
        //    get{ return TU.Globals.GetParaValue("pkid", ""); }
        //}
        //protected bool IsAdd
        //{
        //    get { return String.IsNullOrEmpty(Pkid); }
        //}        

        #endregion

        #region 自定义方法

        private void BindData()
        {
            btnAudit.OnClientClick = wndEdit.GetShowReference("SxhbShEdit.aspx?hbbh=" + Pkid, "弹出窗－评阅") + "return false;";

            // 关闭按钮的客户端脚本
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        private void ShowUI()
        {
            Model.Xmgl.Xm_sxhbmx m = BLL.Dmgl.GetEntity<Model.Xmgl.Xm_sxhbmx>("Hbbh", Pkid);
            if (String.IsNullOrEmpty(m.Pkid))
            {
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                return;
            }

            this.hfdPkid.Text = Pkid;
            this.hfdHbbh.Text = m.Hbbh;
            this.lblHbnr.Text = m.Hbnr.Replace("\n", "<br/>");
            Model.Xmgl.V_xm_sxhb_cur sx = BLL.Dmgl.GetEntity<Model.Xmgl.V_xm_sxhb_cur>(Pkid);
            this.lblFzr.Text = sx.Xm;
            this.lblFzzt.Text = sx.Fzzt;
            this.lblShyj.Text = string.IsNullOrEmpty(sx.Pysj) ? "尚未评阅" : string.Format("【{0}】<span style='color:#730000'>{1}</span>", sx.Ztxsmc, m.Pyyj);
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

        #region 按钮事件

        #endregion
    }
}