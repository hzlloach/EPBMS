<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZyfwView.aspx.cs" Inherits="Web.Xmgl.ZyfwView" %>

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
        .spanText {
            border:0px solid maroon;
            margin-top:0px;
            padding:5px;
            color: #730000;
            line-height:1.5; 
        }
    </style>
    <script type="text/javascript">
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:SimpleForm runat="server" ID="SimpleForm1" Layout="VBox" BodyPadding="10" LabelWidth="65" ShowBorder="true" ShowHeader="false">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" Hidden="true">
                    <Items>
                        <f:Button ID="btnClose" runat="server" EnablePostBack="false" Text="关闭" Icon="SystemClose"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>     
                <f:GroupPanel runat="server" ID="GroupPanel1" Title="基本信息" EnableCollapse="false">
                    <Items>
                        <f:Panel runat="server" BodyPadding="5 0 0 0" Layout="Column" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:Label runat="server" ID="lblXm" Label="姓　　名" ColumnWidth="25%" Text=""></f:Label> 
                                <f:Label runat="server" ID="lblFwrq" Label="服务日期" ColumnWidth="25%" Text=""></f:Label>                      
                                <f:Label runat="server" ID="lblFwss" Label="服务时数" ColumnWidth="25%" Text=""></f:Label>                   
                                <f:Label runat="server" ID="lblFwdd" Label="服务地点" ColumnWidth="25%" Text=""></f:Label>  
                            </Items>
                        </f:Panel>                    
                        <f:Panel ID="Panel1" runat="server" BodyPadding="0 0 15 0" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:Label runat="server" ID="lblFwnr" CssClass="spanMaroon" Label="服务内容" Text=""></f:Label>
                            </Items>
                        </f:Panel>        
                        <f:Panel runat="server" Margin="0 0 10 0" Layout="Fit" ShowBorder="true" ShowHeader="false" Hidden="true">
                            <Items>               
                                <f:ContentPanel runat="server" ShowBorder="false" ShowHeader="false" MaxHeight="50px" AutoScroll="true">
                                    <div class="spanText">
                                        <asp:Literal ID="lblFwnr2" runat="server"></asp:Literal>
                                    </div>
                                </f:ContentPanel>
                            </Items>
                        </f:Panel>             
                    </Items>
                </f:GroupPanel>       
                <f:GroupPanel runat="server" ID="gplZmcl" Title="证明材料" BoxFlex="1" EnableCollapse="false">
                    <Items>                                      
                        <f:Panel ID="Panel3" runat="server" Layout="Fit" CssClass="formitem" ShowBorder="false" ShowHeader="false" >
                            <Items>               
                                <f:ContentPanel ID="ContentPanel1" runat="server" ShowBorder="false" ShowHeader="false" AutoScroll="true">
                                    <div>
                                        <div style="color:red; margin:3px; text-align:left;"><f:Label runat="server" ID="lblHintZm" CssClass="spanRed" Text="提示：点击查看大图 ！"></f:Label></div>
                                        <asp:Repeater ID="rptZmcl" runat="server" >
                                            <ItemTemplate>
                                                <div style="float:left; margin:0px 10px 0px 3px; padding-bottom:10px;">
                                                    <a href="javascript:void(0);" onclick='showWindow("wndView", "Xmgl/ShowAttach.aspx?url=<%#TStar.Utility.Globals.EncodeUrl(Eval("Cflj")) %>","弹出窗－浏览证明材料<%#(Container.ItemIndex + 1) %>：<%#Eval("Clbt") %>");' class="ZoomImg" title='<%#"证明材料" + (Container.ItemIndex + 1) + "：" + Eval("Clbt") %>'><img style="display:block; height:200px; width:150px; border:1px solid #808080; cursor:pointer;" alt='<%#"证明材料" + (Container.ItemIndex + 1) %>' src='<%#Eval("Cflj").ToString().Replace(".pdf", "_thumb.jpg") %>' /></a>
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

    <f:Window ID="wndView" runat="server" Title="浏览证明材料" WindowPosition="Center"  Target="Top" Width="800px" Height="500px"  EnableMaximize="true" EnableMinimize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true" Hidden="true" IsModal="true" CloseAction="Hide">
    </f:Window> 
</body>
</html>
