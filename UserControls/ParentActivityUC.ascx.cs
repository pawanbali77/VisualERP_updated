using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
[Serializable]
public partial class UserControls_ParentActivityUC : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lnkbtn_Click(object sender, EventArgs e)
    {
       UserControl UcAttribute = (UserControl)Page.FindControl("ModelPopupAttributeUC.ascx");
       AjaxControlToolkit.ModalPopupExtender PopupModelAttribute = (AjaxControlToolkit.ModalPopupExtender)UcAttribute.FindControl("mopoExUser");
       PopupModelAttribute.Show();
    }
    protected void lnkBtnInput_Click(object sender, EventArgs e)
    {

    }
    protected void lnkBtnBOM_Click(object sender, EventArgs e)
    {

    }
    protected void lnkBtnTFG_Click(object sender, EventArgs e)
    {

    }
    protected void lnkBtnMachine_Click(object sender, EventArgs e)
    {

    }
    protected void lnkbtnErrorReport_Click(object sender, EventArgs e)
    {

    }
     
}