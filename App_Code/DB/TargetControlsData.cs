using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 

/// <summary>
/// Summary description for ControlsData
/// </summary>
public class TargetControlsData
{
    public TargetControlsData()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static int SaveControlData(tbl_TargetObject processObjData)
    {
        int NewID;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_TargetObjects
                   where x.TargetObjID == processObjData.TargetObjID
                   select x).FirstOrDefault();

        if (qry == null)
        {
            ObjData.tbl_TargetObjects.InsertOnSubmit(processObjData);
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
            NewID = processObjData.TargetObjID;

            return NewID;
        }
        catch
        {
            return 0;

        }        
    }

    public static List<ControlsData.ProcessObjectData> GetAllProcessControlData(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_TargetObjects
                   orderby x.Type, x.ParallelTargetObjID ascending
                   where x.TargetID == processID //&& (x.Type==0 || x.Type==1)
                   select new ControlsData.ProcessObjectData
                   {
                       ProcessObjID = x.TargetObjID,
                       Type = x.Type,
                       XTop = x.XTop,
                       YLeft = x.YLeft,
                       Width = x.Width,
                       Height = x.Height,
                       Title = x.Title,
                       ParallelProcessObjID = x.ParallelTargetObjID,
                       ProcessObjName = x.TargetObjName,
                       TypeParallel = x.ParallelTargetObjID == null?0:1
                   }).OrderBy(a=>a.TypeParallel).ToList();

        return qry.ToList();
    }


    public static List<TargetObjectData> GetProcessInventoryData(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   where x.ProcessID == processID && (x.Type == 0 || x.Type==1)
                   select new TargetObjectData
                   {
                        
                       TargetObjID = x.ProcessObjID,
                       Type = x.Type,
                       XTop = x.XTop,
                       YLeft = x.YLeft,
                       Width = x.Width,
                       Height = x.Height,
                       Title = x.Title,
                   }).ToList();

        return qry.ToList();
    }

    //public class ProcessObjectData
    //{
    //    public int ProcessObjID { get; set; }
    //    public string ProcessObjName { get; set; }
    //    public int? Type { get; set; }
    //    public int? XTop { get; set; }
    //    public int? YLeft { get; set; }
    //    public int? Width { get; set; }
    //    public int? Height { get; set; }
    //    public string Title { get; set; }
    //    public DateTime CreatedDate { get; set; }
    //    public DateTime ModifiedDate { get; set; }
    //    public int? ParallelProcessObjID { get; set; }  /////////////////
    //    public int? TypeParallel { get; set; }

    //}


    public class TargetObjectData
    {
        public int TargetObjID { get; set; }
        public string TagetObjName { get; set; }
        public int? Type { get; set; }
        public int? XTop { get; set; }
        public int? YLeft { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? ParallelTargetObjID { get; set; }  /////////////////
        public int? TypeParallel { get; set; }

    }

    //*******************Enterprise1***********************//


    public static List<TargetObjectData> GetAllProcessDataforEnterPrise(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   orderby x.Type, x.ParallelProcessObjID ascending
                   where x.ProcessID == processID && x.ParallelProcessObjID == null && (x.Type == 0 || x.Type==1)
                   select new TargetObjectData
                   {
                       TargetObjID = x.ProcessObjID,
                       Type = x.Type,
                       XTop = x.XTop,
                       YLeft = x.YLeft,
                       Width = x.Width,
                       Height = x.Height,
                       Title = x.Title,
                       ParallelTargetObjID = x.ParallelProcessObjID,
                       TagetObjName = x.ProcessObjName
                   }).ToList();

        return qry.ToList();
    }

    public static List<TargetObjectData> GetAllParallelDataforEnterPrise(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   orderby x.Type, x.ParallelProcessObjID ascending
                   where x.ProcessID == processID && x.ParallelProcessObjID!=null && x.Type==0
                   select new TargetObjectData
                   {
                       TargetObjID = x.ProcessObjID,
                       Type = x.Type,
                       XTop = x.XTop,
                       YLeft = x.YLeft,
                       Width = x.Width,
                       Height = x.Height,
                       Title = x.Title,
                       ParallelTargetObjID = x.ParallelProcessObjID,
                       TagetObjName = x.ProcessObjName                       
                   }).ToList();

        return qry.ToList();
    }

    public static List<TargetObjectData> GetAllOtherDataforEnterPrise(int processID)
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        //qry will return GetTypeID details according our search query
        var qry = (from x in Objdata.tbl_ProcessObjects
                   orderby x.Type, x.ParallelProcessObjID ascending
                   where x.ProcessID == processID && x.ParallelProcessObjID == null && x.Type != 0 && x.Type !=1
                   select new TargetObjectData
                   {
                       TargetObjID = x.ProcessObjID,
                       Type = x.Type,
                       XTop = x.XTop,
                       YLeft = x.YLeft,
                       Width = x.Width,
                       Height = x.Height,
                       Title = x.Title,
                       ParallelTargetObjID = x.ParallelProcessObjID,
                       TagetObjName = x.ProcessObjName
                   }).ToList();

        return qry.ToList();
    }
}