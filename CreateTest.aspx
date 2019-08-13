<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateTest.aspx.cs" ValidateRequest="false" Inherits="CreateTest" %>

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
                        <li class="breadcrumb-item"><a href="javascript:void(0)">Dashboard</a></li>
                        <li class="breadcrumb-item active"><a href="javascript:void(0)">Home</a></li>
                    </ol>
                </div>
            </div>
            <!-- row -->

            <div class="container-fluid">
                 <div class="row">
                   <div class="col-lg-12">
                        <div class="card">
                            <div class="card-body">
                                <h4 class="card-title">Add New Test</h4>
                                <div class="basic-form">
                                    
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Test Name</label>
                                            <div class="col-sm-10">
                                             <asp:TextBox runat="server" ID="txttestname" CssClass="form-control" placeholder="Test Name"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Test Type(free/paid)</label>
                                            <div class="col-sm-10">
                                             
                                                 <asp:DropDownList ID="ddltesttype" runat="server" OnSelectedIndexChanged="ddltesttype_SelectedIndexChanged"  AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                             
                                            </div>
                                        </div>
                                         <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">No. of Questions</label>
                                            <div class="col-sm-10">
                                                 <asp:TextBox runat="server" ID="txttotalquestions" CssClass="form-control" placeholder="Total Questions"></asp:TextBox>
                                               
                                            </div>
                                        </div>
                                       <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Total marks</label>
                                            <div class="col-sm-10">
                                                 <asp:TextBox runat="server" ID="txttotalmarks" CssClass="form-control" placeholder="Marks"></asp:TextBox>
                                              
                                            </div>
                                        </div>
                                       <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Test Duration</label>
                                            <div class="col-sm-10">
                                                 <asp:TextBox runat="server" ID="txttestduration" CssClass="form-control" placeholder="Duration (in minutes)"></asp:TextBox>
                                              
                                            </div>
                                        </div>
                                      <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Test Description</label>
                                            <div class="col-sm-10">
                                                
                                                <asp:TextBox TextMode="MultiLine" runat="server" ID="txtdescription" Placeholder="Description" CssClass="form-control summernote"></asp:TextBox>
                                            </div>
                                        </div>
                                    
                                        <div class="form-group row">
                                            <div class="col-sm-12">
                                                 <asp:Button runat="server" ID="btnsubmit" OnClick="btnsubmit_Click"  cssclass="btn btn-primary pull-right" Text="Submit"  />
                                         
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
                                <h4 class="card-title">All Test </h4>
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

