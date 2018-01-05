<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xslb_old.aspx.cs" Inherits="Web.Fzgl.Xslb" %>

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
                <f:Panel ID="pnlCond" runat="server" Title="学生信息列表" ShowBorder="true" ShowHeader="true" EnableCollapse="true" CssClass="querypanel">
                    <Items>
                        <f:ContentPanel ID="ContentPanel1" runat="server" ShowBorder="false" ShowHeader="false" BodyPadding="10 10 5 0">
                            <f:DropDownList runat="server" ID="ddlBm" Label="分党委名称" LabelWidth="88" LabelAlign="Right" Width="275px" AutoPostBack="true" OnSelectedIndexChanged="ddlBm_SelectedIndexChanged"></f:DropDownList>
                            <f:ToolbarText ID="ToolbarText1" runat="server" Text="　" CssClass="titleSeparatorSmall" Hidden="true"></f:ToolbarText>   
                            <f:DropDownList runat="server" ID="ddlDzb" Label="党支部名称" LabelWidth="88" LabelAlign="Right" Width="280px" AutoPostBack="true" OnSelectedIndexChanged="ddlDzb_SelectedIndexChanged"></f:DropDownList>
                            <f:ToolbarText runat="server" Text="　" CssClass="titleSeparatorSmall" Hidden="true"></f:ToolbarText>   
                            <f:DropDownList runat="server" ID="ddlBj" Label="班级名称" LabelWidth="75" LabelAlign="Right" Width="225px" AutoPostBack="true" OnSelectedIndexChanged="ddlBj_SelectedIndexChanged"></f:DropDownList>
                            <f:ToolbarText runat="server" Text="　" CssClass="titleSeparatorSmall" Hidden="true"></f:ToolbarText>  
                            <f:DropDownList runat="server" ID="ddlFzzt" Label="发展阶段" LabelWidth="75" LabelAlign="Right" Width="225px" AutoPostBack="true" OnSelectedIndexChanged="ddlFzzt_SelectedIndexChanged">
                                <f:ListItem Text="－请选择－" Value="__" Selected="true" />
                                <f:ListItem Text="积极分子" Value="2" /> 
                                <f:ListItem Text="发展对象" Value="3,4" /> 
                                <f:ListItem Text="预备党员" Value="5" /> 
                                <f:ListItem Text="正式党员" Value="6" /> 
                            </f:DropDownList>
                            <f:ToolbarText runat="server" Text="　" CssClass="titleSeparatorSmall" Hidden="true"></f:ToolbarText> 
                            <f:TwinTriggerBox ID="ttbSearch" runat="server" Label="关键字" LabelWidth="62" LabelAlign="Right" Width="350px" EmptyText="请输入姓名或学号，姓名支持模糊查询" ShowTrigger1="false" Trigger1Icon="Clear" Trigger2Icon="Search" OnTrigger1Click="ttbSearch_Trigger1Click" OnTrigger2Click="ttbSearch_Trigger2Click"></f:TwinTriggerBox>
                            <f:HiddenField ID="tbxBll" runat="server" Text=""></f:HiddenField>
                            <f:HiddenField ID="tbxWhere" runat="server" Text=""></f:HiddenField>
                            <f:HiddenField ID="tbxSort" runat="server" Text=""></f:HiddenField>
                            <div class="divHidden"></div>                            
                        </f:ContentPanel>
                        <%-- 自动缩放大小，填充整行 --%>
                        <%--<f:Form ID="Form2" runat="server" ShowBorder="false" BodyPadding="5px" ShowHeader="false" Title="表单" LabelWidth="80" LabelAlign="Right">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList runat="server" ID="ddlYx" Label="院校名称" Width="180px"></f:DropDownList> 
                                        <f:DropDownList runat="server" ID="ddlZy" Label="专业名称" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlZy_SelectedIndexChanged"></f:DropDownList>   
                                        <f:DropDownList runat="server" ID="ddlBj" Label="班级名称" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlBj_SelectedIndexChanged"></f:DropDownList>  
                                        <f:TwinTriggerBox ID="ttbSearch" runat="server" Width="160px" Label="关键字" EmptyText="输入姓名或名册编号" ShowTrigger1="false" Trigger1Icon="Clear" Trigger2Icon="Search" OnTrigger1Click="ttbSearch_Trigger1Click" OnTrigger2Click="ttbSearch_Trigger2Click"></f:TwinTriggerBox>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>--%>
                    </Items>
                </f:Panel> 

                <%--生源信息列表--%>
                <f:Panel ID="pnlGrid" runat="server" ShowBorder="true" ShowHeader="false" BoxFlex="1" Layout="Fit">                    
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                <f:Button ID="btnBack" runat="server" Text="返回" Icon="DoorOut" Hidden="true"></f:Button>
                                <f:Button ID="btnAddNew" runat="server" Text="新增" Icon="Add" Hidden="true"></f:Button>
                                <f:Button ID="btnDeleteSel" runat="server" Text="删除" Icon="Delete" Hidden="true" OnClick="btnDeleteSel_Click"></f:Button>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server" Hidden="true"></f:ToolbarSeparator>
                                <f:Button ID="btnImport" runat="server" Text="导入" Icon="DatabaseGo" Hidden="true"></f:Button>
                                <f:Button ID="btnExport" runat="server" Text="导出" Icon="PageWhiteExcel" Enabled="true"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Grid ID="Grid1" runat="server" Title="Grid1" EmptyText="<img src='/res/images/no_data_found.jpg' alt='暂无数据'/>" AllowPaging="true" AllowSorting="true" EnableCheckBoxSelect="false" EnableTextSelection="true" EnableHeaderMenu="false" ShowBorder="false" ShowHeader="False" IsDatabasePaging="true" DataKeyNames="Pkid" OnPageIndexChange="Grid1_PageIndexChanged" OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" MinWidth="45px" />
                                <f:BoundField Width="120px" ColumnID="Bmmc" DataField="Bmmc" HeaderText="分党委名称"/>
                                <f:BoundField Width="180px" ColumnID="Dzbmc" DataField="Dzbmc" SortField="Dzbmc" HeaderText="党支部名称" />
                                <f:BoundField Width="150px" ColumnID="Bjmc" DataField="Bjmc" SortField="Bjmc" HeaderText="班级名称" />
                                <f:BoundField Width="120px" ColumnID="Xh" DataField="Xh" SortField="Xh" HeaderText="学号" />
                                <f:BoundField Width="100px" ColumnID="Xm" DataField="Xm" SortField="Xm" HeaderText="姓名" />
                                <f:BoundField Width="100px" ColumnID="Rdlxrxm1" DataField="Rdlxrxm1" SortField="Rdlxrxm1" HeaderText="联系人" />
                                <f:BoundField Width="140px" ColumnID="Fzzt" DataField="Fzzt" SortField="Fzzt" HeaderText="发展阶段" Hidden="true" />
                                <f:BoundField Width="140px" ColumnID="Jjfzrq" DataField="Jjfzrq" SortField="Jjfzrq" HeaderText="确定积极分子日期" Hidden="true" />
                                <f:BoundField Width="140px" ColumnID="Fzdxrq" DataField="Fzdxrq" SortField="Fzdxrq" HeaderText="确定发展对象日期" Hidden="true" />
                                <f:BoundField Width="140px" ColumnID="Rdrq" DataField="Rdrq" SortField="Rdrq" HeaderText="入党日期" Hidden="true" />
                                <f:BoundField Width="140px" ColumnID="Zzrq" DataField="Zzrq" SortField="Zzrq" HeaderText="转正日期" Hidden="true" />
                                <f:BoundField Width="80px" ColumnID="Sxhb" DataField="Sxhb" SortField="Sxhb" HeaderText="思想汇报" />
                                <f:BoundField Width="80px" ColumnID="Zyfw" DataField="Zyfw" SortField="Zyfw" HeaderText="志愿服务" />
                                <f:BoundField Width="65px" ColumnID="Slx" DataField="Slx" SortField="Slx" HeaderText="三联系" />
                                <f:BoundField Width="80px" ColumnID="Jshj" DataField="Jshj" SortField="Jshj" HeaderText="竞赛获奖" />
                                <f:BoundField Width="80px" ColumnID="Qtxm" DataField="Qtxm" SortField="Qtxm" HeaderText="其他项目" />
                                <f:WindowField Width="50px" ColumnID="lbfView" TextAlign="Center" WindowID="wndView" Icon="Magnifier" HeaderText="浏览" ToolTip="浏览" DataIFrameUrlFields="Pkid" DataIFrameUrlFormatString="ShowFzlc.aspx?pkid={0}" Title="弹出窗－浏览" />
                                <f:WindowField Width="50px" Hidden="true" ColumnID="lbfModify" TextAlign="Center" WindowID="wndEdit" Icon="Pencil" HeaderText="编辑" ToolTip="编辑" DataIFrameUrlFields="Pkid" DataIFrameUrlFormatString="BjEdit.aspx?pkid={0}" Title="弹出窗－修改" />
                                <f:LinkButtonField Width="50px" Hidden="true" ColumnID="lbfDelete" TextAlign="Center" Icon="BulletCross" ToolTip="删除" HeaderText="操作" ConfirmText="确认删除？" CommandArgument="Pkid" CommandName="Delete"/>
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

    <f:Window ID="wndImport" runat="server" Title="导入" WindowPosition="Center" Target="Self" Width="500px" Height="300px" EnableMaximize="true" EnableResize="false" EnableDrag="true"  EnableIFrame="true" Hidden="true" IsModal="true" CloseAction="Hide" OnClose="Window_Close">
    </f:Window>    
    <f:Window ID="wndView" runat="server" Title="弹出窗－浏览" WindowPosition="Center"  Target="Self" Width="1000px" Height="700px" EnableMaximize="false" EnableMinimize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true"  IsModal="true" Hidden="true" CloseAction="Hide">
    </f:Window> 
</body>
</html>
