<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="Sign_Up.aspx.cs" Inherits="ActivityApply.Sign_Up" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
    <style type="text/css">
        .mb {
            margin-bottom: 6px;
        }

        .desc {
            margin-bottom: 15px;
            margin-left: 25px;
        }

        .form-control {
            width: 50%;
        }

        .form-horizontal .control-label {
            text-align: right;
        }

        .help-block {
            margin-top: 7px;
        }

        .control-label {
            font-weight: bold;
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
                <!-- BASIC FORM ELELEMNTS -->
                <div id="sections_div">

                </div>
                <div class="row col-sm-12">
                    <a href="../Sign_Up_Check.aspx" role="button" type="submit" class="btn btn-theme btn-lg btn-block">送出</a>
                </div>
                <!-- /row -->
                <!-- INLINE FORM ELELEMNTS -->
                <%--                 <div class="row mt">
                    <div class="col-lg-12">
                        <div class="form-panel">
                            <h4 class="mb"><i class="fa fa-angle-right"></i>第二區段</h4>
                            <div class="form-inline" role="form">
                                <div class="form-group">
                                    <label class="sr-only" for="exampleInputEmail2">Email address</label>
                                    <input type="email" class="form-control" id="exampleInputEmail2" placeholder="信箱">
                                </div>
                                <div class="form-group">
                                    <label class="sr-only" for="exampleInputPassword2">Password</label>
                                    <input type="password" class="form-control" id="exampleInputPassword2" placeholder="密碼">
                                </div>
                                <button type="submit" class="btn btn-theme">登入</button>
                            </div>
                        </div>
                        <!-- /form-panel -->
                    </div>
                    <!-- /col-lg-12 -->
                </div>
                <!-- /row -->
                <!-- INPUT MESSAGES -->
                <div class="row mt">
                    <div class="col-lg-12">
                        <div class="form-panel">
                            <h4 class="mb"><i class="fa fa-angle-right"></i>資料驗證</h4>
                            <div class="form-horizontal tasi-form">
                                <div class="form-group has-success">
                                    <label class="col-sm-2 control-label col-lg-2" for="inputSuccess">輸入資料正確</label>
                                    <div class="col-lg-10">
                                        <input type="text" class="form-control" id="inputSuccess">
                                    </div>
                                </div>
                                <div class="form-group has-warning">
                                    <label class="col-sm-2 control-label col-lg-2" for="inputWarning">輸入資料有問題</label>
                                    <div class="col-lg-10">
                                        <input type="text" class="form-control" id="inputWarning">
                                    </div>
                                </div>
                                <div class="form-group has-error">
                                    <label class="col-sm-2 control-label col-lg-2" for="inputError">未輸入資料</label>
                                    <div class="col-lg-10">
                                        <input type="text" class="form-control" id="inputError">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /form-panel -->
                    </div>
                    <!-- /col-lg-12 -->
                </div>
                <!-- /row -->
                <!-- INPUT MESSAGES -->
                <div class="row mt">
                    <div class="col-lg-12">
                        <div class="form-panel">
                            <h4 class="mb"><i class="fa fa-angle-right"></i>單選、多選、下拉式選單</h4>
                            <div class="checkbox">
                                <label>
                                    <input type="checkbox" value="">
                                    我是多選選擇1
                                </label>
                            </div>
                            <div class="checkbox">
                                <label>
                                    <input type="checkbox" value="">
                                    我是多選選擇2
                                </label>
                            </div>
                            <div class="checkbox">
                                <label>
                                    <input type="checkbox" value="">
                                    我是多選選擇3
                                </label>
                            </div>

                            <div class="radio">
                                <label>
                                    <input type="radio" name="optionsRadios" id="optionsRadios1" value="option1">
                                    我是圓形選擇1
                                </label>
                            </div>
                            <div class="radio">
                                <label>
                                    <input type="radio" name="optionsRadios" id="optionsRadios2" value="option2">
                                    我是圓形選擇2
                                </label>
                            </div>
                            <div class="radio">
                                <label>
                                    <input type="radio" name="optionsRadios" id="optionsRadios3" value="option3">
                                    我是圓形選擇3
                                </label>
                            </div>

                            <hr>
                            <label class="checkbox-inline">
                                <input type="checkbox" id="inlineCheckbox1" value="option1">
                                多選1
                            </label>
                            <label class="checkbox-inline">
                                <input type="checkbox" id="inlineCheckbox2" value="option2">
                                多選2
                            </label>
                            <label class="checkbox-inline">
                                <input type="checkbox" id="inlineCheckbox3" value="option3">
                                多選3
                            </label>

                            <hr>
                            <select class="form-control">
                                <option>下拉式選單1</option>
                                <option>下拉式選單2</option>
                                <option>下拉式選單3</option>
                                <option>下拉式選單4</option>
                                <option>下拉式選單5</option>
                                <option>下拉式選單6</option>
                                <option>下拉式選單7</option>
                                <option>下拉式選單8</option>
                                <option>下拉式選單9</option>
                                <option>下拉式選單10</option>
                            </select>
                            <br>
                        </div>
                        <!-- /form-panel -->
                    </div>
                    <!-- /col-lg-12 -->
                </div>
                <!-- /col-lg-12 -->--%>
            </section>
            <! --/wrapper -->
        </section>
        <!-- /MAIN CONTENT -->
        <!--footer end-->
    </section>

    <%--<!-- js placed at the end of the document so the pages load faster -->
<script src="assets/js/jquery.js"></script>
<script src="assets/js/bootstrap.min.js"></script>
<script class="include" type="text/javascript" src="assets/js/jquery.dcjqaccordion.2.7.js"></script>
<script src="assets/js/jquery.scrollTo.min.js"></script>
<script src="assets/js/jquery.nicescroll.js" type="text/javascript"></script>


<!--common script for all pages-->
<script src="assets/js/common-scripts.js"></script>

<!--script for this page-->
<script src="assets/js/jquery-ui-1.9.2.custom.min.js"></script>

<!--custom switch-->
<script src="assets/js/bootstrap-switch.js"></script>

<!--custom tagsinput-->
<script src="assets/js/jquery.tagsinput.js"></script>

<!--custom checkbox & radio-->

<script type="text/javascript" src="assets/js/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
<script type="text/javascript" src="assets/js/bootstrap-daterangepicker/date.js"></script>
<script type="text/javascript" src="assets/js/bootstrap-daterangepicker/daterangepicker.js"></script>

<script type="text/javascript" src="assets/js/bootstrap-inputmask/bootstrap-inputmask.min.js"></script>
    --%>

    <%--<script src="assets/js/form-component.js"></script>


<script>
    //custom select box

    $(function () {
        $('select.styled').customSelect();
    });

</script>--%>


    <!-- Modal -->
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade" runat="server">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                    <h4 class="modal-title">報名成功</h4>
                </div>

                <div class="modal-body">
                    <p>
                        <h4>感謝您報名此活動，我們將會寄活動相關資訊到您的信箱，如需修改或刪除報名資料也請查閱信箱內容</h4>
                    </p>
                    <br />
                    <br />
                    <hr />
                    <p>
                        <h8>2016  逢甲大學資訊處 - 資訊技術服務中心</h8>
                    </p>
                </div>

                <div class="modal-footer">
                    <%--<button class="btn btn-theme" type="button">確認</button>--%>
                    <a href="../index.aspx" role="button" type="submit" class="btn btn-theme btn-lg">確認</a>
                </div>
            </div>
        </div>
    </div>
    <!-- modal -->
    <script type="text/javascript">
        // 新增區塊
        function Add_Section(sectionInfo) {
            var sectionInfo = JSON.parse(sectionInfo);
            for (var i = 0; i < sectionInfo.length; i++) {
                $("#sections_div").append(Section_Code(sectionInfo[i]));
            }
        }

        // 新增問題
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

        // 區塊程式碼
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

        //#region 文字欄位程式碼
        function TextCol_Code(question, seq) {
            var qusId = "qus_div_" + seq;
            var txtId = "qus_txt_" + seq;
            var code = '<div id="' + qusId + '" class="form-group">\
                            <label class="col-sm-2 control-label">' + question.Acc_title + '</label>\
                            <div class="col-sm-10">\
                                <input type="text" id="' + txtId + '" class="form-control">\
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
                //傳送資料到後台為save_Activity_Form的function
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
                //傳送資料到後台為save_Activity_Form的function
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
    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>
