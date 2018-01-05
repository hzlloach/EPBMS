using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using TU = TStar.Utility;
using TUF = TStar.Utility.FineUI;

namespace Web.Xtgl
{
    public partial class UserInfo : TStar.Web.BasePage
    {
        #region 自定义属性

        private string OperTitle = "修改个人信息";

        #endregion

        #region 自定义方法

        private void BindData()
        {
            // 关闭按钮的客户端脚本
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

            this.lblYhm.Text = TStar.Web.Globals.Account.UserIDName;
            this.tbxXh.Text = TStar.Web.Globals.Account.UserID;
            this.imgPhoto.ImageUrl = this.hfdImg.Text = TStar.Web.Globals.Account.UserInfo.PhotoUrl;
        }

        #endregion

        #region 页面及其他事件
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
            }
        }

        #endregion

        #region 按钮事件

        protected bool Save()
        {
            bool modPhone = false;
            string errmsg = "";
            string pkid = TStar.Web.Globals.Account.Pkid;
            string pwdold = this.tbxPwdOld.Text.Trim();
            string phone = string.IsNullOrEmpty(this.hfdCode.Text) ? "" : this.hfdCode.Text.Substring(6);
            string code = this.tbxYzm.Text.Trim().ToUpper();

            // 修改手机号码
            if (!String.IsNullOrEmpty(phone))
            {
                modPhone = true;
                if (String.IsNullOrEmpty(pwdold))
                {
                    errmsg += "请输入原始密码 ！<br/>";
                }
                else
                {
                    string encpwd = BLL.Globals.MD5Encrypt(pwdold);
                    if (encpwd != TStar.Web.Globals.Account.Password)
                    {
                        errmsg += "原始密码不正确 ！<br/>";
                    }
                }
                if (String.IsNullOrEmpty(code))
                {
                    errmsg += "请输入验证码 ！<br/>";
                }
                else if (code != this.hfdCode.Text.Substring(0, 6))
                {
                    errmsg += "输入的验证码不正确 ！<br/>";
                }
            }
            if (errmsg.Length > 0)
            {
                Alert.Show(errmsg, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                Model.Account_user u = BLL.Account.GetEntity(pkid);
                if (modPhone)
                {
                    if (code != u.VerifyCode)
                    {
                        errmsg += "\n验证码不正确 ！";
                    }
                    else if (DateTime.Now.Ticks > u.Timestamp)
                    {
                        errmsg += "\n验证码已过期 ！";
                    }
                    if (errmsg.Length > 0)
                    {
                        Alert.Show(errmsg, MessageBoxIcon.Error);
                        return false;
                    }

                    BLL.Account.UpdateMobile(u.Pkid, phone);
                }
                if (this.hfdImg.Text != this.imgPhoto.ImageUrl)
                {
                    string photourl = ChangeFile(this.imgPhoto.ImageUrl);
                    BLL.Account.UpdatePhoto(u.Pkid, photourl);
                }
                return true;
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, OperTitle + "失败", MessageBoxIcon.Error);
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                PageContext.RegisterStartupScript(Alert.GetShowInParentReference(OperTitle + "成功", OperTitle + "完成", MessageBoxIcon.Information) + ActiveWindow.GetHidePostBackReference());
            }
        }

        #endregion
        
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
                    errMsg += String.Format("上传的文件必须为图片(格式：jpg、png)。\n");
                }
                else
                {
                    System.Drawing.Image image = System.Drawing.Image.FromStream(f.InputStream);
                    if (image.Height < 112 || image.Width < 80)
                    {
                        flag = false;
                        errMsg += String.Format("上传文件的尺寸太小，建议至少 80 * 112。\n");
                    }
                }
            }
            else// if (f.ContentType.ToLower() != "application/pdf")
            {
                flag = false;
                errMsg += String.Format("上传的文件必须为图片(格式：jpg、png)。\n");//PDF文件或
            }
            if (flag && f.ContentLength > BLL.Globals.SystemSetting.MaxLenPhoto)
            {
                flag = false;
                errMsg += String.Format("上传文件的大小超过{0}K。\n", BLL.Globals.SystemSetting.MaxLenPhoto / 1024);
            }

            if (errMsg.Length > 0)
                Alert.Show(errMsg, "上传提示", MessageBoxIcon.Warning);

            return flag;
        }
        public string UploadFile(HttpPostedFile f)
        {
            string filename = null;
            try
            {
                // 上传路径：Uploads/Photo/部门编号
                string bmbh = TStar.Web.Globals.Account.DeptPkid;
                string xsbh = TStar.Web.Globals.Account.Pkid;
                string uid = TStar.Web.Globals.Account.UserID;
                string dir = String.Format("Uploads/Photo/Temp/");
                string path = Server.MapPath("~/" + dir);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                filename = f.FileName.Substring(f.FileName.Length - 4, 4).ToLower();
                string fullfile = String.Format("{0}_{1}{2}", bmbh + uid, DateTime.Now.Ticks, filename);
                filename = path + fullfile;
                f.SaveAs(filename);
                return "/" + dir + fullfile;
            }
            catch (Exception err)
            {
                Alert.Show(err.Message, "上传附件失败", MessageBoxIcon.Error);
                return "/Uploads/Photo/default.jpg";
            }
        }

        private static string ChangeFile(string photourl)
        {
            string url = photourl;
            int idx = url.LastIndexOf("/");
            string dir = url.Substring(0, idx - 5); // 不带/，并去掉Temp/
            string ext = url.Substring(url.LastIndexOf('.'));
            string oldfilename = url.Substring(idx); // 以/开头
            string bmbh = "/" + oldfilename.Substring(1, 32);
            string newfilename = "/" + oldfilename.Substring(33, oldfilename.IndexOf('_') - 33) + ext;
            string path = TU.WebHelper.MapPath("~" + dir);
            if (!Directory.Exists(path + bmbh)) Directory.CreateDirectory(path + bmbh);
            File.Move(path + "/Temp" + oldfilename, path + bmbh + newfilename);

            return dir + bmbh + newfilename;
        }

        protected void fudPhoto_FileSelected(object sender, EventArgs e)
        {
            if (fudPhoto.HasFile)
            {
                HttpPostedFile f = fudPhoto.PostedFile;

                // 检查上传文件信息
                if (CheckFile(f))
                {
                    // 上传文件
                    string filename = UploadFile(f);
                    if (!filename.EndsWith("/default.jpg"))
                        this.imgPhoto.ImageUrl = filename;
                }
                fudPhoto.Reset();
            }
        }
    }
}