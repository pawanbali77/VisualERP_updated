using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;


public partial class ResetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string UniqueCode = Convert.ToString(Request.QueryString["uCode"]);
            string Email = Convert.ToString(Request.QueryString["email"]);
            tbl_Registration User = RegisterData.Check_EmailandUniqueCode(Email,UniqueCode);
            if(User != null)
            {
                ResetPwdPanel.Visible = true;
            }
            else
            {
                ResetPwdPanel.Visible = false;
                lblExpired.Text = "Reset password link has expired.It was for one time use only";
                return;
            }
        }
    }
    protected void btnChangePwd_Click(object sender, EventArgs e)
    {
        string UniqueCode = Convert.ToString(Request.QueryString["uCode"]);
        string Email = Convert.ToString(Request.QueryString["email"]);
        string Password = txtNewPwd.Text.Trim();
        tbl_Registration data = RegisterData.Update_Password(Email,UniqueCode,Password);
        if(data != null)
        {
            lblStatus.Visible = true;
            lblStatus.Text = "Your password has been updated successfully.";
            lblStatus.ForeColor = Color.Green;
            txtNewPwd.Text = string.Empty;
            txtConfirmPwd.Text = string.Empty;
            lblStatusJScript();
        }
        else
        {
            lblStatus.Visible = true;
            lblStatus.Text = "Password not updated.";
            lblStatus.ForeColor = Color.Red;
            txtNewPwd.Text = string.Empty;
            txtConfirmPwd.Text = string.Empty;
            lblStatusJScript();
        }
    }
    internal void lblStatusJScript()
    {
        ClientScriptManager script = Page.ClientScript;
        if (!script.IsStartupScriptRegistered(this.GetType(), "HideLabel"))
        {
            script.RegisterStartupScript(this.GetType(), "HideLabel",
            "<script type='text/javascript'>HideLabel('" + lblStatus.ClientID + "')</script>");
        }
    }
}