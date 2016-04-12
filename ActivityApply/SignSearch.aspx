<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="SignSearch.aspx.cs" Inherits="Register.SignSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sideCon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainCon" runat="server">
    <section id="container" >
    <!-- **********************************************************************************************************************************************************
    MAIN CONTENT
    *********************************************************************************************************************************************************** -->
    <!--main content start-->
    <section id="main-content">
        <section class="wrapper">
            <div class="row"></div>
            <h3><i class="fa fa-angle-right"></i>報名查詢</h3>

            <div class="row">
                <div class="col-xs-12">
                    <div class="push-down-30">
                        <div class="banners-big">
                            <!--活動查詢：活動名稱包含 <strong></strong> 。-->
                            <form method="get">
                                <div class="input-group">
                                    <span class="input-group-btn">
                                        <button class="btn btn-primary"><span class="glyphicon glyphicon-search" aria-hidden="true"></span></button>
                                    </span>

                                    <input type="text" name="search" class="form-control" placeholder="請輸入信箱">
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </section>
    <!--main content end-->
</section>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>
