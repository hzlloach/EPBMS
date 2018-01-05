<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SxhbEdit.aspx.cs" Inherits="Web.Xmgl.SxhbEdit" %>

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
        .x-form-item-label-default {
            color: maroon;
            font-size:15px;
        }

        #SimpleForm1_ctl01_tbxHbnr-inputEl {
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
                        <f:Button runat="server" ID="btnSave" Text="保存" Icon="SystemSave" ConfirmText="确认保存？" ValidateForms="SimpleForm1" OnClick="btnSave_Click"></f:Button>
                        <f:Button runat="server" ID="btnSubmit" Text="保存并提交" Icon="LaptopGo" Enabled="false" ConfirmText="确认提交？" ValidateForms="SimpleForm1" OnClick="btnSubmit_Click"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>  
                        <f:HiddenField runat="server" ID="hfdPkid"></f:HiddenField>
                        <f:HiddenField runat="server" ID="hfdHbbh"></f:HiddenField>
                        <%--<f:ContentPanel ID="ContentPanel2" runat="server" CssClass="divHidden" ShowBorder="false" ShowHeader="false">
                    	            
                        </f:ContentPanel>--%>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items> 
                <f:ContentPanel runat="server" ShowBorder="false" ShowHeader="false">
                    <div style="text-align:center; color:#730000; font-size:20px; font-family:黑体 Arial; font-weight:bolder;"><span>思 想 汇 报</span>
                    </div>
                </f:ContentPanel>             
                <f:Panel runat="server" ShowBorder="false" ShowHeader="false" BoxFlex="1" Layout="Fit" BodyPadding="0 0 0 0" CssClass="PnlHbnr">  
                    <Items>
                        <f:TextArea runat="server" ID="tbxHbnr" Label="敬爱的党组织" LabelAlign="Top" MaxLength="1500" EmptyText="请输入汇报内容正文，限填 800 ～ 1500 个汉字 ！" Required="true" Hidden="false"></f:TextArea>
                    </Items>
                </f:Panel>                
                <f:Panel ID="pnlHint" runat="server" ShowBorder="false" ShowHeader="false">  
                    <Items>
                        <f:ContentPanel ID="ContentPanel1" runat="server" ShowBorder="false" ShowHeader="false" Height="25px">
                            <div style="text-align:right;color:gray; margin-top:5px">
                                <span>限填 </span><asp:Label runat="server" ForeColor="Black" Font-Bold="true" Text="800～1500"></asp:Label><span> 字，已输入 </span><asp:Label runat="server" ID="lblYsr" ForeColor="Red" Font-Bold="true" Text="0"></asp:Label><span> 字，还可输入 </span><asp:Label runat="server" ID="lblKsr" ForeColor="Blue" Font-Bold="true" Text="1500"></asp:Label><span> 字 ！</span>
                            </div>
                        </f:ContentPanel>
                    </Items>
                </f:Panel>           
            </Items>
        </f:SimpleForm>
    </form>    

    <script src="http://www.jq22.com/jquery/jquery-1.10.2.js"></script>
    <script type="text/javascript">
        var tbxHbnrClientID = '<%= tbxHbnr.ClientID %>';
        var btnSaveClientID = '<%= btnSave.ClientID %>';
        var btnSubmClientID = '<%= btnSubmit.ClientID %>';

        F.ready(function () {
            var tbxHbnr = F(tbxHbnrClientID);
            var btnSave = F(btnSaveClientID);
            var btnSubm = F(btnSubmClientID);
            setYsr(tbxHbnr.value.length);

            $('#' + tbxHbnr.id).keyup(function (evt) {
                var len = tbxHbnr.value.length;
                setYsr(len);
                setButton(len);
            });

            function setYsr(zsYsr) {
                $("[id$='lblYsr']").html(zsYsr);
                var zsKsr = 1500 - parseInt(zsYsr, 10);
                $("[id$='lblKsr']").html(zsKsr);

                if (zsYsr >= 800 && zsYsr <= 1500) {
                    //btnSave.enable();
                    btnSubm.enable();
                }
                else {
                    //btnSave.disable();
                    btnSubm.disable();
                }
            }
        });
    </script>
</body>
</html>
