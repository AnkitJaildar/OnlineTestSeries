<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AssignTests.aspx.cs" Inherits="AssignTests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style type="text/css">
       
       #ContentPlaceHolder1_GridView1 
        {
            border: 1px solid #ccc;
        }
         #ContentPlaceHolder1_GridView1 th
        {
            background-color: #F7F7F7;
            color: #333;
            font-weight: bold;
        }
         #ContentPlaceHolder1_GridView1 th,  #ContentPlaceHolder1_GridView1 td
        {
            padding: 5px;
            border-color: #ccc;
        }
    </style>
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
                                <h4 class="card-title">Assign Questions to Test</h4>
                                <div class="basic-form">
                            <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Test Type(free/paid)</label>
                                            <div class="col-sm-10">
                                             
                                                 <asp:DropDownList ID="ddltesttype" runat="server" OnSelectedIndexChanged="ddltesttype_SelectedIndexChanged"  AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                             
                                            </div>
                                        </div>
                                       <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Test Name</label>
                                            <div class="col-sm-10">
                                             
                                                 <asp:DropDownList ID="ddltest" runat="server" OnSelectedIndexChanged="ddltest_SelectedIndexChanged"  AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                             
                                            </div>
                                        </div>
                                         <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Select Subject</label>
                                            <div class="col-sm-10">
                                              <asp:DropDownList ID="ddlssubjectss" runat="server" OnSelectedIndexChanged="ddlssubjectss_SelectedIndexChanged"  AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2 col-form-label">Select Chapter</label>
                                            <div class="col-sm-10">
                                              <asp:DropDownList ID="ddlchapters" runat="server" OnSelectedIndexChanged="ddlchapters_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                             
                                            </div>
                                        </div>
                                       <div class="form-group row">
                                          <div class="col-sm-12">
                                            <div class="card">
                                            <div class="card-body">
                                            <h6 class="card-title">Select Questions</h6>
                                
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Choose" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1"  runat="server" />
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                     <asp:BoundField DataField="chapterid" HeaderText="Cid" ItemStyle-Width="1" />
                                                <asp:BoundField DataField="qid" HeaderText="Id" ItemStyle-Width="1" />
                                                     <asp:BoundField DataField="chaptername" HeaderText="Chapter" ItemStyle-Width="100" />
                                                <asp:BoundField DataField="questiondescription" HeaderText="Question" HtmlEncode="false" ItemStyle-Width="300" />
                                                    <asp:TemplateField HeaderText="Marks" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                               <asp:TextBox runat="server" ID="txtmarks" Placeholder="Marks"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Duration" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txttime" Placeholder="Minutes"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                                </Columns>
                                                </asp:GridView>
                             
                                            </div>
                                            </div>
                                         
                                            </div>
                                          
                                        </div>
                                     
                                    
                                        <div class="form-group row">
                                            <div class="col-sm-12">
                                                 <asp:Button runat="server" ID="btnsubmit"  OnClick="btnsubmit_Click"  cssclass="btn btn-primary pull-right" Text="Assign"  />
                                         
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
                                <h4 class="card-title">All Test Info</h4>
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

