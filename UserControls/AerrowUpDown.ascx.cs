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

public partial class UserControls_AerrowUpDown : System.Web.UI.UserControl
{
    #region
    int SystemId = 0;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMsg.Visible = false;
            //addBtnHandOff.OnClientClick = String.Format("fnClickUpdate('{0}','{1}')", addBtnHandOff.UniqueID, "");
            FillddlFrom();
            FilldllTo();
            ClearControl();
        }
        //lblprocessName.Text = "";
       //  TextChange();
    }
    //public void TextChange()
    //{
    //    int typeCheck = this.CInt32(Session["TypeID"]);
    //    if (typeCheck==1)
    //    {
    //        lblprocessName.Text = "Output Process";
    //    }
    //    else
    //    {
    //        lblprocessName.Text = "Input Process";
    //    }
    //}
    protected void addBtnHandOff_Click1(object sender, EventArgs e)
    {
        ModelPopupAerrow.Show();
        AddSystemIOData();

    }

    public void AddSystemIOData()
    {
        if (HandOffData.GetDuplicateCheck(this.CInt32(ddlfrmActivity.SelectedValue), this.CInt32(ddltoActivity.SelectedValue), this.CInt32(Session["SystemId"])))
        {
            tbl_SystemIO TblSystemIObj = new tbl_SystemIO();
            TblSystemIObj.FromActivityID = this.CInt32(ddlfrmActivity.SelectedValue);
            TblSystemIObj.ToActivityID = this.CInt32(ddltoActivity.SelectedValue);
            if (Session["TypeID"] != null)
            {
                TblSystemIObj.Type = this.CInt32(Session["TypeID"]);
            }
            if (Session["SystemId"] != null)
            {

                TblSystemIObj.SystemID = this.CInt32((Session["SystemId"]));
            }
            TblSystemIObj.CreatedDate = DateTime.Now;
            bool result = false;
            // result =
            result = HandOffData.SaveSystemIOData(TblSystemIObj);  ////SaveInputData will dave input link in database table information input

            if (result == true)  // if record is updated or inserted
            {
                ClearControl();
                ModelPopupAerrow.Hide();
                Response.Redirect("EnterPriseManager.aspx");
                //lblMsg.Visible = true;
                //lblMsg.Text = "TFG data Saved successfully!";
                //lblMsg.CssClass = "msgSucess";
            }
            else
            {
                //lblMsg.Text = "Error on saving data.!";
                //lblMsg.CssClass = "msgError";
                //lblMsg.Visible = true;
            }
        }
        else
        {
            lblMsg.Text = "This record already exists.!";
            lblMsg.CssClass = "msgError";
            lblMsg.Visible = true;
        }
    }
    public void FillddlFrom()
    {
        ddlfrmName.Items.Clear();
        ddlfrmName.Items.Add(new ListItem("Select", "0"));
        if(Session["SystemId"]!=null)
        {
            SystemId=this.CInt32((Session["SystemId"]));
        }
        foreach (tbl_Process ProcessCollection in ProcessData.GetProcessCollection(SystemId))
        {
            ddlfrmName.Items.Add(new ListItem(ProcessCollection.ProcessName, ProcessCollection.ProcessID.ToString()));
        }
    }

    public void FilldllTo()
    {
        ddlToName.Items.Clear();
        ddlToName.Items.Add(new ListItem("Select", "0"));
        if (Session["SystemId"] != null)
        {
            SystemId = this.CInt32((Session["SystemId"]));
        }
        foreach (tbl_Process ProcessCollection in ProcessData.GetProcessCollection(SystemId))
        {
            ddlToName.Items.Add(new ListItem(ProcessCollection.ProcessName, ProcessCollection.ProcessID.ToString()));
        }
    }

    public void ClearControl()
    {
        ddlfrmName.SelectedIndex = 0;
        ddlToName.SelectedIndex = 0;
        ddlfrmActivity.SelectedIndex = 0;
        ddltoActivity.SelectedIndex = 0;
        lblMsg.Visible = false;
    }

    public void FillddlfrmActivity(int ProcessId)
    {
        ddlfrmActivity.Items.Clear();
        ddlfrmActivity.Items.Add(new ListItem("Select", "0"));
        foreach (tbl_ProcessObject ProcessActivityCollection in ProcessData.GetProcessObjActvityCollection(ProcessId))
        {
            ddlfrmActivity.Items.Add(new ListItem(ProcessActivityCollection.ProcessObjName, ProcessActivityCollection.ProcessObjID.ToString()));
        }
    }
    public void FillddltoActivity(int ProcessId)
    {
        ddltoActivity.Items.Clear();
        ddltoActivity.Items.Add(new ListItem("Select", "0"));
        foreach (tbl_ProcessObject ProcessActivityCollection in ProcessData.GetProcessObjActvityCollection(ProcessId))
        {
            ddltoActivity.Items.Add(new ListItem(ProcessActivityCollection.ProcessObjName, ProcessActivityCollection.ProcessObjID.ToString()));
        }
    }
    protected void ddlfrmName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlfrmName.SelectedIndex > 0)
        {
            FillddlfrmActivity(Convert.ToInt32(ddlfrmName.SelectedValue));

        }
        ModelPopupAerrow.Show();
    }
    protected void ddlToName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlToName.SelectedIndex > 0)
        {
            FillddltoActivity(Convert.ToInt32(ddlToName.SelectedValue));

        }
        ModelPopupAerrow.Show();
    }
    //protected void imgcloseUPDwn_Click(object sender, ImageClickEventArgs e)
    //{
    //    ClearControl();
    //}
}