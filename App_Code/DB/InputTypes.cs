using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InputTypes
/// </summary>
public class InputTypes
{
	public InputTypes()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static List<tbl_InputType> GetTypes()
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_InputTypes
                   select x).ToList();
        return qry;
    }

    public class TypesDetails
    {
        public string Type { get; set; }
        public int? ID { get; set; }
    }
}