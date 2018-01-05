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
    public partial class mddr : TStar.Web.BasePage
    {
        #region 自定义属性

        private string[] Jjfz = { "党支部", "专业", "班级", "学号", "姓名", "性别", "身份证号", "籍贯", "民族", "职务", "手机", "入党申请书提交日期", "积极分子确定日期", "联系人工号", "联系人姓名", "家庭详细住址" };
        private string[] Nfzmd = { "学号", "姓名", "发展对象确定日期", "政审结果", "答辩结果", "答辩日期", "答辩地点", "答辩组成员", "答辩评价意见", "政审未通过原因" };
        private string[] Fzmd = { "学号", "姓名", "表决结果", "支部大会日期", "入党志愿书编号", "备注" };
        protected int[] Columns6Widths = { 110, 80, 75, 120, 120, 250 };
        protected int[] Columns8Widths = { 110, 80, 75, 120, 120, 130, 250, 200 };
        protected int[] Columns10Widths = new int[] { 110, 80, 125, 75, 75, 80, 100, 120, 160, 120 };
        protected int[] Columns16Widths = new int[] { 120, 120, 100, 110, 80, 50, 160, 80, 60, 100, 100, 140, 125, 90, 90, 200 };

        protected string BzName = "错误信息";

        /// <summary>
        /// 模版名称
        /// </summary>
        protected string TemplateName
        {
            get
            {
                string name = hfdTemplateName.Text;
                if (String.IsNullOrEmpty(name))
                {
                    try
                    {
                        name = TU.Globals.GetParaValue("p", "").Replace(" ", "+");
                        name = TU.Globals.TripleDESDecrypt(name).Replace(BLL.Globals.SystemSetting.EncryptCode, "");
                        hfdTemplateName.Text = name;
                    }
                    catch
                    {
                        name = "Err";
                    }
                }
                return name;
            }
        }
        /// <summary>
        /// 表名称
        /// </summary>
        protected string TableName
        {
            get
            {
                switch (TemplateName)
                {
                    default:
                    case "积极分子": return "lc_jjfzmd";
                    case "拟发展对象": return "lc_nfzmd";
                    case "发展名单": return "lc_fzmd";
                    case "转正名单": return "lc_zzmd";
                }
            }
        }

        protected string[] Columns
        {
            get
            {
                object o = TU.Globals.GetObject("$Tmp$Columns");
                if (o == null) return null;
                else return (string[])o;
            }
            set
            {
                TU.Globals.SetObject(value, "$Tmp$Columns", false);
            }
        }

        protected int[] ColumnWidths
        {
            get
            {
                object o = TU.Globals.GetObject("$Tmp$ColumnWidths");
                if (o == null) return null;
                else return (int[])o;
            }
            set
            {
                TU.Globals.SetObject(value, "$Tmp$ColumnWidths", false);
            }
        }

        protected string Bll
        {
            get { return TU.Globals.GetParaValue("bll", "Jjfz"); }
        }
        protected int PageIndex
        {
            get { return this.Grid1.PageIndex + 1; }
        }

        protected DataView ImportDataView
        {
            get { return TU.Globals.GetObject("$Tmp$" + TableName) as DataView; }
            set
            {
                if (value == null)
                    TU.Globals.RemoveObject("$Tmp$" + TableName);
                else
                {
                    DataTable dt = value.Table;
                    DataColumn dc = new DataColumn(BzName, typeof(System.String));
                    dc.DefaultValue = "";
                    dt.Columns.Add(dc);
                    dc = new DataColumn("Del", typeof(System.Int16));
                    dc.DefaultValue = 0;
                    dt.Columns.Add(dc);
                    TU.Globals.SetObject(dt.DefaultView, "$Tmp$" + TableName, false);
                }
            }
        }

        #endregion

        #region 自定义方法

        protected void BindData()
        {
            ImportDataView = null;
            if (TemplateName == "Err")
            {
                FUI.Alert.Show/*Alert.ShowInParent*/("页面参数不正确 ！", "打开失败", FUI.MessageBoxIcon.Error);
                FUI.PageContext.RegisterStartupScript(FUI.ActiveWindow.GetHideReference());
                return;
            }

            btnTemplate.OnClientClick = "javascript:downloadFile('../Template/" + TemplateName + "模版.xls')";

            // 关闭按钮的客户端脚本
            btnClose.OnClientClick = FUI.ActiveWindow.GetHideReference();
        }
        protected void ShowUI()
        {
        }

        /// <summary>
        /// 读取当前模版文件内的表格列信息（字段及宽度）
        /// </summary>
        public void GetGridSetting()
        {
            string filename = TemplateName + "模版.xls";
            string path = Server.MapPath("~/Template/");
            string fullfile = path + filename;

            Columns = GetExcelColumns(fullfile);
            switch (TemplateName)
            {
                default:
                //case 3: ColumnWidths = Columns3Widths; break;
                //case 4: ColumnWidths = Columns4Widths; break;
                //case 5: ColumnWidths = Columns5Widths; break;
                //case 10: ColumnWidths = Columns10Widths; break;

                case "积极分子":
                    ColumnWidths = Columns16Widths; break;
                case "拟发展对象":
                    ColumnWidths = Columns10Widths; break;
                case "发展对象":
                    ColumnWidths = Columns6Widths; break;
                case "转正名单":
                    ColumnWidths = Columns8Widths; break;
            }
        }

        /// <summary>
        /// 读取Excel文件的列头
        /// </summary>
        /// <returns></returns>
        public string[] GetExcelColumns(string filename)
        {
            System.IO.Stream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            DataTable table = new DataTable("ExcelData");

            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            HSSFWorkbook workbook = new HSSFWorkbook(fs);

            //获取excel的第一个sheet
            ISheet sheet = workbook.GetSheetAt(0);

            //获取sheet的首行
            IRow headerRow = sheet.GetRow(0);

            //第一行最后一个方格的编号 即总的列数
            int cellCount = headerRow.LastCellNum;
            string[] columns = new string[cellCount - headerRow.FirstCellNum];
            for (int i = headerRow.FirstCellNum, k = 0; i < cellCount; i++, k++)
            {
                //DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                //table.Columns.Add(column);
                columns[k] = headerRow.GetCell(i).StringCellValue;
            }

            workbook = null;
            sheet = null;

            return columns;// table;
        }

        /// <summary>
        /// 读取Excel文件的数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetExcelData(string filename)
        {
            System.IO.Stream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            DataTable table = new DataTable("ExcelData");

            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            HSSFWorkbook workbook = new HSSFWorkbook(fs);

            //获取excel的第一个sheet
            ISheet sheet = workbook.GetSheetAt(0);

            //获取sheet的首行
            IRow headerRow = sheet.GetRow(0);

            //第一行最后一个方格的编号 即总的列数
            int cellCount = headerRow.LastCellNum;
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum;
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                table.Rows.Add(dataRow);
            }

            workbook = null;
            sheet = null;

            return table;
        }

        /// <summary>
        /// 初始化Grid（自动添加列，自动列宽）
        /// </summary>
        public void InitializeGrid(FUI.Grid grid, string[] columns, int[] widths)
        {
            List<string> list = new List<string>();
            int cnt = grid.Columns.Count;

            // 动态添加列
            for (int i = 0; i < columns.Length; i++)
            {
                string title = columns[i];
                FUI.BoundField bf = new FUI.BoundField();
                bf.DataField = title;
                bf.HeaderText = title;
                bf.DataFormatString = "{0}";
                bf.Width = new Unit(widths[i]);
                Grid1.Columns.Add(bf);
                list.Add(columns[i]);
            }

            // 添加导入备注列
            FUI.BoundField bz = new FUI.BoundField();
            bz.DataField = BzName;
            bz.HeaderText = BzName;
            bz.DataFormatString = "{0}";
            bz.MinWidth = new Unit(300);
            bz.ExpandUnusedSpace = true;
            bz.DataToolTipField = BzName;
            bz.DataToolTipFormatString = "{0}";
            Grid1.Columns.Add(bz);
            //Ext.TemplateField bz = new Ext.TemplateField();
            //bz.ItemTemplate = new Ext.GridRowControl(..TemplateField . = n.DataField = "导入备注";
            //bz.HeaderText = "导入备注"; //columnTitles[i];
            //bz.DataFormatString = "<span style='color:red'>{0}</span>";
            //bz.Width = new Unit(300);
            //Grid1.Columns.Add(bz);                

            Grid1.DataKeyNames = list.ToArray();
        }

        /// <summary>
        /// 依据传入参数TableName初始化Grid
        /// </summary>
        protected void InitializeGrid()
        {
            this.InitializeGrid(this.Grid1, Columns, ColumnWidths);
        }

        /// <summary>
        /// 依据传入参数TableName检查文件字段
        /// </summary>
        protected bool CheckData(DataTable dt)
        {
            string errMsg = "";
            string[] titles = Columns;

            foreach (string title in titles)
            {
                if (dt.Columns.IndexOf(title) == -1)
                    errMsg += String.Format("、【{0}】", title);
            }
            if (errMsg.Length > 0)
            {
                FUI.Alert.Show("数据文件中缺少" + errMsg.Substring(1) + "字段 ！", "上传提示", FUI.MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 依据传入参数TableName导入数据
        /// </summary>
        protected virtual int ImportData(DataRowView drv, string drsj)
        {
            //double fjlsl = 0;
            //int cnt = 0, jlsl = 1;
            //string[] columns = Columns;
            //string xn = TStar.Web.Globals.SystemSetting.Dqxn;
            //string xh = drv[columns[0]].ToString();
            //string xm = drv[columns[1]].ToString();
            //string fzrbh = BLL.Jcgl.JcJs.GetPkid(xh);
            //string bmbh = TStar.Web.Globals.Account.DeptPkid;
            //string djbh = null, ztbh = null, xmrq, jzrq, bz = "";
            //string zbbh = Djzbmc.Substring(0, 32);
            //string xmmc = Djzbmc.Substring(32);
            //if (!BLL.Jcgl.Jc_xs.Exists(bmbh, xh, xm)) throw new Exception("该教师信息不存在。");

            switch (TemplateName)
            {
                case "积极分子":
                    return ImportJjfzmd(drv, drsj);
                case "拟发展对象":
                    return ImportNfzmd(drv, drsj);
                case "发展对象":
                    return ImportFzmd(drv, drsj);
                case "转正名单":
                    return ImportZzmd(drv, drsj);
                //        fjlsl = TU.Globals.Parse2Double(drv[columns[2]].ToString(), -1);
                //        if (fjlsl == -1) throw new Exception(columns[2] + "不正确。");
                //        jlsl = (int)Math.Round(fjlsl);
                //        break;
                //    case 4:
                //        xmmc = drv[columns[3]].ToString();
                //        if (columns[2].IndexOf("等级") > -1)
                //        {
                //            string filter = "Zbbh='" + TU.Globals.GetParaValue("zb", "") + "'";
                //            string djmc = drv[columns[2]].ToString();
                //            djbh = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_xmdjLocal, filter, "Djmc", "Pkid", djmc, "");
                //            if (djbh == "") throw new Exception(columns[2] + "不正确。");
                //        }
                //        else
                //        {
                //            jlsl = TU.Globals.Parse2Int(drv[columns[2]].ToString(), -1);
                //            if (jlsl == -1) throw new Exception(columns[2] + "不正确。");
                //        }
                //        break;
                //    case 5: // 教学评价
                //        fjlsl = TU.Globals.Parse2Double(drv[columns[2]].ToString(), -1);
                //        if (fjlsl == -1) throw new Exception(columns[2] + "不正确。");
                //        jlsl = (int)Math.Round(fjlsl);
                //        string xs = drv[columns[3]].ToString();
                //        string zj = drv[columns[4]].ToString();
                //        bz = String.Format("学生评教分：{0}，专家评教分：{1}", xs, String.IsNullOrEmpty(zj) ? "-" : zj);
                //        break;
            }
            //if (BLL.Yjgl.Yj_xm.Exists(xn, bmbh, zbbh, fzrbh, xmmc)) throw new Exception("该教学业绩项目已导入。");

            //Model.Yjgl.Yj_xm yj = new Model.Yjgl.Yj_xm();
            //DataTable DtXmfp = BLL.Yjgl.Yj_xmfp.CreateDtXmfp();
            //if (jlsl != 0) //jlsl > 0 || jlsl < 0 && TableName == "待定扣分") // 数量为0的不导入
            //{
            //    xmrq = "本学年";
            //    jzrq = TStar.Globals.SystemSettings.DxnJzrq;
            //    yj.Xn = xn;
            //    yj.Bmbh = bmbh;
            //    yj.Zbbh = zbbh;
            //    yj.Djbh = djbh;
            //    yj.Ztbh = ztbh;
            //    yj.Fzrbh = fzrbh;
            //    yj.Fzrxm = xm;
            //    yj.Xmmc = xmmc;
            //    yj.Xmrq = xmrq;
            //    yj.Jzrq = jzrq;
            //    yj.Jlsl = jlsl;
            //    yj.Bz = bz;
            //    yj.Shrbh = TStar.Globals.Account.Pkid;
            //    yj.Bcsj = yj.Shsj = DateTime.Now.AddMinutes(1).ToString("yyyy-MM-dd HH:mm:00");
            //    yj.Ztdm = ((int)TStar.Globals.SystemSettings.YjState.PublicityFinished).ToString();
            //    BLL.Globals.CalculateYjf(yj);
            //}

            //if (jlsl == 0 || BLL.Yjgl.Yj_xm.Insert(yj, DtXmfp) > 0)
            //{
            //    cnt = 1;
            //    drv["Del"] = 1;
            //}

            //return cnt;
            return 0;
        }

        #endregion

        #region 页面及其他事件

        protected virtual void Page_Init(object sender, EventArgs e)
        {
            GetGridSetting();
            InitializeGrid();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
                this.ShowUI();
            }
        }

        #endregion

        #region 按钮事件

        public void btnUpload_Click(object sender, EventArgs e)
        {
            this.Grid1.DataSource = ImportDataView = null;// BLL.Globals.GetNullTable(this.Grid1);
            this.Grid1.DataBind();
            this.btnImport.Enabled = false;

            string fullfile = "";

            // 上传文件
            try
            {
                HttpPostedFile f = this.fudExcel2.PostedFile;
                string filename = this.fudExcel2.ShortFileName.ToLower();
                if (String.IsNullOrEmpty(filename))
                {
                    FUI.Alert.Show/*Alert.ShowInParent*/("请先选择上传文件 ！", "上传提示", FUI.MessageBoxIcon.Warning);
                    return;
                }
                if (!filename.EndsWith(".xls"))
                {
                    FUI.Alert.Show/*Alert.ShowInParent*/("数据文件必须为 Excel 97-2003 格式 ！", "上传提示", FUI.MessageBoxIcon.Warning);
                    return;
                }

                string path = Server.MapPath("~/Uploads/Excel/");
                fullfile = path + String.Format("upexcel_{0}", filename);
                f.SaveAs(fullfile);
            }
            catch (Exception err)
            {
                FUI.Alert.Show/*Alert.ShowInParent*/(err.Message, "上传数据文件失败", FUI.MessageBoxIcon.Error);
                return;
            }

            // 读取文件
            DataTable dt = null;
            try
            {
                dt = GetExcelData(fullfile);
            }
            catch (Exception err)
            {
                FUI.Alert.Show/*Alert.ShowInParent*/(err.Message, "读取数据文件失败", FUI.MessageBoxIcon.Error);
                return;
            }

            // 检查文件字段            
            if (!CheckData(dt)) return;

            // 显示在Grid里
            ImportDataView = dt.DefaultView;
            DataView dv = ImportDataView;
            this.Grid1.PageSize = dv.Count;
            this.Grid1.DataSource = dv;
            this.Grid1.DataBind();

            this.btnImport.Enabled = true;
        }

        public void btnImport_Click(object sender, EventArgs e)
        {
            DataView dv = ImportDataView;
            int rowCnt = dv.Count, cnt = 0;
            if (rowCnt == 0)
            {
                FUI.Alert.Show/*Alert.ShowInParent*/("没有需要导入的数据 ！", "导入提示", FUI.MessageBoxIcon.Error);
                this.btnImport.Enabled = false;
                return;
            }

            string drsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            foreach (DataRowView drv in dv)
            {
                try
                {
                    cnt += ImportData(drv, drsj);
                }
                catch (Exception err)
                {
                    drv[BzName] = err.Message;
                }
            }

            dv.RowFilter = "Del <> 1";
            this.Grid1.DataSource = dv;
            this.Grid1.DataBind();
            //this.Grid1.AutoWidth = true; // 自动列宽

            // 关闭按钮的客户端脚本
            if (cnt > 0) btnClose.OnClientClick = FUI.ActiveWindow.GetHidePostBackReference();

            string msg = "";
            if (cnt == rowCnt) msg = String.Format("成功导入 {0} 条数据 ！", cnt);
            else msg = String.Format("成功导入 {0} 条数据，还有 {1} 条数据有误 ！", cnt, rowCnt - cnt);
            FUI.Alert.Show/*Alert.ShowInParent*/(msg, "导入完成", FUI.MessageBoxIcon.Information);
        }

        #endregion

        //protected override void Page_Init(object sender, EventArgs e)
        //{
        //    Columns10Widths = new int[] { 110, 100, 140, 80, 80, 80, 100, 120, 200, 120 };
        //    base.Page_Init(sender, e);
        //}

        /// <summary>
        /// 导入积极分子
        /// </summary>
        protected int ImportJjfzmd(DataRowView drv, string drsj)
        {
            int cnt = 0;
            string[] columns = Columns;
            string bmbh = TStar.Web.Globals.Account.DeptPkid;
            string dzbmc = drv[columns[0]].ToString().Trim();
            Model.Jcgl.Jd_dzb dzb = BLL.Jcgl.Jd_dzb.GetEntity(bmbh, dzbmc);
            if (string.IsNullOrEmpty(dzb.Pkid)) throw new Exception("该党支部信息不存在。");

            string zymc = drv[columns[1]].ToString().Trim();
            Model.Jcgl.Jd_zy zy = BLL.Jcgl.Jd_zy.GetEntity(bmbh, dzb.Pkid, zymc);
            if (string.IsNullOrEmpty(zy.Pkid)) throw new Exception("该专业信息不存在。");

            string zybh = zy.Pkid;
            string bjmc = drv[columns[2]].ToString().Trim();
            Model.Jcgl.Jd_bj bj = BLL.Jcgl.Jd_bj.GetEntity(bmbh, zybh, bjmc);
            if (string.IsNullOrEmpty(bj.Pkid)) throw new Exception("该班级信息不存在。");

            string xh = drv[columns[3]].ToString().Trim();
            if (string.IsNullOrEmpty(xh)) throw new Exception("学号不能为空。");
            if (BLL.Jcgl.Jc_xs.IsRepeated("Xh", xh)) throw new Exception("该学生已存在。");

            string xm = drv[columns[4]].ToString().Trim();
            if (string.IsNullOrEmpty(xm)) throw new Exception("姓名不能为空。");

            string xb = drv[columns[5]].ToString().Trim();
            string xbdm = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtDm_xb, "Mc", "Dm", xb, "");
            if (string.IsNullOrEmpty(xbdm)) throw new Exception("性别不正确。");

            string sfzh = drv[columns[6]].ToString().Trim();
            if (sfzh.Length != 18) throw new Exception("该身份证号长度不正确。");
            if (BLL.Jcgl.Jc_xs.IsRepeated("Sfzh", sfzh)) throw new Exception("该身份证号已存在。");

            int jcxb = int.Parse(sfzh[16].ToString());
            if (xb == "男" && jcxb % 2 == 0 || xb == "女" && jcxb % 2 == 1) throw new Exception("该身份证号的性别位不正确。");

            DateTime dt;
            string csrq = string.Format("{0}-{1}-{2}", sfzh.Substring(6, 4), sfzh.Substring(10, 2), sfzh.Substring(12, 2));
            if (!DateTime.TryParse(csrq, out dt)) throw new Exception("该身份证号的出生日期位不正确。");

            string jg = drv[columns[7]].ToString();
            if (string.IsNullOrEmpty(jg)) throw new Exception("籍贯不能为空。");

            string mz = drv[columns[8]].ToString();
            if (string.IsNullOrEmpty(mz)) throw new Exception("民族不能为空。");

            string zw = drv[columns[9]].ToString();
            string sj = drv[columns[10]].ToString();
            if (string.IsNullOrEmpty(mz)) throw new Exception("手机不能为空。");

            string sqrq = drv[columns[11]].ToString();
            if(sqrq.Length < 8)  throw new Exception(columns[11] + "不正确。");
            sqrq = string.Format("{0}-{1}-{2}", sqrq.Substring(0, 4), sqrq.Substring(4, 2), sqrq.Substring(6, 2));
            if (!DateTime.TryParse(sqrq, out dt)) throw new Exception(columns[11] + "不正确。");

            string qdrq = drv[columns[12]].ToString();
            if (qdrq.Length < 8) throw new Exception(columns[12] + "不正确。");
            qdrq = string.Format("{0}-{1}-{2}", qdrq.Substring(0, 4), qdrq.Substring(4, 2), qdrq.Substring(6, 2));
            if (!DateTime.TryParse(qdrq, out dt)) throw new Exception(columns[12] + "不正确。");

            string lxrgh = drv[columns[13]].ToString();
            string lxrxm = drv[columns[14]].ToString();
            Model.Jcgl.V_jc_lxr lxr = BLL.Jcgl.Jc_lxr.GetEntity(bmbh, dzb.Pkid, lxrgh, lxrxm);
            if (string.IsNullOrEmpty(lxr.Pkid)) throw new Exception("该联系人信息不存在。");

            string jtdz = drv[columns[15]].ToString();
            if (string.IsNullOrEmpty(jtdz)) throw new Exception(columns[11] + "不能为空。");

            Model.Lcgl.Lc_jjfzmd m = new Model.Lcgl.Lc_jjfzmd();
            m.Bmbh = bmbh;
            m.Zybh = zybh;
            m.Bjbh = bj.Pkid;
            m.Dzbbh = dzb.Pkid;
            m.Xh = xh;
            m.Xm = xm;
            m.Xbdm = xbdm;
            m.Sfzh = sfzh;
            m.Jg = jg;
            m.Mz = mz;
            m.Lxdh = sj;
            m.Zw = zw;
            m.Sqrdrq = sqrq;
            m.Jjfzrq = qdrq;
            m.Lxrbh = lxr.Pkid;
            m.Jtdz = jtdz;
            m.Drsj = drsj;
            if (BLL.Lcgl.Lc_jjfzmd.Save(m))
            {
                cnt = 1;
                drv["Del"] = 1;
            }

            return cnt;
        }

        /// <summary>
        /// 导入拟发展对象
        /// </summary>
        protected int ImportNfzmd(DataRowView drv, string drsj)
        {
            int cnt = 0;
            string[] columns = Columns;
            string xh = drv[columns[0]].ToString().Trim();
            string xm = drv[columns[1]].ToString().Trim();
            string bmbh = TStar.Web.Globals.Account.DeptPkid;
            string dzbbh = BLL.Globals.SystemSetting.IsCommittee ? null : TStar.Web.Globals.Account.UserInfo.Dzbbh;
            Model.Jcgl.Jc_xs xs = BLL.Jcgl.Jc_xs.GetEntity(bmbh, dzbbh, xh, xm);
            if (string.IsNullOrEmpty(xs.Pkid)) throw new Exception("该学生信息不存在。");
            if (BLL.Lcgl.Lc_nfzmd.Exist(xs.Pkid)) throw new Exception("该名单已导入。");
            if (int.Parse(xs.Fzztdm) != (int)TStar.Web.Globals.SystemSetting.Fzzt.Jjfz) throw new Exception("该学生不是积极分子。");

            string fzdxqdrq = drv[columns[2]].ToString().Trim();
            if (fzdxqdrq.Length < 8) throw new Exception(columns[2] + "不正确。");
            fzdxqdrq = string.Format("{0}-{1}-{2}", fzdxqdrq.Substring(0, 4), fzdxqdrq.Substring(4, 2), fzdxqdrq.Substring(6, 2));
            DateTime dt;
            if (!DateTime.TryParse(fzdxqdrq, out dt)) throw new Exception(columns[2] + "不正确。");

            string zsjg = drv[columns[3]].ToString().Trim();
            string zsjgdm = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtDm_jgzt, null, "Mc", "Dm", zsjg, "");
            if (zsjgdm == "") throw new Exception(columns[3] + "不正确。");

            string dbjg = drv[columns[4]].ToString().Trim();
            string dbjgdm = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtDm_jgzt, null, "Mc", "Dm", dbjg, "");
            if (dbjgdm == "") throw new Exception(columns[4] + "不正确。");

            string dbrq = drv[columns[5]].ToString().Trim();
            string dbdd = drv[columns[6]].ToString().Trim();
            string dbzcy = drv[columns[7]].ToString().Trim();
            string dbyj = drv[columns[8]].ToString().Trim();
            if (zsjgdm == "0")
            {
                dbjgdm = zsjgdm;
                dbrq = dbdd = dbzcy = dbyj = "";
            }
            else
            {
                if (dbrq.Length < 8) throw new Exception(columns[5] + "不正确。");
                dbrq = string.Format("{0}-{1}-{2}", dbrq.Substring(0, 4), dbrq.Substring(4, 2), dbrq.Substring(6, 2));
                if (!DateTime.TryParse(dbrq, out dt)) throw new Exception(columns[5] + "不正确。");
                if (dbrq.CompareTo(fzdxqdrq) <= 0) throw new Exception("答辩日期应大于发展对象确定日期。");

                if (string.IsNullOrEmpty(dbdd)) throw new Exception(columns[6] + "不能为空。");
                if (string.IsNullOrEmpty(dbzcy)) throw new Exception(columns[7] + "不能为空。");
                if (string.IsNullOrEmpty(dbyj)) throw new Exception(columns[8] + "不能为空。");
                else if (dbyj.Length > 200) throw new Exception(columns[8] + "限填200个字。");
            }

            string zswtgyy = drv[columns[9]].ToString().Trim();
            if (zsjgdm == "0")
            {
                if (string.IsNullOrEmpty(zswtgyy)) throw new Exception(columns[9] + "不能为空。");
                else if (zswtgyy.Length > 200) throw new Exception(columns[9] + "限填200个字。");
            }

            Model.Lcgl.Lc_nfzmd m = new Model.Lcgl.Lc_nfzmd();
            m.Bmbh = bmbh;
            m.Dzbbh = xs.Dzbbh;
            m.Xsbh = xs.Pkid;
            m.Fzdxrq = fzdxqdrq;
            m.Zsjgdm = zsjgdm;
            m.Dbjgdm = dbjgdm;
            m.Dbrq = dbrq;
            m.Dbdd = dbdd;
            m.Dbzcy = dbzcy;
            m.Dbpjyj = dbyj;
            m.Zswtgyy = zswtgyy;
            m.Drsj = drsj;
            if (BLL.Lcgl.Lc_nfzmd.Save(m))
            {
                cnt = 1;
                drv["Del"] = 1;
            }

            return cnt;
        }

        /// <summary>
        /// 导入发展对象
        /// </summary>
        protected int ImportFzmd(DataRowView drv, string drsj)
        {
            int cnt = 0;
            string[] columns = Columns;
            string xh = drv[columns[0]].ToString().Trim();
            string xm = drv[columns[1]].ToString().Trim();
            string bmbh = TStar.Web.Globals.Account.DeptPkid;
            string dzbbh = BLL.Globals.SystemSetting.IsCommittee ? null : TStar.Web.Globals.Account.UserInfo.Dzbbh;
            Model.Jcgl.Jc_xs xs = BLL.Jcgl.Jc_xs.GetEntity(bmbh, dzbbh, xh, xm);
            if (string.IsNullOrEmpty(xs.Pkid)) throw new Exception("该学生信息不存在。");
            if (BLL.Lcgl.Lc_fzmd.Exist(xs.Pkid)) throw new Exception("该名单已导入。");
            if (int.Parse(xs.Fzztdm) != (int)TStar.Web.Globals.SystemSetting.Fzzt.Nfzdx) throw new Exception("该学生不是拟发展对象。");

            string bjjg = drv[columns[2]].ToString().Trim();
            string bjjgdm = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtDm_jgzt, null, "Mc", "Dm", bjjg, "");
            if (bjjgdm == "") throw new Exception(columns[2] + "不正确。");
            
            DateTime dt;
            string zbdhrq = drv[columns[3]].ToString().Trim();
            if (zbdhrq.Length < 8) throw new Exception(columns[3] + "不正确。");
            zbdhrq = string.Format("{0}-{1}-{2}", zbdhrq.Substring(0, 4), zbdhrq.Substring(4, 2), zbdhrq.Substring(6, 2));
             if (!DateTime.TryParse(zbdhrq, out dt)) throw new Exception(columns[3] + "不正确。");

             string zysbh = drv[columns[4]].ToString().Trim();
             if (string.IsNullOrEmpty(zysbh)) throw new Exception(columns[4] + "不能为空。");

            string bz = drv[columns[5]].ToString().Trim();

            Model.Lcgl.Lc_fzmd m = new Model.Lcgl.Lc_fzmd();
            m.Bmbh = bmbh;
            m.Dzbbh = xs.Dzbbh;
            m.Xsbh = xs.Pkid;
            m.Bjjgdm = bjjgdm;
            m.Zbdhrq = zbdhrq;
            m.Zysbh = zysbh;
            m.Bz = bz;
            m.Drsj = drsj;
            if (BLL.Lcgl.Lc_fzmd.Save(m))
            {
                cnt = 1;
                drv["Del"] = 1;
            }

            return cnt;
        }

        /// <summary>
        /// 导入转正对象
        /// </summary>
        protected int ImportZzmd(DataRowView drv, string drsj)
        {
            int cnt = 0;
            string[] columns = Columns;
            string xh = drv[columns[0]].ToString().Trim();
            string xm = drv[columns[1]].ToString().Trim();
            string bmbh = TStar.Web.Globals.Account.DeptPkid;
            string dzbbh = BLL.Globals.SystemSetting.IsCommittee ? null : TStar.Web.Globals.Account.UserInfo.Dzbbh;
            Model.Jcgl.Jc_xs xs = BLL.Jcgl.Jc_xs.GetEntity(bmbh, dzbbh, xh, xm);
            if (string.IsNullOrEmpty(xs.Pkid)) throw new Exception("该学生信息不存在。");
            if (BLL.Lcgl.Lc_zzmd.Exist(xs.Pkid)) throw new Exception("该名单已导入。");
            if (int.Parse(xs.Fzztdm) != (int)TStar.Web.Globals.SystemSetting.Fzzt.Ybdy) throw new Exception("该学生不是预备党员。");

            string bjjg = drv[columns[2]].ToString().Trim();
            string bjjgdm = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtDm_jgzt, null, "Mc", "Dm", bjjg, "");
            if (bjjgdm == "") throw new Exception(columns[2] + "不正确。");

            DateTime dt;
            string yqzzrq = "";
            if (bjjgdm == "0")
            {
                yqzzrq = drv[columns[3]].ToString().Trim();
                if (yqzzrq.Length < 8) throw new Exception(columns[3] + "不正确。");
                yqzzrq = string.Format("{0}-{1}-{2}", yqzzrq.Substring(0, 4), yqzzrq.Substring(4, 2), yqzzrq.Substring(6, 2));
                if (!DateTime.TryParse(yqzzrq, out dt)) throw new Exception(columns[3] + "不正确。");
                if (dt <= DateTime.Now) throw new Exception("延期转正日期应大于当前日期。");

            }

            string zbdhrq = drv[columns[4]].ToString().Trim();
            if (zbdhrq.Length < 8) throw new Exception(columns[4] + "不正确。");
            zbdhrq = string.Format("{0}-{1}-{2}", zbdhrq.Substring(0, 4), zbdhrq.Substring(4, 2), zbdhrq.Substring(6, 2));
            if (!DateTime.TryParse(zbdhrq, out dt)) throw new Exception(columns[4] + "不正确。");
            
            string fdwshrq = drv[columns[5]].ToString().Trim();
            if (fdwshrq.Length < 8) throw new Exception(columns[5] + "不正确。");
            fdwshrq = string.Format("{0}-{1}-{2}", fdwshrq.Substring(0, 4), fdwshrq.Substring(4, 2), fdwshrq.Substring(6, 2));
            if (!DateTime.TryParse(fdwshrq, out dt)) throw new Exception(columns[5] + "不正确。");

            string shyj = drv[columns[6]].ToString().Trim();
            string bz = drv[columns[7]].ToString().Trim();

            Model.Lcgl.Lc_zzmd m = new Model.Lcgl.Lc_zzmd();
            m.Bmbh = bmbh;
            m.Dzbbh = xs.Dzbbh;
            m.Xsbh = xs.Pkid;
            m.Bjjgdm = bjjgdm;
            m.Yqzzrq = yqzzrq;
            m.Zbdhrq = zbdhrq;
            m.Fdwshrq = fdwshrq;
            m.Shyj = shyj;
            m.Bz = bz;
            m.Drsj = drsj;
            if (BLL.Lcgl.Lc_zzmd.Save(m))
            {
                cnt = 1;
                drv["Del"] = 1;
            }

            return cnt;
        }
    }
}