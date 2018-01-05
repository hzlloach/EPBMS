using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TU = TStar.Utility;

namespace BLL.Lcgl
{
    public class Lc_nfzmd : BLL.Global.Base
    {
        /// <summary>
        /// 判断当学期是否已导入过
        /// </summary>
        public static bool Exist(string xsbh)//, string dbrq)
        {
            return Exists<Model.Lcgl.Lc_nfzmd>(null, new string[] { "Xq", "Xsbh"/*, "Dbrq"*/ }, new string[] { BLL.Globals.SystemSetting.Dqxq, xsbh });
        }

        public static bool Exist(string pkid, string[] fields, string[] keys)
        {
            return Exists<Model.Lcgl.Lc_nfzmd>(pkid, fields, keys);
        }
        
        /// <summary>
        /// 保存名单
        /// </summary>
        public static bool Save(Model.Lcgl.Lc_nfzmd xm)
        {
            int r = 0;
            if (String.IsNullOrEmpty(xm.Pkid)) // 新增
                r = Insert(xm);
            else // 保存
                r = Update(xm);

            return r > 0;
        }

        /// <summary>
        /// 删除单个名单
        /// </summary>
        public static bool Delete(string pkid)
        {
            string where = "Pkid='" + pkid + "'";
            int r = Delete<Model.Lcgl.Lc_nfzmd>(pkid);

            // 写日志

            return r > 0;
        }
        /// <summary>
        /// 批量删除名单
        /// </summary>
        public static bool Delete(string[] xmbhs)
        {
            int r = DeleteList<Model.Lcgl.Lc_nfzmd>(xmbhs);

            // 写日志

            return r > 0;
        }

        /// <summary>
        /// 清空导入的名单
        /// </summary>
        public static int ClearImport(string bmbh, string dzbbh)
        {
            string xmWhere = String.Format("Bmbh='{0}' AND Xq='{1}' AND Ztdm='{2}'", bmbh, BLL.Globals.SystemSetting.Dqxq, (int)TStar.Web.Globals.SystemSetting.Status.Draft);
            if(!string.IsNullOrEmpty(dzbbh)) xmWhere += String.Format(" AND Dzbbh = '{0}'", dzbbh);
            
            // 删除名单表
            return DeleteList<Model.Lcgl.Lc_nfzmd>(xmWhere);
        }

        /// <summary>
        /// 提交导入的名单
        /// </summary>
        public static int Submit(string bmbh, string dzbbh)
        {
            string dqxq = BLL.Globals.SystemSetting.Dqxq;
            string xmWhere = String.Format("Bmbh='{0}' AND Xq='{1}' AND Ztdm='{2}'", bmbh, dqxq, (int)TStar.Web.Globals.SystemSetting.Status.Draft);
            if (!string.IsNullOrEmpty(dzbbh)) xmWhere += String.Format(" AND Dzbbh = '{0}'", dzbbh);

            // 提交名单表
            string czsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int ztdm = (int)TStar.Web.Globals.SystemSetting.Status.Submitted;
            int r = UpdateFields<Model.Lcgl.Lc_nfzmd>(new string[] { "Ztdm", "Drsj" }, new object[] { ztdm, czsj }, xmWhere);

            // 修改学生发展状态
            xmWhere = string.Format("Fzztdm='{0}' AND Pkid IN (SELECT Xsbh FROM lc_nfzmd m WHERE m.Bmbh='{1}' AND m.Xq='{2}' AND m.Ztdm='{3}' AND m.Drsj='{4}' AND m.Dbjgdm='{5}')", (int)TStar.Web.Globals.SystemSetting.Fzzt.Jjfz, bmbh, dqxq, ztdm, czsj, 1);
            string sql = string.Format("UPDATE jc_xs SET Fzztdm='{0}', Fzdxrq=(SELECT Fzdxrq FROM lc_nfzmd m WHERE m.Xsbh=jc_xs.Pkid) WHERE {1}", (int)TStar.Web.Globals.SystemSetting.Fzzt.Nfzdx, xmWhere);
            r = DAL.Globals.Execute(sql); 
            return r;
        }

        private static int Insert(Model.Lcgl.Lc_nfzmd xm)
        {
            xm.Xq = BLL.Globals.SystemSetting.Dqxq;
            xm.Ztdm = (int)TStar.Web.Globals.SystemSetting.Status.Draft;
            int r = Insert<Model.Lcgl.Lc_nfzmd>(xm);

            // 写日志

            return r;
        }

        private static int Update(Model.Lcgl.Lc_nfzmd xm)
        {
            //xm.Ztdm = (int)TStar.Web.Globals.SystemSetting.Status.Submitted;
            int r = Update<Model.Lcgl.Lc_nfzmd>(xm);

            // 写日志

            return r;
        }
    }
}
