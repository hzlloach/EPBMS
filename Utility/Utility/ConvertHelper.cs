using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Web;

namespace TStar.Utility.Common
{
    /// <summary>
    /// 将DataTable转换成泛型集合IList<>助手类
    /// </summary>
    public class ConvertHelper
    {
        /// <summary>
        /// 将字符串转换成相应的枚举值
        /// </summary>
        public static T EnumParse<T>(string value)
        {
            return (T)System.Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// 单表查询结果转换成泛型集合
        /// </summary>
        /// <typeparam name="T">泛型集合类型</typeparam>
        /// <param name="dt">查询结果DataTable</param>
        /// <returns>以实体类为元素的泛型集合</returns>
        public static IList<T> ConvertToList<T>(DataTable dt) where T : new()
        {
            // 定义集合
            List<T> ts = new List<T>();
            // 获得此模型的类型
            Type type = typeof(T);
            //定义一个临时变量
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//将属性名称赋值给临时变量
                    //检查DataTable是否包含此列（列名==对象的属性名）
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite) continue;//该属性不可写，直接跳出
                        //取值
                        object value = dr[tempName];
                        //如果非空，则赋给对象的属性
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                //对象添加到泛型集合中
                ts.Add(t);
            }
            return ts;
        }
        
        /// <summary>
        /// 单表查询结果转换成泛型类
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="dt">查询结果DataTable</param>
        /// <returns>实体类/returns>
        public static T ConvertToEntity<T>(DataTable dt) where T : new()
        {
            if (dt.Rows.Count == 0) return new T();
            return ConvertToEntity<T>(dt.Rows[0]);
            //// 定义泛型类
            //T t = new T();

            //// 获得此模型的类型
            //Type type = typeof(T);
            ////定义一个临时变量
            //string tempName = string.Empty;
            ////遍历DataTable中所有的数据行
            //foreach (DataRow dr in dt.Rows)
            //{
            //    // 获得此模型的公共属性
            //    PropertyInfo[] propertys = t.GetType().GetProperties();
            //    //遍历该对象的所有属性
            //    foreach (PropertyInfo pi in propertys)
            //    {
            //        tempName = pi.Name;//将属性名称赋值给临时变量
            //        //检查DataTable是否包含此列（列名==对象的属性名）
            //        if (dt.Columns.Contains(tempName))
            //        {
            //            // 判断此属性是否有Setter
            //            if (!pi.CanWrite) continue;//该属性不可写，直接跳出
            //            //取值
            //            object value = dr[tempName];
            //            //如果非空，则赋给对象的属性
            //            if (value != DBNull.Value)
            //                pi.SetValue(t, value, null);
            //        }
            //    }
            //}

            //return t;
        }

        /// <summary>
        /// 单行记录转换成泛型类
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="dr">数据行</param>
        /// <returns>实体类/returns>
        public static T ConvertToEntity<T>(DataRow dr) where T : new()
        {
            T t = new T(); // 定义泛型类           
            Type type = typeof(T); // 获得此模型的类型
            DataColumnCollection dcc = dr.Table.Columns;
            PropertyInfo[] propertys = t.GetType().GetProperties(); // 获得此模型的公共属性
                       
            //遍历该对象的所有属性
            foreach (PropertyInfo pi in propertys)
            {
                string tempName = pi.Name; // 将属性名称赋值给临时变量

                // 检查DataTable是否包含此列（列名==对象的属性名）
                if (dcc.Contains(tempName))
                {
                    if (!pi.CanWrite) continue; // 如果该属性不可写，直接跳出
                   
                    object value = dr[tempName]; // 取值                   
                    if (value != DBNull.Value) // 如果非空，则赋给对象的属性
                        pi.SetValue(t, value, null);
                }
            }

            return t;
        }

        /// <summary>
        /// 表单Form集合转换成泛型类
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="formName">表单名称</param>
        /// <returns>实体类/returns>
        public static T ConvertToEntity<T>(string formName = "SimpleForm1") where T : new()
        {
            string keyName = null;
            System.Collections.Specialized.NameValueCollection Col = HttpContext.Current.Request.Form;

            // 定义泛型类
            T t = new T();

            // 获得此模型的类型
            Type type = typeof(T);

            // 获得此模型的公共属性
            PropertyInfo[] propertys = t.GetType().GetProperties();

            //遍历该对象的所有属性
            foreach (PropertyInfo pi in propertys)
            {
                // 判断此属性是否有Setter
                if (!pi.CanWrite) continue;//该属性不可写，直接跳出

                string v = GetKeyValue(Col, pi.Name);
                if (!string.IsNullOrEmpty(v)) pi.SetValue(t, v, null);
                //switch (pi.Name)
                //{
                //    case "Pkid":
                //        keyName = formName + "$hfd" + pi.Name;
                //        if (Col.AllKeys.Contains(keyName))
                //            pi.SetValue(t, Col.GetValues(keyName)[0], null);
                //        break;
                //    default:
                //        keyName = formName + "$tbx" + pi.Name;
                //        if (Col.AllKeys.Contains(keyName))
                //            pi.SetValue(t, Col.GetValues(keyName)[0], null);
                //        else
                //        {
                //            keyName = formName + "$ddl" + pi.Name + "$Value";
                //            if (Col.AllKeys.Contains(keyName))
                //                pi.SetValue(t, Col.GetValues(keyName)[0], null);
                //            else
                //            {
                //                keyName = formName + "$lbl" + pi.Name;
                //                if (Col.AllKeys.Contains(keyName))
                //                    pi.SetValue(t, Col.GetValues(keyName)[0], null);
                //            }
                //        }
                //        break;
                //}
            }

            return t;
        }

        /// <summary>
        /// 表单Form集合转换成泛型类
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="formName">表单名称</param>
        /// <returns>实体类/returns>
        public static T ConvertToEntity<T>(params string[] formNames) where T : new()
        {
            System.Collections.Specialized.NameValueCollection Col = HttpContext.Current.Request.Form;

            // 定义泛型类
            T t = new T();
            
            // 获得此模型的类型
            Type type = typeof(T);

            // 获得此模型的公共属性
            PropertyInfo[] propertys = t.GetType().GetProperties();

            //遍历该对象的所有属性
            foreach (PropertyInfo pi in propertys)
            {
                // 判断此属性是否有Setter
                if (!pi.CanWrite) continue;//该属性不可写，直接跳出

                string v = GetKeyValue(Col, pi.Name, string.Join(",", formNames));
                if(!string.IsNullOrEmpty(v)) pi.SetValue(t, v, null);
            }

            return t;
        }

        private static string GetKeyValue(System.Collections.Specialized.NameValueCollection col, string propertyName, string formName = "SimpleForm1")
        {
            foreach (string name in col.AllKeys)
            {
                if (!Globals.IsInclude(name, formName)) continue;

                if (name.EndsWith(propertyName + "$Value")) return col[name].ToString();
                else if (name.EndsWith(propertyName)) return col[name].ToString();
            }
            return null;
        }
    }
}
