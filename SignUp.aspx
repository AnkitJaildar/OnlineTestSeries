<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SignUp" %>

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
            .zoom2 {
 /*border: 1px solid #ddd;*/
  border-radius: 4px;
 

  transition: transform .2s;

  margin: 0 auto;
  z-index:999999;

}

.zoom2:hover {
  -ms-transform: scale(1.2); /* IE 9 */
  -webkit-transform: scale(1.2); /* Safari 3-8 */
  transform: scale(1.2); 
 
    box-shadow: 0 0 2px 1px black;
}
.responsive {
  width: 100%;
  height: auto;
}
</style>
 <script>
    
     function SuccessMsg(txt) {

         swal("Successfully Signed Up, You can now login", txt, "success");

     }

     function OTP(txt) {

         swal("OTP is sent to your mobile no : ", txt, "success");

     }
     function ErrorShow(txt) {

         swal("Oops!", txt, "error");

     }

     
        function Popup() {
            $('#myModal').modal('show');
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
<body class="h-100" >
    
    
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

    



    <div class="login-form-bg h-100" style="margin:20px">
        <div class="container h-100">
            <div class="row justify-content-center h-100">
                <div class="col-xl-6">
                    <div class="form-input-content">
                        <div class="card login-form mb-0">
                            <div class="card-body pt-5">
                                <a class="text-center" href="../login.aspx" >
                                    <img src="assets/524013_127821830712159_1966805957_n.png" class="responsive" height="100" width="450" /> </a>
        
                                <form class="mt-5 mb-5" runat="server" id="signupform">
                                     <div class="form-group">
                                      <asp:Label ID="lblerror" Visible="false" ForeColor="Red" runat="server"></asp:Label>
                                          
                                    </div>
                                    <div class="form-group" runat="server" id="emial">
                                        <input id="txtemail" runat="server" autocomplete="off" enableviewstate="false" required type="email" class="form-control" placeholder="Email"/>
                                        <asp:HiddenField runat="server" ID="hemail" />
                                    </div>
                                     <div class="form-group" runat="server" id="Uname">
                                        <input id="txtname" runat="server"  class="form-control" placeholder="Name" required/>
                                            <asp:HiddenField runat="server" ID="Hname" />
                                    </div>
                                    <div class="form-group" runat="server" id="phoneno">
                                        <input id="txtmobileno" runat="server" maxlength="10" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"  class="form-control" placeholder="MobileNo." required/>
                                           <asp:HiddenField runat="server" ID="Hmobileno" />
                                    </div>
                                    <div class="form-group" runat="server" id="divotp" visible="false">
                                        <input id="txtotp" runat="server" maxlength="4" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"  class="form-control" placeholder="ENTER OTP." required/>
                                    </div>
                                     <div class="form-group" runat="server" id="country">
                                         <asp:DropDownList ID="DDLCOUNTRY" runat="server"  OnSelectedIndexChanged="DDLCOUNTRY_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                         <asp:HiddenField runat="server" ID="Hcountry" />
                                    </div>
                                     <div class="form-group" runat="server" id="divAutority">
                                    <asp:TextBox runat="server" ID="txtauthority" placeholder="Authority" CssClass="form-control"></asp:TextBox>
                                           <asp:HiddenField runat="server" ID="HAuthority" />
                                    </div>
                                    <div class="form-group" runat="server" id="pswrd">
                                        <input id="txtpassword" runat="server" type="password" class="form-control" placeholder="Password" required/>
                                         <asp:HiddenField runat="server" ID="Hpswrd" />
                                    </div>
                                     <div class="form-group" runat="server" id="cpswrd">
                                        <input id="txtcnfrmpassword" runat="server"  type="password" class="form-control" placeholder="Confirm password" required/>
                                    </div>
                                    <asp:Button runat="server" ID="btnsignup" OnClick="btnsignup_Click"  CssClass="btn btn-lg btn-flat btn-outline-info  w-100" Text="Sign Up" />
                                   <%-- <div class="modal fade" id="myModal" role="dialog">
  <div class="modal-dialog">
    
     
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close pull-right" data-dismiss="modal">&times;</button>
         
        </div>
        <div class="modal-body" style="text-align:center">
         <asp:Button runat="server" ID="btnloginnow" Text="Login in Now" CssClass="zoom2 btn mb-1 btn-outline-info"  OnClick="btnloginnow_Click" />
        </div>
        <div class="modal-footer">
            
         &nbsp <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>--%>
                                </form>
                                
                                <p class="mt-5 login-form__footer">Have account <b><a href="Login" class="text-primary">Sign In</a></b> now</p>
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
