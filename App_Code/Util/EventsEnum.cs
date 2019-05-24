using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EventsEnum
/// </summary>
public class ControlsEnum
{
    public ControlsEnum()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
public enum ProcessControl
{
    Supplier = 2,
    Shipment = 3,
    Forcast = 4,
    Arrow = 5,
    DSchedule = 6,
    Production = 7,
    Electronic = 8,
    DataTable = 9,
    TimelineSegment = 10,
    Timelinetotal = 11,
    Supermarket = 12,
    SafetyStock = 13,
    SignalKanban = 14,
    Withdrawalkanban = 15,
    WithdrawalBatch = 16,
    ProductionKanban = 17,
    BatchKanban = 18,
    KanbanPost = 19,
    FIFOLane = 20,
    KaizenBurst = 21,
    PullArrow1 = 22,
    PullArrow2 = 23,
    PullArrow3 = 24,
    PhysicalPull = 25,
    SequencedPullBall = 26,
    LoadLeveling = 27,
    // ParallelActivity=28
}

public enum ReportTypeID
{
    Attribute = 1,
    Bom = 2,
    TFG = 3,
    Machine = 4,
    PCS = 5,
    DCS = 6,
    TGTGAP = 7,
    Inventory = 8,
    Error=9,
    CustomStandardReport=10
}

public enum ArrowControl
{
    Arrow1 = 30,
    Arrow2 = 31,
    Arrow3 = 32,
    Arrow4 = 33,
    Arrow5 = 34,
    Arrow6 = 35,
    Arrow7 = 36,
    Arrow8 = 37,
    Arrow9 = 38,
    Arrow10 = 39,
    Arrow11 = 40,
    Arrow12 = 41,
    Arrow13 = 42,
    Arrow14 = 43,
    Arrow15 = 44,
    Arrow16 = 45,
    Arrow17 = 46,
    Arrow18 = 47,
    Arrow19 = 48,
    Arrow20 = 49,
    Arrow21 = 50
}

public enum SummaryFunction
{
    Sum = 1,
    Average = 2,
    Median = 3,
    Min = 4,
    Max = 5,
    Standard_deviation = 6
}

public enum FormType
{
    PPESA = 0,
    PDESA = 1,
    ERRORLOG = 2
}

public enum Actions { True = 1, False = 2 };
public enum Status { Active = 0, Canceled = 3 };