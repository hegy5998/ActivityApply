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

            
		        <h2 class="form-login-heading" style="margin-left: 44%;">報名查詢</h2>
		        <div class="login-wrap">
		            <input type="text" class="form-control" placeholder="信箱Email" autofocus/>
		            <br/>
		            <input type="password" class="form-control" placeholder="密碼"/>
		            <label class="checkbox">
		                <span class="pull-right">
		                    <a data-toggle="modal" href="login.html#myModal">忘記密碼?</a>
		                </span>
		            </label>
		            <a class="btn btn-theme btn-block" href="SignSearchContext.aspx" type="submit">查詢</a>
		        </div>
		
		          <!-- Modal -->
		          <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade">
		              <div class="modal-dialog">
		                  <div class="modal-content">
		                      <div class="modal-header">
		                          <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
		                          <h4 class="modal-title">忘記密碼?</h4>
		                      </div>
		                      <div class="modal-body">
		                          <p>輸入信箱Email</p>
		                          <input type="text" name="email" placeholder="ex: user@mail.com.tw" autocomplete="off" class="form-control placeholder-no-fix"/>
		
		                      </div>
		                      <div class="modal-footer">
		                          <button data-dismiss="modal" class="btn btn-default" type="button">取消</button>
		                          <button class="btn btn-theme" type="button">發送Email</button>
		                      </div>
		                  </div>
		              </div>
		          </div>
		          <!-- modal -->
		
        </section>
    </section>
    <!--main content end-->
</section>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>
