using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            GetDropDownList_Industries();

            ViewState["UploadImage"] = "";
        }
    }
    protected void txtclear_Click(object sender, EventArgs e)
    {
        clear();
    }
    public void clear()
    {
        txtEmail.Text = "";
        txtPassword.Text = "";
        txtConfirmPassword.Text = "";
        txtMobile.Text = "";
        txtCompanyname.Text = "";
        ddlIndustries.SelectedIndex = 0;
        chkAgree.Checked = false;
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        InsertRegisterUserDetail();
    }
    private void InsertRegisterUserDetail()
    {
        tbl_Registration registerdetail = new tbl_Registration();
        registerdetail.Email = txtEmail.Text.Trim();
        registerdetail.Password = txtPassword.Text.Trim();
        registerdetail.Mobile = txtMobile.Text.Trim();
        registerdetail.CreatedDate = DateTime.Now;
        registerdetail.Status = true;
        registerdetail.Role = 1;
        registerdetail.IsDeleted = false;
        registerdetail.CompanyName = txtCompanyname.Text.Trim();
        registerdetail.ParentID = 0;
        registerdetail.Industries = ddlIndustries.SelectedIndex;

        string CompanyPhoto = "";

        if (fileUpComp.PostedFile.ContentLength > 0)
        {
            if (fileUpComp.PostedFile != null)
            {
                CompanyPhoto = GenralFunction.UploadBookImage(fileUpComp, Resources.Message.Up_Path, DateTime.Now.Ticks.ToString(), "Hello");
            }
        }
        else
        {
            CompanyPhoto = ViewState["UploadImage"].ToString();
        }
        registerdetail.UploadPhoto = CompanyPhoto;

        var result = RegisterData.register_Users(registerdetail);
        if (result == 1)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "You are registered successfuly";
            lblmsg.ForeColor = System.Drawing.Color.Green;
            clear();
            hplinklogin.Visible = true;
            lblmsgJScript();
        }
        else if (result == 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Registration unsuccessful";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            clear();
            hplinklogin.Visible = false;
            lblmsgJScript();
        }
        else if (result == 2)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Email already exist";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            clear();
            hplinklogin.Visible = true;
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
    public List<DropDownListItem> GetDropDownList_Industries()
    {
        VisualERPDataContext db = new VisualERPDataContext();
        List<DropDownListItem> items = new List<DropDownListItem>();
        items = (from ddlvalue in db.tbl_Industries
                 orderby ddlvalue.ID
                 select new DropDownListItem
                 {
                     Id = Convert.ToInt32(ddlvalue.ID),
                     Name = ddlvalue.Name
                 }).ToList();
        
        ddlIndustries.DataSource = items;
        ddlIndustries.DataTextField = "Name";
        ddlIndustries.DataValueField = "Id";
        ddlIndustries.DataBind();
        return items;
    }

    public class DropDownListItem
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}