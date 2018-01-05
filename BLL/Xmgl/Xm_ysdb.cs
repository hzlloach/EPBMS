using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TU = TStar.Utility;

namespace BLL.Xmgl
{
    public class Xm_ysdb : BLL.Global.Base
    {
        public static bool Exist(string pkid, string[] fields, string[] keys)
        {
            return Exists<Model.Xmgl.Xm_ysdb>(pkid, fields, keys);
        }
        
        /// <summary>
        /// 保存项目
        /// </summary>
        public static bool Save(Model.Xmgl.Xm_ysdb xm)
        {
            int r = 0;
            if (String.IsNullOrEmpty(xm.Pkid)) // 新增
                r = Insert(xm);
            else // 保存
                r = Update(xm);

            return r > 0;
        }

        /// <summary>
        /// 删除单个项目（无上传附件）
        /// </summary>
        public static bool Delete(string pkid)
        {
            string where = "Pkid='" + pkid + "'";
            int r = Delete<Model.Xmgl.Xm_ysdb>(pkid);

            // 写日志

            return r > 0;
        }
        /// <summary>
        /// 批量删除项目（无上传附件）
        /// </summary>
        public static bool Delete(string[] xmbhs)
        {
            int r = DeleteList<Model.Xmgl.Xm_ysdb>(xmbhs);

            // 写日志

            return r > 0;
        }

        /// <summary>
        /// 清空导入的项目
        /// </summary>
        public static int ClearImport(string bmbh, string zbbh)
        {
            string xmWhere = String.Format("Bmbh = '{0}' AND Zbbh = '{1}'", bmbh, zbbh);//TStar.Web.Globals.SystemSetting.Dqxn, 
            
            // 删除项目表
            return DeleteList<Model.Xmgl.Xm_ysdb>(xmWhere);
        }

        private static int Insert(Model.Xmgl.Xm_ysdb xm)
        {
            int r = Insert<Model.Xmgl.Xm_ysdb>(xm);

            // 写日志

            return r;
        }

        private static int Update(Model.Xmgl.Xm_ysdb xm)
        {
            int r = Update<Model.Xmgl.Xm_ysdb>(xm);

            // 写日志

            return r;
        }
    }
}
