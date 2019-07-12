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
using System.IO;
public partial class Production : System.Web.UI.Page
{
    #region
    List<UserControl> lst = new List<UserControl>();
    List<UserControl> Plst = new List<UserControl>(); /////////////////
    int ProcessId = 0;
    int typeId = 0;
    #endregion
    
    string str2 = string.Empty;
    int Top = 50;
    int Left = 50;
    string SelectedType = string.Empty;
    //ArrayList CtrlList = new ArrayList();
    List<Supplier> newList = new List<Supplier>();
    List<int> activityFrom = new List<int>();//////////////////
    List<int> activityTo = new List<int>();//////////////////

    List<ProcessData.ProcessDataProperty> activityNode = new List<ProcessData.ProcessDataProperty>();
    List<TopHeightWidth> maxHeightnWidth = new List<TopHeightWidth>(); // it will contains max height and width for any process
    delegate void DelMethodWithParam(string strProcessObjectId, string strAction);
    delegate void DelMethodWithoutParam();

    DelMethodWithParam delParam;
    string EditId = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["lastZoom"] = null; // last zoom for process manager will be set to zero on this page so user goes back to process page original postion will be shown there
        divErrorMsg.Visible = false;
        EditId = GetPostBackControlId((Page)sender); // to get postback control id that is clicked//////////////////////////////


        if (ViewState["SelectedType"] != null)
            SelectedType = this.CStr(ViewState["SelectedType"]);




        Panel1.Visible = false;
        Panel2.Visible = false;
        PnlParallelProcess.Visible = false;
        pnlAddProcess.Visible = false;
        string scriptJq = "test();";  //call jquery function here on page load
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript", scriptJq, true);

        Label lblManager = (Label)Master.FindControl("lblManager");
        lblManager.Text = "Process View";
        lblManager.Attributes.Add("class", "Process");
        string src = Request.QueryString["src"];
        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            lblManager.Text = "Target View";
            lblManager.Attributes.Add("class", "Target");
        }

        string script = "test();";
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript", script, true);

        //liProcess.Visible = false;
        //liInventory.Visible = false;
        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");

        if (mastertreeview.SelectedNode != null)
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        else if (Session["SelectedNodeValue"] != null)
            ProcessId = this.CInt32(Session["SelectedNodeValue"]);


        //  delParam = new DelMethodWithParam(MethodWithParam);
        //////////////////////////////

        foreach (ListItem item in chklstActivitiesFrom.Items)
        {
            if (item.Selected)
            {
                activityFrom.Add(Convert.ToInt32(item.Value));
                string acctivityname = Activity.GetActivityNameByProcessObjId(Convert.ToInt32(item.Value));
                Session["ActivitiesFromID"] = activityFrom; // it hold activity id that is proess object id in session                
            }
        }

        foreach (ListItem item in chklstActivitiesTo.Items)
        {
            if (item.Selected)
            {
                activityTo.Add(Convert.ToInt32(item.Value));
                string acctivityname = Activity.GetActivityNameByProcessObjId(Convert.ToInt32(item.Value));
                Session["ActivitiesToID"] = activityTo; // it hold activity id that is proess object id in session           
            }
        }

        if ((EditId != "btnAddParallelProcess") && EditId != "ddlParallelActivity")
            FillddlUnits(ProcessId);  /////////////////////////////

        if (EditId != "ddlParallelActivity")
            BindActivityCheckboxList(ProcessId); //////////////////////////////////
        //}


        if (EditId != "btnAddProcessActivity")
            FillddlActivitySequence(ProcessId);

        if (EditId != "addInventeryBtn")
            FillddlInventoryNextTo(ProcessId);



        /////////////////////////////

        if (mastertreeview.SelectedNode != null)
            Session["SelectedNode"] = mastertreeview.SelectedNode.ValuePath;
        //Session["SelectedNodeValue"] = mastertreeview.SelectedNode.Value;

        typeId = TypeData.GetTypeID(ProcessId);

        if (typeId == 5)
        {
            controls.Visible = true;
            liSavedesign.Visible = true;
            ///liProcess.Visible = true;
            //liInventory.Visible = true;
        }
        else
        {
            controls.Visible = false;
            liSavedesign.Visible = false;
            //liProcess.Visible = false;
            //liInventory.Visible = false;
        }


        if (this.Page.Request.Params["__EVENTTARGET"] != null && this.Page.Request.Params["__EVENTTARGET"].Contains("Del-C"))
        {
            DeleteControl();
        }

        if (this.Page.Request.Params["__EVENTTARGET"] != null && this.Page.Request.Params["__EVENTTARGET"].Contains("DeleteInventory"))
        {
            DeleteInventory();
        }

        if (this.Page.Request.Params["__EVENTTARGET"] != null && this.Page.Request.Params["__EVENTTARGET"].Contains("DeleteProcess"))
        {
            DeleteProcess();
        }

        // to stop post back while adding controls
        if (EditId != "lnkBtnProces" && EditId != "lnkBtnparallelProcess" && EditId != "lnkBtnSupplier" && EditId != "lnkBtnInventory" && EditId != "lnkBtnShipment" && EditId != "lnkBtnForcast" && EditId != "lnkBtnArrow" && EditId != "lnkBtnDSchedule" && EditId != "lnkBtnProduction" && EditId != "lnkBtnElectronic" && EditId != "lnkBtnDataTable" && EditId != "lnkBtnTimelineSegment" && EditId != "lnkBtnTimelinetotal" && EditId != "lnkBtnSupermarket" && EditId != "lnkbtnSafetyStock" && EditId != "lnkbtnSignalKanban" && EditId != "lnkbtnWithdrawalkanban" && EditId != "lnkbtnWithdrawalBatch" && EditId != "lnkbtnProductionKanban" && EditId != "lnkbtnBatchKanban" && EditId != "lnkbtnKanbanPost" && EditId != "lnkbtnFIFOLane" && EditId != "lnkbtnKaizenBurst" && EditId != "lnkbtnPullArrow1" && EditId != "lnkbtnPullArrow2" && EditId != "lnkbtnPullArrow3" && EditId != "lnkbtnPhysicalPull" && EditId != "lnkbtnSequencedPullBall" && EditId != "lnkbtnLoadLeveling")
            LoadallControls();

        if (!IsPostBack)
        {
            //AjaxControlToolkit.ModalPopupExtender PopupModelAttribute = (AjaxControlToolkit.ModalPopupExtender)ModelPopupAttributeUC1.FindControl("mopoExUser");
            //PopupModelAttribute.Hide();           
        }

        if (ProcessId != 0)
        {
            if (maxHeightnWidth.Count > 0)
            {
                int maxwidth = maxHeightnWidth.Max(a => a.Width);
                //find height for max top value from list maxtopProcess
                int maxheight = maxHeightnWidth.Max(a => a.Height);

                hdnWidth.Value = Convert.ToString(maxwidth);
                hdnheight.Value = Convert.ToString(maxheight);
                //hdnLastZoom.Value=
            }

        }
    }



    public void LoadallControls()
    {
        int sourceType = 1;
        if (ProcessId != 0)
        {
            Session["SelectedNodeValue"] = ProcessId;
            string divId = "";
            StringBuilder sb2 = new StringBuilder();
            string str2 = string.Empty;
            string str11 = string.Empty;
            string strP = string.Empty;
            int sV = 0;
            if (ViewState["SelectedValue"] != null)
                sV = Convert.ToInt32(ViewState["SelectedValue"]);

            string src = Request.QueryString["src"];

            List<ControlsData.ProcessObjectData> listData = new List<ControlsData.ProcessObjectData>();
            if (!string.IsNullOrEmpty(src) && src == "tgt")
            {
                sourceType = 2;
                listData = TargetControlsData.GetAllProcessControlData(ProcessId);
            }
            else
                listData = ControlsData.GetAllProcessControlData(ProcessId);

            if (listData.Count != 0)
            {
                newList.Clear();
                Bpmn.Visible = true;
                for (int i = 0; i < listData.Count; i++)
                {
                    int ParallelProcessObjID = 0;
                    int top = 0; int left = 0; string title = ""; int width = 50; int height = 50;
                    title = listData[i].Title.ToString();
                    int type = Convert.ToInt32(listData[i].Type.ToString());
                    int DBID = Convert.ToInt32(listData[i].ProcessObjID.ToString());
                    if (listData[i].ParallelProcessObjID != null)
                        ParallelProcessObjID = Convert.ToInt32(listData[i].ParallelProcessObjID.ToString());

                    divId = GetDivid(type, DBID, ProcessId, ParallelProcessObjID);

                    string modifiedhdval = hdnSupplier.Value;
                    //if ((hdnSupplier.Value != string.Empty) && (sV == Convert.ToInt32(mastertreeview.SelectedNode.Value)))
                    if (hdnSupplier.Value != string.Empty && EditId != "TreeView1")
                    {
                        try
                        {
                            string arr = modifiedhdval.Substring(modifiedhdval.IndexOf(divId), modifiedhdval.Substring(modifiedhdval.IndexOf(divId)).IndexOf(","));
                            // Session["PosSupplier"] = modifiedhdval;
                            // string arr = modifiedhdval.Substring(modifiedhdval.IndexOf(divId), modifiedhdval.Substring(modifiedhdval.IndexOf(divId)).IndexOf(","));

                            string[] arrN = arr.Split('@');
                            string[] possX = arrN[1].Split('~');
                            top = Convert.ToInt32(possX[0]);       //x position of control
                            string[] possX1 = possX[1].Split('[');
                            left = Convert.ToInt32(possX1[0]);

                            if (arr.Contains("divCArrow"))
                            {
                                string[] arrN1 = arr.Split('[');
                                string[] arrN2 = arrN1[1].Split(']');
                                string[] arrN3 = arrN2[0].Split('*');
                                width = Convert.ToInt32(arrN3[0]);
                                height = Convert.ToInt32(arrN3[1]);
                            }
                        }
                        catch
                        {
                            top = Convert.ToInt32(listData[i].XTop == null ? 0 : listData[i].XTop);
                            left = Convert.ToInt32(listData[i].YLeft == null ? 0 : listData[i].YLeft);

                        }
                    }
                    else
                    {
                        top = Convert.ToInt32(listData[i].XTop == null ? 0 : listData[i].XTop);
                        left = Convert.ToInt32(listData[i].YLeft == null ? 0 : listData[i].YLeft);
                        width = Convert.ToInt32(listData[i].Width == null ? 0 : listData[i].Width);
                        height = Convert.ToInt32(listData[i].Height == null ? 0 : listData[i].Height);
                    }

                    newList.Add(new Supplier() { SupplierID = divId, EditID = DBID, Top = top, Left = left, Width = width, Height = height, Type = type, Title = title, ParallelProcessID = ParallelProcessObjID });
                    ControlPosition(divId, DBID, top, left, width, height, i, title, type, ParallelProcessObjID, sourceType); // call function to creae control on last position
                    Session["Supplier"] = newList;

                    if (type != 0 || type != 1)
                    {

                        //str11 = divId + "@" + top + "~" + left + ",";
                        str11 = divId + "@" + top + "~" + left + "[" + width + "*" + height + "]" + ",";
                        // sb2.Append(divId + "@" + top + "~" + left + ",");
                        str2 += str11.ToString();
                    }
                    else
                    {
                        strP = divId + "@" + top + "~" + left + "[" + width + "*" + height + "]" + ",";
                    }


                    //Session["PosSupplier"] = str2;
                    //hdnSupplier.Value = str2;
                }
                hdnSupplier.Value = strP + str2;
                ViewState["SelectedValue"] = ProcessId;
            }
            else
            {
                Bpmn.Visible = true;
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

                }
            }

        }
        else
        {
            Bpmn.Visible = false;
        }
    }

    public string GetDivid(int type, int DBID, int processid, int ParallelProcessObjID) // added ParallelProcessObjID 
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
            divId = "divProcess" + processid;
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

    public void load(string divProcessID, int DBID, int Top, int Left, int type)
    {
        string src = Request.QueryString["src"];

        lst.Clear();
        MainDiv1.Controls.Clear();

        string ValuePath = "";
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
        List<ProcessData.ProcessDataProperty> lstpoid;
        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            lstpoid = TargetData.GetAllSingleTargetObjID(ProcessId);
        }
        else
            lstpoid = ProcessData.GetAllSingleProcessObjId(ProcessId);  //////////////////////////////        

        if (lstpoid.Count > 0)
        {
            int countProcess = lstpoid.Count;
            int widthlast = (countProcess * 500) + 220; // 220 is extra width to scroll summary table 
            maxHeightnWidth.Add(new TopHeightWidth() { Width = widthlast, Height = (Top + 400) });
        }

        Table TblFirst = new Table();
        TblFirst.CellPadding = 0;
        TblFirst.CellSpacing = 0;
        TblFirst.BorderWidth = 0;

        TblFirst.Width = Unit.Percentage(90);
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

                if (!string.IsNullOrEmpty(src) && src == "tgt")
                {
                    xx.SourceType = 2;
                }
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
            else
            {
                UserControls_InventeryObject xx = LoadControl("UserControls/InventeryObject.ascx") as UserControls_InventeryObject;

                if (!string.IsNullOrEmpty(src) && src == "tgt")
                {
                    xx.SourceType = 2;
                }
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
            StringBuilder sb = new StringBuilder();
            string str1 = string.Empty;
            foreach (UserControl uc in lst)
            {
                TableCell td = new TableCell();
                td.Controls.Add(uc);
                TrFirst.Controls.Add(td);
            }

            TblFirst.Controls.Add(TrFirst);




            //int InsertedID = 0;
            //VisualERPDataContext ObjData1 = new VisualERPDataContext();
            //tbl_ProcessObject controldata1 = new tbl_ProcessObject();
            //controldata1.ProcessObjID = DBID;
            //controldata1.XTop = Top;
            //controldata1.YLeft = Left;
            //controldata1.Width = 0;
            //controldata1.Height = 0;
            //if (type == 0)
            //    controldata1.Type = 0; //Process module having type1

            //if (type == 1)
            //    controldata1.Type = 1;

            //controldata1.Title = "";
            //controldata1.ProcessID = ProcessId;
            //try
            //{
            //    InsertedID = ControlsData.SaveControlData(controldata1);
            //}
            //catch
            //{
            //}
        }


        HtmlGenericControl div1 = new HtmlGenericControl("div"); // create a html div that will contains above table with id

        div1.ID = divProcessID; // creating div id that will be unique every time when we add process control
        div1.Attributes["name"] = "ContentPlaceHolder1_" + divProcessID;
        div1.Attributes["name"] = divProcessID;  // id will maintain by div name
        //height: 50px;
        div1.Attributes["style"] = "Position:absoulte;top: " + Top + "px; left: " + Left + "px;"; // add div style with given postion
        div1.Controls.Add(TblFirst);
        MainDiv1.Controls.Add(div1);

        string setSession = "callready();";
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript", setSession, true);

        StringBuilder sb2 = new StringBuilder();
        string str2 = string.Empty;
        //if (Session["PosSupplier"] != null)
        //{
        //    str2 += Convert.ToString(Session["PosSupplier"]);
        //}

        //sb2.Append("ContentPlaceHolder1_" + div1.ID + "@" + Top + "-" + Left + ",");




        //sb2.Append(divProcessID + "@" + Top + "~" + Left + ",");
        //str2 += sb2.ToString();
        //Session["PosSupplier"] = str2;
        //hdnSupplier.Value = str2;

        // }
    }

    public void loadParallelActivity(string divProcessID, int DBID, int Top, int Left, int width, int height, int type, int ParallelProcessObjID)
    {
        Plst.Clear();
        // MainDiv1.Controls.Clear();

        string ValuePath = "";
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

        string src = Request.QueryString["src"];

        List<ProcessData.ProcessDataProperty> lstpoidParallel = new List<ProcessData.ProcessDataProperty>();
        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            lstpoidParallel = ProcessData.GetAllParallelTargetObjId(ProcessId);
        }
        else
            lstpoidParallel = ProcessData.GetAllParallelProcessObjId(ProcessId);  //////////////////////////////

        Table TblSecond = new Table();
        TblSecond.CellPadding = 0;
        TblSecond.CellSpacing = 0;
        TblSecond.BorderWidth = 0;

        //TblSecond.Width = Unit.Percentage(50);
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
                if (!string.IsNullOrEmpty(src) && src == "tgt")
                {
                    xx.SourceType = 2;
                }
                xx.ProcessObjectId = DBID;
                //xx.PageMethodWithParamRef = delParam;
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

            TblSecond.Controls.Add(TrSecond);




            //int InsertedID = 0;
            //VisualERPDataContext ObjData1 = new VisualERPDataContext();
            //tbl_ProcessObject controldata1 = new tbl_ProcessObject();
            //controldata1.ProcessObjID = DBID;
            //controldata1.XTop = Top;
            //controldata1.YLeft = Left;
            //controldata1.Width = width;
            //controldata1.Height = height;
            //if (type == 0)
            //    controldata1.Type = 0; //Process module having type1

            //if (type == 1)
            //    controldata1.Type = 1;

            //controldata1.Title = "";
            //controldata1.ProcessID = ProcessId;
            //try
            //{
            //    InsertedID = ControlsData.SaveControlData(controldata1);
            //}
            //catch
            //{
            //}
            //}


            HtmlGenericControl div2 = new HtmlGenericControl("div"); // create a html div that will contains above table with id

            div2.ID = divProcessID; // creating div id that will be unique every time when we add process control
            div2.Attributes["name"] = "ContentPlaceHolder1_" + divProcessID;
            div2.Attributes["name"] = divProcessID;  // id will maintain by div name
            //height: 50px;
            div2.Attributes["style"] = "Position:absolute;top: " + Top + "px; left: " + Left + "px;"; // add div style with given postion
            div2.Controls.Add(TblSecond);
            MainDiv1.Controls.Add(div2);

            string setSession = "callready();";
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "ServerControlScript", setSession, true);

            StringBuilder sb2 = new StringBuilder();
            string str2 = string.Empty;
            //if (Session["PosSupplier"] != null)
            //{
            //    str2 += Convert.ToString(Session["PosSupplier"]);
            //}

            //sb2.Append("ContentPlaceHolder1_" + div1.ID + "@" + Top + "-" + Left + ",");




            //sb2.Append(divProcessID + "@" + Top + "~" + Left + ",");
            //str2 += sb2.ToString();
            //Session["PosSupplier"] = str2;
            //hdnSupplier.Value = str2;
            maxHeightnWidth.Add(new TopHeightWidth() { Width = (Left + 500 + 220), Height = (Top + 400) }); // 220 is extra width to scroll summary table 
        }
    }

    protected void lnkBtnSupplier_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.Supplier);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkBtnShipment_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.Shipment);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkBtnForcast_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.Forcast);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkBtnArrow_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.Arrow);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkBtnDSchedule_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.DSchedule);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkBtnProduction_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.Production);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkBtnElectronic_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.Electronic);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkBtnDataTable_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.DataTable);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkBtnTimelineSegment_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.TimelineSegment);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkBtnTimelinetotal_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.Timelinetotal);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkBtnSupermarket_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.Supermarket);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkbtnSafetyStock_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.SafetyStock);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkbtnSignalKanban_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.SignalKanban);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkbtnWithdrawalkanban_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.Withdrawalkanban);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkbtnWithdrawalBatch_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.WithdrawalBatch);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkbtnProductionKanban_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.ProductionKanban);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkbtnBatchKanban_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.BatchKanban);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkbtnKanbanPost_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.KanbanPost);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkbtnFIFOLane_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.FIFOLane);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkbtnKaizenBurst_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.KaizenBurst);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkbtnPullArrow1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.PullArrow1);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkbtnPullArrow2_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.PullArrow2);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkbtnPullArrow3_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.PullArrow3);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkbtnPhysicalPull_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.PhysicalPull);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkbtnSequencedPullBall_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.SequencedPullBall);
        ViewState["SelectedType"] = SelectedType;
    }

    protected void lnkbtnLoadLeveling_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        SelectedType = Convert.ToString(ProcessControl.LoadLeveling);
        ViewState["SelectedType"] = SelectedType;

    }

    public void ImageControlAdd(string id, int InsertedID, int type, string title)
    {
        UserControls_ImageControl imageControl = LoadControl("UserControls/ImageControl.ascx") as UserControls_ImageControl;
        imageControl.ControlId = id;
        //imageControl.Top = 20;
        //imageControl.Left = 20;
        imageControl.Type = type;
        imageControl.Title = title;
        imageControl.ProcessObjectId = InsertedID;
        // MainDiv1.Controls.Add(imageControl);

        HtmlGenericControl divAddImage = new HtmlGenericControl("div"); // create a html div that will contains above table with id

        divAddImage.ID = id;  // creating div id that will be unique every time when we add process control
        //div1.Attributes["name"] = "ContentPlaceHolder1_" + id;
        divAddImage.Attributes["name"] = id;   // id will maintain by div name
        // div1.Attributes["style"] = "Position:absoulte;width:"+width +"px;height:"+ height+"px;top: " + top + "px; left: " + left + "px;"; // add div style with given postion
        divAddImage.Attributes["style"] = "position: absolute; top:20px; left:20px;  width: 200px; height: 200px;"; // add div style with given postion         

        divAddImage.Controls.Add(imageControl);
        MainDiv1.Controls.Add(divAddImage);

        newList.Add(new Supplier() { SupplierID = id, EditID = InsertedID, Top = 50, Left = 50, Type = type, Title = title });
        Session["Supplier"] = newList;


        int top = 50, left = 50;
        string script = "callready();";
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript", script, true);

        StringBuilder sb2 = new StringBuilder();
        string str2 = string.Empty;
        //if (Session["PosSupplier"] != null)
        //{
        //    str2 += Convert.ToString(Session["PosSupplier"]);
        //}
        if (!string.IsNullOrEmpty(hdnSupplier.Value))
        {
            // str2 += Convert.ToString(Session["PosSupplier"]);
            str2 += hdnSupplier.Value;
        }
        sb2.Append(id + "@" + top + "-" + left + ",");
        str2 += sb2.ToString();
        Session["PosSupplier"] = str2;
        hdnSupplier.Value = str2;
    }

    public void ControlPosition(string id, int DBID, int top, int left, int width, int height, int i, string title, int type, int ParallelProcessObjID, int sourceType = 1) // added ParallelProcessObjID
    {
        if (id.Contains("divProcess") && ParallelProcessObjID == 0 && (type == 0 || type == 1))
        {
            load(id, DBID, top, left, type);
        }
        else if (id.Contains("divParallelProcess") && ParallelProcessObjID != 0)
        {
            loadParallelActivity(id, DBID, top, left, width, height, type, ParallelProcessObjID);
        }

        else if (id.Contains("divCArrow"))
        {
            int EditID = InsertModifiedControlData(id, DBID, type, top, left, width, height, title, sourceType);
            if (EditID != 0)
            {
                UserControls_ArrowControl arrowControl = LoadControl("UserControls/ArrowControl.ascx") as UserControls_ArrowControl;
                arrowControl.ControlId = id.Replace("d", "a");
                arrowControl.Type = type;
                arrowControl.ProcessObjectId = DBID;
                //arrowControl.Top = top;
                //arrowControl.Left = left;
                arrowControl.Width = width;
                arrowControl.Height = height;


                HtmlGenericControl divcontrolarrow = new HtmlGenericControl("div"); // create a html div that will contains above table with id

                divcontrolarrow.ID = id; // creating div id that will be unique every time when we add process control
                //div1.Attributes["name"] = "ContentPlaceHolder1_" + id;
                divcontrolarrow.Attributes["name"] = id;  // id will maintain by div name
                // div1.Attributes["style"] = "Position:absoulte;width:"+width +"px;height:"+ height+"px;top: " + top + "px; left: " + left + "px;"; // add div style with given postion
                divcontrolarrow.Attributes["style"] = "position: absolute; width:50px;height:50px;top: " + top + "px; left: " + left + "px;"; // add div style with given postion                

                divcontrolarrow.Controls.Add(arrowControl);
                MainDiv1.Controls.Add(divcontrolarrow);
            }
            else
            {
                ////error while saving
            }

            newList[i].Title = title;
            newList[i].Top = top;
            newList[i].Left = left;
            newList[i].Type = type;
            Session["Supplier"] = newList;
        }

        else if (type > 1 && type < 30)
        {
            int EditID = InsertModifiedControlData(id, DBID, type, top, left, width, height, title, sourceType);
            if (EditID != 0)
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
                // MainDiv1.Controls.Add(imageControl);
                //successfully inserted

                HtmlGenericControl divImageC = new HtmlGenericControl("div"); // create a html div that will contains above table with id

                divImageC.ID = id; // creating div id that will be unique every time when we add process control
                //div1.Attributes["name"] = "ContentPlaceHolder1_" + id;
                divImageC.Attributes["name"] = id;  // id will maintain by div name
                // div1.Attributes["style"] = "Position:absoulte;width:"+width +"px;height:"+ height+"px;top: " + top + "px; left: " + left + "px;"; // add div style with given postion
                divImageC.Attributes["style"] = "position: absolute; width:50px;height:50px;top: " + top + "px; left: " + left + "px;"; // add div style with given postion                

                divImageC.Controls.Add(imageControl);
                MainDiv1.Controls.Add(divImageC);
            }
            else
            {
                ////error while saving
            }

            newList[i].Title = title;
            newList[i].Top = top;
            newList[i].Left = left;
            newList[i].Type = type;
            Session["Supplier"] = newList;
        }


        
    }

    protected void lnkbtnSavedesign_Click(object sender, EventArgs e)
    {
        //On click save button page load will call which will save current x-y position of control in database

        string scriptJq = "callready();";  //call jquery function here on page load
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript", scriptJq, true);
        string src = Request.QueryString["src"];

        Session["SelectedNodeValue"] = ProcessId;
        string title = "";
        int top, left, type;
        if (Session["Supplier"] != null)
        {
            newList = (List<Supplier>)Session["Supplier"];
            for (int i = 0; i < newList.Count; i++)
            {
                title = newList[i].Title.ToString();
                type = Convert.ToInt32(newList[i].Type.ToString());
                top = Convert.ToInt32(newList[i].Top.ToString());
                left = Convert.ToInt32(newList[i].Left.ToString());

                int returnid;

                //VisualERPDataContext ObjData = new VisualERPDataContext();
                //tbl_ProcessObject controldata = new tbl_ProcessObject();
                //controldata.ProcessObjID = Convert.ToInt32(newList[i].EditID);
                //controldata.XTop = top;
                //controldata.YLeft = left;
                //controldata.Width = 0;
                //controldata.Height = 0;
                //controldata.Type = type;
                //controldata.Title = title;

                //int returnid;

                //returnid = ControlsData.SaveControlData(controldata);



                if (!string.IsNullOrEmpty(src) && src == "tgt")
                {
                    VisualERPDataContext ObjData = new VisualERPDataContext();
                    tbl_TargetObject controldata = new tbl_TargetObject();
                    controldata.TargetObjID = Convert.ToInt32(newList[i].EditID);
                    controldata.XTop = top;
                    controldata.YLeft = left;
                    controldata.Width = 0;
                    controldata.Height = 0;
                    controldata.Type = type;
                    controldata.Title = title;


                    returnid = TargetControlsData.SaveControlData(controldata);
                }

                else
                {

                    VisualERPDataContext ObjData = new VisualERPDataContext();
                    tbl_ProcessObject controldata = new tbl_ProcessObject();
                    controldata.ProcessObjID = Convert.ToInt32(newList[i].EditID);
                    controldata.XTop = top;
                    controldata.YLeft = left;
                    controldata.Width = 0;
                    controldata.Height = 0;
                    controldata.Type = type;
                    controldata.Title = title;

                    returnid = ControlsData.SaveControlData(controldata);
                }





                if (returnid != 0)
                {
                    //successfully inserted
                }
                else
                {
                    ////error while saving
                }
            }

        }
        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            Response.Redirect("TargetManager.aspx");
        }
        else
            Response.Redirect("ProcessManager.aspx");
    }

    protected void btnAddTitle_Click1(object sender, EventArgs e)
    {
        int InsertedID = 0;
        int sourceType = 1;
        string src = Request.QueryString["src"];
        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            sourceType = 2;
        }
        //if (SelectedType == Convert.ToString(ProcessControl.Supplier))
        //{
        //    Panel1.Visible = false;
        //    string title = txtTitleName.Text.Trim();
        //    txtTitleName.Text = "";
        //    //ModalPopupExtender1.Hide();
        //    InsertedID = InsertControlData(2, title);
        //    if (InsertedID != 0)
        //    {
        //        UserControls_Supplier supplierNew = LoadControl("UserControls/Supplier.ascx") as UserControls_Supplier; //load supplier control
        //        //string id = "divSupplier" + Guid.NewGuid(); // create control id that is unique by giving its id with guid
        //        //supplierNew.SupplierId = id;
        //        //supplierNew.Top = 50;
        //        //supplierNew.Left = 50;
        //        supplierNew.Title = title;
        //        supplierNew.SupplierId = "divSupplier" + InsertedID;
        //        supplierNew.ProcessObjectId = InsertedID;
        //        //MainDiv1.Controls.Add(supplierNew);
        //        //successfully inserted

        //        HtmlGenericControl div1 = new HtmlGenericControl("div"); // create a html div that will contains above table with id

        //        div1.ID = "divSupplier" + InsertedID;  // creating div id that will be unique every time when we add process control
        //        //div1.Attributes["name"] = "ContentPlaceHolder1_" + id;
        //        div1.Attributes["name"] = "divSupplier" + InsertedID;   // id will maintain by div name
        //        // div1.Attributes["style"] = "Position:absoulte;width:"+width +"px;height:"+ height+"px;top: " + top + "px; left: " + left + "px;"; // add div style with given postion
        //        div1.Attributes["style"] = "position: absolute; top:10px; left:20px; width: 200px!important; height: 200px!important;"; // add div style with given postion                

        //        div1.Controls.Add(supplierNew);
        //        MainDiv1.Controls.Add(div1);
        //    }
        //    else
        //    {
        //        ////error while saving
        //    }


        //    newList.Add(new Supplier() { SupplierID = "divSupplier" + InsertedID, EditID = InsertedID, Top = 50, Left = 50, Type = 2, Title = title });
        //    Session["Supplier"] = newList;


        //    int top = 50, left = 50;
        //    string script = "callready();";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(),
        //                  "ServerControlScript", script, true);

        //    StringBuilder sb2 = new StringBuilder();
        //    string str2 = string.Empty;
        //    //if (Session["PosSupplier"] != null)
        //    //{
        //    //    str2 += Convert.ToString(Session["PosSupplier"]);
        //    //}
        //    if (!string.IsNullOrEmpty(hdnSupplier.Value))
        //    {
        //        //str2 += Convert.ToString(Session["PosSupplier"]);
        //        str2 += hdnSupplier.Value;
        //    }

        //    sb2.Append("divSupplier" + InsertedID + "@" + top + "~" + left + ",");
        //    str2 += sb2.ToString();
        //    Session["PosSupplier"] = str2;
        //    hdnSupplier.Value = str2;
        //}

        //if (SelectedType == Convert.ToString(ProcessControl.Shipment))
        //{

        //    Panel1.Visible = false;
        //    string title = txtTitleName.Text.Trim();
        //    txtTitleName.Text = "";
        //    //ModalPopupExtender1.Hide();


        //    InsertedID = InsertControlData(3, title);
        //    if (InsertedID != 0)
        //    {
        //        UserControls_Shipment shipmentNew = LoadControl("UserControls/Shipment.ascx") as UserControls_Shipment; //load supplier control
        //        //string id = "divSupplier" + Guid.NewGuid(); // create control id that is unique by giving its id with guid
        //        //supplierNew.SupplierId = id;
        //        //shipmentNew.Top = 50;
        //        //shipmentNew.Left = 50;
        //        shipmentNew.Title = title;
        //        shipmentNew.SupplierId = "divShipment" + InsertedID;
        //        shipmentNew.ProcessObjectId = InsertedID;
        //       // MainDiv1.Controls.Add(shipmentNew);
        //        //successfully inserted

        //        HtmlGenericControl div1 = new HtmlGenericControl("div"); // create a html div that will contains above table with id

        //        div1.ID = "divShipment" + InsertedID;  // creating div id that will be unique every time when we add process control
        //        //div1.Attributes["name"] = "ContentPlaceHolder1_" + id;
        //        div1.Attributes["name"] = "divShipment" + InsertedID;   // id will maintain by div name
        //        // div1.Attributes["style"] = "Position:absoulte;width:"+width +"px;height:"+ height+"px;top: " + top + "px; left: " + left + "px;"; // add div style with given postion
        //        div1.Attributes["style"] = "position: absolute; top:20px; left:20px;  width: 200px!important; height: 200px!important;"; // add div style with given postion                

        //        div1.Controls.Add(shipmentNew);
        //        MainDiv1.Controls.Add(div1);
        //    }
        //    else
        //    {
        //        ////error while saving
        //    }

        //    newList.Add(new Supplier() { SupplierID = "divShipment" + InsertedID, EditID = InsertedID, Top = 50, Left = 50, Type = 3, Title = title });
        //    Session["Supplier"] = newList;


        //    int top = 50, left = 50;
        //    string script = "callready();";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(),
        //                  "ServerControlScript", script, true);

        //    StringBuilder sb2 = new StringBuilder();
        //    string str2 = string.Empty;
        //    if (!string.IsNullOrEmpty(hdnSupplier.Value))
        //    {
        //        // str2 += Convert.ToString(Session["PosSupplier"]);
        //        str2 += hdnSupplier.Value;
        //    }

        //    sb2.Append("divShipment" + InsertedID + "@" + top + "~" + left + ",");
        //    str2 += sb2.ToString();
        //    Session["PosSupplier"] = str2;
        //    hdnSupplier.Value = str2;
        //}

        if (SelectedType == Convert.ToString(ProcessControl.Supplier))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";


            int outputID = InsertControlData(2, title, sourceType);

            string id = "divSupplier" + outputID;
            int type = 2;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.Shipment))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";


            int outputID = InsertControlData(3, title, sourceType);

            string id = "divShipment" + outputID;
            int type = 3;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.Forcast))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";


            int outputID = InsertControlData(4, title, sourceType);

            string id = "divForcast" + outputID;
            int type = 4;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.Arrow))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";


            int outputID = InsertControlData(5, title, sourceType);

            string id = "divArrow" + outputID;
            int type = 5;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.DSchedule))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";


            int outputID = InsertControlData(6, title, sourceType);

            string id = "divDSchedule" + outputID;
            int type = 6;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.Production))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";


            int outputID = InsertControlData(7, title, sourceType);

            string id = "divValueStream" + outputID;
            int type = 7;
            ImageControlAdd(id, outputID, type, title);
        }

        if (SelectedType == Convert.ToString(ProcessControl.Electronic))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";


            int outputID = InsertControlData(8, title, sourceType);

            string id = "divElectronic" + outputID;
            int type = 8;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.DataTable))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(9, title, sourceType);

            string id = "divDataTable" + outputID;
            int type = 9;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.TimelineSegment))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(10, title, sourceType);

            string id = "divTimelineSegment" + outputID;
            int type = 10;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.Timelinetotal))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(11, title, sourceType);

            string id = "divTimelinetotal" + outputID;
            int type = 11;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.Supermarket))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(12, title, sourceType);

            string id = "divSupermarket" + outputID;

            int type = 12;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.SafetyStock))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(13, title, sourceType);

            string id = "divSafetyStock" + outputID;
            int type = 13;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.SignalKanban))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(14, title, sourceType);

            string id = "divSignalKanban" + outputID;
            int type = 14;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.Withdrawalkanban))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(15, title, sourceType);

            string id = "divWithdrawalkanban" + outputID;
            int type = 15;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.WithdrawalBatch))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(16, title, sourceType);

            string id = "divWithdrawalBatch" + outputID;
            int type = 16;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.ProductionKanban))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(17, title, sourceType);

            string id = "divProductionKanban" + outputID;
            int type = 17;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.BatchKanban))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";
            int outputID = InsertControlData(18, title, sourceType);

            string id = "divBatchKanban" + outputID;
            int type = 18;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.KanbanPost))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(19, title, sourceType);

            string id = "divKanbanPost" + outputID;
            int type = 19;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.FIFOLane))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(20, title, sourceType);

            string id = "divFIFOLane" + outputID;
            int type = 20;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.KaizenBurst))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(21, title, sourceType);

            string id = "divKaizenBurst" + outputID;
            int type = 21;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.PullArrow1))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(22, title, sourceType);

            string id = "divPullArrow1" + outputID;
            int type = 22;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.PullArrow2))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(23, title, sourceType);

            string id = "divPullArrow2" + outputID;
            int type = 23;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.PullArrow3))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(24, title, sourceType);

            string id = "divPullArrow3" + outputID;
            int type = 24;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.PhysicalPull))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(25, title, sourceType);

            string id = "divPhysicalPull" + outputID;
            int type = 25;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.SequencedPullBall))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(26, title, sourceType);

            string id = "divSequencedPullBall" + outputID;
            int type = 26;
            ImageControlAdd(id, outputID, type, title);
        }
        if (SelectedType == Convert.ToString(ProcessControl.LoadLeveling))
        {
            Panel1.Visible = false;
            string title = txtTitleName.Text.Trim();
            txtTitleName.Text = "";

            int outputID = InsertControlData(27, title, sourceType);

            string id = "divLoadLeveling" + outputID;
            int type = 27;
            ImageControlAdd(id, outputID, type, title);
        }

    }

    /// <summary>
    /// InsertControlData will insert arrow data in database table parallelactivity
    /// </summary>
    /// <param name="type">enum type selected arrow</param>
    /// <param name="title">no title for arrows</param>
    /// <returns></returns>
    public int InsertControlData(int type, string title, int sourceType = 1)
    {
        string src = Request.QueryString["src"];
        int srcType = 1;
        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            srcType = 2;
        }

        int InsertedID = 0; // output primary key id 
        VisualERPDataContext ObjData = new VisualERPDataContext();

        if (srcType == 1)
        {
            tbl_ProcessObject controldata = new tbl_ProcessObject();
            //controldata.ProcessObjID = id;
            controldata.XTop = 10; // top 50px
            controldata.YLeft = 20; // left 50 px
            controldata.Width = 200;
            controldata.Height = 200;
            controldata.Type = type;
            controldata.Title = title;
            controldata.ProcessID = ProcessId;
            try
            {
                InsertedID = ControlsData.SaveControlData(controldata); // save arrow data method
            }
            catch
            {
            }
        }
        if (srcType == 2)
        {
            tbl_TargetObject controldata = new tbl_TargetObject();
            //controldata.ProcessObjID = id;
            controldata.XTop = 10; // top 50px
            controldata.YLeft = 20; // left 50 px
            controldata.Width = 200;
            controldata.Height = 200;
            controldata.Type = type;
            controldata.Title = title;
            controldata.TargetID = ProcessId;
            try
            {
                InsertedID = TargetControlsData.SaveControlData(controldata); // save arrow data method
            }
            catch
            {
            }
        }
        return InsertedID; // return output id
    }

    public int InsertModifiedControlData(string id, int DBID, int type, int XTop, int YLeft, int width, int height, string title, int sourceType = 1)
    {
        int InsertedID = 0;
        VisualERPDataContext ObjData = new VisualERPDataContext();

        if (sourceType == 1)
        {
            tbl_ProcessObject controldata = new tbl_ProcessObject();
            controldata.ProcessObjID = DBID;
            controldata.XTop = XTop;
            controldata.YLeft = YLeft;
            controldata.Width = width;
            controldata.Height = height;
            controldata.Type = type;
            controldata.Title = title;
            controldata.ProcessID = ProcessId;
            try
            {
                InsertedID = ControlsData.SaveControlData(controldata);
            }
            catch
            {
            }
        }
        if (sourceType == 2)
        {
            tbl_TargetObject controldata = new tbl_TargetObject();
            controldata.TargetObjID = DBID;
            controldata.XTop = XTop;
            controldata.YLeft = YLeft;
            controldata.Width = width;
            controldata.Height = height;
            controldata.Type = type;
            controldata.Title = title;
            controldata.TargetID = ProcessId;
            try
            {
                InsertedID = TargetControlsData.SaveControlData(controldata);
            }
            catch
            {
            }
        }
        return InsertedID;
    }

    protected void imgCloseTree_OnClick(object sender, ImageClickEventArgs e)
    {
        txtTitleName.Text = "";
        string script = "unloadPopupBox();";
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript", script, true);
    }

    public class Supplier
    {
        public string SupplierID { get; set; }
        public int EditID { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public int ParallelProcessID { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    protected void lnkBtnProces_Click(object sender, EventArgs e)
    {
        //SaveDummyProcesObject();
        //ProcesObjectWorkView();
        //CleareControl();
        pnlAddProcess.Visible = true;
        txtAddActivity.Text = string.Empty;
        //ddlNexttoActivity.SelectedIndex = -1;
    }

    protected void lnkBtnInventory_Click(object sender, EventArgs e)
    {
        Panel2.Visible = true;
        //InventoryUC1.PageMethodWithParamRef = delParam;
        //AjaxControlToolkit.ModalPopupExtender PopupModelInventery = (AjaxControlToolkit.ModalPopupExtender)InventoryUC1.FindControl("ModelPopupInventery");
        //PopupModelInventery.Show();
        ////InventryObeject();
    }


    protected void addInventeryBtn_Click(object sender, EventArgs e)
    {
        string src = Request.QueryString["src"];

        int ddlPoid = 0;
        int prePosition = 0;
        int nextPositionInventory = 0;
        if (ddlInventoryNextTo.SelectedIndex > -1)
        {
            if (ddlInventoryNextTo.SelectedItem.Value != "0")
            {
                ddlPoid = Convert.ToInt32(ddlInventoryNextTo.SelectedItem.Value);
                if (!string.IsNullOrEmpty(src) && src == "tgt")
                {
                    prePosition = ProcessData.GetTPositionByPoid(ddlPoid);
                    //nextPositionInventory = prePosition + 1;
                }
                else

                    prePosition = ProcessData.GetPositionByPoid(ddlPoid);
                nextPositionInventory = prePosition + 1; //addind position 1 in previous postion of process
            }
            else
            {
                nextPositionInventory = 1;
            }
        }

        else
        {
            nextPositionInventory = 1;
        }




        bool increased = false;

        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            increased = ProcessData.IncreasNextTargetRowsPosition(ProcessId, nextPositionInventory);
        }
        else
            increased = ProcessData.IncreasNextRowsPosition(ProcessId, nextPositionInventory);
        if (increased == true)
        {
            // position updated successfully
        }
        //int MaxPosition = ProcessData.GetMaxPositionforPoId(ProcessId);
        //int newInventoryPosition = MaxPosition + 1;

        //bool increased = false;
        //increased = ProcessData.IncreasNextRowsPosition(ProcessId, newInventoryPosition);
        //if (increased == true)
        //{
        //    // position updated successfully
        //}

        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            SaveTargetObjectInventery(nextPositionInventory);
        }
        else

            SaveProcesObjectInventery(nextPositionInventory);
        //InvokeLoad();

        // UpdatePanel updat = (UpdatePanel)this.Parent.FindControl("Uppnl1");
        Panel2.Visible = false;


    }

    public void SaveTargetObjectInventery(int newInventoryPosition)
    {
        tbl_TargetObject ProcessObjInventery = new tbl_TargetObject();
        TreeView mastertreeview = (TreeView)this.Page.Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        ProcessObjInventery.TargetID = ProcessId;
        ProcessObjInventery.CreatedDate = DateTime.Now;
        // if()
        ProcessObjInventery.Type = 1;
        ProcessObjInventery.XTop = 20;
        ProcessObjInventery.YLeft = 20;
        ProcessObjInventery.Width = 0;
        ProcessObjInventery.Height = 0;
        ProcessObjInventery.Title = "";
        ProcessObjInventery.Position = newInventoryPosition;
        bool result = false;
        result = TargetData.SaveDumyProcessObject(ProcessObjInventery);


        int ProcessObjId = 0;
        ProcessObjId = ProcessData.GetMaxTargetObjId(ProcessId);
        ViewState["TargetObjID"] = ProcessObjId;
        InsertInventeryData(2);


        //InvokeLoad();
        //ModelPopupInventery.Hide();
    }
    public void SaveProcesObjectInventery(int newInventoryPosition)
    {
        tbl_ProcessObject ProcessObjInventery = new tbl_ProcessObject();
        TreeView mastertreeview = (TreeView)this.Page.Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        ProcessObjInventery.ProcessID = ProcessId;
        ProcessObjInventery.CreatedDate = DateTime.Now;
        // if()
        ProcessObjInventery.Type = 1;
        ProcessObjInventery.XTop = 20;
        ProcessObjInventery.YLeft = 20;
        ProcessObjInventery.Width = 0;
        ProcessObjInventery.Height = 0;
        ProcessObjInventery.Title = "";
        ProcessObjInventery.Position = newInventoryPosition;
        ProcessObjInventery.ProcessObjName = txtInventoryName.Text.Trim();
        bool result = false;
        result = ProcessData.SaveDumyProcessObject(ProcessObjInventery);


        int ProcessObjId = 0;
        ProcessObjId = ProcessData.GetMaxProcessObjId(ProcessId);
        ViewState["ProcessObjID"] = ProcessObjId;
        InsertInventeryData();


        //InvokeLoad();
        //ModelPopupInventery.Hide();
    }

    public void InsertInventeryData(int sourceType = 1)
    {
        tbl_InvantoryTriangle ObjectInventery = new tbl_InvantoryTriangle();
        ObjectInventery.CT = this.CInt32(txtCT.Text.Trim());
        ObjectInventery.Doller = this.CInt32(txtdoller.Text.Trim());
        ObjectInventery.Time = this.CInt32(txttime.Text.Trim());
        if (sourceType == 2)
            ObjectInventery.ProcessObjID = this.CInt32(ViewState["TargetObjID"]);
        else
            ObjectInventery.ProcessObjID = this.CInt32(ViewState["ProcessObjID"]);

        ObjectInventery.CreatedDate = DateTime.Now;
        bool result = false;
        result = InventeryData.SaveProcessObjectInventery(ObjectInventery);
        LoadallControls();
    }

    protected void lnkBtnparallelProcess_Click(object sender, EventArgs e)
    {
        PnlParallelProcess.Visible = true;
        txtParallelProcessName.Text = string.Empty;
    }

    protected void btnAddParallelProcess_Click(object sender, EventArgs e)
    {
        string src = Request.QueryString["src"];
        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            SaveTargetObject();
            ParallelTargetObjectWorkView();
        }
        else
        {
            SaveDummyProcesObject();
            ParallelProcesObjectWorkView();
        }
        CleareControl();
        PnlParallelProcess.Visible = false;
    }

    public void ParallelProcesObjectWorkView()
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        tbl_ProcessObject ProcessObj = new tbl_ProcessObject();
        ProcessObj.ProcessObjName = txtParallelProcessName.Text;
        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        ProcessObj.ProcessID = ProcessId;
        ProcessObj.OrderNo = this.CInt32(ViewState["OrderNO"]);
        ProcessObj.ModifiedDate = DateTime.Now;
        ProcessObj.ProcessObjID = this.CInt32(ViewState["ProcessObjID"]);
        if (Session["SelectedProcessObjID"] != null)
            ProcessObj.ParallelProcessObjID = Convert.ToInt32(Session["SelectedProcessObjID"]);

        var RootElementPosition = ObjData.tbl_ProcessObjects.Where(p => p.ProcessObjID == Convert.ToInt32(Session["SelectedProcessObjID"])).Select(x => x.Position).FirstOrDefault();
        int lastYLeft = 0;
        if(RootElementPosition==1 || RootElementPosition==null)
        {
            lastYLeft = 20;
        }
        else
        {
            for (int i = 1; i < RootElementPosition; i++)
            {
                lastYLeft += 570;
            }
        }
        

        ProcessObj.XTop = Convert.ToInt32(ObjData.tbl_ProcessObjects.Where(p => p.ProcessObjID == Convert.ToInt32(Session["SelectedProcessObjID"])).Select(x => x.XTop).FirstOrDefault()) + 360;
        ProcessObj.YLeft = lastYLeft;
        ProcessObj.Title = "";
        ProcessObj.Width = 0;
        ProcessObj.Height = 0;
        bool result = false;
        result = ProcessData.SaveParallelProcessObject(ProcessObj);

        ObjData.SP_BulkInsertAttribute(this.CInt32(ViewState["ProcessObjID"]), ProcessId, 1);
        if (result == true)
        {
            bool resultFrom = false;
            tbl_ParallelRelationship relationalData;
            for (int i = 0; i < activityFrom.Count; i++)
            {
                relationalData = new tbl_ParallelRelationship();
                int prcobjID = Convert.ToInt32(Convert.ToString(activityFrom[i])); //proobjId is selected atctivity id                
                relationalData.ProcessObjID = this.CInt32(ViewState["ProcessObjID"]);
                relationalData.NeighbourActivityID = prcobjID;
                relationalData.Type = 0; // type 0 for ActivityFrom 
                relationalData.ProcessID = ProcessId;
                resultFrom = ProcessData.SaveRelationshipData(relationalData);
                if (resultFrom == true)
                {
                }
            }
            bool resultTo = false;
            for (int i = 0; i < activityTo.Count; i++)
            {
                relationalData = new tbl_ParallelRelationship();
                int prcobjID = Convert.ToInt32(Convert.ToString(activityTo[i])); //proobjId is selected atctivity id                
                relationalData.ProcessObjID = this.CInt32(ViewState["ProcessObjID"]);
                relationalData.NeighbourActivityID = prcobjID;
                relationalData.Type = 1; // type 1 for ActivityTo 
                relationalData.ProcessID = ProcessId;
                resultTo = ProcessData.SaveRelationshipData(relationalData);
                if (resultTo == true)
                {
                }
            }

            lst.Clear();
            //load();
            //load("divProcess" + ProcessId, this.CInt32(ViewState["ProcessObjID"]), 20, 20);
            LoadallControls();


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

    public void ParallelTargetObjectWorkView()
    {
        tbl_TargetObject ProcessObj = new tbl_TargetObject();
        ProcessObj.TargetObjName = txtParallelProcessName.Text;
        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        ProcessObj.TargetID = ProcessId;
        ProcessObj.OrderNo = this.CInt32(ViewState["OrderNO"]);
        ProcessObj.ModifiedDate = DateTime.Now;
        ProcessObj.TargetObjID = this.CInt32(ViewState["TargetObjID"]);
        if (Session["SelectedTargetObjID"] != null)
            ProcessObj.ParallelTargetObjID = Convert.ToInt32(Session["SelectedTargetObjID"]);
        ProcessObj.XTop = 20;
        ProcessObj.YLeft = 20;
        ProcessObj.Title = "";


        ProcessObj.Width = 0;
        ProcessObj.Height = 0;


        bool result = false;
        result = TargetData.SaveParallelProcessObject(ProcessObj);
        VisualERPDataContext ObjData = new VisualERPDataContext();
        ObjData.SP_BulkInsertAttribute(this.CInt32(ViewState["TargetObjID"]), ProcessId, 2);
        if (result == true)
        {
            bool resultFrom = false;
            tbl_ParallelRelationship relationalData;
            for (int i = 0; i < activityFrom.Count; i++)
            {
                relationalData = new tbl_ParallelRelationship();
                int prcobjID = Convert.ToInt32(Convert.ToString(activityFrom[i])); //proobjId is selected atctivity id                
                relationalData.ProcessObjID = this.CInt32(ViewState["TargetObjID"]);
                relationalData.NeighbourActivityID = prcobjID;
                relationalData.Type = 0; // type 0 for ActivityFrom 
                relationalData.ProcessID = ProcessId;
                resultFrom = TargetData.SaveRelationshipData(relationalData);
                if (resultFrom == true)
                {
                }
            }
            bool resultTo = false;
            for (int i = 0; i < activityTo.Count; i++)
            {
                relationalData = new tbl_ParallelRelationship();
                int prcobjID = Convert.ToInt32(Convert.ToString(activityTo[i])); //proobjId is selected atctivity id                
                relationalData.ProcessObjID = this.CInt32(ViewState["TargetObjID"]);
                relationalData.NeighbourActivityID = prcobjID;
                relationalData.Type = 1; // type 1 for ActivityTo 
                relationalData.ProcessID = ProcessId;
                resultTo = ProcessData.SaveRelationshipData(relationalData);
                if (resultTo == true)
                {
                }
            }

            lst.Clear();
            //load();
            //load("divProcess" + ProcessId, this.CInt32(ViewState["ProcessObjID"]), 20, 20);
            LoadallControls();


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

    protected void imgCloseParallelP_OnClick(object sender, EventArgs e)
    {
        PnlParallelProcess.Visible = false;
        txtParallelProcessName.Text = string.Empty;
    }

    protected void imgCloseInventoryP_OnClick(object sender, EventArgs e)
    {
        Panel2.Visible = false;
        txtCT.Text = string.Empty;
        txtdoller.Text = string.Empty;
        txttime.Text = string.Empty;
    }

    /// <summary>
    /// it will bind the activity checkboxlist 
    /// </summary>
    /// <param name="PoID">processobject id </param>
    public void BindActivityCheckboxList(int PoID)
    {
        List<ProcessData.ProcessDataProperty> activities = ProcessData.GetProcessObjActvities(PoID);
        string src = Request.QueryString["src"];
        //if (!string.IsNullOrEmpty(src) && src == "tgt")
        //{
        //    activities = ProcessData.GetProcessObjActvities(PoID);
        //}

        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            activities = ProcessData.GetTargetObjActvities(PoID);
        }
        else
        {
            activities = ProcessData.GetProcessObjActvities(PoID);
        }





        if (activities.Count != 0)
        {
            lstRelational.Visible = true;
            activityNode.AddRange(activities);
            //
            chklstActivitiesFrom.DataSource = activityNode;
            chklstActivitiesFrom.DataTextField = "ProcessObjectName"; // datatext field
            chklstActivitiesFrom.DataValueField = "ProcessObjID"; // data value field
            chklstActivitiesFrom.DataBind(); // bind checkboxlist 


            chklstActivitiesTo.DataSource = activityNode;
            chklstActivitiesTo.DataTextField = "ProcessObjectName"; // datatext field
            chklstActivitiesTo.DataValueField = "ProcessObjID"; // data value field
            chklstActivitiesTo.DataBind(); // bind checkboxlist 

        }
        else
        {
            lstRelational.Visible = false;
        }
    }

    public void FillddlUnits(int PoID)
    {
        List<ProcessData.ProcessDataProperty> activities = new List<ProcessData.ProcessDataProperty>();
        string src = Request.QueryString["src"];
        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            activities = ProcessData.GetTargetObjActvities(PoID);
        }
        else
        {
            activities = ProcessData.GetProcessObjActvities(PoID);
        }


        ddlParallelActivity.Items.Clear();
        ddlParallelActivity.Items.Add(new ListItem("Select", "0"));
        foreach (ProcessData.ProcessDataProperty proData in activities)
        {
            ddlParallelActivity.Items.Add(new ListItem(proData.ProcessObjectName.ToString(), proData.ProcessObjID.ToString()));
        }
    }

    public void FillddlActivitySequence(int PoID)
    {
        List<ProcessData.ProcessDataProperty> activities = ProcessData.GetPObjActivitySequence(PoID);


        string src = Request.QueryString["src"];
        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            activities = ProcessData.GetTObjActivitySequence(PoID);
        }
        if (activities.Count > 0)
        {
            lstAddProcess.Visible = true;
            ddlNexttoActivity.Items.Clear();
            ddlNexttoActivity.Items.Add(new ListItem("None(Root Node)", "0"));
            foreach (ProcessData.ProcessDataProperty proData in activities)
            {
                ddlNexttoActivity.Items.Add(new ListItem(proData.ProcessObjectName.ToString(), proData.ProcessObjID.ToString()));
            }
        }
        else
        {
            lstAddProcess.Visible = false;
        }
    }




    public void FillddlInventoryNextTo(int PoID)
    {
        List<ProcessData.ProcessDataProperty> activities = ProcessData.GetPObjActivitySequence(PoID);
        string src = Request.QueryString["src"];
        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            activities = ProcessData.GetTObjActivitySequence(PoID);
        }
        if (activities.Count > 0)
        {
            lstInventoryNextTo.Visible = true;
            ddlInventoryNextTo.Items.Clear();
            ddlInventoryNextTo.Items.Add(new ListItem("None(Root Node)", "0"));
            foreach (ProcessData.ProcessDataProperty proData in activities)
            {
                ddlInventoryNextTo.Items.Add(new ListItem(proData.ProcessObjectName.ToString(), proData.ProcessObjID.ToString()));
            }
        }
        else
        {
            lstInventoryNextTo.Visible = false;
        }
    }

    protected void ddlParallelActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        chklstActivitiesFrom.Items.Clear();
        chklstActivitiesTo.Items.Clear();

        int selectPrObjid = Convert.ToInt32(ddlParallelActivity.SelectedItem.Value);
        List<ProcessData.ProcessDataProperty> activities = new List<ProcessData.ProcessDataProperty>();
        string src = Request.QueryString["src"];
        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            Session["SelectedTargetObjID"] = ddlParallelActivity.SelectedItem.Value;
            activities = ProcessData.GetTargetObjActvitiesToSelect(ProcessId, selectPrObjid);
        }
        else
        {
            Session["SelectedProcessObjID"] = ddlParallelActivity.SelectedItem.Value;
            activities = ProcessData.GetProcessObjActvitiesToSelect(ProcessId, selectPrObjid);
        }
        if (activities.Count != 0)
        {
            lstRelational.Visible = true;
            activityNode.AddRange(activities);
            //
            chklstActivitiesFrom.DataSource = activityNode;
            chklstActivitiesFrom.DataTextField = "ProcessObjectName"; // datatext field
            chklstActivitiesFrom.DataValueField = "ProcessObjID"; // data value field
            chklstActivitiesFrom.DataBind(); // bind checkboxlist 


            chklstActivitiesTo.DataSource = activityNode;
            chklstActivitiesTo.DataTextField = "ProcessObjectName"; // datatext field
            chklstActivitiesTo.DataValueField = "ProcessObjID"; // data value field
            chklstActivitiesTo.DataBind(); // bind checkboxlist 

        }
        else
        {
            lstRelational.Visible = false;
        }
        PnlParallelProcess.Visible = true;
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

    protected void lnkbtnArrow1_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow1, (int)ArrowControl.Arrow1);
    }

    protected void lnkbtnArrow2_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow2, (int)ArrowControl.Arrow2);
    }

    protected void lnkbtnArrow3_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow3, (int)ArrowControl.Arrow3);
    }

    protected void lnkbtnArrow4_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow4, (int)ArrowControl.Arrow4);
    }

    protected void lnkbtnArrow5_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow5, (int)ArrowControl.Arrow5);
    }

    protected void lnkbtnArrow6_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow6, (int)ArrowControl.Arrow6);
    }

    protected void lnkbtnArrow7_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow7, (int)ArrowControl.Arrow7);
    }

    protected void lnkbtnArrow8_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow8, (int)ArrowControl.Arrow8);
    }

    protected void lnkbtnArrow9_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow9, (int)ArrowControl.Arrow9);
    }

    protected void lnkbtnArrow10_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow10, (int)ArrowControl.Arrow10);
    }

    protected void lnkbtnArrow11_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow11, (int)ArrowControl.Arrow11);
    }

    protected void lnkbtnArrow12_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow12, (int)ArrowControl.Arrow12);
    }

    protected void lnkbtnArrow13_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow13, (int)ArrowControl.Arrow13);
    }

    protected void lnkbtnArrow14_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow14, (int)ArrowControl.Arrow14);
    }

    protected void lnkbtnArrow15_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow15, (int)ArrowControl.Arrow15);
    }

    protected void lnkbtnArrow16_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow16, (int)ArrowControl.Arrow16);
    }

    protected void lnkbtnArrow17_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow17, (int)ArrowControl.Arrow17);
    }

    protected void lnkbtnArrow18_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow18, (int)ArrowControl.Arrow18);
    }

    protected void lnkbtnArrow19_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow19, (int)ArrowControl.Arrow19);
    }

    protected void lnkbtnArrow20_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow20, (int)ArrowControl.Arrow20);
    }

    protected void lnkbtnArrow21_Click(object sender, EventArgs e)
    {
        AddArrowControlOn(ArrowControl.Arrow21, (int)ArrowControl.Arrow21);
    }

    public void AddArrowControlOn(ArrowControl arrControl, int ArrowType)
    {
        string title = string.Empty;
        SelectedType = Convert.ToString(arrControl);
        ViewState["SelectedType"] = SelectedType; //view state keep the selected arrow type 
        int outputID = InsertControlData(ArrowType, title); // insert record in database and get output id as processobjID
        string id = "divCArrow" + outputID;
        ArrowControlAdd(id, outputID, ArrowType, title); // add arrow control that is our user control
    }

    /// <summary>
    /// calling user control to make the user arrow for process relationship
    /// </summary>
    /// <param name="id">ControlId</param>
    /// <param name="InsertedID">EditID</param>
    /// <param name="type">arrow enum type</param>
    /// <param name="title">title if yes</param>
    public void ArrowControlAdd(string id, int InsertedID, int type, string title)
    {

        string src = Request.QueryString["src"];
        int sourceType = 1;
        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            sourceType = 2;
        }

        UserControls_ArrowControl arrowControl = LoadControl("UserControls/ArrowControl.ascx") as UserControls_ArrowControl;
        arrowControl.SourceType = sourceType;
        arrowControl.ControlId = id.Replace("d", "a");
        arrowControl.Type = type;
        arrowControl.ProcessObjectId = InsertedID;


        HtmlGenericControl div1 = new HtmlGenericControl("div"); // create a html div that will contains above table with id

        div1.ID = id; // creating div id that will be unique every time when we add process control
        //div1.Attributes["name"] = "ContentPlaceHolder1_" + id;
        div1.Attributes["name"] = id;  // id will maintain by div name
        div1.Attributes["style"] = "position: absolute; width:50px;height:50px;top:50px; left:50px;"; // add div style with given postion
        div1.Controls.Add(arrowControl);
        MainDiv1.Controls.Add(div1);


        newList.Add(new Supplier() { SupplierID = id, EditID = InsertedID, Top = 50, Left = 50, Type = type, Title = title });
        Session["Supplier"] = newList;


        int top = 50, left = 50;
        string script = "callready();";
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript", script, true);

        StringBuilder sb2 = new StringBuilder();
        string str2 = string.Empty;
        //if (Session["PosSupplier"] != null)
        //{
        //    str2 += Convert.ToString(Session["PosSupplier"]);
        //}
        if (!string.IsNullOrEmpty(hdnSupplier.Value))
        {
            // str2 += Convert.ToString(Session["PosSupplier"]);
            str2 += hdnSupplier.Value;
        }
        sb2.Append(id + "@" + top + "-" + left + ",");
        str2 += sb2.ToString();
        Session["PosSupplier"] = str2;
        hdnSupplier.Value = str2;
    }

    //*******************Add process between*****************//

    protected void btnAddProcessActivity_Click(object sender, EventArgs e)
    {
        //NextToActivityPosition();
        int ddlPoid = 0;
        int prePosition = 0;
        int nextPositionProcess = 0;
        if (ddlNexttoActivity.SelectedIndex > -1)
        {
            if (ddlNexttoActivity.SelectedItem.Value != "0")
            {
                ddlPoid = Convert.ToInt32(ddlNexttoActivity.SelectedItem.Value);
                prePosition = ProcessData.GetPositionByPoid(ddlPoid);
                nextPositionProcess = prePosition + 1; //addind position 1 in previous postion of process
            }
            else
            {
                nextPositionProcess = 1;
            }
        }

        else
        {
            nextPositionProcess = 1;
        }




        bool increased = false;
        string src = Request.QueryString["src"];
        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            increased = ProcessData.IncreasNextTargetRowsPosition(ProcessId, nextPositionProcess);
        }
        else
            increased = ProcessData.IncreasNextRowsPosition(ProcessId, nextPositionProcess);
        if (increased == true)
        {
            // position updated successfully
        }
        if (!string.IsNullOrEmpty(src) && src == "tgt")
        {
            SaveTargetObject();
            TargetObjectWorkView(nextPositionProcess);
        }
        else
        {
            SaveDummyProcesObject();
            ProcesObjectWorkView(nextPositionProcess);
        }
        CleareControl();
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

    private void SaveTargetObject()
    {
        tbl_TargetObject ProcessDummyObj = new tbl_TargetObject();
        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        ProcessDummyObj.TargetID = ProcessId;
        ProcessDummyObj.CreatedDate = DateTime.Now;

        // if()
        ProcessDummyObj.Type = 0;
        bool result = false;

        result = ProcessData.SaveDumyTargetObject(ProcessDummyObj);
        int OrderNO = 0;
        OrderNO = ProcessData.GetMaxTargetOrderID();

        ViewState["OrderNO"] = OrderNO;

        int ProcessObjId = 0;
        ProcessObjId = ProcessData.GetMaxTargetObjId(ProcessId);

        ViewState["TargetObjID"] = ProcessObjId;
    }

    public void ProcesObjectWorkView(int nextPositionProcess)
    {
        tbl_ProcessObject ProcessObj = new tbl_ProcessObject();
        // ProcessObj.ProcessObjName = "Activity-" + ViewState["OrderNO"];
        if (txtAddActivity.Text != "")
            ProcessObj.ProcessObjName = txtAddActivity.Text;
        else
            ProcessObj.ProcessObjName = "Activity-" + ViewState["OrderNO"];
        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        ProcessObj.ProcessID = ProcessId;
        ProcessObj.OrderNo = this.CInt32(ViewState["OrderNO"]); ;
        ProcessObj.ModifiedDate = DateTime.Now;
        ProcessObj.ProcessObjID = this.CInt32(ViewState["ProcessObjID"]);
        ProcessObj.XTop = 20;
        ProcessObj.YLeft = 20;
        ProcessObj.Title = "";
        ProcessObj.Width = 0;
        ProcessObj.Height = 0;
        ProcessObj.Position = nextPositionProcess;
        bool result = false;
        result = ProcessData.SaveProcessObject(ProcessObj);


        //increase all next process position by 1 in case of update       
        //get list of process after postion you just entered



        VisualERPDataContext ObjData = new VisualERPDataContext();
        ObjData.SP_BulkInsertAttribute(this.CInt32(ViewState["ProcessObjID"]), ProcessId, 1);
        if (result == true)
        {
            lst.Clear();

            LoadallControls();

        }
        else
        {
            string script = "alert(\"Error on saving data.!\");";
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "ServerControlScript", script, true);
        }


    }

    public void TargetObjectWorkView(int nextPositionProcess)
    {
        tbl_TargetObject ProcessObj = new tbl_TargetObject();
        // ProcessObj.ProcessObjName = "Activity-" + ViewState["OrderNO"];
        if (txtAddActivity.Text != "")
            ProcessObj.TargetObjName = txtAddActivity.Text;
        else
            ProcessObj.TargetObjName = "Activity-" + ViewState["OrderNO"];
        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        ProcessObj.TargetID = ProcessId;
        ProcessObj.OrderNo = this.CInt32(ViewState["OrderNO"]); ;
        ProcessObj.ModifiedDate = DateTime.Now;
        ProcessObj.TargetObjID = this.CInt32(ViewState["TargetObjID"]);
        ProcessObj.XTop = 20;
        ProcessObj.YLeft = 20;
        ProcessObj.Title = "";
        ProcessObj.Width = 0;
        ProcessObj.Height = 0;
        ProcessObj.Position = nextPositionProcess;
        bool result = false;
        result = ProcessData.SaveTargetObject(ProcessObj);


        //increase all next process position by 1 in case of update       
        //get list of process after postion you just entered



        VisualERPDataContext ObjData = new VisualERPDataContext();
        ObjData.SP_BulkInsertAttribute(this.CInt32(ViewState["TargetObjID"]), ProcessId, 2);
        if (result == true)
        {
            lst.Clear();

            LoadallControls();

        }
        else
        {
            string script = "alert(\"Error on saving data.!\");";
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "ServerControlScript", script, true);
        }


    }

    public void CleareControl()
    {
        ViewState["OrderNO"] = null;
        ViewState["ProcessObjID"] = null;
        ViewState["TargetObjID"] = null;
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
            string src = Request.QueryString["src"];
            if (!string.IsNullOrEmpty(src) && src == "tgt")
            {
                result = TargetData.DeleteProcessObjDataByID(processobjId);
            }
            else
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
            string src = Request.QueryString["src"];
            if (!string.IsNullOrEmpty(src) && src == "tgt")
            {
                int pid = TargetData.GetProcessIdByPoid(processobjId);

                int position = ProcessData.GetTPositionByPoid(processobjId);
                bool decrease = false;
                decrease = TargetData.DecreaseNextRowsPosition(pid, position);
                if (decrease == true)
                {
                    // position updated successfully
                }
                bool result = false;
                result = InventeryData.DeleteInventeryProcessObjByID(processobjId); ////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables
                bool result1 = false;
                result1 = TargetData.DeleteProcessObjDataByID(processobjId);
            }
            else
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

    }

    public void DeleteProcess()
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


                string src = Request.QueryString["src"];
                if (!string.IsNullOrEmpty(src) && src == "tgt")
                {
                    int PId = TargetData.GetProcessIdByPoid(processobjId);

                    bool IsParalled = false;
                    IsParalled = TargetData.IsParallelProcessByPoid(processobjId);
                    if (IsParalled == true)
                    {
                        bool resultP = false;
                        resultP = TargetData.DeleteParallelProcessObjDataByID(processobjId);////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables
                                                                                            // do nothing because no position define for parallel process
                    }
                    else
                    {
                        int position = TargetData.GetPositionByPoid(processobjId);
                        bool decrease = false;
                        decrease = TargetData.DecreaseNextRowsPosition(PId, position);
                        if (decrease == true)
                        {
                            // position updated successfully
                        }
                    }
                    bool result = false; bool result1 = false; bool resultSystemIO = false;

                    result = TargetData.DeleteProcessObjDataByID(processobjId);////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables           
                    result1 = TargetData.DeleteAttributedataByPoID(processobjId);
                    resultSystemIO = TargetData.DeleteSystemIODataByPoID(processobjId);
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
                    bool result = false; bool result1 = false; bool resultSystemIO = false;

                    result = ProcessData.DeleteProcessObjDataByID(processobjId);////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables           
                    result1 = ProcessData.DeleteAttributedataByPoID(processobjId);
                    resultSystemIO = ProcessData.DeleteSystemIODataByPoID(processobjId);
                }
                lblMsg.Text = "This activity has been deleted.";
                lblMsg.Style.Add("color", "green");
                divErrorMsg.Style.Add("float", "right");
                divErrorMsg.Style.Add("min-width", "200px");
                divErrorMsg.Style.Add("margin-left", "0px");
                divErrorMsg.Style.Add("margin-right", "460px");
                divErrorMsg.Attributes.Add("class", "isa_success");
            }
        }
    }

    public class TopHeightWidth
    {
        //public int Top { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}