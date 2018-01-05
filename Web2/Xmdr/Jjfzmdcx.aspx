<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jjfzmdcx.aspx.cs" Inherits="Web.Xmdr.Jjfzmdcx" %>

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
                <f:Panel runat="server" Title="积极分子名单" ShowBorder="true" ShowHeader="true" EnableCollapse="true">
                    <Items>
                        <f:ContentPanel runat="server" ShowBorder="false" ShowHeader="false" BodyPadding="10 10 5 0">
                            <f:DropDownList runat="server" ID="ddlDzb" Label="党支部" LabelWidth="62" LabelAlign="Right" Width="242px" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged"></f:DropDownList>
                            <f:DropDownList runat="server" ID="ddlDbjg" Label="答辩结果" LabelWidth="75" LabelAlign="Right" Width="175px" Hidden="true" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged"></f:DropDownList>
                            <f:ToolbarText runat="server" Text="　" CssClass="titleSeparatorSmall" Hidden="true"></f:ToolbarText> 
                            <f:TwinTriggerBox ID="ttbSearch" runat="server" Label="关键字" LabelWidth="62" LabelAlign="Right" Width="350px" EmptyText="请输入姓名或学号或身份证号，姓名支持模糊查询" ShowTrigger1="false" Trigger1Icon="Clear" Trigger2Icon="Search" OnTrigger1Click="ttbSearch_Trigger1Click" OnTrigger2Click="ttbSearch_Trigger2Click"></f:TwinTriggerBox>
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
                                <f:Button ID="btnDeleteSel" runat="server" Text="删除" Icon="Delete" OnClick="btnDeleteSel_Click"></f:Button>
                                <f:Button ID="btnClear" runat="server" Text="清空" Icon="DatabaseDelete" ConfirmText="确认要清空原有名单吗 ？" OnClick="btnClear_Click"></f:Button>
                                <f:Button ID="btnSubmit" runat="server" Text="提交" Icon="LaptopGo" ConfirmText="确认要提交名单吗 ？" OnClick="btnSubmit_Click"></f:Button>
                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                <f:Button ID="btnImport" runat="server" Text="导入" Icon="DatabaseGo"></f:Button>
                                <f:Button ID="btnExport" runat="server" Text="导出" Icon="PageWhiteExcel" Hidden="true"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Grid ID="Grid1" runat="server" Title="Grid1" EmptyText="<img src='/res/images/no_data_found.jpg' alt='暂无数据'/>" AllowPaging="true" AllowSorting="true" EnableCheckBoxSelect="true" EnableTextSelection="true" EnableHeaderMenu="false" ShowBorder="false" ShowHeader="False" IsDatabasePaging="true" DataKeyNames="Pkid,Ztdm" OnPageIndexChange="Grid1_PageIndexChanged" OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand" OnPreRowDataBound="Grid1_PreRowDataBound" OnRowDataBound="Grid1_RowDataBound">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" Width="45" />
                                <f:BoundField Width="180px" ColumnID="Dzbmc" DataField="Dzbmc" SortField="Dzbmc" HeaderText="党支部名称" />
                                <f:BoundField Width="120px" ColumnID="Xh" DataField="Xh" SortField="Xh" HeaderText="学号" />
                                <f:BoundField Width="100px" ColumnID="Xm" DataField="Xm" SortField="Xm" HeaderText="姓名" />
                                <f:BoundField Width="90px" ColumnID="Jg" DataField="Jg" SortField="Jg" HeaderText="籍贯" />
                                <f:BoundField Width="60px" ColumnID="Mz" DataField="Mz" SortField="Mz" HeaderText="民族" />
                                <f:BoundField Width="100px" ColumnID="Jjfzrq" DataField="Jjfzrq" SortField="Jjfzrq" HeaderText="确定日期" />
                                <f:TemplateField Width="60px" HeaderText="状态">
                                    <ItemTemplate>
                                        <asp:Label ID="lblZtmc" runat="server" Text='<%# Eval("Ztmc").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:WindowField Width="50px" ColumnID="lbfModify" TextAlign="Center" WindowID="wndEdit" Hidden="true" Icon="Pencil" HeaderText="编辑" ToolTip="编辑" DataIFrameUrlFields="Pkid,Zbbh" DataIFrameUrlFormatString="NfzmdEdit.aspx?pkid={0}" Title="弹出窗－导入修改" />
                                <f:LinkButtonField Width="50px" ColumnID="lbfDelete" TextAlign="Center" Icon="BulletCross" ToolTip="删除" HeaderText="操作" ConfirmText="确认删除？" CommandArgument="Pkid" CommandName="Delete"/>
                                <f:TemplateField ExpandUnusedSpace="true" ><ItemTemplate><div class="divHeightSpan"></div></ItemTemplate></f:TemplateField>
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
      
    <f:Window ID="wndImport" runat="server" Title="弹出窗－导入数据" WindowPosition="Center" Target="Self" Width="800px" Height="450px" EnableMaximize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true" IsModal="true" Hidden="true" CloseAction="Hide" OnClose="Window_Close"></f:Window>
    <f:Window ID="wndEdit" runat="server" Title="弹出窗－导入修改" WindowPosition="Center" Target="Self" Width="450px" Height="300px" EnableMaximize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true" IsModal="true" Hidden="true" CloseAction="Hide" OnClose="Window_Close"></f:Window> 
</body>
</html>
