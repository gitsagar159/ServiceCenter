using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceCenter.Models;
using System.Web.Routing;

namespace ServiceCenter.Controllers
{
    public class PrintController : Controller
    {
        // GET: Print
        public ActionResult WorkOrderView(string CallId)
        {
            return View();
        }

        public ActionResult PrintPreview()
        {
            var report = new Rotativa.ActionAsPdf("WorkOrderView")
            {
                PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
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