using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Questions : System.Web.UI.Page
{
    DAL dal = new DAL();
    DataSet obDs = new DataSet();

    string fileLocation = System.Configuration.ConfigurationManager.AppSettings["FileUploadFolder"].ToString();
    string userid = string.Empty;
    string user_type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            userid = Session["userid"].ToString().ToUpper();
            user_type = Session["usertype"].ToString();
        }
        else
        {
            Response.Redirect("Login");
        }
        if (!IsPostBack)
        {
            BindSubjects();
            bindtableQuestions("0", "0", user_type);
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
    protected void ddlssubjectss_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlssubjectss.SelectedIndex > 0)
            {
                user_type = Session["usertype"].ToString();
                string id = ddlssubjectss.SelectedValue.ToString();
                BindChapters(id);
                bindtableQuestions(id, "0", user_type);

            }
            else
            {
                ddlchapters.Items.Clear();
                ddlchapters.DataSource = null;
                ddlchapters.DataBind();
                ddlchapters.Items.Insert(0, "--Select Chapter--");
                bindtableQuestions("0", "0", user_type);

            }

        }
        catch (Exception ex)
        {

        }


    }

    public void bindtableQuestions(string subid,string chapterid,string usertype)
    {

        try
        {
            divtable.InnerHtml = "";
            StringBuilder htmlTable = new StringBuilder();
            ParameterCollection obParam = new ParameterCollection();
            obParam.Add("@Subjectid", subid);
            obParam.Add("@chapterid", chapterid);
            obParam.Add("@uesrtype", usertype);

            DataSet ds = dal.fnRetriveByPro("GetAllquestions", obParam);

            htmlTable.Append("<table id='table' class='table table-striped table-bordered zero-configuration'>");
            htmlTable.Append("<thead> <tr > <th scope='col'> SNo.</th> <th scope='col'> Subject </th> <th scope='col'> Chapter </th> <th scope='col'> Question </th> <th scope='col'> Type </th> <th scope='col'> Action No </th> </tr> </thead>");

            if (!object.Equals(ds.Tables[0], null))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    htmlTable.Append(" <tbody >");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                       
                        htmlTable.Append("<tr>");
                        htmlTable.Append("<td>" + (i + 1).ToString() + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["subname"] + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["chaptername"] + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["questiondescription"] + "</td>");

                        if (ds.Tables[0].Rows[i]["Servicename"].ToString().ToUpper() == "FREE")
                            htmlTable.Append("<td> <span class='fa fa-circle-o text-success  mr-2'> </span> " + ds.Tables[0].Rows[i]["Servicename"] + " </td>");
                        else
                            htmlTable.Append("<td> <span class='fa fa-circle-o text-warning  mr-2'> </span> " + ds.Tables[0].Rows[i]["Servicename"] + "</td>");


                     
                        //htmlTable.Append("<td> <span> <a href = '#' data-toggle='modal' data-target='#exampleModal' data-toggle = 'tooltip' data-placement= 'top' title = 'Edit' > <i style='color:#5353e8;font-size: 1.5em;' class='fa fa-pencil color-muted m-r-5'> </i> </a> <a href = '#' data-toggle='tooltip' data-placement='top' title='Delete'> <i style='color:#ef3636;font-size: 1.5em;' class='fa fa-close color-danger'> </i> </a> </span> </td>");
                        htmlTable.Append("<td> <span> <a href = 'EditQuestions.aspx?Qid=" + HttpUtility.UrlEncode(Encrypt(ds.Tables[0].Rows[i]["Qid"].ToString())) + "&Subid="+ HttpUtility.UrlEncode(Encrypt(ds.Tables[0].Rows[i]["Subid"].ToString()))+ "&chid=" + HttpUtility.UrlEncode(Encrypt(ds.Tables[0].Rows[i]["chapterid"].ToString())) + "'  data-toggle = 'tooltip' data-placement= 'top' title = 'Edit' > <i style='color:#5353e8;font-size: 1.5em;' class='fa fa-pencil color-muted m-r-5'> </i> </a> <a href = 'EditQuestions.aspx?DQid=" + HttpUtility.UrlEncode(Encrypt(ds.Tables[0].Rows[i]["Qid"].ToString())) + "&Subid=" + HttpUtility.UrlEncode(Encrypt(ds.Tables[0].Rows[i]["Subid"].ToString())) + "&chid=" + HttpUtility.UrlEncode(Encrypt(ds.Tables[0].Rows[i]["chapterid"].ToString())) + "' data-toggle='tooltip' data-placement='top' title='Delete'> <i style='color:#ef3636;font-size: 1.5em;' class='fa fa-close color-danger'> </i> </a> </span> </td>");

                        htmlTable.Append("</tr>");
                    }

                    htmlTable.Append("</table>");
                    divtable.InnerHtml = htmlTable.ToString();
                }
                else
                {
                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td></td>");
                    htmlTable.Append("<td></td>");
                    htmlTable.Append("<td> Not Data Found </td>");
                    htmlTable.Append("<td></td>");
                    htmlTable.Append("<td></td>");

                    htmlTable.Append("</tr>");
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

    private string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }


    protected void ddlchapters_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlchapters.SelectedIndex > 0 && ddlssubjectss.SelectedIndex>0)
            {
                string subid = ddlssubjectss.SelectedValue.ToString();
                user_type = Session["usertype"].ToString();
                string chid = ddlchapters.SelectedValue.ToString();
                
                bindtableQuestions(subid, chid, user_type);

            }
            else
            {
                string subid = ddlssubjectss.SelectedValue.ToString();
                user_type = Session["usertype"].ToString();
                bindtableQuestions(subid, "0", user_type);

            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        Server.Transfer("EditQuestions.aspx");
   
    }
}