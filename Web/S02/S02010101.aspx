<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S02010101.aspx.cs" Inherits="Web.S02.S02010101" %>

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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent_cph" runat="server">
    <asp:Panel ID="form_panel" runat="server" Visible="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <!--活動查詢 START-->
                <div class="QueryTable">
                    <table class="grv">
                        <tr class="grvDataRow">
                            <th style="width: 100px;">活動查詢</th>
                            <td>
                                <asp:TextBox runat="server" ID="q_keyword_tb" />
                            </td>
                            <td rowspan="2" style="width: 100px; text-align: center;"><asp:Button runat="server" ID="q_query_btn" Text="查詢" OnClick="q_query_btn_Click" /></td>
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
                        <div role="tabpanel">
                            <ul class="nav nav-pills" role="tablist">
                                <li role="presentation" class="active"><a href="#already" aria-controls="already" role="tab" data-toggle="pill">已發佈</a></li>
                                <li role="presentation"><a href="#ready" aria-controls="ready" role="tab" data-toggle="pill">未發佈</a></li>
                                <li role="presentation"><a href="#end" aria-controls="end" role="tab" data-toggle="pill">已結束</a></li>
                                <li role="presentation"><a href="#test" aria-controls="end" role="tab" data-toggle="pill">測試用</a></li>
                            </ul>

                            <div class="tab-content">
                                <!--已發佈活動 START-->
                                <div role="tabpanel" class="tab-pane active" id="already">
                                    <br />
                                    <asp:GridView ID="main_gv" class="table table-striped" runat="server" AutoGenerateColumns="False" OnRowCommand="main_gv_RowCommand" OnRowEditing="main_gv_RowEditing" OnRowCancelingEdit="main_gv_RowCancelingEdit" ViewStateMode="Enabled" OnRowDeleting="main_gv_RowDeleting" OnRowDataBound="main_gv_RowDataBound" OnSorting="main_gv_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="活動標題">
                                                <ItemTemplate>
                                                    <asp:Label ID="act_title_lbl" runat="server" Text='<%# Bind("act_title") %>'></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="場次">
                                                <ItemTemplate>
                                                    <asp:Label ID="as_title_lbl" runat="server" Text='<%# Bind("as_title") %>'></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="報名/限制人數">
                                                <ItemTemplate>
                                                    <asp:Label ID="as_num_limit_lbl" runat="server" Text='<%# Bind("as_num_limit") %>'></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="活動日期">
                                                <ItemTemplate>
                                                    <asp:Label ID="as_date_start_lbl" runat="server" Text='<%# Bind("as_date_start") %>'></asp:Label><br />
                                                    ~<br />
                                                    <asp:Label ID="as_date_end_lbl" runat="server" Text='<%# Bind("as_date_end") %>'></asp:Label><br />
                                                </ItemTemplate>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="報名起訖">
                                                <ItemTemplate>
                                                    <asp:Label ID="as_apply_start_lbl" runat="server" Text='<%# Bind("as_apply_start") %>'></asp:Label><br />
                                                    ~<br />
                                                    <asp:Label ID="as_apply_end_lbl" runat="server" Text='<%# Bind("as_apply_end") %>'></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="報名資料">
                                                <ItemTemplate>
                                                    <a href='../S02/S02010102.aspx' target='_blank'>查看</a>/<a href="#">下載</a>
                                                </ItemTemplate>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="基本功能">
                                                <ItemTemplate>
                                                    <asp:Button class="btn-link" ID="editActivity_btn" runat="server" OnClick="editActivity_btn_Click" Text="修改" />/<a href="#">刪除</a>/<a href="#">關閉</a>/<a data-toggle="modal" data-backdrop="static" href="#myModal">協作者</a>
                                                </ItemTemplate>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />

                                    <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="main_gv_RowCommand" OnRowEditing="main_gv_RowEditing" OnRowCancelingEdit="main_gv_RowCancelingEdit" ViewStateMode="Enabled" OnRowDeleting="main_gv_RowDeleting" OnRowDataBound="main_gv_RowDataBound" OnSorting="main_gv_Sorting">
                                        <Columns>
                                            <asp:TemplateField ShowHeader="False" HeaderText="操作">
                                                <HeaderTemplate>
                                                    <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="btnGrv add" Text="新增" ToolTip="新增" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Button ID="edit_btn" runat="server" CommandName="Edit" CssClass="btnGrv edit" Text="編輯" ToolTip="編輯" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                                                    &nbsp;<asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="btnGrv delete" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                                                    <asp:HiddenField ID="act_idn_hf" runat="server" Value='<%# Eval("act_idn") %>' />
                                                    <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                                                </ItemTemplate>

                                                <HeaderStyle Width="70px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="活動標題" SortExpression="Pstid">
                                                <ItemTemplate>
                                                    <asp:Label ID="act_title_lbl" runat="server" Text='<%# Bind("act_title") %>'></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="rowTrigger center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>--%>
                                </div>
                                <!--已發佈活動 END-->

                                <!--未發佈活動 START-->
                                <div role="tabpanel" class="tab-pane" id="ready">
                                    <br />
                                    <asp:GridView ID="ready_gv" class="table table-striped" runat="server" AutoGenerateColumns="False" OnRowCommand="main_gv_RowCommand" OnRowEditing="main_gv_RowEditing" OnRowCancelingEdit="main_gv_RowCancelingEdit" ViewStateMode="Enabled" OnRowDeleting="main_gv_RowDeleting" OnRowDataBound="main_gv_RowDataBound" OnSorting="main_gv_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="活動標題">
                                                <%--<ItemTemplate>
                                                    <asp:Label ID="act_title_lbl" runat="server" Text='<%# Bind("act_title") %>'></asp:Label>
                                                </ItemTemplate>--%>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="報名/限制人數">
                                                <%--<ItemTemplate>
                                                    <asp:Label ID="act_title_lbl" runat="server" Text='<%# Bind("act_title") %>'></asp:Label>
                                                </ItemTemplate>--%>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="活動日期">
                                                <%--<ItemTemplate>
                                                    <asp:Label ID="act_title_lbl" runat="server" Text='<%# Bind("act_title") %>'></asp:Label>
                                                </ItemTemplate>--%>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="報名起訖">
                                                <%--<ItemTemplate>
                                                    <asp:Label ID="act_title_lbl" runat="server" Text='<%# Bind("act_title") %>'></asp:Label>
                                                </ItemTemplate>--%>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="報名資料">
                                                <ItemTemplate>
                                                    <a href='../S02/S02010102.aspx' target='_blank'>查看</a>/<a href="#">下載</a>
                                                </ItemTemplate>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="基本功能">
                                                <ItemTemplate>
                                                    <asp:Button class="btn-link" ID="editActivity_btn" runat="server" OnClick="editActivity_btn_Click" Text="修改" />/<a href="#">刪除</a>/<a href="#">發佈</a>/<a data-toggle="modal" data-backdrop="static" href="#myModal">協作者</a>
                                                </ItemTemplate>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                </div>
                                <!--未發佈活動 END-->

                                <!--已結束活動 START-->
                                <div role="tabpanel" class="tab-pane" id="end">
                                    <br />
                                    <asp:GridView ID="end_gv" class="table table-striped" runat="server" AutoGenerateColumns="False" OnRowCommand="main_gv_RowCommand" OnRowEditing="main_gv_RowEditing" OnRowCancelingEdit="main_gv_RowCancelingEdit" ViewStateMode="Enabled" OnRowDeleting="main_gv_RowDeleting" OnRowDataBound="main_gv_RowDataBound" OnSorting="main_gv_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="活動標題">
                                                <%--<ItemTemplate>
                                                    <asp:Label ID="act_title_lbl" runat="server" Text='<%# Bind("act_title") %>'></asp:Label>
                                                </ItemTemplate>--%>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="報名/限制人數">
                                                <%--<ItemTemplate>
                                                    <asp:Label ID="act_title_lbl" runat="server" Text='<%# Bind("act_title") %>'></asp:Label>
                                                </ItemTemplate>--%>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="活動日期">
                                                <%--<ItemTemplate>
                                                    <asp:Label ID="act_title_lbl" runat="server" Text='<%# Bind("act_title") %>'></asp:Label>
                                                </ItemTemplate>--%>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="報名起訖">
                                                <%--<ItemTemplate>
                                                    <asp:Label ID="act_title_lbl" runat="server" Text='<%# Bind("act_title") %>'></asp:Label>
                                                </ItemTemplate>--%>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="報名資料">
                                                <ItemTemplate>
                                                    <a href='../S02/S02010102.aspx' target='_blank'>查看</a>/<a href="#">下載</a>
                                                </ItemTemplate>

                                                <HeaderStyle />
                                                <ItemStyle CssClass="center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
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
                                    <asp:Button runat="server" ID="delTest_btn" OnClick="delTest_btn_click" Text="刪除" />
                                    <br />

                                    活動場次<br />
                                    活動序號：<asp:TextBox runat="server" ID="as_act" /><br />
                                    報名人數限制：<asp:TextBox runat="server" ID="as_num_limit" /><br />

                                    <asp:Button runat="server" ID="saveTestSession_btn" OnClick="saveTestSession_btn_click" Text="送出" /><br />

                                    活動報名<br />
                                    活動序號：<asp:TextBox runat="server" ID="aa_act" /><br />
                                    場次序號：<asp:TextBox runat="server" ID="aa_as" /><br />

                                    <asp:Button runat="server" ID="saveTestApply_btn" OnClick="saveTestApply_btn_click" Text="送出" />
                                </div>
                            </div>
                        </div>
                        <!--頁籤 END-->
				    </div>
                </div>
                <!--main content end-->

                <!--Modal START-->
                <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">管理協作者</h4>
                            </div>

                            <div class="modal-body">
                                <table class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>帳號</th>
                                            <th>權限</th>
                                            <th></th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        <tr>
                                            <td>阿爽</td>
                                            <td>
                                                <select class="form-control">
                                                    <option>編輯</option>
                                                    <option>只能閱讀</option>
                                                </select>
                                            </td>
                                            <td>
                                                <a role="button" class="btn btn-theme">刪除</a>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>黃翔</td>
                                            <td>
                                                <select class="form-control">
                                                    <option>只能閱讀</option>
                                                    <option>編輯</option>
                                                </select>
                                            </td>
                                            <td>
                                                <a role="button" class="btn btn-theme">刪除</a>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>江奕軒</td>
                                            <td>
                                                <select class="form-control">
                                                    <option>編輯</option>
                                                    <option>只能閱讀</option>
                                                </select>
                                            </td>
                                            <td>
                                                <a role="button" class="btn btn-theme">刪除</a>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <hr />

                                <div class="form-horizontal style-form" method="get">
                                    <div class="form-group">
                                        <label class="col-sm-2 col-sm-2 control-label">新增協作者</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 col-sm-2 control-label">權限</label>
                                        <div class="col-sm-8">
                                            <select class="form-control">
                                                <option>編輯</option>
                                                <option>只能閱讀</option>
                                            </select>
                                        </div>

                                        <div class="col-sm-2">
                                            <a href="#" role="button" type="submit" class="btn btn-theme">送出</a>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="modal-footer">
                                <asp:Button class="btn btn-theme btn-lg" ID="saveHelp_btn" runat="server" OnClick="saveHelp_btn_Click" Text="確認" />
                            </div>
                        </div>
                    </div>
                </div>
                <!--Modal END-->

                <uc1:UCGridViewPager runat="server" ID="ucGridViewPager" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
