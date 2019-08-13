using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AssignTests : System.Web.UI.Page
{
    DAL dal = new DAL();
    DataSet obDs = new DataSet();
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
            BindSubjects();
           // bindtableTest("0");
        }
    }
    public void BindSubjects()
    {

        try
        {

            ParameterCollection obParam = new ParameterCollection();
            obParam.Add("@name", "subject");
            obParam.Add("@id", "0");

            obDs = dal.fnRetriveByPro("BindDropdown", obParam);
            DataTable dt = obDs.Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {

                    ddlssubjectss.DataTextField = "subname";
                    ddlssubjectss.DataValueField = "subid";

                    ddlssubjectss.DataSource = dt;
                    ddlssubjectss.DataBind();
                    ddlssubjectss.Items.Insert(0, "--Select Subject--");
                }
                else
                {
                    ddlssubjectss.DataSource = dt;
                    ddlssubjectss.DataBind();
                    ddlssubjectss.Items.Insert(0, "--Select Subject--");
                }



            }
        }
        catch (Exception ex)
        {

        }

    }
    public void bindtableTest(string testid)
    {

        try
        {
            divtable.InnerHtml = "";
            string query = string.Empty;
            StringBuilder htmlTable = new StringBuilder();
            if (testid == "" || testid == "0")
            {
                query = "";
            }
            else
            {
                query = "select t.testid,q.qid, t.testname,s.servicename,ch.chaptername,q.questiondescription from test t join Servicetype s on t.testtype=s.serviceid join testinfo ti on ti.testid =t.testid join chapters ch on ti.chapterid=ch.chapterid join questions q on q.Qid=ti.qid where t.testid='" + testid+ "' order by ch.chaptername,q.questiondescription";

            }

            DataSet ds = dal.fnRetriveByQuery(query);

            htmlTable.Append("<table class='table table-striped table-bordered zero-configuration'>");
            htmlTable.Append("<thead> <tr > <th scope='col'> SNo.</th> <th scope='col'> Test </th> <th scope='col'> Chapter </th>  <th scope='col'> Question </th> <th scope='col'> Type </th>  <th scope='col'> Action </th> </tr> </thead>");

            if (!object.Equals(ds.Tables[0], null))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    htmlTable.Append(" <tbody >");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        htmlTable.Append("<tr>");
                        htmlTable.Append("<td>" + (i + 1).ToString() + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["testname"].ToString() + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["chaptername"].ToString() + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["questiondescription"].ToString() + "</td>");
                        if (ds.Tables[0].Rows[i]["servicename"].ToString().ToUpper() == "FREE")
                            htmlTable.Append("<td> <span class='label gradient-1 rounded'>" + ds.Tables[0].Rows[i]["servicename"] + "</span> </td>");
                        else
                            htmlTable.Append("<td> <span class='label gradient-2 rounded'>" + ds.Tables[0].Rows[i]["servicename"] + "</span> </td>");
                        htmlTable.Append("<td> <span> <a href = 'EditTestAssign.aspx?testid=" + ds.Tables[0].Rows[i]["testid"].ToString() + "&qid=" + ds.Tables[0].Rows[i]["qid"].ToString() + "'  data-toggle = 'tooltip' data-placement= 'top' title = 'Edit' > <i style='color:#5353e8;font-size: 1.5em;' class='fa fa-pencil color-muted m-r-5'> </i> </a> <a href = '#' data-toggle='tooltip' data-placement='top' title='Delete'> <i style='color:#ef3636;font-size: 1.5em;' class='fa fa-close color-danger'> </i> </a> </span> </td>");

                        htmlTable.Append("</tr>");
                    }

                    htmlTable.Append("</table>");
                    divtable.InnerHtml = htmlTable.ToString();
                }
            }
        }

        catch (Exception ex)
        {

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

        }

    }

    protected void ddltesttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ddltesttype.SelectedIndex > 0)
            {
                string id = ddltesttype.SelectedValue.ToString();

                bindTest(id);


            }
            else
            {

               // bindtableTest("0");

            }

        }
        catch (Exception ex)
        {

        }

    }

    public void bindTest(string testid)
    {

        try
        {

            ParameterCollection obParam = new ParameterCollection();
            obParam.Add("@name", "testbytype");
            obParam.Add("@id", testid);

            obDs = dal.fnRetriveByPro("BindDropdown", obParam);
            DataTable dt = obDs.Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {

                    ddltest.DataTextField = "testname";
                    ddltest.DataValueField = "testid";

                    ddltest.DataSource = dt;
                    ddltest.DataBind();
                    ddltest.Items.Insert(0, "--Select Test--");
                }
                else
                {
                    ddltest.DataSource = dt;
                    ddltest.DataBind();
                    ddltest.Items.Insert(0, "--Select Test--");
                }



            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void ddltest_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ddltest.SelectedIndex > 0)
            {
                string id = ddltest.SelectedValue.ToString();

                bindtableTest(id);


            }
            else
            {

                 bindtableTest("0");

            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlssubjectss_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ddlssubjectss.SelectedIndex > 0)
            {
                string id = ddlssubjectss.SelectedValue.ToString();
                BindChapters(id);

            }
            else
            {
                ddlchapters.Items.Clear();
                ddlchapters.DataSource = null;
                ddlchapters.DataBind();
                ddlchapters.Items.Insert(0, "--Select Chapter--");


            }

        }
        catch (Exception ex)
        {

        }

    }
    public void BindChapters(string id)
    {
        try
        {
            ParameterCollection obParam = new ParameterCollection();
            obParam.Add("@name", "chapter");
            obParam.Add("@id", id);

            obDs = dal.fnRetriveByPro("BindDropdown", obParam);
            DataTable dt = obDs.Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {

                    ddlchapters.DataTextField = "chaptername";
                    ddlchapters.DataValueField = "chapterid";

                    ddlchapters.DataSource = dt;
                    ddlchapters.DataBind();
                    ddlchapters.Items.Insert(0, "--Select Chapter--");
                }
                else
                {
                    ddlchapters.DataSource = dt;
                    ddlchapters.DataBind();
                    ddlchapters.Items.Insert(0, "--Select Chapter--");
                }



            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void ddlchapters_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ddlchapters.SelectedIndex > 0 && ddltesttype.SelectedIndex>0 && ddltest.SelectedIndex>0)
            {
                string id = ddlchapters.SelectedValue.ToString();
                string qtype = ddltesttype.SelectedValue.ToString();
                string testid = ddltest.SelectedValue.ToString();
                BindTablequestionforAssign(id, qtype, testid);


            }
            else
            {
               


            }

        }
        catch (Exception ex)
        {

        }

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string testid = string.Empty;
            if (ddltest.SelectedIndex > 0)
            {
                testid = ddltest.SelectedValue.ToString();
            }
            else
            {
                return;
            }

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("chapterid", typeof(string)),
                        new DataColumn("qid", typeof(string)),
                        //new DataColumn("questiondescription",typeof(string)),
            new DataColumn("marks",typeof(string)),
            new DataColumn("duration",typeof(string))});
            foreach (GridViewRow row in GridView1.Rows)
            {
                if ((row.FindControl("CheckBox1") as CheckBox).Checked)
                {
                    string chapterid = row.Cells[2].Text;
                    string qid = row.Cells[3].Text;
                    //string questiondescription = row.Cells[5].Text;
                    string marks = string.Empty;
                    string duration = string.Empty;
                   
                    if ((row.FindControl("txtmarks") as TextBox).Text.Trim() != "")
                    {
                        marks = (row.FindControl("txtmarks") as TextBox).Text.Trim();
                    }
                    if ((row.FindControl("txttime") as TextBox).Text.Trim() != "")
                    {
                        duration = (row.FindControl("txttime") as TextBox).Text.Trim();
                    }
                   dt.Rows.Add(chapterid, qid,marks,duration);
                }
            }
            if (dt.Rows.Count > 0)
            {
              DataTable dtresult=  AssignAuestiontoTest(dt, testid);
                if (dtresult.Rows.Count > 0)
                {
                    bindtableTest(testid);
                }
                else
                {
                    bindtableTest(testid);
                }


            }

        }
        catch(Exception ex)
        {

        }

    }


    public DataTable AssignAuestiontoTest(DataTable dt,string testid)
    {
        DataTable dtresult = new DataTable();
        
        try
        {
            int count = dt.Rows.Count;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ParameterCollection obParam = new ParameterCollection();

                obParam.Add("@testid", testid);
                obParam.Add("@chapterid", dt.Rows[i]["chapterid"].ToString());
                obParam.Add("@qid", dt.Rows[i]["qid"].ToString());
                obParam.Add("@marks", dt.Rows[i]["marks"].ToString());
                obParam.Add("@time", dt.Rows[i]["duration"].ToString());

                obDs = dal.fnRetriveByPro("AssignAuestionsToTest", obParam);
               
                    if (obDs.Tables.Count > 0)
                    {
                    if(obDs.Tables.Count==1)
                        dtresult.Merge(obDs.Tables[0]);
                    DataTable error = obDs.Tables[1];
                    }
                string id = ddlchapters.SelectedValue.ToString();
                string qtype = ddltesttype.SelectedValue.ToString();
                string testId = ddltest.SelectedValue.ToString();
                BindTablequestionforAssign(id, qtype, testId);


            }
            return dtresult;

        }
        catch(Exception ex)
        {
            return dtresult;

        }
      
    }

    public void BindTablequestionforAssign(string chapterid ,string qtype, string testid)
    {
        ParameterCollection obParam = new ParameterCollection();
        obParam.Add("@chapterid", chapterid);
        obParam.Add("@qtype", qtype);
        obParam.Add("@testid", testid);

        obDs = dal.fnRetriveByPro("Getquestions", obParam);

        GridView1.DataSource = obDs.Tables[0];
        GridView1.DataBind();

    }
}