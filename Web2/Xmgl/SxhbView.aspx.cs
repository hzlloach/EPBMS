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
    public partial class SxhbView : TStar.Web.BasePage
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
            //this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        private void ShowUI()
        {
            Model.Xmgl.Xm_sxhbmx m = BLL.Dmgl.GetEntity<Model.Xmgl.Xm_sxhbmx>("Hbbh", Pkid);
            if (String.IsNullOrEmpty(m.Pkid))
            {
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                return;
            }

            Model.Xmgl.V_xm_sxhb_cur sx = BLL.Dmgl.GetEntity<Model.Xmgl.V_xm_sxhb_cur>(Pkid);
            DateTime dt = new DateTime(1900, 1, 1);
            string shsj = sx.Pysj;            
            bool canAudit = !string.IsNullOrEmpty(shsj) && DateTime.TryParse(shsj, out dt) && dt <= DateTime.Now && DateTime.Now <= dt.AddDays(7);
            this.tbrAudit.Hidden = !((sx.Ztdm == (int)TStar.Web.Globals.SystemSetting.Status.Submitted || canAudit) && sx.Lxrbh == TStar.Web.Globals.Account.Pkid);

            this.lblHbnr.Text = m.Hbnr.Replace("\n", "<br/>");
            this.lblFzr.Text = sx.Xm;
            this.lblYf.Text = sx.Yf;
            this.lblTjsj.Text = sx.Tjsj;
            this.lblLxr.Text = sx.Lxrxm;
            bool isWpy = string.IsNullOrEmpty(sx.Pysj);
            this.lblPyzt.Text = isWpy ? "" : ("【" + sx.Ztxsmc + "】");
            this.lblPysj.Text = isWpy ? "" : ("【评阅时间：" + sx.Pysj + "】");
            this.lblShyj.Text = isWpy ? "尚未评阅" : string.Format("<span style='color:#730000'>{0}</span>", m.Pyyj);
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