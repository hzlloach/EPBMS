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
    public partial class ShowFzlc : TStar.Web.BasePage
    {
        #region 自定义属性
        
        private TStar.Web.Globals.SystemSetting.Fzzt Fzzt
        {
            get { return TU.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.Fzzt>(this.hfdFzztdm.Text); }
        }

        #endregion

        #region 自定义方法

        private void BindData()
        {
        }

        private void ShowUI()
        {
            Model.Jcgl.V_jc_xs xs = BLL.Jcgl.Jc_xs.GetEntity<Model.Jcgl.V_jc_xs>(Pkid);
            if (String.IsNullOrEmpty(xs.Pkid))
            {
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                return;
            }

            // 获取个人业绩统计
            DataTable dt = BLL.Jcgl.Jc_xs.TotalGrxm(xs.Pkid);

            // 个人基本信息
            this.lblXh.Text = xs.Xh;
            this.lblXm.Text = xs.Xm;
            this.lblFzjd.Text = xs.Fzzt;
            this.hfdFzztdm.Text = xs.Fzztdm;
            this.lblDzb.Text = xs.Dzbmc;
            this.lblZymc.Text = xs.Zymc;
            this.lblBjmc.Text = xs.Bjmc;
            this.lblXb.Text = xs.Xb;
            this.lblSfzh.Text = xs.Sfzh;
            this.lblSjhm.Text = xs.Sjhm;
            this.lblMz.Text = xs.Mz;
            this.lblJg.Text = xs.Jg;
            this.lblZw.Text = xs.Zw;
            this.lblJtdz.Text = xs.Jtdz;

            // 发展前信息
            this.lblSqrdrq.Text = xs.Sqrdrq;
            this.lblJjfzrq.Text = xs.Jjfzrq;
            this.lblLxr1.Text = xs.Rdlxrxm1;
            this.lblDxjyrq.Text = xs.Dxjyrq;
            this.lblDxkhzt.Text = xs.Dxkhzt;
            this.lblLxr2.Text = xs.Rdlxrxm2;
            this.lblXxcjpm.Text = xs.Xxcjpm;
            this.lblZhkppm.Text = xs.Zhkppm;
            this.lblBjgms.Text = xs.Bjgms;

            this.lblSxhb0.Text = dt.Rows[0]["Sxhb"].ToString();
            this.lblZyfw0.Text = dt.Rows[0]["Zyfw"].ToString();
            this.lblJshj0.Text = dt.Rows[0]["Jshj"].ToString();
            if (this.lblSxhb0.Text != "0") this.lblSxhb0.OnClientClick = wndView.GetShowReference(string.Format("../Xmgl/Sxhblbgr.aspx?fzrbh={0}&fzzt={1}", xs.Pkid, (int)TStar.Web.Globals.SystemSetting.Fzzt.Fzdx), "弹出窗－" + xs.Xm + "－思想汇报列表【发展前】");
            if (this.lblZyfw0.Text != "0") this.lblZyfw0.OnClientClick = wndView.GetShowReference(string.Format("../Xmgl/Zyfwlbgr.aspx?fzrbh={0}&fzzt={1}", xs.Pkid, (int)TStar.Web.Globals.SystemSetting.Fzzt.Fzdx), "弹出窗－" + xs.Xm + "－志愿服务列表【发展前】");
            if (this.lblJshj0.Text != "0/0/0/0") this.lblJshj0.OnClientClick = wndView.GetShowReference(string.Format("../Xmgl/Jshjlbgr.aspx?fzrbh={0}&fzzt={1}", xs.Pkid, (int)TStar.Web.Globals.SystemSetting.Fzzt.Fzdx), "弹出窗－" + xs.Xm + "－竞赛获奖列表【发展前】");
            //this.lblQtxm0.Text = dt.Rows[0]["Qtxm"].ToString();

            // 发展中信息
            string where = string.Format("Xsbh='{0}'", xs.Pkid);
            DataTable dtFzz = BLL.Jcgl.Jc_xs.GetList<Model.Lcgl.V_lc_nfzmd>(where, "Fzdxrq DESC");
            if (dtFzz.Rows.Count > 0)
            {
                this.tabFzz.Hidden = false;
                Model.Lcgl.V_lc_nfzmd ys = TU.Common.ConvertHelper.ConvertToEntity<Model.Lcgl.V_lc_nfzmd>(dtFzz.Rows[0]);
                this.lblFzdxrq.Text = ys.Fzdxrq;
                this.lblZsjg.Text = ys.Zsjg;
                this.lblDbjg.Text = ys.Dbjg;
                if (ys.Dbjgdm == "0") this.lblDbjg.CssClass = "spanRed";
                this.lblDbrq.Text = ys.Dbrq;
                this.lblDbdd.Text = ys.Dbdd;
                this.lblDbzcy.Text = ys.Dbzcy;
                this.lblDbpjyj.Text = ys.Dbpjyj;
                if (ys.Zsjgdm == "0")
                {
                    this.lblZsjg.CssClass = "spanRed";
                    this.pnlBz.Hidden = false;
                    this.lblBz.Text = ys.Zswtgyy;
                }
            }

            // 发展后信息
            TStar.Web.Globals.SystemSetting.Fzzt fzztdm = TU.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.Fzzt>(xs.Fzztdm);
            switch (fzztdm)
            {
                case TStar.Web.Globals.SystemSetting.Fzzt.Ybdy:
                    bool isYq = !string.IsNullOrEmpty(xs.Zzrq);
                    if (isYq) this.lblZzrq.CssClass = "spanRed";
                    this.lblZzrq.Text = isYq ? (xs.Zzrq + "【延期】") : DateTime.Parse(xs.Rdrq).AddYears(1).ToString("yyyy-MM-dd"); 
                    this.pnlTjYb.Hidden = false;
                    this.pnlTjZs.Hidden = true;
                    this.lblSxhb1.Text = dt.Rows[1]["Sxhb"].ToString();
                    this.lblZyfw1.Text = dt.Rows[1]["Zyfw"].ToString();
                    this.lblSlx1.Text = dt.Rows[1]["Slx"].ToString();
                    //this.lblQtxm0.Text = dt.Rows[0]["Qtxm"].ToString();
                    break;
                case TStar.Web.Globals.SystemSetting.Fzzt.Zsdy:
                    this.lblZzrq.Text = xs.Zzrq;
                    this.lblZzrq.Label = "转正日期";
                    this.pnlTjYb.Hidden = true;
                    this.pnlTjZs.Hidden = false;
                    this.lblSxhb2.Text = dt.Rows[2]["Sxhb"].ToString();
                    this.lblZyfw2.Text = dt.Rows[2]["Zyfw"].ToString();
                    this.lblSlx2.Text = dt.Rows[2]["Slx"].ToString();
                    break;
                default:
                    return;
            }
            this.tabFzh.Hidden = false;
            this.lblRdrq.Text = xs.Rdrq;
            this.lblZysbh.Text = xs.Zysbh;
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

        #endregion
    }
}