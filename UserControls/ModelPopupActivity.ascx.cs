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

public partial class UserControls_ModelPopupActivity : System.Web.UI.UserControl
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
    [BrowsableAttribute(true)]
    public int ProcessObjectId
    {
        set
        {
            ViewState["poId"] = value;
            FillActivity();
            //ClearControl();/// for clearing control for every postback            
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Visible = false;

        if (!IsPostBack)
        {
            FillActivity();
        }       

        SetSession();
    }

    public void FillActivity()
    {
        int ProcessObjID = Convert.ToInt32(ViewState["poId"]);

        string ActivityName = Activity.GetActivityNameByProcessObjId(ProcessObjID);
        txtActivityName.Text = ActivityName;
    }

    protected void btnUpdateActivity_Click(object sender, EventArgs e)
    {
        AddAttribute();
        ModelPopupActivity.Show();
    }

    public void AddAttribute()
    {
        if (Activity.GetDuplicateCheck(txtActivityName.Text.Trim(), Convert.ToInt32(ViewState["poId"])))
        {
            MasterPage mstr = this.Parent.Page.Master as MasterPage;
            TreeView mastertreeview = (TreeView)mstr.FindControl("TreeView1");
            if (mastertreeview.SelectedNode != null)
                ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
            tbl_ProcessObject processObj = new tbl_ProcessObject();
            processObj.ProcessObjName = txtActivityName.Text.Trim();

            //int ProcessObjId = 0;
            // ProcessObjId = ProcessData.GetMaxProcessObjId(ProcessId);
            // ViewState["ProcessObjID"] = ProcessObjId;
            if (Convert.ToInt32(ViewState["poId"]) > -1)
            {
                processObj.ProcessObjID = Convert.ToInt32(ViewState["poId"]);
            }
            // AttributeTbl.ProcessObjectID = this.CInt32(ViewState["ProcessObjID"]);

            processObj.ProcessID = ProcessId;
            processObj.CreatedDate = DateTime.Now;

            if (this.EditIDINT > 0)
            {
                processObj.ModifiedDate = DateTime.Now;
            }

            bool result = false;
            result = Activity.UpdateActivityName(processObj);

            if (result == true)
            {
                //string script = "alert(\"Saved successfully!\");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //              "ServerControlScript", script, true);
                lblMsg.Visible = true;
                lblMsg.Text = "Activity renamed successfully!";
                lblMsg.CssClass = "msgSucess";

            }
            else
            {

                //string script = "alert(\"Error on saving data.!\");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //              "ServerControlScript", script, true);
                lblMsg.Text = "Error on saving data.!";
                lblMsg.CssClass = "msgError";
                lblMsg.Visible = true;
            }
        }
        else
        {
            //lblMsg.Text = "This Activity already exists.!";
            //lblMsg.CssClass = "msgError";
            //lblMsg.Visible = true;
        }
        //ClearControl();
        this.EditIDINT = 0; //// it will set edit mode false
        btnUpdateActivity.Text = "Update";

    }

    //public void ClearControl()
    //{
    //    txtActivityName.Text = "";
    //}

    public int EditIDINT
    {
        get
        {
            if (ViewState["EditIDInt"] != null)
            {
                return (int)ViewState["EditIDInt"];
            }
            return 0;
        }
        set { ViewState["EditIDInt"] = value; }
    }

    public Guid EditDataID
    {
        get
        {
            if (ViewState["EditID"] != null)
            {
                return (Guid)ViewState["EditID"];
            }
            return Guid.Empty;
        }
        set { ViewState["EditID"] = value; }
    }

    private void SetSession()
    {
        MasterPage mstr = this.Parent.Page.Master as MasterPage;
        TreeView mastertreeview = (TreeView)mstr.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
        {
            Session["SelectedNode"] = mastertreeview.SelectedNode.ValuePath;
            Session["SelectedNodeValue"] = mastertreeview.SelectedNode.Value;
        }
    }
}