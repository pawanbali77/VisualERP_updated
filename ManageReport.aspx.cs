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
using System.IO;
using ClosedXML.Excel;
using System.Text.RegularExpressions;
using System.Reflection;

public partial class ManageReport : BasePage
{
    int ProcessId = 0;
    int typeId = 0;
    List<int> activity = new List<int>();
    List<string> activityName = new List<string>();
    Dictionary<int, string> activityDic = new Dictionary<int, string>();


    List<int> AllExistingReports = new List<int>();
    List<string> AllExistingReportsName = new List<string>();
    Dictionary<int, string> AllExistingReportsDic = new Dictionary<int, string>();

    List<int> AllExistingReports_Process = new List<int>();
    List<string> AllExistingReportsName_Process = new List<string>();
    Dictionary<int, string> AllExistingReportsDic_Process = new Dictionary<int, string>();

    List<string> attribute = new List<string>();

    List<string> ExistingReports_Attribute_Attribute = new List<string>();

    List<ProcessData.ProcessDataProperty> activityNode = new List<ProcessData.ProcessDataProperty>();
    List<ProcessData.AllReports> ObjAllExistingReports = new List<ProcessData.AllReports>();
    //List<int> actv = new List<int>();
    //List<NodeItem> actv = new List<NodeItem>();
    Dictionary<int, string> actv = new Dictionary<int, string>();
    string str = string.Empty;
    string str1 = string.Empty;
    string cstr = string.Empty;

    List<int> bomProcessID = new List<int>();
    List<string> bomProcessName = new List<string>();

    int RoleID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["RoleID"] != null)
        {
            RoleID = Convert.ToInt32(Session["RoleID"].ToString());
        }


        divErrorMsg.Visible = false;
        string EditId = GetPostBackControlId((Page)sender); // to get postback control id that is clicked
        VisualERPDataContext obj = new VisualERPDataContext();
        // Handles Load Event
        //pnlActivity.Visible = true;
        //pnlAttribute.Visible = false;

        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript1", "test();", true);

        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript2", "test1();", true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.lnkbtnExporttoExcel);

        pnlActivity.Visible = false;
        pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
        pnlEror.Visible = false;
        pnlAttribute.Visible = false;
        pnlBomProcess.Visible = false;
        pnlAttributeReport.Visible = false;
        pnlBomReport.Visible = false;
        pnlTFGReport.Visible = false;
        pnlMachineReport.Visible = false;
        pnlErrorAttribute.Visible = false;
        chkAllErrorAttributes.Checked = false;
        pnlESAReport.Visible = false;
        pnlInventoryReport.Visible = false;
        pnlReportType.Visible = false;
        pnlCustomStandardReport_Process.Visible = false;
        pnlListSavedReport.Visible = false;
        pnlTgtValueGap.Visible = false;
        pnlErrorReport.Visible = false;
        lnkbtnSaveReport.Visible = false; //hide save button on load
        lnkbtnExporttoExcel.Visible = false; // hide export to excel button on load
        liSaveReport.Visible = false;
        liExporttoExcel.Visible = false;
        chkSelectAllActivity.Checked = false; // uncheck select all checkbox on page load
        chkSelectallInventory.Checked = false;
        chkExistingReports_Category_Attribute.Checked = false;
        chkAllAttributes.Checked = false; // uncheck select all checkbox on page load
        chkAllInventoryAttributes.Checked = false;
        chkSelectAllBom.Checked = false; // uncheck select all checkbox on page load
        chkSelectallExistingReports.Checked = false;
        pnlCustomStandardReport_Process_attribute.Visible = false;
        pnlCustomStandardReport_Selected.Visible = false;
        chkExistingReports_Attribute_Attribute.Checked = false;
        chkSelectallError.Checked = false;
        //divErrorMsg.Visible = false;

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
                Session["Activity"] = activity; // it hold activity id that is proess object id in session
                Session["ActivityName"] = activityName; // it hold activity name that is process object name in session
                Session["ActivityDictionary"] = activityDic; // it will hold dicectory of both activity and activity name in session
            }
        }
        foreach (ListItem item in chkboxError.Items)
        {
            if (item.Selected)
            {
                activity.Add(Convert.ToInt32(item.Value));
                activityName.Add(Convert.ToString(item.Text));
                string acctivityname = Activity.GetActivityNameByProcessObjId(Convert.ToInt32(item.Value));
                activityDic.Add(Convert.ToInt32(item.Value), acctivityname);
                Session["Error"] = activity; // it hold activity id that is proess object id in session
                Session["ErrorName"] = activityName; // it hold activity name that is process object name in session
                Session["ErrorDictionary"] = activityDic; // it will hold dicectory of both activity and activity name in session
            }
        }

        foreach (ListItem item in chkboxInventory.Items)
        {
            if (item.Selected)
            {
                activity.Add(Convert.ToInt32(item.Value));
                activityName.Add(Convert.ToString(item.Text));
                string acctivityname = Activity.GetActivityNameByProcessObjId(Convert.ToInt32(item.Value));
                activityDic.Add(Convert.ToInt32(item.Value), acctivityname);
                Session["InventoryValue"] = activity; // it hold activity id that is proess object id in session
                Session["InventoryName"] = activityName; // it hold activity name that is process object name in session
                Session["InventoryDictionary"] = activityDic; // it will hold dicectory of both activity and activity name in session
            }
        }

        foreach (ListItem item in chkboxExistingReports.Items)
        {
            if (item.Selected)
            {
                AllExistingReports.Add(Convert.ToInt32(item.Value));
                AllExistingReportsName.Add(Convert.ToString(item.Text));
                string ExistingReportsName = item.Text;
                AllExistingReportsDic.Add(Convert.ToInt32(item.Value), ExistingReportsName);
                Session["ExistingReportsValue"] = AllExistingReports; // it hold activity id that is proess object id in session
                Session["ExistingReportsName"] = AllExistingReportsName; // it hold activity name that is process object name in session
                Session["ExistingReportsDictionary"] = AllExistingReportsDic; // it will hold dicectory of both activity and activity name in session
            }
        }

        foreach (ListItem item in chkboxExistingReports_Category_Attribute.Items)
        {
            if (item.Selected)
            {
                AllExistingReports_Process.Add(Convert.ToInt32(item.Value));
                AllExistingReportsName_Process.Add(Convert.ToString(item.Text));
                string ExistingReportsName_Process = item.Text;
                AllExistingReportsDic_Process.Add(Convert.ToInt32(item.Value), ExistingReportsName_Process);
                Session["ExistingReportsValue_process"] = AllExistingReports_Process; // it hold activity id that is proess object id in session
                Session["ExistingReportsName_process"] = AllExistingReportsName_Process; // it hold activity name that is process object name in session
                Session["ExistingReportsDictionary_process"] = AllExistingReportsDic_Process; // it will hold dicectory of both activity and activity name in session
            }
        }



        foreach (ListItem item in chkboxAttribute.Items)
        {
            if (item.Selected)
            {

                attribute.Add(Convert.ToString(item.Text)); // on page checkbox will get clear thats why we hold the checked items in list and session
                Session["AttributeName"] = attribute;
            }
        }

        foreach (ListItem item in chkboxErrorAttribute.Items)
        {
            if (item.Selected)
            {

                attribute.Add(Convert.ToString(item.Text)); // on page checkbox will get clear thats why we hold the checked items in list and session
                Session["ErrorAttributeName"] = attribute;
            }
        }
        foreach (ListItem item in chkboxExistingReports_Attribute_Attribute.Items)
        {
            if (item.Selected)
            {
                ExistingReports_Attribute_Attribute.Add(Convert.ToString(item.Text)); // on page checkbox will get clear thats why we hold the checked items in list and session
                Session["ExistingReports_Attribute_Attribute"] = ExistingReports_Attribute_Attribute;
            }
        }
        foreach (ListItem item in chkboxInventoryAttribute.Items)
        {
            if (item.Selected)
            {
                attribute.Add(Convert.ToString(item.Text)); // on page checkbox will get clear thats why we hold the checked items in list and session
                Session["InventoryAttributeName"] = attribute;
            }
        }

        // on page checkbox will get clear thats why we hold the checked items in list and session
        foreach (ListItem item in chkboxBomProcess.Items)
        {
            if (item.Selected)
            {
                bomProcessID.Add(Convert.ToInt32(item.Value)); // add bom processid in list bomProcessID
                bomProcessName.Add(Convert.ToString(item.Text)); // add bom process name in list bomProcessName
                Session["BomProcessID"] = bomProcessID; // add bomProcessID list in session 
                Session["BomProcessName"] = bomProcessName; // add bomProcessName list in session 
            }
        }

        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
        {
            //ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
            //Session["SelectedNodeValue"] = ProcessId;
            //ViewState["PreviousValue"] = ProcessId;

            // selecting node that have multiple child
            if ((mastertreeview.SelectedNode.ChildNodes.Count > 0) && ((EditId != "lnkBtnPPESAReport") && (EditId != "lnkBtnPDESAReport")))
            {
                MakeChildReport(); // on multiple level of tree will will get report of selected node 

                //check clicked control id 
                if ((EditId != "editBtn") && (EditId != "deleteBtn"))
                {
                    lblMsg.Text = string.Empty;
                    ViewState["sortBy"] = "ReportID"; // report data sort by report id
                    ViewState["isAsc"] = "1";
                    List<Activity.ListReportData> lstData = new List<Activity.ListReportData>(); // generic list 
                    lstData = Activity.GetReportByProcessID(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(mastertreeview.SelectedNode.Value)); // calling GetReportByProcessID method 
                    if (lstData.Count != 0)
                    {
                        pnlListSavedReport.Visible = true; //saved report grid will show and other will hide
                        pnlActivity.Visible = false;
                        pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
                        pnlEror.Visible = false;
                        grdSavedReport.DataSource = lstData; // bind grid generic list as data source
                        grdSavedReport.DataBind();



                        pnlReportType.Visible = false; //***************

                    }
                    else
                    {
                        //pnlListSavedReport.Visible = false;
                        //pnlActivity.Visible = true;
                        //MakeChildReport();

                        //*******************
                        pnlReportType.Visible = true;
                        pnlActivity.Visible = false;
                        pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
                        pnlAttribute.Visible = false;
                        pnlBomProcess.Visible = false;
                        pnlAttributeReport.Visible = false;
                        pnlBomReport.Visible = false;
                        pnlTFGReport.Visible = false;
                        pnlMachineReport.Visible = false;
                        pnlESAReport.Visible = false;
                        pnlListSavedReport.Visible = false;
                        pnlTgtValueGap.Visible = false;
                        divErrorMsg.Visible = false;
                        pnlEror.Visible = false;
                        //*******************
                    }
                }
                else
                {
                    //BindActivityCheckboxList(Convert.ToInt32(mastertreeview.SelectedNode.Value));
                }
                //MakeChildReport();                
            }
            else
            {
                if ((EditId != "editBtn") && (EditId != "deleteBtn"))
                {
                    BindSavedReportGrid(this.CInt32(mastertreeview.SelectedNode.Value)); // bind saved report data
                    lblMsg.Text = string.Empty; // label message will be hide
                }

                if (EditId == "lnkBtnAttributeReport") // maintain currentreport type of attribute in session before click on linkbutton
                    Session["CurrentReport"] = (int)ReportTypeID.Attribute;

                if (EditId == "lnkBtnBOMReport") // maintain currentreport type of Bom in session before click on linkbutton
                    Session["CurrentReport"] = (int)ReportTypeID.Bom;

                if (EditId == "lnkBtnTFGReport") // maintain currentreport type of TFG in session before click on linkbutton
                    Session["CurrentReport"] = (int)ReportTypeID.TFG;

                if (EditId == "lnkBtnMachineReport") // maintain currentreport type of Machine in session before click on linkbutton
                    Session["CurrentReport"] = (int)ReportTypeID.Machine;

                if (EditId == "lnkBtnPPESAReport") // maintain currentreport type of PPESA in session before click on linkbutton
                    Session["CurrentReport"] = (int)ReportTypeID.PCS;

                if (EditId == "lnkBtnPDESAReport") // maintain currentreport type of PDESA in session before click on linkbutton
                    Session["CurrentReport"] = (int)ReportTypeID.DCS;

                if (EditId == "liTgtValueGap") // maintain currentreport type of PDESA in session before click on linkbutton
                    Session["CurrentReport"] = (int)ReportTypeID.TGTGAP;

                if (EditId == "btnNextToInventory") // maintain currentreport type of Inventory Report in session before click on linkbutton
                    Session["CurrentReport"] = (int)ReportTypeID.Inventory;

                if (EditId == "btnNextToError") // maintain currentreport type of Inventory Report in session before click on linkbutton
                    Session["CurrentReport"] = (int)ReportTypeID.Error;

                if (EditId == "btnNextToExistingReports") // maintain currentreport type of Inventory Report in session before click on linkbutton
                    Session["CurrentReport"] = (int)ReportTypeID.CustomStandardReport;

                BindActivityCheckboxList(Convert.ToInt32(mastertreeview.SelectedNode.Value)); // bind activity checkbox
                BindInventoryCheckboxList(Convert.ToInt32(mastertreeview.SelectedNode.Value));

                BindAllExistingReportCheckboxList(Convert.ToInt32(mastertreeview.SelectedNode.Value));

            }
            BindErrorCheckboxList(Convert.ToInt32(mastertreeview.SelectedNode.Value));
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
            //Session["SelectedNodeValue"] = ProcessId;
            ViewState["PreviousValue"] = ProcessId; // previous process id hold in view state but now not in used
            Session["SelectedNode"] = mastertreeview.SelectedNode.ValuePath;
            Session["SelectedNodeValue"] = mastertreeview.SelectedNode.Value;

            typeId = TypeData.GetTypeID(ProcessId); // get tree node type here 

            if (typeId == 5)
            {
                liPPESA.Visible = true; //this will show PPESA report scorecard linkbutton from tree node that is not process
                liPDESA.Visible = true; //this will show PDESA report scorecard linkbutton from tree node that is not process
            }
            else
            {
                liPPESA.Visible = false; //this will hide PPESA report scorecard linkbutton from tree node that is not process
                liPDESA.Visible = false; //this will hide PDESA report scorecard linkbutton from tree node that is not process
            }
        }
        else if (Session["SelectedNodeValue"] != null)
        {
            ProcessId = this.CInt32(Session["SelectedNodeValue"]);
            ViewState["PreviousValue"] = ProcessId;
        }

        if (!IsPostBack)
        {
            BindSavedReportGrid(this.CInt32(ProcessId));
            btnNextToActivity.Visible = false;
        }
        // BindActivityCheckboxList(Convert.ToInt32(mastertreeview.SelectedNode.Value));

    }

    //it will create child report on multiple level of tree. we are creating dynamic query here which will return activitynode with its actual path in tree
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

        for (int i = 0; i < actv.Count; i++)
        {
            int ProID = Convert.ToInt32(actv.ElementAt(i).Key);
            string NodeName = Convert.ToString(actv.ElementAt(i).Value);
            if (i == 0)
            {
                pre = "SELECT tab1.ProcessObjID ,( +tab1.ActivityName) as ActivityName ";
                mid = "FROM (SELECT  ProcessObjID, ( + ProcessObjName +  '    (" + NodeName + "'+')') as ActivityName, ProcessID FROM  dbo.tbl_ProcessObject WHERE (ProcessID = '" + ProID + "') AND (ProcessObjName IS NOT NULL) ";
            }
            else
            {
                post += "UNION SELECT  ProcessObjID, ( + ProcessObjName +  '    (" + NodeName + "'+')') as ActivityName, ProcessID FROM dbo.tbl_ProcessObject WHERE (ProcessID = '" + ProID + "') AND (ProcessObjName IS NOT NULL)";
            }
        }
        string query = "" + pre + "" + mid + "" + post + ")" + " AS tab1 INNER JOIN dbo.tbl_Process ON tab1.ProcessID = dbo.tbl_Process.ProcessID";
        DataSet ds = GetData(query); // passing runtime query to get dataset of records
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
            pnlActivity.Visible = true;
            chkboxActivity.DataSource = dt;
            chkboxActivity.DataTextField = "ActivityName";
            chkboxActivity.DataValueField = "ProcessObjID";
            chkboxActivity.DataBind();
            pnlListSavedReport.Visible = false;
            btnNextToActivity.Visible = true; //*******************
            headerTitle.InnerText = "Select Activity";
            chkSelectAllActivity.Visible = true;
        }
        else
        {
            //Message.Text = "Unable to connect to the database.";
            btnNextToActivity.Visible = false; //************************
            headerTitle.InnerText = "No Activity found under this process";
            chkSelectAllActivity.Visible = false;
        }
    }

    //it will get tree child node values recursion method is used below
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

    // it will get saved report from database that will contains report name and selected attributes
    public void BindSavedReportGrid(int proId)
    {
        ViewState["sortBy"] = "ReportID";
        ViewState["isAsc"] = "1";
        List<Activity.ListReportData> lstData = new List<Activity.ListReportData>();
        lstData = Activity.GetReportByProcessID(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), proId);
        if (lstData.Count != 0)
        {
            pnlListSavedReport.Visible = true;
            pnlActivity.Visible = false;
            pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlEror.Visible = false;
            grdSavedReport.DataSource = lstData;
            grdSavedReport.DataBind();
            pnlReportType.Visible = false; //*******************

        }
        else
        {
            //pnlListSavedReport.Visible = false; //*******************
            //pnlActivity.Visible = true; //*******************

            //*******************
            pnlReportType.Visible = true;
            pnlActivity.Visible = false;
            pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlEror.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlMachineReport.Visible = false;
            pnlESAReport.Visible = false;
            pnlListSavedReport.Visible = false;
            pnlTgtValueGap.Visible = false;
            divErrorMsg.Visible = false;
            //*******************
        }
    }

    /// <summary>
    /// it will bind the activity checkboxlist 
    /// </summary>
    /// <param name="PoID">processobject id </param>
    public void BindActivityCheckboxList(int PoID)
    {
        List<ProcessData.ProcessDataProperty> activities = ProcessData.GetProcessObjActvities(PoID);

        if (activities.Count != 0)
        {
            divAttribute.Visible = true;
            //
            activityNode.AddRange(activities);
            //
            chkboxActivity.DataSource = activityNode;
            chkboxActivity.DataTextField = "ProcessObjectName"; // datatext field
            chkboxActivity.DataValueField = "ProcessObjID"; // data value field
            chkboxActivity.DataBind(); // bind checkboxlist            
            btnNextToActivity.Visible = true;
            headerTitle.InnerText = "Select Activity";
            chkSelectAllActivity.Visible = true;
        }
        else
        {
            headerTitle.InnerText = "No Activity found under this process";
            //divAttribute.Visible = false;
            chkSelectAllActivity.Visible = false;
            btnNextToActivity.Visible = false;
            chkboxActivity.Items.Clear();
        }
    }


    public void BindInventoryCheckboxList(int PoID)
    {
        List<ProcessData.ProcessDataProperty> activities = ProcessData.GetProcessObjInventories(PoID);

        if (activities.Count != 0)
        {
            divAttribute.Visible = true;
            //
            activityNode.AddRange(activities);
            //
            chkboxInventory.DataSource = activities;
            chkboxInventory.DataTextField = "ProcessObjectName"; // datatext field
            chkboxInventory.DataValueField = "ProcessObjID"; // data value field
            chkboxInventory.DataBind(); // bind checkboxlist            
            btnNextToInventory.Visible = true;
            headerTitleInventory.InnerText = "Select Inventory";
            chkSelectallInventory.Visible = true;
        }
        else
        {
            headerTitleInventory.InnerText = "No Inventory found under this process";
            //divAttribute.Visible = false;
            chkSelectallInventory.Visible = false;
            btnNextToInventory.Visible = false;
            chkboxInventory.Items.Clear();
        }
    }
    public void BindAllExistingReportCheckboxList(int PoID)
    {
        List<ProcessData.AllReports> objAllExistingReports = ProcessData.GetAllExistingReportsName(PoID);

        if (objAllExistingReports.Count != 0)
        {
            divAllExistingReports.Visible = true;
            //
            ObjAllExistingReports.AddRange(objAllExistingReports);
            //
            chkboxExistingReports.DataSource = objAllExistingReports;
            chkboxExistingReports.DataTextField = "ReportsName"; // datatext field
            chkboxExistingReports.DataValueField = "ReportsID"; // data value field
            chkboxExistingReports.DataBind(); // bind checkboxlist            
            //btnNextToExistingReports.Visible = true;
            headerTitleAllreports.InnerText = "Select Report";
            chkSelectallExistingReports.Visible = true;
        }
        else
        {
            headerTitleAllreports.InnerText = "No Report found under this process";
            //divAttribute.Visible = false;
            chkSelectallExistingReports.Visible = false;
            //btnNextToExistingReports.Visible = false;
            chkboxExistingReports.Items.Clear();
        }
    }


    public void BindErrorCheckboxList(int PoID)
    {
        List<ProcessData.ProcessDataProperty> Error = ProcessData.GetProcessObjErrors(PoID);

        if (Error.Count != 0)
        {
            divAttribute.Visible = true;
            //
            activityNode.AddRange(Error);
            //
            chkboxError.DataSource = Error;
            chkboxError.DataTextField = "ProcessObjectName"; // datatext field
            chkboxError.DataValueField = "ProcessObjID"; // data value field
            chkboxError.DataBind(); // bind checkboxlist            
            btnNextToInventory.Visible = true;
            headerTitleError.InnerText = "Select Process";
            chkSelectallError.Visible = true;
        }
        else
        {
            headerTitleError.InnerText = "No Process found under this process";
            //divAttribute.Visible = false;
            chkSelectallError.Visible = false;
            btnNextToInventory.Visible = false;
            chkboxError.Items.Clear();
        }
    }

    /// <summary>
    /// it will call on next button on activity panel will will corresponde data according the current report selected
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNextToActivity_Click(object sender, EventArgs e)
    {
        ViewState["ReportEditID"] = 0;
        if (activity.Count > 0) // activity is list have checkbox selection items on page load
        {
            lblMsg.Text = "";
            pnlActivity.Visible = false;
            pnlInventory.Visible = false;
            pnlCustomStandardReport.Visible = false;
            pnlEror.Visible = false;
            pnlListSavedReport.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            ArrayList List = new ArrayList();
            List<ProcessData.ProcessDataProperty> attribute = new List<ProcessData.ProcessDataProperty>();
            List<BomData.BomDataProcessProperty> bomDataR = new List<BomData.BomDataProcessProperty>();
            List<TFGData.ListTFGData> TFGDataR = new List<TFGData.ListTFGData>();
            List<MachineData.ListMachineData> MachineDataR = new List<MachineData.ListMachineData>();
            List<PPESAnPDESA.ListPPESAnPDESAData> ESADataR = new List<PPESAnPDESA.ListPPESAnPDESAData>();
            if (Session["CurrentReport"] != null) // session current report contains the selected report that is whatever Attribute,Bom,Tfg,machine
            {
                int reporttyp = Convert.ToInt32(Session["CurrentReport"]); // get report type 

                //report type 1 for attribute report
                //report type 2 for bom report
                //report type 3 for TFG report
                //report type 4 for Machine report
                //report type 8 for Inventory report
                //report type 9 for Error report
                //report type 10 for Custom Standard report
                if (reporttyp == 1)
                {
                    for (int i = 0; i < activity.Count; i++)
                    {
                        int prcobjID = Convert.ToInt32(activity[i].ToString()); //proobjId is selected atctivity id

                        attribute.AddRange(ProcessData.GetProcessObjAttributes(prcobjID));
                        //List.AddRange(attribute);
                        attribute.Select(x => x.AttributeName).Distinct();
                        var DistinctItems = attribute.GroupBy(x => x.AttributeName).Select(y => y.First());
                        // attribute= attribute.Distinct().ToList();

                        if (attribute.Count != 0)
                        {
                            chkboxAttribute.DataSource = DistinctItems;
                        }
                        //chkboxAttribute.DataSource = attribute;
                    }
                    chkboxAttribute.DataTextField = "AttributeName"; // textfield
                    chkboxAttribute.DataValueField = "AttributeMenuID"; //value field
                    chkboxAttribute.DataBind(); //binding chkboxAttribute 
                    pnlAttribute.Visible = true;
                }
                if (reporttyp == 2)
                {

                    for (int i = 0; i < activity.Count; i++)
                    {
                        int prcobjID = Convert.ToInt32(activity[i].ToString()); //proobjId is selected atctivity id

                        bomDataR.AddRange(BomData.GetAllBomProcess(prcobjID));

                        if (bomDataR.Count != 0)
                        {
                            chkboxBomProcess.DataSource = bomDataR;
                            chkboxBomProcess.DataTextField = "BomProcessName"; // textfield
                            chkboxBomProcess.DataValueField = "BomProcessID"; //value field
                            chkboxBomProcess.DataBind(); //binding chkboxBomProcess 
                            pnlBomProcess.Visible = true;
                        }
                        else
                        {
                            pnlBomProcess.Visible = false;
                            lblMsg.Visible = true;
                            divErrorMsg.Visible = true;
                            lblMsg.Text = "No Bom Process Found under this Activity";
                            lblMsg.Style.Add("color", "red");
                            divErrorMsg.Style.Add("min-width", "231px");
                            divErrorMsg.Style.Add("margin-right", "470px");
                            divErrorMsg.Style.Add("margin-left", "0px");
                            divErrorMsg.Style.Add("float", "right");
                            divErrorMsg.Style.Add("margin-left", "0px");
                            divErrorMsg.Style.Add("padding", "7px 14px 0px 17px");
                            divErrorMsg.Attributes.Add("class", "isa_error");
                        }
                    }
                }

                if (reporttyp == 3)
                {
                    for (int i = 0; i < activity.Count; i++)
                    {
                        int prcobjID = Convert.ToInt32(activity[i].ToString()); //proobjId is selected atctivity id
                        ViewState["sortBy"] = "TFGID";
                        ViewState["isAsc"] = "1";
                        TFGDataR.AddRange(TFGData.GetTFGData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), prcobjID));

                        if (TFGDataR.Count > 0)
                        {
                            grdTFGReport.DataSource = TFGDataR;
                            grdTFGReport.DataBind();
                            pnlTFGReport.Visible = true;
                            lnkbtnSaveReport.Visible = true;
                            if (RoleID == 4)
                            {
                                lnkbtnSaveReport.Visible = false;
                                txtAttributeReportName.Visible = false;
                            }
                            liSaveReport.Visible = true;
                            divTFGName.Visible = true;
                            txtTFGReportName.Text = "";
                            lblMsg.Text = "";
                            lnkbtnExporttoExcel.Visible = true; // show export to excel button if report grid display
                            liExporttoExcel.Visible = true;
                        }
                        else
                        {
                            grdTFGReport.DataSource = null;
                            grdTFGReport.DataBind();
                            divTFGName.Visible = false;
                            lnkbtnSaveReport.Visible = false;
                            liSaveReport.Visible = false;
                            pnlTFGReport.Visible = true;
                            lblMsg.Text = "";
                            lnkbtnExporttoExcel.Visible = false; // hide export to excel button if report grid is null
                            liExporttoExcel.Visible = true;
                        }
                    }
                }

                if (reporttyp == 4)
                {
                    for (int i = 0; i < activity.Count; i++)
                    {
                        int prcobjID = Convert.ToInt32(activity[i].ToString()); //proobjId is selected atctivity id
                        ViewState["sortBy"] = "MachineID";
                        ViewState["isAsc"] = "1";
                        MachineDataR.AddRange(MachineData.GetMachineDataByObjId(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), prcobjID));

                        if (MachineDataR.Count > 0)
                        {
                            grdMachineReport.DataSource = MachineDataR;
                            grdMachineReport.DataBind();
                            pnlMachineReport.Visible = true;
                            lnkbtnSaveReport.Visible = true;
                            if (RoleID == 4)
                            {
                                lnkbtnSaveReport.Visible = false;
                                txtAttributeReportName.Visible = false;
                            }
                            liSaveReport.Visible = true;
                            divMachineName.Visible = true;
                            txtMachineReportName.Text = "";
                            lblMsg.Text = "";
                            lnkbtnExporttoExcel.Visible = true; // show export to excel button if report grid display
                            liExporttoExcel.Visible = true;
                        }
                        else
                        {
                            grdMachineReport.DataSource = null;
                            grdMachineReport.DataBind();
                            divMachineName.Visible = false;
                            lnkbtnSaveReport.Visible = false;
                            liSaveReport.Visible = false;
                            liSaveReport.Visible = false;
                            pnlMachineReport.Visible = true;
                            lblMsg.Text = "";
                            lnkbtnExporttoExcel.Visible = false; // hide export to excel button if report grid is null
                            liExporttoExcel.Visible = false;
                        }
                    }
                }

                if (reporttyp == 8)
                {
                    for (int i = 0; i < activity.Count; i++)
                    {
                        int prcobjID = Convert.ToInt32(activity[i].ToString()); //proobjId is selected atctivity id
                        attribute.AddRange(ProcessData.GetInventoryObjAttributes(prcobjID));
                        attribute.Select(x => x.AttributeName).Distinct();
                        var DistinctItems = attribute.GroupBy(x => x.AttributeName).Select(y => y.First());
                        if (attribute.Count != 0)
                        {
                            chkboxInventoryAttribute.DataSource = DistinctItems;
                        }
                    }
                    chkboxInventoryAttribute.DataTextField = "AttributeName"; // textfield
                    chkboxInventoryAttribute.DataValueField = "AttributeMenuID"; //value field
                    chkboxInventoryAttribute.DataBind(); //binding chkboxInventoryAttribute 
                    pnlInventoryAttribute.Visible = true;
                }
                if (reporttyp == 9)
                {
                    for (int i = 0; i < activity.Count; i++)
                    {
                        int prcobjID = Convert.ToInt32(activity[i].ToString()); //proobjId is selected atctivity id
                        attribute.AddRange(ProcessData.GetErrorObjAttributes(prcobjID));
                        attribute.Select(x => x.AttributeName).Distinct();
                        var DistinctItems = attribute.GroupBy(x => x.AttributeName).Select(y => y.First());
                        if (attribute.Count != 0)
                        {
                            chkboxErrorAttribute.DataSource = DistinctItems;
                        }
                    }
                    chkboxErrorAttribute.DataTextField = "AttributeName"; // textfield
                    chkboxErrorAttribute.DataValueField = "AttributeMenuID"; //value field
                    chkboxErrorAttribute.DataBind(); //binding chkboxInventoryAttribute 
                    pnlErrorAttribute.Visible = true;
                }
            }
            else
            {
                //show add report button
                pnlReportType.Visible = true;
                pnlActivity.Visible = false;
                pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
                pnlEror.Visible = false;
                pnlAttribute.Visible = false;
                pnlBomProcess.Visible = false;
                pnlAttributeReport.Visible = false;
                pnlBomReport.Visible = false;
                pnlTFGReport.Visible = false;
                pnlMachineReport.Visible = false;
                pnlESAReport.Visible = false;
                pnlListSavedReport.Visible = false;
                divErrorMsg.Visible = false;
            }
        }
        else
        {
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please select at least one Activity.";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "231px");
            divErrorMsg.Style.Add("margin-right", "470px");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("float", "right");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("padding", "7px 14px 0px 17px");
            divErrorMsg.Attributes.Add("class", "isa_error");
            pnlActivity.Visible = false;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlESAReport.Visible = false;
            pnlListSavedReport.Visible = false;
        }
        pnlReportType.Visible = false;
    }



    protected void btnNextToExistingReports_Click(object sender, EventArgs e)
    {
        ViewState["ReportEditID"] = 0;
        if (AllExistingReports.Count > 0) // activity is list have checkbox selection items on page load
        {
            List<ProcessData.AllReports> objReportsName = new List<ProcessData.AllReports>();
            lblMsg.Text = "";
            pnlActivity.Visible = false;
            pnlInventory.Visible = false;
            pnlCustomStandardReport.Visible = false;
            pnlEror.Visible = false;
            pnlListSavedReport.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            if (Session["CurrentReport"] != null) // session current report contains the selected report that is whatever Attribute,Bom,Tfg,machine
            {
                //report type 1 for attribute report
                //report type 2 for bom report
                //report type 3 for TFG report
                //report type 4 for Machine report
                //report type 5 for PCS report
                //report type 6 for DCS report
                //report type 7 for TGTGAP report
                //report type 8 for Inventory report
                //report type 9 for Error report
                //report type 10 for Custom Standard report
                int CurrentReport = Convert.ToInt32(Session["CurrentReport"]); // get report type 
                TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
                List<ProcessData.ProcessDataProperty> Process_attribute = new List<ProcessData.ProcessDataProperty>();
                List<ProcessData.ProcessDataProperty> Process_Bom = new List<ProcessData.ProcessDataProperty>();
                List<ProcessData.ProcessDataProperty> Process_TFG = new List<ProcessData.ProcessDataProperty>();
                List<ProcessData.ProcessDataProperty> Process_Machine = new List<ProcessData.ProcessDataProperty>();
                List<ProcessData.ProcessDataProperty> Process_Error = new List<ProcessData.ProcessDataProperty>();
                List<ProcessData.ProcessDataProperty> Process_Inventory = new List<ProcessData.ProcessDataProperty>();
                int VisibleStatus = 0;
                if (CurrentReport == Convert.ToInt32(ReportTypeID.CustomStandardReport))
                {
                    foreach (var row in AllExistingReports)
                    {

                        if (row == Convert.ToInt32(ReportTypeID.Attribute))
                        {
                            Process_attribute = ProcessData.GetProcessObjActvities(Convert.ToInt32(mastertreeview.SelectedNode.Value));
                            VisibleStatus = 1;
                        }
                        if (row == Convert.ToInt32(ReportTypeID.Bom))
                        {
                            Process_Bom = ProcessData.GetProcessObjActvities(Convert.ToInt32(mastertreeview.SelectedNode.Value));
                            VisibleStatus = 1;
                        }
                        if (row == Convert.ToInt32(ReportTypeID.TFG))
                        {
                            Process_TFG = ProcessData.GetProcessObjActvities(Convert.ToInt32(mastertreeview.SelectedNode.Value));
                            VisibleStatus = 1;
                        }
                        if (row == Convert.ToInt32(ReportTypeID.Machine))
                        {
                            Process_Machine = ProcessData.GetProcessObjActvities(Convert.ToInt32(mastertreeview.SelectedNode.Value));
                            VisibleStatus = 1;
                        }
                        if (row == Convert.ToInt32(ReportTypeID.Error))
                        {
                            Process_Error = ProcessData.GetProcessObjActvities(Convert.ToInt32(mastertreeview.SelectedNode.Value));
                            VisibleStatus = 1;
                        }
                        if (row == Convert.ToInt32(ReportTypeID.Inventory))
                        {
                            Process_Inventory = ProcessData.GetProcessObjInventories(Convert.ToInt32(mastertreeview.SelectedNode.Value));
                            VisibleStatus = 1;
                        }
                        else if (row == Convert.ToInt32(ReportTypeID.PCS) || row == Convert.ToInt32(ReportTypeID.DCS) || row == Convert.ToInt32(ReportTypeID.TGTGAP))
                        {
                            divErrorMsg.Visible = true;
                            lblMsg.Text = "Further Selection is not available for PCS, DCS and TGTGAP";
                            lblMsg.Style.Add("color", "red");
                            divErrorMsg.Style.Add("min-width", "231px");
                            divErrorMsg.Style.Add("margin-right", "470px");
                            divErrorMsg.Style.Add("margin-left", "0px");
                            divErrorMsg.Style.Add("float", "right");
                            divErrorMsg.Style.Add("margin-left", "0px");
                            divErrorMsg.Style.Add("padding", "7px 14px 0px 17px");
                            divErrorMsg.Attributes.Add("class", "isa_error");
                        }
                    }
                    if (VisibleStatus == 1)
                    {
                        pnlCustomStandardReport_Process.Visible = true;
                        if (Process_attribute.Count > 0)
                        {
                            pnl_Allreports_Category_Attribute.Visible = true;
                            Allreports_Category_Attribute.InnerText = "Attribute's processes";
                            Allreports_Category_Attribute.Visible = true;
                            chkboxExistingReports_Category_Attribute.DataSource = Process_attribute;
                            chkboxExistingReports_Category_Attribute.DataTextField = "ProcessObjectName"; // textfield
                            chkboxExistingReports_Category_Attribute.DataValueField = "ProcessObjID"; //value field
                            chkboxExistingReports_Category_Attribute.DataBind();
                        }
                        if (Process_Bom.Count > 0)
                        {
                            pnl_Allreports_Category_BOM.Visible = true;
                            Allreports_Category_BOM.InnerText = "BOM's processes";
                            Allreports_Category_BOM.Visible = true;
                            chkboxExistingReports_Category_BOM.DataSource = Process_Bom;
                            chkboxExistingReports_Category_BOM.DataTextField = "ProcessObjectName"; // textfield
                            chkboxExistingReports_Category_BOM.DataValueField = "ProcessObjID"; //value field
                            chkboxExistingReports_Category_BOM.DataBind();
                        }

                        if (Process_TFG.Count > 0)
                        {
                            pnl_Allreports_Category_TFG.Visible = true;
                            Allreports_Category_TFG.InnerText = "TFG's processes";
                            Allreports_Category_TFG.Visible = true;
                            chkboxExistingReports_Category_TFG.DataSource = Process_TFG;
                            chkboxExistingReports_Category_TFG.DataTextField = "ProcessObjectName"; // textfield
                            chkboxExistingReports_Category_TFG.DataValueField = "ProcessObjID"; //value field
                            chkboxExistingReports_Category_TFG.DataBind();
                        }

                        if (Process_Machine.Count > 0)
                        {
                            pnl_Allreports_Category_Machine.Visible = true;
                            Allreports_Category_Machine.InnerText = "Machine's processes";
                            Allreports_Category_Machine.Visible = true;
                            chkboxExistingReports_Category_Machine.DataSource = Process_Machine;
                            chkboxExistingReports_Category_Machine.DataTextField = "ProcessObjectName"; // textfield
                            chkboxExistingReports_Category_Machine.DataValueField = "ProcessObjID"; //value field
                            chkboxExistingReports_Category_Machine.DataBind();
                        }

                        if (Process_Error.Count > 0)
                        {
                            pnl_Allreports_Category_Error.Visible = true;
                            Allreports_Category_Error.InnerText = "Error's processes";
                            Allreports_Category_Error.Visible = true;
                            chkboxExistingReports_Category_Error.DataSource = Process_Error;
                            chkboxExistingReports_Category_Error.DataTextField = "ProcessObjectName"; // textfield
                            chkboxExistingReports_Category_Error.DataValueField = "ProcessObjID"; //value field
                            chkboxExistingReports_Category_Error.DataBind();
                        }

                        if (Process_Inventory.Count > 0)
                        {
                            pnl_Allreports_Category_Inventory.Visible = true;
                            Allreports_Category_Inventory.InnerText = "Inventory's processes";
                            Allreports_Category_Inventory.Visible = true;
                            chkboxExistingReports_Category_Inventory.DataSource = Process_Inventory;
                            chkboxExistingReports_Category_Inventory.DataTextField = "ProcessObjectName"; // textfield
                            chkboxExistingReports_Category_Inventory.DataValueField = "ProcessObjID"; //value field
                            chkboxExistingReports_Category_Inventory.DataBind();
                        }
                    }
                }
            }
        }
        else
        {
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please select at least one Activity.";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "231px");
            divErrorMsg.Style.Add("margin-right", "470px");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("float", "right");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("padding", "7px 14px 0px 17px");
            divErrorMsg.Attributes.Add("class", "isa_error");
            pnlActivity.Visible = false;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlCustomStandardReport.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlESAReport.Visible = false;
            pnlListSavedReport.Visible = false;
            pnlInventoryAttribute.Visible = false;
        }
        pnlReportType.Visible = false;
    }

    /// <summary>
    /// it will get the report according attributes those are selected in checkboxlist
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNextToAttribute_Click(object sender, EventArgs e)
    {
        if (attribute.Count > 0) // activity is list have checkbox selection items on page load
        {
            pnlAttribute.Visible = false;
            pnlActivity.Visible = false;
            pnlInventory.Visible = false;
            pnlCustomStandardReport.Visible = false;
            pnlEror.Visible = false;
            pnlAttributeReport.Visible = true; // attribute report panel will display others will be hide
            pnlListSavedReport.Visible = false;
            txtAttributeReportName.Text = "";
            lblMsg.Text = "";
            GetAttributeReportData(false, false, "");
            lnkbtnSaveReport.Visible = true;
            if (RoleID == 4)
            {
                lnkbtnSaveReport.Visible = false;
                txtAttributeReportName.Visible = false;
            }
            liSaveReport.Visible = true;
            pnlReportType.Visible = false;
        }
        else
        {
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please select at least one Attribute.";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "231px");
            divErrorMsg.Style.Add("margin-right", "470px");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("float", "right");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("padding", "7px 14px 0px 17px");
            divErrorMsg.Attributes.Add("class", "isa_error");

            pnlAttribute.Visible = true;
            pnlActivity.Visible = false;
            pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlEror.Visible = false;
            pnlListSavedReport.Visible = false;
            pnlReportType.Visible = false;
        }

    }

    protected void btnCustomStandardReport_NextToAttribute_Click(object sender, EventArgs e)
    {
        ViewState["ReportEditID"] = 0;
        if (AllExistingReports_Process.Count > 0) // activity is list have checkbox selection items on page load
        {
            lblMsg.Text = "";
            pnlActivity.Visible = false;
            pnlInventory.Visible = false;
            pnlCustomStandardReport.Visible = false;
            pnlEror.Visible = false;
            pnlListSavedReport.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            ArrayList List = new ArrayList();
            List<ProcessData.ProcessDataProperty> attribute = new List<ProcessData.ProcessDataProperty>();
            List<BomData.BomDataProcessProperty> bomDataR = new List<BomData.BomDataProcessProperty>();
            List<TFGData.ListTFGData> TFGDataR = new List<TFGData.ListTFGData>();
            List<MachineData.ListMachineData> MachineDataR = new List<MachineData.ListMachineData>();
            List<PPESAnPDESA.ListPPESAnPDESAData> ESADataR = new List<PPESAnPDESA.ListPPESAnPDESAData>();
            if (Session["CurrentReport"] != null) // session current report contains the selected report that is whatever Attribute,Bom,Tfg,machine
            {

                int CurrentReport = Convert.ToInt32(Session["CurrentReport"]);
                //report type 1 for attribute report
                //report type 2 for bom report
                //report type 3 for TFG report
                //report type 4 for Machine report
                //report type 8 for Inventory report
                //report type 9 for Error report
                //report type 10 for Custom Standard report
                if (CurrentReport == Convert.ToInt32(ReportTypeID.CustomStandardReport))
                {
                    AllExistingReports = (List<int>)Session["ExistingReportsValue"];
                    if (AllExistingReports.Count > 0)
                    {
                        foreach (var row in AllExistingReports)
                        {
                            if (row == Convert.ToInt32(ReportTypeID.Attribute))
                            {
                                for (int i = 0; i < AllExistingReports_Process.Count; i++)
                                {
                                    int prcobjID = Convert.ToInt32(AllExistingReports_Process[i].ToString()); //proobjId is selected atctivity id
                                    attribute.AddRange(ProcessData.GetProcessObjAttributes(prcobjID));
                                    attribute.Select(x => x.AttributeName).Distinct();
                                    var DistinctItems = attribute.GroupBy(x => x.AttributeName).Select(y => y.First());
                                    if (attribute.Count != 0)
                                    {
                                        chkboxExistingReports_Attribute_Attribute.DataSource = DistinctItems;
                                    }
                                }
                                chkboxExistingReports_Attribute_Attribute.DataTextField = "AttributeName"; // textfield
                                chkboxExistingReports_Attribute_Attribute.DataValueField = "AttributeMenuID"; //value field
                                chkboxExistingReports_Attribute_Attribute.DataBind(); //binding chkboxInventoryAttribute 
                                pnlCustomStandardReport_Process_attribute.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        divErrorMsg.Visible = true;
                        lblMsg.Text = "Please select at least one Report.";
                        lblMsg.Style.Add("color", "red");
                        divErrorMsg.Style.Add("min-width", "231px");
                        divErrorMsg.Style.Add("margin-right", "470px");
                        divErrorMsg.Style.Add("margin-left", "0px");
                        divErrorMsg.Style.Add("float", "right");
                        divErrorMsg.Style.Add("margin-left", "0px");
                        divErrorMsg.Style.Add("padding", "7px 14px 0px 17px");
                        divErrorMsg.Attributes.Add("class", "isa_error");
                        pnlActivity.Visible = false;
                        pnlReportType.Visible = false;
                        pnlAttribute.Visible = false;
                        pnlBomProcess.Visible = false;
                        pnlAttributeReport.Visible = false;
                        pnlBomReport.Visible = false;
                        pnlTFGReport.Visible = false;
                        pnlESAReport.Visible = false;
                        pnlListSavedReport.Visible = false;
                    }
                }
            }
            else
            {
                //show add report button
                pnlReportType.Visible = true;
                pnlActivity.Visible = false;
                pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
                pnlEror.Visible = false;
                pnlAttribute.Visible = false;
                pnlBomProcess.Visible = false;
                pnlAttributeReport.Visible = false;
                pnlBomReport.Visible = false;
                pnlTFGReport.Visible = false;
                pnlMachineReport.Visible = false;
                pnlCustomStandardReport_Process_attribute.Visible = false;
                pnlESAReport.Visible = false;
                pnlListSavedReport.Visible = false;
                divErrorMsg.Visible = false;
            }
        }
        else
        {
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please select at least one Process.";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "231px");
            divErrorMsg.Style.Add("margin-right", "470px");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("float", "right");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("padding", "7px 14px 0px 17px");
            divErrorMsg.Attributes.Add("class", "isa_error");
            pnlActivity.Visible = false;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlESAReport.Visible = false;
            pnlListSavedReport.Visible = false;
        }
        pnlReportType.Visible = false;

    }

    protected void btnNextToInventoryAttribute_Click(object sender, EventArgs e)
    {
        if (attribute.Count > 0) // activity is list have checkbox selection items on page load
        {
            pnlAttribute.Visible = false;
            pnlActivity.Visible = false;
            pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlEror.Visible = false;
            pnlInventoryReport.Visible = true; // attribute report panel will display others will be hide
            pnlListSavedReport.Visible = false;
            txtInventoryAttributeReportName.Text = "";
            lblMsg.Text = "";
            GetInventoryAttributeReportData(attribute);
            lnkbtnSaveReport.Visible = true;
            if (RoleID == 4)
            {
                lnkbtnSaveReport.Visible = false;
                txtAttributeReportName.Visible = false;
            }
            liSaveReport.Visible = true;
            pnlReportType.Visible = false;
        }
        else
        {
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please select at least one Attribute.";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "231px");
            divErrorMsg.Style.Add("margin-right", "470px");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("float", "right");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("padding", "7px 14px 0px 17px");
            divErrorMsg.Attributes.Add("class", "isa_error");

            pnlAttribute.Visible = false;
            pnlActivity.Visible = false;
            pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlEror.Visible = false;
            pnlListSavedReport.Visible = false;
            pnlReportType.Visible = false;
        }

    }

    protected void btnNextToErrorAttribute_Click(object sender, EventArgs e)
    {
        if (attribute.Count > 0) // activity is list have checkbox selection items on page load
        {
            pnlAttribute.Visible = false;
            pnlActivity.Visible = false;
            pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlEror.Visible = false;
            pnlInventoryReport.Visible = false;
            pnlErrorReport.Visible = true;
            pnlListSavedReport.Visible = false;
            txtErrorAttributeReportName.Text = "";
            lblMsg.Text = "";
            GetErrorAttributeReportData(attribute);
            lnkbtnSaveReport.Visible = true;
            if (RoleID == 4)
            {
                lnkbtnSaveReport.Visible = false;
                txtAttributeReportName.Visible = false;
            }
            liSaveReport.Visible = true;
            pnlReportType.Visible = false;
        }
        else
        {
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please select at least one Attribute.";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "231px");
            divErrorMsg.Style.Add("margin-right", "470px");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("float", "right");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("padding", "7px 14px 0px 17px");
            divErrorMsg.Attributes.Add("class", "isa_error");
            pnlErrorReport.Visible = false;
            pnlAttribute.Visible = false;
            pnlActivity.Visible = false;
            pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlEror.Visible = false;
            pnlListSavedReport.Visible = false;
            pnlReportType.Visible = false;
        }

    }



    protected void btnNextToCustomStandardReport_Click(object sender, EventArgs e)
    {
        if (ExistingReports_Attribute_Attribute.Count > 0) // activity is list have checkbox selection items on page load
        {
            pnlAttribute.Visible = false;
            pnlActivity.Visible = false;
            pnlInventory.Visible = false;
            pnlCustomStandardReport.Visible = false;
            pnlEror.Visible = false;
            pnlAttributeReport.Visible = false; // attribute report panel will display others will be hide
            pnlListSavedReport.Visible = false;
            pnlCustomStandardReport_Selected.Visible = true;
            txtAttributeReportName.Text = "";
            lblMsg.Text = "";
            GetAttributeReportData(true, false, "");
            lnkbtnSaveReport.Visible = true;
            if (RoleID == 4)
            {
                lnkbtnSaveReport.Visible = false;
                txtAttributeReportName.Visible = false;
            }
            liSaveReport.Visible = true;
            pnlReportType.Visible = false;
        }
        else
        {
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please select at least one Attribute.";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "231px");
            divErrorMsg.Style.Add("margin-right", "470px");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("float", "right");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("padding", "7px 14px 0px 17px");
            divErrorMsg.Attributes.Add("class", "isa_error");

            pnlAttribute.Visible = true;
            pnlActivity.Visible = false;
            pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlEror.Visible = false;
            pnlListSavedReport.Visible = false;
            pnlReportType.Visible = false;
        }

    }

    /// <summary>
    /// get bom report according bom process selection
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNextToBomProcess_Click(object sender, EventArgs e)
    {
        if (bomProcessID.Count > 0) // activity is list have checkbox selection items on page load
        {
            pnlBomProcess.Visible = false;
            pnlActivity.Visible = false;
            pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlEror.Visible = false;
            pnlListSavedReport.Visible = false;
            pnlBomReport.Visible = true;
            pnlTFGReport.Visible = false;
            pnlMachineReport.Visible = false;
            txtBomReportName.Text = "";
            lblMsg.Text = "";
            lnkbtnSaveReport.Visible = true;
            if (RoleID == 4)
            {
                lnkbtnSaveReport.Visible = false;
                txtAttributeReportName.Visible = false;
            }
            liSaveReport.Visible = true;
            GetBomReportData();

            pnlReportType.Visible = false;
        }
        else
        {

            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please select at least one Bom.";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "231px");
            divErrorMsg.Style.Add("margin-right", "470px");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("float", "right");
            divErrorMsg.Style.Add("margin-left", "0px");
            divErrorMsg.Style.Add("padding", "7px 14px 0px 17px");
            divErrorMsg.Attributes.Add("class", "isa_error");

            pnlBomProcess.Visible = true;
            pnlActivity.Visible = false;
            pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlEror.Visible = false;
            pnlListSavedReport.Visible = false;
            pnlBomReport.Visible = false;
        }

    }

    /// <summary>
    /// getting bom process data and bind report for bom 
    /// </summary>
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

            if (BomProcessData.Count > 0)
            {
                grdBomReport.DataSource = BomProcessData;
                grdBomReport.DataBind();
                divReportName.Visible = true;
                lnkbtnExporttoExcel.Visible = true;
                liExporttoExcel.Visible = true;
            }
            else
            {
                grdBomReport.DataSource = null;
                grdBomReport.DataBind();
                divReportName.Visible = false;
                lnkbtnSaveReport.Visible = false;
                lnkbtnExporttoExcel.Visible = false;
                liSaveReport.Visible = false;
                liExporttoExcel.Visible = false;
            }
        }
    }

    /// <summary>
    /// getting TFG data and bind report for TFG
    /// </summary>
    public void GetTFGReportData()
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        List<TFGData.ListTFGData> TFGProcessData = new List<TFGData.ListTFGData>();
        // var getRecord;
        if (Session["Activity"] != null)
        {
            activity = (List<int>)Session["Activity"];
            // for multiple activities

            for (int i = 0; i < activity.Count; i++)
            {
                int prcobjID = Convert.ToInt32(activity[i].ToString()); //proobjId is selected atctivity id
                ViewState["sortBy"] = "TFGID";
                ViewState["isAsc"] = "1";
                TFGProcessData.AddRange(TFGData.GetTFGData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), prcobjID));

                if (TFGProcessData.Count > 0)
                {
                    grdTFGReport.DataSource = TFGProcessData;
                    grdTFGReport.DataBind();
                    pnlTFGReport.Visible = true;
                    lnkbtnSaveReport.Visible = true;
                    if (RoleID == 4)
                    {
                        lnkbtnSaveReport.Visible = false;
                        txtAttributeReportName.Visible = false;
                    }
                    liSaveReport.Visible = true;
                    pnlBomReport.Visible = false;
                    lnkbtnExporttoExcel.Visible = true; // show export to excel button if report grid display
                    liExporttoExcel.Visible = true;
                }
                else
                {
                    grdTFGReport.DataSource = null;
                    grdTFGReport.DataBind();
                    divReportName.Visible = false;
                    lnkbtnSaveReport.Visible = false;
                    lnkbtnExporttoExcel.Visible = false; // hide export to excel button if there is no data in search report
                    liSaveReport.Visible = false;
                    liExporttoExcel.Visible = false;
                }
            }

        }
    }

    /// <summary>
    /// getting Machine data and bind report for Machine 
    /// </summary>
    public void GetMachineReportData()
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        List<MachineData.ListMachineData> MachineProcessData = new List<MachineData.ListMachineData>();
        // var getRecord;
        if (Session["Activity"] != null)
        {
            activity = (List<int>)Session["Activity"];
            // for multiple activities

            for (int i = 0; i < activity.Count; i++)
            {
                int prcobjID = Convert.ToInt32(activity[i].ToString()); //proobjId is selected atctivity id
                ViewState["sortBy"] = "TFGID";
                ViewState["isAsc"] = "1";
                MachineProcessData.AddRange(MachineData.GetMachineDataByObjId(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), prcobjID));

                if (MachineProcessData.Count > 0)
                {
                    grdMachineReport.DataSource = MachineProcessData;
                    grdMachineReport.DataBind();
                    pnlMachineReport.Visible = true;
                    lnkbtnSaveReport.Visible = true;
                    if (RoleID == 4)
                    {
                        lnkbtnSaveReport.Visible = false;
                        txtAttributeReportName.Visible = false;
                    }
                    pnlBomReport.Visible = false;
                    lnkbtnExporttoExcel.Visible = true;
                    liSaveReport.Visible = true;
                    liExporttoExcel.Visible = true;
                }
                else
                {
                    grdMachineReport.DataSource = null;
                    grdMachineReport.DataBind();
                    divReportName.Visible = false;
                    lnkbtnSaveReport.Visible = false;
                    lnkbtnExporttoExcel.Visible = false;
                    liSaveReport.Visible = false;
                    liExporttoExcel.Visible = false;
                }
            }

        }
    }

    /// <summary>
    /// getting Machine data and bind report for Machine 
    /// </summary>
    public void GetESAReportData(int currentReportType)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        List<PPESAnPDESA.ListPPESAnPDESAData> ESAProcessData = new List<PPESAnPDESA.ListPPESAnPDESAData>();
        // var getRecord;
        if (Session["Activity"] != null)
        {
            activity = (List<int>)Session["Activity"];
            // for multiple activities

            for (int i = 0; i < activity.Count; i++)
            {
                int prcobjID = Convert.ToInt32(activity[i].ToString()); //proobjId is selected atctivity id
                ViewState["sortBy"] = "FormID";
                ViewState["isAsc"] = "1";
                int formtype = 0;
                if (currentReportType == 5)
                    formtype = (int)FormType.PPESA;
                else if (currentReportType == 6)
                    formtype = (int)FormType.PDESA;

                ESAProcessData.AddRange(PPESAnPDESA.GetPPESAnPDESADataByPobjID(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), formtype, prcobjID));

                if (ESAProcessData.Count > 0)
                {
                    grdESAReport.DataSource = ESAProcessData;
                    grdESAReport.DataBind();
                    pnlMachineReport.Visible = false;
                    pnlESAReport.Visible = true;
                    lnkbtnSaveReport.Visible = true;
                    if (RoleID == 4)
                    {
                        lnkbtnSaveReport.Visible = false;
                        txtAttributeReportName.Visible = false;
                    }
                    pnlBomReport.Visible = false;
                    lnkbtnExporttoExcel.Visible = true;
                    liSaveReport.Visible = true;
                    liExporttoExcel.Visible = true;
                }
                else
                {
                    grdESAReport.DataSource = null;
                    grdESAReport.DataBind();
                    divReportName.Visible = false;
                    lnkbtnSaveReport.Visible = false;
                    lnkbtnExporttoExcel.Visible = false;
                    liSaveReport.Visible = false;
                    liExporttoExcel.Visible = false;
                }
            }

        }
    }
    protected void btnAddNewColumn_Click(object sender, EventArgs e)
    {
        string ColumnName = txtCustomStandardColumnName.Text;
        GetAttributeReportData(true, true, ColumnName);
    }
    /// <summary>
    /// attrbute report will be create here ..dynamic query will be generated also
    /// </summary>
    public void GetAttributeReportData(bool MixReport, bool AddnewRow, string ColumnName)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        DataTable dtSpecificOrders = new DataTable();
        if (AddnewRow)
        {
            dtSpecificOrders = (DataTable)Session["GridviewCustomStandardReport"];
            if (dtSpecificOrders.Rows.Count > 0)
            {
                dtSpecificOrders.Columns.Add(new DataColumn(ColumnName, typeof(string)));
                GridviewCustomStandardReport.DataSource = dtSpecificOrders;
                GridviewCustomStandardReport.DataBind();


                int cellCount = this.GridviewCustomStandardReport.Rows[0].Cells.Count;
                int rowsCount = this.GridviewCustomStandardReport.Rows.Count;

                for (int j = 0; j < rowsCount; j++)
                {
                    //here i am adding a control.
                    TextBox textBox = new TextBox();
                    textBox.ID = "txtDynamicText" + j.ToString();
                    textBox.Attributes.Add("runat", "server");
                    textBox.CssClass = "Color";
                    this.GridviewCustomStandardReport.Rows[j].Cells[cellCount - 1].Controls.Add(textBox);
                }


                pnlListSavedReport.Visible = false;
                divErrorMsg.Visible = true;
                if (RoleID == 4)
                {

                    txtAttributeReportName.Visible = false;
                }
                pnlAttributeReport.Visible = false;
                pnlCustomStandardReport_Process_attribute.Visible = false;
                pnlCustomStandardReport_Selected.Visible = true;
                btnAddNewColumn.Visible = true;
                pnlReportType.Visible = false;
                txtCustomStandardColumnName.Text = "";
            }
        }
        else
        {
            Session["GridviewCustomStandardReport"] = "";
            if (MixReport)
            {
                string attributeName = "";
                if (Session["ExistingReportsDictionary_process"] != null)
                {
                    AllExistingReportsDic = (Dictionary<int, string>)Session["ExistingReportsDictionary_process"];
                    // for multiple activities
                    string prefix = string.Empty;
                    string middle = string.Empty;
                    string postfix = string.Empty;

                    for (int i = 0; i < AllExistingReportsDic.Count; i++)
                    {
                        int actID = Convert.ToInt32(AllExistingReportsDic.ElementAt(i).Key);
                        string str = Convert.ToString(AllExistingReportsDic.ElementAt(i).Value);
                        //string str = "welcome@to-aspdotnet#sueresh.com";
                        string actName = Regex.Replace(str, "[^a-zA-Z0-9_]+", ""); // actName will be used in sql query as column thats why we need to remove all special characters here
                        if (i == 0)
                        {
                            prefix = "tab0.AttributeName";
                            middle = ",tab0.AttributeValue as [" + actName + "]";
                            //postfix = " from (SELECT AttributeName,AttributeValue FROM [NewVisualERP].[dbo].[tbl_AttributesMenu] where ProcessObjectID ='" + actID.ToString() + "') as tab0";
                            postfix = " from (SELECT AttributeName,AttributeValue FROM tbl_AttributesMenu where ProcessObjectID ='" + actID.ToString() + "') as tab0";
                        }
                        else
                        {
                            prefix = "ISNULL(" + prefix + ",tab" + i.ToString() + ".AttributeName)";
                            middle += ",tab" + i + ".AttributeValue as [" + actName + "]";
                            //postfix += " FULL OUTER JOIN (SELECT AttributeName,AttributeValue FROM [NewVisualERP].[dbo].[tbl_AttributesMenu] where ProcessObjectID ='" + actID.ToString() + "') as tab" + i.ToString() + " on tab0.AttributeName = tab" + i + ".AttributeName";
                            postfix += " FULL OUTER JOIN (SELECT AttributeName,AttributeValue FROM tbl_AttributesMenu where ProcessObjectID ='" + actID.ToString() + "') as tab" + i.ToString() + " on tab0.AttributeName = tab" + i + ".AttributeName";
                        }
                    }

                    string query = "Select distinct " + prefix + " as Attribute" + middle + postfix;

                    // Declare the query string.
                    // Run the query and bind the resulting DataSet
                    // to the GridView control.
                    DataSet ds = GetData(query);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    dtSpecificOrders = dt.Clone();
                    for (int j = 0; j < ExistingReports_Attribute_Attribute.Count; j++)
                    {
                        attributeName = Convert.ToString(ExistingReports_Attribute_Attribute[j].ToString());
                        DataRow[] orderRows = dt.Select("Attribute = '" + attributeName + "'");

                        foreach (DataRow dr in orderRows)
                        {
                            dtSpecificOrders.ImportRow(dr);
                        }
                    }
                    if (dtSpecificOrders.Rows.Count > 0)
                    {
                        Session["GridviewCustomStandardReport"] = dtSpecificOrders;
                        GridviewCustomStandardReport.DataSource = dtSpecificOrders;
                        GridviewCustomStandardReport.DataBind();
                        pnlListSavedReport.Visible = false;
                        //ltrTotalRecord.Text = dtSpecificOrders.Rows.Count.ToString();
                        divErrorMsg.Visible = true;
                        //lnkbtnSaveReport.Visible = true;
                        if (RoleID == 4)
                        {
                            //lnkbtnSaveReport.Visible = false;
                            txtAttributeReportName.Visible = false;
                        }
                        pnlAttributeReport.Visible = false;
                        pnlCustomStandardReport_Process_attribute.Visible = false;
                        //lnkbtnExporttoExcel.Visible = true;
                        //liSaveReport.Visible = true;
                        pnlCustomStandardReport_Selected.Visible = true;
                        btnAddNewColumn.Visible = true;
                        //liExporttoExcel.Visible = true;
                    }
                    else
                    {
                        lnkbtnExporttoExcel.Visible = false;
                        liExporttoExcel.Visible = false;
                        //Message.Text = "Unable to connect to the database.";
                    }
                }
            }
            else
            {
                string attributeName = "";
                // var getRecord;
                if (Session["ActivityDictionary"] != null)
                {
                    activityDic = (Dictionary<int, string>)Session["ActivityDictionary"];
                    // for multiple activities
                    string prefix = string.Empty;
                    string middle = string.Empty;
                    string postfix = string.Empty;

                    for (int i = 0; i < activityDic.Count; i++)
                    {
                        int actID = Convert.ToInt32(activityDic.ElementAt(i).Key);
                        string str = Convert.ToString(activityDic.ElementAt(i).Value);
                        //string str = "welcome@to-aspdotnet#sueresh.com";
                        string actName = Regex.Replace(str, "[^a-zA-Z0-9_]+", ""); // actName will be used in sql query as column thats why we need to remove all special characters here
                        if (i == 0)
                        {
                            prefix = "tab0.AttributeName";
                            middle = ",tab0.AttributeValue as [" + actName + "]";
                            //postfix = " from (SELECT AttributeName,AttributeValue FROM [NewVisualERP].[dbo].[tbl_AttributesMenu] where ProcessObjectID ='" + actID.ToString() + "') as tab0";
                            postfix = " from (SELECT AttributeName,AttributeValue FROM tbl_AttributesMenu where ProcessObjectID ='" + actID.ToString() + "') as tab0";
                        }
                        else
                        {
                            prefix = "ISNULL(" + prefix + ",tab" + i.ToString() + ".AttributeName)";
                            middle += ",tab" + i + ".AttributeValue as [" + actName + "]";
                            //postfix += " FULL OUTER JOIN (SELECT AttributeName,AttributeValue FROM [NewVisualERP].[dbo].[tbl_AttributesMenu] where ProcessObjectID ='" + actID.ToString() + "') as tab" + i.ToString() + " on tab0.AttributeName = tab" + i + ".AttributeName";
                            postfix += " FULL OUTER JOIN (SELECT AttributeName,AttributeValue FROM tbl_AttributesMenu where ProcessObjectID ='" + actID.ToString() + "') as tab" + i.ToString() + " on tab0.AttributeName = tab" + i + ".AttributeName";
                        }
                    }

                    string query = "Select distinct " + prefix + " as Attribute" + middle + postfix;

                    // Declare the query string.
                    // Run the query and bind the resulting DataSet
                    // to the GridView control.
                    DataSet ds = GetData(query);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    dtSpecificOrders = dt.Clone();
                    for (int j = 0; j < attribute.Count; j++)
                    {
                        attributeName = Convert.ToString(attribute[j].ToString());
                        DataRow[] orderRows = dt.Select("Attribute = '" + attributeName + "'");

                        foreach (DataRow dr in orderRows)
                        {
                            dtSpecificOrders.ImportRow(dr);
                        }
                    }
                    if (dtSpecificOrders.Rows.Count > 0)
                    {
                        Session["GridviewCustomStandardReport"] = dtSpecificOrders;
                        GridView1.DataSource = dtSpecificOrders;
                        GridView1.DataBind();
                        pnlListSavedReport.Visible = false;
                        //ltrTotalRecord.Text = dtSpecificOrders.Rows.Count.ToString();
                        divErrorMsg.Visible = true;
                        lnkbtnSaveReport.Visible = true;
                        if (RoleID == 4)
                        {
                            lnkbtnSaveReport.Visible = false;
                            txtAttributeReportName.Visible = false;
                        }
                        pnlAttributeReport.Visible = true;
                        lnkbtnExporttoExcel.Visible = true;
                        liSaveReport.Visible = true;
                        liExporttoExcel.Visible = true;
                    }
                    else
                    {
                        lnkbtnExporttoExcel.Visible = false;
                        liExporttoExcel.Visible = false;
                        //Message.Text = "Unable to connect to the database.";
                    }
                }
            }
        }
    }



    /// <summary>
    /// run created query using dataset
    /// </summary>
    /// <param name="queryString"></param>
    /// <returns></returns>
    public DataSet GetData(String queryString)
    {
        VisualERPDataContext dataContext = new VisualERPDataContext();
        System.Data.SqlClient.SqlConnectionStringBuilder builder =
        new System.Data.SqlClient.SqlConnectionStringBuilder();

        DataSet ds = new DataSet();

        try
        {
            // Connect to the database and run the query.
            // string sqlConnString = System.Configuration.ConfigurationManager.ConnectionStrings["VisualERPConnectionString1"].ConnectionString;
            SqlDataAdapter adapter = new SqlDataAdapter(queryString, dataContext.Connection.ConnectionString);
            // Fill the DataSet.
            adapter.Fill(ds);

        }
        catch (Exception ex)
        {
        }
        return ds;

    }

    protected void lnkbtnList_Click(object sender, EventArgs e)
    {
        //pnlActivity.Visible = false; pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
        //pnlListSavedReport.Visible = true;
        //divErrorMsg.Visible = false;



        //pnlReportType.Visible = true;
        //pnlActivity.Visible = false; pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
        //pnlAttribute.Visible = false;
        //pnlListSavedReport.Visible = false;
        //pnlAttributeReport.Visible = false;

    }

    protected void lnkbtnAdd_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in chkboxActivity.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in chkboxInventory.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in chkboxError.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in chkboxAttribute.Items)
        {
            item.Selected = false;
        }

        foreach (ListItem item in chkboxInventoryAttribute.Items)
        {
            item.Selected = false;
        }

        foreach (ListItem item in chkboxBomProcess.Items)
        {
            item.Selected = false;
        }

        pnlErrorReport.Visible = false;
        pnlReportType.Visible = true;
        pnlActivity.Visible = false; pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
        pnlAttribute.Visible = false;
        pnlBomProcess.Visible = false; pnlEror.Visible = false;
        pnlAttributeReport.Visible = false;
        pnlBomReport.Visible = false;
        pnlTFGReport.Visible = false;
        pnlTgtValueGap.Visible = false;
        pnlMachineReport.Visible = false;
        pnlESAReport.Visible = false;
        pnlListSavedReport.Visible = false;
        divErrorMsg.Visible = false;
        pnlInventoryAttribute.Visible = false;
        pnlErrorAttribute.Visible = false;
        this.IsEdit = false;

    }

    protected void lnkbtnSaveReport_Click(object sender, EventArgs e)
    {
        if (Session["CurrentReport"] != null)
        {
            int reporttyp = Convert.ToInt32(Session["CurrentReport"]);

            if (reporttyp == 1)
            {
                if (Activity.GetDuplicateCheckReportName(txtAttributeReportName.Text.Trim(), ProcessId, (int)ReportTypeID.Attribute, Convert.ToInt32(ViewState["ReportEditID"])))
                {
                    tbl_Report reportdata = new tbl_Report();
                    string activityID = string.Empty;
                    string attName = string.Empty;
                    if (Session["Activity"] != null)
                    {
                        activity = (List<int>)Session["Activity"]; // getting activity id from session that are selected           

                        for (int i = 0; i < activity.Count; i++)
                        {
                            activityID += activity[i].ToString() + ",";
                        }

                        reportdata.ProcessObjID = activityID.ToString();
                    }

                    if (Session["AttributeName"] != null)
                    {
                        attribute = (List<string>)Session["AttributeName"]; // getting activity id from session that are selected           

                        for (int i = 0; i < attribute.Count; i++)
                        {
                            attName += attribute[i].ToString() + ",";
                        }

                        reportdata.AttributeName = attName.ToString();
                        reportdata.ReportTypeID = (int)ReportTypeID.Attribute;
                    }

                    if (Session["SelectedNodeValue"] != null)
                    {
                        ProcessId = Convert.ToInt32(Session["SelectedNodeValue"]); // getting activity id from session that are selected 
                        reportdata.ProcessID = ProcessId;
                        reportdata.ReportName = txtAttributeReportName.Text;
                        if (this.IsEdit == true)
                        {
                            reportdata.ReportID = EditIDINT;
                        }
                    }

                    var listData = Activity.SaveReportData(reportdata);
                    if (listData == true)
                    {
                        pnlAttributeReport.Visible = false;
                        pnlTFGReport.Visible = false;
                        pnlListSavedReport.Visible = true;
                        SetSaveMessage();
                    }
                }
                else
                {
                    SetErrorMessage();
                }
            }
            if (reporttyp == 2)
            {

                if (Activity.GetDuplicateCheckReportName(txtBomReportName.Text.Trim(), ProcessId, (int)ReportTypeID.Bom, Convert.ToInt32(ViewState["ReportEditID"])))
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
                        reportdata.ReportName = txtBomReportName.Text;
                        if (this.IsEdit == true)
                        {
                            reportdata.ReportID = EditIDINT;
                        }
                    }

                    var listData = Activity.SaveReportData(reportdata);
                    if (listData == true)
                    {
                        pnlBomReport.Visible = false;
                        pnlTFGReport.Visible = false;
                        pnlListSavedReport.Visible = true;
                        SetSaveMessage();
                    }
                }
                else
                {
                    SetErrorMessage();
                }
            }
            if (reporttyp == 3)
            {
                if (Activity.GetDuplicateCheckReportName(txtTFGReportName.Text.Trim(), ProcessId, (int)ReportTypeID.TFG, Convert.ToInt32(ViewState["ReportEditID"])))
                {
                    tbl_Report reportdata = new tbl_Report();
                    string activityID = string.Empty;
                    string attName = string.Empty;
                    if (Session["Activity"] != null)
                    {
                        activity = (List<int>)Session["Activity"]; // getting activity id from session that are selected           

                        for (int i = 0; i < activity.Count; i++)
                        {
                            activityID += activity[i].ToString() + ",";
                        }

                        reportdata.ProcessObjID = activityID.ToString();
                    }
                    reportdata.ReportTypeID = (int)ReportTypeID.TFG;

                    if (Session["SelectedNodeValue"] != null)
                    {
                        ProcessId = Convert.ToInt32(Session["SelectedNodeValue"]); // getting activity id from session that are selected 
                        reportdata.ProcessID = ProcessId;
                        reportdata.ReportName = txtTFGReportName.Text;
                        if (this.IsEdit == true)
                        {
                            reportdata.ReportID = EditIDINT;
                        }
                    }

                    var listData = Activity.SaveReportData(reportdata);
                    if (listData == true)
                    {
                        pnlAttributeReport.Visible = false;
                        pnlBomReport.Visible = false;
                        pnlTFGReport.Visible = false;
                        pnlListSavedReport.Visible = true;
                        SetSaveMessage();
                    }
                }
                else
                {
                    SetErrorMessage();
                }
            }
            if (reporttyp == 4)
            {
                if (Activity.GetDuplicateCheckReportName(txtMachineReportName.Text.Trim(), ProcessId, (int)ReportTypeID.Machine, Convert.ToInt32(ViewState["ReportEditID"])))
                {
                    tbl_Report reportdata = new tbl_Report();
                    string activityID = string.Empty;
                    string attName = string.Empty;
                    if (Session["Activity"] != null)
                    {
                        activity = (List<int>)Session["Activity"]; // getting activity id from session that are selected           

                        for (int i = 0; i < activity.Count; i++)
                        {
                            activityID += activity[i].ToString() + ",";
                        }

                        reportdata.ProcessObjID = activityID.ToString();
                    }
                    reportdata.ReportTypeID = (int)ReportTypeID.Machine;

                    if (Session["SelectedNodeValue"] != null)
                    {
                        ProcessId = Convert.ToInt32(Session["SelectedNodeValue"]); // getting activity id from session that are selected 
                        reportdata.ProcessID = ProcessId;
                        reportdata.ReportName = txtMachineReportName.Text;
                        if (this.IsEdit == true)
                        {
                            reportdata.ReportID = EditIDINT;
                        }
                    }

                    var listData = Activity.SaveReportData(reportdata);
                    if (listData == true)
                    {
                        pnlAttributeReport.Visible = false;
                        pnlBomReport.Visible = false;
                        pnlTFGReport.Visible = false;
                        pnlMachineReport.Visible = false;
                        pnlListSavedReport.Visible = true;
                        SetSaveMessage();
                    }
                }
                else
                {
                    SetErrorMessage();
                }
            }
            if (reporttyp == 5 || reporttyp == 6)
            {
                int ReportType = 0;
                if (Convert.ToInt32(Session["CurrentReport"]) == 5)
                    ReportType = Convert.ToInt32(ReportTypeID.PCS);
                else if (Convert.ToInt32(Session["CurrentReport"]) == 6)
                    ReportType = Convert.ToInt32(ReportTypeID.DCS);
                if (Activity.GetDuplicateCheckReportName(txtESAReportName.Text.Trim(), ProcessId, ReportType, Convert.ToInt32(ViewState["ReportEditID"])))
                {
                    tbl_Report reportdata = new tbl_Report();
                    string activityID = string.Empty;
                    string attName = string.Empty;
                    if (Session["Activity"] != null)
                    {
                        activity = (List<int>)Session["Activity"]; // getting activity id from session that are selected           

                        for (int i = 0; i < activity.Count; i++)
                        {
                            activityID += activity[i].ToString() + ",";
                        }

                        reportdata.ProcessObjID = activityID.ToString();
                    }
                    reportdata.ReportTypeID = ReportType;

                    if (Session["SelectedNodeValue"] != null)
                    {
                        ProcessId = Convert.ToInt32(Session["SelectedNodeValue"]); // getting activity id from session that are selected 
                        reportdata.ProcessID = ProcessId;
                        reportdata.ReportName = txtESAReportName.Text;
                        if (this.IsEdit == true)
                        {
                            reportdata.ReportID = EditIDINT;
                        }
                    }

                    var listData = Activity.SaveReportData(reportdata);
                    if (listData == true)
                    {
                        pnlAttributeReport.Visible = false;
                        pnlBomReport.Visible = false;
                        pnlTFGReport.Visible = false;
                        pnlMachineReport.Visible = false;
                        pnlESAReport.Visible = false;
                        pnlListSavedReport.Visible = true;

                        bool updateForm = false;
                        tbl_PPESAnPDESA data = new tbl_PPESAnPDESA();
                        foreach (GridViewRow row in grdESAReport.Rows)
                        {
                            Literal FormID = row.FindControl("litFormID") as Literal;
                            data.FormID = Convert.ToInt32(FormID.Text);
                            data.Cpk = (row.FindControl("txtCpk") as TextBox).Text;
                            data.Cp = (row.FindControl("txtCp") as TextBox).Text;
                            data.Ppk = (row.FindControl("txtPpk") as TextBox).Text;
                            data.LongtermSigma = (row.FindControl("txtLongtermSigma") as TextBox).Text;
                            data.ShorttermSigma = (row.FindControl("txtShorttermSigma") as TextBox).Text;
                            data.ZScore = (row.FindControl("txtZScore") as TextBox).Text;
                            updateForm = PPESAnPDESA.UpdatePPESAnPDESADataOnReport(data); // update aditional fields in form table                            
                        }
                        if (updateForm == true)
                        {
                            SetSaveMessage();
                        }

                    }
                }
                else
                {
                    SetErrorMessage();
                }
            }

            if (reporttyp == 8)
            {
                if (Activity.GetDuplicateCheckReportName(txtInventoryAttributeReportName.Text.Trim(), ProcessId, (int)ReportTypeID.Attribute, Convert.ToInt32(ViewState["ReportEditID"])))
                {
                    tbl_Report reportdata = new tbl_Report();
                    string activityID = string.Empty;
                    string attName = string.Empty;
                    if (Session["InventoryValue"] != null)
                    {
                        activity = (List<int>)Session["InventoryValue"]; // getting activity id from session that are selected           

                        for (int i = 0; i < activity.Count; i++)
                        {
                            activityID += activity[i].ToString() + ",";
                        }

                        reportdata.ProcessObjID = activityID.ToString();
                    }

                    if (Session["InventoryAttributeName"] != null)
                    {
                        attribute = (List<string>)Session["InventoryAttributeName"]; // getting activity id from session that are selected           

                        for (int i = 0; i < attribute.Count; i++)
                        {
                            attName += attribute[i].ToString() + ",";
                        }

                        reportdata.AttributeName = attName.ToString();
                        reportdata.ReportTypeID = (int)ReportTypeID.Inventory;
                    }

                    if (Session["SelectedNodeValue"] != null)
                    {
                        ProcessId = Convert.ToInt32(Session["SelectedNodeValue"]); // getting activity id from session that are selected 
                        reportdata.ProcessID = ProcessId;
                        reportdata.ReportName = txtInventoryAttributeReportName.Text;
                        if (this.IsEdit == true)
                        {
                            reportdata.ReportID = EditIDINT;
                        }
                    }

                    var listData = Activity.SaveReportData(reportdata);
                    if (listData == true)
                    {
                        pnlAttributeReport.Visible = false;
                        pnlTFGReport.Visible = false;
                        pnlListSavedReport.Visible = true;
                        SetSaveMessage();

                    }
                }
                else
                {
                    SetErrorMessage();
                }
            }

            if (reporttyp == 9)
            {
                //        Session["Error"]
                //Session["ErrorName"]
                //Session["ErrorDictionary"]
                if (Activity.GetDuplicateCheckReportName(txtErrorAttributeReportName.Text.Trim(), ProcessId, (int)ReportTypeID.Attribute, Convert.ToInt32(ViewState["ReportEditID"])))
                {
                    tbl_Report reportdata = new tbl_Report();
                    string activityID = string.Empty;
                    string attName = string.Empty;
                    if (Session["Error"] != null)
                    {
                        activity = (List<int>)Session["Error"]; // getting activity id from session that are selected           

                        for (int i = 0; i < activity.Count; i++)
                        {
                            activityID += activity[i].ToString() + ",";
                        }

                        reportdata.ProcessObjID = activityID.ToString();
                    }

                    if (Session["ErrorAttributeName"] != null)
                    {
                        attribute = (List<string>)Session["ErrorAttributeName"]; // getting activity id from session that are selected           

                        for (int i = 0; i < attribute.Count; i++)
                        {
                            attName += attribute[i].ToString() + ",";
                        }

                        reportdata.AttributeName = attName.ToString();
                        reportdata.ReportTypeID = (int)ReportTypeID.Error;
                    }

                    if (Session["SelectedNodeValue"] != null)
                    {
                        ProcessId = Convert.ToInt32(Session["SelectedNodeValue"]); // getting activity id from session that are selected 
                        reportdata.ProcessID = ProcessId;
                        reportdata.ReportName = txtErrorAttributeReportName.Text;
                        if (this.IsEdit == true)
                        {
                            reportdata.ReportID = EditIDINT;
                        }
                    }

                    var listData = Activity.SaveReportData(reportdata);
                    if (listData == true)
                    {
                        pnlAttributeReport.Visible = false;
                        pnlTFGReport.Visible = false;
                        pnlListSavedReport.Visible = true;
                        pnlErrorReport.Visible = false;
                        SetSaveMessage();

                    }
                }
                else
                {
                    SetErrorMessage();
                }
            }
            BindSavedReportGrid(ProcessId);
            this.IsEdit = false;
        }
    }

    public void SetSaveMessage()
    {
        lblMsg.Visible = true;
        divErrorMsg.Visible = true;
        lblMsg.Text = "Report saved successfully.";
        lblMsg.Style.Add("color", "green");
        divErrorMsg.Style.Add("min-width", "200px");
        divErrorMsg.Style.Add("margin-right", "466px");
        divErrorMsg.Style.Add("margin-left", "0px");
        divErrorMsg.Style.Add("float", "right");
        divErrorMsg.Style.Add("padding", "7px 0px 0px 24px");
        divErrorMsg.Attributes.Add("class", "isa_success");
    }

    public void SetErrorMessage()
    {
        lblMsg.Visible = true;
        divErrorMsg.Visible = true;
        lblMsg.Text = "Report already exists.";
        lblMsg.Style.Add("color", "red");
        divErrorMsg.Style.Add("min-width", "200px");
        divErrorMsg.Style.Add("margin-right", "466px");
        divErrorMsg.Style.Add("margin-left", "0px");
        divErrorMsg.Style.Add("float", "right");
        divErrorMsg.Style.Add("padding", "7px 0px 0px 24px");
        divErrorMsg.Attributes.Add("class", "isa_error");
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
                lblMsg.Visible = true;
                divErrorMsg.Visible = true;
                lblMsg.Text = "Report has been deleted successfully";
                lblMsg.Style.Add("color", "green");
                divErrorMsg.Style.Add("min-width", "231px");
                divErrorMsg.Style.Add("margin-right", "466px");
                divErrorMsg.Style.Add("margin-left", "0px");
                divErrorMsg.Style.Add("float", "right");
                divErrorMsg.Style.Add("padding", "7px 0px 0px 8px");
                divErrorMsg.Attributes.Add("class", "isa_success");
            }
            else
            {
                //lblMsg.Visible = true;
                //lblMsg.Text = "Error on Deleting data.!";
                //lblMsg.CssClass = "msgError";
                lblMsg.Visible = true;
                divErrorMsg.Visible = true;
                lblMsg.Text = "Error on Deleting data.!";
                lblMsg.Style.Add("color", "red");
                divErrorMsg.Style.Add("min-width", "231px");
                divErrorMsg.Style.Add("margin-right", "466px");
                divErrorMsg.Style.Add("margin-left", "0px");
                divErrorMsg.Style.Add("float", "right");
                divErrorMsg.Style.Add("padding", "7px 0px 0px 8px");
                divErrorMsg.Attributes.Add("class", "isa_error");
            }
            //ClearControl();
            BindSavedReportGrid(ProcessId);
            this.EditIDINT = 0;
            this.IsEdit = false;
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
        ViewState["ReportEditID"] = this.EditIDINT;
        int reportTyp = Activity.GetReportType(EditIDINT);
        var row = Activity.GetDataByReportID(EditIDINT); // function will get report data by report id 
        if (row != null)
        {
            if (reportTyp == 1)
            {
                Session["CurrentReport"] = reportTyp;
                string ProcessObjID = row.ProcessObjID; // processObj ID in string 
                string AttributeName = row.AttributeName; // attributes name in string 
                string ReportName = row.ReportName;    // get report name

                string[] activityItem = ProcessObjID.Split(','); //split menthod that will split processobID
                string[] attributeItem = AttributeName.Split(',');
                for (int p = 0; p < activityItem.Length - 1; p++)
                {
                    activity.Add(Convert.ToInt32(activityItem[p])); // getting processobID in list activity list<int>
                    Session["Activity"] = activity;
                    string acctivityname = Activity.GetActivityNameByProcessObjId(Convert.ToInt32(activityItem[p]));
                    activityDic.Add(Convert.ToInt32(activityItem[p]), acctivityname);
                }

                Session["ActivityDictionary"] = activityDic;
                attribute.Clear();
                for (int q = 0; q < attributeItem.Length - 1; q++) // getting attributesName in list attributes list<string>
                {
                    attribute.Add(Convert.ToString(attributeItem[q]));
                    Session["AttributeName"] = attribute;
                }

                GetAttributeReportData(false, false, ""); // it will get report for selected reportID
                txtAttributeReportName.Text = ReportName;
                pnlBomReport.Visible = false;
                pnlTFGReport.Visible = false;
                pnlAttributeReport.Visible = true;
                this.IsEdit = true;
            }
            if (reportTyp == 2)
            {
                Session["CurrentReport"] = reportTyp;
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
                txtBomReportName.Text = ReportName;
                pnlAttributeReport.Visible = false;
                pnlTFGReport.Visible = false;
                pnlBomReport.Visible = true;
                this.IsEdit = true;

            }
            if (reportTyp == 3)
            {
                Session["CurrentReport"] = reportTyp;
                string ProcessObjID = row.ProcessObjID; // processObj ID in string 
                                                        //string BomProcessIds = row.AttributeName; // attributes name in string 
                string ReportName = row.ReportName;    // get report name

                string[] activityItem = ProcessObjID.Split(','); //split menthod that will split processobID
                                                                 //string[] bomProcessItem = BomProcessIds.Split(',');
                for (int p = 0; p < activityItem.Length - 1; p++)
                {
                    activity.Add(Convert.ToInt32(activityItem[p])); // getting processobID in list activity list<int>
                    Session["Activity"] = activity;
                    string acctivityname = Activity.GetActivityNameByProcessObjId(Convert.ToInt32(activityItem[p]));
                    activityDic.Add(Convert.ToInt32(activityItem[p]), acctivityname);
                }

                Session["ActivityDictionary"] = activityDic;
                GetTFGReportData(); // it will get report for selected reportID
                txtTFGReportName.Text = ReportName;
                pnlAttributeReport.Visible = false;
                pnlTFGReport.Visible = false;
                pnlBomReport.Visible = false;
                pnlTFGReport.Visible = true;
                divReportName.Visible = true;
                this.IsEdit = true;

            }

            if (reportTyp == 4)
            {
                Session["CurrentReport"] = reportTyp;
                string ProcessObjID = row.ProcessObjID; // processObj ID in string 
                                                        //string BomProcessIds = row.AttributeName; // attributes name in string 
                string ReportName = row.ReportName;    // get report name

                string[] activityItem = ProcessObjID.Split(','); //split menthod that will split processobID
                                                                 //string[] bomProcessItem = BomProcessIds.Split(',');
                for (int p = 0; p < activityItem.Length - 1; p++)
                {
                    activity.Add(Convert.ToInt32(activityItem[p])); // getting processobID in list activity list<int>
                    Session["Activity"] = activity;
                    string acctivityname = Activity.GetActivityNameByProcessObjId(Convert.ToInt32(activityItem[p]));
                    activityDic.Add(Convert.ToInt32(activityItem[p]), acctivityname);
                }

                Session["ActivityDictionary"] = activityDic;
                GetMachineReportData(); // it will get report for selected reportID
                txtMachineReportName.Text = ReportName;
                pnlAttributeReport.Visible = false;
                pnlBomReport.Visible = false;
                pnlTFGReport.Visible = false;
                pnlMachineReport.Visible = true;
                divMachineName.Visible = true;
                this.IsEdit = true;

            }

            if (reportTyp == 5 || reportTyp == 6)
            {
                Session["CurrentReport"] = reportTyp;
                List<PPESAnPDESA.ListPPESAnPDESAData> ESADataR = new List<PPESAnPDESA.ListPPESAnPDESAData>();


                ViewState["sortBy"] = "Sequence";
                ViewState["isAsc"] = "1";

                int ESAtype = 0;

                ESAtype = Convert.ToInt32(FormType.PPESA);
                int reportid = 0;
                if (row.ReportID > 0)
                    reportid = row.ReportID;

                ESADataR.AddRange(PPESAnPDESA.GetPPESAnPDESAReportData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), ESAtype, ProcessId, reportid));
                if (ESADataR.Count > 0)
                    lnkbtnExporttoExcel.Visible = true;
                liExporttoExcel.Visible = true;

                txtESAReportName.Attributes.Add("readonly", "readonly");
                txtESAReportName.Text = ESADataR[0].ReportName;
                grdESAReport.DataSource = ESADataR;
                grdESAReport.DataBind();
                pnlAttributeReport.Visible = false;
                pnlBomReport.Visible = false;
                pnlTFGReport.Visible = false;
                pnlMachineReport.Visible = false;
                pnlESAReport.Visible = true;
                divESAName.Visible = true;
                this.IsEdit = true;


            }

            if (reportTyp == 8)
            {
                Session["CurrentReport"] = reportTyp;
                string ProcessObjID = row.ProcessObjID; // processObj ID in string 
                string AttributeName = row.AttributeName; // attributes name in string 
                string ReportName = row.ReportName;    // get report name

                string[] activityItem = ProcessObjID.Split(','); //split menthod that will split processobID
                string[] attributeItem = AttributeName.Split(',');
                for (int p = 0; p < activityItem.Length - 1; p++)
                {
                    activity.Add(Convert.ToInt32(activityItem[p])); // getting processobID in list activity list<int>
                    Session["InventoryValue"] = activity;
                    string acctivityname = Activity.GetActivityNameByProcessObjId(Convert.ToInt32(activityItem[p]));
                    activityDic.Add(Convert.ToInt32(activityItem[p]), acctivityname);
                }

                Session["InventoryDictionary"] = activityDic;
                attribute.Clear();
                for (int q = 0; q < attributeItem.Length - 1; q++) // getting attributesName in list attributes list<string>
                {
                    attribute.Add(Convert.ToString(attributeItem[q]));
                    Session["InventoryAttributeName"] = attribute;
                }

                GetInventoryAttributeReportData(attribute); // it will get report for selected reportID
                txtInventoryAttributeReportName.Text = ReportName;
                pnlBomReport.Visible = false;
                pnlTFGReport.Visible = false;
                pnlAttributeReport.Visible = false;
                pnlInventoryReport.Visible = true;
                this.IsEdit = true;
            }
            if (reportTyp == 9)
            {
                Session["CurrentReport"] = reportTyp;
                string ProcessObjID = row.ProcessObjID; // processObj ID in string 
                string AttributeName = row.AttributeName; // attributes name in string 
                string ReportName = row.ReportName;    // get report name

                string[] activityItem = ProcessObjID.Split(','); //split menthod that will split processobID
                string[] attributeItem = AttributeName.Split(',');
                for (int p = 0; p < activityItem.Length - 1; p++)
                {
                    activity.Add(Convert.ToInt32(activityItem[p])); // getting processobID in list activity list<int>
                    Session["Error"] = activity;
                    string acctivityname = Activity.GetActivityNameByProcessObjId(Convert.ToInt32(activityItem[p]));
                    activityDic.Add(Convert.ToInt32(activityItem[p]), acctivityname);
                }
                Session["ErrorDictionary"] = activityDic;
                attribute.Clear();
                for (int q = 0; q < attributeItem.Length - 1; q++) // getting attributesName in list attributes list<string>
                {
                    attribute.Add(Convert.ToString(attributeItem[q]));
                    Session["ErrorAttributeName"] = attribute;
                }

                GetErrorAttributeReportData(attribute); // it will get report for selected reportID
                txtErrorAttributeReportName.Text = ReportName;
                pnlBomReport.Visible = false;
                pnlTFGReport.Visible = false;
                pnlAttributeReport.Visible = false;
                pnlInventoryReport.Visible = false;
                pnlErrorReport.Visible = true;
                this.IsEdit = true;
            }
        }
        else
        {
            // no relavent data found to this report ID
        }
        pnlActivity.Visible = false;
        pnlInventory.Visible = false;
        pnlCustomStandardReport.Visible = false;
        pnlAttribute.Visible = false;
        pnlEror.Visible = false;
        pnlBomProcess.Visible = false;
        pnlListSavedReport.Visible = false;
        lnkbtnSaveReport.Visible = true;
        
        if (RoleID == 4)
        {
            lnkbtnSaveReport.Visible = false;
            txtAttributeReportName.Visible = false;
        }
        liSaveReport.Visible = true;

     
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

    public class NodeItem
    {
        public int? NodeValue { get; set; }
        public string NodeText { get; set; }
    }

    protected void lnkBtnAttributeReport_Click(object sender, EventArgs e)
    {
        Session["CurrentReport"] = (int)ReportTypeID.Attribute;
        if (ProcessId != 0) // if process is selected activity block will display
        {
            pnlActivity.Visible = true;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlESAReport.Visible = false;
            pnlListSavedReport.Visible = false;
            divErrorMsg.Visible = false;
            //lblCurrentReport.Text = "Attribute Report";
        }
        else
        {
            pnlActivity.Visible = false; pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false; pnlEror.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlESAReport.Visible = false;
            pnlListSavedReport.Visible = false;
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please choose the process.!";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "194px");
            divErrorMsg.Style.Add("margin-left", "554px");
            divErrorMsg.Attributes.Add("class", "isa_warning");
        }
        this.IsEdit = false;
    }


    protected void btnTgtInventoryReport_Click(object sender, EventArgs e)
    {
        Session["CurrentReport"] = (int)ReportTypeID.Inventory;

        if (ProcessId != 0) // if process is selected activity block will display
        {
            pnlActivity.Visible = false;
            pnlInventory.Visible = true; pnlEror.Visible = false;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlESAReport.Visible = false;
            pnlListSavedReport.Visible = false;
            divErrorMsg.Visible = false;

        }
        else
        {
            pnlActivity.Visible = false;
            pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false; pnlEror.Visible = false;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlESAReport.Visible = false;
            pnlListSavedReport.Visible = false;
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please choose the process.!";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "194px");
            divErrorMsg.Style.Add("margin-left", "554px");
            divErrorMsg.Attributes.Add("class", "isa_warning");
        }
        this.IsEdit = false;
    }

    protected void btnTgtCustomStandardReport_Click(object sender, EventArgs e)
    {
        Session["CurrentReport"] = (int)ReportTypeID.CustomStandardReport;

        if (ProcessId != 0) // if process is selected activity block will display
        {
            pnlActivity.Visible = false;
            pnlInventory.Visible = false;
            pnlCustomStandardReport.Visible = true;
            pnlEror.Visible = false;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlESAReport.Visible = false;
            pnlListSavedReport.Visible = false;
            divErrorMsg.Visible = false;

        }
        else
        {
            pnlActivity.Visible = false;
            pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false; pnlEror.Visible = false;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlESAReport.Visible = false;
            pnlListSavedReport.Visible = false;
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please choose the process.!";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "194px");
            divErrorMsg.Style.Add("margin-left", "554px");
            divErrorMsg.Attributes.Add("class", "isa_warning");
        }
        this.IsEdit = false;
    }

    protected void btnTgtErrorReport_Click(object sender, EventArgs e)
    {
        Session["CurrentReport"] = (int)ReportTypeID.Error;

        if (ProcessId != 0) // if process is selected activity block will display
        {
            pnlActivity.Visible = false;
            pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlESAReport.Visible = false;
            pnlListSavedReport.Visible = false;
            divErrorMsg.Visible = false;
            pnlEror.Visible = true;
        }
        else
        {
            pnlEror.Visible = false;
            pnlActivity.Visible = false;
            pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlESAReport.Visible = false;
            pnlListSavedReport.Visible = false;
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please choose the process.!";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "194px");
            divErrorMsg.Style.Add("margin-left", "554px");
            divErrorMsg.Attributes.Add("class", "isa_warning");
        }
        this.IsEdit = false;
    }

    protected void lnkBtnBOMReport_Click(object sender, EventArgs e)
    {
        Session["CurrentReport"] = (int)ReportTypeID.Bom;

        if (ProcessId != 0) // if process is selected activity block will display
        {
            pnlActivity.Visible = true;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlListSavedReport.Visible = false;
            divErrorMsg.Visible = false;
            //lblCurrentReport.Text = "BOM Report";
        }
        else
        {
            pnlActivity.Visible = false; pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlReportType.Visible = false; pnlEror.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlListSavedReport.Visible = false;
            //lblCurrentReport.Text = "BOM Report";
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please choose the process.!";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "194px");
            divErrorMsg.Style.Add("margin-left", "554px");
            divErrorMsg.Attributes.Add("class", "isa_warning");
        }
        this.IsEdit = false;
    }

    protected void lnkBtnTFGReport_Click(object sender, EventArgs e)
    {
        Session["CurrentReport"] = (int)ReportTypeID.TFG;

        if (ProcessId != 0) // if process is selected activity block will display
        {
            pnlActivity.Visible = true;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlListSavedReport.Visible = false;
            divErrorMsg.Visible = false;
            //lblCurrentReport.Text = "BOM Report";
        }
        else
        {
            pnlActivity.Visible = false; pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false; pnlEror.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlListSavedReport.Visible = false;
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please choose the process.!";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "194px");
            divErrorMsg.Style.Add("margin-left", "554px");
            divErrorMsg.Attributes.Add("class", "isa_warning");
        }
        this.IsEdit = false;


    }

    protected void lnkBtnMachineReport_Click(object sender, EventArgs e)
    {
        Session["CurrentReport"] = (int)ReportTypeID.Machine;
        if (ProcessId != 0) // if process is selected activity block will display
        {
            pnlActivity.Visible = true;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlMachineReport.Visible = false;
            pnlListSavedReport.Visible = false;
            divErrorMsg.Visible = false;

            //lblCurrentReport.Text = "BOM Report";
        }
        else    //else no process selected
        {
            pnlActivity.Visible = false; pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false; pnlEror.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlMachineReport.Visible = false;
            pnlListSavedReport.Visible = false;
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please choose the process.!";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "194px");
            divErrorMsg.Style.Add("margin-left", "554px");
            divErrorMsg.Attributes.Add("class", "isa_warning");
        }
        this.IsEdit = false;


    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    /// <summary>
    /// this will export excel file for multiple reports selection
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkbtnExporttoExcel_Click(object sender, EventArgs e)
    {
        string excelReportName = string.Empty;
        DataTable dt = new DataTable("GridView_Data");
        if (Session["CurrentReport"] != null) //Session["CurrentReport"] will have current report type 1 for attribute
        {
            int reporttyp = Convert.ToInt32(Session["CurrentReport"]);

            if (reporttyp == 1)
            {
                excelReportName = "AttributeReport";
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    dt.Columns.Add(cell.Text);
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    dt.Rows.Add();
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Text == "&nbsp;")
                            dt.Rows[dt.Rows.Count - 1][i] = "";
                        else
                            dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text;
                    }
                }
            }
            if (reporttyp == 2) //Session["CurrentReport"] will have current report type 1 for BOM
            {
                excelReportName = "BOMReport";
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
                }

                foreach (TableCell cell in grdBomReport.HeaderRow.Cells)
                {
                    dt.Columns.Add(cell.Text);
                }

                //because bom report grid is having bound fild there is no dataset so we will get grid data in datatable/dataset firstly
                //create DataTable Structure

                //get the list item and add into the list

                int count = 0;
                foreach (BomData.BomProcessData prop in BomProcessData)
                {
                    DataRow row = dt.NewRow(); // creating datarow for bom grid and make datatable of data
                    count += 1; // it will increase in loop by 1 for searial number
                    row["S No."] = count;
                    row["Description"] = prop.Description;
                    row["BOMLevel"] = prop.BOMLevel;
                    row["BOMRevision"] = prop.BOMRevision;
                    row["weight"] = prop.weight;
                    row["UOM"] = prop.UOM;
                    row["StandardCost"] = prop.StandardCost;
                    row["WorkingCost"] = prop.WorkingCost;
                    row["StdPackQty"] = prop.StdPackQty;
                    row["MaxPackLength"] = prop.MaxPackLength;
                    row["MaxPackWidth"] = prop.MaxPackWidth;
                    row["MaxPackHeight"] = prop.MaxPackHeight;
                    row["ContainerQty"] = prop.ContainerQty;
                    row["MedianRelinishmentLT"] = prop.MedianRelinishmentLT;
                    row["MinRLT"] = prop.MinRLT;
                    row["MaxRLT"] = prop.MaxRLT;
                    row["Rolling12MnthUsage"] = prop.Rolling12MnthUsage;
                    row["AvgMonthUsage"] = prop.AvgMonthUsage;
                    row["RiskFactor"] = prop.RiskFactor;
                    row["MonthStdDevRiskFactor"] = prop.MonthStdDevRiskFactor;
                    row["KanbanQty"] = prop.KanbanQty;
                    row["InService"] = prop.InService;
                    row["In_ServiceDate"] = prop.In_ServiceDate;
                    row["ObsolescenceDate"] = prop.ObsolescenceDate;
                    row["OnHandInventory"] = prop.OnHandInventory;
                    row["OnOrder"] = prop.OnOrder;
                    row["NextShipmentDue"] = prop.NextShipmentDue;
                    row["NextQtyDue"] = prop.NextQtyDue;
                    row["PartReqNxtPerd"] = prop.PartReqNxtPerd;
                    row["CurrentPurchasingOwner"] = prop.CurrentPurchasingOwner;
                    row["CurrentDesignOwner"] = prop.CurrentDesignOwner;
                    dt.Rows.Add(row);
                }

            }
            if (reporttyp == 3) //Session["CurrentReport"] will have current report type 1 for TFG
            {
                excelReportName = "TFGReport";
                List<TFGData.ListTFGData> TFGProcessData = new List<TFGData.ListTFGData>();
                if (Session["Activity"] != null)
                {
                    activity = (List<int>)Session["Activity"];
                    // for multiple activities

                    for (int i = 0; i < activity.Count; i++)
                    {
                        int prcobjID = Convert.ToInt32(activity[i].ToString()); //proobjId is selected atctivity id
                        ViewState["sortBy"] = "TFGID";
                        ViewState["isAsc"] = "1";
                        TFGProcessData.AddRange(TFGData.GetTFGData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), prcobjID));
                    }
                }

                foreach (TableCell cell in grdTFGReport.HeaderRow.Cells)
                {
                    dt.Columns.Add(cell.Text);
                }
                int count = 0;
                foreach (TFGData.ListTFGData prop in TFGProcessData)
                {
                    DataRow row = dt.NewRow(); // creating datarow for bom grid and make datatable of data
                    count += 1; // it will increase in loop by 1 for searial number
                    row["S No."] = count;
                    row["Tool"] = prop.Tool_Fixture_GageName;
                    row["Description"] = prop.TFGDescription;
                    row["Vendor Part"] = prop.TFGVendorPart;
                    row["Vendor"] = prop.TFGVendor;
                    row["Calibration Date"] = prop.CalibrationDate;
                    row["Cost"] = prop.Cost;
                    row["Qty"] = prop.TFGQty;
                    row["Calibration Cycle"] = prop.CalibrationCycle;
                    row["Time To Cailbrate"] = prop.TimeToCailbrate;
                    row["Cost To Calibrate"] = prop.CostToCalibrate;
                    row["Calibration Vendor"] = prop.CalibrationVendor;
                    row["Calibration Vendor Info"] = prop.CalibrationVendorInfo;
                    dt.Rows.Add(row); // datatable row has been created here 
                }
            }
            if (reporttyp == 4) //Session["CurrentReport"] will have current report type 1 for Machine
            {
                excelReportName = "MachineReport";
                List<MachineData.ListMachineData> MachineProcessData = new List<MachineData.ListMachineData>();
                // var getRecord;
                if (Session["Activity"] != null)
                {
                    activity = (List<int>)Session["Activity"];
                    // for multiple activities

                    for (int i = 0; i < activity.Count; i++)
                    {
                        int prcobjID = Convert.ToInt32(activity[i].ToString()); //proobjId is selected atctivity id
                        ViewState["sortBy"] = "TFGID";
                        ViewState["isAsc"] = "1";
                        MachineProcessData.AddRange(MachineData.GetMachineDataByObjId(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), prcobjID));
                    }
                }

                foreach (TableCell cell in grdMachineReport.HeaderRow.Cells)
                {
                    dt.Columns.Add(cell.Text);
                }
                int count = 0;
                foreach (MachineData.ListMachineData prop in MachineProcessData)
                {
                    DataRow row = dt.NewRow();
                    count += 1;
                    row["S No."] = count;
                    row["Machine Name"] = prop.MachineName;
                    row["Machine Type"] = prop.MachineType;
                    row["MTBF"] = prop.MTBF;
                    row["MTTR"] = prop.MTTR;
                    row["Maintenance Cost"] = prop.MaintenanceCost;
                    row["Purchase Price"] = prop.PurchasePrice;
                    row["Book Value"] = prop.BookValue;
                    row["Remaining Life"] = prop.RemainingLife;
                    dt.Rows.Add(row); // datatable row has been created here 
                }
            }

            if (reporttyp == 5 || reporttyp == 6) //Session["CurrentReport"] will have current report type 1 for Machine
            {
                int ESAtype = 0;
                if (reporttyp == 5)
                {
                    excelReportName = "Process Capability Scorecard";
                    ESAtype = Convert.ToInt32(FormType.PPESA);
                }
                else
                {
                    excelReportName = "Design Capability Scorecard";
                    ESAtype = Convert.ToInt32(FormType.PDESA);
                }

                List<PPESAnPDESA.ListPPESAnPDESAData> ESAProcessData = new List<PPESAnPDESA.ListPPESAnPDESAData>();
                TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
                List<int> obj = ProcessData.GetProcessObjActvities_ForPESAandDESA(Convert.ToInt32(mastertreeview.SelectedNode.Value));
                // var getRecord;
                if (obj != null)
                {
                    activity = (List<int>)obj;
                    // for multiple activities

                    for (int i = 0; i < activity.Count; i++)
                    {
                        int prcobjID = Convert.ToInt32(activity[i].ToString()); //proobjId is selected atctivity id
                        ViewState["sortBy"] = "FormID";
                        ViewState["isAsc"] = "1";
                        ESAProcessData.AddRange(PPESAnPDESA.GetPPESAnPDESADataByPobjID(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), ESAtype, prcobjID));
                    }
                }

                foreach (TableCell cell in grdESAReport.HeaderRow.Cells)
                {
                    dt.Columns.Add(cell.Text);
                }
                int count = 0;
                foreach (PPESAnPDESA.ListPPESAnPDESAData prop in ESAProcessData)
                {
                    DataRow row = dt.NewRow();
                    count += 1;
                    row["S No."] = count;
                    row["Process ObjectName"] = prop.ProcessObjectName;
                    row["Product Feature Added"] = prop.ProductFeatureAdded;
                    row["Function of Product Feature"] = prop.FunctionofProductFeature;
                    row["Error Event"] = prop.ErrorEvent;
                    row["Error Event Transfer"] = prop.ErrorEventTransferFunction;
                    row["Actions"] = prop.Actions;
                    row["Countermeasure"] = prop.Countermeasure;
                    row["Countermeasure Effectiveness"] = prop.CountermeasureEffectiveness;
                    row["CPK"] = prop.Cpk;
                    row["CP"] = prop.Cp;
                    row["PPK"] = prop.Ppk;
                    row["Long term Sigma"] = prop.LongtermSigma;
                    row["Short-term Sigma"] = prop.ShorttermSigma;
                    row["Z-Score"] = prop.ZScore;
                    dt.Rows.Add(row); // datatable row has been created here 
                }
            }
            if (reporttyp == 7) //Session["CurrentReport"] will have current report type 1 for Machine
            {
                //int ESAtype = 0;

                excelReportName = "Target Value Gap";
                //  ESAtype = Convert.ToInt32(FormType.PPESA);



                List<SummaryDetail> ESAProcessData = new List<SummaryDetail>();
                // var getRecord;
                //if (Session["Activity"] != null)
                //{
                //    activity = (List<int>)Session["Activity"];
                //    // for multiple activities

                //    for (int i = 0; i < activity.Count; i++)
                //    {
                //        int prcobjID = Convert.ToInt32(activity[i].ToString()); //proobjId is selected atctivity id
                ViewState["sortBy"] = "CreatedDate";
                ViewState["isAsc"] = "1";
                //        ESAProcessData.AddRange(LoadTgtValueGapData());
                //    }
                //}
                ESAProcessData.AddRange(LoadTgtValueGapData());
                dt.Columns.Add("S No.");
                dt.Columns.Add("Attribute");
                dt.Columns.Add("Value");
                dt.Columns.Add("Unit");
                dt.Columns.Add("Target Value");
                dt.Columns.Add("Target Unit");
                dt.Columns.Add("Difference Value");
                //foreach (TableCell cell in gridTgtValueGap.HeaderRow.Cells)
                //{
                //    dt.Columns.Add(cell.Text);
                //}
                int count = 0;
                foreach (SummaryDetail prop in ESAProcessData)
                {
                    DataRow row = dt.NewRow();
                    count += 1;
                    row["S No."] = count;
                    row["Attribute"] = prop.AttributeName;
                    row["Value"] = prop.AttributeValueResult;
                    row["Unit"] = prop.UnitName;
                    row["Target Value"] = prop.TargetValue;
                    row["Target Unit"] = prop.TargetUnitName;
                    row["Difference Value"] = prop.DifferenceValue;
                    dt.Rows.Add(row); // datatable row has been created here 
                }
            }
            if (reporttyp == 8)
            {
                if (txtInventoryAttributeReportName.Text != "")
                {
                    excelReportName = txtInventoryAttributeReportName.Text;
                }
                else
                {
                    excelReportName = "Inventory Report";
                }
                List<InventoryReportFields> objInventoryReportFields = new List<InventoryReportFields>();


                bool IsReportExist = Activity.CheckIfReportExist(excelReportName);
                if (IsReportExist)
                {
                    attribute.Clear();
                    attribute = Activity.FetchAttributelistForExport(excelReportName);
                    dt.Columns.Add("Inventory");

                    foreach (var row in attribute)
                    {
                        if (row.Contains(','))
                        {
                            string[] words;
                            words = row.Split(new string[] { "," }, StringSplitOptions.None);
                            foreach (var obj in words)
                            {
                                if (obj != "")
                                {
                                    dt.Columns.Add(obj);
                                }
                            }
                        }
                        else
                        {
                            string AttrName = row.EndsWith(",") ? row.Substring(0, row.Length - 1) : row;
                            dt.Columns.Add(AttrName.Trim());
                        }
                    }
                }
                else
                {
                    dt.Columns.Add("Inventory");
                    foreach (var SelectedAttrName in attribute)
                    {

                        dt.Columns.Add(SelectedAttrName.Trim());

                    }
                }


                objInventoryReportFields.AddRange(GetInventoryAttributeReportDataForExport(attribute));


                foreach (InventoryReportFields prop in objInventoryReportFields)
                {
                    DataRow row = dt.NewRow();

                    if (prop.Inventory != "")
                        row["Inventory"] = prop.Inventory;

                    if (prop.CT != "")
                        row["CT"] = prop.CT;

                    if (prop.Time != "")
                        row["Time"] = prop.Time;

                    if (prop.Dollar != "")
                        row["$"] = prop.Dollar;

                    dt.Rows.Add(row); // datatable row has been created here 
                }
            }

            if (reporttyp == 9)
            {
                if (txtErrorAttributeReportName.Text != "")
                {
                    excelReportName = txtErrorAttributeReportName.Text;
                }
                else
                {
                    excelReportName = "Error Report";
                }
                List<ErrorReportFields> objErrorReportFields = new List<ErrorReportFields>();

                bool IsReportExist = Activity.CheckIfReportExist(excelReportName);
                if (IsReportExist)
                {
                    attribute.Clear();
                    attribute = Activity.FetchAttributelistForExport(excelReportName);
                    dt.Columns.Add("ErrorName");

                    foreach (var row in attribute)
                    {
                        if (row.Contains(','))
                        {
                            string[] words;
                            words = row.Split(new string[] { "," }, StringSplitOptions.None);
                            foreach (var SelectedAttrName in words)
                            {
                                if (SelectedAttrName != "")
                                {
                                    if (SelectedAttrName.Trim() == "CycleTime")
                                    {
                                        dt.Columns.Add("Cycle Time");
                                    }
                                    if (SelectedAttrName.Trim() == "WorkContent")
                                    {
                                        dt.Columns.Add("Work Content");
                                    }
                                    if (SelectedAttrName.Trim() == "CounterMeasure")
                                    {
                                        dt.Columns.Add("Counter Measure");
                                    }
                                    if (SelectedAttrName.Trim() == "CounterMeasureStrength")
                                    {
                                        dt.Columns.Add("Counter Measure Strength");
                                    }
                                }
                            }
                        }
                        else
                        {
                            string SelectedAttrName = row.EndsWith(",") ? row.Substring(0, row.Length - 1) : row;
                            if (SelectedAttrName.Trim() == "CycleTime")
                            {
                                dt.Columns.Add("Cycle Time");
                            }
                            if (SelectedAttrName.Trim() == "WorkContent")
                            {
                                dt.Columns.Add("Work Content");
                            }
                            if (SelectedAttrName.Trim() == "CounterMeasure")
                            {
                                dt.Columns.Add("Counter Measure");
                            }
                            if (SelectedAttrName.Trim() == "CounterMeasureStrength")
                            {
                                dt.Columns.Add("Counter Measure Strength");
                            }
                        }
                    }
                }
                else
                {
                    dt.Columns.Add("ErrorName");
                    foreach (var SelectedAttrName in attribute)
                    {
                        if (SelectedAttrName.Trim() == "CycleTime")
                        {
                            dt.Columns.Add("Cycle Time");
                        }
                        if (SelectedAttrName.Trim() == "WorkContent")
                        {
                            dt.Columns.Add("Work Content");
                        }
                        if (SelectedAttrName.Trim() == "CounterMeasure")
                        {
                            dt.Columns.Add("Counter Measure");
                        }
                        if (SelectedAttrName.Trim() == "CounterMeasureStrength")
                        {
                            dt.Columns.Add("Counter Measure Strength");
                        }
                    }
                }

                objErrorReportFields.AddRange(GetErrorAttributeReportDataForExport(attribute));

                foreach (ErrorReportFields prop in objErrorReportFields)
                {
                    DataRow row = dt.NewRow();

                    if (prop.CounterMeasure != "")
                        row["Counter Measure"] = prop.CounterMeasure;

                    if (prop.CounterMeasureStrength != "")
                        row["Counter Measure Strength"] = prop.CounterMeasureStrength;

                    if (prop.CycleTime != "")
                        row["Cycle Time"] = prop.CycleTime;

                    if (prop.ErrorName != "")
                        row["ErrorName"] = prop.ErrorName;

                    if (prop.WorkContent != "")
                        row["Work Content"] = prop.WorkContent;

                    dt.Rows.Add(row); // datatable row has been created here 
                }
            }


            //"+ excelReportName +" + DateTime.Now.ToString("hh-mm-ss") + ".xlsx"

            using (XLWorkbook wb = new XLWorkbook()) // we will add our datatable in workbook and after we will set it in memory stream to make excel file
            {
                wb.Worksheets.Add(dt); // adding datatable in worknook

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment;filename=Report.xlsx");
                Response.AddHeader("content-disposition", "attachment;filename=" + excelReportName + "-" + DateTime.Now.ToString("hh-mm-ss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream); //save workbook as memorystream
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }

    protected void lnkBtnPPESAReport_Click(object sender, EventArgs e)
    {
        //Session["CurrentReport"] = (int)ReportTypeID.PPESA;
        //this.IsEdit = false;
        //showhidepanel();
        if (PPESAnPDESA.GetIfReportAlreadyExist(ProcessId, (int)ReportTypeID.PCS))  // first check if there PDESA report alredy exist
        {
            Session["CurrentReport"] = (int)ReportTypeID.PCS;
            this.IsEdit = false;
            List<PPESAnPDESA.ListPPESAnPDESAData> ESADataR = new List<PPESAnPDESA.ListPPESAnPDESAData>();
            ViewState["sortBy"] = "Sequence";
            ViewState["isAsc"] = "1";
            int ESAtype = 0;
            int reportid = 0;
            ESAtype = Convert.ToInt32(FormType.PPESA);

            ESADataR.AddRange(PPESAnPDESA.GetPPESAnPDESAReportData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), ESAtype, ProcessId, reportid));

            if (ESADataR.Count > 0)
            {
                grdESAReport.DataSource = ESADataR;
                grdESAReport.DataBind();
                pnlESAReport.Visible = true;
                lnkbtnSaveReport.Visible = true;
                if (RoleID == 4)
                {
                    lnkbtnSaveReport.Visible = false;
                    txtAttributeReportName.Visible = false;
                }
                liSaveReport.Visible = true;
                divESAName.Visible = true;
                txtESAReportName.Text = "";
                lblMsg.Text = "";
                lnkbtnExporttoExcel.Visible = true; // show export to excel button if report grid display
                liExporttoExcel.Visible = true;
            }
            else
            {
                grdESAReport.DataSource = null;
                grdESAReport.DataBind();
                divESAName.Visible = false;
                lnkbtnSaveReport.Visible = false;
                liSaveReport.Visible = false;
                liSaveReport.Visible = false;
                pnlESAReport.Visible = true;
                lblMsg.Text = "";
                lnkbtnExporttoExcel.Visible = false; // hide export to excel button if report grid is null
                liExporttoExcel.Visible = false;
            }
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlMachineReport.Visible = false;
            pnlListSavedReport.Visible = false;
            divErrorMsg.Visible = false;
            txtESAReportName.Text = "Process Capability Scorecard";
            txtESAReportName.Attributes.Add("readonly", "readonly");

        }
        else
        {
            SetErrorMessage();
            lblMsg.Text = "Scorecard already exists.";
        }
    }

    protected void lnkBtnPDESAReport_Click(object sender, EventArgs e)
    {
        //Session["CurrentReport"] = (int)ReportTypeID.PDESA;
        //this.IsEdit = false;
        //showhidepanel();

        if (PPESAnPDESA.GetIfReportAlreadyExist(ProcessId, (int)ReportTypeID.DCS))  // first check if there PDESA report alredy exist
        {
            Session["CurrentReport"] = (int)ReportTypeID.DCS;
            this.IsEdit = false;
            List<PPESAnPDESA.ListPPESAnPDESAData> ESADataR = new List<PPESAnPDESA.ListPPESAnPDESAData>();

            ViewState["sortBy"] = "Sequence";
            ViewState["isAsc"] = "1";


            int ESAtype = 0;
            int reportid = 0;
            ESAtype = Convert.ToInt32(FormType.PDESA);

            ESADataR.AddRange(PPESAnPDESA.GetPPESAnPDESAReportData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), ESAtype, ProcessId, reportid));

            if (ESADataR.Count > 0)
            {
                grdESAReport.DataSource = ESADataR;
                grdESAReport.DataBind();
                pnlESAReport.Visible = true;
                lnkbtnSaveReport.Visible = true;
                if (RoleID == 4)
                {
                    lnkbtnSaveReport.Visible = false;
                    txtAttributeReportName.Visible = false;
                }
                liSaveReport.Visible = true;
                divESAName.Visible = true;
                txtESAReportName.Text = "";
                lblMsg.Text = "";
                lnkbtnExporttoExcel.Visible = true; // show export to excel button if report grid display
                liExporttoExcel.Visible = true;
            }
            else
            {
                grdESAReport.DataSource = null;
                grdESAReport.DataBind();
                divESAName.Visible = false;
                lnkbtnSaveReport.Visible = false;
                liSaveReport.Visible = false;
                liSaveReport.Visible = false;
                pnlESAReport.Visible = true;
                lblMsg.Text = "";
                lnkbtnExporttoExcel.Visible = false; // hide export to excel button if report grid is null
                liExporttoExcel.Visible = false;
            }
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlMachineReport.Visible = false;
            pnlListSavedReport.Visible = false;
            divErrorMsg.Visible = false;
            txtESAReportName.Text = "Design Capability Scorecard";
            txtESAReportName.Attributes.Add("readonly", "readonly");
        }
        else
        {
            SetErrorMessage();
            lblMsg.Text = "Scorecard already exists.";
        }
    }

    private void showhidepanel()
    {
        if (ProcessId != 0) // if process is selected activity block will display
        {
            pnlActivity.Visible = true;
            pnlReportType.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlMachineReport.Visible = false;
            pnlListSavedReport.Visible = false;
            divErrorMsg.Visible = false;

            //lblCurrentReport.Text = "BOM Report";
        }
        else    //else no process selected
        {
            pnlActivity.Visible = false; pnlInventory.Visible = false; pnlCustomStandardReport.Visible = false;
            pnlReportType.Visible = false; pnlEror.Visible = false;
            pnlAttribute.Visible = false;
            pnlBomProcess.Visible = false;
            pnlAttributeReport.Visible = false;
            pnlBomReport.Visible = false;
            pnlTFGReport.Visible = false;
            pnlMachineReport.Visible = false;
            pnlListSavedReport.Visible = false;
            lblMsg.Visible = true;
            divErrorMsg.Visible = true;
            lblMsg.Text = "Please choose the process.!";
            lblMsg.Style.Add("color", "red");
            divErrorMsg.Style.Add("min-width", "194px");
            divErrorMsg.Style.Add("margin-left", "554px");
            divErrorMsg.Attributes.Add("class", "isa_warning");
        }
    }



    protected void btnTgtValueGap_Click(object sender, EventArgs e)
    {

        // if (PPESAnPDESA.GetIfReportAlreadyExist(ProcessId, (int)ReportTypeID.DCS))  // first check if there PDESA report alredy exist
        //{
        Session["CurrentReport"] = (int)ReportTypeID.TGTGAP;
        this.IsEdit = false;
        List<PPESAnPDESA.ListPPESAnPDESAData> ESADataR = new List<PPESAnPDESA.ListPPESAnPDESAData>();

        ViewState["sortBy"] = "CreatedDate";
        ViewState["isAsc"] = "1";


        int ESAtype = 0;
        int reportid = 0;
        ESAtype = Convert.ToInt32(FormType.PDESA);

        // ESADataR.AddRange(PPESAnPDESA.GetPPESAnPDESAReportData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), ESAtype, ProcessId, reportid));

        //if (ESADataR.Count > 0)
        //{
        gridTgtValueGap.DataSource = LoadTgtValueGapData();
        gridTgtValueGap.DataBind();

        pnlTgtValueGap.Visible = true;
        lnkbtnSaveReport.Visible = true;
        if (RoleID == 4)
        {
            lnkbtnSaveReport.Visible = false;
            txtAttributeReportName.Visible = false;
        }
        liSaveReport.Visible = true;
        //divESAName.Visible = true;
        //txtESAReportName.Text = "";
        lblMsg.Text = "";
        lnkbtnExporttoExcel.Visible = true; // show export to excel button if report grid display
        liExporttoExcel.Visible = true;
        //}
        //else
        //{
        //    grdESAReport.DataSource = null;
        //    grdESAReport.DataBind();
        //    divESAName.Visible = false;
        //    lnkbtnSaveReport.Visible = false;
        //    liSaveReport.Visible = false;
        //    liSaveReport.Visible = false;
        //    pnlESAReport.Visible = true;
        //    lblMsg.Text = "";
        //    lnkbtnExporttoExcel.Visible = false; // hide export to excel button if report grid is null
        //    liExporttoExcel.Visible = false;
        //}
        pnlReportType.Visible = false;
        pnlAttribute.Visible = false;
        pnlBomProcess.Visible = false;
        pnlAttributeReport.Visible = false;
        pnlBomReport.Visible = false;
        pnlTFGReport.Visible = false;
        pnlMachineReport.Visible = false;
        pnlListSavedReport.Visible = false;
        divErrorMsg.Visible = false;
        // txtTVGReportName.Text = "Target Vale Gap";
        // txtTVGReportName.Attributes.Add("readonly", "readonly");
        //}
        //else
        //{
        //    SetErrorMessage();
        //    lblMsg.Text = "Scorecard already exists.";
        //}

    }

    List<SummaryDetail> summaryResult = new List<SummaryDetail>();

    public List<SummaryDetail> LoadTgtValueGapData()
    {

        if (ProcessData.GetSummaryData(ProcessId))
        {
            List<ProcessData.ProcessDataProperty> record = ProcessData.GetSummaryTableRecordForTargetValueGapReport(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ProcessId));
            if (record.Count != 0)
            {
                for (int k = 0; k < record.Count; k++)
                {
                    string AttributeName = Convert.ToString(record[k].AttributeName);
                    // string unitName = ProcessData.GetUnitName(AttributeName);
                    string unitName = Convert.ToString(record[k].UnitName);
                    int FunctionID = Convert.ToInt32(record[k].FunctionID);
                    List<ProcessData.ProcessDataProperty> res = ProcessData.GetProcessAttributeValue(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ProcessId), AttributeName, unitName);


                    if (res.Count > 0)
                    {
                        List<ProcessData.ProcessDataProperty> processData = res.Where(p => p.SourceType.GetValueOrDefault() == 1).ToList();
                        List<ProcessData.ProcessDataProperty> targetData = res.Where(p => p.SourceType.GetValueOrDefault() == 2).ToList();

                        if (FunctionID == 1)
                        {
                            int sum = processData.Sum(x => Convert.ToInt32(x.AttributeValueSum)); // get Sum here
                                                                                                  // add attributename,value, unitname in list to display it in summary table
                            int targetSum = targetData.Sum(x => Convert.ToInt32(x.AttributeValueSum));
                            summaryResult.Add(new SummaryDetail()
                            {
                                AttributeName = AttributeName,
                                AttributeValueResult = Convert.ToString(sum),
                                UnitName = unitName,
                                TargetValue = Convert.ToString(targetSum),
                                TargetUnitName = unitName,
                                DifferenceValue = Convert.ToString(sum - targetSum)
                            });
                        }

                        if (FunctionID == 2)
                        {
                            double average = processData.Average(x => Convert.ToInt32(x.AttributeValueSum)); // get Average here
                            double targetAverage = targetData.Average(x => Convert.ToInt32(x.AttributeValueSum));
                            summaryResult.Add(new SummaryDetail()
                            {
                                AttributeName = AttributeName,
                                AttributeValueResult = String.Format("{0:0.00}", average),
                                UnitName = unitName,
                                TargetValue = String.Format("{0:0.00}", targetAverage),
                                TargetUnitName = unitName,
                                DifferenceValue = String.Format("{0:0.00}", average - targetAverage)
                            });
                        }

                        if (FunctionID == 3)
                        {
                            /****************Median formula ******************/
                            int[] numbers = new int[res.Count]; // initialize int array with max record in list
                            for (int j = 0; j < res.Count; j++)
                            {
                                numbers[j] = res[j].AttributeValueSum; // make int[] array for all value in list
                            }

                            // int[] numbers = { 13, 18, 13, 14, 13, 16, 14, 21, 13 }; // median will be 14
                            // int[] numbers = { 6, 6, 8, 7 }; //median will be 3 

                            int numberCount = numbers.Count(); // count no of values from array
                            int halfIndex = numbers.Count() / 2; // half of count
                                                                 // var sortedNumbers = numbers.OrderBy(n => n); // sorted numbers
                            var sortedNumbers = numbers.OrderBy(c => c).ToArray();
                            double median;
                            if ((numberCount % 2) == 0)
                            {
                                int ElementFirst = sortedNumbers.ElementAt(halfIndex - 1);
                                int ElementSecond = sortedNumbers.ElementAt(halfIndex);
                                int addElemtn = ElementFirst + ElementSecond;
                                median = (double)addElemtn / 2;
                            }
                            else
                            {
                                median = sortedNumbers.ElementAt(halfIndex);
                            }
                            //int total = Convert.ToInt32(median); // get median here
                            // add attributename,value, unitname in list to display it in summary table
                            summaryResult.Add(new SummaryDetail()
                            {
                                AttributeName = AttributeName,
                                AttributeValueResult = String.Format("{0:0.00}", median),
                                UnitName = unitName,
                                TargetValue = String.Format("{0:0.00}", median),
                                TargetUnitName = unitName,
                                DifferenceValue = String.Format("{0:0.00}", median - median)
                            });
                        }

                        if (FunctionID == 4)
                        {
                            int total = processData.Min(x => Convert.ToInt32(x.AttributeValueSum)); // get min here
                            int targetTotal = targetData.Min(x => Convert.ToInt32(x.AttributeValueSum));
                            summaryResult.Add(new SummaryDetail()
                            {
                                AttributeName = AttributeName,
                                AttributeValueResult = Convert.ToString(total),
                                UnitName = unitName,
                                TargetValue = Convert.ToString(targetTotal),
                                TargetUnitName = unitName,
                                DifferenceValue = Convert.ToString(total - targetTotal)
                            });
                        }

                        if (FunctionID == 5)
                        {
                            int total = processData.Max(x => Convert.ToInt32(x.AttributeValueSum)); // get max here
                            int targetTotal = targetData.Max(x => Convert.ToInt32(x.AttributeValueSum));
                            summaryResult.Add(new SummaryDetail()
                            {
                                AttributeName = AttributeName,
                                AttributeValueResult = Convert.ToString(total),
                                UnitName = unitName,
                                TargetValue = Convert.ToString(targetTotal),
                                TargetUnitName = unitName,
                                DifferenceValue = Convert.ToString(total - targetTotal)
                            });
                        }

                        if (FunctionID == 6)
                        {

                            //***************************standard deviation formula******************/

                            int[] numA = new int[res.Count]; // int[] array get all values from list in int[] array
                            for (int j = 0; j < res.Count; j++)
                            {
                                numA[j] = res[j].AttributeValueSum;
                            }
                            //int[] numA = { 9, 2, 5, 4, 12, 7, 8, 11, 9, 3, 7, 4, 12, 5, 4, 10, 9, 6, 9, 4 }; //example to test math function 
                            double ret = 0;
                            int count = numA.Count();
                            if (count > 0)
                            {
                                double avg = numA.Average(); // avg will get avg number of array 
                                double sum = numA.Sum(d => (d - avg) * (d - avg)); // sum of (x(i)-avg) sq root 
                                ret = Math.Sqrt(sum / count); // ret is result for standard deviation formula
                                                              //int total = Convert.ToInt32(ret);
                                                              // add attributename,value, unitname in list to display it in summary table
                                summaryResult.Add(new SummaryDetail()
                                {
                                    AttributeName = AttributeName,
                                    AttributeValueResult = String.Format("{0:0.00}", ret),
                                    UnitName = unitName,
                                    TargetValue = String.Format("{0:0.00}", ret),
                                    TargetUnitName = unitName,
                                    DifferenceValue = String.Format("{0:0.00}", ret - ret)
                                });
                            }
                        }

                        if (FunctionID == 0)
                        {
                            int total = 0;
                            int targetTotal = 0;

                            if (processData.Count == 0)
                            {
                                total = 0;
                            }
                            else
                            {
                                total = processData.Max(x => Convert.ToInt32(x.AttributeValueSum));
                            }

                            if (targetData.Count == 0)
                            {
                                targetTotal = 0;
                            }
                            else
                            {
                                targetTotal = targetData.Max(x => Convert.ToInt32(x.AttributeValueSum));
                            }

                            summaryResult.Add(new SummaryDetail()
                            {
                                AttributeName = AttributeName,
                                AttributeValueResult = Convert.ToString(total),
                                UnitName = unitName,
                                TargetValue = Convert.ToString(targetTotal),
                                TargetUnitName = unitName,
                                DifferenceValue = Convert.ToString(total - targetTotal)
                            });
                        }
                    }
                }
            }
        }
        return summaryResult;
    }
    List<InventoryReportFields> objInventoryReportFields = new List<InventoryReportFields>();
    public void GetInventoryAttributeReportData(List<string> ObjAttribute)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();

        string attributeName = "";
        // var getRecord;
        if (Session["InventoryDictionary"] != null)
        {
            activityDic = (Dictionary<int, string>)Session["InventoryDictionary"];
            // for multiple activities

            string middle = string.Empty;

            for (int i = 0; i < activityDic.Count; i++)
            {
                int actID = Convert.ToInt32(activityDic.ElementAt(i).Key);

                string str = Convert.ToString(activityDic.ElementAt(i).Value);

                if (i == 0)
                {

                    middle = "'" + actID.ToString() + "',";
                }
                else
                {
                    middle += "'" + actID.ToString() + "',";
                }
            }
            middle = middle.TrimEnd(',');
            string ColumnName = string.Empty;
            foreach (var objAttr in ObjAttribute)
            {
                if (objAttr == "$")
                {
                    ColumnName += "IT.Doller,";
                }
                else
                {
                    ColumnName += "IT." + objAttr + ",";
                }
            }
            ColumnName = ColumnName.TrimEnd(',');
            string query = "SELECT PO.ProcessObjID,PO.ProcessObjID,ProcessObjName as Inventory," + ColumnName + " " +
                           " FROM tbl_ProcessObject PO " +
                           " LEFT JOIN [dbo].[tbl_InvantoryTriangle] " +
                           " IT ON PO.ProcessObjID = IT.ProcessObjID " +
                           " WHERE PO.ProcessObjID IN (" + middle + ") " +
                           " ORDER BY PO.ProcessObjName";

            // Declare the query string.
            // Run the query and bind the resulting DataSet
            // to the GridView control.
            DataSet ds = GetData(query);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            DataColumnCollection columns = dt.Columns;


            objInventoryReportFields = (from DataRow dr in dt.Rows
                                        select new InventoryReportFields()
                                        {
                                            Inventory = dr["Inventory"].ToString(),
                                            CT = (!columns.Contains("CT")) ? "" : dr["CT"].ToString(),
                                            Time = (!columns.Contains("Time")) ? "" : dr["Time"].ToString(),
                                            Dollar = (!columns.Contains("Doller")) ? "" : dr["Doller"].ToString()
                                        }).ToList();
            foreach (var item in objInventoryReportFields)
            {
                if (item.CT == "")
                {
                    GridInventoryReport.Columns[1].Visible = false;
                }
                else
                {
                    GridInventoryReport.Columns[1].Visible = true;
                }

                if (item.Time == "")
                {
                    GridInventoryReport.Columns[2].Visible = false;
                }
                else
                {
                    GridInventoryReport.Columns[2].Visible = true;
                }

                if (item.Dollar == "")
                {
                    GridInventoryReport.Columns[3].Visible = false;
                }
                else
                {
                    GridInventoryReport.Columns[3].Visible = true;
                }
            }


            if (objInventoryReportFields.Count > 0)
            {
                GridInventoryReport.DataSource = objInventoryReportFields;
                GridInventoryReport.DataBind();

                pnlListSavedReport.Visible = false;
                divErrorMsg.Visible = true;
                lnkbtnSaveReport.Visible = true;
                if (RoleID == 4)
                {
                    lnkbtnSaveReport.Visible = false;
                    txtAttributeReportName.Visible = false;
                }
                pnlInventoryReport.Visible = true;
                lnkbtnExporttoExcel.Visible = true;
                liSaveReport.Visible = false;
                liExporttoExcel.Visible = true;
                pnlInventoryAttribute.Visible = false;
            }
            else
            {
                lnkbtnExporttoExcel.Visible = false;
                liExporttoExcel.Visible = false;
                //Message.Text = "Unable to connect to the database.";
            }
        }
    }


    List<ErrorReportFields> objErrorReportFields = new List<ErrorReportFields>();
    public void GetErrorAttributeReportData(List<string> ObjAttribute)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();

        string attributeName = "";
        // var getRecord;
        if (Session["ErrorDictionary"] != null)
        {
            activityDic = (Dictionary<int, string>)Session["ErrorDictionary"];
            // for multiple activities

            string middle = string.Empty;

            for (int i = 0; i < activityDic.Count; i++)
            {
                int actID = Convert.ToInt32(activityDic.ElementAt(i).Key);

                string str = Convert.ToString(activityDic.ElementAt(i).Value);

                if (i == 0)
                {

                    middle = "'" + actID.ToString() + "',";
                }
                else
                {
                    middle += "'" + actID.ToString() + "',";
                }
            }
            middle = middle.TrimEnd(',');
            string ColumnName = string.Empty;
            foreach (var objAttr in ObjAttribute)
            {

                ColumnName += "EI." + objAttr + ",";

            }
            ColumnName = ColumnName.TrimEnd(',');
            string query = "SELECT PO.ProcessObjID, PO.ProcessObjID, Error as ErrorName," + ColumnName + " " +
                           " FROM tbl_ProcessObject PO  LEFT JOIN ErrorInfos EI ON PO.ProcessObjID = EI.ProcessID" +
                           " WHERE PO.ProcessObjID IN (" + middle + ") " +
                           " and Error <>''  ORDER BY PO.ProcessObjName";

            // Declare the query string.
            // Run the query and bind the resulting DataSet
            // to the GridView control.
            DataSet ds = GetData(query);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            DataColumnCollection columns = dt.Columns;


            objErrorReportFields = (from DataRow dr in dt.Rows
                                    select new ErrorReportFields()
                                    {
                                        ErrorName = dr["ErrorName"].ToString(),
                                        CounterMeasureStrength = (!columns.Contains("CounterMeasureStrength")) ? "" : dr["CounterMeasureStrength"].ToString(),
                                        CycleTime = (!columns.Contains("CycleTime")) ? "" : dr["CycleTime"].ToString(),
                                        CounterMeasure = (!columns.Contains("CounterMeasure")) ? "" : dr["CounterMeasure"].ToString(),
                                        WorkContent = (!columns.Contains("WorkContent")) ? "" : dr["WorkContent"].ToString(),
                                    }).ToList();
            foreach (var item in objErrorReportFields)
            {
                if (item.WorkContent == "")
                {
                    GridErrorReport.Columns[2].Visible = false;
                }
                else
                {
                    GridErrorReport.Columns[2].Visible = true;
                }

                if (item.CycleTime == "")
                {
                    GridErrorReport.Columns[1].Visible = false;
                }
                else
                {
                    GridErrorReport.Columns[1].Visible = true;
                }

                if (item.CounterMeasureStrength == "")
                {
                    GridErrorReport.Columns[4].Visible = false;
                }
                else
                {
                    GridErrorReport.Columns[4].Visible = true;
                }

                if (item.CounterMeasure == "")
                {
                    GridErrorReport.Columns[3].Visible = false;
                }
                else
                {
                    GridErrorReport.Columns[3].Visible = true;
                }
            }


            if (objErrorReportFields.Count > 0)
            {
                GridErrorReport.DataSource = objErrorReportFields;
                GridErrorReport.DataBind();

                pnlListSavedReport.Visible = false;
                divErrorMsg.Visible = true;
                lnkbtnSaveReport.Visible = true;
                if (RoleID == 4)
                {
                    lnkbtnSaveReport.Visible = false;
                    txtAttributeReportName.Visible = false;
                }
                pnlInventoryReport.Visible = false;
                pnlErrorAttribute.Visible = false;
                lnkbtnExporttoExcel.Visible = true;
                liSaveReport.Visible = false;
                liExporttoExcel.Visible = true;
                pnlInventoryAttribute.Visible = false;
                pnlErrorReport.Visible = true;
            }
            else
            {
                lnkbtnExporttoExcel.Visible = false;
                liExporttoExcel.Visible = false;
                divErrorMsg.Visible = true;
                lblMsg.Text = "No error found for this process..!";
                lblMsg.Style.Add("color", "red");
                divErrorMsg.Style.Add("min-width", "194px");
                divErrorMsg.Style.Add("margin-left", "500px");
                divErrorMsg.Attributes.Add("class", "isa_warning");
            }
        }
    }


    public List<InventoryReportFields> GetInventoryAttributeReportDataForExport(List<string> ObjAttribute)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();

        string attributeName = "";
        // var getRecord;
        if (Session["InventoryDictionary"] != null)
        {
            activityDic = (Dictionary<int, string>)Session["InventoryDictionary"];
            // for multiple activities

            string middle = string.Empty;

            for (int i = 0; i < activityDic.Count; i++)
            {
                int actID = Convert.ToInt32(activityDic.ElementAt(i).Key);

                string str = Convert.ToString(activityDic.ElementAt(i).Value);

                if (i == 0)
                {

                    middle = "'" + actID.ToString() + "',";
                }
                else
                {
                    middle += "'" + actID.ToString() + "',";
                }
            }
            middle = middle.TrimEnd(',');
            string ColumnName = string.Empty;


            foreach (var objAttr in ObjAttribute)
            {
                if (objAttr.Contains(','))
                {
                    string[] words;
                    words = objAttr.Split(new string[] { "," }, StringSplitOptions.None);
                    foreach (var obj in words)
                    {
                        if (obj != "")
                        {
                            if (obj == "$")
                            {
                                ColumnName += "IT.Doller,";
                            }
                            else
                            {
                                ColumnName += "IT." + obj + ",";
                            }
                        }
                    }
                }
                else
                {


                    if (objAttr == "$")
                    {
                        ColumnName += "IT.Doller,";
                    }
                    else
                    {
                        ColumnName += "IT." + objAttr + ",";
                    }
                }
            }
            ColumnName = ColumnName.TrimEnd(',');
            string query = "SELECT PO.ProcessObjID,PO.ProcessObjID,ProcessObjName as Inventory," + ColumnName + " " +
                           " FROM tbl_ProcessObject PO " +
                           " LEFT JOIN[dbo].[tbl_InvantoryTriangle] " +
                           " IT ON PO.ProcessObjID = IT.ProcessObjID " +
                           " WHERE PO.ProcessObjID IN (" + middle + ") " +
                           " ORDER BY PO.ProcessObjName";

            // Declare the query string.
            // Run the query and bind the resulting DataSet
            // to the GridView control.
            DataSet ds = GetData(query);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            DataColumnCollection columns = dt.Columns;


            objInventoryReportFields = (from DataRow dr in dt.Rows
                                        select new InventoryReportFields()
                                        {
                                            Inventory = dr["Inventory"].ToString(),
                                            CT = (!columns.Contains("CT")) ? "" : dr["CT"].ToString(),
                                            Time = (!columns.Contains("Time")) ? "" : dr["Time"].ToString(),
                                            Dollar = (!columns.Contains("Doller")) ? "" : dr["Doller"].ToString()
                                        }).ToList();




        }
        return objInventoryReportFields;
    }


    public List<ErrorReportFields> GetErrorAttributeReportDataForExport(List<string> ObjAttribute)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();

        string attributeName = "";
        // var getRecord;
        if (Session["ErrorDictionary"] != null)
        {
            activityDic = (Dictionary<int, string>)Session["ErrorDictionary"];
            // for multiple activities

            string middle = string.Empty;

            for (int i = 0; i < activityDic.Count; i++)
            {
                int actID = Convert.ToInt32(activityDic.ElementAt(i).Key);

                string str = Convert.ToString(activityDic.ElementAt(i).Value);

                if (i == 0)
                {

                    middle = "'" + actID.ToString() + "',";
                }
                else
                {
                    middle += "'" + actID.ToString() + "',";
                }
            }
            middle = middle.TrimEnd(',');
            string ColumnName = string.Empty;
            foreach (var objAttr in ObjAttribute)
            {
                if (objAttr != "S No.")
                {
                    ColumnName += "EI." + Regex.Replace(objAttr, @"\s", "") + ",";

                }

            }
            ColumnName = ColumnName.TrimEnd(',');
            string query = "SELECT PO.ProcessObjID, PO.ProcessObjID, Error as ErrorName," + ColumnName + " " +
                           " FROM tbl_ProcessObject PO  LEFT JOIN ErrorInfos EI ON PO.ProcessObjID = EI.ProcessID" +
                           " WHERE PO.ProcessObjID IN (" + middle + ") " +
                           " and Error <>''  ORDER BY PO.ProcessObjName";

            // Declare the query string.
            // Run the query and bind the resulting DataSet
            // to the GridView control.
            DataSet ds = GetData(query);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            DataColumnCollection columns = dt.Columns;


            objErrorReportFields = (from DataRow dr in dt.Rows
                                    select new ErrorReportFields()
                                    {
                                        ErrorName = dr["ErrorName"].ToString(),
                                        CounterMeasureStrength = (!columns.Contains("CounterMeasureStrength")) ? "" : dr["CounterMeasureStrength"].ToString(),
                                        CycleTime = (!columns.Contains("CycleTime")) ? "" : dr["CycleTime"].ToString(),
                                        CounterMeasure = (!columns.Contains("CounterMeasure")) ? "" : dr["CounterMeasure"].ToString(),
                                        WorkContent = (!columns.Contains("WorkContent")) ? "" : dr["WorkContent"].ToString(),
                                    }).ToList();




        }
        return objErrorReportFields;
    }

    public class SummaryDetail
    {
        public string AttributeName { get; set; }
        // public int AttributeValueResult { get; set; }
        public string AttributeValueResult { get; set; }
        public string UnitName { get; set; }
        public string TargetValue { get; set; }
        public string TargetUnitName { get; set; }

        public string DifferenceValue { get; set; }

    }



    public class InventoryReportFields
    {
        public string Inventory { get; set; }
        public string Dollar { get; set; }
        public string CT { get; set; }
        public string Time { get; set; }
    }

    public class ErrorReportFields
    {
        public string ErrorName { get; set; }
        public string CycleTime { get; set; }
        public string WorkContent { get; set; }
        public string CounterMeasure { get; set; }
        public string CounterMeasureStrength { get; set; }
    }
}