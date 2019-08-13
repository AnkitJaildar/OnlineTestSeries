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

public partial class Subjects : System.Web.UI.Page
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
            bindtablesubjects();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtsubject.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Provide Subject');", true);
                return;
            }
            if (txtdescription.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Provide Some Description');", true);
                return;
            }
            string subjectprice = txtprice.Text;
            if (txtprice.Text == "")
            {
                subjectprice = "0";
            }
            
            string subject = txtsubject.Text;
            string Description = txtdescription.Text;
            string userid = Session["userid"].ToString();
            ParameterCollection obParam = new ParameterCollection();
            obParam.Add("@subject", subject);
            obParam.Add("@description", Description);  
            obParam.Add("@userid", userid);
            obParam.Add("@price", subjectprice);


            Boolean result = dal.fnExecuteNonQueryByPro("subjectinsert", obParam);
            if (result)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg('Successfully Added');", true);
                txtdescription.Text = "";
                txtsubject.Text = "";
                bindtablesubjects();
            }
      

        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('"+ ex.Message + "');", true);
        }
        
    }

    public void bindtablesubjects()
    {

        try
        {
            divtable.InnerHtml="";
            StringBuilder htmlTable = new StringBuilder();
            DataSet ds = dal.fnRetriveByQuery("select subid,subname,subdescription,price from subjects");

            htmlTable.Append("<table id='table' class='table table-striped table-bordered zero-configuration'>");
            htmlTable.Append("<thead> <tr > <th scope='col'> SNo.</th> <th scope='col'> Subject </th> <th scope='col'> Description </th> <th scope='col'> Price </th> <th scope='col'> Action No </th> </tr> </thead>");

            if (!object.Equals(ds.Tables[0], null))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    htmlTable.Append(" <tbody >");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        htmlTable.Append("<tr>");
                        htmlTable.Append("<td>" + (i+1).ToString() + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["subname"] + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["subdescription"] + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["price"] + "</td>");
                        //htmlTable.Append("<td> <span> <a href = '#' data-toggle='modal' data-target='#exampleModal' data-toggle = 'tooltip' data-placement= 'top' title = 'Edit' > <i style='color:#5353e8;font-size: 1.5em;' class='fa fa-pencil color-muted m-r-5'> </i> </a> <a href = '#' data-toggle='tooltip' data-placement='top' title='Delete'> <i style='color:#ef3636;font-size: 1.5em;' class='fa fa-close color-danger'> </i> </a> </span> </td>");
                        htmlTable.Append("<td> <span> <a href = 'EditSubject.aspx?subid=" + HttpUtility.UrlEncode(Encrypt(ds.Tables[0].Rows[i]["subid"].ToString())) + "'  data-toggle = 'tooltip' data-placement= 'top' title = 'Edit' > <i style='color:#5353e8;font-size: 1.5em;' class='fa fa-pencil color-muted m-r-5'> </i> </a> <a href = 'EditSubject.aspx?Dsubid=" +HttpUtility.UrlEncode(Encrypt( ds.Tables[0].Rows[i]["subid"].ToString())) + "' data-toggle='tooltip' data-placement='top' title='Delete'> <i style='color:#ef3636;font-size: 1.5em;' class='fa fa-close color-danger'> </i> </a> </span> </td>");

                        htmlTable.Append("</tr>");
                    }

                    htmlTable.Append("</table>");
                    divtable.InnerHtml=htmlTable.ToString();
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
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtsubject.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Provide Subject');", true);
                return;
            }
            if (txtdescription.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Provide Some Description');", true);
                return;
            }
            string subject = txtsubject.Text;
            string Description = txtdescription.Text;
            string userid = Session["userid"].ToString();
            ParameterCollection obParam = new ParameterCollection();
            obParam.Add("@subject", subject);
            obParam.Add("@description", Description);
            obParam.Add("@userid", userid);


            Boolean result = dal.fnExecuteNonQueryByPro("subjectinsert", obParam);
            if (result)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg('Successfully Added');", true);
                txtdescription.Text = "";
                txtsubject.Text = "";
                bindtablesubjects();
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('" + ex.Message + "');", true);
        }

    }

    public string EncryptQueryString(string inputText, string key, string salt)
    {
        byte[] plainText = Encoding.UTF8.GetBytes(inputText);

        using (RijndaelManaged rijndaelCipher = new RijndaelManaged())
        {
            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(Encoding.ASCII.GetBytes(key), Encoding.ASCII.GetBytes(salt));
            using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        cryptoStream.FlushFinalBlock();
                        string base64 = Convert.ToBase64String(memoryStream.ToArray());

                        // Generate a string that won't get screwed up when passed as a query string.
                        string urlEncoded = HttpUtility.UrlEncode(base64);
                        return urlEncoded;
                    }
                }
            }
        }
    }

    public string DecryptQueryString(string inputText, string key, string salt)
    {
        byte[] encryptedData = Convert.FromBase64String(inputText);
        PasswordDeriveBytes secretKey = new PasswordDeriveBytes(Encoding.ASCII.GetBytes(key), Encoding.ASCII.GetBytes(salt));

        using (RijndaelManaged rijndaelCipher = new RijndaelManaged())
        {
            using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] plainText = new byte[encryptedData.Length];
                        cryptoStream.Read(plainText, 0, plainText.Length);
                        string utf8 = Encoding.UTF8.GetString(plainText);
                        return utf8;
                    }
                }
            }
        }
    }

}