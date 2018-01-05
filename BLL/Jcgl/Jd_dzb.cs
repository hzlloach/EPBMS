using System;

namespace BLL.Jcgl
{
    /// <summary>
    /// 党支部Jd_dzb
    /// </summary>
    public class Jd_dzb : BLL.Global.Base
    {
        public static bool Exists(string pkid, string bmbh, string dzbdm, string dzbmc)
        {
            return Exists<Model.Jcgl.Jd_dzb>(pkid, new string[] { "Bmbh", "Dzbdm" }, new string[] { bmbh, dzbdm }) || Exists<Model.Jcgl.Jd_dzb>(pkid, new string[] { "Bmbh", "Dzbmc" }, new string[] { bmbh, dzbmc });
        }

        public static Model.Jcgl.Jd_dzb GetEntity(string bmbh, string dzbmc)
        {
            return GetEntity<Model.Jcgl.Jd_dzb>(new string[] { "Bmbh", "Dzbmc" }, bmbh, dzbmc);
        }

        public static bool Save(Model.Jcgl.Jd_dzb m)
        {
            int r = 0;
            try
            {
                if (String.IsNullOrEmpty(m.Pkid))
                {
                    m.Pkid = ("ZB" + Guid.NewGuid().ToString().Replace("-", "").ToUpper()).Substring(0, 32);
                    r = BLL.Dmgl.Insert<Model.Jcgl.Jd_dzb>(m);

                    // 添加联系人及帐号
                    Model.Jcgl.Jc_lxr l = new Model.Jcgl.Jc_lxr();
                    l.Pkid = m.Pkid;
                    l.Bmbh = m.Bmbh;
                    l.Dzbbh = m.Pkid;
                    l.Gh = m.UserID;
                    l.Xm = m.Dzbmc;
                    l.Sjhm = m.Password;
                    l.Lbdm = "0";
                    Jc_lxr.Insert(l);
                }
                else
                {
                    r = BLL.Dmgl.Update<Model.Jcgl.Jd_dzb>(m);
                }
                return r > 0;
            }
            catch (Exception err)
            {
                if (err.Message.IndexOf("唯一") > 0) throw new Exception("党支部代码或名称已存在 ！");
                else throw err;
            }
        }

        public static int Delete(string pkid)
        {
            try
            {
                BLL.Jcgl.Jc_lxr.Delete(pkid);
                int r = Delete<Model.Jcgl.Jd_dzb>(pkid);
                BLL.Globals.SystemCode.RefreshDtJd_dzb();
                return r;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public static int DeleteList(string[] pkids)
        {
            try
            {
                BLL.Jcgl.Jc_lxr.DeleteList(pkids);
                int r = DeleteList<Model.Jcgl.Jd_dzb>(pkids);
                BLL.Globals.SystemCode.RefreshDtJd_dzb();
                return r;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
