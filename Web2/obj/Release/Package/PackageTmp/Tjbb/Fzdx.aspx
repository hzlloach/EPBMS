<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fzdx.aspx.cs" Inherits="Web.Tjbb.Fzdx" %>

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
                <%--生源信息列表--%>
                <f:Panel ID="pnlGrid" runat="server" Title="发展对象汇总表" ShowBorder="true" ShowHeader="true" BoxFlex="1" Layout="Fit">                    
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                <f:Button ID="btnExport" runat="server" Text="导出" Icon="PageWhiteExcel" ConfirmText="确认要导出数据吗 ？" OnClick="btnExport_Click"></f:Button>
                                <f:HiddenField ID="tbxBll" runat="server" Text=""></f:HiddenField>
                                <f:HiddenField ID="tbxWhere" runat="server" Text=""></f:HiddenField>
                                <f:HiddenField ID="tbxSort" runat="server" Text=""></f:HiddenField>
                                <f:Panel ID="pnlFrame" runat="server" ShowBorder="false" ShowHeader="false" Title="" Width="1" Height="1" EnableIFrame="true" EnableAjax="true" EnableAjaxLoading="true"></f:Panel>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Grid ID="Grid1" runat="server" Title="Grid1" EmptyText="<img src='/res/images/no_data_found.jpg' alt='暂无数据'/>" AllowPaging="true" AllowSorting="true" EnableCheckBoxSelect="false" EnableTextSelection="true" EnableHeaderMenu="false" ShowBorder="false" ShowHeader="False" IsDatabasePaging="true" DataKeyNames="Pkid" OnPageIndexChange="Grid1_PageIndexChanged" OnSort="Grid1_Sort">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" MinWidth="45px" />
                                <f:BoundField Width="150px" ColumnID="Bmmc" DataField="Bmmc" HeaderText="分党委名称"/>
                                <f:BoundField Width="150px" ColumnID="Dzbmc" DataField="Dzbmc" SortField="Dzbmc" HeaderText="党支部名称" />                            
                                <f:BoundField Width="110px" ColumnID="Bjmc" DataField="Bjmc" SortField="Bjmc" HeaderText="班级" />   
                                <f:BoundField Width="110px" ColumnID="Xh" DataField="Xh" SortField="Xh" HeaderText="学号" />
                                <f:BoundField Width="70px" ColumnID="Xm" DataField="Xm" SortField="Xm" HeaderText="姓名" />
                                <f:BoundField Width="70px" ColumnID="Lxrxm" DataField="Lxrxm" SortField="Lxrxm" HeaderText="联系人" />
                                <f:BoundField Width="50px" ColumnID="Xb" DataField="Xb" SortField="Xb" HeaderText="性别" />
                                <f:BoundField Width="100px" ColumnID="Jg" DataField="Jg" SortField="Jg" HeaderText="籍贯" />
                                <f:BoundField Width="60px" ColumnID="Mz" DataField="Mz" SortField="Mz" HeaderText="民族" />
                                <f:BoundField Width="90px" ColumnID="Csrq" DataField="Csrq" SortField="Csrq" HeaderText="出生年月" />                       
                                <f:BoundField Width="120px" ColumnID="Zw" DataField="Zw" SortField="Zw" HeaderText="职务" />
                                <f:BoundField Width="110px" ColumnID="Sqrdrq" DataField="Sqrdrq" SortField="Sqrdrq" HeaderText="申请入党时间" />
                                <f:BoundField Width="140px" ColumnID="Jjfzrq" DataField="Jjfzrq" SortField="Jjfzrq" HeaderText="确定积极分子时间" />
                                <f:BoundField Width="140px" ColumnID="Fzdxrq" DataField="Fzdxrq" SortField="Fzdxrq" HeaderText="确定发展对象时间" />
                                <f:BoundField Width="80px" ColumnID="Zsjg" DataField="Zsjg" SortField="Zsjg" HeaderText="政审情况" />
                                <f:BoundField Width="110px" ColumnID="Dbjg" DataField="Dbjg" SortField="Dbjg" HeaderText="党委预审情况" />
                                <f:BoundField Width="140px" ColumnID="Zbdhrq" DataField="Zbdhrq" SortField="Zbdhrq" HeaderText="支部大会通过时间" />
                                <f:BoundField Width="200px" ColumnID="Dzzsyqk" DataField="Dzzsyqk" SortField="Dzzsyqk" HeaderText="党总支审议情况" />
                                <f:BoundField Width="200px" ColumnID="Dwspyj" DataField="Dwspyj" SortField="Dwspyj" HeaderText="党委审批意见" />
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
</body>
</html>
