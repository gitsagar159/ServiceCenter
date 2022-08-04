using ServiceCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceCenter.Services
{
    public static class SessionService
    {
        public static void CreateUserSession(User objUserDetail)
        {
            if (objUserDetail != null)
            {
                HttpContext.Current.Session["User"] = objUserDetail;
            }
        }

        public static void DestroyUserSession()
        {
            HttpContext.Current.Session["User"] = null;
        }

        public static User GetUserSessionValues()
        {
            User ObjUser = new User();

            ObjUser = (User)HttpContext.Current.Session["User"];

            return ObjUser;
        }
    }
}