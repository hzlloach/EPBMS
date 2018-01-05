<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ysdblb.aspx.cs" Inherits="Web.Xmgl.Ysdblb" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title></title>
    <link href="~/res/css/default.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/index.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/fineui.css" rel="stylesheet" type="text/css" />
    <style>
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="pnlFit" runat="server" />                     
        <f:Panel ID="pnlFit" runat="server" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPadding="1" BoxConfigChildMargin="0 0 0 0" ShowBorder="false" ShowHeader="false">
            <Items>                       
                <%--查询条件框--%>        
                <f:Panel runat="server" Title="预审答辩列表" ShowBorder="true" ShowHeader="true" EnableCollapse="true">
                    <Items>
                        <f:ContentPanel runat="server" ID="ContentPanel1" ShowBorder="false" ShowHeader="false" BodyPadding="10 10 5 0">
                            <f:DropDownList runat="server" ID="ddlBm" Label="分党委名称" LabelWidth="88" LabelAlign="Right" Width="275px" AutoPostBack="true" OnSelectedIndexChanged="ddlBm_SelectedIndexChanged"></f:DropDownList>
                            <f:DropDownList runat="server" ID="ddlDzb" Label="党支部名称" LabelWidth="88" LabelAlign="Right" Width="288px" AutoPostBack="true" OnSelectedIndexChanged="ddlDzb_SelectedIndexChanged"></f:DropDownList>
                            <f:DropDownList runat="server" ID="ddlBj" Label="班级名称" LabelWidth="75" LabelAlign="Right" Width="225px" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged"></f:DropDownList>
                            <f:DropDownList runat="server" ID="ddlDbjg" Label="答辩结果" LabelWidth="75" LabelAlign="Right" Width="225px" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged"></f:DropDownList>
                            <f:DropDownList runat="server" ID="ddlLxr" Label="联系人" LabelWidth="62" LabelAlign="Right" Width="180px" EnableEdit="true" EmptyText="可输入关键字" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged"></f:DropDownList>   
                            <f:TwinTriggerBox ID="ttbSearch" runat="server" Label="关键字" LabelWidth="62" LabelAlign="Right" Width="350px" EmptyText="请输入姓名或学号，姓名支持模糊查询" ShowTrigger1="false" Trigger1Icon="Clear" Trigger2Icon="Search" OnTrigger1Click="ttbSearch_Trigger1Click" OnTrigger2Click="ttbSearch_Trigger2Click"></f:TwinTriggerBox>
                            <f:HiddenField runat="server" ID="tbxBll" Text=""></f:HiddenField>
                            <f:HiddenField runat="server" ID="tbxWhere" Text=""></f:HiddenField>
                            <f:HiddenField runat="server" ID="tbxSort" Text=""></f:HiddenField>
                            <f:Panel ID="pnlFrame" runat="server" ShowBorder="false" ShowHeader="false" Title="" Width="1" Height="1" EnableIFrame="true" EnableAjax="true" EnableAjaxLoading="true"></f:Panel>        
                        </f:ContentPanel>
                    </Items>
                </f:Panel> 

                <f:Panel ID="pnlGrid" runat="server" ShowBorder="true" ShowHeader="false" BoxFlex="1" Layout="Fit" CssClass="gridpanel">        
                    <Toolbars>
                        <f:Toolbar runat="server">
                            <Items>
                                <f:Button ID="btnBack" runat="server" Text="返回" Icon="DoorOut" Hidden="true"></f:Button>
                                <f:Button ID="btnExport" runat="server" Text="导出" Icon="PageWhiteExcel" ConfirmText="确认要导出数据吗 ？" OnClick="btnExport_Click"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Grid ID="Grid1" runat="server" Title="Grid1" EmptyText="<img src='/res/images/no_data_found.jpg' alt='暂无数据'/>" AllowPaging="true" AllowSorting="true" EnableCheckBoxSelect="false" EnableTextSelection="true" EnableHeaderMenu="false" ShowBorder="false" ShowHeader="False" IsDatabasePaging="true" DataKeyNames="Pkid" OnPageIndexChange="Grid1_PageIndexChanged" OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand" OnRowDataBound="Grid1_RowDataBound">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" MinWidth="45px"/>
                                <f:BoundField Width="150px" ColumnID="Bmmc" DataField="Bmmc" HeaderText="分党委名称"/>
                                <f:BoundField Width="180px" ColumnID="Dzbmc" DataField="Dzbmc" SortField="Dzbmc" HeaderText="党支部名称"/>
                                <f:BoundField Width="150px" ColumnID="Bjmc" DataField="Bjmc" SortField="Bjmc" HeaderText="班级名称" />
                                <f:BoundField Width="120px" ColumnID="Xh" DataField="Xh" SortField="Xh" HeaderText="学号" />
                                <f:BoundField Width="100px" ColumnID="Xm" DataField="Xm" SortField="Xm" HeaderText="姓名" />
                                <f:BoundField Width="90px" ColumnID="Lxrxm" DataField="Lxrxm" SortField="Lxrxm" HeaderText="联系人" />
                                <f:TemplateField Width="80px" HeaderText="答辩结果">
                                    <ItemTemplate>
                                        <asp:Label ID="lblZtmc" runat="server" Text='<%# (Eval("Dbjg").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:WindowField Width="50px" ColumnID="lbfView" TextAlign="Center" WindowID="wndView" Icon="Magnifier" HeaderText="操作" ToolTip="浏览" DataIFrameUrlFields="Pkid" DataIFrameUrlFormatString="ShowYsdb.aspx?pkid={0}" Title="弹出窗－浏览预审答辩" />
                                <f:LinkButtonField Width="50px" ColumnID="lbfDelete" Hidden="true" TextAlign="Center" Icon="BulletCross" ToolTip="删除" HeaderText="操作" ConfirmText="确认删除？" CommandArgument="Pkid" CommandName="Delete"/>
                                <f:BoundField ExpandUnusedSpace="true" DataField="space" />
                            </Columns>
                            <PageItems>
                                <f:ToolbarSeparator runat="server"> </f:ToolbarSeparator>
                                <f:ToolbarText runat="server" Text="每页记录数："></f:ToolbarText>
                                <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="Grid1_PageSizeChanged">
                                    <f:ListItem Text="10" Value="10" />
                                    <f:ListItem Text="15" Value="15" />
                                    <f:ListItem Text="20" Value="20" />
                                    <f:ListItem Text="30" Value="30" />
                                    <f:ListItem Text="50" Value="50" />
                                </f:DropDownList>
                            </PageItems>
                        </f:Grid>
                    </Items>
                </f:Panel>   
            </Items>
        </f:Panel>
    </form>

    <f:Window ID="wndEdit" runat="server" Title="弹出窗－新增" WindowPosition="Center" Target="Self" Width="860px" Height="670px" EnableMaximize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true" IsModal="true" Hidden="true" CloseAction="Hide">
    </f:Window> 
    <f:Window ID="wndView" runat="server" Title="弹出窗－浏览" WindowPosition="Center" Target="Self" Width="1000px" Height="350px" EnableMaximize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true"  IsModal="true" Hidden="true" CloseAction="Hide">
    </f:Window>  
</body>
</html>
