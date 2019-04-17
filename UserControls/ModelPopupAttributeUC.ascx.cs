/*****************************************************************************
1. * Copyright 1999, All rights reserved, For internal use only
* FILE: ModelPopupAttributeUC..ascx.cs
* PROJECT: VisualERP
* MODULE:ProcessManager
* AUTHOR: Ratnesh
* DATE: 28/7/2013
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
/// <summary>
/// public class User Control ModelPopupAttributeUC Data for bind Attribute data
/// </summary>
/// 
public partial class UserControls_ModelPopupAttributeUC : System.Web.UI.UserControl
{

    #region
    int ProcessId = 0;
    #endregion

    [BrowsableAttribute(true)]
    public int ProcessObjectId
    {
        set 
        {
            ViewState["poId"] = value;
            ClearControl();/// for clearing control for every postback
            ResetBinding();/// for binding grid view data
            FillddlUnits();/// fill drop down list data            
        }
    }

    /// <summary>
    /// this code will fire the for Attribute Data.
    /// NAME: Attribute data Population.
    /// Description: it will Displayed Attribute Data.
    /// Subroutines Called: ---
    /// Parameters:no
    /// Returns:Attribute Data
    /// Globals: no
    /// Design Document Reference: no
    /// Author: Ratnesh
    /// Assumptions and Limitation: --
    /// Exception Processing: ---
    /// Date: By: Description: 28/7/2013
    /// </summary>
    /// <param name="">No Parameters</param>
    /// <returns>it will check post Back data and then Bind Grid View data And drop down</returns>
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Visible = false;

            if (!IsPostBack)
            {
                ClearControl();/// for clearing control for every postback
                ResetBinding();/// for binding grid view data
                FillddlUnits();/// fill drop down list data
            }

            SetSession();
    }

    public void BindGrid()
    {
        if (Convert.ToInt32(ViewState["poId"]) > -1)
        {
            gridAttribute.DataSource = AttributeData.GetAtrributeData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ViewState["poId"]));
            gridAttribute.DataBind();
        }
    }

    public void ResetBinding()
    {
        ViewState["sortBy"] = "AttributeName";
        ViewState["isAsc"] = "1";
        BindGrid(); //bind gridview sorted by Created Date
    }
    protected void gridAttribute_Sorting(object sender, GridViewSortEventArgs e)
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
        mopoExUser.Show();
    }
    protected void gridAttribute_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //gridAttribute.PageIndex = e.;
        //BindGrid();
    }

    protected void gridAttribute_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridAttribute.PageIndex = e.NewPageIndex;
        this.BindGrid(); //// bind grid view
        mopoExUser.Show();
    }

    protected void gridAttribute_RowCreated(object sender, GridViewRowEventArgs e)
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

    public void FillddlUnits()
    {
        ddlUnits.Items.Clear();
        ddlUnits.Items.Add(new ListItem("Select", "0"));
        foreach (tbl_Unit unitData in UnitData.GenUnits())
        {
            ddlUnits.Items.Add(new ListItem(unitData.UnitName, unitData.UnitID.ToString()));
        }
    }

    public void AddAttribute()
    {
        if (AttributeData.GetDuplicateCheck(txtAttributeName.Text.Trim(), Convert.ToInt32(ViewState["poId"]), this.EditIDINT))
        {

            MasterPage mstr = this.Parent.Page.Master as MasterPage;
            TreeView mastertreeview = (TreeView)mstr.FindControl("TreeView1");
            if (mastertreeview.SelectedNode != null)
                ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
            tbl_AttributesMenu AttributeTbl = new tbl_AttributesMenu();
            AttributeTbl.AttributeValue = txtAttrivalue.Text.Trim();
            AttributeTbl.AttributeName = txtAttributeName.Text.Trim();
            AttributeTbl.UnitID = this.CInt32(ddlUnits.SelectedValue);
            //int ProcessObjId = 0;
           // ProcessObjId = ProcessData.GetMaxProcessObjId(ProcessId);
           // ViewState["ProcessObjID"] = ProcessObjId;
            if (Convert.ToInt32(ViewState["poId"]) > -1)
            {
                AttributeTbl.ProcessObjectID = Convert.ToInt32(ViewState["poId"]);
            }
           // AttributeTbl.ProcessObjectID = this.CInt32(ViewState["ProcessObjID"]);

            AttributeTbl.ProcessID = ProcessId;
            AttributeTbl.CreatedDate = DateTime.Now;
            if (radioBtnYes.Checked == true)
                AttributeTbl.IncludeOnMap = true;
            else
                AttributeTbl.IncludeOnMap = false;

            if (this.EditIDINT > 0)
            {
                AttributeTbl.AttributeMenuID = this.EditIDINT;
                AttributeTbl.ModifiedDate = DateTime.Now;
            }

            bool result = false;
            result = AttributeData.SaveAttributeData(AttributeTbl);

            if (result == true)
            {

                //string script = "alert(\"Saved successfully!\");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //              "ServerControlScript", script, true);
                lblMsg.Visible = true;
                lblMsg.Text = "Attribute data saved successfully!";
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
                lblMsg.Visible = true;
                lblMsg.Style.Add("width", "100%!important");
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
        addAttributeBtn.Text = "Add link";

    }
    protected void addAttributeBtn_Click1(object sender, EventArgs e)
    {
        AddAttribute();
        mopoExUser.Show();
    }

    public void ClearControl()
    {
        txtAttrivalue.Text = "";
        txtAttributeName.Text = "";
        ddlUnits.SelectedIndex = 0;
    }
    protected void gridAttribute_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ClearControl();
        this.EditIDINT = this.CInt32(((Literal)gridAttribute.Rows[e.NewEditIndex].FindControl("litAttributeID")).Text);
        FillAttributeDataById();
        mopoExUser.Show();
    }

    public void FillAttributeDataById()
    {
        tbl_AttributesMenu AttributeDataID = new tbl_AttributesMenu();
        AttributeDataID = AttributeData.AttributeByID(this.EditIDINT);////AttributeById will get Attribute by its id that is EditIDINT
        if (AttributeDataID != null)
        {
            txtAttrivalue.Text = AttributeDataID.AttributeValue;
            txtAttributeName.Text = AttributeDataID.AttributeName;
            this.SetDropDownListValue(ddlUnits, AttributeDataID.UnitID.ToString());
            if (AttributeDataID.IncludeOnMap == true)
            {
                radioBtnYes.Checked = true;
            }
            else
            {
                radioBtnNo.Checked = true;
            }
            addAttributeBtn.Text = "Update";
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

    protected void gridAttribute_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ////idDelete will get Attribute Id data for selected grid view row on delete button
        int idDelete = this.CInt32(((ImageButton)gridAttribute.Rows[e.RowIndex].FindControl("deleteBtn")).CommandArgument);
        if (idDelete > 0)
        {
            bool result = false;
            result = AttributeData.DeleteAttribute(idDelete); ////DeleteAttributeData is stored procedure in database that will delete selected Attribute id from multiple tables
            if (result)
            {

                //string script = "alert(\"This record is Deleted.!\");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //              "ServerControlScript", script, true);
                lblMsg.Visible = true;
                lblMsg.Text = "This record is Deleted.!";
                lblMsg.CssClass = "msgSucess";
                lblMsg.Style.Add("width", "100%!important");
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
                lblMsg.Style.Add("width", "100%!important");
                //// result is false
            }
            ClearControl();
            ResetBinding();
            this.EditIDINT = 0;
        }

        mopoExUser.Show();
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

    //protected void imgClosepp_Click(object sender, ImageClickEventArgs e)
    //{
    //    string absolutepath = Request.Url.AbsolutePath;
    //    string returnurl = absolutepath.Substring(absolutepath.LastIndexOf('/') + 1);
    //    if (returnurl == "ProcessManager.aspx")
    //    {
    //        Response.Redirect("ProcessManager.aspx");
    //    }
    //    else
    //    {
    //        Response.Redirect("EnterPriseManager.aspx");
    //    }
    //}
}