using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceCenter.Models
{
    public class ReportModel
    {
    }

    public class ExcelReportResponce
    {
        public string Base64String { get; set; }
        public string FileName { get; set; }
    }

    public class PdfPrintResponce : ExcelReportResponce
    {
        
    }
}