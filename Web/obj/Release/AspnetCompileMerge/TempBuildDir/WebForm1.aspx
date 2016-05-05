<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Web.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Lib/assets/js/jquery.js") %>"></script>

    <!-- dateptimeicker -->
    <link rel="stylesheet" type="text/css" href="<%=ResolveUrl("~/Scripts/Lib/jQuery-datetimepicker/jquery.datetimepicker.css") %>" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Lib/jQuery-datetimepicker/jquery.datetimepicker.js") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input class="datetimepicker" type="text"  id="datetimepicker1"/>
    </div>
    </form>
         <script type="text/javascript">
        //日期時間選擇器
        $(function () {
            $('.datetimepicker').datetimepicker({ lang: 'ch' });
        });
    </script>
</body>
</html>
