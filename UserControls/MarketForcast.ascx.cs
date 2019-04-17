using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class UserControls_MarketForcast : System.Web.UI.UserControl
{
    private int _ProcessObjectId;
    private string _ForcastId;

    private int _Top;
    private int _Left;
    private string _Title;

    // Control's Unique Database Id
    [BrowsableAttribute(true)]
    public int ProcessObjectId
    {
        //get { return 0; }
        set { _ProcessObjectId = value;
        ViewState["MarketForcastPoid"] = _ProcessObjectId;
        }
    }

    // Unique Supplier Id
    [BrowsableAttribute(true)]
    public string SupplierId
    {
        set { _ForcastId = value; }
    }

    // Control position from top
    [BrowsableAttribute(true)]
    public int Top
    {
        set { _Top = value; }
    }

    // Control position from left
    [BrowsableAttribute(true)]
    public int Left
    {
        set { _Left = value; }
    }

    // Unique Supplier Name
    [BrowsableAttribute(true)]
    public string Title
    {
        set { _Title = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        divForcast.Style.Add("top", _Top.ToString() + "px");
        divForcast.Style.Add("left", _Left.ToString() + "px");
        //divForcast.Attributes["name"] = _ForcastId;
        divForcast.InnerText = _Title;

        lnkbtnDeleteForcast.Style.Add("top", "-15px");
        lnkbtnDeleteForcast.Style.Add("left", "-12px");

    }

    protected void lnkbtnDeleteForcast_Click1(object sender, EventArgs e)
    {
        int processobjId = 0;
        processobjId = this.CInt32(ViewState["MarketForcastPoid"]);
        if (processobjId > 0)
        {
            bool result = false;
            result = ProcessData.DeleteProcessObjDataByID(processobjId);////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables

            string absolutepath = Request.Url.AbsolutePath;
            string returnurl = absolutepath.Substring(absolutepath.LastIndexOf('/') + 1);
            if (returnurl == "ProcessManager.aspx")
            {
                Response.Redirect("ProcessManager.aspx");
            }
            else
            {
                Response.Redirect("Production.aspx");
            }
        }

    }
}