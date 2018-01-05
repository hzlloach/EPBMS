<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Homelxr.aspx.cs" Inherits="Web.Home.Homelxr" %>

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
            color: blue;
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
                                        <f:HyperLink runat="server" ID="lblJjfz" Label="积极分子" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="34%" Text="0" NavigateUrl="../Fzgl/Xslb.aspx?fzztdm=2"></f:HyperLink>  
                                        <f:HyperLink runat="server" ID="lblYbdy" Label="预备党员" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="33%" Text="0" NavigateUrl="../Fzgl/Xslb.aspx?fzztdm=5"></f:HyperLink>  
                                        <f:HyperLink runat="server" ID="lblZsdy" Label="正式党员" CssClass="spanLink" ColumnWidth="33%" Text="0" NavigateUrl="../Fzgl/Xslb.aspx?fzztdm=6"></f:HyperLink>  
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
                                        <f:HyperLink runat="server" ID="lblSxhb" Label="思想汇报" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="34%" Text="0" NavigateUrl="../Xmgl/Sxhblb.aspx"></f:HyperLink>  
                                        <f:HyperLink runat="server" ID="lblZyfw" Label="志愿服务" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="33%" Text="0" NavigateUrl="../Xmgl/Zyfwlb.aspx"></f:HyperLink>                    
                                        <f:HyperLink runat="server" ID="lblSlx" Label="三 联 系" CssClass="spanLink" ColumnWidth="33%" Text="0" NavigateUrl="../Xmgl/Slxlb.aspx"></f:HyperLink>                            
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel3" runat="server" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:HyperLink runat="server" ID="lblJshj" Label="竞赛获奖" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="34%" Text="0" NavigateUrl="../Xmgl/Jshjlb.aspx"></f:HyperLink>  
                                        <f:HyperLink runat="server" ID="lblQtxm" Label="其他项目" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="33%" Text="0" NavigateUrl="../Xmgl/Xmsblb.aspx"></f:HyperLink>        
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
                                        <f:HyperLink runat="server" ID="lblSxhb0" Label="思想汇报" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="34%" Text="0" NavigateUrl="../Xmgl/Sxhblb.aspx?ztdm=0"></f:HyperLink>  
                                        <f:HyperLink runat="server" ID="lblZyfw0" Label="志愿服务" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="33%" Text="0" NavigateUrl="../Xmgl/Zyfwlb.aspx?ztdm=0"></f:HyperLink>                    
                                        <f:HyperLink runat="server" ID="lblSlx0" Label="三 联 系" CssClass="spanLink" ColumnWidth="33%" Text="0" NavigateUrl="../Xmgl/Slxlb.aspx?ztdm=0"></f:HyperLink>                            
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel7" runat="server" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:HyperLink runat="server" ID="lblJshj0" Label="竞赛获奖" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="34%" Text="0" NavigateUrl="../Xmgl/Jshjlb.aspx?ztdm=0"></f:HyperLink>  
                                        <f:HyperLink runat="server" ID="lblQtxm0" Label="其他项目" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="33%" Text="0" NavigateUrl="../Xmgl/Xmsblb.aspx?ztdm=0"></f:HyperLink>        
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
