using System;
using System.Collections.Generic;
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
            return View();
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

    }
}