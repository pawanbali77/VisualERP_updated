using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class UserControls_ImageControl : System.Web.UI.UserControl
{
    private int _ProcessObjectId;
    private string _ControlId;

    private int _Top;
    private int _Left;
    private int _Type;
    private string _Title;

    // Control's Unique Database Id
    [BrowsableAttribute(true)]
    public int ProcessObjectId
    {
        //get { return 0; }
        set
        {
            _ProcessObjectId = value;
           
            ViewState["ImgControlPoid"] = _ProcessObjectId;
        }
    }

    // Unique Supplier Id
    [BrowsableAttribute(true)]
    public string ControlId
    {
        set { _ControlId = value; }
    }

    // Control position from top
    [BrowsableAttribute(true)]
    public int Top
    {
        set { _Top = value; }
    }

    // Control position from left
    [BrowsableAttribute(true)]
    public int Left
    {
        set { _Left = value; }
    }

    // Control position from left
    [BrowsableAttribute(true)]
    public int Type
    {
        set { _Type = value; }
    }

    // Unique Supplier Name
    [BrowsableAttribute(true)]
    public string Title
    {
        set { _Title = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string absolutepath = Request.Url.AbsolutePath;
        string returnurl = absolutepath.Substring(absolutepath.LastIndexOf('/') + 1);
        if ((returnurl == "ProcessManager.aspx") || (returnurl == "EnterPriseManager.aspx"))
        {
            lnkbtnDeleteImageControl.Enabled = false;
            lnkbtnDeleteImageControl.Style.Add("cursor", " default!important");
        }

        divSelectedControl.Style.Add("top", _Top.ToString() + "px");
        divSelectedControl.Style.Add("left", _Left.ToString() + "px");
        divSelectedControl.Style.Add("width", "200px!important");
        divSelectedControl.Style.Add("height", "200px!important");
        //divSelectedControl.Attributes["name"] = _ControlId;
        divSelectedControl.InnerText = _Title;
        //  imgSelectControl.ImageUrl = "~/images/Controls/yourimage.jpg";
        switch (_Type)
        {
            case 18:

                // divSelectedControl.Attributes.Add("style", " background-image: url('" +Resources.Path.Batch_Kanban+ "');");
                // divSelectedControl.ImageUrl = Resources.Path.Batch_Kanban;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Batch_Kanban + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "50px");
                lnkbtnDeleteImageControl.Style.Add("left", "0px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CBatchKanban_" + _ProcessObjectId;
                break;
            case 2:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Customer_Supplier + "');");
                //divSelectedControl.ImageUrl = Resources.Path.Customer_Supplier;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Customer_Supplier + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "50px");
                lnkbtnDeleteImageControl.Style.Add("left", "5px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CSupplier_" + _ProcessObjectId;
                break;

            case 3:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Customer_Supplier + "');");
                //divSelectedControl.ImageUrl = Resources.Path.Customer_Supplier;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.External_Shipment + "')");                
                lnkbtnDeleteImageControl.Style.Add("top", "50px");
                lnkbtnDeleteImageControl.Style.Add("left", "5px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CShipment_" + _ProcessObjectId;
                break;

            case 4:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Customer_Supplier + "');");
                //divSelectedControl.ImageUrl = Resources.Path.Customer_Supplier;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Market_Forcast + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "-8px");
                lnkbtnDeleteImageControl.Style.Add("left", "-7px");                
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CForcast_" + _ProcessObjectId;
                break;
            case 5:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Customer_Supplier + "');");
                //divSelectedControl.ImageUrl = Resources.Path.Customer_Supplier;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Annual_Production + "')");
                //lnkbtnDeleteImageControl.Style.Add("top", "-6px");
                lnkbtnDeleteImageControl.Style.Add("top", "0px");
                lnkbtnDeleteImageControl.Style.Add("left", "172px");  
                //lnkbtnDeleteImageControl.Style.Add("left", "56px");
                //divSelectedControl.Style.Add("width", "50px!important");
                //divSelectedControl.Style.Add("height", "50px!important");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CAnnual_" + _ProcessObjectId;
                break;

            case 6:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Customer_Supplier + "');");
                //divSelectedControl.ImageUrl = Resources.Path.Customer_Supplier;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Delivery_Schedule + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "6px");
                lnkbtnDeleteImageControl.Style.Add("left", "170px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CDSchedule_" + _ProcessObjectId;
                break;

            case 7:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Customer_Supplier + "');");
                //divSelectedControl.ImageUrl = Resources.Path.Customer_Supplier;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Production_Control + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "23px");
                lnkbtnDeleteImageControl.Style.Add("left", "172px");
                //divSelectedControl.Style.Add("width", "120px!important");
                //divSelectedControl.Style.Add("height", "106px!important");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CProduction_" + _ProcessObjectId;
                break;
            case 9:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Date_Table + "');");
                //  divSelectedControl.ImageUrl = Resources.Path.Date_Table;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Date_Table + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "5px");
                lnkbtnDeleteImageControl.Style.Add("left", "4px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CDateTable_" + _ProcessObjectId;
                break;
            case 8:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Electronic_Information + "');");
                // imgSelectControl.ImageUrl = Resources.Path.Electronic_Information;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Electronic_Information + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "67px");
                lnkbtnDeleteImageControl.Style.Add("left", "95px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CElectronic_" + _ProcessObjectId;
                break;
            //case 5:
            //    // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.External_Shipment + "');");
            //    //imgSelectControl.ImageUrl = Resources.Path.External_Shipment;
            //    divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.External_Shipment + "')");
            //    lnkbtnDeleteImageControl.Style.Add("top", "50px");
            //    lnkbtnDeleteImageControl.Style.Add("left", "5px");
            //    break;
            case 20:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.FIFO_Lane + "');");
                // imgSelectControl.ImageUrl = Resources.Path.FIFO_Lane;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.FIFO_Lane + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "70px");
                lnkbtnDeleteImageControl.Style.Add("left", "171px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CFIFO_" + _ProcessObjectId;
                break;
            //case 7:
            //    // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Go_See_Production + "');");
            //    // imgSelectControl.ImageUrl = Resources.Path.Go_See_Production;
            //    divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Go_See_Production + "')");
            //    lnkbtnDeleteImageControl.Style.Add("top", "50px");
            //    lnkbtnDeleteImageControl.Style.Add("left", "5px");
            //    break;
            //case 8:
            //  //  divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Inventory + "');");
            //   // imgSelectControl.ImageUrl = Resources.Path.Inventory;
            //    divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Inventory + "')");
            //    break;
            case 21:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Kaizen_Burst + "');");
                // imgSelectControl.ImageUrl = Resources.Path.Kaizen_Burst;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Kaizen_Burst + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "28px");
                lnkbtnDeleteImageControl.Style.Add("left", "85px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CKaizen_" + _ProcessObjectId;
                break;
            case 19:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Kanban_Post + "');");
                // imgSelectControl.ImageUrl = Resources.Path.Kanban_Post;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Kanban_Post + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "14px");
                lnkbtnDeleteImageControl.Style.Add("left", "156px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CKanbanPost_" + _ProcessObjectId;
                break;
            case 27:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Load_Leveling + "');");
                // imgSelectControl.ImageUrl = Resources.Path.Load_Leveling;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Load_Leveling + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "66px");
                lnkbtnDeleteImageControl.Style.Add("left", "174px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CLoadLeveling_" + _ProcessObjectId;
                break;
            case 25:
                //divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Physical_Pull + "');");
                // imgSelectControl.ImageUrl = Resources.Path.Physical_Pull;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Physical_Pull + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "86px");
                lnkbtnDeleteImageControl.Style.Add("left", "175px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CPhysicalPull_" + _ProcessObjectId;
                break;
            //case 13:
            //    //divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Production_Control + "');");
            //   // imgSelectControl.ImageUrl = Resources.Path.Production_Control;
            //    divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Production_Control + "')");
            //    break;
            case 17:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Production_Kanban + "');");
                //imgSelectControl.ImageUrl = Resources.Path.Production_Kanban;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Production_Kanban + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "42px");
                lnkbtnDeleteImageControl.Style.Add("left", "5px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CPhysicalPull_" + _ProcessObjectId;
                break;
            case 22:
                //divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Pull_Aroow_1 + "');");
                // imgSelectControl.ImageUrl = Resources.Path.Pull_Aroow_1;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Pull_Aroow_1 + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "87px");
                lnkbtnDeleteImageControl.Style.Add("left", "0px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CPArrowF_" + _ProcessObjectId;
                break;
            case 23:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Pull_Aroow_2 + "');");
                // imgSelectControl.ImageUrl = Resources.Path.Pull_Aroow_2;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Pull_Aroow_2 + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "175px");
                lnkbtnDeleteImageControl.Style.Add("left", "0px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CPArrowS_" + _ProcessObjectId;
                break;
            case 24:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Pull_Aroow_3 + "');");
                // imgSelectControl.ImageUrl = Resources.Path.Pull_Aroow_3;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Pull_Aroow_3 + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "83px");
                lnkbtnDeleteImageControl.Style.Add("left", "0px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CPArrowT_" + _ProcessObjectId;
                break;
            //case 18:
            //   // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Push + "');");
            //   // imgSelectControl.ImageUrl = Resources.Path.Push;
            //    divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Push + "')");
            //    break;
            case 13:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Safty_Stock + "');");
                // imgSelectControl.ImageUrl = Resources.Path.Safty_Stock;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Safty_Stock + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "1px");
                lnkbtnDeleteImageControl.Style.Add("left", "133px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CSaftyStock_" + _ProcessObjectId;
                break;
            //case 20:
            //   // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Shipment_Arrow + "');");
            //   // imgSelectControl.ImageUrl = Resources.Path.Shipment_Arrow;
            //    divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Shipment_Arrow + "')");
            //    break;
            case 14:
                //divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Singal_Kanban + "');");
                // imgSelectControl.ImageUrl = Resources.Path.Singal_Kanban;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Singal_Kanban + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "28px");
                lnkbtnDeleteImageControl.Style.Add("left", "172px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CSingalKanban_" + _ProcessObjectId;
                break;
            case 26:
                //divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Siquenced_Pull_Ball + "');");
                // imgSelectControl.ImageUrl = Resources.Path.Siquenced_Pull_Ball;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Siquenced_Pull_Ball + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "18px");
                lnkbtnDeleteImageControl.Style.Add("left", "88px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CSiquencedPullBall_" + _ProcessObjectId;
                break;
            case 12:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Supermarket + "');");
                // imgSelectControl.ImageUrl = Resources.Path.Supermarket;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Supermarket + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "-1px");
                lnkbtnDeleteImageControl.Style.Add("left", "136px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CSupermarket_" + _ProcessObjectId;
                break;
            case 10:
                //divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Timeline_Segment + "');");
                // imgSelectControl.ImageUrl = Resources.Path.Timeline_Segment;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Timeline_Segment + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "87px");
                lnkbtnDeleteImageControl.Style.Add("left", "2px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CTimelineSegment_" + _ProcessObjectId;
                break;
            case 11:
                // divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Timeline_Total + "');");
                // imgSelectControl.ImageUrl = Resources.Path.Timeline_Total;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Timeline_Total + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "87px");
                lnkbtnDeleteImageControl.Style.Add("left", "0px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CTimelineTotal_" + _ProcessObjectId;
                break;
            case 16:
                //divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Withdrawal_Batch + "');");
                // imgSelectControl.ImageUrl = Resources.Path.Withdrawal_Batch;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Withdrawal_Batch + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "48px");
                lnkbtnDeleteImageControl.Style.Add("left", "-1px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CWithdrawalBatch_" + _ProcessObjectId;
                break;
            case 15:
                //divSelectedControl.Attributes.Add("style", " background-image: url('" + Resources.Path.Withdrawal_Kanban + "');");
                //imgSelectControl.ImageUrl = Resources.Path.Withdrawal_Kanban;
                divSelectedControl.Style.Add("background-image", "url('" + Resources.Path.Withdrawal_Kanban + "')");
                lnkbtnDeleteImageControl.Style.Add("top", "41px");
                lnkbtnDeleteImageControl.Style.Add("left", "5px");
                lnkbtnDeleteImageControl.ID = "lnkbtnDel-CWithdrawalKanban_" + _ProcessObjectId;
                break;

            default:
                Console.WriteLine("Default case");
                break;
        }
    }

    //protected void lnkbtnDeleteImageControl_Click1(object sender, EventArgs e)
    //{
        //int processobjId = 0;
        //processobjId = this.CInt32(ViewState["ImgControlPoid"]);
        //if (processobjId > 0)
        //{
        //    bool result = false;
        //    result = ProcessData.DeleteProcessObjDataByID(processobjId);////DeleteTFG is stored procedure in database that will delete selected TFG id from multiple tables

        //    string absolutepath = Request.Url.AbsolutePath;
        //    string returnurl = absolutepath.Substring(absolutepath.LastIndexOf('/') + 1);
        //    if (returnurl == "ProcessManager.aspx")
        //    {
        //        Response.Redirect("ProcessManager.aspx");
        //    }
        //    else
        //    {
        //        Response.Redirect("Production.aspx");
        //    }
        //}

    //}
}