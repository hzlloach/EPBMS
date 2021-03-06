﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TU = TStar.Utility;

namespace BLL.Lcgl
{
    public class Lc_dymd : BLL.Global.Base
    {
        /// <summary>
        /// 判断当学期是否已导入过
        /// </summary>
        public static bool Exist(string xh)//, string dbrq)
        {
            return Exists<Model.Lcgl.Lc_dymd>(null, new string[] { "Xh"/*, "Dbrq"*/ }, new string[] { xh });
        }

        public static bool Exist(string pkid, string[] fields, string[] keys)
        {
            return Exists<Model.Lcgl.Lc_dymd>(pkid, fields, keys);
        }
        
        /// <summary>
        /// 保存名单
        /// </summary>
        public static bool Save(Model.Lcgl.Lc_dymd xm)
        {
            try
            {
                int r = 0;
                if (String.IsNullOrEmpty(xm.Pkid)) // 新增
                    r = Insert(xm);
                else // 保存
                    r = Update(xm);

                return r > 0;
            }
            catch (Exception err)
            {
                if (err.Message.IndexOf("Xh") > 0) throw new Exception("该学号重复。");
                if (err.Message.IndexOf("Sfzh") > 0) throw new Exception("该身份证号重复。");
                if (err.Message.IndexOf("Lxdh") > 0) throw new Exception("该手机号码重复。");
                if (err.Message.IndexOf("dymd") > 0) throw new Exception("该学生名单已存在。");
                throw err;
            }
        }

        /// <summary>
        /// 删除单个名单
        /// </summary>
        public static bool Delete(string pkid)
        {
            string where = "Pkid='" + pkid + "'";
            int r = Delete<Model.Lcgl.Lc_dymd>(pkid);

            // 写日志

            return r > 0;
        }
        /// <summary>
        /// 批量删除名单
        /// </summary>
        public static bool Delete(string[] xmbhs)
        {
            int r = DeleteList<Model.Lcgl.Lc_dymd>(xmbhs);

            // 写日志

            return r > 0;
        }

        /// <summary>
        /// 清空导入的名单
        /// </summary>
        public static int ClearImport(string bmbh, string dzbbh)
        {
            string xmWhere = String.Format("Xq='{1}' AND Ztdm='{2}' AND Bmbh = '{0}'", bmbh, BLL.Globals.SystemSetting.Dqxq, (int)TStar.Web.Globals.SystemSetting.Status.Draft);
            if(!string.IsNullOrEmpty(dzbbh)) xmWhere += String.Format(" AND Dzbbh = '{0}'", dzbbh);
            
            // 删除名单表
            return DeleteList<Model.Lcgl.Lc_dymd>(xmWhere);
        }

        /// <summary>
        /// 提交导入的名单
        /// </summary>
        public static int Submit(string bmbh, string dzbbh)
        {
            string xmWhere = String.Format("Xq='{1}' AND Ztdm='{2}' AND Bmbh = '{0}'", bmbh, BLL.Globals.SystemSetting.Dqxq, (int)TStar.Web.Globals.SystemSetting.Status.Draft);
            if (!string.IsNullOrEmpty(dzbbh)) xmWhere += String.Format(" AND Dzbbh = '{0}'", dzbbh);

            // 提交名单表
            string czsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int ztdm = (int)TStar.Web.Globals.SystemSetting.Status.Submitted;
            int r = UpdateFields<Model.Lcgl.Lc_dymd>(new string[] { "Ztdm", "Drsj" }, new object[] { ztdm, czsj }, xmWhere);
            if (r == 0) { throw new Exception("没有需要提交的名单 ！"); }

            // 插入用户表
            string sql = string.Format("INSERT INTO Account_user SELECT m.Pkid, '{0}', m.Xh, m.Xm, '{1}', m.Lxdh, m.Pkid, '00', '01', '1', 'System', '', '{5}', '', '127.0.0.1', 0, '000000', 0, '' FROM lc_dymd m WHERE m.Xq='{2}' AND m.Bmbh = '{3}' AND m.Ztdm='{4}' AND m.Drsj='{5}'", TStar.Web.Globals.Account.UserInfo.DeptID, BLL.Globals.SystemSetting.DefaultEncPwd, BLL.Globals.SystemSetting.Dqxq, TStar.Web.Globals.Account.DeptPkid, ztdm, czsj);
            int r1 = DAL.Globals.Execute(sql);
            // 插入学生表
            sql = string.Format("INSERT INTO jc_xs SELECT m.Pkid, m.Bmbh, m.Dzbbh, m.Zybh, m.Bjbh, m.Xh, m.Xm, m.Sfzh, m.Xbdm, CASE WHEN LEN(m.Zzrq)>0 THEN '6' ELSE '5' END, SUBSTRING(m.Sfzh,7,8), m.Jg, m.Mz, m.Lxdh, m.QQ, m.Zw, m.Jtdz, m.Sqrdrq, m.Jjfzrq, m.Dxkhztdm, m.Dxjyrq, '', '', '', m.Fzdxrq, m.Rdrq, m.Zzrq, m.Zysbh, m.Lxrbh, NULL, Bz=(CASE WHEN LEN(m.Zzrq)>0 THEN '原有正式党员' ELSE '原有预备党员' END) FROM lc_dymd m WHERE m.Xq='{0}' AND m.Bmbh = '{1}' AND m.Ztdm='{2}' AND m.Drsj='{3}'", BLL.Globals.SystemSetting.Dqxq, TStar.Web.Globals.Account.DeptPkid, ztdm, czsj);
            r1 = DAL.Globals.Execute(sql); 
            // 插入提醒表
            sql = string.Format("INSERT INTO xm_sxhbtx SELECT m.Pkid, '2017-10-01', '2017-10-01', '2017-10-01', '2017-10-01' FROM Lc_dymd m WHERE m.Xq='{0}' AND m.Bmbh = '{1}' AND m.Ztdm='{2}' AND m.Drsj='{3}'", BLL.Globals.SystemSetting.Dqxq, TStar.Web.Globals.Account.DeptPkid, ztdm, czsj);
            r1 = DAL.Globals.Execute(sql); 
            return r;
        }

        private static int Insert(Model.Lcgl.Lc_dymd xm)
        {
            xm.Xq = BLL.Globals.SystemSetting.Dqxq;
            xm.Ztdm = (int)TStar.Web.Globals.SystemSetting.Status.Draft;
            int r = Insert<Model.Lcgl.Lc_dymd>(xm);

            // 写日志

            return r;
        }

        private static int Update(Model.Lcgl.Lc_dymd xm)
        {
            //xm.Ztdm = (int)TStar.Web.Globals.SystemSetting.Status.Submitted;
            int r = Update<Model.Lcgl.Lc_dymd>(xm);

            // 写日志

            return r;
        }
    }
}
