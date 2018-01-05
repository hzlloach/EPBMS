using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using FUI = FineUI;
using TU = TStar.Utility;

namespace Web.Xmdr
{
    public partial class Ysdbdr : Xmdr
    {
        #region 自定义属性
        
        public string Bll
        {
            get { return TU.Globals.GetParaValue("bll", "YjDryj"); }
        }

        #endregion

        protected override void Page_Init(object sender, EventArgs e)
        {
            Columns10Widths = new int[] { 110, 100, 140, 80, 80, 80, 100, 120, 200, 120 };
            base.Page_Init(sender, e);
        }

        /// <summary>
        /// 依据传入参数TableName导入数据
        /// </summary>
        protected override int ImportData(DataRowView drv)
        {
            int cnt = 0;
            string[] columns = Columns;
            string xh = drv[columns[0]].ToString();
            string xm = drv[columns[1]].ToString();
            string bmbh = TStar.Web.Globals.Account.DeptPkid;
            string dzbbh = TStar.Web.Globals.Account.UserInfo.Dzbbh;
            Model.Jcgl.Jc_xs xs = BLL.Jcgl.Jc_xs.GetEntity(bmbh, dzbbh, xh, xm);
            if (string.IsNullOrEmpty(xs.Pkid)) throw new Exception("该学生信息不存在。");

            string fzdxqdrq = drv[columns[2]].ToString();
            fzdxqdrq = string.Format("{0}-{1}-{2}", fzdxqdrq.Substring(0, 4), fzdxqdrq.Substring(4, 2), fzdxqdrq.Substring(6, 2));
            DateTime dt;
            if (!DateTime.TryParse(fzdxqdrq, out dt)) throw new Exception(columns[2] + "不正确。");

            string zsjg = drv[columns[3]].ToString();
            string zsjgdm = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtDm_jgzt, null, "Mc", "Dm", zsjg, "");
            if (zsjgdm == "") throw new Exception(columns[3] + "不正确。");

            string dbjg = drv[columns[4]].ToString();
            string dbjgdm = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtDm_jgzt, null, "Mc", "Dm", dbjg, "");
            if (dbjgdm == "") throw new Exception(columns[4] + "不正确。");

            string dbrq = drv[columns[5]].ToString();
            dbrq = string.Format("{0}-{1}-{2}", dbrq.Substring(0, 4), dbrq.Substring(4, 2), dbrq.Substring(6, 2));
            if (!DateTime.TryParse(dbrq, out dt)) throw new Exception(columns[5] + "不正确。");

            string dbdd = drv[columns[6]].ToString();
            if (string.IsNullOrEmpty(dbdd)) throw new Exception(columns[6] + "不能为空。");

            string dbzcy = drv[columns[7]].ToString();
            if (string.IsNullOrEmpty(dbzcy)) throw new Exception(columns[7] + "不能为空。");

            string dbyj = drv[columns[8]].ToString();
            if (string.IsNullOrEmpty(dbyj)) throw new Exception(columns[8] + "不能为空。");
            else if (dbyj.Length > 200) throw new Exception(columns[8] + "限填200个字。");

            string zswtgyy = drv[columns[9]].ToString();
            if (zsjgdm == "0")
            {
                if (string.IsNullOrEmpty(zswtgyy)) throw new Exception(columns[9] + "不能为空。");
                else if (zswtgyy.Length > 200) throw new Exception(columns[9] + "限填200个字。");
            }

            string fzrbh = xs.Pkid;
            string xmrq = drv[columns[3]].ToString();
            if (BLL.Xmgl.Xm_ysdb.Exist(Pkid, new string[] { "Fzrbh", "Dbrq" }, new string[] { fzrbh, dbrq }))
                throw new Exception("该预审答辩结果已导入。");

            Model.Xmgl.Xm_ysdb m = new Model.Xmgl.Xm_ysdb();
            m.Fzrbh = fzrbh;
            m.Fzdxrq = fzdxqdrq;
            m.Zsjgdm = zsjgdm;
            m.Dbjgdm = dbjgdm;
            m.Dbrq = dbrq;
            m.Dbdd = dbdd;
            m.Dbzcy = dbzcy;
            m.Dbpjyj = dbyj;
            m.Zswtgyy = zswtgyy;
            if (BLL.Xmgl.Xm_ysdb.Save(m))
            {
                cnt = 1;
                drv["Del"] = 1;
            }

            return cnt;
        }

    }
}