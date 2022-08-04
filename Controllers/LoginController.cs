using ServiceCenter.Models;
using ServiceCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceCenter.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if(Session["User"] != null)
            {
                return RedirectToAction("Dashboard", "Job");
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult Login(UserLoginModel objUserLoginModel)
        {
            if(objUserLoginModel != null && !string.IsNullOrEmpty(objUserLoginModel.name) && !string.IsNullOrEmpty(objUserLoginModel.password))
            {
                UserService objUserService = new UserService();
                User objUser = objUserService.UserLogin(objUserLoginModel);

                if(objUser.Responce)
                {
                    SessionService.CreateUserSession(objUser);
                    return RedirectToAction("Dashboard", "Job");
                }
                else
                {
                    ViewBag.LoginError = objUser.Message;
                    return View("Index", objUserLoginModel);
                }
            }
            else
            {
                return View("Index");
            }
            
        }

        public ActionResult Logout()
        {
            SessionService.DestroyUserSession();
            return View("Index");
        }
    }
}