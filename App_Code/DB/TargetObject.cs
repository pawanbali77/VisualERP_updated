using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProcessObject
/// </summary>
public class TargetObject
{
    public TargetObject()
    {
        //
        // TODO: Add constructor logic here
        //
    }      

    public class TargetObjectDataProperty
    {
        public string TargetObjectName { get; set; }
        public int TargetID { get; set; }
        public int TargetObjID { get; set; }
        public int OrderNO { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}