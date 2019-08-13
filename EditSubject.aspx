<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditSubject.aspx.cs" ValidateRequest="false" Inherits="EditSubject" %>

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
     <div class="container-fluid" >
                 <div class="row">
                   <div class="col-lg-12">
                        <div class="card">
                            <div class="card-body">
                                <h4 class="card-title">Update Subject</h4>
                                <div class="basic-form">
                                    
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Subject</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox runat="server" ID="txtsubject" CssClass="form-control" placeholder="Subject"></asp:TextBox>
                                               <asp:HiddenField ID="hiddensubid" runat="server" /> 
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
                                                 <asp:TextBox runat="server" TextMode="MultiLine" ID="txtdescription" CssClass="form-control summernote" placeholder="Subject Description"></asp:TextBox>
                                            
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
                                                 <asp:Button runat="server" ID="btnupdate" OnClick="btnupdate_Click"  cssclass="btn btn-primary pull-right" Text="Update"  />
                                          
                                            </div>
                                        </div>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                      </div>
          </div>
          </div>
</asp:Content>

