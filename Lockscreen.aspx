<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lockscreen.aspx.cs" Inherits="Lockscreen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width,initial-scale=1"/>
    <title>Locked</title>
    <!-- Favicon icon -->
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="icon" type="image/png" sizes="16x16" href="../assets/images/favicon.html"/>
    <link rel="stylesheet" href="../../use.fontawesome.com/releases/v5.5.0/css/all.css" integrity="sha384-B4dIYHKNBt8Bc12p+WXckhzcICo0wtJAoU8YZTY5qE0Id1GSseTk6S+L3BlXeVIU" crossorigin="anonymous"/>
    <link href="css/style.css" rel="stylesheet"/>

    <script>
        function ErrorShow(txt) {

            swal(txt ,"", "error");

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
<body>

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

    



    <div class="login-form-bg h-100" style="margin-top:10px;">
        <div class="container h-100">
            <div class="row justify-content-center h-100">
                <div class="col-xl-6">
                    <div class="form-input-content">
                        <div class="card login-form mb-0">
                            <div class="card-body pt-5">
                                <a class="text-center" href="#"> <h4><i class="fa fa-3x fa-key" aria-hidden="true"></i> Locked</h4></a>
                                <form class="mt-5 mb-3 login-input" runat="server" >
                                    <div class="form-group">
                                       <asp:Label  ForeColor="Red" Visible="false" runat="server" ID="lblerror"></asp:Label>
                                    </div>
                                     <div class="form-group" style="text-align:center">
                                       <asp:Label Font-Bold="true" runat="server" ID="lblusername"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <input type="password" id="txtpassword" runat="server" class="form-control" placeholder="Password" required/>
                                    </div>
                                    <asp:Button runat="server" ID="btnloginagain" Text="Unlock" OnClick="btnloginagain_Click" CssClass="btn login-form__btn submit w-100" />
                                    <%--<button class="btn login-form__btn submit w-100">Unlock</button>--%>
                                </form>
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

</body>
</html>
