<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="Sign_Up.aspx.cs" Inherits="Register.Sign_Up" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sideCon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainCon" runat="server">
    <section id="container">
    <!-- **********************************************************************************************************************************************************
    MAIN CONTENT
    *********************************************************************************************************************************************************** -->
    <!--main content start-->
    <section id="main-content">
        <section class="wrapper">
            <div class="row"></div>
            <h3><i class="fa fa-angle-right"></i>活動報名</h3>
            <!-- BASIC FORM ELELEMNTS -->
            <div class="row mt">
                <div class="col-lg-12">
                    <div class="form-panel">
                        <h4 class="mb"><i class="fa fa-angle-right"></i>基本資料</h4>
                        <div class="form-horizontal style-form" >
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">姓名</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">電話</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control">
                                    <span class="help-block">請正確填寫電話，我們會傳送相關訊息到您的手機</span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">身份證字號</label>                                                           
                                <div class="col-sm-10">
                                    <asp:TextBox ID="IdNumber_txt" runat="server" class="form-control round-form"></asp:TextBox>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="格式錯誤" ControlToValidate="IdNumber_txt" MaximumValue="22" MinimumValue="18" ForeColor="Red"></asp:RangeValidator> 
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">地址</label>
                                <div class="col-sm-10">
                                    <input class="form-control" id="focusedInput" type="text" value="必填">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">提醒</label>
                                <div class="col-sm-10">
                                    <input class="form-control" id="disabledInput" type="text" placeholder="這裡是提醒內容" disabled>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">性別</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control" placeholder="男or女">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2 col-sm-2 control-label">不能填寫</label>
                                <div class="col-lg-10">
                                    <p class="form-control-static">我只是用來看的</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div><!-- col-lg-12-->
            </div><!-- /row -->
            <!-- INLINE FORM ELELEMNTS -->
            <div class="row mt">
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
                    </div><!-- /form-panel -->
                </div><!-- /col-lg-12 -->
            </div><!-- /row -->
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
                    </div><!-- /form-panel -->
                </div><!-- /col-lg-12 -->
            </div><!-- /row -->
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
                            <input type="checkbox" id="inlineCheckbox1" value="option1"> 多選1
                        </label>
                        <label class="checkbox-inline">
                            <input type="checkbox" id="inlineCheckbox2" value="option2"> 多選2
                        </label>
                        <label class="checkbox-inline">
                            <input type="checkbox" id="inlineCheckbox3" value="option3"> 多選3
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
                    </div><!-- /form-panel -->
                </div><!-- /col-lg-12 -->
            </div><!-- /col-lg-12 -->
                    <div class="row mt">
                        <a href="../Sign_Up_Check.aspx" role="button" type="submit" class="btn btn-theme btn-lg btn-block">送出</a>
                    </div>
        </section><! --/wrapper -->
    </section><!-- /MAIN CONTENT -->
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
        <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade" runat="server" >
            <div class="modal-dialog" >
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                        <h4 class="modal-title">報名成功</h4>
                    </div>

                    <div class="modal-body">
                        <p><h4>感謝您報名此活動，我們將會寄活動相關資訊到您的信箱，如需修改或刪除報名資料也請查閱信箱內容</h4></p>
                        <br /><br /><hr />
                        <p><h8>2016  逢甲大學資訊處 - 資訊技術服務中心</h8></p>
                    </div>

                    <div class="modal-footer">
                        <%--<button class="btn btn-theme" type="button">確認</button>--%>
                        <a href="../index.aspx" role="button" type="submit" class="btn btn-theme btn-lg">確認</a>
                    </div>
                </div>
            </div>
        </div>
        <!-- modal -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>
