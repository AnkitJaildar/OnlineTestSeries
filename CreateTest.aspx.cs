using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateTest : System.Web.UI.Page
{
    DAL dal = new DAL();
    DataSet obDs = new DataSet();
    Regex tagRegex = new Regex(@"<[^>]+>");
    string userid = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            userid = Session["userid"].ToString().ToUpper();

        }
        else
        {
            Response.Redirect("Login");
        }
        if (!IsPostBack)
        {
            BindTesttype();
            bindtableTest("0");
        }
    }
    public void bindtableTest(string testtype)
    {

        try
        {
            divtable.InnerHtml = "";
            string query = string.Empty;
            StringBuilder htmlTable = new StringBuilder();
            if (testtype == "" || testtype == "0")
            {
                query = "select t.testid, t.testname,t.totalquestions,t.totalmarks,t.testduration,s.servicename from test t join Servicetype s on t.testtype=s.serviceid order by s.serviceid";
            }
            else
            {
                query = "select t.testid, t.testname,t.totalquestions,t.totalmarks,t.testduration,s.servicename from test t join Servicetype s on t.testtype=s.serviceid where s.serviceid='" + testtype+ "' order by s.serviceid ";

            }

            DataSet ds = dal.fnRetriveByQuery(query);

            htmlTable.Append("<table class='table table-striped table-bordered zero-configuration'>");
            htmlTable.Append("<thead> <tr > <th scope='col'> SNo.</th> <th scope='col'> testname </th> <th scope='col'> totalquestions </th>  <th scope='col'> totalmarks </th> <th scope='col'> testduration </th> <th scope='col'> servicename </th>  <th scope='col'> Action No </th> </tr> </thead>");

            if (!object.Equals(ds.Tables[0], null))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    htmlTable.Append(" <tbody >");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        htmlTable.Append("<tr>");
                        htmlTable.Append("<td>" + (i + 1).ToString() + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["testname"] + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["totalquestions"] + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["totalmarks"] + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["testduration"] + "</td>");
                        if(ds.Tables[0].Rows[i]["servicename"].ToString().ToUpper()=="FREE")
                        htmlTable.Append("<td> <span class='label gradient-1 rounded'>" + ds.Tables[0].Rows[i]["servicename"] + "</span> </td>");
                        else
                            htmlTable.Append("<td> <span class='label gradient-2 rounded'>" + ds.Tables[0].Rows[i]["servicename"] + "</span> </td>");
                        //htmlTable.Append("<td> <span> <a href = '#' data-toggle='modal' data-target='#exampleModal' data-toggle = 'tooltip' data-placement= 'top' title = 'Edit' > <i style='color:#5353e8;font-size: 1.5em;' class='fa fa-pencil color-muted m-r-5'> </i> </a> <a href = '#' data-toggle='tooltip' data-placement='top' title='Delete'> <i style='color:#ef3636;font-size: 1.5em;' class='fa fa-close color-danger'> </i> </a> </span> </td>");
                        htmlTable.Append("<td> <span> <a href = 'EditTest.aspx?testid=" + ds.Tables[0].Rows[i]["testid"] + "'  data-toggle = 'tooltip' data-placement= 'top' title = 'Edit' > <i style='color:#5353e8;font-size: 1.5em;' class='fa fa-pencil color-muted m-r-5'> </i> </a> <a href = '#' data-toggle='tooltip' data-placement='top' title='Delete'> <i style='color:#ef3636;font-size: 1.5em;' class='fa fa-close color-danger'> </i> </a> </span> </td>");

                        htmlTable.Append("</tr>");
                    }

                    htmlTable.Append("</table>");
                    divtable.InnerHtml = htmlTable.ToString();
                }
            }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('" + ex.Message + "');", true);
        }
    }


    public void BindTesttype()
    {

        try
        {

            ParameterCollection obParam = new ParameterCollection();
            obParam.Add("@name", "servicetype");
            obParam.Add("@id", "0");

            obDs = dal.fnRetriveByPro("BindDropdown", obParam);
            DataTable dt = obDs.Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {

                    ddltesttype.DataTextField = "Servicename";
                    ddltesttype.DataValueField = "Serviceid";

                    ddltesttype.DataSource = dt;
                    ddltesttype.DataBind();
                    ddltesttype.Items.Insert(0, "--Select Test Type--");
                }
                else
                {
                    ddltesttype.DataSource = dt;
                    ddltesttype.DataBind();
                    ddltesttype.Items.Insert(0, "--Select Test Type--");
                }



            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('" + ex.Message + "');", true);

        }

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string testname = string.Empty;
            string testype = string.Empty;
            string totalquestions = string.Empty;
            string totalmarks = string.Empty;
            string testduration = string.Empty;
            string testdescription = string.Empty;
            string userid = Session["userid"].ToString();
            
            if (ddltesttype.SelectedIndex > 0)
            {
                testype = ddltesttype.SelectedValue.ToString();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Please Select Test Type');", true);
                return;
            }
            
            if (txttestname.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Provide Test Name');", true);
                return;
            }

            testname = txttestname.Text.Trim();
            
            if (txttotalquestions.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Provide Total Questions In Test');", true);
                return;
            }

            totalquestions = txttotalquestions.Text.Trim();
            
            if (txttotalmarks.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Provide Total Marks Of Test');", true);
                return;
            }

            totalmarks = txttotalmarks.Text.Trim();
            
            if (txttestduration.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Provide Test Duration in Minutes');", true);
                return;
            }

            testduration = txttestduration.Text.Trim();
            
            if (txtdescription.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Provide Test Description ');", true);
                return;
            }

            testdescription = txtdescription.Text.Trim();


            ParameterCollection obParam = new ParameterCollection();
           
            obParam.Add("@testname", testname);
            obParam.Add("@testdescription", testdescription);
            obParam.Add("@testtype", testype);
            obParam.Add("@totalquestions", totalquestions);
            obParam.Add("@testduration", testduration);
            obParam.Add("@totalmarks", totalmarks);



            Boolean result = dal.fnExecuteNonQueryByPro("CreateTest", obParam);
            if (result)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg('Test Successfully Created');", true);
                txtdescription.Text = "";
                txttestduration.Text = "";
                txttestname.Text = "";
                txttotalmarks.Text = "";
                txttotalquestions.Text = "";
                bindtableTest(testype);
            }else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Error in creation of test');", true);
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('" + ex.Message + "');", true);
        }
    }

    protected void ddltesttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddltesttype.SelectedIndex > 0)
            {
                string id = ddltesttype.SelectedValue.ToString();

                bindtableTest(id);


            }
            else
            {

                bindtableTest("0");

            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('" + ex.Message + "');", true);
        }

    }
}