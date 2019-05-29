using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ErrorData
/// </summary>
public class ErrorData
{

    public ErrorData()
    {

        //
        // TODO: Add constructor logic here
        //
    }
    public class ListErrorData
    {
        public int ErrorID { get; set; }
        public string Error { get; set; }
        public int CycleTime { get; set; }
        public int CounterMeasureStrength { get; set; }
        public string CounterMeasure { get; set; }
        public int WorkContent { get; set; }
        public int ProcessID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public static string ChangeDate(DateTime? dt)
    {
        string date = string.Empty;
        if (dt != null)
        {
            date = dt.Value.ToString("ddMMM,yyyy");
        }
        return date;
    }


    public static int GetMaxSequenceNo(int processID, int formType)
    {
        return 6;
    }

    public static bool Save(ErrorInfo errorInfo)
    {
        VisualERPDataContext objdata = new VisualERPDataContext();
        ErrorInfo item = objdata.ErrorInfos.FirstOrDefault(p => p.ErrorID == errorInfo.ErrorID);
        if (item != null)
        {
            item.IncludeOnMap = errorInfo.IncludeOnMap;
            item.Error = errorInfo.Error;
            item.CycleTime = errorInfo.CycleTime;
            item.CounterMeasureStrength = errorInfo.CounterMeasureStrength;
            item.CounterMeasure = errorInfo.CounterMeasure;
            item.WorkContent = errorInfo.WorkContent;
            item.ProcessID = errorInfo.ProcessID;
            item.ModifiedDate= DateTime.Now;
            objdata.SubmitChanges();

        }
        else
        {
            objdata.ErrorInfos.InsertOnSubmit(errorInfo);
            objdata.SubmitChanges();

        }
        return true;
    }

    public static bool Delete(int errorID)
    {
        VisualERPDataContext objdata = new VisualERPDataContext();
        ErrorInfo item = objdata.ErrorInfos.FirstOrDefault(p => p.ErrorID == errorID);
        if (item != null)
        {
            objdata.ErrorInfos.DeleteOnSubmit(item);
            objdata.SubmitChanges();
        }
        return true;
    }

    public static ErrorInfo InputByID(Int32 ErrorID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.ErrorInfos
                where bc.ErrorID == ErrorID
                select bc).FirstOrDefault();
    }
    public static bool GetDuplicateCheck(string ErrorName, int poid, int ErrorID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        if (ErrorID > 0)
        {
            //InputNameCount will get LinkID from table tbl_InformationInputs on behalf of LinkName
            var InputNameCount = (from c in ObjData.ErrorInfos
                                  where c.Error.ToLower() == ErrorName.ToLower()
                                   && c.ProcessID == poid
                                     && c.ErrorID != ErrorID
                                  select c.ErrorID).Count();
            if (InputNameCount > 0)
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
            //countcat variable will get LinkName from table tbl_InformationInputs on behalf of LinkName
            var countCat = (from c in ObjData.ErrorInfos
                            where c.Error.ToLower() == ErrorName.ToLower()
                             && c.ProcessID == poid
                            select c.ErrorID).Count();
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
    public static bool DeleteInputLink(int ErrorID)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var InputVar = (from k in ObjData.ErrorInfos
                        where k.ErrorID == ErrorID
                        select k).ToList();
        if (InputVar.Count > 0)
        {
            ObjData.DeleteErrorDataByID(ErrorID); //DeleteInputLink is stored procedure in database that delete inputdata of link Id
            result = true;
        }
        else
        {
            result = false;
        }

        return result;

    }

    public static List<ListErrorData> GetAll(bool inAsc, string SortBy, int poid)
    {
        VisualERPDataContext objdata = new VisualERPDataContext();
        var qry = objdata.ErrorInfos.Where(x => x.ProcessID == poid).Select(e => new ListErrorData
        {
            CounterMeasure = e.CounterMeasure,
            CounterMeasureStrength = Convert.ToInt32(e.CounterMeasureStrength),
            CycleTime = Convert.ToInt32(e.CycleTime),
            Error = e.Error,
            ErrorID = Convert.ToInt32(e.ErrorID),
            ProcessID = Convert.ToInt32(e.ProcessID),
            WorkContent = Convert.ToInt32(e.WorkContent),
            ModifiedDate=Convert.ToDateTime(e.ModifiedDate),
            CreatedDate= Convert.ToDateTime(e.CreatedDate)
        }).ToList();
        if (inAsc)
        {
            return qry.OrderByDescending(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
    }
}


