<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="Web.test" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title></title>
    <style>
        .photo {
            height: 150px;
            line-height: 150px;
            overflow: hidden;
        }

            .photo img {
                height: 150px;
                vertical-align: middle;
            }
    
        .file{ width:80px; height:26px; margin:-3px -10px; opacity:0; filter:alpha(opacity:0); cursor:pointer; }
        .btnLink { background-image:url('../res/images/toolbar/selectFile.png'); background-position:top 0px; width:81px; height:22px; cursor:pointer; }
        .btnLink:hover{ background-position:left -22px; cursor:pointer;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="pnlFit" runat="server" />
        <f:Panel ID="pnlFit" runat="server" ShowBorder="true" ShowHeader="false">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" runat="server" EnablePostBack="false" Text="关闭" Icon="SystemClose"></f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server"></f:ToolbarSeparator>
                        <f:Button runat="server" ID="btnSave" Text="保存" Icon="SystemSave" ValidateForms="SimpleForm1"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <f:HiddenField runat="server" ID="hfdPkid"></f:HiddenField>
                        <f:HiddenField runat="server" ID="hfdFzztdm"></f:HiddenField>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                                                        
                
                    <f:ContentPanel ID="ContentPanel3" runat="server" ShowBorder="false" ShowHeader="false">
                        <div class="btnLink">
                            <asp:FileUpload ID="fudExcel" CssClass="file" runat="server" onchange="showSlectedFile(this, 'hfdFile');" />                      
                        </div>
                    </f:ContentPanel>
                    <f:FileUpload ID="fudExcel2" runat="server" Hidden="true" ButtonOnly="true" ButtonText="选择文件" ButtonIcon="PageExcel" CssStyle="width:75px;cursor:pointer;"></f:FileUpload>
                    <f:Button ID="btnImport" runat="server" Text="开始导入" ConfirmText="确认导入数据 ？" Enabled="false" Icon="DatabaseGo"></f:Button>
                    

            </Items>
        </f:Panel>
        <br />
        注意：上传个人头像通过 AcceptFileTypes="image/*" 来控制默认显示的文件类型。
    </form>
</body>
</html>
