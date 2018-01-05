using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TStar.Utility
{
    public class WebHelper
    {
        public static HttpContext Page
        {
            get { return HttpContext.Current; }
        }


        /// <summary>
        /// 返回与虚拟路径相对应的物理路径
        /// </summary>
        /// <param name="path">以~/开头的虚拟路径</param>
        /// <returns></returns>
        public static string MapPath(string path)
        {
             return Page.Server.MapPath(path);
        }

    }
}
