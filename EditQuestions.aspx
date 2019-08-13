<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditQuestions.aspx.cs" ValidateRequest="false" Inherits="EditQuestions" %>

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
                                <h4 class="card-title">Question</h4>
                                <div class="basic-form">
                                    
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Select Subject</label>
                                            <div class="col-sm-10">
                                              <asp:DropDownList ID="ddlssubjectss" runat="server" OnSelectedIndexChanged="ddlssubjectss_SelectedIndexChanged"   AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                      <div class="form-group row">
                                            <label class="col-sm-2 col-form-label" >Select Chapter <span class="text-danger">*</span>
                                            </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddlchapters" runat="server" CssClass="form-control"></asp:DropDownList>
                                             
                                            </div>
                                          </div>

                                      <div class="form-group row">
                                            <label class="col-sm-2 col-form-label" >Question Type: <span class="text-danger">*</span>
                                            </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddlqtype" runat="server" CssClass="form-control"></asp:DropDownList>
                                             
                                            </div>
                                          </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Question</label>
                                            <div class="col-sm-10">
                                                  <style>
                                                    .modal-header {
                                                        display: block;
                                                    }
                                                </style>
                                              <asp:TextBox TextMode="MultiLine" runat="server" ID="txtquestion" Placeholder="Question" CssClass="form-control summernote"></asp:TextBox>
                                                 <asp:HiddenField ID="hiddenqid" runat="server" /> 
                                             
                                            </div>
                                        </div>
                                         <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Option A:</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox TextMode="MultiLine" runat="server" ID="txta" Placeholder="Option A" CssClass="form-control "></asp:TextBox>
                                                <asp:HiddenField ID="Hiddenansid_a" runat="server" /> 
                                            </div>
                                        </div>
                                     <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Option B:</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox TextMode="MultiLine" runat="server" ID="txtb" Placeholder="Option B" CssClass="form-control "></asp:TextBox>
                                                 <asp:HiddenField ID="Hiddenansid_b" runat="server" /> 
                                            </div>
                                        </div>
                                     <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Option C:</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox TextMode="MultiLine" runat="server" ID="txtc" Placeholder="Option C" CssClass="form-control "></asp:TextBox>
                                                 <asp:HiddenField ID="Hiddenansid_c" runat="server" /> 
                                            </div>
                                        </div>
                                     <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Option D:</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox TextMode="MultiLine" runat="server" ID="txtd" Placeholder="Option D" CssClass="form-control "></asp:TextBox>
                                                <asp:HiddenField ID="Hiddenansid_d" runat="server" /> 
                                            </div>
                                        </div>
                                    
                                            <div class="form-group row">
                                            <label class="col-sm-2 col-form-label" >Correct Option: <span class="text-danger">*</span>
                                            </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddloptions" runat="server" CssClass="form-control"></asp:DropDownList>
                                             
                                            </div>
                                          </div>
                                    <div class="form-group row">
                                            <div class="col-sm-12">
                                                 <asp:Button runat="server" ID="btnupdate" OnClick="btnupdate_Click"  cssclass="btn btn-primary pull-right" Text="Update"/>
                                          
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

