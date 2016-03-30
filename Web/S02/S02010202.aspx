<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S02010202.aspx.cs" Inherits="Web.S02.S02010202" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentSubFunction_cph" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent_cph" runat="server">
    <div class="row" id="add_Session_div">

    </div>

    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <input type="text" id="txt1"/>
    <input type="button" id="Button1" value="123" onclick="get_Json_Data()"/>
    <script type="text/javascript">

        $(document).ready(function () {
            //var json = window.opener.document.getElementById("txt").value;
            //var json_object = eval("(" + json + ")");
            //$("#add_Session_div").append('<input type="text" value="成功!!!"/>');
            get_Json_Data();

            test();

        });
        var jsondata = { "subject": "Math", "score": 80 };
        function test() {

            var jsondata = { activity_Form: [] };
            var activity_Column_Json_Data = {};
            activity_Column_Json_Data.Acc_asc = "1";
            activity_Column_Json_Data.Acc_option = "123";
            jsondata.activity_Form.push(activity_Column_Json_Data);

            var paramJSON = '{ "ID": "10"}';

            var json = window.opener.document.getElementById("save_Json_Data").value;
            var json_object = eval("(" + json + ")");
            var t = json_object["activity_Form"];
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為save_Activity_Form的function
                data: paramJSON,
                url: '/S02/S02010202.aspx/get',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //成功時
                success: function (result) {
                    var a = eval("(" + result.d + ")");
                    alert(a["Acc_title"]);
                   
                   
                },
                //失敗時
                error: function () {
                    alert("失敗!!!!");
                }
            });
        }
        function get_Json_Data() {
            var json = $("#save_Json_Data", window.opener.document).val();
            var json_object = eval("(" + json + ")");
                if (json_object["activity_Form"][0]["Acc_asc"] == 1) {
                    $("#add_Session_div").append('<input type="text" value="成功!!!"/>');
            }
        }

        function get_Activity() {
            $.get("S02010201.aspx?sys_id=S01&sys_pid=S02010201/view_Activity", function (data, status) {
                alert("Data: " + data + "\nStatus: " + status);
            });
            return "成功";
        }
    </script>
</asp:Content>
