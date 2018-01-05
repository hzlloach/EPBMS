<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zyfwlbgr.aspx.cs" Inherits="Web.Xmgl.Zyfwlbgr" %>

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
                <f:Panel ID="pnlCond" runat="server" Title="志愿服务" Hidden="true" ShowBorder="true" ShowHeader="false" EnableCollapse="true">
                    <Items>
                        <f:ContentPanel runat="server" ID="ContentPanel1" ShowBorder="false" ShowHeader="false" BodyPadding="5 15 2 0">
                            <f:DropDownList runat="server" ID="ddlKhzb" Label="项目类别" LabelWidth="75" LabelAlign="Right" Width="275px" AutoPostBack="true" OnSelectedIndexChanged="ddlKhzb_SelectedIndexChanged"></f:DropDownList>
                            <f:HiddenField runat="server" ID="tbxBll" Text=""></f:HiddenField>
                            <f:HiddenField runat="server" ID="tbxWhere" Text=""></f:HiddenField>
                            <f:HiddenField runat="server" ID="tbxSort" Text=""></f:HiddenField>
                            <div class="divHidden"></div>                            
                        </f:ContentPanel>
                    </Items>
                </f:Panel> 

                <f:Panel ID="pnlGrid" runat="server" ShowBorder="true" ShowHeader="false" BoxFlex="1" Layout="Fit" CssClass="gridpanel"> 
                    <Items>
                        <f:Grid ID="Grid1" runat="server" Title="Grid1" EmptyText="<img src='/res/images/no_data_found.jpg' alt='暂无数据'/>" AllowPaging="true" AllowSorting="true" EnableCheckBoxSelect="false" EnableTextSelection="true" EnableHeaderMenu="false" ShowBorder="false" ShowHeader="False" IsDatabasePaging="true" DataKeyNames="Pkid" OnPageIndexChange="Grid1_PageIndexChanged" OnSort="Grid1_Sort">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" MinWidth="40px" />
                                <f:BoundField Width="90px" ColumnID="Xmrq" DataField="Xmrq" SortField="Xmrq" HeaderText="服务日期" />
                                <f:BoundField Width="60px" ColumnID="Jlsl" DataField="Jlsl" HeaderText="时数" />
                                <f:TemplateField Width="350px" ColumnID="Xmmc" SortField="Xmmc" HeaderText="服务内容">
                                    <ItemTemplate>
                                        <asp:Label ID="lblXmmc" runat="server" Text='<%# Eval("Xmmc").ToString().Substring(11) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:WindowField Width="50px" ColumnID="lbfView" TextAlign="Center" WindowID="wndView" Icon="Magnifier" HeaderText="浏览" ToolTip="浏览" DataIFrameUrlFields="Pkid" DataIFrameUrlFormatString="./ZyfwView.aspx?pkid={0}" Title="浏览志愿服务" />
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
    
    <f:Window ID="wndView" runat="server" Title="浏览" WindowPosition="Center"  Target="Self" Width="1000px" Height="700px" EnableMaximize="false" EnableMinimize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true" IsModal="true" Hidden="true" CloseAction="Hide">
    </f:Window> 
</body>
</html>
