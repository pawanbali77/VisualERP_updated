using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Web.Services;

public partial class BOMReport : BasePage
{
    int ProcessId = 0;
    List<int> activity = new List<int>();
    List<string> activityName = new List<string>();
    Dictionary<int, string> activityDic = new Dictionary<int, string>();
    List<int> bomProcessID = new List<int>();
    List<string> bomProcessName = new List<string>();
    List<ProcessData.ProcessDataProperty> activityNode = new List<ProcessData.ProcessDataProperty>();

    Dictionary<int, string> actv = new Dictionary<int, string>();
    string str = string.Empty;
    string str1 = string.Empty;
   

    protected void Page_Load(object sender, EventArgs e)
    {
        string EditId = GetPostBackControlId((Page)sender);
        VisualERPDataContext obj = new VisualERPDataContext();
        // Handles Load Event
        //pnlActivity.Visible = true;
        //pnlAttribute.Visible = false;
        pnlReport.Visible = false;
        pnlActivity.Visible = false;
        //PanelReportType.Visible = true;
        
        //divRecordCount.Visible = false;

        Label lblManager = (Label)Master.FindControl("lblManager");
        lblManager.Text = "Report Manager";
        lblManager.Attributes.Add("class", "Enterprize");

        foreach (ListItem item in chkboxActivity.Items)
        {
            if (item.Selected)
            {
                activity.Add(Convert.ToInt32(item.Value));
                activityName.Add(Convert.ToString(item.Text));
                string acctivityname = Activity.GetActivityNameByProcessObjId(Convert.ToInt32(item.Value));
                activityDic.Add(Convert.ToInt32(item.Value), acctivityname);
                Session["Activity"] = activity;
                Session["ActivityName"] = activityName;
                Session["ActivityDictionary"] = activityDic;
            }
        }

        foreach (ListItem item in chkboxBomProcess.Items)
        {
            if (item.Selected)
            {
                bomProcessID.Add(Convert.ToInt32(item.Value));
                bomProcessName.Add(Convert.ToString(item.Text));
                Session["BomProcessID"] = bomProcessID;
                Session["BomProcessName"] = bomProcessName;
            }
        }

        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
        {
           
            //ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
            //Session["SelectedNodeValue"] = ProcessId;
            //ViewState["PreviousValue"] = ProcessId;

            // selecting node that have multiple child
       
            if (mastertreeview.SelectedNode.ChildNodes.Count > 0)
            {
                MakeChildReport();

                if ((EditId != "editBtn") && (EditId != "deleteBtn"))
                {
                lblMsg.Text = string.Empty;
                ViewState["sortBy"] = "ReportID";
                ViewState["isAsc"] = "1";
                List<Activity.ListReportData> lstData = new List<Activity.ListReportData>();
                lstData = Activity.GetReportByProcessID(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(mastertreeview.SelectedNode.Value), (int)ReportTypeID.Bom);
                if (lstData.Count != 0)
                {
                    pnlListSavedReport.Visible = true;
                    pnlActivity.Visible = false;
                    grdSavedReport.DataSource = lstData;
                    grdSavedReport.DataBind();

                }
                else
                {
                    //pnlListSavedReport.Visible = false;
                    //pnlActivity.Visible = true;
                    //MakeChildReport();
                }
                }                            
            }
            else
            {
                if ((EditId != "editBtn") && (EditId != "deleteBtn"))
                {
                    BindSavedReportGrid(this.CInt32(mastertreeview.SelectedNode.Value));
                    lblMsg.Text = string.Empty;
                }
                BindActivityCheckboxList(Convert.ToInt32(mastertreeview.SelectedNode.Value));
            }

       
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
            Session["SelectedNodeValue"] = ProcessId;
            ViewState["PreviousValue"] = ProcessId;

            // upto this 

            PanelReportType.Visible = true;
            pnlActivity.Visible = false;
            pnlBomProcess.Visible = false;
            pnlListSavedReport.Visible = false;

        }
        else if (Session["SelectedNodeValue"] != null)
        {
            ProcessId = this.CInt32(Session["SelectedNodeValue"]);
            ViewState["PreviousValue"] = ProcessId;
        }

        if (!IsPostBack)
        {
            BindSavedReportGrid(this.CInt32(ProcessId));
        }
        // BindActivityCheckboxList(Convert.ToInt32(mastertreeview.SelectedNode.Value));
        
    }

    public void MakeChildReport()
    {
        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        string pre = string.Empty;
        string mid = string.Empty;
        string post = string.Empty;
        //for (int i = 0; i < mastertreeview.SelectedNode.ChildNodes.Count; i++)
        //{
        //int ProID = Convert.ToInt32(mastertreeview.SelectedNode.ChildNodes[i].Value);
        //GetChildNodeValues(mastertreeview.Nodes[i]);
        GetChildNodeValues(mastertreeview.SelectedNode); // this is function that will get each child node value after selected node

        //}

        for (int i = 0; i < actv.Count; i++)
        {
            //int ProID = Convert.ToInt32(actv[i]);            
            int ProID = Convert.ToInt32(actv.ElementAt(i).Key);
            string NodeName = Convert.ToString(actv.ElementAt(i).Value);
            // ' ('+ 'a->'+'ProcessSys1'+'->'+ dbo.tbl_Process.ProcessName +')'
            if (i == 0)
            {
                // mid = "" + NodeName + "'+'->";
                // pre = "SELECT tab1.ProcessObjID , ( + tab1.ProcessObjName +  '    (" + NodeName + "'+'->'+ dbo.tbl_Process.ProcessName +')') as ActivityName";               
                // post = " FROM (SELECT  ProcessObjID, ProcessObjName, ProcessID FROM  dbo.tbl_ProcessObject WHERE (ProcessID = '" + ProID + "') AND (ProcessObjName IS NOT NULL) ";
                //pre = "SELECT tab1.ProcessObjID ,( +tab1.ActivityName + dbo.tbl_Process.ProcessName) as ActivityName ";
                pre = "SELECT tab1.ProcessObjID ,( +tab1.ActivityName) as ActivityName ";
                mid = "FROM (SELECT  ProcessObjID, ( + ProcessObjName +  '    (" + NodeName + "'+')') as ActivityName, ProcessID FROM  dbo.tbl_ProcessObject WHERE (ProcessID = '" + ProID + "') AND (ProcessObjName IS NOT NULL) ";
                //post = "AS tab1 INNER JOIN dbo.tbl_Process ON tab1.ProcessID = dbo.tbl_Process.ProcessID";
            }
            else
            {
                //mid = "" + NodeName + "'+'->";
                // pre = "SELECT tab1.ProcessObjID , ( + tab1.ProcessObjName +  '    (" + mid + "'+'->'+ dbo.tbl_Process.ProcessName +')') as ActivityName";
                //post += "UNION SELECT  ProcessObjID, ProcessObjName, ProcessID FROM dbo.tbl_ProcessObject WHERE (ProcessID = '" + ProID + "') AND (ProcessObjName IS NOT NULL)";
                //pre = "";
                // mid = "";
                post += "UNION SELECT  ProcessObjID, ( + ProcessObjName +  '    (" + NodeName + "'+')') as ActivityName, ProcessID FROM dbo.tbl_ProcessObject WHERE (ProcessID = '" + ProID + "') AND (ProcessObjName IS NOT NULL)";
            }

        }
        //ViewState["sortBy"] = "CreatedDate";
        //ViewState["isAsc"] = "1";
        //BindActivityCheckboxList(ProID);

        string query = "" + pre + "" + mid + "" + post + ")" + " AS tab1 INNER JOIN dbo.tbl_Process ON tab1.ProcessID = dbo.tbl_Process.ProcessID";

        //List<ProcessData.ProcessDataProperty> activityNode = obj.ExecuteQuery<ProcessData.ProcessDataProperty>query;
        DataSet ds = GetData(query);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        //activityNode.AddRange(dt);

        if (dt.Rows.Count > 0)
        {
            chkboxActivity.DataSource = dt;
            chkboxActivity.DataTextField = "ActivityName";
            chkboxActivity.DataValueField = "ProcessObjID";
            chkboxActivity.DataBind();
            pnlActivity.Visible = true;
            pnlListSavedReport.Visible = false;
        }
        else
        {
            //Message.Text = "Unable to connect to the database.";
        }
    }

    public void GetChildNodeValues(TreeNode node)
    {
        str = Convert.ToString(node.Text);
        str1 += str.ToString() + "->";

        string splitStr = str1.Remove((str1.Length - 2), 2).ToString();

        // Display the node's text value.
        //lblText.Text += node.Text + "<br />";       
        //actv.Add(Convert.ToInt32(node.Value));

        actv.Add(Convert.ToInt32(node.Value), splitStr);

        // Iterate through the child nodes of the parent node passed into
        // this method and display their values.
        int i = 0;
        for (i = 0; i < node.ChildNodes.Count; i++)
        {

            // Recursively call the DisplayChildNodeText method to
            // traverse the tree and display all the child nodes.
            GetChildNodeValues(node.ChildNodes[i]);
        }

        if (node.ChildNodes.Count == i)
            str1 = str1.Substring(0, str1.LastIndexOf(node.Text));


    }

    public DataSet GetData(String queryString)
    {
        System.Data.SqlClient.SqlConnectionStringBuilder builder =
        new System.Data.SqlClient.SqlConnectionStringBuilder();

        DataSet ds = new DataSet();

        try
        {
            // Connect to the database and run the query.
            string sqlConnString = System.Configuration.ConfigurationManager.ConnectionStrings["VisualERPConnectionString"].ConnectionString;

            SqlDataAdapter adapter = new SqlDataAdapter(queryString, sqlConnString);
            // Fill the DataSet.
            adapter.Fill(ds);

        }
        catch (Exception ex)
        {
        }
        return ds;

    }

    public void BindSavedReportGrid(int proId)
    {
        //pnlActivity.Visible = true;
        ViewState["sortBy"] = "ReportID";
        ViewState["isAsc"] = "1";
        List<Activity.ListReportData> lstData = new List<Activity.ListReportData>();
        lstData = Activity.GetReportByProcessID(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), proId,(int)ReportTypeID.Bom);
        if (lstData.Count != 0)
        {
            pnlListSavedReport.Visible = true;
            pnlActivity.Visible = false;
            grdSavedReport.DataSource = lstData;
            grdSavedReport.DataBind();

        }
        else
        {
            //BindActivityCheckboxList(ProcessId);
            pnlListSavedReport.Visible = false;
            pnlActivity.Visible = true;
        }
    }

    public void BindActivityCheckboxList(int PoID)
    {
        lblMsg.Text = "";
        List<ProcessData.ProcessDataProperty> activities = ProcessData.GetProcessObjActvities(PoID);

        if (activities.Count != 0)
        {
            divAttribute.Visible = true;
            //
            activityNode.AddRange(activities);
            //
            chkboxActivity.DataSource = activityNode;
            chkboxActivity.DataTextField = "ProcessObjectName";
            chkboxActivity.DataValueField = "ProcessObjID";
            chkboxActivity.DataBind();
           // pnlActivity.Visible = true;
        }
        else
        {
            divAttribute.Visible = false;
        }
    }

    protected void btnNextToActivity_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        pnlBomProcess.Visible = true;
        pnlActivity.Visible = false;
        ArrayList List = new ArrayList();
        List<BomData.BomDataProcessProperty> attribute = new List<BomData.BomDataProcessProperty>();

        for (int i = 0; i < activity.Count; i++)
        {
            int prcobjID = Convert.ToInt32(activity[i].ToString()); //proobjId is selected atctivity id

            attribute.AddRange(BomData.GetAllBomProcess(prcobjID));

            if (attribute.Count != 0)
            {
                chkboxBomProcess.DataSource = attribute;
                chkboxBomProcess.DataTextField = "BomProcessName"; // textfield
                chkboxBomProcess.DataValueField = "BomProcessID"; //value field
                chkboxBomProcess.DataBind(); //binding chkboxBomProcess 
            }
            else
            {
                pnlBomProcess.Visible = false;
                lblMsg.Text = "No Bom Process Found under this Activity";
                lblMsg.Style.Add("color", "red");
            }
        }
       
    }

    protected void btnNextToBomProcess_Click(object sender, EventArgs e)
    {
        pnlBomProcess.Visible = false;
        pnlActivity.Visible = false;
        pnlReport.Visible = true;
        txtReportName.Text = "";
        lblMsg.Text = "";
        GetBomReportData();
    }

    public void GetBomReportData()
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        List<BomData.BomProcessData> BomProcessData = new List<BomData.BomProcessData>();
        // var getRecord;
        if (Session["BomProcessID"] != null)
        {
            bomProcessID = (List<int>)Session["BomProcessID"];
            // for multiple activities

            for (int i = 0; i < bomProcessID.Count; i++)
            {
                int BomProcessID = Convert.ToInt32(bomProcessID[i].ToString()); //proobjId is selected atctivity id

               // bomProcess.AddRange(ProcessData.GetProcessObjAttributes(BomProcessID));
                BomProcessData.AddRange(BomData.GetBOMProcessDataByBomProcessID(BomProcessID));
            }

            if (BomProcessData != null)
            {
                grdBomReport.DataSource = BomProcessData;
                grdBomReport.DataBind();
                pnlReport.Visible = true;
                pnlBomProcess.Visible = false;
                pnlActivity.Visible = false; //Added recently
            }            
        }
    }

    protected void lnkbtnList_Click(object sender, EventArgs e)
    {
        //pnlActivity.Visible = false;
        //pnlListSavedReport.Visible = true;
        //divRecordCount.Visible = false;
        PanelReportType.Visible = true;
        pnlActivity.Visible = false;
        pnlBomProcess.Visible = false;
        pnlListSavedReport.Visible = false;
        pnlReport.Visible = false;

    }

    protected void lnkbtnAdd_Click(object sender, EventArgs e)
    {
        pnlActivity.Visible = true;
        pnlListSavedReport.Visible = false;
        divRecordCount.Visible = false;

    }

    protected void lnkbtnSaveReport_Click(object sender, EventArgs e)
    {
     if (Activity.GetDuplicateCheckReportName(txtReportName.Text.Trim(), ProcessId,(int)ReportTypeID.Bom))
        {
            tbl_Report reportdata = new tbl_Report();
            string activityID = string.Empty;
            string bomID = string.Empty;
            if (Session["Activity"] != null)
            {
                activity = (List<int>)Session["Activity"]; // getting activity id from session that are selected           

                for (int i = 0; i < activity.Count; i++)
                {
                    activityID += activity[i].ToString() + ",";
                }

                reportdata.ProcessObjID = activityID.ToString();
            }

            if (Session["BomProcessID"] != null)
            {
                bomProcessID = (List<int>)Session["BomProcessID"]; // getting activity id from session that are selected           

                for (int i = 0; i < bomProcessID.Count; i++)
                {
                    bomID += bomProcessID[i].ToString() + ",";
                }

                reportdata.AttributeName = bomID.ToString();
                reportdata.ReportTypeID = (int)ReportTypeID.Bom;
            }

            if (Session["SelectedNodeValue"] != null)
            {
                ProcessId = Convert.ToInt32(Session["SelectedNodeValue"]); // getting activity id from session that are selected 
                reportdata.ProcessID = ProcessId;
                reportdata.ReportName = txtReportName.Text;
                if (this.IsEdit == true)
                {
                    reportdata.ReportID = EditIDINT;
                }
            }

            var listData = Activity.SaveReportData(reportdata);
            if (listData == true)
            {
                pnlReport.Visible = false;
                pnlListSavedReport.Visible = true;
                lblMsg.Text = "Report saved successfully.";
                lblMsg.Style.Add("color", "green");
                divRecordCount.Style.Add("padding", "0px 0px 0px 622px!important");
            }
            else
            {
                //error on saving
            }
        }
        else
        {
            //divMsg.Visible = true;
            lblMsg.Text = "Report saved successfully..!";
            lblMsg.Style.Add("color", "green");
            divRecordCount.Style.Add("padding", "0px 0px 0px 622px!important");
            //lblMsg.CssClass = "msgError";
        }
        //string scriptrefresh = "loadProcessManager();";
        //ScriptManager.RegisterStartupScript(this, this.GetType(),
        //              "ServerControlScript", scriptrefresh, true);
        BindSavedReportGrid(ProcessId);
        this.IsEdit = false;
    }

    protected void grdSavedReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ////idDelete will get Attribute Id data for selected grid view row on delete button
        int idDelete = this.CInt32(((ImageButton)grdSavedReport.Rows[e.RowIndex].FindControl("deleteBtn")).CommandArgument);
        if (idDelete > 0)
        {
            bool result = false;
            result = Activity.DeleteReport(idDelete); ////DeleteAttributeData is stored procedure in database that will delete selected Attribute id from multiple tables
            if (result)
            {
                //lblMsg.Visible = true;
                //lblMsg.Text = "This record is Deleted.!";
                //lblMsg.CssClass = "msgSucess";   
                lblMsg.Text = "Report has been deleted successfully";
                lblMsg.Style.Add("color", "green");
                divRecordCount.Style.Add("padding", "0px 0px 0px 544px!important");
            }
            else
            {
                //lblMsg.Visible = true;
                //lblMsg.Text = "Error on Deleting data.!";
                //lblMsg.CssClass = "msgError";    
                lblMsg.Text = "Error on Deleting data.!";
                lblMsg.Style.Add("color", "red");
                divRecordCount.Style.Add("padding", "0px 0px 0px 647px!important");
            }
            //ClearControl();
            BindSavedReportGrid(ProcessId);
            this.EditIDINT = 0;
        }

        // mopoExUser.Show();
    }

    protected void grdSavedReport_RowCreated(object sender, GridViewRowEventArgs e)
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
                            {
                                image.ImageUrl = "~\\images\\ArrowDown.png";
                            }
                            else
                            {
                                image.ImageUrl = "~\\images\\ArrowUp.png";
                            }

                            cell.Controls.Add(image);
                        }

                    }
                }
            }
        }
    }

    protected void grdSavedReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSavedReport.PageIndex = e.NewPageIndex;

        if (Session["SelectedNodeValue"] != null)
            ProcessId = this.CInt32(Session["SelectedNodeValue"]);

        this.BindSavedReportGrid(ProcessId); //// bind grid view

        pnlListSavedReport.Visible = true;
    }

    protected void grdSavedReport_Sorting(object sender, GridViewSortEventArgs e)
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

        if (Session["SelectedNodeValue"] != null)
            ProcessId = this.CInt32(Session["SelectedNodeValue"]);

        this.BindSavedReportGrid(ProcessId); //// bind grid view        
    }

    protected void grdSavedReport_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblMsg.Text = string.Empty;
        this.EditIDINT = this.CInt32(((Literal)grdSavedReport.Rows[e.NewEditIndex].FindControl("litReportID")).Text); // selected report ID

        var row = Activity.GetDataByReportID(EditIDINT); // function will get report data by report id 
        if (row != null)
        {
            string ProcessObjID = row.ProcessObjID; // processObj ID in string 
            string BomProcessIds = row.AttributeName; // attributes name in string 
            string ReportName = row.ReportName;    // get report name

            string[] activityItem = ProcessObjID.Split(','); //split menthod that will split processobID
            string[] bomProcessItem = BomProcessIds.Split(',');
            for (int p = 0; p < activityItem.Length - 1; p++)
            {
                activity.Add(Convert.ToInt32(activityItem[p])); // getting processobID in list activity list<int>
                Session["Activity"] = activity;
                string acctivityname = Activity.GetActivityNameByProcessObjId(Convert.ToInt32(activityItem[p]));
                activityDic.Add(Convert.ToInt32(activityItem[p]), acctivityname);
            }

            Session["ActivityDictionary"] = activityDic;
            bomProcessID.Clear();
            for (int q = 0; q < bomProcessItem.Length - 1; q++) // getting attributesName in list attributes list<string>
            {
                bomProcessID.Add(Convert.ToInt32(bomProcessItem[q]));
                Session["BomProcessID"] = bomProcessID;
            }

            GetBomReportData(); // it will get report for selected reportID
            txtReportName.Text = ReportName;
            pnlReport.Visible = true;
            pnlListSavedReport.Visible = false;
            this.IsEdit = true;
        }
        else
        {
            // no relavent data found to this report ID
        }
    }

    /// <summary>
    /// Gets the ID of the post back control.    
    /// </summary>
    /// <param name = "page">The page.</param>
    /// <returns></returns>
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

    protected void lnkBtnAttributeReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageReport.aspx");
    }

    protected void lnkBtnBOMReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("BOMReport.aspx");
    }
}