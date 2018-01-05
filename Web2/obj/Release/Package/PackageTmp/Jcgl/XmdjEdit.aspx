<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XmdjEdit.aspx.cs" Inherits="Web.Jcgl.XmdjEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/res/css/index.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/table.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/fineui.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:SimpleForm runat="server" ID="SimpleForm1" BodyPadding="10" LabelWidth="80" ShowBorder="false" ShowHeader="false">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" runat="server" EnablePostBack="false" Text="关闭" Icon="SystemClose"></f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server"></f:ToolbarSeparator>
                        <f:Button runat="server" ID="btnSave" Text="保存" Icon="SystemSave" ConfirmText="确认保存？" ValidateForms="SimpleForm1" OnClick="btnSave_Click"></f:Button>
                        <f:Button runat="server" ID="btnSaveNext" Text="保存并继续" Icon="SystemSaveNew" ConfirmText="确认保存？" ValidateForms="SimpleForm1" OnClick="btnSaveNext_Click"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <%--<f:ContentPanel ID="ContentPanel2" runat="server" CssClass="divHidden" ShowBorder="false" ShowHeader="false">
                    	            
                        </f:ContentPanel>--%>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:HiddenField runat="server" ID="hfdPkid"></f:HiddenField>
                <f:HiddenField runat="server" ID="hfdOldZbdm"></f:HiddenField>
                <f:DropDownList runat="server" ID="ddlBmbh" Label="分党委名称" CssStyle="color:Blue" Hidden="true"></f:DropDownList>
                <f:DropDownList runat="server" ID="ddlZbbh" Label="指标名称" EnableSimulateTree="true" ShowRedStar="true" ></f:DropDownList>                            
                <f:TextBox runat="server" ID="tbxMc" Label="等级名称" MaxLength="20" Required="True" ShowRedStar="true"></f:TextBox>
            </Items>
        </f:SimpleForm>
    </form>
</body>
</html>
