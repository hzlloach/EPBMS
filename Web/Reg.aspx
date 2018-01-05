<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reg.aspx.cs" Inherits="Web.Reg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/res/css/index.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/table.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/fineui.css" rel="stylesheet" type="text/css" />
    <script src="res/js/jquery.min.js" type="text/javascript"></script>
    <script src="res/js/JScript.js" type="text/javascript"></script>
    
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
                        <f:Button runat="server" ID="btnSave" Text="注册" Icon="SystemSave" ConfirmText="确认注册？" ValidateForms="SimpleForm1" OnClick="btnSave_Click"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <%--<f:ContentPanel ID="ContentPanel2" runat="server" CssClass="divHidden" ShowBorder="false" ShowHeader="false">
                    	            
                        </f:ContentPanel>--%>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:HiddenField runat="server" ID="hfdPkid"></f:HiddenField>
                <f:TextBox runat="server" ID="tbxXh" Label="学　　号" MaxLength="12" Required="true" ></f:TextBox>
                <f:TextBox runat="server" ID="tbxXm" Label="姓　　名" MaxLength="20" Required="true" ></f:TextBox>
                <f:TextBox runat="server" ID="tbxSfzh" Label="身份证号" MaxLength="18" Required="true" ></f:TextBox>                
                <f:TextBox runat="server" ID="tbxPwd" Label="新设密码" TextMode="Password" MinLength="6" MaxLength="20" Required="true"  ></f:TextBox>
                <f:TextBox runat="server" ID="tbxPwd2" Label="确认密码" TextMode="Password" MinLength="6" MaxLength="20" Required="true" ></f:TextBox>
                <f:TextBox runat="server" ID="tbxSjhm" Label="手机号码" MaxLength="11" Required="true" ></f:TextBox>
                <f:Panel ID="Panel1" runat="server" BodyPadding="0 0 7 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:TextBox runat="server" ID="tbxYzm" Label="验 证 码" Margin="0 10 0 0" ColumnWidth="70%"  MaxLength="6" ></f:TextBox>                    
                        <f:Button runat="server" ID="btnSend" Text="获取验证码" ColumnWidth="30%" OnClientClick="ajaxGetVerifyCode('SimpleForm1_tbxXh-inputEl', 'SimpleForm1_tbxSjhm-inputEl', 'SimpleForm1_Panel1_btnSend', 'SimpleForm1_Panel1_hfdCode-inputEl',0);return false;"></f:Button>
                        <f:HiddenField runat="server" ID="hfdCode" ></f:HiddenField>
                    </Items>
                </f:Panel>
            </Items>
        </f:SimpleForm>
    </form>
</body>
</html>
