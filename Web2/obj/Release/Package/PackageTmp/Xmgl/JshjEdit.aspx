<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JshjEdit.aspx.cs" Inherits="Web.Xmgl.JshjEdit" %>

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
    </style>
    <script type="text/javascript">
        $(function () {
            initView(1);
            initView(2);
            initView(3);
        });

        function initView(no) {
            if ($("[id$='hfdLj" + no + "']").val() != "")
                $("[id$='divView" + no + "']").show();
        }

        function viewImage(no) {
            var file = $("[id$='hfdLj" + no + "']").val();
            $("img").attr('src', file);
        }
        function delImage(no) {
            var file = "../res/images/nophoto.png";
            $("[id$='divView" + no + "']").hide();
            $("[id$='hfdLj" + no + "']").val("Del@");
            $("[name$='tbxBt" + no + "']").val("");
            $("img").attr('src', file);
        }
        function setFileUpload(no, file) {
            $("img").attr('src', file);
            $("[id$='hfdLj" + no + "']").val(file);
            $("[id$='divView" + no + "']").show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="pnlFit" runat="server" />
        <f:Panel ID="pnlFit" runat="server" Layout="Fit" BodyPadding="1" ShowBorder="true" ShowHeader="false">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" runat="server" EnablePostBack="false" Text="关闭" Icon="SystemClose"></f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server"></f:ToolbarSeparator>
                        <f:Button runat="server" ID="btnSave" Text="保存" Icon="SystemSave" ConfirmText="确认保存？" ValidateForms="SimpleForm1" OnClick="btnSave_Click"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <f:HiddenField runat="server" ID="hfdPkid"></f:HiddenField>
                        <f:HiddenField runat="server" ID="hfdFzrbh"></f:HiddenField>
                        <f:HiddenField runat="server" ID="hfdFzztdm"></f:HiddenField>
                        <f:HiddenField runat="server" ID="hfdYxsc"></f:HiddenField>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:TabStrip runat="server" ID="tabStrip1" TabPosition="Top" ActiveTabIndex="0" ShowBorder="true" EnableTabCloseMenu="false" AutoPostBack="true" OnTabIndexChanged="TabStrip1_TabIndexChanged">
                    <Tabs>
                        <f:Tab runat="server" ID="tabXmxx" Title="项目基本信息" BodyPadding="10" Layout="Fit">
                            <Items>
                                <f:SimpleForm ID="SimpleForm1" runat="server" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:Panel ID="Panel1" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblFzr" Label="姓　　名" Margin="0 15 0 0" ColumnWidth="70%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblFzzt" Label="培养阶段" ColumnWidth="30%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel2" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:DropDownList runat="server" ID="ddlZbbh" Label="项目类别" Width="300px" Hidden="true" Required="true" ShowRedStar="true" AutoPostBack="true" OnSelectedIndexChanged="ddlZbbh_SelectedIndexChanged"></f:DropDownList>
                                                <f:DropDownList runat="server" ID="ddlDjbh" Label="奖项等级" Margin="0 15 0 0" ColumnWidth="70%" Required="true" ShowRedStar="true" Hidden="true"></f:DropDownList>
                                                <f:DatePicker runat="server" ID="dtpXmrq" Label="获奖日期" ColumnWidth="30%" EnableEdit="false" Required="true" ShowRedStar="True"></f:DatePicker>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel4" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:TextBox runat="server" ID="tbxXmmc" Label="奖项名称" ColumnWidth="100%" MaxLength="30" EmptyText="限填 50 个汉字 ！ 格式：学年+奖项名称+等级，如：2017-2018学年浙江省第一届大学生科技竞赛甲组二等奖。" Required="true" ShowRedStar="true"></f:TextBox>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel5" runat="server" BodyPadding="0 0 30 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:TextArea runat="server" ID="tbxBz" Label="备　　注" ColumnWidth="100%" Height="50px" MaxLength="200" EmptyText="限填 200 个汉字 ！"></f:TextArea>
                                            </Items>
                                        </f:Panel>
                                        <f:ContentPanel ID="ContentPanel1" runat="server" ShowBorder="false" ShowHeader="false">
                                            <span class="Red">填写说明：<br /><asp:Literal runat="server" ID="lblTxsm" Text="" ></asp:Literal></span>					                            
                                        </f:ContentPanel>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Tab>
                        <f:Tab runat="server" ID="tabZmcl" Title="项目证明材料" Layout="HBox">
                            <Items>                               
                                <f:Panel ID="Panel6" runat="server" BoxFlex="1" BodyPadding="10" ShowHeader="false" ShowBorder="false">
                                    <Items>   
                                        <f:GroupPanel ID="GroupPanel1" runat="server" Layout="Anchor" Title="附件一">
                                            <Items>
                                                <f:TextBox runat="server" ID="tbxBt1" Label="附件标题" EmptyText="限填 10 个汉字 ！" CssClass="marginr"></f:TextBox>
                                                <f:ContentPanel runat="server" ShowBorder="false" ShowHeader="false">                                                    
                                                    <table>
                                                        <tr>
                                                            <td><f:FileUpload runat="server" ID="fudFj1" ColumnWidth="100" ButtonText="选择文件" ButtonIcon="FolderMagnify" AcceptFileTypes="image/*" ButtonOnly="true" AutoPostBack="true" OnFileSelected="fudFj_FileSelected"></f:FileUpload></td>
                                                            <td>
                                                                <div id="divView1" style="margin-top:-3px; display:none">
                                                                    <f:Button ID="btnView1" runat="server" Text="预览" Icon="Photo" OnClientClick="viewImage(1); return false;"></f:Button>
                                                                    <f:Button ID="btnDel1" runat="server" Text="删除" Icon="Delete" OnClientClick="F.confirm({ok:'delImage(1);',message:'确认删除？'}); return false;"></f:Button>
                                                                </div>
                                                            </td>
                                                            <td><f:HiddenField runat="server" ID="hfdYs1"></f:HiddenField><input type="hidden" runat="server" id="hfdLj1" /></td>
                                                        </tr>
                                                    </table>
                                                </f:ContentPanel>
                                            </Items>
                                        </f:GroupPanel>  
                                        <f:GroupPanel ID="GroupPanel2" runat="server" Layout="Anchor" Title="附件二">
                                            <Items>
                                                <f:TextBox runat="server" ID="tbxBt2" Label="附件标题" EmptyText="限填 10 个汉字 ！" CssClass="marginr"></f:TextBox>
                                                <f:ContentPanel runat="server" ShowBorder="false" ShowHeader="false">
                                                    <table>
                                                        <tr>
                                                            <td><f:FileUpload runat="server" ID="fudFj2" ColumnWidth="100" ButtonText="选择文件" ButtonIcon="FolderMagnify" AcceptFileTypes="image/*" ButtonOnly="true" AutoPostBack="true" OnFileSelected="fudFj_FileSelected"></f:FileUpload></td>
                                                            <td>
                                                                <div id="divView2" style="margin-top:-3px; display:none">
                                                                    <f:Button ID="btnView2" runat="server" Text="预览" Icon="Photo" OnClientClick="viewImage(2); return false;"></f:Button>
                                                                    <f:Button ID="btnDel2" runat="server" Text="删除" Icon="Delete" OnClientClick="F.confirm({ok:'delImage(2);',message:'确认删除？'}); return false;"></f:Button>
                                                                </div>
                                                            </td>
                                                            <td><f:HiddenField runat="server" ID="hfdYs2"></f:HiddenField><input type="hidden" runat="server" id="hfdLj2" /></td>
                                                        </tr>
                                                    </table>
                                                </f:ContentPanel>
                                            </Items>
                                        </f:GroupPanel>  
                                        <f:GroupPanel ID="GroupPanel3" runat="server" Layout="Anchor" Title="附件三">
                                            <Items>
                                                <f:TextBox runat="server" ID="tbxBt3" Label="附件标题" EmptyText="限填 10 个汉字 ！" CssClass="marginr"></f:TextBox>
                                                <f:ContentPanel runat="server" ShowBorder="false" ShowHeader="false">                                                    
                                                    <table>
                                                        <tr>
                                                            <td><f:FileUpload runat="server" ID="fudFj3" ColumnWidth="100" ButtonText="选择文件" ButtonIcon="FolderMagnify" AcceptFileTypes="image/*" ButtonOnly="true" AutoPostBack="true" OnFileSelected="fudFj_FileSelected"></f:FileUpload></td>
                                                            <td>
                                                                <div id="divView3" style="margin-top:-3px; display:none">
                                                                    <f:Button ID="btnView3" runat="server" Text="预览" Icon="Photo" OnClientClick="viewImage(3); return false;"></f:Button>
                                                                    <f:Button ID="btnDel3" runat="server" Text="删除" Icon="Delete" OnClientClick="F.confirm({ok:'delImage(3);',message:'确认删除？'}); return false;"></f:Button>
                                                                </div>
                                                            </td>
                                                            <td><f:HiddenField runat="server" ID="hfdYs3"></f:HiddenField><input type="hidden" runat="server" id="hfdLj3" /></td>
                                                        </tr>
                                                    </table>
                                                </f:ContentPanel>
                                            </Items>
                                        </f:GroupPanel>
                                        <f:ContentPanel ID="ContentPanel3" runat="server" ShowBorder="false" ShowHeader="false">
                                            <span class="Red">上传说明：<br />1、材料附件只能为图片（格式为：jpg、png）；<br/>2、图片大小不能超过 3 M；<br/>3、图片尺寸建议 800 * 600 以上。</span>
                                        </f:ContentPanel>                                        
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel12" runat="server" BoxFlex="1" Margin="0 0 0 0"  BodyPadding="10" ShowHeader="false" ShowBorder="false">
                                    <Items>
                                        <f:Image runat="server" ID="imgPhoto" ImageUrl="~/res/images/nophoto.png" ImageHeight="520" ImageWidth="390" ImageCssStyle="border:1px solid gray;">
                                        </f:Image>
                                    </Items>
                                </f:Panel>
                            </Items>
                        </f:Tab>
                    </Tabs>
                </f:TabStrip>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
