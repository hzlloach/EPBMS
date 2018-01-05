<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xmsb.aspx.cs" Inherits="Web.Xmgl.Xmsb" %>

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
                <f:Panel runat="server" Title="其他项目" ShowBorder="true" ShowHeader="true" EnableCollapse="true">
                    <Items>
                        <f:ContentPanel runat="server" ID="ContentPanel1" ShowBorder="false" ShowHeader="false" BodyPadding="15 15 10 0">
                            <f:DropDownList runat="server" ID="ddlKhzb" Label="项目类别" LabelWidth="75" LabelAlign="Right" Width="275px" AutoPostBack="true" OnSelectedIndexChanged="ddlKhzb_SelectedIndexChanged"></f:DropDownList>
                            <f:ToolbarText runat="server" Text="　" CssClass="titleSeparatorSmall" Hidden="true"></f:ToolbarText> 
                            <f:TwinTriggerBox ID="ttbSearch" runat="server" Label="关键字" LabelWidth="62" LabelAlign="Right" Width="312px" EmptyText="请输入项目名称，支持模糊查询" ShowTrigger1="false" Trigger1Icon="Clear" Trigger2Icon="Search" OnTrigger1Click="ttbSearch_Trigger1Click" OnTrigger2Click="ttbSearch_Trigger2Click"></f:TwinTriggerBox>
                            <f:HiddenField runat="server" ID="tbxBll" Text=""></f:HiddenField>
                            <f:HiddenField runat="server" ID="tbxWhere" Text=""></f:HiddenField>
                            <f:HiddenField runat="server" ID="tbxSort" Text=""></f:HiddenField>
                            <div class="divHidden"></div>                            
                        </f:ContentPanel>
                    </Items>
                </f:Panel> 

                <f:Panel ID="pnlGrid" runat="server" ShowBorder="true" ShowHeader="false" BoxFlex="1" Layout="Fit" CssClass="gridpanel">        
                    <Toolbars>
                        <f:Toolbar runat="server">
                            <Items>
                                <f:Button ID="btnAddNew" runat="server" Text="新增" Icon="Add" Enabled="true"></f:Button>
                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                <f:Button ID="btnDeleteSel" runat="server" Text="删除" Icon="Delete" OnClick="btnDeleteSel_Click"></f:Button>
                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                <f:Button ID="btnSubmit" runat="server" Text="提交" Icon="LaptopGo" OnClick="btnSubmit_Click"></f:Button>
                                <f:ToolbarSeparator runat="server" Hidden="true"></f:ToolbarSeparator>
                                <f:Button ID="btnExport" runat="server" Text="导出" Icon="PageWhiteExcel" Hidden="true"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Grid ID="Grid1" runat="server" Title="Grid1" EmptyText="<img src='/res/images/no_data_found.jpg' alt='暂无数据'/>" AllowPaging="true" AllowSorting="true" EnableCheckBoxSelect="true" EnableTextSelection="true" EnableHeaderMenu="false" ShowBorder="false" ShowHeader="False" IsDatabasePaging="true" DataKeyNames="Pkid,Ztdm,Yxsc,Fjsl" OnPageIndexChange="Grid1_PageIndexChanged" OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand" OnPreRowDataBound="Grid1_PreRowDataBound" OnRowDataBound="Grid1_RowDataBound">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" />
                                <f:BoundField Width="90px" ColumnID="Zbmc" DataField="Zbmc" HeaderText="项目类别"/>
                                <f:BoundField Width="90px" ColumnID="Djmc" DataField="Djmc" SortField="Djmc" HeaderText="项目等级"/>
                                <f:TemplateField Width="300px" ColumnID="Xmmc" SortField="Xmmc" HeaderText="项目名称">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lblXmmc" runat="server" Text='<%# Eval("Xmmc").ToString() %>' NavigateUrl='javascript:;' ToolTip="点击浏览"></asp:HyperLink>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:BoundField Width="100px" ColumnID="Xmrq" DataField="Xmrq" SortField="Xmrq" HeaderText="项目日期" />
                                <f:TemplateField Width="80px" SortField="Ztxsmc" HeaderText="状态">
                                    <ItemTemplate>
                                        <asp:Label ID="lblZtmc" runat="server" Text='<%# GetShzt(Eval("Ztdm").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:WindowField Width="50px" ColumnID="lbfView" TextAlign="Center" WindowID="wndEdit" Icon="Zoom" Hidden="true" HeaderText="浏览" ToolTip="浏览" DataIFrameUrlFields="Pkid" DataIFrameUrlFormatString="XmsbView.aspx?id={0}" Title="弹出窗－浏览" />
                                <f:WindowField Width="50px" ColumnID="lbfModify" TextAlign="Center" WindowID="wndEdit" Icon="Pencil" HeaderText="编辑" ToolTip="编辑" DataIFrameUrlFields="Pkid,Zbbh" DataIFrameUrlFormatString="XmsbEdit.aspx?pkid={0}&zb={1}" Title="弹出窗－修改" />
                                <f:LinkButtonField Width="50px" ColumnID="lbfDelete" TextAlign="Center" Icon="BulletCross" ToolTip="删除" HeaderText="操作" ConfirmText="确认删除？" CommandArgument="Pkid" CommandName="Delete"/>
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

    <f:Window ID="wndEdit" runat="server" Title="弹出窗－新增" WindowPosition="Center" Target="Self" Width="860px" Height="670px" EnableMaximize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true" IsModal="true" Hidden="true" CloseAction="Hide" OnClose="Window_Close">
    </f:Window> 
    <f:Window ID="wndView" runat="server" Title="弹出窗－浏览" WindowPosition="Center" Target="Self" Width="1000px" Height="700px" EnableMaximize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true"  IsModal="true" Hidden="true" CloseAction="Hide">
    </f:Window>  
</body>
</html>
