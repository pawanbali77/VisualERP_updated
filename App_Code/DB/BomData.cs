using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BomData
/// </summary>
public class BomData
{
    public BomData()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static List<tbl_BomTypeCollection> GetTypeCollectionBOM()
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_BomTypeCollections
                   select x).ToList();
        return qry;
    }
    public static int GetTypeID(int BomprocessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_BomProcesses
                   where x.BomProcessID == BomprocessId
                   select x.BomTypeID).FirstOrDefault();

        return Convert.ToInt32(qry);
    }
    public static string GetBomNameById(int BomprocessId, int bomTypeid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_BomProcesses
                   where x.BomProcessID == BomprocessId && x.BomTypeID == bomTypeid
                   select x.BomProcessName).FirstOrDefault();

        return Convert.ToString(qry);
    }
    public class BOMTypeDetail
    {

        public int? BomTypeID { get; set; }
        public string BomTypeName { get; set; }
    }

    public static tbl_BOM BomDataById(int BomProcessId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.tbl_BOMs
                where bc.BomProcessID == BomProcessId
                select bc).FirstOrDefault();
    }
    public static bool SaveBomProcessData(tbl_BomProcess BomProcessTblData)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_BomProcesses
                   where x.BomProcessID == BomProcessTblData.BomProcessID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_BomProcesses.InsertOnSubmit(BomProcessTblData);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {
            qry.BomProcessName = BomProcessTblData.BomProcessName;
            qry.BomProcessID = BomProcessTblData.BomProcessID;
            qry.BomTypeID = BomProcessTblData.BomTypeID;
            // qry.SystemID = ProcessTblData.SystemID;

        }
        try
        {
            ObjData.SubmitChanges();

            return true;
        }
        catch
        {

        }
        return false;
    }


    public static List<BomDataProcessProperty> GetAllBomProcess(int poid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_BomProcesses
                   where x.ProcessObjID == poid //&& x.BomTypeID == 6
                   select new BomDataProcessProperty
                   {
                       BomProcessID = x.BomProcessID,
                       BomProcessName = x.BomProcessName,
                       BomParentID = x.BomParentID

                   }).Distinct().ToList();

        return qry.ToList();
    }

    public static List<BomDataProcessProperty> GetAllProcessByProcessId(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_BomProcesses
                   select new BomDataProcessProperty
                   {
                       BomProcessID = x.BomProcessID,
                       BomProcessName = x.BomProcessName,
                       BomParentID = x.BomParentID

                   }).Distinct().ToList();

        return qry.ToList();
    }

    public static bool SaveBomData(tbl_BOM tblBomData)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_BOMs
                   where x.BomProcessID == tblBomData.BomProcessID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_BOMs.InsertOnSubmit(tblBomData);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {
            qry.Description = tblBomData.Description;
            qry.BOMLevel = tblBomData.BOMLevel;
            qry.BOMRevision = tblBomData.BOMRevision;
            qry.BomProcessID = tblBomData.BomProcessID;
            qry.weight = tblBomData.weight;
            qry.UOM = tblBomData.UOM;
            qry.StandardCost = tblBomData.StandardCost;
            qry.WorkingCost = tblBomData.WorkingCost;
            qry.StdPackQty = tblBomData.StdPackQty;
            qry.MaxPackLength = tblBomData.MaxPackLength;
            qry.MaxPackWidth = tblBomData.MaxPackWidth;
            qry.MaxPackHeight = tblBomData.MaxPackHeight;
            qry.ContainerQty = tblBomData.ContainerQty;
            qry.MedianRelinishmentLT = tblBomData.MedianRelinishmentLT;
            qry.MinRLT = tblBomData.MinRLT;
            qry.MaxRLT = tblBomData.MaxRLT;
            qry.Rolling12MnthUsage = tblBomData.Rolling12MnthUsage;
            qry.AvgMonthUsage = tblBomData.AvgMonthUsage;
            qry.MonthStdDevRiskFactor = tblBomData.MonthStdDevRiskFactor;
            qry.RiskFactor = tblBomData.RiskFactor;
            qry.KanbanQty = tblBomData.KanbanQty;
            qry.InService = tblBomData.InService;
            qry.In_ServiceDate = tblBomData.In_ServiceDate;
            qry.ObsolescenceDate = tblBomData.ObsolescenceDate;
            qry.OnHandInventory = tblBomData.OnHandInventory;
            qry.OnOrder = tblBomData.OnOrder;
            qry.NextShipmentDue = tblBomData.NextShipmentDue;
            qry.NextQtyDue = tblBomData.NextQtyDue;
            qry.PartReqNxtPerd = tblBomData.PartReqNxtPerd;
            qry.CurrentPurchasingOwner = tblBomData.CurrentPurchasingOwner;
            qry.CurrentDesignOwner = tblBomData.CurrentDesignOwner;

        }
        try
        {
            ObjData.SubmitChanges();

            return true;
        }
        catch
        {

        }
        return false;
    }

    public static int GetMaxBomProcessId()
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_BomProcesses
                   select x.BomProcessID).Max();
        return Convert.ToInt32(qry);
    }
    public class BomDataProcessProperty
    {
        public string BomProcessName { get; set; }
        public int? BomProcessID { get; set; }
        public int? BomParentID { get; set; }
        //public string ProcessObjectName { get; set; }
        public int ProcessObjID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? BomType { get; set; }
    }

    public class BomProcessData
    {
        public int? BOMID { get; set; }
        public int? BomProcessID { get; set; }
        public string Description { get; set; }
        public string BOMLevel { get; set; }
        public string BOMRevision { get; set; }
        public double? weight { get; set; }
        public string UOM { get; set; }
        public decimal? StandardCost { get; set; }
        public decimal? WorkingCost { get; set; }
        public double? StdPackQty { get; set; }
        public double? MaxPackLength { get; set; }
        public double? MaxPackWidth { get; set; }
        public double? MaxPackHeight { get; set; }
        public double? ContainerQty { get; set; }
        public string MedianRelinishmentLT { get; set; }
        public string MinRLT { get; set; }
        public string MaxRLT { get; set; }
        public string Rolling12MnthUsage { get; set; }
        public string AvgMonthUsage { get; set; }
        public string RiskFactor { get; set; }
        public string MonthStdDevRiskFactor { get; set; }
        public string KanbanQty { get; set; }
        public bool InService { get; set; }
        public string In_ServiceDate { get; set; }
        public string ObsolescenceDate { get; set; }
        public string OnHandInventory { get; set; }
        public string OnOrder { get; set; }
        public string NextShipmentDue { get; set; }
        public string NextQtyDue { get; set; }
        public string PartReqNxtPerd { get; set; }
        public int? CurrentPurchasingOwner { get; set; }
        public int? CurrentDesignOwner { get; set; }
        public string BomProcessName { get; set; }
    }

    public static List<BomProcessData> GetBOMProcessDataByBomProcessID(int BomProcessID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_BOMs
                   where x.BomProcessID == BomProcessID
                   select new BomProcessData
                   {
                       BOMID = x.BOMID,
                       BomProcessID = x.BomProcessID,
                       Description = x.Description,
                       BOMLevel = x.BOMLevel,
                       BOMRevision = x.BOMRevision,
                       weight = Convert.ToDouble(x.weight),
                       UOM = x.UOM,
                       StandardCost = Convert.ToDecimal(x.StandardCost),
                       WorkingCost = Convert.ToDecimal(x.StandardCost),
                       StdPackQty = Convert.ToDouble(x.StdPackQty),
                       MaxPackLength = Convert.ToDouble(x.MaxPackLength),
                       MaxPackWidth = Convert.ToDouble(x.MaxPackWidth),
                       MaxPackHeight = Convert.ToDouble(x.MaxPackHeight),
                       ContainerQty = Convert.ToDouble(x.ContainerQty),
                       MedianRelinishmentLT = x.MedianRelinishmentLT,
                       MinRLT = x.MinRLT,
                       MaxRLT = x.MaxRLT,
                       Rolling12MnthUsage = x.Rolling12MnthUsage,
                       AvgMonthUsage = x.AvgMonthUsage,
                       RiskFactor = x.RiskFactor,
                       MonthStdDevRiskFactor = x.MonthStdDevRiskFactor,
                       KanbanQty = x.KanbanQty,
                       InService = Convert.ToBoolean(x.InService),
                       In_ServiceDate = ChangeDate((x.In_ServiceDate)),
                       ObsolescenceDate = ChangeDate(x.ObsolescenceDate),
                       OnHandInventory = x.OnHandInventory,
                       OnOrder = x.OnOrder,
                       NextShipmentDue = x.NextShipmentDue,
                       NextQtyDue = x.NextQtyDue,
                       PartReqNxtPerd = x.PartReqNxtPerd,
                       CurrentPurchasingOwner = x.CurrentPurchasingOwner,
                       CurrentDesignOwner = x.CurrentDesignOwner,
                       BomProcessName = (from d in Objdata.tbl_BomProcesses
                                         where d.BomProcessID == BomProcessID
                                         select d.BomProcessName).FirstOrDefault()
                   }).ToList();

        return qry.ToList();
    }
    public  static string ChangeDate(DateTime? dt)
    {
        string date = string.Empty;
        if (dt != null)
        {
            date = dt.Value.ToString("ddMMM,yyyy");
        }
        return date;
    }
}