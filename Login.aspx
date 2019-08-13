<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" EnableEventValidation="true"  %>

<!DOCTYPE html >

<html class="h-100" lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
     <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width,initial-scale=1"/>
  <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <!-- Favicon icon -->
    <link rel="icon" type="image/png" sizes="16x16" href="../assets/images/favicon.html"/>
   
    <link href="css/style.css" rel="stylesheet"/>

    <style>
.zoom {
 
 
  transition: transform .2s; /* Animation */

  margin: 0 auto;
}

.zoom:hover {
  transform: scale(1.1); /* (150% zoom - Note: if the zoom is too large, it will go outside of the viewport) */

}
.zoom2 {
 /*border: 1px solid #ddd;*/
  border-radius: 4px;
 

  transition: transform .2s;

  margin: 0 auto;
  z-index:999999;

}

.zoom2:hover {
  -ms-transform: scale(1.1); /* IE 9 */
  -webkit-transform: scale(1.1); /* Safari 3-8 */
  transform: scale(1.1); 
 
    box-shadow: 0 0 2px 1px black;
}

.responsive {
  width: 100%;
  height: auto;
}
</style>
 <script>

     function ErrorShow() {

         swal("Oops!", "Incorrect Email or password!", "error");

     }
 </script>  
    <style>
        body {
        background-image: url("/images/Diagrammatic.png");
            background-color: #cccccc;
        }
.card {
-webkit-box-shadow: 0px 9px 300px -1px rgba(0,0,0,0.75);
-moz-box-shadow: 0px 9px 300px -1px rgba(0,0,0,0.75);
box-shadow: 0px 9px 300px -1px rgba(0,0,0,0.75);
}
       
</style>
</head>
<body class="h-100">
    
    
    <!--*******************
        Preloader start
    ********************-->
    <div id="preloader">
        <div class="loader">
            <svg class="circular" viewBox="25 25 50 50">
                <circle class="path" cx="50" cy="50" r="20" fill="none" stroke-width="3" stroke-miterlimit="10" />
            </svg>
        </div>
    </div>
    <!--*******************
        Preloader end
    ********************-->

    



    <div class="login-form-bg h-100" >
        <div class="container h-100">
            <div class="row justify-content-center h-100">
                <div class="col-xl-6">
                    <div class="form-input-content">
                        <div class="card login-form mb-0">
                            <div class="card-body pt-5">
                                <a class="text-center" href="#"><a class="text-center" href="../login.aspx" >
                                    <img src="assets/524013_127821830712159_1966805957_n.png" class="responsive" height="100" width="450" /> </a></a>
        
                                <form class="mt-5 mb-5" runat="server">
                                     <div class="form-group">
                                      <asp:Label ID="lblerror" Visible="false" ForeColor="Red" runat="server"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <input id="txtemail" runat="server" type="email" class="zoom2 form-control" placeholder="Email"/>
                                    </div>
                                    <div class="form-group">
                                        <input id="txtpassword" runat="server" type="password" class="zoom2 form-control" placeholder="Password"/>
                                    </div>
                                    <asp:Button runat="server" ID="btnLogin_1" OnClick="Login_Click" CssClass="zoom2 btn btn-lg btn-flat btn-outline-info  w-100" Text="Sign In" />
                                   <%-- <button id="btnLogin" onclick="Login_click" onclientclick="Get();" runat="server" class="btn login-form__btn submit w-100">Sign In</button>--%>
                                </form>
                                <p class="mt-5 login-form__footer">Dont have account? <b><a href="SignUp" class="text-primary">Sign Up</a></b> now</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    

       

    <!--**********************************
        Scripts
    ***********************************-->
    <script src="plugins/common/common.min.js"></script>
    <script src="js/custom.min.js"></script>
    <script src="js/settings.js"></script>
    <script src="js/gleek.js"></script>
    <script src="js/styleSwitcher.js"></script>

    
  

</body>
</html>
