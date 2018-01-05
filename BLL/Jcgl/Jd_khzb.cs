using System;
using System.Data;
using TU = TStar.Utility;

namespace BLL.Jcgl
{
    /// <summary>
    /// 项目指标Jd_khzb
    /// </summary>
    public class Jd_khzb : BLL.Global.Base
    {
        public static Model.Dmgl.Jd_khzb GetEntity(string bmbh, string zybh, string bjmc)
        {
            return GetEntity<Model.Dmgl.Jd_khzb>(new string[] { "Bmbh", "Zybh", "Bjmc" }, bmbh, zybh, bjmc);
        }

        public static bool Save(Model.Dmgl.Jd_khzb m)
        {
            int r = 0;
            bool changed = m.Zbdm.IndexOf("GetKhzbdm") > -1;
            try
            {
                if (String.IsNullOrEmpty(m.Pkid))
                {
                    m.Pkid = ("ZB" + Guid.NewGuid().ToString().Replace("-", "").ToUpper()).Substring(0, 32);
                    r = changed ? BLL.Dmgl.Insert<Model.Dmgl.Jd_khzb>(m, "Zbdm", m.Zbdm) : BLL.Dmgl.Insert<Model.Dmgl.Jd_khzb>(m);
                }
                else
                {
                    r = changed ? BLL.Dmgl.Update<Model.Dmgl.Jd_khzb>(m, "Zbdm", m.Zbdm) : BLL.Dmgl.Update<Model.Dmgl.Jd_khzb>(m);
                }
                if (r > 0) BLL.Globals.SystemCode.RefreshDtJd_khzb();
                return r > 0;
            }
            catch (Exception err)
            {
                if (err.Message.IndexOf("唯一") > 0) throw new Exception("指标名称已存在 ！");
                else throw err;
            }
        }

        public static bool Delete(string pkid)
        {
            int r = Delete<Model.Dmgl.Jd_khzb>(pkid);
            BLL.Globals.SystemCode.RefreshDtJd_khzb();
            return r > 0;
        }

        public static bool DeleteList(string[] ids)
        {
            int r = DeleteList<Model.Dmgl.Jd_khzb>(ids);
            BLL.Globals.SystemCode.RefreshDtJd_khzb();
            return r > 0;
        }
    }
}


