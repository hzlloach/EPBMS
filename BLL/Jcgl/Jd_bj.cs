using System;
using System.Data;
using TU = TStar.Utility;

namespace BLL.Jcgl
{
    /// <summary>
    /// 班级Jd_bj
    /// </summary>
    public class Jd_bj : BLL.Global.Base
    {
        public static Model.Jcgl.Jd_bj GetEntity(string bmbh, string zybh, string bjmc)
        {
            return GetEntity<Model.Jcgl.Jd_bj>(new string[] { "Bmbh", "Zybh", "Bjmc" }, bmbh, zybh, bjmc);
        }

        public static bool Save(Model.Jcgl.Jd_bj m)
        {
            int r = 0;
            try
            {
                if (String.IsNullOrEmpty(m.Pkid))
                {
                    m.Pkid = ("Bj" + Guid.NewGuid().ToString().Replace("-", "").ToUpper()).Substring(0, 32);
                    r = BLL.Dmgl.Insert<Model.Jcgl.Jd_bj>(m);
                }
                else
                {
                    r = BLL.Dmgl.Update<Model.Jcgl.Jd_bj>(m);
                }
                return r > 0;
            }
            catch (Exception err)
            {
                if (err.Message.IndexOf("唯一") > 0) throw new Exception("班级名称已存在 ！");
                else throw err;
            }
        }

        public static void Moni()
        {
            DataView dvDzb = GetList<Model.Jcgl.Jd_dzb>(null, "Dzbmc").DefaultView;
            int cntDzb = dvDzb.Count;
            int count = 6;

            foreach (DataRowView drv in dvDzb)
            {
                for (int i = 0; i < count; i++)
                {
                    Model.Jcgl.Jd_bj bj = new Model.Jcgl.Jd_bj();
                    bj.Bmbh = drv["Bmbh"].ToString();
                    bj.Dzbbh = drv["Pkid"].ToString();
                    bj.Bjmc = drv["Dzbmc"].ToString() + (i + 1);
                    Insert<Model.Jcgl.Jd_bj>(bj);
                }
            }
        }
    }
}
