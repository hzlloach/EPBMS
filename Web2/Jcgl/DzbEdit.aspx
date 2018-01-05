﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DzbEdit.aspx.cs" Inherits="Web.Jcgl.DzbEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/res/css/index.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/table.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/fineui.css" rel="stylesheet" type="text/css" />
    
    <style>
        .x-form-display-field-default {
            color: blue;
        }
    </style>
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
                <f:Label runat="server" ID="lblBmmc" Label="分党委名称" CssStyle="color:Blue"></f:Label>
                <f:DropDownList runat="server" ID="ddlBmbh" Label="分党委名称" CssStyle="color:Blue" Hidden="true"></f:DropDownList>
                <f:TextBox runat="server" ID="tbxDzbdm" Label="党支部代码" MaxLength="2" Required="true"></f:TextBox>
                <f:TextBox runat="server" ID="tbxDzbmc" Label="党支部名称" MaxLength="50" Required="true"></f:TextBox>
                <f:TextBox runat="server" ID="tbxUid" Label="登录帐号名" MaxLength="10" Hidden="true"></f:TextBox>
                <f:TextBox runat="server" ID="tbxPwd" Label="登录密码" MaxLength="20" Hidden="true"></f:TextBox>
                <f:TextBox runat="server" ID="tbxPwd2" Label="确认密码" MaxLength="20" Hidden="true"></f:TextBox>
            </Items>
        </f:SimpleForm>
    </form>
</body>
</html>
