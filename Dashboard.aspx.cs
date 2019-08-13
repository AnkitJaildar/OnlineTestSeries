using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard : System.Web.UI.Page
{
    DAL dal = new DAL();
    DataSet obDs = new DataSet();
    string userid = string.Empty;
    string userrole = string.Empty;
    string usertype = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            userid = Session["userid"].ToString().ToUpper();
            getTotalSubjects();
            getTotalquestions();
            BindLatestNews();
            usertype = Session["usertype"].ToString().ToUpper();
            userrole = Session["Role"].ToString().ToUpper();
            if (userrole == "ADMIN")
            {
                divnewusers.Visible = true;
                divtotaltest.Visible = false;
            } else
            {
                divnewusers.Visible = false;
                divtotaltest.Visible = true;
                getTotaltest();

            }


        }
        else
        {
            Response.Redirect("Login");
        }

    }

    public void getTotalSubjects()
    {
        obDs = dal.fnRetriveByQuery("select count(*) as total from subjects");
        if (obDs.Tables.Count > 0)
        {
            lbltotalsubjects.Text = obDs.Tables[0].Rows[0]["total"].ToString();
        }

    }
    public void getTotaltest()
    {
        obDs = dal.fnRetriveByQuery("select count(*) as total from test where testtype='" + usertype + "'");
        if (obDs.Tables.Count > 0)
        {

            lbltotaltestbyuser.Text = obDs.Tables[0].Rows[0]["total"].ToString();
        }

    }

    public void getTotalquestions()
    {
        obDs = dal.fnRetriveByQuery("select count(*) as total from questions");
        if (obDs.Tables.Count > 0)
        {
            lbltotalquestions.Text = obDs.Tables[0].Rows[0]["total"].ToString();
        }

    }

    public void BindLatestNews()
    {
        News.InnerHtml = "";
        string query = string.Empty;
        StringBuilder htmlTable = new StringBuilder();

        query = "select news,createddate from LatestNotifications where DATEDIFF(day, cast( createddate as date), getdate())<EXPIREDAYS";



        DataSet ds = dal.fnRetriveByQuery(query);



        if (!object.Equals(ds.Tables[0], null))
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
               htmlTable.Append("<marquee  behavior='scroll' scrollamount='3' direction ='up' onmouseover='this.stop();' onmouseout = 'this.start();'>");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    htmlTable.Append("" + ds.Tables[0].Rows[i]["news"] + "");

                  
                }
                  htmlTable.Append("</marquee>");
            }

            News.InnerHtml = htmlTable.ToString();
        }
    } 
}