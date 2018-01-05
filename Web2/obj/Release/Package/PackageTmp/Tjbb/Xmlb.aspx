<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xmlb.aspx.cs" Inherits="Web.Tjbb.Xmlb" %>

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
        <f:Panel ID="pnlFit" runat="server" Layout="Fit" BodyPadding="1" ShowBorder="true" ShowHeader="false">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnExport" runat="server" Text="导出" Icon="PageWhiteExcel"></f:Button>
                        <f:DropDownList runat="server" ID="ddlBmbh" Label="学院名称" Hidden="true" LabelWidth="75" LabelAlign="Right" Width="225px" AutoPostBack="true"></f:DropDownList>
                        <f:DropDownList runat="server" ID="ddlDzbbh" Label="党支部名称" Hidden="true" LabelWidth="88" LabelAlign="Right" Width="225px" AutoPostBack="true"></f:DropDownList>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <f:HiddenField runat="server" ID="tbxBll" Text=""></f:HiddenField>
                        <f:HiddenField runat="server" ID="tbxWhere" Text=""></f:HiddenField>
                        <f:HiddenField runat="server" ID="tbxSort" Text=""></f:HiddenField>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:TabStrip runat="server" ID="tabStrip1" TabPosition="Top" ActiveTabIndex="0" ShowBorder="true" EnableTabCloseMenu="false">
                    <Tabs>
                        <f:Tab runat="server" ID="tabXmxx" Title="按学院统计" BodyPadding="10" Layout="Fit">
                            <Items>
                                <f:Grid ID="grdXy" runat="server" Title="Grid1" EmptyText="<img src='/res/images/no_data_found.jpg' alt='暂无数据'/>" AllowPaging="false" AllowSorting="true" EnableCheckBoxSelect="false" EnableTextSelection="true" EnableHeaderMenu="false" ShowBorder="false" ShowHeader="false" IsDatabasePaging="false" DataKeyNames="Pkid" OnSort="Grid1_Sort">
                                    <Columns>
                                        <f:RowNumberField EnablePagingNumber="true" />
                                        <f:BoundField Width="250px" ColumnID="Mc1" DataField="Bmmc" HeaderText="学院名称"/>
                                        <f:BoundField Width="100px" ColumnID="Rs1" DataField="Rs" HeaderText="人数" Hidden="true"/>
                                        <f:BoundField Width="100px" ColumnID="Jjfz1" DataField="Jjfz" HeaderText="积极分子"/>
                                        <f:BoundField Width="100px" ColumnID="Ybdy1" DataField="Ybdy" HeaderText="预备党员"/>
                                        <f:BoundField Width="100px" ColumnID="Zsdy1" DataField="Zsdy" HeaderText="正式党员"/>
                                        <f:BoundField Width="100px" ColumnID="Xj1" DataField="Xj" HeaderText="小计"/>
                                        <f:BoundField ExpandUnusedSpace="true" DataField="space" />
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:Tab>
                        <f:Tab runat="server" ID="tabZmcl" Title="按学院党支部统计" BodyPadding="10" Layout="Fit">
                            <Items> 
                                <f:Grid ID="grdXydzb" runat="server" Title="Grid1" EmptyText="<img src='/res/images/no_data_found.jpg' alt='暂无数据'/>" AllowPaging="false" AllowSorting="true" EnableCheckBoxSelect="false" EnableTextSelection="true" EnableHeaderMenu="false" ShowBorder="false" ShowHeader="false" IsDatabasePaging="false" DataKeyNames="Pkid" OnSort="Grid1_Sort">
                                    <Columns>
                                        <f:RowNumberField EnablePagingNumber="true" />
                                        <f:BoundField Width="250px" ColumnID="Mc2" DataField="Dzbmc" HeaderText="党支部名称"/>
                                        <f:BoundField Width="100px" ColumnID="Rs2" DataField="Rs" HeaderText="人数" Hidden="true"/>
                                        <f:BoundField Width="100px" ColumnID="Jjfz2" DataField="Jjfz" HeaderText="积极分子"/>
                                        <f:BoundField Width="100px" ColumnID="Ybdy2" DataField="Ybdy" HeaderText="预备党员"/>
                                        <f:BoundField Width="100px" ColumnID="Zsdy2" DataField="Zsdy" HeaderText="正式党员"/>
                                        <f:BoundField Width="100px" ColumnID="Xj2" DataField="Xj" HeaderText="小计"/>
                                        <f:BoundField ExpandUnusedSpace="true" DataField="space" />
                                    </Columns>
                                </f:Grid>                 
                            </Items>
                        </f:Tab>
                        <f:Tab runat="server" ID="tab1" Title="按学院专业统计" BodyPadding="10" Layout="Fit">
                            <Items> 
                                <f:Grid ID="grdXyzy" runat="server" Title="Grid1" EmptyText="<img src='/res/images/no_data_found.jpg' alt='暂无数据'/>" AllowPaging="false" AllowSorting="true" EnableCheckBoxSelect="false" EnableTextSelection="true" EnableHeaderMenu="false" ShowBorder="false" ShowHeader="false" IsDatabasePaging="false" DataKeyNames="Pkid" OnSort="Grid1_Sort">
                                    <Columns>
                                        <f:RowNumberField EnablePagingNumber="true" />
                                        <f:BoundField Width="250px" ColumnID="Mc3" DataField="Zymc" HeaderText="专业名称"/>
                                        <f:BoundField Width="100px" ColumnID="Rs3" DataField="Rs" HeaderText="人数" Hidden="true"/>
                                        <f:BoundField Width="100px" ColumnID="Jjfz3" DataField="Jjfz" HeaderText="积极分子"/>
                                        <f:BoundField Width="100px" ColumnID="Ybdy3" DataField="Ybdy" HeaderText="预备党员"/>
                                        <f:BoundField Width="100px" ColumnID="Zsdy3" DataField="Zsdy" HeaderText="正式党员"/>
                                        <f:BoundField Width="100px" ColumnID="Xj3" DataField="Xj" HeaderText="小计"/>
                                        <f:BoundField ExpandUnusedSpace="true" DataField="space" />
                                    </Columns>
                                </f:Grid>                 
                            </Items>
                        </f:Tab>
                    </Tabs>
                </f:TabStrip>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
