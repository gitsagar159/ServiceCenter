﻿using ServiceCenter.Models;
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
            

            if (objUserLoginModel != null && !string.IsNullOrEmpty(objUserLoginModel.UserName) && !string.IsNullOrEmpty(objUserLoginModel.Password))
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

            User UserSesionDetail = SessionService.GetUserSessionValues();

            if(UserSesionDetail != null)
            {
                UserService objUserService = new UserService();

                objUserService.RegisterLogoutDetail(UserSesionDetail.LoginSessionId);
            }

            SessionService.DestroyUserSession();

            return View("Index");
        }
    }
}