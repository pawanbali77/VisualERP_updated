using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PPESAnPDESA
/// </summary>
public class PPESAnPDESA
{
    #region
    VisualERPDataContext ObjData = new VisualERPDataContext();
    #endregion
    public PPESAnPDESA()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static List<ListPPESAnPDESAData> GetPPESAnPDESAData(bool inAsc, string SortBy, int formtype, int processID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_PPESAnPDESAs
                       //join y in ObjData.tbl_ProcessObjects
                       //on x.ProcessObjectID equals y.ProcessObjID
                       //where x.ProcessObjectID == poid && x.FormType == formtype
                   where x.FormType == formtype && x.ProcessID == processID
                   select new ListPPESAnPDESAData
                   {
                       FormID = x.FormID,
                       FormType = x.FormType,
                       ProcessID = x.ProcessID,
                       ProcessObjectID = x.ProcessObjectID,
                       //ProcessObjectName = y.ProcessObjName,
                       ProductFeatureAdded = x.ProductFeatureAdded,
                       FunctionofProductFeature = x.FunctionofProductFeature,
                       ErrorEvent = x.ErrorEvent,
                       ErrorEventTransferFunction = x.ErrorEventTransferFunction,
                       Actions = x.Actions,
                       ActionCriticalParameter = (bool)x.ActionCriticalParameter,
                       Conditions = x.Conditions,
                       ConditonCriticalParameter = (bool)x.ConditonCriticalParameter,
                       InitialSeverity = x.InitialSeverity,
                       InitialFrequency = x.InitialFrequency,
                       InitialDetection = x.InitialDetection,
                       IntialRPN = x.IntialRPN,
                       Countermeasure = x.Countermeasure,
                       CountermeasureEffectiveness = x.CountermeasureEffectiveness,
                       FinalSeverity = x.FinalSeverity,
                       FinalFrequency = x.FinalFrequency,
                       FinalDetection = x.FinalDetection,
                       FinalRPN = x.FinalRPN,
                       Sequence = x.Sequence
                   }).Distinct().ToList();
        if (inAsc)
        {
            return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
    }

    public static List<ListPPESAnPDESAData> GetPPESAnPDESADataByPobjID(bool inAsc, string SortBy, int formtype, int ProcessObjectID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_PPESAnPDESAs
                   join y in ObjData.tbl_ProcessObjects
                   on x.ProcessObjectID equals y.ProcessObjID
                   //where x.ProcessObjectID == poid && x.FormType == formtype && x.ActionCriticalParameter == true

                   where x.FormType == formtype && x.ProcessObjectID == ProcessObjectID 
                   select new ListPPESAnPDESAData
                   {
                       FormID = x.FormID,
                       FormType = x.FormType,
                       ProcessID = x.ProcessID,
                       ProcessObjectID = x.ProcessObjectID,
                       ProcessObjectName = y.ProcessObjName,
                       ProductFeatureAdded = x.ProductFeatureAdded,
                       FunctionofProductFeature = x.FunctionofProductFeature,
                       ErrorEvent = x.ErrorEvent,
                       ErrorEventTransferFunction = x.ErrorEventTransferFunction,
                       Actions = x.Actions,
                       ActionCriticalParameter = (bool)x.ActionCriticalParameter,
                       Conditions = x.Conditions,
                       ConditonCriticalParameter = (bool)x.ConditonCriticalParameter,
                       InitialSeverity = x.InitialSeverity,
                       InitialFrequency = x.InitialFrequency,
                       InitialDetection = x.InitialDetection,
                       IntialRPN = x.IntialRPN,
                       Countermeasure = x.Countermeasure,
                       CountermeasureEffectiveness = x.CountermeasureEffectiveness,
                       FinalSeverity = x.FinalSeverity,
                       FinalFrequency = x.FinalFrequency,
                       FinalDetection = x.FinalDetection,
                       FinalRPN = x.FinalRPN,
                       Cpk = x.Cpk,
                       Cp = x.Cp,
                       Ppk = x.Ppk,
                       LongtermSigma = x.LongtermSigma,
                       ShorttermSigma = x.ShorttermSigma,
                       ZScore = x.ZScore
                   }).Distinct().ToList();
        if (inAsc)
        {
            return qry.OrderByDescending(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
    }

    public static List<ListPPESAnPDESAData> GetPPESAnPDESAReportData(bool inAsc, string SortBy, int formtype, int processID, int repotid)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_PPESAnPDESAs
                       //join y in ObjData.tbl_Reports
                       //on x.ProcessID equals y.ProcessID                 && x.ActionCriticalParameter == true
                   where x.FormType == formtype && x.ProcessID == processID  
                   select new ListPPESAnPDESAData
                   {
                       FormID = x.FormID,
                       FormType = x.FormType,
                       ProcessID = x.ProcessID,
                       ProcessObjectID = x.ProcessObjectID,
                       ProcessObjectName = (from c in ObjData.tbl_ProcessObjects
                                            where c.ProcessObjID == x.ProcessObjectID
                                            select c.ProcessObjName).FirstOrDefault(),
                       ProductFeatureAdded = x.ProductFeatureAdded,
                       FunctionofProductFeature = x.FunctionofProductFeature,
                       ErrorEvent = x.ErrorEvent,
                       ErrorEventTransferFunction = x.ErrorEventTransferFunction,
                       Actions = x.Actions,
                       ActionCriticalParameter = (bool)x.ActionCriticalParameter,
                       Conditions = x.Conditions,
                       ConditonCriticalParameter = (bool)x.ConditonCriticalParameter,
                       InitialSeverity = x.InitialSeverity,
                       InitialFrequency = x.InitialFrequency,
                       InitialDetection = x.InitialDetection,
                       IntialRPN = x.IntialRPN,
                       Countermeasure = x.Countermeasure,
                       CountermeasureEffectiveness = x.CountermeasureEffectiveness,
                       FinalSeverity = x.FinalSeverity,
                       FinalFrequency = x.FinalFrequency,
                       FinalDetection = x.FinalDetection,
                       FinalRPN = x.FinalRPN,
                       Sequence = x.Sequence,
                       Cpk = x.Cpk,
                       Cp = x.Cp,
                       Ppk = x.Ppk,
                       LongtermSigma = x.LongtermSigma,
                       ShorttermSigma = x.ShorttermSigma,
                       ZScore = x.ZScore,
                       ReportName = (from j in ObjData.tbl_Reports
                                     where j.ReportID == repotid
                                     select j.ReportName).FirstOrDefault()

                   }).Distinct().ToList();
        if (inAsc)
        {
            return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
    }

    public static bool SavePPESAnPDESAData(tbl_PPESAnPDESA PPESAnPDESAData)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_PPESAnPDESAs
                   where x.FormID == PPESAnPDESAData.FormID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_PPESAnPDESAs.InsertOnSubmit(PPESAnPDESAData);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {
            qry.ProcessID = PPESAnPDESAData.ProcessID;
            qry.ProcessObjectID = PPESAnPDESAData.ProcessObjectID;
            qry.FormType = PPESAnPDESAData.FormType;
            qry.ProductFeatureAdded = PPESAnPDESAData.ProductFeatureAdded;
            qry.FunctionofProductFeature = PPESAnPDESAData.FunctionofProductFeature;
            qry.ErrorEvent = PPESAnPDESAData.ErrorEvent;
            qry.ErrorEventTransferFunction = PPESAnPDESAData.ErrorEventTransferFunction;
            qry.Actions = PPESAnPDESAData.Actions;
            qry.ActionCriticalParameter = PPESAnPDESAData.ActionCriticalParameter;
            qry.Conditions = PPESAnPDESAData.Conditions;
            qry.ConditonCriticalParameter = PPESAnPDESAData.ConditonCriticalParameter;
            qry.InitialSeverity = PPESAnPDESAData.InitialSeverity;
            qry.InitialFrequency = PPESAnPDESAData.InitialFrequency;
            qry.InitialDetection = PPESAnPDESAData.InitialDetection;
            qry.IntialRPN = PPESAnPDESAData.IntialRPN;
            qry.Countermeasure = PPESAnPDESAData.Countermeasure;
            qry.CountermeasureEffectiveness = PPESAnPDESAData.CountermeasureEffectiveness;
            qry.FinalSeverity = PPESAnPDESAData.FinalSeverity;
            qry.FinalFrequency = PPESAnPDESAData.FinalFrequency;
            qry.FinalDetection = PPESAnPDESAData.FinalDetection;
            qry.FinalRPN = PPESAnPDESAData.FinalRPN;
            //qry.Cpk = PPESAnPDESAData.Cpk;
            //qry.Cp = PPESAnPDESAData.Cp;
            //qry.Ppk = PPESAnPDESAData.Ppk;
            //qry.LongtermSigma = PPESAnPDESAData.LongtermSigma;
            //qry.ShorttermSigma = PPESAnPDESAData.ShorttermSigma;
            //qry.ZScore = PPESAnPDESAData.ZScore;
            qry.Sequence = PPESAnPDESAData.Sequence;

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

    public static bool UpdatePPESAnPDESADataOnReport(tbl_PPESAnPDESA PPESAnPDESAData)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_PPESAnPDESAs
                   where x.FormID == PPESAnPDESAData.FormID
                   select x).FirstOrDefault();

        if (qry == null)
        {
            return false;
        }
        else
        {
            qry.Cpk = PPESAnPDESAData.Cpk;
            qry.Cp = PPESAnPDESAData.Cp;
            qry.Ppk = PPESAnPDESAData.Ppk;
            qry.LongtermSigma = PPESAnPDESAData.LongtermSigma;
            qry.ShorttermSigma = PPESAnPDESAData.ShorttermSigma;
            qry.ZScore = PPESAnPDESAData.ZScore;

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

    /// <summary>
    /// GetDuplicateCheck will check duplicate check of TFGVendor name if it exist or not 
    /// </summary>
    /// <param name="Attributename">TFGVendor hold the input name user entered in textbox</param>
    /// <param name="Attributeid">TFGID hold link id from base page</param>
    /// <returns>return tru and false </returns>
    public static bool GetDuplicateCheck(string ProductFeatureAdded, int poid, int FormID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        if (FormID > 0)
        {
            //TFGNameCount will get TFGID from table tbl_TFGs on behalf of TFGVendor
            var PPESAameCount = (from c in ObjData.tbl_PPESAnPDESAs
                                 where c.ProductFeatureAdded.ToLower() == ProductFeatureAdded.ToLower()
                                  && c.ProcessObjectID == poid
                                     && c.FormID != FormID
                                 select c.FormID).Count();
            if (PPESAameCount > 0)
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
            //countcat variable will get TFGVendor from table tbl_TFGs on behalf of TFGVendor
            var countCat = (from c in ObjData.tbl_PPESAnPDESAs
                            where c.ProductFeatureAdded.ToLower() == ProductFeatureAdded.ToLower()
                              && c.ProcessObjectID == poid
                            select c.FormID).Count();
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

    /// <summary>
    /// TFGID will get link id we are editing for..
    /// </summary>
    /// <param name="TFGID">TFGID current TFG id</param>
    /// <returns></returns>
    public static tbl_PPESAnPDESA PPESAnPDESAByID(Int32 FormID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.tbl_PPESAnPDESAs
                where bc.FormID == FormID
                select bc).FirstOrDefault();
    }

    /// <summary>
    /// DeleteTFG() will delete selected TFG from database mulitple tables
    /// </summary>
    /// <param name="TFGID">TFGID is current TFGID that is selected</param>
    /// <returns></returns>
    public static bool DeletePPESAnPDESA(int FormID)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var TFGVar = (from k in ObjData.tbl_PPESAnPDESAs
                      where k.FormID == FormID
                      select k).ToList();
        if (TFGVar.Count > 0)
        {
            ObjData.DeletePPESADataByID(FormID); //DeleteTFG is stored procedure in database that delete TFGData of TFG Id
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    public class ListPPESAnPDESAData
    {
        public int FormID { get; set; }
        public int? FormType { get; set; }
        public int? ProcessID { get; set; }
        public int? ProcessObjectID { get; set; }
        public string ProcessName { get; set; }
        public string ProcessObjectName { get; set; }
        public string ProductFeatureAdded { get; set; }
        public string FunctionofProductFeature { get; set; }
        public string ErrorEvent { get; set; }
        public string ErrorEventTransferFunction { get; set; }
        public string Actions { get; set; }
        public bool ActionCriticalParameter { get; set; }
        public string Conditions { get; set; }
        public bool ConditonCriticalParameter { get; set; }
        public int? InitialSeverity { get; set; }
        public int? InitialFrequency { get; set; }
        public int? InitialDetection { get; set; }
        public int? IntialRPN { get; set; }
        public string Countermeasure { get; set; }
        public int? CountermeasureEffectiveness { get; set; }
        public int? FinalSeverity { get; set; }
        public int? FinalFrequency { get; set; }
        public int? FinalDetection { get; set; }
        public int? FinalRPN { get; set; }
        public string Cpk { get; set; }
        public string Cp { get; set; }
        public string Ppk { get; set; }
        public string LongtermSigma { get; set; }
        public string ShorttermSigma { get; set; }
        public string ZScore { get; set; }
        public int? Sequence { get; set; }
        public string ReportName { get; set; }

    }

    public static string ChangeDate(DateTime? dt)
    {
        string date = string.Empty;
        if (dt != null)
        {
            date = dt.Value.ToString("ddMMM,yyyy");
        }
        return date;
    }

    public static bool GetIfReportAlreadyExist(int ProcessID, int ReportType)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();

        var checkESAReoprt = (from c in ObjData.tbl_Reports
                              where c.ProcessID == ProcessID && c.ReportTypeID == ReportType
                              select c).ToList();
        if (checkESAReoprt.Count > 0)
        {
            return false; // if reportname exist it will return result false
        }
        else
        {
            return true; // report name doesn't exist
        }
    }

    public static bool IncreaseNextRowsSequence(int processID, int increaseSequence, int FormType)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_PPESAnPDESAs
                   where x.ProcessID == processID && x.FormType == FormType && x.Sequence >= increaseSequence
                   select x).ToList(); ;

        foreach (tbl_PPESAnPDESA seq in qry)
        {
            seq.Sequence += 1;

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

    public static bool DecreaseNextRowsSequence(int processID, int deletedsequence, int FormType)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_PPESAnPDESAs
                   where x.ProcessID == processID && x.FormType == FormType && x.Sequence > deletedsequence
                   select x).ToList(); ;

        foreach (tbl_PPESAnPDESA seq in qry)
        {
            seq.Sequence -= 1;

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

    public static int GetMaxSequenceNo(int processID, int FormType)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_PPESAnPDESAs
                   where x.ProcessID == processID && x.FormType == FormType
                   select x.Sequence).Max();
        return Convert.ToInt32(qry);
    }

}