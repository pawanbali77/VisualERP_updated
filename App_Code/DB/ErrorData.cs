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
    public List<ErrorInfo> GetAll()
    {
        VisualERPDataContext Objdata = new VisualERPDataContext();
        List<ErrorInfo> errors = new List<ErrorInfo>();
        errors.Add(new ErrorInfo() {
            CycleTime = 1,
             Error="Test Error"

        });

        errors.Add(new ErrorInfo()
        {
            CycleTime = 1,
            Error = "Test Error"

        });

        errors.Add(new ErrorInfo()
        {
            CycleTime = 1,
            Error = "Test Error"

        });

        errors.Add(new ErrorInfo()
        {
            CycleTime = 1,
            Error = "Test Error"

        });
        return errors;
    }
}

public class ErrorInfo
{
    public int ErrorID { get; set; }
    public string Error { get; set; }
    public int CycleTime { get; set; }
   public int  Sequence { get; set; }
    public int CounterMeasureStrength { get; set; }
    public int CounterMeasure { get; set; }
    public int WorkContent { get; set; }
}

