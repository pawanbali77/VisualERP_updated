using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MyProcess : System.Web.UI.Page
{
    string Email = "";
    int ID;
    int Parent_id;
    int RoleID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Email"] != null || Session["ID"] != null || Session["Parent_id"] != null || Session["RoleID"] != null)
            {
                Email = Session["Email"].ToString();
                ID = Convert.ToInt32(Session["ID"].ToString());
                Parent_id = Convert.ToInt32(Session["Parent_id"].ToString());
                RoleID = Convert.ToInt32(Session["RoleID"].ToString());
                if (Parent_id == 0 && RoleID == 1)
                {
                    Bindgridprocess();
                }
                else if (Parent_id != 0 && RoleID == 1)
                {
                    Bindgridprocess2();
                }
                else if (Parent_id != 0 && RoleID == 2)
                {
                    Bindgridprocess3();
                }
                else
                {
                    Bindgridprocess4();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
    public void Bindgridprocess()
    {
        int ID = Convert.ToInt32(Session["ID"].ToString());
        List<GridProcess> data = RegisterData.check_Process(ID);
        if (data.Count > 0)
        {
            gv_process.DataSource = data;
            gv_process.DataBind();
        }
        else
        {
            gv_process.DataSource = data;
            gv_process.DataBind();
            lnkCreateProcess.Visible = true;
        }
    }
    public void Bindgridprocess2()
    {
        Parent_id = Convert.ToInt32(Session["Parent_id"].ToString());
        List<GridProcess> data = RegisterData.check_Process(Parent_id);
        if (data.Count > 0)
        {
            gv_process.DataSource = data;
            gv_process.DataBind();
        }
        else
        {
            gv_process.DataSource = data;
            gv_process.DataBind();
            lnkCreateProcess.Visible = true;
        }
    }
    public void Bindgridprocess3()
    {
        int ID = Convert.ToInt32(Session["ID"].ToString());
        List<GridProcess> data = RegisterData.check_Process_IndividualProcess(ID);
        if (data.Count > 0)
        {
            gv_process.DataSource = data;
            gv_process.DataBind();
        }
        else
        {
            gv_process.DataSource = data;
            gv_process.DataBind();
            lnkCreateProcess.Visible = true;
        }
    }
    public void Bindgridprocess4()
    {
        int Parent_id = Convert.ToInt32(Session["Parent_id"].ToString());
        List<GridProcess> data = RegisterData.check_Process_Viewer(Parent_id);
        if (data.Count > 0)
        {
            gv_process.DataSource = data;
            gv_process.DataBind();
        }
        else
        {
            gv_process.DataSource = data;
            gv_process.DataBind();
        }
    }
    protected void gv_process_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = gv_process.Rows[rowIndex];

            //Fetch value of Name.
            string processname = (row.FindControl("lblProcessName") as Label).Text;

            //string User_RegisterId = (row.FindControl("lblUserRegisterID") as Label).Text;

            string ProcessId = (row.FindControl("lblProcessId") as Label).Text;
            string CompanyID = (row.FindControl("lblCompanyID") as Label).Text;
            string UserID = (row.FindControl("lblUserID") as Label).Text;
            //  Parent_id = Convert.ToInt32(Session["Parent_id"].ToString());

            int RoleID = Convert.ToInt32(Session["RoleID"].ToString());

            Session.Add("ProcessId", ProcessId);
            Session.Add("CompanyID", CompanyID);
            Session.Add("UserID", UserID);
            Session.Add("RoleID", RoleID);

            Response.Redirect("~/Default.aspx");
        }
        if (e.CommandName == "Delete")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gv_process.Rows[rowIndex];
            string ProcessId = (row.FindControl("lblProcessId") as Label).Text;
            using (VisualERPDataContext ctx = new VisualERPDataContext())
            {
                tbl_Process process = (from c in ctx.tbl_Processes
                                       where c.ProcessID == Convert.ToInt32(ProcessId)
                                       select c).FirstOrDefault();
                //  ctx.tbl_Processes.DeleteOnSubmit(process);
                ctx.SubmitChanges();
            }
            this.Bindgridprocess();
        }
    }
    protected void gv_process_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gv_process_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gv_process.EditIndex)
        {
            (e.Row.Cells[3].Controls[1] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
        }
    }
    protected void lblCreateProcess_Click(object sender, EventArgs e)
    {
        Email = Session["Email"].ToString();
        ID = Convert.ToInt32(Session["ID"].ToString());
        int ParentID = Convert.ToInt32(Session["Parent_id"].ToString());
        int ProcessId = Convert.ToInt32(Session["ProcessId"].ToString());
        int CompanyId = Convert.ToInt32(Session["CompanyID"].ToString());
        int UserID = Convert.ToInt32(Session["UserID"].ToString());

        Session.Add("ID", ID);
        Session.Add("Email", Email);
        Session.Add("Parent_id", ParentID);
        Session.Add("ProcessId", ProcessId);
        Session.Add("CompanyId", ParentID);
        Session.Add("UserID", ID);
        Response.Redirect("~/Default.aspx");
    }
    protected void gv_process_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (RoleID == 4)
        {
            e.Row.Cells[3].Visible = false; // hides the Delete column
        }
    }
}