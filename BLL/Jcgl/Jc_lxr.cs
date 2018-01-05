using System;
using System.Data;
using TU = TStar.Utility;

namespace BLL.Jcgl
{            
    /// <summary>
    /// 联系人Jc_lxr
    /// </summary>
    public class Jc_lxr : BLL.Global.Base
    {
        public static Model.Jcgl.V_jc_lxr GetEntity(string bmbh, string dzbbh, string gh, string xm)
        {
            return GetEntity<Model.Jcgl.V_jc_lxr>(new string[] { "Bmbh", "Dzbbh", "Gh", "Xm" }, bmbh, dzbbh, gh, xm);
        }

        public static bool Save(Model.Jcgl.Jc_lxr m)
        {
            if (string.IsNullOrEmpty(m.Pkid)) return Insert(m);
            else return Update(m);
        }

        public static int Delete(string pkid)
        {
            try
            {
                int r = Delete<Model.Jcgl.Jc_lxr>(pkid);
                BLL.Account.DelLxr(pkid);
                BLL.Globals.SystemCode.RefreshDtJc_lxr();
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
                int r = DeleteList<Model.Jcgl.Jc_lxr>(pkids);
                BLL.Account.DelLxrList(pkids);
                BLL.Globals.SystemCode.RefreshDtJc_lxr();
                return r;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public static bool Insert(Model.Jcgl.Jc_lxr m)
        {            
            try
            {
                if (m.Lbdm == "0") // 手机号码里存放初始登录密码
                {
                    BLL.Account.AddDzb(m.Pkid, m.Bmbh, m.Dzbbh, m.Xm, m.Gh, m.Sjhm);
                }
                else
                    BLL.Account.AddLxr(m.Pkid, m.Bmbh, m.Dzbbh, m.Gh, m.Xm, m.Sjhm, m.Issj);
                int r = Insert<Model.Jcgl.Jc_lxr>(m);
                BLL.Globals.SystemCode.RefreshDtJc_lxr();
                return r > 0;
            }
            catch(Exception err)
            {
                throw err;
            }
        }

        private static bool Update(Model.Jcgl.Jc_lxr m)
        {
            try
            {
                if (m.Lbdm == "0")
                    BLL.Account.ModDzb(m.Pkid, m.Gh, m.Sjhm);
                else
                    BLL.Account.ModLxr(m.Pkid, m.Dzbbh, m.Gh, m.Xm, m.Sjhm, m.Issj);
                int r = Update<Model.Jcgl.Jc_lxr>(m);
                BLL.Globals.SystemCode.RefreshDtJc_lxr();
                return r > 0;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        static Random R = new Random();
        public static void Moni(int count)
        {
            //DataView dvDzb = GetList<Model.Jcgl.Jd_dzb>(null, "Dzbmc").DefaultView;
            //int cntDzb = dvDzb.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    int r = R.Next(cntDzb);
            //    Model.Jcgl.Jc_lxr lxr = new Model.Jcgl.Jc_lxr();
            //    lxr.Bmbh = dvDzb[r]["Bmbh"].ToString();
            //    lxr.Bjbh = dvDzb[r]["Pkid"].ToString();
            //    Insert(lxr);
            //}
        }
    }
}
