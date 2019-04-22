using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.ComponentModel;

public partial class UserControls_InventoryUC : System.Web.UI.UserControl
{
    #region
    int ProcessId = 0;
    #endregion
    private int _sourceTypeID = 1;
    [BrowsableAttribute(true)]
    public int SourceType
    {
        get { return _sourceTypeID; }
        set { _sourceTypeID = value; }
    }
    private System.Delegate _delWithParam;
    public Delegate PageMethodWithParamRef
    {
        set 
        { 
            _delWithParam = value;
            Session["delWithParam"] = _delWithParam;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void addInventeryBtn_Click(object sender, EventArgs e)
    {
        SaveProcesObjectInventery();
        //UpdatePanel11.Update();
        InvokeLoad();

        UpdatePanel updat = (UpdatePanel)this.Parent.FindControl("Uppnl1");
        //updat.Update();
        
       
    }

    public void SaveProcesObjectInventery()
    {
        tbl_ProcessObject ProcessObjInventery = new tbl_ProcessObject();
        TreeView mastertreeview = (TreeView)this.Page.Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        ProcessObjInventery.ProcessID = ProcessId;
        ProcessObjInventery.CreatedDate = DateTime.Now;
        // if()
        ProcessObjInventery.Type = 1;
        bool result = false;
        result = ProcessData.SaveDumyProcessObject(ProcessObjInventery);
      

        int ProcessObjId = 0;
        ProcessObjId = ProcessData.GetMaxProcessObjId(ProcessId);
        ViewState["ProcessObjID"] = ProcessObjId;
        InsertInventeryData();

        
        //InvokeLoad();
        //ModelPopupInventery.Hide();
    }

    public void InsertInventeryData()
    {
        tbl_InvantoryTriangle ObjectInventery = new tbl_InvantoryTriangle();
        ObjectInventery.CT = this.CInt32(txtCT.Text.Trim());
        ObjectInventery.Doller = this.CInt32(txtdoller.Text.Trim());
        ObjectInventery.Time = this.CInt32(txttime.Text.Trim());
        ObjectInventery.ProcessObjID = this.CInt32(ViewState["ProcessObjID"]);
        ObjectInventery.CreatedDate = DateTime.Now;
        bool result = false;
        result = InventeryData.SaveProcessObjectInventery(ObjectInventery);
       
    }

    private void InvokeLoad()
    {
        //Parameter to a method is being made ready
        object[] obj = new object[2];
        obj[0] = "0";
        obj[1] = "Inventory";
        _delWithParam = (System.Delegate)Session["delWithParam"];
        _delWithParam.DynamicInvoke(obj);
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        ModelPopupInventery.Hide();
    }
}