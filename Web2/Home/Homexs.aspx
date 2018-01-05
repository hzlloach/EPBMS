<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Homexs.aspx.cs" Inherits="Web.Home.Homexs" %>

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
            <Items>
                <f:GroupPanel runat="server" ID="gplJbxx" Layout="HBox" Title="<span class='spanTitle'> 基本信息 </span>" EnableCollapse="True" Collapsed="false">
                    <Items>
                        <f:Panel ID="Panel14" runat="server" BodyPadding="0 0 10 0" BoxFlex="1" Layout="Fit" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:SimpleForm ID="SimpleForm1" runat="server" LabelWidth="65px" BodyPadding="10" ShowBorder="false" ShowHeader="false" AutoScroll="true">
                                    <Items>
                                        <f:Panel ID="Panel1" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblXh" Label="学　　号" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblXm" Label="姓　　名" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblFzjd" Label="发展阶段" CssClass="spanRed" ColumnWidth="33%" Text=""></f:Label><f:HiddenField ID="hfdFzztdm" runat="server"></f:HiddenField>                       
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel2" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblDzb" Label="支部名称" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblZymc" Label="专业名称" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblBjmc" Label="班级名称" ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel3" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblXb" Label="性　　别" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>                            
                                                <f:Label runat="server" ID="lblSfzh" Label="身份证号" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblSjhm" Label="手　　机" ColumnWidth="33%" Text=""></f:Label>                    
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel4" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblMz" Label="民　　族" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>                            
                                                <f:Label runat="server" ID="lblJg" Label="籍　　贯" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblZw" Label="职　　务" ColumnWidth="33%" Text=""></f:Label>                    
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel5" runat="server" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblJtdz" Label="家庭地址" ColumnWidth="100%" ></f:Label>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel15" runat="server" Width="90px"  Margin="0 0 0 0"  BodyPadding="0" ShowHeader="false" ShowBorder="false">
                            <Items>
                                <f:Image runat="server" ID="imgPhoto" ImageUrl="~/uploads/photo/default.jpg" ImageHeight="112" ImageWidth="80" ImageCssStyle="border:1px solid gray;">
                                </f:Image>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel runat="server" ID="gplFzxx" Layout="HBox" Title="<span class='spanTitle'> 培养信息 </span>" EnableCollapse="true" Collapsed="false">
                    <Items>
                        <f:Panel ID="Panel9" runat="server" BodyPadding="0 0 10 0" BoxFlex="1" Layout="Fit" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:SimpleForm ID="SimpleForm2" runat="server" LabelWidth="92" BodyPadding="10" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:Panel ID="Panel6" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblSqrdrq" Label="申请入党日期" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblJjfzrq" Label="积极分子日期" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblLxr1" Label="联系人一" ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel7" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblDxjyrq" Label="党校结业日期" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblDxkhzt" Label="党校考核结果" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblLxr2" Label="联系人二" ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel8" runat="server" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblXxcjpm" Label="学习成绩排名" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblZhkppm" Label="综合考评排名" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblBjgms" Label="不及格课程数" ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="pnlFzh" runat="server" BodyPadding="10 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false" Hidden="true">
                                            <Items>
                                                <f:Label runat="server" ID="lblRdrq" Label="入党日期" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblZysbh" Label="志愿书编号" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblZzrq" Label="待转正日期" ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel13" runat="server" Width="120px"  Margin="0 0 0 0"  BodyPadding="0" ShowHeader="false" ShowBorder="false">
                            <Items>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel runat="server" ID="gplPycl" Layout="HBox" Title="<span class='spanTitle'> 培养材料 </span>" EnableCollapse="true" Collapsed="false">
                    <Items>
                        <f:Panel ID="Panel18" runat="server" BodyPadding="0 0 10 0" BoxFlex="1" Layout="Fit" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:SimpleForm ID="SimpleForm4" runat="server" LabelWidth="65" BodyPadding="10" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:Panel ID="Panel19" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblSxhb" Label="思想汇报" Margin="0 15 0 0" ColumnWidth="34%" Text="0"></f:Label>  
                                                <f:Label runat="server" ID="lblZyfw" Label="志愿服务" Margin="0 15 0 0" ColumnWidth="33%" Text="0"></f:Label>                    
                                                <f:Label runat="server" ID="lblSlx" Label="三 联 系" ColumnWidth="33%" Text="0"></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel20" runat="server" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblJshj" Label="竞赛获奖" Margin="0 15 0 0" ColumnWidth="34%" Text="0"></f:Label>  
                                                <f:Label runat="server" ID="lblQtxm" Label="其他项目" Margin="0 15 0 0" ColumnWidth="33%" Text="0"></f:Label>                           
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel23" runat="server" Width="120px"  Margin="0 0 0 0"  BodyPadding="0" ShowHeader="false" ShowBorder="false">
                            <Items>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel runat="server" ID="gplYsdb" Layout="HBox" Title="<span class='spanTitle'> 预审答辩 <span>" EnableCollapse="True" Collapsed="false" Hidden="true">
                    <Items>
                        <f:Panel ID="Panel17" runat="server" BodyPadding="0 0 10 0" BoxFlex="1" Layout="Fit" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:SimpleForm ID="SimpleForm3" runat="server" LabelWidth="92" BodyPadding="10" ShowBorder="false" ShowHeader="false">
                                    <Items>                
                                        <f:Panel ID="Panel10" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblFzdxrq" Label="发展对象日期" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblZsjg" Label="政审结果" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblDbjg" Label="答辩结果" ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel11" runat="server" BodyPadding="0 0 10 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblDbrq" Label="预审答辩日期" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblDbdd" Label="预审答辩地点" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblDbzcy" Label="答辩组成员" ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel12" runat="server" BodyPadding="0 0 0 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblDbpjyj" Label="答辩评价意见" ColumnWidth="100%" ></f:Label>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="pnlBz" runat="server" BodyPadding="10 0 0 0" Layout="Column" CssClass="formitem" Hidden="true" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblBz" Label="政审未过原因" ColumnWidth="100%" ></f:Label>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel16" runat="server" Width="120px"  Margin="0 0 0 0"  BodyPadding="0" ShowHeader="false" ShowBorder="false">
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
