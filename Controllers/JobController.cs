using ServiceCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceCenter.Models;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;

namespace ServiceCenter.Controllers
{
    public class JobController : Controller
    {
        #region Action Method
        public ActionResult Dashboard()
        {
            if (!IsSessionValid())
                return RedirectToAction("Index", "Login");

            JobDashboard objJobDashboard = new JobDashboard();

            JobService objJobService = new JobService();
            objJobDashboard = objJobService.GetDashboardData();

            return View(objJobDashboard);
        }

        public ActionResult JobList()
        {
            if (!IsSessionValid())
                return RedirectToAction("Index", "Login");

            /*
            List<CallRegistration> lstCallRegistration = new List<CallRegistration>();

            JobService objJobService = new JobService();
            //lstCallRegistration = objJobService.GetCallRegisterList();

            CallRegistrationListDataModel objCallRegistrationListDataModel = objJobService.GetCallRegisterListBySP(1, "ASC", 1, 20, "a");
            lstCallRegistration = objCallRegistrationListDataModel.CallRegistrationList;

            return View(lstCallRegistration);
            */

            return View();
        }

        public ActionResult ServiceCallRegistation()
        {
            if (!IsSessionValid())
                return RedirectToAction("Index", "Login");

            string CallId = Request["CallId"];

            CallRegistration callRegistration;

            if (!string.IsNullOrEmpty(CallId))
            {
                callRegistration = new CallRegistration();

                JobService objJobService = new JobService();
                callRegistration = objJobService.GetCallRegistrationDetailByOid(CallId);

                string strServType = Convert.ToString(callRegistration.ServType);
                string strCallType = Convert.ToString(callRegistration.CallType);
                string strTechnicianId = Convert.ToString(callRegistration.Technician);
                string strAreaId = Convert.ToString(callRegistration.Area);
                string strFaultTypeId = Convert.ToString(callRegistration.FaultType);


                callRegistration.ServiceTypeDD = ServiceTypeDD(strServType);
                callRegistration.CallTypeDD = CallTypeDD(strCallType);
                callRegistration.AreaDD = AreaDD(strAreaId);

            }
            else
            {
                callRegistration = new CallRegistration();

                callRegistration.ServiceTypeDD = ServiceTypeDD();
                callRegistration.CallTypeDD = CallTypeDD();
                callRegistration.AreaDD = AreaDD();
            }

            return View(callRegistration);
        }

        [HttpPost]
        public ActionResult ServiceCallRegistation(CallRegistration callRegistration)
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

            JobService objJobService = new JobService();
            objResponce = objJobService.InsertUpdateCallRegistration(callRegistration);

            //return Json(new { data = objResponce });
            return RedirectToAction("JobList", "Job");
        }

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

        #region Ajax Methods
        public JsonResult GetJobList()
        {
            List<CallRegistration> lstCallRegistration = new List<CallRegistration>();

            JobService objJobService = new JobService();
            lstCallRegistration = objJobService.GetCallRegisterList();

            return Json(lstCallRegistration, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCustomerDetailsByCustomerId(string CustomerId)
        {
            CustomerMaster objCustomerMaster = new CustomerMaster();

            JobService objJobService = new JobService();
            objCustomerMaster = objJobService.GetCustomerDetailsByCustomerId(CustomerId);

            return Json(objCustomerMaster, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetLatestIdForJobNo()
        {
            int LatestJobIdNo = 0;

            JobService objJobService = new JobService();
            LatestJobIdNo = objJobService.GetLatestIdForJobNo();

            return Json(new { JobIdNo = LatestJobIdNo }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetJobList1()
        {
            try
            {
                var draw = Convert.ToInt32(Request.Form["draw"]);
                var order = Convert.ToInt32(Request.Form["order[0][column]"]);
                var orderDir = Request.Form["order[0][dir]"];
                var startRec = Convert.ToInt32(Request.Form["start"]);
                var pageSize = Convert.ToInt32(Request.Form["length"]);

                string strKeyword = string.Empty;
                int intCallCategory = 0;

                if (!string.IsNullOrEmpty(Request.Form["Keyword"]))
                    strKeyword = Convert.ToString(Request.Form["Keyword"]).Trim();

                if (!string.IsNullOrEmpty(Request.Form["CallCategory"]))
                    intCallCategory = Convert.ToInt32(Request.Form["CallCategory"]);

                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();

                JobService objJobService = new JobService();
                objCallRegistrationListDataModel = objJobService.GetCallRegisterListBySP(order, orderDir.ToUpper(), startRec, pageSize, strKeyword, intCallCategory);

                
                return Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = objCallRegistrationListDataModel.RecordCount,
                    recordsFiltered = objCallRegistrationListDataModel.RecordCount,
                    data = objCallRegistrationListDataModel.CallRegistrationList
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
        
        /*
        public ActionResult GetJobList1(JqueryDatatableParam param)
        {
            JsonResult result;
            try
            {
                var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
                var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];

                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();

                string strSearch = !string.IsNullOrEmpty(param.sSearch) ? param.sSearch : "";

                JobService objJobService = new JobService();
                objCallRegistrationListDataModel = objJobService.GetCallRegisterListBySP(sortColumnIndex, sortDirection, param.iDisplayStart, param.iDisplayLength, strSearch, 0);

                var displayResult = objCallRegistrationListDataModel.CallRegistrationList;
           
                var totalRecords = objCallRegistrationListDataModel.CallRegistrationList.Count;

                result = Json(new
                {
                    param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = displayResult
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
                result = Json(new { }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        */

        [HttpPost]
        public JsonResult AddNewCustomer(CustomerMaster ObjCustomerMaster)
        {
            CustomerMaster objCustomerMasterResponce = new CustomerMaster();

            JobService objJobService = new JobService();
            objCustomerMasterResponce = objJobService.InsertNewCustomer(ObjCustomerMaster);

            return Json(new { data = objCustomerMasterResponce });
        }

        [HttpPost]
        public JsonResult UpdateCheckboxValueByType(string CheckboxType, string CallId, bool CheckoxValue)
        {
            ResponceModel objResponceModel = new ResponceModel();


            JobService objJobService = new JobService();
            objResponceModel = objJobService.UpdateCheckboxValueByType(CheckboxType, CallId, CheckoxValue);

            return Json(new { data = objResponceModel });
        }

        

        #endregion

        #region Select2

        public JsonResult GetCustomerCodeList(string match, int page = 1, int pageSize = 5)
        {
            List<Select2> lstCustomerCode = new List<Select2>();

            JobService objJobService = new JobService();
            lstCustomerCode = objJobService.GetCustomerCodeList(match);

            ResultList<Select2> results = new ResultList<Select2>
            {
                items = lstCustomerCode,
                total_count = lstCustomerCode.Count,
            };

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetItemList(string match, int page = 1, int pageSize = 5)
        {
            List<Select2> lstCustomerCode = new List<Select2>();

            JobService objJobService = new JobService();
            lstCustomerCode = objJobService.GetItemList(match);

            ResultList<Select2> results = new ResultList<Select2>
            {
                items = lstCustomerCode,
                total_count = lstCustomerCode.Count,
            };

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCityList(string match, int page = 1, int pageSize = 5)
        {
            List<Select2> lstcity = new List<Select2>();

            JobService objJobService = new JobService();
            lstcity = objJobService.GetCityList(match);

            ResultList<Select2> results = new ResultList<Select2>
            {
                items = lstcity,
                total_count = lstcity.Count,
            };

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTechnicianList(string match, int page = 1, int pageSize = 5)
        {
            List<Select2> lstTechnician = new List<Select2>();

            JobService objJobService = new JobService();
            lstTechnician = objJobService.GetTechnicianList(match);

            ResultList<Select2> results = new ResultList<Select2>
            {
                items = lstTechnician,
                total_count = lstTechnician.Count,
            };

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFaultTypeList(string match, int page = 1, int pageSize = 5)
        {
            List<Select2> lstFaultType = new List<Select2>();

            JobService objJobService = new JobService();
            lstFaultType = objJobService.GetFaultTypeList(match);

            ResultList<Select2> results = new ResultList<Select2>
            {
                items = lstFaultType,
                total_count = lstFaultType.Count,
            };

            return Json(results, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Dropdwon Binding
        public SelectList ServiceTypeDD(string SelectedValue = "")
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();

            ListItem.Add(new SelectListItem { Text = "In-Warranty", Value = "0", Selected = SelectedValue == "0" ? true : false });
            ListItem.Add(new SelectListItem { Text = "Out-Warranty", Value = "1", Selected = SelectedValue == "1" ? true : false });
            ListItem.Add(new SelectListItem { Text = "Installation ", Value = "2", Selected = SelectedValue == "2" ? true : false });

            return new SelectList(ListItem, "Value", "Text");
        }

        public SelectList CallTypeDD(string SelectedValue = "")
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();

            ListItem.Add(new SelectListItem { Text = "Local", Value = "0", Selected = SelectedValue == "0" ? true : false });
            ListItem.Add(new SelectListItem { Text = "Workshop", Value = "1", Selected = SelectedValue == "1" ? true : false });
            ListItem.Add(new SelectListItem { Text = "Out-Station", Value = "2", Selected = SelectedValue == "2" ? true : false });

            return new SelectList(ListItem, "Value", "Text");
        }
      
        public SelectList AreaDD(string SelectedValue = "")
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();

            JobService ObjJobService = new JobService();
            ListItem = ObjJobService.GetAreaList(SelectedValue);

            return new SelectList(ListItem, "Value", "Text");
        }


        //

        #endregion

        
    }
}