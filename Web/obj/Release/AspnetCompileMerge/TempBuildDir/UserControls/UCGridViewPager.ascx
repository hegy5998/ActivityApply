<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCGridViewPager.ascx.cs" Inherits="Web.UserControls.UCGridViewPager" %>
<asp:Panel ID="pager_pl" runat="server" CssClass="grvPageRow" EnableViewState="False" Visible="False">
    <asp:LinkButton ID="prev_lbtn" CssClass="prev" runat="server" Text="上一頁" CommandArgument="Prev" UseSubmitBehavior="False" OnCommand="pager_Command" EnableViewState="False" />
    <asp:LinkButton ID="next_lbtn" runat="server" CssClass="next" Text="下一頁" CommandArgument="Next" UseSubmitBehavior="False" OnCommand="pager_Command" EnableViewState="False" />
    <% if (PageOffset * Util.CommonConvert.GetIntOrZero(GridView.PageIndex / PageOffset) > 0) { %>
        <a href="javascript:void(0)" class="pageIndex" targetIndex="1" onclick="gridViewChgPageIndex(this)">1</a>
        <a href="javascript:void(0)" class="pageIndexDot" targetIndex="<%=PageOffset * Util.CommonConvert.GetIntOrZero(GridView.PageIndex / PageOffset) %>" onclick="gridViewChgPageIndex(this)">...</a>
    <% } %>
    <% int startPageIndex = PageOffset * Util.CommonConvert.GetIntOrZero(GridView.PageIndex / PageOffset);
       int endPageIndex = PageOffset * (Util.CommonConvert.GetIntOrZero(GridView.PageIndex / PageOffset) + 1) - 1;
       if (endPageIndex > GridView.PageCount - 1) endPageIndex = GridView.PageCount - 1;
       for (int i = startPageIndex; i <= endPageIndex; i++)
       { %>
        <a href="javascript:void(0)" class="pageIndex<%=(GridView.PageIndex == i) ? " select" : "" %>" targetIndex="<%=(i+1).ToString() %>" onclick="gridViewChgPageIndex(this)"><%=(i+1).ToString() %></a>
    <% } %>
    <% if (endPageIndex < GridView.PageCount - 1)
       { %>
        <a href="javascript:void(0)" class="pageIndexDot" targetIndex="<%=endPageIndex + 1 + 1 %>" onclick="gridViewChgPageIndex(this)">...</a>
        <a href="javascript:void(0)" class="pageIndex" targetIndex="<%=GridView.PageCount %>" onclick="gridViewChgPageIndex(this)"><%=GridView.PageCount %></a>
    <% } %>
    <asp:Button ID="chgIndex" CssClass="chgIndex" runat="server" Text="切換分頁" UseSubmitBehavior="False" OnCommand="pager_Command" style="display: none" EnableViewState="False" />
    <input type="hidden" name="targetIndex"/>
    <asp:HiddenField ID="currentIndex_hf" runat="server" EnableViewState="False" />
</asp:Panel>