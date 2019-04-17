using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditProfile : System.Web.UI.Page
{
    string Email = "";
    int ID;
    int Parent_id;
    int RoleID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["UploadImage"] = "";
            if (Session["Email"] != null || Session["ID"] != null || Session["RoleID"] != null)
            {
                Email = Session["Email"].ToString();
                ID = Convert.ToInt32(Session["ID"].ToString());
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
            tbl_Registration data = RegisterData.check_UserRole(ID);
            if (data != null)
            {
                if (data.ParentID == 0 && data.Role == 1 )
                {
                    GridBind_User();
                }
                else if (data.ParentID !=0 && data.Role == 1)
                {
                    GridBind_User();
                }
                else
                {
                    GridBind_User2();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        if (Session["Email"] != null || Session["ID"] != null)
        {
            Email = Session["Email"].ToString();
            ID = Convert.ToInt32(Session["ID"].ToString());
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    public void GridBind_User()
    {
        Parent_id = Convert.ToInt32(Session["Parent_id"].ToString());
        List<GridUserdata> userdata = RegisterData.EditUser(ID , Parent_id);
        if (userdata.Count > 0)
        {
            gv_EditProfile.DataSource = userdata;
            gv_EditProfile.DataBind();
            gv_EditProfile.Visible = true;
        }
        else
        {
            gv_EditProfile.DataSource = userdata;
            gv_EditProfile.DataBind();
            gv_EditProfile.Visible = true;
        }
    }
    protected void gv_EditProfile_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_EditProfile.EditIndex = e.NewEditIndex;
        this.GridBind_User();
    }

    protected void gv_EditProfile_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_EditProfile.EditIndex = -1;
        this.GridBind_User();
    }

    protected void gv_EditProfile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gv_EditProfile.EditIndex)
        //{
        //    //(e.Row.Cells[5].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
        //}
    }

    protected void gv_EditProfile_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gv_EditProfile.Rows[e.RowIndex];
        int Registerid = Convert.ToInt32(gv_EditProfile.DataKeys[e.RowIndex].Values[0]);

        string Mobile = (row.FindControl("txtmobile") as TextBox).Text;
        string Industry = (row.FindControl("ddlIndustry") as DropDownList).SelectedValue;
        FileUpload fuUpload = (FileUpload)gv_EditProfile.Rows[e.RowIndex].Cells[3].FindControl("fileUpComp");
        string CompanyPhoto = "";
        if (fuUpload.PostedFile.ContentLength > 0)
        {
            if (fuUpload.PostedFile != null)
            {
                CompanyPhoto = GenralFunction.UploadBookImage(fuUpload, Resources.Message.Up_Path, DateTime.Now.Ticks.ToString(), "Hello");
            }
        }
        else
        {
            CompanyPhoto = ViewState["UploadImage"].ToString();
            if (CompanyPhoto == null || CompanyPhoto == "")
            {
                tbl_Registration data = RegisterData.check_UserRole(ID);
                CompanyPhoto = data.UploadPhoto;
            }
        }
        using (VisualERPDataContext db = new VisualERPDataContext())
        {
            tbl_Registration customer = (from c in db.tbl_Registrations
                                         where c.RegisterID == Registerid
                                         select c).FirstOrDefault();
            customer.Mobile = Mobile;
            customer.Industries = Convert.ToInt32(Industry);
            customer.UploadPhoto = CompanyPhoto;
            db.SubmitChanges();
        }
        gv_EditProfile.EditIndex = -1;
        this.GridBind_User();
    }

    public void GridBind_User2()
    {
        Parent_id = Convert.ToInt32(Session["Parent_id"].ToString());
        List<GridUserdata2> userdata = RegisterData.EditUser2(ID, Parent_id);
        if (userdata.Count > 0)
        {
            gv_EditProfile2.DataSource = userdata;
            gv_EditProfile2.DataBind();
            gv_EditProfile2.Visible = true;
        }
        else
        {
            gv_EditProfile2.DataSource = userdata;
            gv_EditProfile2.DataBind();
            gv_EditProfile2.Visible = true;
        }
    }

    protected void gv_EditProfile2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_EditProfile2.EditIndex = e.NewEditIndex;
        this.GridBind_User2();
    }

    protected void gv_EditProfile2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_EditProfile2.EditIndex = -1;
        this.GridBind_User2();
    }

    protected void gv_EditProfile2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gv_EditProfile2.EditIndex)
        //{
        //    //(e.Row.Cells[5].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
        //}
    }

    protected void gv_EditProfile2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gv_EditProfile2.Rows[e.RowIndex];

        int Registerid = Convert.ToInt32(gv_EditProfile2.DataKeys[e.RowIndex].Values[0]);

        string Username = (row.FindControl("txtUsername") as TextBox).Text;
        string Mobile = (row.FindControl("txtmobile") as TextBox).Text;
       
        using (VisualERPDataContext db = new VisualERPDataContext())
        {
            tbl_Registration customer = (from c in db.tbl_Registrations
                                         where c.RegisterID == Registerid
                                         select c).FirstOrDefault();
            customer.Username = Username;
            customer.Mobile = Mobile;
            db.SubmitChanges();
        }
        gv_EditProfile2.EditIndex = -1;
        this.GridBind_User2();
    }
}