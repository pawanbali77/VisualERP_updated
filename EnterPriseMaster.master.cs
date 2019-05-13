/*****************************************************************************
1. * Copyright 1999, All rights reserved, For internal use only
*
* FILE: MainMaster.Master.cs
* PROJECT: VisualERP
* MODULE:ProcessManager
* AUTHOR: Ratnesh
* DATE: 23/7/2013
* Description: it contains the header and footer or tree data part of all web pages.
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
using System.Web.Security;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

public partial class EnterPriseMaster : System.Web.UI.MasterPage
{
    #region
    int ProcessId = 0;
    string SystemName = "";
    int typeId = 0;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        // Session["SelectedNode"] = null;
        // BindTreeData();
        ///it will check post Back data and then Bind Tree Data
        ///  VisualERPDataContext ObjData = new VisualERPDataContext();
        string UserImage = "";

        if (Session["CompanyName"] != null)
        {
            UserImage = Session["CompanyName"].ToString();
        }
        var image = RegisterData.ProfileImage(0, UserImage);

        if (image == null || image == "")
        {
            Img_upload.ImageUrl = "~/images/no-image-icon-33.png";
        }
        else
        {
            Img_upload.ImageUrl = image;
        }
        if (!IsPostBack)
        {
            List<ProcessData.ProcessDataProperty> lstreeData = ProcessData.GetAllProcessID();
            TreeView1.Nodes.Clear();
            BindTree(lstreeData, null);

            //BindTreeData();

            if (TreeView1.SelectedNode == null)
            {
                if (Session["SelectedNode"] != null)
                {
                    TreeNode strNode = TreeView1.FindNode(Convert.ToString(Session["SelectedNode"]));

                    if (strNode != null)
                        strNode.Select();

                    Session["SelectedNode"] = null;
                    Session["SelectedNodeValue"] = null;
                }
            }

            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            ////Bind Tree Data
            ClearControl();
            FillddlSystem();

        }
        if (Session["SystemId"] != null)
        {
            lblSysName.Visible = false;
            // btnSystem.Visible = false;
            // SystemName = ProcessData.GetProcessNameById(this.CInt32(Session["SystemName"]));
            // lblSysName.Text = SystemName.ToString();
        }
        else
        {
            // btnSystem.Visible = true;
            lblSysName.Visible = true;
            //              "ServerControlScript", script, true);
            lblSysName.Text = "Please Choose the System Name";
            //lblSysName.CssClass = "msgError";
        }
        if (TreeView1.SelectedNode != null)
        {
            ProcessId = this.CInt32(TreeView1.SelectedNode.Value);
            typeId = TypeData.GetTypeID(ProcessId);
        }
    }


    private void BindTree(IEnumerable<ProcessData.ProcessDataProperty> list, TreeNode parentNode)
    {

        var nodes = list.Where(x => parentNode == null ? x.ParentID == null : x.ParentID == int.Parse(parentNode.Value));
        foreach (var node in nodes)
        {
            TreeNode newNode = new TreeNode(node.ProcessName, node.ProcessID.ToString());
            if (parentNode == null)
            {
                TreeView1.Nodes.Add(newNode);
            }
            else
            {
                parentNode.ChildNodes.Add(newNode);
            }
            BindTree(list, newNode);
        }

    }
    /// <summary>
    /// it is not using any where in enterprisemanager page.
    /// </summary>
    /// <param name="Parent">"Parent" Tree Node parameters accessing</param>
    /// <param name="ParentID">ParentID" argument is the fill the tree data.</param>
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
                TreeView1.Nodes.Add(node);//Add the Data on node
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
    /// it is not using any where in enterprisemanager page.
    /// </summary>
    /// <param name="sender">"sender" argument is the object that raised the event.</param>
    /// <param name="e">EventArgs argument is extra information passed to the event.</param>
    protected void MyTreeView_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        fillTri(e.Node, e.Node.Value);
    }
    /// <summary>
    /// Tree Node Data Population
    /// </summary>
    /*Transfer the control ProcessManager Page*/
    protected void prsBtn_Click(object sender, EventArgs e)
    {
        if (TreeView1.SelectedNode != null)
        {
            TreeView1.SelectedNode.Selected = false;
            TreeView1.SelectedNodeStyle.Reset();
            // TreeView1.SelectedNode.Selected = false;
            Session["SelectedNode"] = null;
            Session["SelectedNodeValue"] = null;
            List<ProcessData.ProcessDataProperty> lstreeData = ProcessData.GetAllProcessID();
            TreeView1.Nodes.Clear();
            BindTree(lstreeData, null);
        }
        Response.Redirect("~/ProcessManager.aspx");
    }
    /*Transfer the control EnterPriseManager Page*/
    protected void enterpriseBtn_Click(object sender, EventArgs e)
    {
        if (TreeView1.SelectedNode != null)
        {
            TreeView1.SelectedNode.Selected = false;
            Session["SelectedNode"] = null;
            Session["SelectedNodeValue"] = null;
            List<ProcessData.ProcessDataProperty> lstreeData = ProcessData.GetAllProcessID();
            TreeView1.Nodes.Clear();
            BindTree(lstreeData, null);
        }
        Response.Redirect("~/EnterPriseManager.aspx");
    }
    //protected void ImgbtnDwn_click(object sender, ImageClickEventArgs e)
    //{
    //    UserControl UcAddProcess = LoadControl("UserControls/ModelPopupAddProcess.ascx") as UserControl;
    //   // UserControl UcAddProcess = (UserControl)Page.FindControl("ModelPopupAddProcess.ascx");

    //    AjaxControlToolkit.ModalPopupExtender PopupModelAddProcess = (AjaxControlToolkit.ModalPopupExtender)UcAddProcess.FindControl("ModelPopupProcess");
    //    PopupModelAddProcess.Show();
    //}

    //protected void ImgbtnCheckout_click(object sender, ImageClickEventArgs e)
    //{

    //}

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
    public void FillddlSystem()
    {
        ddlSystem.Items.Clear();
        ddlSystem.Items.Add(new ListItem("Select", "0"));
        foreach (tbl_Process typedata in TypeData.GetSytemCollection())
        {
            ddlSystem.Items.Add(new ListItem(typedata.ProcessName, typedata.ProcessID.ToString()));
        }
    }

    public void ClearControl()
    {
        ddlSystem.SelectedIndex = 0;
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }


    protected void addProcessBtn_Click(object sender, EventArgs e)
    {
        if (Session["update"].ToString() != null && ViewState["update"].ToString() != null)
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                AddSystem();
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
    }

    public void AddSystem()
    {
        Session["SystemId"] = this.CInt32(ddlSystem.SelectedValue);
        Response.Redirect("EnterPriseManager.aspx");
        //if (TreeView1.SelectedNode != null)
        //{
        //    ProcessId = this.CInt32(TreeView1.SelectedNode.Value);
        //}
        //if (TypeData.GetDuplicateCheck(this.CInt32(ddlSystem.SelectedValue), ProcessId))
        //{
        //    tbl_SystemProcessIO tblSystemProcess = new tbl_SystemProcessIO();
        //    tblSystemProcess.SystemID = this.CInt32(ddlSystem.SelectedValue);

        //    tblSystemProcess.ProcessID = this.CInt32(ProcessId);
        //    tblSystemProcess.CreatedDate = DateTime.Now;
        //    bool result = false;
        //    result = TypeData.SaveSystemProcessData(tblSystemProcess);  ////SaveInputData will dave input link in database table information input

        //    if (result == true)  // if record is updated or inserted
        //    {
        //        btnSystem.Visible = false;
        //        lblSysName.Visible = false;
        //        Session["SystemId"] = this.CInt32(ddlSystem.SelectedValue);
        //    }
        //    else
        //    {
        //        //string script = "alert(\"Error on saving data.!\");";
        //        //ScriptManager.RegisterStartupScript(this, this.GetType(),
        //        //              "ServerControlScript", script, true);
        //        //lblMsg.Visible = true;
        //        //lblMsg.Text = "Error on saving data.!";
        //        //lblMsg.CssClass = "msgError";
        //    }
        //}

    }
    protected void TreeView1_OnSelectedNodeChanged(object sender, EventArgs e)
    {
        if (typeId == 4)
        {
            if (TreeView1.SelectedNode != null)
            {
                Session["SystemId"] = this.CInt32(TreeView1.SelectedNode.Value);
                Response.Redirect("EnterPriseManager.aspx");
            }
        }

        else if (Session["SystemId"] != null)
        {
            if (TypeData.GetDuplicateCheck(this.CInt32(Session["SystemId"]), ProcessId))
            {
                if (TreeView1.SelectedNode != null)
                    ProcessId = this.CInt32(TreeView1.SelectedNode.Value);
                typeId = TypeData.GetTypeID(ProcessId);

                if (typeId == 5)
                {
                    tbl_SystemProcessIO tblSystemProcess = new tbl_SystemProcessIO();
                    tblSystemProcess.SystemID = this.CInt32((Session["SystemId"]));
                    if (TreeView1.SelectedNode != null)
                        ProcessId = this.CInt32(TreeView1.SelectedNode.Value);
                    tblSystemProcess.ProcessID = this.CInt32(ProcessId);
                    tblSystemProcess.CreatedDate = DateTime.Now;
                    bool result = false;
                    result = TypeData.SaveSystemProcessData(tblSystemProcess);
                    Response.Redirect("EnterPriseManager.aspx");
                }
            }
            else
            {
                lblSysName.Visible = true;
                //              "ServerControlScript", script, true);
                lblSysName.Text = "This record already exists.!";
                lblSysName.CssClass = "msgError";
            }
        }


        else
        {
            // btnSystem.Visible = true;
            lblSysName.Visible = true;
            //              "ServerControlScript", script, true);
            lblSysName.Text = "Please Choose the System Name";
            lblSysName.CssClass = "msgError";


        }
    }

    //change
    protected void tgtBtn_Click(object sender, EventArgs e)
    {
        string Email = Session["Email"].ToString();
        int ID = Convert.ToInt32(Session["ID"].ToString());
        int ProcessId = Convert.ToInt32(Session["ProcessId"].ToString());
        int CompanyId = Convert.ToInt32(Session["CompanyID"].ToString());
        int UserID = Convert.ToInt32(Session["UserID"].ToString());


        //int Parent_id = Convert.ToInt32(Session["Parent_id"].ToString());
        if (TreeView1.SelectedNode != null)
        {
            Email = Session["Email"].ToString();
            ID = Convert.ToInt32(Session["ID"].ToString());
            ProcessId = Convert.ToInt32(Session["ProcessId"].ToString());
            CompanyId = Convert.ToInt32(Session["CompanyID"].ToString());
            UserID = Convert.ToInt32(Session["UserID"].ToString());

            // Parent_id = Convert.ToInt32(Session["Parent_id"].ToString());

            TreeView1.SelectedNode.Selected = false;
            TreeView1.SelectedNodeStyle.Reset();
            // TreeView1.SelectedNode.Selected = false;
            Session["SelectedNode"] = null;
            Session["SelectedNodeValue"] = null;

            List<ProcessData.ProcessDataProperty> lstreeData = ProcessData.GetAllProcessID_test(Convert.ToInt32(UserID.ToString()), Convert.ToInt32(CompanyId.ToString()));


            // List<ProcessData.ProcessDataProperty> lstreeData = ProcessData.GetAllProcessID();

            TreeView1.Nodes.Clear();
            BindTree(lstreeData, null);
        }
        //lblManager.Text = "Process Manager";
        Response.Redirect("~/TargetManager.aspx");
    }
}
