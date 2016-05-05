<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="SignSearchContext.aspx.cs" Inherits="ActivityApply.SignSearchContext" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
    <!-- Jquery Validation -->
    <script type="text/javascript" src="<%=ResolveUrl("~/assets/js/validation/jquery.metadata.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/assets/js/validation/jquery.validate.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/assets/js/validation/messages_zh_TW.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/assets/js/validation/additional-methods.js") %>"></script>
    <style type="text/css">
        .p {
            margin-top: 8px;
        }
        /* 驗證錯誤欄位 */
        input[type="password"].error {
            box-shadow: 0px 0px 9px red;
        }
        /* 驗證錯誤訊息文字 */
        label.error {
            color: red;
        }
        /* gridView置中*/
        .grid td, .grid th {
            text-align: center;
        }

        table {
            border-spacing: 0;
            border-collapse: collapse;
        }

        /*.table-striped > tbody > tr:nth-child(odd) > td,
        .table-striped > tbody > tr:nth-child(odd) > th {
            background-color: #f9f9f9;
        }*/

        .div_table-cell {
            display: table-cell;
            text-align: center;
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            //須與form表單ID名稱相同
            $("#form1").validate();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sideCon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainCon" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <section id="container">
        <section id="main-content">
            <section class="wrapper">
                <div class="row"></div>
                <div class="row">
                    <h2 class="form-login-heading" style="margin-left: 44%;">報名查詢</h2>
                    <div class="login-wrap">
                        <asp:TextBox ID="aa_email_txt" CssClass="form-control" runat="server" placeholder="信箱Email" ViewStateMode="Enabled"></asp:TextBox>
                        <asp:HiddenField ID="aa_email_hf" runat="server" ViewStateMode="Enabled" Visible="false" />
                        <br />
                        <asp:Button ID="search_btn" CssClass="btn btn-theme btn-block" runat="server" Text="查詢" OnClick="search_btn_Click" />
                        <a data-toggle="modal" href="SignSearchContext.aspx#myModal" class="btn btn-theme btn-block" runat="server" id="change_password_btn" visible="false">更改密碼</a>
                        <a data-toggle="modal" href="SignSearchContext.aspx#myModal2" class="btn btn-theme btn-block" runat="server" id="forget_password_btn" visible="false">忘記密碼</a>
                    </div>

                    <!-- 更改密碼_Modal -->
                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title">更改密碼</h4>
                                </div>
                                <div class="modal-body">
                                    <p class="p">舊密碼</p>
                                    <asp:TextBox ID="old_password_txt" runat="server" CssClass="form-control placeholder-no-fix" TextMode="Password" minlength="4"></asp:TextBox>
                                    <p class="p">新密碼</p>
                                    <asp:TextBox ID="new_password_txt" runat="server" CssClass="form-control placeholder-no-fix" TextMode="Password" minlength="4"></asp:TextBox>
                                    <p class="p">再次確認密碼</p>
                                    <asp:TextBox ID="new_password_check_txt" runat="server" CssClass="form-control placeholder-no-fix" TextMode="Password" minlength="4"></asp:TextBox>
                                </div>
                                <div class="modal-footer">
                                    <button data-dismiss="modal" class="btn btn-theme" type="button">取消</button>
                                    <asp:Button ID="change_password_ok_btn" runat="server" Text="確定" OnClick="change_password_ok_btn_Click" CssClass="btn btn-theme" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- modal -->

                    <!-- 忘記密碼_Modal -->
                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal2" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title">忘記密碼?</h4>
                                </div>
                                <div class="modal-body">
                                    <p class="p">電子信箱Email</p>
                                    <asp:TextBox ID="email_txt" runat="server" CssClass="form-control placeholder-no-fix"></asp:TextBox>
                                </div>
                                <div class="modal-footer">
                                    <button data-dismiss="modal" class="btn btn-theme" type="button">取消</button>
                                    <asp:Button ID="get_password_ok_btn" runat="server" Text="確定" OnClick="get_password_ok_btn_Click" CssClass="btn btn-theme" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- modal -->
                </div>
                <!-- 報名資料GridView -->
                <div class="advanced-form row div_table-cell">
                    <asp:UpdatePanel ID="lup" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="main_gv" class="table table-striped" CssClass="grid" runat="server" AutoGenerateColumns="False" ViewStateMode="Enabled" PageSize="5" OnRowCommand="main_gv_RowCommand" OnRowDeleting="main_gv_RowDeleting" OnRowDataBound="main_gv_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="活動名稱" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <input id="Act_title" type="button" class="btn-link" value='<%# Eval("Act_title") %>' onclick="GoToActivity(<%# Eval("Act_idn") %>);" style="width: 200px" />
                                            <asp:HiddenField ID="Aa_idn_hf" runat="server" Value='<%# Bind("Aa_idn") %>' Visible="false" ViewStateMode="Enabled" />
                                            <asp:HiddenField ID="Act_idn_hf" runat="server" Value='<%# Bind("Act_idn") %>' Visible="false" ViewStateMode="Enabled" />
                                            <asp:HiddenField ID="Act_class_hf" runat="server" Value='<%# Bind("Act_class") %>' Visible="false" ViewStateMode="Enabled" />
                                            <asp:HiddenField ID="Act_title_hf" runat="server" Value='<%# Bind("Act_title") %>' Visible="false" ViewStateMode="Enabled" />
                                            <asp:HiddenField ID="As_apply_end_hf" runat="server" Value='<%# Bind("As_apply_end") %>' Visible="false" ViewStateMode="Enabled" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="rowTrigger" HorizontalAlign="Center" Width="200px" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="場次名稱" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="As_title_lbl" runat="server" Text='<%# Bind("As_title") %>'></asp:Label>
                                            <asp:HiddenField ID="As_idn_hf" runat="server" Value='<%# Bind("As_idn") %>' Visible="false" ViewStateMode="Enabled" />
                                            <asp:HiddenField ID="As_title_hf" runat="server" Value='<%# Bind("As_title") %>' Visible="false" ViewStateMode="Enabled" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="rowTrigger" HorizontalAlign="Center" Width="130px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="報名人" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Aa_name_lbl" runat="server" Text='<%# Bind("Aa_name") %>'></asp:Label>
                                            <asp:HiddenField ID="Aa_name_hf" runat="server" Value='<%# Bind("Aa_name") %>' Visible="false" ViewStateMode="Enabled" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="rowTrigger" HorizontalAlign="Center" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="活動開始日期" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="As_date_start_lbl" runat="server" Text='<%# Bind("As_date_start") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="rowTrigger" HorizontalAlign="Center" Width="130px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="活動結束日期" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="As_date_end_lbl" runat="server" Text='<%# Bind("As_date_end") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="rowTrigger" HorizontalAlign="Center" Width="130px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="報名日期" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Updtime_lbl" runat="server" Text='<%# Bind("Updtime") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="rowTrigger" HorizontalAlign="Center" Width="130px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="編輯" ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:Button ID="edit_btn" runat="server" Text="修改報名資料" CommandName="Custom_Edit" ToolTip="編輯" CssClass="btnGrv edit" UseSubmitBehavior="false" CommandArgument='<%# Container.DataItemIndex%>' Style="background-color: rgba(0, 0, 0, 0); border-color: rgba(250, 235, 215, 0); color: #2C71BD;" />
                                            <asp:Button ID="delete_btn" runat="server" CommandName="Custom_Delete" Text="取消報名" ToolTip="刪除" CssClass="btnGrv delete" UseSubmitBehavior="false" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" CommandArgument='<%# Container.DataItemIndex%>' Style="background-color: rgba(0, 0, 0, 0); border-color: rgba(250, 235, 215, 0); color: #2C71BD;" />
                                            <asp:Button runat="server" ID="print_btn" CommandName="Custom_dowload" Text="下載資訊" ToolTip="下載" UseSubmitBehavior="false" CommandArgument='<%# Container.DataItemIndex%>' Style="background-color: rgba(0, 0, 0, 0); border-color: rgba(250, 235, 215, 0); color: #2C71BD;" />
                                            <asp:UpdatePanel runat="server" ID="download_upd">
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="print_btn" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle Wrap="False" BackColor="#68dff0" Font-Bold="True" ForeColor="White" Height="38px" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle Wrap="true" BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="main_gv" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <asp:Button ID="download" runat="server" Text="Button" Style="display: none" OnClick="download_Click"/>
                <!-- ModalPopupExtender設定按鈕 -->
                <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" />
                <!-- ModalPopupExtender顯示的Panel -->
                <asp:Panel ID="password_pl" runat="server" Style="display: none">
                    <div class="modal-dialog" style="width: 353px;">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">請輸入密碼</h4>
                            </div>
                            <div class="modal-body">
                                <asp:TextBox CssClass="form-control placeholder-no-fix" ID="password_txt" runat="server" TextMode="Password" minlength="4"></asp:TextBox>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="password_cancle_btn" runat="server" Text="取消" CssClass="btn btn-theme" />
                                <asp:Button ID="password_ok_btn" runat="server" Text="確定" OnClick="password_ok_btn_Click" CssClass="btn btn-theme" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:ModalPopupExtender ID="password_pop" runat="server" PopupControlID="password_pl" TargetControlID="Button1" CancelControlID="password_cancle_btn"></asp:ModalPopupExtender>
            </section>
        </section>
    </section>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=new_password_txt.ClientID %>").rules("add", { required: true, minlength: 4 });
            $("#<%=new_password_check_txt.ClientID %>").rules("add", { required: true, equalTo: $("#<%=new_password_txt.ClientID %>") });
            setSessionBread();
        })
        //#region 設定麵包削尋覽列
        function setSessionBread() {
            //將滅包削內容清空
            $("#add_breach").children().remove();
            //添加首頁
            $("#add_breach").append('<li><a href="index.aspx">首頁</a></li>');
            $("#add_breach").append('<li><a href="SignSearchContext.aspx">報名查詢</a></li>');
        }
        //#endregion

        function GoToActivity(act_idn,As_date_end) { 
            window.open("activity.aspx?act_idn=" + act_idn, "_blank"); 
            //window.location.replace("activity.aspx?act_idn=" + act_idn);
        } 

        function change(){
            window.open("SignChange.aspx", "_blank");
        }

        function download(){
            $("#<%=download.ClientID%>").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>
