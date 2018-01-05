<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XsEdit.aspx.cs" Inherits="Web.Fzgl.XsEdit" %>

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
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="pnlFit" runat="server" />
        <f:Panel ID="pnlFit" runat="server" BodyPadding="10" ShowBorder="true" ShowHeader="false" AutoScroll="true">
            <Toolbars>
                <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                        <f:Button ID="btnClose" runat="server" EnablePostBack="false" Text="关闭" Icon="SystemClose"></f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server"></f:ToolbarSeparator>
                        <f:Button runat="server" ID="btnSave" Text="保存" Icon="SystemSave" ConfirmText="确认保存？" ValidateForms="SimpleForm1" OnClick="btnSave_Click"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:GroupPanel runat="server" ID="gplJbxx" Layout="HBox" Title="<span class='spanTitle'> 基本信息 </span>" EnableCollapse="True" Collapsed="false">
                    <Items>
                        <f:Panel ID="Panel14" runat="server" BodyPadding="0 0 10 0" BoxFlex="1" Layout="Fit" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:SimpleForm ID="SimpleForm1" runat="server" LabelWidth="65px" BodyPadding="10" ShowBorder="false" ShowHeader="false" AutoScroll="true">
                                    <Items>
                                        <f:Panel ID="Panel1" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:TextBox runat="server" ID="tbxXh" Label="学　　号" Margin="0 30 0 0" MaxLength="20" ColumnWidth="34%" Text=""></f:TextBox><f:HiddenField runat="server" ID="lblXhOld"></f:HiddenField>
                                                <f:TextBox runat="server" ID="tbxXm" Label="姓　　名" Margin="0 30 0 0" MaxLength="20" ColumnWidth="33%" Text=""></f:TextBox><f:HiddenField runat="server" ID="lblXmOld"></f:HiddenField>
                                                <f:Label runat="server" ID="lblFzjd" Label="发展阶段" Margin="0 30 0 0" CssClass="spanRed" ColumnWidth="33%" Text=""></f:Label><f:HiddenField ID="hfdFzztdm" runat="server"></f:HiddenField>                       
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel2" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:DropDownList runat="server" ID="ddlDzbbh" Label="支部名称" Margin="0 30 0 0" ColumnWidth="34%" Text="" AutoPostBack="true" OnSelectedIndexChanged="ddlDzb_SelectedIndexChanged"></f:DropDownList>  
                                                <f:DropDownList runat="server" ID="ddlZybh" Label="专业名称" Margin="0 30 0 0" ColumnWidth="33%" Text="" AutoPostBack="true" OnSelectedIndexChanged="ddlZymc_SelectedIndexChanged"></f:DropDownList>                    
                                                <f:DropDownList runat="server" ID="ddlBjbh" Label="班级名称" Margin="0 30 0 0" ColumnWidth="33%" Text=""></f:DropDownList>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel3" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:DropDownList runat="server" ID="ddlXbdm" Label="性　　别" Margin="0 30 0 0"  ColumnWidth="34%" Text=""></f:DropDownList>                            
                                                <f:TextBox runat="server" ID="tbxSfzh" Label="身份证号" Margin="0 30 0 0"   MaxLength="18" ColumnWidth="33%" Text=""></f:TextBox>  
                                                <f:TextBox runat="server" ID="tbxSjhm" Label="手　　机" Margin="0 30 0 0"  MaxLength="20" ColumnWidth="33%" Text=""></f:TextBox><f:HiddenField runat="server" ID="lblSjhmOld"></f:HiddenField>                    
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel4" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:TextBox runat="server" ID="tbxMz" Label="民　　族" Margin="0 30 0 0"  MaxLength="20" ColumnWidth="34%" Text=""></f:TextBox>                            
                                                <f:TextBox runat="server" ID="tbxJg" Label="籍　　贯" Margin="0 30 0 0"   MaxLength="20" ColumnWidth="33%" Text=""></f:TextBox>  
                                                <f:TextBox runat="server" ID="tbxZw" Label="职　　务" Margin="0 30 0 0"  MaxLength="25" ColumnWidth="33%" Text=""></f:TextBox>                    
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel5" runat="server" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:TextBox runat="server" ID="tbxJtdz" Label="家庭地址" Margin="0 30 0 0" MaxLength="50" ColumnWidth="100%" ></f:TextBox>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel15" runat="server" Width="90px" BodyPadding="0" ShowHeader="false" ShowBorder="false">
                            <Items>
                                <f:Image runat="server" ID="imgPhoto" ImageUrl="~/uploads/photo/default.jpg" ImageHeight="112" ImageWidth="80" ImageCssStyle="border:1px solid gray;">
                                </f:Image>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel runat="server" ID="gplFzq" Layout="HBox" Title="<span class='spanTitle'> 发展前 </span>" EnableCollapse="true" Collapsed="false">
                    <Items>
                        <f:Panel ID="Panel9" runat="server" BodyPadding="0 0 10 0" BoxFlex="1" Layout="Fit" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:SimpleForm ID="SimpleForm2" runat="server" LabelWidth="92" BodyPadding="10" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:Panel ID="Panel6" runat="server" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:TextBox runat="server" ID="tbxSqrdrq" Label="申请入党日期" Margin="0 30 0 0"  MaxLength="10" ColumnWidth="34%" Text=""></f:TextBox>  
                                                <f:TextBox runat="server" ID="tbxJjfzrq" Label="积极分子日期" Margin="0 30 0 0"   MaxLength="10" ColumnWidth="33%" Text=""></f:TextBox>                    
                                                <f:DropDownList runat="server" ID="ddlRdlxrbh1" Label="入党联系人" Margin="0 30 0 0"  ColumnWidth="33%" Text=""></f:DropDownList>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel7" runat="server" BodyPadding="10 0 0 0" Hidden="true" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:TextBox runat="server" ID="tbxDxjyrq" Label="党校结业日期" Margin="0 30 0 0"  MaxLength="10" ColumnWidth="34%" Text=""></f:TextBox>  
                                                <f:DropDownList runat="server" ID="ddlDxkhztdm" Label="党校考核结果" Margin="0 30 0 0"   ColumnWidth="33%" Text=""></f:DropDownList>                    
                                                <f:TextBox runat="server" ID="tbxLxr2" Label="联 系 人 二" Margin="0 30 0 0"  ColumnWidth="33%" Text="" Hidden="true"></f:TextBox>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel8" runat="server" BodyPadding="10 0 0 0" Hidden="true" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:TextBox runat="server" ID="tbxXxcjpm" Label="学习成绩排名" Margin="0 30 0 0"  MaxLength="7" ColumnWidth="34%" Text=""></f:TextBox>  
                                                <f:TextBox runat="server" ID="tbxZhkppm" Label="综合考评排名" Margin="0 30 0 0"   MaxLength="7" ColumnWidth="33%" Text=""></f:TextBox>                    
                                                <f:TextBox runat="server" ID="tbxBjgms" Label="不及格课程数" Margin="0 30 0 0"  ColumnWidth="33%" MaxLength="2" Text=""></f:TextBox>                            
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel13" runat="server" Width="90px" BodyPadding="0" ShowHeader="false" ShowBorder="false">
                            <Items>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel runat="server" ID="gplYsdb" Layout="HBox" Title="<span class='spanTitle'> 发展中 <span>" EnableCollapse="True" Collapsed="false" Hidden="true">
                    <Items>
                        <f:Panel ID="Panel17" runat="server" BodyPadding="0 0 10 0" BoxFlex="1" Layout="Fit" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:SimpleForm ID="SimpleForm3" runat="server" LabelWidth="92" BodyPadding="10" ShowBorder="false" ShowHeader="false">
                                    <Items>                
                                        <f:Panel ID="Panel10" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblFzdxrq" Label="发展对象日期" Margin="0 30 0 0"  ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblZsjg" Label="政审结果" Margin="0 30 0 0"   ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblDbjg" Label="答辩结果" Margin="0 30 0 0"  ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel11" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblDbrq" Label="预审答辩日期" Margin="0 30 0 0"  ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblDbdd" Label="预审答辩地点" Margin="0 30 0 0"   ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblDbzcy" Label="答辩组成员" Margin="0 30 0 0"  ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel12" runat="server" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblDbpjyj" Label="答辩评价意见" ColumnWidth="100%" ></f:Label>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="pnlBz" runat="server" BodyPadding="10 0 0 0" Layout="Column" CssClass="formitem" Hidden="true" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblBz" Label="政审未过原因" Margin="0 30 0 0" ColumnWidth="100%" ></f:Label>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel16" runat="server" Width="90px" BodyPadding="0" ShowHeader="false" ShowBorder="false">
                            <Items>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel runat="server" ID="gplFzh" Layout="HBox" Title="<span class='spanTitle'> 发展后 </span>" EnableCollapse="true" Collapsed="false" Hidden="true">
                    <Items>
                        <f:Panel ID="Panel18" runat="server" BodyPadding="0 0 10 0" BoxFlex="1" Layout="Fit" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:SimpleForm ID="SimpleForm4" runat="server" LabelWidth="92" BodyPadding="10" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:Panel ID="pnlFzh" runat="server" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:TextBox runat="server" ID="tbxRdrq" Label="入 党 日 期" Margin="0 30 0 0"  ColumnWidth="34%" Text=""></f:TextBox>  
                                                <f:TextBox runat="server" ID="tbxZysbh" Label="志愿书编号" Margin="0 30 0 0"   ColumnWidth="33%" Text=""></f:TextBox>                    
                                                <f:TextBox runat="server" ID="tbxZzrq" Label="延期转正日期" Margin="0 30 0 0"  ColumnWidth="33%" Text=""></f:TextBox>                            
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel23" runat="server" Width="90px" BodyPadding="0" ShowHeader="false" ShowBorder="false">
                            <Items>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:GroupPanel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
