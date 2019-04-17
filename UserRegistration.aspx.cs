using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Email"] != null || Session["ID"] != null)
            {
                string Email = Session["Email"].ToString();
                string ID = Session["ID"].ToString();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
            GetDropDownList_Role();
        }
    }
    protected void txtclear_Click(object sender, EventArgs e)
    {
        clear();
    }
    public void clear()
    {
        txtUsername.Text = "";
        txtEmail.Text = "";
        txtPassword.Text = "";
        txtConfirmPassword.Text = "";
        txtMobile.Text = "";
        ddlRole.SelectedIndex = 0;
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        InsertnewUserDetail();
    }
    private void InsertnewUserDetail()
    {
        if (Session["Email"] != null || Session["ID"] != null)
        {
            string Email = Session["Email"].ToString();
            string ID = Session["ID"].ToString();

            tbl_Registration newUserdetail = new tbl_Registration();

            newUserdetail.Username = txtUsername.Text.Trim();
            newUserdetail.Email = txtEmail.Text.Trim();
            newUserdetail.Password = txtPassword.Text.Trim();
            newUserdetail.Mobile = txtMobile.Text.Trim();
            newUserdetail.CreatedDate = DateTime.Now;
            newUserdetail.Status = true;
            newUserdetail.Role = Convert.ToInt32(ddlRole.SelectedIndex);
            newUserdetail.IsDeleted = false;
            newUserdetail.UploadPhoto = "";

            tbl_Registration data = RegisterData.check_Parentid(Convert.ToInt32(ID));

            if (data != null)
            {
                newUserdetail.ParentID = data.RegisterID;
                newUserdetail.CompanyName = data.CompanyName;
                newUserdetail.UploadPhoto = data.UploadPhoto;
                newUserdetail.Industries = data.Industries;

                var result = RegisterData.register_newUsers(newUserdetail);
                if (result == 1)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "You are registered user successfuly";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    clear();
                    lblmsgJScript();
                }
                else if (result == 0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Registration unsuccessful";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    clear();
                    lblmsgJScript();
                }
                else if (result == 2)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Email already exist";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    clear();
                    lblmsgJScript();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    internal void lblmsgJScript()
    {
        ClientScriptManager script = Page.ClientScript;
        if (!script.IsStartupScriptRegistered(this.GetType(), "HideLabel"))
        {
            script.RegisterStartupScript(this.GetType(), "HideLabel",
            "<script type='text/javascript'>HideLabel('" + lblmsg.ClientID + "')</script>");
        }
    }


    public List<DropDownListItem> GetDropDownList_Role()
    {
        VisualERPDataContext db = new VisualERPDataContext();
        List<DropDownListItem> items = new List<DropDownListItem>();
        items = (from ddlvalue in db.tbl_Roles
                 orderby ddlvalue.ID
                 select new DropDownListItem
                 {
                     Id = Convert.ToInt32(ddlvalue.ID),
                     Role = ddlvalue.Role
                 }).ToList();

        ddlRole.DataSource = items;
        ddlRole.DataTextField = "Role";
        ddlRole.DataValueField = "Id";
        ddlRole.DataBind();
        return items;
    }

    public class DropDownListItem
    {
        public string Role { get; set; }
        public int Id { get; set; }
    }
}