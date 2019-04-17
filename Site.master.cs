using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    #region
    string ImgUrl = "";
  #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTreeData();
           // fillTri(null, null);
            //CreateDivRuntime();
        }
    }

    public void fillTri(TreeNode parent, string ParentID)
    {
        VisualERPDataContext db = new VisualERPDataContext();
      
        var items = db.tbl_Processes.Where(x => x.ParentID == null).ToList();
        if (ParentID != null)
        {
            items = db.tbl_Processes.Where(x => x.ParentID == Convert.ToInt32(ParentID)).ToList();
            parent.ChildNodes.Clear();
        }
        else
        {
            try
            {
                parent.ChildNodes.Clear();
            }
            catch { }
        }
        foreach (var item in items)
        {
            TreeNode node = new TreeNode();
            node.Text = item.ProcessName;
            node.Value = item.ProcessID.ToString();
            if (parent == null && ParentID == null)
            {
                node.PopulateOnDemand = true;
                node.SelectAction = TreeNodeSelectAction.Expand;

                node.Expand();
               TreeView1.Nodes.Add(node);
            }
            else
            {
                if (node.ChildNodes.Count == 0)
                {
                    node.PopulateOnDemand = true;
                    node.SelectAction = TreeNodeSelectAction.Expand;

                }
                node.CollapseAll();
                parent.ChildNodes.Add(node);
            }

        }
    }


    protected void MyTreeView_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        fillTri(e.Node, e.Node.Value);
    }
    void BindTreeData()
    {
        VisualERPDataContext objData = new VisualERPDataContext();
        /*Declare Parent Node String Array */
        string[,] ParentNode = new string[50, 3];
        int Tcount = 0;
        /* BindAll Parent Table Data*/
        List<tbl_Process> obParentList = (from Cdata in objData.GetTable<tbl_Process>()
                                          where Cdata.ParentID == null 
                                          select Cdata).ToList<tbl_Process>();

        // objData.GetTable<tbl_Process>().ToList<tbl_Process>();
        foreach (var Parent in obParentList)
        {
            ParentNode[Tcount, 0] = Parent.ProcessID.ToString();
            ParentNode[Tcount++, 1] = Parent.ProcessName.ToString();
           // ParentNode[Tcount++, 2] = Convert.ToString(Parent.imageUrl);
        }
        for (int CloopID = 0; CloopID < Tcount; CloopID++)
        {
            TreeNode rootNode = new TreeNode();
            rootNode.Text = ParentNode[CloopID, 1];
            rootNode.Value = ParentNode[CloopID, 0];
            /*Bindall child data with Parent id*/
            List<tbl_Process> objChild = (from Cdata in objData.GetTable<tbl_Process>()
                                          where Cdata.ParentID == Convert.ToInt32(ParentNode[CloopID, 0])
                                          select Cdata).ToList<tbl_Process>();
            foreach (var Child in objChild)
            {
                TreeNode childNode = new TreeNode();
                childNode.Text = Child.ProcessName.ToString();
                childNode.Value = Child.ProcessID.ToString();
                rootNode.ChildNodes.Add(childNode);

                List<tbl_Process> objSubChild = (from Cdata in objData.GetTable<tbl_Process>()
                                                 where Cdata.ParentID == Convert.ToInt32(Child.ProcessID.ToString())
                                                 select Cdata).ToList<tbl_Process>();
                foreach (var SubChild in objSubChild)
                {
                    TreeNode SubchildNode = new TreeNode();
                    SubchildNode.Text = SubChild.ProcessName.ToString();
                    SubchildNode.Value = SubChild.ProcessID.ToString();
                    childNode.ChildNodes.Add(SubchildNode);
                }
            }
            /*BindRoots and child node to Table*/
            TreeView1.Nodes.Add(rootNode);
            TreeView1.ExpandAll();
        }
    }
    protected void btUp_Click(object sender, EventArgs e)
    {

    }
    protected void btDown_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
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
            objData.tbl_Processes.InsertOnSubmit(obj);
            objData.SubmitChanges();
            int newid = obj.ProcessID; ;

            var objC  = (from Cdata in objData.tbl_Processes
                            where Cdata.ProcessID == Convert.ToInt32(selNode)
                            select Cdata).FirstOrDefault();
            objC.ParentID = newid;
            objData.SubmitChanges();



        }
        else {

            tbl_Process obj = new tbl_Process();
            obj.ParentID = Convert.ToInt32(selNode);
            obj.ProcessName = txtNodeName.Text;
            objData.tbl_Processes.InsertOnSubmit(obj);
            objData.SubmitChanges();
        }
    }


    public void CreateDivRuntime()
    {
        Image ThumbImg = new Image();
        ThumbImg.ImageUrl = "~/images/line_line_arrow_end.png";
       
        System.Web.UI.HtmlControls.HtmlGenericControl createDiv =
        new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");      
        createDiv.ID = "createDiv";
        createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "yellow");
        createDiv.Style.Add(HtmlTextWriterStyle.Color, "Red");
        createDiv.Style.Add(HtmlTextWriterStyle.Height, "250px");
        createDiv.Style.Add(HtmlTextWriterStyle.Width, "250px");
        createDiv.InnerHtml = " I'm a div, from code behind "+"~/images/line_line_arrow_end.png";
        
        //createDiv.Style.Add(HtmlTextWriterStyle.BackgroundImage, "~/images/line_line_arrow_end.png");
        //this.Controls.Add(createDiv);
        //createDiv.Controls.Add(ThumbImg);
        rghtDiv.Controls.Add(createDiv);
        
        rghtDiv.Controls.Add(ThumbImg);

    
        
    }

    //void BindTreeData()
    //{
    //    VisualERPDataContext objData = new VisualERPDataContext();/// Declare the linq object for accessing tables and data
    //    /*Declare Parent Node String Array */
    //    string[,] ParentNode = new string[50, 3];
    //    int Tcount = 0;
    //    /* BindAll Parent Table Data*/
    //    List<tbl_Bussiness> objMainParent = (from x in objData.GetTable<tbl_Bussiness>()/// fire the linq query for parent table Business
    //                                         select x).ToList<tbl_Bussiness>();
    //    /*Manage the loop for parent data*/
    //    foreach (var mainParent in objMainParent)/// for checking parent data bind with the local variable and accessing
    //    {
    //        ParentNode[Tcount, 0] = mainParent.BusinessID.ToString();
    //        ParentNode[Tcount++, 1] = mainParent.BusinessName.ToString();
    //    }
    //    for (int CloopID = 0; CloopID < Tcount; CloopID++)
    //    {
    //        TreeNode rootNode = new TreeNode();///Creating the tree node object
    //        rootNode.Text = ParentNode[CloopID, 1];
    //        rootNode.Value = ParentNode[CloopID, 0];
    //        /*Bindall Busines unit child data with Parent id*/
    //        List<tbl_BusinessUnit> objBusiUnitChild = (from Y in objData.GetTable<tbl_BusinessUnit>()
    //                                                   where Y.BusinessID == Convert.ToInt32(ParentNode[CloopID, 0])
    //                                                   select Y).ToList<tbl_BusinessUnit>();/// fire the linq query for child table BusinessUnit and pass the parentID of Business table in where condition
    //        foreach (var busiUnitChild in objBusiUnitChild)/// for checking parent data bind with the local variable and accessing
    //        {
    //            TreeNode childNode = new TreeNode();
    //            childNode.Text = busiUnitChild.BusinessUnitName.ToString();
    //            childNode.Value = busiUnitChild.BusinessUnitID.ToString();
    //            rootNode.ChildNodes.Add(childNode);

    //            List<tbl_BusinessSegment> objBusiSegmentSubChild = (from z in objData.GetTable<tbl_BusinessSegment>()
    //                                                                where z.BusinessUnitID == Convert.ToInt32(busiUnitChild.BusinessUnitID.ToString())
    //                                                                select z).ToList<tbl_BusinessSegment>();/// fire the linq query for child table BusinessSegment and pass the parentID of Business Unit table  in where condition
    //            foreach (var SubChild in objBusiSegmentSubChild)/// for checking subchild data bind with the local variable and accessing
    //            {
    //                TreeNode SubchildNode = new TreeNode();
    //                SubchildNode.Text = SubChild.BusinessSegmentName.ToString();
    //                SubchildNode.Value = SubChild.BusinessSegmentID.ToString();
    //                childNode.ChildNodes.Add(SubchildNode);

    //                List<tbl_System> objSystemSubChild1 = (from z1 in objData.GetTable<tbl_System>()
    //                                                       where z1.BusinessSegmentID == Convert.ToInt32(SubChild.BusinessSegmentID.ToString())
    //                                                       select z1).ToList<tbl_System>();/// fire the linq query for child table System child and pass the parentID of Business Segment table  in where condition
    //                foreach (var SubChild1 in objSystemSubChild1)/// for checking subchild data bind with the local variable and accessing
    //                {
    //                    TreeNode SubchildNode1 = new TreeNode();
    //                    SubchildNode1.Text = SubChild1.SystemName.ToString();
    //                    SubchildNode1.Value = SubChild1.SystemID.ToString();
    //                    SubchildNode.ChildNodes.Add(SubchildNode1);

    //                    List<tbl_Process> obParentList = (from Cdata in objData.GetTable<tbl_Process>()
    //                                                      where Cdata.SystemID == Convert.ToInt32(SubChild1.SystemID.ToString())
    //                                                      select Cdata).ToList<tbl_Process>();/// fire the linq query for child table Process table and pass the parentID of System table  in where condition

    //                    foreach (var ProcessParent in obParentList)/// for checking subchild data bind with the local variable and accessing
    //                    {
    //                        TreeNode SubchildNode2 = new TreeNode();
    //                        SubchildNode2.Text = ProcessParent.ProcessName.ToString();
    //                        SubchildNode2.Value = ProcessParent.ProcessID.ToString();
    //                        SubchildNode1.ChildNodes.Add(SubchildNode2);

    //                        List<tbl_Process> objChild = (from Cdata in objData.GetTable<tbl_Process>()
    //                                                      where Cdata.ParentID == Convert.ToInt32(ProcessParent.ProcessID.ToString())
    //                                                      select Cdata).ToList<tbl_Process>();/// fire the linq query for child table Process table and pass the parentID of Process table   in where condition
    //                        foreach (var Child in objChild)/// for checking subchild data bind with the local variable and accessing
    //                        {
    //                            TreeNode childNode1 = new TreeNode();
    //                            childNode1.Text = Child.ProcessName.ToString();
    //                            childNode1.Value = Child.ProcessID.ToString();
    //                            SubchildNode2.ChildNodes.Add(childNode1);
    //                            List<tbl_Process> objSubChild = (from Cdata in objData.GetTable<tbl_Process>()
    //                                                             where Cdata.ParentID == Convert.ToInt32(Child.ProcessID.ToString())
    //                                                             select Cdata).ToList<tbl_Process>();///fire the linq query for child table Process table and pass the parentID of Process table   in where condition
    //                            foreach (var ProcessSubChild in objSubChild)///for checking subchild data bind with the local variable and accessing
    //                            {
    //                                TreeNode SubchildNodeProcess = new TreeNode();/// Create the final object Tree Node of child node 
    //                                SubchildNodeProcess.Text = ProcessSubChild.ProcessName.ToString();
    //                                SubchildNodeProcess.Value = ProcessSubChild.ProcessID.ToString();
    //                                childNode1.ChildNodes.Add(SubchildNodeProcess);
    //                            }
    //                        }

    //                    }
    //                }

    //            }
    //        }
    //        /*BindRoots and child node to Table*/
    //        TreeView1.Nodes.Add(rootNode);
    //        TreeView1.ExpandAll();
    //    }

    //}
}
