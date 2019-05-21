using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadFiles : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["NewVisualERPConnectionString"].ConnectionString;

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
        string strConnString = ConfigurationManager.ConnectionStrings["NewVisualERPConnectionString"].ConnectionString; ;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select * from [TrainingMaterail]";
                cmd.Connection = con;
                con.Open();
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
                con.Close();
            }
        }
    }

    public bool CheckIfFileExist(string FileName)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["NewVisualERPConnectionString"].ConnectionString; ;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            con.Open();
            SqlCommand check_File_Name = new SqlCommand("SELECT COUNT(*) FROM [TrainingMaterail] WHERE ([ContentPost] = @FileName)", con);
            check_File_Name.Parameters.AddWithValue("@FileName", FileName);
            int FileExist = (int)check_File_Name.ExecuteScalar();
            con.Close();
            if (FileExist > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public string GetFullFileName(int ID)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["NewVisualERPConnectionString"].ConnectionString; ;
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

    protected void LinkBPOST_Click(object sender, EventArgs e)
    {
        if (CheckIfFileExist(TextBixcomment.Text.Trim()))
        {
            ErrorMsg.Visible = true;
            ErrorMsg.Text = "File name already Exist ..!!";
        }
        else
        {
            string folderPath = Server.MapPath("~/FileUploads/");

            string tendString = TextBixcomment.Text.Trim();
            string strname = "";
            string extension = "";
            string getADPOST = "";
            string str = ConfigurationManager.ConnectionStrings["NewVisualERPConnectionString"].ConnectionString; ;
            if (FileUploadpost.HasFile)
            {
                strname = FileUploadpost.FileName.ToString();
                extension = System.IO.Path.GetExtension(strname);
                using (SqlConnection con = new SqlConnection(str))
                {
                    SqlCommand cmd = new SqlCommand();

                    if (extension.ToLower() == ".mp4" || extension.ToLower() == ".MP4" || extension.ToLower() == ".pdf" || extension.ToLower() == ".PDF")
                    {
                        getADPOST = "Insert INTO [TrainingMaterail] (ContentPost,ImageName1,Extension,Path) values (@ContentPost,@ImageName1,@Extension,@Path)";
                        cmd.Parameters.AddWithValue("@ContentPost", TextBixcomment.Text.Replace(Environment.NewLine, "<br/>").Trim());
                        cmd.Parameters.AddWithValue("@ImageName1", strname);
                        cmd.Parameters.AddWithValue("@Extension", extension);
                        cmd.Parameters.AddWithValue("@Path", "/FileUploads/" + tendString + extension);

                        cmd.CommandText = getADPOST;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        FileUploadpost.SaveAs(folderPath + tendString + extension);
                        Response.Redirect(Request.Url.AbsoluteUri);
                    }
                    else
                    {
                        ErrorMsg.Visible = true;
                        ErrorMsg.Text = "File Extension is not correct. Please Upload Pdf or Mp4 files.";
                        TextBixcomment.Text = "";

                    }
                }
            }
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string getADPOST = "";
        string folderPath = Server.MapPath("~/FileUploads/");

        int ID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
        string FullFileName = GetFullFileName(ID);
        if (ID != null)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand();

                getADPOST = "delete from [TrainingMaterail] where Id= " + ID + "";

                cmd.CommandText = getADPOST;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                File.Delete(folderPath + FullFileName);
                BindGrid();
            }
        }
    }

    protected void lblextension_Click(object sender, EventArgs e)
    {
        int ID = Convert.ToInt32((sender as LinkButton).CommandArgument);
        LinkButton LnkSeletedRow = sender as LinkButton;
        if (LnkSeletedRow != null)
        {
            if (LnkSeletedRow.Text == "Download")
            {
                string folderPath = Server.MapPath("~/FileUploads/");
                string FullFileName = GetFullFileName(ID);
                string filePath = folderPath + FullFileName;
                if (ID != null)
                {
                    Response.ContentType = ContentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                    Response.WriteFile(filePath);
                    Response.End();
                }
            }
            else
            {
                Response.Redirect("Player.aspx?id=" + ID + "");
            }
        }
    }
}