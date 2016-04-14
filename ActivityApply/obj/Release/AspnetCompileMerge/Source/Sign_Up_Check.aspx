<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="Sign_Up_Check.aspx.cs" Inherits="Register.Sign_Up_Check" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sideCon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainCon" runat="server">
    <section id="main-content">
    <section class="wrapper">
        <div class="row"></div>
        <h3><i class="fa fa-angle-right"></i>報名資料確認</h3>

        <!-- BASIC FORM ELELEMNTS -->
        <div class="row mt">
            <div class="col-lg-12">
                <div class="form-panel">
                    <h4 class="mb"><i class="fa fa-angle-right"></i>基本資料</h4>
                    <div class="form-horizontal style-form" method="get">
                        <div class="form-group">
                            <label class="col-sm-2 col-sm-2 control-label">姓名</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" placeholder="黃翔" disabled="disabled">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 col-sm-2 control-label">電話</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" placeholder="09XX-XXX-XXX" disabled="disabled">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 col-sm-2 control-label">身份證字號</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control round-form" placeholder="FXXXXXXXXX" disabled="disabled">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 col-sm-2 control-label">地址</label>
                            <div class="col-sm-10">
                                <input class="form-control"  type="text" placeholder="台中市西屯區福星路" disabled="disabled">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 col-sm-2 control-label">性別</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" placeholder="男" disabled="disabled">
                            </div>
                        </div>
                    </div>
                </div>
            </div><!-- col-lg-12-->
        </div><!-- /row -->
        <div class="row mt">
            <div class="col-lg-6">
                <%--<a href="" role"botton" type="submit" class="btn btn-theme btn-lg btn-block">確認</a>--%>
                <a data-toggle="modal" data-backdrop="static" href="#myModal" type="submit" class="btn btn-theme btn-lg btn-block">確認</a>
            </div>
            <div class="col-lg-6">
                <a href="Sign_Up.aspx" type="submit" class="btn btn-theme btn-lg btn-block">修改</a>
            </div>
        </div>

        <!-- Modal -->
        <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade">
            <div class="modal-dialog" >
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                        <h4 class="modal-title">報名成功</h4>
                    </div>

                    <div class="modal-body">
                        <p><h4>感謝您報名此活動，我們將會寄活動相關資訊到您的信箱，如需修改或刪除報名資料也請查閱信箱內容</h4></p>
                        <br /><br /><hr />
                        <p><h8>2016  逢甲大學資訊處 - 資訊技術服務中心</h8></p>
                    </div>

                    <div class="modal-footer">
                        <%--<button class="btn btn-theme" type="button">確認</button>--%>
                        <a href="index.aspx" role="button" type="submit" class="btn btn-theme btn-lg">確認</a>
                    </div>
                </div>
            </div>
        </div>
        <!-- modal -->
    </section>
</section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>
