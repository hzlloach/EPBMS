using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TU = TStar.Utility;

namespace BLL.Xmgl
{
    public class Yj_xm : BLL.Global.Base
    {
        public static bool Exist(string pkid, string[] fields, string[] keys)
        {
            return Exists<Model.Xmgl.Yj_xm>(pkid, fields, keys);
        }
        
        /// <summary>
        /// 保存项目
        /// </summary>
        public static bool Save(Model.Xmgl.Yj_xm xm)
        {
            int r = 0;
            if (String.IsNullOrEmpty(xm.Pkid)) // 新增
                r = Insert(xm);
            else // 保存
                r = Update(xm);

            return r > 0;
        }

        /// <summary>
        /// 删除单个项目
        /// </summary>
        public static bool Delete(string bmbh, string dzbbh, string xsbh, string xmbh)
        {
            string where = "Pkid='" + xmbh + "'";
            int ztdm = (int)TStar.Web.Globals.SystemSetting.Status.Deleted;
            Yj_xmzm.Delete(bmbh, dzbbh, xsbh, xmbh);
            int r = UpdateFields<Model.Xmgl.Yj_xm>("Ztdm", ztdm, where);//Delete<Model.Xmgl.Yj_xm>(xmbh);

            // 写日志

            return r > 0;
        }
        /// <summary>
        /// 批量删除项目
        /// </summary>
        public static bool Delete(string bmbh, string dzbbh, string xsbh, string[] xmbhs)
        {
            int ztdm = (int)TStar.Web.Globals.SystemSetting.Status.Deleted;
            string where = String.Format("Pkid IN ('{0}')", String.Join(",", xmbhs).Replace(",", "','"));
            int r = UpdateFields<Model.Xmgl.Yj_xm>("Ztdm", ztdm, where);

            foreach (string xmbh in xmbhs)
            {
                string path = String.Format("~/Uploads/Zmcl/{0}/{1}/{2}/{3}/", bmbh, dzbbh, xsbh, xmbh);
                string dir = TU.WebHelper.MapPath(path);
                TU.Globals.DeleteDirectory(dir);
            }

            // 写日志

            return r > 0;
        }
        /// <summary>
        /// 删除单个项目（无上传附件）
        /// </summary>
        public static bool Delete(string pkid)
        {
            string where = "Pkid='" + pkid + "'";
            int r = Delete<Model.Xmgl.Yj_xm>(pkid);

            // 写日志

            return r > 0;
        }
        /// <summary>
        /// 批量删除项目（无上传附件）
        /// </summary>
        public static bool Delete(string[] xmbhs)
        {
            int r = DeleteList<Model.Xmgl.Yj_xm>(xmbhs);

            // 写日志

            return r > 0;
        }
        /// <summary>
        /// 重置党校培训情况
        /// </summary>
        public static bool ResetDxpx(string xsbhs)
        {
            // 删除jc_xs表的党校培训情况
            string sql = string.Format("UPDATE jc_xs SET Dxkhztdm=NULL, Dxjyrq='' WHERE Pkid IN ({0})", xsbhs);
            int r = DAL.Globals.Execute(sql);
            return r > 0;
        }

        /// <summary>
        /// 清空导入的项目
        /// </summary>
        public static int ClearImport(string bmbh, string zbbh)
        {
            string xmWhere = String.Format("Bmbh = '{0}' AND Zbbh = '{1}' AND Jzrq='{2}'", bmbh, zbbh, BLL.Globals.SystemSetting.Dqxq);//TStar.Web.Globals.SystemSetting.Dqxn, 
            
            // 删除项目表
            return DeleteList<Model.Xmgl.Yj_xm>(xmWhere);
        }
        
        /// <summary>
        /// 批量提交项目
        /// </summary>
        public static bool Submit(params string[] xmbhs)
        {
            int ztdm = (int)TStar.Web.Globals.SystemSetting.Status.Submitted;
            string where = String.Format("Pkid IN ('{0}')", String.Join(",", xmbhs).Replace(",", "','"));
            int r = UpdateFields<Model.Xmgl.Yj_xm>("Ztdm", ztdm, where);

            // 写日志

            return r > 0;
        }

        /// <summary>
        /// 撤回单个已提交项目
        /// </summary>
        public static bool Revoke(string xmbh)
        {
            string where = "Pkid='" + xmbh + "'";
            string ztdm = ((int)TStar.Web.Globals.SystemSetting.Status.Revoked).ToString();
            int r = UpdateFields<Model.Xmgl.Yj_xm>("Ztdm", ztdm, where);

            // 写日志

            return r > 0;
        }
        
        /// <summary>
        /// 审核项目（需提供Pkid,Shrbh,Ztdm,Shyj）
        /// </summary>
        public static bool Audit(Model.Xmgl.Yj_xm xm)
        {
            string shsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string where = string.Format("Pkid='{0}'", xm.Pkid);
            int r = UpdateFields<Model.Xmgl.Yj_xm>(new string[]{"Shrbh", "Ztdm", "Shyj", "Shsj"}, new string[]{xm.Shrbh, xm.Ztdm.ToString(), xm.Shyj, shsj}, where);

            // 写日志

            return r > 0;
        }

        /// <summary>
        /// 批量审核项目（需提供Shrbh,Ztdm,Shyj）
        /// </summary>
        public static bool Audits(Model.Xmgl.Yj_xm xm, params string[] xmbhs)
        {
            string shsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string where = String.Format("Shrbh='{0}' AND Pkid IN ('{1}')", xm.Shrbh, String.Join(",", xmbhs).Replace(",", "','"));
            int r = UpdateFields<Model.Xmgl.Yj_xm>(new string[] { "Shrbh", "Ztdm", "Shyj", "Shsj" }, new string[] { xm.Shrbh, xm.Ztdm.ToString(), xm.Shyj, shsj }, where);

            // 写日志

            return r > 0;
        }

        /// <summary>
        /// 批量审核通过项目（需提供Shrbh,Ztdm,Shyj）
        /// </summary>
        //public static bool Audits(Model.Xmgl.Yj_xm xm, params string[] xmbhs)
        //{
        //    string shsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    string where = String.Format("Shrbh='{0}' AND Pkid IN ('{1}')", xm.Shrbh, String.Join(",", xmbhs).Replace(",", "','"));
        //    int r = UpdateFields<Model.Xmgl.Yj_xm>(new string[] { "Shrbh", "Ztdm", "Shyj", "Shsj" }, new string[] { xm.Shrbh, xm.Ztdm, "批量审核通过", shsj }, where);

        //    // 写日志

        //    return r > 0;
        //}

        private static int Insert(Model.Xmgl.Yj_xm xm)
        {
            int r = Insert<Model.Xmgl.Yj_xm>(xm);

            // 写日志

            return r;
        }

        private static int Update(Model.Xmgl.Yj_xm xm)
        {
            xm.Ztdm = (int)TStar.Web.Globals.SystemSetting.Status.InModify;
            int r = Update<Model.Xmgl.Yj_xm>(xm);

            // 写日志

            return r;
        }
    }
}
