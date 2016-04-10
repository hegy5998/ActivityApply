<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="activity.aspx.cs" Inherits="Register.activity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sideCon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainCon" runat="server">
    <section id="main-content">
    <section class="wrapper site-min-height">
        <div class="row mt">
			<! -- 1st ROW OF PANELS -->
            <div class="row">

            </div>

			<div class="row mt">
				<!-- TWITTER PANEL -->
				<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 desc">
					<div class="project-wrapper">
		                <div class="project">
		                    <div class="photo-wrapper">
		                        <div class="photo">
<%--		                            <a class="fancybox" href="../assets/img/fcu.jpg"><img class="img-responsive" src="../assets/img/fcu.jpg" alt=""></a>--%>
                                    <a data-toggle="modal" href="#myModal"><img class="img-responsive" src="/assets/img/fcu.jpg" alt=""></a>
		                        </div>

		                        <div class="overlay"></div>
		                    </div>
		                </div>
		            </div>

                    <div class="row"></div>

                    <div class="showback">
                        <h3>短網址</h3>
                        <h5>http://goo.com</h5>
                    </div>

                    <div class="photo">
                        <a class="thumbnail"><img class="imp-responsive" src="../assets/img/qrcodetest.png" alt="" /></a>
                    </div>

                    <div class="row"></div>
				</div><!-- /col-md-4 -->

				<div class="col-lg-9 col-md-9 col-sm-9 col-xs-12 dexc">
                    <div class="showback">
                        <h4>主辦單位</h4>
                        <h5>逢甲大學資訊處</h5>
                        <hr />

                        <h4>連絡人</h4>
                        <h5>黃翔</h5>
                        <h5>0425222222</h5>
                        <hr />

                        <h4>活動簡介</h4>
                        <h5>我是活動簡介</h5>
                        <hr />

                        <h4>開放對象</h4>
                        <h5>我是開放對象</h5>
                        <hr />

                        <h4>附加檔案</h4>
                        <h5>我是附加檔案</h5>
                        <hr />

                        <h4>相關連結</h4>
                        <h5>我是相關連結</h5>
                        <hr />
                    </div>

					<!-- WHITE PANEL - TOP USER -->
					<div class="showback">
						<h3>逢甲大學活動報名系統－台北場</h3>
                        <h5>活動地點：逢甲大學 台北校區</h5>
                        <h5>活動日期：2016-03-16 09:00</h5>
                        <h5>報名日期：2016-03-01 09:00 ~ 2016-03-15 11:00</h5>
                        <h5>剩餘/限制人數：10/20人</h5>

                        <a href="../Sign_Up.aspx" class="btn btn-theme btn-lg" role="button">我要報名</a>
					</div>

                    <div class="showback">
						<h3>逢甲大學活動報名系統－台中場</h3>
                        <h5>活動地點：逢甲大學 台中校區</h5>
                        <h5>活動日期：2016-03-16 09:00</h5>
                        <h5>報名日期：2016-03-01 09:00 ~ 2016-03-15 11:00</h5>
                        <h5>剩餘/限制人數：10/20人</h5>

                        <a href="../Sign_Up.aspx" class="btn btn-theme btn-lg" role="button">我要報名</a>
					</div>

                    <div class="showback">
						<h3>逢甲大學活動報名系統－高雄場</h3>
                        <h5>活動地點：逢甲大學 高雄校區</h5>
                        <h5>活動日期：2016-03-16 09:00</h5>
                        <h5>報名日期：2016-03-01 09:00 ~ 2016-03-15 11:00</h5>
                        <h5>剩餘/限制人數：10/20人</h5>

                        <a href="../Sign_Up.aspx" class="btn btn-theme btn-lg" role="button">我要報名</a>
					</div>
				</div><!-- /col-md-4 -->
			</div><! --/END 1ST ROW OF PANELS -->
        </div>
	</section><! --/wrapper -->
</section><!-- /MAIN CONTENT -->
<!--main content end-->

<!-- js placed at the end of the document so the pages load faster -->
<%--<script src="assets/js/fancybox/jquery.fancybox.js"></script> --%>   
<%--<script src="assets/js/bootstrap.min.js"></script>
<script class="include" type="text/javascript" src="assets/js/jquery.dcjqaccordion.2.7.js"></script>
<script src="assets/js/jquery.scrollTo.min.js"></script>
<script src="assets/js/jquery.nicescroll.js" type="text/javascript"></script>--%>


<!-- Modal -->
<div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade text-center">
    <div class="modal-dialog" style="display: inline-block; width: auto;">
        <div class="modal-content">
            <div class="modal-body">
                <img class="img-responsive" src="/assets/img/fcu.jpg" alt="">
            </div>
        </div>    
    </div>
</div>

    <!--script for this page-->
<script src="../assets/js/sparkline-chart.js"></script>    
    
<script>
    //custom select box

    $(function () {
        $('select.styled').customSelect();
    });

    //script for this page
    $(function () {
        //    fancybox
        jQuery(".fancybox").fancybox();
    });

    //custom select box

    $(function () {
        $("select.styled").customSelect();
    });
</script>
<!-- modal -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>

