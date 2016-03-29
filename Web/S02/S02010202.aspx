<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S02010202.aspx.cs" Inherits="Web.S02.S02010202" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentSubFunction_cph" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent_cph" runat="server">
    <div class="row" id="add_Session_div">

    </div>

    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <input type="text" id="txt1"/>

    <script type="text/javascript">
        var a = parent.document.getElementById("qus_txt_1").value;
        $("#Label1").val(a);
    </script>
</asp:Content>
