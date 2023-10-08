using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Hangfire;
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

            if (UserSesionDetail.Role != (int)UserRole.MasterAdmin)
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
            else if (UserSesionDetail.Role == (int)UserRole.MasterAdmin && UserId == 0)
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

        public ActionResult UserPagesRights()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            return View();
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


        [HttpPost]
        public JsonResult GetUserPagesRightsList()
        {
            try
            {
                int UserId = 0;

                if (!string.IsNullOrEmpty(Request.Form["UserId"]))
                    UserId = Convert.ToInt32(Request.Form["UserId"]);

                User UserSesionDetail = SessionService.GetUserSessionValues();

                UserService objUserService = new UserService();

                UserPageRightsList objUserPageRightsList = objUserService.GetUserPagesRightsListByUserId(UserId);

                return Json(new
                {
                    UserPageRightsList = objUserPageRightsList.lstUserPageRights,
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new
                {
                    Message = "Somthing went wrong",
                    Responce = false,
                    UserPageRightsList = string.Empty
                });
            }

        }

        [HttpPost]
        public JsonResult UpdateUserPageRightsByPageCodeAndUserId(string PageCode, int UserId, bool CheckboxValue, string CheckboxType)
        {
            ResponceModel objResponceModel = new ResponceModel();


            UserService objUserService = new UserService();
            objResponceModel = objUserService.UpdateUserPageRightsByPageCodeAndUserId(PageCode, UserId, CheckboxValue, CheckboxType);

            return Json(new { data = objResponceModel });
        }

        [HttpPost]
        public JsonResult UpdateAllUserPageRightsByUserId(int UserId, bool CheckboxValue, string CheckboxType)
        {
            ResponceModel objResponceModel = new ResponceModel();


            UserService objUserService = new UserService();
            objResponceModel = objUserService.UpdateAllUserPageRightsByUserId(UserId, CheckboxValue, CheckboxType);

            return Json(new { data = objResponceModel });
        }

        public JsonResult GetUserList(string match, int page = 1, int pageSize = 5)
        {
            List<Select2> lstUser = new List<Select2>();

            UserService objUserService = new UserService();
            lstUser = objUserService.GetUserList(match);

            ResultList<Select2> results = new ResultList<Select2>
            {
                items = lstUser,
                total_count = lstUser.Count,
            };

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateUserStatusById(int UserId, bool Status)
        {
            ResponceModel objResponceModel = new ResponceModel();

            UserService objUserService = new UserService();
            objResponceModel = objUserService.UpdateUserStatusById(UserId, Status);

            return Json(new { data = objResponceModel });
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

        public ActionResult SendTestMail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendTestMail(string ToEmail)
        {
            EmailService objEmailSerice = new EmailService();
            objEmailSerice.Sendmail("dsagar159@gmail.com", "Test Mail", "Test subject", "");

            return View();
        }



    }
}