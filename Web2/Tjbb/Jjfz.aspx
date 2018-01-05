<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jjfz.aspx.cs" Inherits="Web.Tjbb.Jjfz" %>

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
                <f:Panel ID="pnlGrid" runat="server" Title="积极分子汇总表" ShowBorder="true" ShowHeader="true" BoxFlex="1" Layout="Fit">                    
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
                                <f:BoundField Width="70px" ColumnID="Rdlxrxm1" DataField="Rdlxrxm1" SortField="Rdlxrxm1" HeaderText="联系人" />
                                <f:BoundField Width="50px" ColumnID="Xb" DataField="Xb" SortField="Xb" HeaderText="性别" />
                                <f:BoundField Width="100px" ColumnID="Jg" DataField="Jg" SortField="Jg" HeaderText="籍贯" />
                                <f:BoundField Width="60px" ColumnID="Mz" DataField="Mz" SortField="Mz" HeaderText="民族" />
                                <f:BoundField Width="90px" ColumnID="Csrq" DataField="Csrq" SortField="Csrq" HeaderText="出生年月" />                       
                                <f:BoundField Width="120px" ColumnID="Zw" DataField="Zw" SortField="Zw" HeaderText="职务" />
                                <f:BoundField Width="110px" ColumnID="Sqrdrq" DataField="Sqrdrq" SortField="Sqrdrq" HeaderText="申请入党时间" />
                                <f:BoundField Width="140px" ColumnID="Jjfzrq" DataField="Jjfzrq" SortField="Jjfzrq" HeaderText="确定积极分子时间" />
                                <f:BoundField Width="105px" ColumnID="Dxjyrq" DataField="Dxjyrq" SortField="Dxjyrq" HeaderText="党校结业时间" />
                                <f:BoundField Width="105px" ColumnID="Dxkhzt" DataField="Dxkhzt" SortField="Dxkhzt" HeaderText="党校考核结果" />
                                <f:BoundField Width="105px" ColumnID="Xxcjpm" DataField="Xxcjpm" SortField="Xxcjpm" HeaderText="学习成绩排名" />
                                <f:BoundField Width="105px" ColumnID="Zhszpm" DataField="Zhszpm" SortField="Zhszpm" HeaderText="综合素质排名" />
                                <f:BoundField Width="105px" ColumnID="Bjgms" DataField="Bjgms" SortField="Bjgms" HeaderText="不及格课程数" />
                                <f:BoundField Width="80px" ColumnID="Sxhb" DataField="Sxhb" SortField="Sxhb" HeaderText="思想汇报" />
                                <f:BoundField Width="80px" ColumnID="Zyfw" DataField="Zyfw" SortField="Zyfw" HeaderText="志愿服务" />
                                <f:BoundField Width="80px" ColumnID="Jshj" DataField="Jshj" SortField="Jshj" HeaderText="竞赛获奖" />
                                <f:BoundField Width="80px" ColumnID="Qtxm" DataField="Qtxm" SortField="Qtxm" HeaderText="其他项目" />
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
