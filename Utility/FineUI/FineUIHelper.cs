using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FineUI;

namespace TStar.Utility.FineUI
{
    public class Helper
    {
        public static HttpContext Page
        {
            get { return HttpContext.Current; }
        }

        #region 绑定控件

        /// <summary>
        /// 重置下拉框
        /// </summary>
        public static void BindDropDownList(DropDownList ddl, string text = "－请选择－")
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("dm", Type.GetType("System.String"));
            dt.Columns.Add("mc", Type.GetType("System.String"));
            dt.Rows.Add(new string[] { "__", text });

            ddl.DataSource = dt.DefaultView;
            ddl.DataTextField = "mc";
            ddl.DataValueField = "dm";
            ddl.DataBind();
            ddl.SelectedIndex = 0;
        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        public static void BindDropDownList(DataTable dt, DropDownList ddl, string textField, string valueField, string sort = null)
        {
            BindDropDownList(dt, ddl, textField, valueField, sort, null, null, null);
        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        public static void BindDropDownList(DataTable dt, DropDownList ddl, string textField, string valueField, string sort, string filter, string selectedValue = null, string firstTitle = null)
        {
            DataView dv = dt.DefaultView;
            string srt = dv.Sort;
            string flt = dv.RowFilter;

            if (!String.IsNullOrEmpty(sort) && srt != sort) dv.Sort = sort;
            if (!String.IsNullOrEmpty(firstTitle)) dv[0][textField] = firstTitle;
            if (!String.IsNullOrEmpty(filter) && flt != filter) dv.RowFilter = filter;

            ddl.DataSource = dv;
            ddl.DataTextField = textField;
            ddl.DataValueField = valueField;
            ddl.DataBind();
            if (selectedValue != null) ddl.SelectedValue = selectedValue;
            else ddl.SelectedIndex = 0;

            if (!String.IsNullOrEmpty(firstTitle)) dt.RejectChanges();
            if (!String.IsNullOrEmpty(sort) && srt != sort) dv.Sort = srt;
            if (!String.IsNullOrEmpty(filter) && flt != filter) dv.RowFilter = flt;
        }

        /// <summary>
        /// 绑定复选列表框
        /// </summary>
        public static void BindCheckBoxList(DataTable dt, CheckBoxList cbl, string text, string value, string sort, string filter = null)
        {
            DataView dv = dt.DefaultView;
            string srt = dv.Sort;
            string flt = dv.RowFilter;

            if (!String.IsNullOrEmpty(sort) && srt != sort) dv.Sort = sort;
            if (!String.IsNullOrEmpty(filter) && flt != filter) dv.RowFilter = filter;

            cbl.DataSource = dt.DefaultView;
            cbl.DataTextField = text;
            cbl.DataValueField = value;
            cbl.DataBind();

            if (!String.IsNullOrEmpty(sort) && srt != sort) dv.Sort = srt;
            if (!String.IsNullOrEmpty(filter) && flt != filter) dv.RowFilter = flt;
        }

        /// <summary>
        /// 绑定单选按钮列表框
        /// </summary>
        public static void BindRadioButtonList(DataTable dt, RadioButtonList rbl, string text, string value, string sort)
        {
            if (!String.IsNullOrEmpty(sort)) dt.DefaultView.Sort = sort;

            rbl.DataSource = dt.DefaultView;
            rbl.DataTextField = text;
            rbl.DataValueField = value;
            rbl.DataBind();
        }

        #endregion

        #region 绑定树型控件

        /// <summary>
        /// 绑定树型视图
        /// </summary>
        /// <param name="dtTree">数据源</param>
        /// <param name="ddl">下拉框</param>
        /// <param name="first">首行显示文本</param>
        public static void BindTreeView(DataTable dtTree, DropDownList ddl, string first)//, string txtField, string dmField)
        {
            if (!String.IsNullOrEmpty(first)) dtTree.Rows[0]["Mc"] = first;

            List<TreeItem> myList = new List<TreeItem>();
            foreach (DataRow dr in dtTree.Rows)
            {
                string dmField = "Dm";
                string txtField = "Mc";
                string id = dr[dmField].ToString();
                string mc = dr[txtField].ToString();
                int lv = int.Parse(dr["depth"].ToString());
                bool canSel = bool.Parse(dr["select"].ToString());
                myList.Add(new TreeItem(id, mc, lv, canSel));
            }
            ddl.EnableSimulateTree = true;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSimulateTreeLevelField = "Level";
            ddl.DataEnableSelectField = "CanSelect";
            ddl.DataSource = myList;
            ddl.DataBind();
        }
        /// <summary>
        /// 绑定树型视图
        /// </summary>
        /// <param name="dtTree">数据源</param>
        /// <param name="ddl">下拉框</param>
        /// <param name="dmField">值字段名称：Dm或Id</param>
        /// <param name="first">首行显示文本</param>
        public static void BindTreeView(DataTable dtTree, DropDownList ddl, string dmField, string first)
        {
            if (!String.IsNullOrEmpty(first)) dtTree.Rows[0]["Mc"] = first;

            List<TreeItem> myList = new List<TreeItem>();
            foreach (DataRow dr in dtTree.Rows)
            {
                string txtField = "Mc";
                string id = dr[dmField].ToString();
                string mc = dr[txtField].ToString();
                int lv = int.Parse(dr["depth"].ToString());
                bool canSel = bool.Parse(dr["select"].ToString());
                myList.Add(new TreeItem(id, mc, lv, canSel));
            }
            ddl.EnableSimulateTree = true;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSimulateTreeLevelField = "Level";
            ddl.DataEnableSelectField = "CanSelect";
            ddl.DataSource = myList;
            ddl.DataBind();
        }

        /// <summary>
        /// 创建树型视图数据源(dm字段需能反映层次关系，如：110000)
        /// </summary>
        /// <param name="dtSource">数据源</param>
        /// <param name="rootSelect">顶级结点是否可选</param>
        /// <param name="fields">对应字段（依次为：id,dm,mc）</param>
        /// <param name="first">首行显示文本</param>
        /// <returns></returns>
        public static DataTable CreateTreeViewSource(DataTable dtSource, bool rootSelect, params string[] fields)
        {
            string fid = fields[0];
            string fdm = fields[1];
            string fmc = fields[2];
            DataTable dtTree = new DataTable();
            dtTree.Columns.Add("id", typeof(System.String));
            dtTree.Columns.Add("dm", typeof(System.String));
            dtTree.Columns.Add("mc", typeof(System.String));
            dtTree.Columns.Add("sjdm", typeof(System.String));
            dtTree.Columns.Add("depth", typeof(System.Int32));
            dtTree.Columns.Add("select", typeof(System.String)); // 是否可选（true或false）

            foreach (DataRow dr in dtSource.Rows)
            {
                string dm = dr[fdm].ToString();
                if (dm == "__") continue;

                int idx = dm.IndexOf('0');
                DataRow ndr = dtTree.NewRow();
                ndr[0] = dr[fid].ToString();
                ndr[1] = dm;
                ndr[2] = dr[fmc].ToString();
                ndr[3] = dm.Substring(0, idx - 1) + "0".PadRight(dm.Length - idx + 1, '0');
                ndr[4] = idx - 1;
                ndr[5] = idx > 1 || rootSelect;
                dtTree.Rows.Add(ndr);
            }
            DataRow fdr = dtTree.NewRow();
            fdr[0] = "__";
            fdr[1] = "__";
            fdr[2] = "－不限－";
            fdr[3] = "__";
            fdr[4] = 0;
            fdr[5] = rootSelect;
            dtTree.Rows.InsertAt(fdr, 0);

            dtTree.AcceptChanges();
            return dtTree;
        }
        /// <summary>
        /// 创建树型视图数据源(dm字段需能反映层次关系，如：110000)
        /// </summary>
        /// <param name="dtSource">数据源</param>
        /// <param name="filter">数据源的过滤条件</param>
        /// <param name="selectFilter">可选的过滤条件</param>
        /// <param name="fields">对应字段（依次为：id,dm,mc）</param>
        /// <returns></returns>
        public static DataTable CreateTreeViewSource(DataTable dtSource, string filter, string selectFilter, bool rootSelect, params string[] fields)
        {
            string fid = fields[0];
            string fdm = fields[1];
            string fmc = fields[2];
            ArrayList al = new ArrayList();
            DataView dv = dtSource.DefaultView;
            string oldFilter = dv.RowFilter;
            dv.RowFilter = selectFilter;
            foreach (DataRowView drv in dv)
            {
                al.Add(drv[fdm].ToString());
            }

            DataTable dtTree = new DataTable();
            dtTree.Columns.Add("id", typeof(System.String));
            dtTree.Columns.Add("dm", typeof(System.String));
            dtTree.Columns.Add("mc", typeof(System.String));
            dtTree.Columns.Add("sjdm", typeof(System.String));
            dtTree.Columns.Add("depth", typeof(System.Int32));
            dtTree.Columns.Add("select", typeof(System.String)); // 是否可选（true或false）

            dv.RowFilter = filter;
            foreach (DataRowView dr in dv)
            {
                string dm = dr[fdm].ToString();
                if (dm == "__") continue;

                int idx = dm.IndexOf('0');
                DataRow ndr = dtTree.NewRow();
                ndr[0] = dr[fid].ToString();
                ndr[1] = dm;
                ndr[2] = dr[fmc].ToString();
                ndr[3] = dm.Substring(0, idx - 1) + "0".PadRight(dm.Length - idx + 1, '0');
                ndr[4] = idx - 1;
                ndr[5] = al.IndexOf(dr[fdm].ToString()) > -1;
                dtTree.Rows.Add(ndr);
            }
            dv.RowFilter = oldFilter;

            DataRow fdr = dtTree.NewRow();
            fdr[0] = "__";
            fdr[1] = "__";
            fdr[2] = "－不限－";
            fdr[3] = "__";
            fdr[4] = 0;
            fdr[5] = rootSelect;
            dtTree.Rows.InsertAt(fdr, 0);

            dtTree.AcceptChanges();
            return dtTree;
        }

        #endregion



        /// <summary>
        /// 设置IFrame链接参数
        /// </summary>
        public static void SetIFrameUrl(Tab tab, string url, params string[] param)
        {
            if (param == null) tab.IFrameUrl = url + "?tick=" + DateTime.Now.Ticks;
            else
            {
                string s = String.Join("&", param);
                s = TripleDESEncrypt(s);
                tab.IFrameUrl = url + "?param=" + s.Replace("+", "@") + "&tick=" + DateTime.Now.Ticks;
            }
        }
        /// <summary>
        /// 设置IFrame链接参数
        /// </summary>
        public static void SetIFrameUrl(Panel pnl, string url, params string[] param)
        {
            if (param == null) pnl.IFrameUrl = url + "?tick=" + DateTime.Now.Ticks;
            else
            {
                string s = String.Join("&", param);
                s = TripleDESEncrypt(s);
                pnl.IFrameUrl = url + "?param=" + s.Replace("+", "@") + "&tick=" + DateTime.Now.Ticks;
            }
        }

        /// <summary>
        /// 三重DES加密
        /// </summary>
        private static string TripleDESEncrypt(string input)
        {
            try
            {
                Byte[] data = Encoding.UTF8.GetBytes(input);
                Byte[] Key0 = { 0x0e, 0x2c, 0x4a, 0x69, 0x87, 0xb5, 0xd3, 0xf1, 0x69, 0x87, 0xb5, 0xd3, 0xf1, 0x0e, 0x2c, 0x4a, 0xd3, 0xf1, 0x0e, 0x2c, 0x4a, 0x69, 0x87, 0xb5 };
                Byte[] IV0 = { 0x1f, 0x3d, 0x5b, 0x78, 0x96, 0xa4, 0xc2, 0xe0 };
                Byte[] result = TripleDES.Create().CreateEncryptor(Key0, IV0).TransformFinalBlock(data, 0, data.Length);

                return Convert.ToBase64String(result);
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        #region 网格工具

        /// <summary>
        /// 获取选中行的ID
        /// </summary>
        public static string GetSelectedRowIDs(Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            int selectedCount = grid.SelectedRowIndexArray.Length;
            if (selectedCount > 0)
            {
                for (int i = 0; i < selectedCount; i++)
                {
                    int rowIndex = grid.SelectedRowIndexArray[i];

                    // 如果是内存分页，所有分页的数据都存在，rowIndex 就是在全部数据中的顺序，而不是当前页的顺序
                    if (grid.AllowPaging && !grid.IsDatabasePaging)
                    {
                        rowIndex = grid.PageIndex * grid.PageSize + rowIndex;
                    }

                    sb.AppendFormat(",{0}", grid.DataKeys[rowIndex][0]);
                }
            }

            return sb.ToString().Substring(1);
        }

        /// <summary>
        /// 获取选中行的Keys
        /// </summary>
        public static string GetSelectedRowKeys(Grid grid, int dataKeysIndex)
        {
            StringBuilder sb = new StringBuilder();
            int selectedCount = grid.SelectedRowIndexArray.Length;
            if (selectedCount > 0)
            {
                for (int i = 0; i < selectedCount; i++)
                {
                    int rowIndex = grid.SelectedRowIndexArray[i];

                    // 如果是内存分页，所有分页的数据都存在，rowIndex 就是在全部数据中的顺序，而不是当前页的顺序
                    if (grid.AllowPaging && !grid.IsDatabasePaging)
                    {
                        rowIndex = grid.PageIndex * grid.PageSize + rowIndex;
                    }

                    sb.AppendFormat(",{0}", grid.DataKeys[rowIndex][dataKeysIndex]);
                }
            }

            return sb.ToString().Substring(1);
        }

        /// <summary>
        /// 获取选中行的Keys(每项之间以'|'分隔)
        /// </summary>
        public static string GetSelectedRowKeys(Grid grid, params int[] dataKeysIndexs)
        {
            StringBuilder sb = new StringBuilder();
            int selectedCount = grid.SelectedRowIndexArray.Length;
            if (selectedCount > 0)
            {
                for (int i = 0; i < selectedCount; i++)
                {
                    int rowIndex = grid.SelectedRowIndexArray[i];

                    // 如果是内存分页，所有分页的数据都存在，rowIndex 就是在全部数据中的顺序，而不是当前页的顺序
                    if (grid.AllowPaging && !grid.IsDatabasePaging)
                    {
                        rowIndex = grid.PageIndex * grid.PageSize + rowIndex;
                    }

                    string s = "";
                    foreach (int idx in dataKeysIndexs)
                    {
                        s += "|" + grid.DataKeys[rowIndex][idx];
                    }
                    sb.AppendFormat(",{0}", s.Substring(1));
                }
            }

            return sb.ToString().Substring(1);
        }

        /// <summary>
        /// 获取选中行的数据
        /// </summary>
        public static string GetSelectedRowString(Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            int selectedCount = grid.SelectedRowIndexArray.Length;
            if (selectedCount > 0)
            {
                sb.AppendFormat("共选中了 {0} 行：", selectedCount);
                sb.Append("<table style=\"width:500px;\">");

                sb.Append("<tr><th>行号</th>");
                foreach (string datakey in grid.DataKeyNames)
                {
                    sb.AppendFormat("<th>{0}</th>", datakey);
                }
                sb.Append("</tr>");


                for (int i = 0; i < selectedCount; i++)
                {
                    int rowIndex = grid.SelectedRowIndexArray[i];
                    sb.Append("<tr>");

                    sb.AppendFormat("<td>{0}</td>", rowIndex + 1);

                    // 如果是内存分页，所有分页的数据都存在，rowIndex 就是在全部数据中的顺序，而不是当前页的顺序
                    if (grid.AllowPaging && !grid.IsDatabasePaging)
                    {
                        rowIndex = grid.PageIndex * grid.PageSize + rowIndex;
                    }

                    object[] dataKeys = grid.DataKeys[rowIndex];
                    for (int j = 0; j < dataKeys.Length; j++)
                    {
                        sb.AppendFormat("<td>{0}</td>", dataKeys[j]);
                    }

                    sb.Append("</tr>");
                }
                sb.Append("</table>");
            }
            else
            {
                sb.Append("<strong>没有选中任何一行！</strong>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将Grid转成表格形式
        /// </summary>
        public static string GetGridTableHtml(Grid grid)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");

            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");

            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (object value in row.Values)
                {
                    string html = value.ToString();
                    // 处理CheckBox
                    if (html.Contains("box-grid-static-checkbox"))
                    {
                        html = html.Contains("box-grid-static-checkbox-uncheck") ? "×" : "√";
                    }

                    // 处理图片
                    if (html.Contains("<img"))
                    {
                        string prefix = Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "");
                        html = html.Replace("src=\"", "src=\"" + prefix);
                    }

                    sb.AppendFormat("<td>{0}</td>", html);
                }
                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }

        #endregion
    }

    #region TreeItem of DropDownList

    public class TreeItem
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _level;
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        private bool _canSelect;
        public bool CanSelect
        {
            get { return _canSelect; }
            set { _canSelect = value; }
        }

        public TreeItem(string id, string name, int level, bool canSelect)
        {
            _id = id;
            _name = name;
            _level = level;
            _canSelect = canSelect;
        }

        public override string ToString()
        {
            return String.Format("Name:{0},Id:{1}", Name, Id);
        }
    }

    #endregion
}