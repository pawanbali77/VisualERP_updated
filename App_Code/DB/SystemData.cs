using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SystemData
/// </summary>
public class SystemData
{
	public SystemData()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    public static List<tbl_System> GetSystemList()
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();

        var qry = (from x in Objdata.tbl_Systems
                   select x).ToList();
        return qry;
    }

    public class SystemDetail
    {
        public int? SystemID { get; set; }
        public string SystemName { get; set; }
    }
}