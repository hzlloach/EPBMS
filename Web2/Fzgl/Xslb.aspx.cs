using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using TU = TStar.Utility;
using TUF = TStar.Utility.FineUI;

namespace Web.Fzgl
{
    public partial class Xslb : TStar.Web.BasePage
    {
        #region 自定义属性

        string DefBll = "xs";
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
        protected string Fzztdm
        {
            get { return TU.Globals.GetParaValue("fzztdm", "__"); }
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

            // 绑定党支部
            string filter = BLL.Globals.SystemSetting.FilterBm;
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_bm, this.ddlBm, "Bmmc", "Pkid", null, filter, "__", "－不限－");

            btnDeleteSel.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
            btnDeleteSel.ConfirmText = String.Format("确认要删除选中的数据行吗？");

            this.Grid1.PageSize = BLL.Globals.PageSize; 
            this.ddlPageSize.SelectedValue = BLL.Globals.PageSize.ToString();
            PageContext.RegisterStartupScript(this.wndView.GetMaximizeReference());

            this.ddlFzzt.SelectedValue = Fzztdm;
            this.ddlBm_SelectedIndexChanged(null, null);

            switch (TStar.Utility.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.UserLevel>(TStar.Web.Globals.Account.UserLevel))
            {
                default:
                    break;
                case TStar.Web.Globals.SystemSetting.UserLevel.Contacts:
                case TStar.Web.Globals.SystemSetting.UserLevel.Branch:
                    this.btnBatCreate.Hidden = false;
                    this.btnBatCreateFdw.Hidden = true;
                    break;
                case TStar.Web.Globals.SystemSetting.UserLevel.Committee:
                    this.btnBatCreate.Hidden = true;
                    this.btnBatCreateFdw.Hidden = false;
                    this.btnBatCreateFdw.OnClientClick = wndSel.GetShowReference("SelDzb.aspx");
                    break;
            }
        }

        private void ShowGridColumn()
        {
            bool showFzzt = false, showJjfzrq = false, showFzdxrq = false, showFzrq = false, showZzrq = false, showMod = false;
            switch (this.ddlFzzt.SelectedIndex)
            {
                default:
                    showFzzt = true;
                    break;
                case 1: 
                    showJjfzrq = true;
                    break;
                case 2:
                    showFzdxrq = true;
                    break;
                case 3:
                    showFzrq = true;
                    break;
                case 4:
                    showZzrq = true;
                    break;
            }

            Grid1.Columns[1].Hidden = this.ddlBm.Items.Count == 1;
            Grid1.Columns[7].Hidden = !showFzzt;   // 是否显示发展阶段列
            Grid1.Columns[8].Hidden = !showJjfzrq; // 是否显示确定积极分子日期列
            Grid1.Columns[9].Hidden = !showFzdxrq; // 是否显示确定发展对象日期列
            Grid1.Columns[10].Hidden = !showFzrq;   // 是否显示入党日期列
            Grid1.Columns[11].Hidden = !showZzrq;  // 是否显示转正日期列
            Grid1.Columns[12].Hidden = !showJjfzrq; // 是否显示党校结业日期列
            Grid1.Columns[13].Hidden = !showJjfzrq;   // 是否显示党校考核状态列
            Grid1.Columns[14].Hidden = !showJjfzrq; // 是否显示学习成绩排名列
            Grid1.Columns[15].Hidden = !showJjfzrq;   // 是否显示综合素质排名列
            Grid1.Columns[16].Hidden = !showJjfzrq;  // 是否显示不及格门数列

            int level = int.Parse(TStar.Web.Globals.Account.UserLevel);
            showMod = level == (int)TStar.Web.Globals.SystemSetting.UserLevel.Committee || level == (int)TStar.Web.Globals.SystemSetting.UserLevel.Branch;
            Grid1.Columns[23].Hidden = !showMod;  // 是否显示修改列（分党委或支部时显示）
        }

        private bool GetQueryResult()
        {
            string cond = BLL.Globals.SystemSetting.CondBmDzbbh.Replace("Lxrbh=", "Rdlxrbh1=");

            // 构造查询条件
            if (this.ddlBm.SelectedValue != "__" && this.ddlBm.Items.Count > 1) cond += string.Format(" AND Bmbh='{0}'", this.ddlBm.SelectedValue);
            if (this.ddlDzb.SelectedValue != "__" && this.ddlDzb.Items.Count > 1) cond += string.Format(" AND Dzbbh='{0}'", this.ddlDzb.SelectedValue);
            if (this.ddlBj.SelectedIndex > 0) cond += string.Format(" AND Bjbh='{0}'", this.ddlBj.SelectedValue);
            switch (this.ddlFzzt.SelectedIndex)
            {
                case 1:
                    cond += " AND Fzztdm IN ('2','3')";
                    break;
                case 2:
                case 3:
                case 4:
                    cond += string.Format(" AND Fzztdm='{0}'", this.ddlFzzt.SelectedValue);
                    break;
            }
            if (this.ddlLxr.SelectedValue != "__" && cond.IndexOf("Rdlxrbh1") == -1) cond += string.Format(" AND Rdlxrbh1='{0}'", this.ddlLxr.SelectedValue);
            if (ttbSearch.Text.Trim() != "")
                cond += String.Format(" AND (xm LIKE '%{0}%' OR xh LIKE '{0}%')", TStar.Web.Globals.FilterString(this.ttbSearch.Text));

            QueryWhere = cond;
            this.BindGrid();

            return true;
        }

        private void BindGrid()
        {
            ShowGridColumn();
            BLL.Globals.BindGrid(Grid1, PageIndex, BLL.Globals.PageSize, Bll, QueryWhere, QuerySort);
        }

        #endregion

        #region 页面及其他事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
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
                        BLL.Jcgl.Jc_xs.Delete<Model.Jcgl.Jc_xs>(pkid);
                        this.BindGrid();
                        break;
                    case "Download":
                        title = "下载";
                        string rq = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        string path = string.Format(@"{0}\{1}\{2}", Server.MapPath("~/Downloads"), rq.Substring(0,8), TStar.Web.Globals.Account.Pkid);
                        string dir = rq.Substring(8);
                        string filename = BLL.Jcgl.Jc_xs.CreatePDFSxhb(path, dir, pkid);
                        TUF.Helper.SetIFrameUrl(this.pnlFrame, "~/Xtgl/Download.aspx", filename);
                        break;
                }
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

        protected void btnBatCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string rq = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string path = string.Format(@"{0}\{1}\{2}", Server.MapPath("~/Downloads"), rq.Substring(0, 8), TStar.Web.Globals.Account.Pkid);
                string dir = rq.Substring(8);
                string where = BLL.Globals.SystemSetting.CondBmDzbbh;
                string filename = BLL.Jcgl.Jc_xs.CreateBatPDFSxhb(path, dir, TStar.Web.Globals.Account.DeptPkid, where, TStar.Web.Globals.Account.UserName);
                TUF.Helper.SetIFrameUrl(this.pnlFrame, "~/Xtgl/Download.aspx", filename);
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "批量下载失败", MessageBoxIcon.Error);
            }
        }
    }
}