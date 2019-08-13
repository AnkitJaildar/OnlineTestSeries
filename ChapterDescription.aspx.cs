using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChapterDescription : System.Web.UI.Page
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
            if (Request.QueryString["chapterid"] != null && Request.QueryString["subid"] != null)
            {
                string chapterid = Request.QueryString["chapterid"].ToString();
                chapterid = Decrypt(HttpUtility.UrlDecode(Request.QueryString["chapterid"]));
                string subid = Request.QueryString["subid"].ToString();
                subid = Decrypt(HttpUtility.UrlDecode(Request.QueryString["subid"]));
               
                BindSubjects(subid);
                ddlssubjectss.Enabled = false;
                BindForm(chapterid, subid);
            }

          

        }

    }
    public void BindSubjects(string subid)
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
                    ddlssubjectss.SelectedValue = subid;
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
    public void BindForm(string chapterid, string subid)
    {
        try
        {
            if (subid != "")
            {
                DataSet ds = dal.fnRetriveByQuery("select sb.subid, sb.subname,ch.chapterid,ch.chaptername,ch.shortdescription from chapters ch join subjects sb on ch.subid =sb.subid where sb.subid='" + subid + "' and ch.chapterid='" + chapterid + "' order by sb.subname,ch.chaptername ");
                if (ds.Tables.Count > 0)
                {
                    hiddenchapterid.Value = chapterid;
                    txtchapter.Text = ds.Tables[0].Rows[0]["chaptername"].ToString();
                    paragraphDescription.InnerHtml = "";
                    StringBuilder html = new StringBuilder();


                    html.Append(ds.Tables[0].Rows[0]["shortdescription"].ToString());
                  
                   
                        paragraphDescription.InnerHtml = html.ToString();
                  
                   



                    //


                }

            }

        }
        catch (Exception ex)
        {

        }
    }
    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    protected void btnreadmore_Click(object sender, EventArgs e)
    {


        string chapterid = hiddenchapterid.Value;
        DataSet ds = dal.fnRetriveByQuery("select ch.chapterid,ch.chaptername,ch.chapterdescription from chapters ch where ch.chapterid='" + chapterid + "'  ");
        if (ds.Tables.Count > 0)
        {
         
            txtchapter.Text = ds.Tables[0].Rows[0]["chaptername"].ToString();
            paragraphDescription.InnerHtml = "";
            StringBuilder html = new StringBuilder();


            html.Append(ds.Tables[0].Rows[0]["chapterdescription"].ToString());


            paragraphDescription.InnerHtml = html.ToString();
            btnreadmore.Visible = false;
        }

    }
}