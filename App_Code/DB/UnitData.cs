using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UnitData
/// </summary>
public class UnitData
{
   
	public UnitData()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static List<tbl_Unit> GenUnits()
    {
         VisualERPDataContext ObjData = new VisualERPDataContext();
         var qry = (from x in ObjData.tbl_Units
                    select x).ToList();
         return qry;
    }

    public class UnitDetail
    {
        public string UnitName { get; set; }
        public int? UnitID { get; set;}


    }
}