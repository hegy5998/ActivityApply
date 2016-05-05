<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCRoleUnitManager.ascx.cs" Inherits="Web.S01.UCRoleUnitManager" %>
<%@ Register Src="~/UserControls/UCGridViewPager.ascx" TagPrefix="uc1" TagName="UCGridViewPager" %>
<%@ Register Src="~/UserControls/UCUnitDDL.ascx" TagPrefix="uc1" TagName="UCUnitDDL" %>
<%@ Register Src="~/S01/UCRoleUnitPositionManagerDialog.ascx" TagPrefix="uc1" TagName="UCRoleUnitPositionManagerDialog" %>



<asp:Panel ID="pl" runat="server" Visible="false">
    <div class="opBox">
        ● 角色「<span class="highlight"><asp:Label ID="sys_rid_lbl" runat="server" Text="" Style="margin-right: 3px"></asp:Label><asp:Label ID="sys_rname_lbl" runat="server" Text=""></asp:Label></span>」具有的單位資料
        <span style="margin-left:15px"><asp:Button ID="setRoleCommonPosition_btn" runat="server" Text="通用職位設定" ToolTip="通用職位設定" UseSubmitBehavior="False" OnClick="setRoleCommonPosition_btn_Click" /></span>
    </div>
    <asp:GridView ID="main_gv" runat="server"
        ItemType="Model.S01.UCRoleUnitManagerInfo.Main"
        DataKeyNames="sys_rid,sys_uid"
        OnRowCommand="main_gv_RowCommand" 
        OnRowCancelingEdit="main_gv_RowCancelingEdit" 
        OnRowDataBound="main_gv_RowDataBound" 
        OnRowDeleting="main_gv_RowDeleting" 
        OnSorting="main_gv_Sorting">
        <Columns>
            <asp:TemplateField ShowHeader="False" HeaderText="操作">
                <HeaderTemplate>
                    <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="btnGrv add" Text="新增" ToolTip="新增" UseSubmitBehavior="False" />
                </HeaderTemplate>
                <HeaderStyle Width="60px" />
                <ItemTemplate>
                    &nbsp;<asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="btnGrv delete" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False"/>
                    <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="setRoleUnitPosition_btn" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                <FooterTemplate>
                    <asp:Button ID="save_btn" runat="server" CommandName="AddSave" CssClass="btnGrv update" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                </FooterTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <EditItemTemplate>
                    &nbsp;<asp:Button ID="save_btn" runat="server" CommandName="Update" CssClass="btnGrv update" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="單位" SortExpression="Sys_uid">
                <ItemTemplate>
                    <asp:Label ID="sys_uid_lbl" runat="server" Text="<%# Item.Sys_uid %>"></asp:Label> <asp:Label ID="sys_uname_lbl" runat="server" Text="<%# Item.Sys_uname %>"></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="rowTrigger" Wrap="false" />
                <FooterTemplate>
                    <uc1:UCUnitDDL runat="server" ID="ucUnitDDL" />
                </FooterTemplate>
                <EditItemTemplate>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="帳號數" SortExpression="Act_count">
                <ItemTemplate>
                    <%# Item.Act_count %>
                </ItemTemplate>
                <HeaderStyle Width="40px" Wrap="False"  />
                <ItemStyle HorizontalAlign="Center"  Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="單位專用&lt;br/&gt;職位數" SortExpression="Sys_rpid_count">
                <ItemTemplate>
                    <%# Item.Sys_rpid_count %> <asp:Button ID="setRoleUnitPosition_btn" runat="server" CommandArgument='<%# Item.Sys_rid+","+Item.Sys_uid %>' CssClass="btnGrv set" Text="設定單位特殊職位" ToolTip="設定單位特殊職位" UseSubmitBehavior="False" OnClick="setRoleUnitPosition_btn_Click" />
                </ItemTemplate>
                <HeaderStyle Width="40px" Wrap="False"  />
                <ItemStyle HorizontalAlign="Center"  Wrap="False" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <uc1:UCGridViewPager runat="server" ID="ucGridViewPager" />
    <uc1:UCRoleUnitPositionManagerDialog runat="server" ID="ucRoleUnitPositionManagerDialog" />
</asp:Panel>