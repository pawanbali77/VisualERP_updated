using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hdnProcessId.Value = Request.QueryString["processId"];
        if (!IsPostBack)
            BindGridData();
    }

    private void BindGridData()
    {
        IEnumerable<tbl_ProcessBlockHeader> processHeaders = ProcessHeaderColumns.GetProcessHeader(Convert.ToInt16(hdnProcessId.Value)).ToList();
        if (processHeaders.Count() == 0)
        {
            List<tbl_ProcessBlockHeader> tbl_ProcessBlockHeader = new List<tbl_ProcessBlockHeader>() {
                 new tbl_ProcessBlockHeader() { SequanceOrder=1, ProcessId = 0, Headerlblname = "Attributes"  },
                 new tbl_ProcessBlockHeader() { SequanceOrder=2,Id=1, ProcessId = 0, Headerlblname = "Inputs"  },
                 new tbl_ProcessBlockHeader() { SequanceOrder=3,Id=1, ProcessId = 0, Headerlblname = "BOM" },
                 new tbl_ProcessBlockHeader() { SequanceOrder=4,Id=1, ProcessId = 0, Headerlblname = "TFG" },
                 new tbl_ProcessBlockHeader() { SequanceOrder=5,Id=1, ProcessId = 0, Headerlblname = "Machine" },
                 new tbl_ProcessBlockHeader() { SequanceOrder=6,Id=1, ProcessId = 0, Headerlblname = "Error Report" }

            };

            gridActivityOrder.DataSource = tbl_ProcessBlockHeader;
            gridActivityOrder.DataBind();
        }
        else
        {
            lnkbtn.Text = processHeaders.Where(x => x.SequanceOrder == 1).Select(x => x.Headerlblname).FirstOrDefault();
            lnkBtnInput.Text = processHeaders.Where(x => x.SequanceOrder == 2).Select(x => x.Headerlblname).FirstOrDefault();
            lnkBtnBOM.Text = processHeaders.Where(x => x.SequanceOrder == 3).Select(x => x.Headerlblname).FirstOrDefault();
            lnkBtnTFG.Text = processHeaders.Where(x => x.SequanceOrder == 4).Select(x => x.Headerlblname).FirstOrDefault();
            lnkBtnMachine.Text = processHeaders.Where(x => x.SequanceOrder == 5).Select(x => x.Headerlblname).FirstOrDefault();
            lnkbtnErrorReport.Text = processHeaders.Where(x => x.SequanceOrder == 6).Select(x => x.Headerlblname).FirstOrDefault();
            gridActivityOrder.DataSource = processHeaders;
            gridActivityOrder.DataBind();
        }
    }

    protected void btnshcolApply_Click(object sender, EventArgs e)
    {
        int processId = Convert.ToInt16(hdnProcessId.Value);
        foreach (GridViewRow row in gridActivityOrder.Rows)
        {
            string lblSOrderNo = ((Label)row.FindControl("lblSOrderNo")).Text;
            string lblCName = ((Label)row.FindControl("lblCName")).Text;
            string txtchangename = ((TextBox)row.FindControl("txtchangename")).Text;
            tbl_ProcessBlockHeader processHeaders = ProcessHeaderColumns.GetProcessHeader(Convert.ToInt16(hdnProcessId.Value),Convert.ToInt16(lblSOrderNo));
            if (processHeaders == null)
            {
                processHeaders = new tbl_ProcessBlockHeader();
                processHeaders.CreatedDate = DateTime.UtcNow;
                processHeaders.UpdatedDate = DateTime.UtcNow;
                processHeaders.ProcessId = processId;
                processHeaders.Headerlblname = lblCName;
                processHeaders.SequanceOrder = Convert.ToInt16(lblSOrderNo);
                if (!string.IsNullOrEmpty(txtchangename))
                    processHeaders.Headerlblname = txtchangename;
            }
            else
            {
                processHeaders.UpdatedDate = DateTime.UtcNow;
                processHeaders.ProcessId = processId;
                processHeaders.Headerlblname = lblCName;
                processHeaders.SequanceOrder = Convert.ToInt16(lblSOrderNo);
                if (!string.IsNullOrEmpty(txtchangename))
                    processHeaders.Headerlblname = txtchangename;
            }
            ProcessHeaderColumns.SaveProcessBlockHeaders(processHeaders);
        }
        BindGridData();
     }
 }