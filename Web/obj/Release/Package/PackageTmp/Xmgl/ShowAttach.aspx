<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowAttach.aspx.cs" Inherits="Web.Xmgl.ShowAttach" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/res/css/index.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/table.css" rel="stylesheet" type="text/css" />
    <link href="~/res/css/fineui.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        body { font-size: 12px; text-align: center; margin: 0px; padding: 0px; }
        /*#pic{ margin:0 auto; width:600px; height:445px; padding:0; border:0px solid #333; }
        #pic img{ max-width:600px;width:expression(document.body.clientWidth > 600 ? "600px": "auto");border:0px dashed #000; }*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager2" AutoSizePanelID="Panel" runat="server" /> 
        <f:Panel ID="Panel" runat="server" BodyPadding="0 0 0 0" ShowBorder="true" ShowHeader="false" Layout="Fit">                   
            <Items>                               
                <f:ContentPanel ID="pnlImg" runat="server" BodyPadding="0 0 0 0" ShowBorder="false" ShowHeader="false" AutoScroll="true">
                    <div id="pic">
                        <f:Image runat="server" ID="img"></f:Image>
                    </div>
                </f:ContentPanel>
            </Items>
        </f:Panel>
    </form>   
</body>
</html>




