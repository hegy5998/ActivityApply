<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Web.S02.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link href="<%=ResolveUrl("~/Css/Lib/bootstrap.css") %>" rel="stylesheet" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Lib/assets/js/bootstrap.min.js") %>"></script>
        <%-- jquery_validation --%>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Lib/jquery-1.9.1.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Lib/jquery-validation/dist/jquery.validate.js") %>"></script>
    <script src="../Scripts/Lib/jquery-validation/dist/jquery.validate.js"></script>
</head>
<body>

    <form class="row" id="add_Session_div" action="">
                    <h3><i class="fa fa-angle-right"></i>活動場次</h3>
                    <div class="showback">
                        <div class="form-horizontal style-form">
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">活動名稱</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control" id="activity_Name_txt_1">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">活動開始日期</label>
                                <div class="col-sm-4">
                                    <input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_Start_1" />
                                </div>
                                <label class="col-sm-2 col-sm-2 control-label">活動結束日期</label>
                                <div class="col-sm-4">
                                    <input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_End_1" onclick="check_date"/>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">報名開始日期</label>
                                <div class="col-sm-4">
                                    <input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_Sign_Start_1" />
                                </div>
                                <label class="col-sm-2 col-sm-2 control-label">報名結束日期</label>
                                <div class="col-sm-4">
                                    <input class="form-control datetimepicker" type="text" id="datetimepicker_Activity_Sign_End_1" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">活動地點</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control" id="activity_Location_1">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">人數限制</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control" id="email" name="email" id="activity_Limit_Num_1">
                                </div>
                            </div>
                            <div class="form-group" style="padding-right: 10px;">
                                <a style="float: right;" class="btn btn-theme" onclick="add_Session_click()">增加場次</a>
                            </div>
                        </div>
                    </div>
                </form>

    <div class="panel panel-default">
					<div class="panel-heading">
						<h3 class="panel-title">Simple Form</h3>
					</div>
					<div class="panel-body">
						<form id="signupForm" method="post" class="form-horizontal" action="" novalidate="novalidate">
							<div class="form-group">
								<label class="col-sm-4 control-label" for="firstname">First name</label>
								<div class="col-sm-5">
									<input type="text" class="form-control" id="firstname" name="firstname" placeholder="First name">
								</div>
							</div>

							<div class="form-group">
								<label class="col-sm-4 control-label" for="lastname">Last name</label>
								<div class="col-sm-5">
									<input type="text" class="form-control" id="lastname" name="lastname" placeholder="Last name">
								</div>
							</div>

							<div class="form-group">
								<label class="col-sm-4 control-label" for="username">Username</label>
								<div class="col-sm-5">
									<input type="text" class="form-control" id="username" name="username" placeholder="Username">
								</div>
							</div>

							<div class="form-group">
								<label class="col-sm-4 control-label" for="email">Email</label>
								<div class="col-sm-5">
									<input type="text" class="form-control"  name="email" placeholder="Email">
								</div>
							</div>

							<div class="form-group">
								<label class="col-sm-4 control-label" for="password">Password</label>
								<div class="col-sm-5">
									<input type="password" class="form-control" id="password" name="password" placeholder="Password">
								</div>
							</div>

							<div class="form-group">
								<label class="col-sm-4 control-label" for="confirm_password">Confirm password</label>
								<div class="col-sm-5">
									<input type="password" class="form-control" id="confirm_password" name="confirm_password" placeholder="Confirm password">
								</div>
							</div>

							<div class="form-group">
								<div class="col-sm-5 col-sm-offset-4">
									<div class="checkbox">
										<label>
											<input type="checkbox" id="agree" name="agree" value="agree">Please agree to our policy
										</label>
									</div>
								</div>
							</div>

							<div class="form-group">
								<div class="col-sm-9 col-sm-offset-4">
									<button type="submit" class="btn btn-primary" name="signup" value="Sign up">Sign up</button>
								</div>
							</div>
						</form>
					</div>
				</div>

    <script type="text/javascript">

        $.validator.setDefaults({
            submitHandler: function () {
                alert("submitted!");
            }
        });

        $(document).ready(function () {
            $("#add_Session_div").validate({
                rules: {
                    firstname: "required",
                    lastname: "required",
                    username: {
                        required: true,
                        minlength: 2
                    },
                    password: {
                        required: true,
                        minlength: 5
                    },
                    confirm_password: {
                        required: true,
                        minlength: 5,
                        equalTo: "#password"
                    },
                    email: {
                        required: true,
                        email: true
                    },
                    agree: "required"
                },
                messages: {
                    firstname: "Please enter your firstname",
                    lastname: "Please enter your lastname",
                    username: {
                        required: "Please enter a username",
                        minlength: "Your username must consist of at least 2 characters"
                    },
                    password: {
                        required: "Please provide a password",
                        minlength: "Your password must be at least 5 characters long"
                    },
                    confirm_password: {
                        required: "Please provide a password",
                        minlength: "Your password must be at least 5 characters long",
                        equalTo: "Please enter the same password as above"
                    },
                    email: "Please enter a valid email address",
                    agree: "Please accept our policy"
                },
                errorElement: "em",
                errorPlacement: function (error, element) {
                    // Add the `help-block` class to the error element
                    error.addClass("help-block");

                    if (element.prop("type") === "checkbox") {
                        error.insertAfter(element.parent("label"));
                    } else {
                        error.insertAfter(element);
                    }
                },
                highlight: function (element, errorClass, validClass) {
                    $(element).parents(".col-sm-5").addClass("has-error").removeClass("has-success");
                },
                unhighlight: function (element, errorClass, validClass) {
                    $(element).parents(".col-sm-5").addClass("has-success").removeClass("has-error");
                }
            });

            $("#signupForm1").validate({
                rules: {
                    firstname1: "required",
                    lastname1: "required",
                    username1: {
                        required: true,
                        minlength: 2
                    },
                    password1: {
                        required: true,
                        minlength: 5
                    },
                    confirm_password1: {
                        required: true,
                        minlength: 5,
                        equalTo: "#password1"
                    },
                    email1: {
                        required: true,
                        email: true
                    },
                    agree1: "required"
                },
                messages: {
                    firstname1: "Please enter your firstname",
                    lastname1: "Please enter your lastname",
                    username1: {
                        required: "Please enter a username",
                        minlength: "Your username must consist of at least 2 characters"
                    },
                    password1: {
                        required: "Please provide a password",
                        minlength: "Your password must be at least 5 characters long"
                    },
                    confirm_password1: {
                        required: "Please provide a password",
                        minlength: "Your password must be at least 5 characters long",
                        equalTo: "Please enter the same password as above"
                    },
                    email1: "Please enter a valid email address",
                    agree1: "Please accept our policy"
                },
                errorElement: "em",
                errorPlacement: function (error, element) {
                    // Add the `help-block` class to the error element
                    error.addClass("help-block");

                    // Add `has-feedback` class to the parent div.form-group
                    // in order to add icons to inputs
                    element.parents(".col-sm-5").addClass("has-feedback");

                    if (element.prop("type") === "checkbox") {
                        error.insertAfter(element.parent("label"));
                    } else {
                        error.insertAfter(element);
                    }

                    // Add the span element, if doesn't exists, and apply the icon classes to it.
                    if (!element.next("span")[0]) {
                        $("<span class='glyphicon glyphicon-remove form-control-feedback'></span>").insertAfter(element);
                    }
                },
                success: function (label, element) {
                    // Add the span element, if doesn't exists, and apply the icon classes to it.
                    if (!$(element).next("span")[0]) {
                        $("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(element));
                    }
                },
                highlight: function (element, errorClass, validClass) {
                    $(element).parents(".col-sm-5").addClass("has-error").removeClass("has-success");
                    $(element).next("span").addClass("glyphicon-remove").removeClass("glyphicon-ok");
                },
                unhighlight: function (element, errorClass, validClass) {
                    $(element).parents(".col-sm-5").addClass("has-success").removeClass("has-error");
                    $(element).next("span").addClass("glyphicon-ok").removeClass("glyphicon-remove");
                }
            });
        });

    </script>
</body>
</html>
