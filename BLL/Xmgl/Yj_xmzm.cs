using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TU = TStar.Utility;

namespace BLL.Xmgl
{
    public class Yj_xmzm : BLL.Global.Base
    {
        /// <summary>
        /// 保存证明材料（删除时Pkid以Del@开头）
        /// </summary>
        public static int Save(Model.Yjgl.Yj_xmzm zm)
        {
            int k = 0;
            string pkid = zm.Pkid;
            if (String.IsNullOrEmpty(pkid))
                Insert(zm);
            else if (zm.Cflj.StartsWith("Del@"))
                Delete(zm);
            else
                Update(zm);
            return k;
        }

        /// <summary>
        /// 删除指定项目的证明材料
        /// </summary>
        public static void Delete(string bmbh, string dzbbh, string xsbh, string xmbh)
        {
            Delete<Model.Yjgl.Yj_xmzm>("Xmbh", xmbh);

            string path = String.Format("~/Uploads/Zmcl/{0}/{1}/{2}/{3}/", bmbh, dzbbh, xsbh, xmbh);
            TU.Globals.DeleteDirectory(TU.WebHelper.MapPath(path));
        }

        private static void ChangeFile(Model.Yjgl.Yj_xmzm zm)
        {
            int idx = zm.Cflj.LastIndexOf("/");
            string dir = zm.Cflj.Substring(0, idx); // 不带/
            string ext = zm.Cflj.Substring(zm.Cflj.LastIndexOf('.'));
            string oldfilename = zm.Cflj.Substring(idx); // 以/开头
            int no = int.Parse(oldfilename[5].ToString());
            string newfilename = "/" + no + "_" + zm.Clbt + ext;
            string path = TU.WebHelper.MapPath("~" + dir);
            File.Move(path + oldfilename, path + newfilename);

            zm.Cflj = dir + newfilename;
        }
        private static void Insert(Model.Yjgl.Yj_xmzm zm)
        {
            // 修改文件名
            ChangeFile(zm);

            Insert<Model.Yjgl.Yj_xmzm>(zm);
        }
        private static void Update(Model.Yjgl.Yj_xmzm zm)
        {
            // 删除原文件
            if (zm.OldCflj != zm.Cflj)
            {
                string path = TU.WebHelper.MapPath("~" + zm.OldCflj);
                File.Delete(path);
            }

            // 修改文件名
            ChangeFile(zm);

            Update<Model.Yjgl.Yj_xmzm>(zm);            
        }
        private static void Delete(Model.Yjgl.Yj_xmzm zm)
        {            
            Delete<Model.Yjgl.Yj_xmzm>(zm.Pkid);

            // 删除原文件
            string path = TU.WebHelper.MapPath("~" + zm.OldCflj);
            File.Delete(path);
        }
    }
}
