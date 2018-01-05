<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelDzb.aspx.cs" Inherits="Web.Fzgl.SelDzb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/res/css/index.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/table.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/fineui.css" rel="stylesheet" type="text/css" />
    <style>
        .x-form-check-group {
            /*border: 1px solid blue;
            margin-left: 0px;
            padding: 3px 0px 3px 3px;*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:SimpleForm runat="server" ID="SimpleForm1" BodyPadding="10" LabelWidth="80" ShowBorder="false" ShowHeader="false" AutoScroll="true">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" runat="server" EnablePostBack="false" Text="关闭" Icon="SystemClose"></f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server"></f:ToolbarSeparator>
                        <f:Button runat="server" ID="btnDownload" Text="下载思想汇报" Icon="SystemSave" ConfirmText="生成思想汇报需要花费几分钟 ！<br/><br/>确认下载思想汇报？" ValidateForms="SimpleForm1" OnClick="btnDownload_Click"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <f:ContentPanel ID="ContentPanel2" runat="server" ShowBorder="false" ShowHeader="false">                    	            
                            <f:Panel ID="pnlFrame" runat="server" ShowBorder="false" ShowHeader="false" Title="" Width="1" Height="1" EnableIFrame="true" EnableAjax="true" EnableAjaxLoading="true"></f:Panel>        
                        </f:ContentPanel>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Label runat="server" ID="lblBmmc" Label="分党委名称" CssStyle="color:Blue"></f:Label>
                <f:DropDownList runat="server" ID="ddlBmbh" Label="分党委名称" CssStyle="color:Blue" Hidden="true"></f:DropDownList>
                <f:CheckBoxList runat="server" ID="cblDzbbh" Label="党支部名称" Required="true" RequiredMessage="请至少选择一项" ColumnNumber="1"></f:CheckBoxList>
                <f:DropDownList runat="server" ID="ddlDzbbh" Label="党支部名称" CssStyle="color:Blue" Hidden="true"></f:DropDownList>
            </Items>
        </f:SimpleForm>
    </form>
</body>
</html>
