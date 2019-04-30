using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.Drawing;

public partial class ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string uniqueCode = string.Empty;
        string Email = txtEmail.Text.Trim();
        tbl_Registration User = RegisterData.Check_Email(Email);
        if(User != null)
        {
            uniqueCode = Convert.ToString(System.Guid.NewGuid());
            tbl_Registration data = RegisterData.Update_UniqueCode(Email,uniqueCode);
            
            if (data != null)
            {
                StringBuilder strBody = new StringBuilder();

              //  for server
                strBody.Append("<a href=http://182.156.245.82:214/ResetPassword.aspx?email=" + Email + "&uCode=" + uniqueCode + ">Click here to change your password</a>");

               // forLocal
               // strBody.Append("<a href=http://localhost:52534/ResetPassword.aspx?email=" + Email + "&uCode=" + uniqueCode + ">Click here to change your password</a>");

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage("test@octalsoftware.com", Email, "Reset Your Password", strBody.ToString());
                System.Net.NetworkCredential mailAuthenticaion = new System.Net.NetworkCredential("test@octalsoftware.com", "octal@123");

                System.Net.Mail.SmtpClient mailclient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                mailclient.EnableSsl = true;
                mailclient.UseDefaultCredentials = false;
                mailclient.Credentials = mailAuthenticaion;
                mail.IsBodyHtml = true;
                mailclient.Send(mail);
                lblStatus.Visible = true;
                lblStatus.Text = "Reset password link has been sent to your email address.";
                lblStatus.ForeColor = Color.Green;
                txtEmail.Text = string.Empty;
                lblStatusJScript();
            }
            else
            {
                lblStatus.Visible = true;
                lblStatus.Text = "Reset link not sent email address.";
                lblStatus.ForeColor = Color.Red;
                txtEmail.Text = string.Empty;
                lblStatusJScript();
            }
        }
        else
        {
            lblStatus.Visible = true;
            lblStatus.Text = "Invalid Email or Password";
            lblStatus.ForeColor = System.Drawing.Color.Red;
            txtEmail.Text = "";
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