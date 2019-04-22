using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AttributeData
/// </summary>
public class AttributeData
{
    #region
      VisualERPDataContext ObjData = new VisualERPDataContext();
    #endregion
    public AttributeData()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public static List<ListAttributeData> GetAtrributeData(bool inAsc,string SortBy,int poid)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   join y in ObjData.tbl_Units
                   on x.UnitID equals y.UnitID
                   where x.ProcessObjectID==poid 
                   select new ListAttributeData
                   {
                       AttributeName= x.AttributeName,
                       AttributeMenuID=x.AttributeMenuID,
                       AttributeValue = x.AttributeValue,
                       IncludeOnMap=Convert.ToBoolean(x.IncludeOnMap),
                       UnitName= y.UnitName

                   }).Distinct().ToList();
        if (inAsc)
        {
            return qry.OrderByDescending(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();
        }

        return qry.OrderBy(x => x.GetType().GetProperty(SortBy).GetValue(x, null)).ToList();

    }

    public static bool SaveAttributeData(tbl_AttributesMenu AttributeData)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var qry = (from x in ObjData.tbl_AttributesMenus
                   where x.AttributeMenuID == AttributeData.AttributeMenuID
                   select x).FirstOrDefault();

        if(qry==null)
        {

           ObjData.tbl_AttributesMenus.InsertOnSubmit(AttributeData);
            //new ObjData().tbl_AttributesMenus.InsertOnSubmit(ListAttributeData);

        }
        else{
            qry.AttributeName = AttributeData.AttributeName;
            qry.AttributeValue = AttributeData.AttributeValue;
            qry.UnitID = AttributeData.UnitID;
            qry.IncludeOnMap = AttributeData.IncludeOnMap;
            qry.ProcessObjectID = AttributeData.ProcessObjectID;
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
    /// GetDuplicateCheck will check duplicate check of Attribute name if it exist or not 
    /// </summary>
    /// <param name="Attributename">Attributename hold the Attribute name user entered in textbox</param>
    /// <param name="Attributeid">Attributeid hold Attribute id from base page</param>
    /// <returns>return tru and false </returns>
    public static bool GetDuplicateCheck(string AttributeName,int poid, int AttributeMenuId)
    {
        VisualERPDataContext ObjData = new VisualERPDataContext();
        if (AttributeMenuId > 0)
        {
            //countcountry will get AttributeName from table tbl_AttributesMenus on behalf of AttributeName
            var AttributeNameCount = (from c in ObjData.tbl_AttributesMenus
                                      where c.AttributeName.ToLower() == AttributeName.ToLower()
                                       && c.ProcessObjectID == poid  
                                     && c.AttributeMenuID != AttributeMenuId
                                      select c.AttributeMenuID).Count();
            if (AttributeNameCount > 0)
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
            var countCat = (from c in ObjData.tbl_AttributesMenus
                            where c.AttributeName.ToLower() == AttributeName.ToLower()
                             && c.ProcessObjectID == poid  
                            select c.AttributeMenuID).Count();
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
    /// AttributeMenuID will get Attribute id we are editing for..
    /// </summary>
    /// <param name="AttributeMenuID">AttributeMenuID current Attribute id</param>
    /// <returns></returns>
    public static tbl_AttributesMenu AttributeByID(Int32 AttributeId)
    { 
        VisualERPDataContext ObjData = new VisualERPDataContext();
        return (from bc in ObjData.tbl_AttributesMenus
                where bc.AttributeMenuID == AttributeId
                select bc).FirstOrDefault();
    }

    /// <summary>
    /// DeleteAttribute() will delete selected Attribute from database mulitple tables
    /// </summary>
    /// <param name="AttributeMenuID">AttributeMenuID is current AttributeMenuID that is selected</param>
    /// <returns></returns>
    public static bool DeleteAttribute(int AttributeMenuId)
    {
        bool result = false;
        VisualERPDataContext ObjData = new VisualERPDataContext();
        var AttibuteVar = (from k in ObjData.tbl_AttributesMenus
                           where k.AttributeMenuID == AttributeMenuId
                  select k).ToList();
        if (AttibuteVar.Count > 0)
        {
           // ObjData.
            ObjData.DeleteAttributeDataByID(AttributeMenuId); //DeleteAttribute is stored procedure in database that delete Attribute of Attribute Id
            result = true;
        }
        else
        {
            result = false; }

        return result;



    }
    public class ListAttributeData
    {

        public string AttributeName { get; set; }
        public int AttributeMenuID { get; set; }
        public string AttributeValue { get; set; }
        public int UnitID { get; set; }
        public bool? IncludeOnMap { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ProcessObjectID{get;set;}
        public DateTime ModifiedDate{get;set;}
        public string UnitName { get; set; }

    }
   
}