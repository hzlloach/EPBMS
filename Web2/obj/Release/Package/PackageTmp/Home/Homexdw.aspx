﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Homexdw.aspx.cs" Inherits="Web.Home.Homexdw" %>


<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title></title>
    <link href="~/res/css/default.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/index.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/fineui.css" rel="stylesheet" type="text/css" />
    <style>
        .x-grid-item .x-grid-cell-Bmmc a {
            /*background-color: #b200ff;*/
            color: maroon;
            text-decoration: none;
        }
        .x-grid-item .x-grid-cell-JjfzRs a {
            /*background-color: #b200ff;*/
            color: blue;
            text-decoration: none;
        }
        .x-grid-item .x-grid-cell-YbdyRs a {
            /*background-color: #b200ff;*/
            color: blue;
            text-decoration: none;
        }
        .x-grid-item .x-grid-cell-ZsdyRs a {
            /*background-color: #b200ff;*/
            color: blue;
            text-decoration: none;
        }
        .x-grid-item .x-grid-cell-Dbs a {
            /*background-color: #b200ff;*/
            color: black;
            text-decoration: none;
        } 
        .x-grid-item .x-grid-cell-JjfzHbs a {
            /*background-color: #b200ff;*/
            color: green;
            text-decoration: none;
        } 
        .x-grid-item .x-grid-cell-YbdyHbs a {
            /*background-color: #b200ff;*/
            color: green;
            text-decoration: none;
        } 
        .x-grid-item .x-grid-cell-ZsdyHbs a {
            /*background-color: #b200ff;*/
            color: green;
            text-decoration: none;
        } 
        .x-grid-item .x-grid-cell-BjLxs a {
            /*background-color: #b200ff;*/
            color: red;
            text-decoration: none;
        } 
        .x-grid-item .x-grid-cell-QsLxs a {
            /*background-color: #b200ff;*/
            color: red;
            text-decoration: none;
        } 
        .x-grid-item .x-grid-cell-XsLxs a {
            /*background-color: #b200ff;*/
            color: red;
            text-decoration: none;
        } 
        .x-grid-item .x-grid-cell-JjfzFws a {
            /*background-color: #b200ff;*/
            color: black;
            text-decoration: none;
        } 
        .x-grid-item .x-grid-cell-YbdyFws a {
            /*background-color: #b200ff;*/
            color: black;
            text-decoration: none;
        } 
        .x-grid-item .x-grid-cell-ZsdyFws a {
            /*background-color: #b200ff;*/
            color: black;
            text-decoration: none;
        } 
        .x-grid-item .x-grid-cell-YjHjs a {
            /*background-color: #b200ff;*/
            color: black;
            text-decoration: none;
        } 
        .x-grid-item .x-grid-cell-XjHjs a {
            /*background-color: #b200ff;*/
            color: black;
            text-decoration: none;
        } 
        .x-grid-item .x-grid-cell-SjHjs a {
            /*background-color: #b200ff;*/
            color: black;
            text-decoration: none;
        } 
        .x-grid-item .x-grid-cell-GjHjs a {
            /*background-color: #b200ff;*/
            color: black;
            text-decoration: none;
        } 
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />                     
        <f:Panel ID="Panel1" runat="server" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPadding="1" BoxConfigChildMargin="0 0 0 0" ShowBorder="false" ShowHeader="false">
            <Items>
                <%--生源信息列表--%>
                <f:Panel ID="pnlGrid" runat="server" ShowBorder="false" ShowHeader="true" Title="全校概况" BoxFlex="1" BodyPadding="0 1 0 0" Layout="Fit">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server" Hidden="true">
                            <Items>
                                <f:Button ID="btnAddNew" runat="server" Text="新增" Icon="Add" Hidden="false"></f:Button>
                                <f:Button ID="btnDeleteSel" runat="server" Text="删除" Icon="Delete" Hidden="true"></f:Button>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server" Hidden="true"></f:ToolbarSeparator>
                                <f:Button ID="btnImport" runat="server" Text="导入" Icon="DatabaseGo" Hidden="true"></f:Button>
                                <f:Button ID="btnExport" runat="server" Text="导出" Icon="PageWhiteExcel" Enabled="true"></f:Button>
                                <f:HiddenField runat="server" ID="tbxBll" Text=""></f:HiddenField>
                                <f:HiddenField runat="server" ID="tbxWhere" Text=""></f:HiddenField>
                                <f:HiddenField runat="server" ID="tbxSort" Text=""></f:HiddenField>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Grid ID="grdXy" runat="server" Title="Grid1" EmptyText="<img src='/res/images/no_data_found.jpg' alt='暂无数据'/>" AllowPaging="false" AllowSorting="false" EnableCheckBoxSelect="false" EnableTextSelection="true" EnableHeaderMenu="false" ShowBorder="true" ShowHeader="false" IsDatabasePaging="false" DataKeyNames="Bmbh">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" MinWidth="40px" />                                
                                <f:HyperLinkField Width="170px" ColumnID="Bmmc" DataTextField="Bmmc" HeaderText="二级党组织名称" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Home/Homefdw.aspx?bmbh={0}" Target="_self"/>
                                <f:BoundField Width="70px" ColumnID="Zbs" DataField="Zbs" HeaderText="支部数" TextAlign="Center"/> <%--SortField="Zbs"--%>
                                <f:GroupField HeaderText="培养人数" TextAlign="Center">
                                    <Columns>
                                        <f:HyperLinkField Width="80px" ColumnID="JjfzRs" DataTextField="JjfzRs" HeaderText="积极分子" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Fzgl/Xslb.aspx?bmbh={0}&fzztdm=2" Target="_self"/> <%--SortField="JjfzRs"--%>
                                        <f:HyperLinkField Width="80px" ColumnID="YbdyRs" DataTextField="YbdyRs" HeaderText="预备党员" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Fzgl/Xslb.aspx?bmbh={0}&fzztdm=5" Target="_self"/> <%--SortField="YbdyRs"--%>
                                        <f:HyperLinkField Width="80px" ColumnID="ZsdyRs" DataTextField="ZsdyRs" HeaderText="正式党员" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Fzgl/Xslb.aspx?bmbh={0}&fzztdm=6" Target="_self"/> <%--SortField="ZsdyRs"--%>
                                        <f:HyperLinkField Width="80px" ColumnID="XjRs" DataTextField="XjRs" HeaderText="小计" Hidden="true" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Fzgl/Xslb.aspx?bmbh={0}&fzztdm=6" Target="_self"/> <%--SortField="ZsdyRs"--%>
                                    </Columns>
                                </f:GroupField>
                                <f:HyperLinkField Width="70px" ColumnID="Dbs" DataTextField="Dbs" HeaderText="预审答<br/>辩次数" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Xmgl/Ysdblb.aspx?bmbh={0}" Target="_self"/>
                                <f:GroupField HeaderText="思想汇报" TextAlign="Center">
                                    <Columns>
                                        <f:HyperLinkField Width="80px" ColumnID="JjfzHbs" DataTextField="JjfzHbs" HeaderText="积极分子" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Xmgl/Sxhblb.aspx?bmbh={0}&fzztdm=2" Target="_self"/> <%--SortField="JjfzRs"--%>
                                        <f:HyperLinkField Width="80px" ColumnID="YbdyHbs" DataTextField="YbdyHbs" HeaderText="预备党员" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Xmgl/Sxhblb.aspx?bmbh={0}&fzztdm=5" Target="_self"/> <%--SortField="YbdyRs"--%>
                                    </Columns>
                                </f:GroupField>
                                <f:GroupField HeaderText="志愿服务" TextAlign="Center">
                                    <Columns>
                                        <f:HyperLinkField Width="80px" ColumnID="JjfzFws" DataTextField="JjfzFws" HeaderText="积极分子" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Xmgl/Zyfwlb.aspx?bmbh={0}&fzztdm=2" Target="_self"/> <%--SortField="JjfzRs"--%>
                                        <f:HyperLinkField Width="80px" ColumnID="YbdyFws" DataTextField="YbdyFws" HeaderText="预备党员" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Xmgl/Zyfwlb.aspx?bmbh={0}&fzztdm=5" Target="_self"/> <%--SortField="YbdyRs"--%>
                                        <f:HyperLinkField Width="80px" ColumnID="ZsdyFws" DataTextField="ZsdyFws" HeaderText="正式党员" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Xmgl/Zyfwlb.aspx?bmbh={0}&fzztdm=6" Target="_self"/> <%--SortField="ZsdyRs"--%>
                                        <f:HyperLinkField Width="80px" ColumnID="DyFws" DataTextField="DyFws" HeaderText="党员" Hidden="true" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Xmgl/Sxhblb.aspx?bmbh={0}&fzztdm=5" Target="_self"/> <%--SortField="YbdyRs"--%>
                                    </Columns>
                                </f:GroupField>
                                <f:GroupField HeaderText="党员“三联系”" TextAlign="Center">
                                    <Columns>
                                        <f:HyperLinkField Width="80px" ColumnID="BjLxs" DataTextField="BjLxs" HeaderText="联系班级" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Xmgl/Slxlb.aspx?bmbh={0}&lbdm=1" Target="_self"/> <%--SortField="BjLxs"--%>
                                        <f:HyperLinkField Width="80px" ColumnID="QsLxs" DataTextField="QsLxs" HeaderText="联系寝室" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Xmgl/Slxlb.aspx?bmbh={0}&lbdm=2" Target="_self"/> <%--SortField="QsLxs"--%>
                                        <f:HyperLinkField Width="80px" ColumnID="XsLxs" DataTextField="XsLxs" HeaderText="联系学生" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Xmgl/Slxlb.aspx?bmbh={0}&lbdm=3" Target="_self"/> <%--SortField="XsLxs"--%>
                                    </Columns>
                                </f:GroupField>
                                <f:GroupField HeaderText="获奖情况" TextAlign="Center">
                                    <Columns>
                                        <f:HyperLinkField Width="50px" ColumnID="YjHjs" DataTextField="YjHjs" HeaderText="院级" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Xmgl/Jshjlb.aspx?bmbh={0}&lbdm=4" Target="_self"/> <%--SortField="YjHjs"--%>
                                        <f:HyperLinkField Width="50px" ColumnID="XjHjs" DataTextField="XjHjs" HeaderText="校级" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Xmgl/Jshjlb.aspx?bmbh={0}&lbdm=3" Target="_self"/> <%--SortField="XjHjs"--%>
                                        <f:HyperLinkField Width="50px" ColumnID="SjHjs" DataTextField="SjHjs" HeaderText="省级" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Xmgl/Jshjlb.aspx?bmbh={0}&lbdm=2" Target="_self"/> <%--SortField="YsHjs"--%>
                                        <f:HyperLinkField Width="65px" ColumnID="GjHjs" DataTextField="GjHjs" HeaderText="国家级" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Xmgl/Jshjlb.aspx?bmbh={0}&lbdm=1" Target="_self"/> <%--SortField="YsHjs"--%>
                                        <f:HyperLinkField Width="80px" ColumnID="YsHjs" DataTextField="YsHjs" HeaderText="校级以上" Hidden="true" TextAlign="Center" DataNavigateUrlFields="Bmbh" DataNavigateUrlFormatString="/Xmgl/Jshjlb.aspx?bmbh={0}&lbdm=2" Target="_self"/> <%--SortField="YsHjs"--%>
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
