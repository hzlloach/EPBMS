using System;
using System.Data;
using TU = TStar.Utility;

namespace BLL.Jcgl
{
    /// <summary>
    /// 项目等级Jd_xmdj
    /// </summary>
    public class Jd_xmdj : BLL.Global.Base
    {
        public static bool Save(Model.Dmgl.Jd_xmdj m)
        {
            int r = 0;
            try
            {
                if (String.IsNullOrEmpty(m.Pkid))
                {
                    m.Pkid = ("DJ" + Guid.NewGuid().ToString().Replace("-", "").ToUpper()).Substring(0, 32);
                    r = BLL.Dmgl.Insert<Model.Dmgl.Jd_xmdj>(m);
                }
                else
                {
                    r = BLL.Dmgl.Update<Model.Dmgl.Jd_xmdj>(m);
                }
                if (r > 0) BLL.Globals.SystemCode.RefreshDtJd_xmdj();
                return r > 0;
            }
            catch (Exception err)
            {
                if (err.Message.IndexOf("唯一") > 0) throw new Exception("等级名称已存在 ！");
                else throw err;
            }
        }

        public static bool Delete(string pkid)
        {
            int r = Delete<Model.Dmgl.Jd_xmdj>(pkid);
            BLL.Globals.SystemCode.RefreshDtJd_xmdj();
            return r > 0;
        }

        public static bool DeleteList(string[] ids)
        {
            int r = DeleteList<Model.Dmgl.Jd_xmdj>(ids);
            BLL.Globals.SystemCode.RefreshDtJd_xmdj();
            return r > 0;
        }
    }
}


