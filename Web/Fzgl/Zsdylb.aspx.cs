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

namespace Web.Fzgl
{
    public partial class Zsdylb : TStar.Web.BasePage
    {
        #region 自定义属性

        protected string Bll
        {
            get
            {
                this.tbxBll.Text = TU.Globals.GetParaValue("bll", "xs");
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
                if (String.IsNullOrEmpty(sort)) sort = "Dzbmc,Bjmc,Xh";                
                return sort;
            }
            set { this.tbxSort.Text = value; }
        }

        #endregion

        #region 自定义方法

        private void BindData()
        {
            // 绑定党支部
            string filter = BLL.Globals.SystemSetting.FilterDzb;//string.Format("Bmbh IN ('__','{0}')", TStar.Web.Globals.Account.DeptPkid);
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_dzb, this.ddlDzb, "Dzbmc", "Pkid", null, filter);
            this.ddlDzb_SelectedIndexChanged(null, null);

            this.Grid1.PageSize = BLL.Globals.PageSize;
            this.ddlPageSize.SelectedValue = BLL.Globals.PageSize.ToString();

            PageContext.RegisterStartupScript(this.wndView.GetMaximizeReference());
        }

        private bool GetQueryResult()
        {
            string cond = string.Format("Fzztdm='6' AND {0}", BLL.Globals.SystemSetting.CondBmDzbbh);
            
            // 构造查询条件
            if (this.ddlDzb.SelectedIndex > 0) cond += string.Format(" AND Dzbbh='{0}'", this.ddlDzb.SelectedValue);
            if (this.ddlBj.SelectedIndex > 0) cond += string.Format(" AND Bjbh='{0}'", this.ddlBj.SelectedValue);

            if (ttbSearch.Text.Trim() != "")
                cond += String.Format(" AND (xm LIKE '%{0}%' OR xh LIKE '{0}%')", TStar.Web.Globals.FilterString(this.ttbSearch.Text));

            QueryWhere = cond;

            this.BindGrid();

            return true;
        }

        private void BindGrid()
        {
            BLL.Globals.BindGrid(Grid1, PageIndex, BLL.Globals.PageSize, Bll, QueryWhere, QuerySort);
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

        protected void ddlDzb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageIndex = 0;
            Grid1.SortField = Grid1.SortDirection = "";

            this.ddlBj_SelectedIndexChanged(null, null);
        }

        protected void ddlBj_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filter = string.Format("Dzbbh IN ('__', '{0}')", this.ddlDzb.SelectedValue);
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_bj, this.ddlBj, "Bjmc", "Pkid", null, filter);

            this.GetQueryResult();
        }

        protected void ttbSearch_Trigger1Click(object sender, EventArgs e)
        {
            QueryWhere = "";
            ttbSearch.Text = String.Empty;
            ttbSearch.ShowTrigger1 = false;
        }
        protected void ttbSearch_Trigger2Click(object sender, EventArgs e)
        {
            Grid1.PageIndex = 0;
            Grid1.SortField = Grid1.SortDirection = "";
            ttbSearch.ShowTrigger1 = !string.IsNullOrEmpty(this.ttbSearch.Text);

            this.GetQueryResult();
        }

        protected void Window_Close(object sender, WindowCloseEventArgs e)
        {
            this.BindGrid();
        }

        #endregion

        #region 按钮事件

        protected void btnDeleteSel_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ids = TUF.Helper.GetSelectedRowIDs(this.Grid1).Split(',');
                BLL.Jcgl.Jc_xs.DeleteList<Model.Jcgl.Jc_xs>(ids);
                this.BindGrid();
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "删除失败", MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 网格事件

        protected void Grid1_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
            //WindowField lbfModify = Grid1.FindColumn("lbfModify") as WindowField;
            //LinkButtonField lbfDelete = Grid1.FindColumn("lbfDelete") as LinkButtonField;

            //// 如果绑定到 DataTable，那么这里的 DataItem 就是 DataRowView
            //DataRowView row = e.DataItem as DataRowView;
            //if (row != null)
            //{
            //    bool canFill = BLL.Globals.CanAdd;
            //    bool canOper = BLL.Globals.CanLnModifySubmit(row["Ztdm"].ToString());
            //    bool isSubmitted = row["Ztdm"].ToString() == ((int)TStar.Globals.SystemSettings.YjState.Submitted).ToString(); //是否已提交
            //    bool isBack = row["Ztdm"].ToString() == ((int)TStar.Globals.SystemSettings.YjState.ModifyEnd).ToString(); //是否退回修改
            //    lbfModify.Icon = canOper ? Ext.Icon.Pencil : Ext.Icon.None;

            //    if (canFill) // 申报中
            //    {
            //        // 是删除还是提交撤回
            //        lbfDelete.Icon = isSubmitted ? Ext.Icon.PencilGo : (canOper ? Ext.Icon.Delete : Ext.Icon.None);
            //    }
            //    else // 申报截止
            //    {
            //        lbfDelete.Icon = canOper ? Ext.Icon.Delete : Ext.Icon.None;
            //    }
            //    switch (lbfDelete.Icon)
            //    {
            //        case Ext.Icon.Delete:
            //            lbfDelete.CommandName = "Delete";
            //            lbfDelete.ToolTip = "删除";
            //            lbfDelete.ConfirmText = "确认删除？";
            //            break;
            //        case Ext.Icon.PencilGo:
            //            lbfDelete.CommandName = "Revoke";
            //            lbfDelete.ToolTip = "提交撤回";
            //            lbfDelete.ConfirmText = "确认撤回？";
            //            break;
            //    }
            //}
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            //Label lblZtmc = Grid1.Rows[e.RowIndex].FindControl("lblZtmc") as Label;
            //string zt = lblZtmc.Text;
            //switch (zt)
            //{
            //    case "未提交":
            //        lblZtmc.ForeColor = Color.Black;
            //        break;
            //    case "已提交":
            //        lblZtmc.ForeColor = Color.Blue;
            //        break;
            //    case "审核通过":
            //    case "公示中":
            //    case "已归档":
            //        lblZtmc.ForeColor = Color.Green;
            //        break;
            //    case "已删除":
            //    case "审核拒绝":
            //    case "退回修改":
            //    default:
            //        lblZtmc.ForeColor = Color.Red;
            //        break;
            //}

            //HyperLink lblXmmc = Grid1.Rows[e.RowIndex].FindControl("lblXmmc") as HyperLink;
            //lblXmmc.Attributes["onclick"] = wndYjView.GetShowReference(String.Format("ShowGryj.aspx?id={0}", Grid1.DataKeys[e.RowIndex][0].ToString()), "弹出窗－历年业绩浏览");
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string title = "";
            try
            {
                string pkid = Grid1.DataKeys[e.RowIndex][0].ToString();
                switch (e.CommandName)
                {
                    case "Delete":
                        title = "删除";
                        BLL.Jcgl.Jc_xs.DeleteList<Model.Jcgl.Jc_xs>(pkid);
                        break;
                }
                this.BindGrid();
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, title + "失败", MessageBoxIcon.Error);
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