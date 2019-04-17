using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
//using Zayko.Finance;
using System.Text;
using System.Globalization;
/// <summary>
/// Summary description for GenralFunction
/// </summary>
public class GenralFunction
{
	public GenralFunction()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string UploadBookImage(FileUpload fu, string filepath, string filename, string previousFileName)
    {
        string StrFilePath = string.Empty;
        if ((fu.PostedFile != null) && (fu.PostedFile.ContentLength > 0))
        {
            string fileFullPath = filepath;
            string fileName = filename;
            string fileExtension = Path.GetExtension(fu.PostedFile.FileName.ToString());

            if (File.Exists(HttpContext.Current.Server.MapPath(fileFullPath + fileName + fileExtension)))
            {
                File.Delete(HttpContext.Current.Server.MapPath(fileFullPath + fileName + fileExtension));
            }
            fu.SaveAs(HttpContext.Current.Server.MapPath(fileFullPath + fileName + fileExtension));
            StrFilePath = fileFullPath + fileName + fileExtension;
        }
        else
        {
            StrFilePath = previousFileName;
        }
        return StrFilePath;
    }
    public static void ShowHidePanels(Panel panelToShow, params Panel[] panelsToHide)
    {
        for (int i = 0; i < panelsToHide.Length; i++) { panelsToHide[i].Visible = false; }
        panelToShow.Visible = true;
    }
}