﻿using ServiceCenter.Services;
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
                return RedirectToAction("Logout", "Login");

            JobDashboard objJobDashboard = new JobDashboard();

            JobService objJobService = new JobService();
            objJobDashboard = objJobService.GetDashboardData();

            return View(objJobDashboard);
        }

        public ActionResult JobList()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            if (!CommonService.CheckForRightsByPageNameAndUserId("JOBALL", PageRightsEnum.List))
                return RedirectToAction("AccessDenied", "Home");


            return View();
        }

        public ActionResult ServiceCallRegistation()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            if (!CommonService.CheckForRightsByPageNameAndUserId("JOBALL", PageRightsEnum.Add))
                return RedirectToAction("AccessDenied", "Home");


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
                string strPaymentBy = Convert.ToString(callRegistration.PaymentBy);


                callRegistration.ServiceTypeDD = ServiceTypeDD(strServType);
                callRegistration.CallTypeDD = CallTypeDD(strCallType);
                callRegistration.AreaDD = AreaDD(strAreaId);
                callRegistration.PaymentByDD = PaymentByDD(strPaymentBy);

            }
            else
            {
                callRegistration = new CallRegistration();

                callRegistration.ServiceTypeDD = ServiceTypeDD();
                callRegistration.CallTypeDD = CallTypeDD();
                callRegistration.AreaDD = AreaDD();
                callRegistration.PaymentByDD = PaymentByDD();
            }

            return View(callRegistration);
        }

        [HttpPost]
        public ActionResult ServiceCallRegistation(CallRegistration callRegistration)
        {

            CommonService.WriteTraceLog("JobController_ServiceCallRegistation -> callRegistration.CallAssignDate : " + callRegistration.CallAssignDate);

            string ErrorMsg = string.Empty;

            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                ErrorMsg = message;

            }

            CallRegistration objCallRegistrationResponce = new CallRegistration();

            JobService objJobService = new JobService();
            objCallRegistrationResponce = objJobService.InsertUpdateCallRegistration(callRegistration);

            return Json(new { data = objCallRegistrationResponce });
            //return RedirectToAction("JobList", "Job");
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

                string MobileNo = string.Empty, CustomerName = string.Empty, Technician = string.Empty, TechnicianType = string.Empty, JobNo = string.Empty, ItemName = string.Empty, CompComplaintNo = string.Empty, UserName = string.Empty;
                int intCallCategory = 0;
                int? CallType, ServType;
                bool? CallAttn, JobDone, Deliver, Canceled, PartPanding, IsCompComplaintNo, CallBack, WorkShopIN, PaymentPanding, GoAfterCall, RepeatFromTech;
                DateTime? FromDate, ToDate, CallAssignFromDate, CallAssignToDate, ModifyFromDate, ModifyToDate;


                if (!string.IsNullOrEmpty(Request.Form["CustomerName"]))
                    CustomerName = Convert.ToString(Request.Form["CustomerName"]).Trim();

                CallType = !string.IsNullOrEmpty(Request.Form["CallType"]) ? Convert.ToInt32(Request.Form["CallType"]) : (int?)null;

                ServType = !string.IsNullOrEmpty(Request.Form["ServType"]) ? Convert.ToInt32(Request.Form["ServType"]) : (int?)null;

                if (!string.IsNullOrEmpty(Request.Form["Technician"]))
                    Technician = Convert.ToString(Request.Form["Technician"]).Trim();

                if (!string.IsNullOrEmpty(Request.Form["TechnicianType"]))
                    TechnicianType = Convert.ToString(Request.Form["TechnicianType"]).Trim();

                if (!string.IsNullOrEmpty(Request.Form["MobileNo"]))
                    MobileNo = Convert.ToString(Request.Form["MobileNo"]).Trim();

                CallAttn = !string.IsNullOrEmpty(Request.Form["CallAttn"]) ? Convert.ToBoolean(Request.Form["CallAttn"]) : (bool?)null;

                JobDone = !string.IsNullOrEmpty(Request.Form["JobDone"]) ? Convert.ToBoolean(Request.Form["JobDone"]) : (bool?)null;

                GoAfterCall = !string.IsNullOrEmpty(Request.Form["GoAfterCall"]) ? Convert.ToBoolean(Request.Form["GoAfterCall"]) : (bool?)null;

                if (!string.IsNullOrEmpty(Request.Form["JobNo"]))
                    JobNo = Convert.ToString(Request.Form["JobNo"]).Trim();

                if (!string.IsNullOrEmpty(Request.Form["CompComplaintNo"]))
                    CompComplaintNo = Convert.ToString(Request.Form["CompComplaintNo"]).Trim();


                IsCompComplaintNo = !string.IsNullOrEmpty(Request.Form["IsCompComplaintNo"]) ? Convert.ToBoolean(Request.Form["IsCompComplaintNo"]) : (bool?)null;

                if (!string.IsNullOrEmpty(Request.Form["ItemName"]))
                    ItemName = Convert.ToString(Request.Form["ItemName"]).Trim();

                Deliver = !string.IsNullOrEmpty(Request.Form["Deliver"]) ? Convert.ToBoolean(Request.Form["Deliver"]) : (bool?)null;

                Canceled = !string.IsNullOrEmpty(Request.Form["Canceled"]) ? Convert.ToBoolean(Request.Form["Canceled"]) : (bool?)null;

                FromDate = !string.IsNullOrEmpty(Request.Form["FromDate"]) ? DateTime.ParseExact(Request.Form["FromDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

                ToDate = !string.IsNullOrEmpty(Request.Form["ToDate"]) ? DateTime.ParseExact(Request.Form["ToDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;


                PartPanding = !string.IsNullOrEmpty(Request.Form["PartPanding"]) ? Convert.ToBoolean(Request.Form["PartPanding"]) : (bool?)null;

                RepeatFromTech = !string.IsNullOrEmpty(Request.Form["RepeatFromTech"]) ? Convert.ToBoolean(Request.Form["RepeatFromTech"]) : (bool?)null;

                CallAssignFromDate = !string.IsNullOrEmpty(Request.Form["CallAssignFromDate"]) ? DateTime.ParseExact(Request.Form["CallAssignFromDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

                CallAssignToDate = !string.IsNullOrEmpty(Request.Form["CallAssignToDate"]) ? DateTime.ParseExact(Request.Form["CallAssignToDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;


                CallBack = !string.IsNullOrEmpty(Request.Form["CallBack"]) ? Convert.ToBoolean(Request.Form["CallBack"]) : (bool?)null;

                WorkShopIN = !string.IsNullOrEmpty(Request.Form["WorkShopIN"]) ? Convert.ToBoolean(Request.Form["WorkShopIN"]) : (bool?)null;

                PaymentPanding = !string.IsNullOrEmpty(Request.Form["PaymentPanding"]) ? Convert.ToBoolean(Request.Form["PaymentPanding"]) : (bool?)null;


                if (!string.IsNullOrEmpty(Request.Form["CallCategory"]))
                    intCallCategory = Convert.ToInt32(Request.Form["CallCategory"]);

                if (!string.IsNullOrEmpty(Request.Form["UserName"]))
                    UserName = Convert.ToString(Request.Form["UserName"]).Trim();

                ModifyFromDate = !string.IsNullOrEmpty(Request.Form["ModifyFromDate"]) ? DateTime.ParseExact(Request.Form["ModifyFromDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

                ModifyToDate = !string.IsNullOrEmpty(Request.Form["ModifyToDate"]) ? DateTime.ParseExact(Request.Form["ModifyToDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();

                JobService objJobService = new JobService();
                objCallRegistrationListDataModel = objJobService.GetCallRegisterListBySP(order, orderDir.ToUpper(), startRec, pageSize, CustomerName, CallType, ServType, Technician, TechnicianType, MobileNo, CallAttn, JobDone, JobNo, CompComplaintNo, ItemName, Deliver, Canceled, PartPanding, IsCompComplaintNo, FromDate, ToDate, CallAssignFromDate, CallAssignToDate, CallBack, WorkShopIN, PaymentPanding, GoAfterCall, RepeatFromTech, UserName, ModifyFromDate, ModifyToDate, intCallCategory);



                return Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = objCallRegistrationListDataModel.RecordCount,
                    recordsFiltered = objCallRegistrationListDataModel.RecordCount,
                    data = objCallRegistrationListDataModel.CallRegistrationList,
                    Oids = objCallRegistrationListDataModel.Oids
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
                    data = string.Empty,
                    Oids = string.Empty
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

        [HttpPost]
        public JsonResult UpdateCompComplaintNoByCallId(string CompComplaintNo, string CallId)
        {
            ResponceModel objResponceModel = new ResponceModel();

            JobService objJobService = new JobService();
            objResponceModel = objJobService.UpdateCompComplaintNoByCallId(CompComplaintNo, CallId);

            return Json(new { data = objResponceModel });
        }

        [HttpPost]
        public JsonResult UpdateJobDoneRegionByCallId(string JobDoneRegion, string CallId)
        {
            ResponceModel objResponceModel = new ResponceModel();

            JobService objJobService = new JobService();
            objResponceModel = objJobService.UpdateJobDoneRegionByCallId(JobDoneRegion, CallId);

            return Json(new { data = objResponceModel });
        }

        [HttpPost]
        public JsonResult UpdateJobRegionByCallId(string JobDoneRegion, string CallId)
        {
            ResponceModel objResponceModel = new ResponceModel();

            JobService objJobService = new JobService();
            objResponceModel = objJobService.UpdateJobRegionByCallId(JobDoneRegion, CallId);

            return Json(new { data = objResponceModel });
        }

        [HttpPost]
        public JsonResult GetBillDetailsByBillNo(string BillNo, string BillDate)
        {
            BillDetailsDataModel objBillDetailsDataModel = new BillDetailsDataModel();

            JobService objJobService = new JobService();
            objBillDetailsDataModel = objJobService.GetBillDetailsByBillNo(BillNo, BillDate);

            return Json(new { data = objBillDetailsDataModel });
        }

        [HttpPost]
        public JsonResult CreateNewCallFromBillDetails(BillDetails objBillDetails)
        {
            CommonService.WriteTraceLog("Controller_CreateNewCallFromBillDetails -> objBillDetails.InvoiceDate : " + objBillDetails.InvoiceDate);

            ResponceModel objResponceModel = new ResponceModel();

            if (objBillDetails != null && !string.IsNullOrEmpty(objBillDetails.CustomerName) && !string.IsNullOrEmpty(objBillDetails.MobileNo) && !string.IsNullOrEmpty(objBillDetails.Address) && !string.IsNullOrEmpty(objBillDetails.BillNo))
            {
                JobService objJobService = new JobService();
                objResponceModel = objJobService.CreateNewCallFromBillDetails(objBillDetails);
            }
            else
            {
                objResponceModel = new ResponceModel() { Responce = false, Message = "Somthing went wrong, Data is missing " };

            }

            return Json(new { data = objResponceModel });
        }

        [HttpPost]
        public JsonResult CreateNewCallFromOldJonNo(string OldJobNo)
        {
            ResponceModel objResponceModel = new ResponceModel();

            if (!string.IsNullOrEmpty(OldJobNo))
            {
                JobService objJobService = new JobService();
                objResponceModel = objJobService.CreateNewCallFromOldJonNo(OldJobNo);
            }
            else
            {
                objResponceModel = new ResponceModel() { Responce = false, Message = "Somthing went wrong, Data is missing " };

            }

            return Json(new { data = objResponceModel });
        }


        [HttpPost]
        public JsonResult AssignMultipleCallToTechnician(string CallIds, string TechnicianId)
        {

            ResponceModel objResponceModel = new ResponceModel();

            if (!string.IsNullOrEmpty(CallIds))
            {
                JobService objJobService = new JobService();
                objResponceModel = objJobService.AssignMultipleCallToTechnician(CallIds, TechnicianId);
            }
            else
            {
                objResponceModel = new ResponceModel() { Responce = false, Message = "Somthing went wrong, Data is missing " };

            }

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

        public JsonResult GetTechnicianTypeList(string match, int page = 1, int pageSize = 5)
        {
            List<Select2> lstTechnicianType = new List<Select2>();

            JobService objJobService = new JobService();
            lstTechnicianType = objJobService.GetTechnicianTypeList(match);

            ResultList<Select2> results = new ResultList<Select2>
            {
                items = lstTechnicianType,
                total_count = lstTechnicianType.Count,
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

        public JsonResult GetUserList(string match, int page = 1, int pageSize = 5)
        {
            List<Select2> lstUser = new List<Select2>();

            JobService objJobService = new JobService();
            lstUser = objJobService.GetUserList(match);

            ResultList<Select2> results = new ResultList<Select2>
            {
                items = lstUser,
                total_count = lstUser.Count,
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
            ListItem.Add(new SelectListItem { Text = "Installation", Value = "2", Selected = SelectedValue == "2" ? true : false });

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

        public SelectList PaymentByDD(string SelectedValue = "")
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();

            ListItem.Add(new SelectListItem { Text = "Cash", Value = "0", Selected = SelectedValue == "0" ? true : false });
            ListItem.Add(new SelectListItem { Text = "Check", Value = "1", Selected = SelectedValue == "1" ? true : false });
            ListItem.Add(new SelectListItem { Text = "Online", Value = "2", Selected = SelectedValue == "2" ? true : false });

            return new SelectList(ListItem, "Value", "Text");
        }


        //

        #endregion


    }
}