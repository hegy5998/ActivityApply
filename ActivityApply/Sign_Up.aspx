<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="Sign_Up.aspx.cs" Inherits="ActivityApply.Sign_Up" %>

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
            $("#form1").validate();
        });
    </script>
    <!-- Jquery Strength -->
    <link href="<%=ResolveUrl("~/assets/css/strength.css")%>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=ResolveUrl("~/assets/js/strength.js") %>"></script>

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
        table {
            box-shadow: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sideCon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainCon" runat="server">
    <div class="advanced-form row" style="display: none; padding-top: 25px;padding-bottom: 25px;">
        <h3>活動報名</h3>
        <section>
            <h3><i class="fa fa-angle-right"></i>活動報名</h3>
            <!-- 放置區塊區域 -->
            <div id="sections_div">
                <!-- 放置問題區域 -->
            </div>
        </section>

        <h3>資料確認</h3>
        <section>
            <h3><i class="fa fa-angle-right"></i>資料確認</h3>
            <div id="check_div" class="col-sm-8 panel panel-default table-responsive">
                <table id="checkData_table" class="panel-body table table-condensed">
                    <tbody>
                        <!-- 放置使用者資料確認欄位 -->
                    </tbody>
                </table>
            </div>
        </section>

        <h3>設定密碼</h3>
        <section>
            <h3><i class="fa fa-angle-right"></i>設定密碼</h3>
            <div  class="col-sm-8 form-horizontal panel panel-default style-form">
                <div id="password_div" class="panel-body">

                </div>
            </div>
        </section>

        <h3>報名完成</h3>
        <section>
            <h3><i class="fa fa-angle-right"></i>報名完成</h3>
            <div class="col-sm-8 panel panel-default">
                <div id="finish_div" class="panel-body">
                    <asp:Button runat="server" ID="print_btn" OnClick="print_ApplyProve" Text="下載報名資訊" CssClass="btn btn-info" />
                </div>
            </div>
        </section>
    </div>


    <script type="text/javascript">
        var sectionList;
        var questionList;
        var email;
        var signFlag = false;
        // Enter 事件：改為按下一步
        $(document).keydown(function () {
            if (event.keyCode == 13) {
                $(".advanced-form").steps("next");
                return false;
            }
        });

        $(document).ready(function () {
            var funcList = [getSectionList,
                            getQuestionList,
                            resizeJquerySteps];
            $(document).queue("myQueue", funcList);
            $(document).dequeue("myQueue");
            //#region jQuery steps
            var form = $(".advanced-form");
            form.steps({
                headerTag: "h3",
                bodyTag: "section",
                contentContainerTag: "div",
                actionContainerTag: "div",
                stepsContainerTag: "div",
                transitionEffect: "slideLeft",
                onStepChanging: function (event, currentIndex, newIndex) {
                    if (currentIndex === 0 && newIndex === 1) {
                        if ($("#form1").valid()) {
                            setUserData();
                            $('tbody').html("");    //清空表格
                            Add_Check();
                            return true;
                        }
                    }
                    if (currentIndex === 1 && newIndex === 0) {
                        return true;
                    }
                    if (currentIndex === 1 && newIndex === 2) {
                        return true;
                    }
                    if (currentIndex === 2 && newIndex === 3) {
                        if ($("#form1").valid()) {
                            if (!signFlag) {
                                if (!SavePassword()) {
                                    alert("報名錯誤，請稍後再試！");
                                    return false;
                                }
                            }
                            if (SaveUserData()) {
                                Add_Finish();
                                return true;
                            }
                        }
                    }
                    resizeJquerySteps();
                    return false;
                },
                onStepChanged: function (event, currentIndex, priorIndex) {
                    if (currentIndex === 2) {
                        if (isSignUp()) {
                            signFlag = true;
                            form.steps("next");
                        }
                        else {
                            Add_SetPwd();
                            $("#password").rules("add", { required: true, minlength: 6, maxlength: 20 });
                            $("#confirm_password").rules("add", { required: true, equalTo: "#password" });
                            $("#password").strength({
                                strengthClass: 'strength',
                                strengthMeterClass: 'strength_meter',
                                strengthButtonClass: 'button_strength',
                                strengthButtonText: 'Show Password',
                                strengthButtonTextToggle: 'Hide Password'
                            });
                        }
                    }
                    resizeJquerySteps();
                },
                onFinishing: function (event, currentIndex) {
                    window.location.replace("Sign_Search_Context.aspx");
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
                url: 'Sign_Up.aspx/getSectionList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //成功時
                success: function (result) {
                    // 加入區塊
                    if (result.d != "false")
                        Add_Section(result.d);
                    else
                        window.location.replace("Index.aspx");
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
                url: 'Sign_Up.aspx/getQuestionList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //成功時
                success: function (result) {
                    // 加入問題
                    if (result.d != "false")
                        Add_Question(result.d);
                    else
                        window.location.replace("Index.aspx");
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
            }
            $.datetimepicker.setLocale('zh-TW');
            $('.datetimepicker').datetimepicker({
                timepicker: false,
                format: 'Y/m/d'
            });
            $(document).dequeue("myQueue");
        }
        //#endregion
        //#region 區塊程式碼
        function Section_Code(section) {
            var sectionId = "sec_div_" + section.Acs_seq;
            var questionId = "question_div_" + section.Acs_seq;
            var code = '<div id="' + sectionId + '" class="panel panel-default">\
                            <div class="panel-heading">' + section.Acs_title + '</div>\
                            <label class="desc" style="margin: 15px 15px 0px 30px;">' + section.Acs_desc + '</label>\
                            <div id="' + questionId + '" class="panel-body form-horizontal style-form">\
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
                                <input type="text" name="' + qusName + '" class="form-control' + Validate(question.Acc_required, question.Acc_validation) + ' >\
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
                                <input type="radio" style="width: auto;" name="' + qusName + '" value="' + val + '"' + (index == 0 ? ' class="' + Validate(question.Acc_required, question.Acc_validation) : '') + '>\
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
                                <input type="checkbox" style="width: auto;" name="' + qusName + '" value="' + val + '"' + (index == 0 && question.Acc_required == "1" ? ' class="' + Validate(question.Acc_required, "length,1,N") : '') + '>' + val + '\
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
                case 'date': code += ' date datetimepicker"'; break;
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
            var res;
            var detailList = { userData: [] };
            for (var i = 0; i < questionList.length; i++) {
                var detailJson = {}
                detailJson.Aad_col_id = questionList[i].Acc_idn;
                detailJson.Aad_title = questionList[i].Acc_title;
                detailJson.Aad_val = questionList[i].Acc_val;
                detailList.userData.push(detailJson);
            }
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為saveUserData的function
                url: 'Sign_Up.aspx/saveUserData',
                data: JSON.stringify(detailList),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                //成功時
                success: function (result) {
                    res = true;
                    var msg = result.d;
                    if (msg.split(":")[0] == "Error") {
                        alert(msg.split(":")[1]);
                        res = false;
                        window.location.replace("Sign_Search_Context.aspx");
                        window.event.returnValue = false;
                    }
                },
                //失敗時
                error: function () {
                    alert("報名錯誤，請稍後再試！");
                    res = false;
                }
            });
            return res;
        }
        //#endregion
        /* 設定密碼 */
        //#region 驗證使用者是否註冊過
        function isSignUp() {
            var res;
            for (var i = 0; i < questionList.length; i++) {
                if (questionList[i].Acc_title.indexOf("Email") != -1) {
                    email = questionList[i].Acc_val;
                    break;
                }
            }
            var data = { "email": email };
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為saveUserData的function
                url: 'Sign_Up.aspx/isSignUp',
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                //成功時
                success: function (result) {
                    res = result.d;
                },
                //失敗時
                error: function () {
                    alert("失敗!!!!");
                    res = false;
                }
            });
            return res;
        }
        //#endregion
        //#region 新增設定密碼欄位
        function Add_SetPwd() {
            $('#password_div').append(SetPwd_Code());
        }
        //#endregion
        //#region 設定密碼欄位程式碼
        function SetPwd_Code() {
            var code = '<div class="form-horizontal style-form">\
                            <label class="desc">您是第一次使用本報名系統，請設定一組個人密碼，供日後修改報名資料時驗證，謝謝。</label>\
                            <div class="form-group">\
                                <label class="col-sm-2 control-label">密碼<label style="color:red">*</label></label>\
                                <div class="col-sm-10">\
                                    <input type="password" id="password" name="password" class="form-control">\
                                </div>\
                            </div>\
                            <div id="" class="form-group">\
                                <label class="col-sm-2 control-label">確認密碼<label style="color:red">*</label></label>\
                                <div class="col-sm-10">\
                                    <input type="password" id="confirm_password" name="confirm_password" class="form-control">\
                                </div>\
                            </div>\
                        </div>';
            return code;
        }
        //#endregion
        //#region 儲存密碼
        function SavePassword() {
            var res;
            var password = $('input[name="password"]').val();
            var data = { Activity_apply_emailInfo: [] };
            var detailJson = {};
            detailJson.Aae_email = email;
            detailJson.Aae_password = password;
            data.Activity_apply_emailInfo.push(detailJson);
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為saveUserData的function
                url: 'Sign_Up.aspx/savePassword',
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                //成功時
                success: function (result) {
                    res = result.d;
                },
                //失敗時
                error: function () {
                    alert("儲存密碼錯誤!");
                    res = false;
                }
            });
            return res;
        }
        //#endregion

        /* 報名完成 */
        //#region 新增報名完成提示
        function Add_Finish() {
            $('#finish_div').prepend(Finish_Code());
        }
        //#endregion
        //#region 報名完成提示程式碼
        function Finish_Code() {
            var code = '<p>報名完成，請至電子信箱查看報名成功確認信！</p>\
                        <p>如有需要可自行下載報名資訊</p>';
            return code;
        }
        //#endregion

        //#region 設定麵包削尋覽列
        function setSessionBread() {
            //將滅包削內容清空
            $("#add_breach").children().remove();
            //添加首頁
            $("#add_breach").append('<li><a href="Index.aspx">首頁</a></li>');
            var act_class = $.url().param("act_class");
            //判斷目前目錄並添加
            var act_class_title;
            for (var count = 0 ; count < $("#add_sub > li > a").length ; count++) {
                var act_num = $("#add_sub > li > a")[count].href.split("act_class=")[$("#add_sub > li > a")[count].href.split("act_class=").length - 1]
                if (act_num == act_class) {
                    act_class_title = $("#add_sub > li > a")[count].innerHTML;
                }
            }
            $("#add_breach").append('<li><a href="Activity_List.aspx?act_class=' + act_class + '">' + act_class_title + '</a></li>');
            $("#add_breach").append('<li><a href="Activity.aspx?act_class=' + $.url().param("act_class") + '&act_idn=' + $.url().param("act_idn") + '&act_title=' + $.url().param("act_title") + '">' + $.url().param("act_title") + '</a></li>');
            $("#add_breach").append('<li>報名</li>');
        }
        //#endregion
    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>
