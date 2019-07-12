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
public partial class ProcessManager : System.Web.UI.Page
{
    #region
    List<UserControl> lst = new List<UserControl>();
    List<UserControl> Plst = new List<UserControl>(); /////////////////
    int ProcessId = 0;
    int typeId = 0;
    #endregion 
    List<string> cyclePath = new List<string>();
    List<int> cyclePathTime = new List<int>();
    string str = string.Empty;
    string str1 = string.Empty;
    string toPoid = string.Empty;

    string str2 = string.Empty;
    int Top = 50;
    int Left = 50;
    string SelectedType = string.Empty;
    //ArrayList CtrlList = new ArrayList();
    List<Supplier> newList = new List<Supplier>();
    List<SummaryDetail> summaryResult = new List<SummaryDetail>(); // it will contain summary data for different functionid

    delegate void DelMethodWithParam(string strProcessObjectId, string strAction);
    delegate void DelMethodWithoutParam();

    DelMethodWithParam delParam;


    List<TopHeightWidth> maxHeightnWidth = new List<TopHeightWidth>(); // it will contains max height and width for any process
    int RoleID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["RoleID"] != null)
        {
            RoleID = Convert.ToInt32(Session["RoleID"].ToString());
        }

        if (RoleID == 4)
        {
            liDesignViewBtn.Visible = false;
        }


        divErrorMsg.Visible = false;
        Label lblManager = (Label)Master.FindControl("lblManager");
        lblManager.Text = "Process View";
        lblManager.Attributes.Add("class", "Process");

        string script = "test();";
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript", script, true);


        // this.RegisterRequiresRaiseEvent(lnkBtnInventory12);
        delParam = new DelMethodWithParam(MethodWithParam);
        //Set method reference to a user control delegate

        //string script = "var contentHeight = $(window).height();var newHeight = contentHeight - $('#header').height() - $('#footer').height() - $('#Title').height() - $('#Bpmn').height() - 73; // + 'px';alert(newHeight);";
        //ScriptManager.RegisterStartupScript(this, this.GetType(),
        //              "ServerControlScript", script, true);
        liProcess.Visible = false;
        liInventory.Visible = false;

        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        else if (Session["SelectedNodeValue"] != null)
        {
            ProcessId = this.CInt32(Session["SelectedNodeValue"]);

            TreeNode strNode = mastertreeview.FindNode(Convert.ToString(Session["SelectedNodeValue"]));
            if (strNode != null)
                strNode.Select();
        }



        //if (this.Page.Request.Params["__EVENTTARGET"] != null && this.Page.Request.Params["__EVENTTARGET"].Contains("Del-C"))
        //{
        //    DeleteControl();            
        //}

        //if (this.Page.Request.Params["__EVENTTARGET"] != null && this.Page.Request.Params["__EVENTTARGET"].Contains("DeleteInventory"))
        //{
        //    DeleteInventory();
        //}

        //if (this.Page.Request.Params["__EVENTTARGET"] != null && this.Page.Request.Params["__EVENTTARGET"].Contains("DeleteProcess"))
        //{
        //    DeleteProcess(ProcessId);
        //}

        if (ProcessId != 0)
        {
            //liZoomIn.Visible = true;
            //liZoomOut.Visible = true;
            liSummary.Visible = true;

            if (RoleID == 4)
            {
                liSummary.Visible = false;
            }

            string divId = "";
            StringBuilder sb2 = new StringBuilder();
            string str2 = string.Empty;
            string str11 = string.Empty;
            int sV = 0;
            if (ViewState["SelectedValue"] != null)
                sV = Convert.ToInt32(ViewState["SelectedValue"]);

            var listData = ControlsData.GetAllProcessControlData(ProcessId); // get all controls record from database

            if (listData.Count != 0)
            {
                for (int i = 0; i < listData.Count; i++)
                {
                    int ParallelProcessObjID = 0;////////////////////
                    int top = 0; int left = 0; int width = 50; int height = 50; string title = "";

                    title = Convert.ToString(listData[i].Title);
                    int type = Convert.ToInt32(listData[i].Type.ToString()); // get control type
                    int DBID = Convert.ToInt32(listData[i].ProcessObjID.ToString()); // get control primary key id
                    if (listData[i].ParallelProcessObjID != null)
                        ParallelProcessObjID = Convert.ToInt32(listData[i].ParallelProcessObjID.ToString());  // get control parallel id
                    divId = GetDivid(type, DBID, ProcessId, ParallelProcessObjID);
                    top = listData[i].XTop != null ? Convert.ToInt32(listData[i].XTop) : 0;  // get top position
                    left = listData[i].YLeft != null ? Convert.ToInt32(listData[i].YLeft) : 0; // get left position
                    width = listData[i].Width != null ? Convert.ToInt32(listData[i].Width) : 0; // get width
                    height = listData[i].Height != null ? Convert.ToInt32(listData[i].Height) : 0; // get height 
                    ControlPosition(divId, DBID, top, left, width, height, i, title, type, ParallelProcessObjID); // call function to creae control on last position

                }
                ViewState["SelectedValue"] = ProcessId;


                liZoomIn.Visible = true;
                liZoomOut.Visible = true;
                li2.Visible = true;

                //if (RoleID == 4)
                //{
                //    liZoomIn.Visible = false;
                //    liZoomOut.Visible = false;
                //    li2.Visible = false;
                //}
            }
            else
            {
                // Bpmn.Visible = true;
                int selectedvalue = 0;
                if (ViewState["SelectedTreeValue"] != null)
                {
                    selectedvalue = Convert.ToInt32(ViewState["SelectedTreeValue"]);
                }
                else
                {
                    selectedvalue = 0;
                }

                if (ProcessId == selectedvalue)
                {
                    // do nothing
                }
                else
                {
                    ViewState["SelectedTreeValue"] = ProcessId;
                    //load(processDivId, InsertedID, Top, Left);
                    //newList.Add(new Supplier() { SupplierID = "divProcess" + InsertedID, EditID = InsertedID, Top = 50, Left = 50, Type = 28, Title = "" });
                    //Session["Supplier"] = newList;
                    //hdnSupplier.Value = "divProcess" + InsertedID + "@" + "50~50,";

                    ViewState["SelectedValue"] = ProcessId;
                }
                //liZoomIn.Visible = false;
                //liZoomOut.Visible = false;

                liZoomIn.Visible = false;
                liZoomOut.Visible = false;
                li2.Visible = false;
                liSummary.Visible = false;
            }


            typeId = TypeData.GetTypeID(ProcessId); // get tree node type here 

            if (typeId == 5)
            {
                liDesignViewBtn.Visible = true;
                if (RoleID == 4)
                {
                    liDesignViewBtn.Visible = false;
                }
            }
            else
            {
                liDesignViewBtn.Visible = false;
            }
        }
        else
        {
            // Bpmn.Visible = false;
            //liZoomIn.Visible = false;
            //liZoomOut.Visible = false;
            liSummary.Visible = false;
        }

        typeId = TypeData.GetTypeID(ProcessId);

        if (typeId == 5)
        {
            liProcess.Visible = true;
            liInventory.Visible = true;
            if (RoleID == 4)
            {
                liProcess.Visible = false;
                liInventory.Visible = false;
            }
        }
        else
        {
            liProcess.Visible = false;
            liInventory.Visible = false;
        }
        //liProcess.Visible = true;
        // load();



        if (!IsPostBack)
        {
            //AjaxControlToolkit.ModalPopupExtender PopupModelAttribute = (AjaxControlToolkit.ModalPopupExtender)ModelPopupAttributeUC1.FindControl("mopoExUser");
            //PopupModelAttribute.Hide();
        }


        if (ProcessId != 0)
        {
            ResetBinding();
            CalculateCriticalPath(ProcessId); //calculate critical path
            if (maxHeightnWidth.Count > 0)
            {
                int maxwidth = maxHeightnWidth.Max(a => a.Width);
                //find height for max top value from list maxtopProcess
                int maxheight = maxHeightnWidth.Max(a => a.Height);

                hdnWidth.Value = Convert.ToString(maxwidth);
                hdnheight.Value = Convert.ToString(maxheight);
            }
        }

        var modifiedhdval = hdnLastZoom.Value;
        if (Session["lastZoom"] != null || hdnLastZoom.Value != string.Empty)
        {
            if (hdnLastZoom.Value == string.Empty)
            {
                hdnLastZoom.Value = (string)(Session["lastZoom"]);
                modifiedhdval = hdnLastZoom.Value;
            }

            Session["lastZoom"] = hdnLastZoom.Value;
            Session["lastZoomE"] = null; // first time enterprise manager last zoom will set to zero 
            Page page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(typeof(Page), "ZoomRefresh", "<script type='text/javascript'>ZoomRefresh('" + modifiedhdval + "');</script>");
        }


        }

    private void MethodWithParam(string strProcessObjectId, string strAction)
    {
        //Session["SeletedPoId"] = strProcessObjectId;
        if (strAction == "attribute")
        {
            ModelPopupAttributeUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            //UserControl UcAttribute = (UserControl)Page.FindControl("ModelPopupAttributeUC.ascx");
            AjaxControlToolkit.ModalPopupExtender PopupModelAttribute = (AjaxControlToolkit.ModalPopupExtender)ModelPopupAttributeUC1.FindControl("mopoExUser");
            PopupModelAttribute.Show();
            if (RoleID == 4)
            {
                PopupModelAttribute.Hide();

            }

        }
        if (strAction == "inputs")
        {
            ModelPopupInputUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            // UserControl UcInput = (UserControl)Page.FindControl("ModelPopupInputUC.ascx");
            AjaxControlToolkit.ModalPopupExtender PopupModelInput = (AjaxControlToolkit.ModalPopupExtender)ModelPopupInputUC1.FindControl("modelInput");
            PopupModelInput.Show();
            if (RoleID == 4)
            {
                PopupModelInput.Hide();
            }
        }
        if (strAction == "BOM")
        {
            ModelPopupBOMUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            //UserControl UcBOM = (UserControl)Page.FindControl("ModelPopupBOMUC.ascx");
            AjaxControlToolkit.ModalPopupExtender PopupModelBOM = (AjaxControlToolkit.ModalPopupExtender)ModelPopupBOMUC1.FindControl("ModelBOM");
            PopupModelBOM.Show();
            if (RoleID == 4)
            {
                PopupModelBOM.Hide();
            }
        }
        if (strAction == "TFG")
        {
            ModelPopupTFGUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            /// UserControl UcTFG = (UserControl)Page.FindControl("ModelPopupTFGUC.ascx");
            AjaxControlToolkit.ModalPopupExtender PopupModelTFG = (AjaxControlToolkit.ModalPopupExtender)ModelPopupTFGUC1.FindControl("ModelTFG");
            PopupModelTFG.Show();
            if (RoleID == 4)
            {
                PopupModelTFG.Hide();
            }
        }
        if (strAction == "ErrorReport")
        {
            ModelPopupErrorReportUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            AjaxControlToolkit.ModalPopupExtender PopupModelErrorReport = (AjaxControlToolkit.ModalPopupExtender)ModelPopupErrorReportUC1.FindControl("ModelErrorReport");
            PopupModelErrorReport.Show();
            if (RoleID == 4)
            {
                PopupModelErrorReport.Hide();
            }
        }
        if (strAction == "Machine")
        {
            ModelPopupMchUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            // UserControl UcMachine = (UserControl)Page.FindControl("ModelPopupMchUC.ascx");
            AjaxControlToolkit.ModalPopupExtender PopupModelMachine = (AjaxControlToolkit.ModalPopupExtender)ModelPopupMchUC1.FindControl("ModelMachine");
            PopupModelMachine.Show();
            if (RoleID == 4)
            {
                PopupModelMachine.Hide();
            }
        }

        if (strAction == "Inventory")
        {
            TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
            if (mastertreeview.SelectedNode != null)
            {
                Session["SelectedNode"] = mastertreeview.SelectedNode.ValuePath;
                Session["SelectedNodeValue"] = mastertreeview.SelectedNode.Value;
            }
            //string script = "alert(\"You have successfully created process work view!\");";
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //              "ServerControlScript", script, true);

            //UpdatePanel11.Update();
            //Session["ProcessId"] = strProcessObjectId;
            HttpContext.Current.Response.Redirect("ProcessManager.aspx");
            // this.RaisePostBackEvent(lnkBtnInventory12, "");

        }

        if (strAction == "Activity")
        {
            ModelPopupActivityUC9.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            // UserControl UcMachine = (UserControl)Page.FindControl("ModelPopupMchUC.ascx");
            AjaxControlToolkit.ModalPopupExtender PopupModelActivity = (AjaxControlToolkit.ModalPopupExtender)ModelPopupActivityUC9.FindControl("ModelPopupActivity");
            PopupModelActivity.Show();
            if (RoleID == 4)
            {
                PopupModelActivity.Hide();
            }
        }
    }

    public void ResetBinding()
    {
        ViewState["sortBy"] = "CreatedDate";
        ViewState["isAsc"] = "1";
        BindGridSummary(); //bind gridview sorted by Created Date
    }

    public void BindGridSummary()
    {
        if (summaryResult.Count > 0)
        {
        }
        else
        {
            TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
            if (mastertreeview.SelectedNode != null)
            {
                ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
            }
            else if (Session["SelectedNodeValue"] != null)
            {
                ProcessId = this.CInt32(Session["SelectedNodeValue"]);
            }
            ///////////////////////////22April////////////////////////

            if (ProcessData.GetSummaryData(ProcessId))
            {
                List<ProcessData.ProcessDataProperty> record = ProcessData.GetSummaryTableRecord(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ProcessId));
                if (record.Count != 0)
                {
                    for (int k = 0; k < record.Count; k++)
                    {
                        string AttributeName = Convert.ToString(record[k].AttributeName);
                        // string unitName = ProcessData.GetUnitName(AttributeName);
                        string unitName = Convert.ToString(record[k].UnitName);
                        int FunctionID = Convert.ToInt32(record[k].FunctionID);
                        List<ProcessData.ProcessDataProperty> res = ProcessData.GetAttributeValue(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ProcessId), AttributeName, unitName);
                        if (res.Count > 0)
                        {
                            if (FunctionID == 1)
                            {
                                int sum = res.Sum(x => Convert.ToInt32(x.AttributeValueSum)); // get Sum here
                                // add attributename,value, unitname in list to display it in summary table
                                summaryResult.Add(new SummaryDetail() { AttributeName = AttributeName, AttributeValueResult = Convert.ToString(sum), UnitName = unitName });
                            }

                            if (FunctionID == 2)
                            {
                                double average = res.Average(x => Convert.ToInt32(x.AttributeValueSum)); // get Average here
                                // add attributename,value, unitname in list to display it in summary table
                                summaryResult.Add(new SummaryDetail() { AttributeName = AttributeName, AttributeValueResult = String.Format("{0:0.00}", average), UnitName = unitName });
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
                                summaryResult.Add(new SummaryDetail() { AttributeName = AttributeName, AttributeValueResult = String.Format("{0:0.00}", median), UnitName = unitName });
                            }

                            if (FunctionID == 4)
                            {
                                int total = res.Min(x => Convert.ToInt32(x.AttributeValueSum)); // get min here
                                // add attributename,value, unitname in list to display it in summary table
                                summaryResult.Add(new SummaryDetail() { AttributeName = AttributeName, AttributeValueResult = Convert.ToString(total), UnitName = unitName });
                            }

                            if (FunctionID == 5)
                            {
                                int total = res.Max(x => Convert.ToInt32(x.AttributeValueSum)); // get max here
                                // add attributename,value, unitname in list to display it in summary table
                                summaryResult.Add(new SummaryDetail() { AttributeName = AttributeName, AttributeValueResult = Convert.ToString(total), UnitName = unitName });
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
                                    summaryResult.Add(new SummaryDetail() { AttributeName = AttributeName, AttributeValueResult = String.Format("{0:0.00}", ret), UnitName = unitName });
                                }
                            }
                        }
                    }

                    gridProcessSummary.DataSource = summaryResult;
                    gridProcessSummary.DataBind();
                    divSummary.Visible = true;
                    if (RoleID == 4)
                    {
                        divSummary.Visible = false;
                    }
                }
                else
                {
                    divSummary.Visible = false;
                }

            }
            else
            {
                divSummary.Visible = false;
            }
        }

    }

    public class SummaryDetail
    {
        public string AttributeName { get; set; }
        // public int AttributeValueResult { get; set; }
        public string AttributeValueResult { get; set; }
        public string UnitName { get; set; }
    }

    public void findType()
    {
        //TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        //int ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        //TypeData.SystemTypeDetail typeId = new TypeData.SystemTypeDetail();
        //typeId = TypeData.GetTypeID(ProcessId);
    }
    public void load(string divProcessID, int DBID, int Top, int Left)
    {
        lst.Clear();
        MainDiv.Controls.Clear();
        // ResetBinding();
        //string ValuePath = "";
        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
        {
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
            // ValuePath = mastertreeview.SelectedNode.ValuePath.ToString();
        }
        else if (Session["SelectedNodeValue"] != null)
        {
            ProcessId = Convert.ToInt32(Session["SelectedNodeValue"]);
            //ValuePath = mastertreeview.SelectedNode.ValuePath.ToString();
        }


        VisualERPDataContext ObjData = new VisualERPDataContext();
        //List<ProcessData.ProcessDataProperty> lstpoid = ProcessData.GetAllProcessObjId(ProcessId);
        List<ProcessData.ProcessDataProperty> lstpoid = ProcessData.GetAllSingleProcessObjId(ProcessId);  ////////////////////////////// 

        if (lstpoid.Count > 0)
        {
            int countProcess = lstpoid.Count;
            int widthlast = (countProcess * 500) + 220; // 220 is extra width to scroll summary table 
            maxHeightnWidth.Add(new TopHeightWidth() { Width = widthlast, Height = (Top + 400) });
        }


        Table TblFirst = new Table();
        TblFirst.CellPadding = 0;
        TblFirst.CellSpacing = 0;
        TblFirst.Width = Unit.Percentage(90);
        TblFirst.BorderWidth = 0;
        TableRow TrFirst = new TableRow();
        //lstpoid = ProcessData.GetAllProcessObjId(ProcessId);
        int j = 0;
        for (int i = 0; i < lstpoid.Count; i++)
        {
            int Type = this.CInt32(lstpoid[i].Type);
            if (Type == 0)
            {
                j++;
                UserControls_ProcessObject xx = LoadControl("UserControls/ProcessObject.ascx") as UserControls_ProcessObject;
                //ModelPopupBOMUC1.Index = j;
                xx.Index = j;
                 xx.ProcessObjectId = this.CInt32(lstpoid[i].ProcessObjID);
                xx.PageMethodWithParamRef = delParam;
                if (i == 0)
                {
                    lst.Add(xx);
                }
                else
                {
                    UserControl ucleftArrow = LoadControl("UserControls/AerrowObject.ascx") as UserControl;
                    lst.Add(ucleftArrow);
                    lst.Add(xx);
                }
            }
            else
            {
                UserControls_InventeryObject xx = LoadControl("UserControls/InventeryObject.ascx") as UserControls_InventeryObject;
                xx.ProcessObjectId = this.CInt32(lstpoid[i].ProcessObjID);
                //xx.PageMethodWithParamRef = delParam;

                if (i == 0)
                {
                    lst.Add(xx);
                }
                else
                {
                    UserControl ucleftArrow = LoadControl("UserControls/AerrowObject.ascx") as UserControl;
                    lst.Add(ucleftArrow);
                    lst.Add(xx);
                }
            }
        }

        if (lst.Count > 0)
        {
            foreach (UserControl uc in lst)
            {
                TableCell td = new TableCell();
                td.Controls.Add(uc);
                TrFirst.Controls.Add(td);
            }
            TblFirst.Controls.Add(TrFirst);

            HtmlGenericControl div1 = new HtmlGenericControl("div"); // create a html div that will contains above table with id

            div1.ID = divProcessID; // creating div id that will be unique every time when we add process control
            div1.Attributes["name"] = "ContentPlaceHolder1_" + divProcessID;
            div1.Attributes["name"] = divProcessID;  // id will maintain by div name
            div1.Attributes["style"] = "Position:relative;width:400px;height:350px;top: " + Top + "px; left: " + Left + "px;"; // add div style with given postion
            div1.Controls.Add(TblFirst);

            MainDiv.Controls.Add(div1);

        }
    }

    public void loadParallelActivity(string divProcessID, int DBID, int Top, int Left, int width, int height, int type, int ParallelProcessObjID)
    {
        Plst.Clear();
        //MainDiv.Controls.Clear();
        // ResetBinding();
        // MainDiv1.Controls.Clear();

        //string ValuePath = "";
        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
        {
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
            // ValuePath = mastertreeview.SelectedNode.ValuePath.ToString();
        }
        else if (Session["SelectedNodeValue"] != null)
        {
            ProcessId = Convert.ToInt32(Session["SelectedNodeValue"]);
            //ValuePath = mastertreeview.SelectedNode.ValuePath.ToString();
        }

        VisualERPDataContext ObjData = new VisualERPDataContext();
        //List<ProcessData.ProcessDataProperty> lstpoid = ProcessData.GetAllProcessObjId(ProcessId);     
        List<ProcessData.ProcessDataProperty> lstpoidParallel = ProcessData.GetAllParallelProcessObjId(ProcessId);  //////////////////////////////


        Table TblSecond = new Table();
        TblSecond.CellPadding = 0;
        TblSecond.CellSpacing = 0;
        TblSecond.BorderWidth = 0;
        TblSecond.Width = Unit.Percentage(50);
       
        TableRow TrSecond = new TableRow();

        //lstpoid = ProcessData.GetAllProcessObjId(ProcessId);

        /////////////////////////////////////

        int t = 0;
        for (int i = 0; i < lstpoidParallel.Count; i++)
        {
            int Type = this.CInt32(lstpoidParallel[i].Type);
            if (Type == 0)
            {
                t++;
                UserControls_ProcessObject xx = LoadControl("UserControls/ProcessObject.ascx") as UserControls_ProcessObject;
                //ModelPopupBOMUC1.Index = j;  
                xx.Index = t;
                //xx.ProcessObjectId = this.CInt32(lstpoidParallel[i].ProcessObjID);
                xx.ProcessObjectId = DBID;
                xx.PageMethodWithParamRef = delParam;
                if (i == 0)
                {
                    UserControl ucDownArrow = LoadControl("UserControls/AerrowDownUC.ascx") as UserControl;
                    Plst.Add(ucDownArrow);
                    Plst.Add(xx);
                    
                }
            }
        }

        ///////////////////////////////////

        if (lstpoidParallel.Count > 0)
        {
            int countProcess = lstpoidParallel.Count;
            int widthlast = (countProcess * 500) + 220; // 220 is extra width to scroll summary table 
            


            StringBuilder sb = new StringBuilder();
            string str1 = string.Empty;
            foreach (UserControl uc in Plst)
            {
                TableCell td = new TableCell();
                TableRow trForEachRow = new TableRow();
                td.Controls.Add(uc);
                trForEachRow.Controls.Add(td);
                TblSecond.Controls.Add(trForEachRow);
            }

            


            HtmlGenericControl div2 = new HtmlGenericControl("div"); // create a html div that will contains above table with id

            div2.ID = divProcessID; // creating div id that will be unique every time when we add process control
            div2.Attributes["name"] = "ContentPlaceHolder1_" + divProcessID;
            div2.Attributes["name"] = divProcessID;  // id will maintain by div name
            div2.Attributes["style"] = "Position:absolute;width:100%;height:" + height + "px;top: " + Top + "px; left: " + Left + "px;"; // add div style with given postion
            div2.Controls.Add(TblSecond);
            MainDiv.Controls.Add(div2);

            //string setSession = "callready();";
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //              "ServerControlScript", setSession, true);

            StringBuilder sb2 = new StringBuilder();
            string str2 = string.Empty;

            maxHeightnWidth.Add(new TopHeightWidth() { Width = widthlast, Height = (Top + 400) }); // 220 is extra width to scroll summary table 
          
        }
    }
    //protected override void OnInit(EventArgs e)
    //{
    //    base.OnInit(e);
    //    if (Session["lst"] != null)
    //    {
    //        load();
    //    }
    //}

    //public static int count = 0;
    protected void lnkBtnProces_Click(object sender, EventArgs e)
    {

        SaveDummyProcesObject();
        ProcesObjectWorkView();
        CleareControl();
    }
    public void CleareControl()
    {
        ViewState["OrderNO"] = null;
        ViewState["ProcessObjID"] = null;
    }

    public void SaveDummyProcesObject()
    {
        tbl_ProcessObject ProcessDummyObj = new tbl_ProcessObject();
        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        ProcessDummyObj.ProcessID = ProcessId;
        ProcessDummyObj.CreatedDate = DateTime.Now;

        // if()
        ProcessDummyObj.Type = 0;
        bool result = false;

        result = ProcessData.SaveDumyProcessObject(ProcessDummyObj);
        int OrderNO = 0;
        OrderNO = ProcessData.GetMaxOrderID();

        ViewState["OrderNO"] = OrderNO;

        int ProcessObjId = 0;
        ProcessObjId = ProcessData.GetMaxProcessObjId(ProcessId);

        ViewState["ProcessObjID"] = ProcessObjId;
    }


    public void ProcesObjectWorkView()
    {
        tbl_ProcessObject ProcessObj = new tbl_ProcessObject();
        ProcessObj.ProcessObjName = "Activity-" + ViewState["OrderNO"];
        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        ProcessObj.ProcessID = ProcessId;
        ProcessObj.OrderNo = this.CInt32(ViewState["OrderNO"]); ;
        ProcessObj.ModifiedDate = DateTime.Now;
        ProcessObj.ProcessObjID = this.CInt32(ViewState["ProcessObjID"]);
        bool result = false;
        result = ProcessData.SaveProcessObject(ProcessObj);
        VisualERPDataContext ObjData = new VisualERPDataContext();
        ObjData.SP_BulkInsertAttribute(this.CInt32(ViewState["ProcessObjID"]), ProcessId, 1);
        if (result == true)
        {
            lst.Clear();
            // load();


            //string script = "alert(\"You have successfully created process work view!\");";
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //              "ServerControlScript", script, true);
        }
        else
        {
            string script = "alert(\"Error on saving data.!\");";
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "ServerControlScript", script, true);
        }


    }
    /// <summary>
    /// Its not using any where in process manager page.
    /// </summary>
    public void ProcessObject()
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();

        if (Session["lst"] != null)
            lst = (List<UserControl>)Session["lst"];

        Table TblFirst = new Table();
        TblFirst.CellPadding = 0;
        TblFirst.CellSpacing = 0;
        TblFirst.Width = Unit.Percentage(100);
        TblFirst.BorderWidth = 0;
        TableRow TrFirst = new TableRow();
        // MainDiv.InnerHtml = "";

        if (lst.Count == 0)
        {
            UserControls_ProcessObject uc1 = LoadControl("UserControls/ProcessObject.ascx") as UserControls_ProcessObject;
            uc1.ProcessObjectId = this.CInt32(ViewState["ProcessObjID"]);

            //if (Session["lst"] != null)
            //    lst = (List<UserControl>)Session["lst"];

            lst.Add(uc1);
            foreach (UserControl uc in lst)
            {
                TableCell td = new TableCell();

                td.Controls.Add(uc);
                TrFirst.Controls.Add(td);
            }

            Session["lst"] = lst;
        }
        else
        {

            UserControl ucleftArrow = LoadControl("UserControls/AerrowObject.ascx") as UserControl;
            lst.Add(ucleftArrow);

            UserControl ucInventery = LoadControl("UserControls/InventeryObject.ascx") as UserControl;
            lst.Add(ucInventery);

            UserControl ucRightArrow = LoadControl("UserControls/AerrowObject.ascx") as UserControl;
            lst.Add(ucRightArrow);

            //UserControl uc2 = new UserControl();
            UserControls_ProcessObject uc2 = LoadControl("UserControls/ProcessObject.ascx") as UserControls_ProcessObject;
            //if (Session["lst"] != null)
            //    lst = (List<UserControl>)Session["lst"];

            lst.Add(uc2);

            foreach (UserControl uc in lst)
            {
                TableCell td = new TableCell();
                td.Controls.Add(uc);
                TrFirst.Controls.Add(td);
            }


            Session["lst"] = lst;

        }
        TblFirst.Controls.Add(TrFirst);
        MainDiv.Controls.Add(TblFirst);
    }

    //void lnkAttribute_Click(object sender, EventArgs e)
    //{
    //    UserControl UcAttribute = (UserControl)Page.FindControl("ModelPopupAttributeUC.ascx");
    //    AjaxControlToolkit.ModalPopupExtender PopupModelAttribute = (AjaxControlToolkit.ModalPopupExtender)UcAttribute.FindControl("mopoExUser");
    //    PopupModelAttribute.Show();
    //}

    protected void lnkBtnInventory_Click(object sender, EventArgs e)
    {

        InventoryUC1.PageMethodWithParamRef = delParam;
        AjaxControlToolkit.ModalPopupExtender PopupModelInventery = (AjaxControlToolkit.ModalPopupExtender)InventoryUC1.FindControl("ModelPopupInventery");
        PopupModelInventery.Show();
        //InventryObeject();

    }
    /// <summary>
    /// Its not using any where in process manager page.
    /// </summary>
    public void InventryObeject()
    {
        Table TblFirst = new Table();
        TblFirst.CellPadding = 0;
        TblFirst.CellSpacing = 0;
        TblFirst.Width = Unit.Percentage(100);
        TblFirst.BorderWidth = 0;
        TableRow TrFirst = new TableRow();
        // MainDiv.InnerHtml = "";

        if (lst.Count == 0)
        {
            UserControl uc1 = LoadControl("UserControls/InventeryObject.ascx") as UserControl;
            lst.Add(uc1);
        }
        else
        {
            UserControl ucAerrow = new UserControl();
            ucAerrow = LoadControl("UserControls/AerrowObject.ascx") as UserControl;
            lst.Add(ucAerrow);


            UserControl uc2 = LoadControl("UserControls/InventeryObject.ascx") as UserControl;
            lst.Add(uc2);
        }

        Session["lst"] = lst;

        foreach (UserControl uc in lst)
        {
            TableCell td = new TableCell();

            td.Controls.Add(uc);
            TrFirst.Controls.Add(td);
        }

        TblFirst.Controls.Add(TrFirst);
        MainDiv.Controls.Add(TblFirst);
    }



    //protected void gridProcessSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gridProcessSummary.PageIndex = e.NewPageIndex;
    //    this.BindGridSummary(); //// bind grid view
    //}
    protected void gridProcessSummary_Sorting(object sender, GridViewSortEventArgs e)
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
        BindGridSummary();
    }
    protected void gridProcessSummary_RowCreated(object sender, GridViewRowEventArgs e)
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
                                image.ImageUrl = "~\\Images\\ArrowDown.gif";
                            else
                                image.ImageUrl = "~\\Images\\ArrowUp.gif";
                            cell.Controls.Add(image);
                        }

                    }
                }
            }
        }
    }

    //protected void btnDesignView_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Production.aspx");
    //}

    public void ControlPosition(string id, int DBID, int top, int left, int width, int height, int i, string title, int type, int ParallelProcessObjID)
    {
        if (id.Contains("divProcess") && ParallelProcessObjID == 0 && type == 0)
        {
            load(id, DBID, top, left);
        }
        else if (id.Contains("divParallelProcess") && ParallelProcessObjID != 0)
        {
            loadParallelActivity(id, DBID, top, left, width, height, type, ParallelProcessObjID);
        }
        else if (type > 1 && type < 30)
        {

            UserControls_ImageControl imageControl = LoadControl("UserControls/ImageControl.ascx") as UserControls_ImageControl;//load supplier control
            //string id = "divSupplier" + Guid.NewGuid(); // create control id that is unique by giving its id with guid
            //supplierNew.SupplierId = id;
            //imageControl.Top = top;
            //imageControl.Left = left;
            imageControl.Title = title;
            imageControl.ControlId = id;
            imageControl.Type = type;
            imageControl.ProcessObjectId = DBID;
            // MainDiv.Controls.Add(imageControl);

            HtmlGenericControl div1 = new HtmlGenericControl("div"); // create a html div that will contains above table with id

            div1.ID = id; // creating div id that will be unique every time when we add process control
            //div1.Attributes["name"] = "ContentPlaceHolder1_" + id;
            div1.Attributes["name"] = id;  // id will maintain by div name
            // div1.Attributes["style"] = "Position:absoulte;width:"+width +"px;height:"+ height+"px;top: " + top + "px; left: " + left + "px;"; // add div style with given postion
            div1.Attributes["style"] = "position: absolute!important;width:50px;height:50px;top: " + top + "px; left: " + left + "px;"; // add div style with given postion                

            div1.Controls.Add(imageControl);
            MainDiv.Controls.Add(div1);
            //successfully inserted


            //newList[i].Title = title;
            //newList[i].Top = top;
            //newList[i].Left = left;
            //newList[i].Type = type;
            //Session["Supplier"] = newList;
            maxHeightnWidth.Add(new TopHeightWidth() { Width = (left + width + 300), Height = (top + height + 300) }); //300 extra gap 
        }
        else if (id.Contains("divCArrow"))
        {

            UserControls_ArrowControl arrowControl = LoadControl("UserControls/ArrowControl.ascx") as UserControls_ArrowControl;
            arrowControl.ControlId = id;
            //arrowControl.Top = top;
            //arrowControl.Left = left;
            arrowControl.Width = width;
            arrowControl.Height = height;
            arrowControl.Type = type;
            arrowControl.ProcessObjectId = DBID;
            // MainDiv.Controls.Add(arrowControl);


            HtmlGenericControl div1 = new HtmlGenericControl("div"); // create a html div that will contains above table with id

            div1.ID = id; // creating div id that will be unique every time when we add process control
            //div1.Attributes["name"] = "ContentPlaceHolder1_" + id;
            div1.Attributes["name"] = id;  // id will maintain by div name
            // div1.Attributes["style"] = "Position:absoulte;width:"+width +"px;height:"+ height+"px;top: " + top + "px; left: " + left + "px;"; // add div style with given postion
            div1.Attributes["style"] = "position: absolute!important;width:50px;height:50px;top: " + top + "px; left: " + left + "px;"; // add div style with given postion                

            div1.Controls.Add(arrowControl);
            MainDiv.Controls.Add(div1);
            //successfully inserted


            //newList[i].Title = title;
            //newList[i].Top = top;
            //newList[i].Left = left;
            //newList[i].Type = 7;
            //Session["Supplier"] = newList;
            maxHeightnWidth.Add(new TopHeightWidth() { Width = (left + width + 300), Height = (top + height + 300) }); //300 extra gap 
        }

    }

    public string GetDivid(int type, int DBID, int ProcessID, int ParallelProcessObjID)
    {
        string divId = "";

        if (type == 2)
            divId = "divSupplier" + DBID;
        if (type == 3)
            divId = "divShipment" + DBID;
        if (type == 4)
            divId = "divForcast" + DBID;
        if (type == 5)
            divId = "divArrow" + DBID;
        if (type == 6)
            divId = "divDSchedule" + DBID;
        if (type == 7)
            divId = "divValueStream" + DBID;
        if (type == 8)
            divId = "divElectronic" + DBID;
        if (type == 9)
            divId = "divDataTable" + DBID;
        if (type == 10)
            divId = "divTimelineSegment" + DBID;
        if (type == 11)
            divId = "divTimelinetotal" + DBID;
        if (type == 12)
            divId = "divSupermarket" + DBID;
        if (type == 13)
            divId = "divSafetyStock" + DBID;
        if (type == 14)
            divId = "divSignalKanban" + DBID;
        if (type == 15)
            divId = "divWithdrawalkanban" + DBID;
        if (type == 16)
            divId = "divWithdrawalBatch" + DBID;
        if (type == 17)
            divId = "divProductionKanban" + DBID;
        if (type == 18)
            divId = "divBatchKanban" + DBID;
        if (type == 19)
            divId = "divKanbanPost" + DBID;
        if (type == 20)
            divId = "divFIFOLane" + DBID;
        if (type == 21)
            divId = "divKaizenBurst" + DBID;
        if (type == 22)
            divId = "divPullArrow1" + DBID;
        if (type == 23)
            divId = "divPullArrow2" + DBID;
        if (type == 24)
            divId = "divPullArrow3" + DBID;
        if (type == 25)
            divId = "divPhysicalPull" + DBID;
        if (type == 26)
            divId = "divSequencedPullBall" + DBID;
        if (type == 27)
            divId = "divLoadLeveling" + DBID;
        if ((type == 0 || type == 1) && ParallelProcessObjID == 0)
            divId = "divProcess" + ProcessID;
        if ((type == 0 || type == 1) && ParallelProcessObjID != 0)
            divId = "divParallelProcess" + DBID;
        if (type == 30)
            divId = "divCArrow" + DBID;
        if (type == 31)
            divId = "divCArrow" + DBID;
        if (type == 32)
            divId = "divCArrow" + DBID;
        if (type == 33)
            divId = "divCArrow" + DBID;
        if (type == 34)
            divId = "divCArrow" + DBID;
        if (type == 35)
            divId = "divCArrow" + DBID;
        if (type == 36)
            divId = "divCArrow" + DBID;
        if (type == 37)
            divId = "divCArrow" + DBID;
        if (type == 38)
            divId = "divCArrow" + DBID;
        if (type == 39)
            divId = "divCArrow" + DBID;
        if (type == 40)
            divId = "divCArrow" + DBID;
        if (type == 41)
            divId = "divCArrow" + DBID;
        if (type == 42)
            divId = "divCArrow" + DBID;
        if (type == 43)
            divId = "divCArrow" + DBID;
        if (type == 44)
            divId = "divCArrow" + DBID;
        if (type == 45)
            divId = "divCArrow" + DBID;
        if (type == 46)
            divId = "divCArrow" + DBID;
        if (type == 47)
            divId = "divCArrow" + DBID;
        if (type == 48)
            divId = "divCArrow" + DBID;
        if (type == 49)
            divId = "divCArrow" + DBID;
        if (type == 50)
            divId = "divCArrow" + DBID;
        return divId;
    }

    public class Supplier
    {
        public string SupplierID { get; set; }
        public int EditID { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
    }

    public void DeleteControl()
    {
        ///delete control from database
        string cmd1 = this.Page.Request.Params["__EVENTTARGET"];
        string[] delC = cmd1.Split('_');
        string cntrlPoid = delC[1];

        int processobjId = 0;
        processobjId = this.CInt32(cntrlPoid);
        if (processobjId > 0)
        {
            bool result = false;
            result = ProcessData.DeleteProcessObjDataByID(processobjId);////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables 
            if (result == true)
            {
                //delete successfully
            }
        }
    }

    public void DeleteInventory()
    {
        ///delete control from database
        string cmd1 = this.Page.Request.Params["__EVENTTARGET"];
        string[] delC = cmd1.Split('_');
        string cntrlPoid = delC[1];

        int processobjId = 0;
        processobjId = this.CInt32(cntrlPoid);
        if (processobjId > 0)
        {
            int pid = ProcessData.GetProcessIdByPoid(processobjId);
            int position = ProcessData.GetPositionByPoid(processobjId);
            bool decrease = false;
            decrease = ProcessData.DecreaseNextRowsPosition(pid, position);
            if (decrease == true)
            {
                // position updated successfully
            }
            bool result = false;
            result = InventeryData.DeleteInventeryProcessObjByID(processobjId); ////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables
            bool result1 = false;
            result1 = ProcessData.DeleteProcessObjDataByID(processobjId);
        }

    }

    public void DeleteProcess(int ProcessId)
    {
        divErrorMsg.Visible = true;
        bool hasreport = false;
        ///delete Process from database
        string cmd1 = this.Page.Request.Params["__EVENTTARGET"];
        string[] delC = cmd1.Split('_');
        string cntrlPoid = delC[1];

        int processobjId = 0;
        processobjId = this.CInt32(cntrlPoid);
        if (processobjId > 0)
        {
            hasreport = ProcessData.HasReport(processobjId);
            if (hasreport)
            {
                string reportName = ProcessData.GetReportName(processobjId);
                //this activity associcated with report please delete report first
                lblMsg.Text = "This activity is associcated with report  " + "<a href='ManageReport.aspx'><span style='color:red; margin-left:3px; margin-right:3px;'><u>" + reportName + "</u></span></a>" + "  Click on report to delete.";
                //lblMsg.Style.Add("color", "red");

                lblMsg.Style.Add("color", "black");
                divErrorMsg.Style.Add("min-width", "450px");
                divErrorMsg.Style.Add("max-width", "450px");
                divErrorMsg.Style.Add("margin-left", "0px");
                divErrorMsg.Attributes.Add("class", "isa_info");
            }
            else
            {
                int PId = ProcessData.GetProcessIdByPoid(processobjId);

                bool IsParalled = false;
                IsParalled = ProcessData.IsParallelProcessByPoid(processobjId);
                if (IsParalled == true)
                {
                    bool resultP = false;
                    resultP = ProcessData.DeleteParallelProcessObjDataByID(processobjId);////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables
                    // do nothing because no position define for parallel process
                }
                else
                {
                    int position = ProcessData.GetPositionByPoid(processobjId);
                    bool decrease = false;
                    decrease = ProcessData.DecreaseNextRowsPosition(PId, position);
                    if (decrease == true)
                    {
                        // position updated successfully
                    }
                }

                bool result = false; bool result1 = false; bool resultSystemIO = false; bool deleteSummaryData = false; bool reportResult = false;


                //join poid data with summary data and delete join attribute data from summarydata
                deleteSummaryData = ProcessData.DeleteSummaryDataByPoid(processobjId, ProcessId);

                result = ProcessData.DeleteProcessObjDataByID(processobjId); ////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables            
                result1 = ProcessData.DeleteAttributedataByPoID(processobjId);
                resultSystemIO = ProcessData.DeleteSystemIODataByPoID(processobjId);
                // reportResult = ProcessData.DeleteReportDataByPoID(processobjId);
                lblMsg.Text = "This activity has been deleted.";
                lblMsg.Style.Add("color", "green");
                divErrorMsg.Style.Add("min-width", "200px");
                divErrorMsg.Style.Add("margin-left", "290px");
                divErrorMsg.Attributes.Add("class", "isa_success");
                //Response.Redirect(Request.RawUrl);
            }
        }
    }

    /// <summary>
    /// Calculate the critical path that have max cycle time for given process id 
    /// </summary>
    /// <param name="ProcessId">Process Id that is selected tree node process id</param>
    public void CalculateCriticalPath(int ProcessId)
    {

        List<ProcessData.ProcessDataProperty> prop = ProcessData.GetPoidIdByPosition(ProcessId); // getting process object id by position from table process object 
        List<string> PathCalculated = new List<string>();
        if (prop != null)
        {
            string defaultpath = string.Empty; string defaultpath1 = string.Empty; string path = string.Empty; string path2 = string.Empty; string poid = string.Empty; string poidfor = string.Empty; string poidfor1 = string.Empty; string FromPoid = string.Empty; string ToPoid = string.Empty;
            for (int i = 0; i < prop.Count; i++)
            {
                poid = Convert.ToString(prop[i].ProcessObjID);
                path += poid + "-";

            }
            defaultpath = path.TrimEnd('-');
            PathCalculated.Add(defaultpath);

            string[] pathArray = defaultpath.Split('-');

            //for (int j = 0; j < pathArray.Length; j++)
            //{
            poidfor = pathArray[0];
            Session["StartNode"] = poidfor;
            FromParallelProcessObjID(poidfor, ProcessId);


            if (poidfor != "") // if process is not empty
            {
                // now find other start points of process manager and find all other path relative to new start point
                string startPoid = string.Empty;
                List<ProcessData.ProcessDataProperty> OtherStartPoint = ProcessData.GetOtherStartPoints(ProcessId); // other start point on process manager page
                if (OtherStartPoint.Count > 0)
                {
                    for (int r = 0; r < OtherStartPoint.Count; r++)
                    {
                        startPoid = Convert.ToString(OtherStartPoint[r].ProcessObjIDParl);
                        str = string.Empty;
                        str1 = string.Empty;
                        FromParallelProcessObjID(startPoid, ProcessId); // getting path for other start point
                    }
                }

                if (cyclePath.Count != 0)
                    for (int c = 0; c < cyclePath.Count; c++)
                    {
                        int add = 0;
                        string pathChar = Convert.ToString(cyclePath[c]);
                        string[] pathValue = pathChar.Split('-');
                        for (int k = 0; k < pathValue.Length; k++)
                        {
                            int valPoid = Convert.ToInt32(pathValue[k]); // get path value that is process obj id
                            add += ProcessData.GetCycleTimeforPoid(valPoid); // add will have total cycle time for each path
                        }
                        cyclePathTime.Add(add); // add other path in list cyclePathTime
                    }

                int MaxValue = cyclePathTime.Max(); // get max cycle time from list cyclePathTime
                ltrMaxCycleTime.Text = Convert.ToString(MaxValue); // display max cycle time on literal 


                //string[] lefttoNextArray = FromPoid.Split('-');
                //}
            }

        }
    }

    /// <summary>
    /// it will make path for given process object id 
    /// </summary>
    /// <param name="poidfor">process objec id</param>
    public void FromParallelProcessObjID(string poidfor, int ProcessId)
    {
        str1 += poidfor.ToString() + "-";  // append process object id to and making path 
        string splitStr = str1.TrimEnd('-'); // trim las char that is "-"

        List<ProcessData.ProcessDataProperty> Fromparallelpoid = ProcessData.GetFromParallelProcessObjId(poidfor, ProcessId); // it will get next or To process object id for given processobj id
        if (Fromparallelpoid.Count != 0 && Fromparallelpoid != null)
        {
            for (int i = 0; i < Fromparallelpoid.Count; i++)
            {
                // Recursively call the DisplayChildNodeText method to
                // traverse the tree and display all the child nodes.
                FromParallelProcessObjID(Convert.ToString(Fromparallelpoid[i].ProcessObjIDParl), ProcessId); // recursive method call and add current poid to desire path
                if (str1.IndexOf(Convert.ToString(Fromparallelpoid[i].ProcessObjIDParl)) >= 0)
                    str1 = str1.Substring(0, str1.IndexOf(Convert.ToString(Fromparallelpoid[i].ProcessObjIDParl))); // pop old path 
            }
        }
        else
        {
            cyclePath.Add(splitStr); // addind each path cycle in list 
            str1 = str1.Substring(0, str1.IndexOf(poidfor));
        }
    }
    protected void lnkEditHeader_ServerClick(object sender, EventArgs e)
    {
        //int FormType = Convert.ToInt32(ViewState["FormType"]);
        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
        {
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        }
        else if (Session["SelectedNodeValue"] != null)
        {
            ProcessId = this.CInt32(Session["SelectedNodeValue"]);
        }
        Response.Redirect("~/Default2.aspx?src=process&&processId=" + ProcessId);


    }
    public class TopHeightWidth
    {
        //public int Top { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}