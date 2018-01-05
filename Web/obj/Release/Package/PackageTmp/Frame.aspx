<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frame.aspx.cs" Inherits="Web.Frame" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>党员培养管理系统</title>
    <link type="text/css" rel="stylesheet" href="~/res/css/default.css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server"></f:PageManager>
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
            <Regions>
                <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" Position="Top" Layout="Fit" runat="server">
                    <Items>
                        <f:ContentPanel runat="server" ShowBorder="false">
                            <div id="header">
                                <table>
                                    <tr>
                                        <td>
                                            <img style="margin:3px 0px 0px 10px; border:0px;" src="./res/images/logo/logo.png" alt="首页" />
                                        </td>
                                        <td style="text-align: right;">
                                            <%--<f:Button ID="Button1" runat="server" CssClass="" Text="企业版示例（MVC）" IconAlign="Top" Icon="Lightning"
                                                EnablePostBack="false" OnClientClick="window.location.href='http://fineui.com/demo_mvc';">
                                            </f:Button>
                                            <f:Button ID="Button2" runat="server" CssClass="" Text="专业版示例" IconAlign="Top" Icon="Star"
                                                EnablePostBack="false" OnClientClick="window.location.href='http://fineui.com/demo_pro';">
                                            </f:Button>--%>
                                            <%--<f:Button ID="Button3" runat="server" CssClass="" Text="加载动画" IconAlign="Top" Icon="Hourglass"
                                                EnablePostBack="false">
                                                <Listeners>
                                                    <f:Listener Event="click" Handler="onLoadingSelectClick" />
                                                </Listeners>
                                            </f:Button>--%>                                       
                                            <f:Button runat="server" ID="btnUser" Text="" IconUrl="~/res/images/toolbar/user.ico" IconAlign="Left" EnablePostBack="false">
                                                <Menu ID="Menu1" runat="server">
                                                    <f:MenuButton ID="MenuButton1" Text="个人信息" Icon="User" EnablePostBack="false" runat="server">
                                                        <Listeners>
                                                            <f:Listener Event="click" Handler="onUserProfileClick" />
                                                        </Listeners>
                                                    </f:MenuButton>
                                                    <f:MenuSeparator ID="MenuSeparator2" runat="server"></f:MenuSeparator>
                                                    <f:MenuButton ID="MenuButton2" Text="安全退出" Icon="DoorOut" EnablePostBack="false" runat="server">
                                                        <Listeners>
                                                            <f:Listener Event="click" Handler="onSignOutClick" />
                                                        </Listeners>
                                                    </f:MenuButton>
                                                </Menu>
                                            </f:Button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </f:ContentPanel>
                    </Items>
                </f:Region>
                <f:Region ID="leftPanel" Split="true" Width="200px" ShowHeader="true" Icon="House" Title="系统菜单" EnableCollapse="true" Layout="Fit" Position="Left" runat="server"></f:Region>
                <f:Region ID="mainRegion" ShowHeader="false" Layout="Fit" Position="Center"
                    runat="server">
                    <Items>
                        <f:TabStrip ID="mainTabStrip" EnableTabCloseMenu="true" ShowBorder="false" runat="server">
                            <Tabs>
                                <f:Tab ID="Tab1" Title="首页" Layout="Fit" Icon="House" runat="server">
                                    <Items>
                                        <f:ContentPanel ID="ContentPanel2" ShowBorder="false" BodyPadding="10px" ShowHeader="false" AutoScroll="true"
                                            runat="server">
                                            <h2>欢迎使用 ！</h2>
                                        </f:ContentPanel>
                                    </Items>
                                </f:Tab>
                            </Tabs>
                        </f:TabStrip>
                    </Items>
                </f:Region>
                <f:Region ID="bottomPanel" RegionPosition="Bottom" ShowBorder="false" ShowHeader="false" EnableCollapse="false" runat="server" Layout="Fit">
                    <Items>
                        <f:ContentPanel ID="ContentPanel3" runat="server" ShowBorder="false" ShowHeader="false">
                            <table class="bottomtable">
                                <tr>
                                    <td style="width: 300px;">&nbsp;版本：V<a target="_blank" href="javascript:">v<asp:Literal runat="server" ID="litVersion"></asp:Literal></a></td>
                                    <td style="text-align: center;">浙江树人大学</td>
                                    <td style="width: 300px; text-align: right;">&nbsp;</td>
                                </tr>
                            </table>
                        </f:ContentPanel>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" EnableCaching="false"></asp:XmlDataSource>
    </form>
    <script src="./res/js/jquery.min.js"></script>
    <script>
        var leftPanelClientID = '<%= leftPanel.ClientID %>';
        var mainTabStripClientID = '<%= mainTabStrip.ClientID %>';
        
        function onSignOutClick() {
            var okScript = "top.window.location.href = './logout.aspx?t=<%=DateTime.Now.Ticks%>'";
            F.confirm({message:'确认要退出吗 ？',ok:okScript});            
        }

        function onUserProfileClick() {
            window.F.alert({ message: '尚未实现', messageIcon: Ext.MessageBox.INFO });
        }

        // 页面控件初始化完毕后，会调用用户自定义的onReady函数
        F.ready(function () {
            var leftPanel = F(leftPanelClientID);
            var treeMenu = leftPanel.items.getAt(0);
            var mainTabStrip = F(mainTabStripClientID);

            // 初始化主框架中的树(或者Accordion+Tree)和选项卡互动，以及地址栏的更新
            // treeMenu： 主框架中的树控件实例，或者内嵌树控件的手风琴控件实例
            // mainTabStrip： 选项卡实例
            // updateHash: 切换Tab时，是否更新地址栏Hash值（默认值：true）
            // refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame（默认值：false）
            // refreshWhenTabChange: 切换选项卡时，是否刷新内部IFrame（默认值：false）
            // maxTabCount: 最大允许打开的选项卡数量
            // maxTabMessage: 超过最大允许打开选项卡数量时的提示信息
            F.initTreeTabStrip(treeMenu, mainTabStrip, {
                updateHash: false,
                //refreshWhenExist: true,
                maxTabCount: 10,
                maxTabMessage: '请先关闭一些选项卡（最多允许打开 10 个）！'
            });

            treeMenu.expandAll();
        });
    </script>
</body>
</html>
