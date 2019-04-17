using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.Cookies["Email"] != null && Request.Cookies["Password"] != null)
            {
                txtEmail.Text = Request.Cookies["Email"].Value;
                txtPassword.Attributes["value"] = Request.Cookies["Password"].Value;
            }
        }
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        if (rememberme.Checked)
        {
            Response.Cookies["Email"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
        }
        else
        {
            Response.Cookies["Email"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
        }

        tbl_Registration user = RegisterData.Login_Check(txtPassword.Text, txtEmail.Text);

        if (user != null)
        {
            if (user.ParentID == 0)
            {
                int ProcessId = 0;
                Session.Add("ID", user.RegisterID);
                Session.Add("Email", user.Email);
                Session.Add("Parent_id", user.ParentID);
                Session.Add("ProcessId", ProcessId);
                Session.Add("CompanyId", user.ParentID);
                Session.Add("UserID", user.RegisterID);
                Session.Add("RoleID", user.Role);
                // Session.Add("Image", user.UploadPhoto);

                Session.Add("CompanyName", user.CompanyName);


                Response.Redirect("~/First_Dashboard.aspx");
            }
            else
            {
                int ProcessId = 0;
                Session.Add("ID", user.RegisterID);
                Session.Add("Email", user.Email);
                Session.Add("Parent_id", user.ParentID);
                Session.Add("ProcessId", ProcessId);
                Session.Add("CompanyId", user.ParentID);
                Session.Add("UserID", user.RegisterID);
                Session.Add("RoleID", user.Role);
              //  Session.Add("Image", user.UploadPhoto);
                Session.Add("CompanyName", user.CompanyName);

                Response.Redirect("~/First_Dashboard.aspx");
            }
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Invalid Email or Password";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            txtEmail.Text = "";
            lblmsgJScript();
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
}