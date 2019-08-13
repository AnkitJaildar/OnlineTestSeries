using System;
using System.Data;
using System.Web.UI.WebControls;


public partial class MyScoreCard : System.Web.UI.Page
{
    DAL dal = new DAL();
    DataSet obDs = new DataSet();
    string userid = string.Empty;
    string userrole = string.Empty;
    string usertype = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            userid = Session["userid"].ToString().ToUpper();
            ShowPerformance();
        }
        else
        {
            Response.Redirect("Login");
        }
      

    }
    public void ShowPerformance()
    {
        try
        {
            string testid = string.Empty;
            int TotalQuestions = 0;
            int Correct = 0;
            double percentage=0;
            ParameterCollection obParam = new ParameterCollection();
         
            obParam.Add("@userid", userid);
          obDs=  dal.fnRetriveByPro("GetLastTestPerformanceByUser", obParam);
            if (obDs.Tables.Count > 0)
            {
                if (obDs.Tables[0].Rows.Count > 0)
                {
                    testid = obDs.Tables[0].Rows[0]["Testid"].ToString();
                    TotalQuestions = Convert.ToInt32(obDs.Tables[0].Rows[0]["TotalQuestions"]);
                    Correct = Convert.ToInt32(obDs.Tables[0].Rows[0]["TotalCorrected"]);
                    percentage = (Convert.ToDouble(Correct) / Convert.ToDouble(TotalQuestions)) * 100 ;

                    percentage = Convert.ToInt32(percentage);
                    if (percentage >= 20 && percentage <=40)
                    {
                        lasttestperformance.Text = "Below Average";
                        lasttestperformancepercentage.Text = percentage.ToString()+"%";
                        lasttestperformanceprogressbar.Style.Add("width", percentage.ToString()+"%");
                        lasttestperformanceprogressbar.Attributes.Add("class", "progress-bar bg-danger wow  progress-");
                    }
                    else if (percentage > 40 && percentage <= 60)
                    {
                        lasttestperformance.Text = "Average";
                        lasttestperformancepercentage.Text = percentage.ToString() + "%";
                        lasttestperformanceprogressbar.Style.Add("width", percentage.ToString() + "%");
                        lasttestperformanceprogressbar.Attributes.Add("class", "progress-bar bg-warning wow  progress-");
                    }
                    else if (percentage > 60 && percentage <= 80)
                    {
                        lasttestperformance.Text = "Above Average";
                        lasttestperformancepercentage.Text = percentage.ToString() + "%";
                        lasttestperformanceprogressbar.Style.Add("width", percentage.ToString() + "%");
                        lasttestperformanceprogressbar.Attributes.Add("class", "progress-bar bg-info wow  progress-");
                    }
                    else if (percentage > 80 && percentage <= 100)
                    {
                        lasttestperformance.Text = "Excellent";
                        lasttestperformancepercentage.Text = percentage.ToString() + "%";
                        lasttestperformanceprogressbar.Style.Add("width", percentage.ToString() + "%");
                        lasttestperformanceprogressbar.Attributes.Add("class", "progress-bar bg-success wow  progress-");
                    }
                    else
                    {
                        lasttestperformance.Text = "Very poor";
                        lasttestperformancepercentage.Text = percentage.ToString() + "%";
                        lasttestperformanceprogressbar.Style.Add("width", percentage.ToString() + "%");
                        lasttestperformanceprogressbar.Attributes.Add("class", "progress-bar bg-danger wow  progress-");
                    }


                }
            }else
            {
                lasttestperformance.Text = "Not Yet Performed";
                //lasttestperformancepercentage.Text = percentage.ToString() + "%";
                //lasttestperformanceprogressbar.Style.Add("width", percentage.ToString() + "%");
                //lasttestperformanceprogressbar.Attributes.Add("class", "progress-bar bg-danger wow  progress-");

            }
        }
        catch(Exception ex)
        {

        }

    }

}