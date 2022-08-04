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
        public string name { get; set; }
        public string password { get; set; }
    }

    public class User : ResponceModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string apikey { get; set; }
        public int status { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string created_at { get; set; }
    }
}