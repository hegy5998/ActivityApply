<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S01000401.aspx.cs" Inherits="Web.S01.S01000401" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/UCSystemModule.ascx" TagPrefix="uc1" TagName="UCSystemModule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Lib/jquery-sortable-0.9.13.js") %>"></script>
    <style>
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
    <script>
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
    <asp:Button ID="setOrder_btn" runat="server" Text="設定順序" OnClick="setOrder_btn_Click" UseSubmitBehavior="false" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent_cph" runat="server">
    
    <asp:ModalPopupExtender ID="popupWindow_mpe" runat="server" 
        PopupControlID="popupWindow_pl" 
        TargetControlID="popupWindow_cancel_btn" 
        BackgroundCssClass="popupWindowOverlay"></asp:ModalPopupExtender>
    <asp:Panel ID="popupWindow_pl" style="display:none;overflow:visible;width:300px;" class="popupWindow" runat="server">
        <div class="popupWindowHeader">
            <div class="title">
                <asp:Label ID="popupWindow_tilte_lbl" runat="server" Text="設定順序"></asp:Label>
            </div>
            <asp:Button ID="popupWindow_cancel_btn" CssClass="close" runat="server" Text="關閉" ToolTip="關閉" UseSubmitBehavior="False" OnClick="popupWindow_cancel_btn_Click" />
        </div>
        <div class="popupWindowContent">
            <asp:UpdatePanel runat="server" ID="popupWindow_content_upl" style="display: inline-block; min-width:100%">
                <ContentTemplate>
                    <ol id="orderList">
                        <asp:Repeater ID="order_rt" runat="server">
                            <ItemTemplate>
                                <li><span class="orderKey"><%# Eval("sys_mid") %></span>：<%# Eval("sys_mname") %>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ol>
                    <asp:HiddenField ID="orderList_hf" runat="server" />
                    <div class="center" style="margin-top:8px">
                        <asp:Button ID="setOrderOK_btn" runat="server" Text="儲存" OnClick="setOrderOK_btn_Click" OnClientClick="setOrderList()" />&nbsp;<asp:Button ID="setOrderCancel_btn" runat="server" Text="取消"  OnClick="popupWindow_cancel_btn_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <div style="margin-bottom: 8px">
        <uc1:UCSystemModule runat="server" ID="ucSystemModule" EnableModule="False" EnableShowAllModule="False" EnableShowAllSystem="False" />
    </div>
    <asp:GridView ID="main_gv" runat="server" AutoGenerateColumns="False" OnRowCommand="main_gv_RowCommand" OnRowEditing="main_gv_RowEditing" OnRowUpdating="main_gv_RowUpdating" OnRowCancelingEdit="main_gv_RowCancelingEdit" ViewStateMode="Enabled" OnRowDeleting="main_gv_RowDeleting" OnRowDataBound="main_gv_RowDataBound" OnSorting="main_gv_Sorting">
        <Columns>
            <asp:TemplateField ShowHeader="False" HeaderText="操作">
                <EditItemTemplate>
                    &nbsp;<asp:Button ID="save_btn" runat="server" CommandName="Update" CssClass="btnGrv update" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                    <asp:HiddenField ID="sys_mid_hf" runat="server" Value='<%# Eval("Sys_mid") %>' />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="save_btn" runat="server" CommandName="AddSave" CssClass="btnGrv update" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                </FooterTemplate>
                <HeaderTemplate>
                    <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="btnGrv add" Text="新增" ToolTip="新增" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Button ID="edit_btn" runat="server" CommandName="Edit" CssClass="btnGrv edit" Text="編輯" ToolTip="編輯" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                    &nbsp;<asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="btnGrv delete" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                    <asp:HiddenField ID="sys_mid_hf" runat="server" Value='<%# Eval("Sys_mid") %>' />
                    <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="模組代碼" SortExpression="Sys_mid">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="center" Text='<%# Bind("Sys_mid") %>' Width="50px" MaxLength="5"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="sys_mid_txt" runat="server" CssClass="center" Text='<%# Bind("Sys_mid") %>' Width="50px" MaxLength="5"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="sys_mid_lbl" runat="server" Text='<%# Bind("Sys_mid") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Center" CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="模組名稱" SortExpression="Sys_mname">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Sys_mname") %>' Width="95%" MaxLength="20"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="sys_mname_txt" runat="server" Text='<%# Bind("Sys_mname") %>' Width="95%" MaxLength="20"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="sys_mname_lbl" runat="server" Text='<%# Bind("Sys_mname") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Left" />
                <HeaderStyle />
                <ItemStyle CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="順序" SortExpression="Sys_mseq">
                <EditItemTemplate>
                    &nbsp;<asp:TextBox ID="TextBox4" runat="server" CssClass="center posInteger" Text='<%# Bind("Sys_mseq") %>' Width="50px" MaxLength="2"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="sys_mseq_txt" runat="server" CssClass="center posInteger" Text='<%# Bind("Sys_mseq") %>' Width="50px" MaxLength="2"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    &nbsp;<asp:Label ID="Label5" runat="server" Text='<%# Bind("Sys_mseq") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <HeaderStyle Width="140px" />
                <ItemStyle HorizontalAlign="Center" CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="啟用" SortExpression="Sys_enable">
                <EditItemTemplate>
                    <asp:RadioButtonList ID="sys_enable_rbl" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Bind("Sys_enable") %>'>
                        <asp:ListItem Value="Y">啟用</asp:ListItem>
                        <asp:ListItem Value="N">停用</asp:ListItem>
                    </asp:RadioButtonList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:RadioButtonList ID="sys_enable_rbl" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Bind("Sys_enable") %>'>
                        <asp:ListItem Selected="True" Value="Y">啟用</asp:ListItem>
                        <asp:ListItem Value="N">停用</asp:ListItem>
                    </asp:RadioButtonList>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# (Eval("Sys_enable") ?? "").ToString() == "Y" ? "啟用":"<span style=\"color:red\">停用</span>" %>' ></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <HeaderStyle Width="115px" />
                <ItemStyle HorizontalAlign="Center" CssClass="rowTrigger" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
