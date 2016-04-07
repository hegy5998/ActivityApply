<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S02010201.aspx.cs" Inherits="Web.S02.S02010201" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
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
    <!-- 儲存活動頁面 -->
    <input type="submit" onclick="Save_btn_Click()" value="儲存活動頁面"/>
    <!-- 儲存報名表 -->
    <input type="submit" onclick="Save_Activity_btn_Click()" value="儲存報名表"/>
    <input type="submit" onclick="upload()" value="上傳檔案"/>
    <!-- 檢視 -->
    <input type="submit" onclick="view_Activity()" value="檢視報名表"/>
    <!-- 儲存活動 -->
    <asp:Button runat="server" ID="Save_btn" Text="儲存活動" OnClick="Save_btn_Click" CssClass="Distancebtn" />
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
      <h4>IChrome : 設定 -> 顯示進階設定 -> 隱私權 -> 內容定... -> JavaScript -> 選擇「允許所有網站執行JavaScript」</h4>
    </noscript>
<div id="Tabs" role="tabpanel">
    <!-- 建立活動標籤_START-->
    <ul class="nav nav-tabs nav-justified" id="myTab" role="tablist">
        <li class="tab-pane active">
            <a data-toggle="tab" href="#activityMenu">活動頁面</a>
        </li>
        <li class="tab-pane">
            <a data-toggle="tab" href="#activityQus">活動報名表</a>
        </li>
    </ul>
    <!-- 建立活動標籤_END-->

    <!-- 標籤內容_START -->
    <div class="tab-content">
        <!-- 活動頁面標籤 START -->  
        <div class="row mt tab-pane active" id="activityMenu" role="tabpanel">
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 desc" runat="server">
                <div class="project-wrapper">
                    <div class="project">
                        <div class="photo-wrapper">
                            <div class="photo">
                                <a data-toggle="modal" href="#pictureModal">
                                    <img class="img-responsive" src="../Scripts/Lib/assets/img/fcu.jpg" alt=""></a>
                            </div>     
                            <div class="overlay"></div>
                        </div>
                    </div>
                </div>
                <div class="row"></div>
            </div>
            <!-- 活動頁面內容_START -->
            <div class="col-lg-9 col-md-9 col-sm-9 col-xs-12 dexc">
                <!-- 增加活動區塊地方_START -->
                <div class="row" id="add_Session_div">
                    <!-- 活動場次區塊_START -->
                    <h3><i class="fa fa-angle-right"></i>活動場次</h3>
                    <!-- 活動名稱 -->
                    <input type="text" class="form-control"  placeholder="活動名稱" id="activity_Name_txt"/><br />
                    <%--<div class="showback" id="delete_Session_div_1">
                        <div class="form-horizontal style-form">
                            <h4 class="red">*為必填</h4>
                            <!-- 場次名稱 -->
                            <div class="form-group">
                                <label class="col-sm-2 control-label">場次名稱<b class="red">*</b></label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control" id="session_Name_txt_1"/>
                                </div>
                            </div>
                            <!-- 活動日期 -->
                            <div class="form-group">
                                <!-- 活動開始日期 -->
                                <label class="col-sm-2 control-label">活動開始日期<b class="red">*</b></label>
                                <div class="col-sm-4">
                                    <input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_Start_txt_1" />
                                </div>
                                <!-- 活動結束日期 -->
                                <label class="col-sm-2 control-label">活動結束日期<b class="red">*</b></label>
                                <div class="col-sm-4">
                                    <input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_End_txt_1"/>
                                </div>
                            </div>
                            <!-- 活動報名日期 -->
                            <div class="form-group">
                                <!-- 活動報名開始日期 -->
                                <label class="col-sm-2 control-label">報名開始日期<b class="red">*</b></label>
                                <div class="col-sm-4">
                                    <input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_Sign_Start_txt_1" />
                                </div>
                                <!-- 活動報名結束日期 -->
                                <label class="col-sm-2 control-label">報名結束日期<b class="red">*</b></label>
                                <div class="col-sm-4">
                                    <input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_Sign_End_txt_1" />
                                </div>
                            </div>
                            <!-- 活動地點 -->
                            <div class="form-group">
                                <label class="col-sm-2 control-label">活動地點<b class="red">*</b></label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control" id="activity_Location_txt_1" />
                                </div>
                            </div>
                            <!-- 活動人數限制 -->
                            <div class="form-group">
                                <label class="col-sm-2 control-label" >人數限制<b class="red">*</b></label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control" placeholder="請填寫數字" id="activity_Limit_Num_txt_1">
                                </div>
                            </div>
                            <!-- 增加場次按鈕 -->
                            <div class="form-group" style="padding-right: 10px;">
                                <a style="float: right;" class="btn btn-theme" onclick="add_Session_click()">增加場次</a>
                            </div>
                        </div>
                    </div>--%>
                    <!-- 活動場次區塊_END -->
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
                                <div class="col-sm-10">
                                    <!-- 活動簡介，使用CKeditor -->
                                    <textarea cols="80" id="editor1" name="editor1" rows="10"></textarea>
                                        <script type="text/javascript">
                                            CKEDITOR.replace('editor1',
                                                {
                                                    toolbar:
                                                    [
                                                        ['Undo', '-', 'Redo', '-', 'Cut', '-', 'Copy', '-', 'Paste', '-', 'PasteText', '-', 'Image', '-', 'Table', '-', 'HorizontalRule', '-', 'SpecialChar', '-', 'Bold', '-', 'Italic', '-', 'Strike', '-', 'TextColor', '-', 'BGColor', '-', 'NumberedList', '-', 'BulletedList', '-', 'Outdent', '-', 'Indent', '-', 'Link', '-', 'Unlink'],
                                                        '/',
                                                        ['Styles', 'Format', 'Font', 'FontSize', 'JustifyLeft', '-', 'JustifyCenter', '-', 'JustifyRight', '-', 'JustifyBlock']
                                                    ]
                                                });
                                        </script>
                                </div>
                            </div>
                            <!-- 主辦單位 -->
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">主辦單位</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control" id ="unit_txt">
                                </div>
                            </div>
                            <!-- 聯絡人 -->
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">聯絡人</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control" id="contact_Person_txt" />
                                </div>
                            </div>
                            <!-- 聯絡人電話 -->
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">聯絡人電話</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control" id="contact_Person_Phone_txt" />
                                </div>
                            </div>
                            <!-- 附加檔案 -->
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">附加檔案</label>
                                <div class="col-sm-10">
                                    <asp:UpdatePanel ChildrenAsTriggers="false" runat="server" UpdateMode="Conditional" ViewStateMode="Enabled">
                                        <ContentTemplate>
                                            <asp:FileUpload ID="FileUpload1" runat="server"/>
                                            <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click1" Style="display: none"/>
                                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                           <asp:postbacktrigger controlid="btnUpload"></asp:postbacktrigger>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    
                                </div>
                            </div>
                            <!-- 相關連結 -->
                            <div class="form-group" >
                                <label class="col-sm-2 col-sm-2 control-label">相關連結</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control"  value="http://" id="relate_Link">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- 活動資訊區塊_END -->
            </div>
            <!-- 活動頁面內容_END -->
        </div>
        <!-- 活動頁面標籤_END -->  

        <!-- 活動報名表標籤_START-->  
        <div class="row mt tab-pane fade" id="activityQus" role="tabpanel">
            <!-- 新增區塊地方_START-->  
            <div class="row mt " id="add_Block_div">
                <!-- 預設區塊一_START -->
                <div class="col-lg-11 col-md-11 col-sm-11 col-xs-12 dexc showback" id="block_div_1">
                    <%--<div class="col-sm-1" style="left: 95%;height: 40px;">
                        <a class="btn" style="color: #768094;" onclick="del_block_click(1)">X</a>
                    </div>--%>
                    <!-- 預設區塊一名稱_基本資料 -->
                    <h3><input type="text" id ="block_title_txt_1" class="form-control" placeholder="區塊名稱" value="基本資料"></h3>
                    <input type="text" id="block_Description_txt_1" class="form-control" placeholder="區塊描述">
                    <!-- 新增問題地方 START-->  
                    <div class="form-horizontal style-form column " id="add_Qus_div_1" style="min-height:50px">
                        
                       <%-- <div id="qus_div_1" class="form-group portlet showback" style="border: 0px; background: #ffffff;padding: 15px;margin-bottom: 15px;box-shadow: 0px 0px 2px #272727;border-radius: 20px;margin: 20px 0px 0px 1px;">
                                        <div class="col-sm-1 portlet-header center" style="background-color: #F1F2F7;height:100px; width:35px">
                                            <img src="../Images/drag_pic.jpg" alt="拖移" height="24px" style="transform: translateY(-50%);top: 50%;position: relative;"/> 
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-10" id="change_Qus_Way_div_1">
                                                <div class="col-sm-10" style="width: 100%">
                                                    <input type="text" ID="qus_txt_1"  placeholder="文字問題" class="form-control" style="width: 100%;">
                                                </div>
                                                <div class="col-sm-10" style="width: 100%">
                                                    <input type="text" ID="qus_context_txt_1"  placeholder="問題描述" class="form-control" style="width: 100%;margin-top: 15px;" >
                                                </div>
                                            </div>
                                            <div class="col-sm-1">
                                                <select class="select" id="select_1" style="width:56px;height:34px;border-radius:4px;margin-left: 50px;">
                                                      <option>單選</option>
                                                      <option>多選</option>
                                                      <option selected="selected">文字</option>
                                                      <option >選單</option>
                                                    </select>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6"  id="change_Qus_Content_div_1">

                                                <div class="panel-group" id="panel_group_1" role="tablist" aria-multiselectable="true" style="width: 350px;margin-left: 50px;">
                                                  <div class="panel panel-default">
                                                    <div class="panel-heading" role="tab" id="heading_1">
                                                      <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#panel_group_1" href="#collapse_1" aria-expanded="true" aria-controls="collapse_1">
                                                          單選問題
                                                        </a>
                                                      </h4>
                                                    </div>
                                                    <div id="collapse_1" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="heading_1">
                                                      <div class="panel-body">
                                                        <div class="col-sm-11" id="add_Qus_Options_div_1">
                                                            <div class="col-sm-4" >
                                                                    '<input name="qus_Options" type="text" class="form-control" style="margin-top: 8px;margin-bottom: 8px;width: 244px;">
                                                            </div>
                                                            <div class="col-sm-11 "  id="newOption_1" style="margin-bottom: 8px;">
                                                                    <a onclick="add_Qus_Options_Click(1,null )">新增選項</a>
                                                            </div>
                                                        </div>
                                                      </div>
                                                    </div>
                                                  </div>
                                                </div>

                                            </div>
                                                <div class="col-sm-3 col-sm-push-3 " style="margin-left: 22px;">
                                                    <div class="row">
                                                        <div class="checkbox checkbox-slider--b-flat checkbox-slider-md" style="float: left; padding-right: 10px;">
                                                            <label>
                                                            <input id="required_checkbox_1" type="checkbox" ><span><a style="font-size:16px;margin-left: -126px;">必填</a></span>
                                                            </label>
                                                        </div>
                                                
                                                        <select class="select_Validation" id="select_Validation_1" style="width:100px;height:34px;border-radius:4px;margin-left: 22px;margin-top: 5px;">
                                                              <option selected="selected" value="">資料驗證(無)</option>
                                                              <option value="cellPhone">手機號碼</option>
                                                              <option value="Email">電子信箱</option>
                                                              <option value="IdNumber">身份證</option>
                                                              <option value="Int">數字</option>
                                                              <option value="Data">日期</option>
                                                            </select>
                                                        <a onclick="del_Qus_click(1)" type="submit" class="btn btn-theme" style="margin-left: 5px;">刪除</a>
                                                    </div>

                                                    <div class="row" id="add_Validation_div_1" style="margin-left: -19px;">

                                                        <label class="col-sm-1 control-label" style="width: 70px;margin-top: 10px;">最小值</label>
                                                        <div class="col-sm-1 " style="width: 58px;margin-top: 10px;">
                                                            <input type="text" id="min_Num_1" class="form-control" style="width: 54px;margin-bottom: 8px;margin-left: -15px" placeholder="可不填"/>
                                                        </div>
                                                        <label class="col-sm-1 control-label " style="width:70px;margin-top: 10px;">最大值</label>
                                                        <div class="col-sm-1 " style="margin-top: 10px;">
                                                            <input type="text" id="max_Num_1" class="form-control" style="width: 54px;margin-bottom: 8px;margin-left: -15px" placeholder="可不填"/>
                                                        </div>

                                                    </div>

                                                </div>
                                        </div>
                                        
                            </div>--%>

                    </div>
                    <!-- 新增問題地方_END-->  
                </div>
                <!-- 預設區塊一_END -->

                <!-- 預設區塊二_START -->
                <div class="col-lg-11 col-md-11 col-sm-11 col-xs-11 dexc showback" id="block_div_2" >
                    <div class="col-sm-1" style="left: 95%;height: 40px;">
                        <a class="btn" style="color: #768094;" onclick="del_block_click(2)">X</a>
                    </div>
                    <h3><input type="text" id ="block_title_txt_2" class="form-control" placeholder="區塊名稱" value="其他"></h3>
                    <input type="text" id="block_Description_txt_2" class="form-control" placeholder="區塊描述">
                    <div class="column form-horizontal style-form" id="add_Qus_div_2" style="min-height:50px">
                    </div>
                </div>
                <!-- 預設區塊二_END -->

                <!-- 功能列_START -->
                <div class="nav nav-pills nav-stacked"  id="suspension_div" data-spy="affix" data-offset-top="60" data-offset-bottom="200">
                    <div class="btn-group-vertical" role="group" aria-label="...">
                        <!-- 新增問題 -->
                        <a type="submit" class="btn btn-theme " onclick="add_Preset_Qus_Click(null , null , null , 'text' , '' , false)" >新增問題</a>
                        <br />
                        <!-- 新增區塊 -->
                        <a type="submit" class="btn btn-theme " onclick="add_Block_Click()">新增區塊</a>
                        <br />
                        <!-- 常用欄位 -->
                        <a data-toggle="modal" data-target="#myModal" data-backdrop="static" role="group"  class="btn btn-theme">常用欄位</a>
                        <%--<br />
                        <!-- 載入範本 -->
                        <a type="submit" class="btn btn-theme " onclick="getText()">載入範本</a>--%>
                    </div>                     
                </div>
                <!-- 功能列_END -->
            </div>        
            <!-- 新增區塊地方_END-->  
        </div>
        
        <!-- 活動報名表標籤_END--> 
    </div>
    <!-- 標籤內容_END -->
</div>
<%--    <!-- 圖片彈出_Modal_START-->
    <div aria-hidden="true" aria-labelledby="ModalLabel" role="dialog" tabindex="-1" id="pictureModal" class="modal fade text-center">
        <div class="modal-dialog" style="display: inline-block; width: auto;">
            <div class="modal-content">
                <div class="modal-body">
                    <img class="img-responsive" src="../Scripts/Lib/assets/img/fcu.jpg" alt="">
                </div>
            </div>
        </div>
    </div>
    <!-- 圖片彈出_Modal_END-->--%>

    <!-- 常用欄位_Modal_START-->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog" style="margin: 2% 30%;padding:0px 0px;border-radius: 6px;">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
            <h4 class="modal-title" id="myModalLabel">常用欄位</h4>
          </div>
          <div class="modal-body">
            <div class="checkbox">
                <label>
                    <input name="check_usually_Column" type="checkbox" value="姓名">
                    姓名
                </label>
            </div>
            <div class="checkbox">
                <label>
                    <input name="check_usually_Column" type="checkbox" value="出生年月日">
                    出生年月日
                </label>
            </div>
            <div class="checkbox">
                <label>
                    <input name="check_usually_Column" type="checkbox" value="身份證字號">
                    身份證字號
                </label>
            </div>
            <div class="checkbox">
                <label>
                    <input name="check_usually_Column" type="checkbox" value="信箱Email">
                    信箱Email
                </label>
            </div>
            <div class="checkbox">
                <label>
                    <input name="check_usually_Column" type="checkbox" value="聯絡電話">
                    聯絡電話
                </label>
            </div>
            <div class="checkbox">
                <label>
                    <input name="check_usually_Column" type="checkbox" value="聯絡地址">
                    聯絡地址
                </label>
            </div>
            <div class="checkbox">
                <label>
                    <input name="check_usually_Column" type="checkbox" value="公司電話">
                    公司電話
                </label>
            </div>
            <div class="checkbox">
                <label>
                    <input name="check_usually_Column" type="checkbox" value="服務單位">
                    服務單位
                </label>
            </div>
            <div class="checkbox">
                <label>
                    <input name="check_usually_Column" type="checkbox" value="職稱">
                    職稱
                </label>
            </div>
            <div class="checkbox">
                <label>
                    <input name="check_usually_Column" type="checkbox" value="傳真">
                    傳真
                </label>
            </div>
            <div class="checkbox">
                <label>
                    <input name="check_usually_Column" type="checkbox" value="用餐">
                    用餐
                </label>
            </div>
              <div class="checkbox">
                <label>
                    <input name="check_usually_Column" type="checkbox" value="備註">
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
    <input type="hidden" id="save_Json_Data" />
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
        var qusId = 1;
        //區塊ID
        var blockId = 3;
        //場次ID
        var sessionId = 1;
        //判斷填寫資料是否正確
        var checkData = true;

        //#region 頁面載入時自動產生問題
        $(document).ready(function () {
            add_Session_click();
            //add_Preset_Qus_Click(題目名稱 , 題目描述 , 欲加入的區塊 , 題目模式 , 資料驗證 ,選項內容 , 是否必填)
            add_Preset_Qus_Click("姓名", "請填寫完整名字", 1, "text" , "" , [], true);
            add_Preset_Qus_Click("出生年月日", "ex:83/07/08", 1, "text", "", [], true);
            add_Preset_Qus_Click("服務單位", "請填寫完整名稱", 1, "text", "", [], true);
            add_Preset_Qus_Click("職稱", null, 1, "text", "", [], true);
            add_Preset_Qus_Click("身份證字號", "英文字母請大寫", 1, "text", "idNumber", [], true);
            add_Preset_Qus_Click("信箱Email", "請填寫正確格式", 1, "text", "email", [], true);
            add_Preset_Qus_Click("連絡電話", "可填手機或是家裡電話", 1, "text", "", [], true);
            add_Preset_Qus_Click("聯絡地址", "請填寫您收的到信的地址", 1, "text", "", [], true);
            add_Preset_Qus_Click("公司電話", null, 1, "text", "", [], false);
            add_Preset_Qus_Click("傳真", null, 1, "text", "", [], false);
            add_Preset_Qus_Click("用餐", null, 1, "singleSelect","", ["葷", "素", "不用餐"], true);
            add_Preset_Qus_Click("備註", "若您有其他的問題,可以在此說明 ", 2, "text", "", [], false);


            $("#activity_Name_txt").blur(function () {
                if ($.trim($(this).val()) == "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#activity_Name_txt_error").length == 0)
                        $(this).after('<em id="activity_Name_txt_error" class="error help-block red" style="width: 52px;margin-bottom: 5px;">必須填寫</em>');
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#activity_Name_txt_error").remove();
                }
            })
        });
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
        function add_Preset_Qus_Click(qus_Title , qus_Desc , add_Block , preset_Qus_Way ,preset_Qus_validation , qus_Option , required) {
            add_Qus_Click(add_Block,required);
            //增加問題內容 change_Qus_Way(問題模式,欲加入題目名稱的地方,欲加入選項的地方,欲加入題目名稱的內容,欲加入選項的第一個內容)
            change_Qus_Way(preset_Qus_Way , qusId , $("#change_Qus_Way_div_" + qusId) , $("#change_Qus_Content_div_" + qusId) , qus_Title , qus_Desc , qus_Option[0]);
            //將選單改為預設所選的
            $("#select_" + qusId).val(preset_Qus_Way);
            $("#select_Validation_" + qusId).val(preset_Qus_validation);
            //將預設文字加入選項中
            if (preset_Qus_Way != "text")
            {
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
                                            '<div class="col-sm-1">' +
                                                //題目模式下拉式選單
                                                '<select class="select" id="select_' + qusId + '" style="width:56px;height:34px;border-radius:4px;margin-left: 50px;">' +
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
                                                '<div class="col-sm-3 col-sm-push-3 " style="margin-left: 22px;">' +
                                                    '<div class="row" >'+
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
                                                              '<option value="data">日期</option>' +
                                                            '</select>' +
                                                        '<a onclick="del_Qus_click(' + qusId + ')" type="submit" class="btn btn-theme" style="margin-left: 5px;">刪除</a>' +
                                                    '</div>' +
                                                    //添加資料驗證數字最大最小值的地方
                                                    '<div class="row" id="add_Validation_div_' + qusId + '" style="margin-left: -25px;">' +
                                                    '</div>' +
                                                '</div>' +
                                        '</div>' +
                                        
                            '</div>');
            //資料驗證選單
            $('#select_Validation_'+qusId).change(function () {
                //抓取現在要更改問題模式的ID
                var str = $(this).attr("id");
                //切割字串抓最後一個陣列即為ID
                var chooseId = str.split("_")[str.split("_").length - 1];
                //儲存欲更改的問題模式
                var qus_Way = $("#select_Validation_" + chooseId).val();
                //判斷如果資料驗證不為數字則要把最大最小值區塊刪掉
                if (qus_Way != "int")
                    $("#add_Validation_div_" + chooseId).children().remove();
                switch (qus_Way) {
                    case "int":
                        $("#add_Validation_div_" + chooseId).append('<label class="col-sm-1 control-label" style="width: 70px;margin-top: 10px;">最小值</label>' +
                                                                    '<div class="col-sm-1" style="width: 58px;margin-top: 10px;">' +
                                                                        '<input type="text" id="min_Num_' + chooseId + '" class="form-control" style="width: 54px;margin-bottom: 8px;margin-left: -15px" placeholder="可不填"/>' +
                                                                    '</div>' +
                                                                    '<label class="col-sm-1 control-label" style="width:70px;margin-top: 10px;">最大值</label>' +
                                                                    '<div class="col-sm-1" style="margin-top: 10px;">' +
                                                                        '<input type="text" id="max_Num_' + chooseId + '" class="form-control" style="width: 54px;margin-bottom: 8px;margin-left: -15px" placeholder="可不填"/>' +
                                                                    '</div>');
                        break;
                    default:
                        break;
                }
            });
            //題目模式選單
            $('#select_'+qusId).change(function () {
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
        function change_Qus_Way(qus_way_int, chooseId , temp_change_Qus_Way_div , temp_change_Qus_Content_div , qus_Title_Value , qus_Desc , present_Qus_Option) {
            //判斷題目模式
            if (qus_way_int == "singleSelect")
                var qus_title = "單選問題";
            else if (qus_way_int == "multiSelect")
                var qus_title = "多選問題";
            else if (qus_way_int == "text")
                var qus_title = "文字問題";
            else if (qus_way_int == "dropDownList")
                var qus_title = "選單問題";
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
                //判斷原來是否存在最大最小值的DIV，如果存在要刪除
                if ($("#add_Validation_div_" + chooseId).children().length > 0)
                    $("#add_Validation_div_" + chooseId).children().remove();
            }
            else
                $('#select_Validation_' + chooseId).attr('disabled', false);
            //移除選擇問題類型區塊內容
            temp_change_Qus_Way_div.children().remove();
            //移除問題內容
            temp_change_Qus_Content_div.children().remove();
            //將問題名稱選項改為單選、多選、選單問題
            temp_change_Qus_Way_div.append('<div class="col-sm-10" style="width: 100%">' +
                                                '<input type="text" ID="qus_txt_' + chooseId + '"  placeholder="' + qus_title + '" class="form-control" style="width: 100%;"' + title_Value + '=' + qus_Title_Value + '>' +
                                            '</div>' +
                                            '<div class="col-sm-10" style="width: 100%">' +
                                                '<input type="text" ID="qus_context_txt_' + chooseId + '" ' + qus_Desc_Value + ' = "' + qus_Desc + '" placeholder="問題描述" class="form-control" style="width: 100%;margin-top: 15px" >' +
                                            '</div>');
            //將問題地方改單選、多選、選單問題，如果為文字問題則不用加
            if (qus_way_int != "text") {
                temp_change_Qus_Content_div.append('<div class="panel-group" id="panel_group_' + chooseId + '" role="tablist" aria-multiselectable="true" style="width: 350px;margin-left: 50px;margin-top: 5px;">' +
                                                  '<div class="panel panel-default">' +
                                                    '<div class="panel-heading" role="tab" id="heading_' + chooseId + '">' +
                                                      '<h4 class="panel-title">' +
                                                        '<a data-toggle="collapse" data-parent="#panel_group_' + chooseId + '" href="#collapse_' + chooseId + '" aria-expanded="true" aria-controls="collapse_' + chooseId + '">' +
                                                          '' + qus_title + '' +
                                                        '</a>' +
                                                      '</h4>' +
                                                    '</div>' +
                                                    '<div id="collapse_' + chooseId + '" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="heading_' + chooseId + '">' +
                                                      '<div class="panel-body">' +
                                                        '<div class="col-sm-11" id="add_Qus_Options_div_' + chooseId + '">' +
                                                            '<div class="col-sm-4" >' +
                                                                    '<input id="qus_Option_' + chooseId + '1" name="qus_Options" type="text" ' + option_Value + '="' + present_Qus_Option + '" class="form-control" style="margin-top: 8px;margin-bottom: 8px;width: 244px;">' +
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
                    if ($("#qus_Option_error_" + chooseId+"1").length==0)
                        $(this).after('<em id="qus_Option_error_' + chooseId + '1" class="error help-block red" style="width: 52px;margin-left: 16px;margin-bottom: 5px;">必須填寫</em>');
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
                                                '<input id="qus_Option_' + id + (count+1) + '" name="qus_Options" type="text" class="form-control" style="width: 244px;margin-bottom: 8px;margin-left: -15px" ' + value + '="' + option_value + '">' +
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
            $("#qus_Option_" + id + (count+1)).blur(function () {
                if($.trim($(this).val())==""){
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#qus_Option_error_" + id + (count+1)).length == 0)
                        $(this).after('<em id="qus_Option_error_' + id + (count + 1) + '" class="error help-block red" style="width: 52px;margin-bottom: 5px;">必須填寫</em>');
                }
                else{
                    $(this).css({ "box-shadow": "" });
                    $("#qus_Option_error_" + id + (count+1)).remove();
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
                                                '<a class="btn" style="color: #768094;" onclick="del_block_click(' + blockId + ')">X</a>' +
                                            '</div>' +
	                                            '<h3><input type="text" id ="block_title_txt_' + blockId + '" class="form-control" placeholder="區塊名稱"></h3>' +
                                                '<input type="text" id="block_Description_txt_' + blockId + '" class="form-control" placeholder="區塊描述">' +
	                                            '<div class="form-horizontal style-form column" id="add_Qus_div_' + blockId + '" style="min-height:50px">' +
	                                            '</div>' +
                                            '</div>');
            $("#block_title_txt_" + blockId).focus();
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
                                    '<input type="text" class="form-control" id="session_Name_txt_' + sessionId + '">' +
                                '</div>' +
                            '</div>' +
                            '<div class="form-group">' +
                                '<label class="col-sm-2 control-label">活動開始日期<b class="red">*</b></label>' +
                                '<div class="col-sm-4">' +
                                    '<input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_Start_txt_' + sessionId + '" />' +
                                '</div>' +
                                '<label class="col-sm-2 control-label">活動結束日期<b class="red">*</b></label>' +
                                '<div class="col-sm-4">' +
                                    '<input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_End_txt_' + sessionId + '" />' +
                                '</div>' +
                            '</div>' +
                            '<div class="form-group">' +
                                '<label class="col-sm-2 control-label">報名開始日期<b class="red">*</b></label>' +
                                '<div class="col-sm-4">' +
                                    '<input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_Sign_Start_txt_' + sessionId + '" />' +
                                '</div>' +
                                '<label class="col-sm-2 control-label">報名結束日期<b class="red">*</b></label>' +
                                '<div class="col-sm-4">' +
                                    '<input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_Sign_End_txt_' + sessionId + '" />' +
                                '</div>' +
                            '</div>' +
                            '<div class="form-group">' +
                                '<label class="col-sm-2 control-label">活動地點<b class="red">*</b></label>' +
                                '<div class="col-sm-10">' +
                                    '<input type="text" class="form-control" id="activity_Location_txt_' + sessionId + '">' +
                                '</div>' +
                            '</div>' +
                            '<div class="form-group">' +
                                '<label class="col-sm-2 control-label">人數限制<b class="red">*</b></label>' +
                                '<div class="col-sm-10">' +
                                    '<input type="text" class="form-control" placeholder="請填寫數字" id="activity_Limit_Num_txt_' + sessionId + '">' +
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
                    lang: 'ch',
                });
            });
            //場次名稱是否填寫的判斷
            $("#session_Name_txt_" + sessionId).blur(function () {
                var choose_Session_Name_temp = $(this).attr("id");
                var choose_Session_Name_txtId = choose_Session_Name_temp.split("_")[choose_Session_Name_temp.split("_").length - 1];
                if ($.trim($(this).val()) == "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#session_Name_txt_error_" + choose_Session_Name_txtId).length == 0)
                        $(this).after('<em id="session_Name_txt_error_' + choose_Session_Name_txtId + '" class="error help-block red" style="width: 52px;margin-bottom: 5px;">必須填寫</em>');
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
                        $(this).after('<em id="activity_Location_txt_error_' + activity_Location_txtId + '" class="error help-block red" style="width: 52px;margin-bottom: 5px;">必須填寫</em>');
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
                if ($.trim($(this).val()) == ""|| isNaN($.trim($(this).val())) || !if_int.test($.trim($(this).val())) ) {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    
                    if ($("#activity_Limit_Num_txt_error_" + activity_Limit_Num_txtId).length == 0)
                        $(this).after('<em id="activity_Limit_Num_txt_error_' + activity_Limit_Num_txtId + '" class="error help-block red" style="width: 105px;margin-bottom: 5px;">必須填寫整數數字</em>');
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
                        $(this).after('<em id="datetimepicker_Activity_Start_txt_error_' + datetimepicker_Activity_Start_txtId + '" class="error help-block red" style="width: 222px;margin-bottom: 5px;">活動開始日期必須在活動結束日期之前</em>');
                    checkData = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_Start_txt_error_" + datetimepicker_Activity_Start_txtId).remove();
                    $("#datetimepicker_Activity_End_txt_" + datetimepicker_Activity_Start_txtId).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_End_txt_error_" + datetimepicker_Activity_Start_txtId).remove();
                }
                //判斷在活動報名開始日期之後
                if ($.trim($(this).val()) <= $.trim($("#datetimepicker_Activity_Sign_Start_txt_" + datetimepicker_Activity_Start_txtId).val()) && $.trim($("#datetimepicker_Activity_Sign_Start_txt_" + datetimepicker_Activity_Start_txtId).val()) != "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#datetimepicker_Activity_Start_txt_error_WithSignStart_" + datetimepicker_Activity_Start_txtId).length == 0)
                        $(this).after('<em id="datetimepicker_Activity_Start_txt_error_WithSignStart_' + datetimepicker_Activity_Start_txtId + '" class="error help-block red" style="width: 222px;margin-bottom: 5px;">活動開始日期必須在報名開始日期之後</em>');
                    checkData = false;
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
                        $(this).after('<em id="datetimepicker_Activity_End_txt_error_' + datetimepicker_Activity_End_txtId + '" class="error help-block red" style="width: 222px;margin-bottom: 5px;">活動結束日期必須在活動開始日期之後</em>');
                    checkData = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_End_txt_error_" + datetimepicker_Activity_End_txtId).remove();
                    $("#datetimepicker_Activity_Start_txt_error_" + datetimepicker_Activity_End_txtId).remove();
                    if ($("#datetimepicker_Activity_Start_txt_error_WithSignStart_" + datetimepicker_Activity_End_txtId).length == 0 && $("#datetimepicker_Activity_Start_txt_error_" + datetimepicker_Activity_End_txtId).length == 0)
                        $("#datetimepicker_Activity_Start_txt_" + datetimepicker_Activity_End_txtId).css({ "box-shadow": "" });
                }
                //判斷在活動報名結束之前
                if ($.trim($(this).val()) <= $.trim($("#datetimepicker_Activity_Sign_End_txt_" + datetimepicker_Activity_End_txtId).val()) && $.trim($("#datetimepicker_Activity_Sign_End_txt_" + datetimepicker_Activity_End_txtId).val()) != "" ) { 
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#datetimepicker_Activity_End_txt_error_withSignEnd_" + datetimepicker_Activity_End_txtId).length == 0)
                        $(this).after('<em id="datetimepicker_Activity_End_txt_error_withSignEnd_' + datetimepicker_Activity_End_txtId + '" class="error help-block red" style="width: 222px;margin-bottom: 5px;">活動結束日期必須在報名結束日期之後</em>');
                    checkData = false;
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
                        $(this).after('<em id="datetimepicker_Activity_Sign_Start_txt_error_' + datetimepicker_Activity_Sign_Start_txt_temp_txtId + '" class="error help-block red" style="width: 222px;margin-bottom: 5px;">報名開始日期必須在報名結束日期之前</em>');
                    checkData = false;
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
                        $(this).after('<em id="datetimepicker_Activity_Sign_Start_txt_error_WithActStart_' + datetimepicker_Activity_Sign_Start_txt_temp_txtId + '" class="error help-block red" style="width: 222px;margin-bottom: 5px;">報名開始日期必須在活動開始日期之前</em>');
                    checkData = false;
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
                        $(this).after('<em id="datetimepicker_Activity_Sign_End_txt_error_' + datetimepicker_Activity_Sign_End_txt_temp_txtId + '" class="error help-block red" style="width: 222px;margin-bottom: 5px;">報名結束日期必須在報名開始日期之後</em>');
                    checkData = false;
                }
                else {
                    $(this).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_Sign_End_txt_error_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).remove();
                    if ($("#datetimepicker_Activity_Sign_Start_txt_error_WithActStart_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).length == 0 && $("#datetimepicker_Activity_Sign_Start_txt_error_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).length == 0)
                        $("#datetimepicker_Activity_Sign_Start_txt_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_Sign_Start_txt_error_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).remove();
                }
                //判斷在活動結束日期之前
                if ($.trim($(this).val()) >= $.trim($("#datetimepicker_Activity_End_txt_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).val()) && $.trim($("#datetimepicker_Activity_End_txt_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).val()) != "") {
                    $(this).css({ "box-shadow": "0px 0px 9px red" });
                    if ($("#datetimepicker_Activity_Sign_End_txt_error_withActEnd_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).length == 0)
                        $(this).after('<em id="datetimepicker_Activity_Sign_End_txt_error_withActEnd_' + datetimepicker_Activity_Sign_End_txt_temp_txtId + '" class="error help-block red" style="width: 222px;margin-bottom: 5px;">報名結束日期必須在活動結束日期之前</em>');
                    checkData = false;
                }
                else {
                    if ($("#datetimepicker_Activity_Sign_End_txt_error_withActEnd_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).length == 0 && $("#datetimepicker_Activity_Sign_End_txt_error_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).length == 0)
                        $(this).css({ "box-shadow": "" });
                    $("#datetimepicker_Activity_Sign_End_txt_error_withActEnd_" + datetimepicker_Activity_Sign_End_txt_temp_txtId).remove();
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
        
        ////#region 日期時間選擇器
        //$(function () {
        //    $('.datetimepicker').datetimepicker({
        //        lang: 'ch',
        //    });
        //});
        ////#endregion

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
                locatioin = 9/10, // 按鈕出現在螢幕的高度
                right = 10, // 距離右邊 px 值
                opacity = 0.8, // 透明度
                speed = 500, // 捲動速度
                $button = $("#goTopButton"),
                $body = $(document),
                $win = $(window);
                $button.attr("src", img);
                $button.on({
                mouseover: function() {$button.css("opacity", 1);},
                mouseout: function() {$button.css("opacity", opacity);},
                click: function() {$("html, body").animate({scrollTop: 0}, speed);}
                });
                window.goTopMove = function () {
                    var scrollH = $body.scrollTop(),
                    winH = $win.height(),
                    css = {"top": winH * locatioin + "px", "position": "fixed", "right": right, "opacity": opacity};
                    if(scrollH > 20) {
                        $button.css(css);
                        $button.fadeIn("slow");
                    } else {
                        $button.fadeOut("slow");
                    }
                };
                $win.on({
                    scroll: function() {goTopMove();},
                    resize: function() {goTopMove();}
            });
        } )();
        //#endregion

        //#region 懸浮區塊
        $('#suspension_div').affix({
            offset: {
                top: 100,
                bottom: function () {
                    return (this.bottom = $('.footer').outerHeight(true))
                }
            }
        });
        //#endregion

        //#region 儲存活動頁面
        function Save_btn_Click() {
            //判斷資料是否正確 1:正確 0:錯誤
            var checkData = true;
            //判斷活動報名結束日期是否在活動開始日期之後
            var checkDataSignEnd = false;
            //錯誤訊息
            var alert_txt = '';
            var alert_txt_all = '';
            //把資料包成List
            var jsondata = { activity_List: [], activity_Session_List: [] };
            //儲存活動場次資訊
            //迴圈依照目前場次ID做為要跑的次數
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
                    //判斷場次名不為空
                    if (!session_Json_Data.As_title) {
                        //alert("場次名不可為空!!");
                        alert_txt += "場次名稱不可為空!!\n";
                        $("#session_Name_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#session_Name_txt_error_" + temp).length == 0)
                            $("#session_Name_txt_" + temp).after('<em id="session_Name_txt_error_' + temp + '" class="error help-block red">必須填寫</em>');
                        checkData = false;
                    }
                    else {
                        $("#session_Name_txt_" + temp).css({ "box-shadow": "" });
                        $("#session_Name_txt_error_" + temp).remove();
                    }
                    //判斷活動地點不為空
                    if (!session_Json_Data.As_position) {
                        //alert("活動地點不可為空!!");
                        alert_txt += "活動地點不可為空!!\n";
                        $("#activity_Location_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#activity_Location_txt_error_" + temp).length == 0)
                            $("#activity_Location_txt_" + temp).after('<em id="activity_Location_txt_error_' + temp + '" class="error help-block red">必須填寫</em>');
                        checkData = false;
                    }
                    else {
                        $("#activity_Location_txt_" + temp).css({ "box-shadow": "" });
                        $("#sactivity_Location_txt_error_" + temp).remove();
                    }
                    //判斷報名人數限制不為空以及要是整數數字
                    var if_int = /^[0-9]*[1-9][0-9]*$/;
                    if (!session_Json_Data.As_num_limit || isNaN(session_Json_Data.As_num_limit) || !if_int.test(session_Json_Data.As_num_limit)) {
                        //alert("報名人數限制不可為空!!");
                        alert_txt += "報名人數限制不可為空!!\n";
                        $("#activity_Limit_Num_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#activity_Limit_Num_txt_error_" + temp).length == 0)
                            $("#activity_Limit_Num_txt_" + temp).after('<em id="activity_Limit_Num_txt_error_' + temp + '" class="error help-block red">必須填寫整數數字</em>');
                        checkData = false;
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
                        checkData = false;
                    }
                    //判斷活動結束日期不能為空
                    if (!session_Json_Data.As_date_end) {
                        $("#datetimepicker_Activity_End_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#datetimepicker_Activity_End_txt_error_" + temp).length == 0)
                            $("#datetimepicker_Activity_End_txt_" + temp).after('<em id="datetimepicker_Activity_End_txt_error_' + temp + '" class="error help-block red">必需填寫</em>');
                        checkData = false;
                    }
                    //判斷活動報名開始日期不能為空
                    if (!session_Json_Data.As_apply_start) {
                        $("#datetimepicker_Activity_Sign_Start_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#datetimepicker_Activity_Sign_Start_txt_error_" + temp).length == 0)
                            $("#datetimepicker_Activity_Sign_Start_txt_" + temp).after('<em id="datetimepicker_Activity_Sign_Start_txt_error_' + temp + '" class="error help-block red">必需填寫</em>');
                        checkData = false;
                    }
                    //判斷活動報名結束日期不能為空
                    if (!session_Json_Data.As_apply_end) {
                        alert_txt += "報名結束日期不可為空!!\n";
                        $("#datetimepicker_Activity_Sign_End_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#datetimepicker_Activity_Sign_End_txt_error_" + temp).length == 0)
                            $("#datetimepicker_Activity_Sign_End_txt_" + temp).after('<em id="datetimepicker_Activity_Sign_End_txt_error_' + temp + '" class="error help-block red">必需填寫</em>');
                        checkData = false;
                    }
                    //判斷活動開始日期是否與活動報名開始日期一樣
                    if (session_Json_Data.As_apply_start >= session_Json_Data.As_date_start && session_Json_Data.As_apply_start && session_Json_Data.As_date_start) {
                        $("#datetimepicker_Activity_Start_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#datetimepicker_Activity_Start_txt_error_WithSignStart_" + temp).length == 0)
                            $("#datetimepicker_Activity_Start_txt_" + temp).after('<em id="datetimepicker_Activity_Start_txt_error_WithSignStart_' + temp + '" class="error help-block red">活動開始時間不能相同於報名開始時間</em>');
                        $("#datetimepicker_Activity_Sign_Start_txt_" + temp).css({ "box-shadow": "0px 0px 9px red" });
                        if ($("#datetimepicker_Activity_Sign_Start_txt_error_WithActStart_" + temp).length == 0)
                            $("#datetimepicker_Activity_Sign_Start_txt_" + temp).after('<em id="datetimepicker_Activity_Sign_Start_txt_error_WithActStart_' + temp + '" class="error help-block red">報名開始時間不能相同於活動開始時間</em>');
                        checkData = false;
                    }
                    if (session_Json_Data.As_date_start != "" && session_Json_Data.As_date_end != "" && session_Json_Data.As_apply_start != "" && session_Json_Data.As_apply_end != ""){
                        if (session_Json_Data.As_date_start < session_Json_Data.As_date_end && session_Json_Data.As_date_start > session_Json_Data.As_apply_start && session_Json_Data.As_apply_start < session_Json_Data.As_apply_end && session_Json_Data.As_apply_start < session_Json_Data.As_date_end && session_Json_Data.As_apply_end < session_Json_Data.As_date_end) {
                            checkData = true;
                        }
                        else
                            checkData = false;
                    }
                    else
                        checkData = false;

                    if (session_Json_Data.As_apply_end > session_Json_Data.As_date_start)
                        checkDataSignEnd = true;


                    if (checkData === false) alert_txt_all += "場次" + temp + "\n" + alert_txt;
                    
                    alert_txt = '';
                    //將場次資料push進jsonData裡面
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
            activity_Json_Data.Act_desc = $("#editor1").val();
            //儲存主辦單位
            activity_Json_Data.Act_unit = $("#unit_txt").val();
            //儲存聯絡人
            activity_Json_Data.Act_contact_name = $("#contact_Person_txt").val();
            //儲存聯絡人電話
            activity_Json_Data.Act_contact_phone = $("#contact_Person_Phone_txt").val();
            //儲存相關連結
            activity_Json_Data.Act_relate_link = $("#relate_Link").val();
            jsondata.activity_List.push(activity_Json_Data);
            //判斷活動名稱不能為空
            if (!activity_Json_Data.Act_title) {
                //alert("活動名稱不可為空!!");
                alert_txt_all += "\n活動名稱不可為空!!\n";
                $("#activity_Name_txt").css({ "box-shadow": "0px 0px 9px red" });
                if ($("#activity_Name_txt_error").length == 0)
                    $("#activity_Name_txt").after('<em id="activity_Name_txt_error" class="error help-block red">必須填寫</em>');
                checkData = false;
            }
            else {
                $("#activity_Name_txt").css({ "box-shadow": "" });
                $("#activity_Name_txt_error" + temp).remove();
            }
           
            //如果資料正確，使用jQuery ajax傳送資料
            if (checkDataSignEnd == true) {
                var if_Save = confirm("您的報名結束日期在活動開始日期之後，確定要儲存嗎?");
                if (if_Save == true) {
                    if (checkData === true) {
                        $.ajax({
                            type: 'post',
                            traditional: true,
                            //將資料傳到後台save_Activity這個function
                            url: '/S02/S02010201.aspx/save_Activity',
                            data: JSON.stringify(jsondata),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            //成功時
                            success: function (result) {
                                upload_File();
                                alert(result.d);
                            },
                            //失敗時
                            error: function () {
                                alert("失敗");
                            }
                        });
                    }
                    else {
                        alert("資料有誤");
                    }
                }
            }
            else {
                if (checkData === true) {
                    $.ajax({
                        type: 'post',
                        traditional: true,
                        //將資料傳到後台save_Activity這個function
                        url: '/S02/S02010201.aspx/save_Activity',
                        data: JSON.stringify(jsondata),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        //成功時
                        success: function (result) {
                            upload_File();
                            alert(result.d);
                        },
                        //失敗時
                        error: function () {
                            alert("失敗");
                        }
                    });
                }
                else {
                    alert("資料有誤");
                }
            }
        };
        //#endregion

        //#region 儲存報名表
        function Save_Activity_btn_Click() {
            //var checkData = true;
            var jsondata = save_Activity_Column();
            //使用ajax傳送
            if (checkData === true)
            {
                $.ajax({
                    type: 'post',
                    traditional: true,
                    //傳送資料到後台為save_Activity_Form的function
                    url: '/S02/S02010201.aspx/save_Activity_Form',
                    data: JSON.stringify(jsondata),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //成功時
                    success: function (result) {
                        alert(result.d);
                    },
                    //失敗時
                    error: function () {
                        alert("失敗!!!!");
                    }
                });
            }
            else if (checkData === false)
            {
                alert("您有資料尚未填寫!!")
            }
        }
        //#endregion 

        //#region 儲存報名表成JSON
        function save_Activity_Column() {
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
                            checkData = false;
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
                                checkData = false;
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
                            if ($("#select_Validation_" + chooseId).val() == "Int") {
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
                                        checkData = false;
                                    }
                                    else {
                                        $("#max_Num_" + chooseId).css({ "box-shadow": "" });
                                        $("#max_Num_error_" + chooseId).remove();
                                    }

                                }
                                else
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
                                        checkData = false;
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
                        checkData = false;
                        alert("您有空白區塊，請新增問題或是刪除區塊!!");
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

        //#region 檢視報名表
        function view_Activity() {
            //var json = JSON.stringify(save_Activity_Column());
            //$("#save_Json_Data").val(json);
            //window.open("S02010202.aspx?sys_id=S01&sys_pid=S02010202");
        }
        //#endregion
    </script>

</asp:Content>
