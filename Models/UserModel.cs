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
}