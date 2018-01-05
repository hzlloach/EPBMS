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
    public partial class Xmsblb : TStar.Web.BasePage
    {
        #region 自定义属性

        string DefBll = "yjxmcx";
        string DefSort = "Bmdm,Dzbdm,Bjmc,Xh,Xmrq DESC";
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
        protected int PageIndex
        {
            get { return this.Grid1.PageIndex + 1; }
        }
        protected int PageSize
        {
            get { return this.Grid1.PageSize; }
        }

        protected string Bmbh
        {
            get { return TU.Globals.GetParaValue("bmbh", ""); }
        }
        protected string Dzbbh
        {
            get { return TU.Globals.GetParaValue("dzbbh", ""); }
        }
        protected string Fzrbh
        {
            get { return TU.Globals.GetParaValue("fzrbh", ""); }
        }
        protected string Lxrbh
        {
            get { return TU.Globals.GetParaValue("lxrbh", ""); }
        }
        protected string Fzztdm
        {
            get { return TU.Globals.GetParaValue("fzzt", ""); }
        }
        protected string Ztdm
        {
            get { return TU.Globals.GetParaValue("ztdm", ""); }
        }

        #endregion

        #region 自定义方法
        
        private void BindData()
        {
            if (Request.UrlReferrer != null)
            {
                string url = Request.UrlReferrer.OriginalString;
                url = url.Substring(url.LastIndexOf("/") + 1).ToLower();
                if (url != "frame.aspx")
                {
                    this.btnBack.Hidden = false;
                    this.btnBack.OnClientClick = "window.location.href='../Home/" + url + "'";
                }
            }

            // 绑定分党委
            string filter = BLL.Globals.SystemSetting.FilterBm;
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_bm, this.ddlBm, "Bmmc", "Pkid", null, filter, "__", "－不限－");
            TUF.Helper.BindTreeView(BLL.Globals.SystemCode.DtTree_khzbLocalBelong, this.ddlKhzb, null);
            Grid1.Columns[1].Hidden = this.ddlBm.Items.Count == 1;

            this.Grid1.PageSize = BLL.Globals.PageSize;
            this.ddlPageSize.SelectedValue = BLL.Globals.PageSize.ToString();
            PageContext.RegisterStartupScript(this.wndView.GetMaximizeReference());

            this.ddlFzzt.SelectedValue = Fzztdm;
            if (!string.IsNullOrEmpty(Ztdm)) this.ddlZt.SelectedValue = Ztdm;
            this.ddlBm_SelectedIndexChanged(null, null);
        }

        private bool GetQueryResult()
        {
            string cond = string.Format("{0} AND (Zbqx='P' OR Sfgd='false')", BLL.Globals.SystemSetting.CondBmDzbbh);

            // 构造查询条件
            if (this.ddlBm.SelectedValue != "__" && this.ddlBm.Items.Count > 1) cond += string.Format(" AND Bmbh='{0}'", this.ddlBm.SelectedValue);
            if (this.ddlDzb.SelectedValue != "__" && this.ddlDzb.Items.Count > 1) cond += string.Format(" AND Dzbbh='{0}'", this.ddlDzb.SelectedValue);
            if (this.ddlBj.SelectedIndex > 0) cond += string.Format(" AND Bjbh='{0}'", this.ddlBj.SelectedValue);
            if (this.ddlKhzb.SelectedIndex > 0) cond += String.Format(" AND Zbdm LIKE '{0}%'", this.ddlKhzb.SelectedValue.Replace("0", ""));
            switch (this.ddlFzzt.SelectedValue)
            {
                case "2":
                    cond += " AND Fzztdm BETWEEN 2 AND 4";
                    break;
                case "5":
                case "6":
                    cond += string.Format(" AND Fzztdm='{0}'", this.ddlFzzt.SelectedValue);
                    break;
            }
            switch (this.ddlZt.SelectedIndex)
            {
                case 0:
                    cond += string.Format(" AND Ztdm={0} AND Lxrbh='{1}' ", (int)TG.SystemSetting.Status.Submitted, TStar.Web.Globals.Account.Pkid);
                    break;
                case 1:
                    cond += string.Format(" AND Ztdm>={0} ", (int)TG.SystemSetting.Status.AuditAccepted);
                    break;
                case 2:
                    cond += string.Format(" AND Ztdm={0} ", (int)TG.SystemSetting.Status.AuditRefused);
                    break;
            }
            if (this.ddlLxr.SelectedValue != "__" && cond.IndexOf("Lxrbh") == -1) cond += string.Format(" AND Lxrbh='{0}'", this.ddlLxr.SelectedValue);
            if (ttbSearch.Text.Trim() != "")
                cond += String.Format(" AND (xm LIKE '%{0}%' OR xh LIKE '{0}%')", TStar.Web.Globals.FilterString(this.ttbSearch.Text));
            
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

                //this.GetQueryResult();
            }
        }

        protected void ddlBm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Bmbh)) this.ddlBm.SelectedValue = Bmbh;
            string filter = string.Format("Bmbh IN ('__', '{0}') AND {1}", this.ddlBm.SelectedValue, BLL.Globals.SystemSetting.FilterDzb);
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_dzb, this.ddlDzb, "Dzbmc", "Pkid", null, filter);

            if (!string.IsNullOrEmpty(Dzbbh)) this.ddlDzb.SelectedValue = Dzbbh;
            this.ddlDzb_SelectedIndexChanged(null, null);
        }

        protected void ddlDzb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filter = this.ddlDzb.SelectedValue == "__" ? string.Format("Bmbh IN ('__', '{0}')", this.ddlBm.SelectedValue) : string.Format("Dzbbh IN ('__', '{0}')", this.ddlDzb.SelectedValue);
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_bj, this.ddlBj, "Bjmc", "Pkid", null, filter);

            if (BLL.Globals.SystemSetting.IsContacts) filter += string.Format(" AND Pkid='{0}'", TStar.Web.Globals.Account.Pkid);
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJc_lxr, this.ddlLxr, "Xm", "Pkid", null, filter);
            if (!string.IsNullOrEmpty(Lxrbh)) this.ddlLxr.SelectedValue = Lxrbh;

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

        protected void Grid1_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
            //// 如果绑定到 DataTable，那么这里的 DataItem 就是 DataRowView
            DataRowView row = e.DataItem as DataRowView;
            if (row == null) return;

            DateTime dt = new DateTime(1900, 1, 1);
            string shsj = row["Shsj"].ToString();
            bool canAudit = !string.IsNullOrEmpty(shsj) && DateTime.TryParse(shsj, out dt) && dt <= DateTime.Now && DateTime.Now <= dt.AddDays(7);
            if (this.ddlZt.SelectedValue != "0" && !canAudit) return;

            WindowField lbfOper = Grid1.FindColumn("lbfOper") as WindowField;
            if (row["Lxrbh"].ToString() == TStar.Web.Globals.Account.Pkid)
            {
                lbfOper.Icon = FineUI.Icon.Pencil;
                lbfOper.ToolTip = "审核";
            }
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            //System.Web.UI.WebControls.Label lblZtmc = Grid1.Rows[e.RowIndex].FindControl("lblZtmc") as System.Web.UI.WebControls.Label;
            //string zt = lblZtmc.Text;
            //switch (zt)
            //{
            //    case "未审核":
            //        lblZtmc.ForeColor = Color.Red;
            //        break;
            //    default:
            //        lblZtmc.Text = "审核通过";
            //        lblZtmc.ForeColor = Color.Blue;
            //        break;
            //}
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