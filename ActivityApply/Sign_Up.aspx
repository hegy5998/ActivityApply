<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="Sign_Up.aspx.cs" Inherits="ActivityApply.Sign_Up" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
    <!-- Jquery Validation -->
    <script src="assets/js/validation/jquery.validate.js"></script>
    <script src="assets/js/validation/messages_zh_TW.js"></script>
    <script src="assets/js/validation/additional-methods.js"></script>
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
        input.error{
            box-shadow: 0px 0px 9px red;
        }
        .required::after {
            content:"*";
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sideCon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainCon" runat="server">
    <section id="container">
        <section id="main-content">
            <section class="wrapper">
                <div class="row"></div>
                <h3><i class="fa fa-angle-right"></i>活動報名</h3>
                <!-- 放置區塊區域 -->
                <div id="sections_div">
                    <div id="thrth" class="form-group">
                            <label class="col-sm-2 control-label">rthtrhrt</label>
                            <div class="col-sm-10">
                                <input type="text" id="rthrthrtb" class="form-control {validate:{required:true, minlength:2}}">
                                <span class="help-block">rbhetbhetb</span>
                            </div>
                        </div>
                </div>
                <!-- 送出按鈕 -->
                <div class="row col-sm-12">
                    <a href="../Sign_Up_Check.aspx" role="button" type="submit" class="btn btn-theme btn-lg btn-block">送出</a>
                </div>
            </section>
        </section>
    </section>

    <script type="text/javascript">
        $(document).ready(function () {
            getSectionList();
            getQuestionList();
            
            //var a = getSectionList();
            //alert(a);
            //$("input").focus(function () {

            //})
            //$("input").blur(function () {
            //    $("#text1").css({ "box-shadow": "0px 0px 9px red" });
            //})
        })

        // #region 取得區塊列表
        function getSectionList() {
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為getSectionList的function
                url: '/Sign_Up.aspx/getSectionList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //成功時
                success: function (result) {
                    // 加入區塊
                    Add_Section(result.d)
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
                url: '/Sign_Up.aspx/getQuestionList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //成功時
                success: function (result) {
                    // 加入區塊
                    Add_Question(result.d)
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
            var sectionInfo = JSON.parse(sectionInfo);
            for (var i = 0; i < sectionInfo.length; i++) {
                $("#sections_div").append(Section_Code(sectionInfo[i]));
            }
        }
        //#endregion

        //#region 新增問題
        function Add_Question(questionInfo) {
            var questionInfo = JSON.parse(questionInfo);
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
            var code = '<div id="' + sectionId + '" class="row col-sm-12 form-panel">\
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
            var txtId = "qus_txt_" + seq;
            var code = '<div id="' + qusId + '" class="form-group">\
                            <label class="col-sm-2 control-label">' + question.Acc_title + '</label>\
                            <div class="col-sm-10">\
                                <input type="text" id="' + txtId + '" class="form-control' + Validate(question.Acc_validation) + '">\
                                <span class="help-block">' + question.Acc_desc + '</span>\
                            </div>\
                        </div>';
            return code;
        }
        //#endregion

        //#region 單選欄位程式碼
        function SingleSelCol_Code(question, seq) {
            var qusId = "qus_div_" + seq;
            var txtId = "qus_txt_" + seq;
            var qusName = "qus_radio_" + seq;
            var code = '<div id="' + qusId + '" class="form-group">\
                            <label class="col-sm-2 control-label">' + question.Acc_title + '</label>\
                            <div class="col-sm-10">\
                                <span class="help-block">' + question.Acc_desc + '</span>';

            //反序列化
            question.Acc_option.split('&').forEach(function (param) {
                param = param.split('=');
                var val = param[1];
                code += '<div class="radio">\
                            <label>\
                                <input type="radio" name="' + qusName + '">\
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
            var txtId = "qus_txt_" + seq;
            var code = '<div id="' + qusId + '" class="form-group">\
                            <label class="col-sm-2 control-label">' + question.Acc_title + '</label>\
                            <div class="col-sm-10">\
                                <span class="help-block">' + question.Acc_desc + '</span>';

            //反序列化
            question.Acc_option.split('&').forEach(function (param) {
                param = param.split('=');
                var val = param[1];
                code += '<div class="checkbox">\
                            <label>\
                                <input type="checkbox">' + val + '\
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
            var txtId = "qus_txt_" + seq;
            var code = '<div id="' + qusId + '" class="form-group">\
                            <label class="col-sm-2 control-label">' + question.Acc_title + '</label>\
                            <div class="col-sm-10">\
                                <select class="form-control">';


            //反序列化
            question.Acc_option.split('&').forEach(function (param) {
                param = param.split('=');
                var val = param[1];
                code += '<option>' + val + '</option>';
            })
            code += '</select>\
                     <span class="help-block">' + question.Acc_desc + '</span>\
                  </div>\
               </div>';
            return code;
        }
        //#endregion

        //#region 資料驗證
        function Validate(mode) {
            var code = '';
            switch (mode.split(",")[0]) {
                case 'required': code += ' required';
                case 'email': code += ' email';
                case 'idNumber': code += ' TWIDCheck';
                case 'cellPhone': code += ' mobileTW';
                case 'date': code += ' date';
                case 'url': code += ' url';
            }
            return code;
        }
        //#endregion
    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>
