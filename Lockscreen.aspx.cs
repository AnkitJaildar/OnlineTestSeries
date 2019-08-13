using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lockscreen : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                string username = Session["username"].ToString().ToUpper();
                lblusername.Text = username;
                Session["UseridforLocked"] = Session["userid"];
                Session["userid"] = null;

            }
            else
            {
                Response.Redirect("Login");
            }

        }
        else
        {
            if (Session["UseridforLocked"] != null)
            {

            }
            else
            {
                Response.Redirect("Login");

            }

        }
        


    }

    protected void btnloginagain_Click(object sender, EventArgs e)
    {
        try
        {

       
        lblerror.Visible = false;
        lblerror.Text = "";
        string password = Page.Request.Form["txtpassword"].ToString();


       
            if (Session["UseridforLocked"] != null && Session["salt"]!=null && Session["userpassword"]!=null)
            {
            if (Session["userpassword"].ToString() == CreateMD5(password+ Session["salt"].ToString()))
            {
                Session["userid"] = Session["UseridforLocked"];

                Session["UseridforLocked"] = null;
                Response.Redirect("Dashboard");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Incorrect Password');", true);
                lblerror.Visible = true;
                lblerror.Text = "Incorrect Password";

                return;
                
            }
           
        }
        else
        {
            Response.Redirect("Login.aspx");

           
        }
        }
        catch(Exception ex)
        {

        }
    }

    public static string CreateMD5(string input)
    {
        // Use input string to calculate MD5 hash
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}