using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Web;
using Maticsoft.DBUtility;
using TU=TStar.Utility;
//using TG = TStar.

namespace DAL
{
    public partial class Globals
    {
        public class SystemCode
        {
            #region 系统代码表

            //public static DataTable DtDataCenterType
            //{
            //    get
            //    {
            //        string tblName = "DtDataCenterType";
            //        DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

            //        if (dt == null)
            //        {
            //            string sql = "SELECT F_TypeCode, F_TypeName FROM T_DT_DataCenterTypeDict order by 1 ";
            //            DataSet ds = DbHelperSQL.Query(sql);
            //            dt = ds.Tables[0];
            //            dt.TableName = tblName;

            //            DataRow dr = dt.NewRow(); // 按表字段依次赋值（特别是F_TypeCode和F_TypeName字段）
            //            dr[0] = "0";
            //            dr[1] = "－请选择－";
            //            dt.Rows.InsertAt(dr, 0);
            //            dt.AcceptChanges();

            //            TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
            //        }
            //        return dt;
            //    }
            //}

            /// <summary>
            /// 性别
            /// </summary>
            public static DataTable DtDm_xb
            {
                get
                {
                    string tblName = "DtDm_xb";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Dm,Mc FROM dm_xb ORDER BY Dm";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;

                        DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        dr["Pkid"] = "__";
                        dr["Dm"] = "__";
                        dr["Mc"] = "－请选择－";
                        dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges();
                        ds.Tables.RemoveAt(0);

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 发展状态代码
            /// </summary>
            public static DataTable DtDm_fzzt
            {
                get
                {
                    string tblName = "DtDm_fzzt";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Dm,Mc FROM dm_fzzt ORDER BY Dm";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;

                        DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        dr["Pkid"] = "__";
                        dr["Dm"] = "__";
                        dr["Mc"] = "－请选择－";
                        dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges();
                        ds.Tables.RemoveAt(0);

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 考核状态代码
            /// </summary>
            public static DataTable DtDm_khzt
            {
                get
                {
                    string tblName = "DtDm_khzt";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Dm,Mc FROM dm_khzt ORDER BY Dm";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;

                        DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        dr["Pkid"] = "__";
                        dr["Dm"] = "__";
                        dr["Mc"] = "－请选择－";
                        dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges();
                        ds.Tables.RemoveAt(0);

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 预审答辩结果状态缓存
            /// </summary>
            public static DataTable DtDm_dbjgzt
            {
                get
                {
                    string tblName = "DtDm_dbjgzt";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Dm,Mc FROM dm_jgzt ORDER BY CONVERT(int,Dm)";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;

                        DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        dr["Pkid"] = "__";
                        dr["Dm"] = "__";
                        dr["Mc"] = "－请选择－";
                        dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges();
                        ds.Tables.RemoveAt(0);

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 结果状态缓存
            /// </summary>
            public static DataTable DtDm_jgzt
            {
                get
                {
                    string tblName = "DtDm_jgzt";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        dt = DtDm_dbjgzt.Copy();
                        dt.TableName = tblName;                        
                        dt.Rows.RemoveAt(1);
                        dt.AcceptChanges();

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 数据状态缓存
            /// </summary>
            public static DataTable DtDm_sjzt
            {
                get
                {
                    string tblName = "DtDm_sjzt";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Dm,Mc, Xsmc FROM dm_sjzt ORDER BY Dm";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;

                        DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        dr["Pkid"] = "__";
                        dr["Dm"] = "__";
                        dr["Mc"] = "－请选择－";
                        dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges();
                        ds.Tables.RemoveAt(0);

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            public static void RefreshDt(string tblName)
            {
                TU.Globals.RemoveObject("$SystemCode$" + tblName);
            }

            #endregion

            #region

            /// <summary>
            /// 树型部门缓存
            /// </summary>
            public static DataTable DtTreeJd_bm
            {
                get
                {
                    string tblName = "DtTreeJd_bm";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Bmdm='B' + Pkid, Bmmc, ParDm='', Bmdm2=Bmdm FROM jd_bm UNION ALL SELECT Bmdm='J' + Pkid, Jysmc AS Bmmc, ParDm='B' + Bmbh, Bmdm='99' FROM jd_jys ORDER BY Bmdm2, ParDm, Bmdm";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;

                        //DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        //dr["Bmdm"] = "__";
                        //dr["Bmmc"] = "－请选择－";
                        //dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges(); 
                        ds.Tables.RemoveAt(0);

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 部门缓存
            /// </summary>
            public static DataTable DtJd_bm
            {
                get
                {
                    string tblName = "DtJd_bm";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Bmdm,Bmmc FROM jd_bm ORDER BY Bmdm,Bmmc";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;

                        DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        dr["Pkid"] = "__";
                        dr["Bmdm"] = "__";
                        dr["Bmmc"] = "－请选择－";
                        dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges();
                        ds.Tables.RemoveAt(0);

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 党支部缓存
            /// </summary>
            public static DataTable DtJd_dzb
            {
                get
                {
                    string tblName = "DtJd_dzb";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Bmbh,Dzbmc FROM jd_dzb ORDER BY Bmbh,Dzbdm";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;

                        DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        dr["Pkid"] = "__";
                        dr["Bmbh"] = "__";
                        dr["Dzbmc"] = "－请选择－";
                        dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges();
                        ds.Tables.RemoveAt(0);

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 专业缓存
            /// </summary>
            public static DataTable DtJd_zy
            {
                get
                {
                    string tblName = "DtJd_zy";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Bmbh,Dzbbh,Zymc FROM V_jd_zy ORDER BY Bmbh,Zymc";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;

                        DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        dr["Pkid"] = "__";
                        dr["Bmbh"] = "__";
                        dr["Dzbbh"] = "__";
                        dr["Zymc"] = "－请选择－";
                        dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges();
                        ds.Tables.RemoveAt(0);

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 班级缓存
            /// </summary>
            public static DataTable DtJd_bj
            {
                get
                {
                    string tblName = "DtJd_bj";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Bmbh,Dzbbh,Zybh,Bjmc FROM V_jd_bj ORDER BY Bmbh,Dzbbh,Bjmc";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;

                        DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        dr["Pkid"] = "__";
                        dr["Bmbh"] = "__";
                        dr["Dzbbh"] = "__";
                        dr["Zybh"] = "__";
                        dr["Bjmc"] = "－请选择－";
                        dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges();
                        ds.Tables.RemoveAt(0);

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 联系人缓存
            /// </summary>
            public static DataTable DtJc_lxr
            {
                get
                {
                    string tblName = "DtJc_lxr";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Bmbh,Dzbbh,Xm FROM V_jc_lxr WHERE Lbdm>0 ORDER BY Xm,Bmbh,Dzbbh";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;

                        DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        dr["Pkid"] = "__";
                        dr["Bmbh"] = "__";
                        dr["Dzbbh"] = "__";
                        dr["Xm"] = "－请选择－";
                        dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges();
                        ds.Tables.RemoveAt(0);

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 考核指标缓存
            /// </summary>
            public static DataTable DtJd_khzb
            {
                get
                {
                    string tblName = "DtJd_khzb";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Bmbh,Zbdm,Zbmc,Zbqx,Txsm,Fzgs,Zdfz,Yxcy,Yxsc,Yxdn,Pxxh,Sfgd,Sfqy FROM jd_khzb zb ORDER BY Zbdm,Bmbh,Pxxh,Zbmc";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;
                        DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        dr["Pkid"] = "__";
                        dr["Bmbh"] = "__";
                        dr["Zbdm"] = "__";
                        dr["Zbmc"] = "－请选择－";
                        dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges();

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 指标显示栏目缓存
            /// </summary>
            public static DataTable DtJd_zbxs
            {
                get
                {
                    string tblName = "DtJd_zbxs";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Bmbh,Zbbh,Mcbt,Rqbt,Slbt FROM jd_zbxs ORDER BY Bmbh,Zbbh";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;

                        DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        dr["Pkid"] = "__";
                        dr["Bmbh"] = "__";
                        dr["Zbbh"] = "__";
                        dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges();
                        ds.Tables.RemoveAt(0);

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 项目等级缓存
            /// </summary>
            public static DataTable DtJd_xmdj
            {
                get
                {
                    string tblName = "DtJd_xmdj";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Bmbh,Zbbh,Djmc,Fzgs,Zdfz,Synx FROM jd_xmdj ORDER BY Bmbh,Pxxh,Aid";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;

                        DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        dr["Pkid"] = "__";
                        dr["Bmbh"] = "__";
                        dr["Zbbh"] = "__";
                        dr["Djmc"] = "－请选择－";
                        dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges();
                        ds.Tables.RemoveAt(0);

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 项目状态缓存
            /// </summary>
            //public static DataTable DtJd_xmzt
            //{
            //    get
            //    {
            //        string tblName = "DtJd_xmzt";
            //        DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

            //        if (dt == null)
            //        {
            //            string sql = "SELECT Pkid,Bmbh,Zbbh,Ztmc,Fzbl FROM jd_xmzt ORDER BY Bmbh,Aid,Ztmc";
            //            DataSet ds = DbHelperSQL.Query(sql);

            //            dt = ds.Tables[0];
            //            dt.TableName = tblName;

            //            DataRow dr = dt.NewRow(); // 按表字段依次赋值
            //            dr["Pkid"] = "__";
            //            dr["Bmbh"] = "__";
            //            dr["Zbbh"] = "__";
            //            dr["Ztmc"] = "－请选择－";
            //            dt.Rows.InsertAt(dr, 0);
            //            dt.AcceptChanges();
            //            ds.Tables.RemoveAt(0);

            //            TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
            //        }
            //        return dt;
            //    }
            //}

            /// <summary>
            /// 项目选项缓存
            /// </summary>
            public static DataTable DtJd_xmxx
            {
                get
                {
                    string tblName = "DtJd_xmxx";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Bmbh,Zbbh,Djbh,Xxmc,Fjfz FROM jd_xmxx ORDER BY Bmbh,Aid,Xxmc";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;

                        DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        dr["Pkid"] = "__";
                        dr["Bmbh"] = "__";
                        dr["Zbbh"] = "__";
                        dr["Djbh"] = "__";
                        dr["Xxmc"] = "－请选择－";
                        dr["Fjfz"] = "0";
                        dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges();
                        ds.Tables.RemoveAt(0);

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            #endregion

            #region 系统设置表

            /// <summary>
            /// 系统设置缓存
            /// </summary>
            public static DataTable DtXt_xtzt
            {
                get
                {
                    string tblName = "DtXt_xtzt";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Bmbh,Dm,Mc,Qz FROM xt_xtsz ORDER BY Bmbh,Dm";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;
                        ds.Tables.RemoveAt(0);

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }

            /// <summary>
            /// 部门授权缓存
            /// </summary>
            public static DataTable DtXt_bmsq
            {
                get
                {
                    string tblName = "DtXt_bmsq";
                    DataTable dt = (DataTable)TU.Globals.GetObject("$SystemCode$" + tblName);

                    if (dt == null)
                    {
                        string sql = "SELECT Pkid,Bmbh,Sqrq,Dqrq,Zym FROM xt_bmsq ORDER BY Bmbh";
                        DataSet ds = DbHelperSQL.Query(sql);

                        dt = ds.Tables[0];
                        dt.TableName = tblName;

                        for (int i = dt.Rows.Count - 1; i >= 0; i--)
                        {
                            string zym = dt.Rows[i]["Zym"].ToString();
                            string bmbh = dt.Rows[i]["Bmbh"].ToString();
                            string sqrq = dt.Rows[i]["Sqrq"].ToString();
                            string dqrq = dt.Rows[i]["Dqrq"].ToString();
                            if (!TU.Globals.IsMatchSummary(zym, bmbh, sqrq, dqrq, Globals.EncryptCode)) dt.Rows.RemoveAt(i);
                        }

                        DataRow dr = dt.NewRow(); // 按表字段依次赋值
                        dr["Pkid"] = "__";
                        dr["Bmbh"] = "__";
                        dt.Rows.InsertAt(dr, 0);
                        dt.AcceptChanges();

                        TU.Globals.SetObject(dt, "$SystemCode$" + tblName, true);
                    }
                    return dt;
                }
            }


            #endregion

            #region 系统日志表

            /// <summary>
            /// 系统日志
            /// </summary>

            #endregion
        }
    }
}
