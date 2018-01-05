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
    public partial class Dxpxcx : TStar.Web.BasePage
    {
        #region 自定义属性

        string DefBll = "dxpxcx";
        string DefSort = "Xm";
        string DefSortFirst = "";

        // 项目类别编号
        private string Zbbh
        {
            get { return TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_khzbLocal, "Zbmc", "Pkid", "党校培训", ""); }
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
                else if (!string.IsNullOrEmpty(DefSortFirst) && !sort.StartsWith(DefSortFirst)) sort = DefSortFirst + "," + sort;
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
            // 绑定考核等级
            string filter = String.Format("Zbbh IN ('__','{0}')", Zbbh);
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_xmdjLocal, this.ddlDjbh, "Djmc", "Pkid", null, filter, "__", "－不限－");
            
            string tblname = TU.Globals.TripleDESEncrypt("党校培训" + BLL.Globals.SystemSetting.EncryptCode);
            btnImport.Enabled = btnClear.Enabled = btnDeleteSel.Enabled = true;
            btnImport.OnClientClick = wndImport.GetShowReference("../Xmdr/Dxpxdr.aspx?p=" + tblname + "&zb=" + Zbbh) + "return false;";
            btnDeleteSel.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
            btnDeleteSel.ConfirmText = String.Format("确定要删除选中的数据行吗？");

            this.Grid1.PageSize = BLL.Globals.PageSize;
            this.ddlPageSize.SelectedValue = PageSize.ToString();
        }

        private bool GetQueryResult()
        {
            string cond = string.Format("Zbbh='{0}' AND {1}", Zbbh, BLL.Globals.SystemSetting.CondBm);// 需要学年,每次导入的是上学年的数据
            if (this.ddlDjbh.SelectedIndex > 0)
                cond += String.Format(" AND Djbh='{0}'", this.ddlDjbh.SelectedValue);
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

                this.GetQueryResult();
            }
        }

        protected void ddlDjbh_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.Xmgl.Yj_xm.ClearImport(TStar.Web.Globals.Account.DeptPkid, Zbbh);
                this.BindGrid();
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "清空数据失败", MessageBoxIcon.Error);
            }
        }
                
        protected void btnDeleteSel_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ids = TU.FineUI.Helper.GetSelectedRowIDs(this.Grid1).Split(',');
                string[] keys = TU.FineUI.Helper.GetSelectedRowKeys(this.Grid1, 1).Split(',');

                string bhs = "", xsbhs = "";
                for (int i = 0; i < ids.Length; i++)
                {
                    bhs += "," + ids[i];
                    xsbhs += ",'" + keys[i] + "'";
                }

                BLL.Xmgl.Yj_xm.Delete(bhs.Substring(1).Split(','));
                BLL.Xmgl.Yj_xm.ResetDxpx(xsbhs.Substring(1));

                this.BindGrid();

                Alert.Show("批量删除成功 ！", "删除完成", MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "批量删除失败", MessageBoxIcon.Error);
            }
        }

        public void btnExport_Click(object sender, EventArgs e)
        {
            string BLL = "Xyyjsh";
            //Star.Globals.SetIFrameUrl(this.pnlFrame, "~/Xtgl/Export.aspx", BLL, QueryWhere);
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
                        BLL.Xmgl.Yj_xm.Delete(pkid);
                        BLL.Xmgl.Yj_xm.ResetDxpx("'" + Grid1.DataKeys[e.RowIndex][1].ToString() + "'");
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