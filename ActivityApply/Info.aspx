<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="ActivityApply.Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
    <style type="text/css">
        .center {
            text-align: center;
        }

        .red {
            color: red;
        }

        .blink {
            animation-duration: 1s;
            animation-name: blink;
            animation-iteration-count: infinite;
            animation-direction: alternate;
            animation-timing-function: ease-in-out;
        }

        .activity_title {
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
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
    <!--引用jquery分頁-->
    <link href="<%=ResolveUrl("~/assets/css/pagination.css")%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sideCon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainCon" runat="server">

    <!-- 如果沒有開啟JavaScript則會請她凱起才能使用本網頁的功能 -->
    <noscript>
        <h1 class="red blink">您沒有開啟JavaScript的功能，請您開啟才能使用本網站的功能!!</h1>
        <h3>開啟方法如下:</h3>
        <h4>IE : 網際網路選項 -> 安全性 -> 網際網路 -> 自訂等級 -> 指令碼處理 -> 啟用</h4>
        <h4>Firefox : 工具 -> 網頁開發者 -> 網頁工具箱 -> 選項 -> 取消打勾「停用JavaScript」</h4>
        <h4>Chrome : 設定 -> 顯示進階設定 -> 隱私權 -> 內容定... -> JavaScript -> 選擇「允許所有網站執行JavaScript」</h4>
    </noscript>
    <%--<p id="new" class="red center" style="font-size: -webkit-xxx-large; display: none">最近活動!!</p>--%>
    <div class="row">
        <div class="col-lg-12">
            <h1 id="class_text" class="page-header text-info">最近活動</h1>
        </div>
    </div>
    <div class="row" style="margin-bottom: 20px;">
        <div class="col-xs-12">
            <div class="push-down-30">
                <div class="banners-big">
                    <!--活動查詢：活動名稱包含 <strong></strong> 。-->
                    <div class="row">
                        <div class="sidebar-search">
                            <div class="input-group custom-search-form">

                                <span class="input-group-btn">
                                    <button id="search_btn" class="btn btn-default" type="button" style="height: 34px; margin-bottom: 0px;">
                                        <i class="fa fa-search"></i>
                                    </button>
                                </span>
                                <span class="input-group-btn" style="font-size: initial; z-index: 3;">
                                    <select class="select" id="act_class" style="height: 34px; width: 100px; margin-right: -1px; border: 1px solid #ccc;">
                                        <option selected="selected" value="0">全部</option>
                                    </select>
                                </span>
                                <input id="search_txt" type="text" class="form-control" placeholder="搜尋活動..." />
                            </div>
                            <!-- /input-group -->
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="add_activity_list" class="advanced-form row">
        <div id="a1" class="row"></div>
        <div id="a2" class="row"></div>
        <div id="a3" class="row"></div>
    </div>

    <div class="row">
        <div id="Searchresult">
            <div id="r1" class="row"></div>
            <div id="r2" class="row"></div>
            <div id="r3" class="row"></div>
        </div>
    </div>

    <div id="hiddenresult" style="display: none;">
        <!-- 列表元素 -->
    </div>
    <div class="row" style="text-align: center;">
        <div id="Pagination" class="pagination" style="margin-bottom: 14px;">
            <!-- 顯示分頁的地方 -->
        </div>
    </div>

    <script type="text/javascript">

        //#region 初始化
        $(document).ready(function () {

            getClass();
            //判斷搜尋列輸入完後如果按ENTER則要執行搜尋功能
            $("#search_txt").keypress(function (event) {
                if (event.keyCode == 13) {
                    $("#search_btn").click();
                    return false;
                }
            });
            searchActivityAllList();
            if ($("#act_class option:selected").text() != "請選擇")
                $("#class_text").text($("#act_class option:selected").text());
        })
        //#endregion

        //#region 分頁事件
        $(function () {
            var initPagination = function () {
                //設定有幾個分頁，要除以你一個分頁要顯示幾筆資料
                var num_entries = $("#hiddenresult div.result").length / 9;
                // 創建分頁
                $("#Pagination").pagination(num_entries, {
                    items_per_page: 1, //每頁顯示幾項
                    num_edge_entries: 1, //邊緣頁數
                    num_display_entries: 10, //主體頁數
                    callback: pageselectCallback,
                    prev_text: "前一頁",
                    next_text: "下一頁"
                });
            };
            function pageselectCallback(page_index, jq) {
                //var new_content = $("#hiddenresult div.result:eq(" + page_index + ")").clone();
                //$("#Searchresult").empty().append(new_content); //装载对应分页的内容
                //$.cookie("page_index", page_index, { expires: 1 });
                //設定一個分頁要顯示幾筆資料
                var items_per_page = 9;
                var max_elem = Math.min((page_index + 1) * items_per_page, $("#hiddenresult div.result").length);
                //$("#Searchresult").html("");
                $("#r1").html("");
                $("#r2").html("");
                $("#r3").html("");
                //判斷現在在第幾個分頁並顯示其內容
                for (var i = page_index * items_per_page; i < max_elem; i++) {
                    var r = i % 9;
                    if (r < 3)
                        $("#r1").append($("#hiddenresult div.result:eq(" + i + ")").clone());
                    else if (3 <= r && r < 6)
                        $("#r2").append($("#hiddenresult div.result:eq(" + i + ")").clone());
                    else if (6 <= r && r < 9)
                        $("#r3").append($("#hiddenresult div.result:eq(" + i + ")").clone());
                }
                return false;
            }
            //載入index.html  load( url, [data], [callback] )
            $("#hiddenresult").load("Activity_List.html", null, initPagination);
        });
        //#endregion

        //#region 獲取分類資料
        function getClass() {
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為getActivityAllList的function
                url: 'activity_List.aspx/getClassList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                //成功時
                success: function (result) {
                    //設定搜尋下拉選單內容
                    setSelect(result.d);
                    //設定麵包削尋覽列
                    var act_class = $.url().param("act_class");
                    setbread(result.d, "-1");
                },
                //失敗時
                error: function () {
                    return false;
                }
            });
        }
        //#endregion

        //#region 設定搜尋下拉選單內容
        function setSelect(act_classInfo) {
            var act_classInfo = JSON.parse(act_classInfo);
            $("#act_class").children().remove();
            $("#act_class").append('<option value="-1">請選擇</option>');
            $("#act_class").append('<option value="0">全部活動</option>');
            for (var count = 0 ; count < act_classInfo.length ; count++) {
                $("#act_class").append('<option value="' + act_classInfo[count].Ac_idn + '">' + act_classInfo[count].Ac_title + '</option>');
            }
        }
        //#endregion

        //#region 搜尋活動事件
        function searchActivityAllList() {
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為getActivityAllList的function
                url: 'Info.aspx/getActivityTopfive',
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
                var r = count % 9;
                if (r < 3)
                    r = 1;
                else if (3 <= r && r < 6)
                    r = 2;
                else if (6 <= r && r < 9)
                    r = 3;
                $("#a" + r).append('<div class="col-lg-4 result">\
                                            <div class="panel panel-info">\
                                                <div class="panel-heading"><a target="_self" href="Activity.aspx?act_class=' + ActivityInfo[count].act_class + '&act_idn=' + ActivityInfo[count].act_idn + '&act_title=' + ActivityInfo[count].act_title + '" title="' + ActivityInfo[count].act_title + ' 場次數:' + ActivityInfo[count].num + '"><h4>' + ActivityInfo[count].act_title + '<a title="場次數" style="cursor: help;">  <spna class="badge bg-info" style="background-color: #35BCFF;font-size: 15px;margin-bottom: 3px;">' + ActivityInfo[count].num + '</span></a></h4></a></div>\
                                                    <div class="panel-body">\
                                                    <p>活動日期：<br>' + dateReviver(ActivityInfo[count].as_date_start) + ' ~ ' + dateReviver(ActivityInfo[count].as_date_end) + '</p>\
                                                    <p>報名日期：<br>' + dateReviver(ActivityInfo[count].as_apply_start) + ' ~ ' + dateReviver(ActivityInfo[count].as_apply_end) + '</p>\
                                                    </div>\
                                                    <div class="panel-footer"><a target="_self" href="Activity.aspx?act_class=' + ActivityInfo[count].act_class + '&act_idn=' + ActivityInfo[count].act_idn + '&act_title=' + ActivityInfo[count].act_title + '">查看活動</a></div>\
                                                </div>\
                                            </div>\
                                        </div>');
            }
            $("#add_activity_list").append('<div class="row"></div>');
            $("#add_activity_list").append('<a class="center" href="activity_List.aspx?act_class=0" style="font-size: large;">查看更多活動</a>');
            $("#Pagination").children().remove();
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

        //#region 設定麵包削尋覽列
        function setbread(act_classInfo, act_class) {
            //判斷是從搜尋按鈕搜尋還是ajax呼叫這個function
            //如果是搜尋按則act_classInfo為空
            if (act_classInfo != "") {
                //轉換分類資料為JSON
                var act_classInfo = JSON.parse(act_classInfo);
                //將滅包削內容清空
                $("#add_breach").children().remove();
                //添加首頁
                $("#add_breach").append('<li><a href="Index.aspx">首頁</a></li>');
                //判斷目前目錄並添加
                for (var count = 0 ; count < act_classInfo.length ; count++) {
                    if (act_class == act_classInfo[count].Ac_idn) {
                        $("#add_breach").append('<li><a href="Activity_List.aspx?act_class=' + act_classInfo[count].Ac_idn + '">' + act_classInfo[count].Ac_title + '</a></li>');
                    }
                }
                if (act_class == 0)
                    $("#add_breach").append('<li><a href="Activity_List.aspx?act_class=0">全部活動</a></li>');
            }
                //如果是搜尋按鈕執行這裡
            else {
                //將滅包削內容清空
                $("#add_breach").children().remove();
                //添加首頁
                $("#add_breach").append('<li><a href="Index.aspx">首頁</a></li>');
                //判斷目前目錄並添加
                if (act_class != 0) {
                    var act_class_title;
                    for (var count = 0 ; count < $("#add_sub > li > a").length ; count++) {
                        var act_num = $("#add_sub > li > a")[count].href.split("act_class=")[$("#add_sub > li > a")[count].href.split("act_class=").length - 1]
                        if (act_num == act_class) {
                            act_class_title = $("#add_sub > li > a")[count].innerHTML;
                        }
                    }
                    $("#add_breach").append('<li><a href="Activity_List.aspx?act_class=' + act_class + '">' + act_class_title + '</a></li>');
                }
                if (act_class == 0)
                    $("#add_breach").append('<li><a href="Activity_List.aspx?act_class=0">全部活動</a></li>');
            }
            //設定分類下拉選單
            $("#act_class").val(act_class);
        }
        //#endregion

    </script>
    <!--引用jquery分頁-->
    <script type="text/javascript" src="<%=ResolveUrl("~/assets/js/jquery.pagination.js") %>"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>
