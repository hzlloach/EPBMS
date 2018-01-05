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

namespace Web.Xmdr
{
    public partial class Dymdcx : TStar.Web.BasePage
    {
        #region 自定义属性

        string DefBll = "dymdcx";
        string DefSort = "Xh";
        string DefSortFirst = "Xh,Xm";
        
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
                else if (!string.IsNullOrEmpty(DefSortFirst) && DefSortFirst.IndexOf(sort) == -1) sort = DefSort + "," + sort;
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
            string filter = BLL.Globals.SystemSetting.FilterDzb;//string.Format("Bmbh IN ('__','{0}')", TStar.Web.Globals.Account.DeptPkid);
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_dzb, this.ddlDzb, "Dzbmc", "Pkid", null, filter, null, "－不限－");
            
            string tblname = TU.Globals.TripleDESEncrypt("党员名单" + BLL.Globals.SystemSetting.EncryptCode);
            btnImport.Enabled = btnClear.Enabled = btnDeleteSel.Enabled = true;
            btnImport.OnClientClick = wndImport.GetShowReference("../Xmdr/mddr.aspx?p=" + tblname) + "return false;";
            btnDeleteSel.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
            btnDeleteSel.ConfirmText = String.Format("确定要删除选中的数据行吗？");
            PageContext.RegisterStartupScript(this.wndImport.GetMaximizeReference());

            this.Grid1.PageSize = BLL.Globals.PageSize;
            this.ddlPageSize.SelectedValue = PageSize.ToString();
        }

        private bool GetQueryResult()
        {
            string cond = string.Format("Xq='{0}' AND {1}", BLL.Globals.SystemSetting.Dqxq, BLL.Globals.SystemSetting.CondBmDzbbh);// 需要学年,每次导入的是上学年的数据
            if (this.ddlDzb.SelectedIndex > 0)
                cond += String.Format(" AND Dzbbh='{0}'", this.ddlDzb.SelectedValue);
            if (ttbSearch.Text.Trim() != "")
                cond += String.Format(" AND (xm LIKE '%{0}%' OR xh LIKE '{0}%' OR sfzh LIKE '{0}')", TStar.Web.Globals.FilterString(this.ttbSearch.Text));
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
                string[] ids = TU.FineUI.Helper.GetSelectedRowIDs(this.Grid1).Split(',');
                string[] keys = TU.FineUI.Helper.GetSelectedRowKeys(this.Grid1, 1).Split(',');

                int cnt = 0;
                string bhs = "", errMsg = "";
                for (int i = 0; i < ids.Length; i++)
                {
                    if (keys[i] == ((int)TStar.Web.Globals.SystemSetting.Status.Draft).ToString())
                        bhs += "," + ids[i];
                    else
                    {
                        cnt++;
                        errMsg += String.Format("第【{0}】提交行：名单状态不可删除。\n", (i + 1));
                    }
                }

                if (cnt > 0)
                {
                    Alert.Show("存在不能删除的数据行，请检查 ！\n\n" + errMsg, "批量删除提示", MessageBoxIcon.Warning);
                    return;
                }

                BLL.Lcgl.Lc_dymd.Delete(bhs.Substring(1).Split(','));
                this.BindGrid();

                //Alert.Show("批量删除成功 ！", "删除完成", MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "批量删除失败", MessageBoxIcon.Error);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.Lcgl.Lc_dymd.ClearImport(TStar.Web.Globals.Account.DeptPkid, this.ddlDzb.SelectedIndex > 0 ? this.ddlDzb.SelectedValue : null);
                this.BindGrid();

                //Alert.Show("清空成功 ！", "清空完成", MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "清空失败", MessageBoxIcon.Error);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.Lcgl.Lc_dymd.Submit(TStar.Web.Globals.Account.DeptPkid, this.ddlDzb.SelectedIndex > 0 ? this.ddlDzb.SelectedValue : null);
                this.BindGrid();

                Alert.Show("提交成功 ！", "提交完成", MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "提交失败", MessageBoxIcon.Error);
            }
        }

        public void btnExport_Click(object sender, EventArgs e)
        {
            string BLL = "Xyyjsh";
            //Star.Globals.SetIFrameUrl(this.pnlFrame, "~/Xtgl/Export.aspx", BLL, QueryWhere);
        }

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
                        lbfModify.Icon = FineUI.Icon.Pencil;
                        lbfDelete.Icon = FineUI.Icon.BulletCross;
                        break;
                    default:
                        lbfModify.Icon = lbfDelete.Icon = FineUI.Icon.None;
                        break;
                }
                switch (lbfDelete.Icon)
                {
                    case FineUI.Icon.Decline:
                        lbfDelete.CommandName = "Delete";
                        lbfDelete.ToolTip = "删除";
                        lbfDelete.ConfirmText = "确认删除？";
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
                case "未提交":
                    lblZtmc.ForeColor = Color.Red;
                    break;
                case "已提交":
                    lblZtmc.ForeColor = Color.Blue;
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
                        BLL.Lcgl.Lc_dymd.Delete(pkid);
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