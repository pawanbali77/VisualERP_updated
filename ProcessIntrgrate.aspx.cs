using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.Data;

public partial class ProcessIntrgrate : System.Web.UI.Page
{
    #region
    List<UserControl> lst = new List<UserControl>();
    int ProcessId = 0;
    int typeId = 0;
    #endregion

    string str2 = string.Empty;

    delegate void DelMethodWithParam(string strProcessObjectId, string strAction);
    delegate void DelMethodWithoutParam();

    DelMethodWithParam delParam;
    protected void Page_Load(object sender, EventArgs e)
    {
        string scriptJq = "callready();";
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript", scriptJq, true);

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
            ProcessId = this.CInt32(Session["SelectedNodeValue"]);

        typeId = TypeData.GetTypeID(ProcessId);

        if (typeId == 5)
        {
            liProcess.Visible = true;
            liInventory.Visible = true;
        }
        else
        {
            liProcess.Visible = false;
            liInventory.Visible = false;
        }
        //liProcess.Visible = true;
        load();


        if (hdnSupplier.Value != string.Empty)
        {

            string modifiedhdval = hdnSupplier.Value;
            Session["PosSupplier"] = modifiedhdval;
            string[] arr = modifiedhdval.Split(',');
            string id, top, left;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                string[] arrN = arr[i].Split('_');
                id = arrN[0];          // id of our control
                string[] possX = arrN[1].Split('-');
                top = possX[0];       //x position of control
                left = possX[1];         // y position of our control
                ControlPosition(id, top, left); // call function to creae control on last position

            }
            Session["Supplier1"] = null;
        }
        else
        {
            if (Session["PosSupplier"] != null)
            {
                string hdn = Convert.ToString(Session["PosSupplier"]);
                hdnSupplier.Value = hdn;
            }

        }

        if (!IsPostBack)
        {
            //AjaxControlToolkit.ModalPopupExtender PopupModelAttribute = (AjaxControlToolkit.ModalPopupExtender)ModelPopupAttributeUC1.FindControl("mopoExUser");
            //PopupModelAttribute.Hide();
        }

        ResetBinding();

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
            //UserControl UcBOM = (UserControl)Page.FindControl("ModelPopupBOMUC.ascx");
            AjaxControlToolkit.ModalPopupExtender PopupModelBOM = (AjaxControlToolkit.ModalPopupExtender)ModelPopupBOMUC1.FindControl("ModelBOM");
            PopupModelBOM.Show();

        }
        if (strAction == "TFG")
        {
            ModelPopupTFGUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            /// UserControl UcTFG = (UserControl)Page.FindControl("ModelPopupTFGUC.ascx");
            AjaxControlToolkit.ModalPopupExtender PopupModelTFG = (AjaxControlToolkit.ModalPopupExtender)ModelPopupTFGUC1.FindControl("ModelTFG");
            PopupModelTFG.Show();
        }

        if (strAction == "ErrorReport")
        {
            ModelPopupErrorReportUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            AjaxControlToolkit.ModalPopupExtender PopupModelErrorReport = (AjaxControlToolkit.ModalPopupExtender)ModelPopupErrorReportUC1.FindControl("ModelErrorReport");
            PopupModelErrorReport.Show();
        }

        if (strAction == "Machine")
        {
            ModelPopupMchUC1.ProcessObjectId = Convert.ToInt32(strProcessObjectId);
            // UserControl UcMachine = (UserControl)Page.FindControl("ModelPopupMchUC.ascx");
            AjaxControlToolkit.ModalPopupExtender PopupModelMachine = (AjaxControlToolkit.ModalPopupExtender)ModelPopupMchUC1.FindControl("ModelMachine");
            PopupModelMachine.Show();
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
    }

    public void ResetBinding()
    {
        ViewState["sortBy"] = "CreatedDate";
        ViewState["isAsc"] = "1";
        BindGridSummary(); //bind gridview sorted by Created Date
    }

    public void BindGridSummary()
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
        List<ProcessData.ProcessDataProperty> prop = ProcessData.GetSummaryData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ProcessId));

        if (prop.Count != 0)
        {
            gridProcessSummary.DataSource = prop;


            gridProcessSummary.DataBind();
            divSummary.Visible = true;
        }
        else { divSummary.Visible = false; }

        // gridProcessSummary.DataSource = ProcessData.GetSummaryData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ProcessId));

        //if (gridProcessSummary.)
        //{
        //    gridProcessSummary.DataSource = ProcessData.GetSummaryData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ProcessId));
        //    gridProcessSummary.DataBind();
        //    divSummary.Visible = true;
        //}


    }
    public void findType()
    {
        //TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        //int ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        //TypeData.SystemTypeDetail typeId = new TypeData.SystemTypeDetail();
        //typeId = TypeData.GetTypeID(ProcessId);
    }
    public void load()
    {
        lst.Clear();
        MainDiv1.Controls.Clear();
        ResetBinding();
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
        List<ProcessData.ProcessDataProperty> lstpoid = ProcessData.GetAllProcessObjId(ProcessId);

        Table TblFirst = new Table();
        TblFirst.ID = "divProcess" + Guid.NewGuid();
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
            MainDiv1.Controls.Add(TblFirst);
        }
    }

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
            load();


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
        MainDiv1.Controls.Add(TblFirst);
    }

    //void lnkAttribute_Click(object sender, EventArgs e)
    //{
    //    UserControl UcAttribute = (UserControl)Page.FindControl("ModelPopupAttributeUC.ascx");
    //    AjaxControlToolkit.ModalPopupExtender PopupModelAttribute = (AjaxControlToolkit.ModalPopupExtender)UcAttribute.FindControl("mopoExUser");
    //    PopupModelAttribute.Show();
    //}

    protected void lnkBtnInventory_Click(object sender, EventArgs e)
    {

        //InventoryUC1.PageMethodWithParamRef = delParam;
        //AjaxControlToolkit.ModalPopupExtender PopupModelInventery = (AjaxControlToolkit.ModalPopupExtender)InventoryUC1.FindControl("ModelPopupInventery");
        //PopupModelInventery.Show();
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
        MainDiv1.Controls.Add(TblFirst);
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

    protected void lnkBtnSupplier_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        List<string> str = new List<string>();
        string str1 = string.Empty;
        if (Session["Supplier"] != null)
        {
            str1 += Convert.ToString(Session["Supplier"]);
            //str.Add(Convert.ToString(Session["Supplier"]));
        }
        string id = "divSupplier" + Guid.NewGuid();
        int top = 20, left = 20;
        sb.Append(" <div class='Supplier' style='top:" + top + "px;left:" + left + "px' id='" + id + "'>Supplier</div>");
        //str.Add(sb.ToString());
        //sb.Append(" <div class='Supplier' style='Position:initial;' id='" + id + "'>Supplier</div>");
        str1 += sb.ToString();
        Session["Supplier"] = str1;

        MainDiv1.InnerHtml = str1.ToString();
        string script = "callready();";
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript", script, true);
        // MainDiv.Attributes.Add("style", "position:absolute");

        StringBuilder sb2 = new StringBuilder();
        string str2 = string.Empty;
        if (Session["PosSupplier"] != null)
        {
            str2 += Convert.ToString(Session["PosSupplier"]);
            //str.Add(Convert.ToString(Session["Supplier"]));
        }
        sb2.Append(id + "_" + top + "-" + left + ",");
        str2 += sb2.ToString();
        Session["PosSupplier"] = str2;
        //hdnSupplier.Value = String.Join(",", str2);
        hdnSupplier.Value = str2;

        //UserControls_Supplier Supplier = LoadControl("UserControls/Supplier.ascx") as UserControls_Supplier;      
        //load(Supplier);
    }

    public void ControlPosition(string id, string top, string left)
    {
        if (id.Contains("divSupplier"))
        {
            StringBuilder sb = new StringBuilder();
            List<string> str = new List<string>();

            if (Session["Supplier1"] != null)
            {
                str2 = Convert.ToString(Session["Supplier1"]);
                // str.Add(Convert.ToString(Session["Supplier"]));
            }
            sb.Append(" <div class='Supplier' style='top:" + top + "px;left:" + left + "px' id='" + id + "'>Supplier</div>");
            //str.Add(sb.ToString());

            str2 += sb.ToString();
            Session["Supplier1"] = str2;
            Session["Supplier"] = str2;
            //MainDiv.InnerHtml = str1.ToString();
        }
        else if (id.Contains("divShipment"))
        {
            StringBuilder sb = new StringBuilder();
            List<string> str = new List<string>();
            string str1 = string.Empty;
            if (Session["Supplier"] != null)
            {
                str1 += Convert.ToString(Session["Supplier"]);
                //str.Add(Convert.ToString(Session["Supplier"]));
            }
            sb.Append("<div class='Shipment' style='top:" + top + "px;left:" + left + "px' id='" + id + "'>Shipment<br />(Daily)</div>");
            //str.Add(sb.ToString());
            str1 += sb.ToString();
            Session["Supplier"] = str1;
            MainDiv1.InnerHtml = str1.ToString();
        }
        else if (id.Contains("divForcast"))
        {
            StringBuilder sb = new StringBuilder();
            List<string> str = new List<string>();
            string str1 = string.Empty;
            if (Session["Supplier"] != null)
            {
                str1 += Convert.ToString(Session["Supplier"]);
                //str.Add(Convert.ToString(Session["Supplier"]));
            }
            sb.Append("<div class='Arrow2' style='top:" + top + "px;left:" + left + "px' id='" + id + "'>Market Forcast</div>");
            //str.Add(sb.ToString());
            str1 += sb.ToString();
            Session["Supplier"] = str1;
            MainDiv1.InnerHtml = str1.ToString();
        }
        else if (id.Contains("divProduction"))
        {
            StringBuilder sb = new StringBuilder();
            List<string> str = new List<string>();
            string str1 = string.Empty;
            if (Session["Supplier"] != null)
            {
                str1 += Convert.ToString(Session["Supplier"]);
                //str.Add(Convert.ToString(Session["Supplier"]));
            }
            sb.Append(" <div class='ProductionC' style='top:" + top + "px;left:" + left + "px' id='" + id + "'><h2>Value Stream</h2><h3>Production <br /> Control</h3></div>");
            //str.Add(sb.ToString());
            str1 += sb.ToString();
            Session["Supplier"] = str1;
            MainDiv1.InnerHtml = str1.ToString();
        }
        else if (id.Contains("divDSchedule"))
        {
            StringBuilder sb = new StringBuilder();
            List<string> str = new List<string>();
            string str1 = string.Empty;
            if (Session["Supplier"] != null)
            {
                str1 += Convert.ToString(Session["Supplier"]);
                //str.Add(Convert.ToString(Session["Supplier"]));
            }
            sb.Append("<div class='Delievery' style='top:" + top + "px;left:" + left + "px' id='" + id + "'>Weekly Delievery Shedule</div>");
            //str.Add(sb.ToString());
            str1 += sb.ToString();
            Session["Supplier"] = str1;
            MainDiv1.InnerHtml = str1.ToString();
        }
        else if (id.Contains("divArrow"))
        {
            StringBuilder sb = new StringBuilder();
            List<string> str = new List<string>();
            string str1 = string.Empty;
            if (Session["Supplier"] != null)
            {
                str1 += Convert.ToString(Session["Supplier"]);
                //str.Add(Convert.ToString(Session["Supplier"]));
            }
            sb.Append("<div class='Annual' style='top:" + top + "px;left:" + left + "px' id='" + id + "'>Annual Production Plan</div>");
            //str.Add(sb.ToString());
            str1 += sb.ToString();
            Session["Supplier"] = str1;
            MainDiv1.InnerHtml = str1.ToString();
        }
    }
    //protected void btnDesignView_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Production.aspx");
    //}
}