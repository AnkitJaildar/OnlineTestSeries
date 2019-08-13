<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChapterDescription.aspx.cs" Inherits="ChapterDescription" %>

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
/*background-color:black;
color:wheat;*/




    
}
.oldprice
{
    text-decoration:line-through;
}
</style>
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
                                <h4 class="card-title">Read Chapter Description</h4>
                                <div class="basic-form">
                                    
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Subject</label>
                                            <div class="col-sm-10">
                                              <asp:DropDownList ID="ddlssubjectss" runat="server"   AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Chapter</label>
                                            <div class="col-sm-10">
                                             <asp:label runat="server" ID="txtchapter" CssClass="form-control" ></asp:label>
                                                 <asp:HiddenField ID="hiddenchapterid" runat="server" /> 
                                             
                                            </div>
                                        </div>
                                      <%--   <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Chapeter Description</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox TextMode="MultiLine" runat="server" ID="txtdecsription" Placeholder="Description" CssClass="form-control summernote"></asp:TextBox>
                                            </div>
                                        </div>--%>
                                    <div class="form-group row ">
                                      <div class="col-12">
                        <div class="card  content">
                            <div class="card-body">
                              <div class="alert alert-danger">Chapter Description:</div>
                                <hr />
                                <p  runat="server" id="paragraphDescription"></p>
                                <br />
                               
                                <asp:Button runat="server" ID="btnreadmore" CssClass="btn mb-1 btn-rounded btn-outline-info pull-right" Text="Read More..." OnClick="btnreadmore_Click" />
                                <br />
                                 <br />
                                  <hr />
                            </div>
                        </div>
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

