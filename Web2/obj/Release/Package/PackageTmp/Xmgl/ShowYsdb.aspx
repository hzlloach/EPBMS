<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowYsdb.aspx.cs" Inherits="Web.Xmgl.ShowYsdb" %>

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
        .spanTitle {
            color: maroon;
            font-size:14px;
            font-weight:bold;
        }

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
                <f:SimpleForm ID="SimpleForm1" runat="server" LabelWidth="95px" ShowBorder="false" ShowHeader="false" Margin="8">
                    <Items>
                        <f:Panel ID="Panel1" runat="server" BodyPadding="0 0 8 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:Label runat="server" ID="lblXh" Label="学　　号" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                <f:Label runat="server" ID="lblXm" Label="姓　　名" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>   
                                <f:Label runat="server" ID="lblDzb" Label="支部名称" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                      
                            </Items>
                        </f:Panel>      
                        <f:Panel ID="Panel10" runat="server" BodyPadding="0 0 8 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:Label runat="server" ID="lblFzdxrq" Label="发展对象日期" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                <f:Label runat="server" ID="lblZsjg" Label="政审结果" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                <f:Label runat="server" ID="lblDbjg" Label="答辩结果" ColumnWidth="33%" Text=""></f:Label>                            
                            </Items>
                        </f:Panel>
                        <f:Panel ID="pnlDb1" runat="server" Hidden="true" BodyPadding="0 0 8 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:Label runat="server" ID="lblDbrq" Label="预审答辩日期" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                <f:Label runat="server" ID="lblDbdd" Label="预审答辩地点" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                <f:Label runat="server" ID="lblDbzcy" Label="答辩组成员" ColumnWidth="33%" Text=""></f:Label>                            
                            </Items>
                        </f:Panel>
                        <f:Panel ID="pnlDb2" runat="server" Hidden="true" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:Label runat="server" ID="lblDbpjyj" Label="答辩评价意见" ColumnWidth="100%" ></f:Label>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="pnlBz" runat="server" Hidden="true" BodyPadding="8 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:Label runat="server" ID="lblBz" Label="政审未过原因" ColumnWidth="100%" ></f:Label>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:SimpleForm>
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
