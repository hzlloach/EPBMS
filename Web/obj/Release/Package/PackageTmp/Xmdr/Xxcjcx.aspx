<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xxcjcx.aspx.cs" Inherits="Web.Xmdr.Xxcjcx" %>

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
                <f:Panel runat="server" Title="学习成绩" ShowBorder="true" ShowHeader="true" EnableCollapse="true" CssClass="querypanel">
                    <Items>
                        <f:ContentPanel runat="server" ShowBorder="false" ShowHeader="false" BodyPadding="15 15 10 0">
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
                        <f:Toolbar runat="server">
                            <Items>
                                <f:Button ID="btnDeleteSel" runat="server" Text="删除" Icon="Delete" OnClick="btnDeleteSel_Click"></f:Button>
                                <f:Button ID="btnClear" runat="server" Text="清空" Icon="DatabaseDelete" ConfirmText="确认要清空原有数据吗 ？" OnClick="btnClear_Click" Hidden="true"></f:Button>
                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                <f:Button ID="btnImport" runat="server" Text="导入" Icon="DatabaseGo"></f:Button>
                                <f:Button ID="btnExport" runat="server" Text="导出" Icon="PageWhiteExcel" Hidden="true"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Grid ID="Grid1" runat="server" Title="Grid1" EmptyText="<img src='/res/images/no_data_found.jpg' alt='暂无数据'/>" AllowPaging="true" AllowSorting="true" EnableCheckBoxSelect="true" EnableTextSelection="true" ShowBorder="false" ShowHeader="False" IsDatabasePaging="true" DataKeyNames="Pkid,Fzrbh" OnPageIndexChange="Grid1_PageIndexChanged" OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand" >
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" />
                                <f:BoundField Width="120px" ColumnID="Xh" DataField="Xh" SortField="Xh" HeaderText="学号" />
                                <f:BoundField Width="100px" ColumnID="Xm" DataField="Xm" SortField="Xm" HeaderText="姓名" />
                                <f:TemplateField Width="110px" ColumnID="Xxcjpm" SortField="Yjzf" HeaderText="学习成绩排名">
                                    <ItemTemplate>
                                        <%# GetXxcjpm(Eval("Bz").ToString()) %>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField Width="110px" ColumnID="Zhkppm" SortField="Kfpyjf" HeaderText="综合考评排名">
                                    <ItemTemplate>
                                        <%# GetZhkppm(Eval("Bz").ToString()) %>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField Width="100px" ColumnID="Bjgms" SortField="Yfpyjf" HeaderText="不及格门数">
                                    <ItemTemplate>
                                        <%# GetBjgms(Eval("Bz").ToString()) %>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:WindowField Width="50px" ColumnID="lbfModify" Hidden="true" TextAlign="Center" WindowID="wndEdit" Icon="Pencil" HeaderText="编辑" ToolTip="编辑" DataIFrameUrlFields="Pkid,Zbbh" DataIFrameUrlFormatString="JshjEdit.aspx?pkid={0}&zb={1}" Title="弹出窗－修改" />
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
      
    <f:Window ID="wndImport" runat="server" Title="弹出窗－导入数据" WindowPosition="Center" Target="Self" Width="800px" Height="450px" EnableMaximize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true" IsModal="true" Hidden="true" CloseAction="Hide" OnClose="Window_Close"></f:Window>
    <f:Window ID="wndEdit" runat="server" Title="弹出窗－修改" WindowPosition="Center" Target="Self" Width="450px" Height="300px" EnableMaximize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true" IsModal="true" Hidden="true" CloseAction="Hide" OnClose="Window_Close"></f:Window> 
</body>
</html>
