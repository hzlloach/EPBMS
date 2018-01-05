<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zblxr.aspx.cs" Inherits="Web.Tjbb.Zblxr" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title></title>
    <link href="~/res/css/default.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/index.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/fineui.css" rel="stylesheet" type="text/css" />
    <style>
        .x-grid-item .x-grid-cell-JjfzRs1 {
            /*background-color: #b200ff;*/
            color: blue;
        }
        .x-grid-item .x-grid-cell-YbdyRs1 {
            /*background-color: #b200ff;*/
            color: blue;
        }
        .x-grid-item .x-grid-cell-ZsdyRs1 {
            /*background-color: #b200ff;*/
            color: blue;
        }
        .x-grid-item .x-grid-cell-JjfzFws1 {
            /*background-color: #b200ff;*/
            color: green;
        } 
        .x-grid-item .x-grid-cell-DyFws1 {
            /*background-color: #b200ff;*/
            color: green;
        } 
        .x-grid-item .x-grid-cell-YjHjs1 {
            /*background-color: #b200ff;*/
            color: red;
        } 
        .x-grid-item .x-grid-cell-XjHjs1 {
            /*background-color: #b200ff;*/
            color: red;
        } 
        .x-grid-item .x-grid-cell-YsHjs1 {
            /*background-color: #b200ff;*/
            color: red;
        } 
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />                     
        <f:Panel ID="Panel1" runat="server" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPadding="1" BoxConfigChildMargin="0 0 0 0" ShowBorder="false" ShowHeader="false">
            <Items>
                <%--生源信息列表--%>
                <f:Panel ID="pnlGrid" runat="server" Title="总表" ShowBorder="true" ShowHeader="true" BoxFlex="1" Layout="Fit">                    
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
                        <f:Grid ID="grid1" runat="server" Title="Grid1" EmptyText="<img src='/res/images/no_data_found.jpg' alt='暂无数据'/>" AllowPaging="false" AllowSorting="false" EnableCheckBoxSelect="false" EnableTextSelection="true" EnableHeaderMenu="false" ShowBorder="false" ShowHeader="false" IsDatabasePaging="false" DataKeyNames="Pkid" OnSort="Grid1_Sort">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" MinWidth="40px" />
                                <f:BoundField Width="100px" ColumnID="Xm1" DataField="Xm" HeaderText="联系人"/>
                                <f:GroupField HeaderText="入党积极分子" TextAlign="Center">
                                    <Columns>
                                        <f:BoundField Width="60px" ColumnID="JjfzRs1" DataField="JjfzRs" HeaderText="人数" TextAlign="Center" /> <%--SortField="JjfzRs"--%>
                                        <f:BoundField Width="110px" ColumnID="JjfzFws1" DataField="JjfzFws" HeaderText="志愿服务时数" TextAlign="Center" /> <%--SortField="JjfzFws"--%>
                                    </Columns>
                                </f:GroupField>
                                <f:BoundField Width="70px" ColumnID="Dbs1" DataField="Dbs" HeaderText="预审答<br/>辩次数" TextAlign="Center"/> <%--SortField="Dbs"--%>
                                <f:GroupField HeaderText="党员人数" TextAlign="Center">
                                    <Columns>
                                        <f:BoundField Width="80px" ColumnID="YbdyRs1" DataField="YbdyRs" HeaderText="预备党员" TextAlign="Center" /> <%--SortField="YbdyRs"--%>
                                        <f:BoundField Width="80px" ColumnID="ZsdyRs1" DataField="ZsdyRs" HeaderText="正式党员" TextAlign="Center" /> <%--SortField="ZsdyRs"--%>
                                    </Columns>
                                </f:GroupField>
                                <f:BoundField Width="80px" ColumnID="DyFws1" DataField="DyFws" HeaderText="党员志愿<br/>服务时数" TextAlign="Center"/> <%--SortField="DyFws"--%>
                                <f:GroupField HeaderText="党员“三联系”" TextAlign="Center">
                                    <Columns>
                                        <f:BoundField Width="80px" ColumnID="BjLxs1" DataField="BjLxs" HeaderText="联系班级" TextAlign="Center" /> <%--SortField="BjLxs"--%>
                                        <f:BoundField Width="80px" ColumnID="QsLxs1" DataField="QsLxs" HeaderText="联系寝室" TextAlign="Center" /> <%--SortField="QsLxs"--%>
                                        <f:BoundField Width="80px" ColumnID="XsLxs1" DataField="XsLxs" HeaderText="联系学生" TextAlign="Center" /> <%--SortField="XsLxs"--%>
                                    </Columns>
                                </f:GroupField>
                                <f:GroupField HeaderText="党员获奖情况" TextAlign="Center">
                                    <Columns>
                                        <f:BoundField Width="50px" ColumnID="YjHjs1" DataField="YjHjs" HeaderText="院级" TextAlign="Center" /> <%--SortField="YjHjs"--%>
                                        <f:BoundField Width="50px" ColumnID="XjHjs1" DataField="XjHjs" HeaderText="校级" TextAlign="Center" /> <%--SortField="XjHjs"--%>
                                        <f:BoundField Width="80px" ColumnID="YsHjs1" DataField="YsHjs" HeaderText="校级以上" TextAlign="Center" /> <%--SortField="YsHjs"--%>
                                    </Columns>
                                </f:GroupField>
                                <f:BoundField ExpandUnusedSpace="true" DataField="space" />
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
