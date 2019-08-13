using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            lblusername.Text = Session["username"].ToString().ToUpper();
            lblprofilename.Text= Session["username"].ToString().ToUpper();
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
}