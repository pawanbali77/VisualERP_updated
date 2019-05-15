using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TrainingVideo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Label lblManager = (Label)Master.FindControl("lblManager");
        lblManager.Text = "GroEngineUniversity";
        lblManager.Attributes.Add("class", "Inventory");


        System.Web.UI.HtmlControls.HtmlGenericControl currdiv = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("divSidebar");
        currdiv.Style.Add("display", "none");

        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    private void BindGrid()
    {
        string strConnString = ConfigurationManager.ConnectionStrings["NewVisualERPConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Id, Name from tbl_Files";
                cmd.Connection = con;
                con.Open();
                DataList1.DataSource = cmd.ExecuteReader();
                DataList1.DataBind();
                con.Close();
            }
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string[] validFileTypes = { "mp4", "MP4" };
        string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
        bool isValidFile = false;
        for (int i = 0; i < validFileTypes.Length; i++)
        {
            if (ext == "." + validFileTypes[i])
            {
                isValidFile = true;
                break;
            }
        }
        if (!isValidFile)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Invalid File. Please upload a File with extension " +
                           string.Join(",", validFileTypes);
        }
        else
        {
            using (BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream))
            {
                byte[] bytes = br.ReadBytes((int)FileUpload1.PostedFile.InputStream.Length);
                string strConnString = ConfigurationManager.ConnectionStrings["NewVisualERPConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(strConnString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "insert into tbl_Files(Name, ContentType, Data) values (@Name, @ContentType, @Data)";
                        cmd.Parameters.AddWithValue("@Name", Path.GetFileName(FileUpload1.PostedFile.FileName));
                        cmd.Parameters.AddWithValue("@ContentType", "video/mp4");
                        cmd.Parameters.AddWithValue("@Data", bytes);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            Label1.ForeColor = System.Drawing.Color.Green;
            Label1.Text = "File uploaded successfully.";
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}