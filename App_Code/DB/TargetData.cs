using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 

/// <summary>
/// Summary description for ProcessData
/// </summary>
public class TargetData
{
    public TargetData()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetProcessNameById(int processId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_Targets 
                   where x.TargetID == processId
                   select x.TargetName).FirstOrDefault();

        return Convert.ToString(qry);
    }

    public static string GetProcessNameByPoid(int poid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_Targets
                   join y in Objdata.tbl_TargetObjects
                   on x.TargetID equals y.TargetID
                   where y.TargetObjID == poid
                   select x.TargetName).FirstOrDefault();

        return Convert.ToString(qry);
    }

    public static string GetFunctionNameById(int processId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_Targets
                   where x.TargetID == processId && x.TypeID == 5
                   select x.FunctionName).FirstOrDefault();

        return Convert.ToString(qry);
    }

    public static string GetProcessObjNameById(int poid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetObjID == poid
                   select x.TargetObjName).FirstOrDefault();

        return Convert.ToString(qry);
    }

    public static bool SaveProcessData(tbl_Target ProcessTblData)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_Targets
                   where x.TargetID == ProcessTblData.TargetID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_Targets.InsertOnSubmit(ProcessTblData);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {
            qry.TargetName = ProcessTblData.TargetName;
            qry.FunctionName = ProcessTblData.FunctionName;
            qry.TargetID  = ProcessTblData.TargetID;
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
            var ProcessNameCount = (from c in ObjData.tbl_Targets
                                    where c.TargetName.ToLower() == ProcessName.ToLower()
                                     && c.TargetID != ProcessId
                                    select c.TargetID).Count();
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
            //countcat variable will get ProcessName from table tbl_Targets on behalf of ProcessName
            var countCat = (from c in ObjData.tbl_Targets
                            where c.TargetName.ToLower() == ProcessName.ToLower()
                            select c.TargetID).Count();
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

    public static bool SaveDumyProcessObject(tbl_TargetObject tblProcessDummyObject)
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

    public static bool SaveProcessObject(tbl_TargetObject tblProcessObject)
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
        var qry = (from x in Objdata.tbl_TargetObjects
                   select x.OrderNo).Max();
        return Convert.ToInt32(qry);
    }
    public static int GetMaxTargetObjID(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == processID
                   select x.TargetObjID).Max();
        return Convert.ToInt32(qry);
    }
    public static List<TargetDataProperty> GetAllTargetObjID(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == processID && (x.Type == 0 || x.Type == 1) && x.ParallelTargetObjID == null
                   select new TargetDataProperty
                   {
                       TargetObjID = x.TargetObjID,
                       Type = x.Type

                   }).Distinct().ToList();

        return qry.ToList();
    }

    public static List<TargetDataProperty> GetAllProcessID()
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_Targets
                   select new TargetDataProperty
                   {
                       TargetID = x.TargetID,
                       TargetName = x.TargetName,
                       ParentID = x.ParentID,

                   }).Distinct().ToList();

        return qry.ToList();
    }
    public static List<TargetDataProperty> GetAllProcessID_test(int UserID, int CompanyId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_Targets
                       // where x.TargetID == Id && (x.ParentID == null || x.ParentID == 0)
                       // where x.TargetID == Id && x.CompanyID == 10
                   where x.CompanyID == CompanyId && x.UserRegisterID == UserID
                   select new TargetDataProperty
                   {
                       TargetID = x.TargetID,
                       TargetName = x.TargetName,
                       ParentID = x.ParentID

                   }).Distinct().ToList();

        return qry.ToList();
    }

    public static tbl_TargetObject ProcessObjectByID(Int32 TargetObjID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.tbl_TargetObjects
                where bc.TargetObjID == TargetObjID
                select bc).FirstOrDefault();
    }

    public static tbl_InvantoryTriangle ProcessObjectInventoryByID(Int32 TargetObjID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.tbl_InvantoryTriangles
                where bc.ProcessObjID == TargetObjID
                select bc).FirstOrDefault();
    }
    public static List<TargetDataProperty> GetActivityOrderData(bool inAsc, string SortBy, int TargetObjID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join y in ObjData.tbl_Units
                   on x.UnitID equals y.UnitID
                   where x.ProcessObjectID == TargetObjID && x.IncludeOnMap == true
                   select new TargetDataProperty
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

    public static List<TargetDataProperty> GetSummaryData(bool inAsc, string SortBy, int processId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join y in ObjData.tbl_Units
                   on x.UnitID equals y.UnitID
                   where x.ProcessID == processId && x.IncludeOnMap == true
                   select new TargetDataProperty
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

    public static List<TargetDataProperty> GetSummaryDataSum(bool inAsc, string SortBy, int processId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
                   where x.ProcessID == processId
                   group new { x, y } by new { x.AttributeName, x.AttributeValue, y.UnitName } into g
                   select new TargetDataProperty
                   {
                       AttributeName = g.Key.AttributeName,
                       AttributeValueSum = (ObjData.tbl_AttributesMenus.Where(a => a.ProcessID == processId && a.AttributeName == g.Key.AttributeName).Sum(a => Convert.ToInt32(a.AttributeValue))),
                       UnitName = g.Key.UnitName,
                   }).Distinct().ToList();

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();

    }

    public static List<TargetData.TargetDataProperty> GetReportData(bool inAsc, string SortBy, int processId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join z in ObjData.tbl_Targets on x.ProcessID equals z.TargetID
                   join y in ObjData.tbl_Units
                   on x.UnitID equals y.UnitID
                   where x.ProcessID == processId && x.IncludeOnMap == true
                   select new TargetDataProperty
                   {
                       AttributeName = x.AttributeName,
                       AttributeMenuID = x.AttributeMenuID,
                       AttributeValue = x.AttributeValue,
                       UnitName = y.UnitName,
                       NodeName = z.TargetName

                   }).Distinct().ToList();
        if (inAsc)
        {
            return qry.OrderByDescending(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
    }
    public static List<TargetDataProperty> GetAllProcessByProcessId(int ProcessId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_Targets
                   where x.TargetID == ProcessId || x.ParentID == null
                   select new TargetDataProperty
                   {
                       TargetID = x.TargetID,
                       TargetName = x.TargetName,
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
        var ProcessObjData = (from k in ObjData.tbl_TargetObjects
                              where k.TargetObjID == Poid
                              select k).ToList();
        if (ProcessObjData.Count > 0)
        {
            ObjData.DeleteTargetObjDataByID(Poid); //DeleteMachine is stored procedure in database that delete TFGData of TFG Id
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
    public static bool DeleteAttributedataByPoID(int Poid, int sourceType=1)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var AttributeObjData = (from k in ObjData.tbl_AttributesMenus
                                where k.ProcessObjectID == Poid && (k.SourceType==null || k.SourceType==sourceType)
                                select k).ToList();
        if (AttributeObjData.Count > 0)
        {
            ObjData.DeleteInventeryProcessObjByID(Poid); //DeleteAttributeDataByProcessObj ID is stored procedure in database that delete AttributeData of By TargetObjID
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
            ObjData.DeleteSystemIODataByPOID(Poid); //DeleteAttributeDataByProcessObj ID is stored procedure in database that delete AttributeData of By TargetObjID
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    public static List<tbl_Target> GetProcessCollection(int systemId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_Targets
                   join y in Objdata.tbl_SystemProcessIOs
                   on x.TargetID equals y.ProcessID
                   where y.SystemID == systemId
                   select x).ToList();
        return qry;
    }
    public static List<tbl_TargetObject> GetProcessObjActvityCollection(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == ProcessId && x.Type == 0
                   select x).ToList();
        return qry;
    }
    public class TargetDataProperty
    {
        public string TargetName { get; set; }
        public int? TargetID { get; set; }
        public int? ParentID { get; set; }
        public int SystemID { get; set; }
        public string TargetObjectName { get; set; }
        public int TargetObjID { get; set; }
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
        public int? TargetObjIDParl { get; set; }
        public int? TargetObjIDParl1 { get; set; }
        public int? FunctionID { get; set; }

        public int? UserID { get; set; }
        public string TargetObjIdsCollection { get; set; }

    }

    public static List<TargetDataProperty> GetProcessObjActvities(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == ProcessId && x.Type == 0
                   select new TargetDataProperty
                   {
                       TargetObjID = x.TargetObjID,
                       TargetObjectName = x.TargetObjName
                   }).ToList();
        return qry.ToList();
    }

    public static List<TargetDataProperty> GetPPESAProcessObjActvities(int ProcessId, int FormType)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_TargetObjects
                   join z in Objdata.tbl_PPESAnPDESAs on x.TargetObjID equals z.ProcessObjectID
                   where x.TargetID == ProcessId && x.Type == 0 && z.FormType == FormType && z.ActionCriticalParameter == true
                   select new TargetDataProperty
                   {
                       TargetObjID = x.TargetObjID,
                       TargetObjectName = x.TargetObjName
                   }).Distinct().ToList();
        return qry.ToList();
    }

    public static List<TargetDataProperty> GetPObjActivitySequence(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == ProcessId && x.Type == 0 && x.ParallelTargetObjID == null
                   select new TargetDataProperty
                   {
                       TargetObjID = x.TargetObjID,
                       TargetObjectName = x.TargetObjName
                   }).ToList();
        return qry.ToList();
    }

    public static List<TargetDataProperty> GetProcessObjActvitiesToSelect(int ProcessId, int selectedPOid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == ProcessId && x.Type == 0 && x.TargetObjID != selectedPOid
                   select new TargetDataProperty
                   {
                       TargetObjID = x.TargetObjID,
                       TargetObjectName = x.TargetObjName
                   }).ToList();
        return qry.ToList();
    }

    public static List<TargetDataProperty> GetProcessObjAttributes(int TargetObjID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_AttributesMenus
                   where x.ProcessObjectID == TargetObjID
                   select new TargetDataProperty
                   {
                       AttributeName = x.AttributeName,
                       //AttributeMenuID = x.AttributeMenuID
                   }).Distinct().ToList();
        return qry.ToList();


    }

    //public static List<ProcessData.ProcessDataProperty> SelectedItemReport(bool inAsc, string SortBy, int processId, string attributeName,int TargetObjID)
    //{
    //    VisualERPDataContext ObjData = new VisualERPDataContext();

    //    var data = (from x in ObjData.tbl_AttributesMenus
    //                    select x).Distinct().ToList();

    //    var distinct = Enumerable.Distinct(data);

    //    var qry = (from x in distinct
    //               join z in ObjData.tbl_Targets on x.TargetID equals z.ProcessID
    //               join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
    //               join p in ObjData.tbl_TargetObjects on x.ProcessObjectID equals p.TargetObjID                   
    //               where x.TargetID == processId && x.IncludeOnMap == true && x.AttributeName == attributeName && x.ProcessObjectID == TargetObjID

    //               select new ProcessDataProperty
    //               {
    //                   AttributeName = x.AttributeName,
    //                   AttributeMenuID = x.AttributeMenuID,
    //                   AttributeValue = x.AttributeValue,
    //                   UnitName = y.UnitName,
    //                   NodeName = z.ProcessName,
    //                   ActivityName = p.ProcessObjName

    //               }).Distinct().ToList();
    //    if (inAsc)
    //    {
    //        return qry.OrderByDescending(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
    //    }

    //    return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
    //}

    public static List<TargetData.TargetDataProperty> SelectedItemReport(bool inAsc, string SortBy, int processId, string attributeName, int TargetObjID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();

        //var data = (from x in ObjData.tbl_AttributesMenus
        //                select x).Distinct().ToList();

        //var distinct = Enumerable.Distinct(data);

        var qry = (from x in ObjData.tbl_AttributesMenus
                   join z in ObjData.tbl_Targets on x.ProcessID equals z.TargetID
                   join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
                   join p in ObjData.tbl_TargetObjects on x.ProcessObjectID equals p.TargetObjID
                   where x.ProcessID == processId && x.IncludeOnMap == true && x.AttributeName == attributeName && x.ProcessObjectID == TargetObjID
                   group new { x, p, y, z } by new { p.TargetObjName, p.TargetObjID, x.AttributeName, x.AttributeValue, y.UnitName, z.TargetName } into g
                   where g.Key.TargetObjID == TargetObjID

                   select new TargetDataProperty
                   {
                       AttributeName = g.Key.AttributeName,
                       //AttributeName = (from o in g select g.Key.AttributeName).Distinct().ToString(),
                       //AttributeName = g.Select(x=> x.).Distinct(),
                       // AttributeMenuID = g.Key.AttributeMenuID,
                       AttributeValue = g.Key.AttributeValue,
                       UnitName = g.Key.UnitName,
                       NodeName = g.Key.TargetName,
                       ActivityName = g.Key.TargetObjName

                   }).Distinct().ToList();
        if (inAsc)
        {
            return qry.OrderByDescending(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
    }


    //*************************Parallel Activity Methods**************************//
    public static bool SaveParallelProcessObject(tbl_TargetObject tblProcessObject)
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
        qry.TargetObjName = tblProcessObject.TargetObjName;
        qry.TargetID = tblProcessObject.TargetID;
        qry.ModifiedDate = tblProcessObject.ModifiedDate;
        qry.OrderNo = tblProcessObject.OrderNo;
        qry.XTop = tblProcessObject.XTop;
        qry.YLeft = tblProcessObject.YLeft;
        qry.Title = tblProcessObject.Title;
        qry.ParallelTargetObjID = tblProcessObject.ParallelTargetObjID;

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

    public static List<ProcessData.ProcessDataProperty> GetAllSingleTargetObjID(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == processID && (x.Type == 0 || x.Type == 1) && x.ParallelTargetObjID == null
                   select new ProcessData.ProcessDataProperty
                   {
                       ProcessObjID = x.TargetObjID,
                       Type = x.Type,
                       Position = x.Position

                   }).Distinct().ToList();

        return qry.OrderBy(p => p.Position).ToList();
    }

    public static List<TargetDataProperty> GetAllParallelTargetObjID(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == processID && (x.Type == 0 || x.Type == 1) && x.ParallelTargetObjID != null
                   select new TargetDataProperty
                   {
                       TargetObjID = x.TargetObjID,
                       Type = x.Type

                   }).Distinct().ToList();

        return qry.ToList();
    }

    public static int GetPositionByPoid(int PreviousPoid)
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
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetObjID == probjid
                   select x.TargetID).FirstOrDefault();

        return Convert.ToInt32(qry);
    }

    public static bool IsParallelProcessByPoid(int probjid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetObjID == probjid
                   select x.ParallelTargetObjID).FirstOrDefault();

        if (qry != null)
            return true;
        else
            return false;
    }


    public static bool IncreasNextRowsPosition(int processID, int InsertedPosition)
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
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == processID && (x.Type == 0 || x.Type == 1) && x.ParallelTargetObjID == null && x.Position > deletedPosition
                   select x).ToList(); ;

        foreach (tbl_TargetObject pos in qry)
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
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == ProcessId
                   select x.Position).Max();
        return Convert.ToInt32(qry);
    }

    public static List<TargetDataProperty> GetPoidIdByPosition(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetID == ProcessId && x.Position != null
                   select new TargetDataProperty
                   {
                       TargetObjID = x.TargetObjID,
                       Position = x.Position
                   }).ToList();

        return qry.OrderBy(p => p.Position).ToList();
    }

    //public static List<ProcessDataProperty> GetFromParallelTargetObjID(string fromPoid)
    //{
    //    VisualERPDataContext Objdata = new VisualERPDataContext();
    //    //qry will return GetTypeID details according our search query
    //    var qry = (from x in Objdata.tbl_ParallelRelationships
    //               where x.NeighbourActivityID == Convert.ToInt32(fromPoid) && x.Type == 0
    //               select new ProcessDataProperty
    //               {
    //                   NeighbourActivityID = x.NeighbourActivityID,
    //                   TargetObjIDParl = x.TargetObjID
    //               }).ToList();

    //    return qry.OrderBy(p => p.TargetObjIDParl).ToList();
    //}

    //public static List<ProcessDataProperty> GetToParallelTargetObjID(string ToPoid)
    //{
    //    VisualERPDataContext Objdata = new VisualERPDataContext();
    //    //qry will return GetTypeID details according our search query
    //    var qry = (from x in Objdata.tbl_ParallelRelationships
    //               where x.TargetObjID == Convert.ToInt32(ToPoid) && x.Type == 1
    //               select new ProcessDataProperty
    //               {
    //                   NeighbourActivityID = x.NeighbourActivityID,
    //                   TargetObjIDParl = x.TargetObjID
    //               }).ToList();

    //    return qry.OrderBy(p => p.TargetObjID).ToList();
    //}

    public static List<TargetDataProperty> GetFromParallelTargetObjID(string fromPoid, int ProcessId)
    {
        if (fromPoid == string.Empty)
        {
            fromPoid = "0";
        }
        int Nxtposition = 0;
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ParallelRelationships
                   where (x.NeighbourActivityID == Convert.ToInt32(fromPoid) && x.Type == 0)
                   select new TargetDataProperty
                   {
                       NeighbourActivityID = x.NeighbourActivityID,
                       TargetObjIDParl = x.ProcessObjID
                   }).ToList();

        var qry1 = (from y in Objdata.tbl_ParallelRelationships
                    where (y.ProcessObjID == Convert.ToInt32(fromPoid) && y.Type == 1)
                    select new TargetDataProperty
                    {
                        NeighbourActivityID = y.ProcessObjID,
                        TargetObjIDParl = y.NeighbourActivityID
                    }).ToList();



        var qry2 = (from z in Objdata.tbl_TargetObjects
                    where (z.TargetObjID == Convert.ToInt32(fromPoid))
                    select z.Position).FirstOrDefault();
        if (qry2 != null)
        {
            int preposition = qry2.Value;
            Nxtposition = preposition + 1;
        }

        var qry3 = (from p in Objdata.tbl_TargetObjects
                    where (p.Position == Nxtposition && p.TargetID == ProcessId)
                    select new TargetDataProperty
                    {
                        NeighbourActivityID = p.TargetObjID,
                        TargetObjIDParl = p.TargetObjID
                    }).ToList();




        var result = qry.Union(qry1).ToList();
        var result1 = result.Union(qry3).ToList();



        return result1.OrderBy(p => p.TargetObjIDParl).ToList();
    }

    public static List<TargetDataProperty> GetToParallelTargetObjID(string ToPoid)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ParallelRelationships
                   where x.ProcessObjID == Convert.ToInt32(ToPoid) && x.Type == 1
                   select new TargetDataProperty
                   {
                       NeighbourActivityID = x.NeighbourActivityID,
                       TargetObjIDParl = x.ProcessObjID
                   }).ToList();

        return qry.OrderBy(p => p.TargetObjID).ToList();
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

    public static List<TargetDataProperty> GetOtherStartPoints(int ProcessId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry1 = (from x in Objdata.tbl_ParallelRelationships
                    where x.Type == 0 && x.ProcessID == ProcessId
                    select new TargetDataProperty
                    {
                        TargetObjIDParl = x.ProcessObjID
                    }).ToList();

        var qry2 = (from x in Objdata.tbl_ParallelRelationships
                    where x.Type == 1 && x.ProcessID == ProcessId
                    select new TargetDataProperty
                    {
                        TargetObjIDParl = x.ProcessObjID
                    }).ToList();

        // var qry3 = qry1.Except(qry2);

        var qry3 = (from file in qry2
                    where !qry1.Any(a => a.TargetObjIDParl == file.TargetObjIDParl)
                    select new TargetDataProperty
                    {
                        TargetObjIDParl = file.TargetObjIDParl
                    }).ToList();
        return qry3.OrderBy(p => p.TargetObjIDParl).ToList();
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

    public static List<TargetDataProperty> GetSummaryDataSum1(bool inAsc, string SortBy, int processId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
                   where x.ProcessID == processId
                   group new { x, y } by new { x.AttributeName, x.AttributeValue, y.UnitName } into g
                   select new TargetDataProperty
                   {
                       AttributeName = g.Key.AttributeName,
                       UnitName = g.Key.UnitName,
                       FunctionID = 0
                   }).Distinct().ToList();

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();

    }

    public static List<TargetDataProperty> GetAttributeValue(bool inAsc, string SortBy, int processId, string AttributeName, string unitname)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
                   where x.ProcessID == processId && x.AttributeName == AttributeName && y.UnitName == unitname
                   && x.SourceType==2
                   select new TargetDataProperty
                   {
                       AttributeName = x.AttributeName,
                       AttributeValueSum = Convert.ToInt32(x.AttributeValue),
                       UnitName = y.UnitName,
                       TargetID = x.ProcessID
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
    //               where x.TargetID == processId
    //               select new ProcessDataProperty
    //               {
    //                   AttributeName = x.AttributeName,
    //                   FunctionID = x.FunctionName
    //               }).Distinct().ToList();

    //    return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();

    //}

    public static List<TargetDataProperty> GetSummaryTableRecord(bool inAsc, string SortBy, int processId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_SummaryDatas
                   where x.ProcessID == processId
                   select new TargetDataProperty
                   {
                       AttributeName = x.AttributeName,
                       FunctionID = x.FunctionName
                   }).Distinct().ToList();

        var qry1 = (from x in ObjData.tbl_AttributesMenus
                    join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
                    where x.ProcessID == processId && x.SourceType==2
                    group new { x, y } by new { x.AttributeName, x.AttributeValue, y.UnitName } into g
                    select new TargetDataProperty
                    {
                        AttributeName = g.Key.AttributeName,
                        //AttributeValueSum = (ObjData.tbl_AttributesMenus.Where(a => a.ProcessID == processId && a.AttributeName == g.Key.AttributeName).Sum(a => Convert.ToInt32(a.AttributeValue))),
                        UnitName = g.Key.UnitName,
                    }).Distinct().ToList();

        var query = (from c in qry1
                     join p in qry on c.AttributeName equals p.AttributeName into ps
                     from subpet in ps.DefaultIfEmpty()
                     select new TargetDataProperty
                     {
                         AttributeName = c.AttributeName,
                         FunctionID = (subpet == null ? 0 : subpet.FunctionID),
                         UnitName = c.UnitName //added 28 april
                     }).OrderBy(a => a.AttributeName).ToList();

        // return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        return query.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();

    }

    public static List<TargetDataProperty> GetAllAttributeforSummary(bool inAsc, string SortBy, int processId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join y in ObjData.tbl_Units on x.UnitID equals y.UnitID
                   where x.ProcessID == processId
                   group new { x, y } by new { x.AttributeName, x.AttributeValue, y.UnitName } into g
                   select new TargetDataProperty
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
                    select new TargetDataProperty
                    {
                        AttributeName = x.AttributeName
                    }).ToList();
        if (qry1.Count > 0) // if there is record
        {
            // qry2 will have attributes list that have process obj id is id that to be deleted 
            var qry2 = (from y in ObjData.tbl_AttributesMenus
                        where y.ProcessObjectID == Poid
                        select new TargetDataProperty
                        {
                            AttributeName = y.AttributeName
                        }).ToList();

            // var qry3 = qry1.Except(qry2);

            // qry3 will have unmatched attributes records for process obj id that to be deleted
            var qry3 = (from file in qry2
                        where !qry1.Any(a => a.AttributeName == file.AttributeName)
                        select new TargetDataProperty
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
                        select new TargetDataProperty
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

    public static List<TargetDataProperty> GetEnterpriseSystemData(bool inAsc, string SortBy, int systemId, int processid)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_SystemProcessIOs
                   join y in ObjData.tbl_AttributesMenus on x.ProcessID equals y.ProcessID
                   join z in ObjData.tbl_Units on y.UnitID equals z.UnitID
                   where x.SystemID == systemId && y.ProcessID == processid
                   select new TargetDataProperty
                   {
                       AttributeName = y.AttributeName,
                       AttributeValue = y.AttributeValue,
                       UnitName = z.UnitName,
                       TargetID = y.ProcessID
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
    //        ObjData.DeleteReportDataByPID(Poid); //DeleteAttributeDataByProcessObj ID is stored procedure in database that delete AttributeData of By TargetObjID
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
                   select x).FirstOrDefault();

        if (qry != null)
            return true;
        else
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

    public static List<TargetDataProperty> GetSavedTargetObjIDsFromReport(int ProcessId, int ReporttypeID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_Reports
                   where x.ProcessID == ProcessId && x.ReportTypeID == ReporttypeID
                   select new TargetDataProperty
                   {
                        TargetObjIdsCollection = x.ProcessObjID
                   }).ToList();
        return qry.ToList();
    }

}