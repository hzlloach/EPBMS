using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Tjbb
{
    public class Zk : BLL.Global.Base
    {
        /// <summary>
        /// 统计总表之学院
        /// </summary>
        public static DataTable TjZbByXy(string bmbh)
        {
            string where = bmbh == "BM0".PadRight(32, '0') ? "" :(" WHERE Bmbh='" + bmbh + "'");
            string sql = string.Format("SELECT * FROM Tj_zb_xy {0} ORDER BY Bmdm", where);
            DataTable dt = DAL.Globals.Query(sql);
            dt.Columns.Add("JjfzRs", typeof(System.Int32));
            dt.Columns.Add("YbdyRs", typeof(System.Int32));
            dt.Columns.Add("ZsdyRs", typeof(System.Int32));
            dt.Columns.Add("XjRs", typeof(System.Int32));
            dt.Columns.Add("JjfzHbs", typeof(System.Int32));
            dt.Columns.Add("YbdyHbs", typeof(System.Int32));
            dt.Columns.Add("JjfzFws", typeof(System.Int32));
            dt.Columns.Add("YbdyFws", typeof(System.Int32));
            dt.Columns.Add("ZsdyFws", typeof(System.Int32));
            dt.Columns.Add("DyFws", typeof(System.Int32));
            dt.Columns.Add("BjLxs", typeof(System.Int32));
            dt.Columns.Add("QsLxs", typeof(System.Int32));
            dt.Columns.Add("XsLxs", typeof(System.Int32));
            dt.Columns.Add("YsHjs", typeof(System.Int32));
            dt.Columns.Add("GjHjs", typeof(System.Int32));
            dt.Columns.Add("SjHjs", typeof(System.Int32));
            dt.Columns.Add("XjHjs", typeof(System.Int32));
            dt.Columns.Add("YjHjs", typeof(System.Int32));
            dt.Columns.Add("DyYsHjs", typeof(System.Int32));
            dt.Columns.Add("DyGjHjs", typeof(System.Int32));
            dt.Columns.Add("DySjHjs", typeof(System.Int32));
            dt.Columns.Add("DyXjHjs", typeof(System.Int32));
            dt.Columns.Add("DyYjHjs", typeof(System.Int32)); 

            int[] zs = { 0, 0 };
            int[] zsRs = { 0, 0, 0, 0 };
            int[] zsHbs = { 0, 0 };
            int[] zsFws = { 0, 0, 0, 0 };
            int[] zsLxs = { 0, 0, 0 };
            int[] zsHjs = { 0, 0, 0, 0, 0 };
            int[] zsDyHjs = { 0, 0, 0, 0, 0 };
            foreach (DataRow dr in dt.Rows)
            {
                zs[0] += int.Parse(dr["Zbs"].ToString());
                zs[1] += int.Parse(dr["Dbs"].ToString());
                    
                // 拆分人数并计算小计
                int i=0, xj = 0, d = 0, idx = 11; 
                string[] s = dr["Rs"].ToString().Split('/');
                for (i = 0; i < zsRs.Length - 1; i++)
                {
                    zsRs[i] += d = int.Parse(s[i]);
                    dr[idx + i] = d;
                    xj += d;
                }
                zsRs[3] += xj;
                dr["XjRs"] = xj;

                // 拆分思想汇报数
                idx += 4;
                s = dr["Hbs"].ToString().Split('/');
                for (i = 0; i < zsHbs.Length; i++)
                {
                    zsHbs[i] += d = int.Parse(s[i]);
                    dr[idx + i] = d;
                }

                // 拆分志愿服务时数
                idx += 2;
                s = dr["Fws"].ToString().Split('/');
                for (i = 0, xj = 0; i < zsFws.Length - 1; i++)
                {
                    zsFws[i] += d = int.Parse(s[i]);
                    dr[idx + i] = d;
                    xj += d;
                }
                dr[idx + zsFws.Length - 1] = d = xj - int.Parse(s[0]);
                zsFws[zsFws.Length - 1] += d;

                // 拆分三联系数
                idx += 4;
                s = dr["Lxs"].ToString().Split('/');
                for (i = 0; i < zsLxs.Length; i++)
                {
                    zsLxs[i] += d = int.Parse(s[i]);
                    dr[idx + i] = d;
                }

                // 拆分获奖数
                idx += 3;
                s = dr["Hjs"].ToString().Split('/');
                for (i = zsHjs.Length - 1, xj=0; i > 0; i--)
                {
                    zsHjs[i] += d = int.Parse(s[i - 1]);
                    dr[idx + i] = d;
                    if (i < 3) xj += d;
                }
                zsHjs[0] += xj;
                dr[idx] = xj;

                // 拆分党员获奖数
                idx += 5;
                s = dr["DyHjs"].ToString().Split('/');
                for (i = zsDyHjs.Length - 1, xj = 0; i > 0; i--)
                {
                    zsDyHjs[i] += d = int.Parse(s[i - 1]);
                    dr[idx + i] = d;
                    if (i < 3) xj += d;
                }
                zsDyHjs[0] += xj;
                dr[idx] = xj;
            }

            // 全部学院时增加合计行
            if (where.Length == 0)
            {
                DataRow drHj = dt.NewRow();
                drHj["Bmmc"] = "合　　计";
                drHj["Zbs"] = zs[0]; 
                drHj["Dbs"] = zs[1];
                drHj["JjfzRs"] = zsRs[0];
                drHj["YbdyRs"] = zsRs[1];
                drHj["ZsdyRs"] = zsRs[2];
                drHj["XjRs"] = zsRs[3];
                drHj["JjfzHbs"] = zsHbs[0];
                drHj["YbdyHbs"] = zsHbs[1];
                drHj["JjfzFws"] = zsFws[0];
                drHj["YbdyFws"] = zsFws[1];
                drHj["ZsdyFws"] = zsFws[2];
                drHj["DyFws"] = zsFws[3];
                drHj["Bjlxs"] = zsLxs[0];
                drHj["QsLxs"] = zsLxs[1];
                drHj["XsLxs"] = zsLxs[2];
                drHj["YsHjs"] = zsHjs[0];
                drHj["GjHjs"] = zsHjs[1];
                drHj["SjHjs"] = zsHjs[2];
                drHj["XjHjs"] = zsHjs[3];
                drHj["YjHjs"] = zsHjs[4];
                drHj["DyYsHjs"] = zsDyHjs[0];
                drHj["DyGjHjs"] = zsDyHjs[1];
                drHj["DySjHjs"] = zsDyHjs[2];
                drHj["DyXjHjs"] = zsDyHjs[3];
                drHj["DyYjHjs"] = zsDyHjs[4];
                dt.Rows.Add(drHj);
            }

            dt.AcceptChanges();
            return dt;
        }

        /// <summary>
        /// 统计总表之学院党支部
        /// </summary>
        public static DataTable TjZbByXyDzb(string bmbh, string dzbbh)
        {
            string where = bmbh == "BM0".PadRight(32, '0') ? "" : ("WHERE Bmbh='" + bmbh + "'");
            if (!string.IsNullOrEmpty(dzbbh)) where += " AND Dzbbh='" + dzbbh + "'";
            string sql = string.Format("SELECT * FROM Tj_zb_xydzb {0} ORDER BY Bmdm, Dzbdm", where);
            DataTable dt = DAL.Globals.Query(sql);
            dt.Columns.Add("JjfzRs", typeof(System.Int32));
            dt.Columns.Add("YbdyRs", typeof(System.Int32));
            dt.Columns.Add("ZsdyRs", typeof(System.Int32));
            dt.Columns.Add("XjRs", typeof(System.Int32));
            dt.Columns.Add("JjfzHbs", typeof(System.Int32));
            dt.Columns.Add("YbdyHbs", typeof(System.Int32));
            dt.Columns.Add("JjfzFws", typeof(System.Int32));
            dt.Columns.Add("YbdyFws", typeof(System.Int32));
            dt.Columns.Add("ZsdyFws", typeof(System.Int32));
            dt.Columns.Add("DyFws", typeof(System.Int32));
            dt.Columns.Add("BjLxs", typeof(System.Int32));
            dt.Columns.Add("QsLxs", typeof(System.Int32));
            dt.Columns.Add("XsLxs", typeof(System.Int32));
            dt.Columns.Add("YsHjs", typeof(System.Int32));
            dt.Columns.Add("GjHjs", typeof(System.Int32));
            dt.Columns.Add("SjHjs", typeof(System.Int32));
            dt.Columns.Add("XjHjs", typeof(System.Int32));
            dt.Columns.Add("YjHjs", typeof(System.Int32));
            dt.Columns.Add("DyYsHjs", typeof(System.Int32));
            dt.Columns.Add("DyGjHjs", typeof(System.Int32));
            dt.Columns.Add("DySjHjs", typeof(System.Int32));
            dt.Columns.Add("DyXjHjs", typeof(System.Int32));
            dt.Columns.Add("DyYjHjs", typeof(System.Int32));

            int[] zs = { 0, 0 };
            int[] zsRs = { 0, 0, 0, 0 };
            int[] zsHbs = { 0, 0 };
            int[] zsFws = { 0, 0, 0, 0 };
            int[] zsLxs = { 0, 0, 0 };
            int[] zsHjs = { 0, 0, 0, 0, 0 };
            int[] zsDyHjs = { 0, 0, 0, 0, 0 };
            foreach (DataRow dr in dt.Rows)
            {
                zs[1] += int.Parse(dr["Dbs"].ToString());

                // 拆分人数并计算小计
                int i = 0, xj = 0, d = 0, idx = 13;
                string[] s = dr["Rs"].ToString().Split('/');
                for (i = 0; i < zsRs.Length - 1; i++)
                {
                    zsRs[i] += d = int.Parse(s[i]);
                    dr[idx + i] = d;
                    xj += d;
                }
                zsRs[3] += xj;
                dr["XjRs"] = xj;

                // 拆分思想汇报数量
                idx += 4;
                s = dr["Hbs"].ToString().Split('/');
                for (i = 0; i < zsHbs.Length; i++)
                {
                    zsHbs[i] += d = int.Parse(s[i]);
                    dr[idx + i] = d;
                }

                // 拆分志愿服务时数
                idx += 2;
                s = dr["Fws"].ToString().Split('/');
                for (i = 0, xj = 0; i < zsFws.Length - 1; i++)
                {
                    zsFws[i] += d = int.Parse(s[i]);
                    dr[idx + i] = d;
                    xj += d;
                }
                dr[idx + zsFws.Length - 1] = d = xj - int.Parse(s[0]);
                zsFws[zsFws.Length - 1] += d;

                // 拆分三联系数
                idx += 4;
                s = dr["Lxs"].ToString().Split('/');
                for (i = 0; i < zsLxs.Length; i++)
                {
                    zsLxs[i] += d = int.Parse(s[i]);
                    dr[idx + i] = d;
                }

                // 拆分获奖数
                idx += 3;
                s = dr["Hjs"].ToString().Split('/');
                for (i = zsHjs.Length - 1, xj = 0; i > 0; i--)
                {
                    zsHjs[i] += d = int.Parse(s[i - 1]);
                    dr[idx + i] = d;
                    if (i < 3) xj += d;
                }
                zsHjs[0] += xj;
                dr[idx] = xj;

                // 拆分党员获奖数
                idx += 5;
                s = dr["DyHjs"].ToString().Split('/');
                for (i = zsDyHjs.Length - 1, xj = 0; i > 0; i--)
                {
                    zsDyHjs[i] += d = int.Parse(s[i - 1]);
                    dr[idx + i] = d;
                    if (i < 3) xj += d;
                }
                zsDyHjs[0] += xj;
                dr[idx] = xj;
            }

            // 非单个党支部时增加合计行
            if (string.IsNullOrEmpty(dzbbh))
            {
                DataRow drHj = dt.NewRow();
                drHj["Dzbmc"] = "合　　计";
                drHj["Dbs"] = zs[1];
                drHj["JjfzRs"] = zsRs[0];
                drHj["YbdyRs"] = zsRs[1];
                drHj["ZsdyRs"] = zsRs[2];
                drHj["XjRs"] = zsRs[3];
                drHj["JjfzHbs"] = zsHbs[0];
                drHj["YbdyHbs"] = zsHbs[1];
                drHj["JjfzFws"] = zsFws[0];
                drHj["YbdyFws"] = zsFws[1];
                drHj["ZsdyFws"] = zsFws[2];
                drHj["DyFws"] = zsFws[3];
                drHj["Bjlxs"] = zsLxs[0];
                drHj["QsLxs"] = zsLxs[1];
                drHj["XsLxs"] = zsLxs[2];
                drHj["YsHjs"] = zsHjs[0];
                drHj["GjHjs"] = zsHjs[1];
                drHj["SjHjs"] = zsHjs[2];
                drHj["XjHjs"] = zsHjs[3];
                drHj["YjHjs"] = zsHjs[4];
                drHj["DyYsHjs"] = zsDyHjs[0];
                drHj["DyGjHjs"] = zsDyHjs[1];
                drHj["DySjHjs"] = zsDyHjs[2];
                drHj["DyXjHjs"] = zsDyHjs[3];
                drHj["DyYjHjs"] = zsDyHjs[4];
                dt.Rows.Add(drHj);
            }

            dt.AcceptChanges();
            return dt;
        }

        /// <summary>
        /// 统计总表之学院联系人
        /// </summary>
        public static DataTable TjZbByXyLxr(string lxrbh)
        {
            string sql = string.Format("SELECT * FROM Tj_zb_lxr WHERE Pkid='{0}'", lxrbh);
            DataTable dt = DAL.Globals.Query(sql);
            dt.Columns.Add("JjfzRs", typeof(System.Int32));
            dt.Columns.Add("YbdyRs", typeof(System.Int32));
            dt.Columns.Add("ZsdyRs", typeof(System.Int32));
            dt.Columns.Add("XjRs", typeof(System.Int32));
            dt.Columns.Add("JjfzHbs", typeof(System.Int32));
            dt.Columns.Add("YbdyHbs", typeof(System.Int32));
            dt.Columns.Add("JjfzFws", typeof(System.Int32));
            dt.Columns.Add("YbdyFws", typeof(System.Int32));
            dt.Columns.Add("ZsdyFws", typeof(System.Int32));
            dt.Columns.Add("DyFws", typeof(System.Int32));
            dt.Columns.Add("BjLxs", typeof(System.Int32));
            dt.Columns.Add("QsLxs", typeof(System.Int32));
            dt.Columns.Add("XsLxs", typeof(System.Int32));
            dt.Columns.Add("YsHjs", typeof(System.Int32));
            dt.Columns.Add("GjHjs", typeof(System.Int32));
            dt.Columns.Add("SjHjs", typeof(System.Int32));
            dt.Columns.Add("XjHjs", typeof(System.Int32));
            dt.Columns.Add("YjHjs", typeof(System.Int32));
            dt.Columns.Add("DyYsHjs", typeof(System.Int32));
            dt.Columns.Add("DyGjHjs", typeof(System.Int32));
            dt.Columns.Add("DySjHjs", typeof(System.Int32));
            dt.Columns.Add("DyXjHjs", typeof(System.Int32));
            dt.Columns.Add("DyYjHjs", typeof(System.Int32));

            int[] zs = { 0, 0 };
            int[] zsRs = { 0, 0, 0, 0 };
            int[] zsHbs = { 0, 0 };
            int[] zsFws = { 0, 0, 0, 0 };
            int[] zsLxs = { 0, 0, 0 };
            int[] zsHjs = { 0, 0, 0, 0, 0 };
            int[] zsDyHjs = { 0, 0, 0, 0, 0 };
            foreach (DataRow dr in dt.Rows)
            {
                zs[1] += int.Parse(dr["Dbs"].ToString());

                // 拆分人数并计算小计
                int i = 0, xj = 0, d = 0, idx = 19;
                string[] s = dr["Rs"].ToString().Split('/');
                for (i = 0; i < zsRs.Length - 1; i++)
                {
                    zsRs[i] += d = int.Parse(s[i]);
                    dr[idx + i] = d;
                    xj += d;
                }
                zsRs[3] += xj;
                dr["XjRs"] = xj;

                // 拆分思想汇报数量
                idx += 4;
                s = dr["Hbs"].ToString().Split('/');
                for (i = 0; i < zsHbs.Length; i++)
                {
                    zsHbs[i] += d = int.Parse(s[i]);
                    dr[idx + i] = d;
                }

                // 拆分志愿服务时数
                idx += 2;
                s = dr["Fws"].ToString().Split('/');
                for (i = 0, xj = 0; i < zsFws.Length - 1; i++)
                {
                    zsFws[i] += d = int.Parse(s[i]);
                    dr[idx + i] = d;
                    xj += d;
                }
                dr[idx + zsFws.Length - 1] = d = xj - int.Parse(s[0]);
                zsFws[zsFws.Length - 1] += d;

                // 拆分三联系数
                idx += 4;
                s = dr["Lxs"].ToString().Split('/');
                for (i = 0; i < zsLxs.Length; i++)
                {
                    zsLxs[i] += d = int.Parse(s[i]);
                    dr[idx + i] = d;
                }

                // 拆分获奖数
                idx += 3;
                s = dr["Hjs"].ToString().Split('/');
                for (i = zsHjs.Length - 1, xj = 0; i > 0; i--)
                {
                    zsHjs[i] += d = int.Parse(s[i - 1]);
                    dr[idx + i] = d;
                    if (i < 3) xj += d;
                }
                zsHjs[0] += xj;
                dr[idx] = xj;

                // 拆分党员获奖数
                idx += 5;
                s = dr["DyHjs"].ToString().Split('/');
                for (i = zsDyHjs.Length - 1, xj = 0; i > 0; i--)
                {
                    zsDyHjs[i] += d = int.Parse(s[i - 1]);
                    dr[idx + i] = d;
                    if (i < 3) xj += d;
                }
                zsDyHjs[0] += xj;
                dr[idx] = xj;
            }

            return dt;
        }

        public static DataTable TjByXy_XXX(string bmbh)
        {
            string where = bmbh == "BM0".PadRight(32, '0') ? "" : (" AND b.Pkid='" + bmbh + "'");
            string sql = string.Format("SELECT b.Bmmc" +
                                       "	, Rs=STUFF(( " +
                                       "		SELECT '/'+CONVERT(varchar,b0.Rs) " +
                                       "		FROM (" +
                                       "			SELECT zt.Dm, Rs=( " +
                                       "				SELECT COUNT(*) " +
                                       "				FROM jc_xs s " +
                                       "				WHERE (CASE s.Fzztdm WHEN 5 THEN 5 WHEN 6 THEN 6 ELSE 4 END)=zt.Dm AND s.Bmbh=b.Pkid " +
                                       "			) " +
                                       "			FROM dm_fzzt zt " +
                                       "			WHERE zt.Dm>3 " +
                                       "		) b0 " +
                                       "		ORDER BY Dm FOR XML PATH('') " +
                                       "	),1,1,'') " +
                                       "FROM jd_bm b " + 
                                       "WHERE b.Bmdm <> '00' " + "{0} " +
                                       "ORDER BY b.Bmdm", where);
            DataTable dt = DAL.Globals.Query(sql);
            dt.Columns.Add("Jjfz", typeof(System.Int32));
            dt.Columns.Add("Ybdy", typeof(System.Int32));
            dt.Columns.Add("Zsdy", typeof(System.Int32));
            dt.Columns.Add("Xj", typeof(System.Int32));

            int[] zs = { 0, 0, 0, 0 };
            foreach (DataRow dr in dt.Rows)
            {
                // 拆分人数并计算小计
                int i = 0, xj = 0, idx = 0;
                string[] s = dr["Rs"].ToString().Split('/');
                for (i = 0, idx = 2; i < zs.Length - 1; i++)
                {
                    int d = int.Parse(s[i]);
                    zs[i] += d;
                    xj += d;
                    dr[idx + i] = d;
                }
                zs[3] += xj;
                dr["Xj"] = xj;
            }

            // 全部学院时增加合计行
            if (where.Length == 0)
            {
                DataRow drHj = dt.NewRow();
                drHj[0] = "合　　计";
                drHj[1] = "0";
                drHj[2] = zs[0];
                drHj[3] = zs[1];
                drHj[4] = zs[2];
                drHj[5] = zs[3];
                dt.Rows.Add(drHj);
            }

            dt.AcceptChanges();
            return dt;
        }

        public static DataTable TjByXyDzb(string bmbh, string dzbbh)
        {
            string where = bmbh == "BM0".PadRight(32, '0') ? "" : (!string.IsNullOrEmpty(dzbbh) ? (" AND d.Pkid='" + dzbbh + "'") : (" AND b.Pkid='" + bmbh + "'"));
            string sql = string.Format("SELECT b.Bmmc, d.Dzbmc" +
                                       "	, Rs=STUFF(( " +
                                       "		SELECT '/'+CONVERT(varchar,b0.Rs) " +
                                       "		FROM (" +
                                       "			SELECT zt.Dm, Rs=( " +
                                       "				SELECT COUNT(*) " +
                                       "				FROM jc_xs s " +
                                       "				WHERE (CASE s.Fzztdm WHEN 5 THEN 5 WHEN 6 THEN 6 ELSE 4 END)=zt.Dm AND s.Bmbh=b.Pkid AND s.Dzbbh=d.Pkid " +
                                       "			) " +
                                       "			FROM dm_fzzt zt " +
                                       "			WHERE zt.Dm>3 " +
                                       "		) b0 " +
                                       "		ORDER BY Dm FOR XML PATH('') " +
                                       "	),1,1,'') " +
                                       "FROM jd_bm b INNER JOIN jd_dzb d ON d.Bmbh=b.Pkid " +
                                       "WHERE b.Bmdm <> '00' " + "{0} " +
                                       "ORDER BY b.Bmdm,2", where);
            DataTable dt = DAL.Globals.Query(sql);
            dt.Columns.Add("Jjfz", typeof(System.Int32));
            dt.Columns.Add("Ybdy", typeof(System.Int32));
            dt.Columns.Add("Zsdy", typeof(System.Int32));
            dt.Columns.Add("Xj", typeof(System.Int32));

            DataRow drXj = null;
            bool isDzb = !string.IsNullOrEmpty(dzbbh); // 是否党支部
            string prefix = "　　";
            int[] zs = { 0, 0, 0, 0 };
            int count = dt.Rows.Count - 1;
            string prebmmc = "", bmmc = "";
            for (int k = count; k >= 0; k--)
            {
                int i = 0, xj = 0, idx = 0;
                DataRow dr = dt.Rows[k];
                bmmc = dr["Bmmc"].ToString();

                // 不是党支部，不用汇总学院数据
                if (!isDzb)
                {
                    if (bmmc != prebmmc)
                    {
                        if (k != count)
                        {
                            // 新增学院的汇总数据
                            drXj = dt.NewRow();
                            drXj[0] = drXj[1] = prebmmc;
                            drXj[2] = "0";
                            drXj[3] = zs[0];
                            drXj[4] = zs[1];
                            drXj[5] = zs[2];
                            drXj[6] = zs[3];
                            dt.Rows.InsertAt(drXj, k + 1);
                        }
                        prebmmc = bmmc;
                        zs[0] = zs[1] = zs[2] = zs[3] = 0;
                    }
                    dr["Dzbmc"] = prefix + dr["Dzbmc"].ToString();
                }

                // 拆分人数并计算小计
                string[] s = dr["Rs"].ToString().Split('/');
                for (i = 0, idx = 3; i < zs.Length - 1; i++)
                {
                    int d = int.Parse(s[i]);
                    zs[i] += d;
                    xj += d;
                    dr[idx + i] = d;
                }
                zs[3] += xj;
                dr["Xj"] = xj;
            }

            // 不是党支部，新增第一个学院的汇总数据
            if (!isDzb)
            {                
                drXj = dt.NewRow();
                drXj[0] = drXj[1] = bmmc;
                drXj[2] = "0";
                drXj[3] = zs[0];
                drXj[4] = zs[1];
                drXj[5] = zs[2];
                drXj[6] = zs[3];
                dt.Rows.InsertAt(drXj, 0);
            }

            dt.AcceptChanges();
            return dt;
        }

        public static DataTable TjByXyZy(string bmbh)
        {
            string where = bmbh == "BM0".PadRight(32, '0') ? "" : (" AND b.Pkid='" + bmbh + "'");
            string sql = string.Format("SELECT b.Bmmc, z.Zymc" +
                                       "	, Rs=STUFF(( " +
                                       "		SELECT '/'+CONVERT(varchar,b0.Rs) " +
                                       "		FROM (" +
                                       "			SELECT zt.Dm, Rs=( " +
                                       "				SELECT COUNT(*) " +
                                       "				FROM jc_xs s " +
                                       "				WHERE (CASE s.Fzztdm WHEN 5 THEN 5 WHEN 6 THEN 6 ELSE 4 END)=zt.Dm AND s.Bmbh=b.Pkid AND s.Zybh=z.Pkid " +
                                       "			) " +
                                       "			FROM dm_fzzt zt " +
                                       "			WHERE zt.Dm>3 " +
                                       "		) b0 " +
                                       "		ORDER BY Dm FOR XML PATH('') " +
                                       "	),1,1,'') " +
                                       "FROM jd_bm b INNER JOIN jd_zy z ON z.Bmbh=b.Pkid " +
                                       "WHERE b.Bmdm <> '00' " + "{0} " +
                                       "ORDER BY b.Bmdm,2", where);
            DataTable dt = DAL.Globals.Query(sql);
            dt.Columns.Add("Jjfz", typeof(System.Int32));
            dt.Columns.Add("Ybdy", typeof(System.Int32));
            dt.Columns.Add("Zsdy", typeof(System.Int32));
            dt.Columns.Add("Xj", typeof(System.Int32));

            DataRow drXj = null;
            string prefix = "　　";
            int[] zs = { 0, 0, 0, 0 };
            int count = dt.Rows.Count - 1;
            string prebmmc = "", bmmc = "";
            for (int k = count; k >= 0; k--)
            {
                int i = 0, xj = 0, idx = 0;
                DataRow dr = dt.Rows[k];
                bmmc = dr["Bmmc"].ToString();

                // 新增学院的汇总数据
                if (bmmc != prebmmc)
                {
                    if (k != count)
                    {
                        drXj = dt.NewRow();
                        drXj[0] = drXj[1] = prebmmc;
                        drXj[2] = "0";
                        drXj[3] = zs[0];
                        drXj[4] = zs[1];
                        drXj[5] = zs[2];
                        drXj[6] = zs[3];
                        dt.Rows.InsertAt(drXj, k + 1);
                    }
                    prebmmc = bmmc;
                    zs[0] = zs[1] = zs[2] = zs[3] = 0;
                }
                dr["Zymc"] = prefix + dr["Zymc"].ToString();

                // 拆分人数并计算小计
                string[] s = dr["Rs"].ToString().Split('/');
                for (i = 0, idx = 3; i < zs.Length - 1; i++)
                {
                    int d = int.Parse(s[i]);
                    zs[i] += d;
                    xj += d;
                    dr[idx + i] = d;
                }
                zs[3] += xj;
                dr["Xj"] = xj;
            }

            // 新增第一个学院的汇总数据
            drXj = dt.NewRow();
            drXj[0] = drXj[1] = bmmc;
            drXj[2] = "0";
            drXj[3] = zs[0];
            drXj[4] = zs[1];
            drXj[5] = zs[2];
            drXj[6] = zs[3];
            dt.Rows.InsertAt(drXj, 0);

            dt.AcceptChanges();
            return dt;
        }


        public static DataRow TjZkByLxr(string lxrbh)
        {
            string sql = string.Format("SELECT * FROM Tj_zb_lxr WHERE Pkid='{0}'", lxrbh);
            DataTable dt = DAL.Globals.Query(sql);
            if (dt.Rows.Count > 0) return dt.Rows[0];
            else return null;
        }

    }
}
