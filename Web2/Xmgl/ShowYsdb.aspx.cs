using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using TU = TStar.Utility;
using TUF = TStar.Utility.FineUI;

namespace Web.Xmgl
{
    public partial class ShowYsdb : TStar.Web.BasePage
    {
        #region 自定义属性

        //private TStar.Web.Globals.SystemSetting.Fzzt Fzzt
        //{
        //    get { return TU.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.Fzzt>(this.hfdFzztdm.Text); }
        //}

        #endregion

        #region 自定义方法

        private void BindData()
        {
        }

        private void ShowUI()
        {
            Model.Lcgl.V_lc_nfzmd ys = BLL.Lcgl.Lc_nfzmd.GetEntity<Model.Lcgl.V_lc_nfzmd>(Pkid);
            if (String.IsNullOrEmpty(ys.Pkid))
            {
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                return;
            }

            // 个人基本信息
            this.lblXh.Text = ys.Xh;
            this.lblXm.Text = ys.Xm;
            this.lblDzb.Text = ys.Dzbmc;

            // 预审答辩
            this.lblFzdxrq.Text = ys.Fzdxrq;
            this.lblZsjg.Text = ys.Zsjg;
            this.lblDbjg.Text = ys.Dbjg;
            if (ys.Dbjgdm == "0") this.lblDbjg.CssClass = "spanRed";
            else if (ys.Dbjgdm == "-1") this.lblDbjg.CssClass = "spanGreen";
            if (ys.Zsjgdm == "0")
            {
                this.lblZsjg.CssClass = "spanRed";
                this.pnlBz.Hidden = false;
                this.lblBz.Text = ys.Zswtgyy;
            }
            else
            {
                this.pnlDb1.Hidden = this.pnlDb2.Hidden = false;
                this.lblDbrq.Text = ys.Dbrq;
                this.lblDbdd.Text = ys.Dbdd;
                this.lblDbzcy.Text = ys.Dbzcy;
                this.lblDbpjyj.Text = ys.Dbpjyj;
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
    }
}