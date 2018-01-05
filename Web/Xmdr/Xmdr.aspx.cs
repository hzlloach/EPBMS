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
    public partial class Xmdr : TStar.Web.BasePage
    {
        #region 自定义属性

        private string[] TeachLoads = { "工号", "姓名", "工作量" };
        private string[] Invigilates = { "工号", "姓名", "监考场数" };
        private string[] Meetings = { "工号", "姓名", "参会次数" };
        private string[] Papers = { "工号", "姓名", "论文篇数" };
        private string[] Appraises = { "工号", "姓名", "评教分", "学生评教分", "专家评教分" };
        private string[] Accidents = { "工号", "姓名", "事故等级", "事故描述" };
        private string[] Deducts = { "工号", "姓名", "扣分等级", "扣分描述" };
        private string[] PendingDeducts = { "工号", "姓名", "扣分分值", "扣分描述" };
        protected int[] Columns3Widths = { 80, 100, 80 };
        protected int[] Columns4Widths = { 80, 100, 120, 250 };
        protected int[] Columns5Widths = { 120, 100, 100, 100, 100 };
        protected int[] Columns10Widths = { 120, 100, 100, 100, 100 };

        protected string BzName = "错误信息";

        /// <summary>
        /// 指标编号(前32位)和名称
        /// </summary>
        protected string Djzbmc
        {
            get
            {
                string djzb = TU.Globals.GetParaValue("zb", "");
                string zbdm = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_khzbLocal, "Pkid", "Zbdm", djzb, "");
                //int idx = zbdm.IndexOf('0');
                //if (idx > 2)
                //{
                //    zbdm = zbdm.Substring(0, idx - 1) + "0".PadLeft(zbdm.Length - idx + 1, '0');
                //    djzb = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_khzbLocal, "Zbdm", "Pkid", zbdm, "");
                //}
                string zbmc = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_khzbLocal, "Zbdm", "Zbmc", zbdm, "");
                return djzb + zbmc;
            }
        }

        protected string TableName
        {
            get
            {
                string name = hfdTableName.Text;
                if (String.IsNullOrEmpty(name))
                {
                    try
                    {
                        name = TU.Globals.GetParaValue("p", "").Replace(" ", "+");
                        name = TU.Globals.TripleDESDecrypt(name).Replace(BLL.Globals.SystemSetting.EncryptCode, "");
                        //if (name.IndexOf("听课") > -1) name = "听课";
                        //else switch (name)
                        //    {
                        //        case "教学总课时": name = "工作量"; break;
                        //        case "教学质量评价": name = "评教"; break;
                        //        case "参加校会议": name = "参会"; break;
                        //    }
                        hfdTableName.Text = name;
                    }
                    catch
                    {
                        name = "Err";
                    }
                }
                return name;
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
            get { return TU.Globals.GetParaValue("bll", "YjDryj"); }
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
            if (TableName == "Err")
            {
                FUI.Alert.Show/*Alert.ShowInParent*/("页面参数不正确 ！", "打开失败", FUI.MessageBoxIcon.Error);
                FUI.PageContext.RegisterStartupScript(FUI.ActiveWindow.GetHideReference());
                return;
            }

            btnTemplate.OnClientClick = "javascript:downloadFile('../Template/" + TableName + "模版.xls')";

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
            string filename = TableName + "模版.xls";
            string path = Server.MapPath("~/Template/");
            string fullfile = path + filename;

            Columns = GetExcelColumns(fullfile);
            switch (Columns.Length)
            {
                default:
                case 3: ColumnWidths = Columns3Widths; break;
                case 4: ColumnWidths = Columns4Widths; break;
                case 5: ColumnWidths = Columns5Widths; break;
                case 10: ColumnWidths = Columns10Widths; break;
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
        protected virtual int ImportData(DataRowView drv)
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

            //switch (columns.Length)
            //{
            //    case 3:
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
            //}
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
                if (!filename.EndsWith(".xls") && !filename.EndsWith(".xlsx"))
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

            foreach (DataRowView drv in dv)
            {
                try
                {
                    cnt += ImportData(drv);
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
    }
}