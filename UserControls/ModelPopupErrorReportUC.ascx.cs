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

public partial class UserControls_ModelPopupErrorReportUC : System.Web.UI.UserControl
{

    int ProcessId = 0;
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
        }
    }
    public void ClearControl()
    {
        txtErrorName.Text = "";
        txtWorkContentValue.Text = "";
        txtCycleTimeValue.Text = "";
        txtCounterMeasureValue.Text = "";
        txtCounterMeasureStrengthValue.Text = "";
    }
    public void ResetBinding()
    {
        ViewState["sortBy"] = "CreatedDate";
        ViewState["isAsc"] = "1";
        BindGrid(); //bind gridview sorted by Created Date
    }
    public void BindGrid()
    {
        if (Convert.ToInt32(ViewState["poId"]) > -1)
        {
            gridErrorReport.DataSource = ErrorData.GetAll(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ViewState["poId"]));
            gridErrorReport.DataBind();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        ///it will check post Back data and then Bind Attribute Data
        if (!IsPostBack)
        {
            ClearControl(); //// for clearing control for every postback
            ResetBinding(); //// for binding grid view data
        }
    }
    protected void gridErrorReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ////idDelete will get Attribute Id data for selected grid view row on delete button
        int idDelete = this.CInt32(((ImageButton)gridErrorReport.Rows[e.RowIndex].FindControl("deleteBtn")).CommandArgument);
        if (idDelete > 0)
        {
            bool result = false;
            result = ErrorData.DeleteInputLink(idDelete); ////DeleteAttributeData is stored procedure in database that will delete selected Attribute id from multiple tables
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
            ModelErrorReport.Show();
        }
    }
    protected void gridErrorReport_Sorting(object sender, GridViewSortEventArgs e)
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
        ModelErrorReport.Show();
    }
    protected void gridErrorReport_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //gridAttribute.PageIndex = e.;
        //BindGrid();
    }
    protected void gridErrorReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridErrorReport.PageIndex = e.NewPageIndex;
        this.BindGrid(); //// bind grid view 
        ModelErrorReport.Show();
    }
    protected void gridErrorReport_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gridErrorReport_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ClearControl();
        this.EditIDINT = this.CInt32(((Literal)gridErrorReport.Rows[e.NewEditIndex].FindControl("litErrorReportId")).Text); //// it will get link id for which we are editing
        FillInputDataById();  //// it will fill data for given link id
        ModelErrorReport.Show();
    }

    /// <summary>
    /// add link button will add all details of link input data in database table
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void addlinkBtn_Click(object sender, EventArgs e)
    {
        AddInputLink();  //// it will add entered link data in and also update 
        ModelErrorReport.Show();
    }

    /// <summary>
    /// add input link will add and update link data 
    /// </summary>
    public void AddInputLink()
    {
        if (ErrorData.GetDuplicateCheck(txtErrorName.Text.Trim(), Convert.ToInt32(ViewState["poId"]), this.EditIDINT)) //// it will check if link name is already exist
        {
            ErrorInfo objErrorData = new ErrorInfo();
            objErrorData.Error = txtErrorName.Text.Trim();
            objErrorData.WorkContent = Convert.ToInt32(txtWorkContentValue.Text.Trim());
            objErrorData.CounterMeasure = txtCounterMeasureValue.Text.Trim();
            objErrorData.CounterMeasureStrength = Convert.ToInt32(txtCounterMeasureStrengthValue.Text.Trim());
            objErrorData.CycleTime = Convert.ToInt32(txtCycleTimeValue.Text.Trim());
            objErrorData.CreatedDate= DateTime.Now;
            objErrorData.ModifiedDate= DateTime.Now;



            if (radioBtnYes.Checked == true)
                objErrorData.IncludeOnMap = true;
            else
                objErrorData.IncludeOnMap = false;

            if (this.EditIDINT > 0)    //// for edit mode
            {
                objErrorData.ErrorID = this.EditIDINT;

            }
            if (Convert.ToInt32(ViewState["poId"]) > -1)
            {
                objErrorData.ProcessID = Convert.ToInt32(ViewState["poId"]);
            }

            bool result = false;
            result = ErrorData.Save(objErrorData);  ////SaveInputData will dave input link in database table information input

            if (result == true)  // if record is updated or inserted
            {
                //string script = "alert(\"Saved successfully!\");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //              "ServerControlScript", script, true);
                lblMsg.Visible = true;
                lblMsg.Text = "Error data saved successfully!";
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
        ErrorInfo InputDataID = new ErrorInfo();  ////InputDataID obj of database table information input
        InputDataID = ErrorData.InputByID(this.EditIDINT);  ////InputById will get input by its id that is EditIDINT
        if (InputDataID != null)
        {
            txtCounterMeasureStrengthValue.Text = Convert.ToString(InputDataID.CounterMeasureStrength);
            txtCounterMeasureValue.Text = Convert.ToString(InputDataID.CounterMeasure);
            txtErrorName.Text = InputDataID.Error;
            txtWorkContentValue.Text = Convert.ToString(InputDataID.WorkContent);
            txtCycleTimeValue.Text = Convert.ToString(InputDataID.CycleTime);

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