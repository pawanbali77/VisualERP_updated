/*****************************************************************************
1. * Copyright 1999, All rights reserved, For internal use only
* FILE: ModelPopupBOMUC.ascx.cs
* PROJECT: VisualERP
* MODULE:ProcessManager
* AUTHOR: Ratnesh
* DATE: 25/7/2013
* Description: it contains the Attribute data and tree data .
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
using System.ComponentModel;
/// <summary>
/// public class User Control Model Popup Data for bind tree data
/// </summary>
public partial class UserControls_ModelPopupDataUc : System.Web.UI.UserControl
{ /// <summary>
    /// this code will fire the for tree Data.
    /// NAME: Tree data Population.
    /// Description: it will Displayed tree data and header and footer.
    /// Subroutines Called: ---
    /// Parameters: ParentID ,TreeNode
    /// Returns: Tree Data
    /// Globals: no
    /// Design Document Reference: no
    /// Author: Ratnesh
    /// Assumptions and Limitation: --
    /// Exception Processing: ---
    /// Date: By: Description: 25/7/2013
    /// </summary>
    /// <param name="">No Parameters</param>
    /// <returns>it will check post Back data and then Bind Tree Data</returns>
    #region
    int BomProcessId;
    int BomtypeId = 0;
    int ProcessId = 0;
    string BomName = "";
    string ProcessObjName = "";
    string ProcessName = "";
    DateTime InServiceDate;
    DateTime ObslncDate;
    #endregion
    private int _sourceTypeID = 1;
    [BrowsableAttribute(true)]
    public int SourceType
    {
        get { return _sourceTypeID; }
        set { _sourceTypeID = value; }
    }
    [BrowsableAttribute(true)]
    public int ProcessObjectId
    {
        set
        {
            ViewState["poId"] = value;
            BomProcessId = 0;
            List<BomData.BomDataProcessProperty> lstreeData = BomData.GetAllBomProcess(this.CInt32(ViewState["poId"]));
            TreeViewBom.Nodes.Clear();
            BindTree(lstreeData, null);
            ResetBinding(BomProcessId);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        ///it will check post Back data and then Bind Tree Data
        if (!IsPostBack)
        {
            List<BomData.BomDataProcessProperty> lstreeData = BomData.GetAllBomProcess(this.CInt32(ViewState["poId"]));
            TreeViewBom.Nodes.Clear();
            BindTree(lstreeData, null);
            ResetBinding(BomProcessId);
            //BindBOMTreeData();
            FillddlTypeBom();

            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

        }
        lblMsg.Text = "";
        lblMsg.Visible = false;
        divBom.Visible = false;
        copyBtn.Visible = false;
        if (TreeViewBom.SelectedNode != null)
        {
            BomProcessId = this.CInt32(TreeViewBom.SelectedNode.Value);
            // Session["SelectedNode"] = TreeViewBom.SelectedNode.ValuePath;
            //Session["SelectedNodeValue"] = TreeViewBom.SelectedNode.Value;
        }
        //else if (Session["SelectedNodeValue"] != null)
        //{
        //    BomProcessId = this.CInt32(Session["SelectedNodeValue"]);
        //}
        //if (txtBomProcess.Text != "Copy")
        //{
        //    FillddlTypeBom();
        //}
        //else
        //{
        //    FillddlOnlyBom();
        //}


        BomtypeId = BomData.GetTypeID(BomProcessId);
        TreeView mastertreeview = (TreeView)this.Page.Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
        {
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
            ProcessName = ProcessData.GetProcessNameById(ProcessId);
            ltrProcessName.Text = ProcessName.ToString();
        }
        if (BomtypeId == 6)
        {
            BomName = BomData.GetBomNameById(BomProcessId, BomtypeId);
            txtBomName.Text = BomName;
            ProcessObjName = ProcessData.GetProcessObjNameById(this.CInt32(ViewState["poId"]));
            txtProcesObjName.Text = ProcessObjName;
            divBom.Visible = true;
            copyBtn.Visible = true;

        }
        else
        {
            divBom.Visible = false;
            copyBtn.Visible = false;
        }
    }



    private void BindTree(IEnumerable<BomData.BomDataProcessProperty> list, TreeNode parentNode)
    {

        var nodes = list.Where(x => parentNode == null ? x.BomParentID == null : x.BomParentID == int.Parse(parentNode.Value));
        foreach (var node in nodes)
        {
            TreeNode newNode = new TreeNode(node.BomProcessName, node.BomProcessID.ToString());
            if (parentNode == null)
            {
                TreeViewBom.Nodes.Add(newNode);
            }
            else
            {
                parentNode.ChildNodes.Add(newNode);
            }
            BindTree(list, newNode);
        }
        TreeViewBom.ExpandAll();


    }
    /// <summary>
    /// on page load this code will run.
    /// </summary>
    /// <param name="Parent">"Parent" Tree Node parameters accessing</param>
    /// <param name="ParentID">ParentID" argument is the fill the tree data.</param>
    /// 
    public void fillTri(TreeNode parent, string ParentID)
    {
        VisualERPDataContext db = new VisualERPDataContext();///creating the object of Linq

        var items = db.tbl_Processes.Where(x => x.ParentID == null).ToList();///creating the variable for accessing tbl_Process data
        if (ParentID != null)///checking parentID
        {
            items = db.tbl_Processes.Where(x => x.ParentID == Convert.ToInt32(ParentID)).ToList();///checking the where condition on behalf of parent ID with in the tbl_Process
            parent.ChildNodes.Clear();///Clearing the old child node data
        }
        else
        {
            try
            {
                parent.ChildNodes.Clear();///Clearing the old child node data inside the try block
            }
            catch { }///catch block accessing the error
        }
        foreach (var item in items)/// foreach loop for bind the parent tree node with the tbl_Process Data
        {
            TreeNode node = new TreeNode();///Create the object of tree node
            node.Text = item.ProcessName;//Represent process name
            node.Value = item.ProcessID.ToString();//Represent process ID
            if (parent == null && ParentID == null)//Checking parent and ParentID
            {
                node.PopulateOnDemand = true;
                node.SelectAction = TreeNodeSelectAction.Expand;

                node.Expand();//Expand the node
                TreeViewBom.Nodes.Add(node);//Add the Data on node
            }
            else
            {
                if (node.ChildNodes.Count == 0)//chield data count
                {
                    node.PopulateOnDemand = true;
                    node.SelectAction = TreeNodeSelectAction.Expand;

                }
                node.CollapseAll();///Node all CollapseAll
                parent.ChildNodes.Add(node);///Add the child Node on parent node
            }

        }
    }
    /// <summary>
    /// Tree Node Population
    /// </summary>
    /// <param name="sender">"sender" argument is the object that raised the event.</param>
    /// <param name="e">EventArgs argument is extra information passed to the event.</param>
    protected void MyTreeViewBom_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        fillTri(e.Node, e.Node.Value);
    }
    /// <summary>
    /// Tree Node Data Population
    /// </summary>

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }
    //protected void btnUpBom_Click(object sender, EventArgs e)
    //{
    //    popup_box_addBom.Style["display"] = "block";
    //    //UserControl UCAddBomProcess = (UserControl)Page.FindControl("AddBomProcessUC.ascx");
    //    //AjaxControlToolkit.ModalPopupExtender AddBomProcess = (AjaxControlToolkit.ModalPopupExtender)AddBomProcessUC1.FindControl("ModelBOMProcess");
    //    //AddBomProcess.Show();
    //}
    //protected void btnDownBom_Click(object sender, EventArgs e)
    //{
    //    popup_box_addBom.Style["display"] = "block";
    //    //UserControl UCAddBomProcess = (UserControl)Page.FindControl("AddBomProcessUC.ascx");
    //    //AjaxControlToolkit.ModalPopupExtender AddBomProcess = (AjaxControlToolkit.ModalPopupExtender)AddBomProcessUC1.FindControl("ModelBOMProcess");
    //    //AddBomProcess.Show();
    //}

    //protected void btnCloseBom_Click(object sender, EventArgs e)
    //{
    //    popup_box_addBom.Style["display"] = "none";
    //}


    /// <summary>
    /// Bom Process Adding process on Bom manager
    /// </summary>
    public void FillddlTypeBom()
    {

        ddlBomProcessType.Items.Clear();
        ddlBomProcessType.Items.Add(new ListItem("Select", "0"));
        foreach (tbl_BomTypeCollection BomTypeData in BomData.GetTypeCollectionBOM())
        {
            ddlBomProcessType.Items.Add(new ListItem(BomTypeData.BomTypeName, BomTypeData.BomTypeID.ToString()));
        }


    }

    public void FillddlOnlyBom()
    {
        ddlBomProcessType.Items.Clear();
        ddlBomProcessType.Items.Add(new ListItem("Select", "0"));
        ddlBomProcessType.Items.Add(new ListItem("Bom", "1"));
    }

    public void ClearControl()
    {
        txtBomProcessNode.Text = "";
        //ddlSystem.SelectedValue = 0;
        ddlBomProcessType.SelectedIndex = 0;
    }

    protected void addBomProcessBtn_Click(object sender, EventArgs e)
    {
        if (Session["update"].ToString() == ViewState["update"].ToString())
        {
            AddBomNode();
            ClearControl();
            List<BomData.BomDataProcessProperty> lstreeData = BomData.GetAllBomProcess(this.CInt32(ViewState["poId"]));
            TreeViewBom.Nodes.Clear();
            BindTree(lstreeData, null);
            TreeViewBom.DataBind();
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        else
        {

        }
        ModelBOM.Show();
        //UpdatepnlBomTree.Update();
    }
    public void AddBomNode()
    {
        if (TreeViewBom.Nodes.Count != 0)
        {
            if (TreeViewBom.SelectedNode != null)
            {
                string selNode = TreeViewBom.SelectedNode.Value;
                VisualERPDataContext objData = new VisualERPDataContext();
                if (txtBomProcess.Text == "Up")
                {
                    var objChild = (from Cdata in objData.tbl_BomProcesses
                                    where Cdata.BomProcessID == Convert.ToInt32(selNode)
                                    select Cdata).FirstOrDefault();
                    tbl_BomProcess obj = new tbl_BomProcess();
                    obj.BomParentID = objChild.BomParentID;
                    obj.ProcessObjID = this.CInt32(ViewState["poId"]);
                    obj.BomProcessName = txtBomProcessNode.Text;
                    obj.BomTypeID = this.CInt32(ddlBomProcessType.SelectedValue);
                    objData.tbl_BomProcesses.InsertOnSubmit(obj);
                    objData.SubmitChanges();
                    int newid = obj.BomProcessID;

                    var objC = (from Cdata in objData.tbl_BomProcesses
                                where Cdata.BomProcessID == Convert.ToInt32(selNode)
                                select Cdata).FirstOrDefault();
                    objC.BomParentID = newid;
                    objData.SubmitChanges();
                    //string script = "alert(\"Saved successfully!\");";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(),
                    //              "ServerControlScript", script, true);
                    //popup_box.Style["display"] = "none";


                }
                else if (txtBomProcess.Text == "Down")
                {


                    tbl_BomProcess obj = new tbl_BomProcess();
                    obj.ProcessObjID = this.CInt32(ViewState["poId"]);
                    obj.BomParentID = Convert.ToInt32(selNode);
                    obj.BomProcessName = txtBomProcessNode.Text;
                    obj.BomTypeID = this.CInt32(ddlBomProcessType.SelectedValue);
                    obj.CreatedDate = DateTime.Now;
                    objData.tbl_BomProcesses.InsertOnSubmit(obj);
                    objData.SubmitChanges();
                    //string script = "alert(\"Saved successfully!\");";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(),
                    //              "ServerControlScript", script, true);
                    //popup_box.Style["display"] = "none";
                }
                else
                {
                    InsertBomProcess();
                    if (TreeViewBom.SelectedNode != null)
                    {
                        BomProcessId = this.CInt32(TreeViewBom.SelectedNode.Value);
                        int CurrBomProcessid = 0;
                        CurrBomProcessid = BomData.GetMaxBomProcessId();
                        VisualERPDataContext ObjData = new VisualERPDataContext();
                        ObjData.SP_CopyInsertBomProcess(BomProcessId, CurrBomProcessid);
                    }


                }

            }
            else
            {
                InsertBomProcess();
            }
        }
        else
        {

            InsertBomProcess();
        }
        ClearControl();
    }

    public void InsertBomProcess()
    {
        this.EditIDINT = 0;
        tbl_BomProcess BomProcessObj = new tbl_BomProcess();
        BomProcessObj.BomProcessName = txtBomProcessNode.Text.Trim();
        if (txtBomProcess.Text != "Copy")
        {
            BomProcessObj.BomTypeID = this.CInt32(ddlBomProcessType.SelectedValue);
        }
        else
        {
            BomProcessObj.BomTypeID = this.CInt32(6);
        }
        BomProcessObj.ProcessObjID = this.CInt32(ViewState["poId"]);
        BomProcessObj.CreatedDate = DateTime.Now;
        if (this.EditIDINT > 0)
        {
            BomProcessObj.BomProcessID = this.EditIDINT;
            BomProcessObj.ModifiedDate = DateTime.Now;
        }

        bool result = false;
        result = BomData.SaveBomProcessData(BomProcessObj);
        ClearControl();
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

    //protected override void OnPreRender(EventArgs e)
    //{
    //    ViewState["update"] = Session["update"];
    //}


    protected void submitBtn_Click(object sender, EventArgs e)
    {
        AddBomDetails();
        ModelBOM.Show();
    }

    public void AddBomDetails()
    {
        tbl_BOM tblBomData = new tbl_BOM();
        //TreeView TreeViewBOM = (TreeView)this.Page.FindControl("TreeViewBom");
        if (TreeViewBom.SelectedNode != null)
        {
            BomProcessId = this.CInt32(TreeViewBom.SelectedNode.Value);

        }


        if (txtInServiceDate.Text != string.Empty && txtObslncDate.Text != string.Empty)
        {
           // InServiceDate = Convert.ToDateTime(txtInServiceDate.Text); 
            InServiceDate = DateTime.ParseExact(txtInServiceDate.Text, "MM/dd/yyyy", null);          
           // ObslncDate = Convert.ToDateTime(txtObslncDate.Text);
            ObslncDate = DateTime.ParseExact(txtObslncDate.Text, "MM/dd/yyyy", null);
        }
        //else if (Session["SelectedNodeValue"] != null)
        //{
        //    BomProcessId = this.CInt32(Session["SelectedNodeValue"]);
        //}
        tblBomData.BomProcessID = BomProcessId;
        tblBomData.Description = txtBomDescription.Text.Trim();
        tblBomData.BOMLevel = txtBomLevel.Text.Trim();
        tblBomData.BOMRevision = txtBomRevision.Text.Trim();
        if (txtWeight.Text != string.Empty)
            tblBomData.weight = Convert.ToDouble(txtWeight.Text.Trim());
        tblBomData.UOM = txtUom.Text.Trim();
        if (txtStandardCost.Text != string.Empty)
            tblBomData.StandardCost = Convert.ToDouble(txtStandardCost.Text.Trim());
        if (txtWorkingCost.Text != string.Empty)
            tblBomData.WorkingCost = Convert.ToDouble(txtWorkingCost.Text.Trim());
        if (txtStandardPackQty.Text != string.Empty)
            tblBomData.StdPackQty = Convert.ToDouble(txtStandardPackQty.Text.Trim());
        if (txtMaxPackLength.Text != string.Empty)
            tblBomData.MaxPackLength = Convert.ToDouble(txtMaxPackLength.Text.Trim());
        if (txtMaxPackWidth.Text != string.Empty)
            tblBomData.MaxPackWidth = Convert.ToDouble(txtMaxPackWidth.Text.Trim());
        if (txtMaxPackHeight.Text != string.Empty)
            tblBomData.MaxPackHeight = Convert.ToDouble(txtMaxPackHeight.Text.Trim());
        if (txtContainerQty.Text != string.Empty)
            tblBomData.ContainerQty = Convert.ToDouble(txtContainerQty.Text.Trim());
        tblBomData.MedianRelinishmentLT = txtMedianLT.Text.Trim();
        tblBomData.MinRLT = txtMinRLT.Text.Trim();
        tblBomData.MaxRLT = txtMxRLT.Text.Trim();
        tblBomData.Rolling12MnthUsage = txtRollingUsage.Text.Trim();
        tblBomData.AvgMonthUsage = txtAvgMnthUsage.Text.Trim();
        tblBomData.MonthStdDevRiskFactor = txtMnthStdDev.Text.Trim();
        tblBomData.RiskFactor = txtRiskFactor.Text.Trim();
        tblBomData.KanbanQty = txtKanbanQty.Text.Trim();
        tblBomData.InService = Convert.ToBoolean(ddlServices.SelectedValue);
        if (txtInServiceDate.Text != string.Empty)
            tblBomData.In_ServiceDate = InServiceDate;
        if (txtObslncDate.Text != string.Empty)
            tblBomData.ObsolescenceDate = ObslncDate;
        tblBomData.OnHandInventory = txtOnHandInventory.Text.Trim();
        tblBomData.OnOrder = txtOnOrder.Text.Trim();
        tblBomData.NextShipmentDue = txtNextShipDue.Text.Trim();
        tblBomData.NextQtyDue = txtNextQtyDue.Text.Trim();
        tblBomData.PartReqNxtPerd = txtPartsReqNxtPeriod.Text.Trim();
        if (txtCurrentPurOwner.Text != string.Empty)
            tblBomData.CurrentPurchasingOwner = this.CInt32(txtCurrentPurOwner.Text.Trim());
        if (txtCurrentDesgOwner.Text != string.Empty)
            tblBomData.CurrentDesignOwner = this.CInt32(txtCurrentDesgOwner.Text.Trim());
        bool result = false;
        result = BomData.SaveBomData(tblBomData);  ////SaveInputData will dave input link in database table information input

        if (result == true)  // if record is updated or inserted
        {
            //string script = "alert(\"Saved successfully!\");";
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //              "ServerControlScript", script, true);
            lblMsg.Visible = true;
            lblMsg.Text = "Input data saved successfully!";
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
        if (TreeViewBom.SelectedNode != null)
        {
            BomProcessId = this.CInt32(TreeViewBom.SelectedNode.Value);
        }
        //else if (Session["SelectedNodeValue"] != null)
        //{
        //    BomProcessId = this.CInt32(Session["SelectedNodeValue"]);
        //}
        ResetBinding(BomProcessId);
        this.EditIDINT = 0;
    }
    //// bind grid view after new row inserted or row updated

    //// it will set edit mode false


    public void ResetBinding(int BomProcessID)
    {
        tbl_BOM tblBomDataFill = new tbl_BOM();  ////InputDataID obj of database table information input
        tblBomDataFill = BomData.BomDataById(BomProcessID);  ////InputById will get input by its id that is EditIDINT
        if (tblBomDataFill != null)
        {

            txtBomDescription.Text = tblBomDataFill.Description;
            txtBomLevel.Text = tblBomDataFill.BOMLevel;
            txtBomRevision.Text = tblBomDataFill.BOMRevision;
            txtWeight.Text = tblBomDataFill.weight.ToString();
            txtUom.Text = tblBomDataFill.UOM;
            txtStandardCost.Text = tblBomDataFill.StandardCost.ToString();
            txtWorkingCost.Text = tblBomDataFill.WorkingCost.ToString();
            txtStandardPackQty.Text = tblBomDataFill.StdPackQty.ToString();
            txtMaxPackLength.Text = tblBomDataFill.MaxPackLength.ToString();
            txtMaxPackWidth.Text = tblBomDataFill.MaxPackWidth.ToString();
            txtMaxPackHeight.Text = tblBomDataFill.MaxPackHeight.ToString();

            txtContainerQty.Text = tblBomDataFill.ContainerQty.ToString();
            txtMedianLT.Text = tblBomDataFill.MedianRelinishmentLT.ToString();
            txtMinRLT.Text = tblBomDataFill.MinRLT.ToString();
            txtMxRLT.Text = tblBomDataFill.MaxRLT.ToString();
            txtRollingUsage.Text = tblBomDataFill.Rolling12MnthUsage;
            txtAvgMnthUsage.Text = tblBomDataFill.AvgMonthUsage.ToString();
            txtMnthStdDev.Text = tblBomDataFill.MonthStdDevRiskFactor.ToString();
            txtRiskFactor.Text = tblBomDataFill.RiskFactor.ToString();
            txtKanbanQty.Text = tblBomDataFill.KanbanQty.ToString();
            this.SetDropDownListValue(ddlServices, tblBomDataFill.InService.ToString());
            if (tblBomDataFill.In_ServiceDate != null)
            {
                DateTime tt = Convert.ToDateTime(tblBomDataFill.In_ServiceDate);
                txtInServiceDate.Text = tt.ToShortDateString();
            }
            if (tblBomDataFill.ObsolescenceDate != null)
            {
                DateTime ttt = Convert.ToDateTime(tblBomDataFill.ObsolescenceDate);
                txtObslncDate.Text = ttt.ToShortDateString();
            }
            txtOnHandInventory.Text = tblBomDataFill.OnHandInventory;
            txtOnOrder.Text = tblBomDataFill.OnOrder;
            txtNextShipDue.Text = tblBomDataFill.NextShipmentDue;
            txtNextQtyDue.Text = tblBomDataFill.NextQtyDue;
            txtPartsReqNxtPeriod.Text = tblBomDataFill.PartReqNxtPerd.ToString();
            if (tblBomDataFill.CurrentPurchasingOwner != -1)
                txtCurrentPurOwner.Text = tblBomDataFill.CurrentPurchasingOwner.ToString();
            if (tblBomDataFill.CurrentDesignOwner != -1)
                txtCurrentDesgOwner.Text = tblBomDataFill.CurrentDesignOwner.ToString();
            submitBtn.Text = "Update"; ////button text changed with update in edit case
            this.EditIDINT = BomProcessID;
        }
        else
        {
            ClearControlBomData();
        }

    }


    public void ClearControlBomData()
    {
        txtBomDescription.Text = "";
        txtBomLevel.Text = "";
        txtBomRevision.Text = "";
        txtWeight.Text = "";
        txtUom.Text = "";
        txtStandardCost.Text = "";
        txtWorkingCost.Text = "";
        txtStandardPackQty.Text = "";
        txtMaxPackLength.Text = "";
        txtMaxPackWidth.Text = "";
        txtMaxPackHeight.Text = "";
        txtRiskFactor.Text = "";
        txtContainerQty.Text = "";
        txtMedianLT.Text = "";
        txtMinRLT.Text = "";
        txtMxRLT.Text = "";
        txtRollingUsage.Text = "";
        txtAvgMnthUsage.Text = "";
        txtMnthStdDev.Text = "";
        txtKanbanQty.Text = "";
        ddlServices.SelectedIndex = 0;

        txtInServiceDate.Text = "";

        txtObslncDate.Text = "";
        txtOnHandInventory.Text = "";
        txtOnOrder.Text = "";
        txtNextShipDue.Text = "";
        txtNextQtyDue.Text = "";
        txtPartsReqNxtPeriod.Text = "";
        txtCurrentPurOwner.Text = "";
        txtCurrentDesgOwner.Text = "";
    }
    protected void resetBtn_Click(object sender, EventArgs e)
    {
        if (TreeViewBom.SelectedNode != null)
        {
            BomProcessId = this.CInt32(TreeViewBom.SelectedNode.Value);

        }
        //else if (Session["SelectedNodeValue"] != null)
        //{
        //    BomProcessId = this.CInt32(Session["SelectedNodeValue"]);
        //}
        ResetBinding(BomProcessId);
        ModelBOM.Show();
    }

    protected void TreeViewBom_SelectedNodeChanged(object sender, EventArgs e)
    {
        ResetBinding(BomProcessId);
        ModelBOM.Show();

    }


    protected void imgClose3_Click(object sender, ImageClickEventArgs e)
    {
        ModelBOM.Hide();
        lblMsg.Text = "";
        lblMsg.CssClass = "";
        if (TreeViewBom.SelectedNode != null)
        {
            TreeViewBom.SelectedNode.Selected = false;
            //BomProcessId = this.CInt32(TreeViewBom.SelectedNode.Value);
            // Session["SelectedNode"] = TreeViewBom.SelectedNode.ValuePath;
            //Session["SelectedNodeValue"] = TreeViewBom.SelectedNode.Value;
        }
    }
}