<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="Activity_List.aspx.cs" Inherits="ActivityApply.Activity_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
    <!--引用jquery分頁-->
    <link href="<%=ResolveUrl("~/assets/css/pagination.css")%>" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .activity_title {
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }
                .divcss5 {
            margin: 0px;
            height: 100%;
            overflow: hidden;
        }

        .divcss5 img {
            width: auto;
            height: auto;
            max-width: 100%;
            min-height: 100%;
            min-width: 100%;
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sideCon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainCon" runat="server">

    <div class="row">
        <div class="col-lg-12">
            <h1 id="class_text" class="page-header text-info">全部</h1>
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

                        <!-- 搜尋按鈕 -->
                        <%--<div class="col-sm-1 col-sx-1" style="padding: 0px; width: 66px; float: left;">
                            <span class="input-group-btn">
                                <button id="search_btn" type="button" class="btn btn-theme03" style="border-radius: 4px; height: 34px; margin-bottom: 0;"><span class="glyphicon glyphicon-search" aria-hidden="true" style="padding-bottom: 6px;">查詢</span></button>
                            </span>
                        </div>--%>
                        <!-- 下拉是選單選擇分類 -->
                        <%--<div class="col-sm-1 col-sx-1" style="padding: 0px; width: 76px; float: left;">
                            <select class="select" id="act_class" style="height: 34px; border-radius: 4px; width: 76px;">
                                <option selected="selected" value="0">全部</option>
                            </select>
                        </div>--%>
                        <!-- 輸入框 -->
                        <%--<div class="col-sm-9" style="padding: 0px; width: 50%; float: left;">
                            <input id="search_txt" type="text" name="search" class="form-control" placeholder="請輸入活動名稱" />
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- 分頁_START -->
    <div id="back" style="background-color:white;position:absolute;z-index:1;">   </div>
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

    <!-- 分頁_END -->

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

            $("#class_text").text($("#act_class option:selected").text());
            var img_count = 0;
            $(".img").each(function () {
                $(this).imagesLoaded(function () {
                    zmnImgCenter($("#img_" + img_count));
                    img_count++;
                });
            });
            $("#img_" + img_count).imagesLoaded(function () {
                $("#back").fadeOut();
            })
        })
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
            $("#act_class").append('<option value="0">全部活動</option>');
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
                else
                    $("#add_breach").append('<li><a href="Activity_List.aspx?act_class=0">全部活動</a></li>');
            }
            //設定分類下拉選單
            $("#act_class").val(act_class);
        }
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
                //設定一個分頁要顯示幾筆資料
                //$.cookie("page_index", page_index, { expires: 1 });
                var items_per_page = 9;
                var max_elem = Math.min((page_index + 1) * items_per_page, $("#hiddenresult div.result").length);
                //$("#Searchresult").html("");
                $("#r1").html("");
                $("#r2").html("");
                $("#r3").html("");
                $("#back").css({ "width": $("#page-wrapper").width(), "height": $("#page-wrapper").height() })
                $("#back").css({ 'background': 'white', 'display': '', 'opacity': '1' });
                //判斷現在在第幾個分頁並顯示其內容
                for (var i = page_index * items_per_page; i < max_elem; i++) {
                    //$("#Searchresult").append($("#hiddenresult div.result:eq(" + i + ")").clone());
                    var r = i % 9;
                    if (r < 3)
                        $("#r1").append($("#hiddenresult div.result:eq(" + i + ")").clone());
                    else if (3 <= r && r < 6)
                        $("#r2").append($("#hiddenresult div.result:eq(" + i + ")").clone());
                    else if (6 <= r && r < 9)
                        $("#r3").append($("#hiddenresult div.result:eq(" + i + ")").clone());
                }
                var img_count = 0;
                $(".img").each(function () {
                    $(this).imagesLoaded(function () {
                        zmnImgCenter($("#img_" + img_count));
                        img_count++;
                    });
                });
                $("#img_" + img_count).imagesLoaded(function () {
                    $("#back").fadeOut();
                })
                return false;
            }
            //載入index.html  load( url, [data], [callback] )
            $("#hiddenresult").load("Activity_List.html", null, initPagination);
        });
        //#endregion

        //图片居中
        function zmnImgCenter(obj) {
            //obj.each(function () {
            var $this = obj;
            var objHeight = $this.height();//图片高度
            var objWidth = $this.width();//图片宽度
            var parentHeight = $this.parent().height();//图片父容器高度
            var parentWidth = $this.parent().width();//图片父容器宽度
            var ratio = objHeight / objWidth;
            //alert(objHeight +" "+ parentHeight);
            $this.css('top', (parentHeight - objHeight) / 2);
            //if (objHeight > parentHeight && objWidth > parentWidth) {//当图片宽高都大于父容器宽高
            //    if (objHeight > objWidth) {//赋值宽高
            //        $this.width(parentWidth);
            //        $this.height(parentWidth * ratio);
            //    }
            //    else {
            //        $this.height(parentHeight);
            //        $this.width(parentHeight / ratio);
            //    }
            //    objHeight = $this.height();//重新获取宽高
            //    objWidth = $this.width();
            //    if (objHeight > objWidth) {
            //        $(this).css("top", (parentHeight - objHeight) / 2);
            //        //定义top属性
            //    }
            //    else {
            //        //定义left属性
            //        $(this).css("left", (parentWidth - objWidth) / 2);
            //    }
            //}
            //else {//当图片宽高小于父容器宽高
            //    if (objWidth > parentWidth) {//当图片宽大于容器宽，小于时利用css text-align属性居中
            //        $(this).css("left", (parentWidth - objWidth) / 2);
            //    }
            //    $(this).css("top", (parentHeight - objHeight) / 2);
            //}
            //})
        }
    </script>
    <!--引用jquery分頁-->
    <script type="text/javascript" src="<%=ResolveUrl("~/assets/js/jquery.pagination.js") %>"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>
