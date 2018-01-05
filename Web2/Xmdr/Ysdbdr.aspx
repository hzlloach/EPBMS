<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ysdbdr.aspx.cs" Inherits="Web.Xmdr.Ysdbdr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/res/css/default.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/index.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/fineui.css" rel="stylesheet" type="text/css" />
    <script src="../res/js/jquery.min.js" type="text/javascript"></script>
    <script src="../res/js/JScript.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showSlectedFile(file, txt_id) {
            $("[id$='" + txt_id + "']").val(file.value);
            clickCtrl("btnUpload");
        }
    </script>
    <style type="text/css">
        .file{ width:80px; height:26px; margin:-3px -10px; opacity:0; filter:alpha(opacity:0); cursor:pointer; }
        .btnLink { background-image:url('../images/toolbar/selectFile.png'); background-position:top 0px; width:81px; height:22px; cursor:pointer; }
        .btnLink:hover{ background-position:left -22px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />                     
        <f:Panel ID="Panel1" runat="server" Layout="Fit" BoxConfigAlign="Stretch" BoxConfigPadding="1" BoxConfigChildMargin="0 0 0 0" ShowBorder="false" ShowHeader="false">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" runat="server" EnablePostBack="false" Text="返回" Icon="SystemClose"></f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server"></f:ToolbarSeparator>
                        <%--<f:ContentPanel ID="ContentPanel3" runat="server" ShowBorder="false" ShowHeader="false">
                            <div class="btnLink">
                                <asp:FileUpload ID="fudExcel" CssClass="file" runat="server" onchange="showSlectedFile(this, 'hfdFile');" />                      
                            </div>
                        </f:ContentPanel>--%>
                        <f:FileUpload ID="fudExcel2" runat="server" ButtonOnly="true" ButtonText="选择文件" ButtonIcon="PageExcel" AutoPostBack="true" OnFileSelected="btnUpload_Click"></f:FileUpload>
                        <f:Button ID="btnImport" runat="server" Text="开始导入" ConfirmText="确认导入数据 ？" Enabled="false" Icon="DatabaseGo" OnClick="btnImport_Click"></f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server"></f:ToolbarSeparator>
                        <f:Button ID="btnTemplate" runat="server" Text="下载模版" Icon="DiskDownload" EnablePostBack="false"></f:Button>
                        <f:ContentPanel ID="ContentPanel2" runat="server" Hidden="true" ShowBorder="false" ShowHeader="false">
                    	    <div style="color:red; font-size:12px;">
                                <f:CheckBox ID="cbxClear" runat="server" Text="导入前先清空"></f:CheckBox>
                            </div>
                        </f:ContentPanel>
                        <f:ContentPanel ID="ContentPanel1" runat="server" CssClass="divHidden" ShowBorder="false" ShowHeader="false">
                    	    <f:Button ID="btnUpload" runat="server" Text="上传数据" Icon="WorldGo" CssStyle="cursor:pointer;" OnClick="btnUpload_Click"></f:Button>
                            <f:HiddenField ID="hfdFile" runat="server"></f:HiddenField>
                            <f:HiddenField ID="hfdTableName" runat="server"></f:HiddenField>
                        </f:ContentPanel>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Panel ID="pnlGrid" runat="server" ShowBorder="false" ShowHeader="false" Layout="Fit">
                    <Items>
                        <f:Grid ID="Grid1" runat="server" Title="Grid1" AllowPaging="true" AllowSorting="false" BoxConfigAlign="Middle" EnableCheckBoxSelect="false" ShowBorder="false" ShowHeader="False" IsDatabasePaging="false">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" Width="50px"  />
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>

