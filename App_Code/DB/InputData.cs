using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InputData
/// </summary>
public class InputData
{
    #region
    VisualERPDataContext ObjData = new VisualERPDataContext();
    #endregion
    public InputData()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static List<ListInputData> GetInputData(bool inAsc, string SortBy, int poid)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_InformationInputs
                   join y in ObjData.tbl_InputTypes
                   on x.LinkType equals y.ID
                   where x.ProcessObjectID == poid 
                   select new ListInputData
                   {
                       InputName = x.LinkName,
                       InputID = x.LinkID,
                       InputValue = x.Link,
                       IncludeOnMap = Convert.ToBoolean(x.IncludeOnMap),
                       TypeName = y.Type

                   }).Distinct().ToList();
        if (inAsc)
        {
            return qry.OrderByDescending(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
    }

    public static bool SaveInputData(tbl_InformationInput inputData)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_InformationInputs
                   where x.LinkID == inputData.LinkID
                   select x).FirstOrDefault();

        if (qry == null)
        {

            ObjData.tbl_InformationInputs.InsertOnSubmit(inputData);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else
        {
            qry.ProcessObjectID = inputData.ProcessObjectID;
            qry.LinkName = inputData.LinkName;
            qry.Link = inputData.Link;
            qry.LinkID = inputData.LinkID;
            qry.IncludeOnMap = inputData.IncludeOnMap;
            qry.LinkType = inputData.LinkType;
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
    /// GetDuplicateCheck will check duplicate check of input name if it exist or not 
    /// </summary>
    /// <param name="LinkName">inputname hold the input name user entered in textbox</param>
    /// <param name="LinkID">linkid hold link id from base page</param>
    /// <returns>return tru and false </returns>
    public static bool GetDuplicateCheck(string LinkName, int poid, int LinkID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        if (LinkID > 0)
        {
            //InputNameCount will get LinkID from table tbl_InformationInputs on behalf of LinkName
            var InputNameCount = (from c in ObjData.tbl_InformationInputs
                                  where c.LinkName.ToLower() == LinkName.ToLower()
                                   && c.ProcessObjectID == poid 
                                     && c.LinkID != LinkID
                                  select c.LinkID).Count();
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
            var countCat = (from c in ObjData.tbl_InformationInputs
                            where c.LinkName.ToLower() == LinkName.ToLower()
                             && c.ProcessObjectID == poid
                            select c.LinkID).Count();
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
    /// LinkID will get link id we are editing for..
    /// </summary>
    /// <param name="AttributeMenuID">LinkID current link id</param>
    /// <returns></returns>
    public static tbl_InformationInput InputByID(Int32 LinkID)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.tbl_InformationInputs
                where bc.LinkID == LinkID
                select bc).FirstOrDefault();
    }

    /// <summary>
    /// DeleteInputLink() will delete selected Link from database mulitple tables
    /// </summary>
    /// <param name="AttributeMenuID">LinkID is current LinkID that is selected</param>
    /// <returns></returns>
    public static bool DeleteInputLink(int LinkID)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var InputVar = (from k in ObjData.tbl_InformationInputs
                        where k.LinkID == LinkID
                           select k).ToList();
        if (InputVar.Count > 0)
        {
            ObjData.DeleteInputDataByID(LinkID); //DeleteInputLink is stored procedure in database that delete inputdata of link Id
            result = true;
        }
        else
        {
            result = false;
        }

        return result;

    }

    public class ListInputData
    {
        public int InputID { get; set; }
        public string InputName { get; set; }
        public string InputValue { get; set; }
        public bool? IncludeOnMap { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TypeName { get; set; }
    }


}