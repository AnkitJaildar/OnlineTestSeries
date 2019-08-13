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

public partial class EditQuestions : System.Web.UI.Page
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
            if (Request.QueryString["Qid"] != null && Request.QueryString["Subid"] != null && Request.QueryString["chid"] != null)
            {
                string Qid = Request.QueryString["Qid"].ToString();
                Qid = Decrypt(HttpUtility.UrlDecode(Request.QueryString["Qid"]));
                string Subid = Request.QueryString["Subid"].ToString();
                Subid = Decrypt(HttpUtility.UrlDecode(Request.QueryString["Subid"]));
                string chid = Request.QueryString["chid"].ToString();
                chid = Decrypt(HttpUtility.UrlDecode(Request.QueryString["chid"]));
                BindSubjects(Subid);
                BindQtype();
                BindChapters(Subid, chid);
                BindForm(Qid);


            }

           else if (Request.QueryString["DQid"] != null && Request.QueryString["Subid"] != null && Request.QueryString["chid"] != null)
            {
                string DQid = Request.QueryString["DQid"].ToString();
                DQid = Decrypt(HttpUtility.UrlDecode(Request.QueryString["DQid"]));
                string Subid = Request.QueryString["Subid"].ToString();
                Subid = Decrypt(HttpUtility.UrlDecode(Request.QueryString["Subid"]));
                string chid = Request.QueryString["chid"].ToString();
                chid = Decrypt(HttpUtility.UrlDecode(Request.QueryString["chid"]));
                BindSubjects(Subid);
                BindQtype();
                BindChapters(Subid, chid);
                BindForm(DQid);
                btnupdate.Text = "Delete";
                btnupdate.CssClass = "btn btn-danger pull-right";
                ddlchapters.Enabled = false;
                ddloptions.Enabled = false;
                ddlssubjectss.Enabled = false;
                ddlqtype.Enabled = false;
                txtquestion.ReadOnly = true;
                txta.ReadOnly = true;
                txtb.ReadOnly = true;
                txtc.ReadOnly = true;
                txtd.ReadOnly = true;

            }
            else
            {
                btnupdate.Text = "Add";
                btnupdate.CssClass = "btn btn-info pull-right";
                BindSubjects();

                BindQtype();
                List<string> Options = new List<string>();
                Options.Add("--select option--");
                Options.Add("A");
                Options.Add("B");
                Options.Add("C");
                Options.Add("D");
                ddloptions.DataSource = Options;
                ddloptions.DataBind();

                
              
                
            }

        }

    }

    public void BindQtype()
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

                    ddlqtype.DataTextField = "Servicename";
                    ddlqtype.DataValueField = "Serviceid";

                    ddlqtype.DataSource = dt;
                    ddlqtype.DataBind();
                    ddlqtype.Items.Insert(0, "--Select Type--");
                   
                    //ddlqtype.Items[1].Attributes.Add("style", "background-color:#5875f9;");
                    //ddlqtype.Items[2].Attributes.Add("style", "background-color:#e0638e;");
                }
                else
                {
                    ddlqtype.DataSource = dt;
                    ddlqtype.DataBind();
                    ddlqtype.Items.Insert(0, "--Select Type--");
                 
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

    public void BindChapters(string subid ,string chapterid)
    {
        try
        {
            ParameterCollection obParam = new ParameterCollection();
            obParam.Add("@name", "chapter");
            obParam.Add("@id", subid);

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

                    ddlchapters.SelectedValue = chapterid;
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
    public void BindForm(string qid)
    {
        try
        {
            string qtype = string.Empty;
            if (qid != "")
            {
                ParameterCollection obParam = new ParameterCollection();
                obParam.Add("@qid", qid);
                DataSet ds = dal.fnRetriveByPro("GetAnwersByQidforupdate", obParam);
                if (ds.Tables.Count > 0)
                {
                    hiddenqid.Value = qid;
                    txtquestion.Text = ds.Tables[0].Rows[0]["questiondescription"].ToString();
                   
                    txta.Text = ds.Tables[0].Rows[0]["ansdescription"].ToString();
                    Hiddenansid_a.Value= ds.Tables[0].Rows[0]["ansid"].ToString();

                    txtb.Text = ds.Tables[0].Rows[1]["ansdescription"].ToString();
                    Hiddenansid_b.Value = ds.Tables[0].Rows[1]["ansid"].ToString();

                    txtc.Text = ds.Tables[0].Rows[2]["ansdescription"].ToString();
                    Hiddenansid_c.Value = ds.Tables[0].Rows[2]["ansid"].ToString();

                    txtd.Text = ds.Tables[0].Rows[3]["ansdescription"].ToString();
                    Hiddenansid_d.Value = ds.Tables[0].Rows[3]["ansid"].ToString();

                    string correctansid= ds.Tables[1].Rows[0]["ansid"].ToString();
                    qtype= ds.Tables[0].Rows[0]["qtype"].ToString();
                    ddloptions.DataTextField = "ansdescription";
                    ddloptions.DataValueField = "ansid";

                    ddloptions.DataSource = ds.Tables[0];
                    ddloptions.DataBind();
                    ddloptions.Items.Insert(0, "--Correct Option--");

                    ddloptions.SelectedValue = correctansid;
                    ddlqtype.SelectedValue = qtype;
                }

            }

        }
        catch (Exception ex)
        {

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

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if(btnupdate.Text== "Add")
        {
            string subid = string.Empty;
            string chapterid = string.Empty;
            string CorrectOption = string.Empty;
            string Qtype = string.Empty;
            if (ddlssubjectss.SelectedIndex > 0)
            {
                subid = ddlssubjectss.SelectedValue.ToString();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Select Subject');", true);
                return;

            }
            if (ddlchapters.SelectedIndex > 0)
            {
                chapterid = ddlchapters.SelectedValue.ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Select Chapter');", true);
                return;

            }
            if (ddloptions.SelectedIndex > 0)
            {
                CorrectOption = ddloptions.SelectedValue.ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Select Correct Option');", true);
                return;

            }
            if (txtquestion.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Enter Question');", true);
                return;

            }
            
            if (ddlqtype.SelectedIndex > 0)
            {
                Qtype = ddlqtype.SelectedValue.ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Select Question Type');", true);
                return;

            }


            string Q = txtquestion.Text.Trim();
          
            string A = txta.Text.Trim();
            string B = txtb.Text.Trim();
            string C = txtc.Text.Trim();
            string D = txtd.Text.Trim();
            string correct = CorrectOption;
            string Q_type = Qtype.Trim();
            ParameterCollection obParam = new ParameterCollection();
            obParam.Add("@question", Q);
            obParam.Add("@A", A);
            obParam.Add("@B", B);
            obParam.Add("@C", C);
            obParam.Add("@D", D);
            obParam.Add("@Correct", correct);
            obParam.Add("@chapterid", chapterid);
            obParam.Add("@qtype", Q_type);

            bool flag = dal.fnExecuteNonQueryByPro("insertquestandans", obParam);
            if (flag)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg('Saved Successfully');", true);
                clearform();
            }else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Not Saved..');", true);
                return;
            }

        }
        else if(btnupdate.Text == "Delete")
        {

        }
        else if (btnupdate.Text == "Update")
        {
            string qid = hiddenqid.Value;
            string subid = string.Empty;
            string chapterid = string.Empty;
            string CorrectOption = string.Empty;
            string Qtype = string.Empty;
            if (ddlssubjectss.SelectedIndex > 0)
            {
                subid = ddlssubjectss.SelectedValue.ToString();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Select Subject');", true);
                return;

            }
            if (ddlchapters.SelectedIndex > 0)
            {
                chapterid = ddlchapters.SelectedValue.ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Select Chapter');", true);
                return;

            }
            if (ddloptions.SelectedIndex > 0)
            {
                CorrectOption = ddloptions.SelectedValue.ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Select Correct Option');", true);
                return;

            }
            if (txtquestion.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Enter Question');", true);
                return;

            }

            if (ddlqtype.SelectedIndex > 0)
            {
                Qtype = ddlqtype.SelectedValue.ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Select Question Type');", true);
                return;

            }


            string Q = txtquestion.Text.Trim();

            string A = txta.Text.Trim();
            string Aansid = Hiddenansid_a.Value;
            string B = txtb.Text.Trim();
            string Bansid = Hiddenansid_b.Value;
            string C = txtc.Text.Trim();
            string Cansid = Hiddenansid_c.Value;
            string D = txtd.Text.Trim();
            string Dansid = Hiddenansid_d.Value;
            string correct = CorrectOption;
            string Q_type = Qtype.Trim();
            ParameterCollection obParam = new ParameterCollection();
            
                obParam.Add("@Qid", qid);
            obParam.Add("@question", Q);
            obParam.Add("@A", A);
            obParam.Add("@Aansid", Aansid);
            obParam.Add("@B", B);
            obParam.Add("@Bansid", Bansid);
            obParam.Add("@C", C);
            obParam.Add("@Cansid", Cansid);
            obParam.Add("@D", D);
            obParam.Add("@Dansid", Dansid);
            obParam.Add("@Correct", correct);
            obParam.Add("@chapterid", chapterid);
            obParam.Add("@qtype", Q_type);

            bool flag = dal.fnExecuteNonQueryByPro("updatequestandans", obParam);
            if (flag)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg('Update Successfully');", true);
                clearform();
                Response.Redirect("Questions");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Not Updated..');", true);
                return;
            }


        }
    }

    public void clearform()
    {
        ddlssubjectss.SelectedIndex = 0;
        ddlchapters.Items.Clear();
        txtquestion.Text = "";
        txta.Text = "";
        txtb.Text = "";
        txtc.Text = "";
        txtd.Text = "";
        ddloptions.SelectedIndex = 0;
        ddlqtype.SelectedIndex = 0;
    }
}