<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S02010401.aspx.cs" Inherits="Web.S02.S02010401" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentSubFunction_cph" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent_cph" runat="server">
        <!--活動查詢 START-->
    <asp:Label ID="caveat" runat="server" Style="color:red; font-size: large;">*請確認為信箱本人才能幫忙重設密碼</asp:Label>
                    <table class="table" style="width: 480px;">
                        <tr class="">
                            <th style="width: 100px;">信箱Email</th>
                            <td>
                                <asp:TextBox runat="server" ID="email_txt" onfocus="javascript:if(this.value=='example@example.com.tw')this.value=''" Text="example@example.com.tw"  Style="width: 280px;background-color: #FEFEFE;"/>
                            <asp:Button runat="server" ID="set_bt" CssClass="btn btn-default" style="margin-left:5px" Text="重設密碼" OnClick="set_bt_Click" OnClientClick="if (!confirm(&quot;確定要重設密碼?&quot;)) return false"/>
                            </td>
                        </tr>
                        <tr class="">
                            <th style="width: 100px;">新密碼</th>
                            <td>
                                <asp:Label ID="password_lb" runat="server" Style="color:red"></asp:Label>
                            </td>
                        </tr>
                    </table>
    
                <!--活動查詢 END-->
</asp:Content>
