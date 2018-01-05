<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="Web.Xtgl.UserInfo" %>

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
                <f:HiddenField runat="server" ID="hfdImg"></f:HiddenField>
                <f:Label runat="server" ID="lblYhm" Label="用 户 名" ></f:Label>
                <f:Image runat="server" ID="imgPhoto" Label="个人头像" ImageHeight="112" ImageWidth="85" ImageCssStyle="border:1px solid gray;" Margin="-10px 0 0 0 "></f:Image>
                <f:FileUpload runat="server" ID="fudPhoto" ShowEmptyLabel="true" ButtonText="上传头像" ButtonOnly="true" ButtonIcon="ImageAdd" AutoPostBack="true" OnFileSelected="fudPhoto_FileSelected"></f:FileUpload>
                <f:TextBox runat="server" ID="tbxPwdOld" Label="登录密码" TextMode="Password" MinLength="6" MaxLength="20" ></f:TextBox>
                <f:TextBox runat="server" ID="tbxSjhm" Label="新手机号" MaxLength="11" ></f:TextBox>
                <f:Panel ID="Panel1" runat="server" BodyPadding="0 0 7 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:TextBox runat="server" ID="tbxYzm" Label="验 证 码" Margin="0 10 0 0" ColumnWidth="70%"  MaxLength="6" ></f:TextBox>                    
                        <f:Button runat="server" ID="btnSend" Text="获取验证码" ColumnWidth="30%" OnClientClick="ajaxGetVerifyCode('SimpleForm1_tbxXh-inputEl', 'SimpleForm1_tbxSjhm-inputEl', 'SimpleForm1_Panel1_btnSend', 'SimpleForm1_Panel1_hfdCode-inputEl',2);return false;"></f:Button>
                        <f:HiddenField runat="server" ID="hfdCode" ></f:HiddenField>
                    </Items>
                </f:Panel>
            </Items>
        </f:SimpleForm>
    </form>
</body>
</html>
