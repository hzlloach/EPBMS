<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JshjView.aspx.cs" Inherits="Web.Xmgl.JshjView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/res/css/index.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/table.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/fineui.css" rel="stylesheet" type="text/css" />
    <script src="../res/js/jquery.min.js"></script>
    <script src="../res/js/JScript.js"></script>
    
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
        <f:SimpleForm runat="server" ID="SimpleForm1" Layout="VBox" BodyPadding="10" LabelWidth="65" ShowBorder="true" ShowHeader="false" AutoScroll="true">
            <Toolbars>
                <f:Toolbar ID="tbrAudit" runat="server" Hidden="true">
                    <Items>
                        <f:Button runat="server" ID="btnAudit" Text="审核" Icon="PageEdit"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>     
                <f:GroupPanel runat="server" ID="GroupPanel1" Title="<span class='spanTitle'> 基本信息 </span>" EnableCollapse="true">
                    <Items>
                        <f:Panel runat="server" BodyPadding="8" Layout="Column" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:Label runat="server" ID="lblXm" Label="姓　　名" ColumnWidth="25%" Text=""></f:Label>                     
                                <f:Label runat="server" ID="lblJxdj" Label="奖项等级" ColumnWidth="25%" Text=""></f:Label>
                                <f:Label runat="server" ID="lblHjrq" Label="获奖日期" ColumnWidth="25%" Text=""></f:Label>    
                            </Items>
                        </f:Panel>                    
                        <f:Panel ID="Panel1" runat="server" Margin="-5 8 8 8"  CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:Label runat="server" ID="lblJxmc" CssClass="spanMaroon" Label="奖项名称" Text=""></f:Label>
                            </Items>
                        </f:Panel>            
                    </Items>
                </f:GroupPanel>   
                <f:GroupPanel runat="server" ID="gplAudit" Title="<span class='spanTitle'> 审核意见 </span>" EnableCollapse="true" Hidden="true">
                    <Items>
                        <f:Panel ID="Panel2" runat="server" BodyPadding="8" Layout="Column" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:Label runat="server" ID="lblShzt" Label="审核结果" CssClass="spanRed" ColumnWidth="25%" Text=""></f:Label>                       
                                <f:Label runat="server" ID="lblShr"  Label="审 核 人" ColumnWidth="25%" Text=""></f:Label> 
                                <f:Label runat="server" ID="lblShsj" Label="审核时间" ColumnWidth="50%" Text=""></f:Label>
                            </Items>
                        </f:Panel>                    
                        <f:Panel ID="pnlShyj" runat="server" Margin="-5 8 8 8" CssClass="formitem" ShowBorder="false" ShowHeader="false" Hidden="true">
                            <Items>
                                <f:Label runat="server" ID="lblShyj" CssClass="spanMaroon" Label="审核意见" Text=""></f:Label>
                            </Items>
                        </f:Panel>         
                    </Items>
                </f:GroupPanel>           
                <f:GroupPanel runat="server" ID="gplZmcl" Title="<span class='spanTitle'> 证明材料 </span>" BoxFlex="1" EnableCollapse="true">
                    <Items>                                      
                        <f:Panel ID="Panel3" runat="server" Layout="Fit" CssClass="formitem" ShowBorder="false" ShowHeader="false" >
                            <Items>               
                                <f:ContentPanel ID="ContentPanel1" runat="server" ShowBorder="false" ShowHeader="false" AutoScroll="true">
                                    <div>
                                        <div style="color:red; margin:3px 8px 0px 8px; text-align:left;"><f:Label runat="server" ID="lblHintZm" CssClass="spanRed" Text="提示：点击查看大图 ！"></f:Label></div>
                                        <asp:Repeater ID="rptZmcl" runat="server" >
                                            <ItemTemplate>
                                                <div style="float:left; margin:0px 10px 0px 8px; padding-bottom:10px;">
                                                    <a href="javascript:void(0);" onclick='showWindow("wndView", "Xmgl/ShowAttach.aspx?url=<%#TStar.Utility.Globals.EncodeUrl(Eval("Cflj")) %>","弹出窗－浏览证明材料<%#(Container.ItemIndex + 1) %>：<%#Eval("Clbt") %>");' class="ZoomImg" title='<%#"证明材料" + (Container.ItemIndex + 1) + "：" + Eval("Clbt") %>'><img style="display:block; height:200px; width:150px; margin-top:-10px; border:1px solid #808080; cursor:pointer;" alt='<%#"证明材料" + (Container.ItemIndex + 1) %>' src='<%#Eval("Cflj").ToString().Replace(".pdf", "_thumb.jpg") %>' /></a>
                                                    <span id="divTitle" runat="server" style="display:block; width:150px; border:1px solid none; text-align:center; color:maroon; padding-top:5px; line-height:120%"><%#Eval("Clbt") %></span>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </f:ContentPanel>
                            </Items>
                        </f:Panel>  
                    </Items>
                </f:GroupPanel>
            </Items>
        </f:SimpleForm>
    </form>   

    <f:Window ID="wndView" runat="server" Title="浏览证明材料" WindowPosition="Center" Target="Top" Width="800px" Height="500px"  EnableMaximize="true" EnableMinimize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true" Hidden="true" IsModal="true" CloseAction="Hide">
    </f:Window>
    <f:Window ID="wndEdit" runat="server" Title="审核" WindowPosition="Center" Target="Self" Width="500px" Height="300px"  EnableMaximize="false" EnableResize="false" EnableDrag="true"  EnableIFrame="true" Hidden="true" IsModal="true" CloseAction="Hide" OnClose="Window_Close">
    </f:Window> 
</body>
</html>
