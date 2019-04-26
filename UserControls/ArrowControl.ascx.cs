using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class UserControls_ArrowControl : System.Web.UI.UserControl
{
    private int _ProcessObjectId;
    private string _ControlId;

    private int _Top;
    private int _Left;
    private int _Width;
    private int _Height;
    private int _Type;
    //private string _Title;

    // Control's Unique Database Id
    private int _sourceTypeID = 1;
    [BrowsableAttribute(true)]
    public int SourceType
    {
        get { return _sourceTypeID; }
        set { _sourceTypeID = value; }
    }

    [BrowsableAttribute(true)]
    public int ProcessObjectId
    {
        //get { return 0; }
        set { _ProcessObjectId = value;

        ViewState["ArrowPoid"] = _ProcessObjectId;
        }
        
    }

    // Unique Supplier Id
    [BrowsableAttribute(true)]
    public string ControlId
    {
        set { _ControlId = value; }
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

    // Control position from top
    [BrowsableAttribute(true)]
    public int Width
    {
        set { _Width = value; }
    }

    // Control position from left
    [BrowsableAttribute(true)]
    public int Height
    {
        set { _Height = value; }
    }

    // Control position from left
    [BrowsableAttribute(true)]
    public int Type
    {
        set { _Type = value; }
    }

    // Unique Supplier Name
    //[BrowsableAttribute(true)]
    //public string Title
    //{
    //    set { _Title = value; }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        string absolutepath = Request.Url.AbsolutePath;
        string returnurl = absolutepath.Substring(absolutepath.LastIndexOf('/') + 1);
        if ((returnurl == "ProcessManager.aspx") || (returnurl == "TargetManager.aspx") || (returnurl == "EnterPriseManager.aspx"))
        {
            lnkbtnDeleteArrowControl.Enabled = false;
            lnkbtnDeleteArrowControl.Style.Add("cursor", " default!important");
        }
        //divSelectedArrow.Style.Add("top", _Top.ToString() + "px");
       // divSelectedArrow.Style.Add("left", _Left.ToString() + "px");
        divSelectedArrow.Style.Add("Width", _Width.ToString() + "px");
        divSelectedArrow.Style.Add("Height", _Height.ToString() + "px");
        divSelectedArrow.Attributes["name"] = _ControlId;
        divSelectedArrow.InnerText = string.Empty;
      //  imgSelectControl.ImageUrl = "~/images/Controls/yourimage.jpg";
        switch (_Type)
        {
            case 30:

                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow1 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "-8px");
                lnkbtnDeleteArrowControl.Style.Add("left", "-1px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                
                break;
            case 31:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow2 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "78px");
                lnkbtnDeleteArrowControl.Style.Add("left", "-3px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                
                break;
            case 32:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow3 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "-3px");
                lnkbtnDeleteArrowControl.Style.Add("left", "-8px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 33:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow4 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "78px");
                lnkbtnDeleteArrowControl.Style.Add("left", "66px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 34:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow5 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "79px");
                lnkbtnDeleteArrowControl.Style.Add("left", "73px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 35:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow6 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "-7px");
                lnkbtnDeleteArrowControl.Style.Add("left", "57px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 36:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow7 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "78px");
                lnkbtnDeleteArrowControl.Style.Add("left", "26px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 37:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow8 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "-7px");
                lnkbtnDeleteArrowControl.Style.Add("left", "25px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 38:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow9 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "75px");
                lnkbtnDeleteArrowControl.Style.Add("left", "72px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 39:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow10 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "-7px");
                lnkbtnDeleteArrowControl.Style.Add("left", "-5px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 40:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow11 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "-7px");
                lnkbtnDeleteArrowControl.Style.Add("left", "57px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 41:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow12 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "76px");
                lnkbtnDeleteArrowControl.Style.Add("left", "-6px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 42:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow13 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "79px");
                lnkbtnDeleteArrowControl.Style.Add("left", "25px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 43:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow14 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "59px");
                lnkbtnDeleteArrowControl.Style.Add("left", "60px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 44:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow15 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "14px");
                lnkbtnDeleteArrowControl.Style.Add("left", "61px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 45:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow16 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "76px");
                lnkbtnDeleteArrowControl.Style.Add("left", "-6px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 46:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow17 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "-7px");
                lnkbtnDeleteArrowControl.Style.Add("left", "-9px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 47:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow18 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "-5px");
                lnkbtnDeleteArrowControl.Style.Add("left", "26px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 48:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow19 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "76px");
                lnkbtnDeleteArrowControl.Style.Add("left", "26px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 49:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow20 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "36px");
                lnkbtnDeleteArrowControl.Style.Add("left", "-6px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;
            case 50:
                divSelectedArrow.Style.Add("background-image", "url('" + Resources.ArrowPath.Arrow21 + "')");
                lnkbtnDeleteArrowControl.Style.Add("top", "37px");
                lnkbtnDeleteArrowControl.Style.Add("left", "55px");
                lnkbtnDeleteArrowControl.ID = "lnkbtnDel-CArrow_" + _ProcessObjectId;
                break;

            default:
                Console.WriteLine("Default case");
                break;
        }
    }


    //protected void lnkbtnDeleteArrowControl_Click1(object sender, EventArgs e)
    //{
    //    int processobjId = 0;
    //    processobjId = this.CInt32(ViewState["ArrowPoid"]);
    //    if (processobjId > 0)
    //    {
    //        bool result = false;
    //        result = ProcessData.DeleteProcessObjDataByID(processobjId);////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables

    //        string absolutepath = Request.Url.AbsolutePath;
    //        string returnurl = absolutepath.Substring(absolutepath.LastIndexOf('/') + 1);
    //        if (returnurl == "ProcessManager.aspx")
    //        {
    //            Response.Redirect("ProcessManager.aspx");
    //        }
    //        else
    //        {
    //            Response.Redirect("Production.aspx");
    //        }
    //    }

    //}
}