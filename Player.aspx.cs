using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Player : System.Web.UI.Page
{
    string strConnString = ConfigurationManager.ConnectionStrings["NewVisualERPConnectionString"].ConnectionString;


    protected void Page_Load(object sender, EventArgs e)
    {

        Label lblManager = (Label)Master.FindControl("lblManager");
        lblManager.Text = "GroEngineUniversity";
        lblManager.Attributes.Add("class", "Inventory");


        System.Web.UI.HtmlControls.HtmlGenericControl currdiv = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("divSidebar");
        currdiv.Style.Add("display", "none");

        int ID = Convert.ToInt32(Request.QueryString["id"]);
        triggerclick.HRef = "/FileCS.ashx?id=" + ID;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public string GetFullFileName(int ID)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["NewVisualERPConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            string FullFileName = "";
            con.Open();
            SqlCommand check_File_Name = new SqlCommand("SELECT ContentPost+extension as [FullFileName] FROM [TrainingMaterail] WHERE ([Id] = @ID)", con);
            check_File_Name.Parameters.AddWithValue("@ID", ID);
            FullFileName = Convert.ToString(check_File_Name.ExecuteScalar());
            con.Close();
            return FullFileName;
        }
    }


}