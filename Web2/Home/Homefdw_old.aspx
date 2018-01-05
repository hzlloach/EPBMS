<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Homefdw.aspx.cs" Inherits="Web.Home.Homefdw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/res/css/index.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/table.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/fineui.css" rel="stylesheet" type="text/css" />
    <script src="../res/js/jquery.min.js"></script>
    
    <style>
        .x-form-display-field-default {
            color: blue;
        }
        
        .spanLink a {
            cursor: pointer;
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="pnlFit" runat="server" />
        <f:Panel ID="pnlFit" runat="server" BodyPadding="10" ShowBorder="true" ShowHeader="false">
            <Items>
                <f:GroupPanel runat="server" ID="GroupPanel1" Title="<span class='spanTitle'> 基本概况 </span>" EnableCollapse="True" Collapsed="false">
                    <Items>
                        <f:SimpleForm ID="SimpleForm1" runat="server" ShowBorder="false" ShowHeader="false" Margin="8">
                            <Items>
                                <f:Panel ID="Panel1" runat="server" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:Label runat="server" ID="lblJjfz" Label="积极分子" Margin="0 15 0 0" ColumnWidth="34%" Text="26"></f:Label>  
                                        <f:Label runat="server" ID="lblYbdy" Label="预备党员" Margin="0 15 0 0" ColumnWidth="33%" Text="8"></f:Label>                    
                                        <f:Label runat="server" ID="lblZsdy" Label="正式党员" ColumnWidth="33%" Text="4"></f:Label>
                                    </Items>
                                </f:Panel>
                            </Items>
                        </f:SimpleForm>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel runat="server" ID="GroupPanel3" Title="<span class='spanTitle'> 项目概况 </span>" EnableCollapse="true" Collapsed="false">
                    <Items>
                        <f:SimpleForm ID="SimpleForm3" runat="server" Margin="8" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:Panel ID="Panel2" runat="server" BodyPadding="0 0 8 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:Label runat="server" ID="lblSxhb" Label="思想汇报" Margin="0 15 0 0" ColumnWidth="34%" Text="110"></f:Label>  
                                        <f:Label runat="server" ID="lblZyfw" Label="志愿服务" Margin="0 15 0 0" ColumnWidth="33%" Text="64"></f:Label>                    
                                        <f:Label runat="server" ID="lblSlx" Label="三 联 系" ColumnWidth="33%" Text="323"></f:Label>                            
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel3" runat="server" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:Label runat="server" ID="lblJshj" Label="竞赛获奖" Margin="0 15 0 0" ColumnWidth="34%" Text="13"></f:Label>  
                                        <f:Label runat="server" ID="lblQtxm" Label="其他项目" Margin="0 15 0 0" ColumnWidth="33%" Text="12"></f:Label>        
                                    </Items>
                                </f:Panel>
                            </Items>
                        </f:SimpleForm>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel runat="server" ID="GroupPanel2" Title="<span class='spanTitle'> 待审项目 </span>" EnableCollapse="true" Collapsed="false">
                    <Items>
                        <f:SimpleForm ID="SimpleForm2" runat="server" Margin="8" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:Panel ID="Panel6" runat="server" BodyPadding="0 0 8 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:HyperLink runat="server" ID="lblSxhb0" Label="思想汇报" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="34%" Text="10"></f:HyperLink>  
                                        <f:HyperLink runat="server" ID="lblZyfw0" Label="志愿服务" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="33%" Text="4"></f:HyperLink>                    
                                        <f:HyperLink runat="server" ID="lblSlx0" Label="三 联 系" CssClass="spanLink" ColumnWidth="33%" Text="23"></f:HyperLink>                            
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel7" runat="server" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:HyperLink runat="server" ID="lblJshj0" Label="竞赛获奖" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="34%" Text="3"></f:HyperLink>  
                                        <f:HyperLink runat="server" ID="lblQtxm0" Label="其他项目" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="33%" Text="2"></f:HyperLink>        
                                    </Items>
                                </f:Panel>
                            </Items>
                        </f:SimpleForm>
                    </Items>
                </f:GroupPanel>
            </Items>
        </f:Panel>
    </form>
    
    <script type="text/javascript">
        //F.ready(function () {
        //    F('wndView').f_maximize();
        //});
    </script>
</body>
</html>
