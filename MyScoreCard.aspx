<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyScoreCard.aspx.cs" Inherits="MyScoreCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
.DashboardDetails{
 -webkit-transition-duration: 0.5s;
   transition-duration: 0.5s;
}
     

        .DashboardDetails:hover{
             /*-webkit-transition-duration: 0.5s;
   transition-duration: 0.5s;*/
            -webkit-box-shadow: 0px 5px 22px -3px rgba(0,0,0,0.75);
-moz-box-shadow: 0px 5px 22px -3px rgba(0,0,0,0.75);
box-shadow: 0px 5px 22px -3px rgba(0,0,0,0.75);
cursor:pointer;


        }


.responsive {
  width: 100%;
  height: auto;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <!--**********************************
            Content body start
        ***********************************-->
        <div class="content-body">

            <div class="row page-titles mx-0">
                <div class="col p-md-0">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="javascript:void(0)">My ScoreCard</a></li>
                       
                    </ol>
                </div>
            </div>
            <!-- row -->

            <div class="container-fluid">
                 <div class="row">
                  <div class="col-md-12">
                        <div class="card DashboardDetails">
                            <div class="card-body">
                                <h4 class="card-title">Your Last Test Performance </h4>
                                <hr />
                                <h5 class="mt-3"><asp:Label runat="server" ID="lasttestperformance"></asp:Label><span class="float-right"><asp:Label runat="server" ID="lasttestperformancepercentage"></asp:Label></span></h5>
                                <div class="progress" style="height: 18px">
                                    <div runat="server" id="lasttestperformanceprogressbar"   role="progressbar">
                                    </div>
                                </div>
                                <h5 class="mt-3"><span class="float-right"><asp:Button runat="server" ID="btnview" Text="View" CssClass="btn btn-flat btn-outline-info pull-right " /></span></h5>
                             <%--   <div class="progress" style="height: 9px">
                                    <div class="progress-bar bg-info wow  progress-" style="width: 90%;" role="progressbar"><span class="sr-only">60% Complete</span>
                                    </div>
                                </div>
                                <h5 class="mt-3">Illustrator <span class="float-right">65%</span></h5>
                                <div class="progress" style="height: 9px">
                                    <div class="progress-bar bg-success wow  progress-" style="width: 65%;" role="progressbar"><span class="sr-only">60% Complete</span>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>

                
                      
                


<%--                <div class="row">
                    <div class="col-lg-4 col-sm-12">
                        <div class="card DashboardDetails">
                            <div class="stat-widget-one">
                                <div class="stat-content">
                                    <div class="stat-text">Last Test Performance</div>
                                    <div class="stat-digit gradient-3-text"><i class="fa fa-usd"></i>8500</div>
                                </div>
                                <div class="progress mb-3">
                                    <div class="progress-bar gradient-3" style="width: 50%;" role="progressbar"><span class="sr-only">50% Complete</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4 col-sm-12">
                        <div class="card DashboardDetails">
                            <div class="stat-widget-one">
                                <div class="stat-content">
                                    <div class="stat-text">Income Detail</div>
                                    <div class="stat-digit gradient-4-text"><i class="fa fa-usd"></i>7800</div>
                                </div>
                                <div class="progress mb-3">
                                    <div class="progress-bar gradient-4" style="width: 40%;" role="progressbar"><span class="sr-only">40% Complete</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4 col-sm-12">
                        <div class="card DashboardDetails">
                            <div class="stat-widget-one">
                                <div class="stat-content">
                                    <div class="stat-text">Task Completed</div>
                                    <div class="stat-digit gradient-4-text"><i class="fa fa-usd"></i> 500</div>
                                </div>
                                <div class="progress mb-3">
                                    <div class="progress-bar gradient-4" style="width: 15%;" role="progressbar"><span class="sr-only">15% Complete</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
               
                <div class="row">
                    <div class="col-lg-3 col-sm-6">
                        <div class="card card-widget">
                            <div class="card-body gradient-3">
                                <div class="media">
                                    <span class="card-widget__icon"><i class="icon-home"></i></span>
                                    <div class="media-body">
                                        <h2 class="card-widget__title">520</h2>
                                        <h5 class="card-widget__subtitle">All Properties</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3 col-sm-6">
                        <div class="card card-widget">
                            <div class="card-body gradient-4">
                                <div class="media">
                                    <span class="card-widget__icon"><i class="icon-tag"></i></span>
                                    <div class="media-body">
                                        <h2 class="card-widget__title">720</h2>
                                        <h5 class="card-widget__subtitle">Open Tickets</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3 col-sm-6">
                        <div class="card card-widget">
                            <div class="card-body gradient-4">
                                <div class="media">
                                    <span class="card-widget__icon"><i class="icon-emotsmile"></i></span>
                                    <div class="media-body">
                                        <h2 class="card-widget__title">1002</h2>
                                        <h5 class="card-widget__subtitle">Task Completed</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3 col-sm-6">
                        <div class="card card-widget">
                            <div class="card-body gradient-9">
                                <div class="media">
                                    <span class="card-widget__icon"><i class="icon-ghost"></i></span>
                                    <div class="media-body">
                                        <h2 class="card-widget__title">420</h2>
                                        <h5 class="card-widget__subtitle">Threats</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-4 col-sm-12">
                        <div class="card DashboardDetails">
                            <div class="card-body">
                                <div class="text-center">
                                    <span class="display-5"><i class="icon-earphones gradient-3-text"></i></span>
                                    <h2 class="mt-3">5K Songs</h2>
                                    <p>Your playlist download complete</p><a href="javascript:void()" class="btn gradient-3 btn-lg border-0 btn-rounded px-5">Download
                                        now</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-12">
                        <div class="card DashboardDetails">
                            <div class="card-body">
                                <div class="text-center">
                                    <span class="display-5"><i class="icon-diamond gradient-4-text"></i></span>
                                    <h2 class="mt-3">765 Point</h2>
                                    <p>Nice, you are doing great!</p>
                                    <a href="javascript:void()" class="btn gradient-4 btn-lg border-0 btn-rounded px-5">Redeem
                                        now</a>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4 col-sm-12">
                        <div class="card DashboardDetails">
                            <div class="card-body">
                                <div class="text-center">
                                    <span class="display-5"><i class="icon-user gradient-4-text"></i></span>
                                    <h2 class="mt-3">5210 Users</h2>
                                    <p>Currently active</p><a href="javascript:void()" class="btn gradient-4 btn-lg border-0 btn-rounded px-5">Add
                                        more</a>
                                </div>
                            </div>
                        </div>
                    </div>

                   
                </div>

                <div class="row">
                    <div class="col-lg-4 col-sm-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <img alt="" class="rounded-circle mt-4" src="images/users/5.jpg">
                                    <h4 class="card-widget__title text-dark mt-3">Deangelo Sena</h4>
                                    <p class="text-muted">Senior Manager</p>
                                    <a class="btn gradient-4 btn-lg border-0 btn-rounded px-5" href="javascript:void()">Folllow</a>
                                </div>
                            </div>
                            <div class="card-footer border-0 bg-transparent">
                                <div class="row">
                                    <div class="col-4 border-right-1 pt-3">
                                        <a class="text-center d-block text-muted" href="javascript:void()">
                                            <i class="fa fa-star gradient-1-text" aria-hidden="true"></i>
                                            <p class="">Star</p>
                                        </a>
                                    </div>
                                    <div class="col-4 border-right-1 pt-3"><a class="text-center d-block text-muted" href="javascript:void()">
                                        <i class="fa fa-heart gradient-3-text"></i>
                                            <p class="">Like</p>
                                        </a>
                                    </div>
                                    <div class="col-4 pt-3"><a class="text-center d-block text-muted" href="javascript:void()">
                                        <i class="fa fa-envelope gradient-4-text"></i>
                                            <p class="">Email</p>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4 col-sm-12">
                        <div class="card card-widget">
                            <div class="card-body">
                                <h5 class="text-muted">This Month</h5>
                                <h2 class="mt-4">$6,932.60</h2>
                                <span>Total Revenue</span>
                                <div class="mt-4">
                                    <h4>2,365</h4>
                                    <h6>Online Earning <span class="pull-right">80%</span></h6>
                                    <div class="progress mb-3" style="height: 7px">
                                        <div class="progress-bar gradient-1" style="width: 80%;" role="progressbar"><span class="sr-only">80% Complete</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <h4>1,250</h4>
                                    <h6 class="m-t-10 text-muted">Offline Earning <span class="pull-right">50%</span></h6>
                                    <div class="progress mb-3" style="height: 7px">
                                        <div class="progress-bar gradient-3" style="width: 50%;" role="progressbar"><span class="sr-only">50% Complete</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="mt-4">
                                    <h4>1,250</h4>
                                    <h6 class="m-t-10 text-muted">Yearly Saving <span class="pull-right">35%</span></h6>
                                    <div class="progress mb-3" style="height: 7px">
                                        <div class="progress-bar gradient-4" style="width: 35%;" role="progressbar"><span class="sr-only">35% Complete</span>
                                        </div>
                                    </div>
                                </div>
                                <!-- <div class="mt-4">
                                    <h4>5,250</h4>
                                    <h6 class="m-t-10 text-muted">Budget Pending <span class="pull-right">70%</span></h6>
                                    <div class="progress mb-3" style="height: 7px">
                                        <div class="progress-bar gradient-8" style="width: 70%;" role="progressbar"><span class="sr-only">70% Complete</span>
                                        </div>
                                    </div>
                                </div> -->
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4 col-sm-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <img alt="" class="rounded-circle mt-4" src="images/users/4.jpg">
                                    <h4 class="card-widget__title text-dark mt-3">Bennie Belvin</h4>
                                    <p class="text-muted">Junior Tester</p>
                                    <a class="btn gradient-4 btn-lg border-0 btn-rounded px-5" href="javascript:void()">Folllow</a>
                                </div>
                            </div>
                            <div class="card-footer border-0 bg-transparent">
                                <div class="row">
                                    <div class="col-4 border-right-1 pt-3">
                                        <a class="text-center d-block text-muted" href="javascript:void()">
                                            <i class="fa fa-star gradient-1-text" aria-hidden="true"></i>
                                            <p class="">Star</p>
                                        </a>
                                    </div>
                                    <div class="col-4 border-right-1 pt-3"><a class="text-center d-block text-muted" href="javascript:void()">
                                        <i class="fa fa-heart gradient-3-text"></i>
                                            <p class="">Like</p>
                                        </a>
                                    </div>
                                    <div class="col-4 pt-3"><a class="text-center d-block text-muted" href="javascript:void()">
                                        <i class="fa fa-envelope gradient-4-text"></i>
                                            <p class="">Email</p>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>

              

                
                
            </div>
            <!-- #/ container -->
        </div>
        <!--**********************************
            Content body end
        ***********************************-->
</asp:Content>

