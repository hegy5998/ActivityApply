<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Base.Master" AutoEventWireup="true" CodeBehind="S02010102.aspx.cs" Inherits="Web.S02.S02010102" %>

<asp:Content ID="Content1" ContentPlaceHolderID="baseHead_cph" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="baseContent_cph" runat="server">
    <h2>逢甲大學活動報名系統</h2>
    <a href="#">刪除全部</a> |
    <a href="#">匯出</a> |
    <a href="#">儲存資料</a>
    <br /><br />

    <table class="table table-striped table-bordered">
        <thead>
            <tr>
                <th>流水號</th>
                <th>姓名</th>
                <th>電子信箱</th>
                <th>服務單位</th>
                <th>刪除</th>
            </tr>
        </thead>
                        
        <tbody>
            <tr>
                <td>1</td>
                <td>逢甲大學活動管理系統</td>
                <td>123@aaa</td>
                <td>逢甲大學資訊處</td>
                <td><a href="#">刪除</a></td>
            </tr>

            <tr>
                <td>2</td>
                <td>逢甲大學活動管理系統</td>
                <td>123@aaa</td>
                <td>逢甲大學資訊處</td>
                <td><a href="#">刪除</a></td>
            </tr>

            <tr>
                <td>3</td>
                <td>逢甲大學活動管理系統</td>
                <td>123@aaa</td>
                <td>逢甲大學資訊處</td>
                <td><a href="#">刪除</a></td>
            </tr>

            <tr>
                <td>4</td>
                <td>逢甲大學活動管理系統</td>
                <td>123@aaa</td>
                <td>逢甲大學資訊處</td>
                <td><a href="#">刪除</a></td>
            </tr>

            <tr>
                <td>5</td>
                <td>逢甲大學活動管理系統</td>
                <td>123@aaa</td>
                <td>逢甲大學資訊處</td>
                <td><a href="#">刪除</a></td>
            </tr>
        </tbody>
    </table>

    <script>
        window.onbeforeunload = function () {
            window.event.returnValue = "尚未儲存資料";
            if (window.event.reason == false) {
                window.event.cancelBubble = true;
            }
        }
    </script>
</asp:Content>
