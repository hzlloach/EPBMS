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
    public partial class Zyfw : TStar.Web.BasePage
    {
        #region 自定义属性

        string DefBll = "yjxmcx";
        string DefSort = "Ztdm,Zbdm,Xmrq DESC";
        string DefSortFirst = "";

        // 项目类别编号
        private string Zbbh
        {
            get { return TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_khzbLocal, "Zbmc", "Pkid", "志愿服务", ""); }
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
            btnAddNew.OnClientClick = wndEdit.GetShowReference("ZyfwEdit.aspx?zb=" + Zbbh, "弹出窗－新增") + "return false;";
            btnDeleteSel.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
            btnDeleteSel.ConfirmText = String.Format("确定要删除选中的数据行吗？");
            btnSubmit.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
            btnSubmit.ConfirmText = String.Format("确定要提交选中的数据行吗？");
            
            this.Grid1.PageSize = BLL.Globals.PageSize;
            this.ddlPageSize.SelectedValue = PageSize.ToString();
            PageContext.RegisterStartupScript(this.wndEdit.GetMaximizeReference());
            PageContext.RegisterStartupScript(this.wndView.GetMaximizeReference());
        }

        protected string GetShzt(string ztdm)
        {
            int scZtdm = (int)TG.SystemSetting.Status.AuditRefused;
            int tjZtdm = (int)TG.SystemSetting.Status.AuditAccepted;
            switch (TU.Common.ConvertHelper.EnumParse<TG.SystemSetting.Status>(ztdm))
            {
                case TG.SystemSetting.Status.Deleted: return "已删除";
                case TG.SystemSetting.Status.Submitted: return "已提交";
                case TG.SystemSetting.Status.ToBeModified: return "退回修改";
                case TG.SystemSetting.Status.Archived: return "已归档";
                default:
                    int zt = TU.Globals.Parse2Int(ztdm, 0);
                    if (zt >= tjZtdm) return "审核通过";
                    else if (zt <= scZtdm) return "审核拒绝";
                    else return "未提交";
            }
        }

        private bool GetQueryResult()
        {
            string cond = string.Format("Zbbh='{0}' AND {1}", Zbbh, BLL.Globals.SystemSetting.CondXsbhFzzt);// String.Format("Xn='{0}' AND SUBSTRING(Zbdm,1,{1})='{2}' AND Fzrbh='{3}' AND Shyj<>'历年业绩分配'", TStar.Globals.SystemSettings.Dqxn, idx, Djzbdm.Substring(0, idx), TStar.Globals.Account.Pkid);
            
            if (ttbSearch.Text.Trim() != "")
                cond += String.Format(" AND (Xmmc LIKE '%{0}%')", TStar.Web.Globals.FilterString(this.ttbSearch.Text));
            if (cond.Length > 0) QueryWhere = cond;
            else QueryWhere = "";

            //string cond = string.Format("Fzrbh='{0}' AND Fzztdm=(SELECT Fzztdm FROM jc_xs x WHERE x.Pkid=Fzrbh)", TStar.Web.Globals.Account.Pkid);

            //// 构造查询条件
            //switch(this.ddlKhzb.SelectedIndex)
            //{
            //    case 0:
            //        break;
            //    case 1: // 未提交
            //        cond += string.Format(" AND Ztdm BETWEEN {0} AND {1}", (int)TG.SystemSetting.Status.Draft, (int)TG.SystemSetting.Status.Revoked);
            //        break;
            //    case 2: // 未评阅
            //        cond += string.Format(" AND Ztdm = {0}", (int)TG.SystemSetting.Status.Submitted);
            //        break;
            //    case 3: // 退回修改
            //        cond += string.Format(" AND Ztdm IN ({0},{1},{2})", (int)TG.SystemSetting.Status.ToBeModified, (int)TG.SystemSetting.Status.InModify, (int)TG.SystemSetting.Status.Modified);
            //        break;
            //    case 4: // 退回重写
            //        cond += string.Format(" AND Ztdm IN ({0},{1},{2})", (int)TG.SystemSetting.Status.ToBeRewritten, (int)TG.SystemSetting.Status.InRewrite, (int)TG.SystemSetting.Status.ReWritten);
            //        break;
            //    case 5: // 评阅通过
            //        cond += string.Format(" AND Ztdm >= {0}", (int)TG.SystemSetting.Status.Audited);
            //        break;
            //}
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ids = TU.FineUI.Helper.GetSelectedRowIDs(this.Grid1).Split(',');
                string[] keys = TU.FineUI.Helper.GetSelectedRowKeys(this.Grid1, 1,2,3).Split(',');

                int cnt = 0;
                string bhs = "", errMsg = "", msg = "";
                for (int i = 0; i < ids.Length; i++)
                {
                    msg = "";
                    string[] s = keys[i].Split('|');
                    bool canSubmit = BLL.Globals.SystemSetting.CanModifyDelete(s[0]);
                    //bool isWtj = BLL.Globals.IsNonSubmitted(s[0]);
                    //bool isBack = BLL.Globals.IsBack(s[0]);
                    if (!canSubmit)
                    {
                        //if (isBack && !BLL.Globals.CanMod) msg += "；退回修改已截止";
                        //else if (isWtj && !BLL.Globals.CanFill) msg += "；申报已截止";
                        //else 
                            msg += "；项目状态不可提交";
                    }
                    else if (!BLL.Globals.SystemSetting.CanSubmit(bool.Parse(s[1]), int.Parse(s[2]))) msg += "；缺少附件材料";
                    if (msg.Length == 0) bhs += "," + ids[i];
                    else
                    {
                        cnt++;
                        errMsg += String.Format("第【{0}】提交行：{1}。\n", (i + 1), msg.Substring(1));
                    }
                }
                if (cnt > 0)
                {
                    Alert.Show("存在不能提交的数据行，请检查 ！\n\n" + errMsg, "提交提示", MessageBoxIcon.Warning);
                    return;
                }

                BLL.Xmgl.Yj_xm.Submit(bhs.Substring(1).Split(','));
                this.BindGrid();

                Alert.Show("提交成功 ！", "提交完成", MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "提交失败", MessageBoxIcon.Error);
            }
        }

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
                    if (BLL.Globals.SystemSetting.CanModifyDelete(keys[i]))
                        bhs += "," + ids[i];
                    else
                    {
                        cnt++;
                        errMsg += String.Format("第【{0}】提交行：项目状态不可删除。\n", (i + 1));
                    }
                }

                if (cnt > 0)
                {
                    Alert.Show("存在不能删除的数据行，请检查 ！\n\n" + errMsg, "批量删除提示", MessageBoxIcon.Warning);
                    return;
                }

                string bmbh = TStar.Web.Globals.Account.DeptPkid;
                string dzbbh = TStar.Web.Globals.Account.UserInfo.Dzbbh;
                string xsbh = TStar.Web.Globals.Account.Pkid;
                BLL.Xmgl.Yj_xm.Delete(bmbh, dzbbh, xsbh, bhs.Substring(1).Split(','));
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
                    case TG.SystemSetting.Status.InModify:
                    case TG.SystemSetting.Status.Revoked:
                        lbfModify.Icon = FineUI.Icon.Pencil;
                        lbfDelete.Icon = FineUI.Icon.BulletCross;
                        break;
                    case TG.SystemSetting.Status.Submitted:
                        lbfModify.Icon = FineUI.Icon.None;
                        lbfDelete.Icon = FineUI.Icon.PencilGo;//LaptopDelete;
                        break;
                    case TG.SystemSetting.Status.ToBeModified:
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
                case "未提交":
                    lblZtmc.ForeColor = Color.Black;
                    break;
                case "已提交":
                    lblZtmc.ForeColor = Color.Blue;
                    break;
                case "已删除":
                case "审核拒绝":
                case "退回修改":
                    lblZtmc.ForeColor = Color.Red;
                    break;
                case "审核通过":
                case "已归档":
                default:
                    lblZtmc.ForeColor = Color.Green;
                    break;
            }

            System.Web.UI.WebControls.HyperLink lblXmmc = Grid1.Rows[e.RowIndex].FindControl("lblXmmc") as System.Web.UI.WebControls.HyperLink;
            lblXmmc.Attributes["onclick"] = wndView.GetShowReference(String.Format("ZyfwView.aspx?pkid={0}", Grid1.DataKeys[e.RowIndex][0].ToString()), "弹出窗－浏览");
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