<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Base.master" AutoEventWireup="true" CodeBehind="S02010105.aspx.cs" Inherits="Web.S02.S02010105" %>

<asp:Content ID="Content1" ContentPlaceHolderID="baseHead_cph" runat="server">
        <script type="text/javascript">
            $(function () {
                //須與form表單ID名稱相同
                $("#form1").validate();
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

<asp:Content ID="Content2" ContentPlaceHolderID="baseContent_cph" runat="server">
    <section class="content-wrapper main-content clear-fix">
        <section id="container">
            <section id="main-content" style="margin-left: 0px;">
                <section class="wrapper" style="margin-top: 38px;">
                    <div class="row"></div>
                    <h3><i class="fa fa-angle-right"></i>活動報名</h3>
                    <!-- 放置區塊區域 -->
                    <div id="sections_div" style=" margin-right: 1%;">
                    <!-- 放置問題區域 -->
                    </div>
                    <!-- 送出按鈕 -->
                    <div class="row col-sm-12">
                        <input role="button" type="submit" class="btn btn-theme btn-lg btn-block" value="本頁僅供查看報名表"/>
                    </div>
                </section>
            </section>
        </section>
    </section>

    <script type="text/javascript">
        $(document).ready(function () {
            var funcList = [getSectionList,
                            getQuestionList];
            $(document).queue("myQueue", funcList);
            $(document).dequeue("myQueue");
        })
        /* 活動報名 */
        // #region 取得區塊列表
        function getSectionList() {
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為getSectionList的function
                url: 'S02010105.aspx/getSectionList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //成功時
                success: function (result) {
                    // 加入區塊
                    if (result.d != "")
                        Add_Section(result.d);
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
                url: 'S02010105.aspx/getQuestionList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //成功時
                success: function (result) {
                    // 加入問題
                    if (result.d != "")
                        Add_Question(result.d);
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
            }
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
                            <div class="col-sm-10">\
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
                            <div class="col-sm-10">\
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
                            <div class="col-sm-10">\
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
                            <div class="col-sm-10">\
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
    </script>

</asp:Content>

