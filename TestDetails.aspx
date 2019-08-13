<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TestDetails.aspx.cs" Inherits="TestDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

.content
{
 
  /*transition-property: padding;*/
margin-right:30px;
margin-bottom:30px;
padding:30px;
 -webkit-transition-duration: 0.5s;
   transition-duration: 0.5s;
}
.content:hover
{
 -webkit-box-shadow: 0px 5px 22px -3px rgba(0,0,0,0.75);
-moz-box-shadow: 0px 5px 22px -3px rgba(0,0,0,0.75);
box-shadow: 0px 5px 22px -3px rgba(0,0,0,0.75);
background-color:black;
color:wheat;




    
}
.oldprice
{
    text-decoration:line-through;
}
</style>

    <script type="text/javascript">
        function ResultPopup() {
            $('#myModal').modal('show');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!--**********************************
            Content body start
        ***********************************-->

        <div class="content-body" >

            <div class="row page-titles mx-0">
                <div class="col p-md-0">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="javascript:void(0)">Dashboard</a></li>
                        <li class="breadcrumb-item active"><a href="javascript:void(0)">Home</a></li>
                    </ol>
                </div>
            </div>
            <!-- row -->

            <div class="container-fluid" style="overflow:auto">
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
              
                  <asp:DataList runat="server" ID="Datalist1" RepeatColumns="4"  
            RepeatDirection="Horizontal" OnEditCommand="Datalist1_EditCommand"  >
                      
    <ItemTemplate>
        
                <div class="content gradient-3">
<span class="display-5"><i class="icon-grid gradient-3-text"></i></span>
                    <br />
    Testid:<asp:Label runat="server" ID="lbltestid" Text ='<%#Eval("testid") %>'></asp:Label> 
    <br />
    test Name: <%#Eval("testname") %>
<br />
test Duration :<%#Eval("testduration") %>
<br />
Total Questions :<%#Eval("totalquestions") %>
<br />
 Total Marks :<%#Eval("totalmarks") %>
<br />

<span class="oldprice">Old Price</span>
                    <br />
                    <br />
<asp:Button runat="server"  ID="btntest" Text='<%#Eval("Testtype") %>' CommandName="Edit" CssClass='<%#Eval("cssclasss") %>' />
    </div>  
     

    </ItemTemplate>
    </asp:DataList>
                    
                   

                
            </div>
            <!-- #/ container -->
        </div>
        <!--**********************************
            Content body end
        ***********************************-->


  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close pull-right" data-dismiss="modal">&times;</button>
         
        </div>
        <div class="modal-body">
          <asp:Label runat="server" id="lblrightquestions"></asp:Label>
             <div runat="server" id="divtable" class="table-responsive"> 
                              
          </div>
        </div>
        <div class="modal-footer">
            <asp:Button runat="server" ID="btndownloadscorecard" Text="Download Score Card" CssClass="btn mb-1 btn-outline-warning pull-left" OnClick="btndownloadscorecard_Click" />
         &nbsp <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>
  




</asp:Content>

