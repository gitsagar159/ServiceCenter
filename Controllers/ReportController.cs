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

                    CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();

                    ReportService objReportService = new ReportService();
                    objCallRegistrationListDataModel = objReportService.GetCallRegistrationReportList(order, orderDir.ToUpper(), startRec, pageSize, dtFromDate, dtToDate);

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

        [HttpGet]
        public ActionResult ExportCallRegisterData(string FromDate, string ToDate)
        {
            byte[] FileData = null;
            string strFileName = string.Empty;

            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
            {
                DateTime dtFromDate = DateTime.ParseExact(Convert.ToString(FromDate), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtToDate = DateTime.ParseExact(Convert.ToString(ToDate), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                ReportService objReportService = new ReportService();
                FileData = objReportService.GetCallRegisterExportData(dtFromDate, dtToDate);

                strFileName = "Call_Registration_Report_" + DateTime.Now.ToString("dd'_'MM'_'yyyy'_'hhmmss") + ".xlsx";

            }

            if (FileData != null)
            {
                return File(FileData, "application/octet-stream", strFileName);
            }
            else
            {
                return new EmptyResult();
            }
        }
        #endregion
    }
}