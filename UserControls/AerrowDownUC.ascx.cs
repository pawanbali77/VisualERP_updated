using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.ComponentModel;

public partial class UserControls_AerrowDownUC : System.Web.UI.UserControl
{
    [BrowsableAttribute(true)]
    public int SystemIOID
    {
        set
        {
            ViewState["SysIOId"] = value;

        }
    }
    private System.Delegate _delWithParam;
    public Delegate PageMethodWithParamRef
    {
        set { _delWithParam = value; }
    }

    private System.Delegate _delNoParam;
    public Delegate PageMethodWithNoParamRef
    {
        set { _delNoParam = value; }
    }


   
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void imgBtnDownUc_Click(object sender, ImageClickEventArgs e)
    {
        //UserControls_HandOffUC zz = LoadControl("HandOffUC.ascx") as UserControls_HandOffUC;
        //zz.ProcessObjectId = Convert.ToInt32(ViewState["poId"]);
        ////UserControl UcBOM = (UserControl)Page.FindControl("ModelPopupBOMUC.ascx");
        //AjaxControlToolkit.ModalPopupExtender PopupModelHandOff = (AjaxControlToolkit.ModalPopupExtender)zz.FindControl("ModelPopupHandOff");
        //PopupModelHandOff.Show();

        object[] obj = new object[2];
        obj[0] = ViewState["SysIOId"].ToString();
        obj[1] = "HandOffOutput";
        _delWithParam.DynamicInvoke(obj);


    }
}