using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using Excel;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;

public partial class UploadBulkData : System.Web.UI.Page
{
    DAL dal = new DAL();
    DataSet obDs = new DataSet();
    Regex tagRegex = new Regex(@"<[^>]+>");
    string fileLocation = System.Configuration.ConfigurationManager.AppSettings["FileUploadFolder"].ToString();
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
              string id=  ddlssubjectss.SelectedValue.ToString();
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
        catch(Exception ex)
        {

        }
       

    }
    protected void btnUploadExcel_Click(object sender, EventArgs e)
    {
        
        int RecordCount = 0;
        int RecordInserted = 0;
       

        string Chapterid = string.Empty;
        StringBuilder sb = new StringBuilder();
        try
        {
            if (ddlssubjectss.SelectedIndex > 0)
            {
                

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Select Subject');", true);

                return;
            }
            if (ddlchapters.SelectedIndex > 0)
            {
                Chapterid = ddlchapters.SelectedValue.ToString();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Select Chapter');", true);

                return;
            }

            DataTable dtExcelRecords = new DataTable();
            if ((exceluploader.PostedFile != null) && (exceluploader.PostedFile.ContentLength > 0))
            {
                try
                {
 
                  string filename = "FileForProcess-" + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + ".xls";
                    string fileExtension = System.IO.Path.GetExtension(exceluploader.PostedFile.FileName);
                    //fileLocation += filename.Replace("\\\\", "\\");
                    fileLocation = Server.MapPath("~/UploadedExcel/"+filename);

                    if (fileExtension != ".xls" && fileExtension != ".xlsx")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Please UpLoad Excel File. ');", true);
                        //llbMSGError.Text = "";
                        //llbMSGError.Text = "Please UpLoad Excel File. ";
                        return;
                    }

                    IExcelDataReader excelReader;
                    dtExcelRecords = new DataTable();
                    DataSet result = new DataSet();
                   // exceluploader.PostedFile.SaveAs(fileLocation);
                    exceluploader.PostedFile.SaveAs(fileLocation);

                    FileStream stream = File.Open(fileLocation, FileMode.Open, FileAccess.Read);
                    if (fileExtension == ".xls")
                    {
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                        excelReader.IsFirstRowAsColumnNames = true;
                        result = excelReader.AsDataSet();
                        excelReader.Close();
                    }
                    else if (fileExtension == ".xlsx")
                    {
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        excelReader.IsFirstRowAsColumnNames = true;
                        result = excelReader.AsDataSet();
                        excelReader.Close();
                    }

                  


                    if (result.Tables[0].Rows.Count > 0 || result != null)
                    {
                        DataTable ExcelSheet = result.Tables[0];


                        //start checking excel column name
                        #region
                        for (int i = 0; i < ExcelSheet.Rows.Count; i++)
                        {
                            //   // string Q = ExcelSheet.Rows[i]["Q"].ToString().Trim();
                            //    if (string.IsNullOrWhiteSpace(ExcelSheet.Rows[i]["Q"].ToString().Trim()) || (ExcelSheet.Rows[i]["Q"].ToString().Trim() != HttpUtility.HtmlEncode(ExcelSheet.Rows[i]["Q"].ToString().Trim())))
                            //    {
                            //        //llbMSGError.Text = "Excel Sheet : Has <b>HSRPOrder No</b> Field Empty At Row : " + i;
                            //        return;
                            //    }
                            //    if (string.IsNullOrWhiteSpace(ExcelSheet.Rows[i]["A"].ToString().Trim()) || (ExcelSheet.Rows[i]["A"].ToString().Trim() != HttpUtility.HtmlEncode(ExcelSheet.Rows[i]["A"].ToString().Trim())))
                            //    {
                            //        //llbMSGError.Text = "Excel Sheet : Has <b>HSRPOrder Date</b> Field Empty At Row : " + i;
                            //        return;
                            //    }

                            //    if (string.IsNullOrWhiteSpace(ExcelSheet.Rows[i]["B"].ToString().Trim()) || (ExcelSheet.Rows[i]["B"].ToString().Trim() != HttpUtility.HtmlEncode(ExcelSheet.Rows[i]["B"].ToString().Trim())))
                            //    {
                            //       // llbMSGError.Text = "Excel Sheet : Has <b>OEMDealer Code</b> Field Empty At Row : " + i;
                            //        return;
                            //    }

                            //    if (string.IsNullOrWhiteSpace(ExcelSheet.Rows[i]["C"].ToString().Trim()) || (ExcelSheet.Rows[i]["C"].ToString().Trim() != HttpUtility.HtmlEncode(ExcelSheet.Rows[i]["C"].ToString().Trim())))
                            //    {
                            //        //llbMSGError.Text = "Excel Sheet : Has <b>Dealer Ship To Code</b> Field Empty At Row : " + i;
                            //        return;
                            //    }

                            //    if (string.IsNullOrWhiteSpace(ExcelSheet.Rows[i]["D"].ToString().Trim()) || (ExcelSheet.Rows[i]["D"].ToString().Trim() != HttpUtility.HtmlEncode(ExcelSheet.Rows[i]["D"].ToString().Trim())))
                            //    {
                            //        //llbMSGError.Text = "Excel Sheet : Has <b>Vehicle Reg No</b> Field Empty At Row : " + i;
                            //        return;
                            //    }
                            //    if (string.IsNullOrWhiteSpace(ExcelSheet.Rows[i]["Correct"].ToString().Trim()) || (ExcelSheet.Rows[i]["Correct"].ToString().Trim() != HttpUtility.HtmlEncode(ExcelSheet.Rows[i]["Correct"].ToString().Trim())))
                            //    {
                            //       // llbMSGError.Text = "Excel Sheet : Has <b>Vehicle Reg Date</b> Field Empty At Row : " + i;
                            //        return;
                            //    }
                            //    if (string.IsNullOrWhiteSpace(ExcelSheet.Rows[i]["qtype"].ToString().Trim()) || (ExcelSheet.Rows[i]["qtype"].ToString().Trim() != HttpUtility.HtmlEncode(ExcelSheet.Rows[i]["qtype"].ToString().Trim())))
                            //    {
                            //        // llbMSGError.Text = "Excel Sheet : Has <b>Vehicle Reg Date</b> Field Empty At Row : " + i;
                            //        return;
                            //    }

                        }
                        #endregion
                        //end checking column name

                        //start inserting records into database
                        #region

                        foreach (DataRow dr in ExcelSheet.Rows)
                        {
                            RecordCount = RecordCount + 1;

                                     
                            string Q = dr["Q"].ToString().Trim();
                            //if (Q.Contains('\''))
                            //{
                            //    Q.Replace("/'", "/''");
                            //}
                            string A = dr["A"].ToString().Trim();
                            string B = dr["B"].ToString().Trim();
                            string C = dr["C"].ToString().Trim();
                            string D = dr["D"].ToString().Trim();
                            string correct = dr["Correct"].ToString().Trim();
                            string Qtype = dr["Qtype"].ToString().Trim();
                            ParameterCollection obParam = new ParameterCollection();
                            obParam.Add("@question", Q);
                            obParam.Add("@A", A);
                            obParam.Add("@B", B);
                            obParam.Add("@C", C);
                            obParam.Add("@D", D);
                            obParam.Add("@Correct", correct);
                            obParam.Add("@chapterid", Chapterid);
                            obParam.Add("@qtype", Qtype);  

                          bool flag=  dal.fnExecuteNonQueryByPro("insertquestandans", obParam);

                        

                            try
                            {

                                RecordInserted = RecordInserted + 1;


                            }
                            catch (Exception ev)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('" + ev.ToString() + "');", true);
                                // llbMSGSuccess.Text = "";
                            }

                        }

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SuccessMsg(' Question inserted: " + RecordInserted + "');", true);
                        #endregion
                        //end inserting records into database


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('No Data in Excel File');", true); //llbMSGError.Text = "No Data in Excel File";
                    }
                    if (File.Exists(fileLocation))
                    {
                        File.Delete(fileLocation);
                    }
                    //End validating excel sheet & saving data into Database
                }
                catch (Exception ev)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('" + ev.ToString() + "');", true);
                    //llbMSGError.Text = ev.Message;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('Upload Valid File');", true);
                // llbMSGError.Text = "Upload Valid File.";
            }
        }
        catch (Exception ev)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ErrorShow('" + ev.ToString() + "');", true);
            // llbMSGError.Text = ev.Message;
        }

    }


}