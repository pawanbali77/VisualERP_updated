using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Activity
/// </summary>
public class Activity
{
    public Activity()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetActivityNameByProcessObjId(int processObjId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return activity name by processobjId
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessObjID == processObjId
                   select x.ProcessObjName).FirstOrDefault();

        return Convert.ToString(qry);
    }
    public static string GetActivityNameByTargetObjId(int processObjId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return activity name by processobjId
        var qry = (from x in Objdata.tbl_TargetObjects
                   where x.TargetObjID == processObjId
                   select x.TargetObjName).FirstOrDefault();

        return Convert.ToString(qry);
    }

    /// <summary>
    /// GetDuplicateCheck will check duplicate check of Attribute name if it exist or not 
    /// </summary>
    /// <param name="Attributename">Attributename hold the Attribute name user entered in textbox</param>
    /// <param name="Attributeid">Attributeid hold Attribute id from base page</param>
    /// <returns>return tru and false </returns>
    public static bool GetDuplicateCheck(string ProcessObjName, int ProcessObjID, int sourceType=1)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        //countcountry will get AttributeName from table tbl_AttributesMenus on behalf of AttributeName

        if (sourceType == 2)
        {
            var ActivityRecord = (from c in ObjData.tbl_TargetObjects
                                  where c.TargetObjName.ToLower() == ProcessObjName.ToLower()
                                 && c.TargetObjID == ProcessObjID
                                  select c).ToList();
            return ActivityRecord.Count > 0 ? false : true;
             
        }
        else
        {
            var ActivityRecord = (from c in ObjData.tbl_ProcessObjects
                                  where c.ProcessObjName.ToLower() == ProcessObjName.ToLower()
                                 && c.ProcessObjID == ProcessObjID
                                  select c).ToList();
            if (ActivityRecord.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public static bool UpdateActivityName(tbl_ProcessObject data)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_ProcessObjects
                   where x.ProcessObjID == data.ProcessObjID
                   select x).FirstOrDefault();

        if (qry == null)
        {
            //ObjData.tbl_AttributesMenus.InsertOnSubmit(AttributeData);
        }
        else
        {
            qry.ProcessObjName = data.ProcessObjName;
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
    public static bool UpdateTargetActivityName(tbl_TargetObject data)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_TargetObjects 
                   where x.TargetObjID == data.TargetObjID
                   select x).FirstOrDefault();

        if (qry == null)
        {
            //ObjData.tbl_AttributesMenus.InsertOnSubmit(AttributeData);
        }
        else
        {
            qry.TargetObjName = data.TargetObjName;
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

    public static bool SaveReportData(tbl_Report ReportData)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_Reports
                   where x.ReportID == ReportData.ReportID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_Reports.InsertOnSubmit(ReportData);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {
            qry.ReportID = ReportData.ReportID;
            qry.ProcessID = ReportData.ProcessID;
            qry.ProcessObjID = ReportData.ProcessObjID;
            qry.AttributeName = ReportData.AttributeName;
            qry.ReportName = ReportData.ReportName;
            qry.ReportTypeID = ReportData.ReportTypeID;
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

    public static tbl_Report Get1ReportByProcessID(bool inAsc,string SortBy,int poID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        tbl_Report report = new tbl_Report();
        report = (from x in ObjData.tbl_Reports
                  where x.ProcessID == poID
                  select x).FirstOrDefault();
        return report;

    }

    public static List<ListReportData> GetReportByProcessID(bool inAsc, string SortBy, int poID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_Reports
                   where x.ProcessID == poID
                   select new ListReportData
                   {
                       ReportID = x.ReportID,
                       ProcessObjID = x.ProcessObjID,
                       AttributeName = x.AttributeName,
                       ReportName = x.ReportName,
                       ReportType = x.ReportTypeID,
                       ReportTypeName = Enum.GetName(typeof(ReportTypeID), x.ReportTypeID)
                   }).Distinct().ToList();
        if (inAsc)
        {
            return qry.OrderByDescending(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();

    }
    public static List<ListReportData> GetReportByProcessID(bool inAsc, string SortBy, int poID, int reportID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_Reports
                   where x.ProcessID == poID 
                   select new ListReportData
                   {
                       ReportID = x.ReportID,
                       ProcessObjID = x.ProcessObjID,
                       AttributeName = x.AttributeName,
                       ReportName = x.ReportName,
                       ReportType = x.ReportTypeID,
                       ReportTypeName = Enum.GetName(typeof(ReportTypeID), x.ReportTypeID)
                   }).Distinct().ToList();
        if (inAsc)
        {
            return qry.OrderByDescending(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();

    }

    /// <summary>
    /// DeleteReport() will delete selected report from database tables
    /// </summary>
    /// <param name="ReportID">ReportID is current ReportID that is selected</param>
    /// <returns></returns>
    public static bool DeleteReport(int ReportID)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var ReportVar = (from k in ObjData.tbl_Reports
                           where k.ReportID == ReportID
                           select k).ToList();
        if (ReportVar.Count > 0)
        {
            // ObjData.
            ObjData.DeleteReportByID(ReportID); //DeleteAttribute is stored procedure in database that delete Attribute of Attribute Id
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    public static tbl_Report GetDataByReportID(int reportID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        tbl_Report report = new tbl_Report();
        report = (from x in ObjData.tbl_Reports
                  where x.ReportID == reportID
                  select x).FirstOrDefault();
        return report;

    }

    /// <summary>
    /// GetDuplicateCheckReportName will check duplicate Report name if it exist or not 
    /// </summary>
    /// <param name="ReportName">ReportName hold the report name user entered in textbox</param>
    /// <param name="ProcessID">ProcessID hold processid of report</param>
    /// <returns>return tru and false </returns>
    public static bool GetDuplicateCheckReportName(string ReportName, int ProcessID,int ReportType,int editReportID=0)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();  
        if (editReportID > 0)
        {
            var ReportData = (from c in ObjData.tbl_Reports
                              where c.ReportName.ToLower() == ReportName.ToLower()
                             && c.ProcessID == ProcessID && c.ReportTypeID == ReportType && c.ReportID != editReportID
                              select c).ToList();
            if (ReportData.Count > 0)
            {
                return false; // if reportname exist it will return result false
            }
            else
            {
                return true; // report name doesn't exist
            }
        }
        else
        {
            var ReportData = (from c in ObjData.tbl_Reports
                              where c.ReportName.ToLower() == ReportName.ToLower()
                             && c.ProcessID == ProcessID && c.ReportTypeID == ReportType
                              select c).ToList();
            if (ReportData.Count > 0)
            {
                return false; // if reportname exist it will return result false
            }
            else
            {
                return true; // report name doesn't exist
            }
        }
    }

     
    public class ListReportData
    {
        public int ReportID { get; set; }
        public int ProcessID { get; set; }
        public string ProcessObjID { get; set; }
        public string AttributeName { get; set; }
        public string ReportName { get; set; }
        public int? ReportType { get; set; }
        public string ReportTypeName { get; set; }
        
    }

    public static int GetReportType(int ReportID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return activity name by processobjId
        var qry = (from x in Objdata.tbl_Reports
                   where x.ReportID == ReportID
                   select x.ReportTypeID).FirstOrDefault();

        return Convert.ToInt32(qry);
    }
}