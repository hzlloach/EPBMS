using System;
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
    public partial class Homefdw : TStar.Web.BasePage
    {
        #region 自定义属性

        #endregion

        #region 自定义方法

        private void BindData()
        {
        }
        
        private void ShowUI()
        {
            string Pkid = TStar.Web.Globals.Account.Pkid;
            string bmbh = TStar.Web.Globals.Account.DeptPkid;

            // 获取发展阶段总况
            DataTable dt = BLL.Tjbb.Fzjd.TjSum(bmbh);

            // 人数
            this.lblJjfz.Text = dt.Rows[0]["Jjfz"].ToString();
            this.lblYbdy.Text = dt.Rows[0]["Ybdy"].ToString();
            this.lblZsdy.Text = dt.Rows[0]["Zsdy"].ToString();
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