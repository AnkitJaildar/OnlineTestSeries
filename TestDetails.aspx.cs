using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;


public partial class TestDetails : System.Web.UI.Page
{
    DAL dal = new DAL();
    DataSet obDs = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["userid"] != null)
        {
            if(Session["TestDetails"] != null)
            {
                DataTable dtanswerskeys = new DataTable();
                //Add Columns to datatable
                dtanswerskeys.Columns.Add("QuestionNo", typeof(Int32));
                dtanswerskeys.Columns.Add("Attempted", typeof(string));
                dtanswerskeys.Columns.Add("Correct", typeof(string));

                divtable.InnerHtml = "";
                StringBuilder htmlTable = new StringBuilder();
                DataTable dt = (DataTable)Session["TestDetails"];
                if (dt.Rows.Count > 0)
                {

                    htmlTable.Append("");
                    htmlTable.Append("<table class='table table-xs mb-0' > ");
                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td><b>Question No.</b></td> <td> <b>Attempted Option</b></td> <td><b>Correct Option</b></td>");
                    htmlTable.Append("</tr>");
                    int TotalQuestions = dt.Rows.Count;
                    int CorrectAnsCount = 0;
                    int AttemptedQuestionsCount = 0;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string QuestionNo = string.Empty;
                        string Attemptedoption = string.Empty;
                        string CorrectOption = string.Empty;

                        QuestionNo = dt.Rows[i]["Row_Number"].ToString();
                        ParameterCollection obParam = new ParameterCollection();
                        obParam.Add("@qid",dt.Rows[i]["qid"].ToString());
                       
                        obDs = dal.fnRetriveByPro("GetAnwersByQidforScore", obParam);
                        if (obDs.Tables.Count > 0)
                        {
                            if (obDs.Tables[0].Rows.Count > 0)
                            {
                                for(int j=0;j< obDs.Tables[0].Rows.Count; j++)
                                {

                             
                                if(Convert.ToInt32(obDs.Tables[0].Rows[j]["ansid"])== Convert.ToInt32(dt.Rows[i]["CorrectAnsid"]))
                                {
                                    CorrectOption = obDs.Tables[0].Rows[j]["ansdescription"].ToString();
                                }
                                    if (Convert.ToInt32(dt.Rows[i]["Ansid"]) != 0)
                                    {
                                        if (Convert.ToInt32(obDs.Tables[0].Rows[j]["ansid"]) == Convert.ToInt32(dt.Rows[i]["Ansid"]))
                                        {
                                            Attemptedoption = obDs.Tables[0].Rows[j]["ansdescription"].ToString();
                                        }

                                    }else
                                    {
                                        Attemptedoption = "Not Attempted";
                                    }
                                
                                }
                            }
                        }

                        if (dt.Rows[i]["Ansid"].ToString() == dt.Rows[i]["CorrectAnsid"].ToString())
                        {
                            CorrectAnsCount = CorrectAnsCount + 1;
                        }
                        if (Convert.ToBoolean(dt.Rows[i]["IsAttempted"]))
                        {
                            AttemptedQuestionsCount = AttemptedQuestionsCount + 1;
                        }
                        dtanswerskeys.Rows.Add(Convert.ToInt32(QuestionNo), Attemptedoption, CorrectOption);

                         htmlTable.Append("<tr>");
                        htmlTable.Append("<td>"+QuestionNo+"</td> <td> "+Attemptedoption+" </td> <td>  "+CorrectOption+" </td>");
                        htmlTable.Append("</tr>");
                    }
                    Session["dtanwerkeys"] = dtanswerskeys;
                    dal.fnExecuteNonQuery("insert into MyScoreCard ([Userid],[Testid],[TotalQuestions],[TotalAttempted],[TotalCorrected]) values ('" + dt.Rows[0]["Userid"].ToString() + "','"+ dt.Rows[0]["Testid"].ToString() + "','"+ TotalQuestions.ToString()+ "','"+ AttemptedQuestionsCount.ToString()+ "','"+ CorrectAnsCount.ToString()+ "')");
                    //lblrightquestions.Text = "Total Questions: " + TotalQuestions + "  Attempted : " + AttemptedQuestionsCount + "  Correct Questions :" + CorrectAnsCount;
                    htmlTable.Append("</table>");
                    divtable.InnerHtml = htmlTable.ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ResultPopup();", true);

                }
                

                Session["TestDetails"] = null;
            }
          string userid = Session["userid"].ToString().ToUpper();
           string username = Session["username"].ToString().ToUpper();
            if (!IsPostBack)
            {
                BindAllTestByUser(userid);
            }
        }
        else
        {
            Response.Redirect("Login");
        }

    }
    public void BindAllTestByUser(string userid)
    {
        ParameterCollection obParam = new ParameterCollection();
        obParam.Add("@userid", userid);
        //obDs = dal.fnRetriveByPro("BindAlltestByuser", obParam); // old 
        obDs = dal.fnRetriveByPro("BindAlltest", obParam);  //new 
        if (obDs.Tables.Count > 0)
        {

        
        DataTable dt = obDs.Tables[0];

        if (dt.Rows.Count > 0)
        {
            Datalist1.DataSource = dt;
            //Datalist1.DataSource = Get(userid);
            Datalist1.DataBind();
        }
        }

    }

    

    public List<TestInfo> Get(string userid)
    {
        List<TestInfo> tests = new List<TestInfo>();
        using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandText = "execute BindAlltestByuser '"+ userid + "'";
            sqlcmd.Connection = sqlcon;
            sqlcon.Open();
            SqlDataReader sqlrd = sqlcmd.ExecuteReader();
            while (sqlrd.Read())
            {
                tests.Add(new TestInfo() { testid = sqlrd["testid"].ToString(), testname = sqlrd["testname"].ToString(), testdescription = sqlrd["testdescription"].ToString(), testtotalquestions  = sqlrd["totalquestions"].ToString(), testduration =sqlrd["testduration"].ToString(), testmarks= sqlrd["totalmarks"].ToString() });
            }
            sqlcon.Close();
        }
        return tests;
    }

    protected void Datalist1_EditCommand(object source, DataListCommandEventArgs e)
    {
        int index = e.Item.ItemIndex;
        string testid = ((Label )Datalist1.Items[index].FindControl("lbltestid")).Text;
        string testtext = ((Button)Datalist1.Items[index].FindControl("btntest")).Text;

        if(testtext.ToUpper().Trim()== "PAID")
        {
            Response.Redirect("OnlinePayment");
        }else
        {
            Server.Transfer("StartTest.aspx?tid=" + HttpUtility.UrlEncode(Encrypt(testid)) + "");
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

    public void EpsionPrint(DataTable dt)
    {


        if (dt.Rows.Count > 0)
        {


            try
            {
                Document pdfDoc = new Document(PageSize.A4, 25, 10, 25, 10);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                Paragraph Text = new Paragraph("                                                           Result With Anwer Keys");
                Paragraph Text2 = new Paragraph("                                ");
                Paragraph Text3 = new Paragraph("                                ");
                pdfDoc.Add(Text);
                pdfDoc.Add(Text2);
                pdfDoc.Add(Text3);

                PdfPTable table = new PdfPTable(3);
                table.AddCell("Question No");
                table.AddCell("Attempted");
                table.AddCell("Correct");
               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
               
                    table.AddCell(dt.Rows[i]["QuestionNo"].ToString());
                    table.AddCell(dt.Rows[i]["Attempted"].ToString());
                    table.AddCell(dt.Rows[i]["Correct"].ToString());

                }
                pdfDoc.Add(table);

                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=ScoreCard.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }
            catch (Exception ex)
            { Response.Write(ex.Message); }
        }

        }

    protected void btndownloadscorecard_Click(object sender, EventArgs e)
    {try
        {
            DataTable dt = (DataTable)Session["dtanwerkeys"];
            
            EpsionPrint(dt);
        }
        catch(Exception ex)
        {

        }

    }
}