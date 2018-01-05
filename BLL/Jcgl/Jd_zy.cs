using System;

namespace BLL.Jcgl
{
    /// <summary>
    /// 党支部Jd_zy
    /// </summary>
    public class Jd_zy : BLL.Global.Base
    {
        public static Model.Jcgl.Jd_zy GetEntity(string bmbh, string dzbbh, string zymc)
        {
            return GetEntity<Model.Jcgl.Jd_zy>(new string[] { "Bmbh", "Dzbbh", "Zymc" }, bmbh, dzbbh, zymc);
        }

        public static bool Save(Model.Jcgl.Jd_zy m)
        {
            int r = 0;
            try
            {
                if (String.IsNullOrEmpty(m.Pkid))
                {
                    m.Pkid = ("Zy" + Guid.NewGuid().ToString().Replace("-", "").ToUpper()).Substring(0, 32);
                    r = BLL.Dmgl.Insert<Model.Jcgl.Jd_zy>(m);
                }
                else
                {
                    r = BLL.Dmgl.Update<Model.Jcgl.Jd_zy>(m);
                }
                return r > 0;
            }
            catch (Exception err)
            {
                if (err.Message.IndexOf("唯一") > 0) throw new Exception("专业名称已存在 ！");
                else throw err;
            }
        }
    }
}
