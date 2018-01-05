using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using O2S.Components.PDFRender4NET;

namespace TStar.Utility
{
    /// <summary>
    ///Globals 的摘要说明
    /// </summary>
    public partial class Globals
    {
        private static Random Rand = new Random();

        public static HttpContext Page
        {
            get { return HttpContext.Current; }
        }

        /// <summary>
        /// 删除指定目录
        /// </summary>
        public static void DeleteDirectory(string dir)
        {
            if (Directory.Exists(dir))
                Directory.Delete(dir, true);
        }

        /// <summary>
        /// 删除指定文件
        /// </summary>
        public static void DeleteFile(string file)
        {
            File.Delete(file);
        }

        /// <summary>
        /// 批量删除指定目录下带前缀的所有文件
        /// </summary>
        public static void DeleteBatFile(string dir, string prefix)
        {
            DirectoryInfo di = new DirectoryInfo(dir);
            FileInfo[] fis = di.GetFiles();  //返回目录中所有文件和子目录
            foreach (FileInfo f in fis)
            {
                if (f.Name.ToLower().StartsWith(prefix))
                    File.Delete(f.FullName);
            }
        }

        #region 公共方法

        public static string EncodeUrl(object url)
        {
            if (url == null || url.ToString() == "") return "";
            else return HttpUtility.UrlEncode(url.ToString(), System.Text.Encoding.GetEncoding("UTF-8"));//GB2312")); //Page.Server.UrlEncode(url.ToString());
        }
        public static string DecodeUrl(object url)
        {
            if (url == null || url.ToString() == "") return "";
            else return HttpUtility.UrlDecode(url.ToString(), System.Text.Encoding.GetEncoding("UTF-8"));//GB2312")); //Page.Server.UrlEncode(url.ToString());
        }
        
        /// <summary>
        /// 将数组构造成MD5加密的摘要字符串
        /// </summary>
        public static string GetEncryptSummary(params object[] keys)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object key in keys)
            {
                sb.Append("|" + key.ToString());
            }
            return Globals.MD5Encrypt(sb.ToString().Substring(1));
        }
        /// <summary>
        /// 验证数组构造的摘要字符串是否匹配
        /// </summary>
        public static bool IsMatchSummary(string summary, params object[] keys)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object key in keys)
            {
                sb.Append("|" + key.ToString());
            }
            return Globals.MD5Encrypt(sb.ToString().Substring(1)).Equals(summary);
        }

        /// <summary>
        /// 将数组构造成加密的字符串
        /// </summary>
        public static string GetEncryptMemo(params object[] keys)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object key in keys)
            {
                sb.Append("|" + key.ToString());
            }
            return TripleDESEncrypt(sb.ToString().Substring(1));
        }

        /// <summary>
        /// 将加密的字符串解析成数组
        /// </summary>
        public static string[] GetDecryptMemo(string ciphertext)
        {
            try
            {
                string s = Globals.TripleDESDecrypt(ciphertext);
                return s.Split('|');
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 取最大值
        /// </summary>
        public static int GetMax(int v1, int v2)
        {
            return v1 >= v2 ? v1 : v2;
        }

        /// <summary>
        /// 取不大于upper的值
        /// </summary>
        public static int GetUpper(int v, int upper)
        {
            if (upper > 0 && v > upper) return upper;
            return v;
        }

        /// <summary>
        /// 转换为整数
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        public static bool Parse2Int(string str)
        {
            int d = 0;
            return int.TryParse(str, out d);
        }

        /// <summary>
        /// 转换为浮点数
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        public static bool Parse2Float(string str)
        {
            float d = 0;
            return float.TryParse(str, out d);
        }

        /// <summary>
        /// 转换为浮点数
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        public static bool Parse2Double(string str)
        {
            double d = 0;
            return double.TryParse(str, out d);
        }

        /// <summary>
        /// 转换为整数
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="def">转换不成功时的默认值</param>
        /// <returns></returns>
        public static int Parse2Int(string str, int def)
        {
            int d = 0;
            if(int.TryParse(str, out d)) return d;
            return def;
        }

        /// <summary>
        /// 转换为浮点数
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="def">转换不成功时的默认值</param>
        /// <returns></returns>
        public static float Parse2Float(string str, float def)
        {
            float d = 0;
            if (float.TryParse(str, out d)) return d;
            return def;
        }

        /// <summary>
        /// 转换为浮点数
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        public static double Parse2Double(string str, double def)
        {
            double d = 0;
            if (double.TryParse(str, out d)) return d;
            return def;
        }
        
        /// <summary>
        /// 转换为浮点数
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="def">转换不成功时的默认值</param>
        /// <returns></returns>
        public static DateTime Parse2DateTime(string str, string def)
        {
            DateTime dtDef = new DateTime(2000, 1, 1);
            DateTime dt = dtDef;
            if (DateTime.TryParse(str, out dt)) return dt;
            if(!DateTime.TryParse(def, out dtDef)) return dt;
            return dtDef;
        }

        /// <summary>
        /// 计算表达式的值
        /// </summary>
        public static int EvalExpression(string exp)
        {
            try
            {
                int v = 0;
                double d = 0;
                string rlt = new DataTable().Compute(exp, null).ToString();

                if (int.TryParse(rlt, out v)) return v;
                if (double.TryParse(rlt, out d)) return (int)(Math.Round(d, 0));
                return int.MinValue;
            }
            catch
            {
                return int.MinValue;
            }
        }

        /// <summary>
        /// 过滤非法字符
        /// </summary>
        public static string FilterRiskChar(string str)
        {
            if(str == null) return "";
            
            str = str.Trim();
            if (String.IsNullOrEmpty(str)) return "";

            string word = "and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join|cmd|;|'|--";//这里加要过滤的SQL字符
            if (str == null)
                return "";
            foreach (string i in word.Split('|'))
            {
                if ((str.ToLower().IndexOf(i + " ") > -1) || (str.ToLower().IndexOf(" " + i) > -1))//如果有非法关键字就返回空
                {
                    return "";
                }
            }

            string click = "onload|onunload|onchange|onsubmit|onreset|onselect|onblur|onfocus|onabort|onkeydown|onkeypress|onkeyup|onclick|ondblclick|onmousedown|onmousemove|onmouseout|onmouseover|onmouseup";//过滤脚本事件

            foreach (string i in click.Split('|'))
            {
                if ((str.ToLower().IndexOf(i) > -1) || (str.ToLower().IndexOf(i) > -1))//如果有非法关键字就返回空
                {
                    return "";
                }
            }

            str = Regex.Replace(str, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML            
            str = Regex.Replace(str, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"-->", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"<!--.*", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "xp_cmdshell", "", RegexOptions.IgnoreCase);

            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("*", "");
            //str = str.Replace("-", "");
            str = str.Replace("?", "");
            str = str.Replace("'", "''");
            str = str.Replace(",", "");
            //str = str.Replace("/", "");
            str = str.Replace(";", "");
            str = str.Replace("*/", "");
            str = str.Replace("\r\n", "");

            return str;
        }

        /// <summary>
        /// 是否在给定的枚举范围内
        /// </summary>
        /// <param name="text"></param>
        /// <param name="enums">枚举范围，以“,”分隔</param>
        /// <returns></returns>
        public static bool IsIN(string text, string enums)
        {
            if (String.IsNullOrEmpty(text)) return false;

            char ch = ',';
            string[] t = enums.Split(ch);
            foreach (string ts in t)
            {
                if (text.Equals(ts)) return true;
            }
            return false;
        }

        /// <summary>
        /// 是否包含给定的字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="enums">给定字符串，以“,”分隔</param>
        /// <returns></returns>
        public static bool IsInclude(string text, string enums)
        {
            if (String.IsNullOrEmpty(text)) return false;

            char ch = ',';
            string[] t = enums.Split(ch);
            foreach (string ts in t)
            {
                if (text.IndexOf(ts) > -1) return true;
            }
            return false;
        }


        /// <summary>
        /// 判断日期d是否≥low且≤high
        /// </summary>
        public static bool IsDateBetweenEqual(string d, string low, string high)
        {
            DateTime dt;
            if (!DateTime.TryParse(d, out dt)) return false;

            DateTime dtLow = DateTime.Parse(low);
            DateTime dtHigh = DateTime.Parse(high);
            return dt >= dtLow && dt <= dtHigh;
        }
        /// <summary>
        /// 判断日期d是否＞low且＜high
        /// </summary>
        public static bool IsDateBetween(string d, string low, string high)
        {
            DateTime dt;
            if (!DateTime.TryParse(d, out dt)) return false;

            DateTime dtLow = DateTime.Parse(low);
            DateTime dtHigh = DateTime.Parse(high);
            return dt > dtLow && dt < dtHigh;
        }
        /// <summary>
        /// 判断s是否≥low且≤high
        /// </summary>
        public static bool IsBetweenEqual(string s, string low, string high)
        {
            return low.CompareTo(s) <= 0 && high.CompareTo(s) >= 0;
        }
        /// <summary>
        /// 判断s是否≥low且≤high
        /// </summary>
        public static bool IsBetweenEqual(long s, long low, long high)
        {
            return low <= s && high >= s;
        }
        /// <summary>
        /// 判断s是否＞low且＜high
        /// </summary>
        public static bool IsBetween(string s, string low, string high)
        {
            return low.CompareTo(s) < 0 && high.CompareTo(s) > 0;
        }
        /// <summary>
        /// 判断s是否＞low且＜high
        /// </summary>
        public static bool IsBetween(long s, long low, long high)
        {
            return low < s && high > s;
        }

        /// <summary>
        /// 过滤字符串
        /// </summary>
        public static string FilterString(string s)
        {
            return FilterRiskChar(s.Trim());
        }

        /// <summary>
        /// 获取URL参数值（找不到时设成默认值）
        /// </summary>
        public static string GetParaValue(string paraName, string nullValue)
        {
            return GetParaValue(Page.Session, Page.Request, paraName, nullValue);
        }
        /// <summary>
        /// 获取URL参数值（找不到时设成默认值）
        /// </summary>
        private static string GetParaValue(HttpSessionState oSession, HttpRequest oRequest, string paraName, string nullValue)
        {
            string strValue = "";
            try
            {
                strValue = oRequest[paraName].ToString();
            }
            catch (NullReferenceException)
            {
                strValue = "";
            }

            if (String.IsNullOrEmpty(strValue))
            {
                try
                {
                    strValue = oSession[paraName].ToString();
                }
                catch (NullReferenceException)
                {
                    strValue = "";
                }

                if (String.IsNullOrEmpty(strValue))
                {
                    strValue = nullValue;
                }
            }

            return strValue;
        }


        public static void SetObject(object obj, string objName, bool isGAC)
        {
            if (System.Web.HttpContext.Current != null)
            {
                RemoveObject(objName);
                if (isGAC)
                {
                    System.Web.HttpContext.Current.Application.Add(objName, obj);
                }
                else
                {
                    System.Web.HttpContext.Current.Session.Add(objName, obj);
                }
            }
        }
        public static object GetObject(string objName)
        {
            if (System.Web.HttpContext.Current != null)
            {
                if (System.Web.HttpContext.Current.Application[objName] != null)
                {
                    return System.Web.HttpContext.Current.Application[objName];
                }
                else
                {
                    return System.Web.HttpContext.Current.Session[objName];
                }
            }
            return null;
        }
        public static void RemoveObject(string objName)
        {
            if (System.Web.HttpContext.Current != null)
            {
                System.Web.HttpContext.Current.Application[objName] = null;
                System.Web.HttpContext.Current.Session[objName] = null;
                System.Web.HttpContext.Current.Application.Remove(objName);
                System.Web.HttpContext.Current.Session.Remove(objName);
            }
        }
        public static void RemoveAllSessions()
        {
            if (System.Web.HttpContext.Current != null)
            {
                System.Web.HttpContext.Current.Session.Clear();
            }
        }

        public static string BindSystemCode(DataTable dt, string dmField, string mcField, string dmValue, string defVal)
        {
            DataView dv = dt.DefaultView;
            string filter = dv.RowFilter;
            string sort = dv.Sort;
            try
            {
                dv.Sort = dmField;
                DataRowView[] drvs = dv.FindRows(dmValue);

                if (drvs.Length > 0)
                    return drvs[0][mcField].ToString();
                else
                    return defVal;
            }
            finally
            {
                dv.RowFilter = filter;
                dv.Sort = sort;
            }
        }
        public static string BindSystemCode(DataTable dt, string filter, string dmField, string mcField, string dmValue, string defVal)
        {
            DataView dv = dt.DefaultView;
            string sort = dv.Sort;
            string flt = dv.RowFilter;
            try
            {
                dv.RowFilter = filter;
                dv.Sort = dmField;
                DataRowView[] drvs = dv.FindRows(dmValue);

                if (drvs.Length > 0)
                    return drvs[0][mcField].ToString();
                else
                    return defVal;
            }
            finally
            {
                dv.Sort = sort;
                dv.RowFilter = flt;
            }
        }
        public static DataRowView GetSystemCodeRow(DataTable dt, string fields, string values)
        {
            DataView dv = dt.DefaultView;
            string filter = dv.RowFilter;
            string sort = dv.Sort;
            try
            {
                dv.Sort = fields;
                DataRowView[] drvs = dv.FindRows(values.Split(','));

                return drvs == null || drvs.Length == 0 ? null : drvs[0];
            }
            finally
            {
                dv.RowFilter = filter;
                dv.Sort = sort;
            }
        }
        
        

        /// <summary>
        /// MD5加密
        /// </summary>
        public static string MD5Encrypt(string input)
        {
            try
            {
                MD5 md5Hasher = MD5.Create();
                byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("X2"));
                }
                return sBuilder.ToString();
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        /// <summary>
        /// 三重DES加密
        /// </summary>
        public static string TripleDESEncrypt(string input)
        {
            try
            {
                Byte[] data = Encoding.UTF8.GetBytes(input);
                Byte[] Key0 = { 0x0e, 0x2c, 0x4a, 0x69, 0x87, 0xb5, 0xd3, 0xf1, 0x69, 0x87, 0xb5, 0xd3, 0xf1, 0x0e, 0x2c, 0x4a, 0xd3, 0xf1, 0x0e, 0x2c, 0x4a, 0x69, 0x87, 0xb5 };
                Byte[] IV0 = { 0x1f, 0x3d, 0x5b, 0x78, 0x96, 0xa4, 0xc2, 0xe0 };
                Byte[] result = TripleDES.Create().CreateEncryptor(Key0, IV0).TransformFinalBlock(data, 0, data.Length);

                return Convert.ToBase64String(result);
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        /// <summary>
        /// 三重DES解密
        /// </summary>
        public static string TripleDESDecrypt(string input)
        {
            try
            {
                Byte[] data = Convert.FromBase64String(input);
                Byte[] Key0 = { 0x0e, 0x2c, 0x4a, 0x69, 0x87, 0xb5, 0xd3, 0xf1, 0x69, 0x87, 0xb5, 0xd3, 0xf1, 0x0e, 0x2c, 0x4a, 0xd3, 0xf1, 0x0e, 0x2c, 0x4a, 0x69, 0x87, 0xb5 };
                Byte[] IV0 = { 0x1f, 0x3d, 0x5b, 0x78, 0x96, 0xa4, 0xc2, 0xe0 };
                Byte[] result = TripleDES.Create().CreateDecryptor(Key0, IV0).TransformFinalBlock(data, 0, data.Length);

                return Encoding.UTF8.GetString(result);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 在数组中查找是否存在指定值的元素（不区分大小写）
        /// </summary>
        public static bool Find(string[] array, string text)
        {
            if (String.IsNullOrEmpty(text)) return false;

            string t = text.ToLower();
            foreach (string s in array)
            {
                if (t.Equals(s.ToString().ToLower()))
                    return true;
            }

            return false;
        }
        /// <summary>
        /// 在数组中查找是否存在指定值的元素（不区分大小写）
        /// </summary>
        public static bool Find(string[] array, string text, out int idx)
        {
            idx = -1;
            if (String.IsNullOrEmpty(text)) return false;

            string t = text.ToLower();
            for (int i = 0; i < array.Length; i++)
            {
                string s = array[i].ToLower();

                if (t.Equals(s))
                {
                    idx = i;
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 在数组中查找是否存在指定值的元素（区分大小写）
        /// </summary>
        public static bool Find(object[] array, object key)
        {
            if (key == null) return false;

            foreach (object s in array)
            {
                if (key.Equals(s))
                    return true;
            }

            return false;
        }/// <summary>
        /// 在数组中查找是否存在指定值的元素（区分大小写）
        /// </summary>
        public static bool Find(object[] array, object key, out int idx)
        {
            idx = -1;
            if (key == null) return false;

            for(int i = 0; i < array.Length; i++)
            {
                object s = array[i];

                if (key.Equals(s))
                {
                    idx = i;
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 在视图中查找指定字段值的记录位置
        /// </summary>
        public static int Find(DataView dv, string field, object key)
        {
            if (key == null) return -1;

            for (int i = 0; i < dv.Count; i++)
            {
                if (key.Equals(dv[i][field])) return i;
            }

            return -1;
        }
        /// <summary>
        /// 在视图中查找指定字段值的记录行
        /// </summary>
        public static DataRow FindRow(DataView dv, string[] fields, object[] keys)
        {
            if (fields.Length != keys.Length) return null;

            for (int i = 0; i < dv.Count; i++)
            {
                bool found = true;
                for (int j = 0; j < fields.Length; j++)
                {
                    if (!keys[j].Equals(dv[i][fields[j]])) found = false;
                }
                if (found) return dv[i].Row;
            }

            return null;
        }
        /// <summary>
        /// 在视图中查找指定字段值的记录行
        /// </summary>
        public static DataRow FindRow(DataView dv, string field, object key)
        {
            if (key == null) return null;

            for (int i = 0; i < dv.Count; i++)
            {
                if (key.Equals(dv[i][field])) return dv[i].Row;
            }

            return null;
        }

        /// <summary>
        /// 截取指定长度的字串（含...）
        /// </summary>
        public static string SubString(string text, int len)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string tmpText = "";
            int cnt = 0, txtlen = text.Length;
            for (int i = 0; i < txtlen; i++)
            {
                char c = text[i];
                byte[] bs = System.Text.Encoding.GetEncoding("gb2312").GetBytes(c.ToString());
                cnt += bs.Length;

                if (cnt > len - 3 && String.IsNullOrEmpty(tmpText)) tmpText = sb.ToString();

                if (cnt == len && i == txtlen - 1) return sb.ToString() + c; // 刚好               
                else if (cnt >= len) return tmpText + "..."; // 添加...

                sb.Append(c);
            }
            return sb.ToString().Trim();
        }

        /// <summary>
        /// 获取指定位置的图像
        /// </summary>
        public static System.Drawing.Image GetImage(string filePath)
        {
            System.Drawing.Image img = null;
            try
            {
                img = System.Drawing.Image.FromFile(filePath);
                System.Drawing.Image bmp = new System.Drawing.Bitmap(img);

                return bmp;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (img != null) img.Dispose();
            }
        }

        /// <summary>
        /// 将字符串转成字节数组
        /// </summary>
        public static byte[] GetBytesFromHexString(string msg)
        {
            int len = msg.Length / 2, d1 = 0, d2 = 0;
            byte[] b = new byte[len];
            for (int i = 0; i < len; i++)
            {
                char c = msg[i + i];
                if (c >= '0' && c <= '9') d1 = c - '0';
                else if (c >= 'a' && c <= 'z') d1 = 10 + c - 'a';
                else if (c >= 'A' && c <= 'Z') d1 = 10 + c - 'A';
                else d1 = 0;
                c = msg[i + i + 1];
                if (c >= '0' && c <= '9') d2 = c - '0';
                else if (c >= 'a' && c <= 'z') d2 = 10 + c - 'a';
                else if (c >= 'A' && c <= 'Z') d2 = 10 + c - 'A';
                else d2 = 0;
                b[i] = (byte)(d1 * 16 + d2);
            }
            return b;
        }

        /// <summary>
        /// 将数据集以字符串显示
        /// </summary>
        public static string DataSet2String(DataSet ds)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            ds.WriteXml(sw);
            return sw.ToString();
        }

        /// <summary>
        /// 清除HTML格式
        /// </summary>
        public static string RemoveHTML(string Htmlstring)
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }

        /// <summary>
        /// 字符串换行截取
        /// </summary>
        /// <param name="text">待处理字符串</param>
        /// <param name="cntPerLine">每行字符数</param>
        /// <param name="allCount">总字符数（0表示不限制）</param>
        /// <param name="wrap">换行字符</param>
        /// <param name="isDeal">是否清除HTML标记</param>
        /// <returns></returns>
        //public static string WrapString(string text, int cntPerLine, int allCount, string wrap, bool isDeal)
        //{
        //bool isYljgsc = (text.IndexOf("原料价格") > -1) || (text.IndexOf("新菜试吃") > -1); // 是否原料价格或新菜试吃
        //if (text.IndexOf("新菜试吃") > -1) // 新菜试吃时去掉后面的备注信息
        //{
        //    int idx = text.IndexOf(Model.Globals.SplitChar);
        //    if(idx > 0) text = text.Substring(0, idx); 
        //}

        //    if (isDeal && !isYljgsc) text = RemoveHTML(text);
        //    if(allCount == 0) allCount= -10;

        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    int cnt = cntPerLine, len = 0;
        //    foreach (char c in text)
        //    {
        //        byte[] bs = System.Text.Encoding.GetEncoding("gb2312").GetBytes(c.ToString());
        //        if (bs.Length == 2) len = 2;
        //        else len = 1;

        //        if (allCount != -10)
        //        {
        //            allCount -= len;
        //            if (allCount < 0) break;
        //        }

        //        cnt -= len;
        //        if (cnt < 0 & !isYljgsc)
        //        {
        //            sb.Append(wrap);
        //            cnt = cntPerLine - bs.Length;
        //        }
        //        sb.Append(c);
        //    }
        //    return sb.ToString().Trim();
        //}
        
        #endregion
    }
}