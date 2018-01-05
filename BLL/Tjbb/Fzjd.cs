using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Tjbb
{
    public class Fzjd : BLL.Global.Base
    {
        public static DataTable TjSum(string bmbh)
        {
            string where = bmbh == "BM0".PadRight(32, '0') ? "" : (" AND b.Pkid='" + bmbh + "'");
            string sql = string.Format("SELECT Rs=STUFF(( " + 
                                       "    SELECT '/'+CONVERT(varchar,b0.Rs) " + 
                                       "	FROM (" + 
                                       "		SELECT zt.Dm, Rs=( " + 
                                       "			SELECT COUNT(*) " + 
                                       "			FROM jc_xs s INNER JOIN jd_bm b ON b.Bmdm <> '00' AND s.Bmbh=b.Pkid " + 
                                       "			WHERE (CASE s.Fzztdm WHEN 5 THEN 5 WHEN 6 THEN 6 ELSE 4 END)=zt.Dm {0}" + //--AND b.Pkid='BM05A0C5144D7F8417CB89B137A20FF4'" + 
                                       "		) " + 
                                       "		FROM dm_fzzt zt " + 
                                       "		WHERE zt.Dm>3 " +
                                       "    ) b0 " +
                                       "	ORDER BY Dm FOR XML PATH('') " + 
                                       "),1,1,'')", where);
            DataTable dt = DAL.Globals.Query(sql);
            dt.Columns.Add("Jjfz", typeof(System.Int32));
            dt.Columns.Add("Ybdy", typeof(System.Int32));
            dt.Columns.Add("Zsdy", typeof(System.Int32));
            dt.Columns.Add("Xj", typeof(System.Int32));

            int[] zs = { 0, 0, 0, 0 };
            foreach (DataRow dr in dt.Rows)
            {
                // 拆分人数并计算小计
                int xj = 0;
                string[] s = dr["Rs"].ToString().Split('/');
                for (int i = 0, idx = 1; i < zs.Length - 1; i++)
                {
                    int d = int.Parse(s[i]);
                    zs[i] += d;
                    xj += d;
                    dr[idx + i] = d;
                }
                zs[3] += xj;
                dr["Xj"] = xj;
            }

            dt.AcceptChanges();
            return dt;
        }

        public static DataTable TjByXy(string bmbh)
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
                                       "	),1,1,''), b.Pkid " +
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
                int xj = 0;
                string[] s = dr["Rs"].ToString().Split('/');
                for (int i = 0, idx = 3; i < zs.Length - 1; i++)
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
                drHj["Jjfz"] = zs[0];
                drHj["Ybdy"] = zs[1];
                drHj["Zsdy"] = zs[2];
                drHj["Xj"] = zs[3];
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
                                       "ORDER BY b.Bmdm,d.Dzbdm", where);
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
                int xj = 0;
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
                for (int i = 0, idx = 3; i < zs.Length - 1; i++)
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
                int xj = 0;
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
                for (int i = 0, idx = 3; i < zs.Length - 1; i++)
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
    }
}
