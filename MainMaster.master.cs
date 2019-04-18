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
/// <summary>

/// <summary>
/// public class Main Master.
/// </summary>
public partial class MainMaster : System.Web.UI.MasterPage
{




    /// <summary>
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
    /// Date: By: Description: 23/7/2013
    /// </summary>
    /// <param name="">No Parameters</param>
    /// <returns>it will check post Back data and then Bind Tree Data</returns>
    /// 




    protected void Page_Load(object sender, EventArgs e)
    {
        // Session["SelectedNode"] = null;
        // BindTreeData();
        ///it will check post Back data and then Bind Tree Data
        ///  VisualERPDataContext ObjData = new VisualERPDataContext();
        ///  
        string Email = "";
        int ID = 0;
        int ProcessId = 0;
        int CompanyId = 0;
        int UserID = 0;
        int RoleID = 0;

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

        if (Session["Email"] != null)
        {
            Email = Session["Email"].ToString();
        }
        if (Session["ID"] != null)
        {
            ID = Convert.ToInt32(Session["ID"].ToString());
        }
        if (Session["ProcessId"] != null)
        {
            ProcessId = Convert.ToInt32(Session["ProcessId"].ToString());
        }
        if (Session["CompanyId"] != null)
        {
            CompanyId = Convert.ToInt32(Session["CompanyID"].ToString());
        }
        if (Session["UserID"] != null)
        {
            UserID = Convert.ToInt32(Session["UserID"].ToString());
        }
        if (Session["RoleID"] != null)
        {
            RoleID = Convert.ToInt32(Session["RoleID"].ToString());
        }
        if (!IsPostBack)
        {
            if (Session["Email"] != null || Session["ID"] != null)
            {
                Email = Session["Email"].ToString();
                ID = Convert.ToInt32(Session["ID"].ToString());
                ProcessId = Convert.ToInt32(Session["ProcessId"].ToString());
                CompanyId = Convert.ToInt32(Session["CompanyID"].ToString());
                UserID = Convert.ToInt32(Session["UserID"].ToString());
                RoleID = Convert.ToInt32(Session["RoleID"].ToString());
                //Parent_id = Convert.ToInt32(Session["Parent_id"].ToString());

                if(RoleID == 1)
                {
                   
                }
                else if (RoleID == 2)
                {
                    Control myControlMenu1 = Page.Master.FindControl("menu_Enterprisemanager");
                    if (myControlMenu1 != null)
                    {
                        myControlMenu1.Visible = false;
                    }
                }
                else if (RoleID == 3)
                {
                    Control myControlMenu1 = Page.Master.FindControl("menu_Enterprisemanager");
                    if (myControlMenu1 != null)
                    {
                        myControlMenu1.Visible = false;
                    }
                    Control myControlMenu2 = Page.Master.FindControl("menu_Formmanager");
                    if (myControlMenu2 != null)
                    {
                        myControlMenu2.Visible = false;
                    }
                    Control myControlMenu3 = Page.Master.FindControl("divAddNode");
                    if (myControlMenu3 != null)
                    {
                        myControlMenu3.Visible = false;
                    }
                }
                else if (RoleID == 4)
                {
                    Control myControlMenu1 = Page.Master.FindControl("menu_Enterprisemanager");
                    if (myControlMenu1 != null)
                    {
                        myControlMenu1.Visible = false;
                    }
                    Control myControlMenu2 = Page.Master.FindControl("menu_Formmanager");
                    if (myControlMenu2 != null)
                    {
                        myControlMenu2.Visible = false;
                    }
                    Control myControlMenu3 = Page.Master.FindControl("divAddNode");
                    if (myControlMenu3 != null)
                    {
                        myControlMenu3.Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            List<ProcessData.ProcessDataProperty> lstreeData = ProcessData.GetAllProcessID_test(Convert.ToInt32(UserID.ToString()), Convert.ToInt32(CompanyId.ToString()));

            // List<ProcessData.ProcessDataProperty> lstreeData = ProcessData.GetAllProcessID();

            TreeView1.Nodes.Clear();
            BindTree(lstreeData, null); ///bind the tree node before the page post back

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

            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString()); ///1.When page is load then automatic button click it was fired I have mange the solutuion for it.
            ////Bind Tree Data
            // addProcessBtn.OnClientClick = String.Format("fnClickUpdate('{0}','{1}')", addProcessBtn.UniqueID, "");
            FillddlType();//Fill the ddltype what ever you want on behalf of tree node

        }
        //ClearControl();
        liFunction.Visible = false;///when ever you will select process then it will appear function name li block
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
    /// on page load this code will run.(both function fillTri and MyTreeView_TreeNodePopulate it is not using.)
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
    /// Tree Node Population
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
        Response.Redirect("~/ProcessManager.aspx");
    }

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
    /*Transfer the control EnterPriseManager Page*/
    protected void enterpriseBtn_Click(object sender, EventArgs e)
    {
        string Email = Session["Email"].ToString();
        int ID = Convert.ToInt32(Session["ID"].ToString());
        //int Parent_id = Convert.ToInt32(Session["Parent_id"].ToString());
        int ProcessId = Convert.ToInt32(Session["ProcessId"].ToString());
        int CompanyId = Convert.ToInt32(Session["CompanyID"].ToString());
        int UserID = Convert.ToInt32(Session["UserID"].ToString());


        if (TreeView1.SelectedNode != null)
        {
            Email = Session["Email"].ToString();
            ID = Convert.ToInt32(Session["ID"].ToString());
            // Parent_id = Convert.ToInt32(Session["Parent_id"].ToString());
            ProcessId = Convert.ToInt32(Session["ProcessId"].ToString());
            CompanyId = Convert.ToInt32(Session["CompanyID"].ToString());
            UserID = Convert.ToInt32(Session["UserID"].ToString());


            TreeView1.SelectedNode.Selected = false;
            Session["SelectedNode"] = null;
            Session["SelectedNodeValue"] = null;

            List<ProcessData.ProcessDataProperty> lstreeData = ProcessData.GetAllProcessID_test(Convert.ToInt32(UserID.ToString()), Convert.ToInt32(CompanyId.ToString()));

            // List<ProcessData.ProcessDataProperty> lstreeData = ProcessData.GetAllProcessID();

            TreeView1.Nodes.Clear();
            BindTree(lstreeData, null);
        }
        Response.Redirect("~/EnterPriseManager.aspx");
    }
    /// <summary>
    /// this fuction for inserting the process
    /// </summary>
    public void InsertProcess()
    {
        //if (ProcessData.GetDuplicateCheck(txtNodeName.Text.Trim(), this.EditIDINT))
        //{
        tbl_Process ProcessObj = new tbl_Process();
        ProcessObj.ProcessName = txtNodeName.Text.Trim();
        ProcessObj.FunctionName = txtFunctionName.Text.Trim();
        ProcessObj.TypeID = this.CInt32(ddlType.SelectedValue);
        ProcessObj.CreatedDate = DateTime.Now;
        if (this.EditIDINT > 0)
        {
            ProcessObj.ProcessID = this.EditIDINT;
            ProcessObj.ModifiedDate = DateTime.Now;
        }
        ProcessObj.UserRegisterID = Convert.ToInt32(Session["UserID"].ToString());
        ProcessObj.CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());
        bool result = false;
        result = ProcessData.SaveProcessData(ProcessObj);
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
    public void FillddlType()
    {
        ddlType.Items.Clear();
        ddlType.Items.Add(new ListItem("Select", "0"));
        foreach (tbl_TypeCollection typedata in TypeData.GetTypeCollection())
        {
            ddlType.Items.Add(new ListItem(typedata.TypeName, typedata.TypeID.ToString()));
        }
    }
    public void ClearControl()
    {
        txtNodeName.Text = "";
        txtFunctionName.Text = "";
        //ddlSystem.SelectedValue = 0;
        ddlType.SelectedIndex = 0;
    }
    ///When page is load then automatic button click it was fired I have mange the solutuion for it.
    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }
    protected void addNode_btn()
    {
        if (TreeView1.Nodes.Count != 0)
        {
            if (TreeView1.SelectedNode != null)
            {
                string selNode = TreeView1.SelectedNode.Value;

                VisualERPDataContext objData = new VisualERPDataContext();

                if (txtpo.Text == "Up")
                {
                    var objChild = (from Cdata in objData.tbl_Processes
                                    where Cdata.ProcessID == Convert.ToInt32(selNode)
                                    select Cdata).FirstOrDefault();
                    tbl_Process obj = new tbl_Process();
                    obj.ParentID = objChild.ParentID;
                    obj.ProcessName = txtNodeName.Text;
                    obj.FunctionName = txtFunctionName.Text;
                    obj.TypeID = this.CInt32(ddlType.SelectedValue);
                    objData.tbl_Processes.InsertOnSubmit(obj);
                    objData.SubmitChanges();
                    int newid = obj.ProcessID; ;

                    var objC = (from Cdata in objData.tbl_Processes
                                where Cdata.ProcessID == Convert.ToInt32(selNode)
                                select Cdata).FirstOrDefault();
                    objC.ParentID = newid;
                    objData.SubmitChanges();
                }
                else
                {
                    tbl_Process obj = new tbl_Process();
                    obj.ParentID = Convert.ToInt32(selNode);
                    obj.ProcessName = txtNodeName.Text;
                    obj.FunctionName = txtFunctionName.Text;
                    obj.TypeID = this.CInt32(ddlType.SelectedValue);
                    obj.UserRegisterID = Convert.ToInt32(Session["UserID"].ToString());
                    obj.CompanyID = Convert.ToInt32(Session["CompanyID"].ToString());

                    objData.tbl_Processes.InsertOnSubmit(obj);
                    objData.SubmitChanges();
                    //string script = "alert(\"Saved successfully!\");";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(),
                    //              "ServerControlScript", script, true);
                    //popup_box.Style["display"] = "none";
                }
            }
            else
            {
                InsertProcess();
            }
        }
        else
        {
            InsertProcess();
        }
        ClearControl();
    }

    protected void addProcessBtn_Click(object sender, EventArgs e)
    {
        string Email = Session["Email"].ToString();
        int ID = Convert.ToInt32(Session["ID"].ToString());
        // int Parent_id = Convert.ToInt32(Session["Parent_id"].ToString());
        int ProcessId = Convert.ToInt32(Session["ProcessId"].ToString());
        int CompanyId = Convert.ToInt32(Session["CompanyID"].ToString());
        int UserID = Convert.ToInt32(Session["UserID"].ToString());

        if (Session["update"].ToString() == ViewState["update"].ToString()) ///2.When page is load then automatic button click it was fired I have mange the solutuion for it.
        {
            Email = Session["Email"].ToString();
            ID = Convert.ToInt32(Session["ID"].ToString());
            //Parent_id = Convert.ToInt32(Session["Parent_id"].ToString());
            ProcessId = Convert.ToInt32(Session["ProcessId"].ToString());
            CompanyId = Convert.ToInt32(Session["CompanyID"].ToString());
            UserID = Convert.ToInt32(Session["UserID"].ToString());

            addNode_btn();
            ClearControl();

            List<ProcessData.ProcessDataProperty> lstreeData = ProcessData.GetAllProcessID_test(Convert.ToInt32(UserID.ToString()), Convert.ToInt32(CompanyId.ToString()));

            // List<ProcessData.ProcessDataProperty> lstreeData = ProcessData.GetAllProcessID();

            TreeView1.Nodes.Clear();
            BindTree(lstreeData, null);
            TreeView1.DataBind();
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());///3.When page is load then automatic button click it was fired I have mange the solutuion for it.
        }
        else
        {

        }
    }

    /// <summary>
    /// I have mange the function name li on belhalf of Process type selection
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int type = 0;
        type = this.CInt32(ddlType.SelectedValue);

        if (txtpo.Text == "Up")
        {
            string script = "ImgbtnUp_click();";
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "ServerControlScript", script, true);
            if (type == 5)
            {
                liFunction.Visible = true;
            }
        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ImgbtnUp_click", "javascript:ImgbtnDwn_click();", true);

            string script = "ImgbtnDwn_click();";
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "ServerControlScript", script, true);
            if (type == 5)
            {
                liFunction.Visible = true;
            }
        }
    }
    protected void imgCloseTree_OnClick(object sender, ImageClickEventArgs e)
    {
        ClearControl();
        string script = "unloadPopupBox();";
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript", script, true);
    }
}
