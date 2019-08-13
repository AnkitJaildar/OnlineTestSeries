<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Questions.aspx.cs" Inherits="Questions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                                                <asp:DropDownList ID="ddlssubjectss" runat="server" OnSelectedIndexChanged="ddlssubjectss_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control btn-outline-info"></asp:DropDownList>
                                             
                                            </div>
                                        </div>
                                          <div class="form-group row">
                                            <label class="col-lg-4 col-form-label" >Select Chapter <span class="text-danger">*</span>
                                            </label>
                                            <div class="col-lg-6">
                                                <asp:DropDownList ID="ddlchapters" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlchapters_SelectedIndexChanged" CssClass="form-control btn-outline-info"></asp:DropDownList>
                                             
                                            </div>
                                        </div>
                                  
                                        <div class="form-group row">
                                            <div class="col-lg-8 ml-auto">
                                                <asp:Button runat="server" ID="btnadd"  cssclass="btn btn-primary" OnClick="btnadd_Click" Text="Add New Question" />
                                               <%-- <button type="submit" class="btn btn-primary">Submit</button>--%>
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
                                <h4 class="card-title">All Questions</h4>
                                <div runat="server" id="divtable" class="table-responsive"> 
          
                                </div>
                            </div>
                        </div>
                    </div>

                       
               
                </div>
            </div>
            <!-- #/ container -->
        </div>
</asp:Content>

