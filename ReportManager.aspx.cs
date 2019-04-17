using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class ReportManage : System.Web.UI.Page
{
    int ProcessId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string scriptJq = "hideAddNode();";  //call jquery function here on page load
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "ServerControlScript", scriptJq, true);
        divReport.Visible = false;
        divRecordCount.Visible = false;
        Label lblManager = (Label)Master.FindControl("lblManager");
        lblManager.Text = "Report Manager";
        lblManager.Attributes.Add("class", "Enterprize");

        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
        {
            if (mastertreeview.SelectedNode.ChildNodes.Count > 0)
            {
                ArrayList List = new ArrayList(); 
                List<ProcessData.ProcessDataProperty> prop = new List<ProcessData.ProcessDataProperty>();              
                for (int i = 0; i < mastertreeview.SelectedNode.ChildNodes.Count; i++)
                {
                    int ProcessId = Convert.ToInt32(mastertreeview.SelectedNode.ChildNodes[i].Value);
                    ViewState["sortBy"] = "CreatedDate";
                    ViewState["isAsc"] = "1";
                    prop = ProcessData.GetReportData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ProcessId));
                    List.AddRange(prop);
                    gridReport.DataSource = List;         
                }
                if (List.Count != 0)
                {
                    divRecordCount.Visible = true;
                    ltrTotalRecord.Text = List.Count.ToString();
                    gridReport.DataBind();
                    divReport.Visible = true;
                }
                else
                {
                    divRecordCount.Visible = false;
                    divReport.Visible = false;
                }

            }
            else
            {
                ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
                Session["SelectedNodeValue"] = mastertreeview.SelectedNode.Value;
                ViewState["sortBy"] = "CreatedDate";
                ViewState["isAsc"] = "1";
                BindGridSummary();
            }

        }
        else if (Session["SelectedNodeValue"] != null)
        {
            ProcessId = this.CInt32(Session["SelectedNodeValue"]);

            TreeNode strNode = mastertreeview.FindNode(Convert.ToString(Session["SelectedNodeValue"]));
            if (strNode != null)
                strNode.Select();

            ViewState["sortBy"] = "CreatedDate";
            ViewState["isAsc"] = "1";
            BindGridSummary();
        }

        //this.Master.FindControl("divSysChoose").Visible = false;
    }

    public void BindGridSummary()
    {
        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");

        List<ProcessData.ProcessDataProperty> prop = ProcessData.GetReportData(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ProcessId));

        if (prop.Count != 0)
        {
            ltrTotalRecord.Text = prop.Count.ToString();
            divRecordCount.Visible = true;
            gridReport.DataSource = prop;
            gridReport.DataBind();
            divReport.Visible = true;
        }
        else
        {
            divRecordCount.Visible = false;
            divReport.Visible = false;
        }
    }

    protected void gridReport_Sorting(object sender, GridViewSortEventArgs e)
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
    protected void gridReport_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void lnkbtnExporttoExcel_Click(object sender, EventArgs e)
    {
        //Response.ClearContent();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Customers.xls"));
        //Response.ContentType = "application/ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        //gridReport.AllowPaging = false;
        //gridReport.DataBind();
        ////Change the Header Row back to white color
        //gridReport.HeaderRow.Style.Add("background-color", "#FFFFFF");
        ////Applying stlye to gridview header cells
        //for (int i = 0; i < gridReport.HeaderRow.Cells.Count; i++)
        //{
        //    gridReport.HeaderRow.Cells[i].Style.Add("background-color", "#507CD1");
        //}
        //int j = 1;
        ////This loop is used to apply stlye to cells based on particular row
        //foreach (GridViewRow gvrow in gridReport.Rows)
        //{
        //    if (j <= gridReport.Rows.Count)
        //    {
        //        if (j % 2 != 0)
        //        {
        //            for (int k = 0; k < gvrow.Cells.Count; k++)
        //            {
        //                gvrow.Cells[k].Style.Add("background-color", "#EFF3FB");
        //            }
        //        }
        //    }
        //    j++;
        //}
        //gridReport.RenderControl(htw);
        //Response.Write(sw.ToString());
        //Response.End();        
    }
   

    //public override void VerifyRenderingInServerForm(GridView control)
    //{
    //    /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
    //       server control at run time. */
    //}
}