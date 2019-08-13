<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Subjects.aspx.cs" ValidateRequest="false" Inherits="Subjects" %>

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
                                <h4 class="card-title">Add New Subject</h4>
                                <div class="basic-form">
                                    
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Subject</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox runat="server" ID="txtsubject" CssClass="form-control" placeholder="Subject"></asp:TextBox>
                                               
                                               <%-- <input type="email" class="form-control" placeholder="Email">--%>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Subject Description</label>
                                            <div class="col-sm-10">
                                                <style>
                                                    .modal-header {
                                                        display: block;
                                                    }
                                                </style>
                                                 <asp:TextBox TextMode="MultiLine" runat="server" ID="txtdescription" CssClass="form-control summernote" ClientIDMode="Static" placeholder="Subject Description"></asp:TextBox>
                                               <%-- <input type="password" class="form-control" placeholder="Password">--%>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label"> Price (Rs.)  </label>
                                            <div class="col-sm-10">
                                               <asp:TextBox runat="server" ID="txtprice" CssClass=" form-control" Placeholder="Price in Rupees.."></asp:TextBox>
                                                
                                            </div>
                                        </div>
                                    
                                        <div class="form-group row">
                                            <div class="col-sm-12">
                                                 <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" cssclass="btn btn-primary pull-right" Text="Submit"  />
                                            <%--    <button type="submit" class="btn btn-dark">Sign in</button>--%>
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
                                <h4 class="card-title">All Subjects</h4>
                                <div runat="server" id="divtable" class="table-responsive"> 
                                   <%-- <table runat="server" id="tblsubjects" class="table table-bordered table-striped verticle-middle">
                                        <thead>
                                            <tr>
                                                <th scope="col">S NO</th>
                                                 <th scope="col">Subject id</th>
                                                <th scope="col">Subject</th>
                                                <th scope="col">Description</th>
                                                <th scope="col">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>Air Conditioner</td>
                                                <td>
                                                    <div class="progress" style="height: 10px">
                                                        <div class="progress-bar gradient-1" style="width: 70%;" role="progressbar"><span class="sr-only">70% Complete</span>
                                                        </div>
                                                    </div> 
                                                </td>
                                                <td>Apr 20,2018</td>
                                                <td><span class="label gradient-1 btn-rounded">70%</span>
                                                </td>
                                                <td><span><a href="#" data-toggle="tooltip" data-placement="top" title="Edit"><i class="fa fa-pencil color-muted m-r-5"></i> </a><a href="#" data-toggle="tooltip" data-placement="top" title="Close"><i class="fa fa-close color-danger"></i></a></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Textiles</td>
                                                <td>
                                                    <div class="progress" style="height: 10px">
                                                        <div class="progress-bar gradient-2" style="width: 70%;" role="progressbar"><span class="sr-only">70% Complete</span>
                                                        </div>
                                                    </div> 
                                                </td>
                                                <td>May 27,2018</td>
                                                <td><span class="label gradient-2 btn-rounded">70%</span>
                                                </td>
                                                <td><span><a href="#" data-toggle="tooltip" data-placement="top" title="Edit"><i class="fa fa-pencil color-muted m-r-5"></i> </a><a href="#" data-toggle="tooltip" data-placement="top" title="Close"><i class="fa fa-close color-danger"></i></a></span>
                                                </td>
                                            </tr>
                                      
                                        </tbody>
                                    </table>--%>
                                </div>
                            </div>
                        </div>
                    </div>

                       <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                        <div class="modal-dialog" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title" id="exampleModalLabel">New message</h5>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span>
                                                    </button>
                                                </div>
                                                <div class="modal-body">
                                                    <form>
                                                        <div class="form-group">
                                                            <label for="recipient-name" class="col-form-label">Recipient:</label>
                                                            <input type="text" class="form-control" id="recipient-name">
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="message-text" class="col-form-label">Message:</label>
                                                            <textarea class="form-control" id="message-text"></textarea>
                                                        </div>
                                                    </form>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                                    <button type="button" class="btn btn-primary">Send message</button>
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

