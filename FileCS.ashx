<%@ WebHandler Language="C#" Class="FileCS" %>

using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
public class FileCS : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        int ID = Convert.ToInt32(context.Request.QueryString["id"]);
        string folderPath = HttpContext.Current.Server.MapPath("~/FileUploads/");
        string FullFileName = GetFullFileName(ID);
        string filePath = folderPath + FullFileName;
        string filename = Path.GetFileName(filePath);
        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
        context.Response.Clear();
        context.Response.Buffer = true;
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + FullFileName);
        context.Response.ContentType = "video/mp4";
        context.Response.BinaryWrite(bytes);
        context.Response.End();
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