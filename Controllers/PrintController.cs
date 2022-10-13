using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceCenter.Models;
using System.Web.Routing;
using ServiceCenter.Services;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ServiceCenter.Controllers
{
    public class PrintController : Controller
    {
        // GET: Print
        public ActionResult WorkOrderView(string CallId)
        {

            CallRegistration objCallRegistration = new CallRegistration();

            if (!string.IsNullOrEmpty(CallId))
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

            var report = new Rotativa.ActionAsPdf("WorkOrderView", new RouteValueDictionary { { "CallId", CallId } })
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

        [HttpPost]
        public ActionResult MultipleWorkorderPrintPreview()
        {

            string CallIds = Request["CallIds"];

            List<byte[]> pdfByteList = new List<byte[]>();

            PdfPrintResponce objPdfPrintResponce = new PdfPrintResponce();

            byte[] FileData = null;
            string strFileName = string.Empty;

            if (!string.IsNullOrEmpty(CallIds))
            {

                string[] lstCallIds = CallIds.Split(',');

                foreach (string CallIdItem in lstCallIds)
                {
                    var actionPDF = new Rotativa.ActionAsPdf("WorkOrderView", new RouteValueDictionary { { "CallId", CallIdItem.Trim() } })
                    {
                        PageMargins = { Left = 0, Bottom = 0, Right = 0, Top = 0 },
                    };

                    byte[] applicationPDFData = actionPDF.BuildFile(ControllerContext);
                    pdfByteList.Add(applicationPDFData);
                }

                FileData = concatAndAddContent(pdfByteList);

                if (FileData != null)
                {
                    objPdfPrintResponce.Base64String = Convert.ToBase64String(FileData, 0, FileData.Length);
                    objPdfPrintResponce.FileName = "Multiple_WorkOrder_" + DateTime.Now.ToString("dd'_'MM'_'yyyy'_'hhmmss") + ".pdf";
                }
            }

            return Json(new { data = objPdfPrintResponce });
        }

        public  byte[] concatAndAddContent(List<byte[]> pdfByteContent)
        {

            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    using (var copy = new PdfSmartCopy(doc, ms))
                    {
                        doc.Open();

                        //Loop through each byte array
                        foreach (var p in pdfByteContent)
                        {

                            //Create a PdfReader bound to that byte array
                            using (var reader = new PdfReader(p))
                            {

                                //Add the entire document instead of page-by-page
                                copy.AddDocument(reader);
                            }
                        }

                        doc.Close();
                    }
                }

                //Return just before disposing
                return ms.ToArray();
            }
        }


    }
}