using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProcessHeaderColumns
/// </summary>
public class ProcessHeaderColumns
{
    public ProcessHeaderColumns()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bool SaveProcessBlockHeaders(tbl_ProcessBlockHeader processBlockHeader)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_ProcessBlockHeaders
                   where x.Id == processBlockHeader.Id
                   select x).FirstOrDefault();
        if (qry == null)
        {
            ObjData.tbl_ProcessBlockHeaders.InsertOnSubmit(processBlockHeader);
        }
        else
        {
            qry.ProcessId = processBlockHeader.ProcessId;
            qry.Headerlblname = processBlockHeader.Headerlblname;
            qry.CreatedDate = DateTime.UtcNow;
            qry.UpdatedDate = DateTime.UtcNow;
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

    public static tbl_ProcessBlockHeader GetProcessHeader(int processId,int Sequaceorder)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_ProcessBlockHeaders
                   where x.ProcessId == processId && x.SequanceOrder == Sequaceorder
                   select x).FirstOrDefault();
        return qry;
    }

    public static IEnumerable<tbl_ProcessBlockHeader> GetProcessHeader(int processId)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        var qry = (from x in Objdata.tbl_ProcessBlockHeaders
                   where x.ProcessId == processId 
                   select x).OrderBy(x=>x.SequanceOrder);
        return qry;
    }
    //public static IEnumerable<tbl_ProcessBlockHeader> GetSHAssemblyColDataByProcessIdformtypeId(int formtypeId, int processId)
    //{
    //    VisualERPDataContext Objdata = new VisualERPDataContext();
    //    var qry = (from x in Objdata.tbl_SHAssemblyCols
    //               where x.ProcessId == processId 
    //               select x).ToList(); ;
    //    return qry;
    //}
}