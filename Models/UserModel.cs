using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceCenter.Models
{
    public class ResponceModel
    {
        public string Message { get; set; }
        public bool Responce { get; set; }

    }

    public class UserModel
    {
    }

    public class UserLoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IPAddress { get; set; }
    }

    public class User : ResponceModel
    {
        public int RowNo { get; set; }
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string APIKey { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string StringRole { get; set; }

        public string Image { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }

        public string LoginSessionId { get; set; }
        public bool IsUserAuthorize { get; set; }

    }

    public enum UserRole
    {
        MasterAdmin = 1,
        Admin = 2
    }

    public class UserListDataModel
    {
        public List<User> UserList { get; set; }

        public int RecordCount { get; set; }
    }

    public class WindowsUsers
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
    }

    
    public class LoginActivity
    {
        public string LoginSessionId { get; set; }
        public string UserName { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public string IPAddress { get; set; }
    }

    public class UserPageRights
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public bool ListRights { get; set; }
        public bool AddRights { get; set; }
        public bool EditRights { get; set; }
        public bool DeleteRights { get; set; }
        public bool PrintRights { get; set; }
        public bool ExportRights { get; set; }
        public bool IsDelete { get; set; }
    }

    public class UserPageRightsList
    {
        public List<UserPageRights> lstUserPageRights { get; set; }
    }

    public enum PageRightsEnum
    {
        List = 1,
        Add = 2,
        Edit = 3,
        Delete = 4,
        Print = 5,
        Export = 6,
    }
}