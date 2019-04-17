using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Reflection;
using System.Web.UI;

public static class TemplateControlExtension
{
    #region TemplateControl Extension Methods

    public static string RootPath(this TemplateControl ctrl)
    {
        string path = "http://";
        HttpContext context = HttpContext.Current;
        if (context.Request.IsSecureConnection)
            path = "https://";
        path += context.Request.ServerVariables["HTTP_HOST"] + context.Request.ApplicationPath;
        path = (path.EndsWith("/") ? path : path + "/");
        return path.ToLower();
    }


    public static string RootPathWithPage(this TemplateControl ctrl, string pagename)
    {
        if (pagename.ToLower().EndsWith(".aspx"))
            return RootPath(ctrl) + pagename.ToLower();
        else
            return RootPath(ctrl) + pagename.ToLower() + ".aspx";
    }

    public static void BindDropDownList(this TemplateControl ctrl, DropDownList ddl, object dataSource, string dataTextField, string dataValueField)
    {
        BindDropDownList(ctrl, ddl, dataSource, dataTextField, dataValueField, "Select");
    }

    public static void BindList(this TemplateControl ctrl, ListControl ddl, object dataSource, string dataTextField, string dataValueField)
    {
        ddl.Items.Clear();
        ddl.DataSource = dataSource;
        ddl.DataTextField = dataTextField;
        ddl.DataValueField = dataValueField;
        ddl.DataBind();
    }
    public static void BindDropDownList(this TemplateControl ctrl, DropDownList ddl, object dataSource, string dataTextField, string dataValueField, string str)
    {
        ddl.Items.Clear();
        ddl.DataSource = dataSource;
        ddl.DataTextField = dataTextField;
        ddl.DataValueField = dataValueField;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem(str, "0"));
    }


    public static void BindCheckBoxList(this TemplateControl ctrl, CheckBoxList chklist, object dataSource, string dataTextField, string dataValueField)
    {
        chklist.Items.Clear();
        chklist.DataSource = dataSource;
        chklist.DataTextField = dataTextField;
        chklist.DataValueField = dataValueField;
        chklist.DataBind();     
    }

    public static void BindDropDownListWithGUID(this TemplateControl ctrl, DropDownList ddl, object dataSource, string dataTextField, string dataValueField, string str)
    {
        ddl.Items.Clear();
        ddl.DataSource = dataSource;
        ddl.DataTextField = dataTextField;
        ddl.DataValueField = dataValueField;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem(str, Guid.Empty.ToString()));
    }

    public static void BindDropDownListValue(this TemplateControl ctrl, DropDownList ddl, object dataSource, string dataTextField, string dataValueField, string str)
    {
        ddl.Items.Clear();
        ddl.DataSource = dataSource;
        ddl.DataTextField = dataTextField;
        ddl.DataValueField = dataValueField;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem(str, "-1"));
    }

    /// <summary>
    /// SetDropDownListValue() will set dropdown list value
    /// </summary>
    /// <param name="ctrl">object TemplateControl</param>
    /// <param name="ddl">object DropDownList</param>
    /// <param name="value">dropdown select value</param>
    public static void SetDropDownListValue(this TemplateControl ctrl, DropDownList ddl, string value)
    {
        try
        {
            ddl.SelectedValue = value;
        }
        catch
        {
            ddl.SelectedIndex = 0;
        }
    }


    /// <summary>
    /// SetDropDownListText() will set dropdown list text 
    /// </summary>
    /// <param name="ctrl">object TemplateControl</param>
    /// <param name="ddl">object DropDownList</param>
    /// <param name="value">dropdown select value</param>
    public static void SetDropDownListText(this TemplateControl ctrl, DropDownList ddl, string value)
    {
        try
        {
            ddl.SelectedValue = ddl.Items.FindByText(value).Value;
        }
        catch
        {
            ddl.SelectedIndex = 0;
        }
    }

    public static void ShowHidePanels(this TemplateControl ctrl, Panel panelToShow, params Panel[] panelsToHide)
    {
        for (int i = 0; i < panelsToHide.Length; i++) { panelsToHide[i].Visible = false; }
        panelToShow.Visible = true;
    }

    public static Guid GetGuid(this TemplateControl ctrl, string str)
    {
        try
        {
            return new Guid(str.Trim());
        }
        catch { return Guid.Empty; }
    }
    public static Guid GetGuid(this TemplateControl ctrl, object str)
    {
        try
        {
            if (str is string)
            {
                return GetGuid(ctrl, str.ToString());
            }
            return (Guid)str;
        }
        catch { return Guid.Empty; }
    }
    public static Int32 CInt32(this TemplateControl ctrl, object value)
    {
        try
        {
            if (value != null)
            {
                return Convert.ToInt32(value);
            }
            else
            {
                return -1;
            }
        }
        catch
        {
            return -1;
        }
    }
    public static decimal CDecimal(this TemplateControl ctrl, object value)
    {
        try
        {
            if (value != null)
            {
                return Convert.ToDecimal(value);
            }
            else
            {
                return -1;
            }
        }
        catch
        {
            return -1;
        }
    }

    public static double CDouble(this TemplateControl ctrl, object value)
    {
        try
        {
            if (value != null)
            {
                return Convert.ToDouble(value);
            }
            else
            {
                return -1;
            }
        }
        catch
        {
            return -1;
        }
    }

    public static string CStr(this TemplateControl ctrl, object value)
    {
        try
        {
            return Convert.ToString(value);
        }
        catch
        {
            return string.Empty;
        }
    }

    public static bool CBool(this TemplateControl ctrl, object value)
    {
        try
        {
            if (CInt32(ctrl, value) == 1)
                return true;
            else if (CInt32(ctrl, value) == 0)
                return false;
            else
                return Convert.ToBoolean(value);
        }
        catch
        {
            return false;
        }
    }

    public static SByte CSByte(this TemplateControl ctrl, object value)
    {
        try
        {
            if (value != null)
            {
                return Convert.ToSByte(value);
            }
            else
            {
                return -1;
            }
        }
        catch
        {
            return -1;
        }
    }

    public static int BoolToInt(this TemplateControl ctrl, object value)
    {
        if (value is Boolean)
        {
            return (Convert.ToBoolean(value) ? 1 : 0);
        }
        else if (value is string)
        {
            try
            {
                return (Convert.ToString(value).ToLower() == "true" ? 1 : 0);
            }
            catch
            {
                return -1;
            }
        }
        else
        {
            return -1;
        }
    }

    public static DataTable ListToDataTable<T>(this TemplateControl ctrl, List<T> list)
    {
        DataTable table = new DataTable();
        foreach (PropertyInfo info in typeof(T).GetProperties())
        {
            table.Columns.Add(new DataColumn(info.Name)); //, info.PropertyType
        }
        foreach (T t in list)
        {
            DataRow row = table.NewRow();
            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                row[info.Name] = info.GetValue(t, null);
            }
            table.Rows.Add(row);
        }
        return table;
    }

    public static bool IsValidDataTable(this TemplateControl ctrl, DataTable table)
    {
        return (table != null) ? ((table.Rows.Count > 0) ? true : false) : false;
    }

    public static void BindDropDownPaging(DropDownList ddl,int AdminPageSize)
    {
        ddl.Items.Clear();
        for (int i = 10; i <= 50;)
        {
            ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
            i = i + 10;        
        }

        try
        {
            ddl.SelectedValue = AdminPageSize.ToString();
        }
        catch
        {
            ddl.Items.Insert(0, new ListItem(AdminPageSize.ToString(), AdminPageSize.ToString()));
        }
    }

    #region Check Query String Key Value and return if exists otherwise return null.
    /// <summary>
    /// Check Query String Key Value and return if exists otherwise return null.
    /// </summary>
    /// <param name="queryStringName">QueryString Key NAme</param>
    /// <returns>QueryString Value if exists otherwise null.</returns>
    public static string CheckQueryString(this TemplateControl ctrl, string queryStringName)
    {
        if (HttpContext.Current.Request.QueryString[queryStringName] != null || Convert.ToString(HttpContext.Current.Request.QueryString[queryStringName]) != "")
        {
            return Convert.ToString(HttpContext.Current.Request.QueryString[queryStringName]);
        }
        else
        {
            return null;
        }
    }
    #endregion

    #endregion
}