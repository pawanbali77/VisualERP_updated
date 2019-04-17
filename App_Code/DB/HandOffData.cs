using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HandOffData
/// </summary>
public class HandOffData
{
    public HandOffData()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bool SaveSystemIOData(tbl_SystemIO TblSystemIO)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_SystemIOs
                   where x.SytemIOID ==TblSystemIO.SytemIOID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_SystemIOs.InsertOnSubmit(TblSystemIO);
        }
        else
        {
            qry.SytemIOID = TblSystemIO.SytemIOID;
            qry.FromActivityID = TblSystemIO.FromActivityID;
            qry.ToActivityID = TblSystemIO.ToActivityID;
            qry.SystemID = TblSystemIO.SystemID;
            qry.Type = TblSystemIO.Type;
           
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

    public static bool SaveHandOffData(tbl_HandOffData HandOffData)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_HandOffDatas
                   where x.HOID == HandOffData.HOID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_HandOffDatas.InsertOnSubmit(HandOffData);
        }
        else
        {
            qry.HOID = HandOffData.HOID;
            qry.HOInputID = HandOffData.HOInputID;
            qry.SytemIOID = HandOffData.SytemIOID;
            qry.HOOutputType = HandOffData.HOOutputType;
            qry.HOOutputName = HandOffData.HOOutputName;
            qry.HOInputLink = HandOffData.HOInputLink;
            qry.IncludeonMap = HandOffData.IncludeonMap;
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
    public static List<ListHandOffData> GetHandOffData(bool inAsc, string SortBy, int SysIOId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_HandOffDatas

                   where x.SytemIOID == SysIOId 
                   select new ListHandOffData
                   {


                       HOID = x.HOID,
                       HOOutputType = x.HOOutputType,
                       HOOutputName = x.HOOutputName,
                       IncludeonMap = x.IncludeonMap,
                       HOInputID = Convert.ToInt32(x.HOInputID),
                       HOInputLink = x.HOInputLink,

                   }).Distinct().ToList();
        if (inAsc)
        {
            return qry.OrderByDescending(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
    }
    /// <summary>
    /// TFGID will get link id we are editing for..
    /// </summary>
    /// <param name="TFGID">TFGID current TFG id</param>
    /// <returns></returns>
    public static List<ListHandOffData> HandOffByID(Int32 HoID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.tbl_HandOffDatas
                where bc.HOID == HoID
                select new ListHandOffData
                {
                   
                    HOOutputType = bc.HOOutputType,
                    HOOutputName = bc.HOOutputName,
                    IncludeonMap = Convert.ToBoolean(bc.IncludeonMap),
                    HOInputID = Convert.ToInt32(bc.HOInputID),
                    HOInputLink = bc.HOInputLink
                }).Distinct().ToList();

    }

    public static List<ListHandOffData> SystemIODataByActivityId(int ActivityId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.tbl_SystemIOs
                where bc.FromActivityID == ActivityId || bc.ToActivityID == ActivityId
                select new ListHandOffData
                {
                    
                    FromActivityID = Convert.ToInt32(bc.FromActivityID),
                    ToActivityID = Convert.ToInt32(bc.ToActivityID),
                    Type = Convert.ToInt32(bc.Type),
                    SytemIOID = Convert.ToInt32(bc.SytemIOID)

                }).Distinct().ToList();

    }

    public static List<ListHandOffData> SystemIODataBySysIOId(int SysIOId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.tbl_SystemIOs
                where bc.SytemIOID == SysIOId
                select new ListHandOffData
                {

                    FromActivityID = Convert.ToInt32(bc.FromActivityID),
                    ToActivityID = Convert.ToInt32(bc.ToActivityID),
                    Type = Convert.ToInt32(bc.Type),
                    // SytemIOID = Convert.ToInt32(bc.SytemIOID)

                }).Distinct().ToList();

    }

    public static bool GetDuplicateCheck(int FromActivityId, int ToActivityId,int SystemId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        if (SystemId > 0)
        {
            //countcountry will get AttributeName from table tbl_AttributesMenus on behalf of AttributeName
            var SystemIOCount = (from c in ObjData.tbl_SystemIOs
                                 where c.FromActivityID == FromActivityId
                                 && c.ToActivityID == ToActivityId
                                 && c.SystemID == SystemId
                                  select c).Count();
            if (SystemIOCount > 0)
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
            var countCat = (from c in ObjData.tbl_SystemIOs
                            where c.FromActivityID == FromActivityId
                            && c.ToActivityID == ToActivityId
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
    public static bool DeleteHandOffData(int HoId)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var HandoffVar = (from k in ObjData.tbl_HandOffDatas
                      where k.HOID == HoId
                      select k).ToList();
        if (HandoffVar.Count > 0)
        {
            ObjData.DeleteHandOffDataByID(HoId); //DeleteTFG is stored procedure in database that delete TFGData of TFG Id
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }
    public class ListHandOffData
    {
        public int HOID { get; set; }
        public int FromActivityID { get; set; }
        public string FromActivityName { get; set; }
        public string ToActivityName { get; set; }
        public int SystemId { get; set; }
        public int? SytemIOID { get; set; }
        public int ToActivityID { get; set; }
        public int Type { get; set; }
        public int HOToID { get; set; }
        public string HOOutputType { get; set; }
        public string HOOutputName { get; set; }
        public bool? IncludeonMap { get; set; }
        public int HOInputID { get; set; }
        public string HOInputLink { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}