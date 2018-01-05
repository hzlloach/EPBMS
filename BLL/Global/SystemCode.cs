using System;
using System.Data;
using System.Configuration;
using System.Web;
using TU = TStar.Utility;
using TUF = TStar.Utility.FineUI;

namespace BLL
{
    public partial class Globals
    {
        public class SystemCode
        {
            #region 系统代码表

            public static DataTable DtDateSpanType
            {
                get
                {
                    string tblName = "DtDateSpanType";
                    string now = DateTime.Now.ToString("yyyy-MM-dd");
                    string date = TU.Globals.GetObject("$SystemCode$" + tblName + "Tick") as string;
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null || now.CompareTo(date) != 0)
                    {
                        dt = new DataTable(tblName);
                        dt.Columns.Add("F_ID", typeof(System.String));
                        dt.Columns.Add("F_TypeCode", typeof(System.String));
                        dt.Columns.Add("F_TypeName", typeof(System.String));
                        DataRow dr = dt.NewRow();
                        dr[0] = "0";
                        dr[1] = "__";
                        dr[2] = "--- 全部 ---";
                        dt.Rows.Add(dr);
                        dr = dt.NewRow();
                        dr[0] = "1";
                        dr[1] = String.Format("='{0}'", now);
                        dr[2] = "当天";
                        dt.Rows.Add(dr);
                        dr = dt.NewRow();
                        dr[0] = "2";
                        dr[1] = String.Format("BETWEEN '{0}' AND '{1}'", DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), now);
                        dr[2] = "一周内";
                        dt.Rows.Add(dr);
                        dr = dt.NewRow();
                        dr[0] = "3";
                        dr[1] = String.Format("BETWEEN '{0}' AND '{1}'", DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd"), now);
                        dr[2] = "半月内";
                        dt.Rows.Add(dr);
                        dr = dt.NewRow();
                        dr[0] = "4";
                        dr[1] = String.Format("BETWEEN '{0}' AND '{1}'", DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"), now);
                        dr[2] = "一月内";
                        dt.Rows.Add(dr);
                        dr = dt.NewRow();
                        dr[0] = "5";
                        dr[1] = String.Format("BETWEEN '{0}' AND '{1}'", DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd"), now);
                        dr[2] = "三月内";
                        dt.Rows.Add(dr);
                        dr = dt.NewRow();
                        dr[0] = "6";
                        dr[1] = String.Format("BETWEEN '{0}' AND '{1}'", DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd"), now);
                        dr[2] = "半年内";
                        dt.Rows.Add(dr);
                        dr = dt.NewRow();
                        dr[0] = "7";
                        dr[1] = String.Format("BETWEEN '{0}' AND '{1}'", DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd"), now);
                        dr[2] = "一年内";
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();

                        TU.Globals.SetObject(now, "$SystemCode$" + tblName + "Tick", true);
                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }

                    return dt;
                }
            }/// <summary>            
            
            /// <summary>
            /// 性别代码
            /// </summary>
            public static DataTable DtDm_xb
            {
                get
                {
                    return DAL.Globals.SystemCode.DtDm_xb;
                }
            }

            /// <summary>
            /// 发展状态代码
            /// </summary>
            public static DataTable DtDm_fzzt
            {
                get
                {
                    return DAL.Globals.SystemCode.DtDm_fzzt;
                }
            }

            /// <summary>
            /// 考核状态代码
            /// </summary>
            public static DataTable DtDm_khzt
            {
                get
                {
                    return DAL.Globals.SystemCode.DtDm_khzt;
                }
            }

            /// <summary>
            /// 结果状态缓存
            /// </summary>
            public static DataTable DtDm_jgzt
            {
                get
                {
                    return DAL.Globals.SystemCode.DtDm_jgzt;
                }
            }

            /// <summary>
            /// 预审答辩结果状态缓存
            /// </summary>
            public static DataTable DtDm_dbjgzt
            {
                get
                {
                    return DAL.Globals.SystemCode.DtDm_dbjgzt;
                }
            }

            /// <summary>
            /// 数据状态缓存
            /// </summary>
            public static DataTable DtDm_sjzt
            {
                get
                {
                    return DAL.Globals.SystemCode.DtDm_sjzt;
                }
            }
            /// <summary>
            /// 树型部门缓存
            /// </summary>
            public static DataTable DtTreeJd_bm
            {
                get
                {
                    return DAL.Globals.SystemCode.DtTreeJd_bm;
                }
            }
            /// <summary>
            /// 部门缓存
            /// </summary>
            public static DataTable DtJd_bm
            {
                get
                {
                    return DAL.Globals.SystemCode.DtJd_bm;
                }
            }
            /// <summary>
            /// 刷新部门缓存
            /// </summary>
            public static void RefreshDtJd_bm()
            {
                DAL.Globals.SystemCode.RefreshDt("DtJd_bm");
                DAL.Globals.SystemCode.RefreshDt("DtTreeJd_bm");
            }
            /// <summary>
            /// 党支部缓存
            /// </summary>
            public static DataTable DtJd_dzb
            {
                get
                {
                    return DAL.Globals.SystemCode.DtJd_dzb;
                }
            }
            /// <summary>
            /// 刷新党支部缓存
            /// </summary>
            public static void RefreshDtJd_dzb()
            {
                DAL.Globals.SystemCode.RefreshDt("DtJd_dzb");
            }
            /// <summary>
            /// 专业缓存
            /// </summary>
            public static DataTable DtJd_zy
            {
                get
                {
                    return DAL.Globals.SystemCode.DtJd_zy;
                }
            }
            /// <summary>
            /// 刷新专业缓存
            /// </summary>
            public static void RefreshDtJd_zy()
            {
                DAL.Globals.SystemCode.RefreshDt("DtJd_zy");
            }
            /// <summary>
            /// 班级缓存
            /// </summary>
            public static DataTable DtJd_bj
            {
                get
                {
                    return DAL.Globals.SystemCode.DtJd_bj;
                }
            }
            /// <summary>
            /// 刷新班级缓存
            /// </summary>
            public static void RefreshDtJd_bj()
            {
                DAL.Globals.SystemCode.RefreshDt("DtJd_bj");
            }
            /// <summary>
            /// 联系人缓存
            /// </summary>
            public static DataTable DtJc_lxr
            {
                get
                {
                    return DAL.Globals.SystemCode.DtJc_lxr;
                }
            }
            /// <summary>
            /// 刷新联系人缓存
            /// </summary>
            public static void RefreshDtJc_lxr()
            {
                DAL.Globals.SystemCode.RefreshDt("DtJc_lxr");
            }

            /// <summary>
            /// 指标显示栏目缓存
            /// </summary>
            public static DataTable DtJd_zbxs
            {
                get
                {
                    return DAL.Globals.SystemCode.DtJd_zbxs;
                }
            }
            /// <summary>
            /// 本地指标显示栏目缓存
            /// </summary>
            public static DataTable DtJd_zbxsLocal
            {
                get
                {
                    string tblName = "DtJd_zbxsLocal";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        dt = DtJd_zbxs.Copy();
                        for (int i = dt.Rows.Count - 1; i >= 0; i--)
                        {
                            string bmbh = dt.Rows[i]["Bmbh"].ToString();
                            if (bmbh == "__" || bmbh == TStar.Web.Globals.Account.DeptPkid) continue;

                            dt.Rows.RemoveAt(i);
                        }
                        dt.AcceptChanges();

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, false);
                    }
                    return dt;
                }
            }
            /// <summary>
            /// 刷新指标显示栏目缓存
            /// </summary>
            public static void RefreshDtJd_zbxs()
            {
                DAL.Globals.SystemCode.RefreshDt("DtJd_zbxs");
                DAL.Globals.SystemCode.RefreshDt("DtJd_zbxsLocal");
            }

            /// <summary>
            /// 考核指标缓存
            /// </summary>
            public static DataTable DtJd_khzb
            {
                get
                {
                    return DAL.Globals.SystemCode.DtJd_khzb;
                }
            }
            /// <summary>
            /// 本地考核指标缓存
            /// </summary>
            public static DataTable DtJd_khzbLocal
            {
                get
                {
                    string tblName = "DtJd_khzbLocal";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        dt = DtJd_khzb.Copy();
                        for (int i = dt.Rows.Count - 1; i >= 0; i--)
                        {
                            string bmbh = dt.Rows[i]["Bmbh"].ToString();
                            if (bmbh == "__" || bmbh == "BM00".PadRight(32, '0') || bmbh == TStar.Web.Globals.Account.DeptPkid) continue;

                            dt.Rows.RemoveAt(i);
                        }
                        dt.AcceptChanges();

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, false);
                    }
                    return dt;
                }
            }
            /// <summary>
            /// 本地考核指标树型缓存
            /// </summary>
            public static DataTable DtTree_khzbLocal
            {
                get
                {
                    string tblName = "DtTree_khzbLocal";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        dt = TUF.Helper.CreateTreeViewSource(DtJd_khzbLocal, true, "Pkid", "Zbdm", "Zbmc");
                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, false);
                    }
                    return dt;
                }
            }
            /// <summary>
            /// 用于隶属指标查询（显示公共及本部门的指标）
            /// </summary>
            public static DataTable DtTree_khzbLocalBelong
            {
                get
                {
                    string tblName = "DtTree_khzbLocalBelong";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string bmbh = TStar.Web.Globals.Account.DeptPkid;
                        dt = TUF.Helper.CreateTreeViewSource(DtJd_khzbLocal, "(Zbqx='P' OR Sfgd='false') AND Zbqx<>'D' AND Bmbh IN ('" + "BM00".PadRight(32, '0') + "','" + bmbh + "')", "", true, "Pkid", "Zbdm", "Zbmc");
                        int cnt = dt.Rows.Count - 1;
                        for (int i = cnt; i > 0; i--)
                        {
                            // 如果分组指标下面没有具体指标，则删除
                            string qx = TU.Globals.BindSystemCode(DtJd_khzbLocal, "Pkid", "Zbqx", dt.Rows[i]["id"].ToString(), "P");
                            if (qx == "P" && (i == dt.Rows.Count - 1 || i < dt.Rows.Count - 1 && dt.Rows[i + 1]["Sjdm"].ToString() != dt.Rows[i]["dm"].ToString()))
                                dt.Rows.RemoveAt(i);
                        }
                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, false);
                    }
                    return dt;
                }
            }
            /// <summary>
            /// 用于隶属指标选择（显示公共及本部门的指标，且分组不能选择）
            /// </summary>
            public static DataTable DtTree_khzbLocalBelongEdit
            {
                get
                {
                    string tblName = "DtTree_khzbLocalBelongEdit";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        dt = DtTree_khzbLocalBelong.Copy();
                        dt.Rows[0]["select"] = false;
                        foreach (DataRow dr in dt.Rows)
                        {
                            string qx = TU.Globals.BindSystemCode(DtJd_khzbLocal, "Pkid", "Zbqx", dr["id"].ToString(), "P");
                            if (qx == "P") dr["select"] = false;
                        }
                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, false);
                    }
                    return dt;
                }
            }
            /// <summary>
            /// 用于分党委自行添加指标（只显示公共及本部门的分组）
            /// </summary>
            public static DataTable DtTree_khzbLocalCatalog
            {
                get
                {
                    string tblName = "DtTree_khzbLocalCatalog";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string bmbh = TStar.Web.Globals.Account.DeptPkid;
                        dt = TUF.Helper.CreateTreeViewSource(DtJd_khzbLocal, "Zbqx='P' AND Bmbh IN ('" + "BM00".PadRight(32, '0') + "','" + bmbh + "')", "", true, "Pkid", "Zbdm", "Zbmc");
                        //int cnt = dt.Rows.Count - 1;
                        //for (int i = cnt; i > 0; i--)
                        //{
                        //    string qx = TU.Globals.BindSystemCode(DtJd_khzbLocal, "Pkid", "Zbqx", dt.Rows[i]["id"].ToString(), "P");
                        //    //if (qx != "P") dt.Rows.RemoveAt(i); else 
                        //    if (i < dt.Rows.Count - 1 && dt.Rows[i + 1]["Sjdm"].ToString() == dt.Rows[i]["dm"].ToString()) dt.Rows[i]["select"] = false;

                        //}
                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, false);
                    }
                    return dt;
                }
            }
            /// <summary>
            /// 用于分党委的隶属指标查询（只显示公共分组及本部门的指标）
            /// </summary>
            public static DataTable DtTree_khzbLocalCatalogBelong
            {
                get
                {
                    string tblName = "DtTree_khzbLocalCatalogBelong";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string bmbh = TStar.Web.Globals.Account.DeptPkid;
                        dt = TUF.Helper.CreateTreeViewSource(DtJd_khzbLocal, "Zbqx='P' OR Bmbh='" + bmbh + "'", "", true, "Pkid", "Zbdm", "Zbmc");
                        int cnt = dt.Rows.Count - 1;
                        for (int i = cnt; i > 0; i--)
                        {
                            // 如果分组指标下面没有具体指标，则删除
                            string qx = TU.Globals.BindSystemCode(DtJd_khzbLocal, "Pkid", "Zbqx", dt.Rows[i]["id"].ToString(), "P");
                            if (qx == "P" && (i == dt.Rows.Count - 1 || i < dt.Rows.Count - 1 && dt.Rows[i + 1]["Sjdm"].ToString() != dt.Rows[i]["dm"].ToString()))
                                dt.Rows.RemoveAt(i);
                        }
                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, false);
                    }
                    return dt;
                }
            }
            /// <summary>
            /// 用于分党委的隶属指标选择（只显示公共分组及本部门的指标，且分组不能选择）
            /// </summary>
            public static DataTable DtTree_khzbLocalCatalogBelongEdit
            {
                get
                {
                    string tblName = "DtTree_khzbLocalCatalogBelongEdit";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        dt = DtTree_khzbLocalCatalogBelong.Copy();
                        dt.Rows[0]["select"] = false;
                        foreach(DataRow dr in dt.Rows)
                        {
                            string qx = TU.Globals.BindSystemCode(DtJd_khzbLocal, "Pkid", "Zbqx", dr["id"].ToString(), "P");
                            if(qx == "P") dr["select"] = false;
                        }
                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, false);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 刷新考核指标缓存
            /// </summary>
            public static void RefreshDtJd_khzb()
            {
                DAL.Globals.SystemCode.RefreshDt("DtJd_khzb");
                DAL.Globals.SystemCode.RefreshDt("DtJd_khzbLocal");
                DAL.Globals.SystemCode.RefreshDt("DtTree_khzbLocalBelong");
                DAL.Globals.SystemCode.RefreshDt("DtTree_khzbLocalBelongEdit");
                DAL.Globals.SystemCode.RefreshDt("DtTree_khzbLocalCatalog");
                DAL.Globals.SystemCode.RefreshDt("DtTree_khzbLocalCatalogBelong");
                DAL.Globals.SystemCode.RefreshDt("DtTree_khzbLocalCatalogBelongEdit");
                RefreshDtJd_zbxs();
            }

            /// <summary>
            /// 项目等级缓存
            /// </summary>
            public static DataTable DtJd_xmdj
            {
                get
                {
                    return DAL.Globals.SystemCode.DtJd_xmdj;
                }
            }
            /// <summary>
            /// 本地项目等级缓存
            /// </summary>
            public static DataTable DtJd_xmdjLocal
            {
                get
                {
                    string tblName = "DtJd_xmdjLocal";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        dt = DtJd_xmdj.Copy();
                        for (int i = dt.Rows.Count - 1; i >= 0; i--)
                        {
                            string bmbh = dt.Rows[i]["Bmbh"].ToString();
                            if (bmbh == "__" || bmbh == "BM00".PadRight(32, '0') || bmbh == TStar.Web.Globals.Account.DeptPkid) continue;

                            dt.Rows.RemoveAt(i);
                        }
                        dt.AcceptChanges();

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, false);
                    }
                    return dt;
                }
            }
            /// <summary>
            /// 刷新项目等级缓存
            /// </summary>
            public static void RefreshDtJd_xmdj()
            {
                DAL.Globals.SystemCode.RefreshDt("DtJd_xmdj");
                DAL.Globals.SystemCode.RefreshDt("DtJd_xmdjLocal");
                RefreshDtJd_khzb();
            }

            ///// <summary>
            ///// 项目状态缓存
            ///// </summary>
            //public static DataTable DtJd_xmzt
            //{
            //    get
            //    {
            //        return DAL.Globals.SystemCode.DtJd_xmzt;
            //    }
            //}
            ///// <summary>
            ///// 本地项目状态缓存
            ///// </summary>
            //public static DataTable DtJd_xmztLocal
            //{
            //    get
            //    {
            //        string tblName = "DtJd_xmztLocal";
            //        DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

            //        if (dt == null)
            //        {
            //            dt = DtJd_xmzt.Copy();
            //            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            //            {
            //                string bmbh = dt.Rows[i]["Bmbh"].ToString();
            //                if (bmbh == "__" || bmbh == TStar.Web.Globals.Account.DeptPkid) continue;

            //                dt.Rows.RemoveAt(i);
            //            }
            //            dt.AcceptChanges();

            //            TU.Globals.SetObject(dt, "$SystemCode$" + tblName, false);
            //        }
            //        return dt;
            //    }
            //}
            ///// <summary>
            ///// 刷新项目状态缓存
            ///// </summary>
            //public static void RefreshDtJd_xmzt()
            //{
            //    DAL.Globals.SystemCode.RefreshDt("DtJd_xmzt");
            //    DAL.Globals.SystemCode.RefreshDt("DtJd_xmztLocal");
            //}

            ///// <summary>
            ///// 项目选项缓存
            ///// </summary>
            //public static DataTable DtJd_xmxx
            //{
            //    get
            //    {
            //        return DAL.Globals.SystemCode.DtJd_xmxx;
            //    }
            //}
            ///// <summary>
            ///// 本地项目选项缓存
            ///// </summary>
            //public static DataTable DtJd_xmxxLocal
            //{
            //    get
            //    {
            //        string tblName = "DtJd_xmxxLocal";
            //        DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

            //        if (dt == null)
            //        {
            //            dt = DtJd_xmxx.Copy();
            //            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            //            {
            //                string bmbh = dt.Rows[i]["Bmbh"].ToString();
            //                if (bmbh == "__" || bmbh == TStar.Web.Globals.Account.DeptPkid) continue;

            //                dt.Rows.RemoveAt(i);
            //            }
            //            dt.AcceptChanges();

            //            TU.Globals.SetObject(dt, "$SystemCode$" + tblName, false);
            //        }
            //        return dt;
            //    }
            //}
            ///// <summary>
            ///// 刷新项目选项缓存
            ///// </summary>
            //public static void RefreshDtJd_xmxx()
            //{
            //    DAL.Globals.SystemCode.RefreshDt("DtJd_xmxx");
            //    DAL.Globals.SystemCode.RefreshDt("DtJd_xmxxLocal");
            //}

            #endregion

            //#region 系统设置表

            ///// <summary>
            ///// 系统设置缓存
            ///// </summary>
            //public static DataTable DtXt_xtzt
            //{
            //    get { return DAL.Globals.SystemCode.DtXt_xtzt; }
            //}
            ///// <summary>
            ///// 刷新系统设置缓存
            ///// </summary>
            //public static void RefreshDtXt_xtzt(Model.Xtgl.Xt_xtsz m)
            //{
            //    DataRow dr = TU.Globals.FindRow(DtXt_xtzt.DefaultView, "Pkid", m.Pkid);
            //    if (dr != null) dr["Qz"] = m.Qz;
            //    else DtXt_xtzt.LoadDataRow(new object[]{m.Pkid, m.Bmbh, m.Dm, m.Mc, m.Qz}, true);
            //}

            ///// <summary>
            ///// 操作设置缓存
            ///// </summary>
            //public static DataTable DtXt_czsz
            //{
            //    get
            //    {
            //        string tblName = "DtXt_czsz";
            //        DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

            //        if (dt == null)
            //        {
            //            string sql = "SELECT Pkid,Xn,Bmbh,Czsz FROM xt_czsz WHERE Xn='" + TStar.Globals.SystemSettings.Dqxn + "' ORDER BY Xn,Bmbh";
            //            DataSet ds = DAL.Globals.Query(sql);

            //            dt = ds.Tables[0];
            //            dt.TableName = tblName;

            //            TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
            //        }
            //        return dt;
            //    }
            //}

            ///// <summary>
            ///// 本地操作设置缓存
            ///// </summary>
            //public static DataTable DtXt_czszLocal
            //{
            //    get
            //    {
            //        string tblName = "DtXt_czszLocal";
            //        DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

            //        if (dt == null)
            //        {
            //            string xn = TStar.Globals.SystemSettings.Dqxn;
            //            string bmbh = TStar.Globals.Account.DeptPkid;
            //            dt = DtXt_czsz.Clone();

            //            DataRow dr = TU.Globals.FindRow(DtXt_czsz.DefaultView, new string[] { "Xn", "Bmbh" }, new object[] { xn, bmbh });
            //            if (dr == null)
            //            {
            //                DataRow ndr = dt.NewRow();
            //                ndr["Xn"] = xn;
            //                ndr["Bmbh"] = bmbh;
            //                ndr["Czsz"] = "0";
            //                dt.Rows.Add(ndr);
            //            }
            //            else
            //            {
            //                dt.Rows.Add(dr.ItemArray);
            //            }
            //            dt.AcceptChanges();

            //            TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
            //        }
            //        return dt;
            //    }
            //}

            //public static int RefreshDtXt_czsz(string czsz)
            //{
            //    int r = 0;
            //    DataTable dt = null;
            //    if (czsz != Gsczsz)
            //    {
            //        DataRow dr = DtXt_czszLocal.Rows[0];
            //        Model.Xtgl.Xt_czsz m = new Model.Xtgl.Xt_czsz();
            //        m.Pkid = dr["Pkid"].ToString();
            //        m.Xn = TStar.Globals.SystemSettings.Dqxn;
            //        m.Bmbh = TStar.Globals.Account.DeptPkid;
            //        m.Czsz = czsz;
            //        dt = DtXt_czsz;

            //        if (String.IsNullOrEmpty(m.Pkid))
            //        {
            //            // 插入设置数据
            //            r = BLL.Dmgl.Insert<Model.Xtgl.Xt_czsz>(m);
            //        }
            //        else
            //        {
            //            // 修改设置数据
            //            r = BLL.Dmgl.Update<Model.Xtgl.Xt_czsz>(m);
            //        }

            //        // 刷新缓存
            //        DAL.Globals.SystemCode.RefreshDt("DtXt_czsz");
            //        DAL.Globals.SystemCode.RefreshDt("DtXt_czszLocal");
            //    }
            //    return r;
            //}

            ///// <summary>
            ///// 部门授权缓存
            ///// </summary>
            //public static DataTable DtXt_bmsq
            //{
            //    get
            //    {
            //        return DAL.Globals.SystemCode.DtXt_bmsq;
            //    }
            //}
            ///// <summary>
            ///// 刷新部门授权缓存
            ///// </summary>
            //public static void RefreshDtXt_bmsq()
            //{
            //    DAL.Globals.SystemCode.RefreshDt("DtXt_bmsq");
            //}


            //#endregion

            //#region 系统日志表

            ///// <summary>
            ///// 系统日志
            ///// </summary>

            //#endregion
        }
    }
}
