// 垂直跑馬燈效果
function slideLine(box, stf, delay, speed, h) {
    var slideBox = document.getElementById(box);
    var delay = delay || 1000, speed = speed || 20, h = h || 20;
    var tid = null, pause = false;
    var s = function () { tid = setInterval(slide, speed); }
    var slide = function () {
        if (pause) return;
        slideBox.scrollTop += 1;
        if (slideBox.scrollTop % h == 0) {
            clearInterval(tid);
            slideBox.appendChild(slideBox.getElementsByTagName(stf)[0]);
            slideBox.scrollTop = 0;
            setTimeout(s, delay);
        }
    }
    slideBox.onmouseover = function () { pause = true; }
    slideBox.onmouseout = function () { pause = false; }
    setTimeout(s, delay);
}

// GridView 切換分頁
function gridViewChgPageIndex(obj) {
    var $grvPageRow = $(obj).closest(".grvPageRow");
    $grvPageRow.find("[name=targetIndex]").val($(obj).attr("targetIndex"));
    $grvPageRow.find("[id*=chgIndex]").click();
}
// 重新導向
function Redirect(TargetPage) {
    if (TargetPage == "back" || TargetPage == "return") {
        if (history.length > 1) history.back(-1);
        else window.location = "./";
    } else if (TargetPage == "close") {
        window.close();
    } else if (TargetPage == "reflash" || TargetPage == "reload") {
        window.location.reload();
    } else {
        var sys_id = TargetPage.substring(0, 3);
        var sys_pid = TargetPage.substring(0, 9);
        window.location.href = TargetPage + "?sys_id=" + sys_id + "&sys_pid=" + sys_pid;
    }
}

// 取得使用jQuery ajax時的Url(actionName: call method name)
function getAjaxUrl(actionName) {
    return curPageFileNameWithInfo + "&" + jqueryAjaxTypeName + "=" + actionName;
}

// 轉換yyymmdd為yyy年mm月dd日
function convertYYYMMDDwithSlash(val) {
    var ret = val;
    try
    {
        if (val.length == 7)
            ret = val.substring(0, 3) + "/" + val.substring(3, 5) + "/" + val.substring(5, 7);
    }
    catch (e) { }
    return ret;
}

// 顯示Growl訊息
function showPopupMessage(theme, header, msg) {
    var sticky = true;
    if (theme == "success") sticky = false;
    $.jGrowl(msg, {
        theme: theme,
        header: header,
        sticky: sticky,
        position: 'center',
        speed: 'fast',
        beforeOpen: function (e, m) {
            $('div.jGrowl').find('div.jGrowl-notification').children().parent().remove();
        }
    });
}

// 清除Growl訊息
function clearPopupMessage() {
    $('div.jGrowl').find('div.jGrowl-notification').children().parent().remove();
}

// 只顯示主內容，隱藏除了主畫面以外的部分
function showOnlyMainContent(){ 
    $('#mainHeader, #mainMenu, #mainMenuOp, #processHeader, #processContentSubFunction, #mainFooter').remove();
    $('#mainSubWrapper, #mainSubWrapper>div').css('background', 'none');
}

// 格式化土地面積
function formatLandArea(obj, len) {
    if (len == null) len = 6;

    var a = parseFloat($(obj).val());
    if (isNaN(a)) a = 0;
    $(obj).val(a.toFixed(len));
    return $(obj);
}

// 重設__EVENTTARGET
function resetEventTarget() {
    $("input[name=__EVENTTARGET]").val("");
}
        
// 取得空間稻作查詢的網址
function getRice3Url(lndno123, lndno4, eid) {
    return "http://210.69.25.152/rice3/index.asp?fsection=" + lndno123 + "&flndno=" + lndno4 + "&fuser_id="
        + eid[0].charCodeAt()
        + eid[1].charCodeAt()
        + eid[2].charCodeAt()
        + "0".charCodeAt();
}

// 取得地政資料查詢的網址
function getPlaceUrl(lndno123, lndno4) {
    return "http://210.69.25.151/EditWeb/QueryNew.aspx?c=" + lndno123[0]
        + "&t=" + lndno123.substr(1, 2)
        + "&s=" + lndno123
        + "&n=" + lndno4;
}


var ITC = {
    combobox: function ($target, menuText, textBoxWidth) {
        if (!textBoxWidth) textBoxWidth = 180;
        $target.each(function () {
            if ($(this).parent().hasClass("ITC-combobox-wrapper") == false) {
            var $textBox = $(this).css("width", textBoxWidth);
            var $wrapper = $textBox.wrap("<div class='ITC-combobox-wrapper' style='width:" + (textBoxWidth + 10) + "px'></div>").parent();
            var $ul = $wrapper.append("<ul class='ITC-combobox-ul' style='width:" + (textBoxWidth + 10) + "px'></ul>").find("ul");

            for (var i = 0 ; i < menuText.length ; i++)
                $ul.append("<li>" + menuText[i] + "</li>");

            $textBox.focus(function () {
                $ul.show();
            }).blur(function () {
                if ($textBox.attr("showingMenu") != "true") $ul.hide();
            });

            $ul.hover(function () {
                $textBox.attr("showingMenu", "true");
            }, function () {
                $textBox.attr("showingMenu", "false");
            }).find("li").click(function () {
                $textBox.val($(this).text());
                $ul.hide();
            });
            }
        });
    }
}