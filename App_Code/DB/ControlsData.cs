using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ControlsData
/// </summary>
public class ControlsData
{
    public ControlsData()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static int SaveControlData(tbl_ProcessObject processObjData)
    {
        int NewID;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_ProcessObjects
                   where x.ProcessObjID == processObjData.ProcessObjID
                   select x).FirstOrDefault();

        if (qry == null)
        {
            ObjData.tbl_ProcessObjects.InsertOnSubmit(processObjData);
            //new ObjData().tbl_ProcessObjects.InsertOnSubmit(ListAttributeData);
        }
        else
        {
            qry.XTop = processObjData.XTop;
            qry.YLeft = processObjData.YLeft;
            qry.Width = processObjData.Width;
            qry.Height = processObjData.Height;
            qry.Title = processObjData.Title;
            qry.Type = processObjData.Type;
        }
        try
        {
            ObjData.SubmitChanges();
            NewID = processObjData.ProcessObjID;

            return NewID;
        }
        catch
        {
            return 0;

        }
    }

    public static List<ProcessObjectData> GetAllProcessControlData(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   orderby x.Type, x.ParallelProcessObjID ascending
                   where x.ProcessID == processID //&& (x.Type==0 || x.Type==1)
                   select new ProcessObjectData
                   {
                       ProcessObjID = x.ProcessObjID,
                       Type = x.Type,
                       XTop = x.XTop,
                       YLeft = x.YLeft,
                       Width = x.Width,
                       Height = x.Height,
                       Title = x.Title,
                       ParallelProcessObjID = x.ParallelProcessObjID,
                       ProcessObjName = x.ProcessObjName,
                       TypeParallel = x.ParallelProcessObjID == null ? 0 : 1
                   }).OrderBy(a => a.TypeParallel).ToList();

        return qry.ToList();
    }


    public static List<ProcessObjectData> GetProcessInventoryData(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == processID && (x.Type == 0 || x.Type == 1)
                   select new ProcessObjectData
                   {
                       ProcessObjID = x.ProcessObjID,
                       Type = x.Type,
                       XTop = x.XTop,
                       YLeft = x.YLeft,
                       Width = x.Width,
                       Height = x.Height,
                       Title = x.Title,
                   }).ToList();

        return qry.ToList();
    }

    public class ProcessObjectData
    {
        public int ProcessObjID { get; set; }
        public string ProcessObjName { get; set; }
        public int? Type { get; set; }
        public int? XTop { get; set; }
        public int? YLeft { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? ParallelProcessObjID { get; set; }  /////////////////
        public int? TypeParallel { get; set; }

    }

    //*******************Enterprise1***********************//


    public static List<ProcessObjectData> GetAllProcessDataforEnterPrise(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   orderby x.Type, x.ParallelProcessObjID ascending
                   where x.ProcessID == processID && x.ParallelProcessObjID == null && (x.Type == 0 || x.Type == 1)
                   select new ProcessObjectData
                   {
                       ProcessObjID = x.ProcessObjID,
                       Type = x.Type,
                       XTop = x.XTop,
                       YLeft = x.YLeft,
                       Width = x.Width,
                       Height = x.Height,
                       Title = x.Title,
                       ParallelProcessObjID = x.ParallelProcessObjID,
                       ProcessObjName = x.ProcessObjName
                   }).ToList();

        return qry.ToList();
    }

    public static List<ProcessObjectData> GetAllParallelDataforEnterPrise(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   orderby x.Type, x.ParallelProcessObjID ascending
                   where x.ProcessID == processID && x.ParallelProcessObjID != null && x.Type == 0
                   select new ProcessObjectData
                   {
                       ProcessObjID = x.ProcessObjID,
                       Type = x.Type,
                       XTop = x.XTop,
                       YLeft = x.YLeft,
                       Width = x.Width,
                       Height = x.Height,
                       Title = x.Title,
                       ParallelProcessObjID = x.ParallelProcessObjID,
                       ProcessObjName = x.ProcessObjName
                   }).ToList();

        return qry.ToList();
    }

    public static List<ProcessObjectData> GetAllOtherDataforEnterPrise(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   orderby x.Type, x.ParallelProcessObjID ascending
                   where x.ProcessID == processID && x.ParallelProcessObjID == null && x.Type != 0 && x.Type != 1
                   select new ProcessObjectData
                   {
                       ProcessObjID = x.ProcessObjID,
                       Type = x.Type,
                       XTop = x.XTop,
                       YLeft = x.YLeft,
                       Width = x.Width,
                       Height = x.Height,
                       Title = x.Title,
                       ParallelProcessObjID = x.ParallelProcessObjID,
                       ProcessObjName = x.ProcessObjName
                   }).ToList();

        return qry.ToList();
    }
}