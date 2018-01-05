using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using TU = TStar.Utility;
using TUF = TStar.Utility.FineUI;

namespace Web.Xmgl
{
    public partial class ShEdit : TStar.Web.BasePage
    {
        #region 自定义属性

        #endregion

        #region 自定义方法

        private void BindData()
        {
            // 关闭按钮的客户端脚本
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        private void ShowUI()
        {
            Model.Xmgl.Yj_xm m = BLL.Xmgl.Yj_xm.GetEntity<Model.Xmgl.Yj_xm>(Pkid);
            if (String.IsNullOrEmpty(m.Pkid))
            {
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                return;
            }
        }

        #endregion

        #region 页面及其他事件
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
                this.ShowUI();
            }
        }

        #endregion

        #region 按钮事件

        protected bool Audit()
        {
            try
            {
                string ztdm = this.ddlShzt.SelectedValue;
                string yj = TStar.Web.Globals.FilterString(this.tbxPyyj.Text.Trim());
                switch (this.ddlShzt.SelectedIndex)
                {
                    case 0:
                        Alert.Show("请选择审核结果 ！", "审核提示", MessageBoxIcon.Warning);
                        return false;
                    case 2:
                        if (string.IsNullOrEmpty(yj))
                        {
                            Alert.Show("请填写审核拒绝意见 ！", "审核提示", MessageBoxIcon.Warning);
                            return false;
                        }
                        break;
                }

                Model.Xmgl.Yj_xm m = new Model.Xmgl.Yj_xm();
                m.Pkid = Pkid;
                m.Ztdm = int.Parse(ztdm);
                m.Shrbh = TStar.Web.Globals.Account.Pkid;
                m.Shyj = yj;
                BLL.Xmgl.Yj_xm.Audit(m);

                return true;
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "审核失败", MessageBoxIcon.Error);
                return false;
            }
        }

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            if (Audit())
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHidePostBackReference();
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("审核成功 ！", "操作完成", MessageBoxIcon.Information, ActiveWindow.GetHidePostBackReference()));
            }
        }

        #endregion
    }
}