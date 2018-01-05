<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SxhbSh.aspx.cs" Inherits="Web.Xmgl.SxhbSh" %>

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
                <f:Panel runat="server" Title="思想汇报" ShowBorder="true" ShowHeader="true" EnableCollapse="true" >
                    <Items>
                        <f:ContentPanel runat="server" ID="ContentPanel1" ShowBorder="false" ShowHeader="false" BodyPadding="15 15 10 0">
                            <f:DropDownList runat="server" ID="ddlZtdm" Label="评阅状态" LabelWidth="75" LabelAlign="Right" Width="205px" AutoPostBack="true" OnSelectedIndexChanged="ddlZtdm_SelectedIndexChanged">
                                <f:ListItem Text="－全部－" Value="__" />
                                <f:ListItem Text="未评阅" Value="0" />
                                <f:ListItem Text="退回修改" Value="1" />
                                <f:ListItem Text="退回重写" Value="2" />
                                <f:ListItem Text="评阅通过" Value="3" />
                            </f:DropDownList>
                            <f:ToolbarText runat="server" Text="　" CssClass="titleSeparatorSmall" Hidden="true"></f:ToolbarText> 
                            <f:TwinTriggerBox ID="ttbSearch" runat="server" Label="关键字" LabelWidth="62" LabelAlign="Right" Width="350px" EmptyText="请输入姓名或学号，姓名支持模糊查询" ShowTrigger1="false" Trigger1Icon="Clear" Trigger2Icon="Search" OnTrigger1Click="ttbSearch_Trigger1Click" OnTrigger2Click="ttbSearch_Trigger2Click"></f:TwinTriggerBox>
                            <f:HiddenField runat="server" ID="tbxBll" Text=""></f:HiddenField>
                            <f:HiddenField runat="server" ID="tbxWhere" Text=""></f:HiddenField>
                            <f:HiddenField runat="server" ID="tbxSort" Text=""></f:HiddenField>
                            <div class="divHidden"></div>                            
                        </f:ContentPanel>
                    </Items>
                </f:Panel> 

                <f:Panel ID="pnlGrid" runat="server" ShowBorder="true" ShowHeader="false" BoxFlex="1" Layout="Fit">        
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server" Hidden="true">
                            <Items>
                                <f:Button ID="btnAddNew" runat="server" Text="撰写思想汇报" Icon="PageEdit" Enabled="true"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Grid ID="Grid1" runat="server" Title="Grid1" EmptyText="<img src='/res/images/no_data_found.jpg' alt='暂无数据'/>" AllowPaging="true" AllowSorting="true" EnableCheckBoxSelect="true" EnableTextSelection="true" ShowBorder="false" ShowHeader="False" IsDatabasePaging="true" DataKeyNames="Pkid" OnPageIndexChange="Grid1_PageIndexChanged" OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand" OnPreRowDataBound="Grid1_PreRowDataBound" OnRowDataBound="Grid1_RowDataBound">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" />
                                <f:BoundField Width="100px" ColumnID="Xm" DataField="Xm" HeaderText="姓名"/>
                                <f:BoundField Width="100px" ColumnID="Yf" DataField="Yf" HeaderText="月份" />
                                <f:BoundField Width="200px" ColumnID="Tjsj" DataField="Tjsj" HeaderText="提交时间" />
                                <f:TemplateField Width="80px" HeaderText="状态">
                                    <ItemTemplate>
                                        <asp:Label ID="lblZtmc" runat="server" Text='<%# (Eval("Ztxsmc").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:WindowField Width="50px" ColumnID="lbfView" TextAlign="Center" WindowID="wndEdit" Icon="Magnifier" Hidden="true" HeaderText="浏览" ToolTip="浏览" DataIFrameUrlFields="Pkid" DataIFrameUrlFormatString="SxhbView.aspx?pkid={0}" Title="弹出窗－浏览" />
                                <f:WindowField Width="50px" ColumnID="lbfModify" TextAlign="Center" WindowID="wndEdit" Icon="Pencil" HeaderText="审核" ToolTip="审核" DataIFrameUrlFields="Pkid,Ztdm" DataIFrameUrlFormatString="SxhbShView.aspx?pkid={0}&ztdm={1}" Title="弹出窗－评阅" />
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

    <f:Window ID="wndEdit" runat="server" Title="评阅" WindowPosition="Center"  Target="Self" Width="1000px" Height="700px"  EnableMaximize="false" EnableResize="false" EnableDrag="true"  EnableIFrame="true" Hidden="true" IsModal="true" CloseAction="Hide" OnClose="Window_Close">
    </f:Window>
</body>
</html>
