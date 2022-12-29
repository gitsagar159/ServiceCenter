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
                //HttpContext.Current.Session["WindowsUser"] = HttpContext.Current.User.Identity.Name;

                UserService objUserService = new UserService();

                UserPageRightsList objUserPageRightsList = objUserService.GetUserPagesRightsListByUserId(objUserDetail.id);

                if(objUserPageRightsList != null && objUserPageRightsList.lstUserPageRights.Count > 0)
                {
                    HttpContext.Current.Session["UserPageRights"] = objUserPageRightsList;
                }

            }
        }

        public static void DestroyUserSession()
        {
            HttpContext.Current.Session["User"] = null;
            HttpContext.Current.Session["UserPageRights"] = null;
        }

        public static User GetUserSessionValues()
        {
            User ObjUser = new User();

            ObjUser = (User)HttpContext.Current.Session["User"];

            return ObjUser;
        }

        public static UserPageRightsList GetUserPageRightsSessionValues()
        {
            UserPageRightsList ObjUserPageRightsList = new UserPageRightsList();

            ObjUserPageRightsList = (UserPageRightsList)HttpContext.Current.Session["UserPageRights"];

            return ObjUserPageRightsList;
        }
    }
}