<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="SignChange.aspx.cs" Inherits="ActivityApply.SignChange" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
    <link href="<%=ResolveUrl("~/assets/css/jquery.steps.css")%>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=ResolveUrl("~/assets/js/jquery.steps.js") %>"></script>
    <!-- Jquery Validation -->
    <script type="text/javascript" src="<%=ResolveUrl("~/assets/js/validation/jquery.metadata.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/assets/js/validation/jquery.validate.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/assets/js/validation/messages_zh_TW.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/assets/js/validation/additional-methods.js") %>"></script>
    <script type="text/javascript">
        $(function () {
            //須與form表單ID名稱相同
        });
    </script>

    <style type="text/css">
        /* 欄位控制項 */
        .form-control {
            width: 50%;
        }
        /* 區塊內文字格式 */
        .form-horizontal .control-label {
            text-align: right;
        }
        /* 欄位說明文字 */
        .help-block {
            margin-top: 7px;
        }
        /* 欄位標題 */
        .control-label {
            font-weight: bold;
        }
        /* 驗證錯誤欄位 */
        input[type="text"].error {
            box-shadow: 0px 0px 9px red;
        }
        /* 驗證錯誤訊息文字 */
        label.error {
            color: red;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sideCon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainCon" runat="server">
    <section id="container">
        <section id="main-content">
            <section class="wrapper">
                <div class="advanced-form row" style="display: none;">
                    <h3>修改報名資料</h3>
                    <fieldset>
                        <h3><i class="fa fa-angle-right"></i>報名資料</h3>
                        <!-- 放置區塊區域 -->
                        <div id="sections_div">
                            <!-- 放置問題區域 -->
                        </div>
                    </fieldset>

                    <h3>資料確認</h3>
                    <fieldset>
                        <h3><i class="fa fa-angle-right"></i>資料確認</h3>
                        <div id="check_div" class="col-sm-8 form-panel table-responsive">
                            <table id="checkData_table" class="table table-condensed">
                                <tbody>
                                    <!-- 放置使用者資料確認欄位 -->
                                </tbody>
                            </table>
                        </div>
                    </fieldset>

                    <h3>修改完成</h3>
                    <fieldset>
                        <h3><i class="fa fa-angle-right"></i>修改完成</h3>
                        <div id="finish_div" class="col-sm-8 form-panel">
                            <p>修改完成，請至電子信箱查看報名資料修改成功確認信！</p>
                            <p>如有需要可重新下載活動證明</p>
                            <asp:Button runat="server" ID="print_btn" OnClick="print_ApplyProve" Text="下載報名資訊" CssClass="btn btn-theme" />
                        </div>
                    </fieldset>
                </div>
            </section>
        </section>
    </section>

    <script type="text/javascript">
        //window.onbeforeunload = function () {
        //    if (!confirm('資料尚未儲存，確定要返回嗎?')) {
        //        return '按一下「取消」停留在此頁';
        //    }
        //    else
        //        window.location.href = '/index.aspx';
        //    //var if_Save = confirm("資料尚未儲存，確定要返回嗎?");
        //    //if (if_Save == true) {
        //    //    window.location.href = 'SignSearchContext.aspx';
        //    //}
        //};
        var sectionList;
        var questionList;
        var applyDetailList;
        var applyId;

        $(document).ready(function () {
            var funcList = [getSectionList,
                            //getApplyDetailList,
                            getQuestionList,
                            resizeJquerySteps];
            $(document).queue("myQueue", funcList);
            $(document).dequeue("myQueue");

            $(document).keydown(function () {
                if (event.keyCode == 13) {
                    $(".advanced-form").steps("next");
                    return false;
                }
            });

            setSessionBread();

            //#region jQuery steps
            var form = $(".advanced-form");
            form.steps({
                headerTag: "h3",
                bodyTag: "fieldset",
                contentContainerTag: "div",
                actionContainerTag: "div",
                stepsContainerTag: "div",
                transitionEffect: "slideLeft",
                onStepChanging: function (event, currentIndex, newIndex) {
                    if (currentIndex === 2 && newIndex === 1) {
                        return false;
                    }
                    if (currentIndex === 0) {
                        if ($("#form1").valid()) {
                            setUserData();
                            $('tbody').html("");    //清空表格
                            Add_Check();
                            return true;
                        }
                        return false;
                    }
                    if (newIndex === 2) {
                        SaveUserData();
                    }
                    return true;
                },
                onStepChanged: function (event, currentIndex, priorIndex) {
                    resizeJquerySteps();
                    return true;
                },
                onFinishing: function (event, currentIndex) {
                    window.location.replace("SignSearchContext.aspx");
                    return true;
                },
                onFinished: function (event, currentIndex) {
                    return true;
                },
                labels: {
                    cancel: "取消",
                    finish: "完成",
                    next: "下一步",
                    previous: "返回",
                    loading: "載入中"
                }
            });
            //調整 jQuery steps 高度
            function resizeJquerySteps() {
                $('.wizard .content').animate({ height: $('.body.current').outerHeight() }, "slow");
            }
            form.show();
            //#endregion
        })

        /* 活動報名 */

        // #region 取得區塊列表
        function getSectionList() {
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為getSectionList的function
                url: 'SignChange.aspx/getSectionList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //成功時
                success: function (result) {
                    // 加入區塊
                    if (result.d != "false")
                        Add_Section(result.d);
                    else
                        window.location.replace("index.aspx");
                },
                //失敗時
                error: function () {
                    alert("失敗!!!!");
                    return false;
                }
            });
        }
        // #endregion

        // #region 取得問題列表
        function getQuestionList() {
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為getQuestionList的function
                url: 'SignChange.aspx/getQuestionList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //成功時
                success: function (result) {
                    if (result.d != "flase")
                        Add_Question(result.d);
                    else
                        window.location.replace("index.aspx");
                },
                //失敗時
                error: function () {
                    alert("失敗!!!!");
                    return false;
                }
            });
        }
        // #endregion

        // #region 取得報名資料
        function getApplyDetailList() {
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為getQuestionList的function
                url: 'SignChange.aspx/getApplyDetailList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //成功時
                success: function (result) {
                    // 儲存報名資料
                    applyDetailList = (result.d);
                },
                //失敗時
                error: function () {
                    alert("失敗!!!!");
                    return false;
                }
            });
        }
        // #endregion

        //#region 新增區塊
        function Add_Section(sectionInfo) {
            sectionInfo = sectionList = JSON.parse(sectionInfo);
            for (var i = 0; i < sectionInfo.length; i++) {
                $("#sections_div").append(Section_Code(sectionInfo[i]));
            }
            $(document).dequeue("myQueue");
        }
        //#endregion

        //#region 新增問題
        function Add_Question(questionInfo) {
            questionInfo = questionList = JSON.parse(questionInfo);
            var applyDetailInfo;
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為getQuestionList的function
                url: 'SignChange.aspx/getApplyDetailList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                //成功時
                success: function (result) {
                    // 儲存報名資料
                    applyDetailInfo = (result.d);
                },
                //失敗時
                error: function () {
                    alert("失敗!!!!");
                    return false;
                }
            });
            applyDetailInfo = applyDetailList = JSON.parse(applyDetailInfo);
            for (var i = 0; i < questionInfo.length; i++) {
                var code = "";
                switch (questionInfo[i].Acc_type) {
                    case "text": code = TextCol_Code(questionInfo[i], i); break;
                    case "singleSelect": code = SingleSelCol_Code(questionInfo[i], i); break;
                    case "multiSelect": code = MultiSelCol_Code(questionInfo[i], i); break;
                    case "dropDownList": code = DropDownListCol_Code(questionInfo[i], i); break;
                }
                $("#question_div_" + questionInfo[i].Acc_asc).append(decodeURI(code));
                if (questionInfo[i].Acc_type == "text")
                    $("#qus_div_" + i).find('input').css({ 'width': '99%' });
                else
                    $("#qus_div_" + i).find('input').css({ 'width': 'auto' });
                $("#qus_div_" + i).find('select').css({ 'width': '99%' });

                //將報名資料填入
                //判斷
                for (var count = 0 ; count < applyDetailInfo.length ; count++) {
                    if (applyDetailInfo[count] != undefined && applyDetailInfo[count].Aad_col_id == questionInfo[i].Acc_idn) {
                        applyId = applyDetailInfo[count].Aad_apply_id;
                        switch (questionInfo[i].Acc_type) {
                            case "text": $("[name=qus_txt_" + i + "]").val(applyDetailInfo[count].Aad_val); break;
                            case "singleSelect": $("[name=qus_radio_" + i + "]" + "[value='" + applyDetailInfo[count].Aad_val + "']").attr('checked', true); break;
                            case "multiSelect":
                                var multiSelectValue = applyDetailInfo[count].Aad_val.split(",");
                                for (var mulcount = 0 ; mulcount < multiSelectValue.length ; mulcount++) {
                                    $("[name=qus_checkbox_" + i + "]" + "[value='" + multiSelectValue[mulcount] + "']").attr('checked', true);
                                }
                                break;
                            case "dropDownList": $("[name=qus_ddl_" + i + "]").val(applyDetailInfo[count].Aad_val); break;
                        }
                    }
                }
                if (questionInfo[i].Acc_title == "電子信箱Email") {
                    $("[name=qus_txt_" + i + "]").attr('disabled', 'disabled');
                    $("[name=qus_txt_" + i + "]").css({ 'background': '#EEEEEE' });
                }
                    
            }
            $(document).dequeue("myQueue");
        }
        //#endregion

        //#region 區塊程式碼
        function Section_Code(section) {
            var sectionId = "sec_div_" + section.Acs_seq;
            var questionId = "question_div_" + section.Acs_seq;
            var code = '<div id="' + sectionId + '" class="col-sm-12 form-panel">\
                            <h4 class="mb"><i class="fa fa-angle-right"></i>' + section.Acs_title + '</h4>\
                            <label class="desc">' + section.Acs_desc + '</label>\
                            <div id="' + questionId + '" class="form-horizontal style-form">\
                            </div>\
                        </div>';
            return code;
        }
        //#endregion

        //#region 文字欄位程式碼
        function TextCol_Code(question, seq) {
            var qusId = "qus_div_" + seq;
            var qusName = questionList[seq].Input_name = "qus_txt_" + seq;
            var code = '<div id="' + qusId + '" class="form-group">\
                            <label class="col-sm-2 control-label">' + question.Acc_title + RequiredMark(question.Acc_required) + '</label>\
                            <div class="col-sm-10 col-lg-5">\
                                <input type="text" name="' + qusName + '" class="form-control' + Validate(question.Acc_required, question.Acc_validation) + '>\
                                <span class="help-block">' + question.Acc_desc + '</span>\
                            </div>\
                        </div>';
            return code;
        }
        //#endregion

        //#region 單選欄位程式碼
        function SingleSelCol_Code(question, seq) {
            var qusId = "qus_div_" + seq;
            var qusName = questionList[seq].Input_name = "qus_radio_" + seq;
            var code = '<div id="' + qusId + '" class="form-group">\
                            <label class="col-sm-2 control-label">' + question.Acc_title + RequiredMark(question.Acc_required) + '</label>\
                            <div class="col-sm-10 col-lg-5">\
                                <span class="help-block">' + question.Acc_desc + '</span>';

            //反序列化
            question.Acc_option.split('&').forEach(function (param, index) {
                param = param.split('=');
                var val = param[1];
                code += '<div class="radio">\
                            <label>\
                                <input type="radio" name="' + qusName + '" value="' + val + '"' + (index == 0 ? ' class="' + Validate(question.Acc_required, question.Acc_validation) : '') + '>\
                                ' + val + '\
                            </label>\
                        </div>';
            })

            code += '</div>\
                  </div>';
            return code;
        }
        //#endregion

        //#region 多選欄位程式碼
        function MultiSelCol_Code(question, seq) {
            var qusId = "qus_div_" + seq;
            var qusName = questionList[seq].Input_name = "qus_checkbox_" + seq;
            var code = '<div id="' + qusId + '" class="form-group">\
                            <label class="col-sm-2 control-label">' + question.Acc_title + RequiredMark(question.Acc_required) + '</label>\
                            <div class="col-sm-10 col-lg-5">\
                                <span class="help-block">' + question.Acc_desc + '</span>';

            //反序列化
            question.Acc_option.split('&').forEach(function (param, index) {
                param = param.split('=');
                var val = param[1];
                code += '<div class="checkbox">\
                            <label>\
                                <input type="checkbox" name="' + qusName + '" value="' + val + '"' + (index == 0 && question.Acc_required == "1" ? ' class="' + Validate(question.Acc_required, "length,1,N") : '') + '>' + val + '\
                            </label>\
                         </div>';
            })
            code += '</div>\
                  </div>';
            return code;
        }
        //#endregion

        //#region 下拉式選單欄位程式碼
        function DropDownListCol_Code(question, seq) {
            var qusId = "qus_div_" + seq;
            var qusName = questionList[seq].Input_name = "qus_ddl_" + seq;
            var code = '<div id="' + qusId + '" class="form-group">\
                            <label class="col-sm-2 control-label">' + question.Acc_title + RequiredMark(question.Acc_required) + '</label>\
                            <div class="col-sm-10 col-lg-5">\
                                <select name="' + qusName + '" class="form-control' + Validate(question.Acc_required, question.Acc_validation) + '>\
                                    <option value=""></option>';

            //反序列化
            question.Acc_option.split('&').forEach(function (param, index) {
                param = param.split('=');
                var val = param[1];
                code += '<option value="' + val + '">' + val + '</option>';
            })
            code += '</select>\
                     <span class="help-block">' + question.Acc_desc + '</span>\
                  </div>\
               </div>';
            return code;
        }
        //#endregion

        //#region 必填符號
        function RequiredMark(isRequired) {
            if (isRequired == "1")
                return '<label style="color:red">*</label>';
            else
                return '';
        }
        //#endregion

        //#region 資料驗證
        function Validate(required, validation) {
            var code = '';

            if (required != "0")
                code += ' required';

            validation = validation.split(",");

            switch (validation[0]) {
                case 'email': code += ' email"'; break;
                case 'idNumber': code += ' TWIDCheck"'; break;
                case 'cellPhone': code += ' mobileTW"'; break;
                case 'date': code += ' date"'; break;
                case 'url': code += ' url"'; break;
                case 'int': code += ' number"' + (validation[1] != 'N' ? ' min="' + validation[1] + '"' : '') + (validation[2] != 'N' ? ' max="' + validation[2] + '"' : ''); break;
                case 'length': code += '"' + (validation[1] != 'N' ? ' minlength="' + validation[1] + '"' : '') + (validation[2] != 'N' ? ' maxlength="' + validation[2] + '"' : ''); break;
                default: code += '"'; break;
            }
            return code;
        }

        //#region 修正 radio 及 checkbox 錯誤訊息顯示位置
        $('#form1').validate({
            errorPlacement: function (label, element) {
                if (element.attr('type') == 'radio' || element.attr('type') == 'checkbox') {
                    label.appendTo(element.parent().parent().parent());
                }
                else {
                    label.insertAfter(element);
                }
            }
        });
        //#endregion

        //#endregion        

        //#region 寫入使用者輸入資料
        function setUserData() {
            for (var i = 0; i < questionList.length; i++) {
                switch (questionList[i].Acc_type) {
                    case "text":
                        if (questionList[i].Acc_validation == 'idNumber')
                            questionList[i].Acc_val = $('input[name="' + questionList[i].Input_name + '"]').val().toUpperCase();
                        else
                            questionList[i].Acc_val = $('input[name="' + questionList[i].Input_name + '"]').val();
                        break;
                    case "singleSelect":
                        questionList[i].Acc_val = $('input:radio:checked[name="' + questionList[i].Input_name + '"]').val();
                        break;
                    case "multiSelect":
                        var qusArray = $('input:checkbox:checked[name="' + questionList[i].Input_name + '"]').map(function () { return $(this).val(); }).get();
                        questionList[i].Acc_val = "";
                        qusArray.forEach(function (val, index) { questionList[i].Acc_val += val + ((index != qusArray.length - 1) ? "," : "") });
                        break;
                    case "dropDownList":
                        questionList[i].Acc_val = $('select[name="' + questionList[i].Input_name + '"]').val();
                        break;
                }
                //判斷選項為undefined
                if (!questionList[i].Acc_val)
                    questionList[i].Acc_val = "";
            }
        }
        //#endregion

        /* 資料確認 */

        //#region 新增使用者資料確認欄位
        function Add_Check() {
            for (var i = 0; i < questionList.length; i++) {
                $('tbody').append(Check_Code(questionList[i]));
            }
        }
        //#endregion

        //#region 使用者資料確認欄位程式碼
        function Check_Code(checkCol) {
            var code = '<tr>\
                            <td>' + checkCol.Acc_title + '</td>\
                            <td>' + checkCol.Acc_val + '</td>\
                        </tr>';
            return code;
        }
        //#endregion

        //#region 儲存使用者資料(POST)
        function SaveUserData() {
            var detailList = { userData: [] };
            for (var i = 0; i < questionList.length; i++) {
                var detailJson = {}
                var ifnew = true;
                //判斷填寫的問題為新問題或舊問題
                for (var count = 0 ; count < applyDetailList.length ; count++) {
                    if (applyDetailList[count] != undefined && applyDetailList[count].Aad_col_id == questionList[i].Acc_idn) {
                        detailJson.Aad_apply_id = applyDetailList[count].Aad_apply_id;
                        detailJson.Aad_col_id = questionList[i].Acc_idn;
                        detailJson.Aad_title = questionList[i].Acc_title;
                        detailJson.Aad_val = questionList[i].Acc_val;
                        detailJson.ifnewqus = 0;
                        detailList.userData.push(detailJson);
                        ifnew = false;
                    }

                }
                if (ifnew == true) {
                    detailJson.Aad_apply_id = applyId
                    detailJson.Aad_col_id = questionList[i].Acc_idn;
                    detailJson.Aad_title = questionList[i].Acc_title;
                    detailJson.Aad_val = questionList[i].Acc_val;
                    detailJson.ifnewqus = 1;
                    detailList.userData.push(detailJson);
                }


            }
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為saveUserData的function
                url: 'SignChange.aspx/saveUserData',
                data: JSON.stringify(detailList),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //成功時
                success: function (result) {
                    
                },
                //失敗時
                error: function () {
                    alert("修改錯誤，請稍後再試！");
                    return false;
                }
            });
        }
        //#endregion

        

        //#region 設定麵包削尋覽列
        function setSessionBread() {
            //將滅包削內容清空
            $("#add_breach").children().remove();
            //添加首頁
            $("#add_breach").append('<li><a href="index.aspx">首頁</a></li>');
            $("#add_breach").append('<li><a href="SignSearchContext.aspx">報名查詢</a></li>');
            $("#add_breach").append('<li>修改報名資料</li>');
        }
        //#endregion

    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>
