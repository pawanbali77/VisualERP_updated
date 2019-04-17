using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MachineData
/// </summary>
public class MachineData
{
    public MachineData()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static List<ListMachineData> GetMachineDataByObjId(bool inAsc, string SortBy, int poid)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_MachineLists

                   where x.ProcessObjectID == poid
                   select new ListMachineData
                   {

                       MachineID = x.MachineID,
                       ProcessObjectID = Convert.ToInt32(x.ProcessObjectID),
                       MachineName = x.MachineName,
                       MachineType = x.MachineType,
                       MachinePhoto = x.MachinePhoto,
                       PMScheduleID = Convert.ToInt32(x.PMScheduleID),
                       MTBF = x.MTBF,
                       MTTR = x.MTTR,
                       MaintenanceCost = (float)x.MaintenanceCost,
                       PurchasePrice = (float)x.PurchasePrice,
                       BookValue = (float)x.BookValue,
                       RemainingLife = x.RemainingLife,
                       ManualID = x.MachineID,
                       PartsListID = Convert.ToInt32(x.PartsListID),
                   }).Distinct().ToList();
        return qry;
    }
    /// <summary>
    /// DeleteMachineDatabyId() will delete selected Mchine Data from database mulitple tables
    /// </summary>
    /// <param name="Machine ID">Machine ID is current Machine ID that is selected</param>
    /// <returns></returns>
    public static bool DeleteMachineDatabyId(int MchId)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var MachineData = (from k in ObjData.tbl_MachineLists
                           where k.MachineID == MchId
                           select k).ToList();
        if (MachineData.Count > 0)
        {
            ObjData.DeleteMachineDataByID(MchId); //DeleteMachine is stored procedure in database that delete TFGData of TFG Id
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    /// <summary>
    /// Machine Id will get link id we are editing for..
    /// </summary>
    /// <param name="MchId">MchId current Machine id</param>
    /// <returns></returns>
    public static tbl_MachineList MachineDataById(Int32 MchId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.tbl_MachineLists
                where bc.MachineID == MchId
                select bc).FirstOrDefault();
    }

    /// <summary>
    /// GetDuplicateCheck will check duplicate check of Machine name if it exist or not 
    /// </summary>
    /// <param name="MchId">Machine Id hold link id from base page</param>
    /// <returns>return tru and false </returns>
    public static bool GetDuplicateCheckMachineName(string MachineName, int poid, int MchId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        if (MchId > 0)
        {
            //MachineNameCount will get MchId from table tbl_MachineLists on behalf of MachineName
            var MachineNameCount = (from c in ObjData.tbl_MachineLists
                                    where c.MachineName.ToLower() == MachineName.ToLower()
                                      && c.ProcessObjectID == poid
                                         && c.MachineID != MchId
                                    select c.MachineID).Count();
            if (MachineNameCount > 0)
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
            //countcat variable will get Machine Name from table tbl_MachineLists on behalf of Machine Name
            var countCat = (from c in ObjData.tbl_MachineLists
                            where c.MachineName.ToLower() == MachineName.ToLower()
                              && c.ProcessObjectID == poid
                            select c.MachineID).Count();
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

    /// <summary>
    /// For Addding Machine data 
    /// </summary>
    /// 
    public static bool SaveMachineData(tbl_MachineList MachineData)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_MachineLists
                   where x.MachineID == MachineData.MachineID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_MachineLists.InsertOnSubmit(MachineData);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {
            qry.MachineID = MachineData.MachineID;
            qry.ProcessObjectID = MachineData.ProcessObjectID;
            qry.MachineName = MachineData.MachineName;
            qry.MachineType = MachineData.MachineType;
            qry.PMScheduleID = MachineData.PMScheduleID;
            qry.MTBF = MachineData.MTBF;
            qry.MTTR = MachineData.MTTR;
            qry.MaintenanceCost = MachineData.MaintenanceCost;
            qry.PurchasePrice = MachineData.PurchasePrice;
            qry.BookValue = MachineData.BookValue;
            qry.RemainingLife = MachineData.RemainingLife;
            qry.ManualID = MachineData.ManualID;
            qry.PartsListID = MachineData.PartsListID;
            qry.MachinePhoto = MachineData.MachinePhoto;
            qry.ModifiedDate = MachineData.ModifiedDate;

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
    public class ListMachineData
    {
        public int MachineID { get; set; }
        public int ProcessObjectID { get; set; }
        public string MachineName { get; set; }
        public string MachineType { get; set; }
        public string MachinePhoto { get; set; }
        public int PMScheduleID { get; set; }
        public string MTBF { get; set; }
        public string MTTR { get; set; }
        public float MaintenanceCost { get; set; }
        public float PurchasePrice { get; set; }
        public float BookValue { get; set; }
        public string RemainingLife { get; set; }
        public int ManualID { get; set; }
        public int PartsListID { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}