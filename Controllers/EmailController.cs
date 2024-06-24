using ServiceCenter.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace ServiceCenter.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        [HttpGet]
        public async Task<JsonResult> SendCompanyCallRegistrationMail()
        {
            bool blnEmailSend = false;
            string ResponceMessage = "";
            try
            {
                DateTime TodayDate = DateTime.Now;
                DateTime MorningReportDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 11, 0, 0);
                DateTime NoonReportDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 13, 30, 0);
                DateTime AfterNoonReportDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 16, 15, 0);
                DateTime EveningReportDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 17, 50, 0);

                CallRegisterService objCallRegisterService = new CallRegisterService();

                StringBuilder sbEmailTrace = new StringBuilder();

                if (TodayDate > MorningReportDate && TodayDate < NoonReportDate)
                {
                    DateTime FromDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 17, 50, 00);
                    DateTime ToDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 10, 59, 59);

                    sbEmailTrace.AppendLine("Morning Report Process Start ");

                    objCallRegisterService.GenerateCompanyViseReport(FromDate.AddDays(-1), ToDate, ref sbEmailTrace);
                }
                else if (TodayDate > NoonReportDate && TodayDate < AfterNoonReportDate)
                {
                    DateTime FromDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 11, 0, 0);
                    DateTime ToDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 13, 29, 59);

                    sbEmailTrace.AppendLine("Noon Report Process Start ");

                    objCallRegisterService.GenerateCompanyViseReport(FromDate, ToDate, ref sbEmailTrace);
                }
                else if (TodayDate > AfterNoonReportDate && TodayDate < EveningReportDate)
                {
                    DateTime FromDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 13, 30, 0);
                    DateTime ToDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 16, 14, 59);

                    sbEmailTrace.AppendLine("After Noon Report Process Start ");

                    objCallRegisterService.GenerateCompanyViseReport(FromDate, ToDate, ref sbEmailTrace);
                }
                else if (TodayDate > EveningReportDate)
                {
                    DateTime FromDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 16, 15, 0);
                    DateTime ToDate = new DateTime(TodayDate.Year, TodayDate.Month, TodayDate.Day, 17, 49, 59);

                    sbEmailTrace.AppendLine("Evening Report Process Start ");

                    objCallRegisterService.GenerateCompanyViseReport(FromDate, ToDate, ref sbEmailTrace);
                }

                CommonService.WriteTraceLog(sbEmailTrace.ToString());

                blnEmailSend = true;
                ResponceMessage = blnEmailSend ? "Email Send" : "Email Not Send";

                return Json(new { ResponceMessage = ResponceMessage, Success = blnEmailSend }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { ResponceMessage = "Somthing Went Wrong", Success = blnEmailSend }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}