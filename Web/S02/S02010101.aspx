<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S02010101.aspx.cs" Inherits="Web.S02.S02010101" %>
<%@ Import Namespace="Util" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/UCSystemModule.ascx" TagPrefix="uc1" TagName="UCSystemModule" %>
<%@ Register Src="~/UserControls/UCGridViewPager.ascx" TagPrefix="uc1" TagName="UCGridViewPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
    <script type="text/javascript">
        $(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            });
        });
    </script>

    <%--查看報名資料--%>
    <script type="text/javascript">
        function GoTo(id) {
            window.open("S02010102.aspx?sys_id=S02&sys_pid=S02010103&i=" + id, "_blank");
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
                active: ($.cookie("tabs") || 0),  
 
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
                <!--活動查詢 START-->
                <div class="QueryTable">
                    <table class="grv">
                        <tr class="grvDataRow">
                            <th style="width: 100px;">活動查詢</th>
                            <td>
                                <asp:TextBox runat="server" ID="q_keyword_tb" />
                            </td>
                            <td rowspan="2" style="width: 100px; text-align: center;">
                                <asp:Button runat="server" ID="q_query_btn" Text="查詢" OnClick="q_query_btn_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <!--活動查詢 END-->

                <!-- **********************************************************************************************************************************************************
                MAIN CONTENT
                *********************************************************************************************************************************************************** -->
                <!--main content start-->
                <div class="row">
                    <div class="content-panel">
                        <!--頁籤 START-->
                        <div role="tabpanel" id="tabs">
                            <!-- 頁籤標題 START -->
                            <ul class="nav nav-pills" role="tablist" id="myTab">
                                <li role="presentation" class="active"><a href="#already" aria-controls="already" role="tab" data-toggle="pill">已發佈</a></li>
                                <li role="presentation"><a href="#ready" aria-controls="ready" role="tab" data-toggle="pill">未發佈</a></li>
                                <li role="presentation"><a href="#end" aria-controls="end" role="tab" data-toggle="pill">已結束</a></li>
                                <li role="presentation"><a href="#test" aria-controls="end" role="tab" data-toggle="pill">測試用</a></li>
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
                                                    <asp:TemplateField HeaderText="活動標題">
                                                        <ItemTemplate>
                                                            <input id="viewActiviity" type="button" class="btn-link" value='<%# Eval("act_title") %>' Onclick="GoToActivity(<%# Eval("act_idn") %>);" />
                                                            <asp:HiddenField ID="as_idn_hf" runat="server" Value='<%# Eval("as_idn") %>' />
                                                            <asp:HiddenField ID="as_isopen_hf" runat="server" Value='<%# Eval("as_isopen") %>' />
                                                            <asp:HiddenField ID="act_idn_hf" runat="server" Value='<%# Eval("act_idn") %>' />
                                                            <asp:HiddenField ID ="act_isopen_hf" runat="server" Value='<%# Eval("act_isopen") %>' />
                                                        </ItemTemplate>

                                                        <HeaderStyle />
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 場次 --%>
                                                    <asp:TemplateField HeaderText="場次">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_title_lbl" runat="server" Text='<%# Bind("as_title") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle />
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 報名人數 --%>
                                                    <asp:TemplateField HeaderText="報名人數">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_num" runat="server" Text='<%# Bind("as_num") %>'></asp:Label>/<asp:Label ID="as_num_limit_lbl" runat="server" Text='<%# Bind("as_num_limit") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="70px"/>
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 活動日期 --%>
                                                    <asp:TemplateField HeaderText="活動日期">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_date_start_lbl" runat="server" Text='<%# Bind("as_date_start") %>'></asp:Label> <asp:Label ID="as_date_starttime_lbl" runat="server" Text='<%# Bind("as_date_starttime") %>'></asp:Label>
                                                             ~ <br />
                                                            <asp:Label ID="as_date_end_lbl" runat="server" Text='<%# Bind("as_date_end") %>'></asp:Label> <asp:Label ID="as_date_endtime" runat="server" Text='<%# Bind("as_date_endtime") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="150px"/>
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 報名起訖 --%>
                                                    <asp:TemplateField HeaderText="報名起訖">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_apply_start_lbl" runat="server" Text='<%# Bind("as_apply_start") %>'></asp:Label> <asp:Label ID="as_apply_starttime_lbl" runat="server" Text='<%# Bind("as_apply_starttime") %>'></asp:Label>
                                                             ~ <br />
                                                            <asp:Label ID="as_apply_end_lbl" runat="server" Text='<%# Bind("as_apply_end") %>'></asp:Label> <asp:Label ID="as_apply_endtime_lbl" runat="server" Text='<%# Bind("as_apply_endtime") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="150px"/>
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 報名資料 --%>
                                                    <asp:TemplateField HeaderText="報名資料">
                                                        <ItemTemplate>
                                                            <input id="checkApply" type="button" class="btn-link" value="查看" Onclick="GoTo(<%# Eval("as_idn") %>);" />/<asp:Button ID="download_btn" runat="server" CommandName="download" class="btn-link" Text="下載" ToolTip="下載" UseSubmitBehavior="False" CommandArgument='<%# Eval("as_idn") %>' />
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="110px"/>
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 基本功能 --%>
                                                    <asp:TemplateField HeaderText="基本功能">
                                                        <ItemTemplate>
                                                            <asp:Button ID="editActivity_btn" runat="server" CommandName="EditActivity" class="btn-link" Text="修改" ToolTip="修改" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" CommandArgument='<%# Eval("act_idn") %>'/>/<asp:Button ID="delete_btn" runat="server" CommandName="Delete" class="btn-link" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />/<asp:Button ID="edit_btn" runat="server" CommandName="Edit" class="btn-link" OnClientClick="if (!confirm(&quot;確定要關閉嗎?&quot;)) return false" Text="關閉" ToolTip="關閉" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />/<asp:Button ID="set_btn" runat="server" CommandName="Set" class="btn-link" Text="協作者" ToolTip="協作者" UseSubmitBehavior="False" />
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="230px"/>
                                                        <ItemStyle CssClass="center" />
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
                                                    <asp:Button ID="controlSet_OK_btn" CssClass="open"  runat="server" Text="開啟" ToolTip="開啟" UseSubmitBehavior="False" />
                                                </div>
                                                <!-- 協作者標頭 END -->

                                                <!-- 協作者內容 START -->
                                                <div class="popupWindowContent">
                                                    <asp:UpdatePanel ID="controlSet_content_upl" runat="server">
                                                        <ContentTemplate>
                                                            <!-- 設定協作者的key -->
                                                            <div> 
                                                                <asp:HiddenField ID="copperate_cop_act_hf" runat="server" />
                                                            </div>

                                                            <!-- GridView START -->
                                                            <asp:GridView ID="controlSet_gv" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="controlSet_gv_RowCancelingEdit" OnRowCommand="controlSet_gv_RowCommand" OnRowEditing="controlSet_gv_RowEditing" OnRowUpdating="controlSet_gv_RowUpdating" ShowHeaderWhenEmpty="True" OnRowDeleting="controlSet_gv_RowDeleting" OnRowDataBound="controlSet_gv_RowDataBound" OnSorting="controlSet_gv_Sorting" >
                                                                <Columns>
                                                                    <%--操作--%>
                                                                    <asp:TemplateField ShowHeader="False" HeaderText="操作">
                                                                        <EditItemTemplate>
                                                                            &nbsp;<asp:Button ID="save_btn" runat="server" CommandName="Update" CssClass="btnGrv update" Text="" ToolTip="存檔" UseSubmitBehavior="False" />
                                                                            &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="" ToolTip="取消" UseSubmitBehavior="False" />
                                                                            <asp:HiddenField ID="old_cop_act_hf" runat="server" Value='<%# Eval("cop_act") %>' />
                                                                            <asp:HiddenField ID="old_cop_id_hf" runat="server" Value='<%# Eval("cop_id") %>' />
                                                                        </EditItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:Button ID="save_btn" runat="server" CommandName="AddSave" CssClass="btnGrv update" Text="" ToolTip="存檔" UseSubmitBehavior="False" />
                                                                            &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="" ToolTip="取消" UseSubmitBehavior="False" />
                                                                        </FooterTemplate>

                                                                        <HeaderTemplate>
                                                                            <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="btnGrv add" Text="" ToolTip="新增" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                                                                        </HeaderTemplate>

                                                                        <ItemTemplate>
                                                                            <asp:Button ID="edit_btn" runat="server" CommandName="Edit" CssClass="btnGrv edit" Text="" ToolTip="編輯" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                                                                            &nbsp;<asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="btnGrv delete" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="" ToolTip="刪除" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                                                                            <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                                                                            <asp:HiddenField ID="cop_act_hf" runat="server" Value='<%# Eval("cop_act") %>' />
                                                                        </ItemTemplate>

                                                                        <FooterStyle HorizontalAlign="Center" />
                                                                        <HeaderStyle Width="70px" />
                                                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                                    </asp:TemplateField>

                                                                    <%--帳號--%>
                                                                    <asp:TemplateField HeaderText="帳號" SortExpression="cop_id">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="cop_id_txt" runat="server" Text='<%# Bind("cop_id") %>'></asp:TextBox>
                                                                        </EditItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="cop_id_txt" runat="server" Text='<%# Bind("cop_id") %>'></asp:TextBox>
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
                                                                            <asp:TextBox ID="cop_authority_txt" runat="server" Text='<%# Bind("cop_authority") %>'></asp:TextBox>
                                                                        </EditItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="cop_authority_txt" runat="server" Text='<%# Bind("cop_authority") %>'></asp:TextBox>
                                                                        </FooterTemplate>

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="cop_authority_lbl" runat="server" Text='<%# Bind("cop_authority") %>'></asp:Label>
                                                                        </ItemTemplate>

                                                                        <ItemStyle CssClass="rowTrigger center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <!-- GridView END -->
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
                                                    <asp:TemplateField HeaderText="活動標題">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="act_title_lbl" runat="server" Text='<%# Bind("act_title") %>'></asp:Label>--%>
                                                            <input id="viewActiviity" type="button" class="btn-link" value='<%# Eval("act_title") %>' Onclick="GoToActivity(<%# Eval("act_idn") %>);" />
                                                            <asp:HiddenField ID="as_idn_hf" runat="server" Value='<%# Eval("as_idn") %>' />
                                                            <asp:HiddenField ID="as_isopen_hf" runat="server" Value='<%# Eval("as_isopen") %>' />
                                                            <asp:HiddenField ID="act_idn_hf" runat="server" Value='<%# Eval("act_idn") %>' />
                                                            <asp:HiddenField ID="act_isopen_hf" runat="server" Value='<%# Eval("act_isopen") %>' />
                                                        </ItemTemplate>

                                                        <HeaderStyle />
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 場次 --%>
                                                    <asp:TemplateField HeaderText="場次">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_title_lbl" runat="server" Text='<%# Bind("as_title") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle />
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 報名/限制人數 --%>
                                                    <asp:TemplateField HeaderText="報名人數">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_num" runat="server" Text='<%# Bind("as_num") %>'></asp:Label>/<asp:Label ID="as_num_limit_lbl" runat="server" Text='<%# Bind("as_num_limit") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="70px"/>
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 活動日期 --%>
                                                    <asp:TemplateField HeaderText="活動日期">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_date_start_lbl" runat="server" Text='<%# Bind("as_date_start") %>'></asp:Label> <asp:Label ID="as_date_starttime_lbl" runat="server" Text='<%# Bind("as_date_starttime") %>'></asp:Label>
                                                             ~ <br />
                                                            <asp:Label ID="as_date_end_lbl" runat="server" Text='<%# Bind("as_date_end") %>'></asp:Label> <asp:Label ID="as_date_endtime" runat="server" Text='<%# Bind("as_date_endtime") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="150px"/>
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 報名起訖 --%>
                                                    <asp:TemplateField HeaderText="報名起訖">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_apply_start_lbl" runat="server" Text='<%# Bind("as_apply_start") %>'></asp:Label> <asp:Label ID="as_apply_starttime_lbl" runat="server" Text='<%# Bind("as_apply_starttime") %>'></asp:Label>
                                                             ~ <br />
                                                            <asp:Label ID="as_apply_end_lbl" runat="server" Text='<%# Bind("as_apply_end") %>'></asp:Label> <asp:Label ID="as_apply_endtime_lbl" runat="server" Text='<%# Bind("as_apply_endtime") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="150px"/>
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 報名資料 --%>
                                                    <asp:TemplateField HeaderText="報名資料">
                                                        <ItemTemplate>
                                                            <input id="checkApply" type="button" class="btn-link" value="查看" Onclick="GoTo(<%# Eval("as_idn") %>);" />/<asp:Button ID="download_btn" runat="server" CommandName="download" class="btn-link" Text="下載" ToolTip="下載" UseSubmitBehavior="False" CommandArgument='<%# Eval("as_idn") %>' />
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="110px"/>
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 基本功能 --%>
                                                    <asp:TemplateField HeaderText="基本功能">
                                                        <ItemTemplate>
                                                            <asp:Button ID="editActivity_btn" runat="server" CommandName="EditActivity" class="btn-link" Text="修改" ToolTip="修改" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" CommandArgument='<%# Eval("act_idn") %>'/>/<asp:Button ID="delete_btn" runat="server" CommandName="Delete" class="btn-link" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />/<asp:Button ID="edit_btn" runat="server" CommandName="Edit" class="btn-link" OnClientClick="if (!confirm(&quot;確定要發佈嗎?&quot;)) return false" Text="發佈" ToolTip="發佈" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />/<asp:Button ID="set_btn" runat="server" CommandName="Set_ready" class="btn-link" Text="協作者" ToolTip="協作者" UseSubmitBehavior="False" />
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="230px"/>
                                                        <ItemStyle CssClass="center" />
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
                                                    <asp:Button ID="readyCopperate_cancel_btn" CssClass="open"  runat="server" Text="開啟" ToolTip="開啟" UseSubmitBehavior="False" />
                                                </div>
                                                <!-- 協作者標頭 END -->

                                                <!-- 協作者內容 START -->
                                                <div class="popupWindowContent">
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <!-- 設定協作者的key -->
                                                            <div> 
                                                                <asp:HiddenField ID="cop_act_ready_hf" runat="server" />
                                                            </div>

                                                            <!-- GridView START -->
                                                            <asp:GridView ID="ready_copperate" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="controlSetReady_gv_RowCancelingEdit" OnRowCommand="controlSet_gv_RowCommand" OnRowEditing="controlSetReady_gv_RowEditing" OnRowUpdating="controlSetReady_gv_RowUpdating" ShowHeaderWhenEmpty="True" OnRowDeleting="controlSetReady_gv_RowDeleting" OnRowDataBound="controlSet_gv_RowDataBound" OnSorting="controlSetReady_gv_Sorting">
                                                                <Columns>
                                                                    <%--操作--%>
                                                                    <asp:TemplateField ShowHeader="False" HeaderText="操作">
                                                                        <EditItemTemplate>
                                                                            &nbsp;<asp:Button ID="save_btn" runat="server" CommandName="Update" CssClass="btnGrv update" Text="" ToolTip="存檔" UseSubmitBehavior="False" />
                                                                            &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="" ToolTip="取消" UseSubmitBehavior="False" />
                                                                            <asp:HiddenField ID="old_cop_act_hf" runat="server" Value='<%# Eval("cop_act") %>' />
                                                                            <asp:HiddenField ID="old_cop_id_hf" runat="server" Value='<%# Eval("cop_id") %>' />
                                                                        </EditItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:Button ID="save_btn" runat="server" CommandName="AddSaveReady" CssClass="btnGrv update" Text="" ToolTip="存檔" UseSubmitBehavior="False" />
                                                                            &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="" ToolTip="取消" UseSubmitBehavior="False" />
                                                                        </FooterTemplate>

                                                                        <HeaderTemplate>
                                                                            <asp:Button ID="add_btn" runat="server" CommandName="AddReady" CssClass="btnGrv add" Text="" ToolTip="新增" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                                                                        </HeaderTemplate>

                                                                        <ItemTemplate>
                                                                            <asp:Button ID="edit_btn" runat="server" CommandName="Edit" CssClass="btnGrv edit" Text="" ToolTip="編輯" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                                                                            &nbsp;<asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="btnGrv delete" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="" ToolTip="刪除" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                                                                            <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                                                                            <asp:HiddenField ID="cop_act_hf" runat="server" Value='<%# Eval("cop_act") %>' />
                                                                        </ItemTemplate>

                                                                        <FooterStyle HorizontalAlign="Center" />
                                                                        <HeaderStyle Width="70px" />
                                                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                                    </asp:TemplateField>

                                                                    <%--帳號--%>
                                                                    <asp:TemplateField HeaderText="帳號" SortExpression="cop_id">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="cop_id_txt" runat="server" Text='<%# Bind("cop_id") %>'></asp:TextBox>
                                                                        </EditItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="cop_id_txt" runat="server" Text='<%# Bind("cop_id") %>'></asp:TextBox>
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
                                                                            <asp:TextBox ID="cop_authority_txt" runat="server" Text='<%# Bind("cop_authority") %>'></asp:TextBox>
                                                                        </EditItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="cop_authority_txt" runat="server" Text='<%# Bind("cop_authority") %>'></asp:TextBox>
                                                                        </FooterTemplate>

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="cop_authority_lbl" runat="server" Text='<%# Bind("cop_authority") %>'></asp:Label>
                                                                        </ItemTemplate>

                                                                        <ItemStyle CssClass="rowTrigger center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <!-- GridView END -->
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
                                                    <asp:TemplateField HeaderText="活動標題">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="act_title_lbl" runat="server" Text='<%# Bind("act_title") %>'></asp:Label>--%>
                                                            <input id="viewActiviity" type="button" class="btn-link" value='<%# Eval("act_title") %>' Onclick="GoToActivity(<%# Eval("act_idn") %>);" />
                                                            <asp:HiddenField ID="as_idn_hf" runat="server" Value='<%# Eval("as_idn") %>' />
                                                        </ItemTemplate>

                                                        <HeaderStyle />
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 場次 --%>
                                                    <asp:TemplateField HeaderText="場次">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_title_lbl" runat="server" Text='<%# Bind("as_title") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle />
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 報名/限制人數 --%>
                                                    <asp:TemplateField HeaderText="報名/限制人數">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_num" runat="server" Text='<%# Bind("as_num") %>'></asp:Label>/<asp:Label ID="as_num_limit_lbl" runat="server" Text='<%# Bind("as_num_limit") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="100px"/>
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 活動日期 --%>
                                                    <asp:TemplateField HeaderText="活動日期">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_date_start_lbl" runat="server" Text='<%# Bind("as_date_start") %>'></asp:Label> <asp:Label ID="as_date_starttime_lbl" runat="server" Text='<%# Bind("as_date_starttime") %>'></asp:Label>
                                                             ~ <br />
                                                            <asp:Label ID="as_date_end_lbl" runat="server" Text='<%# Bind("as_date_end") %>'></asp:Label> <asp:Label ID="as_date_endtime" runat="server" Text='<%# Bind("as_date_endtime") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="200px"/>
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 報名起訖 --%>
                                                    <asp:TemplateField HeaderText="報名起訖">
                                                        <ItemTemplate>
                                                            <asp:Label ID="as_apply_start_lbl" runat="server" Text='<%# Bind("as_apply_start") %>'></asp:Label> <asp:Label ID="as_apply_starttime_lbl" runat="server" Text='<%# Bind("as_apply_starttime") %>'></asp:Label>
                                                             ~ <br />
                                                            <asp:Label ID="as_apply_end_lbl" runat="server" Text='<%# Bind("as_apply_end") %>'></asp:Label> <asp:Label ID="as_apply_endtime_lbl" runat="server" Text='<%# Bind("as_apply_endtime") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="200px"/>
                                                        <ItemStyle CssClass="center" />
                                                    </asp:TemplateField>

                                                    <%-- 報名資料 --%>
                                                    <asp:TemplateField HeaderText="報名資料">
                                                        <ItemTemplate>
                                                            <input id="checkApply" type="button" class="btn-link" value="查看" Onclick="GoTo(<%# Eval("as_idn") %>);" />/<asp:Button ID="download_btn" runat="server" CommandName="download" class="btn-link" Text="下載" ToolTip="下載" UseSubmitBehavior="False" CommandArgument='<%# Eval("as_idn") %>' />
                                                        </ItemTemplate>

                                                        <HeaderStyle Width="110px"/>
                                                        <ItemStyle CssClass="center" />
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

                                <div role="tabpanel" class="tab-pane" id="test">
                                    活動<br />
                                    活動標題：<asp:TextBox runat="server" ID="act_title" /><br />
                                    是否發布：<asp:TextBox runat="server" ID="act_isopen" /><br />
                                    活動編號：<asp:TextBox runat="server" ID="act_idn" /><br />

                                    <asp:Button runat="server" ID="test_submit" OnClick="test_submit_click" Text="送出" />
                                    <asp:Button runat="server" ID="editTest_btn" OnClick="editTest_btn_click" Text="修改" />
                                    <br />

                                    活動場次<br />
                                    活動序號：<asp:TextBox runat="server" ID="as_act" /><br />
                                    報名人數限制：<asp:TextBox runat="server" ID="as_num_limit" /><br />

                                    <asp:Button runat="server" ID="saveTestSession_btn" OnClick="saveTestSession_btn_click" Text="送出" /><br />

                                    活動報名<br />
                                    活動序號：<asp:TextBox runat="server" ID="aa_act" /><br />
                                    場次序號：<asp:TextBox runat="server" ID="aa_as" /><br />

                                    <asp:Button runat="server" ID="saveTestApply_btn" OnClick="saveTestApply_btn_click" Text="送出" /><br />

                                    協作者<br />
                                    協同活動：<asp:TextBox runat="server" ID="cop_act" /><br />
                                    協同帳號：<asp:TextBox runat="server" ID="cop_id" /><br />
                                    權限：<asp:TextBox runat="server" ID="cop_authority" /><br />

                                    <asp:Button runat="server" ID="saveTestCop_btn" OnClick="saveTestCop_btn_click" Text="送出" />
                                </div>
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