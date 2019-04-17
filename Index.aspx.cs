using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class index : System.Web.UI.Page
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

    protected void lnkbtn_Click(object sender, EventArgs e)
    {
        mopoExUser.Show();
        // divAttr.Disabled = false;
    }


    //protected void lnkBtnProces_Click(object sender, EventArgs e)
    //{

    //    FunctionProcesFirst();

    //}

    //public void FunctionProcesFirst()
    //{
    //    StringBuilder strbuild = new StringBuilder();
    //    strbuild.Append("<table align='center'  width='100%' border='0' cellspacing='0' cellpadding='0'>");
    //    strbuild.Append("<tr><td><table class='activity_block' cellpadding='0' cellspacing='0'>");
    //    strbuild.Append("<tr class='activity_block_top'><td class='th_fieldsi left'><asp:LinkButton ID='lnkbtn' runat='server' OnClick='lnkbtn_Click'>Attributes</asp:LinkButton></td>");
    //    strbuild.Append("<td class='th_fieldsi'><a href='#'>Inputs</a></td>");
    //    strbuild.Append("<td class='th_fieldsi'><a href='#'>BOM</a></td>");
    //    strbuild.Append("<td class='th_fieldsi'><a href='#'>TFG</a></td>");
    //    strbuild.Append("<td class='th_fieldsi'><a href='#'>Machine</a></td>");
    //    strbuild.Append("<tr><td colspan='5' class='activity_txt'>Activity 1</td></tr>");
    //    strbuild.Append("<tr><td colspan='5'> <table border='0' cellpadding='0' cellspacing='0' width='100%'> <tr class='block_1_top'> <td class='attributes'> <a class='block_arrow' href='#'>Attribute</a></td> <td class='value'><a class='block_arrow' href='#'>Value</a></td>");
    //    strbuild.Append("<td class='unit'><a class='block_arrow' href='#'>Unit</a></td></tr>");
    //    strbuild.Append("<tr class='field_row bg_white'><td class='attributes_td'>VA Time:</td><td class='value_td'>12</td><td class='unit_td'>Sec</td></tr>");
    //    strbuild.Append("<tr class='field_row'><td class='attributes_td'>NVA Ti</td><td class='value_td'>15</td><td class='unit_td'>Min</td></tr>");
    //    strbuild.Append("<tr class='field_row bg_white'><td class='attributes_td'>VA Time</td><td class='value_td'>1</td><td class='unit_td'>Min</td></tr>");
    //    strbuild.Append("<tr class='field_row'><td class='attributes_td'>NVA Time</td><td class='value_td'>15</td><td class='unit_td'>Min</td></tr></table></td> </tr>");
    //    strbuild.Append("</table></td><td><div class='nxt_arrow'><img src='images/nxt_arrow.png'/></div></td>");
    //    MainDiv.InnerHtml=" ";
    //    MainDiv.InnerHtml=strbuild.ToString();

    //    strbuild.Append("<td><table class='small_block' cellpadding='0' cellspacing='0'><tr><td class='small_block_top' colspan='3'><img src='images/small_block_top.png'/></td></tr>");
    //    strbuild.Append("<tr class=block_1_top'><td class='Small_b3' style='padding-left: 2%; width: 32%; border-left: 1px solid #CCCCCC;><a class='block_arrow' href='#'>CT</a></td><td class='Small_b3' style='padding-left: 2%; width: 29.9%;><a class='block_arrow' href='#'>$</a></td>");
    //    strbuild.Append("<td class='Small_b3' style='border-right: 1px solid #CCCCCC;'><a class='block_arrow' href='#'>Time</a></td></tr>");
    //    strbuild.Append("<tr class='bg_white' style='box-shadow: 4px 5px 4px #F3F3F3;'><td class='Small_b3' style='padding-left: 2%; width: 32%; border-left: 1px solid #CCCCCC;border-right: 1px solid #CCCCCC;'>CT</td><td class='Small_b3' style='padding-left: 2%; width: 26.8%; border-right: 1px solid #CCCCCC;'>$</td>");
    //    strbuild.Append("<td class='Small_b3' style='border-right: 1px solid #CCCCCC; padding-left: 2%;'>Time</td></tr></table> </td>");

    //    strbuild.Append("<td><div class='nxt_arrow'><img src='images/nxt_arrow.png' /></div></td>");

    //    strbuild.Append("</tr></table>");



    //}


}