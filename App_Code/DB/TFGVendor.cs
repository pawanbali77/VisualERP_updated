using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TFGVendor
/// </summary>
public class TFGVendor
{
	public TFGVendor()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static List<tbl_TFGVendor> GetVendors()
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_TFGVendors
                   select x).ToList();
        return qry;
    }

    public static List<tbl_Calibration> GetCalibrationVendors()
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_Calibrations
                   select x).ToList();
        return qry;
    }

    public class VendorsDetails
    {
        public string TFGVendor { get; set; }
        public int? TFGVendorID { get; set; }
    }

    public class CalibrationVendorsDetails
    {
        public string CalibrationVendorInfo { get; set; }
        public string CalibrationVendor { get; set; }
        public int? CalibrationVendorID { get; set; }
    }
    
}