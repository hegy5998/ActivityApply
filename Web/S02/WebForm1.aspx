<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Web.S02.WebForm1" %>

<!DOCTYPE HTML>
<html>
<head>
  <meta charset="utf-8">
<title></title>
</head>

<body>
    <form runat="server">
        <%--<asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ChildrenAsTriggers="false" runat="server" UpdateMode="Conditional" ViewStateMode="Enabled">
            <ContentTemplate>--%>
                <asp:FileUpload ID="FileUpload1" runat="server"  />
                <%--<asp:Button ID="Button1" runat="server" Text="上傳檔案" OnClick="Button1_Click" />--%>
               <%-- <asp:LinkButton ID="Button1" runat="server">上傳檔案</asp:LinkButton>--%>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
           <%-- </ContentTemplate>
            <Triggers>
                <asp:postbacktrigger controlid="Button1"></asp:postbacktrigger>
            </Triggers>
        </asp:UpdatePanel>--%>

        <asp:ImageButton ID="Button1" runat="server" OnClick="Button1_Click" Style="display: none"></asp:ImageButton>


    </form>

    <input type="submit" onclick="ClickAll()" value="檢視報名表"/>
    <script>
        function ClickAll() {
            //綜合
            document.getElementById("Button1").click();
        }
        function view_Activity() {
            __doPostBack('Button1', '');
            //var json = JSON.stringify(save_Activity_Column());
            //$("#save_Json_Data").val(json);
            //window.open("S02010202.aspx?sys_id=S01&sys_pid=S02010202");
        }
    </script>
</body> 
</html>