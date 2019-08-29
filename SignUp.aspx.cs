using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SignUp : System.Web.UI.Page
{
    DAL dal = new DAL();
    DataSet obDs = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCountry();
        }
       
    }
    protected void FullScreenMode()
    {
        StringBuilder FullScreenScript = new StringBuilder();
        //Check the name of the opened window in order to avoid of window re-open over and over again...
        FullScreenScript.Append("if(this.name != 'InFullScreen')");
        FullScreenScript.Append("{" + Environment.NewLine);
        FullScreenScript.Append("window.open(window.location.href,'InFullScreen','width=' + screen.availWidth + 'px, height=' + screen.availHeight + 'px, menubar=no,toolbar=no,status=no,scrollbars=auto');" + Environment.NewLine);
        FullScreenScript.Append("}");

        this.ClientScript.RegisterStartupScript(this.GetType(), "InFullScreen", FullScreenScript.ToString(), true);
    }
    protected void btnsignup_Click(object sender, EventArgs e)
    {
        try
        {

        lblerror.Visible = false;
        lblerror.Text = "";
            #region OTP
            if (btnsignup.Text.Trim().ToUpper() == "CONFIRM")
            {
                string otp = Page.Request.Form["txtotp"].ToString();
                if (otp == Session["signupotp"].ToString())
                {

                

                string userid=   hemail.Value;
                string name= Hname.Value;
                string Mobileno=  Hmobileno.Value;
                string Country= Hcountry.Value;
                string autority= HAuthority.Value;
                string password = Hpswrd.Value;

                string salt = CreateSalt(16);
                    string hashedpassword = CreateMD5(password + salt);

                    ParameterCollection obParam = new ParameterCollection();
                    obParam.Add("@userid", userid);
                    obParam.Add("@username", name);
                    obParam.Add("@mobileno", Mobileno);
                    obParam.Add("@salt", salt);
                    obParam.Add("@hashedpassword", hashedpassword);
                    obParam.Add("@country", Country);
                    obParam.Add("@authority", autority);
                    bool result = dal.fnExecuteNonQueryByPro("RegisterNewUser", obParam);
                    if (result)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg('" + name + "');", true);

                         divotp.Visible = false;
                        txtemail.Visible = true;
                        txtpassword.Visible = true;
                        btnsignup.Text = "Login";


                        // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Popup();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Incorrect OTP');", true);
                    lblerror.Visible = true;
                    lblerror.Text = "Incorrect OTP...";
                    return;

                }
            }
            #endregion

            #region Login
           else if (btnsignup.Text.Trim().ToUpper() == "LOGIN")
            {
                hemail.Value = "";
                Hname.Value = "";
                Hmobileno.Value = "";
                Hcountry.Value = "";
                HAuthority.Value = "";
                Hpswrd.Value = "";

                string userid = Page.Request.Form["txtemail"].ToString();
                bool isEmail = Regex.IsMatch(userid, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (!isEmail)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Invalid Email id');", true);
                    lblerror.Visible = true;
                    lblerror.Text = "Invalid Email id...";
                    return;
                }
                string password = Page.Request.Form["txtpassword"].ToString();
                if (userid.Trim() != "" && password.Trim() != "")
                {
                    ParameterCollection obParam = new ParameterCollection();
                    obParam.Add("@userid", userid);
                    obParam.Add("@pwd", password);
                    obDs = dal.fnRetriveByPro("CheckUser", obParam);
                    if (obDs.Tables[0].Rows.Count > 0)
                    {
                        string salt = obDs.Tables[0].Rows[0]["salt"].ToString();
                        string hashedpassword = obDs.Tables[0].Rows[0]["hashedpassword"].ToString();

                        if (hashedpassword == CreateMD5(password + salt))
                        {
                            Session["Userid"] = obDs.Tables[0].Rows[0]["userid"].ToString();
                            Session["UserName"] = obDs.Tables[0].Rows[0]["username"].ToString();
                            Session["salt"] = obDs.Tables[0].Rows[0]["salt"].ToString();
                            Session["userpassword"] = obDs.Tables[0].Rows[0]["hashedpassword"].ToString();
                            Session["usertype"] = obDs.Tables[0].Rows[0]["usertype"].ToString();
                            Session["Role"] = obDs.Tables[0].Rows[0]["Role"].ToString();
                            Response.Redirect("Dashboard");

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Email or password incorrect..');", true);
                            lblerror.Visible = true;
                            lblerror.Text = "Email or password incorrect..";
                            return;

                        }



                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Email or password incorrect..');", true);
                        lblerror.Visible = true;
                        lblerror.Text = "Email or password incorrect..";
                        return;
                    }

                }
                else
                {

                    // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow();", true);
                    lblerror.Visible = true;
                    lblerror.Text = "Please Provide Email or password ..";
                    return;
                }
            }
            #endregion

            #region Signup
           else if (btnsignup.Text.Trim().ToUpper() == "SIGN UP")
            {
                hemail.Value = "";
                Hname.Value = "";
                Hmobileno.Value = "";
                Hcountry.Value = "";
                HAuthority.Value = "";
                Hpswrd.Value = "";

                string userid = Page.Request.Form["txtemail"].ToString();
        bool isEmail = Regex.IsMatch(userid, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        if (!isEmail)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Invalid Email id');", true);
            lblerror.Visible = true;
            lblerror.Text = "Invalid Email id...";
            return;
        }

        string name = Page.Request.Form["txtname"].ToString();
        if (name == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Provide Name');", true);
            lblerror.Visible = true;
            lblerror.Text = "Plaease Provide Name";
            return;

        }
        string Mobileno = Page.Request.Form["txtmobileno"].ToString();
        if (Mobileno.Length != 10)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('InValid Mobile No');", true);
            lblerror.Visible = true;
            lblerror.Text = "InValid Mobile No";
            return;

        }
            string Country =string.Empty;
       if (DDLCOUNTRY.SelectedIndex > 0)
        {
         Country=  DDLCOUNTRY.SelectedItem.Text;
        }
        else
        {
           
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Select Your Country');", true);
             lblerror.Visible = true;
            lblerror.Text = "Select Your Country";
           return;
        }
            
        
        if (txtauthority.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Provide Your Autority');", true);
            lblerror.Visible = true;
            lblerror.Text = "Plaease Provide Autority";
            return;

        }
        string autority = txtauthority.Text.Trim();
        string password = Page.Request.Form["txtpassword"].ToString();
        string Confrimpassword = Page.Request.Form["txtcnfrmpassword"].ToString();
        if (password=="" || Confrimpassword == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Provide Password');", true);
            lblerror.Visible = true;
            lblerror.Text = "Provide Password";
            return;
        }
        if (password!= Confrimpassword)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Password missmatch');", true);
            lblerror.Visible = true;
            lblerror.Text = "Password missmatch";
            return;
        }

                
              obDs=  dal.fnRetriveByQuery("select * from users where userid='" + userid + "'");
               if(obDs.Tables.Count>0)
               {
                   if (obDs.Tables[0].Rows.Count > 0)
                   {
                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Email id already exists ,try with different one');", true);
                       lblerror.Visible = true;
                       lblerror.Text = "Email id already exists..";
                       return;
                   }
                   else
                   {
                       Uname.Visible = false;
                       phoneno.Visible = false;
                       country.Visible = false;
                       divAutority.Visible = false;
                       cpswrd.Visible = false;
                       txtpassword.Visible = false;
                       txtemail.Visible = false;
                       divotp.Visible = true;
                       btnsignup.Text = "Confirm";
                       hemail.Value = userid;
                       Hname.Value = name;
                       Hmobileno.Value = Mobileno;
                       Hcountry.Value = Country;
                       HAuthority.Value = autority;
                       Hpswrd.Value = password;
                       string result = SendSms(Mobileno);

                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "OTP('" + Mobileno + "');", true);

                   }
               }

              

                //  string salt = CreateSalt(16);
                //  string hashedpassword = CreateMD5(password +salt);

                //  ParameterCollection obParam = new ParameterCollection();
                //  obParam.Add("@userid", userid);
                //  obParam.Add("@username", name);
                //  obParam.Add("@mobileno", Mobileno);
                //  obParam.Add("@salt", salt);
                //  obParam.Add("@hashedpassword", hashedpassword);
                //  obParam.Add("@country", Country);
                //  obParam.Add("@authority", autority);
                //bool result=  dal.fnExecuteNonQueryByPro("RegisterNewUser", obParam);
                //  if (result)
                //  {
                //      ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg('"+ name + "');", true);

                //      Uname.Visible = false;
                //      phoneno.Visible = false;
                //      country.Visible = false;
                //      divAutority.Visible = false;
                //      cpswrd.Visible = false;
                //      btnsignup.Text = "Login";


                //      // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Popup();", true);
                //  }

            }

            #endregion

        }
        catch (Exception ex)
        {

        }


    }

    private static string getSalt()
    {
        var random = new RNGCryptoServiceProvider();

        // Maximum length of salt
        int max_length = 32;

        // Empty salt array
        byte[] salt = new byte[max_length];

        // Build the random bytes
        random.GetNonZeroBytes(salt);

        // Return the string encoded salt
        return Convert.ToBase64String(salt);
    }

    public static string CreateMD5(string input)
    {
        // Use input string to calculate MD5 hash
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
    public string CreateSalt(int size)
    {
        //Generate a cryptographic random number.
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] buff = new byte[size];
        rng.GetBytes(buff);
        return Convert.ToBase64String(buff);
    }
    public void BindCountry()
    {
        try
        {

            ParameterCollection obParam = new ParameterCollection();
            obParam.Add("@name", "Country");
            obParam.Add("@id", "0");

            obDs = dal.fnRetriveByPro("BindDropdown", obParam);
            DataTable dt = obDs.Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {

                    DDLCOUNTRY.DataTextField = "Country";
                    DDLCOUNTRY.DataValueField = "authority";

                    DDLCOUNTRY.DataSource = dt;
                    DDLCOUNTRY.DataBind();
                    DDLCOUNTRY.Items.Insert(0, "--Select Country--");
                }
                else
                {
                    DDLCOUNTRY.DataSource = dt;
                    DDLCOUNTRY.DataBind();
                    DDLCOUNTRY.Items.Insert(0, "--Select Country--");
                }



            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('" + ex.Message + "');", true);
        }
    }

    protected void DDLCOUNTRY_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLCOUNTRY.SelectedIndex > 0)
        {

            txtauthority.Attributes.Add("placeholder", DDLCOUNTRY.SelectedValue.ToString());
        }
        else
        {
            txtauthority.Attributes.Add("placeholder", "");
        }

    }



    protected void btnloginnow_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login");
    }
    public string SendSms( string number)
    {
        Session["signupotp"] = null;
        String result = "";

         int otp=  GenerateRandomNo();
        Session["signupotp"] = otp.ToString();
       string message = otp.ToString()+ " is your One Time Password for siging up in Gautamaviator.com ";

        string sendURL = "YOURAPI=0&number=" + number + "&text=" + message + "&route=2";

        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(sendURL);
        myRequest.Method = "GET";
        WebResponse myResponse = myRequest.GetResponse();
        StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
        result = sr.ReadToEnd();

        var array = result.Split();

        sr.Close();
        myResponse.Close();
        System.Threading.Thread.Sleep(350);






       // String url = "";
       //// String url = "";
       //         StreamWriter myWriter = null;


       //         HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

       //         objRequest.Method = "GET";
       //         objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
       //         objRequest.ContentType = "application/x-www-form-urlencoded";
       //         try
       //         {
       //             myWriter = new StreamWriter(objRequest.GetRequestStream());
       //             myWriter.Write(url);
       //         }
       //         catch (Exception e)
       //         {
       //             return "Error";
       //         }
       //         finally
       //         {
       //             myWriter.Close();
       //         }

       //         HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
       //         using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
       //         {
       //             result = sr.ReadToEnd();
       //             // Close and clean up the StreamReader
       //             sr.Close();
       //         }
            
        
        return result;

    }

    public int GenerateRandomNo()
    {
        int _min = 0000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }
}
