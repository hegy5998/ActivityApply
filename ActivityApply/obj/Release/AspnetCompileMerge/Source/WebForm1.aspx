<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Register.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css"/>
    <script>
	$(function () {
		$("#privacylaw").dialog({
		  modal: true,
		  width: 550,
		  buttons: {
			"OK": function() {
				$( this ).dialog( "close" );
			}
		  }
		});
	});
    </script>
    <div id="privacylaw" title="隱私權聲明">
      <p style="line-height: 2em;">
      <B>『 test 』 活動報名告知聲明</B><BR/>
      逢甲大學為教育或訓練行政之目的，本報名表所蒐集之個人資料，將僅存放於校內，作為本次活動管理與聯繫之用，並將於活動結束後一個月內依規定銷毀。您得以本次活動聯絡人聯絡方式行使查閱、更正等個人資料保護法第3條的當事人權利。如必填欄位提供的資料不完整或不確實，將無法完成本次活動報名申請。
      </p>
      <div style="text-align: right;">
	    聯絡人: 123 	&nbsp;	聯絡人電話: 123  </div>
    </div>
</body>
</html>
