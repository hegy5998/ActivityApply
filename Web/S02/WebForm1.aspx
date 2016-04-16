<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Web.S02.WebForm1" %>

<!DOCTYPE HTML>
<html>
<head>
  <meta charset="utf-8">
<title></title>
    <link href="../Css/Lib/jquery.pagepiling.css" rel="stylesheet" />
    <script src="../Scripts/Lib/jquery-1.9.1.min.js"></script>
    <script src="../Scripts/Lib/jquery.pagepiling.js"></script>
</head>

<body>
<ul id="myMenu">
    <li data-menuanchor="firstPage" class="active"><a href="#firstPage">First section</a></li>
    <li data-menuanchor="secondPage"><a href="#secondPage">Second section</a></li>

</ul>
  <div id="pagepiling">
    <div class="section" ><img class="img-responsive" src="../Scripts/Lib/assets/img/fcu.jpg" alt=""></div>
    <div class="section" > <img class="img-responsive" src="../Scripts/Lib/assets/img/fcu.jpg" alt=""></div>
</div>
    <script>
        $(document).ready(function () {
            $('#pagepiling').pagepiling({
                anchors: ['firstPage', 'secondPage'],
                menu: '#myMenu'
            });
        });
    </script>
</body> 
</html>