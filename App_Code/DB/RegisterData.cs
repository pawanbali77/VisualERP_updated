using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RegisterData
/// </summary>
public class RegisterData
{
    public RegisterData()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static tbl_Registration Login_Check(string password, string email)
    {
        VisualERPDataContext db = new VisualERPDataContext();
        return (from k in db.tbl_Registrations
                where k.Password == password && k.Email == email && k.Status == true && k.IsDeleted == false
                select k).FirstOrDefault();
    }
    public static tbl_Registration check_Parentid(int id)
    {
        VisualERPDataContext db = new VisualERPDataContext();
        return (from k in db.tbl_Registrations
                where k.RegisterID == id && k.Status == true && k.IsDeleted == false
                select k).FirstOrDefault();
    }
    public static tbl_Registration check_UserRole(int id)
    {
        VisualERPDataContext db = new VisualERPDataContext();
        return (from k in db.tbl_Registrations
                where k.RegisterID == id && k.Status == true && k.IsDeleted == false
                select k).FirstOrDefault();
    }
    public static int register_Users(tbl_Registration sp)
    {
        VisualERPDataContext db = new VisualERPDataContext();
        tbl_Registration obj = new tbl_Registration();
        obj = (from data in db.tbl_Registrations where data.Email == sp.Email select data).FirstOrDefault();
        if (obj == null)
        {
            obj = new tbl_Registration();
            obj.Email = sp.Email;
            obj.Password = sp.Password;
            obj.Mobile = sp.Mobile;
            obj.CompanyName = sp.CompanyName;
            obj.CreatedDate = sp.CreatedDate;
            obj.Status = sp.Status;
            obj.Role = sp.Role;
            obj.IsDeleted = sp.IsDeleted;
            obj.ParentID = sp.ParentID;
            obj.Industries = sp.Industries;
            obj.UploadPhoto = sp.UploadPhoto;

            db.tbl_Registrations.InsertOnSubmit(obj);
            try
            {
                db.SubmitChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        else
        {
            return 2;
        }
    }
    public static int register_newUsers(tbl_Registration sp_new)
    {
        VisualERPDataContext db = new VisualERPDataContext();
        tbl_Registration obj = new tbl_Registration();
        obj = (from data in db.tbl_Registrations where data.Email == sp_new.Email && data.RegisterID == sp_new.ParentID select data).FirstOrDefault();
        if (obj == null)
        {
            obj = new tbl_Registration();
            obj.Username = sp_new.Username;
            obj.Email = sp_new.Email;
            obj.Password = sp_new.Password;
            obj.Mobile = sp_new.Mobile;
            obj.CompanyName = sp_new.CompanyName;
            obj.CreatedDate = sp_new.CreatedDate;
            obj.Status = sp_new.Status;
            obj.Industries= sp_new.Industries;
            obj.Role = sp_new.Role;
            obj.IsDeleted = sp_new.IsDeleted;
            obj.ParentID = sp_new.ParentID;
            obj.UploadPhoto = sp_new.UploadPhoto;
            db.tbl_Registrations.InsertOnSubmit(obj);
            try
            {
                db.SubmitChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        else
        {
            return 2;
        }
    }
    public static List<GridData> allusers(int id)
    {
        VisualERPDataContext db = new VisualERPDataContext();
        List<GridData> gridData = (from reg in db.tbl_Registrations
                                   where reg.ParentID == id && reg.IsDeleted == false
                                   join rol in db.tbl_Roles on reg.Role equals rol.ID
                                   select new GridData()
                                   {
                                       RegisterID = reg.RegisterID,
                                       mobile = reg.Mobile,
                                       status = reg.Status,
                                       Userrole = rol.Role,
                                       username = reg.Username
                                   }).ToList();
        return gridData;
    }
    public static List<GridProcess> check_Process(int id)
    {
        VisualERPDataContext db = new VisualERPDataContext();
        List<GridProcess> data = (from k in db.tbl_Processes
                                  where k.CompanyID == id && (k.ParentID == null || k.ParentID == 0)
                                  select new GridProcess()
                                  {
                                      CompanyID = k.CompanyID,
                                      ProcessName = k.ProcessName,
                                      ProcessId = k.ProcessID,
                                      UserID = k.UserRegisterID
                                  }).ToList();
        return data;
    }



    public static List<GridProcess> check_Process_Viewer(int id)
    {
        VisualERPDataContext db = new VisualERPDataContext();
        List<GridProcess> data = (from k in db.tbl_Processes
                                  where k.CompanyID == id && (k.ParentID == null || k.ParentID == 0)
                                  select new GridProcess()
                                  {
                                      CompanyID = k.CompanyID,
                                      ProcessName = k.ProcessName,
                                      ProcessId = k.ProcessID,
                                      UserID = k.UserRegisterID
                                  }).ToList();
        return data;
    }
    public static List<GridProcess> check_Process_IndividualProcess(int id)
    {
        VisualERPDataContext db = new VisualERPDataContext();
        List<GridProcess> data = (from k in db.tbl_Processes
                                  where k.UserRegisterID == id && (k.ParentID == null || k.ParentID == 0)
                                  select new GridProcess()
                                  {
                                      CompanyID = k.CompanyID,
                                      ProcessName = k.ProcessName,
                                      ProcessId = k.ProcessID,
                                      UserID = k.UserRegisterID
                                  }).ToList();
        return data;
    }
    public static List<GridUserdata> EditUser(int id , int Parent_id)
    {
        VisualERPDataContext db = new VisualERPDataContext();
        List<GridUserdata> gridData = (from reg in db.tbl_Registrations
                                       where reg.RegisterID == id && reg.ParentID == Parent_id && reg.IsDeleted == false
                                       join indus in db.tbl_Industries on reg.Industries equals indus.ID
                                       select new GridUserdata()
                                       {
                                           RegisterID = reg.RegisterID,
                                           mobile = reg.Mobile,
                                           status = reg.Status,
                                           Industry = indus.Name,
                                           UploadPhoto = reg.UploadPhoto,
                                           username = reg.Username,
                                           Companyimage = reg.UploadPhoto,
                                           Companyname = reg.CompanyName

                                       }).ToList();
        return gridData;
    }
    public static List<GridUserdata2> EditUser2(int id , int Parent_id)
    {
        VisualERPDataContext db = new VisualERPDataContext();
        List<GridUserdata2> gridData = (from reg in db.tbl_Registrations
                                       where reg.RegisterID == id && reg.IsDeleted == false && reg.ParentID == Parent_id
                                        select new GridUserdata2()
                                       {
                                           RegisterID = reg.RegisterID,
                                           mobile = reg.Mobile,
                                           status = reg.Status,
                                           username = reg.Username
                                         
                                       }).ToList();
        return gridData;
    }
    public static string ProfileImage(int id, string ComapnyName)
    {
        using (VisualERPDataContext db = new VisualERPDataContext())
        {
            var profileLogo = db.tbl_Registrations.FirstOrDefault(s => s.CompanyName.Contains(ComapnyName) && s.ParentID == 0).UploadPhoto;                                      
            return profileLogo;
        }           
    }
    public static tbl_Registration Check_Email(string email)
    {
        VisualERPDataContext db = new VisualERPDataContext();

        return (from k in db.tbl_Registrations
                where k.Email == email && k.Status == true && k.IsDeleted == false
                select k).FirstOrDefault();
    }
    public static tbl_Registration Update_UniqueCode(string email , string uniquecode)
    {
        VisualERPDataContext db = new VisualERPDataContext();
        var  data = db.tbl_Registrations.Single(x => x.Email == email);
        data.UniqueCode = uniquecode;
        db.SubmitChanges();
        return data;
    }
   public static tbl_Registration Check_EmailandUniqueCode(string email , string uniquecode)
    {
        VisualERPDataContext db = new VisualERPDataContext();
        return (from k in db.tbl_Registrations
                where k.Email == email && k.UniqueCode == uniquecode && k.Status == true && k.IsDeleted == false
                select k).FirstOrDefault();
    }
    public static tbl_Registration Update_Password(string email, string uniquecode , string password)
    {
        VisualERPDataContext db = new VisualERPDataContext();
        var data = db.tbl_Registrations.Single(x => x.Email == email && x.UniqueCode == uniquecode);
        data.Password = password;
        data.UniqueCode = "";
        db.SubmitChanges();
        return data;
    }
}

public class GridData
{
    public int RegisterID { get; set; }
    public string mobile { get; set; }
    public bool? status { get; set; }
    public string Userrole { get; set; }
    public string username { get; set; }
}
public class GridProcess
{
    public int? UserID { get; set; }
    public int? CompanyID { get; set; }
    public int ProcessId { get; set; }
    public string ProcessName { get; set; }
}

public class GridUserdata
{
    public int RegisterID { get; set; }
    public string mobile { get; set; }
    public bool? status { get; set; }
    public string Industry { get; set; }
    public string username { get; set;}
    public string UploadPhoto { get; set;}
    public string Companyname { get; set; }
    public string Companyimage { get; set; }
}

public class GridUserdata2
{
    public int RegisterID { get; set; }
    public string mobile { get; set; }
    public bool? status { get; set; }
    public string username { get; set; }
}

