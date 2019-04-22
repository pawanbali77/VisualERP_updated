/*****************************************************************************
1. * Copyright 1999, All rights reserved, For internal use only
* FILE: ModelPopupTFGUC..ascx.cs
* PROJECT: VisualERP
* MODULE:ProcessManager
* AUTHOR: Jayant
* DATE: 19/8/2013
* Description: it contains the TFG data and tree data.
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

public partial class UserControls_ModelPopupTFGUC : System.Web.UI.UserControl
{

    private int _sourceType = 1;
    [BrowsableAttribute(true)]
    public int SourceType
    {
        get { return _sourceType; }
        set { _sourceType = value; }
    }

    [BrowsableAttribute(true)]
    public int ProcessObjectId
    {
        set
        {
            ViewState["poId"] = value;
            ClearControl(); //// for clearing control for every postback
            ResetBinding(); //// for binding grid view data
            FillddlTFGVendor();  //// FillddlTFGVendor
            FillddlCalibrationVendor(); ////FillddlCalibrationVendor
        }
    }

    /// <summary>
    /// this code will fire the for TFG Data.
    /// NAME: TFG data Population.
    /// Description: it will Displayed TFG Data.
    /// Subroutines Called: ---
    /// Parameters:no
    /// Returns:TFG Data
    /// Globals: no
    /// Design Document Reference: no
    /// Author: jayant
    /// Assumptions and Limitation: --
    /// Exception Processing: ---
    /// Date: By: Description: 19/8/2013
    /// </summary>
    /// <param name="">No Parameters</param>
    /// <returns>it will check post Back data and then Bind Grid View data And drop down</returns>
    protected void Page_Load(object sender, EventArgs e)
    {
        lnkbtnOpenDiv.Visible = true;
        lblMsg.Visible = false;
        if (!IsPostBack)
        {
            ClearControl(); //// for clearing control for every postback
            ResetBinding(); //// for binding grid view data
            FillddlTFGVendor();  //// FillddlTFGVendor
            FillddlCalibrationVendor(); ////FillddlCalibrationVendor



        }
    }

    /// <summary>
    /// clear control will clear all controls from input pop up
    /// </summary>
    public void ClearControl()
    {
        txtprocessObj.Text = "";
        txtTFGQty.Text = "";
        txtTool.Text = "";
        txtCycle.Text = "";
        txtTFGDesc.Text = "";
        txttimeCali.Text = "";
        txtcostCali.Text = "";
        ddlcaliVender.SelectedIndex = 0;
        txtvenderPart.Text = "";
        txtCaliDate.Text = "";
        ddlTFGvender.SelectedIndex = 0;
        txtVenderInfo.Text = "";
        txtTFGCost.Text = "";
    }

    /// <summary>
    /// resetbinding sorted by created date
    /// </summary>
    public void ResetBinding()
    {
        ViewState["sortBy"] = "CreatedDate";
        ViewState["isAsc"] = "1";
        BindGrid(); //bind gridview sorted by Created Date
    }

    /// <summary>
    /// bind grid will bind the grid view with TFG data
    /// </summary>
    public void BindGrid()
    {
        if (Convert.ToInt32(ViewState["poId"]) > -1)
        {
            gridTFG.DataSource = TFGData.GetTFGData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ViewState["poId"]));
            gridTFG.DataBind();
        }
    }

    /// <summary>
    /// fill ddl TFG Vendor will fill drop down menu with its TFG  data
    /// </summary>
    public void FillddlTFGVendor()
    {
        ddlTFGvender.Items.Clear();
        ddlTFGvender.Items.Add(new ListItem("Select", "0"));
        foreach (tbl_TFGVendor typeData in TFGVendor.GetVendors())
        {
            ddlTFGvender.Items.Add(new ListItem(typeData.TFGVendor, typeData.TFGVendorID.ToString()));  //// adding each type of TFG data  with drop down
        }
    }

    /// <summary>
    /// fill ddl CalibrationVendor will fill drop down menu with its TFG data
    /// </summary>
    public void FillddlCalibrationVendor()
    {
        ddlcaliVender.Items.Clear();
        ddlcaliVender.Items.Add(new ListItem("Select", "0"));
        foreach (tbl_Calibration typeData in TFGVendor.GetCalibrationVendors())
        {
            ddlcaliVender.Items.Add(new ListItem(typeData.CalibrationVendor, typeData.CalibrationVendorID.ToString()));  //// adding each type of TFG data with drop down
        }
    }
    protected void gridTFG_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ////idDelete will get Attribute Id data for selected grid view row on delete button
        int idDelete = this.CInt32(((ImageButton)gridTFG.Rows[e.RowIndex].FindControl("deleteBtn")).CommandArgument);
        if (idDelete > 0)
        {
            bool result = false;
            result = TFGData.DeleteTFG(idDelete); ////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables
            if (result)
            {

                lblMsg.Visible = true;
                lblMsg.Text = "This record is Deleted.!";
                lblMsg.CssClass = "msgSucess";
                lblMsg.Style.Add("width", "100%!important");
                //// result is true
            }
            else
            {

                lblMsg.Visible = true;
                lblMsg.Text = "Error on Deleting data.!";
                lblMsg.CssClass = "msgError";
                lblMsg.Style.Add("width", "100%!important");
                //// result is false
            }
            ClearControl();
            ResetBinding();
            this.EditIDINT = 0;
            ModelTFG.Show();

        }
    }
    protected void gridTFG_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OpenPopup", "javascript:OpenPopup('divTFG');", true);
        lnkbtnOpenDiv.Visible = false;
        ClearControl();
        this.EditIDINT = this.CInt32(((Literal)gridTFG.Rows[e.NewEditIndex].FindControl("litTFGId")).Text); //// it will get link id for which we are editing
        FillTFGDataById();  //// it will fill data for given link id
        ModelTFG.Show();
    }

    protected void gridTFG_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //gridTFG.PageIndex = e.;
        //BindGrid();
    }
    protected void gridTFG_Sorting(object sender, GridViewSortEventArgs e)
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
        BindGrid();
        ModelTFG.Show();
    }

    protected void gridTFG_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void gridTFG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridTFG.PageIndex = e.NewPageIndex;
        this.BindGrid(); //// bind grid view 
        ModelTFG.Show();
    }
    protected void submitBtn_Click(object sender, EventArgs e)
    {
        AddTFGData();  //// it will add entered TFG data in and also update 
        ModelTFG.Show();
    }

    /// <summary>
    /// add input link will add and update link data 
    /// </summary>
    public void AddTFGData()
    {
        if (TFGData.GetDuplicateCheck(txtTool.Text.Trim(), Convert.ToInt32(ViewState["poId"]), this.EditIDINT)) //// it will check if link name is already exist
        {
            tbl_TFG TFGDataFields = new tbl_TFG();

            if (Convert.ToInt32(ViewState["poId"]) > -1)
            {
                TFGDataFields.ProcessObjectID = Convert.ToInt32(ViewState["poId"]);
            }

            TFGDataFields.TFGQty = this.CInt32(txtTFGQty.Text.Trim());
            TFGDataFields.Tool_Fixture_GageName = txtTool.Text.Trim();
            TFGDataFields.CalibrationCycle = txtCycle.Text.Trim();
            TFGDataFields.TFGDescription = txtTFGDesc.Text.Trim();
            TFGDataFields.TimeToCailbrate = txttimeCali.Text.Trim();
            TFGDataFields.CostToCalibrate = this.CInt32(txtcostCali.Text.Trim());

            TFGDataFields.TFGVendorPart = txtvenderPart.Text.Trim();
            TFGDataFields.TFGVendor = ddlTFGvender.SelectedItem.Text.ToString();
            TFGDataFields.TFGVendorID = this.CInt32(ddlTFGvender.SelectedValue); 
            if(txtCaliDate.Text!=string.Empty)
            TFGDataFields.CalibrationDate1 = Convert.ToDateTime(txtCaliDate.Text.Trim());
           
            TFGDataFields.TFGCost = this.CInt32(txtTFGCost.Text.Trim());

            TFGDataFields.CalibrationVendorInfo = txtVenderInfo.Text.Trim();
            TFGDataFields.CalibrationVendor = ddlcaliVender.SelectedItem.Text.ToString();
            TFGDataFields.CalibrationVendorID = this.CInt32(ddlcaliVender.SelectedValue);

            //TFGDataFields.CalibratedBy = txttimeCali.Text.Trim();
            TFGDataFields.CreatedDate = DateTime.Now;

            if (this.EditIDINT > 0)    //// for edit mode
            {
                TFGDataFields.TFGID = this.EditIDINT;
            }

            bool result = false;
            // result =
            result = TFGData.SaveTFGData(TFGDataFields);  ////SaveInputData will dave input link in database table information input

            if (result == true)  // if record is updated or inserted
            {
                //string script = "alert(\"Saved successfully!\");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //              "ServerControlScript", script, true);
                lblMsg.Visible = true;
                lblMsg.Text = "TFG data Saved successfully!";
                lblMsg.CssClass = "msgSucess";
                lblMsg.Style.Add("width", "100%!important");
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

        ResetBinding();  //// bind grid view after new row inserted or row updated
        ClearControl();
        this.EditIDINT = 0; //// it will set edit mode false
        submitBtn.Text = "Add link";
        lnkbtnOpenDiv.Visible = true;
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
    /// it will fill link data bi link id 
    /// </summary>
    public void FillTFGDataById()
    {
        tbl_TFG TFGGetData = new tbl_TFG();
        TFGGetData = TFGData.TFGByID(this.EditIDINT);////TFGById will get TFG by its id that is EditIDINT

        if (Convert.ToInt32(ViewState["poId"]) > -1)
        {
            TFGGetData.ProcessObjectID = Convert.ToInt32(ViewState["poId"]);
        }
        if (TFGGetData != null)
        {
            VisualERPDataContext ObjData = new VisualERPDataContext();

            if (SourceType == 2)
            {
                tbl_TargetObject qry = (from x in ObjData.tbl_TargetObjects
                                        where x.TargetObjID == TFGGetData.ProcessObjectID
                                        select x).FirstOrDefault();
                if (qry != null)
                {
                    txtprocessObj.Text = qry.TargetObjName;
                }

            }
            else
            {
                tbl_ProcessObject qry = (from x in ObjData.tbl_ProcessObjects
                                         where x.ProcessObjID == TFGGetData.ProcessObjectID
                                         select x).FirstOrDefault();
                if (qry != null)
                {
                    txtprocessObj.Text = qry.ProcessObjName;
                }
            }
            //  txtprocessObj.Text = Convert.ToString(TFGDataID.ProcessObjectID);
            txtTFGQty.Text = TFGGetData.TFGQty.ToString();
            txtTool.Text = TFGGetData.Tool_Fixture_GageName;
            txtCycle.Text = TFGGetData.CalibrationCycle;
            txtTFGDesc.Text = TFGGetData.TFGDescription;
            txttimeCali.Text = TFGGetData.TimeToCailbrate;
            txtcostCali.Text = TFGGetData.CostToCalibrate.ToString();
            txtvenderPart.Text = TFGGetData.TFGVendorPart;

            txtCaliDate.Text = TFGGetData.CalibrationDate1.ToString();   // get datetime from database

            txtVenderInfo.Text = TFGGetData.CalibrationVendorInfo;
            txtTFGCost.Text = TFGGetData.TFGCost.ToString();
            this.SetDropDownListValue(ddlcaliVender, TFGGetData.CalibrationVendorID.ToString());
            this.SetDropDownListValue(ddlTFGvender, TFGGetData.TFGVendorID.ToString());
            submitBtn.Text = "Update";
      
        }
    }

    /// <summary>
    /// it will open all popup fields and fill the processobject name with process object id
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkbtnOpenDiv_Click(object sender, EventArgs e)
    {
        clearControl();
        if (Convert.ToInt32(ViewState["poId"]) > -1)
        {
            int proId = Convert.ToInt32(ViewState["poId"]);

            VisualERPDataContext ObjData = new VisualERPDataContext();


            if (SourceType == 2)
            {
                var qry = (from x in ObjData.tbl_TargetObjects
                           where x.TargetObjID == proId
                           select x).FirstOrDefault();
                if (qry != null)
                {
                    txtprocessObj.Text = qry.TargetObjName;
                }
            }
            else
            {
                var qry = (from x in ObjData.tbl_ProcessObjects
                           where x.ProcessObjID == proId
                           select x).FirstOrDefault();
                if (qry != null)
                {
                    txtprocessObj.Text = qry.ProcessObjName;
                }
            }
                
        }
        if (lblMsg.Visible == true)
        {
            lblMsg.Visible = false;
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OpenPopup", "javascript:OpenPopup('divTFG');", true);
        lnkbtnOpenDiv.Visible = false;
        ModelTFG.Show();
    }
    protected void ddlcaliVender_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OpenPopup", "javascript:OpenPopup('divTFG');", true);
        lnkbtnOpenDiv.Visible = false;
        //txtVenderInfo.Text = ddlcaliVender.SelectedItem.Text.ToString();
        int cvId = this.CInt32(ddlcaliVender.SelectedValue);
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_Calibrations
                   where x.CalibrationVendorID == cvId
                   select x).FirstOrDefault();
        if (qry != null)
        {
            txtVenderInfo.Text = qry.CalibrationVendorInfo;
        }
        ModelTFG.Show();
    }
    //protected void ResetBtn_Click(object sender, EventArgs e)
    //{
    //    if (EditIDINT > 0)
    //    {

    //        FillTFGDataById();
    //        ModelTFG.Show();
    //    }
    //    else
    //    {
    //        txtTFGQty.Text = "";
    //        txtTool.Text = "";
    //        txtCycle.Text = "";
    //        txtTFGDesc.Text = "";
    //        txttimeCali.Text = "";
    //        txtcostCali.Text = "";
    //        txtvenderPart.Text = "";
    //        txtCaliDate.Text = "";
    //        txtVenderInfo.Text = "";
    //        txtTFGCost.Text = "";
    //        this.ddlTFGvender.SelectedIndex = -1;
    //        this.ddlcaliVender.SelectedIndex = -1;
    //        ModelTFG.Show();
    //    }
    //}
    protected void bckBtn_Click(object sender, EventArgs e)
    {
        ResetBinding();
        ModelTFG.Show();
    }

    public void clearControl()
    {
        txtTFGQty.Text = "";
        txtTool.Text = "";
        txtCycle.Text = "";
        txtTFGDesc.Text = "";
        txttimeCali.Text = "";
        txtcostCali.Text = "";
        txtvenderPart.Text = "";
        txtCaliDate.Text = "";
        txtVenderInfo.Text = "";
        txtTFGCost.Text = "";
        this.ddlTFGvender.SelectedIndex = -1;
        this.ddlcaliVender.SelectedIndex = -1;
        ModelTFG.Show();
    }
}