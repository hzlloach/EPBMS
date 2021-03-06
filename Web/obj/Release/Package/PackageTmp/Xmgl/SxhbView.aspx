﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SxhbView.aspx.cs" Inherits="Web.Xmgl.SxhbView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/res/css/index.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/table.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/fineui.css" rel="stylesheet" type="text/css" />
    
    <style>
        .spanText {
            border:0px solid maroon;
            margin-top:0px;
            padding:5px;
            color: blue;
            font-size:15px;
            line-height:1.5; 
        }
        .spanTextPy {
            border:0px solid maroon;
            margin-top:0px;
            padding:5px;
            color: red;
            line-height:1.5; 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:SimpleForm runat="server" ID="SimpleForm1" Layout="VBox" BodyPadding="10" LabelWidth="70" ShowBorder="true" ShowHeader="false">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" Hidden="true">
                    <Items>
                        <f:Button ID="btnClose" runat="server" EnablePostBack="false" Text="关闭" Icon="SystemClose"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>     
                <f:Panel runat="server" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:Label runat="server" ID="lblFzr" Label="姓　　名" Margin="0 15 0 0" ColumnWidth="37%" Text=""></f:Label>                    
                        <f:Label runat="server" ID="lblYf" Label="月　　份" ColumnWidth="37%" Text=""></f:Label>                       
                        <f:Label runat="server" ID="lblTjsj" Label="提交时间" ColumnWidth="26%" Text=""></f:Label>
                    </Items>
                </f:Panel>                    
                <f:Panel runat="server" CssClass="formitem" ShowBorder="false" ShowHeader="false" Height="25px">
                    <Items>
                        <f:Label runat="server" Text="" Label="汇报内容"></f:Label>
                    </Items>
                </f:Panel>                    
                <f:Panel runat="server" BoxFlex="1" Layout="Fit" CssClass="formitem" ShowBorder="true" ShowHeader="false" >
                    <Items>               
                        <f:ContentPanel ID="ContentPanel1" runat="server" ShowBorder="false" ShowHeader="false" AutoScroll="true">
                            <div class="spanText">
                                <asp:Literal ID="lblHbnr" runat="server"></asp:Literal>
                            </div>
                        </f:ContentPanel>
                    </Items>
                </f:Panel>                    
                <f:Panel runat="server" Margin="10 0 0 0" Layout="Column" ShowBorder="false" ShowHeader="false" Height="25px">
                    <Items>
                        <f:Label runat="server" Text="" Label="评阅意见" Width="10px"></f:Label>
                        <f:Label runat="server" ID="lblPyzt" CssClass="spanRed" Label="" Text=""></f:Label>
                        <f:Label runat="server" ID="lblPysj" CssClass="spanGreen" Label="" Text=""></f:Label>                          
                    </Items>
                </f:Panel>                    
                <f:Panel runat="server" Layout="Fit" ShowBorder="true" ShowHeader="false">
                    <Items>               
                        <f:ContentPanel ID="ContentPanel2" runat="server" ShowBorder="false" ShowHeader="false" MaxHeight="50px" AutoScroll="true">
                            <div class="spanTextPy">
                                <asp:Literal ID="lblShyj" runat="server"></asp:Literal>
                            </div>
                        </f:ContentPanel>
                    </Items>
                </f:Panel>                         
            </Items>
        </f:SimpleForm>
    </form>

    <script src="../res/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
    </script>
</body>
</html>
