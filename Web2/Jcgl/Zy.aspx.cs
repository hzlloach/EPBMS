﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using TU = TStar.Utility;
using TUF = TStar.Utility.FineUI;

namespace Web.Jcgl
{
    public partial class Zy : TStar.Web.BasePage
    {
        #region 自定义属性

        protected string Bll
        {
            get
            {
                this.tbxBll.Text = TU.Globals.GetParaValue("bll", "zy");
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
                if (String.IsNullOrEmpty(sort)) sort = "Bmbh,Dzbdm,Zymc";
                else
                {
                    if (sort.IndexOf("Bmbh") == -1) sort = "Bmbh," + sort;
                }
                return sort;
            }
            set { this.tbxSort.Text = value; }
        }

        #endregion

        #region 自定义方法

        private void BindData()
        {
            // 绑定分党委
            string filter = BLL.Globals.SystemSetting.FilterBm;
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_bm, this.ddlBm, "Bmmc", "Pkid", null, filter);
            this.ddlBm_SelectedIndexChanged(null, null);

            btnAddNew.Enabled = btnDeleteSel.Enabled = true;
            btnAddNew.OnClientClick = wndEdit.GetShowReference("ZyEdit.aspx", "弹出窗－新增") + "return false;";
            btnDeleteSel.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
            btnDeleteSel.ConfirmText = String.Format("确认要删除选中的数据行吗？");
            string tblname = TU.Globals.TripleDESEncrypt("专业" + BLL.Globals.SystemSetting.EncryptCode);
            btnImport.OnClientClick = wndImport.GetShowReference("../Jcgl/Zydr.aspx?p=" + tblname) + "return false;";
            //PageContext.RegisterStartupScript(this.wndEdit.GetMaximizeReference());

            this.Grid1.PageSize = BLL.Globals.PageSize; 
            this.ddlPageSize.SelectedValue = BLL.Globals.PageSize.ToString();
        }

        private bool GetQueryResult()
        {
            string cond = BLL.Globals.SystemSetting.CondBm;

            // 构造查询条件
            if (this.ddlBm.SelectedValue != "__" && this.ddlBm.Items.Count > 1) cond += string.Format(" AND Bmbh='{0}'", this.ddlBm.SelectedValue);
            if (this.ddlDzb.SelectedValue != "__" && this.ddlDzb.Items.Count > 1) cond += string.Format(" AND Dzbbh='{0}'", this.ddlDzb.SelectedValue);

            if (ttbSearch.Text.Trim() != "")
                cond += String.Format(" AND Zymc LIKE '%{0}%'", TStar.Web.Globals.FilterString(this.ttbSearch.Text));

            //    // 保存排序字段
            //    if (isSort)
            //    {
            //        sort = values["Grid1_sortField"];
            //        if (sort != "") sortField = sort;
            //        direc = values["Grid1_sortDirection"];
            //    }
            //    MyGrid.SortField(isSort ? sort : "", direc);

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

        protected void ddlBm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filter = string.Format("Bmbh IN ('__', '{0}') AND {1}", this.ddlBm.SelectedValue, BLL.Globals.SystemSetting.FilterDzb);
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_dzb, this.ddlDzb, "Dzbmc", "Pkid", null, filter);

            this.ddl_SelectedIndexChanged(null, null);
        }

        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageIndex = 0;
            Grid1.SortField = Grid1.SortDirection = "";

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
                BLL.Dmgl.DeleteList<Model.Jcgl.Jd_zy>(ids);
                BLL.Globals.SystemCode.RefreshDtJd_zy();
                this.BindGrid();
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "删除失败", MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 网格事件

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
                        BLL.Dmgl.Delete<Model.Jcgl.Jd_zy>(pkid);
                        BLL.Globals.SystemCode.RefreshDtJd_zy();
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