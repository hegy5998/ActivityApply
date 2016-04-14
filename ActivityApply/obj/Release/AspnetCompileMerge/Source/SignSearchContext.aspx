﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="SignSearchContext.aspx.cs" Inherits="ActivityApply.SignSearchContext" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
    <style type="text/css">
        .p {
            margin-top: 8px;
        }
    </style>
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

                        <%--<a class="btn btn-theme btn-block" href="SignSearchContext.aspx" type="submit">查詢</a>--%>
                        <asp:Button ID="search_btn" CssClass="btn btn-theme btn-block" runat="server" Text="查詢" OnClick="search_btn_Click" />
                        <a data-toggle="modal" href="SignSearchContext.aspx#myModal" class="btn btn-theme btn-block" runat="server" id="change_password_btn" visible="false">更改密碼</a>
                        <a data-toggle="modal" href="SignSearchContext.aspx#myModal2" class="btn btn-theme btn-block" runat="server" id="forget_password_btn" visible="false">忘記密碼</a>
                    </div>

                    <!-- Modal -->
                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title">更改密碼</h4>
                                </div>
                                <div class="modal-body">
                                    <p class="p">舊密碼</p>
                                    <asp:TextBox ID="old_password_txt" runat="server" CssClass="form-control placeholder-no-fix"></asp:TextBox>
                                    <p class="p">新密碼</p>
                                    <asp:TextBox ID="new_password_txt" runat="server" CssClass="form-control placeholder-no-fix"></asp:TextBox>
                                    <p class="p">再次確認密碼</p>
                                    <asp:TextBox ID="new_password_check_txt" runat="server" CssClass="form-control placeholder-no-fix"></asp:TextBox>
                                </div>
                                <label class="checkbox" style="margin-right: 21px;">
                                    <span class="pull-right">
                                        <a data-toggle="modal" href="SignSearchContext.aspx#myModal2">忘記密碼</a>
                                    </span>
                                </label>
                                <div class="modal-footer">
                                    <button data-dismiss="modal" class="btn btn-theme" type="button">取消</button>
                                    <asp:Button ID="change_password_ok_btn" runat="server" Text="確定" OnClick="change_password_ok_btn_Click" CssClass="btn btn-theme" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- modal -->

                    <!-- Modal -->
                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal2" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title">忘記密碼?</h4>
                                </div>
                                <div class="modal-body">
                                    <p class="p">電子信箱Email</p>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control placeholder-no-fix"></asp:TextBox>
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

                <div class="advanced-form row">
                    <asp:UpdatePanel ID="lup" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="main_gv" runat="server" AutoGenerateColumns="false" ViewStateMode="Enabled" PageSize="20" OnRowCommand="main_gv_RowCommand" OnRowDeleting="main_gv_RowDeleting" OnRowDataBound="main_gv_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="活動名稱">
                                        <ItemTemplate>
                                            <asp:Label ID="Act_title_lbl" runat="server" Text='<%# Bind("Act_title") %>'></asp:Label>
                                            <asp:HiddenField ID="Act_idn_hf" runat="server" Value='<%# Bind("Act_idn") %>' Visible="false" ViewStateMode="Enabled" />
                                            <asp:HiddenField ID="Act_class_hf" runat="server" Value='<%# Bind("Act_class") %>' Visible="false" ViewStateMode="Enabled" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="rowTrigger" HorizontalAlign="Center" Width="700px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="場次名稱">
                                        <ItemTemplate>
                                            <asp:Label ID="As_title_lbl" runat="server" Text='<%# Bind("As_title") %>'></asp:Label>
                                            <asp:HiddenField ID="As_idn_hf" runat="server" Value='<%# Bind("As_idn") %>' Visible="false" ViewStateMode="Enabled" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="rowTrigger" HorizontalAlign="Center" Width="700px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="報名人">
                                        <ItemTemplate>
                                            <asp:Label ID="Aa_name_lbl" runat="server" Text='<%# Bind("Aa_name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="rowTrigger" HorizontalAlign="Center" Width="700px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="報名日期">
                                        <ItemTemplate>
                                            <asp:Label ID="Updtime_lbl" runat="server" Text='<%# Bind("Updtime") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="rowTrigger" HorizontalAlign="Center" Width="700px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="編輯" ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:Button ID="Button1" runat="server" Style="display: none;" Text="Button" />
                                            <asp:Button ID="edit_btn" runat="server" Text="修改報名資料" CommandName="Custom_Edit" ToolTip="編輯" CssClass="btnGrv edit" UseSubmitBehavior="false" CommandArgument='<%# Container.DataItemIndex%>' />
                                            <asp:Button ID="delete_btn" runat="server" CommandName="Delete" Text="取消報名" ToolTip="刪除" CssClass="btnGrv delete" UseSubmitBehavior="false" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" />
                                            <asp:Panel ID="password_pl" runat="server">
                                                <div class="modal-dialog" style="width: 353px;">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h4 class="modal-title">請輸入密碼</h4>
                                                        </div>
                                                        <div class="modal-body">
                                                            <asp:TextBox CssClass="form-control placeholder-no-fix" ID="password_txt" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button ID="password_cancle_btn" runat="server" Text="取消" />
                                                            <asp:Button ID="password_ok_btn" runat="server" Text="確定" OnClick="password_ok_btn_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:ModalPopupExtender ID="password_pop" runat="server" PopupControlID="password_pl" TargetControlID="edit_btn" CancelControlID="password_cancle_btn" OkControlID="password_ok_btn" OnLoad="password_pop_Load"></asp:ModalPopupExtender>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Wrap="False" />
                                <RowStyle Wrap="False" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="main_gv" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </section>
        </section>
    </section>



    <script>
        $(document).ready(function () {
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
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>