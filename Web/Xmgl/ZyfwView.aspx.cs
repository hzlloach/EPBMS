using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using FineUI;
using TU = TStar.Utility;
using TUF = TStar.Utility.FineUI;

namespace Web.Xmgl
{
    public partial class ZyfwView : TStar.Web.BasePage
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
            Model.Xmgl.V_yj_xm m = BLL.Xmgl.Yj_xm.GetEntity<Model.Xmgl.V_yj_xm>("Pkid", Pkid);
            if (String.IsNullOrEmpty(m.Pkid))
            {
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                return;
            }

            // 基本信息
            this.lblXm.Text = m.Xm;
            this.lblFwrq.Text = m.Xmrq;
            this.lblFwdd.Text = m.Xmmc.Substring(11).Replace("志愿服务", "");
            this.lblFwss.Text = m.Jlsl + "（小时）";
            this.lblFwnr.Text = m.Bz;

            // 证明材料
            if (m.Fjsl == 0) this.lblHintZm.Text = "无";
            else
            {
                DataTable dtCl = BLL.Xmgl.Yj_xm.GetList<Model.Yjgl.Yj_xmzm>("Xmbh", Pkid, "aid");
                if (dtCl.Rows.Count > 0)
                {
                    this.rptZmcl.DataSource = dtCl.DefaultView;
                    this.rptZmcl.DataBind();
                }
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
    }
}