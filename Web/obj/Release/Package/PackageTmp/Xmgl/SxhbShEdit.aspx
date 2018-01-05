<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SxhbShEdit.aspx.cs" Inherits="Web.Xmgl.SxhbShEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/res/css/index.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/table.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/fineui.css" rel="stylesheet" type="text/css" />
    <%--<script src="../res/js/jquery.js"></script>--%>
    
    <style>
        #SimpleForm1_ctl01_tbxPyyj-inputEl {
            color: blue;
            font-size:15px;
            line-height:1.5;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:SimpleForm runat="server" ID="SimpleForm1" Layout="VBox" BodyPadding="10" LabelWidth="70" ShowBorder="true" ShowHeader="false">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" runat="server" EnablePostBack="false" Text="关闭" Icon="SystemClose"></f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server"></f:ToolbarSeparator>
                        <f:Button runat="server" ID="btnSave" Text="评阅" Icon="SystemSave" ConfirmText="确认评阅？" ValidateForms="SimpleForm1" OnClick="btnAudit_Click"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <%--<f:ContentPanel ID="ContentPanel2" runat="server" CssClass="divHidden" ShowBorder="false" ShowHeader="false">
                    	            
                        </f:ContentPanel>--%>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>         
                <f:Panel runat="server" ShowBorder="false" ShowHeader="false" Height="25px" Hidden="true">    
                    <Items>     
                        <f:HiddenField runat="server" ID="hfdPkid"></f:HiddenField>
                        <f:HiddenField runat="server" ID="hfdHbbh"></f:HiddenField>
                        <f:HiddenField runat="server" ID="hfdTjxh"></f:HiddenField>
                    </Items>
                </f:Panel>               
                <f:Panel ID="pnlHint" runat="server" ShowBorder="false" ShowHeader="false">  
                    <Items>
                        <f:DropDownList runat="server" ID="ddlShzt" Label="评阅结果">
                            <f:ListItem Text="评阅通过" Value="22" />
                            <f:ListItem Text="退回修改" Value="21" />
                            <f:ListItem Text="退回重写" Value="20" />
                        </f:DropDownList>
                    </Items>
                </f:Panel>                    
                <f:Panel runat="server" ShowBorder="false" ShowHeader="false" BoxFlex="1" Layout="Fit" BodyPadding="0 0 0 0" CssClass="PnlHbnr">  
                    <Items>
                        <f:TextArea runat="server" ID="tbxPyyj" Label="评阅意见" LabelAlign="Top" MinLength="20" MaxLength="100" EmptyText="请输入评阅意见，限填 20 ～ 100 个汉字 ！" Required="true" Hidden="false"></f:TextArea>
                    </Items>
                </f:Panel>         
            </Items>
        </f:SimpleForm>
    </form> 
</body>
</html>
