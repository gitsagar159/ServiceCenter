using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceCenter.Models;
using ServiceCenter.Services;

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

    }
}