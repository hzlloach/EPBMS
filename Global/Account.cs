using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using TU = TStar.Utility;
using TUD = TStar.Utility.DataSource;

namespace TStar.Web
{
    public partial class Globals
    {
        public class Account
        {
            static string csyh = "xxfdw"; //201405114106 201305113103 600372 600928 600389 xdw
            #region 属性

            public static string Pkid
            {
                get { return UserInfo.Pkid; }
            }
            public static string UserID
            {
                get { return UserInfo.UserID; }
            }
            public static string UserName
            {
                get { return UserInfo.UserName; }
            }
            public static string UserIDName
            {
                get { return UserInfo.UserIDName; }
            }
            public static string Password
            {
                get { return UserInfo.Password; }
            }
            public static string DeptPkid
            {
                get { return TU.Globals.BindSystemCode(DAL.Globals.SystemCode.DtJd_bm, "Bmdm", "Pkid", UserInfo.DeptID, ""); }
            }
            public static string DeptName
            {
                get { return TU.Globals.BindSystemCode(DAL.Globals.SystemCode.DtJd_bm, "Bmdm", "Bmmc", UserInfo.DeptID, ""); }
            }
            public static string RegTime
            {
                get { return TStar.Web.Globals.Account.UserInfo.RegTime; }
            }
            public static string UserLevel
            {
                get
                {
                    return TStar.Web.Globals.Account.UserInfo.Level;//(SystemSetting.UserLevel)System.Enum.Parse(typeof(SystemSetting.UserLevel), 
                }
            }
            public static Model.Account_user UserInfo
            {
                get
                {
                    return GetSession();
                }
                set
                {
                    SaveSession(value);
                }
            }

            #endregion

            #region 方法

            /// <summary>
            /// 获取登录信息
            /// </summary>
            public static Model.Account_user GetSession()
            {
                return TU.Globals.GetObject("$SystemCode$UserInfo") as Model.Account_user;
            }

            /// <summary>
            /// 保存登录信息
            /// </summary>
            public static void SaveSession(Model.Account_user user)
            {
                if (user != null)
                {
                    TU.Globals.SetObject(user, "$SystemCode$UserInfo", false);
                }
            }

            /// <summary>
            /// 删除登录信息
            /// </summary>
            public static void RemoveSession()
            {
                // Globals.SaveLog("系统", "系统退出", "");
                TU.Globals.RemoveObject("$SystemCode$UserInfo");
                TU.Globals.RemoveAllSessions();
            }

            /// <summary>
            /// 验证登录状态
            /// </summary>
            public static bool IsLogined()
            {
                return UserInfo != null;
            }

            /// <summary>
            /// 检查登录信息
            /// </summary>
            public static void CheckAuthority()
            {
                if (Globals.debug && UserInfo == null)
                {
                    UserInfo = TUD.SQLHelper.GetEntity<Model.Account_user>("UserID", csyh);//Admin xxxy Director 201705011101 600372 600928
                    Model.Jcgl.V_jc_lxr l = null;
                    switch (TStar.Utility.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.UserLevel>(UserInfo.Level))
                    {
                        case TStar.Web.Globals.SystemSetting.UserLevel.Student:
                            Model.Jcgl.V_jc_xs xs = TUD.SQLHelper.GetEntity<Model.Jcgl.V_jc_xs>(UserInfo.Pkid);
                            UserInfo.Dzbbh = xs.Dzbbh;
                            UserInfo.Dzbmc = xs.Dzbmc;
                            UserInfo.Fzztdm = xs.Fzztdm;
                            UserInfo.Fzzt = xs.Fzzt;
                            break;
                        case TStar.Web.Globals.SystemSetting.UserLevel.Contacts:
                            l = TUD.SQLHelper.GetEntity<Model.Jcgl.V_jc_lxr>(UserInfo.Pkid);
                            UserInfo.Dzbbh = l.Dzbbh;
                            UserInfo.Dzbmc = l.Dzbmc;
                            UserInfo.Fzztdm = "0";
                            UserInfo.Fzzt = "联系人";
                            break;
                        case TStar.Web.Globals.SystemSetting.UserLevel.Branch:
                            l = TUD.SQLHelper.GetEntity<Model.Jcgl.V_jc_lxr>(UserInfo.Pkid);
                            UserInfo.Dzbbh = l.Dzbbh;
                            UserInfo.Dzbmc = l.Dzbmc;
                            UserInfo.Fzztdm = "0";
                            UserInfo.Fzzt = "党支部";
                            break;
                        case TStar.Web.Globals.SystemSetting.UserLevel.Committee:
                            l = TUD.SQLHelper.GetEntity<Model.Jcgl.V_jc_lxr>(UserInfo.Pkid);
                            UserInfo.Dzbbh = l.Dzbbh;
                            UserInfo.Dzbmc = l.Dzbmc;
                            UserInfo.Fzztdm = "0";
                            UserInfo.Fzzt = "分党委";
                            break;
                    }
                }

                if (!IsLogined()) Page.Response.Redirect("~/logout.aspx");                
            }

            #endregion

            #region 缓存代码

            //public static DataTable DtAccountRole
            //{
            //    get
            //    {
            //        string tblName = "DtAccountRole";
            //        DataTable dt = TU.Globals.GetObject("$SystemCode$" + tblName) as DataTable;

            //        if (dt == null)
            //        {
            //            dt = BLL.OS.T_OS_Role.GetUserRoleList(UID).Tables[0];
            //            TU.Globals.SetObject(dt, "$SystemCode$" + tblName, false);
            //        }
            //        return dt;
            //    }
            //}
            //public static void RefreshDtAccountRole()
            //{
            //    TU.Globals.RemoveObject("$SystemCode$DtAccountRole");
            //}

            //public static DataTable DtAccountRight
            //{
            //    get
            //    {
            //        string tblName = "DtAccountRight";
            //        DataTable dt = TU.Globals.GetObject("$SystemCode$" + tblName) as DataTable;

            //        if (dt == null)
            //        {
            //            dt = BLL.OS.T_OS_Right.GetList("").Tables[0];
            //            TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
            //        }
            //        return dt;
            //    }
            //}
            //public static void RefreshDtAccountRight()
            //{
            //    TU.Globals.RemoveObject("$SystemCode$DtAccountRight");
            //}

            #endregion
        }
    }
}
