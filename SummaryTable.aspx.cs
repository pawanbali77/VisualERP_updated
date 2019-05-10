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

public partial class SummaryTable : System.Web.UI.Page
{
    int ProcessId = 0;
    string EditId = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["lastZoom"] = null; // last zoom for process manager will be set to zero on this page so user goes back to process page original postion will be shown there
        //divSummaryTable.Visible = false;
        EditId = GetPostBackControlId((Page)sender); // to get postback control id that is clicked//////////////////////////////

        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
        {
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
            SetSession();
        }
        else if (Session["SelectedNodeValue"] != null)
        {
            ProcessId = this.CInt32(Session["SelectedNodeValue"]);

            TreeNode strNode = mastertreeview.FindNode(Convert.ToString(Session["SelectedNodeValue"]));
            if (strNode != null)
                strNode.Select();
        }
       
        if (!IsPostBack)
        {

        }

        if (EditId != "lnkbtnSaveSummary" && ProcessId != 0)
            ResetBinding(ProcessId);
    }

    public void ResetBinding(int ProcessId)
    {
        ViewState["sortBy"] = "CreatedDate";
        ViewState["isAsc"] = "1";
        BindGridSummary(ProcessId); //bind gridview sorted by Created Date
    }

    public void BindGridSummary(int ProcessId)
    {
        if (ProcessData.GetSummaryData(ProcessId))
        {
            List<ProcessData.ProcessDataProperty> record = ProcessData.GetSummaryTableRecord(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ProcessId));

            if (record.Count != 0)
            {
                gridProcessSummary.DataSource = record;
                gridProcessSummary.DataBind();
                divSummaryTable.Visible = true;
                lnkbtnSaveSummary.Visible = true;
            }
        }
        else
        {
            List<ProcessData.ProcessDataProperty> prop = ProcessData.GetSummaryDataSum1(this.CBool(ViewState["isAsc"]), ViewState["sortBy"].ToString(), Convert.ToInt32(ProcessId));

            if (prop.Count != 0)
            {
                gridProcessSummary.DataSource = prop;
                gridProcessSummary.DataBind();
                divSummaryTable.Visible = true;
                lnkbtnSaveSummary.Visible = true;
            }
            else
            {
                gridProcessSummary.DataSource = null;
                gridProcessSummary.DataBind();
                divSummaryTable.Visible = false;
                lnkbtnSaveSummary.Visible = false;
            }
        }
    }

    protected void gridProcessSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DropDownList ddlFunction = (e.Row.FindControl("ddlSelectFunction") as DropDownList);
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlSelectFunction");
            Label lblFunctionID = (Label)e.Row.FindControl("lblFunctionID");
            if (lblFunctionID.Text == "0")
            {
                FillEnumFunction(ddl);
            }
            else
            {
                FillEnumFunction(ddl);
                //ReportTypeName = Enum.GetName(typeof(SummaryFunction), lblFunctionID);
                ddl.SelectedValue = lblFunctionID.Text;
            }           
        }
    }

    public void FillEnumFunction(DropDownList ddl)
    {
        ddl.Items.Insert(0, new ListItem("--Select Function--", "0"));
        string[] enumNames = Enum.GetNames(typeof(SummaryFunction));
        foreach (string item in enumNames)
        {
            //get the enum item value
            int value = (int)Enum.Parse(typeof(SummaryFunction), item);
            ListItem listItem = new ListItem(item, value.ToString());
            ddl.Items.Add(listItem);
        }
    }

    protected void lnkbtnSaveSummary_Click(object sender, EventArgs e)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        TreeView mastertreeview = (TreeView)Master.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
            ProcessId = this.CInt32(mastertreeview.SelectedNode.Value);
        else if (Session["SelectedNodeValue"] != null)
        {
            ProcessId = this.CInt32(Session["SelectedNodeValue"]);
        }

        if (ProcessData.DeleteExistingRecord(ProcessId))
        {
            foreach (GridViewRow grd in gridProcessSummary.Rows)
            {
                string AttributeName = (grd.FindControl("lblAttributeName") as Label).Text;
                DropDownList ddl = grd.FindControl("ddlSelectFunction") as DropDownList;
                int FunctionID = Convert.ToInt32(ddl.SelectedValue);

                tbl_SummaryData smmrydata = new tbl_SummaryData();
                smmrydata.AttributeName = AttributeName;
                smmrydata.FunctionName = FunctionID;
                smmrydata.ProcessID = ProcessId;
                try
                {
                    ObjData.tbl_SummaryDatas.InsertOnSubmit(smmrydata);
                    ObjData.SubmitChanges();
                }
                catch
                {
                }
            }
        }
       
        Session["SelectedNodeValue"] = ProcessId;
        Response.Redirect("ProcessManager.aspx");
       
    }

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

    private void SetSession()
    {
        MasterPage mstr = this.Page.Master as MasterPage;
        TreeView mastertreeview = (TreeView)mstr.FindControl("TreeView1");
        if (mastertreeview.SelectedNode != null)
        {
            Session["SelectedNode"] = mastertreeview.SelectedNode.ValuePath;
            Session["SelectedNodeValue"] = mastertreeview.SelectedNode.Value;
        }
    }


    protected void lnkbtnCancelSummary_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProcessManager.aspx");
    }
}