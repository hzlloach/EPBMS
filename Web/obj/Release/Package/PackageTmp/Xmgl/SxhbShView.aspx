<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SxhbShView.aspx.cs" Inherits="Web.Xmgl.SxhbShView" %>

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
        /*#SimpleForm1_ctl01_tbxHbnr-inputEl*/
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
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" runat="server" EnablePostBack="false" Text="关闭" Icon="SystemClose"></f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server"></f:ToolbarSeparator>
                        <f:Button runat="server" ID="btnAudit" Text="评阅" Icon="SystemSave"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>         
                <f:Panel runat="server" ShowBorder="false" ShowHeader="false" Height="25px" Hidden="true">    
                    <Items>     
                        <f:HiddenField runat="server" ID="hfdPkid"></f:HiddenField>
                        <f:HiddenField runat="server" ID="hfdHbbh"></f:HiddenField>
                        <f:HiddenField runat="server" ID="hfdTjxh"></f:HiddenField>
                        <f:Label runat="server" ID="lblTjxh" Label="序　　号"  CssStyle="color:Blue"></f:Label>
                    </Items>
                </f:Panel>  
                <f:Panel runat="server" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:Label runat="server" ID="lblFzr" Label="姓　　名" Margin="0 15 0 0" ColumnWidth="50%" Text=""></f:Label>                    
                        <f:Label runat="server" ID="lblFzzt" Label="培养阶段" ColumnWidth="50%" Text=""></f:Label>                            
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
                <f:Panel runat="server" Margin="10 0 0 0" ShowBorder="false" ShowHeader="false" Height="25px">
                    <Items>
                        <f:Label runat="server" Text="" Label="评阅意见"></f:Label>
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

    <f:Window ID="wndEdit" runat="server" Title="审核" WindowPosition="Center"  Target="Self" Width="500px" Height="300px"  EnableMaximize="false" EnableResize="false" EnableDrag="true"  EnableIFrame="true" Hidden="true" IsModal="true" CloseAction="Hide" OnClose="Window_Close">
    </f:Window> 

    <script src="http://www.jq22.com/jquery/jquery-1.10.2.js"></script>
    <script type="text/javascript">
    </script>
</body>
</html>
