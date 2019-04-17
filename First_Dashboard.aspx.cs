using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class First_Dashboard : System.Web.UI.Page
{
    string Email = "";
    int ID;
    int Parent_id;
    int RoleID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
            if (data.ParentID == 0)
            {
                gridbind_alluser();
            }
            else if (data.ParentID != 0 && data.Role == 1)
            {
                gridbind_alluserwithparentId();
            }
            else
            {
                Email = Session["Email"].ToString();
                ID = Convert.ToInt32(Session["ID"].ToString());
                Parent_id = Convert.ToInt32(Session["Parent_id"].ToString());
                //int ProcessId = Convert.ToInt32(Session["ProcessId"].ToString());
                int CompanyId = Convert.ToInt32(Session["CompanyID"].ToString());
                int UserID = Convert.ToInt32(Session["UserID"].ToString());
                int RoleID = Convert.ToInt32(Session["RoleID"].ToString());

                int ProcessId = 0;

                Session.Add("ID", ID);
                Session.Add("Email", Email);
                Session.Add("Parent_id", Parent_id);
                Session.Add("ProcessId", ProcessId);
                Session.Add("CompanyId", CompanyId);
                Session.Add("UserID", ID);
                Session.Add("RoleID", RoleID);

                if (RoleID == 1)
                {

                }
                else if (RoleID == 2)
                {
                    Control myControlMenu1 = Page.Master.FindControl("menu_UserRegister");
                    if (myControlMenu1 != null)
                    {
                        myControlMenu1.Visible = false;
                    }
                    Control myControlMenu2 = Page.Master.FindControl("menu_AllUsers");
                    if (myControlMenu2 != null)
                    {
                        myControlMenu2.Visible = false;
                    }
                    Control myControlMenu3 = Page.Master.FindControl("menu_Process");
                    if (myControlMenu3 != null)
                    {
                        myControlMenu3.Visible = true;
                    }
                }
                else if (RoleID == 3)
                {

                }
                else if (RoleID == 4)
                {
                    Control myControlMenu1 = Page.Master.FindControl("menu_UserRegister");
                    if (myControlMenu1 != null)
                    {
                        myControlMenu1.Visible = false;
                    }
                    Control myControlMenu2 = Page.Master.FindControl("menu_AllUsers");
                    if (myControlMenu2 != null)
                    {
                        myControlMenu2.Visible = false;
                    }
                    Control myControlMenu3 = Page.Master.FindControl("menu_Process");
                    if (myControlMenu3 != null)
                    {
                        myControlMenu3.Visible = true;
                    }
                }
                Response.Redirect("~/MyProcess.aspx");
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
    public void gridbind_alluser()
    {
        List<GridData> data = RegisterData.allusers(ID);
        if (data.Count > 0)
        {
            gv_alluser.DataSource = data;
            gv_alluser.DataBind();
        }
        else
        {
            gv_alluser.DataSource = data;
            gv_alluser.DataBind();
        }
    }

    public void gridbind_alluserwithparentId()
    {
        Parent_id = Convert.ToInt32(Session["Parent_id"].ToString());
        List<GridData> data = RegisterData.allusers(Parent_id);
        if (data.Count > 0)
        {
            gv_alluser.DataSource = data;
            gv_alluser.DataBind();
        }
        else
        {
            gv_alluser.DataSource = data;
            gv_alluser.DataBind();
        }
    }
    protected void gv_alluser_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_alluser.EditIndex = e.NewEditIndex;
        this.gridbind_alluser();
    }

    protected void gv_alluser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gv_alluser.EditIndex)
        {
            (e.Row.Cells[5].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
        }
    }
    protected void gv_alluser_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_alluser.EditIndex = -1;
        this.gridbind_alluser();
    }
    protected void gv_alluser_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gv_alluser.Rows[e.RowIndex];
        
        int Registerid = Convert.ToInt32(gv_alluser.DataKeys[e.RowIndex].Values[0]);

        string Username = (row.FindControl("txtusername") as TextBox).Text;
        string Mobile = (row.FindControl("txtmobile") as TextBox).Text;
        string Role = (row.FindControl("ddlrole") as DropDownList).SelectedValue;

        if (Role == "0")
        {
            tbl_Registration data = RegisterData.check_UserRole(Registerid);
            Role = data.Role.ToString();
        }
        
        string Status = (row.FindControl("ddlstatus") as DropDownList).SelectedValue;
        if (Status == "0")
        {
            Status = "False";
        }
        else
        {
            Status = "True";
        }
        using (VisualERPDataContext db = new VisualERPDataContext())
        {
            tbl_Registration customer = (from c in db.tbl_Registrations
                                         where c.RegisterID == Registerid
                                         select c).FirstOrDefault();
            customer.Username = Username;
            customer.Mobile = Mobile;
            customer.Role = Convert.ToInt32(Role);
            customer.Status = Convert.ToBoolean(Status);
            db.SubmitChanges();
        }
        gv_alluser.EditIndex = -1;
        this.gridbind_alluser();
    }
    protected void gv_alluser_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int Registerid = Convert.ToInt32(gv_alluser.DataKeys[e.RowIndex].Values[0]);
        using (VisualERPDataContext ctx = new VisualERPDataContext())
        {
            tbl_Registration customer = (from c in ctx.tbl_Registrations
                                         where c.RegisterID == Registerid
                                         select c).FirstOrDefault();
            ctx.tbl_Registrations.DeleteOnSubmit(customer);
            ctx.SubmitChanges();
        }
        this.gridbind_alluser();
    }
}