<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S01000501.aspx.cs" Inherits="Web.S01.S01000501" %>

<%@ Import Namespace="Util" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/UCSystemModule.ascx" TagPrefix="uc1" TagName="UCSystemModule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Lib/jquery-sortable-0.9.13.js") %>"></script>
    <style type="text/css">
        body.dragging, body.dragging * {
            cursor: pointer !important;
        }

        .dragged {
            position: absolute;
            opacity: 0.5;
            z-index: 2000;
        }

        ol#orderList {
            margin: 0;
            padding-left: 25px;
        }

            ol#orderList li {
                cursor: pointer;
            }

                ol#orderList li.placeholder {
                    position: relative;
                }

                    ol#orderList li.placeholder:before {
                        position: absolute;
                    }
    </style>
    <script type="text/javascript">
        function setOrderList() {
            var val = "";
            var items = $("#orderList li .orderKey").each(function () {
                val += $(this).html() + ",";
            });
            $("[id$=orderList_hf]").val(val);
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentSubFunction_cph" runat="server">
    <asp:Button ID="setOrder_btn" runat="server" CssClass="btn btn-default" Text="設定順序" OnClick="setOrder_btn_Click" UseSubmitBehavior="false" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent_cph" runat="server">

    <asp:ModalPopupExtender ID="popupWindow_mpe" runat="server"
        PopupControlID="popupWindow_pl"
        TargetControlID="popupWindow_cancel_btn"
        BackgroundCssClass="popupWindowOverlay">
    </asp:ModalPopupExtender>
    <asp:Panel ID="popupWindow_pl" Style="display: none; overflow: visible; width: 300px;" class="popupWindow" runat="server">
        <div class="popupWindowHeader">
            <div class="title">
                <asp:Label ID="popupWindow_tilte_lbl" runat="server" Text="設定順序"></asp:Label>
            </div>
            <asp:Button ID="popupWindow_cancel_btn" CssClass="close" runat="server" Text="關閉" ToolTip="關閉" UseSubmitBehavior="False" OnClick="popupWindow_cancel_btn_Click" />
        </div>
        <div class="popupWindowContent">
            <asp:UpdatePanel runat="server" ID="popupWindow_content_upl" style="display: inline-block; min-width: 100%">
                <ContentTemplate>
                    <ol id="orderList">
                        <asp:Repeater ID="order_rt" runat="server">
                            <ItemTemplate>
                                <li><span class="orderKey"><%# Eval("sys_pid") %></span>：<%# Eval("sys_pname") %>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ol>
                    <asp:HiddenField ID="orderList_hf" runat="server" />
                    <div class="center" style="margin-top: 8px">
                        <asp:Button ID="setOrderOK_btn" runat="server" CssClass="btn btn-default" Text="儲存" OnClick="setOrderOK_btn_Click" OnClientClick="setOrderList()" />&nbsp;<asp:Button ID="setOrderCancel_btn" runat="server" CssClass="btn btn-default" Text="取消" OnClick="popupWindow_cancel_btn_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <div class="opBox">
        <uc1:UCSystemModule runat="server" ID="ucSystemModule" EnableModule="True" EnableShowAllModule="True" EnableShowAllSystem="False" />
        &nbsp;<span class="alert">※ [連結]空白的作業，不會顯示在Menu。</span>
    </div>
    <asp:GridView ID="main_gv" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowCancelingEdit="main_gv_RowCancelingEdit" OnRowCommand="main_gv_RowCommand" OnRowEditing="main_gv_RowEditing" OnRowDataBound="main_gv_RowDataBound" OnRowUpdating="main_gv_RowUpdating" OnRowDeleting="main_gv_RowDeleting" OnSorting="main_gv_Sorting">
        <Columns>
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
                    <asp:HiddenField ID="old_sys_mid_hf" runat="server" Value='<%# Eval("Sys_mid") %>' />
                    <asp:HiddenField ID="old_sys_pid_hf" runat="server" Value='<%# Eval("Sys_pid") %>' />
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
                    <asp:HiddenField ID="old_sys_pid_hf" runat="server" Value='<%# Eval("Sys_pid") %>' />
                    <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Center" Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="系統&lt;br/&gt;代碼" SortExpression="Sys_id">
                <EditItemTemplate>
                    <asp:Label ID="sys_id_lbl" runat="server" Text='<%# Bind("Sys_id") %>'></asp:Label>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="sys_id_lbl" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="sys_id_lbl" runat="server" Text='<%# Bind("Sys_id") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <HeaderStyle Width="35px" />
                <ItemStyle HorizontalAlign="Center" CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="系統名稱" SortExpression="Sys_id">
                <EditItemTemplate>
                    <asp:DropDownList ID="sys_id_ddl" runat="server" AutoPostBack="True" DataSourceID="sys_id_ods" DataTextField="Sys_name" DataValueField="Sys_id" OnSelectedIndexChanged="sys_id_ddl_SelectedIndexChanged" SelectedValue='<%# Bind("Sys_id") %>' Width="98%">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="sys_id_ods" runat="server" SelectMethod="GetSystemList" TypeName="BusinessLayer.S01.S010005BL" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="sys_id_ddl" runat="server" AutoPostBack="True" DataSourceID="sys_id_ods" DataTextField="Sys_name" DataValueField="Sys_id" OnSelectedIndexChanged="sys_id_ddl_SelectedIndexChanged" Width="98%">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="sys_id_ods" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetSystemList" TypeName="BusinessLayer.S01.S010005BL"></asp:ObjectDataSource>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="sys_name_lbl" runat="server" Text='<%# Eval("Sys_name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="模組&lt;br/&gt;代碼" SortExpression="Sys_mid">
                <EditItemTemplate>
                    <asp:Label ID="sys_mid_lbl" runat="server" Text='<%# Bind("Sys_mid") %>'></asp:Label>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="sys_mid_lbl" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="sys_mid_lbl" runat="server" Text='<%# Bind("Sys_mid") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <HeaderStyle Width="50px" />
                <ItemStyle HorizontalAlign="Center" CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="模組名稱" SortExpression="Sys_mname">
                <EditItemTemplate>
                    <asp:DropDownList ID="sys_mid_ddl" runat="server" DataTextField="Sys_mname" DataValueField="Sys_mid" Width="98%" AutoPostBack="True" OnSelectedIndexChanged="sys_mid_ddl_SelectedIndexChanged">
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="sys_mid_ddl" runat="server" AutoPostBack="True" DataTextField="Sys_mname" DataValueField="Sys_mid" OnSelectedIndexChanged="sys_mid_ddl_SelectedIndexChanged" Width="98%">
                    </asp:DropDownList>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="sys_mname_lbl" runat="server" Text='<%# Bind("Sys_mname") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="作業代碼" SortExpression="Sys_pid">
                <EditItemTemplate>
                    <asp:TextBox ID="sys_pid_txt" runat="server" Text='<%# Bind("Sys_pid") %>' Width="90%" CssClass="center" MaxLength="10"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="sys_pid_txt" runat="server" CssClass="center" MaxLength="10" Text='<%# Bind("Sys_pid") %>' Width="90%"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="sys_pid_lbl" runat="server" Text='<%# Bind("Sys_pid") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="90px" />
                <ItemStyle HorizontalAlign="Center" CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="作業名稱" SortExpression="Sys_pname">
                <EditItemTemplate>
                    <asp:TextBox ID="sys_pname_txt" runat="server" Text='<%# Bind("Sys_pname") %>' Width="90%" MaxLength="50"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="sys_pname_txt" runat="server" MaxLength="50" Text='<%# Bind("Sys_pname") %>' Width="90%"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="sys_pname_lbl" runat="server" Text='<%# Bind("Sys_pname") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="連結">
                <EditItemTemplate>
                    <asp:TextBox ID="sys_purl_txt" runat="server" Text='<%# Bind("Sys_purl") %>' Width="98%"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="sys_purl_txt" runat="server" Text='<%# Bind("Sys_purl") %>' Width="98%"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Sys_purl") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="順序" SortExpression="Sys_seq">
                <EditItemTemplate>
                    <asp:TextBox ID="sys_seq_txt" runat="server" Text='<%# Bind("Sys_seq") %>' CssClass="center" MaxLength="2" Width="80%"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="sys_seq_txt" runat="server" CssClass="center" MaxLength="2" Width="80%" Text='<%# Bind("Sys_seq") %>'></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Sys_seq") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                <ItemStyle HorizontalAlign="Center" CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="顯示於&lt;br/&gt;選單" SortExpression="Sys_show">
                <EditItemTemplate>
                    <asp:DropDownList ID="sys_show_ddl" runat="server" SelectedValue='<%# Bind("Sys_show") %>'>
                        <asp:ListItem Value="Y">顯示</asp:ListItem>
                        <asp:ListItem Value="N">隱藏</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="sys_show_ddl" runat="server" SelectedValue='<%# Bind("Sys_show") %>'>
                        <asp:ListItem Value="Y">顯示</asp:ListItem>
                        <asp:ListItem Value="N">隱藏</asp:ListItem>
                    </asp:DropDownList>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# (Eval("Sys_show") ?? "").ToString().Equals("Y") ? "顯示":"<span style=\"color:blue\">隱藏</span>" %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <HeaderStyle Width="75px" />
                <ItemStyle HorizontalAlign="Center" CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="子功能" ShowHeader="False" SortExpression="Sys_cid_count">
                <EditItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# CommonConvert.GetIntOrZero(Eval("Sys_cid_count")) > 0 ? "<span style=\"color:blue\">" + Eval("Sys_cid_count") +"</span>" : "0" %>'></asp:Label>
                    &nbsp;
                   
                    <span style="position: relative;">
                        <i class="fa fa-cog fa-lg" aria-hidden="true"></i>
                        <asp:Button ID="set_btn" runat="server" CommandName="Set" CssClass="movedown" Text="設定" ToolTip="設定" UseSubmitBehavior="False" />
                    </span>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# CommonConvert.GetIntOrZero(Eval("Sys_cid_count")) > 0 ? "<span style=\"color:blue\">" + Eval("Sys_cid_count") +"</span>" : "0" %>'></asp:Label>
                    &nbsp;
                   
                    <span style="position: relative;">
                        <i class="fa fa-cog fa-lg" aria-hidden="true"></i>
                        <asp:Button ID="set_btn" runat="server" CommandName="Set" CssClass="movedown" Text="設定" ToolTip="設定" UseSubmitBehavior="False" />
                    </span>
                </ItemTemplate>
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="啟用" SortExpression="Sys_enable">
                <EditItemTemplate>
                    <asp:DropDownList ID="sys_enable_ddl" runat="server" SelectedValue='<%# Bind("Sys_enable") %>'>
                        <asp:ListItem Value="Y">啟用</asp:ListItem>
                        <asp:ListItem Value="N">停用</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="sys_enable_ddl" runat="server" SelectedValue='<%# Bind("Sys_enable") %>'>
                        <asp:ListItem Value="Y">啟用</asp:ListItem>
                        <asp:ListItem Value="N">停用</asp:ListItem>
                    </asp:DropDownList>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# (Eval("Sys_enable") ?? "").ToString().Equals("Y") ? "啟用":"<span style=\"color:red\">停用</span>" %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <HeaderStyle Width="65px" />
                <ItemStyle HorizontalAlign="Center" CssClass="rowTrigger" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ModalPopupExtender ID="controlSet_mpe" runat="server" PopupControlID="controlSet_pl" TargetControlID="controlSet_OK_btn" BackgroundCssClass="popupWindowOverlay" OkControlID="controlSet_cancel_btn"></asp:ModalPopupExtender>
    <asp:Panel ID="controlSet_pl" class="popupWindow" runat="server" Visible="false">
        <div class="popupWindowHeader">
            <div class="title">
                <asp:UpdatePanel ID="controlSet_title_upl" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="controlSet_tilte_lbl" runat="server" Text="Label"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:Button ID="controlSet_cancel_btn" CssClass="close" runat="server" Text="關閉" ToolTip="關閉" OnClick="controlSet_cancel_btn_Click" UseSubmitBehavior="False" />
            <asp:Button ID="controlSet_OK_btn" CssClass="open" runat="server" Text="開啟" ToolTip="開啟" UseSubmitBehavior="False" />
        </div>
        <div class="popupWindowContent">
            <asp:UpdatePanel ID="controlSet_content_upl" runat="server">
                <ContentTemplate>
                    <div style="margin-bottom: 5px;">
                        <asp:Label ID="controlSet_sys_pid_lbl" runat="server"></asp:Label>
                        <asp:Label ID="controlSet_sys_pname_lbl" runat="server"></asp:Label>
                    </div>
                    <asp:GridView ID="controlSet_gv" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="controlSet_gv_RowCancelingEdit" OnRowCommand="controlSet_gv_RowCommand" OnRowEditing="controlSet_gv_RowEditing" OnRowUpdating="controlSet_gv_RowUpdating" ShowHeaderWhenEmpty="True" OnRowDeleting="controlSet_gv_RowDeleting" OnRowDataBound="controlSet_gv_RowDataBound" OnSorting="controlSet_gv_Sorting">
                        <Columns>
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
                                    <asp:HiddenField ID="old_sys_pid_hf" runat="server" Value='<%# Eval("Sys_pid") %>' />
                                    <asp:HiddenField ID="old_sys_cid_hf" runat="server" Value='<%# Eval("Sys_cid") %>' />
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
                                    <asp:HiddenField ID="sys_pid_hf" runat="server" Value='<%# Eval("Sys_pid") %>' />
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="子功能名稱" SortExpression="Sys_cid">
                                <EditItemTemplate>
                                    <asp:TextBox ID="sys_cid_txt" runat="server" MaxLength="50" Text='<%# Bind("Sys_cid") %>' Width="95%"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="sys_cid_txt" runat="server" MaxLength="50" Text='<%# Bind("Sys_cid") %>' Width="95%"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="sys_cid_lbl" runat="server" Text='<%# Bind("Sys_cid") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
                                <ItemStyle CssClass="rowTrigger" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="子功能描述" SortExpression="Sys_cnote">
                                <EditItemTemplate>
                                    <asp:TextBox ID="sys_cnote_txt" runat="server" MaxLength="50" Text='<%# Bind("Sys_cnote") %>' Width="95%"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="sys_cnote_txt" runat="server" MaxLength="50" Text='<%# Bind("Sys_cnote") %>' Width="95%"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="sys_cnote_lbl" runat="server" Text='<%# Bind("Sys_cnote") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="rowTrigger" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
</asp:Content>
