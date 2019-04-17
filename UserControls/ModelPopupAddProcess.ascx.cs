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

public partial class UserControls_ModelPopupAddProcess : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            addProcessBtn.OnClientClick = String.Format("fnClickUpdate('{0}','{1}')", addProcessBtn.UniqueID, "");
            FillddlType();
        }
    }
    protected void addProcessBtn_Click1(object sender, EventArgs e)
    {
       // Page.Validate();
        if(Page.IsValid)
        {
        InsertProcess();
        ClearControl();
        }
       // ModelPopupProcess.Hide();
    }
    public void FillddlType()
    {
        ddlType.Items.Clear();
        ddlType.Items.Add(new ListItem("Select", "0"));
        foreach (tbl_TypeCollection typedata in TypeData.GetTypeCollection())
        {
            ddlType.Items.Add(new ListItem(typedata.TypeName, typedata.TypeID.ToString()));
        }
    }

    public void ClearControl()
    {
        txtProcessName.Text = "";
        //ddlSystem.SelectedValue = 0;
       // ddlSystem.SelectedIndex = 0;
    }
    public void InsertProcess()
    {
        if (ProcessData.GetDuplicateCheck(txtProcessName.Text.Trim(), this.EditIDINT))
        {
            tbl_Process ProcessObj = new tbl_Process();
            ProcessObj.ProcessName = txtProcessName.Text.Trim();
            //ProcessObj.SystemID = this.CInt32(ddlSystem.SelectedValue);
            ProcessObj.CreatedDate = DateTime.Now;
            if (this.EditIDINT > 0)
            {
                ProcessObj.ProcessID = this.EditIDINT;
                ProcessObj.ModifiedDate = DateTime.Now;
            }

            bool result = false;
            result = ProcessData.SaveProcessData(ProcessObj);

            if (result == true)
            {
                //string script = "alert(\"Saved successfully!\");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //              "ServerControlScript", script, true);
            }
            else
            {
                //string script = "alert(\"Error on saving data.!\");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //              "ServerControlScript", script, true);
            }
        }
        else
        {

            //string script = "alert(\"This record already exists.!\");";
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //              "ServerControlScript", script, true);
        }
    }

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
}