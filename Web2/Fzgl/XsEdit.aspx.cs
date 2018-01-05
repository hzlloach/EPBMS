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

namespace Web.Fzgl
{
    public partial class XsEdit : TStar.Web.BasePage
    {
        #region 自定义属性

        #endregion

        #region 自定义方法

        private void BindData()
        {
            TU.FineUI.Helper.BindDropDownList(BLL.Globals.SystemCode.DtDm_xb, ddlXbdm, "Mc", "Dm");
            TU.FineUI.Helper.BindDropDownList(BLL.Globals.SystemCode.DtDm_khzt, ddlDxkhztdm, "Mc", "Dm");

            string filter = BLL.Globals.SystemSetting.FilterDzb;
            TU.FineUI.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_dzb, ddlDzbbh, "Dzbmc", "Pkid", null, filter);

            // 关闭按钮的客户端脚本
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        private void ShowUI()
        {
            //string Pkid = TStar.Web.Globals.Account.Pkid;
            Model.Jcgl.V_jc_xs_hz xs = BLL.Jcgl.Jc_xs.GetEntity<Model.Jcgl.V_jc_xs_hz>(Pkid);
            if (String.IsNullOrEmpty(xs.Pkid))
            {
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                return;
            }

            // 获取个人业绩统计
            //DataTable dt = BLL.Jcgl.Jc_xs.TotalGrxm(xs.Pkid);

            // 个人基本信息
            this.tbxXh.Text = this.lblXhOld.Text = xs.Xh;
            this.tbxXm.Text = this.lblXmOld.Text = xs.Xm;
            this.lblFzjd.Text = xs.Fzzt;
            this.hfdFzztdm.Text = xs.Fzztdm;
            this.ddlDzbbh.SelectedValue = xs.Dzbbh;
            this.ddlDzb_SelectedIndexChanged(null, null);
            this.ddlZybh.SelectedValue = xs.Zybh;
            this.ddlZymc_SelectedIndexChanged(null, null);
            this.ddlBjbh.SelectedValue = xs.Bjbh;
            this.ddlXbdm.SelectedValue = xs.Xbdm;
            this.tbxSfzh.Text = xs.Sfzh;
            this.tbxSjhm.Text = this.lblSjhmOld.Text = xs.Sjhm;
            this.tbxMz.Text = xs.Mz;
            this.tbxJg.Text = xs.Jg;
            this.tbxZw.Text = xs.Zw;
            this.tbxJtdz.Text = xs.Jtdz;
            if (!string.IsNullOrEmpty(xs.PhotoUrl)) this.imgPhoto.ImageUrl = xs.PhotoUrl;

            // 发展前信息
            this.tbxSqrdrq.Text = xs.Sqrdrq;
            this.tbxJjfzrq.Text = xs.Jjfzrq;
            this.ddlRdlxrbh1.SelectedValue = xs.Rdlxrbh1;
            this.tbxDxjyrq.Text = xs.Dxjyrq;
            this.ddlDxkhztdm.SelectedValue = xs.Dxkhztdm;
            //this.tbxLxr2.Text = xs.Rdlxrxm2;
            this.tbxXxcjpm.Text = xs.Xxcjpm;
            this.tbxZhkppm.Text = xs.Zhkppm;
            this.tbxBjgms.Text = xs.Bjgms;

            // 发展中信息
            string where = string.Format("Xsbh='{0}'", xs.Pkid);
            DataTable dtFzz = BLL.Jcgl.Jc_xs.GetList<Model.Lcgl.V_lc_nfzmd>(where, "Fzdxrq DESC");
            if (dtFzz.Rows.Count > 0)
            {
                this.gplYsdb.Hidden = false;
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
                    //if (!string.IsNullOrEmpty(xs.Zzrq)) this.tbxZzrq.Label = "延期转正日期";
                    this.tbxZzrq.EmptyText = "延期转正才需填写";
                    break;
                case TStar.Web.Globals.SystemSetting.Fzzt.Zsdy:
                    this.tbxZzrq.Label = "转 正 日 期";
                    break;
                default:
                    return;
            }
            this.gplFzh.Hidden = false;
            this.tbxRdrq.Text = xs.Rdrq;
            this.tbxZysbh.Text = xs.Zysbh;
            this.tbxZzrq.Text = xs.Zzrq;
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

        protected void ddlDzb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filter = this.ddlDzbbh.SelectedValue == "__" ? "Bmbh='__'" : string.Format("Dzbbh IN ('__', '{0}')", this.ddlDzbbh.SelectedValue);
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_zy, this.ddlZybh, "Zymc", "Pkid", null, filter);
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJc_lxr, this.ddlRdlxrbh1, "Xm", "Pkid", null, filter);

            this.ddlZymc_SelectedIndexChanged(null, null);
        }

        protected void ddlZymc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filter = this.ddlDzbbh.SelectedValue == "__" ? "Bmbh='__'" : string.Format("Zybh IN ('__', '{0}')", this.ddlZybh.SelectedValue);
            TUF.Helper.BindDropDownList(BLL.Globals.SystemCode.DtJd_bj, this.ddlBjbh, "Bjmc", "Pkid", null, filter);
        }

        #endregion

        #region 按钮事件

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime dt;
            string errMsg = "";
            string oxh = this.lblXhOld.Text.Trim();
            string oxm = this.lblXmOld.Text.Trim();
            string osj = this.lblSjhmOld.Text.Trim();
            Model.Jcgl.Jc_xs xs = TU.Common.ConvertHelper.ConvertToEntity<Model.Jcgl.Jc_xs>("SimpleForm1", "SimpleForm2", "SimpleForm3", "SimpleForm4");
            xs.Pkid = Pkid;
            xs.Bmbh = TStar.Web.Globals.Account.DeptPkid;

            // 基本信息
            if (String.IsNullOrEmpty(xs.Xh)) errMsg += "请输入学号 ！\n";
            else if (BLL.Jcgl.Jc_xs.IsRepeated(xs.Pkid, "Xh", xs.Xh)) errMsg += "该学生已存在 ！\n";
            if (String.IsNullOrEmpty(xs.Xm)) errMsg += "请输入姓名 ！\n";
            if (xs.Dzbbh == "__") errMsg += "请选择党支部 ！\n";
            if (xs.Zybh == "__") errMsg += "请选择专业 ！\n";
            if (xs.Bjbh == "__") errMsg += "请选择班级 ！\n";
            if (xs.Xbdm == "__") errMsg += "请选择性别 ！\n";
            if (String.IsNullOrEmpty(xs.Sfzh)) errMsg += "请输入身份证号 ！\n";
            else if (xs.Sfzh.Length != 18) errMsg += "身份证号长度不正确 ！\n";
            else if (BLL.Jcgl.Jc_xs.IsRepeated(xs.Pkid, "Sfzh", xs.Sfzh)) errMsg += "身份证号已存在 ！\n";
            else
            {
                int jcxb = int.Parse(xs.Sfzh[16].ToString());
                if (xs.Xbdm == "1" && jcxb % 2 == 0 || xs.Xbdm == "2" && jcxb % 2 == 1) errMsg += "身份证号的性别位不正确 ！\n";

                xs.Csrq = string.Format("{0}.{1}.{2}", xs.Sfzh.Substring(6, 4), xs.Sfzh.Substring(10, 2), xs.Sfzh.Substring(12, 2));
                if (!DateTime.TryParse(xs.Csrq, out dt)) errMsg += "身份证号的出生日期位不正确 ！\n";
            }
            if (String.IsNullOrEmpty(xs.Sjhm)) errMsg += "请输入手机号码 ！\n";
            if (String.IsNullOrEmpty(xs.Mz)) errMsg += "请输入民族 ！\n";
            if (String.IsNullOrEmpty(xs.Jg)) errMsg += "请输入籍贯 ！\n";
            if (String.IsNullOrEmpty(xs.Zw)) errMsg += "请输入职务 ！\n";
            if (String.IsNullOrEmpty(xs.Jtdz)) errMsg += "请输入家庭地址 ！\n";
            // 发展前信息
            if (String.IsNullOrEmpty(xs.Sqrdrq)) errMsg += "请输入申请入党日期 ！\n";
            else if (!DateTime.TryParse(xs.Sqrdrq, out dt)) errMsg += "申请入党日期不正确 ！\n";
            if (String.IsNullOrEmpty(xs.Jjfzrq)) errMsg += "请输入积极分子日期 ！\n";
            else if (!DateTime.TryParse(xs.Jjfzrq, out dt)) errMsg += "积极分子日期不正确 ！\n";
            if (xs.Rdlxrbh1 == "__") errMsg += "请选择入党联系人 ！\n";
            // 发展后信息
            if (!this.gplFzh.Hidden) 
            {
                xs.Rdrq = this.tbxRdrq.Text.Trim();
                xs.Zysbh = this.tbxZysbh.Text.Trim();
                xs.Zzrq = this.tbxZzrq.Text.Trim();

                if (String.IsNullOrEmpty(xs.Rdrq)) errMsg += "请输入入党日期 ！\n";
                else if (!DateTime.TryParse(xs.Rdrq, out dt)) errMsg += "入党日期不正确 ！\n";
                if (String.IsNullOrEmpty(xs.Zysbh)) errMsg += "请输入志愿书编号 ！\n";
                else if (BLL.Jcgl.Jc_xs.IsRepeated(xs.Pkid, "Zysbh", xs.Zysbh)) errMsg += "志愿书编号已存在 ！\n";
                if (String.IsNullOrEmpty(xs.Zzrq))
                {
                    if(((int)TStar.Web.Globals.SystemSetting.Fzzt.Zsdy).ToString() == xs.Fzztdm) errMsg += "请输入转正日期 ！\n";
                }
                else if (!DateTime.TryParse(xs.Zzrq, out dt)) errMsg += this.tbxZzrq.Label.Replace(" ", "") + "不正确 ！\n";
            }
            if (errMsg.Length > 0)
            {
                Alert.Show(errMsg, "保存提示", MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (BLL.Jcgl.Jc_xs.Update(xs, xs.Xh != oxh || xs.Xm != oxm || xs.Sjhm != osj))
                    PageContext.RegisterStartupScript(Alert.GetShowInParentReference("保存成功 ！", "操作完成", MessageBoxIcon.Information) + ActiveWindow.GetHidePostBackReference());
                else
                    Alert.Show("未成功保存，请与管理员联系 ！", "保存失败", MessageBoxIcon.Warning);
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "保存失败", MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}