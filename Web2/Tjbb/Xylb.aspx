<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xylb.aspx.cs" Inherits="Web.Tjbb.Xylb" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title></title>
    <link href="~/res/css/default.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/index.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/fineui.css" rel="stylesheet" type="text/css" />
    <style>
        .divBlock {
            float:left; position:relative; border:4px solid #730000; margin:10px; padding:20px 10px; width:30%; height:150px; font-size:22px; font-weight:bold; color:red; background-color:yellow;
        }
        .divMiddle {
            width:100%; height:60px; margin-top:25px; text-align:center; line-height:120%;
        }
        a {
            color: red;
            text-decoration: none;
        }
        a:hover {
            background-color: yellowgreen;
        }
        a:visited {
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="pnlFit" runat="server" />  
        <f:Panel ID="pnlFit" runat="server" Layout="Fit" BodyPadding="1" ShowBorder="false" ShowHeader="false">            
            <Items>              
                <f:ContentPanel ID="ContentPanel1" runat="server" ShowBorder="true" ShowHeader="false">
                    <div>
                        <asp:Repeater ID="rptXy" runat="server" >
                            <ItemTemplate>
                                <div class="divBlock">
                                    <a href="Zblb.aspx?bmbh=<%#Eval("Pkid").ToString() %>" target="_self" ><div class="divMiddle" ><%#Eval("Bmmc").ToString() + "（" + Eval("Xj").ToString() + "）<br/>【积极分子：" + Eval("Jjfz").ToString() + "；预备党员：" + Eval("Ybdy").ToString() + "；正式党员：" + Eval("Zsdy").ToString() + "】"%></div></a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </f:ContentPanel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
