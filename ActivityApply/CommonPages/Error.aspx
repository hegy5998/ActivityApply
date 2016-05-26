<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Web.CommonPages.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>系統錯誤</title>
    <style type="text/css">
        a img, img a
        {
            border: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <%
            System.Net.IPAddress SvrIP = new System.Net.IPAddress(System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].Address);
            string serverName = SvrIP.ToString();
            switch (SvrIP.ToString())
            {
                case "172.16.82.1":
                    serverName = "主機1";
                    break;
                case "172.16.82.2":
                    serverName = "主機2";
                    break;
                case "172.16.82.3":
                    serverName = "主機3";
                    break;
                case "172.16.82.4":
                    serverName = "主機4";
                    break;
                case "172.16.82.5":
                    serverName = "主機5";
                    break;
                case "172.16.82.6":
                    serverName = "主機6";
                    break;
            }
        %>
        <div style="margin: 30px auto; text-align: center; vertical-align: middle;">
            <a href="<%=ResolveUrl("~/Index.aspx") %>" style="display: inline-block; position: relative">
                <img alt="" src="../Images/ErrorPage.jpg" />
                <div style="position: absolute; top: 457px; right: 73px; font-size:13px; color: #333"><%=serverName %></div>
            </a>
        </div>
        <div></div>
    </form>
</body>
</html>
