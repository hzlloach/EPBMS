using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;
using System.Web;

namespace DAL
{
    public partial class Globals
    {
        /// <summary>
        /// 加解密附加码
        /// </summary>
        public static string EncryptCode = "$TStar_PBMS$";

        public string GetParaValue(string paraName, string nullValue)
        {            
            object o = HttpContext.Current.Request.QueryString[paraName];
            if (o != null) return o.ToString();

            o = HttpContext.Current.Request.Form[paraName];
            if (o != null) return o.ToString();

            o = HttpContext.Current.Session[paraName];
            if (o != null) return o.ToString();

            o = HttpContext.Current.Application[paraName];
            if (o != null) return o.ToString();

            return nullValue;
        }



        /// <summary>
        /// 执行SQL语句
        /// </summary>
        public static int Execute(string sql)
        {
            return DbHelperSQL.ExecuteSql(sql);
        }

        /// <summary>
        /// 执行SQL语句，返回单个结果
        /// </summary>
        public static object GetSingle(string sql)
        {
            return DbHelperSQL.GetSingle(sql);
        }

        /// <summary>
        /// 获取执行结果集
        /// </summary>
        public static DataTable Query(string sql)
        {
            return DbHelperSQL.Query(sql).Tables[0];
        }

        /// <summary>
        /// 获取执行结果集
        /// </summary>
        public static DataTable Query(string totalField, string tblName, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(String.Format("SELECT {0} FROM {1}", totalField, tblName));
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }

        /// <summary>
        /// 获取执行结果集
        /// </summary>
        public static DataTable Query(string totalField, string tblName, string strWhere, string strSort)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(String.Format("SELECT {0} FROM {1}", totalField, tblName));
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            if (!string.IsNullOrEmpty(strSort))
            {
                strSql.Append(" ORDER BY " + strSort);
            }
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public static int GetRecordCount(string tblName, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM " + tblName);
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public static DataSet GetListByPage(int pageSize, int pageIndex, string tblName, string strWhere, string sort)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@ordName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000)
                    };
            parameters[0].Value = tblName;
            parameters[1].Value = String.IsNullOrEmpty(sort) ? "Pkid" : sort;
            parameters[2].Value = pageSize;
            parameters[3].Value = pageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = strWhere;
            return DbHelperSQL.RunProcedure("SP_GetRecordByPage", parameters, "ds");
        }

        //public static DataSet GetTjList(int pageSize, int pageIndex, string sql, string total, string sort)
        //{
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@strTotal", SqlDbType.VarChar, 4000),
        //            new SqlParameter("@totName", SqlDbType.VarChar, 255),
        //            new SqlParameter("@ordName", SqlDbType.VarChar, 255),
        //            new SqlParameter("@PageSize", SqlDbType.Int),
        //            new SqlParameter("@PageIndex", SqlDbType.Int)
        //            };
        //    parameters[0].Value = sql.Replace("\r\n", "");
        //    parameters[1].Value = total;
        //    parameters[2].Value = sort;
        //    parameters[3].Value = pageSize;
        //    parameters[4].Value = pageIndex;
        //    return DbHelperSQL.RunProcedure("SP_GetTotalRecordByPage", parameters, "ds");
        //}
    }
}

