using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using TU = TStar.Utility;
using TUF = TStar.Utility.FineUI;

namespace Web.Fzgl
{
    public partial class SelDzb : System.Web.UI.Page
    {

        #region 自定义方法

        private void BindData()
        {
            // 绑定分党委
            this.lblBmmc.Text = TStar.Web.Globals.Account.DeptName;
            string filter = BLL.Globals.SystemSetting.FilterBm;
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_bm, this.ddlBmbh, "Bmmc", "Pkid", null, filter);

            filter = string.Format("Bmbh='{0}' AND {1}", this.ddlBmbh.SelectedValue, BLL.Globals.SystemSetting.FilterDzb);
            TUF.Helper.BindCheckBoxList(BLL.Globals.SystemCode.DtJd_dzb, this.cblDzbbh, "Dzbmc", "Pkid", null, filter);

            //btnDownload.OnClientClick = this.cblDzbbh.SelectedValueArray.GetNoSelectionAlertReference("请至少选择一项！");

            // 关闭按钮的客户端脚本
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        private void ShowUI()
        {
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

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string[] dzbbhs = this.cblDzbbh.SelectedValueArray;
            if (dzbbhs == null || dzbbhs.Length == 0)
            {
                Alert.Show("请至少选择一个党支部 ！", "选择提示", MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string rq = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string path = string.Format(@"{0}\{1}\{2}", Server.MapPath("~/Downloads"), rq.Substring(0, 8), TStar.Web.Globals.Account.Pkid);
                string dir = rq.Substring(8);
                string filename = BLL.Jcgl.Jc_xs.CreateBatPDFSxhb(path, dir, TStar.Web.Globals.Account.DeptPkid, dzbbhs);
                TUF.Helper.SetIFrameUrl(this.pnlFrame, "~/Xtgl/Download.aspx", filename);
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "批量下载失败", MessageBoxIcon.Error);
            }
        }
    }
}