using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.Linq;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public BasePage()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetExpires(DateTime.Now.AddMinutes(-1));
    }
   

    public int EditIDINT
    {
        get
        {
            if (ViewState["EditIDInt"] != null)
            {
                return (int)ViewState["EditIDInt"];
            }
            return 0;
        }
        set { ViewState["EditIDInt"] = value; }
    }

    public Guid EditDataID
    {
        get
        {
            if (ViewState["EditID"] != null)
            {
                return (Guid)ViewState["EditID"];
            }
            return Guid.Empty;
        }
        set { ViewState["EditID"] = value; }
    }


    public bool IsEdit
    {
        get
        {
            if (ViewState["IsEdit"] != null)
            {
                return (bool)ViewState["IsEdit"];
            }
            return false;
        }
        set { ViewState["IsEdit"] = value; }
    }

}