using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InventeryData
/// </summary>
public class InventeryData
{
	public InventeryData()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool SaveProcessObjectInventery(tbl_InvantoryTriangle tblProcessInventoryObject)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_InvantoryTriangles
                   where x.InvantoryTriangleID == tblProcessInventoryObject.InvantoryTriangleID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_InvantoryTriangles.InsertOnSubmit(tblProcessInventoryObject);
            // return 
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {

            qry.CT = tblProcessInventoryObject.CT;
            qry.Doller = tblProcessInventoryObject.Doller;
            qry.Time = tblProcessInventoryObject.Time;
            qry.ProcessObjID = tblProcessInventoryObject.ProcessObjID;
            qry.ModifiedDate = DateTime.Now;
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
    public static bool DeleteInventeryProcessObjByID(int Poid)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var ProcessObjInventery = (from k in ObjData.tbl_InvantoryTriangles
                              where k.ProcessObjID == Poid
                              select k).ToList();
        if (ProcessObjInventery.Count > 0)
        {
            ObjData.DeleteInventeryProcessObjByID(Poid); //DeleteMachine is stored procedure in database that delete TFGData of TFG Id
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

}