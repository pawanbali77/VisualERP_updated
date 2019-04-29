using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SystemData
/// </summary>
public class TypeData
{
    public TypeData()
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

    public static List<tbl_TypeCollection> GetTypeCollection()
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_TypeCollections
                   select x).ToList();
        return qry;
    }

    public static List<tbl_Process> GetSytemCollection()
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_Processes
                   where x.TypeID==4
                   select x).ToList();
        return qry;
    }
    public static int GetTypeID(int processId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
       var qry = (from x in Objdata.tbl_Processes
                   where x.ProcessID == processId
                   select x.TypeID).FirstOrDefault();

        return Convert.ToInt32(qry);
    }
    public static bool SaveSystemProcessData(tbl_SystemProcessIO tblSysProcess)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_SystemProcessIOs
                   where x.SysID == tblSysProcess.SysID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_SystemProcessIOs.InsertOnSubmit(tblSysProcess);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {
            qry.SysID = tblSysProcess.SysID;
            qry.SystemID = tblSysProcess.SystemID;
            qry.ProcessID = tblSysProcess.ProcessID;
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
    public static bool GetDuplicateCheck(int SystemId, int ProcessId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        if (ProcessId > 0)
        {
            //countcountry will get AttributeName from table tbl_AttributesMenus on behalf of AttributeName
            var ProcessIdCount = (from c in ObjData.tbl_SystemProcessIOs
                                      where c.ProcessID == ProcessId
                                     && c.SystemID == SystemId
                                      select c).Count();
            if (ProcessIdCount > 0)
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
            //countcat variable will get AttributeName from table tbl_AttributesMenus on behalf of AttributeName
            var countCat = (from c in ObjData.tbl_SystemProcessIOs
                             where c.ProcessID == ProcessId
                                     && c.SystemID == SystemId
                            select c).Count();
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

    public static List<SystemTypeDetail> GetAllProcessId(int SystemId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_SystemProcessIOs
                   where x.SystemID == SystemId
                   select new SystemTypeDetail
                   {
                       ProcessID = x.ProcessID,
                   }).Distinct().ToList();

        return qry.ToList();
    }
    public class SystemTypeDetail
    {
        public int? SystemID { get; set; }
        public string SystemName { get; set; }
        public int? ProcessID {get;set;}
        public string ProcessName{get;set;}
        public int? TypeID { get; set; }
        public string TypeName { get; set; } 
    }
}