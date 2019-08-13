<%@ Application Language="C#" %>

<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">
    

    
    void Application_Start(object sender, EventArgs e)
    {

        RegisterRoutes(RouteTable.Routes);
        // Code that runs on application startup

    }
    static void RegisterRoutes(RouteCollection routes)
    {      routes.MapPageRoute("Login", "Login", "~/Login.aspx");
         routes.MapPageRoute("SignUp", "SignUp", "~/SignUp.aspx");
        routes.MapPageRoute("Dashboard", "Dashboard", "~/Dashboard.aspx");
        routes.MapPageRoute("Subjects", "Subjects", "~/Subjects.aspx");
          routes.MapPageRoute("Chapters", "Chapters", "~/Chapters.aspx");
          routes.MapPageRoute("Questions", "Questions", "~/Questions.aspx");
         routes.MapPageRoute("UploadBulkData", "UploadBulkData", "~/UploadBulkData.aspx");
        routes.MapPageRoute("CreateTest", "CreateTest", "~/CreateTest.aspx");
         routes.MapPageRoute("AssignTests", "AssignTests", "~/AssignTests.aspx");
           routes.MapPageRoute("TestDetails", "TestDetails", "~/TestDetails.aspx");
         routes.MapPageRoute("ExploreChapters", "ExploreChapters", "~/ExploreChapters.aspx");
           routes.MapPageRoute("MyProfile", "MyProfile", "~/MyProfile.aspx");
          routes.MapPageRoute("Lock", "Lock", "~/LockScreen.aspx");
         routes.MapPageRoute("MyScoreCard", "MyScoreCard", "~/MyScoreCard.aspx");
         routes.MapPageRoute("OnlinePayment", "OnlinePayment", "~/OnlinePayment.aspx");
           routes.MapPageRoute("LatestNews", "LatestNews", "~/LatestNews.aspx");
           routes.MapPageRoute("UserActivation", "UserActivation", "~/UserActivation.aspx");
        

    
   
   
        //routes.MapPageRoute("CustomerDetails", "Customers/{CustomerId}", "~/CustomerDetails.aspx");
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
 

    }

    void Application_Error(object sender, EventArgs e)
    {
        Exception err = Server.GetLastError();
        Exception objerr = Server.GetLastError().GetBaseException();
        // Code that runs when an unhandled error occurs

    }
    protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Headers.Remove("X-Powered-By");
        HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
        HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
        HttpContext.Current.Response.Headers.Remove("Server");
    }
    
     protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //Check If it is a new session or not , if not then do the further checks
            if (Request.Cookies["ASP.NET_SessionId"] != null && Request.Cookies["ASP.NET_SessionId"].Value != null)
            {
                string newSessionID = Request.Cookies["ASP.NET_SessionID"].Value;
                //Check the valid length of your Generated Session ID
                //if (newSessionID.Length <= 24)
                //{
                //    //Log the attack details here
                //    Response.Cookies["TriedTohack"].Value = "True";
                //    throw new HttpException("Invalid Request");
                //}

                //Genrate Hash key for this User,Browser and machine and match with the Entered NewSessionID
                //if (GenerateHashKey() != newSessionID.Substring(24))
                //{
                //    //Log the attack details here
                //    Response.Cookies["TriedTohack"].Value = "True";
                //    throw new HttpException("Invalid Request");
                //}

                //Use the default one so application will work as usual//ASP.NET_SessionId
                Request.Cookies["ASP.NET_SessionId"].Value = Request.Cookies["ASP.NET_SessionId"].Value.Substring(0, 24);
            }

        }
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            //Pass the custom Session ID to the browser.
            if (Response.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = Request.Cookies["ASP.NET_SessionId"].Value + GenerateHashKey();
            }

        }
        private string GenerateHashKey()
        {
            StringBuilder myStr = new StringBuilder();
            myStr.Append(Request.Browser.Browser);
            myStr.Append(Request.Browser.Platform);
            myStr.Append(Request.Browser.MajorVersion);
            myStr.Append(Request.Browser.MinorVersion);
            //myStr.Append(Request.LogonUserIdentity.User.Value);
        System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] hashdata = sha.ComputeHash(Encoding.UTF8.GetBytes(myStr.ToString()));
            return Convert.ToBase64String(hashdata);
        }



    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
         
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
