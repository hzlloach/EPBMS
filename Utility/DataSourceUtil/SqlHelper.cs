using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using Maticsoft.DBUtility;

namespace TStar.Utility.DataSource
{
    public class SQLHelper
    {
        /// <summary>
        /// 在数组中查找是否存在指定值的元素（不区分大小写）
        /// </summary>
        private static bool Find(string[] array, string text)
        {
            string t = text.ToLower();
            foreach (string s in array)
            {
                if (t.Equals(s.ToString().ToLower()))
                    return true;
            }

            return false;
        }

        #region 通用的存在方法

        /// <summary>
        /// 通用的存在方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="pkField">排除主键字段，可为空</param>
        /// <param name="fields">条件字段数组</param>
        /// <returns>是否存在</returns>
        public static bool Exists<T>(T t, string pkField, params string[] fields)
        {
            try
            {
                if (fields.Length == 0) throw new Exception("请至少包含一个条件字段。");

                object value;
                Type type = typeof(T); // 获得此模型的类型

                StringBuilder sbWhere = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();

                if (!String.IsNullOrEmpty(pkField))
                {
                    sbWhere.Append(String.Format(" AND {0}<>@{0}", pkField));

                    value = type.GetProperty(pkField).GetValue(t, null);
                    SqlParameter sp = new SqlParameter("@" + pkField, value ?? "");
                    list.Add(sp);
                }

                for (int i = 0; i < fields.Length; i++)
                {
                    string fieldName = fields[i]; // 将属性名称赋值给临时变量
                    value = type.GetProperty(fieldName).GetValue(t, null);

                    sbWhere.Append(String.Format(" AND {0}=@{0}", fieldName));

                    SqlParameter sp = new SqlParameter("@" + fieldName, value);
                    list.Add(sp);
                }

                SqlParameter[] para = list.ToArray(); // 转化为数组
                string sql = string.Format("SELECT COUNT(1) FROM {0} WHERE {1}", type.Name, sbWhere.ToString().Substring(4));

                return DbHelperSQL.Exists(sql, para);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 通用的存在方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="pkField">排除主键字段，可为空</param>
        /// <param name="pkKey">主键值</param>
        /// <param name="field">条件字段</param>
        /// <param name="key">值</param>
        /// <returns>是否存在</returns>
        public static bool Exists<T>(string pkField, object pkKey, string field, object key)
        {
            try
            {
                Type type = typeof(T); // 获得此模型的类型

                StringBuilder sbWhere = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();

                if (!String.IsNullOrEmpty(pkField))
                {
                    sbWhere.Append(String.Format(" AND {0}<>@{0}", pkField));
                    SqlParameter psp = new SqlParameter("@" + pkField, pkKey ?? "");
                    list.Add(psp);
                }

                sbWhere.Append(String.Format(" AND {0}=@{0}", field));
                SqlParameter sp = new SqlParameter("@" + field, key);
                list.Add(sp);

                SqlParameter[] para = list.ToArray(); // 转化为数组
                string sql = string.Format("SELECT COUNT(1) FROM {0} WHERE {1}", type.Name, sbWhere.ToString().Substring(4));

                return DbHelperSQL.Exists(sql, para);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的存在方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="pKfield">排除主键字段，可为空</param>
        /// <param name="pkKey">主键值</param>
        /// <param name="fields">条件字段数组</param>
        /// <param name="keys">值数组</param>
        /// <returns>是否存在</returns>
        public static bool Exists<T>(string pkField, object pkKey, string[] fields, params object[] keys)
        {
            try
            {
                if (fields.Length == 0) throw new Exception("请至少包含一个条件字段。");

                Type type = typeof(T); // 获得此模型的类型

                StringBuilder sbWhere = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();

                if (fields.Length != keys.Length) throw new Exception("值数组长度不正确。");
                if (!String.IsNullOrEmpty(pkField))
                {
                    sbWhere.Append(String.Format(" AND {0}<>@{0}", pkField));
                    SqlParameter sp = new SqlParameter("@" + pkField, pkKey ?? "");
                    list.Add(sp);
                }

                for (int i = 0; i < fields.Length; i++)
                {
                    string fieldName = fields[i]; // 将属性名称赋值给临时变量

                    sbWhere.Append(String.Format(" AND {0}=@{0}", fieldName));
                    SqlParameter sp = new SqlParameter("@" + fieldName, keys[i]);
                    list.Add(sp);
                }

                SqlParameter[] para = list.ToArray(); // 转化为数组
                string sql = string.Format("SELECT COUNT(1) FROM {0} WHERE {1}", type.Name, sbWhere.ToString().Substring(4));

                return DbHelperSQL.Exists(sql, para);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 通用的增加方法
        /// <summary>
        /// 通用的增加方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">对象</param>
        /// <returns>返回受影响的行数</returns>
        public static int Insert<T>(T t)
        {
            try
            {
                StringBuilder sbValues = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();

                Type type = typeof(T); // 获得此模型的类型                
                PropertyInfo[] propertys = t.GetType().GetProperties(); // 获得此模型的公共属性

                for (int i = 0; i < propertys.Length; i++)
                {
                    if (!propertys[i].CanWrite) continue; // 该属性不可写，直接跳过

                    object value = propertys[i].GetValue(t, null);
                    string fieldName = propertys[i].Name.ToLower(); // 将属性名称赋值给临时变量

                    switch (fieldName)
                    {
                        case "aid":
                            continue;
                        case "pkid":
                            if (value == null || value.ToString() == "" || value.ToString().Length < 32)
                            {
                                string tv = value == null ? "" : value.ToString();
                                value = tv + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32 - tv.Length).ToUpper();
                                propertys[i].SetValue(t, value, null);
                            }
                            break;
                    }

                    if (i == propertys.Length - 1)
                        sbValues.Append("@" + fieldName);
                    else
                        sbValues.Append("@" + fieldName + ",");

                    SqlParameter sp = new SqlParameter("@" + fieldName, value);
                    list.Add(sp);
                }

                SqlParameter[] para = list.ToArray(); // 转化为数组
                string sql = string.Format("INSERT INTO [{0}]({1}) VALUES({2})", type.Name, sbValues.ToString().Replace("@", ""), sbValues.ToString());

                return DbHelperSQL.ExecuteSql(sql, para);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的增加方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="field">设置为表达式</param>
        /// <param name="expression">表达式，例如函数：GetDdh(@date,@type)。示例前后相关的参数列表：{ "dbo.GetKhzbdm('{0}')", String.Format("dbo.GetKhzbPkid('Z{0}' + @$TmpVar0, '{1}')", DeptID, Guid.NewGuid().ToString().Replace("-", "").ToUpper())}</param>
        /// <returns>返回受影响的行数</returns>
        public static int Insert<T>(T t, string field, string expression)
        {
            try
            {
                StringBuilder sbReturn = new StringBuilder();
                StringBuilder sbColumns = new StringBuilder();
                StringBuilder sbValues = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();
                List<String> listSet = new List<String>();
                List<int> listIdx = new List<int>();

                Type type = typeof(T); // 获得此模型的类型                
                PropertyInfo[] propertys = t.GetType().GetProperties(); // 获得此模型的公共属性

                for (int i = 0; i < propertys.Length; i++)
                {
                    if (!propertys[i].CanWrite) continue; // 该属性不可写，直接跳过

                    object value = propertys[i].GetValue(t, null);
                    string fieldName = propertys[i].Name.ToLower(); // 将属性名称赋值给临时变量

                    switch (fieldName)
                    {
                        case "aid":
                            continue;
                        case "pkid":
                            if (value == null || value.ToString() == "" || value.ToString().Length < 32)
                            {
                                string tv = value == null ? "" : value.ToString();
                                value = tv + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32 - tv.Length).ToUpper();
                                propertys[i].SetValue(t, value, null);
                            }
                            break;
                    }
                    sbColumns.Append("," + fieldName);

                    // 诸如函数之类的计算表达式
                    if (field.ToLower() == fieldName)
                    {
                        string tmpVar = "@$TmpVar0";
                        listSet.Add(String.Format("SET {0}={1};", tmpVar, expression));

                        if (i == propertys.Length - 1)
                            sbValues.Append(tmpVar);
                        else
                            sbValues.Append(tmpVar + ",");

                        SqlParameter tsp = new SqlParameter(tmpVar, value);
                        tsp.Direction = ParameterDirection.InputOutput;
                        list.Add(tsp);
                        listIdx.Add(i);

                        continue;
                    }

                    if (i == propertys.Length - 1)
                        sbValues.Append("@" + fieldName);
                    else
                        sbValues.Append("@" + fieldName + ",");

                    SqlParameter sp = new SqlParameter("@" + fieldName, value);
                    list.Add(sp);
                }

                SqlParameter[] para = list.ToArray(); // 转化为数组
                string sql = string.Format("{3}INSERT INTO [{0}]({1}) VALUES({2})", type.Name, sbColumns.ToString().Substring(1), sbValues.ToString(), listSet[0]);

                int r = DbHelperSQL.ExecuteSql(sql, para);
                // 修改表达式参数的值
                propertys[listIdx[0]].SetValue(t, para[listIdx[0]].Value, null);
                return r;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的增加方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="fields">设置为表达式的字段数组</param>
        /// <param name="expressions">表达式数组，例如函数：GetDdh(@date,@type)。示例前后相关的参数列表：{ "dbo.GetKhzbdm('{0}')", String.Format("dbo.GetKhzbPkid('Z{0}' + @$TmpVar0, '{1}')", DeptID, Guid.NewGuid().ToString().Replace("-", "").ToUpper())}</param>
        /// <returns>返回受影响的行数</returns>
        public static int Insert<T>(T t, string[] fields, string[] expressions)
        {
            try
            {
                if (fields.Length == 0) throw new Exception("请至少包含一个字段。");
                if (fields.Length != expressions.Length) throw new Exception("参数个数不一致。");

                StringBuilder sbReturn = new StringBuilder();
                StringBuilder sbColumns = new StringBuilder();
                StringBuilder sbValues = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();
                List<String> listSet = new List<String>();
                List<int> listIdx = new List<int>();

                Type type = typeof(T); // 获得此模型的类型                
                PropertyInfo[] propertys = t.GetType().GetProperties(); // 获得此模型的公共属性

                for (int i = 0; i < propertys.Length; i++)
                {
                    if (!propertys[i].CanWrite) continue; // 该属性不可写，直接跳过

                    object value = propertys[i].GetValue(t, null);
                    string fieldName = propertys[i].Name.ToLower(); // 将属性名称赋值给临时变量

                    switch (fieldName)
                    {
                        case "aid":
                            continue;
                        case "pkid":
                            if (value == null || value.ToString() == "" || value.ToString().Length < 32)
                            {
                                string tv = value == null ? "" : value.ToString();
                                value = tv + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32 - tv.Length).ToUpper();
                                propertys[i].SetValue(t, value, null);
                            }
                            break;
                    }
                    sbColumns.Append("," + fieldName);

                    // 诸如函数之类的计算表达式
                    int idx = -1;
                    if (Globals.Find(fields, fieldName, out idx))
                    {
                        string tmpVar = "@$TmpVar" + idx;
                        if (idx >= listSet.Count)
                            listSet.Add(String.Format("SET {0}={1};", tmpVar, expressions[idx]));
                        else
                            listSet.Insert(idx, String.Format("SET {0}={1};", tmpVar, expressions[idx]));

                        if (i == propertys.Length - 1)
                            sbValues.Append(tmpVar);
                        else
                            sbValues.Append(tmpVar + ",");

                        SqlParameter tsp = new SqlParameter(tmpVar, value);
                        tsp.Direction = ParameterDirection.InputOutput;
                        list.Add(tsp);
                        listIdx.Add(i);

                        continue;
                    }

                    if (i == propertys.Length - 1)
                        sbValues.Append("@" + fieldName);
                    else
                        sbValues.Append("@" + fieldName + ",");

                    SqlParameter sp = new SqlParameter("@" + fieldName, value);
                    list.Add(sp);
                }

                SqlParameter[] para = list.ToArray(); // 转化为数组
                string sql = string.Format("{3}INSERT INTO [{0}]({1}) VALUES({2})", type.Name, sbColumns.ToString().Substring(1), sbValues.ToString(), String.Join("", listSet.ToArray()));

                int r = DbHelperSQL.ExecuteSql(sql, para);
                // 修改表达式参数的值
                for (int i = 0; i < listIdx.Count; i++)
                {
                    propertys[listIdx[i]].SetValue(t, para[listIdx[i]].Value, null);
                }
                return r;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 通用的修改方法
        /// <summary>
        /// 通用的修改方法（以Pkid为条件，修改整个对象）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">对象</param>
        /// <returns>返回受影响的行数</returns>
        public static int Update<T>(T t)
        {
            try
            {
                StringBuilder sbValues = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();

                Type type = typeof(T); // 获得此模型的类型               
                PropertyInfo[] propertys = t.GetType().GetProperties(); // 获得此模型的公共属性

                string pkid = "";
                for (int i = 0; i < propertys.Length; i++)
                {
                    if (!propertys[i].CanWrite) continue; // 该属性不可写，直接跳过

                    object value = propertys[i].GetValue(t, null);
                    if (value == null) continue; // 为空时跳过
                    string fieldName = propertys[i].Name.ToLower(); // 将属性名称赋值给临时变量

                    switch (fieldName)
                    {
                        case "aid":
                            continue;
                        case "pkid":
                            pkid = value.ToString();
                            goto AddParameter;
                    }
                    sbValues.Append(String.Format("{0}=@{0},", fieldName));

                AddParameter:
                    SqlParameter sp = new SqlParameter("@" + fieldName, value);
                    list.Add(sp);
                }

                SqlParameter[] para = list.ToArray(); // 转化为数组
                string strValue = sbValues.ToString();
                if (strValue.EndsWith(",")) strValue = strValue.Substring(0, strValue.Length - 1);
                string sql = string.Format("UPDATE {0} SET {1} WHERE Pkid=@Pkid", type.Name, strValue);

                return DbHelperSQL.ExecuteSql(sql, para);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的修改方法（以Pkid为条件，修改整个对象，指定字段允许有表达式）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="fields">设置为表达式的字段</param>
        /// <param name="expressions">表达式，例如函数：GetDdh(@date,@type)。示例前后相关的参数列表：{ "dbo.GetKhzbdm('{0}')", String.Format("dbo.GetKhzbPkid('Z{0}' + @$TmpVar0, '{1}')", DeptID, Guid.NewGuid().ToString().Replace("-", "").ToUpper())}</param>
        /// <returns>返回受影响的行数</returns>
        public static int Update<T>(T t, string field, string expression)
        {
            try
            {
                StringBuilder sbValues = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();
                List<String> listSet = new List<String>();
                List<int> listIdx = new List<int>();

                Type type = typeof(T); // 获得此模型的类型               
                PropertyInfo[] propertys = t.GetType().GetProperties(); // 获得此模型的公共属性

                string pkid = "";
                for (int i = 0; i < propertys.Length; i++)
                {
                    if (!propertys[i].CanWrite) continue; // 该属性不可写，直接跳过

                    object value = propertys[i].GetValue(t, null);
                    string fieldName = propertys[i].Name.ToLower(); // 将属性名称赋值给临时变量

                    switch (fieldName)
                    {
                        case "aid":
                            continue;
                        case "pkid":
                            pkid = value.ToString();
                            goto AddParameter;
                    }

                    // 诸如函数之类的计算表达式
                    if (field.ToLower() == fieldName)
                    {
                        string tmpVar = "@$TmpVar0";
                        listSet.Add(String.Format("SET {0}={1};", tmpVar, expression));

                        if (i == propertys.Length - 1)
                            sbValues.Append(String.Format("{0}={1}", field, tmpVar));
                        else
                            sbValues.Append(String.Format("{0}={1},", field, tmpVar));

                        SqlParameter tsp = new SqlParameter(tmpVar, value);
                        tsp.Direction = ParameterDirection.InputOutput;
                        list.Add(tsp);
                        listIdx.Add(i);

                        continue;
                    }

                    //if (i == propertys.Length - 1)
                    //    sbValues.Append(String.Format("{0}=@{0}", fieldName));
                    //else
                        sbValues.Append(String.Format("{0}=@{0},", fieldName));

                AddParameter:
                    SqlParameter sp = new SqlParameter("@" + fieldName, value);
                    list.Add(sp);
                }

                SqlParameter[] para = list.ToArray(); // 转化为数组
                string strValue = sbValues.ToString();
                if (strValue.EndsWith(",")) strValue = strValue.Substring(0, strValue.Length - 1);
                string sql = string.Format("{2}UPDATE {0} SET {1} WHERE Pkid=@Pkid", type.Name, strValue, listSet[0]);

                int r = DbHelperSQL.ExecuteSql(sql, para);
                // 修改表达式参数的值
                propertys[listIdx[0]].SetValue(t, para[listIdx[0]].Value, null);
                return r;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的修改方法（以Pkid为条件，修改整个对象，指定字段允许有表达式）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="fields">设置为表达式的字段数组</param>
        /// <param name="expressions">表达式数组，例如函数：GetDdh(@date,@type)。示例前后相关的参数列表：{ "dbo.GetKhzbdm('{0}')", String.Format("dbo.GetKhzbPkid('Z{0}' + @$TmpVar0, '{1}')", DeptID, Guid.NewGuid().ToString().Replace("-", "").ToUpper())}</param>
        /// <returns>返回受影响的行数</returns>
        public static int Update<T>(T t, string[] fields, string[] expressions)
        {
            try
            {
                if (fields.Length == 0) throw new Exception("请至少包含一个字段。");
                if (fields.Length != expressions.Length) throw new Exception("参数个数不一致。");

                StringBuilder sbValues = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();
                List<String> listSet = new List<String>();
                List<int> listIdx = new List<int>();

                Type type = typeof(T); // 获得此模型的类型               
                PropertyInfo[] propertys = t.GetType().GetProperties(); // 获得此模型的公共属性

                string pkid = "";
                for (int i = 0; i < propertys.Length; i++)
                {
                    if (!propertys[i].CanWrite) continue; // 该属性不可写，直接跳过

                    object value = propertys[i].GetValue(t, null);
                    string fieldName = propertys[i].Name.ToLower(); // 将属性名称赋值给临时变量

                    switch (fieldName)
                    {
                        case "aid":
                            continue;
                        case "pkid":
                            pkid = value.ToString();
                            goto AddParameter;
                    }

                    // 诸如函数之类的计算表达式
                    int idx = -1;
                    if (Globals.Find(fields, fieldName, out idx))
                    {
                        string tmpVar = "@$TmpVar" + idx;
                        if (idx >= listSet.Count)
                            listSet.Add(String.Format("SET {0}={1};", tmpVar, expressions[idx]));
                        else
                            listSet.Insert(idx, String.Format("SET {0}={1};", tmpVar, expressions[idx]));

                        if (i == propertys.Length - 1)
                            sbValues.Append(String.Format("{0}={1}", fields[idx], tmpVar));
                        else
                            sbValues.Append(String.Format("{0}={1},", fields[idx], tmpVar));

                        SqlParameter tsp = new SqlParameter(tmpVar, value);
                        tsp.Direction = ParameterDirection.InputOutput;
                        list.Add(tsp);
                        listIdx.Add(i);

                        continue;
                    }

                    //if (i == propertys.Length - 1)
                    //    sbValues.Append(String.Format("{0}=@{0}", fieldName));
                    //else
                        sbValues.Append(String.Format("{0}=@{0},", fieldName));

                AddParameter:
                    SqlParameter sp = new SqlParameter("@" + fieldName, value);
                    list.Add(sp);
                }

                SqlParameter[] para = list.ToArray(); // 转化为数组
                string strValue = sbValues.ToString();
                if (strValue.EndsWith(",")) strValue = strValue.Substring(0, strValue.Length - 1);
                string sql = string.Format("{2}UPDATE {0} SET {1} WHERE Pkid=@Pkid", type.Name, strValue, String.Join("", listSet.ToArray()));
                
                int r = DbHelperSQL.ExecuteSql(sql, para);
                // 修改表达式参数的值
                for (int i = 0; i < listIdx.Count; i++)
                {
                    propertys[listIdx[i]].SetValue(t, para[listIdx[i]].Value, null);
                }
                return r;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的修改方法（以指定字段为条件，修改整个对象）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="whereFields">条件字段数组</param>
        /// <returns>返回受影响的行数</returns>
        public static int Update<T>(T t, string[] whereFields)
        {
            try
            {
                if (whereFields.Length == 0) throw new Exception("请至少包含一个条件字段。");

                StringBuilder sbValues = new StringBuilder();
                StringBuilder sbWheres = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();

                Type type = typeof(T); // 获得此模型的类型               
                PropertyInfo[] propertys = t.GetType().GetProperties(); // 获得此模型的公共属性

                for (int i = 0; i < propertys.Length; i++)
                {
                    if (!propertys[i].CanWrite) continue; // 该属性不可写，直接跳过

                    object value = propertys[i].GetValue(t, null);
                    string fieldName = propertys[i].Name.ToLower(); // 将属性名称赋值给临时变量

                    switch (fieldName)
                    {
                        case "aid":
                        case "pkid":
                            continue;
                    }

                    if (Find(whereFields, fieldName))
                        sbWheres.Append(String.Format(" AND {0}=@{0}", fieldName));
                    else
                        sbValues.Append(String.Format(",{0}=@{0}", fieldName));

                    SqlParameter sp = new SqlParameter("@" + fieldName, value);
                    list.Add(sp);
                }

                SqlParameter[] para = list.ToArray(); // 转化为数组
                string sql = string.Format("UPDATE {0} SET {1} WHERE {2}", type.Name, sbValues.ToString().Substring(1), sbWheres.ToString().Substring(4));

                return DbHelperSQL.ExecuteSql(sql, para);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的修改方法（以Pkid为条件，修改指定字段）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="fields">待修改字段数组</param>
        /// <returns>返回受影响的行数</returns>
        public static int UpdateFields<T>(T t, params string[] fields)
        {
            try
            {
                if (fields.Length == 0) throw new Exception("请至少包含一个代修改的字段。");

                StringBuilder sbValues = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();

                Type type = typeof(T); // 获得此模型的类型               
                PropertyInfo[] propertys = t.GetType().GetProperties(); // 获得此模型的公共属性

                string pkid = "";
                for (int i = 0; i < propertys.Length; i++)
                {
                    object value = propertys[i].GetValue(t, null);
                    string fieldName = propertys[i].Name.ToLower(); // 将属性名称赋值给临时变量

                    switch (fieldName)
                    {
                        case "aid":
                            continue;
                        case "pkid":
                            pkid = value.ToString();
                            goto AddParameter;
                    }

                    if (Globals.Find(fields, fieldName))
                    {
                        sbValues.Append(String.Format(",{0}=@{0}", fieldName));
                    }

                AddParameter:
                    SqlParameter sp = new SqlParameter("@" + fieldName, value);
                    list.Add(sp);
                }

                SqlParameter[] para = list.ToArray(); // 转化为数组
                string sql = string.Format("UPDATE {0} SET {1} WHERE Pkid=@Pkid", type.Name, sbValues.ToString().Substring(1));

                return DbHelperSQL.ExecuteSql(sql, para);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的批量修改方法
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="field">待修改字段</param>
        /// <param name="key">值</param>
        /// <param name="where">条件</param>
        /// <returns>返回受影响的行数</returns>
        public static int UpdateFields<T>(string field, object key, string where)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();

                Type type = typeof(T); // 获得此模型的类型 

                if (!String.IsNullOrEmpty(where)) where = "WHERE " + where;
                string sql = string.Format("UPDATE {0} SET {1}=@{1} {2}", type.Name, field, where);
                SqlParameter sp = new SqlParameter("@" + field, key);

                return DbHelperSQL.ExecuteSql(sql, sp);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的批量修改方法
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="fields">待修改字段数组</param>
        /// <param name="keys">值数组</param>
        /// <param name="where">条件</param>
        /// <returns>返回受影响的行数</returns>
        public static int UpdateFields<T>(string[] fields, object[] keys, string where)
        {
            try
            {
                if (fields.Length != keys.Length) throw new Exception("值数组长度不正确。");

                StringBuilder sb = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();

                Type type = typeof(T); // 获得此模型的类型

                for (int i = 0; i < fields.Length; i++)
                {
                    string field = fields[i];
                    object value = keys[i];

                    if (i == fields.Length - 1)
                        sb.Append(String.Format("{0}=@{0}", field));
                    else
                        sb.Append(String.Format("{0}=@{0},", field));

                    SqlParameter sp = new SqlParameter("@" + field, value);
                    list.Add(sp);
                }

                SqlParameter[] para = list.ToArray(); // 转化为数组
                if (!String.IsNullOrEmpty(where)) where = "WHERE " + where;
                string strValue = sb.ToString();
                if (strValue.EndsWith(",")) strValue = strValue.Substring(0, strValue.Length - 1);
                string sql = string.Format("UPDATE {0} SET {1} {2}", type.Name, strValue, where);

                return DbHelperSQL.ExecuteSql(sql, para);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 通用的删除方法
        /// <summary>
        /// 通用的删除方法
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="pkid">主键pkid</param>
        /// <returns>返回受影响的行数</returns>
        public static int Delete<T>(string pkid)
        {
            try
            {
                Type type = typeof(T); // 获得此模型的类型  

                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter sp = new SqlParameter("@Pkid", pkid);
                list.Add(sp);

                SqlParameter[] para = list.ToArray(); //转化为数组
                string sql = string.Format("DELETE FROM {0} WHERE Pkid=@Pkid", type.Name);

                return DbHelperSQL.ExecuteSql(sql, para);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的删除方法
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="field">条件字段</param>
        /// <param name="key">值</param>
        /// <returns>返回受影响的行数</returns>
        public static int Delete<T>(string field, object key)
        {
            try
            {
                Type type = typeof(T); // 获得此模型的类型  

                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter sp = new SqlParameter("@" + field, key);
                list.Add(sp);

                SqlParameter[] para = list.ToArray(); //转化为数组
                string sql = string.Format("DELETE FROM {0} WHERE {1}=@{1}", type.Name, field);

                return DbHelperSQL.ExecuteSql(sql, para);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的删除方法
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="fields">条件字段数组</param>
        /// <param name="keys">值数组</param>
        /// <returns>返回受影响的行数</returns>
        public static int Delete<T>(string[] fields, object[] keys)
        {
            try
            {
                if (fields.Length != keys.Length) throw new Exception("值数组长度不正确。");

                Type type = typeof(T); // 获得此模型的类型  

                StringBuilder sb = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();

                for (int i = 0; i < fields.Length; i++)
                {
                    if (i == fields.Length - 1)
                        sb.Append(String.Format("{0}=@{0}", fields[i]));
                    else
                        sb.Append(String.Format("{0}=@{0} AND ", fields[i]));

                    SqlParameter sp = new SqlParameter("@" + fields[i], keys[i]);
                    list.Add(sp);
                }

                SqlParameter[] para = list.ToArray(); //转化为数组
                string sql = string.Format("DELETE FROM {0} WHERE {1}", type.Name, sb.ToString());

                return DbHelperSQL.ExecuteSql(sql, para);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的批量删除方法
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="where">条件</param>
        /// <returns>返回受影响的行数</returns>
        public static int DeleteBat<T>(string where)
        {
            try
            {
                Type type = typeof(T); // 获得此模型的类型  

                if (!String.IsNullOrEmpty(where)) where = "WHERE " + where;
                string sql = string.Format("DELETE FROM {0} {1}", type.Name, where);

                return DbHelperSQL.ExecuteSql(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的批量删除方法
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="pkids">主键pkid数组</param>
        /// <returns>返回受影响的行数</returns>
        public static int DeleteBat<T>(string[] pkids)
        {
            try
            {
                Type type = typeof(T); // 获得此模型的类型  

                string sql = string.Format("DELETE FROM {0} WHERE Pkid IN ('{1}')", type.Name, string.Join("','", pkids));

                return DbHelperSQL.ExecuteSql(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的批量删除方法
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="field">条件字段</param>
        /// <param name="keys">值数组</param>
        /// <returns>返回受影响的行数</returns>
        public static int DeleteBat<T>(string field, params object[] keys)
        {
            try
            {
                Type type = typeof(T); // 获得此模型的类型  

                StringBuilder sb = new StringBuilder();
                foreach (object o in keys)
                {
                    sb.Append(",'" + o.ToString() + "'");
                }

                string sql = string.Format("DELETE FROM {0} WHERE {1} IN ({2})", type.Name, field, sb.ToString().Substring(1));

                return DbHelperSQL.ExecuteSql(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region 已注释
        ///// <summary>
        ///// 通用的删除方法
        ///// </summary>
        ///// <param name="tablename">表名</param>
        ///// <param name="pkid">主键pkid</param>
        ///// <returns>返回受影响的行数</returns>
        //public static int Delete(string tablename, string pkid)
        //{
        //    try
        //    {
        //        List<SqlParameter> list = new List<SqlParameter>();
        //        SqlParameter sp = new SqlParameter("@Pkid", pkid);
        //        list.Add(sp);

        //        SqlParameter[] para = list.ToArray(); //转化为数组
        //        string sql = string.Format("DELETE {0} WHERE Pkid=@Pkid", tablename);

        //        return DbHelperSQL.ExecuteSql(sql, para);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// 通用的删除方法
        ///// </summary>
        ///// <param name="tablename">表名</param>
        ///// <param name="field">条件字段</param>
        ///// <param name="key">值</param>
        ///// <returns>返回受影响的行数</returns>
        //public static int Delete(string tablename, string field, object key)
        //{
        //    try
        //    {
        //        List<SqlParameter> list = new List<SqlParameter>();
        //        SqlParameter sp = new SqlParameter("@" + field, key);
        //        list.Add(sp);

        //        SqlParameter[] para = list.ToArray(); //转化为数组
        //        string sql = string.Format("DELETE {0} WHERE {1}=@{1}", tablename, field);

        //        return DbHelperSQL.ExecuteSql(sql, para);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// 通用的批量删除方法
        ///// </summary>
        ///// <param name="tablename">表名</param>
        ///// <param name="pkids">主键pkid数组</param>
        ///// <returns>返回受影响的行数</returns>
        //public static int DeleteBat(string tablename, params string[] pkids)
        //{
        //    try
        //    {
        //        string sql = string.Format("DELETE {0} WHERE Pkid IN ('{1}')", tablename, string.Join("','", pkids));

        //        return DbHelperSQL.ExecuteSql(sql);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// 通用的批量删除方法
        ///// </summary>
        ///// <param name="tablename">表名</param>
        ///// <param name="field">条件字段</param>
        ///// <param name="keys">值数组</param>
        ///// <returns>返回受影响的行数</returns>
        //public static int DeleteBat(string tablename, string field, params object[] keys)
        //{
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        foreach (object o in keys)
        //        {
        //            sb.Append(",'" + o.ToString() + "'");
        //        }

        //        string sql = string.Format("DELETE {0} WHERE {1} IN ({2})", tablename, field, sb.ToString().Substring(1));

        //        return DbHelperSQL.ExecuteSql(sql);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion
        #endregion

        #region 通用的获取实体方法
        /// <summary>
        /// 通用的获取实体方法
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="pkid">主键pkid</param>
        /// <returns>实体信息</returns>
        public static T GetEntity<T>(string pkid) where T : new()
        {
            try
            {
                Type type = typeof(T); // 获得此模型的类型               

                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter sp = new SqlParameter("@Pkid", pkid);
                list.Add(sp);

                SqlParameter[] para = list.ToArray(); //转化为数组
                string sql = string.Format("SELECT TOP 1 * FROM {0} WHERE Pkid=@Pkid", type.Name);
                DataTable dt = DbHelperSQL.Query(sql, para).Tables[0];

                T t = Common.ConvertHelper.ConvertToEntity<T>(dt);
                return t;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的获取实体方法
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="field">条件字段</param>
        /// <param name="key">值</param>
        /// <returns>实体信息</returns>
        public static T GetEntity<T>(string field, object key) where T : new()
        {
            try
            {
                Type type = typeof(T); // 获得此模型的类型  

                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter sp = new SqlParameter("@" + field, key);
                list.Add(sp);

                SqlParameter[] para = list.ToArray(); //转化为数组
                string sql = string.Format("SELECT TOP 1 * FROM {0} WHERE {1}=@{1}", type.Name, field);
                DataTable dt = DbHelperSQL.Query(sql, para).Tables[0];

                T t = Common.ConvertHelper.ConvertToEntity<T>(dt);
                return t;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的获取实体方法
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="fields">条件字段数组</param>
        /// <param name="keys">值数组</param>
        /// <returns>实体信息</returns>
        public static T GetEntity<T>(string[] fields, object[] keys, string where) where T : new()
        {
            try
            {
                if (fields.Length == 0) throw new Exception("请至少包含一个条件字段。");
                if (fields.Length != keys.Length) throw new Exception("参数个数不一致。");

                StringBuilder sb = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();
                Type type = typeof(T); // 获得此模型的类型   

                for (int i = 0; i < fields.Length; i++)
                {
                    sb.Append(String.Format(" AND {0}=@{0}", fields[i]));

                    SqlParameter sp = new SqlParameter("@" + fields[i], keys[i]);
                    list.Add(sp);
                }

                string strWhere = Globals.FilterRiskChar(where).Replace("''", "'");
                if (!String.IsNullOrEmpty(strWhere)) strWhere = " AND " + strWhere;
                SqlParameter[] para = list.ToArray(); //转化为数组
                string sql = string.Format("SELECT TOP 1 * FROM {0} WHERE {1}{2}", type.Name, sb.ToString().Substring(4), strWhere);
                DataTable dt = DbHelperSQL.Query(sql, para).Tables[0];

                T t = Common.ConvertHelper.ConvertToEntity<T>(dt);
                return t;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 通用的条件查询方法
        /// <summary>
        /// 通用的条件查询方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序字段</param>
        /// <returns>数据表</returns>
        public static DataTable Query<T>(string where, string sort)
        {
            try
            {
                Type type = typeof(T); // 获得此模型的类型    

                if (!String.IsNullOrEmpty(where)) where = "WHERE " + where;
                if (!String.IsNullOrEmpty(sort)) sort = "ORDER BY " + sort;
                string sql = string.Format("SELECT * FROM {0} {1} {2}", type.Name, where, sort);
                DataTable dt = DbHelperSQL.Query(sql).Tables[0];

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的条件查询方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="field">条件字段</param>
        /// <param name="key">值</param>
        /// <param name="sort">排序字段</param>
        /// <returns>数据表</returns>
        public static DataTable Query<T>(string field, object key, string sort)
        {
            try
            {
                Type type = typeof(T); // 获得此模型的类型    

                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter sp = new SqlParameter("@" + field, key);
                list.Add(sp);

                SqlParameter[] para = list.ToArray(); //转化为数组
                string sql = string.Format("SELECT * FROM {0} WHERE {1}=@{1} ORDER BY {2}", type.Name, field, String.IsNullOrEmpty(sort) ? field : sort);
                DataTable dt = DbHelperSQL.Query(sql, para).Tables[0];

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的条件查询方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="fields">条件字段数组</param>
        /// <param name="keys">值数组</param>
        /// <param name="sort">排序字段</param>
        /// <returns>数据表</returns>
        public static DataTable Query<T>(string[] fields, object[] keys, string sort)
        {
            try
            {
                if (fields.Length == 0) throw new Exception("请至少包含一个条件字段。");
                if (fields.Length != keys.Length) throw new Exception("参数个数不一致。");

                Type type = typeof(T); // 获得此模型的类型    

                StringBuilder sb = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();

                for (int i = 0; i < fields.Length; i++)
                {
                    sb.Append(String.Format(" AND {0}=@{0}", fields[i]));

                    SqlParameter sp = new SqlParameter("@" + fields[i], keys[i]);
                    list.Add(sp);
                }

                SqlParameter[] para = list.ToArray(); //转化为数组
                string sql = string.Format("SELECT * FROM {0} WHERE {1} ORDER BY [2}", type.Name, sb.ToString().Substring(4), String.IsNullOrEmpty(sort) ? fields[0] : sort);
                DataTable dt = DbHelperSQL.Query(sql, para).Tables[0];

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region 已注释
        ///// <summary>
        ///// 通用的条件查询方法
        ///// </summary>
        ///// <param name="tablename">表名</param>
        ///// <param name="where">查询条件</param>
        ///// <param name="sort">排序字段</param>
        ///// <returns>数据表</returns>
        //public static DataTable Query(string tablename, string where, string sort)
        //{
        //    try
        //    {
        //        if (!String.IsNullOrEmpty(where)) where = "WHERE " + where;
        //        if (!String.IsNullOrEmpty(sort)) where = "ORDER BY " + sort;
        //        string sql = string.Format("SELECT * FROM {0} {1} {2}", tablename, where, sort);
        //        DataTable dt = DbHelperSQL.Query(sql).Tables[0];

        //        return dt;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// 通用的条件查询方法
        ///// </summary>
        ///// <param name="tablename">表名</param>
        ///// <param name="field">条件字段</param>
        ///// <param name="key">值</param>
        ///// <param name="sort">排序字段</param>
        ///// <returns>数据表</returns>
        //public static DataTable Query(string tablename, string field, object key, string sort)
        //{
        //    try
        //    {
        //        List<SqlParameter> list = new List<SqlParameter>();
        //        SqlParameter sp = new SqlParameter("@" + field, key);
        //        list.Add(sp);

        //        SqlParameter[] para = list.ToArray(); //转化为数组
        //        string sql = string.Format("SELECT * FROM {0} WHERE {1}=@{1} ORDER BY {2}", tablename, field, String.IsNullOrEmpty(sort) ? field : sort);
        //        DataTable dt = DbHelperSQL.Query(sql, para).Tables[0];

        //        return dt;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// 通用的条件查询方法
        ///// </summary>
        ///// <param name="tablename">表名</param>
        ///// <param name="fields">条件字段数组</param>
        ///// <param name="keys">值数组</param>
        ///// <param name="sort">排序字段</param>
        ///// <returns>数据表</returns>
        //public static DataTable Query(string tablename, string[] fields, object[] keys, string sort)
        //{
        //    try
        //    {
        //        if (fields.Length != keys.Length) throw new Exception("参数个数不一致。");

        //        StringBuilder sb = new StringBuilder();
        //        List<SqlParameter> list = new List<SqlParameter>();

        //        for (int i = 0; i < fields.Length; i++)
        //        {
        //            sb.Append(String.Format(" AND {0}=@{0}", fields[i]));

        //            SqlParameter sp = new SqlParameter("@" + fields[i], keys[i]);
        //            list.Add(sp);
        //        }

        //        SqlParameter[] para = list.ToArray(); //转化为数组
        //        string sql = string.Format("SELECT * FROM {0} WHERE {1} ORDER BY [2}", tablename, sb.ToString().Substring(4), String.IsNullOrEmpty(sort) ? fields[0] : sort);
        //        DataTable dt = DbHelperSQL.Query(sql, para).Tables[0];

        //        return dt;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion
        #endregion

        #region 通用的获取记录数方法
        /// <summary>
        /// 通用的获取记录数方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="field">条件字段</param>
        /// <param name="key">值</param>
        /// <returns>记录数</returns>
        public static int GetRecordCount<T>(string where)
        {
            try
            {
                Type type = typeof(T); // 获得此模型的类型    

                if (!String.IsNullOrEmpty(where)) where = "WHERE " + where;
                string sql = string.Format("SELECT COUNT(1) FROM {0} {1}", type.Name, where);

                object obj = DbHelperSQL.GetSingle(sql);
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的获取记录数方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="field">条件字段</param>
        /// <param name="key">值</param>
        /// <returns>记录数</returns>
        public static int GetRecordCount<T>(string field, object key)
        {
            try
            {
                Type type = typeof(T); // 获得此模型的类型    

                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter sp = new SqlParameter("@" + field, key);
                list.Add(sp);

                SqlParameter[] para = list.ToArray(); //转化为数组
                string sql = string.Format("SELECT COUNT(1) FROM {0} WHERE {1}=@{1}", type.Name, field);

                object obj = DbHelperSQL.GetSingle(sql, para);
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的获取记录数方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="fields">条件字段数组</param>
        /// <param name="keys">值数组</param>
        /// <returns>记录数</returns>
        public static int GetRecordCount<T>(string[] fields, object[] keys)
        {
            try
            {
                if (fields.Length == 0) throw new Exception("请至少包含一个条件字段。");
                if (fields.Length != keys.Length) throw new Exception("参数个数不一致。");

                Type type = typeof(T); // 获得此模型的类型    

                StringBuilder sb = new StringBuilder();
                List<SqlParameter> list = new List<SqlParameter>();

                for (int i = 0; i < fields.Length; i++)
                {
                    sb.Append(String.Format(" AND {0}=@{0}", fields[i]));

                    SqlParameter sp = new SqlParameter("@" + fields[i], keys[i]);
                    list.Add(sp);
                }

                SqlParameter[] para = list.ToArray(); //转化为数组
                string sql = string.Format("SELECT COUNT(1) FROM {0} WHERE {1}", type.Name, sb.ToString().Substring(4));

                object obj = DbHelperSQL.GetSingle(sql, para);
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region 已注释
        ///// <summary>
        ///// 通用的获取记录数方法
        ///// </summary>
        ///// <param name="tablename">表名</param>
        ///// <param name="field">条件字段</param>
        ///// <param name="key">值</param>
        ///// <returns>记录数</returns>
        //public static int GetRecordCount(string tablename, string where)
        //{
        //    try
        //    {
        //        if (!String.IsNullOrEmpty(where)) where = "WHERE " + where;
        //        string sql = string.Format("SELECT COUNT(1) FROM {0} {1}", tablename, where);

        //        object obj = DbHelperSQL.GetSingle(sql);
        //        return Convert.ToInt32(obj);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// 通用的获取记录数方法
        ///// </summary>
        ///// <param name="tablename">表名</param>
        ///// <param name="field">条件字段</param>
        ///// <param name="key">值</param>
        ///// <returns>记录数</returns>
        //public static int GetRecordCount(string tablename, string field, object key)
        //{
        //    try
        //    {
        //        List<SqlParameter> list = new List<SqlParameter>();
        //        SqlParameter sp = new SqlParameter("@" + field, key);
        //        list.Add(sp);

        //        SqlParameter[] para = list.ToArray(); //转化为数组
        //        string sql = string.Format("SELECT COUNT(1) FROM {0} WHERE {1}=@{1}", tablename, field);

        //        object obj = DbHelperSQL.GetSingle(sql, para);
        //        return Convert.ToInt32(obj);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// 通用的获取记录数方法
        ///// </summary>
        ///// <param name="tablename">表名</param>
        ///// <param name="fields">条件字段数组</param>
        ///// <param name="keys">值数组</param>
        ///// <returns>记录数</returns>
        //public static int GetRecordCount(string tablename, string[] fields, object[] keys)
        //{
        //    try
        //    {
        //        if (fields.Length != keys.Length) throw new Exception("参数个数不一致。");

        //        StringBuilder sb = new StringBuilder();
        //        List<SqlParameter> list = new List<SqlParameter>();

        //        for (int i = 0; i < fields.Length; i++)
        //        {
        //            sb.Append(String.Format(" AND {0}=@{0}", fields[i]));

        //            SqlParameter sp = new SqlParameter("@" + fields[i], keys[i]);
        //            list.Add(sp);
        //        }

        //        SqlParameter[] para = list.ToArray(); //转化为数组
        //        string sql = string.Format("SELECT COUNT(1) FROM {0} WHERE {1}", tablename, sb.ToString().Substring(4));

        //        object obj = DbHelperSQL.GetSingle(sql, para);
        //        return Convert.ToInt32(obj);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion
        #endregion

        #region 通用的分页查询方法
        /// <summary>
        /// 通用的分页查询方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序字段</param>
        /// <returns>数据表</returns>
        public static DataTable QueryByPage<T>(int pageSize, int pageIndex, string where, string sort)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@ordName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000)
                    };

                Type type = typeof(T); // 获得此模型的类型  
                parameters[0].Value = type.Name;
                parameters[1].Value = String.IsNullOrEmpty(sort) ? "Pkid" : sort;
                parameters[2].Value = pageSize;
                parameters[3].Value = pageIndex;
                parameters[4].Value = 0;
                parameters[5].Value = where;
                return DbHelperSQL.RunProcedure("SP_GetRecordByPage", parameters, "ds").Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通用的统计方法
        /// </summary>
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
        #endregion
    }
}
