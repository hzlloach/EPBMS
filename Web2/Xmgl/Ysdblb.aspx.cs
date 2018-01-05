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
    public partial class Ysdblb : TStar.Web.BasePage
    {
        #region 自定义属性

        string DefBll = "nfzmdcx";
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
        protected string Lxrbh
        {
            get { return TU.Globals.GetParaValue("lxrbh", ""); }
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

            // 绑定分党委、答辩结果
            string filter = BLL.Globals.SystemSetting.FilterBm;
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_bm, this.ddlBm, "Bmmc", "Pkid", null, filter, "__", "－不限－");
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtDm_dbjgzt, this.ddlDbjg, "Mc", "Dm", null);
                       
            this.Grid1.PageSize = BLL.Globals.PageSize;
            this.ddlPageSize.SelectedValue = PageSize.ToString();

            this.ddlBm_SelectedIndexChanged(null, null);
        }

        private bool GetQueryResult()
        {
            string cond = BLL.Globals.SystemSetting.CondBmDzbbh;

            // 构造查询条件
            if (this.ddlBm.SelectedValue != "__" && this.ddlBm.Items.Count > 1) cond += string.Format(" AND Bmbh='{0}'", this.ddlBm.SelectedValue);
            if (this.ddlDzb.SelectedValue != "__" && this.ddlDzb.Items.Count > 1) cond += string.Format(" AND Dzbbh='{0}'", this.ddlDzb.SelectedValue);
            if (this.ddlBj.SelectedIndex > 0) cond += string.Format(" AND Bjbh='{0}'", this.ddlBj.SelectedValue);
            if (this.ddlDbjg.SelectedIndex > 0) cond += String.Format(" AND Dbjgdm='{0}'", this.ddlDbjg.SelectedValue);
            if (this.ddlLxr.SelectedValue != "__" && cond.IndexOf("Lxrbh") == -1) cond += string.Format(" AND Lxrbh='{0}'", this.ddlLxr.SelectedValue);
            if (ttbSearch.Text.Trim() != "")
                cond += String.Format(" AND (Xm LIKE '%{0}%' OR xh LIKE '{0}%')", TStar.Web.Globals.FilterString(this.ttbSearch.Text));
            
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
            //WindowField lbfModify = Grid1.FindColumn("lbfModify") as WindowField;
            LinkButtonField lbfDelete = Grid1.FindColumn("lbfDelete") as LinkButtonField;

            //// 如果绑定到 DataTable，那么这里的 DataItem 就是 DataRowView
            DataRowView row = e.DataItem as DataRowView;
            if (row != null)
            {
                switch (TStar.Utility.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.Status>(row["Ztdm"].ToString()))
                {
                    case TG.SystemSetting.Status.Draft:
                    case TG.SystemSetting.Status.InModify:
                    case TG.SystemSetting.Status.Revoked:
                        lbfDelete.Icon = FineUI.Icon.BulletCross;
                        break;
                    case TG.SystemSetting.Status.Submitted:
                        lbfDelete.Icon = FineUI.Icon.PencilGo;//LaptopDelete;
                        break;
                    case TG.SystemSetting.Status.ToBeModified:
                        lbfDelete.Icon = FineUI.Icon.BulletCross;
                        break;
                    default:
                        break;
                }
                switch (lbfDelete.Icon)
                {
                    case FineUI.Icon.Decline:
                        lbfDelete.CommandName = "Delete";
                        lbfDelete.ToolTip = "删除";
                        lbfDelete.ConfirmText = "确认删除？";
                        break;
                    case FineUI.Icon.PencilGo://LaptopDelete
                        lbfDelete.CommandName = "Revoke";
                        lbfDelete.ToolTip = "撤回";
                        lbfDelete.ConfirmText = "确认撤回？";
                        break;
                }
            }
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            System.Web.UI.WebControls.Label lblZtmc = Grid1.Rows[e.RowIndex].FindControl("lblZtmc") as System.Web.UI.WebControls.Label;
            string zt = lblZtmc.Text;
            switch (zt)
            {
                case "通过":
                    lblZtmc.ForeColor = Color.Blue;
                    break;
                case "未通过":
                    lblZtmc.ForeColor = Color.Red;
                    break;
                case "未答辩":
                default:
                    lblZtmc.ForeColor = Color.Green;
                    break;
            }

            //System.Web.UI.WebControls.HyperLink lblXmmc = Grid1.Rows[e.RowIndex].FindControl("lblXmmc") as System.Web.UI.WebControls.HyperLink;
            //lblXmmc.Attributes["onclick"] = wndView.GetShowReference(String.Format("JshjEdit.aspx?pkid={0}", Grid1.DataKeys[e.RowIndex][0].ToString()), "弹出窗－浏览");
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
                        string bmbh = TStar.Web.Globals.Account.DeptPkid;
                        string dzbbh = TStar.Web.Globals.Account.UserInfo.Dzbbh;
                        string xsbh = TStar.Web.Globals.Account.Pkid;
                        BLL.Xmgl.Yj_xm.Delete(bmbh, dzbbh, xsbh, pkid);
                        break;
                    case "Revoke":
                        title = "撤回";
                        BLL.Xmgl.Yj_xm.Revoke(pkid);
                        break;
                }

                this.BindGrid();

                //Alert.Show(title + "成功 ！", "操作完成", MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, title + "失败", MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}