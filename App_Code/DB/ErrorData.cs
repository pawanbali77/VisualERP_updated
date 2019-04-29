using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ErrorData
/// </summary>
public class ErrorData
{
    VisualERPDataContext objdata = new VisualERPDataContext();
    public ErrorData()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int GetMaxSequenceNo(int processID, int formType)
    {
        return 6;
    }

    public bool Save(ErrorInfo errorInfo)
    {
        ErrorInfo item= objdata.ErrorInfos.FirstOrDefault(p => p.ErrorID == errorInfo.ErrorID);
        if(item!=null)
        {
            item.Error = errorInfo.Error;
            item.CycleTime = errorInfo.CycleTime;
            item.CounterMeasureStrength = errorInfo.CounterMeasureStrength;
            item.CounterMeasure = errorInfo.CounterMeasure;
            item.WorkContent = errorInfo.WorkContent;
            item.ProcessID = errorInfo.ProcessID;
            objdata.SubmitChanges();

        }
        else
        {
            objdata.ErrorInfos.InsertOnSubmit(errorInfo);
            objdata.SubmitChanges();

        }
        return true;
    }
    public bool Delete(int errorID)
    {
        ErrorInfo item = objdata.ErrorInfos.FirstOrDefault(p => p.ErrorID == errorID);
        if (item != null)
        {
            objdata.ErrorInfos.DeleteOnSubmit(item);
            objdata.SubmitChanges();
        }
        return true;
    }
    public List<ErrorInfo> GetAll()
    { 
        return objdata.ErrorInfos.ToList();
        
    }
}
 

