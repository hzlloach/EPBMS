using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data;
using TU = TStar.Utility;

namespace BLL
{
    public partial class Globals
    {
        public static WSSmsService.SmsServiceSoapClient WsSms = new WSSmsService.SmsServiceSoapClient();

        #region 公共方法

        public static string SendMsg(string phone, string content)
        {
            return WsSms.SendMsg(phone, content);
        }

        public static DateTime Min(DateTime dt1, DateTime dt2)
        {
            return dt1 <= dt2 ? dt1 : dt2;
        }

        public static string MD5Encrypt(string input)
        {
            return TU.Globals.MD5Encrypt(SystemSetting.EncryptCode + input);
        }

        // 补全行集合
        public static void MergeDataTable(DataTable dtOrignal, DataView dvColumn, string columnID, string filter, string sort)
        {
            DataRow dr;
            DataView dv = dtOrignal.DefaultView;
            dv.Sort = columnID;
            dvColumn.RowFilter = filter;
            foreach (DataRowView drv in dvColumn)
            {
                string colText = drv[columnID].ToString();
                if (dv.Find(colText) == -1)
                {
                    dr = dtOrignal.NewRow();
                    foreach (DataColumn dc in dtOrignal.Columns)
                    {
                        if (dc.ColumnName == columnID) dr[columnID] = colText;
                        else
                        {
                            switch (dc.DataType.Name)
                            {
                                case "String": dr[dc.ColumnName] = ""; break;
                                case "Int":
                                case "Float":
                                case "Decimal": dr[dc.ColumnName] = 0; break;
                                default: dr[dc.ColumnName] = System.DBNull.Value; break;
                            }
                        }
                    }
                    dtOrignal.Rows.Add(dr);
                }
            }
            dvColumn.RowFilter = "";
            dv.Sort = sort;
        }
        public static void MergeDataTable2(DataTable dtOrignal, DataView dvColumn, string columnIDs, string filter, string sort)
        {
            string[] columnID = columnIDs.Split(',');
            int colCnt = dtOrignal.Columns.Count;
            DataRow dr;
            DataView dv = dtOrignal.DefaultView;
            dv.Sort = columnID[0];
            dvColumn.RowFilter = filter;
            foreach (DataRowView drv in dvColumn)
            {
                string colText = drv[columnID[0]].ToString();
                if (dv.Find(colText) == -1)
                {
                    dr = dtOrignal.NewRow();
                    for (int i = 0; i < colCnt; i++)
                    {
                        DataColumn dc = dtOrignal.Columns[i];
                        if (dc.ColumnName == columnID[0])
                        {
                            dr[columnID[0]] = colText;
                            dr[columnID[1]] = drv[columnID[1]].ToString();
                            i++;
                        }
                        else
                        {
                            switch (dc.DataType.Name)
                            {
                                case "String": dr[dc.ColumnName] = ""; break;
                                case "Int":
                                case "Float":
                                case "Decimal": dr[dc.ColumnName] = 0; break;
                                default: dr[dc.ColumnName] = System.DBNull.Value; break;
                            }
                        }
                    }
                    dtOrignal.Rows.Add(dr);
                }
            }
            dvColumn.RowFilter = "";
            dv.Sort = sort;
        }

        // 补全列集合，并分解到列
        public static DataTable ResolveDataTable(DataTable dtOrignal, DataView dvColumn, string columnIDs, string matchColumnID, string resolveColumnID, string filter, string sort)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            Type colType = dtOrignal.Columns[resolveColumnID].DataType;
            string[] cols = columnIDs.Split(',');
            foreach (string col in cols)
            {
                dt.Columns.Add(col, typeof(System.String));
            }
            string oldSort = dvColumn.Sort;
            dvColumn.RowFilter = filter;
            dvColumn.Sort = matchColumnID;
            foreach (DataRowView drv in dvColumn)
            {
                string colName = drv[matchColumnID].ToString();
                DataColumn dc = new DataColumn(colName, colType);
                dc.DefaultValue = 0;
                dt.Columns.Add(dc);
            }
            dvColumn.Sort = oldSort;
            dvColumn.RowFilter = "";

            DataView dv = dt.DefaultView;
            foreach (DataRow dr in dtOrignal.Rows)
            {
                sb = new StringBuilder();
                foreach (string col in cols)
                {
                    sb.Append(String.Format(" AND {0}='{1}'", col, dr[col].ToString()));
                }
                dv.RowFilter = sb.ToString().Substring(5);
                DataRow ndr;
                if (dv.Count == 0)
                {
                    ndr = dt.NewRow();
                    foreach (string col in cols)
                    {
                        ndr[col] = dr[col];
                    }
                    dt.Rows.Add(ndr);
                }
                else ndr = dv[0].Row;
                ndr[dr[matchColumnID].ToString()] = dr[resolveColumnID];
            }
            dv.RowFilter = "";
            dv.Sort = sort;

            return dt;
        }

        /// <summary>
        /// 分解数据表（一行数据），将行列信息转换成行数据
        /// </summary>
        /// <param name="dtOrignal">原始数据表</param>
        /// <param name="fields">要转换的列字段，多个字段用,分隔</param>
        /// <param name="titles">对应的字段名称，多个字段用,分隔</param>
        /// <returns></returns>
        public static DataTable SplitDataTable(DataTable dtOrignal, string fields, string titles)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(System.String));
            dt.Columns.Add("Value", typeof(System.String));
            string[] cols = fields.Split(',');
            string[] tits = titles.Split(',');

            for (int i = 0; i < cols.Length; i++)
            {
                int idx = dtOrignal.Columns.IndexOf(cols[i]);
                if (idx == -1) continue;

                DataRow dr = dt.NewRow();
                dr[0] = tits[i];
                dr[1] = dtOrignal.Rows[0][idx].ToString();
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// 分解数据表，将行列信息转换成行数据
        /// </summary>
        /// <param name="dtOrignal">原始数据表</param>
        /// <param name="valField">行条件时对应的值字段名称</param>
        /// <param name="fields">要转换的列字段，多个字段用,分隔；如果是行条件，则为键值对，如：type=1</param>
        /// <param name="titles">对应的字段名称，多个字段用,分隔</param>
        /// <param name="units">对应的单位，多个字段用,分隔</param>
        /// <returns></returns>
        public static DataTable SplitDataTable(DataTable dtOrignal, string valField, string fields, string titles, string units)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(System.String));
            dt.Columns.Add("Value", typeof(System.String));
            string[] cols = fields.Split(',');
            string[] tits = titles.Split(',');
            string[] _units = units.Split(',');

            for (int i = 0; i < cols.Length; i++)
            {
                string unit = string.IsNullOrEmpty(_units[i]) ? "" : (" " + _units[i]);
                int idx = dtOrignal.Columns.IndexOf(cols[i]);
                if (idx > -1) // 是列信息
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = tits[i];
                    dr[1] = dtOrignal.Rows[0][idx].ToString() + unit;
                    dt.Rows.Add(dr);
                }
                else // 行数据
                {
                    DataView dv = dtOrignal.DefaultView;
                    dv.RowFilter = cols[i];
                    if (dv.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = tits[i];
                        dr[1] = dv[0][valField].ToString() + unit;
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// 用于调试时查看表中数据
        /// </summary>
        /// <param name="dv">表对应的视图</param>
        public static string ShowDV(DataView dv)
        {
            DataTable dt = dv.Table;
            string s = "", t = "", r = "";
            foreach (DataColumn dc in dt.Columns)
            {
                s += "	" + dc.ColumnName;
            }
            s = s.Substring(1);
            int k = 0;
            foreach (DataRowView drv in dv)
            {
                t = "";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    r = "	" + /*dt.Columns[i].ColumnName + "：" + */drv[i].ToString();
                    //if (r.Length <= 32) 
                    t += r;
                }
                s += "\n【" + (++k) + "】" + t.Substring(1);
            }

            return s;
        }

        #endregion
    }
}
