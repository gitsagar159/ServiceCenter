using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceCenter.Models;
using System.Web.Routing;
using ServiceCenter.Services;

namespace ServiceCenter.Controllers
{
    public class PrintController : Controller
    {
        // GET: Print
        public ActionResult WorkOrderView(string CallId)
        {

            CallRegistration objCallRegistration = new CallRegistration();

            if(!string.IsNullOrEmpty(CallId))
            {
                ReportService objReportService = new ReportService();
                objCallRegistration = objReportService.GetWorkorderDetailByCallId(CallId);
            }
            else
            {
                objCallRegistration = new CallRegistration();
            }

            return View(objCallRegistration);
        }

        public ActionResult PrintPreview()
        {
            string CallId = Request["CallId"];

            var report = new Rotativa.ActionAsPdf("WorkOrderView",new RouteValueDictionary { { "CallId", CallId } })
            {
                PageMargins = { Left = 0, Bottom = 0, Right = 0, Top = 0 },
            };
            return report;
        }

        public ActionResult DownloadPrint()
        {
            //var report = new Rotativa.ActionAsPdf("UserDetail");
            //return report;
            string strDateTime = DateTime.Now.ToString("ddMMyyyy_hhmmss");

            return new Rotativa.ActionAsPdf("WorkOrderView")
            {


                FileName = "Work_Order_" + strDateTime + ".pdf"
            };
        }


    }
}