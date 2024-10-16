<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CRMSystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
	<meta name="keywords" content=""/>
	<meta name="author" content=""/>
	<meta name="robots" content=""/>
	<meta name="viewport" content="width=device-width, initial-scale=1"/>
	<meta name="description" content="Yeshadmin:Customer Relationship Management Admin Bootstrap 5 Template"/>
	<meta property="og:title" content="Yeshadmin:Customer Relationship Management Admin Bootstrap 5 Template"/>
	<meta property="og:description" content="Yeshadmin:Customer Relationship Management Admin Bootstrap 5 Template"/>
	<meta property="og:image" content="https://yeshadmin.dexignzone.com/"/>
	<meta name="format-detection" content="telephone=no"/>
	
	<!-- PAGE TITLE HERE -->
	 
	<!-- FAVICONS ICON -->

	
	<link href="vendor/bootstrap-select/dist/css/bootstrap-select.min.css" rel="stylesheet"/>
	<link href="vendor/swiper/css/swiper-bundle.min.css" rel="stylesheet"/>
	<link href="vendor/swiper/css/swiper-bundle.min.css" rel="stylesheet"/>
	<link rel="stylesheet" href="../../cdnjs.cloudflare.com/ajax/libs/noUiSlider/14.6.4/nouislider.min.css"/>
	<link href="vendor/datatables/css/jquery.dataTables.min.css" rel="stylesheet"/>
	<link href="../../cdn.datatables.net/buttons/1.6.4/css/buttons.dataTables.min.css" rel="stylesheet"/>
	<link href="vendor/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet"/>
	
	<!-- tagify-css -->
	<link href="vendor/tagify/dist/tagify.css" rel="stylesheet"/>
	
	<!-- Style css -->
    <link href="css/style.css" rel="stylesheet"/>
</head>
<body class="vh-100">

    <form runat="server">


    
	<div class="page-wraper">

		<!-- Content -->
		<div class="browse-job login-style3">
			<!-- Coming Soon -->
			<div class="bg-img-fix overflow-hidden" style="background:#fff url(images/background/bg6.jpg); height: 100vh;">
				<div class="row gx-0">
					<div class="col-xl-4 col-lg-5 col-md-6 col-sm-12 vh-100 bg-white ">
						<div id="mCSB_1" class="mCustomScrollBox mCS-light mCSB_vertical mCSB_inside" style="max-height: 653px;" tabindex="0">
							<div id="mCSB_1_container" class="mCSB_container" style="position:relative; top:41px; left:0;" dir="ltr">
								<div class="login-form style-2">
									
									
									<div class="card-body">
										
									
										<nav>
											<div class="nav nav-tabs border-bottom-0" id="nav-tab" role="tablist">
												
										<div class="tab-content w-100" id="nav-tabContent">
										  <div class="tab-pane fade show active loginform" id="nav-personal" role="tabpanel" aria-labelledby="nav-personal-tab">
											 
													<h3 class="form-title m-t0">Login</h3>
													<div class="dz-separator-outer m-b5">
														<div class="dz-separator bg-primary style-liner"></div>
													</div>
													<p>Enter your User Name and your password.</p>
													<div class="form-group mb-3">
											<label class="text-label form-label" for="validationCustomUsername">Username</label>
                                            <div class="input-group">
												<span class="input-group-text"> <i class="fa fa-user"></i> </span>
                                                <input type="text" class="form-control validated"   id="txtUser" placeholder="Enter a username.."  data-label="UserName"/>
                                            </div>
												 <label class="UserName-error" style="color: red; margin-top: 0.2rem; display: none"></label>



													</div>
													<div class="form-group mb-3">

													    <label class="text-label form-label" for="dz-password">Password *</label>
                                            <div class="input-group transparent-append">
												<span class="input-group-text"> <i class="fa fa-lock"></i> </span>
                                                <input type="password" class="form-control validated" id="txtPass" placeholder="Enter a Password.." data-label="password"/>
												<span class="input-group-text show-pass"> 
													<i class="fa fa-eye-slash"></i>
													<i class="fa fa-eye"></i>
												</span>
                                              
                                            </div>
                                                                        <label class="password-error" style="color: red; margin-top: 0.2rem; display: none"></label>

													</div>


													<div class="form-group text-left forget-main loginbttn">
                                                   <%--   <a href="Dashboard.aspx" class="btn btn-primary loginbttnwid">Submit</a>--%>
                                                        <button id="loginbtn" class="btn btn-primary loginbttnwid" onclick="return validLoginCheck(event);">Submit</button>
													</div>
													
												 
												
										  </div>
										  
										</div>
										
										</div>
									</nav>
									</div>
										<div class="card-footer">
											<div class=" bottom-footer clearfix m-t10 m-b20 row text-center">
											<div class="col-lg-12 text-center">
												<span> © Copyright by <span class="heart"></span>
												<a href="https://www.4techbugs.com/"> 4techbugs  </a> All rights reserved.</span> 
											</div>
										</div>
									</div>	
											
								</div>
							</div>
							<div id="mCSB_1_scrollbar_vertical" class="mCSB_scrollTools mCSB_1_scrollbar mCS-light mCSB_scrollTools_vertical" style="display: block;">
								<div class="mCSB_draggerContainer">
								<div id="mCSB_1_dragger_vertical" class="mCSB_dragger" style="position: absolute; min-height: 0px; display: block; height: 652px; max-height: 643px; top: 0px;">
								<div class="mCSB_dragger_bar" style="line-height: 0px;"></div><div class="mCSB_draggerRail"></div></div></div>
							</div>
						</div>
					</div>
				</div>
				</div>
			</div>
			<!-- Full Blog Page Contant -->
		</div>
		<!-- Content END-->
	 
        </form>
<!--**********************************
	Scripts
***********************************-->
<!-- Required vendors -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js" integrity="sha512-v2CJ7UaYy4JwqLDIrZUI/4hqeoQieOmAZNXBeQyjo21dadnwR+8ZaIJVT8EE2iyI61OV8e6M8PP2/4hpQINQ/g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
<script src="vendor/global/global.min.js"></script>
<script src="vendor/bootstrap-select/dist/js/bootstrap-select.min.js"></script>
<script src="js/deznav-init.js"></script>
 <script src="js/custom.js"></script>
<script src="js/demo.js"></script>
<script src="js/styleSwitcher.js"></script>
    <script src="js/Ajax/Login.js"></script>
  
</body>
</html>
