<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="Web.FileUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="pnlFit" runat="server" />
        <f:Panel ID="pnlFit" runat="server" Margin="0" ShowBorder="false" ShowHeader="false">
            <Items> 
                        <f:TextBox runat="server" ID="tbxBt1" Label="附件标题" EmptyText="限填 20 个汉字 ！" CssClass="marginr"></f:TextBox>
                        <f:Panel runat="server" ShowBorder="false" CssStyle="margin-bottom:5px;" ShowHeader="false">
                            <Items>
                                <f:TextBox runat="server" ID="hfdLj1" Label=""></f:TextBox>
                                <f:SimpleForm ShowBorder="false" ShowHeader="false" runat="server">
                                    <Toolbars>
                                        <f:Toolbar runat="server">
                                    <Items>
<f:FileUpload runat="server" ID="fudFj1" ButtonText="选择文件" ButtonIcon="FolderMagnify" AcceptFileTypes="image/*" ButtonOnly="true"></f:FileUpload>
                                
                                    </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                </f:SimpleForm>
                                <f:Button ID="btnView1" runat="server" Text="预览" Icon="Photo" Hidden="false"></f:Button>
                                <f:Button ID="btnDel1" runat="server" Text="删除" Icon="Delete" Hidden="false" Margin="0 0 0 5" ConfirmText="确认删除？"></f:Button>
                            </Items>
                        </f:Panel>
            </Items>
        </f:Panel> 
    </form>
</body>
</html>
