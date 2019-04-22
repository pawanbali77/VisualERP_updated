/*****************************************************************************
1. * Copyright 1999, All rights reserved, For internal use only
* FILE: ModelPopupMchUC.ascx.cs
* PROJECT: VisualERP
* MODULE:MachineManager
* AUTHOR: Ratnesh
* DATE: 16/9/2013
* Description: it contains the Machine data.
*
* Notes: Linq query is used. Database designed in dbml file that contains all stored procedures, database table and functions etc.
*
* Special Instructions: HTML script input is not allowed.
*
* REVISION HISTORY
* Date: By: Description:
*
*****************************************************************************/
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

public partial class UserControls_ModelPopupMchUC : System.Web.UI.UserControl
{
    private int _sourceTypeID = 1;
    [BrowsableAttribute(true)]
    public int SourceType
    {
        get { return _sourceTypeID; }
        set { _sourceTypeID = value; }
    }
    /// <summary>
    /// Its browsable var for attending the Process object id.
    /// </summary>
    [BrowsableAttribute(true)]
    public int ProcessObjectId
    {
        set
        {
            ViewState["poId"] = value;
            ClearControl(); //// for clearing control for every postback
            ResetBindingGrid(); //// for binding grid view data
            lblMsg.Visible = false;

        }
    }
    /// <summary>
    /// For binding the grid on behalf of process object id.
    /// </summary>
    public void ResetBindingGrid()
    {
        ViewState["sortBy"] = "CreatedDate";
        ViewState["isAsc"] = "1";
        BindGridMachine(); //bind gridview sorted by Created Date
    }
    /// <summary>
    /// Its method For binding the grid on behalf of process object id.
    /// </summary>
    public void BindGridMachine()
    {
        if (Convert.ToInt32(ViewState["poId"]) > -1)
        {
            gridMch.DataSource = MachineData.GetMachineDataByObjId(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ViewState["poId"]));
            gridMch.DataBind();
        }
    }
    /// <summary>
    /// Its page load event which handle the panels and Grid binding data.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        PanalMch.Visible = true;
        PanelMchRepair.Visible = false;
        lnkbtnOpenDivMachine.Visible = true;
        gridMch.Visible = true;
        ltrMachine.Text = "Machine List";
        //if (ViewState["MachineId"] != null)
        //{
        //    PanalMch.Visible = false;
        //    PanelMchRepair.Visible = true;
        //}
        if (!IsPostBack)
        {
            ClearControl(); //// for clearing control for every postback
            ResetBindingGrid(); //// for binding grid view data

        }
    }
    /// <summary>
    /// it will fill Machine data data by process object id 
    /// </summary>
    public void FillMachineDataById()
    {
        lblMsg.Visible = false;
        tbl_MachineList tblMchData = new tbl_MachineList();
        tblMchData = MachineData.MachineDataById(this.EditIDINT);////TFGById will get TFG by its id that is EditIDINT

        if (Convert.ToInt32(ViewState["poId"]) > -1)
        {
            tblMchData.ProcessObjectID = Convert.ToInt32(ViewState["poId"]);
        }
        if (tblMchData != null)
        {
            VisualERPDataContext ObjData = new VisualERPDataContext();
            var qry = (from x in ObjData.tbl_ProcessObjects
                       where x.ProcessObjID == tblMchData.ProcessObjectID
                       select x).FirstOrDefault();
            if (qry != null)
            {

                txtProcessObjName.Text = qry.ProcessObjName;
            }
            txtMchName.Text = tblMchData.MachineName.ToString();

            txtMchType.Text = tblMchData.MachineType.ToString();
            txtPMId.Text = tblMchData.PMScheduleID.ToString();
            txtMtbf.Text = tblMchData.MTBF.ToString();
            txtMttr.Text = tblMchData.MTTR.ToString();
            txtMainCost.Text = tblMchData.MaintenanceCost.ToString();
            txtPurchasePrice.Text = tblMchData.PurchasePrice.ToString();
            txtbookVal.Text = tblMchData.BookValue.ToString();
            txtRemainLife.Text = tblMchData.RemainingLife.ToString();
            txtManualId.Text = tblMchData.ManualID.ToString();
            txtPartlistId.Text = tblMchData.PartsListID.ToString();
            ViewState["MachineImage"] = tblMchData.MachinePhoto;
            if (tblMchData.MachinePhoto != null && tblMchData.MachinePhoto != "")
            {
                hyview.Visible = true;
                imagerequiredField.Visible = false;
                string Url = this.RootPath() + "Handler/ResizerImage.ashx?path=" + tblMchData.MachinePhoto.ToString() + "&w=300&h=200&t=c";
                hyview.Attributes.Add("onclick", "javascript: OpenNewWindow('" + Url + "')");
                //ModelMachine.Show();
                // hyview.NavigateUrl = book.CoverPic;
                // hyview.NavigateUrl = this.RootPath() + "Handler/ResizerImage.ashx?path=" + tblMchData.MachinePhoto.ToString() + "&w=300&h=200&t=c";

            }
            else
            {
                hyview.Visible = false;
            }

            submitBtn.Text = "Update";
        }
    }
    /// <summary>
    /// For Pageing purpose we used this event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridMch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridMch.PageIndex = e.NewPageIndex;
        this.BindGridMachine(); //// bind grid view 
        ModelMachine.Show();
    }
    /// <summary>
    /// For Deletinmg the row on behalf of machine id.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridMch_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ////idDelete will get Attribute Id data for selected grid view row on delete button
        int idDelete = this.CInt32(((ImageButton)gridMch.Rows[e.RowIndex].FindControl("deleteBtn")).CommandArgument);
        if (idDelete > 0)
        {
            bool result = false;
            result = MachineData.DeleteMachineDatabyId(idDelete); ////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables
            if (result)
            {
                bool resultLog = false;
                resultLog = MachineRepairData.DeleteMachineRepairDataByMachineID(idDelete);
                lblMsg.Visible = true;
                lblMsg.Text = "This record is Deleted.!";
                lblMsg.CssClass = "msgSucess";
                lblMsg.Style.Add("width", "100%!important");
                lblMsg.Visible = true;

                //// result is true
            }
            else
            {

                lblMsg.Visible = true;
                lblMsg.Text = "Error on Deleting data.!";
                lblMsg.CssClass = "msgError";
                lblMsg.Style.Add("width", "100%!important");
                lblMsg.Visible = true;
                //// result is false
            }
            ClearControl();
            ResetBindingGrid();
            this.EditIDINT = 0;
            ModelMachine.Show();

        }
    }
    /// <summary>
    /// For Editing the row on behalf of machine id.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridMch_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OpenPopup", "javascript:OpenPopup('divMachineAdd');", true);
        lnkbtnOpenDivMachine.Visible = false;
        gridMch.Visible = false;
        ClearControl();
        this.EditIDINT = this.CInt32(((Literal)gridMch.Rows[e.NewEditIndex].FindControl("litMachineId")).Text); //// it will get link id for which we are editing
        FillMachineDataById();  //// it will fill data for Machine id
        ModelMachine.Show();
    }
    protected void gridMch_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    /// <summary>
    /// For sorting the columns on behalf of column click .
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridMch_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (e.SortExpression == ViewState["sortBy"].ToString())
        {
            if (ViewState["isAsc"].ToString() == "1")
                ViewState["isAsc"] = "0";
            else
                ViewState["isAsc"] = "1";
        }
        else
        {
            ViewState["isAsc"] = "0";
        }
        ViewState["sortBy"] = e.SortExpression;
        BindGridMachine();
        ModelMachine.Show();
    }
    /// <summary>
    /// For Creating the row
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridMch_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.Header)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                if (cell.HasControls())
                {
                    LinkButton button = cell.Controls[0] as LinkButton;
                    if (button != null)
                    {
                        Image image = new Image();
                        image.CssClass = "gridSortImage";
                        if (ViewState["sortBy"].ToString() == button.CommandArgument)
                        {
                            if (ViewState["isAsc"].ToString() == "1")
                                image.ImageUrl = "~\\Images\\ArrowDown.png";
                            else
                                image.ImageUrl = "~\\Images\\ArrowUp.png";
                            cell.Controls.Add(image);
                        }

                    }
                }
            }
        }
    }
    /// <summary>
    /// Its the row command event which is handle the command name
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridMch_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Repairlog")
        {
            // Response.Redirect(ClassGeneral.RootPath() + "admin/MemberActivity.aspx?userID=" + e.CommandArgument.ToString());
            int MachineId = 0;
            MachineId = this.CInt32(e.CommandArgument);
            ViewState["MachineId"] = MachineId;
            if (ViewState["MachineId"] != null)
            {
                PanalMch.Visible = false;
                PanelMchRepair.Visible = true;

            }

            //GenralFunction.ShowHidePanels(PanelMchRepair,PanalMch);
            ResetBindingGridRepaier(MachineId);
            ModelMachine.Show();
            lblMsg.Visible = false;
        }

        else if (e.CommandName == "Edit")
        {

        }
        else
        {
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
    /// <summary>
    /// For clearing all control data 
    /// </summary>
    public void ClearControl()
    {
        txtMchName.Text = "";
        txtMchType.Text = "";
        txtPMId.Text = "";
        txtMtbf.Text = "";
        txtMttr.Text = "";
        txtMainCost.Text = "";
        txtPurchasePrice.Text = "";
        txtbookVal.Text = "";
        txtRemainLife.Text = "";
        txtManualId.Text = "";
        txtPartlistId.Text = "";
        //lblMsg.Visible = false;

    }
    /// <summary>
    /// Its the link button which is contain the fields so when ever click this button then it will
    /// open the popup.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkbtnOpenDivMachine_Click(object sender, EventArgs e)
    {
        this.EditIDINT = 0;
        hyview.Visible = false;
        //gridMch.Visible = false;
        ViewState["MachineImage"] = "";
        ClearControl();
        if (Convert.ToInt32(ViewState["poId"]) > -1)
        {
            int proId = Convert.ToInt32(ViewState["poId"]);

            VisualERPDataContext ObjData = new VisualERPDataContext();
            var qry = (from x in ObjData.tbl_ProcessObjects
                       where x.ProcessObjID == proId
                       select x).FirstOrDefault();
            if (qry != null)
            {

                txtProcessObjName.Text = qry.ProcessObjName;
            }
        }
        if (lblMsg.Visible == true)
        {
            lblMsg.Visible = false;
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OpenPopup", "javascript:OpenPopup('divMachineAdd');", true);
        lnkbtnOpenDivMachine.Visible = false;
        gridMch.Visible = false;
        submitBtn.Text = "Add Machine";
        ModelMachine.Show();
        ltrMachine.Text = "Add Machine";
    }
    /// <summary>
    /// For adding the machie  data on behalf of machine id
    /// </summary>
    protected void submitBtn_Click(object sender, EventArgs e)
    {
        AddMachineData();
        lnkbtnOpenDivMachine.Visible = true;
        gridMch.Visible = true;//// it will add entered TFG data in and also update 
        ModelMachine.Show();
        ltrMachine.Text = "Machine List";
    }
    /// <summary>
    ///Its the method For adding/updating the machie  data on behalf of machine id
    /// </summary>
    public void AddMachineData()
    {
        if (MachineData.GetDuplicateCheckMachineName(txtMchName.Text.Trim(), Convert.ToInt32(ViewState["poId"]), this.EditIDINT)) //// it will check if Machine name is already exist
        {
            tbl_MachineList tblMachineFields = new tbl_MachineList();
            if (Convert.ToInt32(ViewState["poId"]) > -1)
            {
                tblMachineFields.ProcessObjectID = Convert.ToInt32(ViewState["poId"]);
            }

            tblMachineFields.MachineName = txtMchName.Text.Trim();
            tblMachineFields.MachineType = txtMchType.Text.Trim();
            tblMachineFields.PMScheduleID = Convert.ToInt32(txtPMId.Text.Trim());
            tblMachineFields.MTBF = txtMtbf.Text.Trim();
            tblMachineFields.MTTR = txtMttr.Text.Trim();
            tblMachineFields.MaintenanceCost = Convert.ToDouble(txtMainCost.Text.Trim());
            tblMachineFields.PurchasePrice = Convert.ToDouble(txtPurchasePrice.Text.Trim());
            tblMachineFields.BookValue = Convert.ToDouble(txtbookVal.Text.Trim());
            tblMachineFields.RemainingLife = txtRemainLife.Text.Trim();
            tblMachineFields.ManualID = Convert.ToInt32(txtManualId.Text.Trim());
            tblMachineFields.PartsListID = Convert.ToInt32(txtPartlistId.Text.Trim());
            tblMachineFields.CreatedDate = DateTime.Now;
            if (this.EditIDINT > 0)    //// for edit mode
            {
                tblMachineFields.MachineID = this.EditIDINT;
                tblMachineFields.ModifiedDate = DateTime.Now;
            }
            string MachinePhoto = "";

            if (fileUpMch.PostedFile.ContentLength > 0)
            {
                if (fileUpMch.PostedFile != null)
                {


                    MachinePhoto = GenralFunction.UploadBookImage(fileUpMch, Resources.Message.Path, DateTime.Now.Ticks.ToString(), "Hello");

                }
            }
            else
            {
                MachinePhoto = ViewState["MachineImage"].ToString();
            }
            tblMachineFields.MachinePhoto = MachinePhoto;
            bool result = false;
            // result =
            result = MachineData.SaveMachineData(tblMachineFields);  ////SaveInputData will dave input link in database table information input

            if (result == true)  // if record is updated or inserted
            {
                //string script = "alert(\"Saved successfully!\");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //              "ServerControlScript", script, true);
                lblMsg.Visible = true;
                lblMsg.Text = "Machine data Saved successfully!";
                lblMsg.CssClass = "msgSucess";
                lblMsg.Style.Add("width", "100%!important");
                lblMsg.Visible = true;
            }
            else
            {
                //string script = "alert(\"Error on saving data.!\");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //              "ServerControlScript", script, true);
                lblMsg.Text = "Error on saving data.!";
                lblMsg.CssClass = "msgError";
                lblMsg.Style.Add("width", "100%!important");
                lblMsg.Visible = true;
            }
        }
        else
        {
            //string script = "alert(\"This record already exists.!\");";
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //              "ServerControlScript", script, true);
            lblMsg.Text = "This record already exists.!";
            lblMsg.CssClass = "msgError";
            lblMsg.Style.Add("width", "100%!important");
            lblMsg.Visible = true;
        }

        ResetBindingGrid();  //// bind grid view after new row inserted or row updated
        ClearControl();
        this.EditIDINT = 0; //// it will set edit mode false
        submitBtn.Text = "Add Machine";
        //lnkbtnOpenDivMachine.Visible = true;
    }
    /// <summary>
    /// Its the Back button which used for the back button functionality
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void bckBtnMch_Click(object sender, EventArgs e)
    {
        lnkbtnOpenDivMachine.Visible = true;
        //PanalMch.Visible = true;
        ResetBindingGrid();
        ModelMachine.Show();
        ltrMachine.Text = "Machine List";
    }

    //////////////////////////////////////////////////////////////////*****************Machin Repair Data**********************////////////////////////////////////////////////
    /// <summary>
    /// Its the link button which is contain the fields so when ever click this button then it will
    /// open the popup.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkBtnOpenDivMchRepair_Click(object sender, EventArgs e)
    {
        PanelMchRepair.Visible = true;
        PanalMch.Visible = false;
        gridMchRepair.Visible = false;

        this.EditIDINT = 0;
        ClearControlMchRepair();
        if (lblMsg.Visible == true)
        {
            lblMsg.Visible = false;
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OpenPopup", "javascript:OpenPopup('divMachineAddRepair');", true);
        lnkBtnOpenDivMchRepair.Visible = false;
        btnSubmitMchRepair.Text = "Add Repair";
        ModelMachine.Show();
        ltrMachine.Text = "Add Repair log";

    }
    /// <summary>
    /// For adding the machie repair data on behalf of machine id
    /// </summary>
    protected void btnSubmitMchRepair_Click(object sender, EventArgs e)
    {
        AddMachineRepairData();  //// it will add entered Machine Repair data in and also update 
        lnkBtnOpenDivMchRepair.Visible = true;
        gridMchRepair.Visible = true;
        ModelMachine.Show();
        PanelMchRepair.Visible = true;
        PanalMch.Visible = false;
        ltrMachine.Text = "Repair log list";
    }
    /// <summary>
    ///Its the method For adding the machie repair data on behalf of machine id
    /// </summary>
    public void AddMachineRepairData()
    {

        tbl_RepairLog tblMachineRepair = new tbl_RepairLog();
        tblMachineRepair.MachineID = this.CInt32(ViewState["MachineId"]);
        tblMachineRepair.Critical = txtCritical.Text.Trim();
        tblMachineRepair.TTR = txtTTR.Text.Trim();
        tblMachineRepair.SkillTypeOfRepair = txtSkillTypeOfRepair.Text.Trim();
        tblMachineRepair.TypeOfRepair = txtTypeOfRepair.Text.Trim();
        tblMachineRepair.ActualRepair = txtActualRepair.Text.Trim();
        tblMachineRepair.CostOfRepairParts = Convert.ToDouble(txtCostOfRepairParts.Text.Trim());
        tblMachineRepair.CostOfRepairLabor = Convert.ToDouble(txtCostOfRepairLabor.Text.Trim());
        tblMachineRepair.CostOfRepairOutsource = Convert.ToDouble(txtCostOfRepairOutsource.Text.Trim());
        tblMachineRepair.Scheduled_Unscheduled = txtScheduledUnscheduled.Text.Trim();
        tblMachineRepair.DownTime = txtDownTime.Text.Trim();
        tblMachineRepair.Preventive_Predictive_Reactive = txtPreventivePredictiveReactive.Text.Trim();
        tblMachineRepair.RootCause = txtRootCause.Text.Trim();
        tblMachineRepair.Countermeasure = txtCounterMeasure.Text.Trim();
        tblMachineRepair.CreatedDate = DateTime.Now;
        if (this.EditIDINT > 0)    //// for edit mode
        {
            tblMachineRepair.MachineRepairID = this.EditIDINT;
            tblMachineRepair.ModifiedDate = DateTime.Now;
        }
        bool result = false;
        // result =
        result = MachineRepairData.SaveMachineRepairData(tblMachineRepair);  ////SaveInputData will dave input link in database table information input

        if (result == true)  // if record is updated or inserted
        {
            //string script = "alert(\"Saved successfully!\");";
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //              "ServerControlScript", script, true);
            lblMsg.Visible = true;
            lblMsg.Text = "Machine Repair data Saved successfully!";
            lblMsg.CssClass = "msgSucess";
            lblMsg.Style.Add("width", "100%!important");
            lblMsg.Visible = true;
        }
        else
        {
            //string script = "alert(\"Error on saving data.!\");";
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //              "ServerControlScript", script, true);
            lblMsg.Text = "Error on saving data.!";
            lblMsg.CssClass = "msgError";
            lblMsg.Style.Add("width", "100%!important");
            lblMsg.Visible = true;
        }

        ResetBindingGridRepaier(this.CInt32(ViewState["MachineId"])); //// bind grid view after new row inserted or row updated
        ClearControlMchRepair();
        this.EditIDINT = 0; //// it will set edit mode false
        btnSubmitMchRepair.Text = "Add Machine";
        lnkBtnOpenDivMchRepair.Visible = true;
    }
    /// <summary>
    /// Its the Back button which used for the back button functionality
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void bckBtnMchRepair_Click(object sender, EventArgs e)
    {
        // //PanelMchRepair.Visible = true;
        // //PanalMch.Visible = false;
        // lnkBtnOpenDivMchRepair.Visible = true;
        // this.ResetBindingGridRepaier(this.CInt32(ViewState["MachineId"]));

        // ltrMachine.Text = "Repair List";
        //// PanalMch.Visible = true;
        // //PanelMchRepair.Visible = false;
        // ModelMachine.Show();


        if (ViewState["MachineId"] != null)
        {
            lnkBtnOpenDivMchRepair.Visible = true;
            PanalMch.Visible = false;
            PanelMchRepair.Visible = true;
            ResetBindingGridRepaier(this.CInt32(ViewState["MachineId"]));
            gridMchRepair.Visible = true;
            ModelMachine.Show();
            lblMsg.Visible = false;
        }
    }
    /// <summary>
    /// Foe pageing purpose which is used and binding the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridMchRepair_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridMchRepair.PageIndex = e.NewPageIndex;
        this.ResetBindingGridRepaier(this.CInt32(ViewState["MachineId"])); //// bind grid view 
        ModelMachine.Show();
    }
    /// <summary>
    /// For Grid Row Deleting Event which is handle the Machine repair ID which for deleted.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridMchRepair_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PanelMchRepair.Visible = true;
        PanalMch.Visible = false;
        lnkBtnOpenDivMchRepair.Visible = true;
        ////idDelete will get Machine Repair Id data for selected grid view row on delete button
        int idDelete = this.CInt32(((ImageButton)gridMchRepair.Rows[e.RowIndex].FindControl("deleteBtnMchRepair")).CommandArgument);
        if (idDelete > 0)
        {
            bool result = false;
            result = MachineRepairData.DeleteMachineRepairDataByID(idDelete); ////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables
            if (result)
            {

                lblMsg.Visible = true;
                lblMsg.Text = "This record is Deleted.!";
                lblMsg.CssClass = "msgSucess";
                lblMsg.Style.Add("width", "100%!important");
                lblMsg.Visible = true;
                //// result is true
            }
            else
            {

                lblMsg.Visible = true;
                lblMsg.Text = "Error on Deleting data.!";
                lblMsg.CssClass = "msgError";
                lblMsg.Style.Add("width", "100%!important");
                lblMsg.Visible = true;
                //// result is false
            }
            ClearControlMchRepair();
            ResetBindingGridRepaier(this.CInt32(ViewState["MachineId"]));
            this.EditIDINT = 0;
            ModelMachine.Show();
        }
    }
    /// <summary>
    /// For Grid Row edting Event which is handle the Edit ID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridMchRepair_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OpenPopup", "javascript:OpenPopup('divMachineAddRepair');", true);
        lnkBtnOpenDivMchRepair.Visible = false;
        ClearControlMchRepair();
        this.EditIDINT = this.CInt32(((Literal)gridMchRepair.Rows[e.NewEditIndex].FindControl("litMchRepairId")).Text); //// it will get link id for which we are editing
        FillMchRepairDataByMchRepairId();  //// it will fill data for Machine id
        PanelMchRepair.Visible = true;
        PanalMch.Visible = false;
        gridMchRepair.Visible = false;
        ModelMachine.Show();
    }
    /// <summary>
    /// Fill All Data on Behalf of Machine ID
    /// </summary>
    public void FillMchRepairDataByMchRepairId()
    {

        lblMsg.Visible = false;
        tbl_RepairLog tblRepairlog = new tbl_RepairLog();
        tblRepairlog = MachineRepairData.MachineRepairDataById(this.EditIDINT);////Machine RepairID will get Machine Repair Data by its id that is EditIDINT

        if (tblRepairlog != null)
        {
            txtCritical.Text = tblRepairlog.Critical.ToString();

            txtTTR.Text = tblRepairlog.TTR.ToString();
            txtSkillTypeOfRepair.Text = tblRepairlog.SkillTypeOfRepair.ToString();
            txtTypeOfRepair.Text = tblRepairlog.TypeOfRepair.ToString();
            txtActualRepair.Text = tblRepairlog.ActualRepair.ToString();
            txtCostOfRepairParts.Text = tblRepairlog.CostOfRepairParts.ToString();
            txtCostOfRepairLabor.Text = tblRepairlog.CostOfRepairLabor.ToString();
            txtCostOfRepairOutsource.Text = tblRepairlog.CostOfRepairOutsource.ToString();
            txtScheduledUnscheduled.Text = tblRepairlog.Scheduled_Unscheduled.ToString();
            txtDownTime.Text = tblRepairlog.DownTime.ToString();
            txtPreventivePredictiveReactive.Text = tblRepairlog.Preventive_Predictive_Reactive.ToString();
            txtRootCause.Text = tblRepairlog.RootCause.ToString();
            txtCounterMeasure.Text = tblRepairlog.Countermeasure.ToString();
            btnSubmitMchRepair.Text = "Update";
            ltrMachine.Text = "Edit Repair log";
        }

    }
    protected void gridMchRepair_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    /// <summary>
    /// For sorting Grid columns
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridMchRepair_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (e.SortExpression == ViewState["sortByRepair"].ToString())
        {
            if (ViewState["isAscRepair"].ToString() == "1")
                ViewState["isAscRepair"] = "0";
            else
                ViewState["isAscRepair"] = "1";
        }
        else
        {
            ViewState["isAscRepair"] = "0";
        }
        ViewState["sortByRepair"] = e.SortExpression;
        ResetBindingGridRepaier(this.CInt32(ViewState["MachineId"]));
    }
    /// <summary>
    /// For Grid Repair Row Creation
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridMchRepair_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.Header)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                if (cell.HasControls())
                {
                    LinkButton button = cell.Controls[0] as LinkButton;
                    if (button != null)
                    {
                        Image image = new Image();
                        image.CssClass = "gridSortImage";
                        if (ViewState["sortByRepair"].ToString() == button.CommandArgument)
                        {
                            if (ViewState["isAscRepair"].ToString() == "1")
                                image.ImageUrl = "~\\Images\\ArrowDown.png";
                            else
                                image.ImageUrl = "~\\Images\\ArrowUp.png";
                            cell.Controls.Add(image);
                        }

                    }
                }
            }
        }
    }
    /// <summary>
    /// For Binding the Grid 
    /// </summary>
    /// <param name="MachineId"></param>
    public void ResetBindingGridRepaier(int MachineId)
    {

        ViewState["sortByRepair"] = "CreatedDate";
        ViewState["isAscRepair"] = "1";
        gridMchRepair.DataSource = MachineRepairData.GetMachineRepairDataByMchId(this.CBool(ViewState["isAscRepair"]), ViewState["sortByRepair"].ToString(), MachineId);
        gridMchRepair.DataBind();
        ltrMachine.Text = "Repair list";
        PanelMchRepair.Visible = true;

    }
    /// <summary>
    /// For clearing all control data 
    /// </summary>
    public void ClearControlMchRepair()
    {
        txtCritical.Text = "";
        txtTTR.Text = "";
        txtSkillTypeOfRepair.Text = "";
        txtTypeOfRepair.Text = "";
        txtActualRepair.Text = "";
        txtCostOfRepairParts.Text = "";
        txtCostOfRepairLabor.Text = "";
        txtCostOfRepairOutsource.Text = "";
        txtScheduledUnscheduled.Text = "";
        txtDownTime.Text = "";
        txtPreventivePredictiveReactive.Text = "";
        txtRootCause.Text = "";
        txtCounterMeasure.Text = "";
    }

}