using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using FineUI;

namespace Web
{
    public partial class Frame : TStar.Web.BasePage
    {
        private string _menuType = "menu";//"accordion";//
        protected void Page_Init(object sender, EventArgs e)
        {
            HttpCookie menuCookie = Request.Cookies["MenuStyle_v6"];
            if (menuCookie != null)
            {
                _menuType = menuCookie.Value;
            }
            //switch((TStar.Web.Globals.SystemSetting.UserLevel)System.Enum.Parse(typeof(TStar.Web.Globals.SystemSetting.UserLevel), TStar.Web.Globals.Account.UserLevel))
            switch (TStar.Utility.Common.ConvertHelper.EnumParse<TStar.Web.Globals.SystemSetting.UserLevel>(TStar.Web.Globals.Account.UserLevel))
            {
                default:
                case TStar.Web.Globals.SystemSetting.UserLevel.Student :
                    XmlDataSource1.DataFile = "~/res/menu/menuxs.xml";
                    tabHome.IFrameUrl = "~/Home/Homexs.aspx";
                    break;
                case TStar.Web.Globals.SystemSetting.UserLevel.Contacts:
                    XmlDataSource1.DataFile = "~/res/menu/menulxr.xml";
                    tabHome.IFrameUrl = "~/Home/Homelxr.aspx";
                    break;
                case TStar.Web.Globals.SystemSetting.UserLevel.Branch:
                    XmlDataSource1.DataFile = "~/res/menu/menuzb.xml";
                    tabHome.IFrameUrl = "~/Home/Homezb.aspx";
                    break;
                case TStar.Web.Globals.SystemSetting.UserLevel.Committee:
                    XmlDataSource1.DataFile = "~/res/menu/menufdw.xml";
                    tabHome.IFrameUrl = "~/Home/Homefdw.aspx";
                    break;
                case TStar.Web.Globals.SystemSetting.UserLevel.Party:
                    XmlDataSource1.DataFile = "~/res/menu/menuxdw.xml";
                    tabHome.IFrameUrl = "~/Home/Homexdw.aspx";//"~/Tjbb/Xylb.aspx";
                    break;
                case TStar.Web.Globals.SystemSetting.UserLevel.System:
                    XmlDataSource1.DataFile = "~/res/menu/menuxt.xml";
                    break;
            }
            this.btnUser.Text = TStar.Web.Globals.Account.UserIDName;

            if (_menuType == "accordion")
                InitAccordionMenu();
            else
                InitTreeMenu();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litVersion.Text = TStar.Web.Globals.SystemSetting.Version;
            }
        }


        private Accordion InitAccordionMenu()
        {
            Accordion accordionMenu = new Accordion();
            accordionMenu.ID = "accordionMenu";
            accordionMenu.ShowBorder = false;
            accordionMenu.ShowHeader = false;
            leftPanel.Items.Add(accordionMenu);


            XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
            XmlNodeList xmlNodes = xmlDoc.SelectNodes("/Tree/TreeNode");
            foreach (XmlNode xmlNode in xmlNodes)
            {
                if (xmlNode.HasChildNodes)
                {
                    AccordionPane accordionPane = new AccordionPane();
                    accordionPane.Title = xmlNode.Attributes["Text"].Value;
                    accordionPane.Layout = Layout.Fit;
                    accordionPane.ShowBorder = false;

                    var accordionPaneIconAttr = xmlNode.Attributes["Icon"];
                    if (accordionPaneIconAttr != null)
                    {
                        accordionPane.Icon = (Icon)Enum.Parse(typeof(Icon), accordionPaneIconAttr.Value, true);
                    }

                    accordionMenu.Items.Add(accordionPane);

                    Tree innerTree = new Tree();
                    innerTree.ShowBorder = false;
                    innerTree.ShowHeader = false;
                    innerTree.EnableIcons = true;
                    innerTree.AutoScroll = true;
                    innerTree.EnableSingleClickExpand = true;
                    accordionPane.Items.Add(innerTree);

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(String.Format("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Tree>{0}</Tree>", xmlNode.InnerXml));
                    ResolveXmlDocument(doc);

                    // 绑定AccordionPane内部的树控件
                    innerTree.NodeDataBound += treeMenu_NodeDataBound;
                    innerTree.PreNodeDataBound += treeMenu_PreNodeDataBound;
                    innerTree.DataSource = doc;
                    innerTree.DataBind();
                }
            }

            return accordionMenu;
        }

        private Tree InitTreeMenu()
        {
            Tree treeMenu = new Tree();
            treeMenu.ID = "treeMenu";
            treeMenu.ShowBorder = false;
            treeMenu.ShowHeader = false;
            treeMenu.EnableIcons = true;
            treeMenu.AutoScroll = true;
            treeMenu.EnableSingleClickExpand = true;
            leftPanel.Items.Add(treeMenu);

            XmlDocument doc = XmlDataSource1.GetXmlDocument();
            ResolveXmlDocument(doc);

            // 绑定 XML 数据源到树控件
            treeMenu.NodeDataBound += treeMenu_NodeDataBound;
            treeMenu.PreNodeDataBound += treeMenu_PreNodeDataBound;
            treeMenu.DataSource = doc;
            treeMenu.DataBind();

            return treeMenu;
        }

        #region ResolveXmlDocument

        private void ResolveXmlDocument(XmlDocument doc)
        {
            ResolveXmlDocument(doc, doc.DocumentElement.ChildNodes);
        }

        private int ResolveXmlDocument(XmlDocument doc, XmlNodeList nodes)
        {
            // nodes 中渲染到页面上的节点个数
            int nodeVisibleCount = 0;

            foreach (XmlNode node in nodes)
            {
                // Only process Xml elements (ignore comments, etc)
                if (node.NodeType == XmlNodeType.Element)
                {
                    XmlAttribute removedAttr;

                    // 是否叶子节点
                    bool isLeaf = node.ChildNodes.Count == 0;                    

                    // 存在子节点
                    if (!isLeaf)
                    {
                        // 递归
                        int childVisibleCount = ResolveXmlDocument(doc, node.ChildNodes);

                        if (childVisibleCount == 0)
                        {
                            removedAttr = doc.CreateAttribute("Removed");
                            removedAttr.Value = "true";

                            node.Attributes.Append(removedAttr);
                        }
                    }

                    removedAttr = node.Attributes["Removed"];
                    if (removedAttr == null)
                    {
                        nodeVisibleCount++;
                    }
                }
            }

            return nodeVisibleCount;
        }

        #endregion

        /// <summary>
        /// 树节点的绑定后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMenu_NodeDataBound(object sender, FineUI.TreeNodeEventArgs e)
        {
            // 是否叶子节点
            bool isLeaf = e.XmlNode.ChildNodes.Count == 0;

            //string isNewHtml = GetIsNewHtml(e.XmlNode);
            //if (!String.IsNullOrEmpty(isNewHtml))
            //{
            //    e.Node.Text += isNewHtml;
            //}

            if (isLeaf)
            {
                // 设置节点的提示信息
                e.Node.ToolTip = e.Node.Text;
            }
        }
        
        /// <summary>
        /// 树节点的预绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMenu_PreNodeDataBound(object sender, TreePreNodeEventArgs e)
        {
            // 是否叶子节点
            bool isLeaf = e.XmlNode.ChildNodes.Count == 0;

            XmlAttribute removedAttr = e.XmlNode.Attributes["Removed"];
            if (removedAttr != null)
            {
                e.Cancelled = true;
            }
        }
    }
}