<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="Sign_Search_Context.aspx.cs" Inherits="ActivityApply.Sign_Search_Context" %>

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
        .grid {
            width: 100%;
        }

            .grid td, .grid th {
                text-align: center;
                padding: 8px;
            }
        /*GridView分頁顏色*/
        .PagerCss TD A:hover {
            WIDTH: 20px;
            COLOR: black;
        }

        .PagerCss TD A:active {
            WIDTH: 20px;
            COLOR: black;
        }

        .PagerCss TD A:link {
            WIDTH: 20px;
            COLOR: black;
        }

        .PagerCss TD A:visited {
            WIDTH: 20px;
            COLOR: black;
        }

        .PagerCss TD SPAN {
            FONT-WEIGHT: bold;
            FONT-SIZE: 20px;
            WIDTH: 20px;
            COLOR: #6196FF;
            text-decoration: underline;
        }

        table {
            border-spacing: 0;
            border-collapse: collapse;
        }

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
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header text-info">報名查詢</h1>
        </div>
    </div>
    <div class="row">


        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">信箱E-MAIL</div>
                <div class="panel-body">
                    <div class="form-group">
                        <asp:TextBox ID="aa_email_txt" CssClass="form-control" runat="server" placeholder="信箱Email" ViewStateMode="Enabled"></asp:TextBox>
                        <asp:HiddenField ID="aa_email_hf" runat="server" ViewStateMode="Enabled" Visible="false" />
                    </div>
                    <asp:Button ID="search_btn" CssClass="btn btn-info" runat="server" Text="送出查詢" OnClick="search_btn_Click" />
                    <a data-toggle="modal" href="SignSearchContext.aspx#myModal" class="btn btn-default" runat="server" id="change_password_btn" visible="false">修改密碼</a>                    <a data-toggle="modal" href="SignSearchContext.aspx#myModal2" class="btn btn-default" runat="server" id="forget_password_btn" visible="false">忘記密碼</a>
                    <div class="row">
                        <div class="col-lg-6"></div>
                        <!-- /.col-lg-6 (nested) -->
                        <!-- /.col-lg-6 (nested) -->
                    </div>
                    <!-- /.row (nested) -->
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->


            <!-- 報名資料GridView -->
            <div class="table-responsive dataTable_wrapper" style="margin-bottom: 70px;">
                <asp:UpdatePanel ID="lup" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="main_gv" class="table table-striped table-bordered table-hover" CssClass="grid" runat="server" AutoGenerateColumns="False" ViewStateMode="Enabled" PageSize="10" OnRowCommand="main_gv_RowCommand" OnRowDeleting="main_gv_RowDeleting" OnRowDataBound="main_gv_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="main_gv_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="活動名稱" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a title="<%# Eval("Act_title") %>">
                                            <input id="Act_title" type="button" class="btn-link" value='<%# Eval("Act_title") %>' onclick="GoToActivity(<%# Eval("Act_idn") %> , '<%# Eval("As_date_end") %>    ');" style="width: 200px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;" /></a>
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
                                        <a title="<%# Eval("As_title") %>" style="color: #333333; cursor: default">
                                            <p style="width: 110px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis; margin: 0px">
                                                <asp:Label ID="As_title_lbl" runat="server" Text='<%# Bind("As_title") %>'></asp:Label></p>
                                        </a>
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
                                <asp:TemplateField HeaderText="編輯" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="edit_btn" runat="server" Text="修改資料" CommandName="Custom_Edit" ToolTip="編輯" CssClass="btn btn-info" UseSubmitBehavior="false" CommandArgument='<%# Container.DataItemIndex%>' />
                                        <asp:Button ID="delete_btn" runat="server" CommandName="Custom_Delete" Text="取消報名" ToolTip="刪除" CssClass="btn btn-default" UseSubmitBehavior="false" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" CommandArgument='<%# Container.DataItemIndex%>' />
                                        <asp:Button runat="server" ID="print_btn" CommandName="Custom_dowload" Text="下載資訊" ToolTip="下載" CssClass="btn btn-default" UseSubmitBehavior="false" CommandArgument='<%# Container.DataItemIndex%>' />
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

                            <HeaderStyle Wrap="False" BackColor="White" Font-Bold="True" ForeColor="black" Height="38px" BorderColor="whitesmoke" BorderStyle="Solid" BorderWidth="3px" />
                            <PagerSettings FirstPageText="第一頁" LastPageText="最末頁" NextPageText="下一頁" PreviousPageText="上一頁" Mode="NumericFirstLast" />


                            <PagerStyle BackColor="White" ForeColor="black" CssClass="PagerCss" BorderColor="whitesmoke" BorderStyle="Solid" BorderWidth="3px" />
                            <RowStyle Wrap="true" BackColor="#EDEDED" BorderColor="whitesmoke" BorderStyle="Solid" BorderWidth="3px" />


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
        </div>
        <!-- /.col-lg-12 -->
    </div>

    <asp:Button ID="download" runat="server" Text="Button" Style="display: none" OnClick="download_Click" />
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
                    <asp:TextBox CssClass="form-control placeholder-no-fix" ID="password_txt" runat="server" TextMode="Password" minlength="4" MaxLength="20"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="password_cancle_btn" runat="server" Text="取消" CssClass="btn btn-default" />
                    <asp:Button ID="password_ok_btn" runat="server" Text="確定" OnClick="password_ok_btn_Click" CssClass="btn btn-info" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:ModalPopupExtender ID="password_pop" runat="server" PopupControlID="password_pl" TargetControlID="Button1" CancelControlID="password_cancle_btn"></asp:ModalPopupExtender>


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
                    <asp:TextBox ID="old_password_txt" runat="server" CssClass="form-control placeholder-no-fix" TextMode="Password" minlength="4" MaxLength="20"></asp:TextBox>
                    <p class="p">新密碼</p>
                    <asp:TextBox ID="new_password_txt" runat="server" CssClass="form-control placeholder-no-fix" TextMode="Password" minlength="4" MaxLength="20"></asp:TextBox>
                    <p class="p">再次確認密碼</p>
                    <asp:TextBox ID="new_password_check_txt" runat="server" CssClass="form-control placeholder-no-fix" TextMode="Password" minlength="4" MaxLength="20"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button data-dismiss="modal" class="btn btn-default" type="button">取消</button>
                    <asp:Button ID="change_password_ok_btn" runat="server" Text="確定" OnClick="change_password_ok_btn_Click" CssClass="btn btn-info" />
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
                    <button data-dismiss="modal" class="btn btn-default" type="button">取消</button>
                    <asp:Button ID="get_password_ok_btn" runat="server" Text="確定" OnClientClick="return check()" OnClick="get_password_ok_btn_Click" CssClass="btn btn-info" />
                </div>
            </div>
        </div>
    </div>
    <!-- modal -->


    <script type="text/javascript">
        $(document).ready(function () {
            //判斷更改密碼內新密碼確是否相同
            $("#<%=new_password_txt.ClientID %>").rules("add", { required: true, minlength: 4 });
            $("#<%=new_password_check_txt.ClientID %>").rules("add", { required: true, equalTo: $("#<%=new_password_txt.ClientID %>") });
            //設定麵包削尋覽
            setSessionBread();
            
            //打完Email按下Enter會去按查詢按鈕
            $("#<%=aa_email_txt.ClientID %>").keypress(function (event) {
                if (event.keyCode == 13) {
                    $("#<%=search_btn.ClientID %>").click();
                    return false;
                }
            });
            //打完密碼按下Enter會去按確認按鈕
            $("#<%=password_txt.ClientID %>").keypress(function (event) {
                if (event.keyCode == 13) {
                    $("#<%=password_ok_btn.ClientID %>").click();
                    return false;
                }
            });
            //更改密碼Enter事件
            $("#<%=old_password_txt.ClientID %>").keypress(function (event) {
                if (event.keyCode == 13) {
                    $("#<%=change_password_ok_btn.ClientID %>").click();
                    return false;
                }
            });
            $("#<%=new_password_txt.ClientID %>").keypress(function (event) {
                if (event.keyCode == 13) {
                    $("#<%=change_password_ok_btn.ClientID %>").click();
                    return false;
                }
            });
            $("#<%=new_password_check_txt.ClientID %>").keypress(function (event) {
                if (event.keyCode == 13) {
                    $("#<%=change_password_ok_btn.ClientID %>").click();
                    return false;
                }
            });
            //忘記密碼Enter事件
            $("#<%=email_txt.ClientID %>").keypress(function (event) {
                if (event.keyCode == 13) {
                    $("#<%=get_password_ok_btn.ClientID %>").click();
                    return false;
                }
            });
            $("#container").height($("#main-content").height());
        })

        function check(){
            if($.trim($("#<%= email_txt.ClientID %>").val()) == ""){
                alert("請輸入信箱!");
                $("#<%= email_txt.ClientID %>").focus();
                return false;
            }
            else
                return true;
        }
        //#region 設定麵包削尋覽列
        function setSessionBread() {
            //將滅包削內容清空
            $("#add_breach").children().remove();
            //添加首頁
            $("#add_breach").append('<li><a href="Index.aspx">首頁</a></li>');
            $("#add_breach").append('<li><a href="Sign_Search_Context.aspx">報名查詢</a></li>');
        }
        //#endregion

        //#region 活動頁面跳頁
        function GoToActivity(act_idn,as_date_end) { 
            var NowDate = new Date();
            var date_end = new Date(as_date_end);
            //如果活動已結束則不跳頁
            if(date_end > NowDate)
                window.open("Activity.aspx?act_idn=" + act_idn, "_blank"); 
            else
                alert("活動已結束");
        } 
        //#endregion 

        //下載報名資訊
        function download(){
            $("#<%=download.ClientID%>").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>



