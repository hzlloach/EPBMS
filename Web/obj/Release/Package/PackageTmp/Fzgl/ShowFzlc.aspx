<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowFzlc.aspx.cs" Inherits="Web.Xmgl.ShowFzlc" %>

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
        
        .spanLink a {
            cursor: pointer;
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="pnlFit" runat="server" />
        <f:Panel ID="pnlFit" runat="server" Layout="Fit" BodyPadding="1" ShowBorder="true" ShowHeader="false">
            <Items>
                <f:TabStrip runat="server" ID="tabStrip1" TabPosition="Top" ActiveTabIndex="0" ShowBorder="true" EnableTabCloseMenu="false" AutoPostBack="true" >
                    <Tabs>
                        <f:Tab runat="server" ID="tabGrxx" Title="个人基本信息" BodyPadding="10" Layout="Fit">
                            <Items>
                                <f:SimpleForm ID="SimpleForm1" runat="server" ShowBorder="false" ShowHeader="false" AutoScroll="true">
                                    <Items>
                                        <f:Panel ID="Panel1" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblXh" Label="学　　号" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblXm" Label="姓　　名" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblFzjd" Label="发展阶段" CssClass="spanRed" ColumnWidth="33%" Text=""></f:Label><f:HiddenField ID="hfdFzztdm" runat="server"></f:HiddenField>                       
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel2" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblDzb" Label="支部名称" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblZymc" Label="专业名称" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblBjmc" Label="班级名称" ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel3" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblXb" Label="性　　别" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>                            
                                                <f:Label runat="server" ID="lblSfzh" Label="身份证号" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblSjhm" Label="手　　机" ColumnWidth="33%" Text=""></f:Label>                    
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel4" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblMz" Label="民　　族" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>                            
                                                <f:Label runat="server" ID="lblJg" Label="籍　　贯" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblZw" Label="职　　务" ColumnWidth="33%" Text=""></f:Label>                    
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel5" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblJtdz" Label="家庭地址" ColumnWidth="100%" ></f:Label>
                                            </Items>
                                        </f:Panel>
                                        <f:ContentPanel ID="ContentPanel1" runat="server" Hidden="true" ShowBorder="false" ShowHeader="false">
                                            <span class="Red">填写说明：<br /><asp:Literal runat="server" ID="lblTxsm" Text="" ></asp:Literal></span>					                            
                                        </f:ContentPanel>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Tab>
                        <f:Tab runat="server" ID="tabFzq" Title="发展前信息" BodyPadding="10" Layout="Fit">
                            <Items>
                                <f:SimpleForm ID="SimpleForm2" runat="server" LabelWidth="95" ShowBorder="false" ShowHeader="false" AutoScroll="true">
                                    <Items>
                                        <f:Panel ID="Panel6" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblSqrdrq" Label="申请入党日期" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblJjfzrq" Label="积极分子日期" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblLxr1" Label="联系人一" ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel7" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblDxjyrq" Label="党校结业日期" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblDxkhzt" Label="党校考核状态" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblLxr2" Label="联系人二" ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel8" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblXxcjpm" Label="学习成绩排名" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblZhkppm" Label="综合考评排名" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblBjgms" Label="不及格课程数" ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel9" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:HyperLink runat="server" ID="lblSxhb0" Label="思想汇报篇数" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:HyperLink>              
                                                <f:HyperLink runat="server" ID="lblZyfw0" Label="志愿服务时数" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:HyperLink>   
                                                <f:HyperLink runat="server" ID="lblJshj0" Label="竞赛获奖数量" CssClass="spanLink" ColumnWidth="33%" Text=""></f:HyperLink>                    
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Tab>
                        <f:Tab runat="server" ID="tabFzz" Hidden="true" Title="发展中信息" BodyPadding="10" Layout="Fit">
                            <Items>
                                <f:SimpleForm ID="SimpleForm3" runat="server" LabelWidth="95" ShowBorder="false" ShowHeader="false" AutoScroll="true">
                                    <Items>
                                        <f:Panel ID="Panel10" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblFzdxrq" Label="发展对象日期" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblZsjg" Label="政审结果" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblDbjg" Label="答辩结果" ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel11" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblDbrq" Label="预审答辩日期" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblDbdd" Label="预审答辩地点" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblDbzcy" Label="答辩组成员" ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel12" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblDbpjyj" Label="答辩评价意见" ColumnWidth="100%" ></f:Label>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="pnlBz" runat="server" Hidden="true" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblBz" Label="政审未过原因" ColumnWidth="100%" ></f:Label>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Tab>                        
                        <f:Tab runat="server" ID="tabFzh" Hidden="true" Title="发展后信息" BodyPadding="10" Layout="Fit">
                            <Items>
                                <f:SimpleForm ID="SimpleForm4" runat="server" LabelWidth="95" ShowBorder="false" ShowHeader="false" AutoScroll="true">
                                    <Items>
                                        <f:Panel ID="Panel13" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:Label runat="server" ID="lblRdrq" Label="入党日期" Margin="0 15 0 0" ColumnWidth="34%" Text=""></f:Label>  
                                                <f:Label runat="server" ID="lblZysbh" Label="志愿书编号" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:Label>                    
                                                <f:Label runat="server" ID="lblZzrq" Label="待转正日期" ColumnWidth="33%" Text=""></f:Label>                            
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="pnlTjYb" runat="server" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:HyperLink runat="server" ID="lblSxhb1" Label="思想汇报篇数" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="34%" Text="" ></f:HyperLink>              
                                                <f:HyperLink runat="server" ID="lblZyfw1" Label="志愿服务时数" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:HyperLink>    
                                                <f:HyperLink runat="server" ID="lblSlx1" Label="三联系情况" CssClass="spanLink" ColumnWidth="33%" Text=""></f:HyperLink>                    
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="pnlTjZs" runat="server" Hidden="true" BodyPadding="0 0 15 0" Layout="Column" CssClass="formitem" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <f:HyperLink runat="server" ID="lblSxhb2" Label="思想汇报篇数" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="34%" Text="" NavigateUrl="javascript" ></f:HyperLink>              
                                                <f:HyperLink runat="server" ID="lblZyfw2" Label="志愿服务时数" CssClass="spanLink" Margin="0 15 0 0" ColumnWidth="33%" Text=""></f:HyperLink>   
                                                <f:HyperLink runat="server" ID="lblSlx2" Label="三联系情况" CssClass="spanLink" ColumnWidth="33%" Text=""></f:HyperLink>          
                                                <%--<f:HyperLink runat="server" ID="HyperLink3" Label="竞赛获奖数量" ColumnWidth="33%" Text=""></f:HyperLink> --%>                                
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Tab>
                    </Tabs>
                </f:TabStrip>
            </Items>
        </f:Panel>
    </form>
    
    <f:Window ID="wndView" runat="server" Title="弹出窗－" WindowPosition="Center" Target="Top" Width="1000px" Height="600px" EnableMaximize="true" EnableMinimize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true"  IsModal="true" Hidden="true" CloseAction="Hide">
    </f:Window>
    
    <script type="text/javascript">
        //F.ready(function () {
        //    F('wndView').f_maximize();
        //});
    </script>
</body>
</html>
