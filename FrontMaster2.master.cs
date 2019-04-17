using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrontMaster2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserImage = "";

        if (Session["CompanyName"] != null)
        {
            UserImage = Session["CompanyName"].ToString();
        }
        var image = RegisterData.ProfileImage(0, UserImage);

        if (image == null || image == "")
        {
            Img_upload.ImageUrl = "~/images/no-image-icon-33.png";
        }
        else
        {
            Img_upload.ImageUrl = image;
        }


        checkRole();

    }

    public void checkRole()
    {
        int RoleID = Convert.ToInt32(Session["RoleID"].ToString());

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
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }


}
