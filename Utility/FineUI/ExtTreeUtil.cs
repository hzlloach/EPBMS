using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TU=TStar.Utility;

namespace TStar.Utility
{
    public partial class Globals
    {
        /// <summary>
        ///Tree 的摘要说明
        /// </summary>
        public class ExtTreeUtil
        {
            /// <summary>
            /// 绑定树型视图
            /// </summary>
            /// <param name="dtTree">数据源</param>
            /// <param name="ddl">下拉框</param>
            /// <param name="first">首行显示文本</param>
            public static void BindTreeView(DataTable dtTree, ExtAspNet.DropDownList ddl, string first)//, string txtField, string dmField)
            {
                if (!String.IsNullOrEmpty(first)) dtTree.Rows[0]["Mc"] = first;

                List<Globals.ExtTreeItem> myList = new List<Globals.ExtTreeItem>();
                foreach (DataRow dr in dtTree.Rows)
                {
                    string dmField = "Dm";
                    string txtField = "Mc";
                    string id = dr[dmField].ToString();
                    string mc = dr[txtField].ToString();
                    int lv = int.Parse(dr["depth"].ToString());
                    bool canSel = bool.Parse(dr["select"].ToString());
                    myList.Add(new Globals.ExtTreeItem(id, mc, lv, canSel));
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
            public static void BindTreeView(DataTable dtTree, ExtAspNet.DropDownList ddl, string dmField, string first)
            {
                if (!String.IsNullOrEmpty(first)) dtTree.Rows[0]["Mc"] = first;

                List<Globals.ExtTreeItem> myList = new List<Globals.ExtTreeItem>();
                foreach (DataRow dr in dtTree.Rows)
                {
                    string txtField = "Mc";
                    string id = dr[dmField].ToString();
                    string mc = dr[txtField].ToString();
                    int lv = int.Parse(dr["depth"].ToString());
                    bool canSel = bool.Parse(dr["select"].ToString());
                    myList.Add(new Globals.ExtTreeItem(id, mc, lv, canSel));
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
            /// 根据有父子关系的数据表创建树
            /// </summary>
            /// <param name="dt">数据表</param>
            /// <param name="filter">过滤条件</param>
            /// <param name="txtFieldName">显示文本的字段名</param>
            /// <param name="valFieldName">显示值的字段名</param>
            /// <param name="isPostback">是否允许回发</param>
            /// <param name="childFieldName">子字段名</param>
            /// <param name="parFieldName">父字段名</param>
            public static void CreateTree(ExtAspNet.Tree tree, DataTable dt, string filter, string txtFieldName, string valFieldName, bool isPostback, string childFieldName, string parFieldName)
            {
                if (dt.DataSet == null)
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    ds.Relations.Add("TreeRelation", dt.Columns[childFieldName], dt.Columns[parFieldName], false);
                }
                DataView dv = dt.DefaultView;
                if(!String.IsNullOrEmpty(filter)) dv.RowFilter = filter;
                
                foreach (DataRowView row in dv)
                {
                    if (row[childFieldName].ToString() == "__") continue;

                    if (row.Row.IsNull(parFieldName) || String.IsNullOrEmpty(row[parFieldName].ToString()))
                    {
                        ExtAspNet.TreeNode node = new ExtAspNet.TreeNode();
                        node.NodeID = row[valFieldName].ToString(); ;
                        node.Text = row[txtFieldName].ToString();
                        node.Icon = ExtAspNet.Icon.Folder;
                        node.IconUrl = "../images/treeview/node.jpg";
                        node.EnablePostBack = isPostback;
                        //node.Expanded = true;
                        tree.Nodes.Add(node);

                        ResolveSubTree(row.Row, txtFieldName, valFieldName, node);
                    }
                }
                //ds.Relations.RemoveAt(0);
                //ds.Tables.RemoveAt(0);

                //return tree;
            }
            private static void ResolveSubTree(DataRow dataRow, string txtFieldName, string valFieldName, ExtAspNet.TreeNode treeNode)
            {
                DataRow[] rows = dataRow.GetChildRows("TreeRelation");
                if (rows.Length > 0)
                {
                    //treeNode.Expanded = true;
                    foreach (DataRow row in rows)
                    {
                        ExtAspNet.TreeNode node = new ExtAspNet.TreeNode();
                        node.NodeID = row[valFieldName].ToString(); ;
                        node.Text = row[txtFieldName].ToString();
                        node.EnablePostBack = treeNode.EnablePostBack;
                        treeNode.Nodes.Add(node);

                        ResolveSubTree(row, txtFieldName, valFieldName, node);
                    }
                }
            }
            
            public static string GetTreeNodePath(DataTable dtTreeView, string nodeValue, char joinChar)
            {
                ArrayList al = new ArrayList();
                StringBuilder sb = new StringBuilder();
                Queue<DataRowView> queue = new Queue<DataRowView>();
                
                DataView dv = dtTreeView.DefaultView;
                dv.RowFilter = "dm='" + nodeValue + "'";
                if(dv.Count == 0) return "";

                queue.Enqueue(dv[0]);
                while (queue.Count > 0)
                {
                    DataRowView drv = queue.Dequeue();
                    al.Insert(0, drv["mc"].ToString());
                    dv.RowFilter = "dm='" + drv["sjdm"].ToString() + "'";
                    if (dv.Count > 0) queue.Enqueue(dv[0]);
                }
                foreach (string s in al)
                {
                    sb.Append(joinChar + s);
                }
                return sb.ToString().Substring(1);
            }

            private static string GetRadioString(string dm)
            {
                return "<input type='radio' id='rbn" + dm + "' name='tvRBuild' onclick='treeRadioButtonClick(this);'/>";
            }
            private static string GetRadioString(string dm, bool isLinked )
            {
                if (isLinked)
                    return "<input type='radio' id='rbn" + dm + "' name='tvRBuild' onclick='treeRadioLinkClick(this);'/>";
                else
                    return GetRadioString(dm);
            }
            private static string GetCheckString(string dm, string parDm)
            {
                return "<input type='checkbox' id='ckb" + (String.IsNullOrEmpty(parDm) ? "" : (parDm + "_")) + dm + "' onclick='treeCheckBoxClick(this);'/>";
            }
            private static string GetCheckStringNoClick(string dm, string parDm)
            {
                return "<input type='checkbox' id='ckb" + (String.IsNullOrEmpty(parDm) ? "" : (parDm + "_")) + dm + "'/>";
            }
            
            public static void CreateRadioTree(TreeView tree, DataTable dtTree, int leafDepth, int radioDepth)
            {
                string dm = "";
                //tree.ImageSet = TreeViewImageSet.XPFileExplorer;

                Queue<TreeNode> queue = new Queue<TreeNode>();
                DataView dv = dtTree.DefaultView;
                dv.RowFilter = "depth='0'";
                foreach (DataRowView drv in dv)
                {
                    int rdepth = int.Parse(drv["depth"].ToString());
                    TreeNode root = new TreeNode();
                    root.SelectAction = TreeNodeSelectAction.None;
                    root.Value = dm = drv["dm"].ToString();
                    root.Text = (rdepth >= radioDepth ? GetRadioString(dm) : "") + "<img class='treeIcon' src='../../images/treeview/root.jpg' height='16px' width='16px' >" + drv["mc"].ToString();
                    root.NavigateUrl = "#";
                    tree.Nodes.Add(root);
                    queue.Enqueue(root);
                }

                while (queue.Count > 0)
                {
                    TreeNode parNode = queue.Dequeue();
                    dv.RowFilter = "sjdm='" + parNode.Value + "'"; //.Substring(2)
                    int count = dv.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int ndepth = int.Parse(dv[i]["depth"].ToString());
                        bool isLeaf = ndepth >= leafDepth;
                        TreeNode node = new TreeNode();
                        node.SelectAction = TreeNodeSelectAction.None;
                        node.Value = dm = dv[i]["dm"].ToString();
                        node.Text = (radioDepth >= 0 && ndepth >= radioDepth ? GetRadioString(dm) : "") + "<img class='treeIcon' src='../../images/treeview/" + (isLeaf ? "leaf" : "node") + ".jpg' height='16px' width='16px'>" + dv[i]["mc"].ToString();
                        node.NavigateUrl = "#";
                        parNode.ChildNodes.Add(node);
                        queue.Enqueue(node);
                    }
                }

                dv.RowFilter = "";
                tree.ExpandAll();
            }
            public static void CreateRadioCheckTree(TreeView tree, DataTable dtTree, int leafDepth)
            {
                string dm = "";
                //tree.ImageSet = TreeViewImageSet.XPFileExplorer;

                Queue<TreeNode> queue = new Queue<TreeNode>();
                DataView dv = dtTree.DefaultView;
                dv.RowFilter = "depth='0'";
                foreach (DataRowView drv in dv)
                {
                    int rdepth = int.Parse(drv["depth"].ToString());
                    TreeNode root = new TreeNode();
                    root.SelectAction = TreeNodeSelectAction.None;
                    root.Value = dm = drv["dm"].ToString();
                    root.Text = "<img class='treeIcon' src='../../images/treeview/root.jpg' height='16px' width='16px'>" + drv["mc"].ToString();
                    root.NavigateUrl = "#";
                    tree.Nodes.Add(root);
                    queue.Enqueue(root);
                }

                while (queue.Count > 0)
                {
                    TreeNode parNode = queue.Dequeue();
                    dv.RowFilter = "sjdm='" + parNode.Value + "'";
                    int count = dv.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int ndepth = int.Parse(dv[i]["depth"].ToString());
                        bool isLeaf = ndepth >= leafDepth;
                        TreeNode node = new TreeNode();
                        node.SelectAction = TreeNodeSelectAction.None;
                        node.Value = dm = dv[i]["dm"].ToString();
                        node.Text = GetCheckStringNoClick(dm, parNode.Value) + "<img class='treeIcon' src='../../images/treeview/" + (isLeaf ? "leaf" : "node") + ".jpg' height='16px' width='16px'>" + dv[i]["mc"].ToString();
                        node.NavigateUrl = "#";
                        parNode.ChildNodes.Add(node);
                        queue.Enqueue(node);
                    }
                }

                dv.RowFilter = "";
                tree.ExpandAll();
            }
            public static void CreateCheckTree(TreeView tree, DataTable dtTree, int leafDepth, int checkDepth)
            {
                string dm = "";
                //tree.ImageSet = TreeViewImageSet.XPFileExplorer;

                Queue<TreeNode> queue = new Queue<TreeNode>();
                DataView dv = dtTree.DefaultView;
                dv.RowFilter = "depth='0'";
                foreach (DataRowView drv in dv)
                {
                    int rdepth = int.Parse(drv["depth"].ToString());
                    TreeNode root = new TreeNode();
                    root.SelectAction = TreeNodeSelectAction.None;
                    root.Value = dm = drv["dm"].ToString();
                    root.Text = (rdepth >= checkDepth ? GetCheckString(dm, "") : "") + "<img class='treeIcon' src='../../images/treeview/root.jpg' height='16px' width='16px'>" + drv["mc"].ToString();
                    root.NavigateUrl = "#";
                    tree.Nodes.Add(root);
                    queue.Enqueue(root);
                }

                while (queue.Count > 0)
                {
                    TreeNode parNode = queue.Dequeue();
                    dv.RowFilter = "sjdm='" + parNode.Value + "'";
                    int count = dv.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int ndepth = int.Parse(dv[i]["depth"].ToString());
                        bool isLeaf = ndepth >= leafDepth;
                        TreeNode node = new TreeNode();
                        node.SelectAction = TreeNodeSelectAction.None;
                        node.Value = dm = dv[i]["dm"].ToString();
                        node.Text = (ndepth >= checkDepth ? GetCheckString(dm, parNode.Value) : "") + "<img class='treeIcon' src='../../images/treeview/" + (isLeaf ? "leaf" : "node") + ".jpg' height='16px' width='16px'>" + dv[i]["mc"].ToString();
                        node.NavigateUrl = "#";
                        parNode.ChildNodes.Add(node);
                        queue.Enqueue(node);
                    }
                }

                dv.RowFilter = "";
                tree.ExpandAll();
            }
        }

        #region TreeItem of DropDownList

        public class ExtTreeItem
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

            public ExtTreeItem(string id, string name, int level, bool canSelect)
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
}