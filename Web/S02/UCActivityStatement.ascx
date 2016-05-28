<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCActivityStatement.ascx.cs" Inherits="Web.S02.UCActivityStatement" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<style>
    #activityStatement tr.fvRow > th {
        width: 100px;
    }
    .xdsoft_datetimepicker {
        z-index: 200000;
    }
</style>
<asp:FormView ID="main_fv" runat="server" CssClass="fv"
    ItemType="Model.Activity_statementInfo"
    DataKeyNames="ast_id"
    OnItemInserting="main_fv_ItemInserting"
    OnItemUpdating="main_fv_ItemUpdating">
    <InsertItemTemplate>
        <table class="table" id="activityStatement">
            <tr>
                <th><span class="alert">*</span>標  題</th>
                <td>
                    <asp:TextBox ID="ast_title_txt" runat="server" Text='<%# BindItem.Ast_title %>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>敘  述</th>
                <td>
                    <asp:TextBox ID="ast_desc_txt" runat="server" Text='<%# BindItem.Ast_desc %>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>內  文</th>
                <td>
                    <asp:TextBox ID="ast_content" runat="server" Text='<%# BindItem.Ast_content %>' TextMode="MultiLine" Width="100%" Height="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>有效期限</th>
                <td>
                    <asp:TextBox ID="ast_term_txt" runat="server" Text='<%# BindItem.Ast_term %>' CssClass="datetimepicker"></asp:TextBox>                
                </td>
            </tr>
            <tr>
                <th>共  用</th>
                <td>
                    <asp:RadioButtonList ID="ast_public_rbl" runat="server" RepeatDirection="Horizontal" CssClass="fv" SelectedValue='<%# BindItem.Ast_public %>'>
                        <asp:ListItem Value="0" Text="關閉" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="開啟"></asp:ListItem>
                    </asp:RadioButtonList>
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
        <table class="table" id="activityStatement">
            <tr>
                <th><span class="alert">*</span>標  題</th>
                <td>
                    <asp:TextBox ID="ast_title_txt" runat="server" Text='<%# BindItem.Ast_title %>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>敘  述</th>
                <td>
                    <asp:TextBox ID="ast_desc_txt" runat="server" Text='<%# BindItem.Ast_desc %>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>內  文</th>
                <td>
                    <asp:TextBox ID="ast_content" runat="server" Text='<%# BindItem.Ast_content %>' TextMode="MultiLine" Width="100%" Height="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>有效期限</th>
                <td>
                    <asp:TextBox ID="ast_term_txt" runat="server" Text='<%# BindItem.Ast_term %>' CssClass="datetimepicker"></asp:TextBox>           
                </td>
            </tr>
            <tr>
                <th>共  用</th>
                <td>
                    <asp:RadioButtonList ID="ast_public_rbl" runat="server" RepeatDirection="Horizontal" CssClass="fv" SelectedValue='<%# BindItem.Ast_public %>'>
                        <asp:ListItem Value="0" Text="關閉"></asp:ListItem>
                        <asp:ListItem Value="1" Text="開啟"></asp:ListItem>
                    </asp:RadioButtonList>
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
<script type="text/javascript">
    function init() {
        $.datetimepicker.setLocale('zh-TW');
        $('.datetimepicker').datetimepicker({
            timepicker: false,
            format:'Y/m/d'
        });
    }
</script>
