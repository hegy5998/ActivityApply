<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S02010201.aspx.cs" Inherits="Web.S02.S02010201" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
    <style type="text/css">
        .table {
            box-shadow: none;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentSubFunction_cph" runat="server">
    <%--    <!-- 儲存活動頁面 -->
    <input type="button" onclick="Save_btn_Click()" value="儲存活動頁面" />
    <!-- 儲存報名表 -->
    <input type="button" onclick="Save_Activity_btn_Click()" value="儲存報名表" />
    <!-- 上傳檔案 -->
    <input type="button" onclick="upload_File()" value="上傳檔案" />--%>
    <input id="loading" type="button" data-toggle="modal" data-target="#myModal1" data-backdrop="static" role="group" Style="display: none;"/>
    <!-- 隱藏按鈕防止按下Enter返回列表 -->
    <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="return false;" Style="display: none;" />
    <!-- 儲存活動 -->
    <input type="button" onclick="save()" value="儲存活動" />
    <!-- 檢視 -->
    <input type="button" onclick="view_Activity()" value="檢視報名表" />
    <!-- 新增問題 -->
    <input id="add_qus_btn" type="button" onclick="add_Preset_Qus_Click(null, null, null, 'text', '', false)" value="新增問題" style="display: none;" />
    <!-- 新增區塊 -->
    <input id="add_block_btn" type="button" onclick="add_Block_Click()" value="新增區塊" style="display: none;" />
    <!-- 常用欄位 -->
    <input id="add_usually_btn" type="button" data-toggle="modal" data-target="#myModal" data-backdrop="static" role="group" value="常用欄位" style="display: none;" />
    <!-- 返回列表 -->
    <asp:Button runat="server" ID="Back_btn" Text="返回列表" OnClick="Back_btn_Click" CssClass="Distancebtn" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent_cph" runat="server">
    <!-- 如果沒有開啟JavaScript則會請她凱起才能使用本網頁的功能 -->
    <noscript>
        <h1 class="red blink">您沒有開啟JavaScript的功能，請您開啟才能使用本網站的功能!!</h1>
        <h3>開啟方法如下:</h3>
        <h4>IE : 網際網路選項 -> 安全性 -> 網際網路 -> 自訂等級 -> 指令碼處理 -> 啟用</h4>
        <h4>Firefox : 工具 -> 網頁開發者 -> 網頁工具箱 -> 選項 -> 取消打勾「停用JavaScript」</h4>
        <h4>Chrome : 設定 -> 顯示進階設定 -> 隱私權 -> 內容定... -> JavaScript -> 選擇「允許所有網站執行JavaScript」</h4>
    </noscript>
    <!-- 統一公告事項 START -->
    <div class="alert alert-danger" role="alert" style="margin-bottom: 0px; height: 40px;">
        <h4 style="margin-bottom: 0px; text-align: center; line-height: 40px;">請注意：為因應個資法，活動超過『活動日期』後３０天，系統將會自動移除此相關報名資訊！請審慎使用！</h4>
    </div>
    <div class="alert alert-warning" role="alert" style="margin-bottom: 0px; height: 40px;">
        <h4 style="margin-bottom: 0px; text-align: center; line-height: 40px;">請在建立完成活動頁面後，接著建立活動報名表 !</h4>
    </div>
    <!-- 統一公告事項 END -->
    <div id="Tabs" role="tabpanel">
        <!-- 建立活動標籤_START-->
        <ul class="nav nav-tabs nav-justified" id="myTab" role="tablist">
            <li class="tab-pane active">
                <a data-toggle="tab" href="#activityMenu" onclick="activityMenu()" style="background-color: white;">活動頁面</a>
            </li>
            <li class="tab-pane">
                <a data-toggle="tab" href="#activityQus" onclick="activityQus()" style="background-color: white;">活動報名表</a>
            </li>
        </ul>
        <!-- 建立活動標籤_END-->

        <!-- 標籤內容_START -->
        <div class="tab-content">
            <!-- 活動頁面標籤 START -->
            <div class="row mt tab-pane active" id="activityMenu" role="tabpanel" style="background-color: white; margin: 0px 1px 0px 1px;">
                <div style="margin: 25px 0px 0px 0px;">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 desc" runat="server">
                        <div class="project-wrapper">
                            <div class="project">
                                <div class="photo-wrapper">
                                    <div class="photo">
                                        <img class="img-responsive" src="../Scripts/Lib/assets/img/fcu.jpg" alt="" onclick="upload_img()" />
                                    </div>
                                    <div class="overlay"></div>
                                </div>
                            </div>
                            <asp:UpdatePanel ChildrenAsTriggers="false" runat="server" UpdateMode="Conditional" ViewStateMode="Enabled">
                                <ContentTemplate>
                                    <label id="imgpath_lab">選擇圖片</label>
                                    <br />
                                    <label class="red">圖片僅限制上傳jpg、jpeg、png類型檔案</label>
                                    <br />
                                    <label class="red">大小限制為2MB</label>
                                    <asp:FileUpload ID="imgUpload" runat="server" onchange="upload_imgpath(this.value)" Style="display: none" accept=".png,.jpg,.jpeg,.gif" />
                                    <asp:Button ID="imageUpload_btn" runat="server" Style="display: none" OnClick="imageUpload_btn_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="imageUpload_btn"></asp:PostBackTrigger>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row"></div>
                    </div>
                    <!-- 活動頁面內容_START -->
                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-12 dexc">
                        <!-- 增加活動區塊地方_START -->
                        <div class="row" id="add_Session_div">
                            <!-- 活動場次區塊_START -->
                            <h3><i class="fa fa-angle-right"></i>活動分類</h3>

                            <select class="select" id="select_class" style="width: 100%; height: 30px; border-radius: 4px; margin-bottom: 15px;">
                            </select>

                            <!-- 活動名稱 -->
                            <h3><i class="fa fa-angle-right"></i>活動名稱</h3>
                            <input type="text" class="form-control" placeholder="活動名稱" id="activity_Name_txt" maxlength="60" /><br />
                            <!-- 報名限制 -->
                            <h3><i class="fa fa-angle-right"></i>報名次數限制<a title="此區填寫的數字可以限制同一個報名者(姓名以及信箱相同)在這個活動最多可以報名幾個場次"> <i class="fa fa-question-circle" aria-hidden="true"></i></a></h3>
                            <input type="text" class="form-control" placeholder="不填寫則不限制(只能填寫數字)" id="act_num_limit_txt" maxlength="3" onkeyup="return ValidateNumber($(this),value)" /><br />

                        </div>
                        <!-- 增加活動區塊地方_END -->

                        <!-- 活動資訊區塊_START -->
                        <div class="row">
                            <h3><i class="fa fa-angle-right"></i>活動資訊</h3>
                            <div class="showback">
                                <div class="form-horizontal style-form">
                                    <!-- 活動簡介 -->
                                    <div class="form-group">
                                        <label class="col-sm-2 col-sm-2 control-label">活動簡介</label>
                                        <div class="col-sm-10" style="width: 100%; margin-top: 15px;">
                                            <!-- 活動簡介，使用CKeditor -->
                                            <textarea cols="80" id="editor1" name="editor1" rows="10"></textarea>
                                            <script type="text/javascript">
                                                CKEDITOR.replace('editor1',
                                                    {

                                                    });
                                            </script>
                                        </div>
                                    </div>
                                    <!-- 主辦單位 -->
                                    <div class="form-group">
                                        <label class="col-sm-2 col-sm-2 control-label">主辦單位</label>
                                        <div class="col-sm-10">
                                            <input type="text" class="form-control" id="unit_txt" maxlength="20" />
                                        </div>
                                    </div>
                                    <!-- 聯絡人 -->
                                    <div class="form-group">
                                        <label class="col-sm-2 col-sm-2 control-label">聯絡人</label>
                                        <div class="col-sm-10">
                                            <input type="text" class="form-control" id="contact_Person_txt" maxlength="30" />
                                        </div>
                                    </div>
                                    <!-- 聯絡人電話 -->
                                    <div class="form-group">
                                        <label class="col-sm-2 col-sm-2 control-label">聯絡人電話</label>
                                        <div class="col-sm-10">
                                            <input type="text" class="form-control" id="contact_Person_Phone_txt" maxlength="30" />
                                        </div>
                                    </div>
                                    <!-- 附加檔案 -->
                                    <div class="form-group">
                                        <label class="col-sm-2 col-sm-2 control-label">附加檔案</label>
                                        <div class="col-sm-10">
                                            <asp:UpdatePanel ChildrenAsTriggers="false" runat="server" UpdateMode="Conditional" ViewStateMode="Enabled">
                                                <ContentTemplate>
                                                    <input type="button" onclick="upload_file_btn()" class="btn btn-theme" value="選擇檔案" />
                                                    <label id="filepath_lab">選擇檔案</label>
                                                    <asp:FileUpload ID="FileUpload" runat="server" onchange="upload_file(this.value)" Style="display: none" />
                                                    <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click1" Style="display: none" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnUpload"></asp:PostBackTrigger>
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <br />
                                            <label class="red">僅限制上傳jpg、png、jpeg、gif、doc、docx、txt、ppt、pptx、xls、xlsx、pdf、rar、zip、7z類型檔案</label>
                                            <br />
                                            <label class="red">大小限制為4MB</label>
                                        </div>
                                    </div>
                                    <!-- 相關連結 -->
                                    <div class="form-group">
                                        <label class="col-sm-2 col-sm-2 control-label">相關連結</label>
                                        <div class="col-sm-10">
                                            <input type="text" class="form-control" placeholder="https://" id="relate_Link" maxlength="255" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- 活動資訊區塊_END -->
                    </div>
                    <!-- 活動頁面內容_END -->
                </div>
            </div>
            <!-- 活動頁面標籤_END -->

            <!-- 活動報名表標籤_START-->
            <div class="row mt tab-pane fade" id="activityQus" role="tabpanel" style="background-color: white; margin: 0px 1px 0px 1px;">
                <!-- 新增區塊地方_START-->
                <div class="row mt " id="add_Block_div" style="margin: 25px 0px 0px 25px;">
                    <!-- 預設區塊一_START -->
                    <div class="col-lg-11 col-md-11 col-sm-11 col-xs-11 dexc showback" id="block_div_1">
                        <!-- 預設區塊一名稱_基本資料 -->
                        <h3>
                            <input type="text" id="block_title_txt_1" class="form-control" placeholder="區塊名稱" value="基本資料" maxlength="60" /></h3>
                        <input type="text" id="block_Description_txt_1" class="form-control" placeholder="區塊描述" maxlength="255" />
                        <!-- 新增問題地方 START-->
                        <div class="form-horizontal style-form column " id="add_Qus_div_1" style="min-height: 50px">
                            <div id="qus_div_1" class="form-group portlet showback" style="border: 0px; background: #ffffff; padding: 15px; margin-bottom: 15px; box-shadow: 0px 0px 2px #272727; border-radius: 20px; margin: 20px 0px 0px 1px;">
                                <div class="col-sm-1 portlet-header center" style="background-color: #F1F2F7; height: 100px; width: 35px">
                                    <img src="../Images/drag_pic.jpg" alt="拖移" style="height: 24px; transform: translateY(-50%); top: 50%; position: relative;" />
                                </div>
                                <div class="row">
                                    <div class="col-sm-10" id="change_Qus_Way_div_1">
                                        <div class="col-sm-10" style="width: 100%">
                                            <input type="text" id="qus_txt_1" class="form-control" style="width: 100%;" value="姓名" disabled="" />
                                        </div>
                                        <div class="col-sm-10" style="width: 100%">
                                            <input type="text" id="qus_context_txt_1" placeholder="問題描述" class="form-control" style="width: 100%; margin-top: 15px" maxlength="255" />
                                        </div>
                                    </div>
                                    <div class="col-sm-1" style="padding: inherit;">
                                        <select class="select" id="select_1" disabled="" style="width: 56px; height: 34px; border-radius: 4px; background-color: #EEEEEE; float: right;">
                                            <option selected="selected" value="text">文字</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6" id="change_Qus_Content_div_1">
                                    </div>
                                    <div>
                                        <div style="float: right;">
                                            <div class="checkbox checkbox-slider--b-flat checkbox-slider-md" style="float: left; padding-right: 10px;">
                                                <label>
                                                    <input id="required_checkbox_1" type="checkbox" checked="checked" disabled="" /><span><a style="font-size: 16px; margin-left: -126px;">必填</a></span>
                                                </label>
                                            </div>
                                            <select class="select_Validation" id="select_Validation_1" disabled="" style="width: 100px; height: 34px; border-radius: 4px; margin-left: 22px; margin-top: 5px; background-color: #EEEEEE;">
                                                <option selected="selected" value="length">字數</option>
                                            </select>
                                        </div>
                                        <div id="add_Validation_div_1" style="margin-right: 39px; float: right;">
                                            <label class="col-sm-1 control-label" style="width: auto; margin-top: 6px;">最小值</label>
                                            <div class="col-sm-1" style="width: auto; margin-top: 6px;">
                                                <input type="text" id="min_Num_1" class="form-control" style="width: 85px; margin-bottom: 8px; margin-left: -15px; background-color: #EEEEEE;" placeholder="可不填" disabled="" value="0" />
                                            </div>
                                            <label class="col-sm-1 control-label" style="width: auto; margin-top: 6px;">最大值</label>
                                            <div class="col-sm-1" style="width: auto; margin-top: 6px;">
                                                <input type="text" id="max_Num_1" class="form-control" style="width: 85px; margin-bottom: 8px; margin-left: -15px; background-color: #EEEEEE;" placeholder="可不填" disabled="" value="50" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="qus_div_2" class="form-group portlet showback" style="border: 0px; background: #ffffff; padding: 15px; margin-bottom: 15px; box-shadow: 0px 0px 2px #272727; border-radius: 20px; margin: 20px 0px 0px 1px;">
                                <div class="col-sm-1 portlet-header center" style="background-color: #F1F2F7; height: 100px; width: 35px">
                                    <img src="../Images/drag_pic.jpg" alt="拖移" style="height: 24px; transform: translateY(-50%); top: 50%; position: relative;" />
                                </div>
                                <div class="row">
                                    <div class="col-sm-10" id="change_Qus_Way_div_2">
                                        <div class="col-sm-10" style="width: 100%">
                                            <input type="text" id="qus_txt_2" class="form-control" style="width: 100%;" value="電子信箱Email" disabled="" />
                                        </div>
                                        <div class="col-sm-10" style="width: 100%">
                                            <input type="text" id="qus_context_txt_2" placeholder="問題描述" class="form-control" style="width: 100%; margin-top: 15px" maxlength="255" />
                                        </div>
                                    </div>
                                    <div class="col-sm-1" style="padding: inherit;">
                                        <select class="select" id="select_2" disabled="" style="width: 56px; height: 34px; border-radius: 4px; background-color: #EEEEEE; float: right">
                                            <option selected="selected" value="text">文字</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6" id="change_Qus_Content_div_2">
                                    </div>
                                    <div>
                                        <div style="float: right;">
                                            <div class="checkbox checkbox-slider--b-flat checkbox-slider-md" style="float: left; padding-right: 10px;">
                                                <label>
                                                    <input id="required_checkbox_2" type="checkbox" checked="checked" disabled="" /><span><a style="font-size: 16px; margin-left: -126px;">必填</a></span>
                                                </label>
                                            </div>
                                            <select class="select_Validation" id="select_Validation_2" disabled="" style="width: 100px; height: 34px; border-radius: 4px; margin-left: 22px; margin-top: 5px; background-color: #EEEEEE;">
                                                <option selected="selected" value="email">電子信箱</option>
                                            </select>
                                        </div>
                                        <div class="row" id="add_Validation_div_2" style="margin-left: -25px;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- 新增問題地方_END-->
                    </div>
                    <!-- 預設區塊一_END -->

                    <%--<!-- 功能列_START -->
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1" style="padding: 0px">
                        <div class="nav nav-tabs nav-stacked" id="suspension_div" data-spy="affix">
                            <div class="btn-group-vertical" role="group" aria-label="...">
                                <!-- 新增問題 -->
                                <a class="btn btn-theme " onclick="add_Preset_Qus_Click(null , null , null , 'text' , '' , false)" style="border-radius: 8px;">新增問題</a>
                                <br />
                                <!-- 新增區塊 -->
                                <a class="btn btn-theme " onclick="add_Block_Click()" style="border-radius: 8px;">新增區塊</a>
                                <br />
                                <!-- 常用欄位 -->
                                <a data-toggle="modal" data-target="#myModal" data-backdrop="static" role="group" class="btn btn-theme" style="border-radius: 8px;">常用欄位</a>
                                <br />
                                <!-- 載入範本 -->
                                <a type="submit" class="btn btn-theme " onclick="getText()">載入範本</a>
                            </div>
                        </div>
                    </div>
                    <!-- 功能列_END -->--%>

                    <!-- 預設區塊二_START -->
                    <div class="col-lg-11 col-md-11 col-sm-11 col-xs-11 dexc showback" id="block_div_2">
                        <div class="col-sm-1" style="left: 95%; height: 40px;">
                            <a class="btn" style="color: #768094; padding-left: 3px;" onclick="del_block_click(2)">X</a>
                        </div>
                        <h3>
                            <input type="text" id="block_title_txt_2" class="form-control" placeholder="區塊名稱" value="其他" maxlength="60" /></h3>
                        <input type="text" id="block_Description_txt_2" class="form-control" placeholder="區塊描述" maxlength="255" />
                        <div class="column form-horizontal style-form" id="add_Qus_div_2" style="min-height: 50px">
                        </div>
                    </div>
                    <!-- 預設區塊二_END -->
                </div>
                <!-- 新增區塊地方_END-->
            </div>
            <!-- 活動報名表標籤_END-->
        </div>
        <!-- 標籤內容_END -->
    </div>
    <!-- 常用欄位_Modal_START-->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="margin: 2% 30%; padding: 0px 0px; border-radius: 6px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="myModalLabel">常用欄位</h4>
                </div>
                <div class="modal-body">
                    <%--                    <div class="checkbox">
                        <label>
                            <input name="check_usually_Column" type="checkbox" value="姓名" />
                            姓名
                        </label>
                    </div>--%>
                    <div class="checkbox">
                        <label>
                            <input name="check_usually_Column" type="checkbox" value="出生年月日" />
                            出生年月日
                        </label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input name="check_usually_Column" type="checkbox" value="身份證字號" />
                            身份證字號
                        </label>
                    </div>
                    <%--                    <div class="checkbox">
                        <label>
                            <input name="check_usually_Column" type="checkbox" value="信箱Email" />
                            信箱Email
                        </label>
                    </div>--%>
                    <div class="checkbox">
                        <label>
                            <input name="check_usually_Column" type="checkbox" value="聯絡電話" />
                            聯絡電話
                        </label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input name="check_usually_Column" type="checkbox" value="聯絡地址" />
                            聯絡地址
                        </label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input name="check_usually_Column" type="checkbox" value="公司電話" />
                            公司電話
                        </label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input name="check_usually_Column" type="checkbox" value="服務單位" />
                            服務單位
                        </label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input name="check_usually_Column" type="checkbox" value="職稱" />
                            職稱
                        </label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input name="check_usually_Column" type="checkbox" value="傳真" />
                            傳真
                        </label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input name="check_usually_Column" type="checkbox" value="用餐" />
                            用餐
                        </label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input name="check_usually_Column" type="checkbox" value="備註" />
                            備註
                        </label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="add_Usually_Column()">增加</button>
                    <button type="button" class="btn btn-primary" data-dismiss="modal">取消</button>
                </div>
            </div>
        </div>
    </div>
    <!-- 常用欄位_Modal_END-->

    <div class="modal fade" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="margin: 2% 30%; padding: 0px 0px; border-radius: 6px;text-align: center;">
            <div class="">
                <div class="modal-body">
                    <i class="fa fa-spinner fa-pulse fa-5x fa-fw margin-bottom" style="color: white"></i>
                    <span class="sr-only">Loading...</span>
                </div>
            </div>
        </div>
    </div>

    <!-- 隱藏欄位儲存報名表JSON字串-->
    <input type="hidden" id="save_Activity_Column_Json" />
    <input type="hidden" id="save_Activity_Json" />
    <!-- JavaScript_START-->
    <script type="text/javascript">


        //#region 沒有開啟JavaScript閃爍文字提醒
        $(function () {
            setInterval(flicker, 1000);//迴圈閃爍，間隔1秒
        })
        function flicker() {//閃爍函數
            $('.shine').fadeOut(500).fadeIn(500);
        }
        //#endregion

        //問題ID
        var qusId = 3;
        //區塊ID
        var blockId = 3;
        //場次ID
        var sessionId = 1;
        //判斷活動頁面資料是否正確
        var check_Activity_Data = false;
        //判斷報名表資料是否正確
        var check_Activity_Column_Data = false;
        //判斷活動報名結束日期是否在活動開始日期之後
        var checkDataSignEnd = false;

        //#region 頁面載入時自動產生問題
        $(document).ready(function () {
            //設定分類下拉選單資料
            getClass();
            //載入預設場次
            add_Session_click();
            //add_Preset_Qus_Click(題目名稱 , 題目描述 , 欲加入的區塊 , 題目模式 , 資料驗證 ,選項內容 , 是否必填)
            //add_Preset_Qus_Click("姓名", "請填寫完整名字", 1, "text", "", [], true);
            //add_Preset_Qus_Click("出生年月日", "ex:83/07/08", 1, "text", "", [], true);
            //add_Preset_Qus_Click("服務單位", "請填寫完整名稱", 1, "text", "", [], true);
            //add_Preset_Qus_Click("職稱", null, 1, "text", "", [], true);
            //add_Preset_Qus_Click("身份證字號", "英文字母請大寫", 1, "text", "idNumber", [], true);
            //add_Preset_Qus_Click("信箱Email", "請填寫正確格式", 1, "text", "email", [], true);
            //add_Preset_Qus_Click("連絡電話", "可填手機或是家裡電話", 1, "text", "", [], true);
            //add_Preset_Qus_Click("聯絡地址", "請填寫您收的到信的地址", 1, "text", "", [], true);
            //add_Preset_Qus_Click("公司電話", null, 1, "text", "", [], false);
            //add_Preset_Qus_Click("傳真", null, 1, "text", "", [], false);
            add_Preset_Qus_Click("用餐", null, 2, "singleSelect", "", ["葷", "素", "不用餐"], true);
            //add_Preset_Qus_Click("備註", "若您有其他的問題,可以在此說明 ", 2, "text", "", [], false);

            //活動名稱判斷是否填寫
            $("#activity_Name_txt").blur(function () {
                if ($.trim($(this).val()) == "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#activity_Name_txt_error").length == 0)
                        $(this).after('<em id="activity_Name_txt_error" class="error help-block red" style="width: auto;margin-bottom: 5px;">必須填寫</em>');
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#activity_Name_txt_error").remove();
                }
            })
            $("#block_title_txt_1").blur(function () {
                if ($.trim($(this).val()) == "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#block_title_txt_error_1").length == 0)
                        $(this).after('<em id="block_title_txt_error_1" class="error help-block red" style="width: auto;margin-bottom: 5px;">必須填寫</em>');
                    check_Activity_Column_Data = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#block_title_txt_error_1").remove();
                }
            })
            $("#block_title_txt_2").blur(function () {
                if ($.trim($(this).val()) == "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#block_title_txt_error_2").length == 0)
                        $(this).after('<em id="block_title_txt_error_2" class="error help-block red" style="width: auto;margin-bottom: 5px;">必須填寫</em>');
                    check_Activity_Column_Data = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#block_title_txt_error_2").remove();
                }
            })
        });
        //#endregion

        //#region 只能輸入數字判斷
        function ValidateNumber(e, pnumber) {
            if (!/^\d+$/.test(pnumber)) {
                $(e).val(/^\d+/.exec($(e).val()));
            }
            return false;
        }
        //#endregion

        //#region 切換Tabs顯示跟隱藏按鈕
        function activityMenu() {
            $("#add_qus_btn").css({ "display": "none" });
            $("#add_block_btn").css({ "display": "none" });
            $("#add_usually_btn").css({ "display": "none" });
        }
        function activityQus() {
            $("#add_qus_btn").css({ "display": "" });
            $("#add_block_btn").css({ "display": "" });
            $("#add_usually_btn").css({ "display": "" });
        }
        //#endregion

        //#region 獲取分類資料
        function getClass() {
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為getActivityAllList的function
                url: 'S02010201.aspx/getClassList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                //成功時
                success: function (result) {
                    //設定搜尋下拉選單內容
                    setSelect(result.d);
                },
                //失敗時
                error: function () {
                    return false;
                }
            });
        }
        //#endregion

        //#region 設定分類下拉選單內容
        function setSelect(act_classInfo) {
            var act_classInfo = JSON.parse(act_classInfo);
            $("#select_class").children().remove();
            $("#select_class").append('<option value="0">請選擇分類</option>');
            for (var count = 0 ; count < act_classInfo.length ; count++) {
                $("#select_class").append('<option value="' + act_classInfo[count].Ac_idn + '">' + act_classInfo[count].Ac_title + '</option>');
            }
            $("#select_class").val('0');

            //判斷分類必選
            $("#select_class").blur(function () {
                if ($(this).val() == 0) {
                    $("#select_class").css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#select_class_txt_error").length == 0)
                        $("#select_class").after('<em id="select_class_txt_error" class="error help-block red" style="margin-top: -10px;">必須選擇</em>');
                    check_Activity_Data = false;
                }
                else {
                    $("#select_class").css({ "box-shadow": "" });
                    $("#select_class_txt_error").remove();
                }

            })
        }
        //#endregion

        //#region 新增常用欄位
        function add_Usually_Column() {
            $("input[name='check_usually_Column']").each(function () {
                if ($(this).attr('checked')) {
                    switch ($(this).val()) {
                        case "姓名":
                            add_Preset_Qus_Click("姓名", "請填寫完整名字", null, "text", "", [], true);
                            break;
                        case "身份證字號":
                            add_Preset_Qus_Click("身份證字號", "英文字母請大寫", null, "text", "idNumber", [], true);
                            break;
                        case "出生年月日":
                            add_Preset_Qus_Click("出生年月日", "ex:83/07/08", null, "text", "", [], true);
                            break;
                        case "服務單位":
                            add_Preset_Qus_Click("服務單位", "請填寫完整名稱", null, "text", "", [], true);
                            break;
                        case "信箱Email":
                            add_Preset_Qus_Click("信箱Email", "請填寫正確格式", null, "text", "email", [], true);
                            break;
                        case "聯絡電話":
                            add_Preset_Qus_Click("聯絡電話", "可填手機或是家裡電話", null, "text", "", [], true);
                            break;
                        case "公司電話":
                            add_Preset_Qus_Click("公司電話", null, null, "text", "", [], false);
                            break;
                        case "傳真":
                            add_Preset_Qus_Click("傳真", null, null, "text", "", [], false);
                            break;
                        case "用餐":
                            add_Preset_Qus_Click("用餐", null, null, "singleSelect", "", ["葷", "素", "不用餐"], true);
                            break;
                        case "聯絡地址":
                            add_Preset_Qus_Click("聯絡地址", "請填寫您收的到信的地址", null, "text", "", [], true);
                            break;
                        case "職稱":
                            add_Preset_Qus_Click("職稱", null, null, "text", "", [], true);
                            break;
                        case "備註":
                            add_Preset_Qus_Click("備註", "若您有其他的問題,可以在此說明 ", null, "text", "", [], false);
                            break;
                    }
                    //alert($(this).val());
                    $(this).prop("checked", false);
                }
            });
        }
        //#endregion

        //#region 新增問題分發
        function add_Preset_Qus_Click(qus_Title, qus_Desc, add_Block, preset_Qus_Way, preset_Qus_validation, qus_Option, required) {
            add_Qus_Click(add_Block, required);
            //增加問題內容 change_Qus_Way(問題模式,欲加入題目名稱的地方,欲加入選項的地方,欲加入題目名稱的內容,欲加入選項的第一個內容)
            change_Qus_Way(preset_Qus_Way, qusId, $("#change_Qus_Way_div_" + qusId), $("#change_Qus_Content_div_" + qusId), qus_Title, qus_Desc, qus_Option[0]);
            //將選單改為預設所選的
            $("#select_" + qusId).val(preset_Qus_Way);
            $("#select_Validation_" + qusId).val(preset_Qus_validation);
            //將預設文字加入選項中
            if (preset_Qus_Way != "text") {
                var qus_Option_length = qus_Option.length;
                for (var count = 1; count < qus_Option_length; count++) {
                    add_Qus_Options_Click(qusId, qus_Option[count]);
                }
            }
            //$('#qus_txt_' + qusId).focus();
            //產生完問題之後問題ID加一
            qusId++;
            $(".column").sortable({
                connectWith: ".column",
                handle: ".portlet-header",
                cancel: ".portlet-toggle",
                placeholder: "portlet-placeholder ui-corner-all",
                cancel: ".portlet-state-disabled",
                axis: "y",
                cursor: 'move',
                opacity: 0.6,//拖動時透明度 
                //revert: true,
                start: function (event, ui) {
                    var start_pos = ui.item.index();
                    ui.item.data('start_pos', start_pos);
                },
                update: function (event, ui) {
                }
            });
            $(".portlet")
                .addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
                .find(".portlet-header")
                .addClass("ui-widget-header ui-corner-all");
            //.prepend( "<span class='ui-icon ui-icon-minusthick portlet-toggle'></span>");

            $(".portlet-toggle").click(function () {
                var icon = $(this);
                icon.toggleClass("ui-icon-minusthick ui-icon-plusthick");
                icon.closest(".portlet").find(".portlet-content").toggle();
            });
        };
        //#endregion

        //#region 新增問題
        function add_Qus_Click(add_blockId, required) {
            //判斷是仔入預設問題還是新增問題
            if (add_blockId === null) {
                //判斷區塊是否存在
                var chooseId = blockId - 1;
                var $check_block_exit = $("#block_div_" + (chooseId));
                while ($check_block_exit.length === 0) {
                    chooseId--;
                    $check_block_exit = $("#block_div_" + chooseId);
                };
            }
            else
                var chooseId = add_blockId;
            //判斷預設是否必填
            if (required == true)
                var check = "checked";
            else
                var check = "";
            //新增問題至最後一個區塊內(預設文字問題)
            $('#add_Qus_div_' + chooseId).append('<div id="qus_div_' + qusId + '" class="form-group portlet showback" style="border: 0px; background: #ffffff;padding: 15px;margin-bottom: 15px;box-shadow: 0px 0px 2px #272727;border-radius: 20px;margin: 20px 0px 0px 1px;">' +
                                        '<div class="col-sm-1 portlet-header center" style="background-color: #F1F2F7;height:100px; width:35px">' +
                                            '<img src="../Images/drag_pic.jpg" alt="拖移" height="24px" style="transform: translateY(-50%);top: 50%;position: relative;"/> ' +
                                        '</div>' +
                                        '<div class="row">' +
                                            //添加題目問題以及題目描述的地方
                                            '<div class="col-sm-10" id="change_Qus_Way_div_' + qusId + '">' +
                                            '</div>' +
                                            '<div class="col-sm-1"  style="padding: inherit;">' +
                                                //題目模式下拉式選單
                                                '<select class="select" id="select_' + qusId + '" style="width: 56px; height: 34px; border-radius: 4px;float: right;">' +
                                                    '<option value="singleSelect">單選</option>' +
                                                    '<option value="multiSelect">多選</option>' +
                                                    '<option selected="selected" value="text">文字</option>' +
                                                    '<option value="dropDownList">選單</option>' +
                                                '</select>' +
                                            '</div>' +
                                        '</div>' +
                                        '<div class="row">' +
                                            //如果是單選、多選、選單則在此處添加選項
                                            '<div class="col-sm-6"  id="change_Qus_Content_div_' + qusId + '">' +
                                            '</div>' +
                                                '<div >' +
                                                    '<div style="float: right;">' +
                                                        '<div class="checkbox checkbox-slider--b-flat checkbox-slider-md" style="float: left; padding-right: 10px;">' +
                                                            '<label>' +
                                                            '<input id="required_checkbox_' + qusId + '" type="checkbox" ' + check + '="' + check + '"><span><a style="font-size:16px;margin-left: -126px;">必填</a></span>' +
                                                            '</label>' +
                                                        '</div>' +
                                                        //如欲添加更多資料驗證格式這裡添加
                                                        '<select class="select_Validation" id="select_Validation_' + qusId + '" style="width:100px;height:34px;border-radius:4px;margin-left: 22px;margin-top: 5px;">' +
                                                              '<option selected="selected" value="">資料驗證(無)</option>' +
                                                              '<option value="cellPhone">手機號碼</option>' +
                                                              '<option value="email">電子信箱</option>' +
                                                              '<option value="idNumber">身份證</option>' +
                                                              '<option value="int">數字</option>' +
                                                              '<option value="length">字數</option>' +
                                                            '</select>' +
                                                        '<a onclick="del_Qus_click(' + qusId + ')" type="submit" class="btn btn-theme" style="margin-left: 5px;">刪除</a>' +
                                                    '</div>' +
                                                    //添加資料驗證數字最大最小值的地方
                                                    '<div  id="add_Validation_div_' + qusId + '" style="margin-right: 52px;float: right;">' +
                                                    '</div>' +
                                                '</div>' +
                                        '</div>' +
                                '</div>');
            //資料驗證選單
            $('#select_Validation_' + qusId).change(function () {
                //抓取現在要更改問題模式的ID
                var str = $(this).attr("id");
                //切割字串抓最後一個陣列即為ID
                var chooseId = str.split("_")[str.split("_").length - 1];
                //儲存欲更改的問題模式
                var qus_Way = $("#select_Validation_" + chooseId).val();
                //判斷如果資料驗證不為數字則要把最大最小值區塊刪掉
                if (qus_Way != "int" || qus_Way != "length")
                    $("#add_Validation_div_" + chooseId).children().remove();
                switch (qus_Way) {
                    case "int":
                        $("#add_Validation_div_" + chooseId).append('<label class="col-sm-1 control-label" style="width: auto;margin-top: 6px;">最小值</label>' +
                                                                    '<div class="col-sm-1" style="width: auto;margin-top: 6px;">' +
                                                                        '<input type="text" id="min_Num_' + chooseId + '" class="form-control" style="width: 85px;margin-bottom: 8px;margin-left: -15px" placeholder="可不填" maxlength="9" onkeyup="return ValidateNumber($(this),value)"/>' +
                                                                    '</div>' +
                                                                    '<label class="col-sm-1 control-label" style="width: auto;margin-top: 6px;">最大值</label>' +
                                                                    '<div class="col-sm-1" style="width: auto;margin-top: 6px;">' +
                                                                        '<input type="text" id="max_Num_' + chooseId + '" class="form-control" style="width: 85px;margin-bottom: 8px;margin-left: -15px" placeholder="可不填" maxlength="9" onkeyup="return ValidateNumber($(this),value)"/>' +
                                                                    '</div>');
                        break;
                    case "length":
                        $("#add_Validation_div_" + chooseId).append('<label class="col-sm-1 control-label" style="width: auto;margin-top: 6px;">最小值</label>' +
                                                                    '<div class="col-sm-1" style="width: auto;margin-top: 6px;">' +
                                                                        '<input type="text" id="min_Num_' + chooseId + '" class="form-control" style="width: 85px;margin-bottom: 8px;margin-left: -15px" placeholder="可不填" maxlength="9" onkeyup="return ValidateNumber($(this),value)"/>' +
                                                                    '</div>' +
                                                                    '<label class="col-sm-1 control-label" style="width: auto;margin-top: 6px;">最大值</label>' +
                                                                    '<div class="col-sm-1" style="width: auto;margin-top: 6px;">' +
                                                                        '<input type="text" id="max_Num_' + chooseId + '" class="form-control" style="width: 85px;margin-bottom: 8px;margin-left: -15px" placeholder="可不填" maxlength="9" onkeyup="return ValidateNumber($(this),value)"/>' +
                                                                    '</div>');
                        break;
                    default:
                        break;
                }
                var if_int = /^[0-9]*[1-9][0-9]*$/;

                $("#max_Num_" + chooseId).blur(function () {
                    if (isNaN($.trim($(this).val()))) {
                        $(this).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#max_Num_error_" + chooseId).length == 0)
                            $(this).after('<em id="max_Num_error_' + chooseId + '" class="error help-block red" style="width: auto;margin-bottom: 5px;">只能輸入數字</em>');
                        check_Activity_Column_Data = false;
                    }
                    else {
                        $("#max_Num_error_" + chooseId).remove();
                        if ($("#max_Num_error_" + chooseId).length == 0 && $("#max_Num_withmin_error_" + chooseId).length == 0)
                            $(this).css({ "box-shadow": "" });
                    }

                    if ($.trim($(this).val()) != "" && if_int.test($.trim($(this).val())) && $.trim($("#min_Num_" + chooseId).val()) != "" && if_int.test($.trim($("#min_Num_" + chooseId).val())) && parseInt($.trim($(this).val())) < parseInt($.trim($("#min_Num_" + chooseId).val()))) {
                        $(this).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#max_Num_withmin_error_" + chooseId).length == 0)
                            $(this).after('<em id="max_Num_withmin_error_' + chooseId + '" class="error help-block red" style="width: auto;margin-bottom: 5px;">最大值需大於最小值</em>');
                        check_Activity_Column_Data = false;
                    }
                    else {
                        $("#max_Num_withmin_error_" + chooseId).remove();
                        $("#min_Num_withmin_error_" + chooseId).remove();
                        if ($("#max_Num_error_" + chooseId).length == 0 && $("#max_Num_withmin_error_" + chooseId).length == 0)
                            $(this).css({ "box-shadow": "" });
                        if ($("#min_Num_error_" + chooseId).length == 0 && $("#min_Num_withmin_error_" + chooseId).length == 0)
                            $("#min_Num_" + chooseId).css({ "box-shadow": "" });
                    }

                })
                $("#min_Num_" + chooseId).blur(function () {
                    if (isNaN($.trim($(this).val()))) {
                        $(this).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#min_Num_error_" + chooseId).length == 0)
                            $(this).after('<em id="min_Num_error_' + chooseId + '" class="error help-block red" style="width: auto;margin-bottom: 5px;">只能輸入數字</em>');
                        check_Activity_Column_Data = false;
                    }
                    else {
                        $(this).css({ "box-shadow": "" });
                        $("#min_Num_error_" + chooseId).remove();
                    }

                    if ($.trim($(this).val()) != "" && if_int.test($.trim($(this).val())) && $.trim($("#max_Num_" + chooseId).val()) != "" && if_int.test($.trim($("#max_Num_" + chooseId).val())) && parseInt($.trim($(this).val())) > parseInt($.trim($("#max_Num_" + chooseId).val()))) {
                        $(this).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#min_Num_withmin_error_" + chooseId).length == 0)
                            $(this).after('<em id="min_Num_withmin_error_' + chooseId + '" class="error help-block red" style="width: auto;margin-bottom: 5px;">最小值需小於最大值</em>');
                        check_Activity_Column_Data = false;
                    }
                    else {
                        $("#max_Num_withmin_error_" + chooseId).remove();
                        $("#min_Num_withmin_error_" + chooseId).remove();
                        if ($("#min_Num_error_" + chooseId).length == 0 && $("#min_Num_withmin_error_" + chooseId).length == 0)
                            $(this).css({ "box-shadow": "" });
                        if ($("#max_Num_error_" + chooseId).length == 0 && $("#max_Num_withmin_error_" + chooseId).length == 0)
                            $("#max_Num_" + chooseId).css({ "box-shadow": "" });
                    }
                })
            });


            //題目模式選單
            $('#select_' + qusId).change(function () {
                //抓取現在要更改問題模式的ID
                var str = $(this).attr("id");
                //切割字串抓最後一個陣列即為ID
                var chooseId = str.split("_")[str.split("_").length - 1];
                //儲存欲更改的問題模式
                var qus_Way = $(this).find(':selected').val();
                //抓到要新增問題、描述以及新增問題的區塊
                var temp_change_Qus_Way_div = $("#change_Qus_Way_div_" + chooseId);
                var temp_change_Qus_Content_div = $("#change_Qus_Content_div_" + chooseId);
                switch (qus_Way) {
                    //單選問題
                    case "singleSelect":
                        change_Qus_Way("singleSelect", chooseId, temp_change_Qus_Way_div, temp_change_Qus_Content_div, $("#qus_txt_" + chooseId).val(), $("#qus_context_txt_" + chooseId).val(), null);
                        break;
                        //多選問題
                    case "multiSelect":
                        change_Qus_Way("multiSelect", chooseId, temp_change_Qus_Way_div, temp_change_Qus_Content_div, $("#qus_txt_" + chooseId).val(), $("#qus_context_txt_" + chooseId).val(), null);
                        break;
                        //文字
                    case "text":
                        change_Qus_Way("text", chooseId, temp_change_Qus_Way_div, temp_change_Qus_Content_div, $("#qus_txt_" + chooseId).val(), $("#qus_context_txt_" + chooseId).val(), null);
                        break;
                        //選單
                    case "dropDownList":
                        change_Qus_Way("dropDownList", chooseId, temp_change_Qus_Way_div, temp_change_Qus_Content_div, $("#qus_txt_" + chooseId).val(), $("#qus_context_txt_" + chooseId).val(), null);
                        break;
                    default:
                        break;
                }
            });
        }
        //刪除問題
        function del_Qus_click(chooseId) {
            $('#qus_div_' + chooseId).remove();
        };
        //#endregion

        //#region 更改問題模式 change_Qus_Way(問題模式 , 選的ID , 欲加入題目名稱的地方 , 欲加入選項的地方 , 欲加入題目名稱的內容 , 欲加入的問題描述 , 欲加入選項的第一個內容)
        function change_Qus_Way(qus_way_int, chooseId, temp_change_Qus_Way_div, temp_change_Qus_Content_div, qus_Title_Value, qus_Desc, present_Qus_Option) {
            //判斷題目模式
            if (qus_way_int == "singleSelect")
                var qus_title = "單選標題";
            else if (qus_way_int == "multiSelect")
                var qus_title = "多選標題";
            else if (qus_way_int == "text")
                var qus_title = "文字標題";
            else if (qus_way_int == "dropDownList")
                var qus_title = "選單標題";
            //判斷是否有填入的題目名稱
            if (qus_Title_Value == null)
                var title_Value = "";
            else
                var title_Value = "value";
            //判斷是否有填入的問題描述
            if (qus_Desc == null)
                var qus_Desc_Value = "";
            else
                var qus_Desc_Value = "value";
            //判斷是否有填入的問題選項
            if (present_Qus_Option == null) {
                var option_Value = "";
                present_Qus_Option = "";
            }
            else
                var option_Value = "value";
            //判斷如果是選、多選、選單則要禁用資料驗證
            if (qus_way_int == "singleSelect" || qus_way_int == "multiSelect" || qus_way_int == "dropDownList") {
                $('#select_Validation_' + chooseId).attr('disabled', true);
                $('#select_Validation_' + chooseId).val("");
                $("#select_Validation_" + chooseId).css({ "background-color": "#EEEEEE" });
                //判斷原來是否存在最大最小值的DIV，如果存在要刪除
                if ($("#add_Validation_div_" + chooseId).children().length > 0)
                    $("#add_Validation_div_" + chooseId).children().remove();
            }
            else {
                $('#select_Validation_' + chooseId).attr('disabled', false);
                $("#select_Validation_" + chooseId).css({ "background-color": "" });
            }

            //移除選擇問題類型區塊內容
            temp_change_Qus_Way_div.children().remove();
            //移除問題內容
            temp_change_Qus_Content_div.children().remove();
            //將問題名稱選項改為單選、多選、選單問題
            temp_change_Qus_Way_div.append('<div class="col-sm-10" style="width: 100%">' +
                                                '<input type="text" ID="qus_txt_' + chooseId + '"  placeholder="' + qus_title + '" class="form-control" style="width: 100%;" maxlength="60" ' + title_Value + '="' + qus_Title_Value + '"  />' +
                                            '</div>' +
                                            '<div class="col-sm-10" style="width: 100%">' +
                                                '<input type="text" ID="qus_context_txt_' + chooseId + '" ' + qus_Desc_Value + ' = "' + qus_Desc + '" placeholder="問題描述" class="form-control" style="width: 100%;margin-top: 15px" maxlength="255" />' +
                                            '</div>');

            //將問題地方改單選、多選、選單問題，如果為文字問題則不用加
            if (qus_way_int != "text") {
                temp_change_Qus_Content_div.append('<div class="panel-group" id="panel_group_' + chooseId + '" role="tablist" aria-multiselectable="true" style="width: 350px;margin-left: 50px;margin-top: 5px;">' +
                                                  '<div class="panel panel-default">' +
                                                    '<div class="panel-heading" role="tab" id="heading_' + chooseId + '">' +
                                                      '<h4 class="panel-title">' +
                                                        '<a data-toggle="collapse" data-parent="#panel_group_' + chooseId + '" href="#collapse_' + chooseId + '" aria-expanded="true" aria-controls="collapse_' + chooseId + '" style="color:#2a6496;">' +
                                                          '' + qus_title.split('')[0] + qus_title.split('')[1] + '選項' +
                                                        '</a>' +
                                                      '</h4>' +
                                                    '</div>' +
                                                    '<div id="collapse_' + chooseId + '" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="heading_' + chooseId + '">' +
                                                      '<div class="panel-body">' +
                                                        '<div class="col-sm-11" id="add_Qus_Options_div_' + chooseId + '">' +
                                                            '<div class="col-sm-4" >' +
                                                                    '<input id="qus_Option_' + chooseId + '1" name="qus_Options" type="text" ' + option_Value + '="' + present_Qus_Option + '" class="form-control" style="margin-top: 8px;margin-bottom: 8px;width: 244px;" maxlength="30">' +
                                                            '</div>' +
                                                            '<div class="col-sm-11 "  id="newOption_' + chooseId + '" style="margin-bottom: 8px;">' +
                                                                    '<a onclick="add_Qus_Options_Click(' + chooseId + ',' + null + ')">新增選項</a>' +
                                                            '</div>' +
                                                        '</div>' +
                                                      '</div>' +
                                                    '</div>' +
                                                  '</div>' +
                                                '</div>');
            }
            $("#qus_txt_" + chooseId).focus();
            //當焦點離開時判斷如果沒有填寫則警號
            $("#qus_txt_" + chooseId).blur(function () {
                if ($.trim($(this).val()) == "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red", "margin-bottom": "0px" });
                    if ($("#qus_txt_error_" + chooseId).length == 0)
                        $(this).after('<em id="qus_txt_error_' + chooseId + '" class="error help-block red">必須填寫</em>');
                    check_Activity_Column_Data = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#qus_txt_error_" + chooseId).remove();
                }

            })
            //當焦點離開時判斷如果沒有填寫則警號
            $("#qus_Option_" + chooseId + "1").blur(function () {
                if ($.trim($(this).val()) == "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#qus_Option_error_" + chooseId + "1").length == 0)
                        $(this).after('<em id="qus_Option_error_' + chooseId + '1" class="error help-block red" style="width: auto;margin-left: 16px;margin-bottom: 5px;">必須填寫</em>');
                    check_Activity_Column_Data = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#qus_Option_error_" + chooseId + 1).remove();
                }

            })
        }
        //#endregion

        //#region 新增單多選選項 add_Qus_Options_Click(加入選項區塊的ID , 加入的文字)
        function add_Qus_Options_Click(id, option_value) {
            //先將新增選項這個事件移除
            $('#newOption_' + id).remove();
            //計算目前裡面有多少問題數量
            var count = $('#add_Qus_Options_div_' + id).children().length;
            if ($.trim(option_value) == "") {
                var value = "";
                option_value = "";
            }
            else
                var value = "value";
            //將選項加入預設好的div裡面
            $('#add_Qus_Options_div_' + id).append('<div class="col-sm-11 " id="del_Qus_Options_' + id + (count) + '">' +
                                             '<div class="col-sm-4" >' +
                                                '<input id="qus_Option_' + id + (count + 1) + '" name="qus_Options" type="text" class="form-control" style="width: 244px;margin-bottom: 8px;margin-left: -15px" ' + value + '="' + option_value + '" maxlength="30"/>' +
                                            '</div>' +
                                            '<div class="col-sm-2 col-sm-push-3" style="margin-top:10px;margin-bottom: 5px;margin-left: 100px;">' +
                                             '<a onclick="del_Qus_Options_Click(' + id + (count) + ')">X</a>' +
                                             '</div>' +
                                    '</div>' +

                                    '<div class="col-sm-11" id="newOption_' + id + '" style="margin-bottom: 8px;">' +
                                        '<a onclick="add_Qus_Options_Click(' + id + ',' + null + ')">新增選項</a>' +
                                    '</div>');
            $("#qus_Option_" + id + (count + 1)).focus();
            //當焦點離開時判斷如果沒有填寫則警號
            $("#qus_Option_" + id + (count + 1)).blur(function () {
                if ($.trim($(this).val()) == "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#qus_Option_error_" + id + (count + 1)).length == 0)
                        $(this).after('<em id="qus_Option_error_' + id + (count + 1) + '" class="error help-block red" style="width: 150px;margin-bottom: 5px;">必須填寫</em>');
                    check_Activity_Column_Data = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#qus_Option_error_" + id + (count + 1)).remove();
                }
            })
        };
        //刪除單多選選項
        function del_Qus_Options_Click(id) {
            $("#del_Qus_Options_" + id).remove();
        }
        //#endregion

        //#region 新增區塊
        function add_Block_Click() {
            //新增區塊至預設好的div
            $('#add_Block_div').append('<div class="col-lg-11 col-md-11 col-sm-11 col-xs-12 dexc showback" id="block_div_' + blockId + '">' +
                                            '<div class="col-sm-1" style="left: 95%;height: 40px;">' +
                                                '<a class="btn" style="color: #768094;padding-left: 3px;" onclick="del_block_click(' + blockId + ')">X</a>' +
                                            '</div>' +
                                                '<h3><input type="text" id ="block_title_txt_' + blockId + '" class="form-control" placeholder="區塊名稱" maxlength="60" /></h3>' +
                                                '<input type="text" id="block_Description_txt_' + blockId + '" class="form-control" placeholder="區塊描述" />' +
                                                '<div class="form-horizontal style-form column" id="add_Qus_div_' + blockId + '" style="min-height:50px">' +
                                                '</div>' +
                                            '</div>');
            $("#block_title_txt_" + blockId).focus();
            //當焦點離開時判斷如果沒有填寫則警號
            $("#block_title_txt_" + blockId).blur(function () {
                var blId = $(this).attr("id").split('_')[$(this).attr("id").split('_').length - 1]
                if ($.trim($(this).val()) == "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#block_title_txt_error_" + blId).length == 0)
                        $(this).after('<em id="block_title_txt_error_' + blId + '" class="error help-block red" style="width: auto;margin-bottom: 5px;">必須填寫</em>');
                    check_Activity_Column_Data = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#block_title_txt_error_" + blId).remove();
                }
            })
            //區塊ID加一
            blockId++;
            //將拖拉function再次呼叫，不然新增出來的區塊不能進行拖拉處理
            $(".column").sortable({
                connectWith: ".column",
                handle: ".portlet-header",
                cancel: ".portlet-toggle",
                placeholder: "portlet-placeholder ui-corner-all",
                cancel: ".portlet-state-disabled",
                axis: "y",
                cursor: 'move',
                //containment: '#add_Block_div',
                opacity: 0.6,//拖動時透明度  
                //revert: true,
                start: function (event, ui) {
                    var start_pos = ui.item.index();
                    ui.item.data('start_pos', start_pos);
                },

            });
            $(".portlet")
                .addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
                .find(".portlet-header")
                .addClass("ui-widget-header ui-corner-all");
            //.prepend( "<span class='ui-icon ui-icon-minusthick portlet-toggle'></span>");

            $(".portlet-toggle").click(function () {
                var icon = $(this);
                icon.toggleClass("ui-icon-minusthick ui-icon-plusthick");
                icon.closest(".portlet").find(".portlet-content").toggle();
            });
        };
        //刪除區塊
        function del_block_click(chooseId) {
            $('#block_div_' + chooseId).remove();
        };
        //#endregion

        //#region 新增場次
        function add_Session_click() {
            if (sessionId == 1)
                var display = "display:none";
            else
                var display = "";
            //講場次新增至預設好的div裡面
            $('#add_Session_div').append('<div class="showback" id="delete_Session_div_' + sessionId + '">' +
                        '<div class="form-horizontal style-form">' +

                            '<div class="form-group">' +
                            '<div class="col-sm-3">' +
                            '<h4 class="red">*為必填</h4>' +
                            '</div>' +
                                '<div class="col-sm-1" style="left: 68%;height: 40px;' + display + ';" >' +
                                    '<a class="btn" style="color: #768094;" onclick="del_Session_click(' + sessionId + ')">X</a>' +
                                '</div>' +
                            '</div>' +
                            '<div class="form-group">' +
                                '<label class="col-sm-2 control-label">場次名稱<b class="red">*</b></label>' +
                                '<div class="col-sm-10">' +
                                    '<input type="text" class="form-control" id="session_Name_txt_' + sessionId + '" maxlength="60" />' +
                                '</div>' +
                            '</div>' +
                            '<div class="form-group">' +
                                '<label class="col-sm-2 control-label">活動開始日期<b class="red">*</b></label>' +
                                '<div class="col-sm-4">' +
                                    '<input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_Start_txt_' + sessionId + '" readonly="true" style="background-color: white;cursor: auto;"/>' +
                                '</div>' +
                                '<label class="col-sm-2 control-label">活動結束日期<b class="red">*</b></label>' +
                                '<div class="col-sm-4">' +
                                    '<input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_End_txt_' + sessionId + '" readonly="true" style="background-color: white;cursor: auto;"/>' +
                                '</div>' +
                            '</div>' +
                            '<div class="form-group">' +
                                '<label class="col-sm-2 control-label">報名開始日期<b class="red">*</b></label>' +
                                '<div class="col-sm-4">' +
                                    '<input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_Sign_Start_txt_' + sessionId + '" readonly="true" style="background-color: white;cursor: auto;"/>' +
                                '</div>' +
                                '<label class="col-sm-2 control-label">報名結束日期<b class="red">*</b></label>' +
                                '<div class="col-sm-4">' +
                                    '<input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_Sign_End_txt_' + sessionId + '" readonly="true" style="background-color: white;cursor: auto;"/>' +
                                '</div>' +
                            '</div>' +
                            '<div class="form-group">' +
                                '<label class="col-sm-2 control-label">活動地點<b class="red">*</b></label>' +
                                '<div class="col-sm-10">' +
                                    '<input type="text" class="form-control" id="activity_Location_txt_' + sessionId + '" maxlength="30">' +
                                '</div>' +
                            '</div>' +
                            '<div class="form-group">' +
                                '<label class="col-sm-2 control-label">人數限制<b class="red">*</b></label>' +
                                '<div class="col-sm-10">' +
                                    '<input type="text" class="form-control" placeholder="請填寫數字" id="activity_Limit_Num_txt_' + sessionId + '" onkeyup="return ValidateNumber($(this),value)">' +
                                '</div>' +
                            '</div>' +
                            '<div class="form-group">' +
                                '<label class="col-sm-2 control-label">備註</label>' +
                                '<div class="col-sm-10">' +
                                    '<input type="text" class="form-control" id="activity_remark_txt_' + sessionId + '" maxlength="100">' +
                                '</div>' +
                            '</div>' +
                            '<div class="form-group" style="padding-right: 10px;">' +
                                '<a style="float: right;" class="btn btn-theme" onclick="add_Session_click()">增加場次</a>' +
                            '</div>' +
                        '</div>' +
                    '</div>');
            //呼叫日期時間選擇器
            $(function () {
                $('.datetimepicker').datetimepicker({
                    lang: 'ch'
                });
            });
            //場次名稱是否填寫的判斷
            $("#session_Name_txt_" + sessionId).blur(function () {
                var choose_Session_Name_temp = $(this).attr("id");
                var choose_Session_Name_txtId = choose_Session_Name_temp.split("_")[choose_Session_Name_temp.split("_").length - 1];
                if ($.trim($(this).val()) == "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#session_Name_txt_error_" + choose_Session_Name_txtId).length == 0)
                        $(this).after('<em id="session_Name_txt_error_' + choose_Session_Name_txtId + '" class="error help-block red" style="width: auto;margin-bottom: 5px;">必須填寫</em>');
                    check_Activity_Data = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#session_Name_txt_error_" + choose_Session_Name_txtId).remove();
                }
            })
            //活動地點
            $("#activity_Location_txt_" + sessionId).blur(function () {
                var activity_Location_txt_temp = $(this).attr("id");
                var activity_Location_txtId = activity_Location_txt_temp.split("_")[activity_Location_txt_temp.split("_").length - 1];
                if ($.trim($(this).val()) == "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#activity_Location_txt_error_" + activity_Location_txtId).length == 0)
                        $(this).after('<em id="activity_Location_txt_error_' + activity_Location_txtId + '" class="error help-block red" style="width: auto;margin-bottom: 5px;">必須填寫</em>');
                    check_Activity_Data = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#activity_Location_txt_error_" + activity_Location_txtId).remove();
                }
            })
            //人數限制
            $("#activity_Limit_Num_txt_" + sessionId).blur(function () {
                var if_int = /^[0-9]*[1-9][0-9]*$/;
                var activity_Limit_Num_txt_temp = $(this).attr("id");
                var activity_Limit_Num_txtId = activity_Limit_Num_txt_temp.split("_")[activity_Limit_Num_txt_temp.split("_").length - 1];
                if ($.trim($(this).val()) == "" || isNaN($.trim($(this).val())) || !if_int.test($.trim($(this).val())) || parseInt($.trim($(this).val())) > 100000) {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });

                    if ($("#activity_Limit_Num_txt_error_" + activity_Limit_Num_txtId).length == 0)
                        $(this).after('<em id="activity_Limit_Num_txt_error_' + activity_Limit_Num_txtId + '" class="error help-block red" style="width: auto;margin-bottom: 5px;">必須填寫整數數字</em>');
                    if (parseInt($.trim($(this).val())) > 100000)
                        $("#activity_Limit_Num_txt_error_" + activity_Limit_Num_txtId).text("請輸入小於100000的數字!!");
                    check_Activity_Data = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#activity_Limit_Num_txt_error_" + activity_Limit_Num_txtId).remove();
                }
            })
            //活動開始日期
            $("#datetimepicker_Activity_Start_txt_" + sessionId).blur(function () {
                //取得ID
                var datetimepicker_Activity_Start_txt_temp = $(this).attr("id");
                var datetimepicker_Activity_Start_txtId = datetimepicker_Activity_Start_txt_temp.split("_")[datetimepicker_Activity_Start_txt_temp.split("_").length - 1];
                //判斷在活動結束日期之前
                if ($.trim($(this).val()) >= $.trim($("#datetimepicker_Activity_End_txt_" + datetimepicker_Activity_Start_txtId).val()) && $.trim($("#datetimepicker_Activity_End_txt_" + datetimepicker_Activity_Start_txtId).val()) != "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#datetimepicker_Activity_Start_txt_error_" + datetimepicker_Activity_Start_txtId).length == 0)
                        $(this).after('<em id="datetimepicker_Activity_Start_txt_error_' + datetimepicker_Activity_Start_txtId + '" class="error help-block red" style="width: auto;margin-bottom: 5px;">活動開始日期必須在活動結束日期之前</em>');
                    check_Activity_Data = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_Start_txt_error_" + datetimepicker_Activity_Start_txtId).remove();
                    if ($("#datetimepicker_Activity_Start_txt_error_" + datetimepicker_Activity_Start_txtId).length == 0 && $("#datetimepicker_Activity_End_txt_error_withSignEnd_" + datetimepicker_Activity_Start_txtId).length == 0)
                        $("#datetimepicker_Activity_End_txt_" + datetimepicker_Activity_Start_txtId).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_End_txt_error_" + datetimepicker_Activity_Start_txtId).remove();
                }
                //判斷在活動報名開始日期之後
                if ($.trim($(this).val()) <= $.trim($("#datetimepicker_Activity_Sign_Start_txt_" + datetimepicker_Activity_Start_txtId).val()) && $.trim($("#datetimepicker_Activity_Sign_Start_txt_" + datetimepicker_Activity_Start_txtId).val()) != "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#datetimepicker_Activity_Start_txt_error_WithSignStart_" + datetimepicker_Activity_Start_txtId).length == 0)
                        $(this).after('<em id="datetimepicker_Activity_Start_txt_error_WithSignStart_' + datetimepicker_Activity_Start_txtId + '" class="error help-block red" style="width: 222px;margin-bottom: 5px;">活動開始日期必須在報名開始日期之後</em>');
                    check_Activity_Data = false;
                }
                else {
                    if ($("#datetimepicker_Activity_Start_txt_error_WithSignStart" + datetimepicker_Activity_Start_txtId).length == 0 && $("#datetimepicker_Activity_Start_txt_error_" + datetimepicker_Activity_Start_txtId).length == 0 && $.trim($("#datetimepicker_Activity_Sign_Start_txt_" + datetimepicker_Activity_Start_txtId).val()) != "" && $.trim($("#datetimepicker_Activity_End_txt_" + datetimepicker_Activity_Start_txtId).val()) != "")
                        $(this).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_Sign_Start_txt_error_WithActStart_" + datetimepicker_Activity_Start_txtId).remove();
                    $("#datetimepicker_Activity_Start_txt_error_WithSignStart_" + datetimepicker_Activity_Start_txtId).remove();
                    if ($("#datetimepicker_Activity_Sign_Start_txt_error_WithActStart_" + datetimepicker_Activity_Start_txtId).length == 0 && $("#datetimepicker_Activity_Sign_Start_txt_error_" + datetimepicker_Activity_Start_txtId).length == 0)
                        $("#datetimepicker_Activity_Sign_Start_txt_" + datetimepicker_Activity_Start_txtId).css({ "box-shadow": "" });
                }
            })
            //活動結束日期
            $("#datetimepicker_Activity_End_txt_" + sessionId).blur(function () {
                //取得ID
                var datetimepicker_Activity_End_txt_temp = $(this).attr("id");
                var datetimepicker_Activity_End_txtId = datetimepicker_Activity_End_txt_temp.split("_")[datetimepicker_Activity_End_txt_temp.split("_").length - 1];
                //判斷在活動開始日期之前
                if ($.trim($(this).val()) <= $.trim($("#datetimepicker_Activity_Start_txt_" + datetimepicker_Activity_End_txtId).val()) && $.trim($("#datetimepicker_Activity_Start_txt_" + datetimepicker_Activity_End_txtId).val()) != "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#datetimepicker_Activity_End_txt_error_" + datetimepicker_Activity_End_txtId).length == 0)
                        $(this).after('<em id="datetimepicker_Activity_End_txt_error_' + datetimepicker_Activity_End_txtId + '" class="error help-block red" style="width: auto;margin-bottom: 5px;">活動結束日期必須在活動開始日期之後</em>');
                    check_Activity_Data = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_End_txt_error_" + datetimepicker_Activity_End_txtId).remove();
                    $("#datetimepicker_Activity_Start_txt_error_" + datetimepicker_Activity_End_txtId).remove();
                    if ($("#datetimepicker_Activity_Start_txt_error_WithSignStart_" + datetimepicker_Activity_End_txtId).length == 0 && $("#datetimepicker_Activity_Start_txt_error_" + datetimepicker_Activity_End_txtId).length == 0)
                        $("#datetimepicker_Activity_Start_txt_" + datetimepicker_Activity_End_txtId).css({ "box-shadow": "" });
                }
                //判斷在活動報名結束之前
                if ($.trim($(this).val()) <= $.trim($("#datetimepicker_Activity_Sign_End_txt_" + datetimepicker_Activity_End_txtId).val()) && $.trim($("#datetimepicker_Activity_Sign_End_txt_" + datetimepicker_Activity_End_txtId).val()) != "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#datetimepicker_Activity_End_txt_error_withSignEnd_" + datetimepicker_Activity_End_txtId).length == 0)
                        $(this).after('<em id="datetimepicker_Activity_End_txt_error_withSignEnd_' + datetimepicker_Activity_End_txtId + '" class="error help-block red" style="width: 222px;margin-bottom: 5px;">活動結束日期必須在報名結束日期之後</em>');
                    check_Activity_Data = false;
                }
                else {
                    if ($("#datetimepicker_Activity_End_txt_error_withSignEnd_" + datetimepicker_Activity_End_txtId).length == 0 && $("#datetimepicker_Activity_End_txt_error_" + datetimepicker_Activity_End_txtId).length == 0)
                        $(this).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_End_txt_error_withSignEnd_" + datetimepicker_Activity_End_txtId).remove();
                    $("#datetimepicker_Activity_Sign_End_txt_error_withActEnd_" + datetimepicker_Activity_End_txtId).remove();
                    if ($("#datetimepicker_Activity_Sign_End_txt_error_withActEnd_" + datetimepicker_Activity_End_txtId).length == 0 && $("#datetimepicker_Activity_Sign_End_txt_error_" + datetimepicker_Activity_End_txtId).length == 0)
                        $("#datetimepicker_Activity_Sign_End_txt_" + datetimepicker_Activity_End_txtId).css({ "box-shadow": "" });
                }
            })
            //報名開始日期
            $("#datetimepicker_Activity_Sign_Start_txt_" + sessionId).blur(function () {
                //取得ID
                var datetimepicker_Activity_Sign_Start_txt_temp = $(this).attr("id");
                var datetimepicker_Activity_Sign_Start_txt_temp_txtId = datetimepicker_Activity_Sign_Start_txt_temp.split("_")[datetimepicker_Activity_Sign_Start_txt_temp.split("_").length - 1];
                //判斷在活動報名結束日期之前
                if ($.trim($(this).val()) >= $.trim($("#datetimepicker_Activity_Sign_End_txt_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).val()) && $.trim($("#datetimepicker_Activity_Sign_End_txt_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).val()) != "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#datetimepicker_Activity_Sign_Start_txt_error_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).length == 0)
                        $(this).after('<em id="datetimepicker_Activity_Sign_Start_txt_error_' + datetimepicker_Activity_Sign_Start_txt_temp_txtId + '" class="error help-block red" style="width: auto;margin-bottom: 5px;">報名開始日期必須在報名結束日期之前</em>');
                    check_Activity_Data = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_Sign_Start_txt_error_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).remove();
                    if ($("#datetimepicker_Activity_Sign_End_txt_error_withActEnd_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).length == 0)
                        $("#datetimepicker_Activity_Sign_End_txt_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_Sign_End_txt_error_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).remove();
                }
                //判斷在活動開始日期之前
                if ($.trim($(this).val()) >= $.trim($("#datetimepicker_Activity_Start_txt_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).val()) && $.trim($("#datetimepicker_Activity_Start_txt_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).val()) != "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#datetimepicker_Activity_Sign_Start_txt_error_WithActStart_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).length == 0)
                        $(this).after('<em id="datetimepicker_Activity_Sign_Start_txt_error_WithActStart_' + datetimepicker_Activity_Sign_Start_txt_temp_txtId + '" class="error help-block red" style="width: auto;margin-bottom: 5px;">報名開始日期必須在活動開始日期之前</em>');
                    check_Activity_Data = false;
                }
                else {
                    if ($("#datetimepicker_Activity_Sign_Start_txt_error_WithActStart_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).length == 0 && $("#datetimepicker_Activity_Sign_Start_txt_error_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).length == 0)
                        $(this).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_Sign_Start_txt_error_WithActStart_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).remove();
                    $("#datetimepicker_Activity_Start_txt_error_WithSignStart_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).remove();
                    if ($("#datetimepicker_Activity_Start_txt_error_WithSignStart_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).length == 0 && $("#datetimepicker_Activity_Start_txt_error_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).length == 0)
                        $("#datetimepicker_Activity_Start_txt_" + datetimepicker_Activity_Sign_Start_txt_temp_txtId).css({ "box-shadow": "" });
                }
            })
            //報名結束日期
            $("#datetimepicker_Activity_Sign_End_txt_" + sessionId).blur(function () {
                //取得ID
                var datetimepicker_Activity_Sign_End_txt_temp = $(this).attr("id");
                var datetimepicker_Activity_Sign_End_txt_temp_txtId = datetimepicker_Activity_Sign_End_txt_temp.split("_")[datetimepicker_Activity_Sign_End_txt_temp.split("_").length - 1];
                //判斷在活動報名開始日期之後
                if ($.trim($(this).val()) <= $.trim($("#datetimepicker_Activity_Sign_Start_txt_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).val()) && $.trim($("#datetimepicker_Activity_Sign_Start_txt_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).val()) != "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#datetimepicker_Activity_Sign_End_txt_error_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).length == 0)
                        $(this).after('<em id="datetimepicker_Activity_Sign_End_txt_error_' + datetimepicker_Activity_Sign_End_txt_temp_txtId + '" class="error help-block red" style="width: auto;margin-bottom: 5px;">報名結束日期必須在報名開始日期之後</em>');
                    check_Activity_Data = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_Sign_End_txt_error_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).remove();
                    $("#datetimepicker_Activity_Sign_Start_txt_error_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).remove();
                    if ($("#datetimepicker_Activity_Sign_Start_txt_error_WithActStart_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).length == 0 && $("#datetimepicker_Activity_Sign_Start_txt_error_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).length == 0)
                        $("#datetimepicker_Activity_Sign_Start_txt_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).css({ "box-shadow": "" });
                }
                //判斷在活動結束日期之前
                if ($.trim($(this).val()) >= $.trim($("#datetimepicker_Activity_End_txt_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).val()) && $.trim($("#datetimepicker_Activity_End_txt_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).val()) != "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#datetimepicker_Activity_Sign_End_txt_error_withActEnd_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).length == 0)
                        $(this).after('<em id="datetimepicker_Activity_Sign_End_txt_error_withActEnd_' + datetimepicker_Activity_Sign_End_txt_temp_txtId + '" class="error help-block red" style="width: 222px;margin-bottom: 5px;">報名結束日期必須在活動結束日期之前</em>');
                    check_Activity_Data = false;
                }
                else {
                    $("#datetimepicker_Activity_End_txt_error_withSignEnd_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).remove();
                    if ($("#datetimepicker_Activity_End_txt_error_withSignEnd_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).length == 0 && $("#datetimepicker_Activity_End_txt_error_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).length == 0)
                        $("#datetimepicker_Activity_End_txt_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_Sign_End_txt_error_withActEnd_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).remove();
                    if ($("#datetimepicker_Activity_Sign_End_txt_error_withActEnd_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).length == 0 && $("#datetimepicker_Activity_End_txt_error_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).length == 0 && $("#datetimepicker_Activity_Sign_End_txt_error_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).length == 0)
                        $(this).css({ "box-shadow": "" });
                }
            })
            //場次ID加一
            sessionId++;
        };
        //移除場次
        function del_Session_click(chooseId) {
            $('#delete_Session_div_' + chooseId).remove();
        };
        //#endregion

        //#region 報名表問題拖拉 
        $(function () {
            $(".column").sortable({
                connectWith: ".column",
                handle: ".portlet-header",
                cancel: ".portlet-toggle",
                placeholder: "portlet-placeholder ui-corner-all",
                cancel: ".portlet-state-disabled",
                axis: "y",
                cursor: 'move',
                opacity: 0.6,//拖動時透明度 
                //revert: true,
                start: function (event, ui) {
                    var start_pos = ui.item.index();
                    ui.item.data('start_pos', start_pos);
                },
                update: function (event, ui) {
                }
            });
            $(".portlet")
                .addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
                .find(".portlet-header")
                .addClass("ui-widget-header ui-corner-all");
            //.prepend( "<span class='ui-icon ui-icon-minusthick portlet-toggle'></span>");

            $(".portlet-toggle").click(function () {
                var icon = $(this);
                icon.toggleClass("ui-icon-minusthick ui-icon-plusthick");
                icon.closest(".portlet").find(".portlet-content").toggle();
            });
        });
        //#endregion     

        //#region 回到頂端按鈕 
        (function () {
            $("body").append("<img id='goTopButton' style='display: none; z-index: 5; cursor: pointer;' title='回到頂端'/>");
            var img = "../Images/go-top.png",
            locatioin = 9 / 10, // 按鈕出現在螢幕的高度
            right = 10, // 距離右邊 px 值
            opacity = 0.8, // 透明度
            speed = 500, // 捲動速度
            $button = $("#goTopButton"),
            $body = $(document),
            $win = $(window);
            $button.attr("src", img);
            $button.on({
                mouseover: function () { $button.css("opacity", 1); },
                mouseout: function () { $button.css("opacity", opacity); },
                click: function () { $("html, body").animate({ scrollTop: 0 }, speed); }
            });
            window.goTopMove = function () {
                var scrollH = $body.scrollTop(),
                winH = $win.height(),
                css = { "top": winH * locatioin + "px", "position": "fixed", "right": right, "opacity": opacity };
                if (scrollH > 20) {
                    $button.css(css);
                    $button.fadeIn("slow");
                } else {
                    $button.fadeOut("slow");
                }
            };
            $win.on({
                scroll: function () { goTopMove(); },
                resize: function () { goTopMove(); }
            });
        })();
        //#endregion

        //#region 懸浮區塊
        $('#suspension_div').affix({
            offset: {
                top: 250,
                bottom: function () {
                    return (this.bottom = $('.footer').outerHeight(true))
                }
            }
        });
        //#endregion

        //#region 儲存活動頁面
        function Save_btn_Click() {
            var jsondata = Save_Activity_Json();
            var jsondata_column = Save_Activity_Column_Json();
            if (check_Activity_Data === true && check_Activity_Column_Data === true) {
                if (checkDataSignEnd == true) {
                    var if_Save = confirm("您的報名結束日期在活動開始日期之後，確定要儲存嗎?");
                    if (if_Save == true) {
                        $("#loading").click();
                        $.ajax({
                            type: 'post',
                            traditional: true,
                            //將資料傳到後台save_Activity這個function
                            url: 'S02010201.aspx/save_Activity',
                            data: JSON.stringify(jsondata),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,
                            processData: false,
                            async: false,
                            //成功時
                            success: function (result) {
                                if (result.d == "true")
                                    Save_Activity_btn_Click(jsondata_column);
                                else {
                                    $("#loading").click();
                                    alert("活動儲存失敗");
                                }
                                    
                            },
                            //失敗時
                            error: function () {
                                $("#loading").click();
                                alert("活動儲存失敗");
                            }
                        });
                    }
                }
                else {
                    $("#loading").click();
                    $.ajax({
                        type: 'post',
                        traditional: true,
                        //將資料傳到後台save_Activity這個function
                        url: 'S02010201.aspx/save_Activity',
                        data: JSON.stringify(jsondata),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        processData: false,
                        async: false,
                        //成功時
                        success: function (result) {
                            if (result.d == "true")
                                Save_Activity_btn_Click(jsondata_column);
                            else {
                                $("#loading").click();
                                alert("活動儲存失敗");
                            }
                        },
                        //失敗時
                        error: function () {
                            $("#loading").click();
                            alert("活動儲存失敗");
                        }
                    });
                }
            }
            else if (check_Activity_Data === false) {
                alert("活動頁面資料有誤");
            }
            else if (check_Activity_Column_Data === false) {
                alert("報名表資料有誤");
            }
        }
        //#endregion

        //#region 儲存活動頁面資訊成JSON
        function Save_Activity_Json() {
            //判斷資料是否正確 true:正確 false:錯誤
            check_Activity_Data = true;
            //把資料包成List
            var jsondata = { activity_List: [], activity_Session_List: [] };
            //儲存活動場次資訊，迴圈依照目前場次ID做為要跑的次數
            for (var temp = 1 ; temp < sessionId ; temp++) {
                //抓取場次區塊依照ID順序
                var $delete_Session_div = $("#delete_Session_div_" + temp);
                //判斷這個場次區塊是否存在
                if ($delete_Session_div.length > 0) {
                    var session_Json_Data = {};
                    //存入場次名稱
                    session_Json_Data.As_title = $("#session_Name_txt_" + temp).val();
                    //存入場次活動開始日期
                    session_Json_Data.As_date_start = $("#datetimepicker_Activity_Start_txt_" + temp).val();
                    //存入場次活動結束日期
                    session_Json_Data.As_date_end = $("#datetimepicker_Activity_End_txt_" + temp).val();
                    //存入場次活動報名開始日期
                    session_Json_Data.As_apply_start = $("#datetimepicker_Activity_Sign_Start_txt_" + temp).val();
                    //存入場次活動報名結束日期
                    session_Json_Data.As_apply_end = $("#datetimepicker_Activity_Sign_End_txt_" + temp).val();
                    //存入場次活動地點
                    session_Json_Data.As_position = $("#activity_Location_txt_" + temp).val();
                    //存入場次活動人數限制
                    session_Json_Data.As_num_limit = $("#activity_Limit_Num_txt_" + temp).val();
                    //存入場次備註
                    session_Json_Data.As_remark = $("#activity_remark_txt_" + temp).val();
                    //判斷場次名不為空
                    if (!session_Json_Data.As_title) {
                        $("#session_Name_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#session_Name_txt_error_" + temp).length == 0)
                            $("#session_Name_txt_" + temp).after('<em id="session_Name_txt_error_' + temp + '" class="error help-block red">必須填寫</em>');
                        check_Activity_Data = false;
                    }
                    else {
                        $("#session_Name_txt_" + temp).css({ "box-shadow": "" });
                        $("#session_Name_txt_error_" + temp).remove();
                    }
                    //判斷活動地點不為空
                    if (!session_Json_Data.As_position) {
                        $("#activity_Location_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#activity_Location_txt_error_" + temp).length == 0)
                            $("#activity_Location_txt_" + temp).after('<em id="activity_Location_txt_error_' + temp + '" class="error help-block red">必須填寫</em>');
                        check_Activity_Data = false;
                    }
                    else {
                        $("#activity_Location_txt_" + temp).css({ "box-shadow": "" });
                        $("#sactivity_Location_txt_error_" + temp).remove();
                    }
                    //判斷報名人數限制不為空以及要是整數數字
                    var if_int = /^[0-9]*[1-9][0-9]*$/;
                    if (!session_Json_Data.As_num_limit || isNaN(session_Json_Data.As_num_limit) || !if_int.test(session_Json_Data.As_num_limit) || parseInt($.trim($("#activity_Limit_Num_txt_" + temp).val())) > 100000) {
                        $("#activity_Limit_Num_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#activity_Limit_Num_txt_error_" + temp).length == 0)
                            $("#activity_Limit_Num_txt_" + temp).after('<em id="activity_Limit_Num_txt_error_' + temp + '" class="error help-block red">必須填寫整數數字</em>');
                        if (parseInt($.trim($("#activity_Limit_Num_txt_" + temp).val())) > 100000)
                            $("#activity_Limit_Num_txt_error_" + temp).text("請輸入小於100000的數字!!");
                        check_Activity_Data = false;
                    }
                    else {
                        $("#activity_Limit_Num_txt_" + temp).css({ "box-shadow": "" });
                        $("#activity_Limit_Num_txt_error_" + temp).remove();
                    }
                    //判斷活動開始日期不能為空
                    if (!session_Json_Data.As_date_start) {
                        $("#datetimepicker_Activity_Start_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#datetimepicker_Activity_Start_txt_error_" + temp).length == 0)
                            $("#datetimepicker_Activity_Start_txt_" + temp).after('<em id="datetimepicker_Activity_Start_txt_error_' + temp + '" class="error help-block red">必需填寫</em>');
                        check_Activity_Data = false;
                    }
                    //判斷活動結束日期不能為空
                    if (!session_Json_Data.As_date_end) {
                        $("#datetimepicker_Activity_End_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#datetimepicker_Activity_End_txt_error_" + temp).length == 0)
                            $("#datetimepicker_Activity_End_txt_" + temp).after('<em id="datetimepicker_Activity_End_txt_error_' + temp + '" class="error help-block red">必需填寫</em>');
                        check_Activity_Data = false;
                    }
                    //判斷活動報名開始日期不能為空
                    if (!session_Json_Data.As_apply_start) {
                        $("#datetimepicker_Activity_Sign_Start_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#datetimepicker_Activity_Sign_Start_txt_error_" + temp).length == 0)
                            $("#datetimepicker_Activity_Sign_Start_txt_" + temp).after('<em id="datetimepicker_Activity_Sign_Start_txt_error_' + temp + '" class="error help-block red">必需填寫</em>');
                        check_Activity_Data = false;
                    }
                    //判斷活動報名結束日期不能為空
                    if (!session_Json_Data.As_apply_end) {
                        $("#datetimepicker_Activity_Sign_End_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#datetimepicker_Activity_Sign_End_txt_error_" + temp).length == 0)
                            $("#datetimepicker_Activity_Sign_End_txt_" + temp).after('<em id="datetimepicker_Activity_Sign_End_txt_error_' + temp + '" class="error help-block red">必需填寫</em>');
                        check_Activity_Data = false;
                    }
                    //判斷活動開始日期是否與活動報名開始日期一樣
                    if (session_Json_Data.As_apply_start >= session_Json_Data.As_date_start && session_Json_Data.As_apply_start && session_Json_Data.As_date_start) {
                        $("#datetimepicker_Activity_Start_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#datetimepicker_Activity_Start_txt_error_WithSignStart_" + temp).length == 0)
                            $("#datetimepicker_Activity_Start_txt_" + temp).after('<em id="datetimepicker_Activity_Start_txt_error_WithSignStart_' + temp + '" class="error help-block red">活動開始時間不能相同於報名開始時間</em>');
                        $("#datetimepicker_Activity_Sign_Start_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#datetimepicker_Activity_Sign_Start_txt_error_WithActStart_" + temp).length == 0)
                            $("#datetimepicker_Activity_Sign_Start_txt_" + temp).after('<em id="datetimepicker_Activity_Sign_Start_txt_error_WithActStart_' + temp + '" class="error help-block red">報名開始時間不能相同於活動開始時間</em>');
                        check_Activity_Data = false;
                    }
                    if (session_Json_Data.As_date_start != "" && session_Json_Data.As_date_end != "" && session_Json_Data.As_apply_start != "" && session_Json_Data.As_apply_end != "") {
                        if (session_Json_Data.As_date_start < session_Json_Data.As_date_end && session_Json_Data.As_date_start > session_Json_Data.As_apply_start && session_Json_Data.As_apply_start < session_Json_Data.As_apply_end && session_Json_Data.As_apply_start < session_Json_Data.As_date_end && session_Json_Data.As_apply_end < session_Json_Data.As_date_end) {
                            //check_Activity_Data = true;
                        }
                        else
                            check_Activity_Data = false;
                    }
                    else
                        check_Activity_Data = false;
                    //判斷活動結束報名時間是否在活動開始時間之後
                    if (session_Json_Data.As_apply_end > session_Json_Data.As_date_start)
                        checkDataSignEnd = true;

                    jsondata.activity_Session_List.push(session_Json_Data);
                }
            }
            ///更新 ckeditor 內容(避免使用ajax傳至後台時 ckeditor內容還是舊的)
            for (instance in CKEDITOR.instances) {
                CKEDITOR.instances[instance].updateElement();
            }
            //儲存活動資訊
            var activity_Json_Data = {};
            //儲存活動標題
            activity_Json_Data.Act_title = $("#activity_Name_txt").val();
            //儲存活動描述
            activity_Json_Data.Act_desc = CKEDITOR.instances.editor1.getData();;
            CKEDITOR.instances.editor1.setData('');
            //儲存主辦單位
            activity_Json_Data.Act_unit = $("#unit_txt").val();
            //儲存聯絡人
            activity_Json_Data.Act_contact_name = $("#contact_Person_txt").val();
            //儲存聯絡人電話
            activity_Json_Data.Act_contact_phone = $("#contact_Person_Phone_txt").val();
            //儲存相關連結
            activity_Json_Data.Act_relate_link = $("#relate_Link").val();
            //儲存報名場次次數限制
            if ($.trim($("#act_num_limit_txt").val()) == "")
                activity_Json_Data.Act_num_limit = 0;
            else
                activity_Json_Data.Act_num_limit = $.trim($("#act_num_limit_txt").val());
            //儲存活動分類
            if ($("#select_class").val() == 0) {
                $("#select_class").css({ "box-shadow": "0px 0px 9px red" });
                if ($("#select_class_txt_error").length == 0)
                    $("#select_class").after('<em id="select_class_txt_error" class="error help-block red" style="margin-top: -10px;">必須選擇</em>');
                check_Activity_Data = false;
            }
            else {
                $("#select_class").css({ "box-shadow": "" });
                $("#select_class_txt_error").remove();
                activity_Json_Data.Act_class = $("#select_class").val();
            }
            //判斷活動名稱不能為空
            if (!activity_Json_Data.Act_title) {
                $("#activity_Name_txt").css({ "box-shadow": "0px 0px 9px red" });
                if ($("#activity_Name_txt_error").length == 0)
                    $("#activity_Name_txt").after('<em id="activity_Name_txt_error" class="error help-block red">必須填寫</em>');
                check_Activity_Data = false;
            }
            else {
                $("#activity_Name_txt").css({ "box-shadow": "" });
                $("#activity_Name_txt_error" + temp).remove();
            }
            //儲存上傳圖片以及附加檔案的副檔名，用來判斷是否符合格式
            var imgFileName = document.getElementById("<%=this.imgUpload.ClientID %>").value.split(".")[document.getElementById("<%=this.imgUpload.ClientID %>").value.split(".").length - 1];
            var FileName = document.getElementById("<%=this.FileUpload.ClientID %>").value.split(".")[document.getElementById("<%=this.FileUpload.ClientID %>").value.split(".").length - 1];

            //如果資料正確，使用jQuery ajax傳送資料
            if (imgFileName != "jpg" && imgFileName != "jpeg" && imgFileName != "png" && imgFileName) {
                alert("圖片只能上傳jpg、jpeg、png格式");
                check_Activity_Data = false;
            }
            if (FileName != "jpg" && FileName != "jpeg" && FileName != "png" && FileName != "gif" && FileName != "doc" && FileName != "docx" && FileName != "txt" && FileName != "ppt" && FileName != "pptx" && FileName != "xls" && FileName != "xlsx" && FileName != "pdf" && FileName != "rar" && FileName != "zip" && FileName != "7z" && FileName) {
                alert("附加檔案只能上傳jpg、png、jpeg、gif、doc、docx、txt、ppt、pptx、xls、xlsx、pdf、rar、zip、7z格式");
                check_Activity_Data = false;
            }

            jsondata.activity_List.push(activity_Json_Data);

            return jsondata;
        }
        //#endregion

        //#region 儲存報名表
        function Save_Activity_btn_Click(jsondata_column) {
            //var checkData = true;
            //var jsondata = Save_Activity_Column_Json();
            //使用ajax傳送
            if (check_Activity_Column_Data == true && check_Activity_Data == true) {
                $.ajax({
                    type: 'post',
                    traditional: true,
                    //傳送資料到後台為save_Activity_Form的function
                    url: 'S02010201.aspx/save_Activity_Form',
                    data: JSON.stringify(jsondata_column),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    //成功時
                    success: function (result) {
                        if (result.d == "true") {
                            $("#loading").click();
                            alert("活動儲存成功");
                            upload_File();
                        }
                        else {
                            $("#loading").click();
                            alert("活動儲存失敗");
                        }         
                    },
                    //失敗時
                    error: function () {
                        $("#loading").click();
                        alert("活動儲存失敗");
                    }
                });
            }
            else if (check_Activity_Column_Data === false) {
                alert("報名表資料有誤!!");
            }
        }
        //#endregion 

        //#region 儲存報名表成JSON
        function Save_Activity_Column_Json() {
            check_Activity_Column_Data = true;
            var jsondata = { activity_Form: [], activity_Section: [] };
            //區塊順序初始化
            var block_asc = 1;
            //迴圈依照blockID 判斷要跑幾次
            for (var add_Qus_count = 1 ; add_Qus_count < blockId ; add_Qus_count++) {
                //抓取現在block的資訊
                var $block_div = $("#block_div_" + add_Qus_count);
                //抓取新增問題地方的資訊
                var $add_Qus_div = $("#add_Qus_div_" + add_Qus_count);
                //判斷block是否存在
                if ($block_div.length > 0) {
                    //問題順序初始化
                    var qus_seq = 1;
                    //將目前block內的問題進行排序並存成陣列
                    var $qus_sortable = $add_Qus_div.sortable("toArray");
                    //抓取現在的block的ID並進行切割取得純數字ID
                    var choose_blockId_temp = $block_div.attr("id");
                    var choose_blockId = choose_blockId_temp.split("_")[choose_blockId_temp.split("_").length - 1];
                    //依照陣列長度判斷是否有問題存在
                    if ($qus_sortable.length > 0) {
                        //如果陣列裡有問題則把紅色陰影去掉
                        $("#block_div_" + choose_blockId).css({ "box-shadow": "" });
                        var activity_Section_Json_Data = {};
                        //判斷block是否有標題
                        if ($.trim($("#block_title_txt_" + choose_blockId).val()) == "") {
                            $("#block_title_txt_" + choose_blockId).css({ "box-shadow": "0px 0px 9px red" });
                            if ($("#block_title_txt_error_" + choose_blockId).length == 0)
                                $("#block_title_txt_" + choose_blockId).after('<em id="block_title_txt_error_' + choose_blockId + '" class="error help-block red"><h6>必須填寫</h6></em>');
                            check_Activity_Column_Data = false;
                        }
                        else {
                            $("#block_title_txt_" + choose_blockId).css({ "box-shadow": "" });
                            $("#block_title_txt_error_" + choose_blockId).remove();
                            //if (checkData != 0) checkData = 1;
                        }
                        //儲存block名稱
                        activity_Section_Json_Data.Acs_title = $("#block_div_" + choose_blockId).find("#block_title_txt_" + choose_blockId).val();
                        //儲存block描述
                        activity_Section_Json_Data.Acs_desc = $("#block_div_" + choose_blockId).find("#block_Description_txt_" + choose_blockId).val();
                        //儲存block順序
                        activity_Section_Json_Data.Acs_seq = block_asc;
                        //將block資料push到jsondata的activity_Section[]裡面
                        jsondata.activity_Section.push(activity_Section_Json_Data);
                        //依照問題排序陣列來獲得這個block裡面有幾個問題
                        for (var qus_count = 0; qus_count < $qus_sortable.length; qus_count++) {
                            var activity_Column_Json_Data = new Object;
                            //獲得這個問題的純數字ID
                            var chooseId_temp = $("#" + $qus_sortable[qus_count]).attr("id");
                            var chooseId = chooseId_temp.split("_")[chooseId_temp.split("_").length - 1];
                            //判斷問題是否有標題
                            if ($.trim($("#qus_txt_" + chooseId).val()) == "") {
                                $("#qus_txt_" + chooseId).css({ "box-shadow": "0px 0px 9px red" });
                                if ($("#qus_txt_error_" + chooseId).length == 0)
                                    $("#qus_txt_" + chooseId).after('<em id="qus_txt_error_' + chooseId + '" class="error help-block red">必須填寫</em>');
                                check_Activity_Column_Data = false;
                            }
                            else {
                                $("#qus_txt_" + chooseId).css({ "box-shadow": "" });
                                $("#qus_txt_error_" + chooseId).remove();
                            }
                            //儲存題目名稱
                            activity_Column_Json_Data.Acc_title = $("#" + $qus_sortable[qus_count]).find("#qus_txt_" + chooseId).val();
                            //儲存題目描述
                            activity_Column_Json_Data.Acc_desc = $("#" + $qus_sortable[qus_count]).find("#qus_context_txt_" + chooseId).val();
                            //儲存題目順序
                            activity_Column_Json_Data.Acc_seq = qus_seq;
                            //判斷是否必填
                            if ($("#" + $qus_sortable[qus_count]).find("#required_checkbox_" + chooseId).is(":checked") === false)
                                //沒有必填存0
                                activity_Column_Json_Data.Acc_required = 0;
                            else if ($("#" + $qus_sortable[qus_count]).find("#required_checkbox_" + chooseId).is(":checked") === true)
                                //必填存1
                                activity_Column_Json_Data.Acc_required = 1;
                            //儲存問題模式
                            //activity_Column_Json_Data.Acc_type = $("#" + $qus_sortable[qus_count]).find("#select_" + chooseId).val();
                            activity_Column_Json_Data.Acc_type = $("#select_" + chooseId).val();
                            //儲存資料驗證方式
                            if ($("#select_Validation_" + chooseId).val() == "int" || $("#select_Validation_" + chooseId).val() == "length") {
                                if ($.trim($("#min_Num_" + chooseId).val()) == "")
                                    var min_Num = "N";
                                else
                                    var min_Num = $.trim($("#min_Num_" + chooseId).val());
                                if ($.trim($("#max_Num_" + chooseId).val()) == "")
                                    var max_Num = "N";
                                else
                                    var max_Num = $.trim($("#max_Num_" + chooseId).val());
                                if (max_Num != "N" && min_Num != "N") {
                                    if (max_Num < min_Num) {
                                        $("#max_Num_" + chooseId).css({ "box-shadow": "0px 0px 9px red" });
                                        $("#max_Num_" + chooseId).after('<em id="max_Num_error_' + chooseId + '" class="error help-block red" style="width: 91px;margin-left: -57px;">必須大於最小值</em>');
                                        check_Activity_Column_Data = false;
                                    }
                                    else {
                                        $("#max_Num_" + chooseId).css({ "box-shadow": "" });
                                        $("#max_Num_error_" + chooseId).remove();
                                    }

                                }

                                activity_Column_Json_Data.Acc_validation = $("#select_Validation_" + chooseId).val() + ',' + min_Num + ',' + max_Num;
                            }
                            else
                                activity_Column_Json_Data.Acc_validation = $("#select_Validation_" + chooseId).val();
                            //判斷問題模式如果不為文字則要存選項內容
                            if ($("#" + $qus_sortable[qus_count]).find("select").val() != "文字") {
                                //抓取ID為add_Qus_Options_div_的div
                                var $option_div = $("#" + $qus_sortable[qus_count]).find("[id^=add_Qus_Options_div_]");
                                //判斷是否每個選項都有填寫
                                $option_div.find($('[name="qus_Options"]')).each(function () {
                                    if ($.trim($(this).val()) == "") {
                                        //this.focus();
                                        $(this).css({ "box-shadow": "0px 0px 9px red" });
                                        check_Activity_Column_Data = false;
                                    }
                                    else {
                                        $(this).css({ "box-shadow": "" });
                                    }

                                });
                                //將上面抓到的div裡面所有的input轉成序列儲存
                                var $option = $option_div.find($('[name="qus_Options"]')).serialize();
                                //var $option = $.map($option_div.find($('[name="qus_Options"]')), function ($op) {
                                //    return $($op).val();
                                //})
                                //儲存選項序列
                                activity_Column_Json_Data.Acc_option = $option;

                            }
                            //問題順序加一
                            qus_seq++;
                            //儲存block順序
                            activity_Column_Json_Data.Acc_asc = block_asc;
                            //將問題資料push到jsondata裡面
                            jsondata.activity_Form.push(activity_Column_Json_Data);
                        }
                        $("#block_div_error_" + choose_blockId).remove();
                    }
                    else {
                        $("#block_div_" + choose_blockId).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#block_div_error_" + choose_blockId).length == 0)
                            $("#block_div_" + choose_blockId).prepend('<em id="block_div_error_' + choose_blockId + '" class="error help-block red"><h3>內無問題，請刪除或是新增問題</h3></em>');
                        check_Activity_Column_Data = false;
                        //alert("您有空白區塊，請新增問題或是刪除區塊!!");
                    }
                    //區塊順序加一
                    block_asc++;
                }
            }
            return jsondata;
        }
        //#endregion

        //#region 上傳檔案
        function upload_File() {
            //抓取上傳檔案的按鈕，藉此可以由前端呼叫後端的function
            document.getElementById("<%=this.btnUpload.ClientID %>").click();
        }
        //#endregion

        // #region 檢視報名表
        function view_Activity() {
            var json_Column = JSON.stringify(Save_Activity_Column_Json());
            if (check_Activity_Column_Data == true) {
                $("#save_Activity_Column_Json").val(json_Column);
                window.open("S02010202.aspx?sys_id=S01&sys_pid=S02010202");
            }
            else
                alert("報名表尚有資料未填寫或有錯誤");
        }
        // #endregion

        //#region 儲存活動
        function save() {
            Save_btn_Click();
        }
        //#endregion

        //#region 點擊上傳圖片事件
        function upload_img() {
            document.getElementById("<%=this.imgUpload.ClientID %>").click();
        }
        function upload_imgpath(path) {
            if (path != "") {
                var path = path.split("\\")[path.split("\\").length - 1];
                var imgFileName = path.split(".")[path.split(".").length - 1];
                //如果資料正確，使用jQuery ajax傳送資料
                if (imgFileName != "jpg" && imgFileName != "jpeg" && imgFileName != "png" && imgFileName) {
                    alert("圖片只能上傳jpg、jpeg、png格式");
                    check_Activity_Data = false;
                    $("#<%=imgUpload.ClientID %>").val("");
                    //錯誤提示
                    $('#imgpath_lab').text("請選擇正確的圖片格式!");
                }
                else
                    //設定上傳圖片檔名
                    $('#imgpath_lab').text(path);
            }
            else
                $('#imgpath_lab').text("選擇圖片");
        }
        //#endregion
        function upload_file_btn() {
            document.getElementById("<%=this.FileUpload.ClientID %>").click();
        }
        function upload_file(path) {
            if (path != "") {
                var path = path.split("\\")[path.split("\\").length - 1];
                var FileName = path.split(".")[path.split(".").length - 1];
                //如果資料正確，使用jQuery ajax傳送資料
                if (FileName != "jpg" && FileName != "jpeg" && FileName != "png" && FileName != "gif" && FileName != "doc" && FileName != "docx" && FileName != "txt" && FileName != "ppt" && FileName != "pptx" && FileName != "xls" && FileName != "xlsx" && FileName != "pdf" && FileName != "rar" && FileName != "zip" && FileName != "7z" && FileName) {
                    alert("附加檔案只能上傳jpg、png、jpeg、gif、doc、docx、txt、ppt、pptx、xls、xlsx、pdf、rar、zip、7z格式");
                    check_Activity_Data = false;
                    $("#<%=FileUpload.ClientID %>").val("");
                    $("#filepath_lab").text("請選擇正確的檔案格式!");
                }
                else
                    $("#filepath_lab").text(path);
            }
            else
                $("#filepath_lab").text("選擇檔案");
        }

    </script>
    <!-- JavaScript_END-->
</asp:Content>
