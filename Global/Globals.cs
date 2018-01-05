using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json.Linq;
using TU = TStar.Utility;

namespace TStar.Web
{
    public partial class Globals
    {
        private static bool debug = false;//cs.Debug;
        private static Random Rand = new Random();

        public static HttpContext Page
        {
            get { return HttpContext.Current; }
        }

        public static string FilterString(string text)
        {
            return TU.Globals.FilterString(text);
        }

        public static string[] ConvertFromJArray(JArray array)
        {
            string[] rlt = new string[array.Count];
            for (int i = 0; i < array.Count; i++)
            {
                rlt[i] = array[i].ToString();
            }
            return rlt;
        }

        /// <summary>
        /// 去除开头的指定子串
        /// </summary>
        public static string DelStartString(string text, string substr)
        {
            return text.StartsWith(substr) ? text.Substring(substr.Length) : text;
        }
        /// <summary>
        /// 去除结尾的指定子串
        /// </summary>
        public static string DelEndString(string text, string substr)
        {
            return text.EndsWith(substr) ? text.Remove(text.Length - substr.Length) : text;
        }
        /// <summary>
        /// 在开头添加指定子串
        /// </summary>
        public static string AddStartString(string text, string substr)
        {
            return text.StartsWith(substr) ? text : substr + text;
        }
        /// <summary>
        /// 在结尾添加指定子串
        /// </summary>
        public static string AddEndString(string text, string substr)
        {
            return text.EndsWith(substr) ? text : text + substr;
        }

        /// <summary>
        /// 给定相对根目录的页面，获取对应的绝对路径页面
        /// </summary>
        /// <returns></returns>
        public static string GetAbsolutePagePath(string pagePath)
        {
            string root = Page.Request.ApplicationPath;
            return AddEndString(root, "/") + DelStartString(pagePath, "/");
        }

        /// <summary>
        /// 在指定位置创建新目录
        /// </summary>
        /// <param name="mapPath">网站虚拟路径，如：~/Uploads</param>
        /// <param name="dir">目录名称</param>
        public static void CreateDir(string mapPath, string dir)
        {
            string d = Page.Server.MapPath(mapPath) + @"\" + dir;
            if (!Directory.Exists(d)) Directory.CreateDirectory(d);
        }

        /// <summary>
        /// 删除指定位置的目录
        /// </summary>
        /// <param name="mapPath">网站虚拟路径，如：~/Uploads</param>
        public static void DeleteDir(string mapPath, string dir)
        {
            string path = Page.Server.MapPath(mapPath) + @"\" + dir;
            if (Directory.Exists(path)) Directory.Delete(path, true);
        }
        /// <summary>
        /// 删除指定位置的目录
        /// </summary>
        /// <param name="mapPath">网站虚拟路径，如：~/Uploads</param>
        /// <param name="xmbhs"></param>
        public static void DeleteDir(string mapPath, string[] dirs)
        {
            string root = Page.Server.MapPath(mapPath);
            foreach (string dir in dirs)
            {
                string path = root + @"\" + dir;
                if (Directory.Exists(path)) Directory.Delete(path, true);
            }
        }

        /// <summary>
        /// 查找指定字段的查询条件（日期字段需以time结尾）
        /// </summary>
        public static string GetQueryFieldValue(string where, string key)
        {
            string[] keys = where.Split(new string[] { "AND", "OR" }, StringSplitOptions.None);
            for (int i = 0; i < keys.Length; i++)
            {
                string s = keys[i];
                if (s.IndexOf(key) >= 0)
                {
                    string v = s.Split('\'')[1];
                    if (key.ToLower().EndsWith("time") && s.ToUpper().IndexOf("BETWEEN") >= 0)
                    {
                        DateTime dt1 = DateTime.Parse(v);
                        DateTime dt2 = DateTime.Parse(keys[i + 1].Trim().Replace("'", ""));
                        if (dt1.Day == 1 && dt2 == dt1.AddMonths(1).AddDays(-1)) return dt1.ToString("yyyy年MM月");
                        else if (dt1.ToString("MM-dd") == "01-01" && dt2.ToString("MM-dd") == "12-31" && dt1.Year == dt2.Year) return dt1.ToString("yyyy年");
                        else return dt1.ToString("yyyy-MM-dd") + "至" + dt2.ToString("yyyy-MM-dd");
                    }
                    else return v;
                }
            }
            return "";
        }

        ///// <summary>
        ///// 删除指定项目编号的上传图片目录
        ///// </summary>
        ///// <param name="mapPath">网站虚拟路径，如：~/Uploads</param>
        ///// <param name="xmbh"></param>
        //public static void DeleteDir(string mapPath, string xmbh)
        //{
        //    string path = Page.Server.MapPath(mapPath) + @"\" + xmbh;
        //    if (Directory.Exists(path)) Directory.Delete(path, true);
        //}
        ///// <summary>
        ///// 删除指定项目编号的上传图片目录
        ///// </summary>
        ///// <param name="mapPath">网站虚拟路径，如：~/Uploads</param>
        ///// <param name="xmbhs"></param>
        //public static void DeleteDir(string mapPath, string[] xmbhs)
        //{
        //    string dir = Page.Server.MapPath(mapPath);
        //    foreach (string bh in xmbhs)
        //    {
        //        string path = dir + @"\" + bh;
        //        if (Directory.Exists(path)) Directory.Delete(path, true);
        //    }
        //}
    }
}
