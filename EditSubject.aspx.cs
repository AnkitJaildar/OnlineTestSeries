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

public partial class EditSubject : System.Web.UI.Page
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
            if (Request.QueryString["subid"] != null)
            {
                string subid = Request.QueryString["subid"].ToString();
                 subid=  Decrypt(HttpUtility.UrlDecode(Request.QueryString["subid"]));
              //  subid=   DecryptQueryString(subid, "jx!098#", "0123");
                BindForm(subid);
                
            }

            if (Request.QueryString["Dsubid"] != null)
            {
                string subid = Request.QueryString["Dsubid"].ToString();
                subid= Decrypt(HttpUtility.UrlDecode(Request.QueryString["Dsubid"]));
                btnupdate.Text = "Delete";
                btnupdate.CssClass = "btn btn-danger pull-right";
                txtsubject.ReadOnly = true;
                txtdescription.ReadOnly = true;
                txtprice.ReadOnly = true;
                BindForm(subid);

            }
           
        }
        

    }
    public void BindForm(string subid)
    {
        try
        {
            if (subid != "")
            {
                DataSet ds = dal.fnRetriveByQuery("select subid,subname,subdescription,price from subjects where subid='" + subid + "'");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hiddensubid.Value = ds.Tables[0].Rows[0]["subid"].ToString();
                        txtsubject.Text = ds.Tables[0].Rows[0]["subname"].ToString();
                        txtdescription.Text = ds.Tables[0].Rows[0]["subdescription"].ToString();
                        txtprice.Text = ds.Tables[0].Rows[0]["price"].ToString();
                    }
                   
                }

            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnupdate.Text.Trim() == "Delete")
            {
                string subid = hiddensubid.Value.ToString();
                ParameterCollection obParam = new ParameterCollection();
                obParam.Add("@subid", subid);
                Boolean result = dal.fnExecuteNonQueryByPro("Deletesubject", obParam);
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg('Deleted Successfully ');", true);
                    txtdescription.Text = "";
                    txtsubject.Text = "";
                    Response.Redirect("subjects");


                }else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Not Deleted ');", true);
                }
            }
            else
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

                string subid=hiddensubid.Value.ToString();
                string subject = txtsubject.Text;
                string Description = txtdescription.Text;
                string userid = Session["userid"].ToString();
                ParameterCollection obParam = new ParameterCollection();
                obParam.Add("@subid", subid);
                obParam.Add("@subject", subject);
                obParam.Add("@description", Description);
                obParam.Add("@userid", userid);
                obParam.Add("@price", subjectprice);



                Boolean result = dal.fnExecuteNonQueryByPro("updatesubject", obParam);
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg('Updated Successfully ');", true);
                    txtdescription.Text = "";
                    txtsubject.Text = "";
                    Response.Redirect("subjects");

          
                }

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('"+ex.Message+"');", true);
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
}