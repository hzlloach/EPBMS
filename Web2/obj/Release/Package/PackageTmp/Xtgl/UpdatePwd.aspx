<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePwd.aspx.cs" Inherits="Web.Xtgl.UpdatePwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/res/css/index.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/table.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/fineui.css" rel="stylesheet" type="text/css" />
    <script src="../res/js/jquery.min.js" type="text/javascript"></script>
    <script src="../res/js/JScript.js" type="text/javascript"></script>
    
    <style>
        .x-form-display-field-default {
            color: blue;
        }
    </style>

    <script type="text/javascript">
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:SimpleForm runat="server" ID="SimpleForm1" BodyPadding="10" LabelWidth="70" ShowBorder="false" ShowHeader="false">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" runat="server" EnablePostBack="false" Text="关闭" Icon="SystemClose"></f:Button>
                        <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                        <f:Button runat="server" ID="btnSave" Text="保存" Icon="SystemSave" ConfirmText="确认修改？" ValidateForms="SimpleForm1" OnClick="btnSave_Click"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <%--<f:ContentPanel ID="ContentPanel2" runat="server" CssClass="divHidden" ShowBorder="false" ShowHeader="false">
                    	            
                        </f:ContentPanel>--%>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:HiddenField runat="server" ID="tbxXh"></f:HiddenField>
                <f:Label runat="server" ID="lblYhm" Label="用 户 名" ></f:Label>
                <f:TextBox runat="server" ID="tbxPwdOld" Label="原始密码" TextMode="Password" MinLength="6" MaxLength="20" ></f:TextBox>
                <f:TextBox runat="server" ID="tbxPwd" Label="新设密码" TextMode="Password" MinLength="6" MaxLength="20" ></f:TextBox>
                <f:TextBox runat="server" ID="tbxPwd2" Label="确认密码" TextMode="Password" MinLength="6" MaxLength="20" ></f:TextBox>
            </Items>
        </f:SimpleForm>
    </form>
</body>
</html>
