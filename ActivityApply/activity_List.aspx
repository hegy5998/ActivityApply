<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="activity_List.aspx.cs" Inherits="ActivityApply.activity_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
    <!--引用jquery分頁-->
    <link href="assets/css/pagination.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sideCon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainCon" runat="server">
    <section id="container">
        <section id="main-content">
            <section class="wrapper">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="push-down-30">
                            <div class="banners-big">
                                <!--活動查詢：活動名稱包含 <strong></strong> 。-->
                                <div <%--method="get"--%>>
                                    <div class="input-group">
                                        <!-- 搜尋按鈕 -->
                                        <div class="col-sm-1">
                                            <span class="input-group-btn">
                                                <button id="search_btn" type="button" class="btn btn-theme03" style="border-radius: 4px;"><span class="glyphicon glyphicon-search" aria-hidden="true">活動查詢</span></button>
                                            </span>
                                        </div>
                                        <!-- 下拉是選單選擇分類 -->
                                        <div class="col-sm-1">
                                            <select class="select" id="act_class" style="height: 34px; border-radius: 4px;">
                                                <option selected="selected" value="0">全部</option>
                                            </select>
                                        </div>
                                        <!-- 輸入框 -->
                                        <div class="col-sm-10" style="padding-left: 0px;margin-left: -2px;">
                                            <input id="search_txt" type="text" name="search" class="form-control" placeholder="請輸入活動名稱" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- 分頁_START -->
                <div id="Pagination" class="pagination" style="margin-bottom: 0px;">
                    <!-- 顯示分頁的地方 -->
                </div>
                <div id="Searchresult"></div>
                <div id="hiddenresult" style="display: none;">
                    <!-- 列表元素 -->
                </div>
                <!-- 分頁_END -->
            </section>
        </section>
    </section>
    <script type="text/javascript">

        //#region 初始化
        $(document).ready(function () {
            //設定分類
            getClass();
            //判斷搜尋列輸入完後如果按ENTER則要執行搜尋功能
            $("#search_txt").keypress(function (event) {
                if (event.keyCode == 13) {
                    $("#search_btn").click();
                    return false;
                }
            });
        })
        //#endregion

        //#region 獲取分類資料
        function getClass() {
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為getActivityAllList的function
                url: '/activity_List.aspx/getClassList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                //成功時
                success: function (result) {
                    //設定搜尋下拉選單內容
                    setSelect(result.d);
                    //設定麵包削尋覽列
                    var act_class = $.url().param("act_class");
                    setbread(result.d, act_class);
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
            $("#act_class").append('<option value="0">全部</option>');
            for (var count = 0 ; count < act_classInfo.length ; count++) {
                $("#act_class").append('<option value="' + act_classInfo[count].Ac_idn + '">' + act_classInfo[count].Ac_title + '</option>');
            }
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
                $("#add_breach").append('<li><a href="../activity_List.aspx?act_class=0">首頁</a></li>');
                //判斷目前目錄並添加
                for (var count = 0 ; count < act_classInfo.length ; count++) {
                    if (act_class == act_classInfo[count].Ac_idn) {
                        $("#add_breach").append('<li><a href="../activity_List.aspx?act_class=' + act_classInfo[count].Ac_idn + '">' + act_classInfo[count].Ac_title + '</a></li>');
                    }
                }
            }
            //如果是搜尋按牛執行這裡
            else {
                //將滅包削內容清空
                $("#add_breach").children().remove();
                //添加首頁
                $("#add_breach").append('<li><a href="../activity_List.aspx?act_class=0">首頁</a></li>');
                //判斷目前目錄並添加
                if (act_class != 0) {
                    var act_class_title;
                    for (var count = 0 ; count < $("#add_sub > li > a").length ; count++) {
                        var act_num = $("#add_sub > li > a")[count].href.split("act_class=")[$("#add_sub > li > a")[count].href.split("act_class=").length - 1]
                        if (act_num == act_class) {
                            act_class_title = $("#add_sub > li > a")[count].innerHTML;
                        }
                    }
                    $("#add_breach").append('<li><a href="../activity_List.aspx?act_class=' + act_class + '">' + act_class_title + '</a></li>');
                }
            }
            //設定分類下拉選單
            $("#act_class").val(act_class);
        }
        //#endregion

        //#region 分頁事件
        $(function () {
            var initPagination = function () {
                //設定有幾個分頁，要除以你一個分頁要顯示幾筆資料
                var num_entries = $("#hiddenresult div.result").length / 8;
                // 創建分頁
                $("#Pagination").pagination(num_entries, {
                    items_per_page: 1, //每頁顯示幾項
                    num_edge_entries: 1, //邊緣頁數
                    num_display_entries: 50, //主體頁數
                    callback: pageselectCallback,
                    prev_text: "前一頁",
                    next_text: "下一頁"
                });
            };
            function pageselectCallback(page_index, jq) {
                //var new_content = $("#hiddenresult div.result:eq(" + page_index + ")").clone();
                //$("#Searchresult").empty().append(new_content); //装载对应分页的内容
                //設定一個分頁要顯示幾筆資料
                var items_per_page = 8;
                var max_elem = Math.min((page_index + 1) * items_per_page, $("#hiddenresult div.result").length);
                $("#Searchresult").html("");
                //判斷現在在第幾個分頁並顯示其內容
                for (var i = page_index * items_per_page; i < max_elem; i++) {
                    $("#Searchresult").append($("#hiddenresult div.result:eq(" + i + ")").clone());
                }
                return false;
            }
            //載入index.html  load( url, [data], [callback] )
            $("#hiddenresult").load("activity_List.html", null, initPagination);
        });
        //#endregion

        //#region 調整 jQuery steps 高度
        function resizeJquerySteps() {
            $('#Searchresult').animate({ height: $('#Searchresult').outerHeight() }, "slow");
        }
        //#endregion
    </script>
    <!--引用jquery分頁-->
    <script src="assets/js/jquery.pagination.js"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>
