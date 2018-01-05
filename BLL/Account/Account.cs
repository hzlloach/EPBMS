using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using TU = TStar.Utility;
using TUD = TStar.Utility.DataSource;

namespace BLL
{
    public class Account
    {
        /// <summary>
        /// 根据给定的Pkid以及指定字段及其值判断是否存在相应记录
        /// </summary>
        public static bool Exists(string pkid, string field, string key)
        {
            return TUD.SQLHelper.Exists<Model.Account_user>("Pkid", pkid, new string[] { field }, new object[] { key });
        }

        /// <summary>
        /// 新增联系人用户
        /// </summary>
        public static int AddLxr(string pkid, string bmbh, string dzbbh, string gh, string xm, string sjhm, bool issj)
        {
            try
            {
                Model.Account_user u = new Model.Account_user();
                u.Pkid = pkid;
                u.DeptID = TU.Globals.BindSystemCode(Globals.SystemCode.DtJd_bm, "Pkid", "Bmdm", bmbh, ""); ;
                u.UserID = gh;
                u.UserName = xm;
                u.Password = BLL.Globals.MD5Encrypt(sjhm.Substring(5, 6));
                u.Mobile = sjhm;
                u.Level = "01";
                int r = TUD.SQLHelper.Insert<Model.Account_user>(u);

                if (issj)
                {
                    string sql = string.Format("UPDATE Account_user SET Mobile='{0}' WHERE Pkid=(SELECT Pkid FROM jc_lxr WHERE Lbdm=0 AND Dzbbh='{1}')", u.Pkid, dzbbh);
                    DAL.Globals.Execute(sql);
                }
                return r;
            }
            catch (Exception err)
            {
                if (err.Message.IndexOf("CI") >= 0) throw new Exception("工号已存在 ！");
                else if (err.Message.IndexOf("Mobile") > 0) throw new Exception("手机号码已存在 ！");
                else throw err;
            }
        }

        /// <summary>
        /// 修改联系人用户
        /// </summary>
        public static int ModLxr(string pkid, string dzbbh, string gh, string xm, string sjhm, bool issj)
        {
            try
            {
                Model.Account_user u = new Model.Account_user();
                u.Pkid = pkid;
                u.UserID = gh;
                u.UserName = xm;
                u.Mobile = sjhm;
                int r = TUD.SQLHelper.UpdateFields<Model.Account_user>(u, "UserID", "UserName", "Mobile");

                string sql = string.Format("UPDATE Account_user SET Mobile=Pkid WHERE [Level]='02' AND Mobile='{0}';", u.Pkid);
                if(issj) sql += string.Format("UPDATE Account_user SET Mobile='{0}' WHERE Pkid=(SELECT Pkid FROM jc_lxr WHERE Lbdm=0 AND Dzbbh='{1}')", u.Pkid, dzbbh);
                DAL.Globals.Execute(sql);

                return r;
            }
            catch (Exception err)
            {
                if (err.Message.IndexOf("CI") >= 0) throw new Exception("工号已存在 ！");
                else if (err.Message.IndexOf("Mobile") > 0) throw new Exception("手机号码已存在 ！");
                else throw err;
            }
        }

        /// <summary>
        /// 删除联系人用户
        /// </summary>
        public static int DelLxr(string pkid)
        {
            try
            {
                int r = TUD.SQLHelper.Delete<Model.Account_user>(pkid);

                string sql = string.Format("UPDATE Account_user SET Mobile=Pkid WHERE [Level]='02' AND Mobile='{0}'", pkid);
                DAL.Globals.Execute(sql);

                return r;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 批量删除联系人用户
        /// </summary>
        public static int DelLxrList(string[] pkids)
        {
            try
            {
                int r = TUD.SQLHelper.DeleteBat<Model.Account_user>(pkids);

                string sql = string.Format("UPDATE Account_user SET Mobile=Pkid WHERE [Level]='02' AND Mobile IN ('{0}')", string.Join("','", pkids));
                DAL.Globals.Execute(sql);

                return r;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 新增党支部用户
        /// </summary>
        public static int AddDzb(string pkid, string bmbh, string dzbbh, string dzbmc, string uid, string pwd)
        {
            try
            {
                Model.Account_user u = new Model.Account_user();
                //u.Pkid = Guid.NewGuid().ToString().Replace(DefSort = "Bmdm,Dzbdm,Bjmc,Xh", "").ToUpper();
                u.Pkid = pkid;
                u.DeptID = TU.Globals.BindSystemCode(Globals.SystemCode.DtJd_bm, "Pkid", "Bmdm", bmbh, "");
                u.UserID = uid;
                u.UserName = dzbmc;
                u.Password = BLL.Globals.MD5Encrypt(pwd);
                u.Level = "02";
                int r = TUD.SQLHelper.Insert<Model.Account_user>(u);

                return r;
            }
            catch (Exception err)
            {
                if (err.Message.IndexOf("CI") >= 0) throw new Exception("帐号已存在 ！");
                else throw err;
            }
        }

        /// <summary>
        /// 修改党支部用户
        /// </summary>
        public static int ModDzb(string pkid, string uid, string pwd)
        {
            try
            {
                Model.Account_user u = new Model.Account_user();
                u.Pkid = pkid;
                u.UserID = uid;
                u.Password = BLL.Globals.MD5Encrypt(pwd);
                int r = string.IsNullOrEmpty(pwd) ? TUD.SQLHelper.UpdateFields<Model.Account_user>(u, "UserID") : TUD.SQLHelper.UpdateFields<Model.Account_user>(u, "UserID", "Password");
                return r;
            }
            catch (Exception err)
            {
                if (err.Message.IndexOf("CI") >= 0) throw new Exception("帐号已存在 ！");
                else throw err;
            }
        }

        ///// <summary>
        ///// 删除党支部用户
        ///// </summary>
        //public static int DelDzb(string pkid)
        //{
        //    try
        //    {
        //        int r = TUD.SQLHelper.Delete<Model.Account_user>(pkid);

        //        string sql = string.Format("UPDATE Account_user SET Mobile=Pkid WHERE [Level]='02' AND Mobile='{0}'", pkid);
        //        DAL.Globals.Execute(sql);

        //        return r;
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }
        //}

        /// <summary>
        /// 新增记录
        /// </summary>
        private static int Insert(Model.Account_user u)
        {
            return TUD.SQLHelper.Insert<Model.Account_user>(u);
        }

        /// <summary>
        /// 根据给定的Pkid删除一条记录
        /// </summary>
        public static int Delete(string pkid)
        {
            try
            {
                return TUD.SQLHelper.Delete<Model.Account_user>(pkid);
            }
            catch (Exception err)
            {
                if (err.Message.IndexOf("约束") > -1)
                {
                    throw new Exception("存在关联数据，不能删除 ！");
                }
                throw err;
            }
        }

        /// <summary>
        /// 根据给定的一串Pkid批量删除记录
        /// </summary>
        public static int DeleteList(string[] pkids)
        {
            try
            {
                return TUD.SQLHelper.DeleteBat<Model.Account_user>(pkids);
            }
            catch (Exception err)
            {
                if (err.Message.IndexOf("约束") > -1)
                {
                    throw new Exception("存在关联数据，不能删除 ！");
                }
                throw err;
            }
        }

        /// <summary>
        /// 根据给定的Pkid修改一条记录
        /// </summary>
        public static int Update(Model.Account_user u)
        {
            return TUD.SQLHelper.Update<Model.Account_user>(u);
        }

        /// <summary>
        /// 根据给定的Pkid修改角色
        /// </summary>
        //public static int UpdateLevel(string pkid, TStar.Globals.SystemSettings.UserLevel level)
        //{
        //    string where = String.Format("Pkid='{0}'", pkid);
        //    return TUD.SQLHelper.UpdateFields<Model.Account_user>("Level", ((int)level).ToString("D2"), where);
        //}

        /// <summary>
        /// 根据给定的Pkid修改密码
        /// </summary>
        public static int UpdatePwd(string pkid, string pwd)
        {
            string encpwd = BLL.Globals.MD5Encrypt(pwd);
            string where = String.Format("Pkid='{0}'", pkid);
            int r = TUD.SQLHelper.UpdateFields<Model.Account_user>("Password", encpwd, where);
            if (r > 0 && TStar.Web.Globals.Account.UserInfo != null) TStar.Web.Globals.Account.UserInfo.Password = encpwd;
            return r;
        }

        /// <summary>
        /// 根据给定的Pkid修改手机
        /// </summary>
        public static int UpdateMobile(string pkid, string mobile)
        {
            string where = String.Format("Pkid='{0}'", pkid);

            try
            {
                return TUD.SQLHelper.UpdateFields<Model.Account_user>("Mobile", mobile, where);
            }
            catch(Exception err)
            {
                if (err.Message.IndexOf("唯一") > 0)
                    throw new Exception("此手机号码已注册 ！");
                else throw err;
            }
        }

        /// <summary>
        /// 根据给定的Pkid修改头像
        /// </summary>
        public static int UpdatePhoto(string pkid, string photourl)
        {
            string where = String.Format("Pkid='{0}'", pkid);
            int r = TUD.SQLHelper.UpdateFields<Model.Account_user>("PhotoUrl", photourl, where);
            if (r > 0) TStar.Web.Globals.Account.UserInfo.PhotoUrl = photourl;
            return r;
        }

        /// <summary>
        /// 根据给定的Pkid获取实体
        /// </summary>
        public static Model.Account_user GetEntity(string pkid)
        {
            Model.Account_user u = TUD.SQLHelper.GetEntity<Model.Account_user>(pkid);
            GetExtendInfo(u);
            return u;
        }

        public static Model.Account_user GetEntityByUserId(string uid)
        {
            Model.Account_user u = TUD.SQLHelper.GetEntity<Model.Account_user>("UserID", uid);
            GetExtendInfo(u);
            return u;
        }

        public static Model.Account_user Validate(string uid, string pwd)
        {
            pwd = BLL.Globals.MD5Encrypt(pwd);

            Model.Account_user u = GetEntityByUserId(uid);
            if (!String.IsNullOrEmpty(u.Pkid) && u.Status == "01" && u.Password == pwd) return u;
            return null;
        }

        private static void GetExtendInfo(Model.Account_user u)
        {
            Model.Jcgl.V_jc_lxr l = null;
            switch (TStar.Utility.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.UserLevel>(u.Level))
            {
                case TStar.Web.Globals.SystemSetting.UserLevel.Student:
                    Model.Jcgl.V_jc_xs xs = BLL.Global.Base.GetEntity<Model.Jcgl.V_jc_xs>(u.Pkid);
                    u.Dzbbh = xs.Dzbbh;
                    u.Dzbmc = xs.Dzbmc;
                    u.Fzztdm = xs.Fzztdm;
                    u.Fzzt = xs.Fzzt;
                    break;
                case TStar.Web.Globals.SystemSetting.UserLevel.Contacts:
                    l = BLL.Global.Base.GetEntity<Model.Jcgl.V_jc_lxr>(u.Pkid);
                    u.Dzbbh = l.Dzbbh;
                    u.Dzbmc = l.Dzbmc;
                    u.Fzztdm = "0";
                    u.Fzzt = "联系人";
                    break;
                case TStar.Web.Globals.SystemSetting.UserLevel.Branch:
                    l = BLL.Global.Base.GetEntity<Model.Jcgl.V_jc_lxr>(u.Pkid);
                    u.Dzbbh = l.Dzbbh;
                    u.Dzbmc = l.Dzbmc;
                    u.Fzztdm = "0";
                    u.Fzzt = "党支部";
                    break;
                case TStar.Web.Globals.SystemSetting.UserLevel.Committee:
                    l = BLL.Global.Base.GetEntity<Model.Jcgl.V_jc_lxr>(u.Pkid);
                    u.Dzbbh = l.Dzbbh;
                    u.Dzbmc = l.Dzbmc;
                    u.Fzztdm = "0";
                    u.Fzzt = "分党委";
                    break;
                case TStar.Web.Globals.SystemSetting.UserLevel.Party:
                    u.Dzbbh = "";
                    u.Dzbmc = "党总支";
                    u.Fzztdm = "0";
                    u.Fzzt = "党总支";
                    break;
            }
        }



        public static bool ValidatePhone(string uid, string phone)
        {
            Model.Account_user u = GetEntityByUserId(uid);
            if (!String.IsNullOrEmpty(u.Pkid) && (u.Mobile == phone)) return true;
            return false;
        }

        public static bool SetCode(string uid, string code, int minutes)
        {
            int r = TUD.SQLHelper.UpdateFields<Model.Account_user>(new string[] { "VerifyCode", "Timestamp" }, new string[] { code, DateTime.Now.AddMinutes(minutes).Ticks.ToString() }, "UserID='" + uid + "'"); ;
            return r >= 1;
        }

        public static bool ValidateCode(string uid, string code)
        {
            Model.Account_user u = GetEntityByUserId(uid);
            if (code == u.VerifyCode && DateTime.Now.Ticks <= u.Timestamp) return true;
            return false;
        }
    }
}
