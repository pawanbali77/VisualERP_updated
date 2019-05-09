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

public partial class EnterPriseManager : System.Web.UI.Page
{
    #region

    List<UserControl> lst = new List<UserControl>();
    List<UserControl> Plst = new List<UserControl>(); /////////////////
    List<TopnHeight> maxtopProcess = new List<TopnHeight>();

    int ProcessId = 0;
    string SystemName = "";
    string FunctionName = "";
    string FunctionNameProper = "";
    string ProcessObjName = "";
    int fromActivityId = 0;
    int ToActivityId = 0;
    int typeCheck = 0;
    string ProcessName = "";
    int j;
    #endregion
    delegate void DelMethodWithParam(string strProcessObjectId, string strAction);
    delegate void DelMethodWithoutParam();
    List<SummaryDetail> summaryResult = new List<SummaryDetail>(); // it will contain summary data for different functionid
    List<SummaryDetail> enterpriseSummary = new List<SummaryDetail>();

    DelMethodWithParam delParam;

    int ParallelProcessObjID = 0;////////////////////
    int top = 0; int left = 0; int width = 50; int height = 50; string title = ""; int DBID = 0; int type = 0; int pri = 0; int topAfter = 0;
    List<TopHeightWidth> maxHeightnWidth = new List<TopHeightWidth>(); // it will contains max height and width for any process
    protected void Page_Load(object sender, EventArgs e)
    {
        divErrorMsg.Visible = false;
        divSummary.Visible = false;
        Label lblManager = (Label)Master.FindControl("lblManager");
        lblManager.Text = "Enterprise View";
        lblManager.Attributes.Add("class", "Enterprize");


        //  ModelPopupMchUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
        AjaxControlToolkit.ModalPopupExtender PopupModelHandOff = (AjaxControlToolkit.ModalPopupExtender)HandOffUC1.FindControl("ModelPopupHandOff123");
        PopupModelHandOff.Hide();
        string script1 = "test();";
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript", script1, true);

        delParam = new DelMethodWithParam(MethodWithParam);
        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        else if (Session["SelectedNodeValue"] != null)
            ProcessId = this.CInt32(Session["SelectedNodeValue"]);

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
            DeleteProcess(ProcessId);
        }
        // load();

        List<TypeData.SystemTypeDetail> lstCollectProcessIds = TypeData.GetAllProcessId(this.CInt32(Session["SystemId"]));
        if (lstCollectProcessIds.Count > 0)
        {
            for (int k = 0; k < lstCollectProcessIds.Count; k++)
            {
                List<List<Object>> lst = new List<List<Object>>();
                List<int> lstProcessObj = new List<int>();
                TableRow trMain = new TableRow();

                ProcessId = this.CInt32(lstCollectProcessIds[k].ProcessID);

                if (ProcessId != 0)
                {
                    // liSummary.Visible = true;
                    string divId = "";
                    StringBuilder sb2 = new StringBuilder();
                    string str2 = string.Empty;
                    string str11 = string.Empty;
                    int sV = 0;
                    if (ViewState["SelectedValue"] != null)
                        sV = Convert.ToInt32(ViewState["SelectedValue"]);

                    var processData = ControlsData.GetAllProcessDataforEnterPrise(ProcessId);
                    if (processData.Count != 0)
                    {
                        int topP = Convert.ToInt32(processData[0].XTop.ToString());
                        if (k > 0)
                        {
                            //find max top value from list maxtopProcess
                            topAfter = maxtopProcess.Max(a => a.Top);
                            //find height for max top value from list maxtopProcess
                            int addHeight = (from m in maxtopProcess
                                             where m.Top == topAfter
                                             select m.Height).FirstOrDefault();
                            //add emptry row height and process box height into topAfter
                            topAfter += (200 + addHeight);
                            topP += topAfter;
                            maxtopProcess.Add(new TopnHeight() { Top = topP, Height = 325 });
                            if (processData.Count > 0)
                            {
                                int countProcess = processData.Count;
                                int widthlast = (countProcess * 500) + 220;  //220 is extra width to scroll summary table
                                maxHeightnWidth.Add(new TopHeightWidth() { Width = widthlast, Height = topP + 325 +200 }); //200px is extra height if input output icon present in the end 
                            }
                        }
                        else
                        {
                            if (processData.Count > 0)
                            {
                                int countProcess = processData.Count;
                                int widthlast = (countProcess * 500)+ 220; //220 is extra width to scroll summary table
                                maxHeightnWidth.Add(new TopHeightWidth() { Width = widthlast, Height = topP + 325 + 200 }); //200px is extra height if input output icon present in the end 
                            }
                            maxtopProcess.Add(new TopnHeight() { Top = topP, Height = 325 });

                        }
                        int leftP = Convert.ToInt32(processData[0].YLeft.ToString());
                        load("divProcess" + ProcessId + "", ProcessId, topP, leftP, k);
                    }

                    var paralleldata = ControlsData.GetAllParallelDataforEnterPrise(ProcessId);
                    if (paralleldata.Count != 0)
                    {
                        for (int i = 0; i < paralleldata.Count; i++)
                        {
                            ParallelProcessObjID = Convert.ToInt32(paralleldata[i].ParallelProcessObjID.ToString());  // get control parallel id
                            DBID = Convert.ToInt32(paralleldata[i].ProcessObjID.ToString()); // get control primary key id   
                            divId = GetDivid(type, DBID, ProcessId, ParallelProcessObjID);
                            top = Convert.ToInt32(paralleldata[i].XTop.ToString()); // get top position
                            left = Convert.ToInt32(paralleldata[i].YLeft.ToString()); // get left position
                            if (k > 0)
                            {
                                top += topAfter;
                                maxtopProcess.Add(new TopnHeight() { Top = top, Height = 325 });
                                maxHeightnWidth.Add(new TopHeightWidth() { Width = (left + 500 + 300), Height = (top + 400) }); //300 is extra width to scroll the summary table
                            }
                            else
                            {
                                maxtopProcess.Add(new TopnHeight() { Top = top, Height = 325 });
                                maxHeightnWidth.Add(new TopHeightWidth() { Width = (left + 500 + 300), Height = (top + 400) }); //300 is extra width to scroll the summary table
                            }

                            loadParallelActivity(divId, DBID, top, left, width, height, ParallelProcessObjID, ProcessId); //loading parallel Processes                            
                        }

                    }

                    var otherControlsData = ControlsData.GetAllOtherDataforEnterPrise(ProcessId); // get all controls record from database
                    //var listData = ControlsData.GetAllProcessControlData(ProcessId); // get all controls record from database

                    if (otherControlsData.Count != 0)
                    {
                        for (int i = 0; i < otherControlsData.Count; i++)
                        {
                            title = otherControlsData[i].Title.ToString();
                            type = Convert.ToInt32(otherControlsData[i].Type.ToString()); // get control type
                            DBID = Convert.ToInt32(otherControlsData[i].ProcessObjID.ToString()); // get control primary key id                              
                            divId = GetDivid(type, DBID, ProcessId, ParallelProcessObjID);
                            top = Convert.ToInt32(otherControlsData[i].XTop.ToString()); // get top position
                            left = Convert.ToInt32(otherControlsData[i].YLeft.ToString()); // get left position
                            width = Convert.ToInt32(otherControlsData[i].Width.ToString()); // get width
                            height = Convert.ToInt32(otherControlsData[i].Height.ToString()); // get height 
                            if (k > 0)
                            {
                                top += topAfter;
                                maxtopProcess.Add(new TopnHeight() { Top = top, Height = height });
                                maxHeightnWidth.Add(new TopHeightWidth() { Width = (left + width), Height = (top + height + 300) }); // 300 extra gap 
                            }
                            else
                            {
                                maxtopProcess.Add(new TopnHeight() { Top = top, Height = height });
                                maxHeightnWidth.Add(new TopHeightWidth() { Width = (left + width), Height = (top + height + 300) }); // 300 extra gap 
                            }
                            ControlPosition(divId, DBID, top, left, width, height, i, title, type, ParallelProcessObjID, ProcessId); // call function to creae control on last position

                        }
                        ViewState["SelectedValue"] = ProcessId;
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
                            ViewState["SelectedValue"] = ProcessId;
                        }
                        // liSummary.Visible = false;
                    }
                }
                else
                {
                    // liSummary.Visible = false;
                }
            }
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
            if (maxHeightnWidth.Count > 0)
            {
                int maxwidth = maxHeightnWidth.Max(a => a.Width);
                //find height for max top value from list maxtopProcess
                int maxheight = maxHeightnWidth.Max(a => a.Height);
                hdnWidth.Value = Convert.ToString(maxwidth);
                hdnheight.Value = Convert.ToString(maxheight);
            }

        }

        var modifiedhdval = hdnLastZoomE.Value;
        if (Session["lastZoomE"] != null || hdnLastZoomE.Value != string.Empty)
        {
            if (hdnLastZoomE.Value == string.Empty)
            {
                hdnLastZoomE.Value = (string)(Session["lastZoomE"]);
                modifiedhdval = hdnLastZoomE.Value;
            }

            Session["lastZoomE"] = hdnLastZoomE.Value;
            Page page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(typeof(Page), "ZoomRefreshE", "<script type='text/javascript'>ZoomRefresh('" + modifiedhdval + "');</script>");
        }

        if (Session["SystemId"] != null)
        {
            ResetBinding();
            liarrowDown.Visible = true;
            liarrowUp.Visible = true;
        }
        else
        {
            liarrowDown.Visible = false;
            liarrowUp.Visible = false;
        }

    }


    private void MethodWithParam(string strProcessObjectId, string strAction)
    {
        if (strAction == "attribute")
        {
            ModelPopupAttributeUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            AjaxControlToolkit.ModalPopupExtender PopupModelAttribute = (AjaxControlToolkit.ModalPopupExtender)ModelPopupAttributeUC1.FindControl("mopoExUser");
            PopupModelAttribute.Show();
        }
        if (strAction == "inputs")
        {
            ModelPopupInputUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            // UserControl UcInput = (UserControl)Page.FindControl("ModelPopupInputUC.ascx");
            AjaxControlToolkit.ModalPopupExtender PopupModelInput = (AjaxControlToolkit.ModalPopupExtender)ModelPopupInputUC1.FindControl("modelInput");
            PopupModelInput.Show();
        }
        if (strAction == "BOM")
        {
            ModelPopupBOMUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            AjaxControlToolkit.ModalPopupExtender PopupModelBOM = (AjaxControlToolkit.ModalPopupExtender)ModelPopupBOMUC1.FindControl("ModelBOM");
            PopupModelBOM.Show();

        }
        if (strAction == "TFG")
        {
            ModelPopupTFGUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            AjaxControlToolkit.ModalPopupExtender PopupModelTFG = (AjaxControlToolkit.ModalPopupExtender)ModelPopupTFGUC1.FindControl("ModelTFG");
            PopupModelTFG.Show();
        }

        if (strAction == "Machine")
        {
            ModelPopupMchUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            AjaxControlToolkit.ModalPopupExtender PopupModelMachine = (AjaxControlToolkit.ModalPopupExtender)ModelPopupMchUC1.FindControl("ModelMachine");
            PopupModelMachine.Show();
        }
        if (strAction == "HandOffOutput")
        {
            HandOffUC1.TypeIO = 1;
            HandOffUC1.SystemIOId = Convert.ToInt32(strProcessObjectId);
            //  ModelPopupMchUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            AjaxControlToolkit.ModalPopupExtender PopupModelHandOff = (AjaxControlToolkit.ModalPopupExtender)HandOffUC1.FindControl("ModelPopupHandOff123");
            PopupModelHandOff.Show();
        }
        if (strAction == "HandOffInput")
        {
            HandOffUC1.TypeIO = 0;
            HandOffUC1.SystemIOId = Convert.ToInt32(strProcessObjectId);
            //  ModelPopupMchUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            AjaxControlToolkit.ModalPopupExtender PopupModelHandOff = (AjaxControlToolkit.ModalPopupExtender)HandOffUC1.FindControl("ModelPopupHandOff123");
            PopupModelHandOff.Show();
        }
        //if (strAction == "Inventory")
        //{
        //    TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        //    if (mastertreeview.SelectedNode != null)
        //    {
        //        Session["SelectedNode"] = mastertreeview.SelectedNode.ValuePath;
        //        Session["SelectedNodeValue"] = mastertreeview.SelectedNode.Value;
        //    }
        //    HttpContext.Current.Response.Redirect("EnterPriseManager.aspx");
        //}

        if (strAction == "Activity")
        {
            ModelPopupActivityucA.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            // UserControl UcMachine = (UserControl)Page.FindControl("ModelPopupMchUC.ascx");
            AjaxControlToolkit.ModalPopupExtender PopupModelActivity = (AjaxControlToolkit.ModalPopupExtender)ModelPopupActivityucA.FindControl("ModelPopupActivity");
            PopupModelActivity.Show();
        }
    }

    public void load(string divProcessID, int ProcessId, int Top, int Left, int k)
    {
        List<List<Object>> lstN = new List<List<Object>>();
        List<int> lstProcessObj = new List<int>();
        TableRow trMain = new TableRow();
        lstN.Clear();

        VisualERPDataContext ObjData = new VisualERPDataContext();
        SystemName = ProcessData.GetProcessNameById(this.CInt32(Session["SystemId"]));
        lblSystemName.Text = SystemName.ToString();


        Table tblMain = new Table();
        tblMain.CellPadding = 0;
        tblMain.CellSpacing = 0;
        tblMain.Width = Unit.Percentage(100);
        tblMain.BorderWidth = 0;



        List<ProcessData.ProcessDataProperty> lstpoid = ProcessData.GetAllSingleProcessObjId(ProcessId);

        if (lstpoid.Count > 0)
        {
            if (k % 2 == 0)
            {

                TableCell tdFunction = new TableCell();
                trMain.Controls.Add(tdFunction);

                System.Web.UI.HtmlControls.HtmlGenericControl DivBlue = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                //DivBlue.ID = "DivBlue1";
                DivBlue.Attributes["class"] = "GreenStrip";
                tdFunction.Controls.Add(DivBlue);

                Table TblBlue = new Table();
                TblBlue.CellPadding = 0;
                TblBlue.CellSpacing = 0;
                TblBlue.BorderWidth = 0;
                DivBlue.Controls.Add(TblBlue);

                TableRow TrBlue = new TableRow();
                TblBlue.Controls.Add(TrBlue);


                TableCell TdBlue = new TableCell();
                TdBlue.Height = 313;
                TrBlue.Controls.Add(TdBlue);

                FunctionName = ProcessData.GetFunctionNameById(ProcessId);

                FunctionNameProper = ObjData.udf_PutSpacesBetweenChars(FunctionName.ToString());
                TdBlue.Text = FunctionNameProper.ToString();
            }
            else
            {
                TableCell tdFunction = new TableCell();
                trMain.Controls.Add(tdFunction);

                System.Web.UI.HtmlControls.HtmlGenericControl DivBlue = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                //DivBlue.ID = "DivBlue1";
                DivBlue.Attributes["class"] = "BlueStrip";
                tdFunction.Controls.Add(DivBlue);

                Table TblBlue = new Table();
                TblBlue.CellPadding = 0;
                TblBlue.CellSpacing = 0;
                TblBlue.BorderWidth = 0;
                DivBlue.Controls.Add(TblBlue);

                TableRow TrBlue = new TableRow();
                TblBlue.Controls.Add(TrBlue);


                TableCell TdBlue = new TableCell();
                TdBlue.Height = 313;
                TrBlue.Controls.Add(TdBlue);

                FunctionName = ProcessData.GetFunctionNameById(ProcessId);

                FunctionNameProper = ObjData.udf_PutSpacesBetweenChars(FunctionName.ToString());
                TdBlue.Text = FunctionNameProper.ToString();
            }


        }


        Table TblFirst = new Table();
        TblFirst.CellPadding = 0;
        TblFirst.CellSpacing = 0;
        TblFirst.Width = Unit.Percentage(100);
        TblFirst.BorderWidth = 0;
        TableRow TrFirst = new TableRow();
        //lstpoid = ProcessData.GetAllProcessObjId(ProcessId);

        int j = 0;
        for (int i = 0; i < lstpoid.Count; i++)
        {
            int Type = this.CInt32(lstpoid[i].Type);
            lstProcessObj.Add(lstpoid[i].ProcessObjID);
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
                    List<Object> lstPO = new List<Object>();
                    lstPO.Add(lstpoid[i].ProcessObjID);
                    lstPO.Add(xx);
                    lstN.Add(lstPO);
                    //lst.Add(xx);                          
                }
                else
                {
                    UserControl ucleftArrow = LoadControl("UserControls/AerrowObject.ascx") as UserControl;
                    List<Object> lstArrow = new List<Object>();
                    lstArrow.Add(0);
                    lstArrow.Add(ucleftArrow);
                    lstN.Add(lstArrow);

                    List<Object> lstPO = new List<Object>();
                    lstPO.Add(lstpoid[i].ProcessObjID);
                    lstPO.Add(xx);
                    lstN.Add(lstPO);

                    //UserControl ucleftArrow = LoadControl("UserControls/AerrowObject.ascx") as UserControl;
                    //lst.Add(ucleftArrow);
                    //lst.Add(xx);
                }

            }
            else
            {
                UserControls_InventeryObject xx = LoadControl("UserControls/InventeryObject.ascx") as UserControls_InventeryObject;
                xx.ProcessObjectId = this.CInt32(lstpoid[i].ProcessObjID);
                //xx.PageMethodWithParamRef = delParam;

                if (i == 0)
                {
                    List<Object> lstPO = new List<Object>();
                    lstPO.Add(lstpoid[i].ProcessObjID);
                    lstPO.Add(xx);
                    lstN.Add(lstPO);
                    //lst.Add(xx);
                }
                else
                {
                    UserControl ucleftArrow = LoadControl("UserControls/AerrowObject.ascx") as UserControl;
                    List<Object> lstArrow = new List<Object>();
                    lstArrow.Add(0);
                    lstArrow.Add(ucleftArrow);
                    lstN.Add(lstArrow);

                    List<Object> lstPO = new List<Object>();
                    lstPO.Add(lstpoid[i].ProcessObjID);
                    lstPO.Add(xx);
                    lstN.Add(lstPO);

                    //lst.Add(xx);
                }

            }
        }

        if (lstN.Count > 0)
        {
            foreach (List<Object> processObject in lstN)
            {
                int processObjectId = (int)processObject[0];
                UserControl uc = (UserControl)processObject[1];
                //pri++;
                if (uc.AppRelativeVirtualPath == "~/UserControls/ProcessObject.ascx")
                {

                    Table NewtblUC = new Table();
                    NewtblUC.CellPadding = 0;
                    NewtblUC.CellSpacing = 0;
                    NewtblUC.CssClass = "activity_block12";

                    // NewtblUC.Width = Unit.Percentage(100);

                    // FIRST ROW 
                    TableRow Newtr1UC = new TableRow();
                    TableRow Newtr2UC = new TableRow();
                    TableRow Newtr3UC = new TableRow();

                    TableCell Newtd1 = new TableCell();



                    // Newtd1.ColumnSpan = 2;
                    Newtd1.Controls.Add(uc);

                    Newtr1UC.Controls.Add(Newtd1);
                    NewtblUC.Controls.Add(Newtr1UC);


                    //SECOND ROW


                    List<HandOffData.ListHandOffData> lstHandObj = HandOffData.SystemIODataByActivityId(this.CInt32(processObjectId));
                    if (lstHandObj.Count > 0)
                    {
                        for (int m = 0; m < lstHandObj.Count; m++)
                        {
                            TableCell Newtd2 = new TableCell();
                            TableCell Newtd3 = new TableCell();
                            //Newtd2.Text = "ratnesh";
                            pri++;
                            fromActivityId = lstHandObj[m].FromActivityID;
                            ToActivityId = lstHandObj[m].ToActivityID;
                            typeCheck = lstHandObj[m].Type;
                            if (ToActivityId == processObjectId && typeCheck == 0)
                            {

                                UserControls_AerrowUpUc UCAerrowUP = LoadControl("UserControls/AerrowUpUc.ascx") as UserControls_AerrowUpUc;
                                UCAerrowUP.SystemIOID = this.CInt32(lstHandObj[m].SytemIOID);
                                UCAerrowUP.PageMethodWithParamRef = delParam;
                                Newtd2.Controls.Add(UCAerrowUP);
                                Label lblnewActivity = new Label();
                                lblnewActivity.ID = "lblAcitiviy" + pri;
                                fromActivityId = lstHandObj[m].FromActivityID;
                                ProcessObjName = ProcessData.GetProcessObjNameById(this.CInt32(fromActivityId));
                                ProcessName = ProcessData.GetProcessNameByPoid(this.CInt32(fromActivityId));
                                lblnewActivity.Text = ProcessName + "/" + ProcessObjName;
                                Newtd3.Controls.Add(lblnewActivity);

                            }
                            else if (fromActivityId == processObjectId && typeCheck == 1)
                            {
                                UserControls_AerrowDownUC UCAerrowDwn = LoadControl("UserControls/AerrowDownUC.ascx") as UserControls_AerrowDownUC;
                                UCAerrowDwn.SystemIOID = this.CInt32(lstHandObj[m].SytemIOID);
                                UCAerrowDwn.PageMethodWithParamRef = delParam;
                                Newtd2.Controls.Add(UCAerrowDwn);
                                ToActivityId = lstHandObj[m].ToActivityID;

                                Label lblnewActivity = new Label();
                                lblnewActivity.ID = "lblAcitiviy" + pri;
                                ToActivityId = lstHandObj[m].ToActivityID;
                                ProcessObjName = ProcessData.GetProcessObjNameById(this.CInt32(ToActivityId));
                                ProcessName = ProcessData.GetProcessNameByPoid(this.CInt32(ToActivityId));
                                lblnewActivity.Text = ProcessName + "/" + ProcessObjName;
                                Newtd3.Controls.Add(lblnewActivity);

                            }
                            else
                            {
                                if (ToActivityId == processObjectId)
                                {
                                    UserControls_AerrowUpUc UCAerrowUP = LoadControl("UserControls/AerrowUpUc.ascx") as UserControls_AerrowUpUc;
                                    UCAerrowUP.SystemIOID = this.CInt32(lstHandObj[m].SytemIOID);
                                    UCAerrowUP.PageMethodWithParamRef = delParam;
                                    Newtd2.Controls.Add(UCAerrowUP);
                                    Label lblnewActivity = new Label();
                                    lblnewActivity.ID = "lblAcitiviy" + pri;
                                    fromActivityId = lstHandObj[m].FromActivityID;
                                    ProcessObjName = ProcessData.GetProcessObjNameById(this.CInt32(fromActivityId));
                                    ProcessName = ProcessData.GetProcessNameByPoid(this.CInt32(fromActivityId));
                                    lblnewActivity.Text = ProcessName + "/" + ProcessObjName;
                                    Newtd3.Controls.Add(lblnewActivity);
                                }

                                else if (fromActivityId == processObjectId)
                                {
                                    UserControls_AerrowDownUC UCAerrowDwn = LoadControl("UserControls/AerrowDownUC.ascx") as UserControls_AerrowDownUC;
                                    UCAerrowDwn.SystemIOID = this.CInt32(lstHandObj[m].SytemIOID);
                                    UCAerrowDwn.PageMethodWithParamRef = delParam;
                                    Newtd2.Controls.Add(UCAerrowDwn);
                                    ToActivityId = lstHandObj[m].ToActivityID;

                                    Label lblnewActivity = new Label();
                                    lblnewActivity.ID = "lblAcitiviy" + pri;
                                    ToActivityId = lstHandObj[m].ToActivityID;
                                    ProcessObjName = ProcessData.GetProcessObjNameById(this.CInt32(ToActivityId));
                                    ProcessName = ProcessData.GetProcessNameByPoid(this.CInt32(ToActivityId));
                                    lblnewActivity.Text = ProcessName + "/" + ProcessObjName;
                                    Newtd3.Controls.Add(lblnewActivity);
                                }

                            }

                            Newtr2UC.Controls.Add(Newtd2);
                            Newtr3UC.Controls.Add(Newtd3);
                        }
                    }
                    else
                    {
                        TableCell Newtd2 = new TableCell();
                        TableCell Newtd3 = new TableCell();
                        Newtd2.Height = 45;
                        Newtd3.Height = 15;
                        Newtr2UC.Controls.Add(Newtd2);
                        Newtr3UC.Controls.Add(Newtd3);
                    }

                    Newtd1.ColumnSpan = lstHandObj.Count;
                    NewtblUC.Controls.Add(Newtr2UC);
                    NewtblUC.Controls.Add(Newtr3UC);

                    TableCell td = new TableCell();
                    td.Controls.Add(NewtblUC);
                    TrFirst.Controls.Add(td);
                }

                else
                {
                    TableCell td = new TableCell();
                    td.Controls.Add(uc);
                    TrFirst.Controls.Add(td);
                }

            }
            TblFirst.Controls.Add(TrFirst);
        }

        TableRow tblRowBlnk = new TableRow();
        TableCell tdRowBlnk = new TableCell();
        tdRowBlnk.ColumnSpan = 2;
        tdRowBlnk.CssClass = "tdRowBlnkClass";
        tblRowBlnk.Controls.Add(tdRowBlnk);
        //tdRowBlnk.Controls.Add("");

        TableCell tdMain = new TableCell();
        tdMain.Controls.Add(TblFirst);
        trMain.Controls.Add(tdMain);


        tblMain.Controls.Add(trMain);
        tblMain.Controls.Add(tblRowBlnk);
        //MainDiv.Controls.Add(TblFirst);
        //MainDivRole.Controls.Add(MiddleDiv);
        //this.DataBind();


        HtmlGenericControl div1 = new HtmlGenericControl("div"); // create a html div that will contains above table with id

        div1.ID = divProcessID; // creating div id that will be unique every time when we add process control
        div1.Attributes["name"] = "ContentPlaceHolder1_" + divProcessID;
        div1.Attributes["name"] = divProcessID;  // id will maintain by div name
        div1.Attributes["style"] = "Position:absolute;width:400px;height:850px;top: " + Top + "px; left: " + Left + "px;"; // add div style with given postion
        div1.Controls.Add(tblMain);
        MainDiv.Controls.Add(div1);




        ////////////add here parallel processes and other controls as well

    }

    public void loadParallelActivity(string divProcessID, int DBID, int Top, int Left, int width, int height, int ParallelProcessObjID, int Pid)
    {
        List<List<Object>> lstNP = new List<List<Object>>();
        List<int> lstProcessObjP = new List<int>();
        TableRow trMainP = new TableRow();
        lstNP.Clear();

        VisualERPDataContext ObjData = new VisualERPDataContext();
        Table tblMain = new Table();
        tblMain.CellPadding = 0;
        tblMain.CellSpacing = 0;
        tblMain.Width = Unit.Percentage(100);
        tblMain.BorderWidth = 0;


        Table TblFirstP = new Table();
        TblFirstP.CellPadding = 0;
        TblFirstP.CellSpacing = 0;
        TblFirstP.Width = Unit.Percentage(100);
        TblFirstP.BorderWidth = 0;
        TableRow TrFirst = new TableRow();

        int j = 0;

        lstProcessObjP.Add(DBID);

        j++;
        UserControls_ProcessObject xx = LoadControl("UserControls/ProcessObject.ascx") as UserControls_ProcessObject;
        //ModelPopupBOMUC1.Index = j;
        xx.Index = j;
        xx.ProcessObjectId = DBID;
        xx.PageMethodWithParamRef = delParam;

        List<Object> lstPO = new List<Object>();

        lstPO.Add(DBID);
        lstPO.Add(xx);
        lstNP.Add(lstPO);

        if (lstNP.Count > 0)
        {
            foreach (List<Object> processObject in lstNP)
            {
                int processObjectId = (int)processObject[0];
                UserControl uc = (UserControl)processObject[1];
                //pri++;
                if (uc.AppRelativeVirtualPath == "~/UserControls/ProcessObject.ascx")
                {

                    Table NewtblUC = new Table();
                    NewtblUC.CellPadding = 0;
                    NewtblUC.CellSpacing = 0;
                    NewtblUC.CssClass = "activity_block12";

                    // NewtblUC.Width = Unit.Percentage(100);

                    // FIRST ROW 
                    TableRow Newtr1UC = new TableRow();
                    TableRow Newtr2UC = new TableRow();
                    TableRow Newtr3UC = new TableRow();

                    TableCell Newtd1 = new TableCell();



                    // Newtd1.ColumnSpan = 2;
                    Newtd1.Controls.Add(uc);

                    Newtr1UC.Controls.Add(Newtd1);
                    NewtblUC.Controls.Add(Newtr1UC);


                    //SECOND ROW


                    List<HandOffData.ListHandOffData> lstHandObj = HandOffData.SystemIODataByActivityId(this.CInt32(processObjectId));
                    if (lstHandObj.Count > 0)
                    {
                        for (int m = 0; m < lstHandObj.Count; m++)
                        {
                            TableCell Newtd2 = new TableCell();
                            TableCell Newtd3 = new TableCell();
                            //Newtd2.Text = "ratnesh";
                            pri++;
                            fromActivityId = lstHandObj[m].FromActivityID;
                            ToActivityId = lstHandObj[m].ToActivityID;
                            typeCheck = lstHandObj[m].Type;
                            if (ToActivityId == processObjectId && typeCheck == 0)
                            {

                                UserControls_AerrowUpUc UCAerrowUP = LoadControl("UserControls/AerrowUpUc.ascx") as UserControls_AerrowUpUc;
                                UCAerrowUP.SystemIOID = this.CInt32(lstHandObj[m].SytemIOID);
                                UCAerrowUP.PageMethodWithParamRef = delParam;
                                Newtd2.Controls.Add(UCAerrowUP);
                                Label lblnewActivity = new Label();
                                lblnewActivity.ID = "lblAcitiviy" + pri;
                                fromActivityId = lstHandObj[m].FromActivityID;
                                ProcessObjName = ProcessData.GetProcessObjNameById(this.CInt32(fromActivityId));
                                ProcessName = ProcessData.GetProcessNameByPoid(this.CInt32(fromActivityId));
                                lblnewActivity.Text = ProcessName + "/" + ProcessObjName;
                                Newtd3.Controls.Add(lblnewActivity);

                            }
                            else if (fromActivityId == processObjectId && typeCheck == 1)
                            {
                                UserControls_AerrowDownUC UCAerrowDwn = LoadControl("UserControls/AerrowDownUC.ascx") as UserControls_AerrowDownUC;
                                UCAerrowDwn.SystemIOID = this.CInt32(lstHandObj[m].SytemIOID);
                                UCAerrowDwn.PageMethodWithParamRef = delParam;
                                Newtd2.Controls.Add(UCAerrowDwn);
                                ToActivityId = lstHandObj[m].ToActivityID;

                                Label lblnewActivity = new Label();
                                lblnewActivity.ID = "lblAcitiviy" + pri;
                                ToActivityId = lstHandObj[m].ToActivityID;
                                ProcessObjName = ProcessData.GetProcessObjNameById(this.CInt32(ToActivityId));
                                ProcessName = ProcessData.GetProcessNameByPoid(this.CInt32(ToActivityId));
                                lblnewActivity.Text = ProcessName + "/" + ProcessObjName;
                                Newtd3.Controls.Add(lblnewActivity);

                            }
                            else
                            {
                                if (ToActivityId == processObjectId)
                                {
                                    UserControls_AerrowUpUc UCAerrowUP = LoadControl("UserControls/AerrowUpUc.ascx") as UserControls_AerrowUpUc;
                                    UCAerrowUP.SystemIOID = this.CInt32(lstHandObj[m].SytemIOID);
                                    UCAerrowUP.PageMethodWithParamRef = delParam;
                                    Newtd2.Controls.Add(UCAerrowUP);
                                    Label lblnewActivity = new Label();
                                    lblnewActivity.ID = "lblAcitiviy" + pri;
                                    fromActivityId = lstHandObj[m].FromActivityID;
                                    ProcessObjName = ProcessData.GetProcessObjNameById(this.CInt32(fromActivityId));
                                    ProcessName = ProcessData.GetProcessNameByPoid(this.CInt32(fromActivityId));
                                    lblnewActivity.Text = ProcessName + "/" + ProcessObjName;
                                    Newtd3.Controls.Add(lblnewActivity);
                                }

                                else if (fromActivityId == processObjectId)
                                {
                                    UserControls_AerrowDownUC UCAerrowDwn = LoadControl("UserControls/AerrowDownUC.ascx") as UserControls_AerrowDownUC;
                                    UCAerrowDwn.SystemIOID = this.CInt32(lstHandObj[m].SytemIOID);
                                    UCAerrowDwn.PageMethodWithParamRef = delParam;
                                    Newtd2.Controls.Add(UCAerrowDwn);
                                    ToActivityId = lstHandObj[m].ToActivityID;

                                    Label lblnewActivity = new Label();
                                    lblnewActivity.ID = "lblAcitiviy" + pri;
                                    ToActivityId = lstHandObj[m].ToActivityID;
                                    ProcessObjName = ProcessData.GetProcessObjNameById(this.CInt32(ToActivityId));
                                    ProcessName = ProcessData.GetProcessNameByPoid(this.CInt32(ToActivityId));
                                    lblnewActivity.Text = ProcessName + "/" + ProcessObjName;
                                    Newtd3.Controls.Add(lblnewActivity);
                                }

                            }

                            Newtr2UC.Controls.Add(Newtd2);
                            Newtr3UC.Controls.Add(Newtd3);
                        }
                    }
                    else
                    {
                        TableCell Newtd2 = new TableCell();
                        TableCell Newtd3 = new TableCell();
                        Newtd2.Height = 45;
                        Newtd3.Height = 15;
                        Newtr2UC.Controls.Add(Newtd2);
                        Newtr3UC.Controls.Add(Newtd3);
                    }

                    Newtd1.ColumnSpan = lstHandObj.Count;
                    NewtblUC.Controls.Add(Newtr2UC);
                    NewtblUC.Controls.Add(Newtr3UC);

                    TableCell td = new TableCell();
                    td.Controls.Add(NewtblUC);
                    TrFirst.Controls.Add(td);
                }

                else
                {
                    TableCell td = new TableCell();
                    td.Controls.Add(uc);
                    TrFirst.Controls.Add(td);
                }

            }
            TblFirstP.Controls.Add(TrFirst);
        }

        TableRow tblRowBlnk = new TableRow();
        TableCell tdRowBlnk = new TableCell();
        tdRowBlnk.ColumnSpan = 2;
        tdRowBlnk.CssClass = "tdRowBlnkClass";
        tblRowBlnk.Controls.Add(tdRowBlnk);
        //tdRowBlnk.Controls.Add("");

        TableCell tdMain = new TableCell();
        tdMain.Controls.Add(TblFirstP);
        trMainP.Controls.Add(tdMain);


        tblMain.Controls.Add(trMainP);
        tblMain.Controls.Add(tblRowBlnk);
        //MainDiv.Controls.Add(TblFirst);
        //MainDivRole.Controls.Add(MiddleDiv);
        //this.DataBind();


        HtmlGenericControl div1 = new HtmlGenericControl("div"); // create a html div that will contains above table with id

        div1.ID = divProcessID; // creating div id that will be unique every time when we add process control
        div1.Attributes["name"] = "ContentPlaceHolder1_" + divProcessID;
        div1.Attributes["name"] = divProcessID;  // id will maintain by div name
        div1.Attributes["style"] = "Position:absolute;width:400px;height:850px;top: " + Top + "px; left: " + Left + "px;"; // add div style with given postion
        div1.Controls.Add(tblMain);
        MainDiv.Controls.Add(div1);

    }

    public void ResetBinding()
    {
        if (Session["SystemId"] != null)
        {
            ViewState["sortBy"] = "CreatedDate";
            ViewState["isAsc"] = "1";
            BindGridSummary(this.CInt32(Session["SystemId"]));
        }//bind gridview sorted by Created Date
        else
        {
            divSummary.Visible = false;
        }
    }

    //public void BindGridSummary1(int SystemId)
    //{
    //    List<TypeData.SystemTypeDetail> lstCollectProcessIds = TypeData.GetAllProcessId(SystemId);
    //    List<ProcessData.ProcessDataProperty> lst = new List<ProcessData.ProcessDataProperty>();
    //    if (lstCollectProcessIds.Count > 0)
    //    {
    //        for (int k = 0; k < lstCollectProcessIds.Count; k++)
    //        {

    //            ProcessID = this.CInt32(lstCollectProcessIds[k].ProcessID);
    //            List<ProcessData.ProcessDataProperty> prop = ProcessData.GetSummaryData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ProcessID));

    //            if (prop.Count != 0)
    //            {
    //                foreach (ProcessData.ProcessDataProperty property in prop)
    //                    lst.Add(property);
    //            }

    //        }
    //        divSummary.Visible = true;
    //        gridProcessSummary.DataSource = lst;
    //        gridProcessSummary.DataBind();
    //    }
    //    else
    //    {
    //        divSummary.Visible = false;
    //    }




    //}

    public void BindGridSummary(int SystemId)
    {
        List<TypeData.SystemTypeDetail> lstCollectProcessIds = TypeData.GetAllProcessId(SystemId);
        List<ProcessData.ProcessDataProperty> lst = new List<ProcessData.ProcessDataProperty>();

        if (enterpriseSummary.Count > 0)
        {
        }
        else
        {
            if (lstCollectProcessIds.Count > 0)
            {
                for (int A = 0; A < lstCollectProcessIds.Count; A++)
                {
                    ProcessId = this.CInt32(lstCollectProcessIds[A].ProcessID);
                    if (ProcessData.GetSummaryData(ProcessId))
                    {
                        List<ProcessData.ProcessDataProperty> record = ProcessData.GetSummaryTableRecord(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ProcessId));
                        if (record.Count != 0)
                        {
                            for (int k = 0; k < record.Count; k++)
                            {
                                string AttributeName = Convert.ToString(record[k].AttributeName);
                                //string unitName = ProcessData.GetUnitName(AttributeName);
                                string unitName = Convert.ToString(record[k].UnitName);
                                int FunctionID = Convert.ToInt32(record[k].FunctionID);
                                List<ProcessData.ProcessDataProperty> res = ProcessData.GetAttributeValue(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ProcessId), AttributeName, unitName);
                                if (res.Count > 0)
                                {
                                    if (FunctionID == 1)
                                    {
                                        int sum = res.Sum(x => Convert.ToInt32(x.AttributeValueSum)); // get Sum here
                                        // add attributename,value, unitname in list to display it in summary table
                                        summaryResult.Add(new SummaryDetail() { AttributeName = AttributeName, AttributeValueResult = Convert.ToString(sum), UnitName = unitName, FunctionID = 1, ProcessID = ProcessId });
                                    }

                                    if (FunctionID == 2)
                                    {
                                        double average = res.Average(x => Convert.ToInt32(x.AttributeValueSum)); // get Average here
                                        // add attributename,value, unitname in list to display it in summary table
                                        summaryResult.Add(new SummaryDetail() { AttributeName = AttributeName, AttributeValueResult = String.Format("{0:0.00}", average), UnitName = unitName, FunctionID = 2, ProcessID = ProcessId });
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
                                        // int[] numbers = { 1, 2, 4, 7 }; //median will be 3 

                                        int numberCount = numbers.Count(); // count no of values from array
                                        int halfIndex = numbers.Count() / 2; // half of count
                                        //var sortedNumbers = numbers.OrderBy(n => n); // sorted numbers
                                        var sortedNumbers = numbers.OrderBy(c => c).ToArray();
                                        double median;
                                        if ((numberCount % 2) == 0)
                                        {
                                            int ElementFirst = sortedNumbers.ElementAt(halfIndex - 1);
                                            int ElementSecond = sortedNumbers.ElementAt(halfIndex);
                                            int addElemtn = ElementFirst + ElementSecond;
                                            median = (double)addElemtn / 2;
                                            //median = ((sortedNumbers.ElementAt(halfIndex) +
                                            //    sortedNumbers.ElementAt((halfIndex - 1))) / 2);
                                        }
                                        else
                                        {
                                            median = sortedNumbers.ElementAt(halfIndex);
                                        }
                                        // int total = Convert.ToInt32(median); // get median here
                                        // add attributename,value, unitname in list to display it in summary table
                                        summaryResult.Add(new SummaryDetail() { AttributeName = AttributeName, AttributeValueResult = String.Format("{0:0.00}", median), UnitName = unitName, FunctionID = 3, ProcessID = ProcessId });
                                    }

                                    if (FunctionID == 4)
                                    {
                                        int total = res.Min(x => Convert.ToInt32(x.AttributeValueSum)); // get min here
                                        // add attributename,value, unitname in list to display it in summary table
                                        summaryResult.Add(new SummaryDetail() { AttributeName = AttributeName, AttributeValueResult = Convert.ToString(total), UnitName = unitName, FunctionID = 4, ProcessID = ProcessId });
                                    }

                                    if (FunctionID == 5)
                                    {
                                        int total = res.Max(x => Convert.ToInt32(x.AttributeValueSum)); // get max here
                                        // add attributename,value, unitname in list to display it in summary table
                                        summaryResult.Add(new SummaryDetail() { AttributeName = AttributeName, AttributeValueResult = Convert.ToString(total), UnitName = unitName, FunctionID = 5, ProcessID = ProcessId });
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
                                            // int total = Convert.ToInt32(ret);
                                            // add attributename,value, unitname in list to display it in summary table
                                            summaryResult.Add(new SummaryDetail() { AttributeName = AttributeName, AttributeValueResult = String.Format("{0:0.00}", ret), UnitName = unitName, FunctionID = 6, ProcessID = ProcessId });
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                // summary data table will be created here for each process selected in enterprise manager now for each -
                //process id take distinct attribute and display finally in summary table
                string attName = string.Empty;
                string unitname = string.Empty;

                var result = (from x in summaryResult
                              group new { x } by new { x.AttributeName, x.FunctionID, x.UnitName } into g
                              select new SummaryDetail
                               {
                                   AttributeName = g.Key.AttributeName,
                                   FunctionID = g.Key.FunctionID,
                                   UnitName = g.Key.UnitName,

                               }).OrderBy(a => a.AttributeName).ToList();
                if (result.Count > 0)
                {
                    //for (int p = 0; p < lstCollectProcessIds.Count; p++)
                    //{
                    //    ProcessId = this.CInt32(lstCollectProcessIds[p].ProcessID);
                    //    lst.AddRange(ProcessData.GetEnterpriseSystemData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), SystemId, ProcessId));
                    //}
                    var summaryProcessId = (from x in summaryResult
                                            group new { x } by new { x.ProcessID } into g
                                            select new SummaryDetail
                                            {
                                                ProcessID = g.Key.ProcessID

                                            }).OrderBy(a => a.ProcessID).ToList();
                    for (int p = 0; p < summaryProcessId.Count; p++)
                    {
                        ProcessId = this.CInt32(summaryProcessId[p].ProcessID);
                        lst.AddRange(ProcessData.GetEnterpriseSystemData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), SystemId, ProcessId));
                    }
                    for (int u = 0; u < result.Count; u++)
                    {
                        string attrname = result[u].AttributeName;
                        int funcid = Convert.ToInt32(result[u].FunctionID);
                        //string unitName = ProcessData.GetUnitName(attrname);
                        string unitName = result[u].UnitName;
                        int prssid = result[u].ProcessID;

                        //lst = ProcessData.GetEnterpriseSystemData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), SystemId, prssid);

                        List<ProcessData.ProcessDataProperty> enterpriseS = (from r in lst
                                                                             where r.AttributeName == attrname && r.UnitName == unitName
                                                                             select r).ToList();
                        if (enterpriseS.Count > 0)
                        {
                            if (funcid == 1)
                            {
                                int sum = enterpriseS.Sum(x => Convert.ToInt32(x.AttributeValue)); // get Sum here
                                // add attributename,value, unitname in list to display it in summary table
                                enterpriseSummary.Add(new SummaryDetail() { AttributeName = attrname, AttributeValueResult = Convert.ToString(sum), UnitName = unitName, FunctionID = 1 });
                            }

                            if (funcid == 2)
                            {
                                double average = enterpriseS.Average(x => Convert.ToInt32(x.AttributeValue)); // get Average here
                                // add attributename,value, unitname in list to display it in summary table
                                enterpriseSummary.Add(new SummaryDetail() { AttributeName = attrname, AttributeValueResult = String.Format("{0:0.00}", average), UnitName = unitName, FunctionID = 2 });
                            }

                            if (funcid == 3)
                            {
                                /****************Median formula ******************/
                                int[] numbers = new int[enterpriseS.Count]; // initialize int array with max record in list
                                for (int j = 0; j < enterpriseS.Count; j++)
                                {
                                    numbers[j] = Convert.ToInt32(enterpriseS[j].AttributeValue); // make int[] array for all value in list
                                }

                                // int[] numbers = { 13, 18, 13, 14, 13, 16, 14, 21, 13 }; // median will be 14
                                // int[] numbers = { 1, 2, 4, 7 }; //median will be 3 

                                int numberCount = numbers.Count(); // count no of values from array
                                int halfIndex = numbers.Count() / 2; // half of count
                                //var sortedNumbers = numbers.OrderBy(n => n); // sorted numbers
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
                                enterpriseSummary.Add(new SummaryDetail() { AttributeName = attrname, AttributeValueResult = String.Format("{0:0.00}", median), UnitName = unitName, FunctionID = 3 });
                            }

                            if (funcid == 4)
                            {
                                int total = enterpriseS.Min(x => Convert.ToInt32(x.AttributeValue)); // get min here
                                // add attributename,value, unitname in list to display it in summary table
                                enterpriseSummary.Add(new SummaryDetail() { AttributeName = attrname, AttributeValueResult = Convert.ToString(total), UnitName = unitName, FunctionID = 4 });
                            }

                            if (funcid == 5)
                            {
                                int total = enterpriseS.Max(x => Convert.ToInt32(x.AttributeValue)); // get max here
                                // add attributename,value, unitname in list to display it in summary table
                                enterpriseSummary.Add(new SummaryDetail() { AttributeName = attrname, AttributeValueResult = Convert.ToString(total), UnitName = unitName, FunctionID = 5 });
                            }

                            if (funcid == 6)
                            {

                                //***************************standard deviation formula******************/

                                int[] numA = new int[enterpriseS.Count]; // int[] array get all values from list in int[] array
                                for (int j = 0; j < enterpriseS.Count; j++)
                                {
                                    numA[j] = Convert.ToInt32(enterpriseS[j].AttributeValue);
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
                                    enterpriseSummary.Add(new SummaryDetail() { AttributeName = attrname, AttributeValueResult = String.Format("{0:0.00}", ret), UnitName = unitName, FunctionID = 6 });
                                }
                            }
                        }

                    }
                }

                if (enterpriseSummary.Count > 0)
                {
                    gridProcessSummary.DataSource = enterpriseSummary;
                    gridProcessSummary.DataBind();
                    divSummary.Visible = true;
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
        BindGridSummary(this.CInt32(Session["SystemId"]));
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


    protected void lnkbtnDown_Click(object sender, EventArgs e)
    {
        // Response.Redirect("EnterPriseManager.aspx");
        //ModelPopupAttributeUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
        // UserControl UcAerrowUpDown = (UserControl)Page.FindControl("AerrowUpDown.ascx");
        AjaxControlToolkit.ModalPopupExtender PopupModelAerrow = (AjaxControlToolkit.ModalPopupExtender)AerrowUpDown1.FindControl("ModelPopupAerrow");
        PopupModelAerrow.Show();
        Session["TypeID"] = 1;
    }
    protected void lnkbtnUp_Click(object sender, EventArgs e)
    {

        //ModelPopupAttributeUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
        // UserControl UcAerrowUpDown = (UserControl)Page.FindControl("AerrowUpDown.ascx");
        AjaxControlToolkit.ModalPopupExtender PopupModelAerrow = (AjaxControlToolkit.ModalPopupExtender)AerrowUpDown1.FindControl("ModelPopupAerrow");
        PopupModelAerrow.Show();
        Session["TypeID"] = 0;
    }

    public class SummaryDetail
    {
        public string AttributeName { get; set; }
        // public int AttributeValueResult { get; set; }
        public string AttributeValueResult { get; set; }
        public string UnitName { get; set; }
        public int FunctionID { get; set; }
        public int ProcessID { get; set; }
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

    public void ControlPosition(string id, int DBID, int top, int left, int width, int height, int i, string title, int type, int ParallelProcessObjID, int pid)
    {
        if (id.Contains("divParallelProcess") && ParallelProcessObjID != 0)
        {
            //loadParallelActivity(id, DBID, top, left, width, height, type, ParallelProcessObjID, pid);
        }
        else if (type > 1 && type < 30)
        {
            UserControls_ImageControl imageControl = LoadControl("UserControls/ImageControl.ascx") as UserControls_ImageControl;//load supplier control           
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


        }
        else if (id.Contains("divCArrow"))
        {

            UserControls_ArrowControl arrowControl = LoadControl("UserControls/ArrowControl.ascx") as UserControls_ArrowControl;
            arrowControl.ControlId = id;
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

        }

    }

    public class TopnHeight
    {
        public int Top { get; set; }
        public int Height { get; set; }
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

                bool result = false; bool result1 = false; bool resultSystemIO = false; bool deleteSummaryData = false;


                //join poid data with summary data and delete join attribute data from summarydata
                deleteSummaryData = ProcessData.DeleteSummaryDataByPoid(processobjId, ProcessId);

                result = ProcessData.DeleteProcessObjDataByID(processobjId); ////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables            
                result1 = ProcessData.DeleteAttributedataByPoID(processobjId);
                resultSystemIO = ProcessData.DeleteSystemIODataByPoID(processobjId);
                lblMsg.Text = "This activity has been deleted.";
                lblMsg.Style.Add("color", "green");
                divErrorMsg.Style.Add("min-width", "200px");
                divErrorMsg.Style.Add("margin-left", "330px");
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