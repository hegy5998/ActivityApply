<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Register.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
    <style type="text/css">
        .center{
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sideCon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainCon" runat="server">
    <section id="container">
        <section id="main-content">
            <section class="wrapper">
                <div id="add_activity_list" class="advanced-form row">
                    <p class="center" style="color:red;font-size:-webkit-xxx-large">最新活動!!</p>
                    
                </div>
            </section>
        </section>
    </section>
    
    <script type="text/javascript">
        //#region 初始化
        $(document).ready(function () {
            searchActivityAllList();
        })
        //#endregion

        //#region 搜尋活動事件
        function searchActivityAllList() {
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為getActivityAllList的function
                url: 'index.aspx/getActivityTopfive',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                //成功時
                success: function (result) {
                    // 加入活動列表
                    addActivity(result.d);
                },
                //失敗時
                error: function () {
                    return false;
                }
            });
        }
        //#endregion

        //#region 把搜尋到的資料加到分頁中
        function addActivity(ActivityInfo) {
            //轉換活動的JSON字串成JSON物件
            var ActivityInfo = JSON.parse(ActivityInfo);
            //產生活動列表
            for (var count = 0 ; count < ActivityInfo.length ; count++) {
                $("#add_activity_list").append('<div class="row">\
                                                <div class="content-panel">\
                                                    <h3><a target="_self" href="activity.aspx?act_class=' + ActivityInfo[count].act_class + '&act_idn=' + ActivityInfo[count].act_idn + '&act_title=' + ActivityInfo[count].act_title + '">' + ActivityInfo[count].act_title + '(' + ActivityInfo[count].num + ')</a></h3>\
                                                    <label>活動日期：' + dateReviver(ActivityInfo[count].as_date_start) + ' ~ ' + dateReviver(ActivityInfo[count].as_date_end) + '</label>\
                                                    <br />\
                                                    <label>報名日期：' + dateReviver(ActivityInfo[count].as_apply_start) + ' ~ ' + dateReviver(ActivityInfo[count].as_apply_end) + '</label>\
                                                </div>\
                                            </div>');
            }
            $("#add_activity_list").append('<div class="row"></div>');
            $("#add_activity_list").append('<a class="center" href="activity_List.aspx?act_class=0" style="font-size: large;">查看更多活動</a>');
            //後臺轉換DateTime格式時會把他轉成字串，使用JSON.parse會把它當成子串解析，需要在做轉換與切割成我們要的格式
            function dateReviver(datavalue) {
                if (datavalue != null) {
                    var datavalue = datavalue.split("T");
                    return datavalue[0] + " " + datavalue[1].substring(0, 5);
                }
                else
                    return "";
            };

        }
        //#endregion

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>
