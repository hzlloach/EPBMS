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

namespace Web.Tjbb
{
    public partial class Ybdy : TStar.Web.BasePage
    {
        #region 自定义属性

        string DefBll = "hz_ybdy";
        string DefSort = "Bmdm,Dzbdm,Bjmc,Xh";
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
        protected int PageIndex
        {
            get { return this.Grid1.PageIndex + 1; }
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
            this.Grid1.PageSize = BLL.Globals.PageSize; 
            this.ddlPageSize.SelectedValue = BLL.Globals.PageSize.ToString();
        }

        private bool GetQueryResult()
        {
            string cond = BLL.Globals.SystemSetting.CondBmDzbbh;
            QueryWhere = cond;

            this.BindGrid();
            return true;
        }

        private void BindGrid()
        {
            BLL.Globals.BindGrid(Grid1, PageIndex, BLL.Globals.PageSize, Bll, QueryWhere, QuerySort);
            this.btnExport.Enabled = Grid1.RecordCount > 0;
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                TUF.Helper.SetIFrameUrl(this.pnlFrame, "~/Xtgl/Export.aspx", DefBll, QueryWhere, QuerySort);
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "导出失败", MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 网格事件

        protected void Grid1_PageIndexChanged(object sender, GridPageEventArgs e)
        {
            this.Grid1.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void Grid1_PageSizeChanged(object sender, EventArgs e)
        {
            BLL.Globals.PageSize = Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);

            // 更改每页显示数目时，防止 PageIndex 越界
            if (Grid1.PageIndex > Grid1.PageCount - 1)
            {
                Grid1.PageIndex = Grid1.PageCount - 1;
            }

            this.BindGrid();
        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            QuerySort = String.Format("{0} {1}", e.SortField, e.SortDirection);
            this.BindGrid();
        }

        #endregion
    }
}