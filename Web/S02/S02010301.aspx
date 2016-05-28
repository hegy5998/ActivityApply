<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S02010301.aspx.cs" Inherits="Web.S02.S02010301" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentSubFunction_cph" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent_cph" runat="server">
    <!-- 分類資料GridView -->
    <div class="advanced-form row">
        <asp:UpdatePanel ID="lup" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="main_gv" Width="600px" runat="server" AutoGenerateColumns="false" ViewStateMode="Enabled" PageSize="20" OnSorting="main_gv_Sorting" OnRowCommand="main_gv_RowCommand" OnRowDeleting="main_gv_RowDeleting" OnRowDataBound="main_gv_RowDataBound" OnRowEditing="main_gv_RowEditing" OnRowCancelingEdit="main_gv_RowCancelingEdit" OnRowUpdating="main_gv_RowUpdating">
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
                                    <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="movedown" Text="新增" ToolTip="新增" UseSubmitBehavior="False" />
                                </span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <span style="position: relative;">
                                    <i class="fa fa-pencil btn btn-primary btn-xs" aria-hidden="true"></i>
                                    <asp:Button ID="edit_btn" runat="server" Text="修改報名資料" CommandName="Edit" ToolTip="編輯" CssClass="movedown" UseSubmitBehavior="false" CommandArgument='<%# Container.DataItemIndex%>' />
                                </span>
                                &nbsp;
                                <span style="position: relative;">
                                    <i class="fa fa-trash-o btn btn-danger btn-xs" aria-hidden="true"></i>
                                    <asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="movedown" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False"/>
                                </span>
                                <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="分類名稱">
                            <EditItemTemplate>
                                <asp:TextBox ID="Ac_title_txt" runat="server" CssClass="center" Text='<%# Bind("Ac_title")  %>' Width="100px" MaxLength="30"></asp:TextBox>
                                <asp:HiddenField ID="Ac_idn_hf" runat="server" Value='<%# Bind("Ac_idn") %>' Visible="false" ViewStateMode="Enabled" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="Ac_title_txt" runat="server" CssClass="center" Width="100px" MaxLength="30"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Ac_title_lb" runat="server" Text='<%# Bind("Ac_title") %>'></asp:Label>
                                <asp:HiddenField ID="Ac_idn_hf" runat="server" Value='<%# Bind("Ac_idn") %>' Visible="false" ViewStateMode="Enabled" />
                            </ItemTemplate>
                            <ItemStyle CssClass="rowTrigger" HorizontalAlign="Center" Width="100px" />
                            <FooterStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="分類敘述">
                            <EditItemTemplate>
                                <asp:TextBox ID="Ac_desc_txt" runat="server" CssClass="center" Text='<%# Bind("Ac_desc")  %>' Width="100px" MaxLength="255"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="Ac_desc_txt" runat="server" CssClass="center" Width="100px" MaxLength="255"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Ac_desc_lb" runat="server" Text='<%# Bind("Ac_desc") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="rowTrigger" HorizontalAlign="Center" Width="100px" />
                            <FooterStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="分類順序">
                            <EditItemTemplate>
                                <asp:TextBox ID="Ac_seq_txt" runat="server" CssClass="center" Text='<%# Bind("Ac_seq")  %>' Width="50px" MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="Ac_seq_txt" runat="server" CssClass="center" Width="50px" MaxLength="4"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Ac_seq_lb" runat="server" Text='<%# Bind("Ac_seq") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="rowTrigger" HorizontalAlign="Center" Width="100px" />
                            <FooterStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle Width="30px" Wrap="False" />
                    <RowStyle Wrap="False" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="main_gv" EventName="DataBound" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
