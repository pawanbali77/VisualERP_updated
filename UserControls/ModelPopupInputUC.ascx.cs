/*****************************************************************************
1. * Copyright 1999, All rights reserved, For internal use only
* FILE: ModelPopupInputUC..ascx.cs
* PROJECT: VisualERP
* MODULE:ProcessManager
* AUTHOR: Jayant
* DATE: 16/8/2013
* Description: it contains the Attribute data and tree data.
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

public partial class UserControls_ModelPopupInputUC : System.Web.UI.UserControl
{

    [BrowsableAttribute(true)]
    public int ProcessObjectId
    {
        set
        {
            ViewState["poId"] = value;
            ClearControl(); //// for clearing control for every postback
            ResetBinding(); //// for binding grid view data
            FillddlTypes();  //// FillddlTypes
        }
    }

    /// <summary>
    /// this code will fire the for input Data.
    /// NAME: input data Population.
    /// Description: it will Displayed input Data.
    /// Subroutines Called: ---
    /// Parameters:no
    /// Returns:input Data
    /// Globals: no
    /// Design Document Reference: no
    /// Author: jayant
    /// Assumptions and Limitation: --
    /// Exception Processing: ---
    /// Date: By: Description: 28/7/2013
    /// </summary>
    /// <param name="">No Parameters</param>
    /// <returns>it will check post Back data and then Bind Grid View data And drop down</returns>
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        ///it will check post Back data and then Bind Attribute Data
        if (!IsPostBack)
        {
            ClearControl(); //// for clearing control for every postback
            ResetBinding(); //// for binding grid view data
            FillddlTypes();  //// FillddlTypes
            //lblMsg.Attributes.Remove("msgError");
            //lblMsg.Attributes.Remove("msgSucess");

        }
        
    }

    /// <summary>
    /// bind grid will bind the grid view with information input data
    /// </summary>
    public void BindGrid()
    {
        if (Convert.ToInt32(ViewState["poId"]) > -1)
        {
            gridInput.DataSource = InputData.GetInputData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ViewState["poId"]));
            gridInput.DataBind();
        }
    }

    /// <summary>
    /// resetbinding sorted by created date
    /// </summary>
    public void ResetBinding()
    {
        ViewState["sortBy"] = "InputName";
        ViewState["isAsc"] = "1";
        BindGrid(); //bind gridview sorted by Created Date
    }

    /// <summary>
    /// clear control will clear all controls from input pop up
    /// </summary>
    public void ClearControl()
    {
        txtLinkName.Text = "";
        txtLinkValue.Text = "";
        ddlTypes.SelectedIndex = 0;
    }

    /// <summary>
    /// fill ddl type will fill drop down menu with its link type data
    /// </summary>
    public void FillddlTypes()
    {
        ddlTypes.Items.Clear();
        ddlTypes.Items.Add(new ListItem("Select", "0"));
        foreach (tbl_InputType typeData in InputTypes.GetTypes())
        {
            ddlTypes.Items.Add(new ListItem(typeData.Type, typeData.ID.ToString()));  //// adding each type of link with drop down
        }
    }

    protected void gridInput_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ////idDelete will get Attribute Id data for selected grid view row on delete button
        int idDelete = this.CInt32(((ImageButton)gridInput.Rows[e.RowIndex].FindControl("deleteBtn")).CommandArgument);
        if (idDelete > 0)
        {
            bool result = false;
            result = InputData.DeleteInputLink(idDelete); ////DeleteAttributeData is stored procedure in database that will delete selected Attribute id from multiple tables
            if (result)
            {
                //string script = "alert(\"This record is Deleted.!\");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //              "ServerControlScript", script, true);
                lblMsg.Visible = true;
                lblMsg.Text = "This record is Deleted.!";
                lblMsg.CssClass = "msgSucess";
                //// result is true
            }
            else
            {
                //string script = "alert(\"Error on Deleting data.!\");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //              "ServerControlScript", script, true);
                lblMsg.Visible = true;
                lblMsg.Text = "Error on Deleting data.!";
                lblMsg.CssClass = "msgError";
                //// result is false
            }
            ClearControl();
            ResetBinding();
            this.EditIDINT = 0;
            modelInput.Show();
        }
    }
    protected void gridInput_Sorting(object sender, GridViewSortEventArgs e)
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
        modelInput.Show();
    }
    protected void gridInput_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //gridAttribute.PageIndex = e.;
        //BindGrid();
    }
    protected void gridInput_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridInput.PageIndex = e.NewPageIndex;
        this.BindGrid(); //// bind grid view 
        modelInput.Show();
    }
    protected void gridInput_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gridInput_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ClearControl();
        this.EditIDINT = this.CInt32(((Literal)gridInput.Rows[e.NewEditIndex].FindControl("litLinkId")).Text); //// it will get link id for which we are editing
        FillInputDataById();  //// it will fill data for given link id
        modelInput.Show();
    }

    /// <summary>
    /// add link button will add all details of link input data in database table
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void addlinkBtn_Click(object sender, EventArgs e)
    {
        AddInputLink();  //// it will add entered link data in and also update 
        modelInput.Show();
    }

    /// <summary>
    /// add input link will add and update link data 
    /// </summary>
    public void AddInputLink()
    {
        if (InputData.GetDuplicateCheck(txtLinkName.Text.Trim(), Convert.ToInt32(ViewState["poId"]), this.EditIDINT)) //// it will check if link name is already exist
        {
            tbl_InformationInput InformationInputTbl = new tbl_InformationInput();
            InformationInputTbl.Link = txtLinkValue.Text.Trim();
            InformationInputTbl.LinkName = txtLinkName.Text.Trim();
            InformationInputTbl.LinkType = this.CInt32(ddlTypes.SelectedValue);
            InformationInputTbl.CreatedDate = DateTime.Now;
            if (radioBtnYes.Checked == true)
                InformationInputTbl.IncludeOnMap = true;
            else
                InformationInputTbl.IncludeOnMap = false;

            if (this.EditIDINT > 0)    //// for edit mode
            {
                InformationInputTbl.LinkID = this.EditIDINT;
                InformationInputTbl.ModifiedDate = DateTime.Now;
            }
            if (Convert.ToInt32(ViewState["poId"]) > -1)
            {
                InformationInputTbl.ProcessObjectID = Convert.ToInt32(ViewState["poId"]);
            }

            bool result = false;
            result = InputData.SaveInputData(InformationInputTbl);  ////SaveInputData will dave input link in database table information input

            if (result == true)  // if record is updated or inserted
            {
                //string script = "alert(\"Saved successfully!\");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //              "ServerControlScript", script, true);
                lblMsg.Visible = true;
                lblMsg.Text = "Input data saved successfully!";
                lblMsg.CssClass = "msgSucess";
                lblMsg.Style.Add("width", "100%!important");
            }
            else
            {
                //string script = "alert(\"Error on saving data.!\");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //              "ServerControlScript", script, true);
                lblMsg.Visible = true;
                lblMsg.Text = "Error on saving data.!";
                lblMsg.CssClass = "msgError";
                lblMsg.Style.Add("width", "100%!important");
            }
        }
        else
        {

            //string script = "alert(\"This record already exists.!\");";
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //              "ServerControlScript", script, true);
            lblMsg.Visible = true;
            lblMsg.Text = "This record already exists.!";
            lblMsg.CssClass = "msgError";
            lblMsg.Style.Add("width", "100%!important");
        }
        ResetBinding();  //// bind grid view after new row inserted or row updated
        ClearControl();
        this.EditIDINT = 0; //// it will set edit mode false
        addlinkBtn.Text = "Add link";
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
    public void FillInputDataById()
    {
        tbl_InformationInput InputDataID = new tbl_InformationInput();  ////InputDataID obj of database table information input
        InputDataID = InputData.InputByID(this.EditIDINT);  ////InputById will get input by its id that is EditIDINT
        if (InputDataID != null)
        {
            txtLinkValue.Text = InputDataID.Link;
            txtLinkName.Text = InputDataID.LinkName;
            this.SetDropDownListValue(ddlTypes, InputDataID.LinkType.ToString());
            if (InputDataID.IncludeOnMap == true)
            {
                radioBtnYes.Checked = true; ////if include on map is true radiobtnyes is checked
            }
            else
            {
                radioBtnNo.Checked = true;
            }
            addlinkBtn.Text = "Update"; ////button text changed with update in edit case
        }
    }
}