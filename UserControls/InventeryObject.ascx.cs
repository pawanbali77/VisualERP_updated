using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class UserControls_InventeryObject : System.Web.UI.UserControl
{
    [BrowsableAttribute(true)]
    public int ProcessObjectId
    {
        get { return 0; }
        set { BindInventoryData(value); }
    }
    private int _sourceTypeID = 1;
    [BrowsableAttribute(true)]
    public int SourceType
    {
        get { return _sourceTypeID; }
        set { _sourceTypeID = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void  BindInventoryData(int poid)
    {
        tbl_InvantoryTriangle ProcessObjInventory = new tbl_InvantoryTriangle();
        ProcessObjInventory = ProcessData.ProcessObjectInventoryByID(poid);////AttributeById will get Attribute by its id that is EditIDINT
        if (ProcessObjInventory != null)
        {
            ltrCT.Text = ProcessObjInventory.CT.ToString();
            ltrDoller.Text = ProcessObjInventory.Doller.ToString();
            ltrTime.Text = ProcessObjInventory.Time.ToString();
            txtInventoryName.Text = ProcessData.GetInventoryName(ProcessObjInventory.ProcessObjID);
            if (SourceType==2)
                ViewState["TargetObjID"] = poid;
            else
            ViewState["ProcessObjID"] = poid;
            deleteBtnTriangleid.ID = "lnkDeleteInventory_" + poid;
        }
    }
    //protected void deleteBtnTriangleid_Click(object sender, EventArgs e)
    //{
    //    int processobjId = 0;
    //    processobjId = this.CInt32(ViewState["ProcessObjID"]);
    //    if (processobjId > 0)
    //    {

    //        int ProcessId = ProcessData.GetProcessIdByPoid(processobjId);
    //        int position = ProcessData.GetPositionByPoid(processobjId);
    //        bool decrease = false;
    //        decrease = ProcessData.DecreaseNextRowsPosition(ProcessId, position);
    //        if (decrease == true)
    //        {
    //            // position updated successfully
    //        }
    //        bool result = false;
    //        result = InventeryData.DeleteInventeryProcessObjByID(processobjId); ////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables
    //        bool result1 = false;
    //        result1 = ProcessData.DeleteProcessObjDataByID(processobjId);
    //        string absolutepath = Request.Url.AbsolutePath;
    //        string returnurl = absolutepath.Substring(absolutepath.LastIndexOf('/') + 1);
    //        if (returnurl == "ProcessManager.aspx")
    //        {
    //            Response.Redirect("ProcessManager.aspx");
    //        }
    //        else
    //        {
    //            //Response.Redirect("EnterPriseManager.aspx");
    //            Response.Redirect("Production.aspx");
    //        }
    //    }
    //}
}