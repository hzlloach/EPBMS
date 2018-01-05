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
    public partial class ZyfwEdit : TStar.Web.BasePage
    {
        #region 自定义属性

        private string StrTxsm = "申报项目的时间必须为近 3 个月内；\n项目基本信息填写保存后，才能上传项目证明材料；\n每个项目页输入完后点击上方的【保存】按钮进行保存。";
        private string[] szs = { "一", "二", "三", "四", "五" };
        private TextBox[] tbs = null;//new ExtAspNet.TextBox[] { tbxTitle1, tbxTitle2, tbxTitle3, tbxTitle4, tbxTitle5 };
        private HtmlInputHidden[] hhfs = null;//new ExtAspNet.HiddenField[] { hidAttach1, hidAttach2, hidAttach3, hidAttach4, hidAttach5 };
        private HiddenField[] hfds = null;
        private FineUI.FileUpload[] fuds = null;
        List<int> lstXhs = new List<int>();

        //protected string Pkid
        //{
        //    get { return TU.Globals.GetParaValue("pkid", ""); }
        //}
        //protected bool IsAdd
        //{
        //    get { return String.IsNullOrEmpty(Pkid); }
        //}
        // 顶级指标
        private string Zbbh
        {
            get { return TU.Globals.GetParaValue("zb", ""); }
        }
        private string Xmbh
        {
            get { return this.hfdPkid.Text; }
            set { this.hfdPkid.Text = value; }
        }
        private bool HasSaved
        {
            get { return !string.IsNullOrEmpty(this.hfdPkid.Text); }
        }

        #endregion

        #region 自定义方法

        private void BindData()
        {
            string yjzbdm = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_khzbLocal, "Pkid", "Zbdm", Zbbh, "");
            if (String.IsNullOrEmpty(yjzbdm))
            {
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                return;
            }

            // 绑定项目类别
            TUF.Helper.BindTreeView(BLL.Globals.SystemCode.DtTree_khzbLocal, this.ddlZbbh, "id", "－请选择－");
            this.ddlZbbh.SelectedValue = Zbbh;
            this.lblZbmc.Text = this.ddlZbbh.SelectedText;
            this.tbxBt1.Required = this.tbxBt1.ShowRedStar = true;

            this.ShowTxsm("");

            this.dtpXmrq.MinDate = BLL.Globals.SystemSetting.MinDate;//DateTime.Now.AddMonths(-3);//DateTime.Parse("2013-01-01");
            this.dtpXmrq.MaxDate = DateTime.Now;

            // 关闭按钮的客户端脚本
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        private void ShowUI()
        {
            this.lblFzr.Text = TStar.Web.Globals.Account.UserName;
            this.lblFzzt.Text = TStar.Web.Globals.Account.UserInfo.Fzzt;
            this.hfdFzztdm.Text = TStar.Web.Globals.Account.UserInfo.Fzztdm;
            this.hfdFzrbh.Text = TStar.Web.Globals.Account.Pkid;

            if (!IsAdd)
            {
                Model.Xmgl.Yj_xm xm = BLL.Dmgl.GetEntity <Model.Xmgl.Yj_xm>(Pkid);
                if (String.IsNullOrEmpty(xm.Pkid))
                {
                    PageContext.RegisterStartupScript(Alert.GetShowInParentReference("页面参数不正确 ！", "打开失败", MessageBoxIcon.Error) + ActiveWindow.GetHideReference());
                    return;
                }
                if (!BLL.Globals.SystemSetting.CanModifyDelete(xm.Ztdm))
                {
                    PageContext.RegisterStartupScript(Alert.GetShowInParentReference("该项目已不能修改 ！", "提示", MessageBoxIcon.Information) + ActiveWindow.GetHideReference());
                    return;
                }

                // 项目基本信息
                Xmbh = xm.Pkid;
                this.dtpXmrq.SelectedDate = DateTime.Parse(xm.Xmrq);
                this.tbxXmmc.Text = xm.Xmmc;
                this.tbxFwdd.Text = xm.Xmmc.Substring(11, xm.Xmmc.Length - 15);
                this.tbxJlsl.Text = xm.Jlsl.ToString();
                this.tbxBz.Text = xm.Bz;
                
                // 证明材料信息
                DataTable dtCl = BLL.Dmgl.GetList<Model.Yjgl.Yj_xmzm>("Xmbh", xm.Pkid, "aid");
                for (int i = 0; i < dtCl.Rows.Count; i++)
                {
                    string pkid = dtCl.Rows[i]["Pkid"].ToString();
                    string clbt = dtCl.Rows[i]["Clbt"].ToString();
                    string cflj = dtCl.Rows[i]["Cflj"].ToString();
                    BindZmcl(i, tbs[i], hfds[i], hhfs[i], pkid, clbt, cflj);
                }
            }
        }
        
        private void ShowTxsm(string hint)
        {
            if (String.IsNullOrEmpty(hint)) hint = "";
            string[] hs = (hint + "\n" + StrTxsm).Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int k = 1;
            hint = "";
            foreach (string sm in hs)
            {
                int idx = 0;// sm.IndexOf('、') + 1;
                hint += "\n" + (k++) + "、" + (idx > 0 ? sm.Substring(idx) : sm);
            }
            this.lblTxsm.Text = hint.Substring(1).Replace("\n", "<br/>");
        }

        private void BindZmcl(int no, TextBox tbxBt, HiddenField hdfYs, HtmlInputHidden hhfLj, params string[] param)//ContentPanel divView, 
        {
            string pkid = param[0];
            string clbt = param[1];
            string cflj = param[2];

            tbxBt.Text = clbt;
            hdfYs.Text = pkid + "|" + clbt + "|" + cflj;
            hhfLj.Value = cflj;

            //ivView.Hidden = false;
            //btnView.OnClientClick = this.imgPhoto.ImageUrl//String.Format("X('Window2').box_show('ShowAttach.aspx?url={0}','弹出窗－附件浏览－附件材料{1}：{2}');return false;", TStar.Utility.Globals.EncodeUrl(url), szs[no], title);
            //Window2.GetShowReference(String.Format("ShowAttach.aspx?url={0}", TStar.Utility.Globals.EncodeUrl(url)), "弹出窗－附件浏览－" + title) + "return false;";
            //btnDel.OnClientClick = String.Format("$('id$=divView{0}_content').hide();", btnDel.ID[btnDel.ID.Length-1]) + "return false;";
        }

        #region 保存项目

        private void SaveXmxx()
        {
            // 获取指标显示栏目
            //DataRow dr = TU.Globals.FindRow(BLL.Globals.SystemCode.DtJd_zbxsLocal.DefaultView, "Zbbh", this.ddlEjzb.SelectedValue);
            //Model.Dmgl.Jd_zbxs zbxs = TU.ConvertHelper.ConvertToEntity<Model.Dmgl.Jd_zbxs>(dr);

            //bool hasXmmc = !String.IsNullOrEmpty(zbxs.Mcbt); // 需输入名称
            //bool hasJlsl = !String.IsNullOrEmpty(zbxs.Slbt); // 需输入数量
            //bool hasXmrq = !String.IsNullOrEmpty(zbxs.Rqbt); // 需输入日期

            bool flag = true;
            string fzztdm = this.hfdFzztdm.Text;// this.lblDqxn.Text;
            string bmbh = TStar.Web.Globals.Account.DeptPkid;
            string zbbh = this.ddlZbbh.SelectedValue;
            //string ztbh = this.ddlXmzt.SelectedValue;
            //if (this.ddlXmzt.Hidden) ztbh = cbxXmzt.Checked ? this.ddlXmzt.Items[1].Value : "__";
            string fzrbh = this.hfdFzrbh.Text;// TStar.Globals.Account.Pkid;
            //string fzrxm = this.lblFzr.Text;// TStar.Globals.Account.UserName;
            string fwdd = TU.Globals.FilterString(this.tbxFwdd.Text);
            string fwss = this.tbxJlsl.Text;
            string xmmc = "";
            DateTime? dtXmrq = this.dtpXmrq.SelectedDate;
            //string jzrq = xmrq;
            //int jlsl = 0;
            //int.TryParse(this.tbxJlsl.Text.Trim(), out jlsl);
            string bz = TU.Globals.FilterString(this.tbxBz.Text);
            string bcsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //int canExist = 0;
            string errMsg = "";
                        
            if (dtXmrq == null)
            {
                flag = false;
                errMsg += "请选择项目日期 ！\n";
            }
            //else if (dtXmrq < DateTime.Now.AddMonths(-3) || dtXmrq > DateTime.Now)
            //{
            //    errMsg += "请选择近三个月内的日期！\n";
            //}
            if (String.IsNullOrEmpty(fwdd))
            {
                flag = false;
                errMsg += "请输入服务地点 ！\n";
            }
            if (String.IsNullOrEmpty(fwss))
            {
                errMsg += "请输入服务时数 ！\n";
            }
            if (String.IsNullOrEmpty(bz))
            {
                errMsg += "请输入服务内容 ！\n";
            }
            if (flag)
            {
                xmmc = dtXmrq.Value.ToString("yyyy年MM月dd日") + fwdd + "志愿服务";
                if (BLL.Xmgl.Yj_xm.Exist(Pkid, new string[] { "Fzztdm", "Bmbh", "Zbbh", "Fzrbh", "Xmmc" }, new string[] { fzztdm, bmbh, zbbh, fzrbh, xmmc }))
                {
                    errMsg += "输入的项目信息已存在 ！\n";
                }
            }

            if (errMsg.Length > 0)
            {
                Alert.Show(errMsg, "保存提示", MessageBoxIcon.Warning);
                return;
            }

            Model.Xmgl.Yj_xm xm = new Model.Xmgl.Yj_xm();
            xm.Pkid = Pkid;
            xm.Fzztdm = fzztdm;
            xm.Bmbh = bmbh;
            xm.Zbbh = zbbh;
            xm.Fzrbh = fzrbh;
            xm.Xmmc = xmmc;
            xm.Jlsl = int.Parse(fwss);
            xm.Xmrq = xm.Jzrq = dtXmrq.Value.ToString("yyyy-MM-dd");
            xm.Bz = bz;
            xm.Bcsj = bcsj;

            try
            {
                BLL.Xmgl.Yj_xm.Save(xm);
                Xmbh = xm.Pkid;
                this.tabStrip1.ActiveTabIndex = 1;

                Alert.Show("项目基本信息保存成功 ！", "操作完成", MessageBoxIcon.Information);

                // 修改关闭按钮的客户端脚本
                this.btnClose.OnClientClick = ActiveWindow.GetHidePostBackReference();
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "保存项目基本信息失败", MessageBoxIcon.Error);
                return;
            }
        }
        private void SaveXmzm(string xmbh)
        {
            string errMsg = "";
            List<string> lstFiles = new List<string>();

            // 检查附件信息
            for (int i = 0; i < this.tbs.Length; i++)
            {
                errMsg += CheckInfo(i);
            }
            if (errMsg.Length > 0)
            {
                Alert.Show(errMsg, "上传提示", MessageBoxIcon.Warning);
                return;
            }

            // 保存附件信息  
            for (int no = 0, i = 0; i < this.lstXhs.Count; i++)
            {
                try
                {
                    no = this.lstXhs[i];
                    string zy = hfds[no].Text; // 原始附件信息
                    string clbt = tbs[no].Text;
                    string cflj = hhfs[no].Value;
                    string[] zys = zy.Split('|');
                    bool isDel = cflj.StartsWith("Del@") && !String.IsNullOrEmpty(zy);
                    bool hasFile = !String.IsNullOrEmpty(cflj) && !cflj.StartsWith("Del@");
                    bool isAdd = String.IsNullOrEmpty(zy) && hasFile;
                    bool isMod = !isAdd && !isDel && (clbt != zys[1] || cflj != zys[2]);

                    if (isAdd || isDel || isMod)
                    {
                        // 保存附件
                        Model.Yjgl.Yj_xmzm zm = new Model.Yjgl.Yj_xmzm();
                        zm.Pkid = zys[0];
                        zm.Xmbh = Xmbh;
                        zm.Clbt = clbt;
                        zm.Cflj = hhfs[no].Value;
                        if (!isAdd) zm.OldCflj = zys[2];
                        BLL.Xmgl.Yj_xmzm.Save(zm);
                    }
                }
                catch (Exception err)
                {
                    Alert.Show(err.Message, string.Format("保存附件{0}失败", szs[no]), MessageBoxIcon.Error);
                    return;
                }
            }

            // 清理临时文件
            string bmbh = TStar.Web.Globals.Account.DeptPkid;
            string dzbbh = TStar.Web.Globals.Account.UserInfo.Dzbbh;
            string xsbh = TStar.Web.Globals.Account.Pkid;
            ClearTmpFiles(bmbh, dzbbh, xsbh, Xmbh);

            PageContext.RegisterStartupScript(Alert.GetShowInParentReference("项目证明材料保存成功 ！", "操作完成", MessageBoxIcon.Information) + ActiveWindow.GetHidePostBackReference());
        }

        #endregion

        #region 上传文件

        // no从0开始
        public string CheckInfo(int no)
        {
            string errMsg = "";
            string xh = szs[no];
            string bt = this.tbs[no].Text.Trim();
            bool isOk = false;
            bool isDel = this.hhfs[no].Value.StartsWith("Del@");
            bool isFirst = no == 0 && this.tbs[no].Required;
            bool hastitle = !string.IsNullOrEmpty(TStar.Web.Globals.FilterString(bt));
            bool hasfile = !String.IsNullOrEmpty(hhfs[no].Value) && !isDel;
            if (isFirst || hasfile || hastitle)
            {
                isOk = true;
                if ((isFirst || hasfile) && !hastitle)
                {
                    isOk = false;
                    errMsg += String.Format("请输入附件{0}的标题 ！\n", xh);
                }
                else if (TU.Globals.IsInclude(bt, " ,　,\\,/,:,*,?,',\",<,>"))
                {
                    isOk = false;
                    errMsg += String.Format("附件{0}的名称包含：空格, \\, /, :, *, ?, ', \", <, > 字符 ！\n", xh);
                }
                if ((isFirst || hastitle) && !hasfile)
                {
                    isOk = false;
                    errMsg += String.Format("请选择附件{0}的上传文件 ！\n", xh);
                }
            }
            if (isOk || isDel) lstXhs.Add(no);
            return errMsg;
        }
        public bool CheckFile(HttpPostedFile f)
        {
            bool flag = true;
            string errMsg = "";
            string file = f.FileName.ToLower();
            if (f.ContentType.ToLower().StartsWith("image")) // 是图片
            {
                if (!file.EndsWith(".jpg") && !file.EndsWith(".png"))
                {
                    flag = false;
                    errMsg += String.Format("上传的附件必须为图片(格式：jpg、png)。\n");
                }
                else
                {
                    System.Drawing.Image image = System.Drawing.Image.FromStream(f.InputStream);
                    if (image.Height < 600 || image.Width < 800)
                    {
                        flag = false;
                        errMsg += String.Format("上传附件的尺寸太小，建议至少 800 * 600。\n");
                    }
                }
            }
            else// if (f.ContentType.ToLower() != "application/pdf")
            {
                flag = false;
                errMsg += String.Format("上传的附件必须为图片(格式：jpg、png)。\n");//PDF文件或
            }
            if (flag && f.ContentLength > BLL.Globals.SystemSetting.MaxLenImg)
                errMsg += String.Format("上传附件的大小超过{1}M。\n", BLL.Globals.SystemSetting.MaxLenImg);

            if (errMsg.Length > 0)
                Alert.Show(errMsg, "上传提示", MessageBoxIcon.Warning);

            return flag;
        }
        public string UploadFile(string xmbh, int no, HttpPostedFile f)//DateTime.Now.ToString("yyyyMM")
        {
            string filename = null;
            try
            {
                // 上传路径：Uploads/Zmcl/部门编号/党支部编号/学生编号/项目编号
                string bmbh = TStar.Web.Globals.Account.DeptPkid;
                string dzbbh = TStar.Web.Globals.Account.UserInfo.Dzbbh;
                string xsbh = this.hfdFzrbh.Text;
                string dir = String.Format("Uploads/Zmcl/{0}/{1}/{2}/{3}/", bmbh, dzbbh, xsbh, xmbh);
                string path = Server.MapPath("~/" + dir);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                filename = f.FileName.Substring(f.FileName.Length - 4, 4).ToLower();
                string fullfile = String.Format("temp{0}_{1}{2}", no, DateTime.Now.Ticks, filename);
                filename = path + fullfile;
                f.SaveAs(filename);
                return "/" + dir + fullfile;
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "上传附件失败", MessageBoxIcon.Error);
                return "/res/images/nophoto.png";
            }
        }
        private void ClearTmpFiles(string bmbh, string dzbbh, string xsbh, string xmbh)
        {
            string path = String.Format("~/Uploads/Zmcl/{0}/{1}/{2}/{3}/", bmbh, dzbbh, xsbh, xmbh);
            string dir = TU.WebHelper.MapPath(path);

            TU.Globals.DeleteBatFile(dir, "temp");
        }

        #endregion

        #endregion

        #region 页面及其他事件

        protected void Page_Load(object sender, EventArgs e)
        {
            tbs = new TextBox[] { tbxBt1, tbxBt2, tbxBt3 };
            hfds = new HiddenField[] { hfdYs1, hfdYs2, hfdYs3 };
            hhfs = new HtmlInputHidden[] { hfdLj1, hfdLj2, hfdLj3 };
            fuds = new FineUI.FileUpload[] { fudFj1, fudFj2, fudFj3 };

            if (!IsPostBack)
            {
                this.BindData();
                this.ShowUI();
            }
        }

        protected void TabStrip1_TabIndexChanged(object sender, EventArgs e)
        {
            if (tabStrip1.ActiveTabIndex != 0 && !HasSaved)
            {
                tabStrip1.ActiveTabIndex = 0;
                Alert.Show("请先保存项目基本信息 ！", "操作提示", MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region 按钮事件

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.tabStrip1.ActiveTabIndex)
                {
                    case 0:
                        SaveXmxx();
                        if (HasSaved) this.btnClose.OnClientClick = ActiveWindow.GetHidePostBackReference();
                        break;
                    case 1:
                        SaveXmzm(Xmbh);
                        break;
                }
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "保存失败", MessageBoxIcon.Error);
                return;
            }
        }

        protected void fudFj_FileSelected(object sender, EventArgs e)
        {
            FineUI.FileUpload fud = sender as FineUI.FileUpload;
            HttpPostedFile f = fud.PostedFile;
            int no = int.Parse(fud.ClientID.Substring(fud.ClientID.Length - 1, 1)) - 1;

            // 检查上传文件信息
            if (CheckFile(f))
            {
                // 上传文件
                string filename = UploadFile(Xmbh, no, f);
                if (!filename.EndsWith("/nophoto.png")) PageContext.RegisterStartupScript(string.Format("setFileUpload({0}, '{1}');", no + 1, filename));

                //Alert.Show("上传附件成功 ！", "上传完成", MessageBoxIcon.Information);
            }
        }

        #endregion
    }
}