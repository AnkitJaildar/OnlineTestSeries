<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UploadBulkData.aspx.cs" Inherits="UploadBulkData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function ErrorShow(txt) {

            swal("Oops!", txt, "error");

        }
        function SuccessMsg(txt) {

            swal("", txt, "success");

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
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-body">
                               
                                     
                                        <div class="form-group row">
                                            <label class="col-lg-4 col-form-label" >Select Subject <span class="text-danger">*</span>
                                            </label>
                                            <div class="col-lg-6">
                                                <asp:DropDownList ID="ddlssubjectss" runat="server" OnSelectedIndexChanged="ddlssubjectss_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                             
                                            </div>
                                        </div>
                                          <div class="form-group row">
                                            <label class="col-lg-4 col-form-label" >Select Chapter <span class="text-danger">*</span>
                                            </label>
                                            <div class="col-lg-6">
                                                <asp:DropDownList ID="ddlchapters" runat="server" CssClass="form-control"></asp:DropDownList>
                                             
                                            </div>
                                        </div>
                                  <div class="form-group row">
                                            <label class="col-lg-4 col-form-label" >Upload Excel<span class="text-danger">*</span>
                                            </label>
                                            <div class="col-lg-8">
                                               <asp:FileUpload runat="server" ID="exceluploader" />
                                                <a href="assets/QuestionAns%20Format.xlsx">Download Excel Format</a>
                                            </div>
                                        </div>
                         
                                        <div class="form-group row">
                                            <label class="col-lg-4 col-form-label"><a href="#">Terms &amp; Conditions</a>  <span class="text-danger">*</span>
                                            </label>
                                            <div class="col-lg-8">
                                                <label class="css-control css-control-primary css-checkbox" for="val-terms">
                                                    <input type="checkbox" class="css-control-input" id="val-terms" name="val-terms" value="1"> <span class="css-control-indicator"></span> I agree to the terms</label>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-8 ml-auto">
                                                <asp:Button runat="server" ID="btnUploadExcel" OnClick="btnUploadExcel_Click" cssclass="btn btn-primary" Text="Submit" />
                                               <%-- <button type="submit" class="btn btn-primary">Submit</button>--%>
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

