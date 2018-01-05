<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NfzmdEdit.aspx.cs" Inherits="Web.Xmdr.NfzmdEdit" %>

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
        <f:SimpleForm runat="server" ID="SimpleForm1" BodyPadding="10" LabelWidth="70" ShowBorder="false" ShowHeader="false">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" runat="server" EnablePostBack="false" Text="关闭" Icon="SystemClose"></f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server"></f:ToolbarSeparator>
                        <f:Button runat="server" ID="btnSave" Text="保存" Icon="SystemSave" ConfirmText="确认保存？" ValidateForms="SimpleForm1" OnClick="btnSave_Click"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:SimpleForm ID="SimpleForm2" runat="server" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:Panel ID="Panel1" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:HiddenField runat="server" ID="hfdPkid"></f:HiddenField>
                                <f:Label runat="server" ID="lblXh" Label="学　　号" Margin="0 15 0 0" ColumnWidth="50%" Text=""></f:Label>                    
                                <f:Label runat="server" ID="lblXm" Label="姓　　名" Text=""></f:Label>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel2" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:DropDownList runat="server" ID="ddlZsjg" Label="政审结果" Margin="0 15 0 0" ColumnWidth="50%" Required="true" ShowRedStar="true"></f:DropDownList>
                                <f:DropDownList runat="server" ID="ddlDbjg" Label="答辩结果" Required="true" ShowRedStar="true" ></f:DropDownList>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel4" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:TextBox runat="server" ID="tbxLxdx" Label="联系对象" Margin="0 15 0 0" ColumnWidth="70%" MaxLength="15" EmptyText="限填 15 个汉字 ！ 填写联系班级 / 寝室 / 学生的名称。" Required="true" ShowRedStar="true"></f:TextBox>
                                <f:DatePicker runat="server" ID="dtpXmrq" Label="联系日期" ColumnWidth="30%" EnableEdit="false" Required="true" ShowRedStar="True"></f:DatePicker>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel5" runat="server" BodyPadding="0 0 30 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:TextArea runat="server" ID="tbxBz" Label="联系内容" ColumnWidth="100%" Height="50px" MinLength="20" MaxLength="100" EmptyText="限填 20 ～ 100 个汉字 ！"></f:TextArea>
                            </Items>
                        </f:Panel>
                        <f:ContentPanel ID="ContentPanel1" runat="server" ShowBorder="false" ShowHeader="false">
                            <span class="Red">填写说明：<br /><asp:Literal runat="server" ID="lblTxsm" Text="" ></asp:Literal></span>					                            
                        </f:ContentPanel>
                    </Items>
                </f:SimpleForm
                <f:DropDownList runat="server" ID="ddlBmbh" Hidden="true" Label="学院名称" CssStyle="color:Blue"></f:DropDownList>
                <f:DropDownList runat="server" ID="ddlDzbbh" Label="支部名称"></f:DropDownList>
                <f:TextBox runat="server" ID="tbxBjmc" Label="班级名称" MaxLength="50" Required="true"></f:TextBox>
            </Items>
        </f:SimpleForm>
    </form>
</body>
</html>
