using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TFGData
/// </summary>
public class TFGData
{
    #region
    VisualERPDataContext ObjData = new VisualERPDataContext();
    #endregion

    public TFGData()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static List<ListTFGData> GetTFGData(bool inAsc, string SortBy, int poid)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_TFGs
                   join y in ObjData.tbl_TFGVendors
                   on x.TFGVendorID equals y.TFGVendorID
                   where x.ProcessObjectID == poid
                   select new ListTFGData
                   {

                       TFGID = x.TFGID,
                       ProcessObjectID = Convert.ToInt32(x.ProcessObjectID),
                       Tool_Fixture_GageName = x.Tool_Fixture_GageName,
                       TFGDescription = x.TFGDescription,
                       TFGVendorPart = x.TFGVendorPart,
                       TFGVendor = x.TFGVendor,
                       TFGVendorID = Convert.ToInt32(x.TFGVendorID),
                       TFGCost = (float)x.TFGCost,
                       TFGQty = (float)x.TFGQty,
                       CalibrationCycle = x.CalibrationCycle,
                       TimeToCailbrate = x.TimeToCailbrate,
                       CostToCalibrate = (float)x.CostToCalibrate,
                       CalibrationVendor = x.CalibrationVendor,
                       CalibrationVendorID = Convert.ToInt32(x.CalibrationVendorID),
                       CalibrationVendorInfo = x.CalibrationVendorInfo,
                      // CalibratedBy = x.CalibratedBy,
                       //CalibrationDate1 = Convert.ToDateTime(x.CalibrationDate1)
                       CalibrationDate = ChangeDate(x.CalibrationDate1),
                       Cost = Convert.ToDecimal(x.TFGCost)

                   }).Distinct().ToList();
        if (inAsc)
        {
            return qry.OrderByDescending(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
    }  

    public static bool SaveTFGData(tbl_TFG TFGData)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_TFGs
                   where x.TFGID == TFGData.TFGID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_TFGs.InsertOnSubmit(TFGData);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {
            qry.ProcessObjectID = TFGData.ProcessObjectID;
            qry.Tool_Fixture_GageName = TFGData.Tool_Fixture_GageName;
            qry.TFGDescription = TFGData.TFGDescription;
            qry.TFGVendorPart = TFGData.TFGVendorPart;
            qry.TFGVendor = TFGData.TFGVendor;
            qry.TFGVendorID = TFGData.TFGVendorID;
            qry.TFGCost = TFGData.TFGCost;
            qry.TFGQty = TFGData.TFGQty;
            qry.CalibrationCycle = TFGData.CalibrationCycle;
            qry.TimeToCailbrate = TFGData.TimeToCailbrate;
            qry.CostToCalibrate = TFGData.CostToCalibrate;
            qry.CalibrationVendor = TFGData.CalibrationVendor;
            qry.CalibrationVendorID = TFGData.CalibrationVendorID;
            qry.CalibrationVendorInfo = TFGData.CalibrationVendorInfo;
            qry.CalibratedBy = TFGData.CalibratedBy;
            qry.CalibrationDate1 = TFGData.CalibrationDate1;
            qry.CalibrationDate2 = TFGData.CalibrationDate2;
            qry.CalibrationDate3 = TFGData.CalibrationDate3;
            qry.CalibrationDate4 = TFGData.CalibrationDate4;
            qry.CalibrationDate5 = TFGData.CalibrationDate5;
           
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
    public static bool GetDuplicateCheck(string ToolName, int poid, int TFGID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        if (TFGID > 0)
        {
            //TFGNameCount will get TFGID from table tbl_TFGs on behalf of TFGVendor
            var TFGNameCount = (from c in ObjData.tbl_TFGs
                                where c.Tool_Fixture_GageName.ToLower() == ToolName.ToLower()
                                  && c.ProcessObjectID == poid
                                     && c.TFGID != TFGID
                                  select c.TFGID).Count();
            if (TFGNameCount > 0)
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
            var countCat = (from c in ObjData.tbl_TFGs
                            where c.Tool_Fixture_GageName.ToLower() == ToolName.ToLower()
                              && c.ProcessObjectID == poid 
                            select c.TFGID).Count();
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
    public static tbl_TFG TFGByID(Int32 TFGID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.tbl_TFGs
                where bc.TFGID == TFGID
                select bc).FirstOrDefault();
    }

    /// <summary>
    /// DeleteTFG() will delete selected TFG from database mulitple tables
    /// </summary>
    /// <param name="TFGID">TFGID is current TFGID that is selected</param>
    /// <returns></returns>
    public static bool DeleteTFG(int TFGID)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var TFGVar = (from k in ObjData.tbl_TFGs
                        where k.TFGID == TFGID
                        select k).ToList();
        if (TFGVar.Count > 0)
        {
            ObjData.DeleteTFGDataByID(TFGID); //DeleteTFG is stored procedure in database that delete TFGData of TFG Id
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    public class ListTFGData
    {
        public int TFGID { get; set; }
        public int ProcessObjectID { get; set; }
        public string Tool_Fixture_GageName { get; set; }
        public string TFGDescription { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TFGVendorPart { get; set; }
        public string TFGVendor { get; set; }
        public int TFGVendorID { get; set; }
        public float TFGCost { get; set; }
        public float TFGQty { get; set; }
        public string CalibrationCycle { get; set; }
        public string TimeToCailbrate { get; set; }
        public float CostToCalibrate { get; set; }
        public string CalibrationVendor { get; set; }
        public int CalibrationVendorID { get; set; }
        public string CalibrationVendorInfo { get; set; }
        public string CalibratedBy { get; set; }
        public DateTime CalibrationDate1 { get; set; }
        public DateTime CalibrationDate2 { get; set; }
        public DateTime CalibrationDate3 { get; set; }
        public DateTime CalibrationDate4 { get; set; }
        public DateTime CalibrationDate5 { get; set; }

        public string CalibrationDate { get; set; }
        public decimal Cost { get; set; }

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
}