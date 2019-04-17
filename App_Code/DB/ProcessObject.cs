using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProcessObject
/// </summary>
public class ProcessObject
{
    public ProcessObject()
    {
        //
        // TODO: Add constructor logic here
        //
    }      

    public class ProcessObjectDataProperty
    {
        public string ProcessObjectName { get; set; }
        public int ProcessID { get; set; }
        public int ProcessObjID { get; set; }
        public int OrderNO { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}