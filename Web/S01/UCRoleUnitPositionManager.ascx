<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCRoleUnitPositionManager.ascx.cs" Inherits="Web.S01.UCRoleUnitPositionManager" %>

<%@ Register Src="~/UserControls/UCGridViewPager.ascx" TagPrefix="uc1" TagName="UCGridViewPager" %>
<%@ Register Src="~/UserControls/UCUnitDDL.ascx" TagPrefix="uc1" TagName="UCUnitDDL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Panel ID="pl" runat="server" Visible="false">
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Lib/jquery-sortable-0.9.13.js") %>"></script>
    <asp:ModalPopupExtender ID="popupWindow_mpe" runat="server"
        PopupControlID="popupWindow_pl"
        TargetControlID="popupWindow_cancel_btn"
        BackgroundCssClass="popupWindowOverlay">
    </asp:ModalPopupExtender>
    <asp:Panel ID="popupWindow_pl" Style="display: none; overflow: visible; width: 300px; height: 300px" class="popupWindow" runat="server">
        <div class="popupWindowHeader">
            <div class="title">
                <asp:Label ID="popupWindow_tilte_lbl" runat="server" Text="設定順序"></asp:Label>
            </div>
            <asp:Button ID="popupWindow_cancel_btn" CssClass="close" runat="server" Text="關閉" ToolTip="關閉" UseSubmitBehavior="False" OnClick="popupWindow_cancel_btn_Click" />
        </div>
        <div class="popupWindowContent" style="height: 300px">
            <asp:UpdatePanel runat="server" ID="popupWindow_content_upl" style="display: inline-block; min-width: 100%">
                <ContentTemplate>
                    <ol id="orderList">
                        <asp:Repeater ID="order_rt" runat="server">
                            <ItemTemplate>
                                <li><span class="orderKey"><%# Eval("sys_rpid") %></span>：<%# Eval("sys_rpname") %>
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
        角色：<span class="highlight" style="margin-right: 10px"><asp:Label ID="sys_rid_lbl" runat="server" Text="" Style="margin-right: 3px"></asp:Label><asp:Label ID="sys_rname_lbl" runat="server" Text=""></asp:Label></span>
        單位：<span class="highlight"><asp:Label ID="sys_uid_lbl" runat="server" Text="" Style="margin-right: 3px"></asp:Label><asp:Label ID="sys_uname_lbl" runat="server" Text=""></asp:Label></span>
        <asp:Button ID="setOrder_btn" runat="server" CssClass="btn btn-default" Text="設定順序" UseSubmitBehavior="false" OnClick="setOrder_btn_Click" />
    </div>
    <asp:UpdatePanel ID="updatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="setOrderOK_btn" />
        </Triggers>
        <ContentTemplate>
            <asp:GridView ID="main_gv" runat="server"
                ItemType="Model.S01.UCRoleUnitManagerPositionInfo.Main"
                DataKeyNames="sys_rid,sys_uid,sys_rpid"
                OnRowCommand="main_gv_RowCommand"
                OnRowEditing="main_gv_RowEditing"
                OnRowUpdating="main_gv_RowUpdating"
                OnRowCancelingEdit="main_gv_RowCancelingEdit"
                OnRowDataBound="main_gv_RowDataBound"
                OnRowDeleting="main_gv_RowDeleting"
                OnSorting="main_gv_Sorting">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="操作">
                        <HeaderTemplate>
                            <span style="position: relative;">
                                <i class="fa fa-plus btn btn-info btn-xs" aria-hidden="true"></i>
                                <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="movedown" Text="新增" ToolTip="新增" UseSubmitBehavior="False" />
                            </span>
                        </HeaderTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemTemplate>
                            <span style="position: relative;">
                                <i class="fa fa-pencil btn btn-primary btn-xs" aria-hidden="true"></i>
                                <asp:Button ID="edit_btn" runat="server" CommandName="Edit" CssClass="movedown" Text="編輯" ToolTip="編輯" UseSubmitBehavior="False" />
                            </span>
                            &nbsp;
                    <span style="position: relative;">
                        <i class="fa fa-trash-o btn btn-danger btn-xs" aria-hidden="true"></i>
                        <asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="movedown" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" />
                    </span>
                            <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
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
                        <FooterStyle HorizontalAlign="Center" />
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
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="職位代碼" SortExpression="Sys_rpid">
                        <HeaderStyle Width="100px" />
                        <ItemTemplate>
                            <%# Item.Sys_rpid %>
                        </ItemTemplate>
                        <ItemStyle CssClass="rowTrigger center" Wrap="false" />
                        <FooterTemplate>
                            <asp:Label ID="sys_rpid_lbl" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="職位名稱" SortExpression="Sys_rpname">
                        <ItemTemplate>
                            <%# Item.Sys_rpname %>
                        </ItemTemplate>
                        <ItemStyle CssClass="rowTrigger" Wrap="false" />
                        <FooterTemplate>
                            <asp:TextBox ID="sys_rpname_txt" runat="server" Width="95%"></asp:TextBox>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="sys_rpname_txt" runat="server" Width="95%" Text='<%# BindItem.Sys_rpname %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="排序" SortExpression="sys_seq">
                        <HeaderStyle Width="60px" />
                        <ItemTemplate>
                            <%# Item.Sys_seq %>
                        </ItemTemplate>
                        <ItemStyle CssClass="rowTrigger center" Wrap="false" />
                        <FooterTemplate>
                            <asp:TextBox ID="sys_seq_txt" CssClass="center" runat="server" Width="50px"></asp:TextBox>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="sys_seq_txt" CssClass="center" runat="server" Width="50px" Text='<%# BindItem.Sys_seq %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="帳號數" SortExpression="Act_count">
                        <HeaderStyle Width="60px" />
                        <ItemTemplate><%# Item.Act_count %></ItemTemplate>
                        <ItemStyle CssClass="rowTrigger center" Wrap="false" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <uc1:UCGridViewPager runat="server" ID="ucGridViewPager" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
