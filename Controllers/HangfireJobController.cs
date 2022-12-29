using Hangfire;
using Hangfire.Common;
using Microsoft.Ajax.Utilities;
using ServiceCenter.Models;
using ServiceCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServiceCenter.Controllers
{
    public class HangfireJobController : Controller
    {
        // GET: HangfireJob
        
        public ActionResult SendCompanyReportMorning()
        {

            DateTime TodayDate = DateTime.Now;
            DateTime FromDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 0, 0, 0);
            DateTime ToDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 11, 00, 0);

            ReportService objReportService = new ReportService();
            objReportService.GenerateCompanyViseReport(FromDate, ToDate);

            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendCompanyReportNoon()
        {
            DateTime TodayDate = DateTime.Now;
            DateTime FromDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 11, 00, 0);
            DateTime ToDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 16, 00, 0);

            ReportService objReportService = new ReportService();
            objReportService.GenerateCompanyViseReport(FromDate, ToDate);

            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendCompanyReportEvening()
        {
            DateTime TodayDate = DateTime.Now;
            DateTime FromDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 16, 00, 0);
            DateTime ToDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 20, 00, 0);

            ReportService objReportService = new ReportService();
            objReportService.GenerateCompanyViseReport(FromDate, ToDate);

            return Json(0, JsonRequestBehavior.AllowGet);
        }
        

        /*
        public async Task<ActionResult> SendCompanyReportNoon()
        {
            DateTime TodayDate = DateTime.Now;
            DateTime FromDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 10, 30, 0);
            DateTime ToDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 13, 30, 0);

            ReportService objReportService = new ReportService();
            objReportService.GenerateCompanyViseReport(FromDate, ToDate);

            return Json(0, JsonRequestBehavior.AllowGet);
        }
        */

    }
}