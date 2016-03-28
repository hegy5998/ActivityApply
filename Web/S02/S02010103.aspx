<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S02010103.aspx.cs" Inherits="Web.S02.S02010103" %>

<%@ Register Src="~/UserControls/UCGridViewPager.ascx" TagPrefix="uc1" TagName="UCGridViewPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
    <script type="text/javascript">
        $(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="mainContentSubFunction_cph" runat="server">
    <asp:Button runat="server" ID="SaveEdit_btn" Text="儲存修改" OnClick="Save_btn_Click" CssClass="Distancebtn" />
    <asp:Button runat="server" ID="Back_btn" Text="返回列表" OnClick="Back_btn_Click" CssClass="Distancebtn" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent_cph" runat="server">
    <asp:Panel ID="form_panel" runat="server" Visible="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <div class="row">
                <! -- 1st ROW OF PANELS -->
                    <div role="tabpanel">
                        <ul class="nav nav-tabs nav-justified" role="tablist">
                            <li role="presentation" class="active"><a href="#ActivityInformation" aria-controls="ActivityInformation" role="tab" data-toggle="tab">修改活動資訊</a></li>
                            <li role="presentation"><a href="#SignUp" aria-controls="SignUp" role="tab" data-toggle="tab">修改報名表</a></li>
                        </ul>

                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane active" id="ActivityInformation">
                                <div class="row">
                                    <!-- TWITTER PANEL -->
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 desc">
                                        <div class="project-wrapper">
                                            <div class="project">
                                                <div class="photo-wrapper">
                                                    <div class="photo">
                                                        <a data-toggle="modal" href="#myModal"><img class="img-responsive" src="/Scripts/Lib/assets/img/fcu.jpg" alt=""></a>
                                                    </div>

                                                    <div class="overlay"></div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row"></div>
                                    </div><!-- /col-md-4 -->

                                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-12 dexc">
                                    <!-- WHITE PANEL - TOP USER -->
                                        <h3><i class="fa fa-angle-right"></i>活動場次</h3>
                                        <div class="showback">
                                            <div class="form-horizontal style-form" method="get">
                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">活動名稱</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" class="form-control">
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">活動開始日期</label>
                                                    <div class="col-sm-4">
                                                        <%--input type="text" class="form-control">--%>
                                                        <input class="form-control datetimepicker" type="text"  id="datetimepicker1"/>
                                                    </div>

                                                    <label class="col-sm-2 col-sm-2 control-label">活動結束日期</label>
                                                    <div class="col-sm-4">
                                                        <input class="form-control datetimepicker" type="text"  id="datetimepicker2"/>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">報名開始日期</label>
                                                    <div class="col-sm-4">
                                                        <input class="form-control datetimepicker" type="text"  id="datetimepicker3"/>
                                                    </div>

                                                    <label class="col-sm-2 col-sm-2 control-label">報名結束日期</label>
                                                    <div class="col-sm-4">
                                                        <input class="form-control datetimepicker" type="text"  id="datetimepicker4"/>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">活動地點</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" class="form-control">
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">人數限制</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" class="form-control">
                                                    </div>
                                                </div>

                                                <div class="form-group" style=" padding-right:10px;">
                                                    <a style="float:right;" href="#" type="submit" class="btn btn-theme">增加場次</a>
                                                </div>
                                            </div>
                                        </div>

                                        <h3><i class="fa fa-angle-right"></i>活動資訊</h3>

                                        <div class="showback">
                                            <div class="form-horizontal style-form">
                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">活動簡介</label>
                                                    <div class="col-sm-10">
                                                        <textarea class="form-control" rows="3"></textarea>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">主辦單位</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" class="form-control">
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">開放對象</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" class="form-control">
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">聯絡人</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" class="form-control">
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">聯絡人電話</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" class="form-control">
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">附加檔案</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" class="form-control">
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2 col-sm-2 control-label">相關連結</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" class="form-control">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div><!-- /col-md-4 -->
                                </div><! --/END 1ST ROW OF PANELS -->
                            </div>

                            <div role="tabpanel" class="tab-pane" id="SignUp">
                                <div class="row">
                                    <div class="col-lg-11 col-md-11 col-sm-11 col-xs-12 dexc showback">
                                        <h3><input type="text" class="form-control" placeholder="區塊一名稱"></h3>

                                        <div class="form-horizontal style-form column">
                                            <div class="form-group portlet" style="border: 0px;">
                                                <div class="row">
                                                    <div class="portlet-header center" style="background-color: #F1F2F7;height: 24px;">
                                                        <img src="../Images/drag_pic.jpg" alt="拖移" height="24px"/> 
                                                    </div>
                                
                                                    <div class="col-sm-11">
                                                        <%--<asp:TextBox  class="form-control" Text="文字問題">--%>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text="文字問題"></asp:TextBox>
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <div class="dropdown">
                                                            <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-expanded="true">
                                                                <span class="caret"></span>
                                                            </button>

                                                            <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">單選</a></li>
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">多選</a></li>
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">文字</a></li>
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">選單</a></li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-9">
                                                        <input type="text" class="form-control">
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-3 col-sm-push-9">
                                                        <div class="checkbox" style="float: left; padding-right: 10px;">
                                                            <label>
                                                                <input type="checkbox" value="">
                                                                必填
                                                            </label>
                                                        </div>

                                                        <a href="../create_Activity_Qus.aspx" type="submit" class="btn btn-theme ">資料驗證</a>
                                                        <a href="../create_Activity_Qus.aspx" type="submit" class="btn btn-theme ">刪除</a>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group portlet" style="border: 0px;">
                                                <div class="row">
                                                    <div class="col-sm-11 portlet-header">
                                                        <input type="text" class="form-control" placeholder="單選">
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <div class="dropdown">
                                                            <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu2" data-toggle="dropdown" aria-expanded="true">
                                                                <span class="caret"></span>
                                                            </button>

                                                            <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">單選</a></li>
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">多選</a></li>
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">文字</a></li>
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">選單</a></li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-9">
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optionsRadios" id="optionsRadios1" value="option1">
                                                                <input type="text" class="form-control">
                                                            </label>
                                                        </div>

                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optionsRadios" id="optionsRadios2" value="option2">
                                                                <input type="text" class="form-control">
                                                            </label>
                                                        </div>

                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optionsRadios" id="optionsRadios3" value="option3">
                                                                <a href="#">新增選項</a>
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-3 col-sm-push-9">
                                                        <div class="checkbox" style="float: left; padding-right: 10px;">
                                                            <label>
                                                                <input type="checkbox" value="">
                                                                必填
                                                            </label>
                                                        </div>

                                                        <a href="../create_Activity_Qus.aspx" type="submit" class="btn btn-theme ">資料驗證</a>
                                                        <a href="../create_Activity_Qus.aspx" type="submit" class="btn btn-theme ">刪除</a>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group portlet" style="border: 0px;">
                                                <div class="row">
                                                    <div class="col-sm-11 portlet-header">
                                                        <input type="text" class="form-control" placeholder="多選">
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <div class="dropdown">
                                                            <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu3" data-toggle="dropdown" aria-expanded="true">
                                                                <span class="caret"></span>
                                                            </button>

                                                            <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">單選</a></li>
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">多選</a></li>
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">文字</a></li>
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">選單</a></li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-9">
                                                        <div class="checkbox">
                                                            <label>
                                                                <input type="checkbox" value="">
                                                                <input type="text" class="form-control">
                                                            </label>
                                                        </div>

                                                        <div class="checkbox">
                                                            <label>
                                                                <input type="checkbox" value="">
                                                                <input type="text" class="form-control">
                                                            </label>
                                                        </div>

                                                        <div class="checkbox">
                                                            <label>
                                                                <input type="checkbox" value="">
                                                                <a href="#">新增選項</a>
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-3 col-sm-push-9">
                                                        <div class="checkbox" style="float: left; padding-right: 10px;">
                                                            <label>
                                                                <input type="checkbox" value="">
                                                                必填
                                                            </label>
                                                        </div>

                                                        <a href="../create_Activity_Qus.aspx" type="submit" class="btn btn-theme ">資料驗證</a>
                                                        <a href="../create_Activity_Qus.aspx" type="submit" class="btn btn-theme ">刪除</a>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group portlet" style="border: 0px;">
                                                <div class="row">
                                                    <div class="col-sm-11 portlet-header">
                                                        <input type="text" class="form-control" placeholder="下拉式選單">
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <div class="dropdown">
                                                            <button class="btn btn-default dropdown-toggle" type="button" id="dropdowsnMenu4" data-toggle="dropdown" aria-expanded="true">
                                                                <span class="caret"></span>
                                                            </button>

                                                            <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">單選</a></li>
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">多選</a></li>
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">文字</a></li>
                                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">選單</a></li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-9">
                                                        <div class="row">
                                                            <div class="col-sm-1">
                                                                <h5>1</h5>
                                                            </div>

                                                            <div class="col-sm-11 col-sm-pull-1">
                                                                <div class="checkbox">
                                                                    <label>
                                                                        <input type="text" class="form-control">
                                                                    </label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-1">
                                                            <h5>2</h5>
                                                        </div>

                                                        <div class="col-sm-11 col-sm-pull-1">
                                                            <div class="checkbox">
                                                                <label>
                                                                    <input type="text" class="form-control">
                                                                </label>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-1">
                                                            <h5>3</h5>
                                                        </div>

                                                        <div class="col-sm-11 col-sm-pull-1">
                                                            <div class="checkbox">
                                                                <label>
                                                                    <a href="#">新增選項</a>
                                                                </label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-3 col-sm-push-9">
                                                        <div class="checkbox" style="float: left; padding-right: 10px;">
                                                            <label>
                                                                <input type="checkbox" value="">
                                                                必填
                                                            </label>
                                                        </div>

                                                        <a href="../create_Activity_Qus.aspx" type="submit" class="btn btn-theme ">資料驗證</a>
                                                        <a href="../create_Activity_Qus.aspx" type="submit" class="btn btn-theme ">刪除</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- TWITTER PANEL -->
                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-12 desc">
                                        <div class="btn-group-vertical" role="group" aria-label="...">
                                            <a href="#" type="submit" class="btn btn-theme ">新增問題</a>
                                            <br />

                                            <a href="#" type="submit" class="btn btn-theme ">新增區塊</a>
                                            <br />

                                            <a data-toggle="modal" data-backdrop="static" href="#usuallyQusModal" role="group" type="submit" class="btn btn-theme">常用欄位</a>
                                            <br />

                                            <a href="#" type="submit" class="btn btn-theme ">載入範本</a>
                                            <br />

                                            <a href="#" type="submit" class="btn btn-theme ">檢視</a>
                                            <br />

                                            <asp:Button ID="Button1" Text="Submit" runat="server" />
                                        </div>
                                    </div>
                                    <!-- /col-md-4 -->

                                    <div class="col-lg-11 col-md-11 col-sm-11 col-xs-11 dexc showback">
                                        <h3><input type="text" class="form-control" placeholder="區塊二名稱"></h3>
                                            
                                        <div class="column form-horizontal style-form">
                                            <div class="form-group portlet" style="border: 0px;">
                                                <label class="col-sm-2 col-sm-2 control-label portlet-header">活動簡介</label>
                                                    
                                                <div class="col-sm-10">
                                                    <textarea class="form-control" rows="3"></textarea>
                                                </div>
                                            </div>

                                            <div class="form-group portlet" style="border: 0px;">
                                                <label class="col-sm-2 col-sm-2 control-label portlet-header">主辦單位</label>
                                                    
                                                <div class="col-sm-10">
                                                    <input type="text" class="form-control">
                                                </div>
                                            </div>

                                            <div class="form-group portlet" style="border: 0px;">
                                                <label class="col-sm-2 col-sm-2 control-label portlet-header">活動地點</label>
                                                    
                                                <div class="col-sm-10">
                                                    <input type="text" class="form-control">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>        
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Modal -->
                <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade text-center">
                    <div class="modal-dialog" style="display: inline-block; width: auto;">
                        <div class="modal-content">
                            <a title="Close" class="fancybox-item fancybox-close" href=""></a>

                            <div class="modal-body">
                                <img class="img-responsive" src="/Scripts/Lib/assets/img/fcu.jpg" alt="">
                            </div>
                        </div>
                    </div>
                </div>
                <!-- modal -->

                <link rel="stylesheet" href="http://localhost:53294/code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
                <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
                <script type="text/javascript" src="http://mybidrobot.allalla.com/jquery/jquery.ui.datepicker-zh-TW.js"></script>
                <link rel="stylesheet" href="/resources/demos/style.css"/>

                <script>
                    $(function () {
                        $("#datepicker1").datepicker({
                            changeYear: true,
                            changeMonth: true,  //月份下拉列表 
                            dateFormat: "yy/mm/dd",
                        });
                    });
                    $(function () {
                        $("#datepicker2").datepicker({
                            changeYear: true,
                            changeMonth: true,  //月份下拉列表 
                            dateFormat: "yy/mm/dd",
                        });
                    });
                    $(function () {
                        $("#datepicker3").datepicker({
                            changeYear: true,
                            changeMonth: true,  //月份下拉列表 
                            dateFormat: "yy/mm/dd",
                        });
                    });
                    $(function () {
                        $("#datepicker4").datepicker({
                            changeYear: true,
                            changeMonth: true,  //月份下拉列表 
                            dateFormat: "yy/mm/dd",
                        });
                    });
                </script>

                <script>
                    $(function () {
                        $('.datetimepicker').datetimepicker({ lang: 'ch' });
                    });
                </script>

                <uc1:UCGridViewPager runat="server" ID="ucGridViewPager" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
