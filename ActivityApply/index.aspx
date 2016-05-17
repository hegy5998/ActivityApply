<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ActivityApply.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
    <style type="text/css">
        .center {
            text-align: center;
        }
        .red{
            color:red;
        }
        .blink {
            animation-duration: 1s;
            animation-name: blink;
            animation-iteration-count: infinite;
            animation-direction: alternate;
            animation-timing-function: ease-in-out;
            }
            @keyframes blink {
                from {
                    opacity: 1;
                }
                to {
                    opacity: 0;
                }
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
                    <!-- 如果沒有開啟JavaScript則會請她凱起才能使用本網頁的功能 -->
                    <noscript>
                        <h1 class="red blink">您沒有開啟JavaScript的功能，請您開啟才能使用本網站的功能!!</h1>
                        <h3>開啟方法如下:</h3>
                        <h4>IE : 網際網路選項 -> 安全性 -> 網際網路 -> 自訂等級 -> 指令碼處理 -> 啟用</h4>
                        <h4>Firefox : 工具 -> 網頁開發者 -> 網頁工具箱 -> 選項 -> 取消打勾「停用JavaScript」</h4>
                        <h4>Chrome : 設定 -> 顯示進階設定 -> 隱私權 -> 內容定... -> JavaScript -> 選擇「允許所有網站執行JavaScript」</h4>
                    </noscript>
                    <p id ="new" class="red center" style="font-size: -webkit-xxx-large;display:none">最近活動!!</p>
                    <a class="center" href="Activity_List.aspx?act_class=0" style="font-size: large;">查看更多活動</a>
                </div>
            </section>
        </section>
    </section>

    <script type="text/javascript">

        //#region 初始化
        $(document).ready(function () {
            $("#new").css({ 'display': '' });
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
