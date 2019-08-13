using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    DAL dal = new DAL();
    DataSet obDs = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            Session.Abandon();

        }


    }

    protected void Login_Click(object sender, EventArgs e)
    {
        

        string userid = Page.Request.Form["txtemail"].ToString();
        string password = Page.Request.Form["txtpassword"].ToString();

        lblerror.Visible = false;
        lblerror.Text = "";
       


        if (userid.Trim()!="" && password.Trim() != "")
        {
            ParameterCollection obParam = new ParameterCollection();
            obParam.Add("@userid", userid);
            obParam.Add("@pwd", password);
            obDs = dal.fnRetriveByPro("CheckUser", obParam);
            if (obDs.Tables[0].Rows.Count > 0)
            {
                string salt = obDs.Tables[0].Rows[0]["salt"].ToString();
                string hashedpassword= obDs.Tables[0].Rows[0]["hashedpassword"].ToString();
               
                if (hashedpassword == CreateMD5(password + salt))
                {
                    Session["Userid"] = obDs.Tables[0].Rows[0]["userid"].ToString();
                    Session["UserName"] = obDs.Tables[0].Rows[0]["username"].ToString();
                    Session["salt"] = obDs.Tables[0].Rows[0]["salt"].ToString();
                    Session["userpassword"] = obDs.Tables[0].Rows[0]["hashedpassword"].ToString();
                    Session["usertype"] = obDs.Tables[0].Rows[0]["usertype"].ToString();
                    Session["Role"] = obDs.Tables[0].Rows[0]["Role"].ToString();
                    Response.Redirect("Dashboard");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow();", true);
                    lblerror.Visible = true;
                    lblerror.Text = "Email or password incorrect..";
                    return;

                }


               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow();", true);
                lblerror.Visible = true;
                lblerror.Text = "Email or password incorrect..";
                return;
            }
  
        }
        else
        {
           
           // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow();", true);
            lblerror.Visible = true;
            lblerror.Text = "Please Provide Email or password ..";
            return;
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