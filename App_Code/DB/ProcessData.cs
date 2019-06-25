using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProcessData
/// </summary>
public class ProcessData
{
    public ProcessData()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetProcessNameById(int processId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_Processes
                   where x.ProcessID == processId
                   select x.ProcessName).FirstOrDefault();

        return Convert.ToString(qry);
    }
    public static bool DeleteProcessNodeFromTree(int ProcessID)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();

        if (ProcessID > 0)
        {
            ObjData.DeleteProcessNodeFromTree(ProcessID);
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }
    public static bool UpdateProcessNodeFromTree(string ProcessName, int ProcessID)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();

        if (ProcessID > 0)
        {
            ObjData.UpdateProcessNodeName(ProcessID, ProcessName);
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }
    public static string GetProcessNameByPoid(int poid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_Processes
                   join y in Objdata.tbl_ProcessObjects
                   on x.ProcessID equals y.ProcessID
                   where y.ProcessObjID == poid
                   select x.ProcessName).FirstOrDefault();

        return Convert.ToString(qry);
    }

    public static string GetFunctionNameById(int processId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_Processes
                   where x.ProcessID == processId && x.TypeID == 5
                   select x.FunctionName).FirstOrDefault();

        return Convert.ToString(qry);
    }

    public static string GetProcessObjNameById(int poid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessObjID == poid
                   select x.ProcessObjName).FirstOrDefault();

        return Convert.ToString(qry);
    }

    public static bool SaveProcessData(tbl_Process ProcessTblData)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_Processes
                   where x.ProcessID == ProcessTblData.ProcessID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_Processes.InsertOnSubmit(ProcessTblData);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {
            qry.ProcessName = ProcessTblData.ProcessName;
            qry.FunctionName = ProcessTblData.FunctionName;
            qry.ProcessID = ProcessTblData.ProcessID;
            qry.TypeID = ProcessTblData.TypeID;
            qry.UserRegisterID = ProcessTblData.UserRegisterID;
            qry.CompanyID = ProcessTblData.CompanyID;

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

    public static bool GetDuplicateCheck(string ProcessName, int ProcessId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        if (ProcessId > 0)
        {
            //Process Name Count will get Process from table Process on behalf of ProcessName
            var ProcessNameCount = (from c in ObjData.tbl_Processes
                                    where c.ProcessName.ToLower() == ProcessName.ToLower()
                                     && c.ProcessID != ProcessId
                                    select c.ProcessID).Count();
            if (ProcessNameCount > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            //countcat variable will get ProcessName from table tbl_Processes on behalf of ProcessName
            var countCat = (from c in ObjData.tbl_Processes
                            where c.ProcessName.ToLower() == ProcessName.ToLower()
                            select c.ProcessID).Count();
            if (countCat > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public static bool SaveDumyProcessObject(tbl_ProcessObject tblProcessDummyObject)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_ProcessObjects
                   where x.ProcessObjID == tblProcessDummyObject.ProcessObjID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_ProcessObjects.InsertOnSubmit(tblProcessDummyObject);
            // return 
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {
            qry.ProcessObjName = tblProcessDummyObject.ProcessObjName;
            qry.ProcessID = tblProcessDummyObject.ProcessID;
            qry.ModifiedDate = tblProcessDummyObject.ModifiedDate;
            qry.OrderNo = tblProcessDummyObject.OrderNo;
            qry.Width = tblProcessDummyObject.Width;
            qry.Height = tblProcessDummyObject.Height;
            qry.Type = 0;
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


    public static bool SaveDumyTargetObject(tbl_TargetObject tblProcessDummyObject)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_TargetObjects
                   where x.TargetObjID == tblProcessDummyObject.TargetObjID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_TargetObjects.InsertOnSubmit(tblProcessDummyObject);
            // return 
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {
            qry.TargetObjName = tblProcessDummyObject.TargetObjName;
            qry.TargetID = tblProcessDummyObject.TargetID;
            qry.ModifiedDate = tblProcessDummyObject.ModifiedDate;
            qry.OrderNo = tblProcessDummyObject.OrderNo;
            qry.Width = tblProcessDummyObject.Width;
            qry.Height = tblProcessDummyObject.Height;
            qry.Type = 0;
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

    public static bool SaveProcessObject(tbl_ProcessObject tblProcessObject)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_ProcessObjects
                   where x.ProcessObjID == tblProcessObject.ProcessObjID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_ProcessObjects.InsertOnSubmit(tblProcessObject);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {
            qry.ProcessObjName = tblProcessObject.ProcessObjName;
            qry.ProcessID = tblProcessObject.ProcessID;
            qry.ModifiedDate = tblProcessObject.ModifiedDate;
            qry.OrderNo = tblProcessObject.OrderNo;
            qry.XTop = tblProcessObject.XTop;
            qry.YLeft = tblProcessObject.YLeft;
            qry.Width = tblProcessObject.Width;
            qry.Height = tblProcessObject.Height;
            qry.Title = tblProcessObject.Title;
            qry.Position = tblProcessObject.Position;
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
    public static bool SaveTargetObject(tbl_TargetObject tblProcessObject)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_TargetObjects
                   where x.TargetObjID == tblProcessObject.TargetObjID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_TargetObjects.InsertOnSubmit(tblProcessObject);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {
            qry.TargetObjName = tblProcessObject.TargetObjName;
            qry.TargetID = tblProcessObject.TargetID;
            qry.ModifiedDate = tblProcessObject.ModifiedDate;
            qry.OrderNo = tblProcessObject.OrderNo;
            qry.XTop = tblProcessObject.XTop;
            qry.YLeft = tblProcessObject.YLeft;
            qry.Width = tblProcessObject.Width;
            qry.Height = tblProcessObject.Height;
            qry.Title = tblProcessObject.Title;
            qry.Position = tblProcessObject.Position;
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

    public static int GetMaxOrderID()
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   select x.OrderNo).Max();
        return Convert.ToInt32(qry);
    }
    public static int GetMaxTargetOrderID()
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_TargetObjects
                   select x.OrderNo).Max();
        return Convert.ToInt32(qry);
    }
    public static int GetMaxProcessObjId(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == processID
                   select x.ProcessObjID).Max();
        return Convert.ToInt32(qry);
    }


    public static int GetMaxTargetObjId(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == processID
                   select x.TargetObjID).Max();
        return Convert.ToInt32(qry);
    }
    public static List<ProcessDataProperty> GetAllProcessObjId(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == processID && (x.Type == 0 || x.Type == 1) && x.ParallelProcessObjID == null
                   select new ProcessDataProperty
                   {
                       ProcessObjID = x.ProcessObjID,
                       Type = x.Type

                   }).Distinct().ToList();

        return qry.ToList();
    }

    public static List<ProcessDataProperty> GetAllProcessID()
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_Processes
                   select new ProcessDataProperty
                   {
                       ProcessID = x.ProcessID,
                       ProcessName = x.ProcessName,
                       ParentID = x.ParentID,

                   }).Distinct().ToList();

        return qry.ToList();
    }
    public static List<ProcessDataProperty> GetAllProcessID_test(int UserID, int CompanyId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();

        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_Processes
                       // where x.ProcessID == Id && (x.ParentID == null || x.ParentID == 0)
                       // where x.ProcessID == Id && x.CompanyID == 10
                   where x.CompanyID == CompanyId && x.UserRegisterID == UserID
                   select new ProcessDataProperty
                   {
                       ProcessID = x.ProcessID,
                       ProcessName = x.ProcessName,
                       ParentID = x.ParentID

                   }).Distinct().ToList();

        foreach (var row in qry)
        {
            bool isData = Objdata.tbl_TargetObjects.Where(po => po.TargetID == row.ProcessID).Any();
            if (isData)
            {
                row.ProcessName = row.ProcessName + "_TV";
            }

        }
        return qry.ToList();
    }

    public static tbl_ProcessObject ProcessObjectByID(Int32 ProcessObjid)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.tbl_ProcessObjects
                where bc.ProcessObjID == ProcessObjid
                select bc).FirstOrDefault();
    }

    public static tbl_InvantoryTriangle ProcessObjectInventoryByID(Int32 ProcessObjid)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.tbl_InvantoryTriangles
                where bc.ProcessObjID == ProcessObjid
                select bc).FirstOrDefault();
    }
    public static string GetInventoryName(Int32? ProcessObjid)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.tbl_ProcessObjects
                where bc.ProcessObjID == ProcessObjid
                select bc.ProcessObjName).FirstOrDefault();
    }

    public static List<ProcessDataProperty> GetActivityOrderData(bool inAsc, string SortBy, int processObjID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join y in ObjData.tbl_Units
                   on x.UnitID equals y.UnitID
                   where x.ProcessObjectID == processObjID && x.IncludeOnMap == true
                   select new ProcessDataProperty
                   {
                       AttributeName = x.AttributeName,
                       AttributeMenuID = x.AttributeMenuID,
                       AttributeValue = x.AttributeValue,
                       UnitName = y.UnitName
                   }).Distinct().OrderBy(a => a.AttributeName).ToList();
        if (inAsc)
        {
            return qry.OrderByDescending(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
    }

    public static List<ProcessDataProperty> GetSummaryData(bool inAsc, string SortBy, int processId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join y in ObjData.tbl_Units
                   on x.UnitID equals y.UnitID
                   where x.ProcessID == processId && x.IncludeOnMap == true
                   select new ProcessDataProperty
                   {
                       AttributeName = x.AttributeName,
                       AttributeMenuID = x.AttributeMenuID,
                       AttributeValue = x.AttributeValue,
                       UnitName = y.UnitName

                   }).Distinct().ToList();
        if (inAsc)
        {
            return qry.OrderByDescending(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
    }

    public static List<ProcessDataProperty> GetSummaryDataSum(bool inAsc, string SortBy, int processId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
                   where x.ProcessID == processId
                   group new { x, y } by new { x.AttributeName, x.AttributeValue, y.UnitName } into g
                   select new ProcessDataProperty
                   {
                       AttributeName = g.Key.AttributeName,
                       AttributeValueSum = (ObjData.tbl_AttributesMenus.Where(a => a.ProcessID == processId && a.AttributeName == g.Key.AttributeName).Sum(a => Convert.ToInt32(a.AttributeValue))),
                       UnitName = g.Key.UnitName,
                   }).Distinct().ToList();

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();

    }

    public static List<ProcessData.ProcessDataProperty> GetReportData(bool inAsc, string SortBy, int processId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join z in ObjData.tbl_Processes on x.ProcessID equals z.ProcessID
                   join y in ObjData.tbl_Units
                   on x.UnitID equals y.UnitID
                   where x.ProcessID == processId && x.IncludeOnMap == true
                   select new ProcessDataProperty
                   {
                       AttributeName = x.AttributeName,
                       AttributeMenuID = x.AttributeMenuID,
                       AttributeValue = x.AttributeValue,
                       UnitName = y.UnitName,
                       NodeName = z.ProcessName

                   }).Distinct().ToList();
        if (inAsc)
        {
            return qry.OrderByDescending(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
    }
    public static List<ProcessDataProperty> GetAllProcessByProcessId(int ProcessId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_Processes
                   where x.ProcessID == ProcessId || x.ParentID == null
                   select new ProcessDataProperty
                   {
                       ProcessID = x.ProcessID,
                       ProcessName = x.ProcessName,
                       ParentID = x.ParentID

                   }).Distinct().ToList();
        return qry.ToList();
    }

    /// <summary>
    /// DeleteMachineDatabyId() will delete selected Mchine Data from database mulitple tables
    /// </summary>
    /// <param name="Machine ID">Machine ID is current Machine ID that is selected</param>
    /// <returns></returns>
    public static bool DeleteProcessObjDataByID(int Poid)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var ProcessObjData = (from k in ObjData.tbl_ProcessObjects
                              where k.ProcessObjID == Poid
                              select k).ToList();
        if (ProcessObjData.Count > 0)
        {
            ObjData.DeleteProcessObjDataByID(Poid); //DeleteMachine is stored procedure in database that delete TFGData of TFG Id
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    /// <summary>
    /// DeleteMachineDatabyId() will delete selected Mchine Data from database mulitple tables
    /// </summary>
    /// <param name="Machine ID">Machine ID is current Machine ID that is selected</param>
    /// <returns></returns>
    public static bool DeleteParallelProcessObjDataByID(int Poid)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var ProcessObjData = (from k in ObjData.tbl_ParallelRelationships
                              where k.ProcessObjID == Poid
                              select k).ToList();
        if (ProcessObjData.Count > 0)
        {
            ObjData.DeleteParallelProcessObjDataByID(Poid); //DeleteMachine is stored procedure in database that delete TFGData of TFG Id
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    /// <summary>
    /// DeleteAttributedataByPoID() will delete selected Attribute Data from database mulitple tables
    /// </summary>
    /// <param name="Process Object ID">Process Object  ID is current >Process Object  ID that is selected</param>
    /// <returns></returns>
    public static bool DeleteAttributedataByPoID(int Poid)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var AttributeObjData = (from k in ObjData.tbl_AttributesMenus
                                where k.ProcessObjectID == Poid
                                select k).ToList();
        if (AttributeObjData.Count > 0)
        {
            ObjData.DeleteAttributeDataByProcessObjID(Poid); //DeleteAttributeDataByProcessObj ID is stored procedure in database that delete AttributeData of By ProcessObjID
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }


    /// <summary>
    /// DeleteSystemIODataByPOID() will delete selected SystemIOData Data from database mulitple tables
    /// </summary>
    /// <param name="Process Object ID">Process Object  ID is current >Process Object  ID that is selected</param>
    /// <returns></returns>
    public static bool DeleteSystemIODataByPoID(int Poid)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var SystemIOData = (from k in ObjData.tbl_SystemIOs
                            where k.FromActivityID == Poid || k.ToActivityID == Poid
                            select k).ToList();
        if (SystemIOData.Count > 0)
        {
            ObjData.DeleteSystemIODataByPOID(Poid); //DeleteAttributeDataByProcessObj ID is stored procedure in database that delete AttributeData of By ProcessObjID
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    public static List<tbl_Process> GetProcessCollection(int systemId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_Processes
                   join y in Objdata.tbl_SystemProcessIOs
                   on x.ProcessID equals y.ProcessID
                   where y.SystemID == systemId
                   select x).ToList();
        return qry;
    }
    public static List<tbl_ProcessObject> GetProcessObjActvityCollection(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == ProcessId && x.Type == 0
                   select x).ToList();
        return qry;
    }
    public class ProcessDataProperty
    {
        internal int? parallelId;

        public string ProcessName { get; set; }
        public int? ProcessID { get; set; }
        public int? ParentID { get; set; }
        public int SystemID { get; set; }
        public string ProcessObjectName { get; set; }
        public int ProcessObjID { get; set; }
        public string AttributeName { get; set; }
        public int AttributeMenuID { get; set; }
        public string AttributeValue { get; set; }
        public int UnitID { get; set; }
        public string UnitName { get; set; }
        public int OrderNO { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? Type { get; set; }
        public string NodeName { get; set; }
        public string ActivityName { get; set; }
        public int? Position { get; set; }
        public int AttributeValueSum { get; set; }
        public int? NeighbourActivityID { get; set; }
        public int? ProcessObjIDParl { get; set; }
        public int? ProcessObjIDParl1 { get; set; }
        public int? FunctionID { get; set; }

        public int? UserID { get; set; }
        public string ProcessObjIdsCollection { get; set; }
        public int? SourceType { get; set; }

    }
    public class AllReports
    {

        public string ReportsName { get; set; }
        public int ReportsID { get; set; }
    }

    public static List<ProcessDataProperty> GetProcessObjActvities(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == ProcessId && x.Type == 0
                   select new ProcessDataProperty
                   {
                       ProcessObjID = x.ProcessObjID,
                       ProcessObjectName = x.ProcessObjName
                   }).ToList();
        return qry.ToList();
    }

    public static List<ProcessDataProperty> GetProcessObjInventories(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == ProcessId && x.Type == 1
                   select new ProcessDataProperty
                   {
                       ProcessObjID = x.ProcessObjID,
                       ProcessObjectName = x.ProcessObjName
                   }).ToList();
        return qry.ToList();
    }

    public static List<ProcessData.AllReports> GetAllExistingReportsName(int ProcessId)
    {

        List<ProcessData.AllReports> objGetAllExistingReportsName = new List<ProcessData.AllReports>
        {
             new ProcessData.AllReports { ReportsID =1,ReportsName= "Attribute Report"}
             //new ProcessData.AllReports { ReportsID =2,ReportsName= "BOM Report"},
             //new ProcessData.AllReports { ReportsID =3,ReportsName= "TFG Report"},
             //new ProcessData.AllReports { ReportsID =4,ReportsName= "Machine Report"},
             //new ProcessData.AllReports { ReportsID =9,ReportsName= "Error Report"},
             //new ProcessData.AllReports { ReportsID =5,ReportsName= "Process Capability Scorecard"},
             //new ProcessData.AllReports { ReportsID =6,ReportsName= "Design Capability Scorecard"},
             //new ProcessData.AllReports { ReportsID =7,ReportsName= "Target Value Gap"},
             //new ProcessData.AllReports { ReportsID =8,ReportsName= "Inventory Report"},

        };
        return objGetAllExistingReportsName.ToList();
    }

    public static List<ProcessDataProperty> GetProcessObjErrors(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == ProcessId && x.Type == 0
                   select new ProcessDataProperty
                   {
                       ProcessObjID = x.ProcessObjID,
                       ProcessObjectName = x.ProcessObjName
                   }).ToList();
        return qry.ToList();
    }

    public static List<ProcessDataProperty> GetTargetObjActvities(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == ProcessId && x.Type == 0
                   select new ProcessDataProperty
                   {
                       ProcessObjID = x.TargetObjID,
                       ProcessObjectName = x.TargetObjName
                   }).ToList();
        return qry.ToList();
    }

    public static List<ProcessDataProperty> GetPPESAProcessObjActvities(int ProcessId, int FormType)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_ProcessObjects
                   join z in Objdata.tbl_PPESAnPDESAs on x.ProcessObjID equals z.ProcessObjectID
                   where x.ProcessID == ProcessId && x.Type == 0 && z.FormType == FormType && z.ActionCriticalParameter == true
                   select new ProcessDataProperty
                   {
                       ProcessObjID = x.ProcessObjID,
                       ProcessObjectName = x.ProcessObjName
                   }).Distinct().ToList();
        return qry.ToList();
    }

    public static List<ProcessDataProperty> GetPObjActivitySequence(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == ProcessId && x.Type == 0 && x.ParallelProcessObjID == null
                   select new ProcessDataProperty
                   {
                       ProcessObjID = x.ProcessObjID,
                       ProcessObjectName = x.ProcessObjName
                   }).ToList();
        return qry.ToList();
    }

    public static List<ProcessDataProperty> GetTObjActivitySequence(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == ProcessId && x.Type == 0 && x.ParallelTargetObjID == null
                   select new ProcessDataProperty
                   {
                       ProcessObjID = x.TargetObjID,
                       ProcessObjectName = x.TargetObjName
                   }).ToList();
        return qry.ToList();
    }


    public static List<ProcessDataProperty> GetProcessObjActvitiesToSelect(int ProcessId, int selectedPOid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == ProcessId && x.Type == 0 && x.ProcessObjID != selectedPOid
                   select new ProcessDataProperty
                   {
                       ProcessObjID = x.ProcessObjID,
                       ProcessObjectName = x.ProcessObjName
                   }).ToList();
        return qry.ToList();
    }

    public static List<ProcessDataProperty> GetTargetObjActvitiesToSelect(int ProcessId, int selectedPOid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == ProcessId && x.Type == 0 && x.ProcessObjID != selectedPOid
                   select new ProcessDataProperty
                   {
                       ProcessObjID = x.ProcessObjID,
                       ProcessObjectName = x.ProcessObjName
                   }).ToList();
        return qry.ToList();
    }

    public static List<ProcessDataProperty> GetProcessObjAttributes(int ProcessObjId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_AttributesMenus
                   where x.ProcessObjectID == ProcessObjId
                   select new ProcessDataProperty
                   {
                       AttributeName = x.AttributeName,
                       //AttributeMenuID = x.AttributeMenuID
                   }).Distinct().ToList();
        return qry.ToList();


    }

    public static List<ProcessDataProperty> GetInventoryObjAttributes(int ProcessObjId)
    {

        List<ProcessDataProperty> qry = new List<ProcessDataProperty>
        {
             new ProcessDataProperty { AttributeName = "CT"},
             new ProcessDataProperty { AttributeName = "$" },
             new ProcessDataProperty { AttributeName = "Time"}
        };
        return qry.ToList();
    }

    public static List<ProcessDataProperty> GetErrorObjAttributes(int ProcessObjId)
    {

        List<ProcessDataProperty> qry = new List<ProcessDataProperty>
        {
             new ProcessDataProperty { AttributeName = "CycleTime" },
             new ProcessDataProperty { AttributeName = "WorkContent" },
             new ProcessDataProperty { AttributeName = "CounterMeasure" },
             new ProcessDataProperty { AttributeName = "CounterMeasureStrength"}
        };
        return qry.ToList();
    }



    public static List<ProcessData.ProcessDataProperty> SelectedItemReport(bool inAsc, string SortBy, int processId, string attributeName, int processObjID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();

        //var data = (from x in ObjData.tbl_AttributesMenus
        //                select x).Distinct().ToList();

        //var distinct = Enumerable.Distinct(data);

        var qry = (from x in ObjData.tbl_AttributesMenus
                   join z in ObjData.tbl_Processes on x.ProcessID equals z.ProcessID
                   join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
                   join p in ObjData.tbl_ProcessObjects on x.ProcessObjectID equals p.ProcessObjID
                   where x.ProcessID == processId && x.IncludeOnMap == true && x.AttributeName == attributeName && x.ProcessObjectID == processObjID
                   group new { x, p, y, z } by new { p.ProcessObjName, p.ProcessObjID, x.AttributeName, x.AttributeValue, y.UnitName, z.ProcessName } into g
                   where g.Key.ProcessObjID == processObjID

                   select new ProcessDataProperty
                   {
                       AttributeName = g.Key.AttributeName,
                       //AttributeName = (from o in g select g.Key.AttributeName).Distinct().ToString(),
                       //AttributeName = g.Select(x=> x.).Distinct(),
                       // AttributeMenuID = g.Key.AttributeMenuID,
                       AttributeValue = g.Key.AttributeValue,
                       UnitName = g.Key.UnitName,
                       NodeName = g.Key.ProcessName,
                       ActivityName = g.Key.ProcessObjName

                   }).Distinct().ToList();
        if (inAsc)
        {
            return qry.OrderByDescending(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
    }


    //*************************Parallel Activity Methods**************************//
    public static bool SaveParallelProcessObject(tbl_ProcessObject tblProcessObject)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_ProcessObjects
                   where x.ProcessObjID == tblProcessObject.ProcessObjID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_ProcessObjects.InsertOnSubmit(tblProcessObject);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        qry.ProcessObjName = tblProcessObject.ProcessObjName;
        qry.ProcessID = tblProcessObject.ProcessID;
        qry.ModifiedDate = tblProcessObject.ModifiedDate;
        qry.OrderNo = tblProcessObject.OrderNo;
        qry.XTop = tblProcessObject.XTop;
        qry.YLeft = tblProcessObject.YLeft;
        qry.Title = tblProcessObject.Title;
        qry.ParallelProcessObjID = tblProcessObject.ParallelProcessObjID;

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

    public static bool SaveRelationshipData(tbl_ParallelRelationship ParallelData)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        tbl_ParallelRelationship qry = (from x in ObjData.tbl_ParallelRelationships
                                        where x.ID == ParallelData.ID
                                        select x).FirstOrDefault();

        if (qry == null)
        {
            qry = new tbl_ParallelRelationship();
            ObjData.tbl_ParallelRelationships.InsertOnSubmit(ParallelData);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        //else
        //{
        qry.ProcessObjID = ParallelData.ProcessObjID;
        qry.NeighbourActivityID = ParallelData.NeighbourActivityID;
        qry.Type = ParallelData.Type;
        // }

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

    public static List<ProcessDataProperty> GetAllSingleProcessObjId(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == processID && (x.Type == 0 || x.Type == 1) && x.ParallelProcessObjID == null
                   select new ProcessDataProperty
                   {
                       ProcessObjID = x.ProcessObjID,
                       Type = x.Type,
                       Position = x.Position

                   }).Distinct().ToList();

        return qry.OrderBy(p => p.Position).ToList();
    }

    public static List<ProcessDataProperty> GetAllParallelProcessObjId(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        List<ProcessDataProperty> Data = Objdata.tbl_ProcessObjects.Where(x => x.ProcessID == processID && (x.Type == 0 || x.Type == 1) && x.ParallelProcessObjID != null).Select(po => new ProcessDataProperty
        {
            ProcessObjID = po.ProcessObjID,
            Type = po.Type,
            parallelId = po.ParallelProcessObjID
        }).ToList();

        foreach (var row in Data)
        {

            row.Position = Objdata.tbl_ProcessObjects.Where(p => p.ProcessObjID == row.parallelId).Select(pos => pos.Position).FirstOrDefault();
        }
        return Data;


    }

    public static List<ProcessDataProperty> GetAllParallelTargetObjId(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == processID && (x.Type == 0 || x.Type == 1) && x.ParallelTargetObjID != null
                   select new ProcessDataProperty
                   {
                       ProcessObjID = x.TargetObjID,
                       Type = x.Type

                   }).Distinct().ToList();

        return qry.ToList();
    }

    public static int GetPositionByPoid(int PreviousPoid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessObjID == PreviousPoid
                   select x.Position).FirstOrDefault();
        if (qry != null)
            return Convert.ToInt32(qry);
        else
            return 0;
    }

    public static int GetTPositionByPoid(int PreviousPoid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetObjID == PreviousPoid
                   select x.Position).FirstOrDefault();
        if (qry != null)
            return Convert.ToInt32(qry);
        else
            return 0;
    }

    public static int GetProcessIdByPoid(int probjid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessObjID == probjid
                   select x.ProcessID).FirstOrDefault();

        return Convert.ToInt32(qry);
    }

    public static bool IsParallelProcessByPoid(int probjid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessObjID == probjid
                   select x.ParallelProcessObjID).FirstOrDefault();

        if (qry != null)
            return true;
        else
            return false;
    }


    public static bool IncreasNextRowsPosition(int processID, int InsertedPosition)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == processID && (x.Type == 0 || x.Type == 1) && x.ParallelProcessObjID == null && x.Position >= InsertedPosition
                   select x).ToList(); ;

        foreach (tbl_ProcessObject pos in qry)
        {
            pos.Position += 1;

            // Insert any changes to column values.
        }

        try
        {
            Objdata.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
            // Provide for exceptions.
        }
    }
    public static bool IncreasNextTargetRowsPosition(int processID, int InsertedPosition)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == processID && (x.Type == 0 || x.Type == 1) && x.ParallelTargetObjID == null && x.Position >= InsertedPosition
                   select x).ToList(); ;

        foreach (tbl_TargetObject pos in qry)
        {
            pos.Position += 1;

            // Insert any changes to column values.
        }

        try
        {
            Objdata.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
            // Provide for exceptions.
        }
    }
    public static bool DecreaseNextRowsPosition(int processID, int deletedPosition)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == processID && (x.Type == 0 || x.Type == 1) && x.ParallelProcessObjID == null && x.Position > deletedPosition
                   select x).ToList(); ;

        foreach (tbl_ProcessObject pos in qry)
        {
            pos.Position -= 1;

            // Insert any changes to column values.
        }

        try
        {
            Objdata.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
            // Provide for exceptions.
        }
    }

    public static int GetMaxPositionforPoId(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == ProcessId
                   select x.Position).Max();
        return Convert.ToInt32(qry);
    }

    public static List<ProcessDataProperty> GetPoidIdByPosition(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == ProcessId && x.Position != null
                   select new ProcessDataProperty
                   {
                       ProcessObjID = x.ProcessObjID,
                       Position = x.Position
                   }).ToList();

        return qry.OrderBy(p => p.Position).ToList();
    }

    //public static List<ProcessDataProperty> GetFromParallelProcessObjId(string fromPoid)
    //{
    //    VisualERPDataContext Objdata = new VisualERPDataContext();
    //    //qry will return GetTypeID details according our search query
    //    var qry = (from x in Objdata.tbl_ParallelRelationships
    //               where x.NeighbourActivityID == Convert.ToInt32(fromPoid) && x.Type == 0
    //               select new ProcessDataProperty
    //               {
    //                   NeighbourActivityID = x.NeighbourActivityID,
    //                   ProcessObjIDParl = x.ProcessObjID
    //               }).ToList();

    //    return qry.OrderBy(p => p.ProcessObjIDParl).ToList();
    //}

    //public static List<ProcessDataProperty> GetToParallelProcessObjId(string ToPoid)
    //{
    //    VisualERPDataContext Objdata = new VisualERPDataContext();
    //    //qry will return GetTypeID details according our search query
    //    var qry = (from x in Objdata.tbl_ParallelRelationships
    //               where x.ProcessObjID == Convert.ToInt32(ToPoid) && x.Type == 1
    //               select new ProcessDataProperty
    //               {
    //                   NeighbourActivityID = x.NeighbourActivityID,
    //                   ProcessObjIDParl = x.ProcessObjID
    //               }).ToList();

    //    return qry.OrderBy(p => p.ProcessObjID).ToList();
    //}

    public static List<ProcessDataProperty> GetFromParallelProcessObjId(string fromPoid, int ProcessId)
    {
        List<ProcessDataProperty> objProcessDataProperty = new List<ProcessDataProperty>();
        if (fromPoid == string.Empty)
        {
            fromPoid = "0";
        }
        int Nxtposition = 0;
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query

        var qry = (from x in Objdata.tbl_ParallelRelationships
                   where (x.NeighbourActivityID == Convert.ToInt32(fromPoid) && x.Type == 0)
                   select new ProcessDataProperty
                   {
                       NeighbourActivityID = x.NeighbourActivityID,
                       ProcessObjIDParl = x.ProcessObjID
                   }).ToList();

        var qry1 = (from y in Objdata.tbl_ParallelRelationships
                    where (y.ProcessObjID == Convert.ToInt32(fromPoid) && y.Type == 1)
                    select new ProcessDataProperty
                    {
                        NeighbourActivityID = y.ProcessObjID,
                        ProcessObjIDParl = y.NeighbourActivityID
                    }).ToList();



        var qry2 = (from z in Objdata.tbl_ProcessObjects
                    where (z.ProcessObjID == Convert.ToInt32(fromPoid))
                    select z.Position).FirstOrDefault();
        if (qry2 != null)
        {
            int preposition = qry2.Value;
            Nxtposition = preposition + 1;
        }

        var qry3 = (from p in Objdata.tbl_ProcessObjects
                    where (p.Position == Nxtposition && p.ProcessID == ProcessId)
                    select new ProcessDataProperty
                    {
                        NeighbourActivityID = p.ProcessObjID,
                        ProcessObjIDParl = p.ProcessObjID
                    }).ToList();



        if (qry.Count > 0)
        {
            var result = qry.Union(qry1).ToList();
            if (result.Count > 0)
            {
                objProcessDataProperty = result.Union(qry3).ToList();
                if (objProcessDataProperty.Count > 0)
                {
                    return objProcessDataProperty.OrderBy(p => p.ProcessObjIDParl).ToList();
                }
            }
        }





        return objProcessDataProperty;
    }

    public static List<ProcessDataProperty> GetToParallelProcessObjId(string ToPoid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ParallelRelationships
                   where x.ProcessObjID == Convert.ToInt32(ToPoid) && x.Type == 1
                   select new ProcessDataProperty
                   {
                       NeighbourActivityID = x.NeighbourActivityID,
                       ProcessObjIDParl = x.ProcessObjID
                   }).ToList();

        return qry.OrderBy(p => p.ProcessObjID).ToList();
    }

    public static int GetCycleTimeforPoid(int Poid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_AttributesMenus
                   where x.ProcessObjectID == Poid && x.AttributeName == "Total Cycle Time"
                   select x.AttributeValue).FirstOrDefault();
        return Convert.ToInt32(qry);
    }

    public static List<ProcessDataProperty> GetOtherStartPoints(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry1 = (from x in Objdata.tbl_ParallelRelationships
                    where x.Type == 0 && x.ProcessID == ProcessId
                    select new ProcessDataProperty
                    {
                        ProcessObjIDParl = x.ProcessObjID
                    }).ToList();

        var qry2 = (from x in Objdata.tbl_ParallelRelationships
                    where x.Type == 1 && x.ProcessID == ProcessId
                    select new ProcessDataProperty
                    {
                        ProcessObjIDParl = x.ProcessObjID
                    }).ToList();

        // var qry3 = qry1.Except(qry2);

        var qry3 = (from file in qry2
                    where !qry1.Any(a => a.ProcessObjIDParl == file.ProcessObjIDParl)
                    select new ProcessDataProperty
                    {
                        ProcessObjIDParl = file.ProcessObjIDParl
                    }).ToList();
        return qry3.OrderBy(p => p.ProcessObjIDParl).ToList();
    }

    public static bool DeleteExistingRecord(int ProcessId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_SummaryDatas
                   where x.ProcessID == ProcessId
                   select x).FirstOrDefault();
        if (qry != null)
        {
            ObjData.DeleteSummaryDataByProcessID(ProcessId);
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

    public static bool GetSummaryData(int ProcessId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_SummaryDatas
                   where x.ProcessID == ProcessId
                   select x).FirstOrDefault();
        if (qry != null)
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    public static bool SaveSummaryData(tbl_SummaryData summaryTblData)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_SummaryDatas
                   where x.ProcessID == summaryTblData.ProcessID
                   select x).FirstOrDefault();
        if (qry == null)
        {
            ObjData.tbl_SummaryDatas.InsertOnSubmit(summaryTblData);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);
        }
        else
        {
            qry.ProcessID = summaryTblData.ProcessID;
            qry.FunctionName = summaryTblData.FunctionName;
            qry.AttributeName = summaryTblData.AttributeName;
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

    public static List<ProcessDataProperty> GetSummaryDataSum1(bool inAsc, string SortBy, int processId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
                   where x.ProcessID == processId
                   group new { x, y } by new { x.AttributeName, x.AttributeValue, y.UnitName } into g
                   select new ProcessDataProperty
                   {
                       AttributeName = g.Key.AttributeName,
                       UnitName = g.Key.UnitName,
                       FunctionID = 0
                   }).Distinct().ToList();

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();

    }

    public static List<ProcessDataProperty> GetAttributeValue(bool inAsc, string SortBy, int processId, string AttributeName, string unitname)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
                   where x.ProcessID == processId && x.AttributeName == AttributeName && y.UnitName == unitname
                   && (x.SourceType == null || x.SourceType == 1)
                   select new ProcessDataProperty
                   {
                       AttributeName = x.AttributeName,
                       AttributeValueSum = Convert.ToInt32(x.AttributeValue),
                       UnitName = y.UnitName,
                       ProcessID = x.ProcessID
                   }).ToList();

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();

    }

    public static List<ProcessDataProperty> GetProcessAttributeValue(bool inAsc, string SortBy, int processId, string AttributeName, string unitname)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
                   where x.ProcessID == processId && x.AttributeName == AttributeName && y.UnitName == unitname
                   select new ProcessDataProperty
                   {
                       AttributeName = x.AttributeName,
                       AttributeValueSum = Convert.ToInt32(x.AttributeValue),
                       UnitName = y.UnitName,
                       ProcessID = x.ProcessID,
                       SourceType = x.SourceType ?? 1

                   }).ToList();

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();

    }

    public static List<ProcessDataProperty> GetTargetAttributeValue(bool inAsc, string SortBy, int processId, string AttributeName, string unitname)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
                   where x.ProcessID == processId && x.AttributeName == AttributeName && y.UnitName == unitname
                   select new ProcessDataProperty
                   {
                       AttributeName = x.AttributeName,
                       AttributeValueSum = Convert.ToInt32(x.AttributeValue),
                       UnitName = y.UnitName,
                       ProcessID = x.ProcessID
                   }).ToList();

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();

    }

    public static string GetUnitName(string attributeName)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_AttributesMenus
                   join p in Objdata.tbl_Units on x.UnitID equals p.UnitID
                   where x.AttributeName == attributeName
                   select p.UnitName).FirstOrDefault();

        return Convert.ToString(qry);
    }

    //public static List<ProcessDataProperty> GetSummaryTableRecord(bool inAsc, string SortBy, int processId)
    //{
    //    VisualERPDataContext ObjData = new VisualERPDataContext();
    //    var qry = (from x in ObjData.tbl_SummaryDatas
    //               where x.ProcessID == processId
    //               select new ProcessDataProperty
    //               {
    //                   AttributeName = x.AttributeName,
    //                   FunctionID = x.FunctionName
    //               }).Distinct().ToList();

    //    return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();

    //}

    public static List<ProcessDataProperty> GetSummaryTableRecord(bool inAsc, string SortBy, int processId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_SummaryDatas
                   where x.ProcessID == processId
                   select new ProcessDataProperty
                   {
                       AttributeName = x.AttributeName,
                       FunctionID = x.FunctionName
                   }).Distinct().ToList();

        var qry1 = (from x in ObjData.tbl_AttributesMenus
                    join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
                    where x.ProcessID == processId && (x.SourceType == null || x.SourceType == 1)
                    group new { x, y } by new { x.AttributeName, x.AttributeValue, y.UnitName } into g
                    select new ProcessDataProperty
                    {
                        AttributeName = g.Key.AttributeName,
                        //AttributeValueSum = (ObjData.tbl_AttributesMenus.Where(a => a.ProcessID == processId && a.AttributeName == g.Key.AttributeName).Sum(a => Convert.ToInt32(a.AttributeValue))),
                        UnitName = g.Key.UnitName,
                    }).Distinct().ToList();

        var query = (from c in qry1
                     join p in qry on c.AttributeName equals p.AttributeName into ps
                     from subpet in ps.DefaultIfEmpty()
                     select new ProcessDataProperty
                     {
                         AttributeName = c.AttributeName,
                         FunctionID = (subpet == null ? 0 : subpet.FunctionID),
                         UnitName = c.UnitName  //added 28 april
                     }).OrderBy(a => a.AttributeName).ToList();

        return query.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        // return query.ToList();

    }


    public static List<ProcessDataProperty> GetSummaryTableRecordForTargetValueGapReport(bool inAsc, string SortBy, int processId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_SummaryDatas
                   where x.ProcessID == processId
                   select new ProcessDataProperty
                   {
                       AttributeName = x.AttributeName,
                       FunctionID = x.FunctionName
                   }).Distinct().ToList();

        var qry1 = (from x in ObjData.tbl_AttributesMenus
                    join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
                    where x.ProcessID == processId
                    group new { x, y } by new { x.AttributeName, x.AttributeValue, y.UnitName } into g
                    select new ProcessDataProperty
                    {
                        AttributeName = g.Key.AttributeName,
                        //AttributeValueSum = (ObjData.tbl_AttributesMenus.Where(a => a.ProcessID == processId && a.AttributeName == g.Key.AttributeName).Sum(a => Convert.ToInt32(a.AttributeValue))),
                        UnitName = g.Key.UnitName

                    }).Distinct().ToList();

        var query = (from c in qry1
                     join p in qry on c.AttributeName equals p.AttributeName into ps
                     from subpet in ps.DefaultIfEmpty()
                     select new ProcessDataProperty
                     {
                         AttributeName = c.AttributeName,
                         FunctionID = (subpet == null ? 0 : subpet.FunctionID),
                         UnitName = c.UnitName
                     }).OrderBy(a => a.AttributeName).ToList();

        return query.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        // return query.ToList();

    }

    public static List<ProcessDataProperty> GetAllAttributeforSummary(bool inAsc, string SortBy, int processId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
                   where x.ProcessID == processId
                   group new { x, y } by new { x.AttributeName, x.AttributeValue, y.UnitName } into g
                   select new ProcessDataProperty
                   {
                       AttributeName = g.Key.AttributeName,
                       //AttributeValueSum = (ObjData.tbl_AttributesMenus.Where(a => a.ProcessID == processId && a.AttributeName == g.Key.AttributeName).Sum(a => Convert.ToInt32(a.AttributeValue))),
                       UnitName = g.Key.UnitName,
                   }).Distinct().ToList();

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();

    }

    /// <summary>
    /// DeleteSummaryDataByPoid() will join attibute of poid with summary table and delete similar data from summary data
    /// </summary>
    /// <param name="Poid">Process Object id</param>
    /// <returns>bool</returns>
    public static bool DeleteSummaryDataByPoid(int Poid, int processId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        // qry1 will get record all attributes records where processobj id is not current process obj id 
        var qry1 = (from x in ObjData.tbl_AttributesMenus
                    where x.ProcessID == processId && x.ProcessObjectID != Poid
                    select new ProcessDataProperty
                    {
                        AttributeName = x.AttributeName
                    }).ToList();
        if (qry1.Count > 0) // if there is record
        {
            // qry2 will have attributes list that have process obj id is id that to be deleted 
            var qry2 = (from y in ObjData.tbl_AttributesMenus
                        where y.ProcessObjectID == Poid
                        select new ProcessDataProperty
                        {
                            AttributeName = y.AttributeName
                        }).ToList();

            // var qry3 = qry1.Except(qry2);

            // qry3 will have unmatched attributes records for process obj id that to be deleted
            var qry3 = (from file in qry2
                        where !qry1.Any(a => a.AttributeName == file.AttributeName)
                        select new ProcessDataProperty
                        {
                            AttributeName = file.AttributeName
                        }).ToList();

            foreach (var name in qry3) // for each unmatched attrbutes find attribute in summary data table and delete it
            {
                var result = ObjData.tbl_SummaryDatas.Where(a => a.AttributeName == name.AttributeName).FirstOrDefault();
                if (result != null)
                {
                    ObjData.tbl_SummaryDatas.DeleteOnSubmit(result);
                }

            }
        }
        else
        {      // for any process if last activity is deleted that time qr1 will have null record and we will delete all records of that process from summary table     
            var qryF = (from x in ObjData.tbl_SummaryDatas
                        where x.ProcessID == processId
                        select new ProcessDataProperty
                        {
                            AttributeMenuID = x.ID
                        }).ToList();
            foreach (var name in qryF)
            {
                var resultF = ObjData.tbl_SummaryDatas.Where(a => a.ID == name.AttributeMenuID).FirstOrDefault();
                if (resultF != null)
                {
                    ObjData.tbl_SummaryDatas.DeleteOnSubmit(resultF);
                }
            }
        }

        try
        {
            ObjData.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            // Provide for exceptions.
            return false;
        }
    }

    public static List<ProcessDataProperty> GetEnterpriseSystemData(bool inAsc, string SortBy, int systemId, int processid)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_SystemProcessIOs
                   join y in ObjData.tbl_AttributesMenus on x.ProcessID equals y.ProcessID
                   join z in ObjData.tbl_Units on y.UnitID equals z.UnitID
                   where x.SystemID == systemId && y.ProcessID == processid
                   select new ProcessDataProperty
                   {
                       AttributeName = y.AttributeName,
                       AttributeValue = y.AttributeValue,
                       UnitName = z.UnitName,
                       ProcessID = y.ProcessID
                   }).ToList();

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();

    }

    ///// <summary>
    ///// DeleteSystemIODataByPOID() will delete selected SystemIOData Data from database mulitple tables
    ///// </summary>
    ///// <param name="Process Object ID">Process Object  ID is current >Process Object  ID that is selected</param>
    ///// <returns></returns>
    //public static bool DeleteReportDataByPoID(int Poid)
    //{
    //    bool result = false;
    //    VisualERPDataContext ObjData = new VisualERPDataContext();
    //    var reportData = (from k in ObjData.tbl_Reports
    //                        where k.ProcessID == Poid 
    //                        select k).ToList();
    //    if (reportData.Count > 0)
    //    {
    //        ObjData.DeleteReportDataByPID(Poid); //DeleteAttributeDataByProcessObj ID is stored procedure in database that delete AttributeData of By ProcessObjID
    //        result = true;
    //    }
    //    else
    //    {
    //        result = false;
    //    }

    //    return result;
    //}

    public static bool HasReport(int probjid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_Reports
                   where x.ProcessObjID.Contains(Convert.ToString(probjid))
                   select x.ProcessObjID).ToList();
        foreach (var row in qry)
        {
            if (!string.IsNullOrEmpty(row) && row.Contains(','))
            {
                string[] data = row.Split(',');
                if (data != null)
                {
                    foreach (var obj in data)
                    {
                        if (!string.IsNullOrEmpty(obj))
                        {
                            if (Convert.ToInt32(obj) == probjid)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    public static string GetReportName(int probjid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_Reports
                   where x.ProcessObjID.Contains(Convert.ToString(probjid))
                   select x.ReportName).FirstOrDefault();

        return Convert.ToString(qry);
    }

    public static List<ProcessDataProperty> GetSavedProcessObjIdsFromReport(int ProcessId, int ReporttypeID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_Reports
                   where x.ProcessID == ProcessId && x.ReportTypeID == ReporttypeID
                   select new ProcessDataProperty
                   {
                       ProcessObjIdsCollection = x.ProcessObjID
                   }).ToList();
        return qry.ToList();
    }

}