<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="hello.aspx.cs" Inherits="EmptyProjectNet20.hello" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Button Text="点击弹出对话框" runat="server" ID="btnHello" OnClick="btnHello_Click">
        </f:Button>
        <br />
        <f:Label runat="server" ID="lblTime"></f:Label>
        <input  type="button" value="test" onclick="F.alert({ message: '尚未实现', messageIcon: Ext.MessageBox.INFO });" />
    </form>
</body>
</html>
