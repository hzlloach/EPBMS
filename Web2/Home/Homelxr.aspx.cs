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
    public partial class Homelxr : TStar.Web.BasePage
    {
        #region 自定义属性

        #endregion

        #region 自定义方法

        private void BindData()
        {
        }

        private void ShowUI()
        {
            DataRow dr = BLL.Tjbb.Zk.TjZkByLxr(TStar.Web.Globals.Account.Pkid);
            if (dr == null) return;

            string[] s = dr["Rs"].ToString().Split('/');
            this.lblJjfz.Text = s[0];
            this.lblYbdy.Text = s[1];
            this.lblZsdy.Text = s[2];

            s = dr["Hbs"].ToString().Split('/');
            this.lblSxhb.Text = (int.Parse(s[0]) + int.Parse(s[1]) + int.Parse(s[2])).ToString();
            this.lblSxhb.ToolTip = string.Format("积极分子：{0}，预备党员：{1}。",  s[0], s[1]);

            s = dr["Fws"].ToString().Split('/');
            this.lblZyfw.Text = (int.Parse(s[0]) + int.Parse(s[1]) + int.Parse(s[2])).ToString();
            this.lblZyfw.ToolTip = string.Format("积极分子：{0}，预备党员：{1}，正式党员：{2}。", s[0], s[1], s[2]);

            s = dr["Lxs"].ToString().Split('/');
            this.lblSlx.Text = (int.Parse(s[0]) + int.Parse(s[1]) + int.Parse(s[2])).ToString();
            this.lblSlx.ToolTip = string.Format("联系班级：{0}，联系寝室：{1}，联系学生：{2}。", s[0], s[1], s[2]);

            s = dr["Hjs"].ToString().Split('/');
            this.lblJshj.Text = (int.Parse(s[0]) + int.Parse(s[1]) + int.Parse(s[2]) + int.Parse(s[3])).ToString();
            this.lblJshj.ToolTip = string.Format("国家级：{0}，省级：{1}，校级：{2}，院级：{3}。", s[0], s[1], s[2], s[3]);

            s = dr["Qts"].ToString().Split('/');
            this.lblQtxm.Text = (int.Parse(s[0]) + int.Parse(s[1]) + int.Parse(s[2])).ToString();
            this.lblQtxm.ToolTip = string.Format("积极分子：{0}，预备党员：{1}，正式党员：{2}。", s[0], s[1], s[2]);

            //s = dr["Hjs"].ToString().Split('/');
            //this.lblJshj.Text = (int.Parse(s[0]) + int.Parse(s[1]) + int.Parse(s[2])).ToString();

            lblSxhb0.Text = dr["DsHbs"].ToString();
            lblZyfw0.Text = dr["DsFws"].ToString();
            lblSlx0.Text = dr["DsLxs"].ToString();
            lblJshj0.Text = dr["DsHjs"].ToString();
            lblQtxm0.Text = dr["DsQts"].ToString();
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