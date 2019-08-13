<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StartTest.aspx.cs" Inherits="StartTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
       .radiobuttonlist
{

    font: 12px Verdana, sans-serif;
    color: #000; /* non selected color */
}


.radiobuttonlist label
{
    color: #3E3928;
    background-color:#E8E5D4;
    padding-left: 6px;
    padding-right: 6px;
    padding-top: 2px;
    padding-bottom: 2px;
    border: 1px solid #AAAAAA;
    margin: 0px 0px 0px 0px;
    white-space: nowrap;
    clear: left;
    margin-right: 5px;
    margin-bottom:5px;

}

.radiobuttonlist label:hover
{
    color: #CC3300;
    background: #D1CFC2;

}

.radiobuttoncontainer
{
    position:relative;
    z-index:1;
}

.radiobuttonbackground
{
    position:relative;
    z-index:0;
    border: solid 1px #AcA899; 
    padding: 10px; 
    background-color:#F3F2E7;
}

        .btnqstin{
            margin-right:2px;
        }

        .protected {
    -moz-user-select:none;
    -webkit-user-select:none;
    user-select:none;
}
    </style>

    <script type="text/javascript">
        window.onload = function () {
            document.onkeydown = function (e) {
                return (e.which || e.keyCode) != 116;
            };
        }
        document.addEventListener("contextmenu", function (e) {
            e.preventDefault();
        }, false);

        //document.onmousedown = disableclick;
        //status = "Right Click Disabled";
        //function disableclick(event) {
        //    if (event.button == 2) {
        //        alert(status);
               
        //        return false;
        //    }

       // }


          function goFullscreen(id) {
              // Get the element that we want to take into fullscreen mode
              var element = document.getElementById(id);

              // These function will not exist in the browsers that don't support fullscreen mode yet, 
              // so we'll have to check to see if they're available before calling them.
              //var height = $(window).height();  // Get the height of the browser window area.
              //var element = $("#onlinetestarea");          // Find the element to resize.
              //element.height(height);

            

              if (element.mozRequestFullScreen) {
                  // This is how to go into fullscren mode in Firefox
                  // Note the "moz" prefix, which is short for Mozilla.
                  element.mozRequestFullScreen();
              } else if (element.webkitRequestFullScreen) {
                  // This is how to go into fullscreen mode in Chrome and Safari
                  // Both of those browsers are based on the Webkit project, hence the same prefix.
                  element.webkitRequestFullScreen();
              }
              // Hooray, now we're in fullscreen mode!
          }
          function ErrorShow(txt) {

              swal("Oops!", txt, "error");

          }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="content-body protected" id="onlinetestarea"  style=" overflow: auto;" >

         <%--   <div class="row page-titles mx-0">
                <div class="col p-md-0">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="javascript:void(0)">Test</a></li>
                        
                    </ol>
                </div>
            </div>--%>
        
          
          <asp:ScriptManager runat="server" id="scrtpmngr"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                 <div runat="server" id="TimerArea" visible="false" class="container-fluid">
                  <div  class="row">
                    <div class="col-12">
                       
                           
                                <asp:Timer runat="server" ID="TestTimer"  OnTick="TestTimer_Tick" Interval="1000"></asp:Timer>
                              <div class="alert alert-danger">Time Left: &nbsp &nbsp<asp:Label runat="server" ID="lbltimeleft" ForeColor="Red"></asp:Label>
                                 
                                 &nbsp &nbsp  <asp:Label runat="server" ID="lbltimewarning" ForeColor="Red"></asp:Label>
                              
                              
                        </div>
                    </div>
                </div>
                     </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnstarttest" EventName="click" />

            </Triggers>
        </asp:UpdatePanel>
          <asp:UpdatePanel runat="server" ID="pnltest" UpdateMode="Conditional" >
              <ContentTemplate>
              <div runat="server" id="divtest" class="container-fluid">
                <div runat="server" id="instructionArea" class="row">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                              <div class="alert alert-danger">Instructions:</div>
                                <p runat="server" id="paragraphinstruction"></p>
                                <asp:Button runat="server" ID="btnstarttest" CssClass="btn mb-1 btn-rounded btn-outline-info" Text="Start Test" OnClick="btnstarttest_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                 

                   <div runat="server" id="ColorIndicationdiv" visible="false" class="row">
                        <div class="col-lg-8 col-sm-12">
                        <div class="card">
                            <div class="card-body">
                                  <asp:HiddenField ID="hiddentid" runat="server" />
                                 
                              
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-12">
                        <div class="card">
                            <div class="card-body">
                              
                                  <div class="btn-group mb-2 btn-group-sm">
                                        <button class="btn btn-default" type="button">unseen</button>&nbsp &nbsp
                                        <button class="btn btn-warning" type="button">only seen</button>&nbsp &nbsp
                                        <button class="btn btn-info" type="button">attempted</button>
                                    </div>
                              
                            </div>
                        </div>
                    </div>
                </div>
                  
                <div runat="server" id="testArea" visible="false"  class="row">

                    <div class="col-lg-8 col-sm-12">
                        <div class="card">
                        <div class="card-body">
                        <asp:DataList runat="server" ID="Datalist1" 
                        RepeatDirection="Horizontal" >
                        <ItemTemplate>

                         <i class="fa fa-quora" aria-hidden="true"></i> <asp:Label runat="server" ID="Label1" ForeColor="Black" Text ='<%#Eval("Row_Number") %>'></asp:Label>: <asp:Label runat="server" ID="lbltestid" Text ='<%#Eval("questiondescription") %>' ForeColor="Black"></asp:Label> 

                        </ItemTemplate>

                        </asp:DataList>

                    <br />
                          <asp:RadioButtonList runat="server"   ID="rbtnanslist" RepeatColumns="1" RepeatDirection="Horizontal" CssClass="radiobuttonlist">
                              
                           </asp:RadioButtonList>

                            <br />
                            <asp:HiddenField runat="server" ID="hiddenqid" />
                            <asp:Button runat="server" ID="btnsubmitans" OnClick="btnsubmitans_Click" Text="Submit Answer" CssClass="btn mb-1  btn-outline-info pull-right" />
                            <br />
                            <hr />
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
 <asp:Button runat="server" Text="First" ID="btnfirst" OnClick="btnfirst_Click" CssClass="btn mb-1   btn-outline-info" />
                              
                         <asp:Button runat="server" Text="Previous" ID="btnprevious" OnClick="btnprevious_Click"  CssClass="btn mb-1  btn-outline-warning"/>
                   
                       <asp:Button runat="server" Text="Next" ID="btnnext" OnClick="btnnext_Click"  CssClass="btn mb-1    btn-outline-info"/>
                   
                         <asp:Button runat="server" Text="Last" ID="btnlast" OnClick="btnlast_Click"  CssClass="btn mb-1    btn-outline-danger"/>
                    </div>
                   
                           </div>
                             <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                     <asp:Button runat="server" ID="btnfinish" OnClick="btnfinish_Click" Text="Finish" CssClass="btn mb-1 btn-flat  btn-outline-danger pull-right" />
                                        </div>
                        </div>
                                 </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-12">
                        <div class="card">
                            <div class="card-body">

                         <asp:DataList runat="server" ID="DatalistQuestionNumbers" RepeatColumns="4" OnEditCommand="DatalistQuestionNumbers_EditCommand"
                        RepeatDirection="Horizontal" >
                        <ItemTemplate>
                     
                            <asp:Label runat="server" ID="lblqidlist" Text ='<%#Eval("qid") %>' Visible="false"></asp:Label>
<%--                       <asp:Button runat="server" ID="btn" Text='<%#Eval("Row_Number") %>' CssClass="btn btnqstin mb-1 btn-outline-default" CommandName="Edit" />--%>
                             <asp:Button runat="server" ID="btn" Text='<%#Eval("Row_Number") %>' CssClass="btn btnqstin mb-1 btn-outline-default" autopostback="false" />

                        </ItemTemplate>

                        </asp:DataList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                  </ContentTemplate>
              </asp:UpdatePanel>
            <!-- #/ container -->
        </div>

</asp:Content>

