using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    DAL dal = new DAL();
    DataSet obDs = new DataSet();
    string userid = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindNews();
        if (Session["userid"] != null)
        {


            lblusername.Text = Session["username"].ToString().ToUpper();
            if (Session["WelcomeNote"] != null)
            {
                string value = Session["WelcomeNote"].ToString().Trim();
                if (value.ToUpper() == "WELCOME")
                {

                }else
                {
                    Session["WelcomeNote"] = "WELCOME";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg('" + lblusername.Text + "');", true);
                }
            }
            else
            {
                Session["WelcomeNote"] = "WELCOME";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg('" + lblusername.Text + "');", true);

            }
                 
            lblprofilename.Text = Session["username"].ToString().ToUpper();

            if (Session["Role"] != null)
            {
                string userrole = Session["Role"].ToString();
                if (userrole != "ADMIN")
                {
                    limanagment.Visible = false;
                    menumanagetest.Visible = false;
                    MenuManagment.Visible = false;
                }
            }else
            {
                Response.Redirect("Login.aspx");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }

    protected void BindNews()
    {
        NewsUL.InnerHtml = "";
        string query = string.Empty;
        StringBuilder htmlTable = new StringBuilder();

        query = "select * from LatestNotifications";



        DataSet ds = dal.fnRetriveByQuery(query);

      

        if (!object.Equals(ds.Tables[0], null))
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblnewnotifications.Text = ds.Tables[0].Rows.Count.ToString();
                lbltotal.Text = ds.Tables[0].Rows.Count.ToString();
                labeltotalnewNotifications.Text= ds.Tables[0].Rows.Count.ToString();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    htmlTable.Append("<li><a href = 'javascript:void()'><span class='mr -3 avatar-icon bg-success-lighten-2'><i class='icon-present'></i></span><div class='notification-content'><h6 class='notification-heading'>" + ds.Tables[0].Rows[i]["news"] + "</h6>"
                                                    + "<span class='notification-text'>News Date: " + ds.Tables[0].Rows[i]["createddate"] + "</span>" +
                                                "</div></a>");

                    htmlTable.Append("</li>");
                }
            }

            NewsUL.InnerHtml = htmlTable.ToString();
        }
    }

}
