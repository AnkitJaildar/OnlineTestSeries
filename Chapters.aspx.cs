using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Security.Cryptography;

public partial class Chapters : System.Web.UI.Page
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
            BindSubjects();
            bindtableChapters("0");
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
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('" + ex.Message + "');", true);
        }

    }
   
    protected void ddlssubjectss_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlssubjectss.SelectedIndex > 0)
            {
                string id = ddlssubjectss.SelectedValue.ToString();
                bindtableChapters(id);



            }
            else
            {

                bindtableChapters("0");

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
          //  Page.Request.Form["txtdecsription"].ToString();
            string subid = string.Empty;
            string chapter = string.Empty;
            string Shortchapterdescription = string.Empty;
            string chapterdescription = string.Empty;
            string userid = Session["userid"].ToString();

            if (ddlssubjectss.SelectedIndex > 0)
            {
                subid = ddlssubjectss.SelectedValue.ToString();
              
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('First Select Subject');", true);
                return;
            }
            if (txtchapter.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Provide Chapter Name');", true);
                return;
            }
            chapter = txtchapter.Text.Trim();
            Shortchapterdescription = txtshortdescription.Text;
            chapterdescription = txtdecsription.Text.Trim();


            ParameterCollection obParam = new ParameterCollection();
            obParam.Add("@subid", subid);
            obParam.Add("@chapter", chapter);
            obParam.Add("@description", chapterdescription);
            obParam.Add("@userid", userid);
            obParam.Add("@shortdescription", Shortchapterdescription);



            Boolean result = dal.fnExecuteNonQueryByPro("chapterinsert", obParam);
            if (result)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg('Added Successfully ');", true);
                txtchapter.Text = "";
                txtdecsription.Text = "";
                txtshortdescription.Text = "";
                bindtableChapters(subid);
            }else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Something Went Wrong');", true);

            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('" + ex.Message + "');", true);
        }

    }

    public void bindtableChapters(string subid)
    {

        try
        {
            divtable.InnerHtml = "";
            string query = string.Empty;
            StringBuilder htmlTable = new StringBuilder();
            if(subid=="" || subid == "0")
            {
                query = "select sb.subid, sb.subname,ch.chapterid,ch.chaptername,ch.shortdescription from chapters ch join subjects sb on ch.subid =sb.subid order by sb.subname,ch.chaptername";
            }
            else
            {
                query = "select sb.subid, sb.subname,ch.chapterid,ch.chaptername,ch.shortdescription from chapters ch join subjects sb on ch.subid =sb.subid where sb.subid='" + subid + "' order by sb.subname,ch.chaptername ";

            }

            DataSet ds = dal.fnRetriveByQuery(query);

            htmlTable.Append("<table class='table table-striped table-bordered zero-configuration'>");
            htmlTable.Append("<thead> <tr > <th scope='col'> SNo.</th> <th scope='col'> Subject </th> <th scope='col'> Chapter </th>  <th scope='col'> Description </th>   <th scope='col'> Action No </th> </tr> </thead>");

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
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["shortdescription"] + "</td>");
                        //htmlTable.Append("<td> <span> <a href = '#' data-toggle='modal' data-target='#exampleModal' data-toggle = 'tooltip' data-placement= 'top' title = 'Edit' > <i style='color:#5353e8;font-size: 1.5em;' class='fa fa-pencil color-muted m-r-5'> </i> </a> <a href = '#' data-toggle='tooltip' data-placement='top' title='Delete'> <i style='color:#ef3636;font-size: 1.5em;' class='fa fa-close color-danger'> </i> </a> </span> </td>");
                        htmlTable.Append("<td> <span> <a href = 'EditChapter.aspx?chapterid=" + HttpUtility.UrlEncode(Encrypt(ds.Tables[0].Rows[i]["chapterid"].ToString())) + "&subid=" + HttpUtility.UrlEncode(Encrypt(ds.Tables[0].Rows[i]["subid"].ToString())) + " '  data-toggle = 'tooltip' data-placement= 'top' title = 'Edit' > <i style='color:#5353e8;font-size: 1.5em;' class='fa fa-pencil color-muted m-r-5'> </i> </a> <a href = 'EditChapter.aspx?Dchapterid=" + HttpUtility.UrlEncode(Encrypt(ds.Tables[0].Rows[i]["chapterid"].ToString())) + "&subid=" + HttpUtility.UrlEncode(Encrypt(ds.Tables[0].Rows[i]["subid"].ToString())) + " ' data-toggle='tooltip' data-placement='top' title='Delete'> <i style='color:#ef3636;font-size: 1.5em;' class='fa fa-close color-danger'> </i> </a> </span> </td>");

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
}