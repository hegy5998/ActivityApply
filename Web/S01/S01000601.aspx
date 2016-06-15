<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S01000601.aspx.cs" Inherits="Web.S01.S01000601" %>

<%@ Register Src="~/UserControls/UCGridViewPager.ascx" TagPrefix="uc1" TagName="UCGridViewPager" %>
<%@ Register Src="~/S01/UCRoleUnitManager.ascx" TagPrefix="uc1" TagName="UCRoleUnitManager" %>
<%@ Register Src="~/S01/UCRoleUnitPositionManagerDialog.ascx" TagPrefix="uc1" TagName="UCRoleUnitPositionManagerDialog" %>


<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent_cph" runat="server">
    <div style="display: table;">
        <div style="display: table-cell; vertical-align: top; padding-right: 20px">
            <div class="opBox">
                ● 角色資料
           
            </div>
            <asp:GridView ID="main_gv" runat="server"
                ItemType="Model.S01.S010006Info.Main"
                DataKeyNames="sys_rid"
                OnRowCommand="main_gv_RowCommand"
                OnRowEditing="main_gv_RowEditing"
                OnRowUpdating="main_gv_RowUpdating"
                OnRowCancelingEdit="main_gv_RowCancelingEdit"
                OnRowDeleting="main_gv_RowDeleting"
                OnRowDataBound="main_gv_RowDataBound"
                OnSorting="main_gv_Sorting"
                Width="600px">
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
                            <asp:HiddenField ID="sys_rid_hf" runat="server" Value='<%# Item.Sys_rid %>' />
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
                            <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="setRoleUnit_btn" />
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="角色&lt;br/&gt;代碼" SortExpression="Sys_rid">
                        <EditItemTemplate>
                            <asp:TextBox ID="sys_rid_txt" runat="server" CssClass="center" Text='<%# BindItem.Sys_rid %>' Width="50px" MaxLength="4"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="sys_rid_txt" runat="server" CssClass="center" Width="50px" MaxLength="4"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="sys_rid_lbl" runat="server" Text='<%# Item.Sys_rid %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Center" CssClass="rowTrigger" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="角色名稱" SortExpression="Sys_rname">
                        <EditItemTemplate>
                            <asp:TextBox ID="sys_rname_txt" runat="server" Text='<%# BindItem.Sys_rname %>' Width="95%" MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="sys_rname_txt" runat="server" Width="95%" MaxLength="20"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="sys_rname_lbl" runat="server" Text='<%# Item.Sys_rname %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="100px" />
                        <ItemStyle CssClass="rowTrigger" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="角色說明" SortExpression="Sys_rnote">
                        <EditItemTemplate>
                            <asp:TextBox ID="sys_rnote_txt" runat="server" Text='<%# BindItem.Sys_rnote %>' Width="95%" MaxLength="100"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="sys_rnote_txt" runat="server" Width="95%" MaxLength="100"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="sys_rnote_lbl" runat="server" Text='<%# Item.Sys_rnote %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="rowTrigger" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="帳號數" SortExpression="Act_count">
                        <ItemTemplate>
                            <%# Item.Act_count %>
                        </ItemTemplate>
                        <HeaderStyle Width="30px" Wrap="False" />
                        <ItemStyle CssClass="rowTrigger" HorizontalAlign="Center" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="通用&lt;br/&gt;職位數" SortExpression="Common_position_count">
                        <ItemTemplate>
                            <span style="margin-right: 2px"><%# Item.Common_position_count %></span>
                        </ItemTemplate>
                        <HeaderStyle Width="30px" Wrap="False" />
                        <ItemStyle CssClass="rowTrigger" HorizontalAlign="Center" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="單位數" SortExpression="Sys_uid_count">
                        <ItemTemplate>
                            <span style="margin-right: 2px"><%# Item.Sys_uid_count %></span>
                            <span style="position: relative;">
                        <i class="fa fa-cog fa-lg" aria-hidden="true"></i>
                            <asp:Button ID="setRoleUnit_btn" runat="server" CommandArgument='<%# BindItem.Sys_rid %>' CssClass="movedown" Text="設定單位" ToolTip="設定單位" OnClick="setRoleUnit_btn_Click" UseSubmitBehavior="False" />
                        </span>
                                </ItemTemplate>
                        <HeaderStyle Width="30px" Wrap="False" />
                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <uc1:UCGridViewPager runat="server" ID="ucGridViewPager" />
        </div>
        <div style="min-width: 300px; display: table-cell; vertical-align: top; white-space: nowrap">
            <uc1:UCRoleUnitManager runat="server" ID="ucRoleUnitManager" />
        </div>
    </div>
</asp:Content>
