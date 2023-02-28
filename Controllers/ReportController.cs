using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Ocsp;
using ServiceCenter.Models;
using ServiceCenter.Services;
using static System.Net.WebRequestMethods;

namespace ServiceCenter.Controllers
{
    public class ReportController : Controller
    {
        #region Action Method
        public ActionResult CallRegistrationReport()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            if (!CommonService.CheckForRightsByPageNameAndUserId("RPTCRE", PageRightsEnum.List))
                return RedirectToAction("AccessDenied", "Home");

            return View();
        }

        public ActionResult DailyCompanyReport()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            if (!CommonService.CheckForRightsByPageNameAndUserId("RPTCRE", PageRightsEnum.Add))
                return RedirectToAction("AccessDenied", "Home");

            return View();
        }

        public ActionResult DailyMailReportStatus()
        {
            return View();
        }

        public ActionResult DownloadCompanyReport(string FileName)
        {
            string ReportFolderPath = ConfigurationManager.AppSettings["CompanyReportPath"].ToString();

            string FilePath = Path.Combine(ReportFolderPath, FileName);

            return File(FilePath, "application/ms-excel", FileName);
        }

        public ActionResult PendingCallReport()
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.PendingCallReport(null, null);

            return View(objCallRegistrationListDataModel);
        }

        [HttpPost]
        public ActionResult PendingCallReport(AnalyticalReportModel objAnalyticalReportModel)
        {
            DateTime? DtFromDate = (DateTime?)null, DtToDate = (DateTime?)null;

            DtFromDate = !string.IsNullOrEmpty(objAnalyticalReportModel.FromDate) ? DateTime.ParseExact(objAnalyticalReportModel.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            DtToDate = !string.IsNullOrEmpty(objAnalyticalReportModel.ToDate) ? DateTime.ParseExact(objAnalyticalReportModel.ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.PendingCallReport(DtFromDate, DtToDate);

            return View(objCallRegistrationListDataModel);
        }

        public ActionResult TechnicianCallReport()
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.TechnicianCallReportDaily(null, null);

            return View(objCallRegistrationListDataModel);
        }

        [HttpPost]
        public ActionResult TechnicianCallReport(AnalyticalReportModel objAnalyticalReportModel)
        {
            DateTime? DtFromDate = (DateTime?)null, DtToDate = (DateTime?)null;

            DtFromDate = !string.IsNullOrEmpty(objAnalyticalReportModel.FromDate) ? DateTime.ParseExact(objAnalyticalReportModel.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            DtToDate = !string.IsNullOrEmpty(objAnalyticalReportModel.ToDate) ? DateTime.ParseExact(objAnalyticalReportModel.ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.TechnicianCallReportDaily(DtFromDate, DtToDate);

            return View(objCallRegistrationListDataModel);
        }

        public ActionResult WorkshopInOutCallReport()
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.WorkshopInOutCallReportDaily(null, null);

            return View(objCallRegistrationListDataModel);
        }

        [HttpPost]
        public ActionResult WorkshopInOutCallReport(AnalyticalReportModel objAnalyticalReportModel)
        {
            DateTime? DtFromDate = (DateTime?)null, DtToDate = (DateTime?)null;

            DtFromDate = !string.IsNullOrEmpty(objAnalyticalReportModel.FromDate) ? DateTime.ParseExact(objAnalyticalReportModel.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            DtToDate = !string.IsNullOrEmpty(objAnalyticalReportModel.ToDate) ? DateTime.ParseExact(objAnalyticalReportModel.ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.WorkshopInOutCallReportDaily(DtFromDate, DtToDate);

            return View(objCallRegistrationListDataModel);
        }

        public ActionResult ServiceTypeReport()
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.ServiceTypeReportDaily(null, null);

            return View(objCallRegistrationListDataModel);
        }

        [HttpPost]
        public ActionResult ServiceTypeReport(AnalyticalReportModel objAnalyticalReportModel)
        {
            DateTime? DtFromDate = (DateTime?)null, DtToDate = (DateTime?)null;

            DtFromDate = !string.IsNullOrEmpty(objAnalyticalReportModel.FromDate) ? DateTime.ParseExact(objAnalyticalReportModel.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            DtToDate = !string.IsNullOrEmpty(objAnalyticalReportModel.ToDate) ? DateTime.ParseExact(objAnalyticalReportModel.ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.ServiceTypeReportDaily(DtFromDate, DtToDate);

            return View(objCallRegistrationListDataModel);
        }

        public ActionResult ServiceTypeReportDailyUserVise()
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.ServiceTypeReportDaily(null, null);

            return View(objCallRegistrationListDataModel);
        }

        [HttpPost]
        public ActionResult ServiceTypeReportDailyUserVise(AnalyticalReportModel objAnalyticalReportModel)
        {

            DateTime? DtFromDate = (DateTime?)null, DtToDate = (DateTime?)null;

            DtFromDate = !string.IsNullOrEmpty(objAnalyticalReportModel.FromDate) ? DateTime.ParseExact(objAnalyticalReportModel.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            DtToDate = !string.IsNullOrEmpty(objAnalyticalReportModel.ToDate) ? DateTime.ParseExact(objAnalyticalReportModel.ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.ServiceTypeReportDaily(DtFromDate, DtToDate);

            return View(objCallRegistrationListDataModel);
        }

        public ActionResult ServiceTypeReportDailyItemVise()
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.ServiceTypeReportDaily(null, null);

            return View(objCallRegistrationListDataModel);
        }

        [HttpPost]
        public ActionResult ServiceTypeReportDailyItemVise(AnalyticalReportModel objAnalyticalReportModel)
        {
            DateTime? DtFromDate = (DateTime?)null, DtToDate = (DateTime?)null;

            DtFromDate = !string.IsNullOrEmpty(objAnalyticalReportModel.FromDate) ? DateTime.ParseExact(objAnalyticalReportModel.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            DtToDate = !string.IsNullOrEmpty(objAnalyticalReportModel.ToDate) ? DateTime.ParseExact(objAnalyticalReportModel.ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.ServiceTypeReportDaily(DtFromDate, DtToDate);

            return View(objCallRegistrationListDataModel);
        }

        public ActionResult PartPendingCallReport()
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.PartPendingCallReport(null, null);

            return View(objCallRegistrationListDataModel);
        }

        [HttpPost]
        public ActionResult PartPendingCallReport(AnalyticalReportModel objAnalyticalReportModel)
        {
            DateTime? DtFromDate = (DateTime?)null, DtToDate = (DateTime?)null;

            DtFromDate = !string.IsNullOrEmpty(objAnalyticalReportModel.FromDate) ? DateTime.ParseExact(objAnalyticalReportModel.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            DtToDate = !string.IsNullOrEmpty(objAnalyticalReportModel.ToDate) ? DateTime.ParseExact(objAnalyticalReportModel.ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.PartPendingCallReport(DtFromDate, DtToDate);

            return View(objCallRegistrationListDataModel);
        }

        public ActionResult PaymentPendingReport()
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.PaymentPendingReportDaily(null, null);

            return View(objCallRegistrationListDataModel);
        }

        [HttpPost]
        public ActionResult PaymentPendingReport(AnalyticalReportModel objAnalyticalReportModel)
        {
            DateTime? DtFromDate = (DateTime?)null, DtToDate = (DateTime?)null;

            DtFromDate = !string.IsNullOrEmpty(objAnalyticalReportModel.FromDate) ? DateTime.ParseExact(objAnalyticalReportModel.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            DtToDate = !string.IsNullOrEmpty(objAnalyticalReportModel.ToDate) ? DateTime.ParseExact(objAnalyticalReportModel.ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.PaymentPendingReportDaily(DtFromDate, DtToDate);

            return View(objCallRegistrationListDataModel);
        }

        public ActionResult RepeatFromTechReport()
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.RepeatFromTechReportDaily(null, null);

            return View(objCallRegistrationListDataModel);
        }

        [HttpPost]
        public ActionResult RepeatFromTechReport(AnalyticalReportModel objAnalyticalReportModel)
        {
            DateTime? DtFromDate = (DateTime?)null, DtToDate = (DateTime?)null;

            DtFromDate = !string.IsNullOrEmpty(objAnalyticalReportModel.FromDate) ? DateTime.ParseExact(objAnalyticalReportModel.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            DtToDate = !string.IsNullOrEmpty(objAnalyticalReportModel.ToDate) ? DateTime.ParseExact(objAnalyticalReportModel.ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.RepeatFromTechReportDaily(DtFromDate, DtToDate);

            return View(objCallRegistrationListDataModel);
        }

        public ActionResult CancelCallReport()
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.CancelCallReportDaily(null, null);

            return View(objCallRegistrationListDataModel);
        }

        [HttpPost]
        public ActionResult CancelCallReport(AnalyticalReportModel objAnalyticalReportModel)
        {
            DateTime? DtFromDate = (DateTime?)null, DtToDate = (DateTime?)null;

            DtFromDate = !string.IsNullOrEmpty(objAnalyticalReportModel.FromDate) ? DateTime.ParseExact(objAnalyticalReportModel.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            DtToDate = !string.IsNullOrEmpty(objAnalyticalReportModel.ToDate) ? DateTime.ParseExact(objAnalyticalReportModel.ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.CancelCallReportDaily(DtFromDate, DtToDate);

            return View(objCallRegistrationListDataModel);
        }

        public ActionResult GoAfterCallReport()
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.GoAfterCallReportDaily(null, null);

            return View(objCallRegistrationListDataModel);
        }

        [HttpPost]
        public ActionResult GoAfterCallReport(AnalyticalReportModel objAnalyticalReportModel)
        {
            DateTime? DtFromDate = (DateTime?)null, DtToDate = (DateTime?)null;

            DtFromDate = !string.IsNullOrEmpty(objAnalyticalReportModel.FromDate) ? DateTime.ParseExact(objAnalyticalReportModel.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            DtToDate = !string.IsNullOrEmpty(objAnalyticalReportModel.ToDate) ? DateTime.ParseExact(objAnalyticalReportModel.ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.GoAfterCallReportDaily(DtFromDate, DtToDate);

            return View(objCallRegistrationListDataModel);
        }

        public ActionResult CollationReport()
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.CollationReportDaily(null, null);

            return View(objCallRegistrationListDataModel);
        }

        [HttpPost]
        public ActionResult CollationReport(AnalyticalReportModel objAnalyticalReportModel)
        {
            DateTime? DtFromDate = (DateTime?)null, DtToDate = (DateTime?)null;

            DtFromDate = !string.IsNullOrEmpty(objAnalyticalReportModel.FromDate) ? DateTime.ParseExact(objAnalyticalReportModel.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            DtToDate = !string.IsNullOrEmpty(objAnalyticalReportModel.ToDate) ? DateTime.ParseExact(objAnalyticalReportModel.ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.CollationReportDaily(DtFromDate, DtToDate);

            return View(objCallRegistrationListDataModel);
        }

        public ActionResult CallAssignToJobDoneTimingReport()
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.CallAssignToJobDoneTimingReportDaily(null, null);

            return View(objCallRegistrationListDataModel);
        }

        [HttpPost]
        public ActionResult CallAssignToJobDoneTimingReport(AnalyticalReportModel objAnalyticalReportModel)
        {
            DateTime? DtFromDate = (DateTime?)null, DtToDate = (DateTime?)null;

            DtFromDate = !string.IsNullOrEmpty(objAnalyticalReportModel.FromDate) ? DateTime.ParseExact(objAnalyticalReportModel.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            DtToDate = !string.IsNullOrEmpty(objAnalyticalReportModel.ToDate) ? DateTime.ParseExact(objAnalyticalReportModel.ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.CallAssignToJobDoneTimingReportDaily(DtFromDate, DtToDate);

            return View(objCallRegistrationListDataModel);
        }

        public ActionResult CallbackReceivedOrPendingReport()
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.CallbackReceivedOrPendingReportDaily(null, null);

            return View(objCallRegistrationListDataModel);
        }

        [HttpPost]
        public ActionResult CallbackReceivedOrPendingReport(AnalyticalReportModel objAnalyticalReportModel)
        {
            DateTime? DtFromDate = (DateTime?)null, DtToDate = (DateTime?)null;

            DtFromDate = !string.IsNullOrEmpty(objAnalyticalReportModel.FromDate) ? DateTime.ParseExact(objAnalyticalReportModel.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            DtToDate = !string.IsNullOrEmpty(objAnalyticalReportModel.ToDate) ? DateTime.ParseExact(objAnalyticalReportModel.ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            ReportService objReportService = new ReportService();
            objCallRegistrationListDataModel = objReportService.CallbackReceivedOrPendingReportDaily(DtFromDate, DtToDate);

            return View(objCallRegistrationListDataModel);
        }

        public ActionResult CallSummaryReport()
        {
            /*
            CallSummaryDataModel objCallSummaryDataModel = new CallSummaryDataModel();

            DateTime? FromDate = (DateTime?)null, ToDate = (DateTime?)null;

            ReportService objReportService = new ReportService();
            objCallSummaryDataModel = objReportService.CallSummaryReport(FromDate, ToDate);

            return View(objCallSummaryDataModel);
            */
            return View();
        }

        #endregion

        #region Company Email

        public ActionResult CompanyEmailList()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            return View();
        }

        [HttpPost]
        public JsonResult GetCompanyEmailList()
        {
            try
            {
                // Initialization.
                var draw = Request.Form.GetValues("draw")?[0];
                var order = Convert.ToInt32(Request.Form.GetValues("order[0][column]")?[0]);
                var orderDir = Request.Form.GetValues("order[0][dir]")?[0];
                var startRec = Convert.ToInt32(Request.Form.GetValues("start")?[0]);
                var pageSize = Convert.ToInt32(Request.Form.GetValues("length")?[0]);

                string ItemCompanyName = string.Empty, CompanyEmail= string.Empty;

                if (!string.IsNullOrEmpty(Request.Form["ItemCompanyName"]))
                    ItemCompanyName = Convert.ToString(Request.Form["ItemCompanyName"]).Trim();

                if (!string.IsNullOrEmpty(Request.Form["CompanyEmail"]))
                    CompanyEmail = Convert.ToString(Request.Form["CompanyEmail"]).Trim();


                ItemForSendCompanyReportListDataModel objCompanyEmailListDataModel = new ItemForSendCompanyReportListDataModel();

                ReportService objReportService = new ReportService();
                objCompanyEmailListDataModel = objReportService.GetCompanyEmailList(order, orderDir.ToUpper(), startRec, pageSize, ItemCompanyName, CompanyEmail);


                return Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = objCompanyEmailListDataModel.RecordCount,
                    recordsFiltered = objCompanyEmailListDataModel.RecordCount,
                    data = objCompanyEmailListDataModel.ItemForSendCompanyReportList
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


        public ActionResult CompanyEmailForm()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            ItemForSendCompanyReport objItemForSendCompanyReport = new ItemForSendCompanyReport(); ;

            string strId = Request["Id"];

            int Id = 0;

            int.TryParse(strId, out Id);

            if (Id > 0)
            {
                ReportService objReportService = new ReportService();
                objItemForSendCompanyReport = objReportService.GetCompanyEmailDetails(Id);
            }


            return View(objItemForSendCompanyReport);
        }

        [HttpPost]
        public JsonResult CompanyEmailForm(ItemForSendCompanyReport objItemForSendCompanyReport)
        {
            ResponceModel objResponceModel = new ResponceModel();

            ReportService objReportService = new ReportService();
            objResponceModel = objReportService.InsertUpdateCompanyEmail(objItemForSendCompanyReport.Id, objItemForSendCompanyReport.ItemCompanyName, objItemForSendCompanyReport.CompanyEmail);

            return Json(new { data = objResponceModel });
        }

        [HttpPost]
        public JsonResult DeleteAreaById(int Id)
        {
            ResponceModel objResponceModel = new ResponceModel();

            ReportService objReportService = new ReportService();
            objResponceModel = objReportService.DeleteCompanyEmailById(Id);

            return Json(new { data = objResponceModel });
        }



        #endregion

        #region Ajax Method

        [HttpPost]
        public JsonResult GetCallRegistrationReportList()
        {
            try
            {
                var draw = Convert.ToInt32(Request.Form["draw"]);
                var order = Convert.ToInt32(Request.Form["order[0][column]"]);
                var orderDir = Request.Form["order[0][dir]"];
                var startRec = Convert.ToInt32(Request.Form["start"]);
                var pageSize = Convert.ToInt32(Request.Form["length"]);

                if(!string.IsNullOrEmpty(Request.Form["FromDate"]) && !string.IsNullOrEmpty(Request.Form["ToDate"]))
                {
                    DateTime dtFromDate = DateTime.ParseExact(Convert.ToString(Request.Form["FromDate"]).Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime dtToDate = DateTime.ParseExact(Convert.ToString(Request.Form["ToDate"]).Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    int intCallCategory = 0;

                    if (!string.IsNullOrEmpty(Request.Form["CallCategory"]))
                        intCallCategory = Convert.ToInt32(Request.Form["CallCategory"]);

                    CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();

                    ReportService objReportService = new ReportService();
                    objCallRegistrationListDataModel = objReportService.GetCallRegistrationReportList(order, orderDir.ToUpper(), startRec, pageSize, dtFromDate, dtToDate, intCallCategory);

                    return Json(new
                    {
                        draw = Convert.ToInt32(draw),
                        recordsTotal = objCallRegistrationListDataModel.RecordCount,
                        recordsFiltered = objCallRegistrationListDataModel.RecordCount,
                        data = objCallRegistrationListDataModel.CallRegistrationList
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        draw = Convert.ToInt32(0),
                        recordsTotal = 0,
                        recordsFiltered = 0,
                        data = string.Empty
                    });
                }

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
        public JsonResult GetDailyReportMailList()
        {
            try
            {
                // Initialization.
                var draw = Request.Form.GetValues("draw")?[0];
                var order = Convert.ToInt32(Request.Form.GetValues("order[0][column]")?[0]);
                var orderDir = Request.Form.GetValues("order[0][dir]")?[0];
                var startRec = Convert.ToInt32(Request.Form.GetValues("start")?[0]);
                var pageSize = Convert.ToInt32(Request.Form.GetValues("length")?[0]);

                string CompanyName = string.Empty;
                DateTime? FromDate, ToDate;

                if (!string.IsNullOrEmpty(Request.Form["CompanyName"]))
                    CompanyName = Convert.ToString(Request.Form["CompanyName"]).Trim();

                FromDate = !string.IsNullOrEmpty(Request.Form["FromDate"]) ? DateTime.ParseExact(Request.Form["FromDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

                ToDate = !string.IsNullOrEmpty(Request.Form["ToDate"]) ? DateTime.ParseExact(Request.Form["ToDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;


                DailyMailReportStatusListDataModel objDailyMailReportStatusListDataModel = new DailyMailReportStatusListDataModel();

                ReportService objReportService = new ReportService();
                objDailyMailReportStatusListDataModel = objReportService.GetDailyReportMailList(order, orderDir.ToUpper(), startRec, pageSize, CompanyName, FromDate, ToDate);


                return Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = objDailyMailReportStatusListDataModel.RecordCount,
                    recordsFiltered = objDailyMailReportStatusListDataModel.RecordCount,
                    data = objDailyMailReportStatusListDataModel.DailyMailReportStatusList
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

        [HttpGet]
        public JsonResult SendPendingCallReportMail()
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
                ReportService objReportService = new ReportService();
                objCallRegistrationListDataModel = objReportService.PendingCallReport(null, null);

                string strPendinCallReportHtml = RenderRazorViewToString("~/Views/Report/_PendingCallReport.cshtml", objCallRegistrationListDataModel);

                if(!string.IsNullOrEmpty(strPendinCallReportHtml))
                {
                    string ToEmail = ConfigurationManager.AppSettings["DailyCallReportEmail"].ToString();
                    string CcEmail = ConfigurationManager.AppSettings["DailyCallReportEmailCC"].ToString();
                    EmailService objEmailService = new EmailService();
                    blnEmailSend = objEmailService.Sendmail(ToEmail, strPendinCallReportHtml, "LAST 72 HOUR PENDING CALL REPORT", "", CcEmail);
                }
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { data = ResponceMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { data = "Somthing Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SendTechnicianCallReportMail()
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
                ReportService objReportService = new ReportService();
                objCallRegistrationListDataModel = objReportService.TechnicianCallReportDaily(null, null);

                string strPendinCallReportHtml = RenderRazorViewToString("~/Views/Report/_TechnicianCallReportPartial.cshtml", objCallRegistrationListDataModel);

                if (!string.IsNullOrEmpty(strPendinCallReportHtml))
                {
                    string ToEmail = ConfigurationManager.AppSettings["DailyCallReportEmail"].ToString();
                    string CcEmail = ConfigurationManager.AppSettings["DailyCallReportEmailCC"].ToString();
                    EmailService objEmailService = new EmailService();
                    blnEmailSend = objEmailService.Sendmail(ToEmail, strPendinCallReportHtml, "DAILY TECHNICIAN REPORT", "", CcEmail);
                }
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { data = ResponceMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { data = "Somthing Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SendCollationReportDailyMail()
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
                ReportService objReportService = new ReportService();
                objCallRegistrationListDataModel = objReportService.CollationReportDaily(null, null);

                string strPendinCallReportHtml = RenderRazorViewToString("~/Views/Report/_CollationReport.cshtml", objCallRegistrationListDataModel);

                if (!string.IsNullOrEmpty(strPendinCallReportHtml))
                {
                    string ToEmail = ConfigurationManager.AppSettings["DailyCallReportEmail"].ToString();
                    string CcEmail = ConfigurationManager.AppSettings["DailyCallReportEmailCC"].ToString();
                    EmailService objEmailService = new EmailService();
                    blnEmailSend = objEmailService.Sendmail(ToEmail, strPendinCallReportHtml, "DAILY COLLATION REPORT", "", CcEmail);
                }
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { data = ResponceMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { data = "Somthing Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SendWorkshopInOutCallReportDailyMail()
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
                ReportService objReportService = new ReportService();
                objCallRegistrationListDataModel = objReportService.WorkshopInOutCallReportDaily(null, null);

                string strPendinCallReportHtml = RenderRazorViewToString("~/Views/Report/WorkshopInOutCallReportDaily.cshtml", objCallRegistrationListDataModel);

                if (!string.IsNullOrEmpty(strPendinCallReportHtml))
                {
                    string ToEmail = ConfigurationManager.AppSettings["DailyCallReportEmail"].ToString();
                    string CcEmail = ConfigurationManager.AppSettings["DailyCallReportEmailCC"].ToString();
                    EmailService objEmailService = new EmailService();
                    blnEmailSend = objEmailService.Sendmail(ToEmail, strPendinCallReportHtml, "WORKSHOP IN OUT CALL REPORT", "", CcEmail);
                }
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { data = ResponceMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { data = "Somthing Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SendServiceTypeReportDailyMail()
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
                ReportService objReportService = new ReportService();
                objCallRegistrationListDataModel = objReportService.ServiceTypeReportDaily(null, null);

                string strPendinCallReportHtml = RenderRazorViewToString("~/Views/Report/_ServiceTypeReport.cshtml", objCallRegistrationListDataModel);

                if (!string.IsNullOrEmpty(strPendinCallReportHtml))
                {
                    string ToEmail = ConfigurationManager.AppSettings["DailyCallReportEmail"].ToString();
                    string CcEmail = ConfigurationManager.AppSettings["DailyCallReportEmailCC"].ToString();
                    EmailService objEmailService = new EmailService();
                    blnEmailSend = objEmailService.Sendmail(ToEmail, strPendinCallReportHtml, "IN-WARRANTY | OUT-WARRANTY | INSTALLATION REPORT", "", CcEmail);
                }
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { data = ResponceMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { data = "Somthing Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SendServiceTypeReportDailyUserViseMail()
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
                ReportService objReportService = new ReportService();
                objCallRegistrationListDataModel = objReportService.ServiceTypeReportDaily(null, null);

                string strPendinCallReportHtml = RenderRazorViewToString("~/Views/Report/_ServiceTypeReportDailyUserVise.cshtml", objCallRegistrationListDataModel);

                if (!string.IsNullOrEmpty(strPendinCallReportHtml))
                {
                    string ToEmail = ConfigurationManager.AppSettings["DailyCallReportEmail"].ToString();
                    string CcEmail = ConfigurationManager.AppSettings["DailyCallReportEmailCC"].ToString();
                    EmailService objEmailService = new EmailService();
                    blnEmailSend = objEmailService.Sendmail(ToEmail, strPendinCallReportHtml, "IN-WARRANTY | OUT-WARRANTY | INSTALLATION REPORT", "", CcEmail);
                }
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { data = ResponceMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { data = "Somthing Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SendServiceTypeReportDailyItemViseMail()
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
                ReportService objReportService = new ReportService();
                objCallRegistrationListDataModel = objReportService.ServiceTypeReportDaily(null, null);

                string strPendinCallReportHtml = RenderRazorViewToString("~/Views/Report/_ServiceTypeReportDailyItemVise.cshtml", objCallRegistrationListDataModel);

                if (!string.IsNullOrEmpty(strPendinCallReportHtml))
                {
                    string ToEmail = ConfigurationManager.AppSettings["DailyCallReportEmail"].ToString();
                    string CcEmail = ConfigurationManager.AppSettings["DailyCallReportEmailCC"].ToString();
                    EmailService objEmailService = new EmailService();
                    blnEmailSend = objEmailService.Sendmail(ToEmail, strPendinCallReportHtml, "IN-WARRANTY | OUT-WARRANTY | INSTALLATION REPORT", "", CcEmail);
                }
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { data = ResponceMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { data = "Somthing Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SendPartPendingCallReportMail()
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
                ReportService objReportService = new ReportService();
                objCallRegistrationListDataModel = objReportService.PartPendingCallReport(null, null);

                string strPendinCallReportHtml = RenderRazorViewToString("~/Views/Report/_PartPendingCallReport.cshtml", objCallRegistrationListDataModel);

                if (!string.IsNullOrEmpty(strPendinCallReportHtml))
                {
                    string ToEmail = ConfigurationManager.AppSettings["DailyCallReportEmail"].ToString();
                    string CcEmail = ConfigurationManager.AppSettings["DailyCallReportEmailCC"].ToString();
                    EmailService objEmailService = new EmailService();
                    blnEmailSend = objEmailService.Sendmail(ToEmail, strPendinCallReportHtml, "LAST 72 PART PENDING REPORT", "", CcEmail);
                }
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { data = ResponceMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { data = "Somthing Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SendPaymentPendingReportDailyMail()
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
                ReportService objReportService = new ReportService();
                objCallRegistrationListDataModel = objReportService.PaymentPendingReportDaily(null, null);

                string strPendinCallReportHtml = RenderRazorViewToString("~/Views/Report/_PaymentPendingReport.cshtml", objCallRegistrationListDataModel);

                if (!string.IsNullOrEmpty(strPendinCallReportHtml))
                {
                    string ToEmail = ConfigurationManager.AppSettings["DailyCallReportEmail"].ToString();
                    string CcEmail = ConfigurationManager.AppSettings["DailyCallReportEmailCC"].ToString();
                    EmailService objEmailService = new EmailService();
                    blnEmailSend = objEmailService.Sendmail(ToEmail, strPendinCallReportHtml, "PAYMENT PENDING REPORT", "", CcEmail);
                }
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { data = ResponceMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { data = "Somthing Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SendRepeatFromTechReportDailyMail()
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
                ReportService objReportService = new ReportService();
                objCallRegistrationListDataModel = objReportService.RepeatFromTechReportDaily(null, null);

                string strPendinCallReportHtml = RenderRazorViewToString("~/Views/Report/RepeatFromTechReportDaily.cshtml", objCallRegistrationListDataModel);

                if (!string.IsNullOrEmpty(strPendinCallReportHtml))
                {
                    string ToEmail = ConfigurationManager.AppSettings["DailyCallReportEmail"].ToString();
                    string CcEmail = ConfigurationManager.AppSettings["DailyCallReportEmailCC"].ToString();
                    EmailService objEmailService = new EmailService();
                    blnEmailSend = objEmailService.Sendmail(ToEmail, strPendinCallReportHtml, "REPEAT FROM TECHNICIAN REPORT", "", CcEmail);
                }
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { data = ResponceMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { data = "Somthing Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SendCancelCallReportDailyMail()
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
                ReportService objReportService = new ReportService();
                objCallRegistrationListDataModel = objReportService.CancelCallReportDaily(null, null);

                string strPendinCallReportHtml = RenderRazorViewToString("~/Views/Report/_CancelCallReport.cshtml", objCallRegistrationListDataModel);

                if (!string.IsNullOrEmpty(strPendinCallReportHtml))
                {
                    string ToEmail = ConfigurationManager.AppSettings["DailyCallReportEmail"].ToString();
                    string CcEmail = ConfigurationManager.AppSettings["DailyCallReportEmailCC"].ToString();
                    EmailService objEmailService = new EmailService();
                    blnEmailSend = objEmailService.Sendmail(ToEmail, strPendinCallReportHtml, "CANCEL CALL REPORT", "", CcEmail);
                }
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { data = ResponceMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { data = "Somthing Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SendGoAfterCallReportDailyMail()
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
                ReportService objReportService = new ReportService();
                objCallRegistrationListDataModel = objReportService.GoAfterCallReportDaily(null, null);

                string strPendinCallReportHtml = RenderRazorViewToString("~/Views/Report/_GoAfterCallReport.cshtml", objCallRegistrationListDataModel);

                if (!string.IsNullOrEmpty(strPendinCallReportHtml))
                {
                    string ToEmail = ConfigurationManager.AppSettings["DailyCallReportEmail"].ToString();
                    string CcEmail = ConfigurationManager.AppSettings["DailyCallReportEmailCC"].ToString();
                    EmailService objEmailService = new EmailService();
                    blnEmailSend = objEmailService.Sendmail(ToEmail, strPendinCallReportHtml, "GO AFTER CALL REPORT", "", CcEmail);
                }
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { data = ResponceMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { data = "Somthing Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SendCallAssignToJobDoneTimingReportDailyMail()
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
                ReportService objReportService = new ReportService();
                objCallRegistrationListDataModel = objReportService.CallAssignToJobDoneTimingReportDaily(null, null);

                string strPendinCallReportHtml = RenderRazorViewToString("~/Views/Report/_CallAssignToJobDoneTimingReport.cshtml", objCallRegistrationListDataModel);

                if (!string.IsNullOrEmpty(strPendinCallReportHtml))
                {
                    string ToEmail = ConfigurationManager.AppSettings["DailyCallReportEmail"].ToString();
                    string CcEmail = ConfigurationManager.AppSettings["DailyCallReportEmailCC"].ToString();
                    EmailService objEmailService = new EmailService();
                    blnEmailSend = objEmailService.Sendmail(ToEmail, strPendinCallReportHtml, "Call Assign To Job Done Timing Report", "", CcEmail);
                }
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { data = ResponceMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { data = "Somthing Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetCallSummaryReportData(string FromDate, string ToDate)
        {
            CallSummaryDataModel objCallSummaryDataModel = new CallSummaryDataModel();

            DateTime? DtFromDate = (DateTime?)null, DtToDate = (DateTime?)null;

            DtFromDate = !string.IsNullOrEmpty(FromDate) ? DateTime.ParseExact(FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            DtToDate = !string.IsNullOrEmpty(ToDate) ? DateTime.ParseExact(ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

            ReportService objReportService = new ReportService();
            objCallSummaryDataModel = objReportService.CallSummaryReport(DtFromDate, DtToDate);
            
            return Json(new { data = objCallSummaryDataModel }, JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult SendCallSummaryReportMail()
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                DateTime? FromDate = (DateTime?)null, ToDate = (DateTime?)null;

                CallSummaryDataModel objCallSummaryDataModel = new CallSummaryDataModel();
                ReportService objReportService = new ReportService();
                objCallSummaryDataModel = objReportService.CallSummaryReport(FromDate, ToDate);

                string strPendinCallReportHtml = RenderRazorViewToString("~/Views/Report/CallSummaryReport.cshtml", objCallSummaryDataModel);

                if (!string.IsNullOrEmpty(strPendinCallReportHtml))
                {
                    string ToEmail = ConfigurationManager.AppSettings["DailyCallReportEmail"].ToString();
                    string CcEmail = ConfigurationManager.AppSettings["DailyCallReportEmailCC"].ToString();
                    EmailService objEmailService = new EmailService();
                    blnEmailSend = objEmailService.Sendmail(ToEmail, strPendinCallReportHtml, "Call Summary Report", "", CcEmail);
                }
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { data = ResponceMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { data = "Somthing Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SendCompanyReportManually(int ReportId)
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                ReportService objReportService = new ReportService();
                blnEmailSend = objReportService.SendCallForRegisterIntoCompany(ReportId);
                
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { Success = blnEmailSend, ResponceMessage = ResponceMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { Success = false, ResponceMessage = "Somthing Went Wrong" }, JsonRequestBehavior.AllowGet);
            }
        }



        #endregion


        #region  Export

        [HttpPost]
        public JsonResult ExportCallRegisterData(string FromDate, string ToDate, string CallCategory)
        {
            ExcelReportResponce objExcelReportResponce = new ExcelReportResponce();

            byte[] FileData = null;
            string strFileName = string.Empty;

            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
            {
                DateTime dtFromDate = DateTime.ParseExact(Convert.ToString(FromDate), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtToDate = DateTime.ParseExact(Convert.ToString(ToDate), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                int intCallCategory = 0;
                int.TryParse(CallCategory, out intCallCategory);

                ReportService objReportService = new ReportService();
                FileData = objReportService.GetCallRegisterExportData(dtFromDate, dtToDate, intCallCategory);

                if (FileData != null)
                {
                    objExcelReportResponce.Base64String = Convert.ToBase64String(FileData, 0, FileData.Length);
                    objExcelReportResponce.FileName = "Call_Registration_Report_" + DateTime.Now.ToString("dd'_'MM'_'yyyy'_'hhmmss") + ".xlsx";

                }
            }

            return Json(new { data = objExcelReportResponce });
        }

        [HttpPost]
        public JsonResult ExportCallRegisterDataForTechnician(string CallIds)
        {
            ExcelReportResponce objExcelReportResponce = new ExcelReportResponce();

            byte[] FileData = null;
            string strFileName = string.Empty;

            if (!string.IsNullOrEmpty(CallIds) )
            {
                ReportService objReportService = new ReportService();
                FileData = objReportService.GetCallRegisterExportForTechnician(CallIds);

                if(FileData != null)
                {
                    objExcelReportResponce.Base64String = Convert.ToBase64String(FileData, 0, FileData.Length);
                    objExcelReportResponce.FileName = "Technician_Calls_" + DateTime.Now.ToString("dd'_'MM'_'yyyy'_'hhmmss") + ".xlsx";
                }
            }

            return Json(new { data = objExcelReportResponce });

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

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

    }
}