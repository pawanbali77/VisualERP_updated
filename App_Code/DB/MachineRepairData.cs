using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MachineRepairData
/// </summary>
public class MachineRepairData
{
   // VisualERPDataContext ObjData = new VisualERPDataContext();
	public MachineRepairData()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static List<ListMachineRepairData> GetMachineRepairDataByMchId(bool inAsc, string SortBy, int machineId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_RepairLogs

                   where x.MachineID == machineId
                   select new ListMachineRepairData
                   {

                      
                       MachineRepairID=x.MachineRepairID,
                       Critical = x.Critical,
                       TTR = x.TTR,
                       SkillTypeOfRepair = x.TypeOfRepair,
                       TypeOfRepair = x.TypeOfRepair,
                       ActualRepair = x.ActualRepair,
                       CostOfRepairParts = (float)x.CostOfRepairParts,
                       CostOfRepairLabor = (float)x.CostOfRepairLabor,
                       CostOfRepairOutsource = (float)x.CostOfRepairOutsource,
                       Scheduled_Unscheduled = x.Scheduled_Unscheduled,
                       DownTime = x.DownTime,
                       Preventive_Predictive_Reactive = x.Preventive_Predictive_Reactive,
                       RootCause = x.RootCause,
                       Countermeasure = x.Countermeasure,
                   }).Distinct().ToList();
        return qry;
    }

    /// <summary>
    /// MachineRepair Id will get link id we are editing for..
    /// </summary>
    /// <param name="MachineRepair Id">MachineRepair Id current MachineRepair Id</param>
    /// <returns></returns>
    public static tbl_RepairLog MachineRepairDataById(Int32 MchRepairId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.tbl_RepairLogs
                where bc.MachineRepairID == MchRepairId
                select bc).FirstOrDefault();
    }

    /// <summary>
    /// For Addding Machine Repair data 
    /// </summary>
    /// 
    public static bool SaveMachineRepairData(tbl_RepairLog tblMchRepairLog)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_RepairLogs
                   where x.MachineRepairID == tblMchRepairLog.MachineRepairID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_RepairLogs.InsertOnSubmit(tblMchRepairLog);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {
            qry.MachineID = tblMchRepairLog.MachineID;
            qry.MachineRepairID = tblMchRepairLog.MachineRepairID;
            qry.Critical = tblMchRepairLog.Critical;
            qry.TTR = tblMchRepairLog.TTR;
            qry.SkillTypeOfRepair = tblMchRepairLog.SkillTypeOfRepair;
            qry.TypeOfRepair = tblMchRepairLog.TypeOfRepair;
            qry.ActualRepair = tblMchRepairLog.ActualRepair;
            qry.CostOfRepairParts = tblMchRepairLog.CostOfRepairParts;
            qry.CostOfRepairLabor = tblMchRepairLog.CostOfRepairLabor;
            qry.CostOfRepairOutsource = tblMchRepairLog.CostOfRepairOutsource;
            qry.Scheduled_Unscheduled = tblMchRepairLog.Scheduled_Unscheduled;
            qry.DownTime = tblMchRepairLog.DownTime;
            qry.Preventive_Predictive_Reactive = tblMchRepairLog.Preventive_Predictive_Reactive;
            qry.RootCause = tblMchRepairLog.RootCause;
            qry.Countermeasure = tblMchRepairLog.Countermeasure;
            qry.ModifiedDate = tblMchRepairLog.ModifiedDate;
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

    /// <summary>
    /// DeleteMachineRepairDatabyId() will delete selected Mchine Data from database mulitple tables
    /// </summary>
    /// <param name="MachineRepairID">MachineRepair ID is current MachineRepair ID that is selected</param>
    /// <returns></returns>
    public static bool DeleteMachineRepairDataByID(int MchRepairId)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var MachineRepairData = (from k in ObjData.tbl_RepairLogs
                           where k.MachineRepairID == MchRepairId
                           select k).ToList();
        if (MachineRepairData.Count > 0)
        {
            ObjData.DeleteMachineRepairDataByID(MchRepairId); //Delete Machine Repair Data is stored procedure in database that delete TFGData of TFG Id
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    /// <summary>
    /// DeleteMachineRepairDatabyId() will delete selected Mchine Data from database mulitple tables
    /// </summary>
    /// <param name="MachineRepairID">MachineRepair ID is current MachineRepair ID that is selected</param>
    /// <returns></returns>
    public static bool DeleteMachineRepairDataByMachineID(int MachineID)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var MachineRepairData = (from k in ObjData.tbl_RepairLogs
                                 where k.MachineID == MachineID
                                 select k).ToList();
        if (MachineRepairData.Count > 0)
        {
            ObjData.DeleteMachineRepairDataByMachineID(MachineID); //Delete Machine Repair Data is stored procedure in database that delete TFGData of TFG Id
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }
    public class ListMachineRepairData
    {
        public int MachineID { get; set; }
        public int MachineRepairID { get; set; }
        public string Critical { get; set; }
        public string TTR { get; set; }
        public string SkillTypeOfRepair { get; set; }
        public string TypeOfRepair { get; set; }
        public string ActualRepair { get; set; }
        public float CostOfRepairParts { get; set; }
        public float CostOfRepairLabor { get; set; }
        public float CostOfRepairOutsource { get; set; }
        public string Scheduled_Unscheduled { get; set; }
        public string RemainingLife { get; set; }
        public string DownTime { get; set; }
        public string Preventive_Predictive_Reactive { get; set; }
        public string RootCause { get; set; }
        public string Countermeasure { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}