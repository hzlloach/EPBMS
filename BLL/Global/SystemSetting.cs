using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG = TStar.Web.Globals;
using TU = TStar.Utility;

namespace BLL
{
    public partial class Globals
    {
        public class SystemSetting
        {
            static SystemSetting()
            {
                DefaultEncPwd = MD5Encrypt(_DefaultPwd); 
            }

            /// <summary>
            /// 加解密附加码
            /// </summary>
            public static string EncryptCode = DAL.Globals.EncryptCode;//"$TStar_TPAMS$";

            static string _MaxLenPdf = ConfigurationManager.AppSettings["MaxLenPdf"];
            /// <summary>
            /// 上传PDF文件限制大小（字节）
            /// </summary>
            public static int MaxLenPdf = (String.IsNullOrEmpty(_MaxLenPdf) ? 5 : int.Parse(_MaxLenPdf)) * 1048576;

            static string _MaxLenImg = ConfigurationManager.AppSettings["MaxLenImg"];
            /// <summary>
            /// 上传IMG文件限制大小（字节）
            /// </summary>
            public static int MaxLenImg = (String.IsNullOrEmpty(_MaxLenImg) ? 1 : int.Parse(_MaxLenImg)) * 1048576;

            static string _MaxLenPhoto = ConfigurationManager.AppSettings["MaxLenPhoto"];
            /// <summary>
            /// 上传头像文件限制大小（字节）
            /// </summary>
            public static int MaxLenPhoto = (String.IsNullOrEmpty(_MaxLenPhoto) ? 50 : int.Parse(_MaxLenPhoto)) * 1024;

            static string _DefaultPwd = ConfigurationManager.AppSettings["DefaultPwd"];
            /// <summary>
            /// 默认加密密码
            /// </summary>
            public static string DefaultEncPwd = "";


            /// <summary>
            /// 当前学期
            /// </summary>
            public static string Dqxq
            {
                get
                {
                    int year = DateTime.Now.Year;
                    int month = DateTime.Now.Month;
                    DateTime dt = new DateTime(year, 2, 15);
                    if (DateTime.Now >= dt && month <= 8) return string.Format("{0}/{1}-2", year - 1, year);
                    else
                        if (month > 8) return string.Format("{0}/{1}-1", year, year + 1);
                        else return string.Format("{0}/{1}-1", year - 1, year);
                }
            }
            /// <summary>
            /// 当前学期名称
            /// </summary>
            public static string Dqxqmc
            {
                get { return Dqxq.Replace("-", "学年第") + "学期"; }
            }

            /// <summary>
            /// 当学期起始时间
            /// </summary>
            public static string DxqQssj
            {
                get
                {
                    int year = DateTime.Now.Year;
                    int month = DateTime.Now.Month;
                    DateTime dt = new DateTime(year, 2, 15);
                    if (DateTime.Now >= dt && month <= 8) return dt.ToString("yyyy-MM-dd 00:00:00");
                    else
                        if (month > 8) return string.Format("{0}-09-01 00:00:00", year);
                        else return string.Format("{0}-09-01 00:00:00", year - 1);
                }
            }
            /// <summary>
            /// 当学期截止时间
            /// </summary>
            public static string DxqJzsj
            {
                get
                {
                    int year = DateTime.Now.Year;
                    int month = DateTime.Now.Month;
                    DateTime dt = new DateTime(year, 2, 14, 23, 59, 59);
                    if (DateTime.Now > dt && month <= 8) return string.Format("{0}-08-31 23:59:59", year);
                    else
                        if (month > 8) return dt.AddYears(1).ToString("yyyy-MM-dd 23:59:59");
                        else return dt.ToString("yyyy-MM-dd 23:59:59");
                }
            }

            /// <summary>
            /// 项目申报允许的最小日期
            /// </summary>
            public static DateTime MinDate
            {
                get
                {
                    DateTime dtSqrq;
                    string sqrq = TU.Globals.BindSystemCode(DAL.Globals.SystemCode.DtXt_bmsq, "Bmbh", "Sqrq", TStar.Web.Globals.Account.DeptPkid, "Error");
                    if (DateTime.TryParse(sqrq, out dtSqrq) && dtSqrq <= DateTime.Now && DateTime.Now <= dtSqrq.AddMonths(2))
                        return DateTime.Now.AddYears(-1);
                    else return DateTime.Now.AddMonths(-3);
                }
            }

            #region 查询限制条件

            /// <summary>
            /// 部门查询限制条件
            /// </summary>
            public static string CondBm
            {
                get { return string.Format("Bmbh='{0}'", TG.Account.DeptPkid); }
            }

            /// <summary>
            /// 党支部查询限制条件
            /// </summary>
            public static string CondBmDzbbh
            {
                get 
                {
                    switch (TU.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.UserLevel>(TG.Account.UserLevel))
                    {
                        case TStar.Web.Globals.SystemSetting.UserLevel.Party:
                            return string.Format("Bmbh<>'{0}'", TG.Account.DeptPkid);
                        default:
                        case TStar.Web.Globals.SystemSetting.UserLevel.Committee:
                            return string.Format("Bmbh='{0}'", TG.Account.DeptPkid);
                        case TStar.Web.Globals.SystemSetting.UserLevel.Branch:
                            return string.Format("Bmbh='{0}' AND Dzbbh='{1}'", TG.Account.DeptPkid, TG.Account.UserInfo.Dzbbh);
                        case TStar.Web.Globals.SystemSetting.UserLevel.Contacts:
                            return string.Format("Bmbh='{0}' AND Dzbbh='{1}' AND Lxrbh='{2}'", TG.Account.DeptPkid, TG.Account.UserInfo.Dzbbh, TG.Account.Pkid);
                        case TStar.Web.Globals.SystemSetting.UserLevel.Student:
                            return string.Format("Bmbh='{0}' AND Dzbbh='{1}' AND Pkid='{2}'", TG.Account.DeptPkid, TG.Account.UserInfo.Dzbbh, TG.Account.Pkid);
                    }
                }
            }

            /// <summary>
            /// 联系人查询限制条件
            /// </summary>
            public static string CondLxrbh
            {
                get { return string.Format("Lxrbh='{0}'", TG.Account.Pkid); }
            }

            /// <summary>
            /// 学生查询限制条件
            /// </summary>
            public static string CondXsbh
            {
                get { return string.Format("Fzrbh='{0}'", TG.Account.Pkid); }
            }

            /// <summary>
            /// 学生及当前发展状态查询限制条件
            /// </summary>
            public static string CondXsbhFzzt
            {
                get { return string.Format("Fzrbh='{0}' AND Fzztdm='{1}'", TG.Account.Pkid, TG.Account.UserInfo.Fzztdm); }
            }

            #endregion

            #region 绑定限制条件

            /// <summary>
            /// 部门绑定限制条件
            /// </summary>
            public static string FilterBm
            {
                get
                {
                    switch (TU.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.UserLevel>(TG.Account.UserLevel))
                    {
                        default:
                        case TStar.Web.Globals.SystemSetting.UserLevel.Party:
                            return string.Format("Pkid<>'{0}'", TG.Account.DeptPkid);
                        case TStar.Web.Globals.SystemSetting.UserLevel.Committee:
                        case TStar.Web.Globals.SystemSetting.UserLevel.Branch:
                        case TStar.Web.Globals.SystemSetting.UserLevel.Contacts:
                        case TStar.Web.Globals.SystemSetting.UserLevel.Student:
                            return string.Format("Pkid='{0}'", TG.Account.DeptPkid);
                    }
                }
            }

            /// <summary>
            /// 党支部绑定限制条件
            /// </summary>
            public static string FilterDzb
            {
                get
                {
                    switch (TU.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.UserLevel>(TG.Account.UserLevel))
                    {
                        default:
                        case TStar.Web.Globals.SystemSetting.UserLevel.Party:
                            return "Bmbh <> ''";
                        case TStar.Web.Globals.SystemSetting.UserLevel.Committee:
                            return string.Format("Bmbh IN ('__','{0}')", TG.Account.DeptPkid);
                        case TStar.Web.Globals.SystemSetting.UserLevel.Branch:
                        case TStar.Web.Globals.SystemSetting.UserLevel.Contacts:
                        case TStar.Web.Globals.SystemSetting.UserLevel.Student:
                            return string.Format("Bmbh='{0}' AND Pkid='{1}'", TG.Account.DeptPkid, TG.Account.UserInfo.Dzbbh);
                    }
                }
            }

            #endregion

            /// <summary>
            /// 是否联系人
            /// </summary>
            public static bool IsContacts
            {
                get
                {
                    return TStar.Utility.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.UserLevel>(TStar.Web.Globals.Account.UserLevel) == TStar.Web.Globals.SystemSetting.UserLevel.Contacts;
                }
            }

            /// <summary>
            /// 是否分党委
            /// </summary>
            public static bool IsCommittee
            {
                get
                {
                    return TStar.Utility.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.UserLevel>(TStar.Web.Globals.Account.UserLevel) == TStar.Web.Globals.SystemSetting.UserLevel.Committee;
                }
            }



            /// <summary>
            /// 是否退回修改
            /// </summary>
            public static bool IsBack(int ztdm)
            {
                int zt = ztdm;
                return zt == (int)TStar.Web.Globals.SystemSetting.Status.ToBeModified;
            }
            /// <summary>
            /// 是否可以修改/删除/提交
            /// </summary>
            public static bool CanModifyDelete(string ztdm)
            {
                return CanModifyDelete(int.Parse(ztdm));
            }
            /// <summary>
            /// 是否可以修改/删除/提交
            /// </summary>
            public static bool CanModifyDelete(int ztdm)
            {
                int zt = ztdm;
                int ztDraft = (int)TStar.Web.Globals.SystemSetting.Status.Draft;
                int ztSubmit = (int)TStar.Web.Globals.SystemSetting.Status.Submitted;
                //bool isWtj = IsNonSubmitted(ztdm);
                //bool isBack = IsBack(ztdm);
                //return CanFill && isWtj || CanMod && isBack;

                return zt >= ztDraft && zt < ztSubmit || IsBack(ztdm);
            }
            /// <summary>
            /// 是否可以提交
            /// </summary>
            public static bool CanSubmit(bool yxsc, int fjsl)
            {
                return !(yxsc && fjsl == 0);
            }                        
            /// <summary>
            /// 是否未提交
            /// </summary>
            public static bool IsNonSubmitted(string ztdm)
            {
                //int zt = int.Parse(ztdm);
                //return TU.Globals.IsBetweenEqual((long)zt, (long)TStar.Globals.SystemSettings.YjState.ModifyStart, (long)TStar.Globals.SystemSettings.YjState.ModifyEnd);
                return true;
            }            
            /// <summary>
            /// 是否允许审核
            /// </summary>
            public static bool CanAudit(string ztdm)
            {
                //int zt = int.Parse(ztdm);
                //int bzzt = -100;
                //int tjzt = (int)TStar.Globals.SystemSettings.YjState.Submitted;
                //int xgzt = (int)TStar.Globals.SystemSettings.YjState.ModifyEnd;
                //switch (TStar.Globals.Account.UserLevel)
                //{
                //    case TStar.Globals.SystemSettings.UserLevel.Director: // 主任
                //        bzzt = (int)TStar.Globals.SystemSettings.YjState.DirectorAudited;
                //        return zt >= tjzt && zt <= bzzt || zt == -bzzt || zt == xgzt;
                //    case TStar.Globals.SystemSettings.UserLevel.DepartAdmin: // 学院管理员
                //        bzzt = (int)TStar.Globals.SystemSettings.YjState.DepartAudited;
                //        return zt >= tjzt && zt <= bzzt || zt == -bzzt || zt == xgzt;
                //    //case TStar.Globals.SystemSettings.UserLevel.CollegeAdmin: // 学校管理员
                //    //    bzzt = (int)TStar.Globals.SystemSettings.YjState.CollegeAudited;
                //    //    return zt >= tjzt && zt <= bzzt || zt == -bzzt;
                //    default:
                        return false;
                //}
            }                   
        }
    }
}
