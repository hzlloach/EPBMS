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

namespace Web.Home
{
    public partial class Homexdw : TStar.Web.BasePage
    {
        #region 自定义属性

        string DefBll = "tj_zbxy";
        string DefSort = "Dzbdm";
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
            DataTable dtXy = BLL.Tjbb.Zk.TjZbByXy(bmbh);
            BLL.Globals.BindGrid(grdXy, dtXy.DefaultView);

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

        #endregion

        #region 网格事件

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            QuerySort = String.Format("{0} {1}", e.SortField, e.SortDirection);
            this.GetQueryResult();
        }

        #endregion
    }
}