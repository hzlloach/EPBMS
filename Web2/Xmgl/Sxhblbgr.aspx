<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sxhblbgr.aspx.cs" Inherits="Web.Xmgl.Sxhblbgr" %>

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
                <f:Panel runat="server" Title="思想汇报" ShowBorder="true" ShowHeader="false" EnableCollapse="true">
                    <Items>
                        <f:ContentPanel runat="server" ID="ContentPanel1" ShowBorder="false" ShowHeader="false" BodyPadding="5 15 2 0">
                            <f:DropDownList runat="server" ID="ddlZtdm" Label="评阅状态" LabelWidth="75" LabelAlign="Right" Width="205px" AutoPostBack="true" OnSelectedIndexChanged="ddlZtdm_SelectedIndexChanged">
                                <f:ListItem Text="－全部－" Value="__" />
                                <f:ListItem Text="未评阅" Value="1" />
                                <f:ListItem Text="评阅通过" Value="4" Selected="true" />
                            </f:DropDownList>
                            <f:HiddenField runat="server" ID="tbxBll" Text=""></f:HiddenField>
                            <f:HiddenField runat="server" ID="tbxWhere" Text=""></f:HiddenField>
                            <f:HiddenField runat="server" ID="tbxSort" Text=""></f:HiddenField>
                            <div class="divHidden"></div>                            
                        </f:ContentPanel>
                    </Items>
                </f:Panel> 

                <f:Panel ID="pnlGrid" runat="server" ShowBorder="true" ShowHeader="false" BoxFlex="1" Layout="Fit" CssClass="gridpanel"> 
                    <Items>
                        <f:Grid ID="Grid1" runat="server" Title="Grid1" EmptyText="<img src='/res/images/no_data_found.jpg' alt='暂无数据'/>" AllowPaging="true" AllowSorting="true" EnableCheckBoxSelect="false" EnableTextSelection="true" EnableHeaderMenu="false" ShowBorder="false" ShowHeader="False" IsDatabasePaging="true" DataKeyNames="Pkid" OnPageIndexChange="Grid1_PageIndexChanged" OnSort="Grid1_Sort" OnRowDataBound="Grid1_RowDataBound">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" MinWidth="40px" />
                                <f:BoundField Width="150px" ColumnID="Xm" DataField="Xm" HeaderText="姓名" Hidden="true"/>
                                <f:BoundField Width="80px" ColumnID="Tjxh" DataField="Tjxh" Hidden="true" HeaderText="序号" />
                                <f:BoundField Width="100px" ColumnID="Yf" DataField="Yf" HeaderText="月份" />
                                <f:BoundField Width="200px" ColumnID="Tjsj" DataField="Tjsj" HeaderText="提交时间" />
                                <f:TemplateField Width="80px" HeaderText="评阅状态">
                                    <ItemTemplate>
                                        <asp:Label ID="lblZtmc" runat="server" Text='<%# (Eval("Ztmc").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:WindowField Width="50px" ColumnID="lbfView" TextAlign="Center" WindowID="wndView" Icon="Magnifier" HeaderText="浏览" ToolTip="浏览" DataIFrameUrlFields="Pkid" DataIFrameUrlFormatString="../Xmgl/SxhbView.aspx?pkid={0}" Title="浏览思想汇报" />
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
