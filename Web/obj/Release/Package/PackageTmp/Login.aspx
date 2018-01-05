<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta name="viewport" content="width=device-width" /> 
        <meta http-equiv="Content-Type" content="text/html" charset="utf-8"/>  
        <title>系统登录</title> 
        <script src="res/js/jquery.min.js" type="text/javascript"></script>
        <style type="text/css">
            body { padding:0; margin:0; } 
            #tblLogin { position: absolute; width:1110px; height:610px; left:50%; top:50%; margin-left:-550px; margin-top:-305px; }

            ﻿table.tblForm {
                border:0px solid black;
                border-collapse: collapse;
                border-spacing: 0px;
                width: 100%;
                font-family: 宋体;
                font-size: 12px;            
            }
            .tblForm td {
                border:0px solid black;
                border-collapse: collapse;
                border-spacing: 0px;
                background-repeat: no-repeat;
                font-weight: normal;
            }
            .myTextbox {                
                border:1px solid #840000;
                padding:5px;
            }
            
            .grey { color:gray; text-decoration: underline; cursor:pointer; }
            .grey a{ color:gray; }
            .grey a:hover { color: #840000; }

        </style>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#tbxUserID").keydown(function (e) {
                    if (e.keyCode == 13) {
                        $("#tbxPassword").focus();
                    }
                });
                $("#tbxPassword").keydown(function (e) {
                    if (e.keyCode == 13) {
                        F("btnLogin").click();
                    }
                });
            });
        </script>
    </head>
    <body style="background-color:#6D0D0F">
        <form id="form1" runat="server">
            <f:PageManager ID="PageManager1" AjaxLoadingType="Default" runat="server" />
            <table id="tblLogin" class="tblForm" border="0" cellspacing="0" cellpadding="0">
                <tr style="height:152px">
                    <td style="width:370px; background-image:url('/res/images/login/bg_part1x1.png')"></td>
                    <td style="width:370px; background-image:url('/res/images/login/bg_part1x2.png')"></td>
                    <td style="width:370px; background-image:url('/res/images/login/bg_part1x3.png')"></td>
                </tr>
                <tr style="height:152px">
                    <td style="background-image:url('/res/images/login/bg_part2x1.png')"></td>
                    <td style="background-image:url('/res/images/login/bg_part2x2.png'); vertical-align:top;">
                        <table style="position: relative; top:112px; left:135px;" border"0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <div style="float:left; margin-top:0px;border:0px solid white"><span style="color:#84171A; font-weight:bold;" >用户名</span></div>
                                    <div style="float:left; margin:-6px 10px;border:0px solid white"><asp:TextBox runat="server" ID="tbxUserID" CssClass="myTextbox" Width="200px"></asp:TextBox></div>
                                </td>
                            </tr>
                            <tr>
                                <td style="border:0px solid white">
                                    <div style="float:left; margin-top:30px;border:0px solid white"><span style="color:#84171A; font-weight:bold;" >密　码</span></div>
                                    <div style="float:left; margin:24px 10px;border:0px solid white"><asp:TextBox runat="server" ID="tbxPassword" CssClass="myTextbox" TextMode="Password" Width="200px"></asp:TextBox></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="background-image:url('/res/images/login/bg_part2x3.png')"></td>
                </tr>
                <tr style="height:152px">
                    <td style="background-image:url('/res/images/login/bg_part3x1.png')"></td>
                    <td style="background-image:url('/res/images/login/bg_part3x2.png')">             
                        <div style="position: relative; top:-5px; left:135px; height:32px; border:0px solid white">
                            <div style="float:left; margin:5px 0px 0px 55px">
                                <f:Button runat="server" ID="btnLogin" Text="登　录" OnClick="btnLogin_Click" />
                            </div>
                            <div style="float:left; margin:5px 20px;">                         
                                <f:Button runat="server" ID="btnReg" Text="注　册" Hidden="true" />
                            </div>
                            <div style="float:left; margin:5px 0px 0px 55px;">
                                <f:HyperLink runat="server" ID="btnForget" Text="忘记密码" CssClass="grey" />
                            </div>
                        </div>
                    </td>
                    <td style="background-image:url('/res/images/login/bg_part3x3.png')"></td>
                </tr>
                <tr style="height:154px">
                 <td style="background-image:url('/res/images/login/bg_part4x1.png')"></td>
                 <td style="background-image:url('/res/images/login/bg_part4x2.png')"></td>
                 <td style="background-image:url('/res/images/login/bg_part4x3.png')"></td>
                </tr>
            </table>
        </form>

        <f:Window ID="wndReg" runat="server" Title="新用户注册" Width="410px" Height="300px" WindowPosition="Center" Target="Self" EnableMaximize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true" Hidden="true" IsModal="true" CloseAction="Hide" IFrameUrl="Reg.aspx"></f:Window>
        <f:Window ID="wndPwd" runat="server" Title="忘记密码" Width="410px" Height="250px" WindowPosition="Center" Target="Self" EnableMaximize="false" EnableResize="false" EnableDrag="false" EnableIFrame="true" Hidden="true" IsModal="true" CloseAction="Hide" IFrameUrl="Forget.aspx"></f:Window>
    </body>
</html>