using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Reflection;

public partial class FormManager : BasePage
{
    int ProcessId = 0;
    int typeId = 0;
    string EditId = string.Empty;
    List<int> activity = new List<int>();
    List<string> activityName = new List<string>();
    Dictionary<int, string> activityDic = new Dictionary<int, string>();
    List<ProcessData.ProcessDataProperty> activityNode = new List<ProcessData.ProcessDataProperty>();
    int RoleID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["RoleID"] != null)
        {
            RoleID = Convert.ToInt32(Session["RoleID"].ToString());
        }
        EditId = GetPostBackControlId((Page)sender); // to get postback control id that is clicked//////////////////////////////
        string script = "test();";
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript", script, true);

        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript1", "hideSuccessMsg();", true);

        //txtFinalRPN1.Attributes.Add("readonly", "readonly");
        //txtIntialRPN1.Attributes.Add("readonly", "readonly");
        divErrorMsg.Visible = false;
        Label lblManager = (Label)Master.FindControl("lblManager");
        lblManager.Text = "Form Manager";
        lblManager.Attributes.Add("class", "PPESA");

        //foreach (ListItem item in radiobtnlistActivity.Items)
        //{
        //    if (item.Selected)
        //    {
        //        ViewState["poid"] = item.Value;
        //    }
        //}

        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
        {
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
            // BindActivityCheckboxList(ProcessId); 
        }
        else if (Session["SelectedNodeValue"] != null)
        {
            ProcessId = this.CInt32(Session["SelectedNodeValue"]);
            TreeNode strNode = mastertreeview.FindNode(Convert.ToString(Session["SelectedNodeValue"]));
            if (strNode != null)
                strNode.Select();

            //BindActivityCheckboxList(ProcessId); 
        }

        if (!IsPostBack)
        {
            OnTreeNodeChange();

        }

        OnTreeNodeChange();

        typeId = TypeData.GetTypeID(ProcessId); // get tree node type here 

        if (typeId == 5)
        {
            ulTopmenus.Visible = true;
        }
        else
        {
            ulTopmenus.Visible = false;
            // pnlActivity.Visible = false;
            // pnlAddForm.Visible = false;
            pnlListPPESA.Visible = false;
        }

        if (ProcessId == 0)
            headerTitle.InnerText = "Form Manager";
    }

    public void OnTreeNodeChange()
    {
        if (ViewState["FormType"] != null && EditId == "TreeView1")
        {
            // if (EditId != "lnkbtnAddPDESAForm")            
            ResetBinding(Convert.ToInt32(ViewState["FormType"])); // first time when page will load PPESA list will be display
            int formtype = Convert.ToInt32(ViewState["FormType"]);
            if (formtype == 0)
            {
                headerTitle.InnerText = "List PPESA";
                // lnkbtnAddPDESAForm.Visible = false; 
                // lnkbtnSaveForm.Visible = false;
                if (ProcessId > 0)
                {
                    string cid = lnkbtnViewPPESAForm.ClientID;
                    string script2 = "actvieclassByid(" + cid + ")";
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                  "script", script2, true);
                }
            }
           else if(formtype==2)
            {

                headerTitle.InnerText = "Error Log";
                // lnkbtnAddPPESAForm.Visible = false;
                //lnkbtnSaveForm.Visible = false;
                if (ProcessId > 0)
                {
                    string cid = lnkbtnErrorRecord.ClientID;
                    string script2 = "actvieclassByid(" + cid + ")";
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                  "script", script2, true);
                }
            }
            else
            {
                headerTitle.InnerText = "List PDESA";
                // lnkbtnAddPPESAForm.Visible = false;
                //lnkbtnSaveForm.Visible = false;
                if (ProcessId > 0)
                {
                    string cid = lnkbtnViewPDESAForm.ClientID;
                    string script2 = "actvieclassByid(" + cid + ")";
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                  "script", script2, true);
                }
            }
        }
        else if (EditId == "TreeView1" || EditId == "")
        {
            //if (EditId != "lnkbtnAddPDESAForm")
            ResetBinding(Convert.ToInt32(FormType.PPESA)); // first time when page will load PPESA list will be display
            pnlListPPESA.Visible = true;
            headerTitle.InnerHtml = "List PPESA";
            //lnkbtnSaveForm.Visible = false;
            if (ProcessId > 0)
            {
                string cid = lnkbtnViewPPESAForm.ClientID;
                string script2 = "actvieclassByid(" + cid + ")";
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                              "script", script2, true);
            }
        }

    }

    //public void BindActivityCheckboxList(int PoID)
    //{
    //    List<ProcessData.ProcessDataProperty> activities = ProcessData.GetProcessObjActvities(PoID);
    //    if (activities.Count != 0)
    //    {
    //        activityNode.AddRange(activities);
    //        radiobtnlistActivity.DataSource = activityNode;
    //        radiobtnlistActivity.DataTextField = "ProcessObjectName"; // datatext field
    //        radiobtnlistActivity.DataValueField = "ProcessObjID"; // data value field
    //        radiobtnlistActivity.DataBind(); // bind checkboxlist
    //        btnNextToActivity.Visible = true;
    //        ltrActivity.Text = "Select Activity";
    //    }
    //    else
    //    {
    //        ltrActivity.Text = "No Activity found under this process";
    //        btnNextToActivity.Visible = false;
    //        radiobtnlistActivity.Items.Clear();

    //    }
    //}

    //protected void btnNextToActivity_Click(object sender, EventArgs e)
    //{
    //    pnlActivity.Visible = false;
    //    pnlAddForm.Visible = true;
    //    pnlListPPESA.Visible = false;
    //    lnkbtnSaveForm.Visible = true;
    //    ClearControl();
    //}

    //protected void lnkbtnAddPPESAForm_Click(object sender, EventArgs e)
    //{
    //    if (ProcessId != 0)
    //    {
    //        string cid = lnkbtnAddPPESAForm.ClientID;
    //        string script3 = "actvieclass(" + cid + ")";
    //        ScriptManager.RegisterStartupScript(this, this.GetType(),
    //                      "script", script3, true);

    //        pnlActivity.Visible = true;
    //        pnlAddForm.Visible = false;
    //        pnlListPPESA.Visible = false;
    //        lnkbtnSaveForm.Visible = false;
    //        if (Convert.ToInt32(ViewState["FormType"]) == 0)
    //            headerTitle.InnerHtml = "Add PPESA";
    //        else
    //            headerTitle.InnerHtml = "Add PDESA";

    //        this.EditIDINT = 0;
    //        lnkbtnSaveForm.Text = "Save Record";
    //        lnkbtnSaveForm.Visible = false;

    //    }
    //    else
    //    {
    //        lblMsg.Visible = true;
    //        divErrorMsg.Visible = true;
    //        lblMsg.Text = "Please select a process.";
    //        lblMsg.Style.Add("color", "red");
    //        divErrorMsg.Attributes.Add("class", "isa_error");
    //        pnlActivity.Visible = false;
    //        pnlAddForm.Visible = false;
    //        pnlListPPESA.Visible = false;
    //        lnkbtnSaveForm.Visible = false;
    //    }
    //}

    //protected void lnkbtnAddPDESAForm_Click(object sender, EventArgs e)
    //{
    //    pnlActivity.Visible = true;
    //    pnlAddForm.Visible = false;
    //    pnlListPPESA.Visible = false;
    //    headerTitle.InnerHtml = "Add PDESA";
    //    ViewState["FormType"] = Convert.ToInt32(FormType.PDESA);
    //    this.EditIDINT = 0;
    //    lnkbtnSaveForm.Text = "Save Record";
    //    lnkbtnSaveForm.Visible = true;
    //    lnkbtnAddPPESAForm.Attributes.Add("class", "active");
    //}

    protected void lnkbtnViewPPESAForm_Click(object sender, EventArgs e)
    {
        string cid = lnkbtnViewPPESAForm.ClientID;
        string script1 = "actvieclass(" + cid + ")";
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "script", script1, true);

        //pnlActivity.Visible = false;
        //pnlAddForm.Visible = false;
        pnlListPPESA.Visible = true;
        pnlListErrorRecord.Visible = false;
        //liAddFormP.Visible = true;
        //lnkbtnAddPPESAForm.Visible = true;
        headerTitle.InnerHtml = "List PPESA";
        ViewState["FormType"] = Convert.ToInt32(FormType.PPESA);
        ResetBinding(Convert.ToInt32(ViewState["FormType"]));
    }

    protected void lnkbtnViewPDESAForm_Click(object sender, EventArgs e)
    {
        string cid = lnkbtnViewPDESAForm.ClientID;
        string script2 = "actvieclass(" + cid + ")";
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "script", script2, true);

        // pnlActivity.Visible = false;
        //pnlAddForm.Visible = false;
        pnlListPPESA.Visible = true;
        pnlListErrorRecord.Visible = false;
        //liAddFormP.Visible = true;
        //lnkbtnAddPPESAForm.Visible = true;
        headerTitle.InnerHtml = "List PDESA";
        ViewState["FormType"] = Convert.ToInt32(FormType.PDESA);
        ResetBinding(Convert.ToInt32(ViewState["FormType"]));
    }

    protected void lnkbtnSaveForm_Click(object sender, EventArgs e)
    {
        //if (PPESAnPDESA.GetDuplicateCheck(txtProductFeatureAdded.Text.Trim(), Convert.ToInt32(ViewState["poid"]), this.EditIDINT)) //// it will check if link name is already exist
        //{
        //    tbl_PPESAnPDESA data = new tbl_PPESAnPDESA();
        //    if (ViewState["FormType"] != null)
        //        data.FormType = Convert.ToInt32(ViewState["FormType"]);
        //    data.ProcessID = ProcessId;
        //    if (ViewState["poid"] != null)
        //        data.ProcessObjectID = Convert.ToInt32(ViewState["poid"]);
        //    data.ProductFeatureAdded = txtProductFeatureAdded.Text.Trim();
        //    data.FunctionofProductFeature = txtFunctionProductFeature.Text.Trim();
        //    data.ErrorEvent = txtErrorEvent.Text.Trim();
        //    data.ErrorEventTransferFunction = txtErrorEventTransferFunction.Text.Trim();
        //    data.Actions = txtActions.Text.Trim();
        //    data.ActionCriticalParameter = Convert.ToBoolean(drpActionCriticalParameter.SelectedItem.Value);
        //    data.Conditions = txtConditions.Text.Trim();
        //    data.ConditonCriticalParameter = Convert.ToBoolean(drpConditonCriticalParameter.Text.Trim());
        //    data.InitialSeverity = Convert.ToInt32(ddlInitialSeverity.SelectedItem.Value);
        //    data.InitialFrequency = Convert.ToInt32(ddlInitialFrequency.SelectedItem.Value);
        //    data.InitialDetection = Convert.ToInt32(ddlInitialDetection.SelectedItem.Value);
        //    data.IntialRPN = Convert.ToInt32(txtIntialRPN1.Text.Trim());
        //    data.Countermeasure = txtCountermeasure.Text.Trim();
        //    data.CountermeasureEffectiveness = Convert.ToInt32(ddlCountermeasureEffectiveness.SelectedItem.Value);
        //    data.FinalSeverity = Convert.ToInt32(ddlFinalSeverity.Text.Trim());
        //    data.FinalFrequency = Convert.ToInt32(ddlFinalFrequency.Text.Trim());
        //    data.FinalDetection = Convert.ToInt32(ddlFinalDetection.Text.Trim());
        //    data.FinalRPN = Convert.ToInt32(txtFinalRPN1.Text.Trim());

        //    if (this.EditIDINT > 0)    //// for edit mode
        //    {
        //        data.FormID = this.EditIDINT;
        //    }

        //    bool result = false;
        //    // result =
        //    result = PPESAnPDESA.SavePPESAnPDESAData(data);  ////SaveInputData will dave input link in database table information input

        //    if (result == true)  // if record is updated or inserted
        //    {
        //        ResetBinding(Convert.ToInt32(ViewState["FormType"]));
        //        // pnlActivity.Visible = false;
        //       // pnlAddForm.Visible = false;
        //        pnlListPPESA.Visible = true;
        //        lnkbtnSaveForm.Visible = false;
        //        lblMsg.Visible = true;
        //        divErrorMsg.Visible = true;
        //        lblMsg.Text = "Report saved successfully.";
        //        lblMsg.Style.Add("color", "green");
        //        divErrorMsg.Attributes.Add("class", "isa_success");

        //        string cid = string.Empty;
        //        if (Convert.ToInt32(ViewState["FormType"]) == 0)
        //            cid = lnkbtnViewPPESAForm.ClientID;
        //        else
        //            cid = lnkbtnViewPDESAForm.ClientID;

        //        string script2 = "actvieclassByid(" + cid + ")";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(),
        //                      "script", script2, true);
        //    }
        //    else
        //    {
        //        // pnlActivity.Visible = false;
        //       // pnlAddForm.Visible = true;
        //        pnlListPPESA.Visible = false;
        //        lnkbtnSaveForm.Visible = false;
        //        lblMsg.Visible = true;
        //        divErrorMsg.Visible = true;
        //        lblMsg.Text = "Error on Deleting data.!";
        //        lblMsg.Style.Add("color", "red");
        //        divErrorMsg.Attributes.Add("class", "isa_error");
        //    }
        //}

        int formType = 0;
        if (ViewState["FormType"] != null)
            formType = Convert.ToInt32(ViewState["FormType"]);

        if (formType == 2)
        {

          
            foreach (GridViewRow row in grdErrorGrid.Rows)
            {
                ErrorInfo data = new ErrorInfo();
                data.ErrorID = Convert.ToInt32((row.FindControl("hdnErrorID") as HiddenField).Value);
                data.ProcessID = Convert.ToInt32((row.FindControl("hdnProcessID") as HiddenField).Value);

                string error = (row.FindControl("txtError") as TextBox).Text;
                data.Error = error;

                string cycleTime= (row.FindControl("txtCycleTime") as TextBox).Text;
                if (string.IsNullOrEmpty(cycleTime))
                     data.CycleTime = 0;
                else
                data.CycleTime = Convert.ToInt32(cycleTime);


                string workContent = (row.FindControl("txtWorkContent") as TextBox).Text;
                if (string.IsNullOrEmpty(workContent))
                    data.WorkContent = 0;
                else
                    data.WorkContent = Convert.ToInt32(workContent);

                string counterMeasure = (row.FindControl("txtCounterMeasure") as TextBox).Text;
                if (string.IsNullOrEmpty(counterMeasure))
                    data.CounterMeasure = 0;
                else
                    data.CounterMeasure = Convert.ToInt32(counterMeasure);

                string counterMeasureStrength = (row.FindControl("txtCounterMeasureStrength") as TextBox).Text;
                if (string.IsNullOrEmpty(counterMeasureStrength))
                    data.CounterMeasureStrength = 0;
                else
                    data.CounterMeasureStrength = Convert.ToInt32(counterMeasureStrength);
               
                

                new ErrorData().Save(data);
            }

        }
        else
        {
            bool updateForm = false;
            tbl_PPESAnPDESA data = new tbl_PPESAnPDESA();
            foreach (GridViewRow row in gridPPESA.Rows)
            {
                data.FormID = Convert.ToInt32((row.FindControl("litFormID") as Literal).Text);
                data.ProcessID = ProcessId;
                DropDownList ddlProcessObjectID = row.FindControl("ddlProcessObjectID") as DropDownList;
                if (ddlProcessObjectID.SelectedIndex > 0)
                {
                    data.ProcessObjectID = Convert.ToInt32((row.FindControl("ddlProcessObjectID") as DropDownList).SelectedValue);
                }
                else
                {
                    data.ProcessObjectID = null;
                }
                data.Sequence = Convert.ToInt32((row.FindControl("litSequence") as Literal).Text);
                data.FormType = Convert.ToInt32((row.FindControl("litFormType") as Literal).Text);
                data.ProductFeatureAdded = (row.FindControl("txtProductFeatureAdded") as TextBox).Text;
                data.FunctionofProductFeature = (row.FindControl("txtFunctionProductFeature") as TextBox).Text;
                data.ErrorEvent = (row.FindControl("txtErrorEvent") as TextBox).Text;
                data.ErrorEventTransferFunction = (row.FindControl("txtErrorEventTransferFunction") as TextBox).Text;
                data.Actions = (row.FindControl("txtActions") as TextBox).Text;
                data.ActionCriticalParameter = Convert.ToBoolean((row.FindControl("drpActionCriticalParameter") as DropDownList).SelectedValue);
                data.Conditions = (row.FindControl("txtConditions") as TextBox).Text;
                data.ConditonCriticalParameter = Convert.ToBoolean((row.FindControl("drpConditonCriticalParameter") as DropDownList).SelectedValue);
                data.InitialSeverity = Convert.ToInt32((row.FindControl("ddlInitialSeverity") as DropDownList).SelectedValue);
                data.InitialFrequency = Convert.ToInt32((row.FindControl("ddlInitialFrequency") as DropDownList).SelectedValue);
                data.InitialDetection = Convert.ToInt32((row.FindControl("ddlInitialDetection") as DropDownList).SelectedValue);
                TextBox txtIntialRPN = row.FindControl("txtIntialRPN") as TextBox;
                if (txtIntialRPN.Text != "")
                {
                    data.IntialRPN = Convert.ToInt32((row.FindControl("txtIntialRPN") as TextBox).Text);
                }
                else
                {
                    data.IntialRPN = null;
                }
                data.Countermeasure = (row.FindControl("txtCountermeasure") as TextBox).Text;
                data.CountermeasureEffectiveness = Convert.ToInt32((row.FindControl("ddlCountermeasureEffectiveness") as DropDownList).SelectedValue);
                data.FinalSeverity = Convert.ToInt32((row.FindControl("ddlFinalSeverity") as DropDownList).SelectedValue);
                data.FinalFrequency = Convert.ToInt32((row.FindControl("ddlFinalFrequency") as DropDownList).SelectedValue);
                data.FinalDetection = Convert.ToInt32((row.FindControl("ddlFinalDetection") as DropDownList).SelectedValue);
                TextBox txtFinalRPN = row.FindControl("txtFinalRPN") as TextBox;
                if (txtFinalRPN.Text != "")
                {
                    data.FinalRPN = Convert.ToInt32((row.FindControl("txtFinalRPN") as TextBox).Text);
                }
                else
                {
                    data.FinalRPN = null;
                }
                updateForm = PPESAnPDESA.SavePPESAnPDESAData(data); // update aditional fields in form table                            
            }
            if (updateForm == true)  // if record is updated or inserted
            {
                ResetBinding(Convert.ToInt32(ViewState["FormType"]));
                pnlListPPESA.Visible = true;
                //lnkbtnSaveForm.Visible = false;
                lblMsg.Visible = true;
                divErrorMsg.Visible = true;
                lblMsg.Text = "Report saved successfully.";
                lblMsg.Style.Add("color", "green");
                divErrorMsg.Attributes.Add("class", "isa_success");

                string cid = string.Empty;
                if (Convert.ToInt32(ViewState["FormType"]) == 0)
                    cid = lnkbtnViewPPESAForm.ClientID;
                else
                    cid = lnkbtnViewPDESAForm.ClientID;

                string script2 = "actvieclassByid(" + cid + ")";
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                              "script", script2, true);
            }
            else
            {
                pnlListPPESA.Visible = false;
                lnkbtnSaveForm.Visible = false;
                lblMsg.Visible = true;
                divErrorMsg.Visible = true;
                lblMsg.Text = "Error on Deleting data.!";
                lblMsg.Style.Add("color", "red");
                divErrorMsg.Attributes.Add("class", "isa_error");
            }
        }
    }

    public void ClearControl()
    {
        //txtProductFeatureAdded.Text = string.Empty;
        //txtFunctionProductFeature.Text = string.Empty;
        //txtErrorEvent.Text = string.Empty;
        //txtErrorEventTransferFunction.Text = string.Empty;
        //txtActions.Text = string.Empty;
        //txtConditions.Text = string.Empty;
        //this.ddlInitialSeverity.SelectedIndex = -1;
        //this.ddlInitialFrequency.SelectedIndex = -1;
        //this.ddlInitialDetection.SelectedIndex = -1;
        //txtIntialRPN1.Text = string.Empty;
        //txtCountermeasure.Text = string.Empty;
        //this.ddlCountermeasureEffectiveness.SelectedIndex = -1;
        //this.ddlFinalSeverity.SelectedIndex = -1;
        //this.ddlFinalFrequency.SelectedIndex = -1;
        //this.ddlFinalDetection.SelectedIndex = -1;
        //txtFinalRPN1.Text = string.Empty;
        //this.drpActionCriticalParameter.SelectedIndex = -1;
        //this.drpConditonCriticalParameter.SelectedIndex = -1;
    }

    public void BindGrid()
    {
        if (Convert.ToInt32(ViewState["poId"]) > -1)
        {
            gridPPESA.DataSource = PPESAnPDESA.GetPPESAnPDESAData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ViewState["FormType"]), ProcessId);
            gridPPESA.DataBind();
        }
    }

    public void ResetBinding(int formType)
    {
        //ViewState["sortBy"] = "ProductFeatureAdded";

        ViewState["sortBy"] = "Sequence";
        ViewState["isAsc"] = "1";
        if (Convert.ToInt32(ViewState["poId"]) > -1)
        {
            if(formType==2)
            {
                ErrorData errorData = new ErrorData();
                List<ErrorInfo> listData = errorData.GetAll();
                grdErrorGrid.DataSource = listData;
                grdErrorGrid.DataBind();

                pnlListPPESA.Visible = false;
                pnlListErrorRecord.Visible = true;
            }
            else
            {
                List<PPESAnPDESA.ListPPESAnPDESAData> listData = PPESAnPDESA.GetPPESAnPDESAData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), formType, ProcessId);
                gridPPESA.DataSource = listData;
                gridPPESA.DataBind();
                if (listData.Count == 0)
                {
                    List<ProcessData.ProcessDataProperty> acty = ProcessData.GetProcessObjActvities(ProcessId);
                    if (acty.Count > 0)
                    {
                        btnAddNewRow.Visible = true;
                        if (RoleID == 4)
                        {
                            btnAddNewRow.Visible = false;
                        }
                    }
                    else
                    {
                        btnAddNewRow.Visible = false;
                    }

                    //btnAddNewRow.Visible = false;
                    lnkbtnSaveForm.Visible = false;
                }
                else
                {
                    btnAddNewRow.Visible = true;
                    if (RoleID == 4)
                    {
                        btnAddNewRow.Visible = false;
                    }
                    lnkbtnSaveForm.Visible = true;
                    if (RoleID == 4)
                    {
                        lnkbtnSaveForm.Visible = false;
                    }
                }
                pnlListPPESA.Visible = true;
                pnlListErrorRecord.Visible = false;
            }

           
            
        }

       
        //pnlActivity.Visible = false;
        //pnlAddForm.Visible = false;
    }

    protected void gridPPESA_Sorting(object sender, GridViewSortEventArgs e)
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

    protected void gridPPESA_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //gridAttribute.PageIndex = e.;
        //BindGrid();
    }

    protected void gridPPESA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridPPESA.PageIndex = e.NewPageIndex;
        this.BindGrid(); //// bind grid view       
    }

    protected void gridPPESA_RowCreated(object sender, GridViewRowEventArgs e)
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
        //if (RoleID == 4)
        //{
        //    e.Row.Cells[0].Visible = false;
        //}
    }

    //protected void gridPPESA_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    ClearControl();
    //    this.EditIDINT = this.CInt32(((Literal)gridPPESA.Rows[e.NewEditIndex].FindControl("litFormID")).Text);
    //    ViewState["poid"] = this.CInt32(((Literal)gridPPESA.Rows[e.NewEditIndex].FindControl("litpoid")).Text);
    //    ViewState["FormType"] = this.CInt32(((Literal)gridPPESA.Rows[e.NewEditIndex].FindControl("litFormType")).Text); ;
    //    //FillPPESADataById();
    //   // pnlActivity.Visible = false;
    //    pnlListPPESA.Visible = false;
    //    pnlAddForm.Visible = true;
    //    lnkbtnSaveForm.Visible = true;

    //    //liAddFormP.Visible = false;
    //    string cid = string.Empty;
    //    if (Convert.ToInt32(ViewState["FormType"]) == 0)
    //        cid = lnkbtnViewPPESAForm.ClientID;
    //    else
    //        cid = lnkbtnViewPDESAForm.ClientID;

    //    string script2 = "actvieclassByid(" + cid + ")";
    //    ScriptManager.RegisterStartupScript(this, this.GetType(),
    //                  "script", script2, true);
    //}

    protected void gridPPESA_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int formtyp = 0;
        GridViewRow row = gridPPESA.Rows[e.RowIndex];
        Literal delSequence = (Literal)row.FindControl("litDelSequence");
        if (ViewState["FormType"] != null)
            formtyp = Convert.ToInt32(ViewState["FormType"]);
        bool decrease = false;
        decrease = PPESAnPDESA.DecreaseNextRowsSequence(ProcessId, Convert.ToInt32(delSequence.Text), formtyp); //first decrease the next sequence of all rows by 1 and than delete the selected row
        if (decrease == true)
        {
            ////idDelete will get Attribute Id data for selected grid view row on delete button
            int idDelete = this.CInt32(((ImageButton)gridPPESA.Rows[e.RowIndex].FindControl("deleteBtn")).CommandArgument);
            if (idDelete > 0)
            {
                bool result = false;
                result = PPESAnPDESA.DeletePPESAnPDESA(idDelete); ////DeleteAttributeData is stored procedure in database that will delete selected Attribute id from multiple tables
                if (result)
                {
                    lblMsg.Visible = true;
                    divErrorMsg.Visible = true;
                    lblMsg.Text = "Record has been deleted successfully";
                    lblMsg.Style.Add("color", "green");
                    divErrorMsg.Style.Add("width", "290px");
                    divErrorMsg.Style.Add("margin-right", "600px");
                    divErrorMsg.Style.Add("margin-left", "0px");
                    divErrorMsg.Style.Add("float", "right");
                    divErrorMsg.Style.Add("padding", "8px 0px 0px 60px");
                    divErrorMsg.Attributes.Add("class", "isa_success");
                }
                else
                {
                    lblMsg.Visible = true;
                    divErrorMsg.Visible = true;
                    lblMsg.Text = "Error on Deleting data.!";
                    lblMsg.Style.Add("color", "red");
                    divErrorMsg.Style.Add("width", "290px");
                    divErrorMsg.Style.Add("margin-right", "600px");
                    divErrorMsg.Style.Add("margin-left", "0px");
                    divErrorMsg.Style.Add("float", "right");
                    divErrorMsg.Style.Add("padding", "8px 0px 0px 60px");
                    divErrorMsg.Attributes.Add("class", "isa_error");

                }
                //ClearControl();
                ResetBinding(Convert.ToInt32(ViewState["FormType"]));
                this.EditIDINT = 0;
            }
        }
    }

    //public void FillPPESADataById()
    //{
    //    tbl_PPESAnPDESA PPESAData = new tbl_PPESAnPDESA();
    //    PPESAData = PPESAnPDESA.PPESAnPDESAByID(this.EditIDINT);////AttributeById will get Attribute by its id that is EditIDINT
    //    if (PPESAData != null)
    //    {
    //        txtProductFeatureAdded.Text = PPESAData.ProductFeatureAdded;
    //        txtFunctionProductFeature.Text = PPESAData.FunctionofProductFeature;
    //        txtErrorEvent.Text = PPESAData.ErrorEvent;
    //        txtErrorEventTransferFunction.Text = PPESAData.ErrorEventTransferFunction;
    //        txtActions.Text = PPESAData.Actions;
    //        txtConditions.Text = PPESAData.Conditions;

    //        txtIntialRPN.Text = Convert.ToString(PPESAData.IntialRPN);
    //        txtCountermeasure.Text = Convert.ToString(PPESAData.Countermeasure);
    //        this.SetDropDownListValue(ddlCountermeasureEffectiveness, Convert.ToString(PPESAData.CountermeasureEffectiveness));

    //        txtFinalRPN.Text = Convert.ToString(PPESAData.FinalRPN);
    //        this.SetDropDownListValue(drpActionCriticalParameter, PPESAData.ActionCriticalParameter.ToString());
    //        this.SetDropDownListValue(drpConditonCriticalParameter, PPESAData.ConditonCriticalParameter.ToString());

    //        this.SetDropDownListValue(ddlInitialSeverity, Convert.ToString(PPESAData.InitialSeverity));
    //        this.SetDropDownListValue(ddlInitialFrequency, Convert.ToString(PPESAData.InitialFrequency));
    //        this.SetDropDownListValue(ddlInitialDetection, Convert.ToString(PPESAData.InitialDetection));

    //        this.SetDropDownListValue(ddlFinalSeverity, Convert.ToString(PPESAData.FinalSeverity));
    //        this.SetDropDownListValue(ddlFinalFrequency, Convert.ToString(PPESAData.FinalFrequency));
    //        this.SetDropDownListValue(ddlFinalDetection, Convert.ToString(PPESAData.FinalDetection));

    //        lnkbtnSaveForm.Text = "Update Record";
    //    }
    //}

    public string GetPostBackControlId(Page page)
    {
        if (!page.IsPostBack)
            return string.Empty;

        System.Web.UI.Control control = null;
        // first we will check the "__EVENTTARGET" because if post back made by the controls
        // which used "_doPostBack" function also available in Request.Form collection.
        string controlName = page.Request.Params["__EVENTTARGET"];
        if (!String.IsNullOrEmpty(controlName))
        {
            control = page.FindControl(controlName);
        }
        else
        {
            // if __EVENTTARGET is null, the control is a button type and we need to
            // iterate over the form collection to find it

            // ReSharper disable TooWideLocalVariableScope
            string controlId;
            System.Web.UI.Control foundControl;
            // ReSharper restore TooWideLocalVariableScope

            foreach (string ctl in page.Request.Form)
            {
                // handle ImageButton they having an additional "quasi-property" 
                // in their Id which identifies mouse x and y coordinates
                if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                {
                    controlId = ctl.Substring(0, ctl.Length - 2);
                    foundControl = page.FindControl(controlId);
                }
                else
                {
                    foundControl = page.FindControl(ctl);
                }

                if (!(foundControl is Button || foundControl is ImageButton)) continue;

                control = foundControl;
                break;
            }
        }

        return control == null ? String.Empty : control.ID;
    }

    protected void gridPPESA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlProcessObjectID = (DropDownList)e.Row.FindControl("ddlProcessObjectID");
            DropDownList ddlActionCriticalParameter = (DropDownList)e.Row.FindControl("drpActionCriticalParameter");
            DropDownList ddlConditonCriticalParameter = (DropDownList)e.Row.FindControl("drpConditonCriticalParameter");
            DropDownList drpInitialSeverity = (DropDownList)e.Row.FindControl("ddlInitialSeverity");
            DropDownList drpInitialFrequency = (DropDownList)e.Row.FindControl("ddlInitialFrequency");
            DropDownList drpInitialDetection = (DropDownList)e.Row.FindControl("ddlInitialDetection");
            DropDownList drpFinalSeverity = (DropDownList)e.Row.FindControl("ddlFinalSeverity");
            DropDownList drpFinalFrequency = (DropDownList)e.Row.FindControl("ddlFinalFrequency");
            DropDownList drpFinalDetection = (DropDownList)e.Row.FindControl("ddlFinalDetection");
            TextBox txtIntialRPN = (TextBox)e.Row.FindControl("txtIntialRPN");
            TextBox txtFinalRPN = (TextBox)e.Row.FindControl("txtFinalRPN");

            txtIntialRPN.Attributes.Add("readonly", "readonly");
            txtFinalRPN.Attributes.Add("readonly", "readonly");
            //  fill costume ddl
            List<ProcessData.ProcessDataProperty> activities = ProcessData.GetProcessObjActvities(ProcessId);
            if (activities.Count > 0)
            {
                foreach (var c in activities)
                {
                    ddlProcessObjectID.Items.Add(new ListItem(c.ProcessObjectName, Convert.ToString(c.ProcessObjID)));
                }
                ddlProcessObjectID.Items.Insert(0, "--Select Activity--");
            }
            if (DataBinder.Eval(e.Row.DataItem, "ProcessObjectID") != null)
            {
                ddlProcessObjectID.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ProcessObjectID"));
            }
            if (DataBinder.Eval(e.Row.DataItem, "ActionCriticalParameter") != null)
            {
                ddlActionCriticalParameter.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ActionCriticalParameter"));
            }
            if (DataBinder.Eval(e.Row.DataItem, "ConditonCriticalParameter") != null)
            {
                ddlConditonCriticalParameter.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ConditonCriticalParameter"));
            }
            if (DataBinder.Eval(e.Row.DataItem, "InitialSeverity") != null)
            {
                drpInitialSeverity.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InitialSeverity"));
                drpInitialSeverity.Attributes["onchange"] = "calculateInitialSeverity(this,'" + txtIntialRPN.ClientID + "','" + drpInitialFrequency.ClientID + "','" + drpInitialDetection.ClientID + "');";
            }
            else
            {
                drpInitialSeverity.Attributes["onchange"] = "calculateInitialSeverity(this,'" + txtIntialRPN.ClientID + "','" + drpInitialFrequency.ClientID + "','" + drpInitialDetection.ClientID + "');";
            }
            if (DataBinder.Eval(e.Row.DataItem, "InitialFrequency") != null)
            {
                drpInitialFrequency.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InitialFrequency"));
                drpInitialFrequency.Attributes["onchange"] = "calculateInitialFrequency(this,'" + txtIntialRPN.ClientID + "','" + drpInitialSeverity.ClientID + "','" + drpInitialDetection.ClientID + "');";
            }
            else
            {
                drpInitialFrequency.Attributes["onchange"] = "calculateInitialFrequency(this,'" + txtIntialRPN.ClientID + "','" + drpInitialSeverity.ClientID + "','" + drpInitialDetection.ClientID + "');";
            }
            if (DataBinder.Eval(e.Row.DataItem, "InitialDetection") != null)
            {
                drpInitialDetection.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InitialDetection"));
                drpInitialDetection.Attributes["onchange"] = "calculateInitialDetection(this,'" + txtIntialRPN.ClientID + "','" + drpInitialSeverity.ClientID + "','" + drpInitialFrequency.ClientID + "');";
            }
            else
            {
                drpInitialDetection.Attributes["onchange"] = "calculateInitialDetection(this,'" + txtIntialRPN.ClientID + "','" + drpInitialSeverity.ClientID + "','" + drpInitialFrequency.ClientID + "');";
            }
            if (DataBinder.Eval(e.Row.DataItem, "CountermeasureEffectiveness") != null)
            {
                DropDownList drpCountermeasureEffectiveness = (DropDownList)e.Row.FindControl("ddlCountermeasureEffectiveness");
                drpCountermeasureEffectiveness.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CountermeasureEffectiveness"));
            }
            if (DataBinder.Eval(e.Row.DataItem, "FinalSeverity") != null)
            {
                drpFinalSeverity.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FinalSeverity"));
                drpFinalSeverity.Attributes["onchange"] = "calculateFinalSeverity(this,'" + txtFinalRPN.ClientID + "','" + drpFinalFrequency.ClientID + "','" + drpFinalDetection.ClientID + "');";
            }
            else
            {
                drpFinalSeverity.Attributes["onchange"] = "calculateFinalSeverity(this,'" + txtFinalRPN.ClientID + "','" + drpFinalFrequency.ClientID + "','" + drpFinalDetection.ClientID + "');";
            }
            if (DataBinder.Eval(e.Row.DataItem, "FinalFrequency") != null)
            {
                drpFinalFrequency.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FinalFrequency"));
                drpFinalFrequency.Attributes["onchange"] = "calculateFinalFrequency(this,'" + txtFinalRPN.ClientID + "','" + drpFinalSeverity.ClientID + "','" + drpFinalDetection.ClientID + "');";
            }
            else
            {
                drpFinalFrequency.Attributes["onchange"] = "calculateFinalFrequency(this,'" + txtFinalRPN.ClientID + "','" + drpFinalSeverity.ClientID + "','" + drpFinalDetection.ClientID + "');";
            }
            if (DataBinder.Eval(e.Row.DataItem, "FinalDetection") != null)
            {
                drpFinalDetection.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FinalDetection"));
                drpFinalDetection.Attributes["onchange"] = "calculateFinalDetection(this,'" + txtFinalRPN.ClientID + "','" + drpFinalSeverity.ClientID + "','" + drpFinalFrequency.ClientID + "');";
            }
            else
            {
                drpFinalDetection.Attributes["onchange"] = "calculateFinalDetection(this,'" + txtFinalRPN.ClientID + "','" + drpFinalSeverity.ClientID + "','" + drpFinalFrequency.ClientID + "');";
            }
        }
    }

    protected void btnAddNewRow_Click(object sender, EventArgs e)
    {
        int formtyp = 0;
        tbl_PPESAnPDESA data = new tbl_PPESAnPDESA();
        if (ViewState["FormType"] != null)
            formtyp = Convert.ToInt32(ViewState["FormType"]);

        data.FormType = formtyp;

        int maxSequenceNo = 0;
        maxSequenceNo = PPESAnPDESA.GetMaxSequenceNo(ProcessId, formtyp);
        //add row at next sequence

        data.Sequence = maxSequenceNo + 1; //next sequence after maxsequence no
        data.ProcessID = ProcessId;
        data.ActionCriticalParameter = false;
        data.ConditonCriticalParameter = false;
        bool result = false;
        // result =
        result = PPESAnPDESA.SavePPESAnPDESAData(data);  ////SaveInputData will dave input link in database table information input

        if (result == true)  // if record is updated or inserted
        {
            ResetBinding(Convert.ToInt32(ViewState["FormType"]));
            // pnlActivity.Visible = false;
            //pnlAddForm.Visible = false;
            pnlListPPESA.Visible = true;
            //lnkbtnSaveForm.Visible = false;
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Row added successfully";
            lblMsg.Style.Add("color", "green");
            divErrorMsg.Attributes.Add("class", "isa_success");

            string cid = string.Empty;
            if (Convert.ToInt32(ViewState["FormType"]) == 0)
                cid = lnkbtnViewPPESAForm.ClientID;
            else
                cid = lnkbtnViewPDESAForm.ClientID;

            string script2 = "actvieclassByid(" + cid + ")";
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "script", script2, true);
        }
        else
        {
            // pnlActivity.Visible = false;
            //pnlAddForm.Visible = true;
            pnlListPPESA.Visible = false;
            //lnkbtnSaveForm.Visible = false;
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Error on adding row!";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Attributes.Add("class", "isa_error");
        }
    }

    protected void btnAddNewErrorRecord_Click(object sender, EventArgs e)
    {
        int formtyp = 0;
        ErrorInfo data = new ErrorInfo();
        if (ViewState["FormType"] != null)
            formtyp = Convert.ToInt32(ViewState["FormType"]);

        //data.FormType = formtyp;

        int maxSequenceNo = 0;
        maxSequenceNo =new ErrorData().GetMaxSequenceNo(ProcessId, formtyp);
        ////add row at next sequence 
         data.ProcessID = ProcessId;
         data.Error = string.Empty;
         data.CycleTime = 0;
        data.CounterMeasure = 0;
        data.CounterMeasureStrength = 0;
        data.WorkContent = 0;
         bool result = false;
        
         result = new ErrorData().Save(data);  ////SaveInputData will dave input link in database table information input

         if (result == true)  // if record is updated or inserted
         {
            ResetBinding(Convert.ToInt32(ViewState["FormType"]));
            // pnlActivity.Visible = false;
            //pnlAddForm.Visible = false;
            pnlListPPESA.Visible = false;
            pnlListErrorRecord.Visible = true;
            //lnkbtnSaveForm.Visible = false;
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Row added successfully";
            lblMsg.Style.Add("color", "green");
            divErrorMsg.Attributes.Add("class", "isa_success");

            string cid = string.Empty;
            if (Convert.ToInt32(ViewState["FormType"]) == 0)
                cid = lnkbtnViewPPESAForm.ClientID;
            else if (Convert.ToInt32(ViewState["FormType"]) == 2)
                cid = lnkbtnErrorRecord.ClientID;
            else
                cid = lnkbtnViewPDESAForm.ClientID;

            string script2 = "actvieclassByid(" + cid + ")";
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "script", script2, true);
        }
        else
        {
            // pnlActivity.Visible = false;
            //pnlAddForm.Visible = true;
            pnlListPPESA.Visible = false;
            pnlListErrorRecord.Visible = false;
            //lnkbtnSaveForm.Visible = false;
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Error on adding row!";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Attributes.Add("class", "isa_error");
        }
    }

    private void FillddlNumbers(DropDownList ddl)
    {
        // This would create 1 - 10
        for (int i = 1; i < 11; i++)
        {
            ListItem li = new ListItem();
            li.Text = i.ToString();
            li.Value = i.ToString();
            ddl.Items.Add(li);
        }
    }

    private void FillddlNumber(DropDownList ddl)
    {
        // This would create 1 - 5
        for (int i = 1; i < 6; i++)
        {
            ListItem li = new ListItem();
            li.Text = i.ToString();
            li.Value = i.ToString();
            ddl.Items.Add(li);
        }
    }
    protected void gridPPESA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int formtyp = 0;
        int sequence = int.Parse(e.CommandArgument.ToString()); //find row sequence
        if (ViewState["FormType"] != null)
            formtyp = Convert.ToInt32(ViewState["FormType"]);

        if (e.CommandName == "add")
        {
            //increase sequense of row and next rows by one 
            bool increase = false;
            increase = PPESAnPDESA.IncreaseNextRowsSequence(ProcessId, sequence, formtyp);

            if (increase == true)
            {
                // sequence updated successfully now add new row add commandargument sequence

                tbl_PPESAnPDESA data = new tbl_PPESAnPDESA();

                data.FormType = formtyp;
                data.Sequence = sequence;
                data.ProcessID = ProcessId;
                data.ActionCriticalParameter = false;
                data.ConditonCriticalParameter = false;
                bool result = false;
                // result =
                result = PPESAnPDESA.SavePPESAnPDESAData(data);  ////SaveInputData will dave input link in database table information input

                if (result == true)  // if record is updated or inserted
                {
                    ResetBinding(Convert.ToInt32(ViewState["FormType"]));
                    // pnlActivity.Visible = false;
                    //pnlAddForm.Visible = false;
                    pnlListPPESA.Visible = true;
                    //lnkbtnSaveForm.Visible = false;
                    lblMsg.Visible = true;
                    divErrorMsg.Visible = true;
                    lblMsg.Text = "Row added successfully";
                    lblMsg.Style.Add("color", "green");
                    divErrorMsg.Attributes.Add("class", "isa_success");

                    string cid = string.Empty;
                    if (Convert.ToInt32(ViewState["FormType"]) == 0)
                        cid = lnkbtnViewPPESAForm.ClientID;
                    else
                        cid = lnkbtnViewPDESAForm.ClientID;

                    string script2 = "actvieclassByid(" + cid + ")";
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                  "script", script2, true);
                }
                else
                {
                    // pnlActivity.Visible = false;
                    //pnlAddForm.Visible = true;
                    pnlListPPESA.Visible = false;
                    //lnkbtnSaveForm.Visible = false;
                    lblMsg.Visible = true;
                    divErrorMsg.Visible = true;
                    lblMsg.Text = "Error on adding row!";
                    lblMsg.Style.Add("color", "red");
                    divErrorMsg.Attributes.Add("class", "isa_error");
                }
            }
            else
            {
                //error in next sequence updation
            }
        }

    }

    protected void lnkbtnErrorRecord_Click(object sender, EventArgs e)
    {
        ViewState["FormType"] = Convert.ToInt32(FormType.ERRORLOG);
        pnlListPPESA.Visible = false;
        pnlListErrorRecord.Visible = true;
        //ViewState["sortBy"] = "ProductFeatureAdded";
        ViewState["sortBy"] = "Sequence";
        ViewState["isAsc"] = "1";
        headerTitle.InnerText = "Error Log";
        if (Convert.ToInt32(ViewState["poId"]) > -1)
        {
            ErrorData errorData = new ErrorData();
            List<ErrorInfo> listData = errorData.GetAll();
            grdErrorGrid.DataSource = listData;
            grdErrorGrid.DataBind();


            btnAddNewRow.Visible = true;
            if (RoleID == 4)
            {
                btnAddNewRow.Visible = false;
            }
            lnkbtnSaveForm.Visible = true;
            if (RoleID == 4)
            {
                lnkbtnSaveForm.Visible = false;
            }

        }
         
    }

    protected void grdErrorGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            int formtyp = 0;
            ErrorInfo data = new ErrorInfo();
            if (ViewState["FormType"] != null)
                formtyp = Convert.ToInt32(ViewState["FormType"]);

            //data.FormType = formtyp;

            int maxSequenceNo = 0;
            maxSequenceNo = new ErrorData().GetMaxSequenceNo(ProcessId, formtyp);
            ////add row at next sequence 
            data.ProcessID = ProcessId;
            data.Error = string.Empty;
            data.CycleTime = 0;
            bool result = false;

            result = new ErrorData().Save(data);  ////SaveInputData will dave input link in database table information input

            if (result == true)  // if record is updated or inserted
            {
                ResetBinding(Convert.ToInt32(ViewState["FormType"]));
                // pnlActivity.Visible = false;
                //pnlAddForm.Visible = false;
                pnlListPPESA.Visible = false;
                pnlListErrorRecord.Visible = true;
                //lnkbtnSaveForm.Visible = false;
                lblMsg.Visible = true;
                divErrorMsg.Visible = true;
                lblMsg.Text = "Row added successfully";
                lblMsg.Style.Add("color", "green");
                divErrorMsg.Attributes.Add("class", "isa_success");

                string cid = string.Empty;
                if (Convert.ToInt32(ViewState["FormType"]) == 0)
                    cid = lnkbtnViewPPESAForm.ClientID;
                else if (Convert.ToInt32(ViewState["FormType"]) == 2)
                    cid = lnkbtnErrorRecord.ClientID;
                else
                    cid = lnkbtnViewPDESAForm.ClientID;

                string script2 = "actvieclassByid(" + cid + ")";
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                              "script", script2, true);
            }
        }
        if (e.CommandName == "Remove")
        {
           int errorID=Convert.ToInt32( e.CommandArgument);
            new ErrorData().Delete(errorID);
            ResetBinding(Convert.ToInt32(ViewState["FormType"]));
            // pnlActivity.Visible = false;
            //pnlAddForm.Visible = false;
            pnlListPPESA.Visible = false;
            pnlListErrorRecord.Visible = true;
            //lnkbtnSaveForm.Visible = false;
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Row deleted successfully";
            lblMsg.Style.Add("color", "green");
            divErrorMsg.Attributes.Add("class", "isa_success");

            string cid = string.Empty;
            if (Convert.ToInt32(ViewState["FormType"]) == 0)
                cid = lnkbtnViewPPESAForm.ClientID;
            else if (Convert.ToInt32(ViewState["FormType"]) == 2)
                cid = lnkbtnErrorRecord.ClientID;
            else
                cid = lnkbtnViewPDESAForm.ClientID;

            string script2 = "actvieclassByid(" + cid + ")";
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "script", script2, true);
        }
    }

}