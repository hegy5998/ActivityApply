<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S02010101.aspx.cs" Inherits="Web.S02.S02010101" %>

<%@ Import Namespace="Util" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/UCSystemModule.ascx" TagPrefix="uc1" TagName="UCSystemModule" %>
<%@ Register Src="~/UserControls/UCGridViewPager.ascx" TagPrefix="uc1" TagName="UCGridViewPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
    <style type="text/css">
        .activity_title {
            width: 150px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            border-color: transparent;
            background-color: transparent;
            box-shadow: none;
            color: gray;
            text-align: center;
        }

        .scroll_box {
            width: auto;
            height: 250px;
            overflow-x: auto;
            overflow-y: auto;
            border-style: ridge;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            });
        });
    </script>

    <%--查看報名資料--%>
    <script type="text/javascript">
        function GoTo(id) {
            window.open("S02010102.aspx?sys_id=S02&sys_pid=S02010102&i=" + id, "_blank");
        }
    </script>

    <%-- 檢視活動 --%>
    <script type="text/javascript">
        function GoToActivity(id) {
            window.open("S02010104.aspx?sys_id=S02&sys_pid=S02010104&i=" + id, "_blank");
        }
    </script>

    <%--將頁籤記錄到cookie--%>
    <script type="text/javascript">
        $(function () {
            //tabs頁籤 使用cookie記住最後開啟的頁籤
            $("#tabs").tabs({
                //起始頁active: 導向cookie("tabs")所指頁籤
                //active: ($.cookie("tabs") || 0),  
                active: (1), 
 
                //換頁動作activate
                activate: function (event, ui) {   
                    //取得選取的頁籤編號
                    var newIndex = ui.newTab.parent().children().index(ui.newTab);  
 
                    //記錄到cookie
                    $.cookie("tabs", newIndex, { expires: 1 });                        
                }
            });          
        })
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="mainContentSubFunction_cph" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent_cph" runat="server">
    <asp:Panel ID="form_panel" runat="server" Visible="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="sidebar-search" style="padding: 6px 0px 0px 12px; margin: 0px 0px 14px 10px; width: 354px; background-color: white; height: 40px; border-radius: 5px;">
                    <div class="input-group custom-search-form">
                        <span class="input-group-btn">
                            <i class="fa fa-search btn btn-primary btn-xs" aria-hidden="true" style="width: 55px; margin-bottom: 0px;">
                                <label style="margin-top: 3px; font-size: initial;">查詢</label>
                            </i>
                            <asp:Button runat="server" ID="Button1" CssClass="movedown" Text="查詢" OnClick="q_query_btn_Click" />
                        </span>
                        <span class="input-group-btn" style="font-size: initial; z-index: 3;">
                            <asp:DropDownList runat="server" ID="q_keyword_ddl" Style="width: 76px; height: 28px; margin-right: -1px;"></asp:DropDownList>
                        </span>
                        <asp:TextBox runat="server" ID="q_keyword_tb" Style="width: 200px; height: 28px;" CssClass="form-control" />
                    </div>
                    <!-- /input-group -->
                </div>

                <!-- **********************************************************************************************************************************************************
                MAIN CONTENT
                *********************************************************************************************************************************************************** -->
                <!--main content start-->
                <div class="row">
                    <div class="content-panel" style="box-shadow: none;">
                        <!--頁籤 START-->
                        <div role="tabpanel" id="tabs" style="margin-top: 15px;">
                            <!-- 頁籤標題 START -->
                            <ul class="nav nav-pills" role="tablist" id="myTab">
                                <li role="presentation"><a href="#already" aria-controls="already" role="tab" data-toggle="pill">已發佈</a></li>
                                <li role="presentation" class="active"><a href="#ready" aria-controls="ready" role="tab" data-toggle="pill">未發佈</a></li>
                                <li role="presentation"><a href="#end" aria-controls="end" role="tab" data-toggle="pill">已結束</a></li>
                            </ul>
                            <!-- 頁籤標題 END -->

                            <div class="tab-content">
                                <!--已發佈活動 START-->
                                <div role="tabpanel" class="tab-pane active" id="already">
                                    <br />
                                    <asp:UpdatePanel ID="upl" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <asp:GridView ID="main_gv" runat="server" class="table table-striped" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowCommand="main_gv_RowCommand" OnRowDeleting="main_gv_RowDeleting" OnSorting="main_gv_Sorting" ViewStateMode="Enabled" OnRowEditing="main_gv_RowClose">
                                                <Columns>
                                                    <%-- 活動標題 --%>
                                                    <asp:TemplateField HeaderText="活動標題" SortExpression="act_title">
                                                        <ItemTemplate>
                                                            <a title="<%# Eval("act_title") %>">
                                                                <input id="viewActiviity" type="button" class="btn-link" value='<%# Eval("Act_title") %>' onclick="GoToActivity(<%# Eval("act_idn") %>);" style="width: 150px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;" /></a>
                                                            <asp:HiddenField ID="as_idn_hf" runat="server" Value='<%# Eval("as_idn") %>' />
                                                            <asp:HiddenField ID="as_isopen_hf" runat="server" Value='<%# Eval("as_isopen") %>' />
                                                            <asp:HiddenField ID="act_idn_hf" runat="server" Value='<%# Eval("act_idn") %>' />
                                                            <asp:HiddenField ID="act_isopen_hf" runat="server" Value='<%# Eval("act_isopen") %>' />
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="150px" />
                                                        <ItemStyle CssClass="center" Height="50" />
                                                    </asp:TemplateField>

                                                    <%-- 場次 --%>
                                                    <asp:TemplateField HeaderText="場次" SortExpression="as_title">
                                                        <ItemTemplate>
                                                            <a title="<%# Eval("as_title") %>">
                                                                <input id="as_title_lbl" type="text" readonly="true" class="activity_title" value='<%# Eval("as_title") %>' style="cursor:default;background-color: transparent;"/>
                                                                <%--<p style="width: 150px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis; margin: 0px;">
                                                                    <asp:Label ID="as_title_lbl" runat="server" Text='<%# Bind("as_title") %>' Style="cursor: default; color: #808080;"></asp:Label>
                                                                </p>--%>
                                                            </a>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="150px" />
                                                        <ItemStyle CssClass="center" Height="50" />
                                                    </asp:TemplateField>

                                                    <%-- 報名人數 --%>
                                                    <asp:TemplateField HeaderText="報名人數" SortExpression="as_num">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_num" runat="server" Text='<%# Bind("as_num") %>'></asp:Label>/<asp:Label ID="as_num_limit_lbl" runat="server" Text='<%# Bind("as_num_limit") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle CssClass="center" Height="50" />
                                                    </asp:TemplateField>

                                                    <%-- 活動日期 --%>
                                                    <asp:TemplateField HeaderText="活動日期" SortExpression="as_date_start">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_date_start_lbl" runat="server" Text='<%# Bind("as_date_start") %>'></asp:Label>~<asp:Label ID="as_date_end_lbl" runat="server" Text='<%# Bind("as_date_end") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="140px" />
                                                        <ItemStyle CssClass="center" Height="50" />
                                                    </asp:TemplateField>

                                                    <%-- 報名起訖 --%>
                                                    <asp:TemplateField HeaderText="報名起訖" SortExpression="as_apply_start">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_apply_start_lbl" runat="server" Text='<%# Bind("as_apply_start") %>'></asp:Label>~<asp:Label ID="as_apply_end_lbl" runat="server" Text='<%# Bind("as_apply_end") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="140px" />
                                                        <ItemStyle CssClass="center" Height="50" />
                                                    </asp:TemplateField>

                                                    <%-- 報名資料 --%>
                                                    <asp:TemplateField HeaderText="報名資料">
                                                        <ItemTemplate>
                                                            <input id="checkApply" type="button" class="btn-link" value="查看" onclick="GoTo(<%# Eval("as_idn") %>);" />/<asp:Button ID="download_btn" runat="server" CommandName="download" class="btn-link" Text="下載" ToolTip="下載" UseSubmitBehavior="False" CommandArgument='<%# Eval("as_idn") %>' />
                                                            <asp:UpdatePanel runat="server" ID="download_upd">
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="download_btn" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="110px" />
                                                        <ItemStyle CssClass="center" Height="50" />
                                                    </asp:TemplateField>

                                                    <%-- 基本功能 --%>
                                                    <asp:TemplateField HeaderText="基本功能">
                                                        <ItemTemplate>
                                                            <asp:Button ID="editActivity_btn" runat="server" CommandName="EditActivity" class="btn-link" Text="修改" ToolTip="修改" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" CommandArgument='<%# Eval("act_idn") %>' />/<asp:Button ID="delete_btn" runat="server" CommandName="Delete" class="btn-link" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" CommandArgument='<%# Eval("act_idn") %>' />/<asp:Button ID="edit_btn" runat="server" CommandName="Edit" class="btn-link" OnClientClick="if (!confirm(&quot;確定要關閉嗎?&quot;)) return false" Text="關閉" ToolTip="關閉" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" CommandArgument='<%# Eval("act_idn") %>' />/<asp:Button ID="set_btn" runat="server" CommandName="Set" class="btn-link" Text="協作者" ToolTip="協作者" UseSubmitBehavior="False" CommandArgument='<%# Eval("act_idn") %>' OnPreRender="ManageControlAuth" />
                                                            <asp:HiddenField ID="createid_hf" runat="server" Value='<%# Eval("createid") %>' />
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="230px" />
                                                        <ItemStyle CssClass="center" Height="50" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                            <%-- 分頁 --%>
                                            <uc1:UCGridViewPager runat="server" ID="ucGridViewPager" />

                                            <!-- 宣告協作者彈出視窗 -->
                                            <asp:ModalPopupExtender ID="controlSet_mpe" runat="server" PopupControlID="controlSet_pl" TargetControlID="controlSet_OK_btn" BackgroundCssClass="popupWindowOverlay" OkControlID="controlSet_cancel_btn"></asp:ModalPopupExtender>

                                            <!-- 協作者 START -->
                                            <asp:Panel ID="controlSet_pl" class="popupWindow" runat="server" Visible="false">
                                                <!-- 協作者標頭 START -->
                                                <div class="popupWindowHeader">
                                                    <!-- 標題 -->
                                                    <div class="title">
                                                        <asp:UpdatePanel ID="controlSet_title_upl" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Label ID="controlSet_tilte_lbl" runat="server" Text="管理協作者"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                    <!-- 關閉按鈕 -->
                                                    <asp:Button ID="controlSet_cancel_btn" CssClass="close" runat="server" Text="關閉" ToolTip="關閉" OnClick="controlSet_cancel_btn_Click" UseSubmitBehavior="False" />
                                                    <asp:Button ID="controlSet_OK_btn" CssClass="open" runat="server" Text="開啟" ToolTip="開啟" UseSubmitBehavior="False" />
                                                </div>
                                                <!-- 協作者標頭 END -->

                                                <!-- 協作者內容 START -->
                                                <div class="popupWindowContent">
                                                    <asp:UpdatePanel ID="controlSet_content_upl" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="main_pl" runat="server" Visible="false">
                                                                <%--多重view START--%>
                                                                <asp:MultiView ID="mv" runat="server">
                                                                    <%--主要view(GridView) START--%>
                                                                    <asp:View ID="main_view" runat="server">
                                                                        <!-- 設定協作者的key -->
                                                                        <div>
                                                                            <asp:HiddenField ID="copperate_cop_act_hf" runat="server" />
                                                                            <asp:HiddenField ID="new_hf" runat="server" />
                                                                        </div>

                                                                        <!-- GridView START -->
                                                                        <asp:GridView ID="controlSet_gv" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="controlSet_gv_RowCancelingEdit" OnRowCommand="controlSet_gv_RowCommand" OnRowEditing="controlSet_gv_RowEditing" OnRowUpdating="controlSet_gv_RowUpdating" ShowHeaderWhenEmpty="True" OnRowDeleting="controlSet_gv_RowDeleting" OnRowDataBound="controlSet_gv_RowDataBound" OnSorting="controlSet_gv_Sorting" OnRowCreated="controlSet_gv_RowCreated">
                                                                            <Columns>
                                                                                <%--操作--%>
                                                                                <asp:TemplateField ShowHeader="False" HeaderText="操作">
                                                                                    <EditItemTemplate>
                                                                                        <span style="position: relative;">
                                                                                            <i class="fa fa-floppy-o btn btn-success btn-xs" aria-hidden="true"></i>
                                                                                            <asp:Button ID="save_btn" runat="server" CommandName="Update" CssClass="movedown" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                                                                                        </span>
                                                                                        &nbsp;                   
                                                                                       
                                                                                        <span style="position: relative;">
                                                                                            <i class="fa fa-undo btn btn-warning btn-xs" aria-hidden="true"></i>
                                                                                            <asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="movedown" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                                                                                        </span>
                                                                                        <asp:HiddenField ID="old_cop_act_hf" runat="server" Value='<%# Eval("cop_act") %>' />
                                                                                        <asp:HiddenField ID="old_cop_id_hf" runat="server" Value='<%# Eval("cop_id") %>' />
                                                                                    </EditItemTemplate>

                                                                                    <FooterTemplate>
                                                                                        <span style="position: relative;">
                                                                                            <i class="fa fa-floppy-o btn btn-success btn-xs" aria-hidden="true"></i>
                                                                                            <asp:Button ID="save_btn" runat="server" CommandName="AddSave" CssClass="movedown" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                                                                                        </span>
                                                                                        &nbsp;                   
                                                                                       
                                                                                        <span style="position: relative;">
                                                                                            <i class="fa fa-undo btn btn-warning btn-xs" aria-hidden="true"></i>
                                                                                            <asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="movedown" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                                                                                        </span>
                                                                                    </FooterTemplate>

                                                                                    <HeaderTemplate>
                                                                                        <span style="position: relative;">
                                                                                            <i class="fa fa-plus btn btn-info btn-xs" aria-hidden="true"></i>
                                                                                            <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="movedown" Text="新增" ToolTip="新增" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                                                                                        </span>
                                                                                    </HeaderTemplate>

                                                                                    <ItemTemplate>
                                                                                        <span style="position: relative;">
                                                                                            <i class="fa fa-pencil btn btn-primary btn-xs" aria-hidden="true"></i>
                                                                                            <asp:Button ID="edit_btn" runat="server" CommandName="Edit" CssClass="movedown" Text="編輯" ToolTip="編輯" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                                                                                        </span>
                                                                                        &nbsp;
                                                                                       
                                                                                        <span style="position: relative;">
                                                                                            <i class="fa fa-trash-o btn btn-danger btn-xs" aria-hidden="true"></i>
                                                                                            <asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="movedown" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                                                                                        </span>
                                                                                        <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                                                                                        <%--<asp:HiddenField ID="cop_act_hf" runat="server" Value='<%# Eval("cop_act") %>' />--%>
                                                                                    </ItemTemplate>

                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                    <HeaderStyle Width="70px" />
                                                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                                                </asp:TemplateField>

                                                                                <%--帳號--%>
                                                                                <asp:TemplateField HeaderText="帳號" SortExpression="cop_id">
                                                                                    <EditItemTemplate>
                                                                                        <asp:Button ID="account_btn" runat="server" Text="請選擇" CommandName="account" CssClass="btn-link" UseSubmitBehavior="false" CommandArgument='<%# Bind("cop_id") %>' />
                                                                                        <asp:Label ID="accountEdit_lbl" runat="server" Text=""></asp:Label>
                                                                                    </EditItemTemplate>

                                                                                    <FooterTemplate>
                                                                                        <asp:Button ID="account_btn" runat="server" Text="請選擇" CommandName="account" CssClass="btn-link" UseSubmitBehavior="false" CommandArgument='<%# Bind("cop_id") %>' />
                                                                                        <asp:Label ID="account_lbl" runat="server" Text=""></asp:Label>
                                                                                    </FooterTemplate>

                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="cop_id_lbl" runat="server" Text='<%# Bind("cop_id") %>'></asp:Label>
                                                                                    </ItemTemplate>

                                                                                    <HeaderStyle />
                                                                                    <ItemStyle CssClass="rowTrigger center" />
                                                                                </asp:TemplateField>

                                                                                <%--權限--%>
                                                                                <asp:TemplateField HeaderText="權限" SortExpression="cop_authority">
                                                                                    <EditItemTemplate>
                                                                                        <asp:DropDownList ID="copEdit_authority_dll" runat="server"></asp:DropDownList>
                                                                                    </EditItemTemplate>

                                                                                    <FooterTemplate>
                                                                                        <asp:DropDownList ID="cop_authority_dll" runat="server" Text='<%# Bind("cop_authority") %>'></asp:DropDownList>
                                                                                    </FooterTemplate>

                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="cop_authority_lbl" runat="server" Text='<%# Bind("cop_authority") %>'></asp:Label>
                                                                                    </ItemTemplate>

                                                                                    <ItemStyle CssClass="rowTrigger center" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <!-- GridView END -->
                                                                    </asp:View>
                                                                    <%--主要view(GridView)--%>

                                                                    <%--設定帳號 START--%>
                                                                    <asp:View ID="auth_view" runat="server">
                                                                        <asp:Panel ID="account_pl" runat="server">
                                                                            <asp:Label ID="accountpassword_lbl" runat="server" Text="選擇帳號"></asp:Label><br />
                                                                            <br />
                                                                            <div class="scroll_box">
                                                                                <asp:RadioButtonList ID="account_radiobuttonlist" runat="server" CssClass="fv " RepeatDirection="Horizontal" RepeatColumns="2" Width="75%">
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <asp:HiddenField ID="row_idn_hf" runat="server" />
                                                                        </asp:Panel>
                                                                        <asp:Button ID="setAccount_btn" runat="server" CssClass="btn btn-default" Text="確認" UseSubmitBehavior="false" OnClick="setAccount_btn_Click" />
                                                                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-default" Text="返回" UseSubmitBehavior="false" OnClick="back_btn_Click" /><br />
                                                                    </asp:View>
                                                                    <%--設定帳號 END--%>
                                                                </asp:MultiView>
                                                                <%--多重view END--%>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <!-- 協作者內容 END -->
                                            </asp:Panel>
                                            <!-- 協作者 END -->
                                        </ContentTemplate>

                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="main_gv" EventName="DataBound" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <br />
                                </div>
                                <!--已發佈活動 END-->

                                <!--未發佈活動 START-->
                                <div role="tabpanel" class="tab-pane" id="ready">
                                    <br />

                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <asp:GridView ID="ready_gv" class="table table-striped" runat="server" AutoGenerateColumns="False" OnRowCommand="main_gv_RowCommand" ViewStateMode="Enabled" OnRowDeleting="main_gv_RowDeleting" OnSorting="main_gv_Sorting" OnRowEditing="main_gv_RowClose">
                                                <Columns>
                                                    <%-- 活動標題 --%>
                                                    <asp:TemplateField HeaderText="活動標題" SortExpression="act_title">
                                                        <ItemTemplate>
                                                            <a title="<%# Eval("act_title") %>">
                                                                <input id="viewActiviity" type="button" class="btn-link" value='<%# Eval("Act_title") %>' onclick="GoToActivity(<%# Eval("act_idn") %>);" style="width: 150px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;" /></a>
                                                            <asp:HiddenField ID="as_idn_hf" runat="server" Value='<%# Eval("as_idn") %>' />
                                                            <asp:HiddenField ID="as_isopen_hf" runat="server" Value='<%# Eval("as_isopen") %>' />
                                                            <asp:HiddenField ID="act_idn_hf" runat="server" Value='<%# Eval("act_idn") %>' />
                                                            <asp:HiddenField ID="act_isopen_hf" runat="server" Value='<%# Eval("act_isopen") %>' />
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="150px" />
                                                        <ItemStyle CssClass="center" Height="50px" />
                                                    </asp:TemplateField>

                                                    <%-- 場次 --%>
                                                    <asp:TemplateField HeaderText="場次" SortExpression="as_title">
                                                        <ItemTemplate>
                                                            <a title="<%# Eval("as_title") %>">
                                                                <input id="as_title_lbl" type="text" readonly="true" class="activity_title" value='<%# Eval("as_title") %>' style="cursor:default;background-color: transparent;"/>
                                                                <%--<p style="width: 150px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis; margin: 0px;">
                                                                    <asp:Label ID="as_title_lbl" runat="server" Text='<%# Bind("as_title") %>' Style="cursor: default; color: #808080;"></asp:Label>
                                                                </p>--%>
                                                            </a>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="150px" />
                                                        <ItemStyle CssClass="center" Height="50px" />
                                                    </asp:TemplateField>

                                                    <%-- 報名/限制人數 --%>
                                                    <asp:TemplateField HeaderText="報名人數" SortExpression="as_num">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_num" runat="server" Text='<%# Bind("as_num") %>'></asp:Label>/<asp:Label ID="as_num_limit_lbl" runat="server" Text='<%# Bind("as_num_limit") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle CssClass="center" Height="50px" />
                                                    </asp:TemplateField>

                                                    <%-- 活動日期 --%>
                                                    <asp:TemplateField HeaderText="活動日期" SortExpression="as_date_start">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_date_start_lbl" runat="server" Text='<%# Bind("as_date_start") %>'></asp:Label>~<asp:Label ID="as_date_end_lbl" runat="server" Text='<%# Bind("as_date_end") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="140px" />
                                                        <ItemStyle CssClass="center" Height="50px" />
                                                    </asp:TemplateField>

                                                    <%-- 報名起訖 --%>
                                                    <asp:TemplateField HeaderText="報名起訖" SortExpression="as_apply_start">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_apply_start_lbl" runat="server" Text='<%# Bind("as_apply_start") %>'></asp:Label>~<asp:Label ID="as_apply_end_lbl" runat="server" Text='<%# Bind("as_apply_end") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="140px" />
                                                        <ItemStyle CssClass="center" Height="50px" />
                                                    </asp:TemplateField>

                                                    <%-- 報名資料 --%>
                                                    <asp:TemplateField HeaderText="報名資料">
                                                        <ItemTemplate>
                                                            <input id="checkApply" type="button" class="btn-link" value="查看" onclick="GoTo(<%# Eval("as_idn") %>);" />/<asp:Button ID="download_btn" runat="server" CommandName="download" class="btn-link" Text="下載" ToolTip="下載" UseSubmitBehavior="False" CommandArgument='<%# Eval("as_idn") %>' />
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="110px" />
                                                        <ItemStyle CssClass="center" Height="50px" />
                                                    </asp:TemplateField>

                                                    <%-- 基本功能 --%>
                                                    <asp:TemplateField HeaderText="基本功能">
                                                        <ItemTemplate>
                                                            <asp:Button ID="editActivity_btn" runat="server" CommandName="EditActivity" class="btn-link" Text="修改" ToolTip="修改" UseSubmitBehavior="False" CommandArgument='<%# Eval("act_idn") %>' OnPreRender="ManageControlAuth" />/<asp:Button ID="delete_btn" runat="server" CommandName="Delete" class="btn-link" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" CommandArgument='<%# Eval("act_idn") %>' OnPreRender="ManageControlAuth" />/<asp:Button ID="edit_btn" runat="server" CommandName="Edit" class="btn-link" OnClientClick="if (!confirm(&quot;確定要發佈嗎?&quot;)) return false" Text="發佈" ToolTip="發佈" UseSubmitBehavior="False" CommandArgument='<%# Eval("act_idn") %>' OnPreRender="ManageControlAuth" />/<asp:Button ID="set_btn" runat="server" CommandName="Set_ready" class="btn-link" Text="協作者" ToolTip="協作者" UseSubmitBehavior="False" CommandArgument='<%# Eval("act_idn") %>' OnPreRender="ManageControlAuth" />
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="230px" />
                                                        <ItemStyle CssClass="center" Height="50px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                            <%-- 分頁 --%>
                                            <uc1:UCGridViewPager runat="server" ID="ucGridViewPagerReady" />

                                            <!-- 宣告協作者彈出視窗 -->
                                            <asp:ModalPopupExtender ID="ready_copperate_pop" runat="server" PopupControlID="readyCopperate_pl" TargetControlID="readyCopperate_OK_btn" BackgroundCssClass="popupWindowOverlay" OkControlID="readyCopperate_cancel_btn"></asp:ModalPopupExtender>

                                            <!-- 協作者 START -->
                                            <asp:Panel ID="readyCopperate_pl" class="popupWindow" runat="server" Visible="false">
                                                <!-- 協作者標頭 START -->
                                                <div class="popupWindowHeader">
                                                    <!-- 標題 -->
                                                    <div class="title">
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Label ID="readyCopperate_title" runat="server" Text="管理協作者"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                    <!-- 關閉按鈕 -->
                                                    <asp:Button ID="readyCopperate_OK_btn" CssClass="close" runat="server" Text="關閉" ToolTip="關閉" OnClick="controlSet_cancel_btn_Click" UseSubmitBehavior="False" />
                                                    <asp:Button ID="readyCopperate_cancel_btn" CssClass="open" runat="server" Text="開啟" ToolTip="開啟" UseSubmitBehavior="False" />
                                                </div>
                                                <!-- 協作者標頭 END -->

                                                <!-- 協作者內容 START -->
                                                <div class="popupWindowContent">
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="ready_pl" runat="server" Visible="false">
                                                                <%--多重view START--%>
                                                                <asp:MultiView ID="ready_mv" runat="server">
                                                                    <%--主要view(GridView) START--%>
                                                                    <asp:View ID="ready_view" runat="server">
                                                                        <!-- 設定協作者的key -->
                                                                        <div>
                                                                            <asp:HiddenField ID="cop_act_ready_hf" runat="server" />
                                                                            <asp:HiddenField ID="readynew_hf" runat="server" />
                                                                        </div>

                                                                        <!-- GridView START -->
                                                                        <asp:GridView ID="ready_copperate" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="controlSetReady_gv_RowCancelingEdit" OnRowCommand="controlSet_gv_RowCommand" OnRowEditing="controlSetReady_gv_RowEditing" OnRowUpdating="controlSetReady_gv_RowUpdating" ShowHeaderWhenEmpty="True" OnRowDeleting="controlSetReady_gv_RowDeleting" OnRowDataBound="ready_gv_RowDataBound" OnSorting="controlSet_gv_Sorting" OnRowCreated="controlSet_gv_RowCreated">
                                                                            <Columns>
                                                                                <%--操作--%>
                                                                                <asp:TemplateField ShowHeader="False" HeaderText="操作">
                                                                                    <EditItemTemplate>
                                                                                        <span style="position: relative;">
                                                                                            <i class="fa fa-floppy-o btn btn-success btn-xs" aria-hidden="true"></i>
                                                                                            <asp:Button ID="readysave_btn" runat="server" CommandName="Update" CssClass="movedown" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                                                                                        </span>
                                                                                        &nbsp;                   
                                                                           
                                                                                        <span style="position: relative;">
                                                                                            <i class="fa fa-undo btn btn-warning btn-xs" aria-hidden="true"></i>
                                                                                            <asp:Button ID="readycancel_btn" runat="server" CommandName="Cancel" CssClass="movedown" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                                                                                        </span>
                                                                                        <asp:HiddenField ID="readyold_cop_act_hf" runat="server" Value='<%# Eval("cop_act") %>' />
                                                                                        <asp:HiddenField ID="readyold_cop_id_hf" runat="server" Value='<%# Eval("cop_id") %>' />
                                                                                    </EditItemTemplate>

                                                                                    <FooterTemplate>
                                                                                        <span style="position: relative;">
                                                                                            <i class="fa fa-floppy-o btn btn-success btn-xs" aria-hidden="true"></i>
                                                                                            <asp:Button ID="readysave_btn" runat="server" CommandName="AddSaveReady" CssClass="movedown" Text="" ToolTip="存檔" UseSubmitBehavior="False" />
                                                                                        </span>
                                                                                        &nbsp;<span style="position: relative;">
                                                                                            <i class="fa fa-undo btn btn-warning btn-xs" aria-hidden="true"></i>
                                                                                            <asp:Button ID="readycancel_btn" runat="server" CommandName="Cancel" CssClass="movedown" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                                                                                        </span>
                                                                                    </FooterTemplate>

                                                                                    <HeaderTemplate>
                                                                                        <span style="position: relative;">
                                                                                            <i class="fa fa-plus btn btn-info btn-xs" aria-hidden="true"></i>
                                                                                            <asp:Button ID="readyadd_btn" runat="server" CommandName="AddReady" CssClass="movedown" Text="" ToolTip="新增" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                                                                                        </span>
                                                                                    </HeaderTemplate>

                                                                                    <ItemTemplate>
                                                                                        <span style="position: relative;">
                                                                                            <i class="fa fa-pencil btn btn-primary btn-xs" aria-hidden="true"></i>
                                                                                            <asp:Button ID="readyedit_btn" runat="server" CommandName="Edit" CssClass="movedown" Text="編輯" ToolTip="編輯" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                                                                                        </span>
                                                                                        &nbsp;
                                                                           
                                                                                        <span style="position: relative;">
                                                                                            <i class="fa fa-trash-o btn btn-danger btn-xs" aria-hidden="true"></i>
                                                                                            <asp:Button ID="readydelete_btn" runat="server" CommandName="Delete" CssClass="movedown" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                                                                                        </span>
                                                                                        <asp:HiddenField ID="readyrowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                                                                                    </ItemTemplate>

                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                    <HeaderStyle Width="70px" />
                                                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                                                </asp:TemplateField>

                                                                                <%--帳號--%>
                                                                                <asp:TemplateField HeaderText="帳號" SortExpression="cop_id">
                                                                                    <EditItemTemplate>
                                                                                        <asp:Button ID="readyaccount_btn" runat="server" Text="請選擇" CommandName="account" CssClass="btn-link" UseSubmitBehavior="false" CommandArgument='<%# Bind("cop_id") %>' />
                                                                                        <asp:Label ID="readyaccountEdit_lbl" runat="server" Text=""></asp:Label>
                                                                                    </EditItemTemplate>

                                                                                    <FooterTemplate>
                                                                                        <asp:Button ID="readyaccount_btn" runat="server" Text="請選擇" CommandName="account" CssClass="btn-link" UseSubmitBehavior="false" CommandArgument='<%# Bind("cop_id") %>' />
                                                                                        <asp:Label ID="readyaccount_lbl" runat="server" Text=""></asp:Label>
                                                                                    </FooterTemplate>

                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="readycop_id_lbl" runat="server" Text='<%# Bind("cop_id") %>'></asp:Label>
                                                                                    </ItemTemplate>

                                                                                    <HeaderStyle />
                                                                                    <ItemStyle CssClass="rowTrigger center" />
                                                                                </asp:TemplateField>

                                                                                <%--權限--%>
                                                                                <asp:TemplateField HeaderText="權限" SortExpression="cop_authority">
                                                                                    <EditItemTemplate>
                                                                                        <asp:DropDownList ID="readycopEdit_authority_dll" runat="server"></asp:DropDownList>
                                                                                    </EditItemTemplate>

                                                                                    <FooterTemplate>
                                                                                        <asp:DropDownList ID="readycop_authority_dll" runat="server" Text='<%# Bind("cop_authority") %>'></asp:DropDownList>
                                                                                    </FooterTemplate>

                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="readycop_authority_lbl" runat="server" Text='<%# Bind("cop_authority") %>'></asp:Label>
                                                                                    </ItemTemplate>

                                                                                    <ItemStyle CssClass="rowTrigger center" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <!-- GridView END -->
                                                                    </asp:View>
                                                                    <%--主要view(GridView)--%>

                                                                    <%--設定帳號 START--%>
                                                                    <asp:View ID="readyaccount_view" runat="server">
                                                                        <asp:Panel ID="readyAccount" runat="server">
                                                                            <asp:Label ID="readyaccountpassword_lbl" runat="server" Text="選擇帳號"></asp:Label><br />
                                                                            <br />
                                                                            <div class="scroll_box">
                                                                                <asp:RadioButtonList ID="readyaccount_radiobuttonlist" runat="server" CssClass="fv" RepeatDirection="Horizontal" RepeatColumns="2" Width="75%">
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <asp:HiddenField ID="readyrow_idn_hf" runat="server" Visible="false" ViewStateMode="Enabled" />
                                                                        </asp:Panel>
                                                                        <asp:Button ID="readysetAccount_btn" runat="server" CssClass="btn btn-default" Text="確認" UseSubmitBehavior="false" OnClick="setAccount_btn_Click" />
                                                                        <asp:Button ID="readyback_btn" runat="server" CssClass="btn btn-default" Text="返回" UseSubmitBehavior="false" OnClick="back_btn_Click" /><br />
                                                                    </asp:View>
                                                                    <%--設定帳號 END--%>
                                                                </asp:MultiView>
                                                                <%--多重view END--%>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <!-- 協作者內容 END -->
                                            </asp:Panel>
                                            <!-- 協作者 END -->
                                        </ContentTemplate>

                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ready_gv" EventName="DataBound" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                    <br />
                                </div>
                                <!--未發佈活動 END-->

                                <!--已結束活動 START-->
                                <div role="tabpanel" class="tab-pane" id="end">
                                    <br />
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:GridView ID="end_gv" class="table table-striped" runat="server" AutoGenerateColumns="False" OnRowCommand="main_gv_RowCommand" ViewStateMode="Enabled" OnRowDeleting="main_gv_RowDeleting" OnSorting="main_gv_Sorting" DataKeyNames="as_idn">
                                                <Columns>
                                                    <%-- 活動標題 --%>
                                                    <asp:TemplateField HeaderText="活動標題" SortExpression="act_title">
                                                        <ItemTemplate>
                                                            <a title="<%# Eval("act_title") %>">
                                                                <input id="act_title_lbl" type="text" readonly="true" class="activity_title" value='<%# Eval("act_title") %>' style="cursor:default;background-color: transparent;"/>
                                                                <%--<p style="width: 150px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis; margin: 0px;">
                                                                    <asp:Label ID="act_title_lbl" runat="server" Text='<%# Bind("act_title") %>' Style="cursor: default; color: #808080;"></asp:Label>
                                                                </p>--%>
                                                            </a>
                                                            <asp:HiddenField ID="as_idn_hf" runat="server" Value='<%# Eval("as_idn") %>' />
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="150px" />
                                                        <ItemStyle CssClass="center" Height="50px" />
                                                    </asp:TemplateField>

                                                    <%-- 場次 --%>
                                                    <asp:TemplateField HeaderText="場次" SortExpression="as_title">
                                                        <ItemTemplate>
                                                            <a title="<%# Eval("as_title") %>">
                                                                <input id="as_title_lbl" type="text" readonly="true" class="activity_title" value='<%# Eval("as_title") %>' style="cursor:default;background-color: transparent;"/>
                                                                <%--<p style="width: 150px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis; margin: 0px;">
                                                                    <asp:Label ID="as_title_lbl" runat="server" Text='<%# Bind("as_title") %>' Style="cursor: default; color: #808080;"></asp:Label>
                                                                </p>--%>
                                                            </a>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="150px" />
                                                        <ItemStyle CssClass="center" Height="50px" />
                                                    </asp:TemplateField>

                                                    <%-- 報名/限制人數 --%>
                                                    <asp:TemplateField HeaderText="報名/限制人數" SortExpression="as_num">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_num" runat="server" Text='<%# Bind("as_num") %>'></asp:Label>/<asp:Label ID="as_num_limit_lbl" runat="server" Text='<%# Bind("as_num_limit") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="100px" />
                                                        <ItemStyle CssClass="center" Height="50px" />
                                                    </asp:TemplateField>

                                                    <%-- 活動日期 --%>
                                                    <asp:TemplateField HeaderText="活動日期" SortExpression="as_date_start">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_date_start_lbl" runat="server" Text='<%# Bind("as_date_start") %>'></asp:Label>~<asp:Label ID="as_date_end_lbl" runat="server" Text='<%# Bind("as_date_end") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="200px" />
                                                        <ItemStyle CssClass="center" Height="50px" />
                                                    </asp:TemplateField>

                                                    <%-- 報名起訖 --%>
                                                    <asp:TemplateField HeaderText="報名起訖" SortExpression="as_apply_start">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_apply_start_lbl" runat="server" Text='<%# Bind("as_apply_start") %>'></asp:Label>~<asp:Label ID="as_apply_end_lbl" runat="server" Text='<%# Bind("as_apply_end") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="200px" />
                                                        <ItemStyle CssClass="center" Height="50px" />
                                                    </asp:TemplateField>

                                                    <%-- 報名資料 --%>
                                                    <asp:TemplateField HeaderText="報名資料">
                                                        <ItemTemplate>
                                                            <input id="checkApply" type="button" class="btn-link" value="查看" onclick="GoTo(<%# Eval("as_idn") %>);" />/<asp:Button ID="download_btn" runat="server" CommandName="download" class="btn-link" Text="下載" ToolTip="下載" UseSubmitBehavior="False" CommandArgument='<%# Eval("as_idn") %>' />
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="110px" />
                                                        <ItemStyle CssClass="center" Height="50px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                            <%-- 分頁 --%>
                                            <uc1:UCGridViewPager runat="server" ID="ucGridViewPagerEnd" />
                                        </ContentTemplate>

                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="end_gv" EventName="DataBound" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                    <br />
                                </div>
                                <!--已結束活動 END-->
                            </div>
                        </div>
                        <!--頁籤 END-->
                    </div>
                </div>
                <!--main content end-->
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
