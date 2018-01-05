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
    public partial class SxhbEdit : TStar.Web.BasePage
    {
        #region 自定义属性

        //protected string Pkid
        //{
        //    get{ return TU.Globals.GetParaValue("pkid", ""); }
        //}
        //protected bool IsAdd
        //{
        //    get { return String.IsNullOrEmpty(Pkid); }
        //}
        /// <summary>
        /// 重写时的原Pkid
        /// </summary>
        private string Glbh 
        {
            get { return TU.Globals.GetParaValue("glbh", ""); }
        }
        private string Ztdm
        {
            get { return TU.Globals.GetParaValue("ztdm", "10"); }
        }
        private string Tjxh
        {
            get { return TU.Globals.GetParaValue("tjxh", "0"); }
        }
        private string Tjjzsj
        {
            get { return TU.Globals.GetParaValue("tjjzsj", ""); }
        }

        #endregion

        #region 自定义方法

        private void BindData()
        {
            //this.btnSave.Enabled = this.btnSubmit.Enabled = !IsAdd;
            // 关闭按钮的客户端脚本
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        private void ShowUI()
        {
            if (!IsAdd)
            {
                Model.Xmgl.Xm_sxhbmx m = BLL.Dmgl.GetEntity<Model.Xmgl.Xm_sxhbmx>("Hbbh", Pkid);
                if (String.IsNullOrEmpty(m.Pkid))
                {
                    PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                    return;
                }

                this.hfdPkid.Text = Pkid;
                this.hfdHbbh.Text = m.Hbbh;
                this.tbxHbnr.Text = m.Hbnr;
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

        protected bool Save(bool isSubmit)
        {
            try
            {
                Model.Xmgl.Xm_sxhbmx m = TU.Common.ConvertHelper.ConvertToEntity<Model.Xmgl.Xm_sxhbmx>();

                if (Ztdm == ((int)TStar.Web.Globals.SystemSetting.Status.ToBeRewritten).ToString()) m.Hbbh = Glbh;
                BLL.Xmgl.Xm_sxhb.Save(m, TStar.Web.Globals.Account.Pkid, Ztdm, isSubmit);

                return true;
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "保存失败", MessageBoxIcon.Error);
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save(false))
            {
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("保存成功 ！", "操作完成", MessageBoxIcon.Information) + ActiveWindow.GetHidePostBackReference());
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Save(true))
            {
                this.SimpleForm1.Reset();
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("提交成功 ！", "操作完成", MessageBoxIcon.Information) + ActiveWindow.GetHidePostBackReference());
            }
        }

        #endregion
    }
}