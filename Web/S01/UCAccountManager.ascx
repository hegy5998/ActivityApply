<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAccountManager.ascx.cs" Inherits="Web.S01.UCAccountManager" %>
<%@ Register Src="~/S01/UCAccountRoleManager.ascx" TagPrefix="uc1" TagName="UCAccountRoleManager" %>

<style>
    #accountManager tr.fvRow > th {
        width: 100px;
    }
</style>
<asp:FormView ID="main_fv" runat="server" CssClass="fv"
    ItemType="Model.S01.UCAccountManagerInfo.Main"
    DataKeyNames="act_id"
    OnItemInserting="main_fv_ItemInserting"
    OnItemUpdating="main_fv_ItemUpdating">
    <InsertItemTemplate>
        <table class="table" id="accountManager">
            <tr>
                <th><span class="alert">*</span>帳　號</th>
                <td>
                    <asp:TextBox ID="act_id_txt" runat="server" Text='<%# BindItem.Act_id %>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th><span class="alert">*</span>名　稱</th>
                <td>
                    <asp:TextBox ID="act_name_txt" runat="server" Text='<%# BindItem.Act_name %>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>信　箱</th>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# BindItem.Act_mail %>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th><span class="alert">*</span>身　分</th>
                <td style="padding: 5px 9px">
                    <uc1:UCAccountRoleManager runat="server" ID="ucAccountRoleManager" />
                </td>
            </tr>
            <tr>
                <th>密　碼</th>
                <td>
                    <asp:TextBox ID="act_pwd_txt" runat="server" Text='<%# BindItem.Act_pwd %>' TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>密碼確認</th>
                <td>
                    <asp:TextBox ID="act_pwd_confirm_txt" runat="server" Text='<%# Bind("Act_pwd_confirm") %>' TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th></th>
                <td style="text-align: center;padding-right: 140px;">
                    <asp:Button ID="add_btn" runat="server" CssClass="btn btn-default" Text="新增" CommandName="Insert" UseSubmitBehavior="false" />
                    &nbsp;<asp:Button ID="cancel_btn" runat="server" CssClass="btn btn-default" Text="取消" OnClick="cancel_btn_Click" UseSubmitBehavior="false" />
                </td>
            </tr>
        </table>
    </InsertItemTemplate>
    <EditItemTemplate>
        <table class="table" id="accountManager">
            <tr>
                <th><span class="alert"></span>帳　號</th>
                <td>
                    <%# Item.Act_id %>
                </td>
            </tr>
            <tr>
                <th><span class="alert">*</span>名　稱</th>
                <td>
                    <asp:TextBox ID="act_name_txt" runat="server" Text='<%# BindItem.Act_name %>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>信　箱</th>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# BindItem.Act_mail %>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th><span class="alert">*</span>身　分</th>
                <td style="padding: 5px 9px">
                    <uc1:UCAccountRoleManager runat="server" ID="ucAccountRoleManager" />
                </td>
            </tr>
            <tr>
                <th>密　碼</th>
                <td>
                    <asp:TextBox ID="act_pwd_txt" runat="server" Text='<%# BindItem.Act_pwd %>' TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>密碼確認</th>
                <td>
                    <asp:TextBox ID="act_pwd_confirm_txt" runat="server" Text='' TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th></th>
                <td style="text-align: center;padding-right: 140px;">
                    <asp:Button ID="update_btn" runat="server" CssClass="btn btn-default" Text="儲存" CommandName="Update" UseSubmitBehavior="false" />
                    &nbsp;<asp:Button ID="cancel_btn" runat="server" CssClass="btn btn-default" Text="取消" OnClick="cancel_btn_Click" UseSubmitBehavior="false" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
