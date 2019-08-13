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

public partial class StartTest : System.Web.UI.Page
{
    DAL dal = new DAL();
    DataSet obDs = new DataSet();
    string userid = string.Empty;

    static int currentposition = 0;
    static int totalrows = 0;

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
            if (Request.QueryString["tid"] != null)
            {
                string tid = string.Empty;
                string Querytid = Request.QueryString["tid"].ToString();
                tid = Decrypt(HttpUtility.UrlDecode(Request.QueryString["tid"]));
                hiddentid.Value = tid;
                Session["TestDetails"] = null;
                gettestdetails(tid);


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

    protected void btnstarttest_Click(object sender, EventArgs e)
    {
        string duration = "3";
        string tid = string.Empty;
        tid = hiddentid.Value;
        if (tid != "")
        {

           
            DataTable dt = BindQuestionsInitialy(tid);
            if (dt.Rows.Count > 0)
            {

           
            if (dt.Rows[0]["testduration"] != null)
            {

                 duration = dt.Rows[0]["testduration"].ToString();

            }
           
            int totalquestions = dt.Rows.Count;
            DatalistQuestionNumbers.DataSource = dt;
            DatalistQuestionNumbers.DataBind();
            Button btn = DatalistQuestionNumbers.Items[0].FindControl("btn") as Button;
            Label lbl = DatalistQuestionNumbers.Items[0].FindControl("lblqidlist") as Label;
            btn.CssClass = "btn mb-1 btn-warning";
             string qid=  lbl.Text;
            UpdateSessionDatatable(qid);

        
            testArea.Visible = true;
            instructionArea.Visible = false;
            ColorIndicationdiv.Visible = true;
            TimerArea.Visible = true;

             int totalseconds = Convert.ToInt32(duration) * 60;
            TimeSpan ts = TimeSpan.FromSeconds(totalseconds - 1);
                Session["Timeleft"] = ts.ToString();
            lbltimeleft.Text = ts.ToString();
           TestTimer.Enabled = true;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", " goFullscreen('onlinetestarea');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('There is no questons in this test... ');", true);
            }
        }
    }


    protected void TestTimer_Tick(object sender, EventArgs e)
    {
        // int i = Convert.ToInt32(lbltimeleft.Text);

        TimeSpan ts = TimeSpan.Parse(lbltimeleft.Text);
        int i = Convert.ToInt32(ts.TotalSeconds);

        i = i - 1;
        if (i == 10)
        {
            lbltimewarning.Text = "Only 10 seconds Left";
        }
        if (i <= 1)
        {
            TestTimer.Enabled = false;
            TimeSpan ts2 = TimeSpan.FromSeconds(i);
            lbltimeleft.Text = ts2.ToString();
            testArea.Visible = false;
            lbltimewarning.Text = "Time Over!  Your Attempted Questions Are Automatically Saved..";
            Response.Redirect("TestDetails");
           
        }
        else
        {
            TimeSpan ts1 = TimeSpan.FromSeconds(i);
            lbltimeleft.Text = ts1.ToString();
            //lbltimeleft.Text = i.ToString();

        }

    }

    public DataTable BindQuestionsInitialy(string tid)
    {
        currentposition = 0;
        DataTable dt = new DataTable();
        string duration = string.Empty;
        if (tid != "")
        {
            ParameterCollection obParam = new ParameterCollection();

            obParam.Add("@testid", tid);

            obDs = dal.fnRetriveByPro("GetQuestionsAndAnwersBytest", obParam);
            dt = obDs.Tables[0];
            if (obDs.Tables.Count > 0)
            {
                if (obDs.Tables[0].Rows.Count > 0)
                {
                    

                    #region creating datatable
                    DataTable dtResultMaintain = new DataTable();
                    //Add Columns to datatable
                    dtResultMaintain.Columns.Add("Row_Number", typeof(Int32));
                    dtResultMaintain.Columns.Add("Userid", typeof(string));
                    dtResultMaintain.Columns.Add("Testid", typeof(string));
                    dtResultMaintain.Columns.Add("Qid", typeof(Int32));
                    dtResultMaintain.Columns.Add("Ansid", typeof(Int32));
                    dtResultMaintain.Columns.Add("IsSeen", typeof(Boolean));
                    dtResultMaintain.Columns.Add("IsAttempted", typeof(Boolean));
                    dtResultMaintain.Columns.Add("CorrectAnsid", typeof(Int32));

                    //Adding rows and data
                    for (int i=0; i < obDs.Tables[0].Rows.Count; i++)
                    {
                        
                        dal.fnExecuteNonQuery("insert into ResultMaintain ([Userid],[Testid],[Qid],[Ansid],[IsSeen],[IsAttempted],[CorrectAnsid],[TestStartDatetime],[TestDuration]) VALUES ('" + userid + "','" + tid + "','" + obDs.Tables[0].Rows[i]["qid"].ToString() + "',null,0,0,'" + obDs.Tables[0].Rows[i]["Ansid"].ToString() + "',getdate(),'"+ obDs.Tables[0].Rows[0]["testduration"].ToString() + "')");
                        dtResultMaintain.Rows.Add(Convert.ToInt32(obDs.Tables[0].Rows[i]["Row_Number"]),userid,tid, Convert.ToInt32(obDs.Tables[0].Rows[i]["qid"]),0,0,0, Convert.ToInt32(obDs.Tables[0].Rows[i]["Ansid"]));

                    }
                    #endregion

                    Session["TestDetails"] = dtResultMaintain;


                    dt = obDs.Tables[0];
                    totalrows = obDs.Tables[0].Rows.Count;
                    duration = obDs.Tables[0].Rows[0]["testduration"].ToString();
                    PagedDataSource objPds;

                    // Populate the repeater control with the DataSet at page init or pageload
                    objPds = new PagedDataSource();
                    objPds.DataSource = obDs.Tables[0].DefaultView;

                    // Indicate that the data should be paged
                    objPds.AllowPaging = true;
                    objPds.CurrentPageIndex = currentposition;
                    string qid = obDs.Tables[0].Rows[currentposition]["qid"].ToString();
                    BindAnsList(qid);
                    // Set the number of items you wish to display per page
                    objPds.PageSize = 1;

                    btnfirst.Enabled = !objPds.IsFirstPage;//1
                    btnnext.Enabled = !objPds.IsLastPage;//2
                    btnprevious.Enabled = !objPds.IsFirstPage;//3
                    btnlast.Enabled = !objPds.IsLastPage;//4
                    Datalist1.DataSource = objPds;
                    Datalist1.DataBind();

                }


            }
        }
        return dt;

    }
    public string BindQuestions(string tid)
    {
        string duration = string.Empty;
        if (tid != "")
        {
            ParameterCollection obParam = new ParameterCollection();

            obParam.Add("@testid", tid);

            obDs = dal.fnRetriveByPro("GetQuestionsAndAnwersBytest", obParam);

            if (obDs.Tables.Count > 0)
            {
                if (obDs.Tables[0].Rows.Count>0)
                { 
                totalrows = obDs.Tables[0].Rows.Count;
                duration = obDs.Tables[0].Rows[0]["testduration"].ToString();
                PagedDataSource objPds;

                // Populate the repeater control with the DataSet at page init or pageload
                objPds = new PagedDataSource();
                objPds.DataSource = obDs.Tables[0].DefaultView;

                // Indicate that the data should be paged
                objPds.AllowPaging = true;
                objPds.CurrentPageIndex = currentposition;
                string qid = obDs.Tables[0].Rows[currentposition]["qid"].ToString();
                BindAnsList(qid);
                // Set the number of items you wish to display per page
                objPds.PageSize = 1;
                
                btnfirst.Enabled = !objPds.IsFirstPage;//1
                btnnext.Enabled = !objPds.IsLastPage;//2
                btnprevious.Enabled = !objPds.IsFirstPage;//3
                btnlast.Enabled = !objPds.IsLastPage;//4
                Datalist1.DataSource = objPds;
                Datalist1.DataBind();

                }


            }
        }
        return duration;

    }
      public void BindAnsList(string qid)
    {
        rbtnanslist.DataSource = null;
        ParameterCollection obParam = new ParameterCollection();

        obParam.Add("@qid", qid);

        obDs = dal.fnRetriveByPro("GetAnwersByQid", obParam);

        if (obDs.Tables.Count > 0)
        {
            hiddenqid.Value = qid;
            rbtnanslist.DataTextField = "ansdescription";
            rbtnanslist.DataValueField = "ansid";
            rbtnanslist.DataSource = obDs.Tables[0];
            rbtnanslist.DataBind();


        }
    }


    protected void btnfirst_Click(object sender, EventArgs e)
    {
        currentposition = 0;
        int rownumber = currentposition + 1;
        int count = DatalistQuestionNumbers.Items.Count;
        for (int i = 0; i < count; i++)
        {
            Button btn = DatalistQuestionNumbers.Items[i].FindControl("btn") as Button;
            if (btn.Text == rownumber.ToString())
            {
                //Label lbl = DatalistQuestionNumbers.Items[i].FindControl("lblqidlist") as Label;

                //string qid = lbl.Text;
                //BindAnsIfAttempted(qid);
                //UpdateSessionDatatable(qid);
                //btn.CssClass = "btn mb-1 btn-warning";

                Label lbl = DatalistQuestionNumbers.Items[i].FindControl("lblqidlist") as Label;

                string qid = lbl.Text;
                bool result = BindAnsIfAttempted(qid);
                if (!result)
                {
                    UpdateSessionDatatable(qid);
                    btn.CssClass = "btn mb-1 btn-warning";
                   

                }
                // if more than one is checked, continue the loop and track all the texts. 
                // Like: string selectedItemTexts += ID.Text + ",";
                break;
            }

        }

        string tid = string.Empty;
        tid = hiddentid.Value;
        BindQuestions(tid);

    }

    protected void btnnext_Click(object sender, EventArgs e)
    {

        if (currentposition == totalrows - 1)
        {

        }
        else
        {
            currentposition = currentposition + 1;
            int rownumber = currentposition + 1;
            int count = DatalistQuestionNumbers.Items.Count;
            for (int i = 0; i < count; i++)
            {

                Button btn = DatalistQuestionNumbers.Items[i].FindControl("btn") as Button;
                if (btn.Text == rownumber.ToString())
                {
                    //Label lbl = DatalistQuestionNumbers.Items[i].FindControl("lblqidlist") as Label;
                   
                    //string qid = lbl.Text;
                    //UpdateSessionDatatable(qid);
                    //btn.CssClass = "btn mb-1 btn-warning";
                    Label lbl = DatalistQuestionNumbers.Items[i].FindControl("lblqidlist") as Label;

                    string qid = lbl.Text;
                    bool result = BindAnsIfAttempted(qid);
                    if (!result)
                    {
                        UpdateSessionDatatable(qid);
                        btn.CssClass = "btn mb-1 btn-warning";

                    }
                    break;
                }

            }
            string tid = string.Empty;
            tid = hiddentid.Value;
            BindQuestions(tid);

        }



    }

    protected void btnprevious_Click(object sender, EventArgs e)
    {
        if (currentposition == 0)
        {

        }
        else
        {
            currentposition = currentposition - 1;
            int rownumber = currentposition + 1;
            int count = DatalistQuestionNumbers.Items.Count;
            for (int i = 0; i < count; i++)
            {
                Button btn = DatalistQuestionNumbers.Items[i].FindControl("btn") as Button;
                if (btn.Text == rownumber.ToString())
                {
                    Label lbl = DatalistQuestionNumbers.Items[i].FindControl("lblqidlist") as Label;

                    string qid = lbl.Text;
                   bool result= BindAnsIfAttempted(qid);
                    if (!result)
                    {
                        UpdateSessionDatatable(qid);
                        btn.CssClass = "btn mb-1 btn-warning";

                    }
                   
                    // if more than one is checked, continue the loop and track all the texts. 
                    // Like: string selectedItemTexts += ID.Text + ",";
                    break;
                }

            }
            string tid = string.Empty;
            tid = hiddentid.Value;
            BindQuestions(tid);
        }

        
    }

    protected void btnlast_Click(object sender, EventArgs e)
    {
        currentposition = totalrows - 1;
        int rownumber = currentposition + 1;
        int count = DatalistQuestionNumbers.Items.Count;
        for (int i = 0; i < count; i++)
        {
            Button btn = DatalistQuestionNumbers.Items[i].FindControl("btn") as Button;
            if (btn.Text == rownumber.ToString())
            {
                //Label lbl = DatalistQuestionNumbers.Items[i].FindControl("lblqidlist") as Label;

                //string qid = lbl.Text;
                //UpdateSessionDatatable(qid);
                //btn.CssClass = "btn mb-1 btn-warning";


                Label lbl = DatalistQuestionNumbers.Items[i].FindControl("lblqidlist") as Label;

                string qid = lbl.Text;
                bool result = BindAnsIfAttempted(qid);
                if (!result)
                {
                    UpdateSessionDatatable(qid);
                    btn.CssClass = "btn mb-1 btn-warning";

                }
                // if more than one is checked, continue the loop and track all the texts. 
                // Like: string selectedItemTexts += ID.Text + ",";
                break;
            }

        }
        string tid = string.Empty;
        tid = hiddentid.Value;
        BindQuestions(tid);
    }

    protected void btnsubmitans_Click(object sender, EventArgs e)
    {
        if (rbtnanslist.SelectedIndex >= 0)
        {
           

            string qid = hiddenqid.Value;
            string ansid =rbtnanslist.SelectedValue.ToString();
            string ans1 = rbtnanslist.SelectedItem.Text.ToString();




            int count = DatalistQuestionNumbers.Items.Count;
            for (int i = 0; i < count; i++)
            {
                Label lbl = DatalistQuestionNumbers.Items[i].FindControl("lblqidlist") as Label;
                if (lbl.Text== qid)
                {
                    UpdateSessionDatatableAnsId(qid, ansid);
                    Button btn = DatalistQuestionNumbers.Items[i].FindControl("btn") as Button;
                    btn.CssClass = "btn mb-1 btn-info";
                    // if more than one is checked, continue the loop and track all the texts. 
                    // Like: string selectedItemTexts += ID.Text + ",";
                    break;
                }
               
            }

        }
      

    }

    protected void DatalistQuestionNumbers_EditCommand(object source, DataListCommandEventArgs e)
    {
        int index = e.Item.ItemIndex;
        string qid = ((Label)DatalistQuestionNumbers.Items[index].FindControl("lblqidlist")).Text;
        string currentIndex= ((Button)DatalistQuestionNumbers.Items[index].FindControl("btn")).Text;
       
        currentposition = Convert.ToInt32(currentIndex)-1;

        string tid = string.Empty;
        tid = hiddentid.Value;
        BindQuestions(tid);

        ((Button)DatalistQuestionNumbers.Items[index].FindControl("btn")).CssClass = "btn mb-1 btn-warning";

    }


    public void UpdateSessionDatatable(string qid)
    {
        DataTable dt = (DataTable)Session["TestDetails"];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToInt32(dt.Rows[i]["qid"]) == Convert.ToInt32(qid))
            {
                dal.fnExecuteNonQuery("WITH resultmaintain_view AS (SELECT TOP 1 * from resultmaintain WHERE qid = '"+ qid + "' ORDER BY id DESC) update resultmaintain_view set IsSeen = 1");
                dt.Rows[i]["IsSeen"] = true;
            }
        }
        Session["TestDetails"] = dt;

    }

   public void UpdateSessionDatatableAnsId(string qid,string ansid)
    {
        DataTable dt = (DataTable)Session["TestDetails"];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToInt32(dt.Rows[i]["qid"]) == Convert.ToInt32(qid))
            {
                dal.fnExecuteNonQuery("WITH resultmaintain_view AS (SELECT TOP 1 * from resultmaintain WHERE qid = '" + qid + "' ORDER BY id DESC) update resultmaintain_view set Ansid='" + ansid + "' , IsAttempted=1 ");
                dt.Rows[i]["Ansid"] = ansid;
                dt.Rows[i]["IsAttempted"] = true; 
            }
        }
        Session["TestDetails"] = dt;

    }

    public bool BindAnsIfAttempted(string qid)
    {
        bool result = false;
        DataTable dt = (DataTable)Session["TestDetails"];
        string ansid = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToInt32(dt.Rows[i]["qid"]) == Convert.ToInt32(qid))
            {
                if (Convert.ToBoolean(dt.Rows[i]["IsAttempted"]))
                {

                    result = true;
                    ansid = dt.Rows[i]["Ansid"].ToString();

                rbtnanslist.DataSource = null;
                ParameterCollection obParam = new ParameterCollection();

                obParam.Add("@qid", qid);

                obDs = dal.fnRetriveByPro("GetAnwersByQid", obParam);

                if (obDs.Tables.Count > 0)
                {
                    hiddenqid.Value = qid;
                    rbtnanslist.DataTextField = "ansdescription";
                    rbtnanslist.DataValueField = "ansid";
                    rbtnanslist.DataSource = obDs.Tables[0];
                    rbtnanslist.DataBind();

                    rbtnanslist.SelectedValue = ansid;
                 
                        result = true;


                }
                }

            }
        }

        return result;
    }

    public void gettestdetails(string tid)
    {
        try
        {
            paragraphinstruction.InnerHtml = "";
            obDs = dal.fnRetriveByQuery("select testdescription from test where testid='" + tid + "'");
            if (obDs.Tables.Count > 0)
            {
                if (obDs.Tables[0].Rows.Count > 0)
                {
                    StringBuilder html = new StringBuilder();


                    html.Append(obDs.Tables[0].Rows[0]["testdescription"].ToString());
                    paragraphinstruction.InnerHtml = html.ToString();

                }
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void btnfinish_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("TestDetails");
        }
        catch(Exception ex)
        {

        }
    }
}