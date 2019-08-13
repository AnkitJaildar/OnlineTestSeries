<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditChapter.aspx.cs" ValidateRequest="false" Inherits="EditChapter" %>

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
                                <h4 class="card-title">Edit  Chapter</h4>
                                <div class="basic-form">
                                    
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Select Subject</label>
                                            <div class="col-sm-10">
                                              <asp:DropDownList ID="ddlssubjectss" runat="server"   AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Chapter</label>
                                            <div class="col-sm-10">
                                             <asp:TextBox runat="server" ID="txtchapter" CssClass="form-control" placeholder="Chapter"></asp:TextBox>
                                                 <asp:HiddenField ID="hiddenchapterid" runat="server" /> 
                                             
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Short Description</label>
                                            <div class="col-sm-10">
                                                  <style>
                                                    .modal-header {
                                                        display: block;
                                                    }
                                                </style>
                                                <asp:TextBox TextMode="MultiLine" runat="server" ID="txtshortdescription" Placeholder="Description" CssClass="form-control summernote"></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Full Description</label>
                                            <div class="col-sm-10">
                                                 
                                                <asp:TextBox TextMode="MultiLine" runat="server" ID="txtdecsription" Placeholder="Description" CssClass="form-control summernote"></asp:TextBox>
                                            </div>
                                        </div>
                                    
                                        <div class="form-group row">
                                            <div class="col-sm-12">
                                                 <asp:Button runat="server" ID="btnupdate" OnClick="btnupdate_Click"  cssclass="btn btn-primary pull-right" Text="Update"  />
                                         
                                            </div>
                                        </div>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                      </div>
       
            </div>
            <!-- #/ container -->
                 </div>
</asp:Content>

