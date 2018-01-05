using System;
using System.Data;
using System.Drawing;
using System.IO;
using TU = TStar.Utility;

namespace BLL.Jcgl
{
    /// <summary>
    /// 学生Jc_xs
    /// </summary>
    public class Jc_xs : BLL.Global.Base
    {
        /// <summary>
        /// 判断是否重复（新增时）
        /// </summary>
        public static bool IsRepeated(string field, string key)
        {
            return Exists<Model.Jcgl.Jc_xs>("", field, key);
        }
        /// <summary>
        /// 判断是否重复（修改时）
        /// </summary>
        public static bool IsRepeated(string pkid, string field, string key)
        {
            return Exists<Model.Jcgl.Jc_xs>(pkid, field, key);
        }

        /// <summary>
        /// 判断指定党支部内是否存在指定Xh和Xm的学生
        /// </summary>
        public static bool Exists(string bmbh, string dzbbh, string xh, string xm)
        {
            string[] fields = new string[]{"Bmbh", "Xh", "Xm"};
            string[] keys = new string[] { bmbh, xh, xm };
            if (!string.IsNullOrEmpty(dzbbh))
            {
                fields = new string[] { "Bmbh", "Dzbbh", "Xh", "Xm" };
                keys = new string[] { bmbh,dzbbh, xh, xm };
            }
            return Exists<Model.Jcgl.Jc_xs>("", fields, keys);
        }

        /// <summary>
        /// 根据学号和姓名查找
        /// </summary>
        public static Model.Jcgl.Jc_xs GetEntity(string xh, string xm)
        {
            return GetEntity<Model.Jcgl.Jc_xs>(new string[] { "Xh", "Xm" }, xh, xm);
        }
        public static Model.Jcgl.Jc_xs GetEntity(string bmbh, string dzbbh, string xh, string xm)
        {
            string[] fields = new string[] { "Bmbh", "Xh", "Xm" };
            string[] keys = new string[] { bmbh, xh, xm };
            if (!string.IsNullOrEmpty(dzbbh))
            {
                fields = new string[] { "Bmbh", "Dzbbh", "Xh", "Xm" };
                keys = new string[] { bmbh, dzbbh, xh, xm };
            }
            return GetEntity<Model.Jcgl.Jc_xs>(fields, keys);
        }

        public static void Insert(Model.Jcgl.Jc_xs m)
        {
            Model.Account_user au = new Model.Account_user();
            au.DeptID = TU.Globals.BindSystemCode(Globals.SystemCode.DtJd_bm, "Pkid", "Bmdm", m.Bmbh, "");
            au.UserID = m.Xh;// ("Xs" + DateTime.Now.Ticks.ToString() + Guid.NewGuid().ToString().Replace("-", "")).Substring(0, 32);
            au.UserName = m.Xm;
            au.Level = "00";
            try
            {
                Insert<Model.Account_user>(au);
                m.Pkid = au.Pkid;
                m.Sfzh = DateTime.Now.Ticks.ToString();
                Insert<Model.Jcgl.Jc_xs>(m);
            }
            catch (Exception err)
            {
                string s = err.Message;
            }
        }

        public static bool Update(Model.Jcgl.Jc_xs m, bool modUser)
        {
            try
            {
                if (modUser)
                {
                    Model.Account_user au = new Model.Account_user();
                    au.Pkid = m.Pkid;
                    au.UserID = m.Xh;
                    au.UserName = m.Xm;
                    au.Mobile = m.Sjhm;
                    UpdateFields<Model.Account_user>(au, "UserId", "UserName", "Mobile");
                }
                return Update<Model.Jcgl.Jc_xs>(m) > 0;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 生成单个学生的思想汇报PDF
        /// </summary>
        /// <param name="path">保存位置（Downloads\yyyyMMdd\当前用户编号）</param>
        /// <param name="dir">临时目录（HHmmssfff）</param>
        /// <param name="xsbh">学生编号</param>
        public static string CreatePDFSxhb(string path, string dir, string xsbh)
        {
            Model.Jcgl.V_jc_xs xs = GetEntity<Model.Jcgl.V_jc_xs>(xsbh);
            if (string.IsNullOrEmpty(xs.Pkid)) throw new Exception("该学生不存在 ！");

            // 获取已提交的思想汇报
            string where = string.Format("Fzrbh='{0}' AND Ztdm>=22", xsbh);
            DataTable dt = BLL.Xmgl.Xm_sxhb.GetAudittedList(where);
            if (dt.Rows.Count == 0) throw new Exception("该学生没有提交思想汇报 ！");

            return CreatePDFSxhb(path, dir, dt.DefaultView);
        }
        private static string CreatePDFSxhb(string path, string dir, DataView dv)
        {
            string watermark = path.Substring(0, path.LastIndexOf("Downloads")) + "Downloads\\watermark.png";
            path +=  "\\" + dir;
            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);

            string bmmc = dv[0]["Bmmc"].ToString();
            string dzbmc = dv[0]["Dzbmc"].ToString();
            string bjmc = dv[0]["Bjmc"].ToString();
            string xh = dv[0]["Xh"].ToString();
            string xm = dv[0]["Xm"].ToString();
            string fzjd = dv[0]["Fzjd"].ToString();
            string lxrxm = dv[0]["Lxrxm"].ToString();
            string filename = string.Format(@"{0}\{1}{2}.pdf", path, xh, xm);
            TU.PDFHelper Pdf = new TU.PDFHelper(filename);

            try
            {
                Pdf.SetAbstract("思想汇报", xm);
                Pdf.SetEncryption(null, null, TU.PDFHelper.Permissions.AllowCopy | TU.PDFHelper.Permissions.AllowPrinting);
                Pdf.SetPreferences(true, TU.PDFHelper.Permissions.HideToolbar);

                Pdf.SetWatermark(watermark);

                Pdf.Open(2, true, true, false, TU.PDFHelper.Alignment.Center, dzbmc, xm + "的思想汇报", fzjd, null, null, null);
                Pdf.SetFont(60, TU.PDFHelper.FontFamily.LiShu, true);
                Pdf.AddParagraph("\n思想汇报汇编", TU.PDFHelper.Alignment.Center, 0, 1.5, 0, 1);
                Pdf.SetFont(20, TU.PDFHelper.FontFamily.KaiTi, true);
                Pdf.AddParagraph("二级党组织：　" + bmmc, TU.PDFHelper.Alignment.Left, 4, 1.5, 2, 0.5);
                Pdf.AddParagraph("党支部名称：　" + dzbmc, TU.PDFHelper.Alignment.Left,4, 1.5, 0, 0.5);
                Pdf.AddParagraph("所在班级名：　" + bjmc, TU.PDFHelper.Alignment.Left, 4, 1.5, 0, 0.5);
                Pdf.AddParagraph("学　　　号：　" + xh, TU.PDFHelper.Alignment.Left, 4, 1.5, 0, 0.5);
                Pdf.AddParagraph("姓　　　名：　" + xm, TU.PDFHelper.Alignment.Left, 4, 1.5, 0, 0.5);
                Pdf.AddParagraph("现发展阶段：　" + fzjd, TU.PDFHelper.Alignment.Left, 4, 1.5, 0, 0.5);
                Pdf.AddParagraph("联系人姓名：　" + lxrxm, TU.PDFHelper.Alignment.Left, 4, 1.5, 0, 0.5);
                Pdf.AddParagraph("思想汇报数：　共 " + dv.Count + " 篇", TU.PDFHelper.Alignment.Left, 4, 1.5, 0, 0.5);

                foreach (DataRowView dr in dv)
                {
                    Pdf.NewPage();
                    Pdf.SetFont(16, TU.PDFHelper.FontFamily.SontTi, true);
                    Pdf.AddParagraph("思想汇报", TU.PDFHelper.Alignment.Center, 0, 1.5, 0.5, 0.5);

                    Pdf.SetFont(12);
                    Pdf.AddParagraph("敬爱的党组织：", TU.PDFHelper.Alignment.Left, 0, 1.5);
                    Pdf.AddParagraph(dr["Hbnr"].ToString(), TU.PDFHelper.Alignment.Left, 0, 1.5);
                    if (dr["Hbnr"].ToString().LastIndexOf("汇报人") < 0)
                    {
                        Pdf.AddParagraph("\n汇报人：" + dr["Xm"].ToString(), TU.PDFHelper.Alignment.Right, 0, 1.5);
                        Pdf.AddParagraph(DateTime.Parse(dr["Tjsj"].ToString()).ToString("yyyy年MM月dd日"), TU.PDFHelper.Alignment.Right, 0, 1.5);
                    }

                    Pdf.SetFont(13, Color.Red, TU.PDFHelper.FontFamily.KaiTi, true);
                    Pdf.AddParagraph("\n联系人评语：");
                    Pdf.SetFont(12, Color.Red, TU.PDFHelper.FontFamily.KaiTi, false);
                    if (string.IsNullOrEmpty(dr["Shrxm"].ToString())) Pdf.AddParagraph("无");
                    else
                    {
                        Pdf.AddParagraph(dr["Pyyj"].ToString());
                        Pdf.AddParagraph("评阅人：" + dr["Shrxm"].ToString(), TU.PDFHelper.Alignment.Right, 0, 1.5);
                        Pdf.AddParagraph(DateTime.Parse(dr["Pysj"].ToString()).ToString("yyyy年MM月dd日"), TU.PDFHelper.Alignment.Right, 0, 1.5);
                    }
                }
                return filename;// xs.Xh + ".pdf";// 
            }
            catch (Exception err)
            {
                throw new Exception("生成思想汇报文件出错：\n　　"　+ err.Message);
            }
            finally
            {
                Pdf.Close();
            }            
        }

        /// <summary>
        /// 批量生成学生的思想汇报PDF
        /// </summary>
        /// <param name="path">保存位置（Downloads\yyyyMMdd\当前用户编号）</param>
        /// <param name="dir">临时目录（BatHHmmssfff）</param>
        /// <param name="bmbh">部门编号</param>
        /// <param name="dzbbhs">党支部编号数组</param>
        public static string CreateBatPDFSxhb(string path, string dir, string bmbh, string[] dzbbhs)
        {
            string where = string.Format("Bmbh='{0}'", bmbh);
            if (dzbbhs != null) where += " AND Dzbbh IN ('" + string.Join("','", dzbbhs) + "')";
            string filename = dzbbhs.Length == 1 ? TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_dzb, "Pkid", "Dzbmc", dzbbhs[0], "党支部") : TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_bm, "Pkid", "Bmmc", bmbh, "部门");
            return CreateBatPDFSxhb(path, dir, bmbh, where, filename);
        }            
        /// <summary>
        /// 批量生成学生的思想汇报PDF
        /// </summary>
        /// <param name="path">保存位置（Downloads\yyyyMMdd\当前用户编号）</param>
        /// <param name="dir">临时目录（BatHHmmssfff）</param>
        /// <param name="bmbh">部门编号</param>
        /// <param name="where">学生筛选条件（dzbbh、lxrbh）</param>
        public static string CreateBatPDFSxhb(string path, string dir, string bmbh, string where, string zipFilename)
        {
            try
            {
                // 获取已提交的思想汇报
                DataTable dt = BLL.Xmgl.Xm_sxhb.GetAudittedList(where);
                DataView dv = dt.DefaultView;
                dir = "Bat" + dir;

                string dzbmc = "";
                for (int i = 0; i < dt.Rows.Count; ) //in dzbbhs)
                {
                    //string dzbbh = 
                    //string dzbmc = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_dzb, "Pkid", "Dzbmc", dzbbh, "未知");
                    string xsbh = dt.Rows[i]["Fzrbh"].ToString();
                    dzbmc = dt.Rows[i]["Dzbmc"].ToString();

                    dv.RowFilter = "Fzrbh='" + xsbh + "'";
                    CreatePDFSxhb(path + "\\" + dir, dzbmc, dv);

                    i += dv.Count;
                }

                // 打包文件夹
                string filename = path + "\\" + dir + "\\" + zipFilename + ".zip";
                if (TU.ZipHelper.CompressFile(path + "\\" + dir, filename)) return filename;
                else throw new Exception("压缩文件夹时出现未知错误 ！");
            }
            catch (Exception err)
            {
                throw new Exception("批量生成思想汇报文件出错：\n　　" + err.Message);
            } 
        }

        static Random R = new Random();
        public static void Moni()
        {
            DataView dvBj = GetList<Model.Jcgl.Jd_bj>(null, "Pkid").DefaultView;
            int cntDzb = dvBj.Count;
            int count = 100;

            foreach (DataRowView drv in dvBj)
            {
                for (int i = 0; i < count; i++)
                {
                    Model.Jcgl.Jc_xs m = new Model.Jcgl.Jc_xs();
                    m.Bmbh = drv["Bmbh"].ToString();
                    m.Bjbh = drv["Pkid"].ToString();
                    m.Xh = m.Xm = "2017" + m.Bjbh + (i+1).ToString("D3");

                    string dzbbh = drv["Dzbbh"].ToString();
                    m.Rdlxrbh1 = GetEntity<Model.Jcgl.Jc_lxr>("Dzbbh", dzbbh).Pkid;

                    m.Sqrdrq = "20160101";
                    m.Jjfzrq = "20170101";
                    Insert(m);
                }
            }
        }
    }
}
