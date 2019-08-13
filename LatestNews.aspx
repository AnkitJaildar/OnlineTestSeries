<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="LatestNews.aspx.cs" Inherits="LatestNews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script>
        function ErrorShow(txt) {

            swal("Oops!", txt, "error");

        }
        function SuccessMsg(txt) {
            swal("", txt, "info");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


     
            <div class="content-body">

            <div class="row page-titles mx-0">
                <div class="col p-md-0">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="javascript:void(0)">Notifications</a></li>
                       
                    </ol>
                </div>
            </div>
            <!-- row -->

            <div class="container-fluid">
                 <div class="row">
                   <div class="col-lg-12">
                        <div class="card">
                            <div class="card-body">
                                <h4 class="card-title">Publish Latest News</h4>
                                <div class="basic-form">
                                    
                                    
                                         <div class="form-group row ">
                                            <label class="col-sm-2 col-form-label">Add Notifications</label>
                                            <div class="col-sm-10">
                                                  <style>
                                                    .modal-header {
                                                        display: block;
                                                    }
                                                </style>
                                                <asp:TextBox TextMode="MultiLine" runat="server" ID="txtlatestnews" Placeholder="Description" CssClass="form-control  summernote" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                     <div class="form-group row ">
                                            <label class="col-sm-2 col-form-label">News Expire Days</label>
                                         <asp:HiddenField runat="server" ID="hiddennewsid" />
                                            <div class="col-sm-10">
                                    <asp:TextBox TextMode="Number" ID="txtexpiredays" placeholder="Expire days" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                         </div>
                                        <div class="form-group row">
                                            <div class="col-sm-12">
                                                 <asp:Button runat="server" ID="btnsubmit" OnClick="btnsubmit_Click"  cssclass="btn btn-primary pull-right" Text="Add Notification"  />
                                         
                                            </div>
                                        </div>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                      </div>
                <div class="row">
                 
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-body">
                                <h4 class="card-title">News</h4>
                                <div runat="server" id="divtable" class="table-responsive"> 
                                
                                </div>
                            </div>
                        </div>
                    </div>
                     
                </div>
            </div>
            <!-- #/ container -->
                 </div>
        <!--**********************************
            Content body end
        ***********************************-->
</asp:Content>

