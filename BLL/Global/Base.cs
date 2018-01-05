using System;
using System.Data;
using System.Linq;
using System.Text;
using TUD = TStar.Utility.DataSource;

namespace BLL.Global
{
    public class Base
    {
        /// <summary>
        /// 根据给定的Pkid以及指定字段及其值判断是否存在相应记录
        /// </summary>
        public static bool Exists<T>(string pkid, string field, string key)
        {
            return TUD.SQLHelper.Exists<T>("Pkid", pkid, new string[] { field }, new object[] { key });
        }

        /// <summary>
        /// 根据给定的Pkid以及指定字段及其值判断是否存在相应记录
        /// </summary>
        public static bool Exists<T>(string pkid, string[] fields, params string[] keys)
        {
            return TUD.SQLHelper.Exists<T>("Pkid", pkid, fields, keys);
        }

        /// <summary>
        /// 插入记录
        /// </summary>
        public static int Insert<T>(T t)
        {
            return TUD.SQLHelper.Insert<T>(t);
        }

        /// <summary>
        /// 插入记录，指定一个字段可为表达式值
        /// </summary>
        public static int Insert<T>(T t, string field, string expression)
        {
            return TUD.SQLHelper.Insert<T>(t, field, expression);//new string[] { field }, new string[] { expression });
        }

        /// <summary>
        /// 插入记录，指定字段可为表达式值
        /// </summary>
        public static int Insert<T>(T t, string[] fields, string[] expressions)
        {
            return TUD.SQLHelper.Insert<T>(t, fields, expressions);
        }

        /// <summary>
        /// 根据给定的Pkid修改一条记录
        /// </summary>
        public static int Update<T>(T t)
        {
            return TUD.SQLHelper.Update<T>(t);
        }

        /// <summary>
        /// 根据给定的Pkid修改一条记录，指定字段可为表达式值
        /// </summary>
        public static int Update<T>(T t, string field, string expression)
        {
            return TUD.SQLHelper.Update<T>(t, field, expression);
        }

        /// <summary>
        /// 根据给定的Pkid修改一条记录，指定字段可为表达式值
        /// </summary>
        public static int Update<T>(T t, string[] fields, string[] expressions)
        {
            return TUD.SQLHelper.Update<T>(t, fields, expressions);
        }

        /// <summary>
        /// 根据给定的字段修改一条记录
        /// </summary>
        public static int Update<T>(T t, string[] fields)
        {
            return TUD.SQLHelper.Update<T>(t, fields);
        }

        /// <summary>
        /// 根据Pkid修改指定字段的值
        /// </summary>
        public static int UpdateFields<T>(T t, params string[] fields)
        {
            return TUD.SQLHelper.UpdateFields<T>(t, fields);
        }

        /// <summary>
        /// 根据指定条件修改指定字段的值
        /// </summary>
        public static int UpdateFields<T>(string field, object key, string where)
        {
            return TUD.SQLHelper.UpdateFields<T>(field, key, where);
        }

        /// <summary>
        /// 根据指定条件修改指定字段的值
        /// </summary>
        public static int UpdateFields<T>(string[] field, object[] key, string where)
        {
            return TUD.SQLHelper.UpdateFields<T>(field, key, where);
        }

        /// <summary>
        /// 根据给定的Pkid删除一条记录
        /// </summary>
        public static int Delete<T>(string pkid)
        {
            try
            {
                return TUD.SQLHelper.Delete<T>(pkid);
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
        /// 根据给定的字段及其值删除一条记录
        /// </summary>
        public static int Delete<T>(string field, object key)
        {
            try
            {
                return TUD.SQLHelper.Delete<T>(field, key);
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
        /// 根据给定的字段及其值删除一条记录
        /// </summary>
        public static int Delete<T>(string[] fields, object[] keys)
        {
            try
            {
                return TUD.SQLHelper.Delete<T>(fields, keys);
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
        public static int DeleteList<T>(string[] pkids)
        {
            try
            {
                return TUD.SQLHelper.DeleteBat<T>(pkids);
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
        /// 根据给定的条件批量删除记录
        /// </summary>
        public static int DeleteList<T>(string where)
        {
            try
            {
                return TUD.SQLHelper.DeleteBat<T>(where);
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
        /// 清除记录
        /// </summary>
        public static int Clear<T>()
        {
            try
            {
                return TUD.SQLHelper.DeleteBat<T>("");
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
        /// 根据给定的Pkid获取实体
        /// </summary>
        public static T GetEntity<T>(string pkid) where T : new()
        {
            return TUD.SQLHelper.GetEntity<T>(pkid);
        }

        /// <summary>
        /// 根据给定的条件获取实体
        /// </summary>
        public static T GetEntity<T>(string field, object key) where T : new()
        {
            return TUD.SQLHelper.GetEntity<T>(field, key);
        }

        /// <summary>
        /// 根据给定的条件获取实体
        /// </summary>
        public static T GetEntity<T>(string[] fields, params object[] keys) where T : new()
        {
            return TUD.SQLHelper.GetEntity<T>(fields, keys, null);
        }

        /// <summary>
        /// 根据给定的条件获取实体列表
        /// </summary>
        public static DataTable GetList<T>(string where, string sort) where T : new()
        {
            return TUD.SQLHelper.Query<T>(where, sort);
        }

        /// <summary>
        /// 根据给定的条件获取实体列表
        /// </summary>
        public static DataTable GetList<T>(string field, object key, string sort) where T : new()
        {
            return TUD.SQLHelper.Query<T>(field, key, sort);
        }

        /// <summary>
        /// 根据给定的条件获取实体列表
        /// </summary>
        public static DataTable GetList<T>(string[] fields, object[] keys, string sort) where T : new()
        {
            return TUD.SQLHelper.Query<T>(fields, keys, sort);
        }

        /// <summary>
        /// 统计个人业绩项目
        /// </summary>
        public static DataTable TotalGrxm(string xsbh)
        {
            string sql = string.Format("SELECT * FROM TotalYjxm_Gr('{0}') ORDER BY Fzztdm", xsbh);
            return DAL.Globals.Query(sql);
        }
    }
}
