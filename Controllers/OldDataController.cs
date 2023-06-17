using ServiceCenter.Models;
using ServiceCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceCenter.Controllers
{
    public class OldDataController : Controller
    {
        private OldDataService objOldDataService;

        public ActionResult JobList()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            return View();
        }

        [HttpPost]
        public JsonResult GetJobList()
        {
            try
            {
                var draw = Convert.ToInt32(Request.Form["draw"]);
                var order = Convert.ToInt32(Request.Form["order[0][column]"]);
                var orderDir = Request.Form["order[0][dir]"];
                var startRec = Convert.ToInt32(Request.Form["start"]);
                var pageSize = Convert.ToInt32(Request.Form["length"]);

                string MobileNo = string.Empty, CustomerName = string.Empty, Technician = string.Empty, TechnicianType = string.Empty, JobNo = string.Empty, ItemName = string.Empty, CompComplaintNo = string.Empty;
                string UserName = string.Empty, SrNo = string.Empty, ItemKeyword = string.Empty, Pincode = string.Empty, FaultType = string.Empty, FaultDesc = string.Empty, Area = string.Empty;
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

                if (!string.IsNullOrEmpty(Request.Form["Pincode"]))
                    Pincode = Convert.ToString(Request.Form["Pincode"]).Trim();

                CallAttn = !string.IsNullOrEmpty(Request.Form["CallAttn"]) ? Convert.ToBoolean(Request.Form["CallAttn"]) : (bool?)null;

                JobDone = !string.IsNullOrEmpty(Request.Form["JobDone"]) ? Convert.ToBoolean(Request.Form["JobDone"]) : (bool?)null;

                GoAfterCall = !string.IsNullOrEmpty(Request.Form["GoAfterCall"]) ? Convert.ToBoolean(Request.Form["GoAfterCall"]) : (bool?)null;

                if (!string.IsNullOrEmpty(Request.Form["JobNo"]))
                    JobNo = Convert.ToString(Request.Form["JobNo"]).Trim();

                if (!string.IsNullOrEmpty(Request.Form["SrNo"]))
                    SrNo = Convert.ToString(Request.Form["SrNo"]).Trim();

                if (!string.IsNullOrEmpty(Request.Form["CompComplaintNo"]))
                    CompComplaintNo = Convert.ToString(Request.Form["CompComplaintNo"]).Trim();


                IsCompComplaintNo = !string.IsNullOrEmpty(Request.Form["IsCompComplaintNo"]) ? Convert.ToBoolean(Request.Form["IsCompComplaintNo"]) : (bool?)null;

                if (!string.IsNullOrEmpty(Request.Form["ItemName"]))
                    ItemName = Convert.ToString(Request.Form["ItemName"]).Trim();

                if (!string.IsNullOrEmpty(Request.Form["ItemKeyword"]))
                    ItemKeyword = Convert.ToString(Request.Form["ItemKeyword"]).Trim();

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

                if (!string.IsNullOrEmpty(Request.Form["FaultType"]))
                    FaultType = Convert.ToString(Request.Form["FaultType"]).Trim();

                if (!string.IsNullOrEmpty(Request.Form["FaultDesc"]))
                    FaultDesc = Convert.ToString(Request.Form["FaultDesc"]).Trim();


                if (!string.IsNullOrEmpty(Request.Form["CallCategory"]))
                    intCallCategory = Convert.ToInt32(Request.Form["CallCategory"]);

                if (!string.IsNullOrEmpty(Request.Form["UserName"]))
                    UserName = Convert.ToString(Request.Form["UserName"]).Trim();

                if (!string.IsNullOrEmpty(Request.Form["Area"]))
                    Area = Convert.ToString(Request.Form["Area"]).Trim();

                ModifyFromDate = !string.IsNullOrEmpty(Request.Form["ModifyFromDate"]) ? DateTime.ParseExact(Request.Form["ModifyFromDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

                ModifyToDate = !string.IsNullOrEmpty(Request.Form["ModifyToDate"]) ? DateTime.ParseExact(Request.Form["ModifyToDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();

                objOldDataService = new OldDataService();
                objCallRegistrationListDataModel = objOldDataService.GetCallRegisterListBySP(order, orderDir.ToUpper(), startRec, pageSize, CustomerName, CallType, ServType, Technician, TechnicianType, MobileNo, Pincode, CallAttn, JobDone, JobNo, SrNo, CompComplaintNo, ItemName, ItemKeyword, Deliver, Canceled, PartPanding, IsCompComplaintNo, FromDate, ToDate, CallAssignFromDate, CallAssignToDate, CallBack, WorkShopIN, PaymentPanding, GoAfterCall, RepeatFromTech, UserName, FaultType, FaultDesc, Area, ModifyFromDate, ModifyToDate);



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


        #region Extra
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

        #region Select2 Dropdown

        public JsonResult GetItemList(string match, int page = 1, int pageSize = 5)
        {
            List<Select2> lstDropDownItem = new List<Select2>();

            objOldDataService = new OldDataService();
            lstDropDownItem = objOldDataService.GetItemList(match);

            ResultList<Select2> results = new ResultList<Select2>
            {
                items = lstDropDownItem,
                total_count = lstDropDownItem.Count,
            };

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAreaList(string match, int page = 1, int pageSize = 5)
        {
            List<Select2> lstDropDownItem = new List<Select2>();

            objOldDataService = new OldDataService();
            lstDropDownItem = objOldDataService.GetAreaList(match);

            ResultList<Select2> results = new ResultList<Select2>
            {
                items = lstDropDownItem,
                total_count = lstDropDownItem.Count,
            };

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompanyList(string match, int page = 1, int pageSize = 5)
        {
            List<Select2> lstDropDownItem = new List<Select2>();

            objOldDataService = new OldDataService();
            lstDropDownItem = objOldDataService.GetCompanyList(match);

            ResultList<Select2> results = new ResultList<Select2>
            {
                items = lstDropDownItem,
                total_count = lstDropDownItem.Count,
            };

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBrandList(string match, int page = 1, int pageSize = 5)
        {
            List<Select2> lstDropDownItem = new List<Select2>();

            objOldDataService = new OldDataService();
            lstDropDownItem = objOldDataService.GetBrandList(match);

            ResultList<Select2> results = new ResultList<Select2>
            {
                items = lstDropDownItem,
                total_count = lstDropDownItem.Count,
            };

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}