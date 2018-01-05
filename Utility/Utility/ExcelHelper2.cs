using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace TStar.Utility.Excel
{
    public class WebHelper
    {
        /// <summary>
        /// 自动设置Excel列宽
        /// </summary>
        /// <param name="sheet">Excel表</param>
        private static void AutoSizeColumns(ISheet sheet)
        {
            if (sheet.PhysicalNumberOfRows > 0)
            {
                IRow headerRow = sheet.GetRow(0);

                for (int i = 0, l = headerRow.LastCellNum; i < l; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
            }
        }

        /// <summary>
        /// 输出文件到浏览器
        /// </summary>
        /// <param name="ms">Excel文档流</param>
        /// <param name="context">HTTP上下文</param>
        /// <param name="fileName">保存的文件名</param>
        private static void RenderToBrowser(MemoryStream ms, HttpContext context, string fileName)
        {
            if (ms == null) return;

            if (String.IsNullOrEmpty(fileName)) fileName = DateTime.Now.Ticks + ".xls";

            if (context.Request.Browser.Browser == "IE")
                fileName = HttpUtility.UrlEncode(fileName);
            context.Response.AddHeader("Content-Disposition", "attachment;fileName=" + fileName);
            context.Response.BinaryWrite(ms.ToArray());
        }

        /// <summary>
        /// DataTable转换成Excel文档流
        /// </summary>
        /// <param name="table">数据源</param>
        /// <param name="autosize">是否自动调整列宽</param>
        /// <param name="pics">图片数组，包含物理绝对路径</param>
        private static MemoryStream RenderToExcel(DataView dv, bool autosize, string[] pics)
        {
            MemoryStream ms = new MemoryStream();

            using (IWorkbook workbook = new HSSFWorkbook())
            {
                ISheet sheet = null, sheetPic = null;

                #region 导出数据
                if(dv != null && dv.Count > 0)
                {
                    sheet = workbook.CreateSheet("数据");
                    DataColumnCollection Dcc = dv.Table.Columns;

                    // 单元格格式：带边框
                    ICellStyle cellstyle = workbook.CreateCellStyle();
                    cellstyle.BorderBottom = CellBorderType.THIN;
                    cellstyle.BorderLeft = CellBorderType.THIN;
                    cellstyle.BorderRight = CellBorderType.THIN;
                    cellstyle.BorderTop = CellBorderType.THIN;

                    // 标题行
                    IRow headerRow = sheet.CreateRow(0);
                    foreach (DataColumn column in Dcc)
                    {
                        ICell cell = headerRow.CreateCell(column.Ordinal);
                        cell.CellStyle = cellstyle;
                        cell.SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value
                    }

                    // 数据行
                    int rowIndex = 1;
                    foreach (DataRowView drv in dv)
                    {
                        IRow dataRow = sheet.CreateRow(rowIndex);
                        foreach (DataColumn column in Dcc)
                        {
                            ICell cell = dataRow.CreateCell(column.Ordinal);
                            cell.CellStyle = cellstyle;
                            SetCellValue(cell, drv[column.ColumnName]);
                        }

                        rowIndex++;
                    }

                    // 自动列宽
                    if (autosize) AutoSizeColumns(sheet);
                }
                #endregion
                
                #region 导出图片
                if (pics != null && pics.Length > 0)
                {
                    sheetPic = workbook.CreateSheet("图表");
                    InsertImages(sheetPic, workbook, pics);
                }
                #endregion

                // 输出到流
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                if (sheet != null) sheet.Dispose();
                if (sheetPic != null) sheetPic.Dispose();
            }
            return ms;
        }
        
        /// <summary>
        /// DataTable转换成Excel文档流
        /// </summary>
        /// <param name="table">数据源</param>
        /// <param name="titles">标题数组</param>
        /// <param name="template">模板文件物理绝对路径</param>
        /// <param name="blankRows">预留空白行数</param>
        /// <param name="titles">标题数组</param>
        /// <param name="pics">图片数组，包含物理绝对路径</param>
        private static MemoryStream RenderToExcel(DataView dv, string template, int blankRows, string[] titles, string[] pics)
        {
            if (!File.Exists(template)) return null;

            MemoryStream ms = new MemoryStream();
            using (FileStream file = new FileStream(template, FileMode.Open, FileAccess.Read))
            {
                using (IWorkbook workbook = new HSSFWorkbook(file))
                {
                    ISheet sheet = null, sheetPic = null;

                    #region 导出数据
                    if (dv != null && dv.Count > 0)
                    {
                        int titleRows = titles == null ? 0 : titles.Length;
                        sheet = workbook.GetSheetAt(0);

                        // 标题行
                        for (int i = 0; i < titleRows; i++)
                        {
                            IRow titleRow = sheet.GetRow(i);
                            ICell cell = titleRow.GetCell(0);
                            cell.SetCellValue(titles[i]);                            
                        }
                        
                        // 数据行
                        int rowIndex = titleRows + blankRows; // 忽略标题行
                        IRow templateRow = sheet.GetRow(rowIndex);
                        foreach (DataRowView row in dv)
                        {
                            CopyRow(sheet, templateRow.RowNum, rowIndex);

                            IRow dataRow = sheet.GetRow(rowIndex++);
                            foreach (DataColumn column in dv.Table.Columns)
                            {
                                ICell cell = dataRow.GetCell(column.Ordinal);
                                SetCellValue(cell, row[column.ColumnName]);
                            }
                        }
                    }
                    #endregion

                    #region 导出图片
                    if (pics != null && pics.Length > 0)
                    {
                        sheetPic = workbook.CreateSheet("图表");
                        InsertImages(sheetPic, workbook, pics);
                    }
                    #endregion

                    // 输出到流
                    workbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;

                    if (sheet != null) sheet.Dispose();
                    if (sheetPic != null) sheetPic.Dispose();
                }
            }
            return ms;
        }

        /// <summary>
        /// DataTable转换成Excel文档流
        /// </summary>
        /// <param name="table">数据源</param>
        /// <param name="titles">标题数组</param>
        /// <param name="template">模板文件物理绝对路径</param>
        /// <param name="titles">标题数组</param>
        /// <param name="pics">图片数组，包含物理绝对路径</param>
        /// <param name="dynColStartInx">动态列起始下标</param>
        private static MemoryStream RenderToExcel(DataView dv, string template, string[] titles, string[] pics, int dynColStartInx)
        {
            if (!File.Exists(template)) return null;

            MemoryStream ms = new MemoryStream();
            using (FileStream file = new FileStream(template, FileMode.Open, FileAccess.Read))
            {
                using (IWorkbook workbook = new HSSFWorkbook(file))
                {
                    ISheet sheet = null, sheetPic = null;

                    #region 导出数据
                    if (dv != null && dv.Table.Rows.Count > 0)
                    {
                        int titleRows = titles == null ? 0 : titles.Length;
                        sheet = workbook.GetSheetAt(0);

                        // 标题行
                        for (int i = 0; i < titleRows; i++)
                        {
                            IRow titleRow = sheet.GetRow(i);
                            ICell cell = titleRow.GetCell(0);
                            cell.SetCellValue(titles[i]);
                        }

                        // 数据列
                        int rowIndex = titleRows;
                        IRow templateRow = sheet.GetRow(rowIndex);
                        IRow templateValRow = sheet.GetRow(rowIndex+1);
                        ICell templateColCell = templateRow.GetCell(dynColStartInx);
                        ICell templateValCell = templateValRow.GetCell(dynColStartInx);
                        for (int i = dynColStartInx+1; i < dv.Table.Columns.Count; i++)
                        {
                            ICell cell = templateRow.CreateCell(i);
                            CopyCell(templateColCell, cell);
                            cell.SetCellValue(dv.Table.Columns[i].ColumnName);

                            CopyCell(templateValCell, templateValRow.CreateCell(i));
                        }

                        // 数据行
                        rowIndex = titles.Length + 1; // 忽略标题行
                        templateRow = sheet.GetRow(rowIndex);
                        foreach (DataRowView row in dv)
                        {
                            CopyRow(sheet, templateRow.RowNum, rowIndex);

                            IRow dataRow = sheet.GetRow(rowIndex++);
                            foreach (DataColumn column in dv.Table.Columns)
                            {
                                ICell cell = dataRow.GetCell(column.Ordinal);
                                SetCellValue(cell, row[column.ColumnName]);
                            }
                        }
                    }
                    #endregion

                    #region 导出图片
                    if (pics != null && pics.Length > 0)
                    {
                        sheetPic = workbook.CreateSheet("图表");
                        InsertImages(sheetPic, workbook, pics);
                    }
                    #endregion

                    // 输出到流
                    workbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;

                    if (sheet != null) sheet.Dispose();
                    if (sheetPic != null) sheetPic.Dispose();
                }
            }
            return ms;
        }
        
        /// <summary>
        /// 添加图片
        /// </summary>
        private static void InsertImages(ISheet sheet, IWorkbook workbook, string[] pics)
        {
            try
            {
                int row = 0;
                //string path = HttpContext.Current.Server.MapPath("~/");
                //if (path.Contains("\\")) path = path.Remove(path.Length - 1);
                HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                foreach (string picurl in pics)
                {
                    string fileName = picurl;
                    if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
                    {
                        byte[] bytes = System.IO.File.ReadAllBytes(fileName);
                        int pictureIdx = workbook.AddPicture(bytes, NPOI.SS.UserModel.PictureType.JPEG);
                        HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 0, row, 0, row);
                        HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);

                        pict.Resize(); // 用图片原始大小来显示
                        IClientAnchor anchor2 = pict.GetPreferredSize();
                        row = anchor2.Row2 + 2;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// DataTable转换成Excel文档流，并输出到客户端
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <param name="table">数据源</param>
        /// <param name="fileName">保存的文件名</param>
        /// <param name="autosize">是否自动调整列宽</param>
        /// <param name="pics">图片数组，包含物理绝对路径</param>
        public static void ExcelOutput(HttpContext context, DataView dv, string fileName, bool autosize, string[] pics)
        {
            using (MemoryStream ms = RenderToExcel(dv, autosize, pics))
            {
                RenderToBrowser(ms, context, fileName);
            }
        }

        /// <summary>
        /// DataTable转换成Excel文档流，并输出到客户端
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <param name="table">数据源</param>
        /// <param name="template">模板文件物理绝对路径</param>
        /// <param name="titles">标题数组</param>
        /// <param name="pics">图片数组，包含物理绝对路径</param>
        public static void ExcelOutput(HttpContext context, DataView dv, string template, int blankRows, string[] titles, string[] pics)
        {
            using (MemoryStream ms = RenderToExcel(dv, template, blankRows, titles, pics))
            {
                string[] s = template.Split('\\');
                string fileName = s[s.Length - 1];
                RenderToBrowser(ms, context, fileName);
            }
        }

        /// <summary>
        /// DataTable转换成Excel文档流，并输出到客户端
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <param name="table">数据源</param>
        /// <param name="template">模板文件物理绝对路径</param>
        /// <param name="titles">标题数组</param>
        /// <param name="pics">图片数组，包含物理绝对路径</param>
        /// <param name="dynColStartInx">动态列起始下标</param>
        public static void ExcelOutput(HttpContext context, DataView dv, string template, string[] titles, string[] pics, int dynColStartInx)
        {
            using (MemoryStream ms = RenderToExcel(dv, template, titles, pics, dynColStartInx))
            {
                string[] s = template.Split('\\');
                string fileName = s[s.Length - 1];
                RenderToBrowser(ms, context, fileName);
            }
        }

        private static void CopyRow(ISheet sheet, int fromRowIndex, int toRowIndex)
        {
            IRow sourceRow = sheet.GetRow(fromRowIndex);
            if (sourceRow == null || fromRowIndex == toRowIndex) return;

            IRow destRow = sheet.GetRow(toRowIndex);
            if (destRow == null) destRow = sheet.CreateRow(toRowIndex);
            
            // 行高
            destRow.Height = sourceRow.Height;

            for (int i = sourceRow.FirstCellNum; i < sourceRow.LastCellNum; i++)
            {
                ICell sourceCell = sourceRow.GetCell(i);
                ICell destCell = destRow.GetCell(i);
                if (destCell == null) destCell = destRow.CreateCell(i);

                CopyCell(sourceCell, destCell);
            }
        }

        private static void CopyCell(ICell sourceCell, ICell destCell)
        {
            //单元格的赋值(公式)
            //switch (sourceCell.CellType)
            //{
            //    case CellType.BOOLEAN:
            //        destCell.SetCellValue(sourceCell.BooleanCellValue);
            //        break;
            //    case CellType.NUMERIC:
            //        destCell.SetCellValue(sourceCell.NumericCellValue);
            //        //(sourceCell.DateCellValue); // 日期型数据
            //        break;
            //    case CellType.STRING:
            //        destCell.SetCellValue(sourceCell.StringCellValue);
            //        break;
            //    case CellType.BLANK:
            //        destCell.SetCellValue("");
            //        break;
            //    case CellType.FORMULA:
            //        destCell.CellFormula = sourceCell.CellFormula;
            //        break;
            //}

            //单元格的公式赋值
            //if(sourceCell.CellType == CellType.FORMULA)
            //    destCell.CellFormula = sourceCell.CellFormula;

            //单元格的格式
            destCell.CellStyle = sourceCell.CellStyle;

            //对单元格的批注赋值
            if (sourceCell.CellComment != null)
            {
                HSSFPatriarch patr = (HSSFPatriarch)destCell.Sheet.CreateDrawingPatriarch();
                IComment comment = patr.CreateCellComment(new HSSFClientAnchor(0, 0, 0, 0, destCell.ColumnIndex, destCell.RowIndex, destCell.ColumnIndex + 1, destCell.RowIndex + 1));

                comment.String = new HSSFRichTextString(sourceCell.CellComment.String.ToString());
                comment.Author = sourceCell.CellComment.Author;

                destCell.CellComment = comment;
            }
        }

        private static void SetCellValue(ICell cell, object value)
        {
            double d = 0;
            string s = value == null ? "" : value.ToString();
            if (double.TryParse(s, out d)) cell.SetCellValue(d);
            else cell.SetCellValue(s);


            //单元格的赋值(公式)
            switch (cell.CellType)
            {
                case CellType.BOOLEAN:
                    cell.SetCellValue(sourceCell.BooleanCellValue);
                    break;
                case CellType.NUMERIC:
                    cell.SetCellValue(sourceCell.NumericCellValue);
                    //(sourceCell.DateCellValue); // 日期型数据
                    break;
                case CellType.STRING:
                    cell.SetCellValue(sourceCell.StringCellValue);
                    break;
                case CellType.BLANK:
                    cell.SetCellValue("");
                    break;
                case CellType.FORMULA:
                    cell.CellFormula = sourceCell.CellFormula;
                    break;
            }
        }
    }
}
