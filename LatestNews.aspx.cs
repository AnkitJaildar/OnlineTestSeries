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

public partial class LatestNews : System.Web.UI.Page
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
            if (Request.QueryString["News"] != null)
            {
                string Newsid = Decrypt(HttpUtility.UrlDecode(Request.QueryString["News"]));
                bindform(Newsid);
                btnsubmit.Text = "Update";
            }
            if (Request.QueryString["DNews"] != null)
            {
                string Newsid = Decrypt(HttpUtility.UrlDecode(Request.QueryString["DNews"]));
                txtexpiredays.ReadOnly = true;
                bindform(Newsid);
                btnsubmit.Text = "Delete";
                btnsubmit.CssClass = "btn btn-danger pull-right";

            }


            bindallnews();
        }
    }
    public void bindallnews()
    {

        try
        {
            divtable.InnerHtml = "";
            string query = string.Empty;
            StringBuilder htmlTable = new StringBuilder();

            query = "select [id], [News],[createddate],(case when isnull([Expiredays],'')='' then '0' when isnull([Expiredays],'')!='' then [Expiredays] end) as Expiredays  from LatestNotifications";



            DataSet ds = dal.fnRetriveByQuery(query);

            htmlTable.Append("<table class='table table-striped table-bordered zero-configuration'>");
            htmlTable.Append("<thead> <tr > <th scope='col'> SNo.</th> <th scope='col'> News </th>   <th scope='col'> Publish Date </th>  <th scope='col'> Expire in Days </th>  <th scope='col'> Action No </th> </tr> </thead>");

            if (!object.Equals(ds.Tables[0], null))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    htmlTable.Append(" <tbody >");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        htmlTable.Append("<tr>");
                        htmlTable.Append("<td>" + (i + 1).ToString() + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["news"] + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["createddate"] + "</td>");
                        htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["Expiredays"] + " Days</td>");

                        //htmlTable.Append("<td> <span> <a href = '#' data-toggle='modal' data-target='#exampleModal' data-toggle = 'tooltip' data-placement= 'top' title = 'Edit' > <i style='color:#5353e8;font-size: 1.5em;' class='fa fa-pencil color-muted m-r-5'> </i> </a> <a href = '#' data-toggle='tooltip' data-placement='top' title='Delete'> <i style='color:#ef3636;font-size: 1.5em;' class='fa fa-close color-danger'> </i> </a> </span> </td>");
                        htmlTable.Append("<td> <span> <a href = 'LatestNews.aspx?News=" + HttpUtility.UrlEncode(Encrypt(ds.Tables[0].Rows[i]["id"].ToString())) + "'  data-toggle = 'tooltip' data-placement= 'top' title = 'Edit' > <i style='color:#5353e8;font-size: 1.5em;' class='fa fa-pencil color-muted m-r-5'> </i> </a> <a href = 'LatestNews.aspx?DNews=" + HttpUtility.UrlEncode(Encrypt(ds.Tables[0].Rows[i]["id"].ToString())) + "' data-toggle='tooltip' data-placement='top' title='Delete'> <i style='color:#ef3636;font-size: 1.5em;' class='fa fa-close color-danger'> </i> </a> </span> </td>");

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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtlatestnews.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Please Provide some News');", true);
                return;
            }

            if (txtexpiredays.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Please Provide Expire Days..');", true);
                return;
            }
            string Expiredays = txtexpiredays.Text;

            string news = txtlatestnews.Text;
            if (btnsubmit.Text.ToUpper().Trim() == "ADD NOTIFICATION")
            {


                ParameterCollection obParam = new ParameterCollection();
                obParam.Add("@news", news);
                obParam.Add("@expiredays", Expiredays);

                bool i = dal.fnExecuteNonQueryByPro("LatestNews", obParam);
                if (i)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg('Added Successfully ');", true);
                    txtlatestnews.Text = "";
                    txtexpiredays.Text = "";
                    bindallnews();
                }
            }
            if (btnsubmit.Text.ToUpper().Trim() == "UPDATE")
            {
                string Newsid = string.Empty;
                Newsid = hiddennewsid.Value;
                ParameterCollection obParam = new ParameterCollection();
                obParam.Add("@newsid", Newsid);
                obParam.Add("@news", news);
                obParam.Add("@expiredays", Expiredays);


                bool i = dal.fnExecuteNonQueryByPro("UpdateLatestNews", obParam);
                if (i)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg('Update Successfully ');", true);
                    txtlatestnews.Text = "";
                    txtexpiredays.Text = "";
                    bindallnews();
                }

            }

            if (btnsubmit.Text.ToUpper().Trim() == "DELETE")
            {
                string Newsid = string.Empty;
                Newsid= hiddennewsid.Value;
                ParameterCollection obParam = new ParameterCollection();
                obParam.Add("@newsid", Newsid);
                bool i = dal.fnExecuteNonQueryByPro("DeleteLatestNews", obParam);
                if (i)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg('Deleted Successfully ');", true);
                    txtlatestnews.Text = "";
                    txtexpiredays.Text = "";
                    bindallnews();
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

    public void bindform(string newsid)
    {
        string query = string.Empty;
        query = "select [id], [News],[createddate],(case when isnull([Expiredays],'')='' then '0' when isnull([Expiredays],'')!='' then [Expiredays] end) as Expiredays  from LatestNotifications where id='" + newsid + "'";

        DataSet ds = dal.fnRetriveByQuery(query);
        if (!object.Equals(ds.Tables[0], null))
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                hiddennewsid.Value = ds.Tables[0].Rows[0]["id"].ToString();
                txtlatestnews.Text = ds.Tables[0].Rows[0]["news"].ToString();
                txtexpiredays.Text = ds.Tables[0].Rows[0]["Expiredays"].ToString();

            }
        }
    }
}