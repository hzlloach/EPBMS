<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xmsblb.aspx.cs" Inherits="Web.Xmgl.Xmsblb" %>

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
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />                     
        <f:Panel ID="Panel1" runat="server" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPadding="1" BoxConfigChildMargin="0 0 0 0" ShowBorder="false" ShowHeader="false">
            <Items>                       
                <%--查询条件框--%>        
                <f:Panel ID="pnlCond" runat="server" Title="其他项目列表" ShowBorder="true" ShowHeader="true" EnableCollapse="true">
                    <Items>
                        <f:ContentPanel runat="server" ID="ContentPanel1" ShowBorder="false" ShowHeader="false" BodyPadding="10 10 5 0">
                            <f:DropDownList runat="server" ID="ddlBm" Label="分党委名称" LabelWidth="88" LabelAlign="Right" Width="275px" AutoPostBack="true" OnSelectedIndexChanged="ddlBm_SelectedIndexChanged"></f:DropDownList>
                            <f:DropDownList runat="server" ID="ddlDzb" Label="党支部名称" LabelWidth="88" LabelAlign="Right" Width="280px" AutoPostBack="true" OnSelectedIndexChanged="ddlDzb_SelectedIndexChanged"></f:DropDownList>
                            <f:DropDownList runat="server" ID="ddlBj" Label="班级名称" LabelWidth="75" LabelAlign="Right" Width="225px" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged"></f:DropDownList>
                            <f:DropDownList runat="server" ID="ddlKhzb" Label="项目类别" LabelWidth="75" LabelAlign="Right" Width="225px" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged"></f:DropDownList>
                            <f:DropDownList runat="server" ID="ddlFzzt" Label="发展阶段" LabelWidth="75" LabelAlign="Right" Width="175px" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged">
                                <f:ListItem Text="－请选择－" Value="__" Selected="true" />
                                <f:ListItem Text="积极分子" Value="2" /> 
                                <f:ListItem Text="预备党员" Value="5" /> 
                                <f:ListItem Text="正式党员" Value="6" /> 
                            </f:DropDownList>
                            <f:DropDownList runat="server" ID="ddlZt" Label="审核状态" LabelWidth="75" LabelAlign="Right" Width="175px" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged">
                                <f:ListItem Text="未审核" Value="0" /> 
                                <f:ListItem Text="审核通过" Value="1" Selected="true" /> 
                                <f:ListItem Text="审核拒绝" Value="-1" /> 
                            </f:DropDownList>
                            <f:DropDownList runat="server" ID="ddlLxr" Label="联系人" LabelWidth="62" LabelAlign="Right" Width="180px" EnableEdit="true" EmptyText="可输入关键字" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged"></f:DropDownList>   
                            <f:TwinTriggerBox ID="ttbSearch" runat="server" Label="关键字" LabelWidth="62" LabelAlign="Right" Width="350px" EmptyText="请输入姓名或学号，姓名支持模糊查询" ShowTrigger1="false" Trigger1Icon="Clear" Trigger2Icon="Search" OnTrigger1Click="ttbSearch_Trigger1Click" OnTrigger2Click="ttbSearch_Trigger2Click"></f:TwinTriggerBox>
                            <f:HiddenField ID="tbxBll" runat="server" Text=""></f:HiddenField>
                            <f:HiddenField ID="tbxWhere" runat="server" Text=""></f:HiddenField>
                            <f:HiddenField ID="tbxSort" runat="server" Text=""></f:HiddenField>
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
                        <f:Grid ID="Grid1" runat="server" Title="Grid1" EmptyText="<img src='/res/images/no_data_found.jpg' alt='暂无数据'/>" AllowPaging="true" AllowSorting="true" EnableCheckBoxSelect="false" EnableTextSelection="true" EnableHeaderMenu="false" ShowBorder="false" ShowHeader="False" IsDatabasePaging="true" DataKeyNames="Pkid,Ztdm,Lxrbh" OnPageIndexChange="Grid1_PageIndexChanged" OnSort="Grid1_Sort" OnPreRowDataBound="Grid1_PreRowDataBound" OnRowDataBound="Grid1_RowDataBound">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" MinWidth="45px" />
                                <f:BoundField Width="150px" ColumnID="Bmmc" DataField="Bmmc" HeaderText="分党委名称"/>
                                <f:BoundField Width="180px" ColumnID="Dzbmc" DataField="Dzbmc" SortField="Dzbmc" HeaderText="党支部名称" />
                                <f:BoundField Width="150px" ColumnID="Bjmc" DataField="Bjmc" SortField="Bjmc" HeaderText="班级名称" />
                                <f:BoundField Width="90px" ColumnID="Zbmc" DataField="Zbmc" HeaderText="项目类别"/>
                                <f:BoundField Width="90px" ColumnID="Djmc" DataField="Djmc" SortField="Djmc" HeaderText="项目等级"/>
                                <f:BoundField Width="120px" ColumnID="Xh" DataField="Xh" SortField="Xh" HeaderText="学号" />
                                <f:BoundField Width="100px" ColumnID="Xm" DataField="Xm" SortField="Xm" HeaderText="姓名" />
                                <f:BoundField Width="90px" ColumnID="Lxrxm" DataField="Lxrxm" SortField="Lxrxm" HeaderText="联系人" />
                                <f:BoundField Width="140px" ColumnID="Fzzt" DataField="Fzzt" SortField="Fzzt" HeaderText="发展阶段" Hidden="true" />
                                <f:BoundField Width="100px" ColumnID="Xmrq" DataField="Xmrq" SortField="Xmrq" HeaderText="项目日期" />
                                <f:TemplateField Width="350px" ColumnID="Xmmc" SortField="Xmmc" HeaderText="项目名称">
                                    <ItemTemplate>
                                        <asp:Label ID="lblXmmc" runat="server" Text='<%# Eval("Xmmc").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:WindowField Width="50px" ColumnID="lbfOper" TextAlign="Center" WindowID="wndView" Icon="Magnifier" HeaderText="操作" ToolTip="浏览" DataIFrameUrlFields="Pkid" DataIFrameUrlFormatString="XmsbView.aspx?pkid={0}" Title="弹出窗－浏览项目信息" />
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
    
    <f:Window ID="wndView" runat="server" Title="浏览" WindowPosition="Center"  Target="Self" Width="1000px" Height="700px" EnableMaximize="false" EnableMinimize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true" IsModal="true" Hidden="true" CloseAction="Hide" OnClose="Window_Close">
    </f:Window> 
</body>
</html>
