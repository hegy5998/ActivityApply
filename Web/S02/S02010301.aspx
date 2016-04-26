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
                <asp:GridView ID="main_gv" Width="600px" runat="server" AutoGenerateColumns="false" ViewStateMode="Enabled" PageSize="20"  OnSorting="main_gv_Sorting" OnRowCommand="main_gv_RowCommand" OnRowDeleting="main_gv_RowDeleting" OnRowDataBound="main_gv_RowDataBound" OnRowEditing="main_gv_RowEditing" OnRowCancelingEdit="main_gv_RowCancelingEdit" OnRowUpdating="main_gv_RowUpdating">
                    <Columns>
                        <asp:TemplateField ShowHeader="False" HeaderText="操作">
                            <EditItemTemplate>
                                <asp:Button ID="save_btn" runat="server" CommandName="Update" CssClass="btnGrv update" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                                &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="save_btn" runat="server" CommandName="AddSave" CssClass="btnGrv update" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                                &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                            </FooterTemplate>
                            <HeaderTemplate>
                                <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="btnGrv add" Text="新增" ToolTip="新增" UseSubmitBehavior="False" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button ID="edit_btn" runat="server" Text="修改報名資料" CommandName="Edit" ToolTip="編輯" CssClass="btnGrv edit" UseSubmitBehavior="false" CommandArgument='<%# Container.DataItemIndex%>' />
                                &nbsp;<asp:Button ID="delete_btn" runat="server" CommandName="Delete" Text="取消" ToolTip="刪除" CssClass="btnGrv delete" UseSubmitBehavior="false" OnClientClick="if (!confirm(&quot;確定要刪除嗎?該分類內的活動將不被看到!&quot;)) return false" CommandArgument='<%# Container.DataItemIndex%>' />
                                <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="分類名稱">
                            <EditItemTemplate>
                                <asp:TextBox ID="Ac_title_txt" runat="server" CssClass="center" Text='<%# Bind("Ac_title")  %>' Width="100px"></asp:TextBox>
                                <asp:HiddenField ID="Ac_idn_hf" runat="server" Value='<%# Bind("Ac_idn") %>' Visible="false" ViewStateMode="Enabled" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="Ac_title_txt" runat="server" CssClass="center" Width="100px"></asp:TextBox>
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
                                <asp:TextBox ID="Ac_desc_txt" runat="server" CssClass="center" Text='<%# Bind("Ac_desc")  %>' Width="100px"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="Ac_desc_txt" runat="server" CssClass="center" Width="100px"></asp:TextBox>
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
