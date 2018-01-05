using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using TU = TStar.Utility;
using TUF = TStar.Utility.FineUI;
using TG = TStar.Web.Globals;

namespace Web.Tjbb
{
    public partial class Xmlb : TStar.Web.BasePage
    {
        #region 自定义属性

        string DefBll = "tj_xmlb";
        string DefSort = "1";
        string DefSortFirst = "";

        protected string Bll
        {
            get
            {
                this.tbxBll.Text = TU.Globals.GetParaValue("bll", DefBll);
                return this.tbxBll.Text;
            }
        }
        protected string QueryWhere
        {
            get { return this.tbxWhere.Text; }
            set { this.tbxWhere.Text = value; }
        }
        protected string QuerySort
        {
            get
            {
                string sort = this.tbxSort.Text;
                if (String.IsNullOrEmpty(sort)) sort = DefSort;
                else if (!string.IsNullOrEmpty(DefSortFirst) && !sort.StartsWith(DefSortFirst))
                    sort = DefSortFirst + "," + sort;
                return sort;
            }
            set { this.tbxSort.Text = value; }
        }

        #endregion

        #region 自定义方法

        private void BindData()
        {
        }

        private bool GetQueryResult()
        {
            string bmbh = TStar.Web.Globals.Account.DeptPkid;
            string dzbbh = "";
            switch (TStar.Utility.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.UserLevel>(TStar.Web.Globals.Account.UserLevel))
            {
                case TStar.Web.Globals.SystemSetting.UserLevel.Contacts:
                case TStar.Web.Globals.SystemSetting.UserLevel.Branch:
                    dzbbh = TStar.Web.Globals.Account.UserInfo.Dzbbh;
                    break;
                case TStar.Web.Globals.SystemSetting.UserLevel.Committee:
                case TStar.Web.Globals.SystemSetting.UserLevel.Party:
                    break;
                default :
                    return true;
            }

            DataTable dtXy = BLL.Tjbb.Fzjd.TjByXy(bmbh);
            BLL.Globals.BindGrid(grdXy, dtXy.DefaultView);

            DataTable dtXyDzb = BLL.Tjbb.Fzjd.TjByXyDzb(bmbh, dzbbh);
            BLL.Globals.BindGrid(grdXydzb, dtXyDzb.DefaultView);

            DataTable dtXyZy = BLL.Tjbb.Fzjd.TjByXyZy(bmbh);
            BLL.Globals.BindGrid(grdXyzy, dtXyZy.DefaultView);

            return true;
        }

        #endregion

        #region 页面及其他事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();

                this.GetQueryResult();
            }
        }

        #endregion

        #region 按钮事件

        public void btnExport_Click(object sender, EventArgs e)
        {
            string BLL = "Xyyjsh";
            //Star.Globals.SetIFrameUrl(this.pnlFrame, "~/Xtgl/Export.aspx", BLL, QueryWhere);
        }

        #endregion

        #region 网格事件

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            //QuerySort = String.Format("{0} {1}", e.SortField, e.SortDirection);
            //this.BindGrid();
        }

        #endregion
    }
}