<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Register.index" %>

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
                                <div method="get">
                                    <div class="input-group">
                                        <span class="input-group-btn">
                                            <button class="btn btn-primary"><span class="glyphicon glyphicon-search" aria-hidden="true">活動查詢</span></button>
                                        </span>
                                        <div class="input-group-btn">
                                            <button type="button" class="btn btn-theme03">分類</button>
                                            <button type="button" class="btn btn-theme03 dropdown-toggle" data-toggle="dropdown" style="padding-left: 5px; padding-right: 5px">
                                                <span class="caret"></span>
                                                <span class="sr-only">Toggle Dropdown</span>
                                            </button>
                                            <ul class="dropdown-menu" role="menu">
                                                <li><a href="#">分類一</a></li>
                                                <li><a href="#">分類二</a></li>
                                            </ul>
                                        </div>
                                        <input type="text" name="search" class="form-control" placeholder="請輸入活動名稱">
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
        $(function () {
            var initPagination = function () {
                //設定有幾個分頁，要除以你一個分頁要顯示幾筆資料
                var num_entries = $("#hiddenresult div.result").length/8;
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
            $("#hiddenresult").load("index.html", null, initPagination);
        });
    </script>
    <!--引用jquery分頁-->
    <script src="assets/js/jquery.pagination.js"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>
