/*****************************************************************************
1. * Copyright 1999, All rights reserved, For internal use only
* FILE: HandOffUC.ascx.cs
* PROJECT: VisualERP
* MODULE:HandOffManager
* AUTHOR: Jayant
* DATE: 20/09/2013
* Description: it contains the Enterprise Manager Data and handoff inputs/outputs.
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

public partial class UserControls_HandOffUC : System.Web.UI.UserControl
{
    #region
    int SysIOId = 0;
    string ProcessObjNameFrom = "";
    int fromActivityId = 0;
    int ToActivityId = 0;
    int typeCheck = 0;
    string ProcessNameFrom = "";
    string ProcessObjNameTo = "";
    string ProcessNameTo = "";
    #endregion
    [BrowsableAttribute(true)]
    public int SystemIOId
    {
        // get { return 0; }
        //set { ResetBinding(value); }

        set
        {
            ViewState["SysIOId"] = value;
            if (lblMsg.Visible == true)
            {
                lblMsg.Visible = false;
            }
            ResetBinding();
        }
    }

    [BrowsableAttribute(true)]
    public int TypeIO
    {
        // get { return 0; }
        //set { ResetBinding(value); }

        set
        {
            ViewState["TypeIO"] = value;
            load();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lnkbtnOpenDivHandOff.Visible = true;
        if (!IsPostBack)
        {
            ClearControl(); //// for clearing control for every postback
            if (ViewState["SysIOId"] != null)
            {
                ResetBinding(); //// for binding grid view data
            }

        }

        
     
    }

    public void load()
    {
        if (ViewState["TypeIO"] != null)
        {
            typeCheck = this.CInt32(ViewState["TypeIO"]);
            if (typeCheck == 0)
            {
                HoNamelbl.Text = "";
                HoTypelbl.Text = "";
                HoHeadlbl.Text = "";
                HoNamelbl.Text = "HO Input Name";
                HoTypelbl.Text = "HO Input Type";
                HoHeadlbl.Text = "Input";
            }
            else
            {
                HoNamelbl.Text = "";
                HoTypelbl.Text = "";
                HoHeadlbl.Text = "";
                HoNamelbl.Text = "HO Output Name";
                HoTypelbl.Text = "HO Output Type";
                HoHeadlbl.Text = "Output";
            }

        }
    }

    protected void lnkbtnOpenDivHandOff_Click(object sender, EventArgs e)
    {
        ClearControl();
        if (lblMsg.Visible == true)
        {
            lblMsg.Visible = false;
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OpenPopup", "javascript:OpenPopup('divHandOff');", true);
        lnkbtnOpenDivHandOff.Visible = false;
        if (ViewState["SysIOId"] != null)
        {
            List<HandOffData.ListHandOffData> lstHandObj = HandOffData.SystemIODataBySysIOId(this.CInt32(ViewState["SysIOId"]));
            fromActivityId = lstHandObj[0].FromActivityID;
            ToActivityId = lstHandObj[0].ToActivityID;
            ProcessObjNameFrom = ProcessData.GetProcessObjNameById(this.CInt32(fromActivityId));
            ProcessNameFrom = ProcessData.GetProcessNameByPoid(this.CInt32(fromActivityId));
            txtFromName.Text = ProcessNameFrom + "/" + ProcessObjNameFrom;

            ProcessObjNameTo = ProcessData.GetProcessObjNameById(this.CInt32(ToActivityId));
            ProcessNameTo = ProcessData.GetProcessNameByPoid(this.CInt32(ToActivityId));

            txtHotoName.Text = ProcessNameTo + "/" + ProcessObjNameTo;
        }

        ModelPopupHandOff123.Show();
    }
    protected void BtnSubmitHandOff_Click(object sender, EventArgs e)
    {
        AddHandoffInfoData();  //// it will add entered TFG data in and also update 
        ModelPopupHandOff123.Show();
    }
    protected void bckBtnHandOff_Click(object sender, EventArgs e)
    {
        ResetBinding();
        ModelPopupHandOff123.Show();
    }

    public void AddHandoffInfoData()
    {
        if (ViewState["SysIOId"] != null)
        {
            SysIOId = this.CInt32(ViewState["SysIOId"]);
            tbl_HandOffData TblHandOffdata = new tbl_HandOffData();
            TblHandOffdata.HOOutputName = txtHoOutputName.Text.Trim();
            TblHandOffdata.HOOutputType = txtOutputType.Text.Trim();
            TblHandOffdata.HOInputID = this.CInt32(txtHoInputId.Text.Trim());
            TblHandOffdata.HOInputLink = txtHOInputLink.Text.Trim();
            TblHandOffdata.SytemIOID = SysIOId;
            if (radioBtnYes.Checked == true)
                TblHandOffdata.IncludeonMap = true;
            else
                TblHandOffdata.IncludeonMap = false;
            //TFGDataFields.CalibratedBy = txttimeCali.Text.Trim();


            if (this.EditIDINT > 0)    //// for edit mode
            {
                TblHandOffdata.HOID = this.EditIDINT;

                TblHandOffdata.ModifiedDate = DateTime.Now;
            }

            bool result = false;
            // result =
            result = HandOffData.SaveHandOffData(TblHandOffdata);  ////SaveInputData will dave input link in database table information input

            if (result == true)  // if record is updated or inserted
            {
                //string script = "alert(\"Saved successfully!\");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //              "ServerControlScript", script, true);
                lblMsg.Visible = true;
                lblMsg.Text = "HandOffdata data Saved successfully!";
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

            ResetBinding();  //// bind grid view after new row inserted or row updated
            ClearControl();
            this.EditIDINT = 0; //// it will set edit mode false
            BtnSubmitHandOff.Text = "Submit";
            lnkbtnOpenDivHandOff.Visible = true;
        }
    }
    protected void gridHandOff_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gridHandOff_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ////idDelete will get Attribute Id data for selected grid view row on delete button
        int idDelete = this.CInt32(((ImageButton)gridHandOff.Rows[e.RowIndex].FindControl("deleteBtnHandOff")).CommandArgument);
        if (idDelete > 0)
        {
            bool result = false;
            result = HandOffData.DeleteHandOffData(idDelete); ////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables
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
            ModelPopupHandOff123.Show();
        }
    }


    protected void gridHandOff_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OpenPopup", "javascript:OpenPopup('divHandOff');", true);
        lnkbtnOpenDivHandOff.Visible = false;
        ClearControl();
        this.EditIDINT = this.CInt32(((Literal)gridHandOff.Rows[e.NewEditIndex].FindControl("litHOID")).Text); //// it will get link id for which we are editing
        FillHandOffDataById();  //// it will fill data for given link id
        ModelPopupHandOff123.Show();
    }
    public void FillHandOffDataById()
    {
        List<HandOffData.ListHandOffData> newHandOffObj = new List<HandOffData.ListHandOffData>();
        newHandOffObj = HandOffData.HandOffByID(this.EditIDINT);////TFGById will get TFG by its id that is EditIDINT
        if (ViewState["SysIOId"] != null)
        {
            List<HandOffData.ListHandOffData> lstHandObj = HandOffData.SystemIODataBySysIOId(this.CInt32(ViewState["SysIOId"]));
            fromActivityId = lstHandObj[0].FromActivityID;
            ToActivityId = lstHandObj[0].ToActivityID;
            ProcessObjNameFrom = ProcessData.GetProcessObjNameById(this.CInt32(fromActivityId));
            ProcessNameFrom = ProcessData.GetProcessNameByPoid(this.CInt32(fromActivityId));
            txtFromName.Text = ProcessNameFrom + "/" + ProcessObjNameFrom;

            ProcessObjNameTo = ProcessData.GetProcessObjNameById(this.CInt32(ToActivityId));
            ProcessNameTo = ProcessData.GetProcessNameByPoid(this.CInt32(ToActivityId));

            txtHotoName.Text = ProcessNameTo + "/" + ProcessObjNameTo;
        }
        if (newHandOffObj != null)
        {
            VisualERPDataContext ObjData = new VisualERPDataContext();
            if (newHandOffObj[0].IncludeonMap == true)
            {
                radioBtnYes.Checked = true;
                radioBtnNo.Checked = false;
            }
            else
            {
                radioBtnNo.Checked = true;
                radioBtnYes.Checked = false;
            }
            txtHoOutputName.Text = newHandOffObj[0].HOOutputName.ToString();
            txtHoInputId.Text = newHandOffObj[0].HOInputID.ToString();
            txtOutputType.Text = newHandOffObj[0].HOOutputType.ToString();
            txtHOInputLink.Text = newHandOffObj[0].HOInputLink.ToString();
            BtnSubmitHandOff.Text = "Update";
        }
    }
    protected void gridHandOff_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void gridHandOff_Sorting(object sender, GridViewSortEventArgs e)
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
    }
    protected void gridHandOff_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridHandOff.PageIndex = e.NewPageIndex;
        this.BindGrid(); //// bind grid view 
        ModelPopupHandOff123.Show();
    }
    public void ClearControl()
    {
        txtHoInputId.Text = "";
        txtHoOutputName.Text = "";
        txtOutputType.Text = "";
        txtHOInputLink.Text = "";
        //  ModelPopupHandOff123.Show();


    }
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
        if (Convert.ToInt32(ViewState["SysIOId"]) > -1)
        {
            gridHandOff.DataSource = HandOffData.GetHandOffData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), this.CInt32(ViewState["SysIOId"]));
            gridHandOff.DataBind();
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
    protected void gridHandOff_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {

            if (ViewState["TypeIO"] != null)
            {
                typeCheck = this.CInt32(ViewState["TypeIO"]);
                if (typeCheck == 0)
                {
                    e.Row.Cells[2].Text = "HO Input Type";
                    e.Row.Cells[3].Text = "HO Input Name";

                }
                else
                {
                    e.Row.Cells[2].Text = "HO Output Type";
                    e.Row.Cells[3].Text = "HO Output Name";
                }
            }

        }

    }
}