using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceCenter.Models;
using ServiceCenter.Services;

namespace ServiceCenter.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (!IsSessionValid())
                return RedirectToAction("Index", "Login");

            User UserSesionDetail = SessionService.GetUserSessionValues();

            if(UserSesionDetail.Role != (int)UserRole.MasterAdmin)
                return RedirectToAction("Dashboard", "Job");

            return View();
        }


        public ActionResult UserAddEdit()
        {
            if (!IsSessionValid())
                return RedirectToAction("Index", "Login");

            int UserId = !string.IsNullOrEmpty(Request["UserId"]) ? Convert.ToInt32(Request["UserId"]) : 0;

            User UserSesionDetail = SessionService.GetUserSessionValues();

            if (UserSesionDetail.Role == (int)UserRole.MasterAdmin || UserSesionDetail.id == UserId)
            {
                User objUser = new User();

                UserService objUserService = new UserService();
                objUser = objUserService.GetUserDetailById(UserId);
                ViewBag.UserRoleDD = UserRoleDD(objUser.Role.ToString());
                return View(objUser);
            }
            else if(UserSesionDetail.Role == (int)UserRole.MasterAdmin && UserId == 0)
            {
                ViewBag.UserRoleDD = UserRoleDD("2");
                return View(new User());
            }
            else
            {
                return RedirectToAction("Dashboard", "Job");
            }
            
        }

        [HttpPost]
        public ActionResult UserAddEdit(User objUser)
        {
            string ErrorMsg = string.Empty;

            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                ErrorMsg = message;

            }

            ResponceModel objResponce = new ResponceModel();

            UserService objUserService = new UserService();
            objResponce = objUserService.AddUpdateUser(objUser);

            if (objResponce.Responce == false)
            {
                ViewBag.UserRoleDD = UserRoleDD();

                ViewBag.ErrorMessage = objResponce.Message;

                return View(objUser);
            }
            else
            {
                ViewBag.SuccessMessage = objResponce.Message;

                return RedirectToAction("Index", "User");
            }


        }

        #region DropDwon

        public SelectList UserRoleDD(string SelectedValue = "")
        {
            return new SelectList(Enum.GetValues(typeof(UserRole)).OfType<Enum>()
            .Select(x => new SelectListItem
            {
                Text = Enum.GetName(typeof(UserRole), x),
                Value = (Convert.ToInt32(x)).ToString(),
                Selected = SelectedValue == (Convert.ToInt32(x)).ToString() ? true : false
            }), "Value", "Text");
        }

        #endregion

        #region Ajax Methods


        [HttpPost]
        public JsonResult GetUserList()
        {
            try
            {
                var draw = Convert.ToInt32(Request.Form["draw"]);
                var order = Convert.ToInt32(Request.Form["order[0][column]"]);
                var orderDir = Request.Form["order[0][dir]"];
                var startRec = Convert.ToInt32(Request.Form["start"]);
                var pageSize = Convert.ToInt32(Request.Form["length"]);

                string strUserName = string.Empty, strMobileNo = String.Empty;

                if (!string.IsNullOrEmpty(Request.Form["UserName"]))
                    strUserName = Convert.ToString(Request.Form["UserName"]).Trim();

                if (!string.IsNullOrEmpty(Request.Form["MobileNo"]))
                    strMobileNo = Convert.ToString(Request.Form["MobileNo"]).Trim();

                UserListDataModel objUserListDataModel = new UserListDataModel();

                UserService objUserService = new UserService();
                objUserListDataModel = objUserService.GetUserList(order, orderDir.ToUpper(), startRec, pageSize, strUserName, strMobileNo);

                return Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = objUserListDataModel.RecordCount,
                    recordsFiltered = objUserListDataModel.RecordCount,
                    data = objUserListDataModel.UserList
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new
                {
                    draw = Convert.ToInt32(0),
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = string.Empty
                });
            }

        }

        #endregion

        #region Common Method

        public bool IsSessionValid()
        {
            if (Session["User"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}