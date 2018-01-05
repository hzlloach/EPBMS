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

namespace Web.Xmgl
{
    public partial class Slxlbgr : TStar.Web.BasePage
    {
        #region 自定义属性

        string DefBll = "yjxmcx_all";
        string DefSort = "Xmrq DESC";
        string DefSortFirst = "";

        // 项目类别编号
        private string Zbbh
        {
            get { return TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_khzbLocal, "Zbmc", "Pkid", "党员三联系", ""); }
        }
        protected string Fzrbh
        {
            get { return TU.Globals.GetParaValue("fzrbh", ""); }
        }
        protected string Fzztdm
        {
            get { return TU.Globals.GetParaValue("fzzt", ""); }
        }

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
        protected int PageIndex
        {
            get { return this.Grid1.PageIndex + 1; }
        }
        protected int PageSize
        {
            get { return this.Grid1.PageSize; }
        }

        #endregion

        #region 自定义方法

        private void BindData()
        {
            //TUF.Helper.BindTreeView(BLL.Globals.SystemCode.DtTree_khzbLocalOther, this.ddlKhzb, null);
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_khzbLocal, this.ddlKhzb, "Zbmc", "Pkid", null, null, Zbbh);
      
            this.Grid1.PageSize = BLL.Globals.PageSize;
            this.ddlPageSize.SelectedValue = PageSize.ToString();
            PageContext.RegisterStartupScript(this.wndView.GetMaximizeReference());
        }

        private bool GetQueryResult()
        {
            string cond = string.Format("Fzrbh='{0}' AND Fzztdm{1}'{2}' AND Ztdm>={3}", Fzrbh, (Fzztdm=="4" ? "<=" : "="), Fzztdm, (int)TG.SystemSetting.Status.Audited );
            
            // 构造查询条件
            if (this.ddlKhzb.SelectedValue != "__")
            {
                cond += string.Format(" AND Zbbh='{0}'", this.ddlKhzb.SelectedValue);
            }
            QueryWhere = cond;

            this.BindGrid();

            return true;
        }

        private void BindGrid()
        {
            BLL.Globals.BindGrid(Grid1, PageIndex, PageSize, Bll, QueryWhere, QuerySort);
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

        protected void ddlKhzb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageIndex = 0;
            Grid1.SortField = Grid1.SortDirection = "";

            this.GetQueryResult();
        }

        #endregion

        #region 按钮事件

        #endregion

        #region 网格事件

        protected void Grid1_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
            WindowField lbfModify = Grid1.FindColumn("lbfModify") as WindowField;
            LinkButtonField lbfDelete = Grid1.FindColumn("lbfDelete") as LinkButtonField;

            //// 如果绑定到 DataTable，那么这里的 DataItem 就是 DataRowView
            DataRowView row = e.DataItem as DataRowView;
            if (row != null)
            {
                switch (TStar.Utility.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.Status>(row["Ztdm"].ToString()))
                {
                    case TG.SystemSetting.Status.Draft:
                    case TG.SystemSetting.Status.InRewrite:
                    case TG.SystemSetting.Status.InModify:
                    //case TG.SystemSetting.Status.Revoked:
                        lbfModify.Icon = FineUI.Icon.Pencil;
                        lbfDelete.Icon = FineUI.Icon.LaptopGo;
                        break;
                    case TG.SystemSetting.Status.Submitted:
                        lbfModify.Icon = FineUI.Icon.None;
                        lbfDelete.Icon = FineUI.Icon.None;//LaptopDelete;
                        break;
                    case TG.SystemSetting.Status.ToBeModified:
                        lbfModify.Icon = FineUI.Icon.Pencil;
                        lbfDelete.Icon = FineUI.Icon.None;
                        break;
                    case TG.SystemSetting.Status.ToBeRewritten:
                        lbfModify.Icon = FineUI.Icon.PencilAdd;
                        lbfDelete.Icon = FineUI.Icon.None;
                        break;
                    default:
                        lbfModify.Icon = lbfDelete.Icon = FineUI.Icon.None;
                        break;
                }
                switch (lbfModify.Icon)
                {
                    case FineUI.Icon.Pencil:
                        lbfModify.DataIFrameUrlFormatString = "SxhbEdit.aspx?pkid={0}&ztdm={1}";
                        lbfModify.Title = "弹出窗－修改";
                        lbfModify.ToolTip = "修改";
                        break;
                    case FineUI.Icon.PencilAdd:
                        lbfModify.DataIFrameUrlFormatString = "SxhbEdit.aspx?glbh={0}&ztdm={1}";
                        lbfModify.Title = "弹出窗－重写";
                        lbfModify.ToolTip = "重写";
                        break;
                }
                switch (lbfDelete.Icon)
                {
                    case FineUI.Icon.LaptopGo:
                        lbfDelete.CommandName = "Submit";
                        lbfDelete.ToolTip = "提交";
                        lbfDelete.ConfirmText = "确认提交？";
                        break;
                    //case FineUI.Icon.LaptopDelete:
                    //    lbfDelete.CommandName = "Revoke";
                    //    lbfDelete.ToolTip = "撤回";
                    //    lbfDelete.ConfirmText = "确认撤回？";
                    //    break;
                }
            }
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            System.Web.UI.WebControls.Label lblZtmc = Grid1.Rows[e.RowIndex].FindControl("lblZtmc") as System.Web.UI.WebControls.Label;
            string zt = lblZtmc.Text;
            switch (zt)
            {
                case "未提交":
                case "重写中":
                case "修改中":
                    lblZtmc.ForeColor = Color.Black;
                    break;
                case "已提交":
                    lblZtmc.Text = "未评阅";
                    lblZtmc.ForeColor = Color.Blue;
                    break;
                case "退回重写":
                case "退回修改":
                case "已删除":
                case "已重写":
                case "已修改":
                    lblZtmc.ForeColor = Color.Red;
                    break;
                case "评阅通过":
                case "已归档":
                default:
                    lblZtmc.ForeColor = Color.Green;
                    break;
            }
        }

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