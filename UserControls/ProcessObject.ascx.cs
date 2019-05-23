using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class UserControls_ProcessObject : System.Web.UI.UserControl
{
    private System.Delegate _delWithParam;
    public Delegate PageMethodWithParamRef
    {
        set { _delWithParam = value; }
    }

    private System.Delegate _delNoParam;
    public Delegate PageMethodWithNoParamRef
    {
        set { _delNoParam = value; }
    }


    [BrowsableAttribute(true)]
    public int ProcessObjectId
    {
        get { return 0; }
        set { ResetBinding(value); }
    }

    [BrowsableAttribute(true)]
    public int Index
    {
        set { ViewState["Index"] = value; }
    }

    private int _sourceTypeID=1;
    [BrowsableAttribute(true)]
    public int SourceType
    {
        get { return _sourceTypeID; }
        set { _sourceTypeID=value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        //ResetBinding();
      
            string absolutepath = Request.Url.AbsolutePath;
        string returnurl = absolutepath.Substring(absolutepath.LastIndexOf('/') + 1);
        if (returnurl == "Production.aspx")
        {
            lnkbtn.Enabled = false;
            attribute.Attributes.Add("class", "th_fieldsiPro left");          //disable attribute link button and remove hover class  
            lnkBtnInput.Enabled = false;
            Input.Attributes.Add("class", "th_fieldsiPro"); //disable attribute link button and remove hover class 
            lnkBtnBOM.Enabled = false;
            BOM.Attributes.Add("class", "th_fieldsiPro");   //disable attribute link button and remove hover class 
            lnkBtnTFG.Enabled = false;
            TFG.Attributes.Add("class", "th_fieldsiPro");  //disable attribute link button and remove hover class 
            lnkBtnMachine.Enabled = false;
            Machine.Attributes.Add("class", "th_fieldsiPro");  //disable attribute link button and remove hover class 
            lblOrderNo.Enabled = false;
            ErrorReport.Attributes.Add("class", "th_fieldsiPro right last");  //disable attribute link button and remove hover class 
            lnkbtnErrorReport.Enabled = false;
            
        }
        if ((returnurl == "ProcessManager.aspx") || (returnurl == "EnterPriseManager.aspx") || (returnurl == "TargetManager.aspx"))
        {
            deleteBtnPoid.Enabled = false;
            deleteBtnPoid.Style.Add("cursor", " default!important");
        }
        else
        {
            lnkbtn.Enabled = true;
            lnkBtnInput.Enabled = true;
            lnkBtnBOM.Enabled = true;
            lnkBtnTFG.Enabled = true;
            lnkBtnMachine.Enabled = true;
            lblOrderNo.Enabled = true;
            lnkbtnErrorReport.Enabled = true;
        }
    }

    private void BindColName(int pId)
    {
        IEnumerable<tbl_ProcessBlockHeader> processHeaders = ProcessHeaderColumns.GetProcessHeader(pId).ToList();
        if (processHeaders.Count() > 0)
        {
            lnkbtn.Text = processHeaders.Where(x => x.SequanceOrder == 1).Select(x => x.Headerlblname).FirstOrDefault();
            lnkBtnInput.Text = processHeaders.Where(x => x.SequanceOrder == 2).Select(x => x.Headerlblname).FirstOrDefault();
            lnkBtnBOM.Text = processHeaders.Where(x => x.SequanceOrder == 3).Select(x => x.Headerlblname).FirstOrDefault();
            lnkBtnTFG.Text = processHeaders.Where(x => x.SequanceOrder == 4).Select(x => x.Headerlblname).FirstOrDefault();
            lnkBtnMachine.Text = processHeaders.Where(x => x.SequanceOrder == 5).Select(x => x.Headerlblname).FirstOrDefault();
            lnkbtnErrorReport.Text = processHeaders.Where(x => x.SequanceOrder == 6).Select(x => x.Headerlblname).FirstOrDefault();
        }
    }

    public void BindDataOrderGrid(int poId)
    {

        int processId = 0;
        if (SourceType == 2)  //2: Target
        {
          ViewState["TargetObjID"] = poId;
        }
        else
            ViewState["ProcessObjID"] = poId;



        if (SourceType == 2)  //2: Target
        {
            tbl_TargetObject TargetObj = TargetData.ProcessObjectByID(poId);
                if (TargetObj != null)
            {

                // lblOrderNo.Text = ProcessObj.ProcessObjName;
                if (ViewState["Index"] != null)
                    lblOrderNo.Text = Activity.GetActivityNameByTargetObjId(this.CInt32(poId));
                //lblOrderNo.Text = "Activity-" + ViewState["Index"].ToString();
                tbl_ProcessObject tblProcessObj = new tbl_ProcessObject();
                //ProcessObj.ProcessObjName = "Activity-" + ViewState["Index"].ToString();
                TargetObj.TargetObjName = Activity.GetActivityNameByTargetObjId(this.CInt32(poId));
                TargetObj.TargetObjID = this.CInt32(poId);
                processId = tblProcessObj.ProcessID ?? 0;
                bool result = false;
                result = TargetData.SaveProcessObject(TargetObj);


                ViewState["TargetID"] = TargetObj.TargetID;
                gridActivityOrder.DataSource = ProcessData.GetActivityOrderData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(poId));
                gridActivityOrder.DataBind();
                deleteBtnPoid.ID = "lnkDeleteProcess_" + poId;


            }
        }
        else
        { 
            tbl_ProcessObject ProcessObj = ProcessData.ProcessObjectByID(poId);////AttributeById will get Attribute by its id that is EditIDINT
            processId = ProcessObj.ProcessID ?? 0;

            if (ProcessObj != null)
            {

                // lblOrderNo.Text = ProcessObj.ProcessObjName;
                if (ViewState["Index"] != null)
                    lblOrderNo.Text = Activity.GetActivityNameByProcessObjId(this.CInt32(poId));
                //lblOrderNo.Text = "Activity-" + ViewState["Index"].ToString();
                tbl_ProcessObject tblProcessObj = new tbl_ProcessObject();
                //ProcessObj.ProcessObjName = "Activity-" + ViewState["Index"].ToString();
                ProcessObj.ProcessObjName = Activity.GetActivityNameByProcessObjId(this.CInt32(poId));
                ProcessObj.ProcessObjID = this.CInt32(poId);
                
                bool result = false;
                result = ProcessData.SaveProcessObject(ProcessObj);


                ViewState["ProcessID"] = ProcessObj.ProcessID;
                gridActivityOrder.DataSource = ProcessData.GetActivityOrderData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(poId));
                gridActivityOrder.DataBind();
                deleteBtnPoid.ID = "lnkDeleteProcess_" + poId;


            }
      }
      BindColName(processId);
    }
    protected void lnkbtn_Click(object sender, EventArgs e)
    {
        //UserControl UcAttribute = (UserControl)Page.FindControl("ModelPopupAttributeUC.ascx");
        //AjaxControlToolkit.ModalPopupExtender PopupModelAttribute = (AjaxControlToolkit.ModalPopupExtender)UcAttribute.FindControl("mopoExUser");
        //PopupModelAttribute.Show();
    }


    protected void gridActivityOrder_RowCreated(object sender, GridViewRowEventArgs e)
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
    //protected void gridActivityOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gridActivityOrder.PageIndex = e.NewPageIndex;
    //    this.BindDataOrderGrid(); //// bind grid view 
    //}
    protected void gridActivityOrder_Sorting(object sender, GridViewSortEventArgs e)
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

        if (SourceType == 2)  //2: Target
        {
            BindDataOrderGrid(this.CInt32(ViewState["TargetObjID"]));
        }
        else
        {
            BindDataOrderGrid(this.CInt32(ViewState["ProcessObjID"]));
        }

            
    }
    public void ResetBinding(int poId)
    {
        ViewState["sortBy"] = "CreatedDate";
        ViewState["isAsc"] = "1";
        BindDataOrderGrid(poId); //bind gridview sorted by Created Date
    }
    protected void lnkbtn_Click1(object sender, EventArgs e)
    {
        //Parameter to a method is being made ready
        object[] obj = new object[2];

        if (SourceType == 2)  //2: Target
        {
            obj[0] = ViewState["TargetObjID"].ToString();
        }
        else
            obj[0] = ViewState["ProcessObjID"].ToString();


        obj[1] = "attribute";
        _delWithParam.DynamicInvoke(obj);
    }

    protected void lnkBtnInput_Click(object sender, EventArgs e)
    {
        //Parameter to a method is being made ready
        object[] obj = new object[2];
        if (SourceType == 2)  //2: Target
        {
            obj[0] = ViewState["TargetObjID"].ToString();
        }
        else
            obj[0] = ViewState["ProcessObjID"].ToString();
        obj[1] = "inputs";
        _delWithParam.DynamicInvoke(obj);
    }

    protected void lnkBtnBOM_Click(object sender, EventArgs e)
    {
        //Parameter to a method is being made ready
        object[] obj = new object[2];
        if (SourceType == 2)  //2: Target
        {
            obj[0] = ViewState["TargetObjID"].ToString();
        }
        else
            obj[0] = ViewState["ProcessObjID"].ToString();
        obj[1] = "BOM";
        _delWithParam.DynamicInvoke(obj);

    }

    protected void lnkBtnTFG_Click(object sender, EventArgs e)
    {
        //Parameter to a method is being made ready
        object[] obj = new object[2];
        if (SourceType == 2)  //2: Target
        {
            obj[0] = ViewState["TargetObjID"].ToString();
        }
        else
            obj[0] = ViewState["ProcessObjID"].ToString();
        obj[1] = "TFG";
        _delWithParam.DynamicInvoke(obj);
    }

    protected void lnkbtnErrorReport_Click(object sender, EventArgs e)
    {
        //Parameter to a method is being made ready
        object[] obj = new object[2];
        if (SourceType == 2)  //2: Target
        {
            obj[0] = ViewState["TargetObjID"].ToString();
        }
        else
            obj[0] = ViewState["ProcessObjID"].ToString();
        obj[1] = "ErrorReport";
        _delWithParam.DynamicInvoke(obj);
    }

    protected void lnkBtnMachine_Click(object sender, EventArgs e)
    {
        //Parameter to a method is being made ready
        object[] obj = new object[2];
        if (SourceType == 2)  //2: Target
        {
            obj[0] = ViewState["TargetObjID"].ToString();
        }
        else
            obj[0] = ViewState["ProcessObjID"].ToString();
        obj[1] = "Machine";
        _delWithParam.DynamicInvoke(obj);
    }
    //protected void deleteBtnPoid_Click(object sender, EventArgs e)
    //{
    //    int processobjId = 0;
    //    processobjId = this.CInt32(ViewState["ProcessObjID"]);
    //    if (processobjId > 0)
    //    {
    //        //reduce position of next processes after delete any process
    //        processobjId = this.CInt32(ViewState["ProcessObjID"]);
    //        int ProcessId = ProcessData.GetProcessIdByPoid(processobjId);
    //        int position = ProcessData.GetPositionByPoid(processobjId);
    //        bool decrease = false;
    //        decrease = ProcessData.DecreaseNextRowsPosition(ProcessId, position);
    //        if (decrease == true)
    //        {
    //            // position updated successfully
    //        }


    //        bool result = false; bool result1 = false; bool resultSystemIO = false;
    //        result = ProcessData.DeleteProcessObjDataByID(processobjId);////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables
    //        result1 = ProcessData.DeleteAttributedataByPoID(processobjId);
    //        resultSystemIO = ProcessData.DeleteSystemIODataByPoID(processobjId);
    //        string absolutepath = Request.Url.AbsolutePath;
    //        string returnurl = absolutepath.Substring(absolutepath.LastIndexOf('/') + 1);
    //        if (returnurl == "ProcessManager.aspx")
    //        {
    //            Response.Redirect("ProcessManager.aspx");
    //        }
    //        else
    //        {
    //            Response.Redirect("Production.aspx");
    //        }
    //    }

    //}

    protected void lnkActivityName_Click(object sender, EventArgs e)
    {
        //Parameter to a method is being made ready
        object[] obj = new object[2];
        if (SourceType == 2)  //2: Target
        {
            obj[0] = ViewState["TargetObjID"].ToString();
        }
        else
            obj[0] = ViewState["ProcessObjID"].ToString();
        obj[1] = "Activity";
        _delWithParam.DynamicInvoke(obj);
    }
}