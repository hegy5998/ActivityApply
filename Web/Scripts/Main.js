var qs_sys_pid = "";

$(function () {
    // Init Menu
    $('.mainMenuModule ul').hide();
    if (qs_sys_pid.length > 0) {
        var $activeItem = $(".mainMenuProcess a[sys_pid^=" + qs_sys_pid + "]");
        $activeItem.parent().parent().parent().find('a:first').addClass("active");
        $activeItem.parent().parent().parent().find('a:first').children().children().removeClass('fa-folder');
        $activeItem.parent().parent().parent().find('a:first').children().children().addClass('fa-folder-open');
        $activeItem.addClass("active");
        $activeItem.closest(".mainMenuModule ul").show();
    }
    else
        $('.mainMenuModule ul:first').show();
    $('.mainMenuModule li a').each(function () {
        $(this).click(function () {
            var checkElement = $(this).next();
            if ((checkElement.is('ul'))) {
                $('.mainMenuModule ul:visible').slideUp(300);
                $('a').removeClass('active');
                $('a').children().children().removeClass('fa-folder-open');
                $('a').children().children().addClass('fa-folder');
                if (!checkElement.is(':visible')) {
                    checkElement.slideDown(300);
                    $(this).addClass("active");
                    $(this).children().children().removeClass('fa-folder');
                    $(this).children().children().addClass('fa-folder-open');
                }
                return false;
            }
        });
        $(this).find("div").filter(function () {
            if ($(this).height() > 50) return true;
        }).addClass("longText");
    });

    // 捲動時，控制子選單是否要浮動
    var funcBarAnimation;
    $(window).scroll(function () {
        clearTimeout(funcBarAnimation);
        if ($(window).scrollTop() > 163) {
            if ($("#processContentSubFunctionInner").hasClass("stick") == false)
                funcBarAnimation = setTimeout(function () {
                    $("#processContentSubFunction").css("height", $("#processContentSubFunction").height());
                    $("#processContentSubFunctionInner").hide().addClass("stick").stop().fadeIn();
                }, 250);
        } else {
            if ($("#processContentSubFunctionInner").hasClass("stick")) {
                $("#processContentSubFunction").css("height", "auto");
                $("#processContentSubFunctionInner").removeClass("stick");
            }
        }
    });

    // Config jQuery Dialog
    $.extend($.ui.dialog.prototype.options, {
        modal: true,
        resizable: false,
        draggable: false,
        closeText: "關閉",
        autoOpen: false,
        width: 'auto'
    });

    // 偵測使用者是否有任何操作動作(click, scroll, keypress)(可應用在保留session)
    //var sessionTrigged = false;
    //var resetSessionTriggedTime = 3000; // 重新允許發送sessionRequest的時間(毫秒)
    //setInterval(function () { sessionTrigged = false; }, resetSessionTriggedTime);
    //$(window).bind("click scroll keypress", function () {
    //    if (sessionTrigged == false) {
    //        sessionTrigged = true;
    //    }
    //});

    //Class 有 NumberLimit 為則限制數字輸入
    $(".NumberLimit").live("keyup", function (e) {
        var num = $.trim($(this).val());
        if (num != "" && num != "-") {
            var regex = /(^-\d+[.]?\d*$|^\d+[.]?\d*$)/;
            if (!regex.test(num)) {
                var v = num.match(/(^-\d+[.]?\d*|^\d+[.]?\d*)/);
                v = v == null ? '0' : v[0];
                $(this).val(v);
                showPopupMessage("error", "限制輸入數字", "此欄位限制僅能輸入阿拉伯數字！<BR />請確認您輸入的數字是正確格式！");
            }
        }
    });

    // 避免於textbox中，按下enter就submit
    $(".preventSubmit").live("keypress", function (e) {
        if (e.which == 13) {
            e.preventDefault();
        }
    });
});

// Initital
function init() {
    $(function () {
        // GridView 新增列顯示在第一列
        $(".grvDataRow.footer").each(function () {
            $(this).insertAfter($(this).siblings(".grvHeaderRow:last"));
        });

        // GridView 的Header欄位，若沒有任何資料，則隱藏該行
        $(".grvHeaderRow").each(function (index) {
            $(this).find("th").each(function (index) {
                if ($.trim($(this).html()).length == 0) {
                    $(this).closest(".grv").find(".grvHeaderRow, .grvDataRow").each(function () {
                        $(this).find(">th:eq(" + index + ")").hide();
                        $(this).find(">td:eq(" + index + ")").hide();
                    })
                }
            })
        })

        // GridView 若無資料，則產生空白列
        $(".grv").not(".noGenEmptyRow").each(function () {
            if (($(this).find(".grvDataRow").length == 0 && $(this).find(".grvEmptyRow").length == 0)
                || $(this).find(".grvDataRow:first").css("display") == "none") {
                var $headerRow = $(this).find(".grvHeaderRow:last");
                var headerRowCellCount = $headerRow.find("th").length;
                $(this).find(".grvEmptyRow").remove();
                $headerRow.after("<tr class='grvEmptyRow'><td colspan='" + headerRowCellCount + "'>-- 沒有資料 --</td></tr>");
            }
        });

        // 若子功能列沒有任何東西，則隱藏
        if ($("#processContentSubFunctionInner>div input").length == 0) {
            hideSubFunctionBar();
        }
        

        // 隱藏GridView圖形按鈕文字
        $(".btnGrv").val(" ");

        // 顯示PlaceHolder
        $('input[placeholder]').placeholder();

        // 設定GridView可直接點選某列，直接觸發該列預設事件
        $(".rowTrigger").attr("onclick", "triggerRowDefaultControl(this)");

        // 設定匯出按鈕 postback 後重設 __EVENTTARGET
        $('.btnExport').each(function () { $(this).attr('onclick', $(this).attr('onclick').replace(';resetEventTarget();', '').replace('__doPostBack', ';__doPostBack').replace(';;', ';').replace(':;', ':') + ';resetEventTarget();'); });
    });
}

function triggerRowDefaultControl(sender) {
    var thisRow = $(sender).parent("tr");
    var defaultControlId = thisRow.find("[id*=rowDefaultTriggerControlID_hf]").val();
    thisRow.find("[id*=" + defaultControlId + "]").click();
}

// 控制選單呈現or隱藏
function opMenu() {
    var $menuOp = $("#mainMenuOp");
    var $menu = $("#mainMenu");
    var $mainSubWrapperDiv = $("#mainSubWrapper>div");
    if ($menu.filter(":visible").length > 0) {
        // 固定顯示->隱藏
        $mainSubWrapperDiv.addClass("fullview");
        $menu.hide().addClass("fullview");
        $menuOp.addClass("fullview");
    }
    else {
        // 隱藏-> 固定顯示
        $menu.show();
        $mainSubWrapperDiv.removeClass("fullview");
        $menuOp.removeClass("fullview");
    }
    // 避免CrystalReport在控制選單收何時，寬度錯誤問題
    $("[id$=__UI_mb]").css("width", "auto");
}

// 顯示子功能列
function hideSubFunctionBar() {
    $("#processContentSubFunction").hide();
}
// 隱藏子功能列
function showSubFunctionBar() {
    $("#processContentSubFunction").show();
}

// 顯示載入中
function block() {
    $("#loadingBox>div").show();
}
// 隱藏載入中
function unblock() {
    $("#loadingBox>div").hide();
}

// 顯示使用說明
function showManual(url) {
    window.open('https://docs.google.com/viewer?url=' + url, 'Manual', config = 'toolbar=no,menubar=no,location=no,directories=no,status=no');
}

// 取得AngularJS的MainControl's $scope
function getNgScope() {
    return $(".baseForm[ng-controller=MainCtrl]").scope();
}

// 顯示載入中
function blockui(msg) {
    $.blockUI({
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#444',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .8,
            color: '#fff'
        },
        message: (typeof (msg) == 'undefined' ? "系統正為您處理中, 請耐心等候 ..." : msg)
    });
}
// 隱藏載入中
function unblockui() {
    $.unblockUI();
}