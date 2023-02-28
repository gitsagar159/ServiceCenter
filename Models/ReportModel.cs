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


    public class ItemForSendCompanyReport
    {
        public int RowNo { get; set; }
        public int Id { get; set; }
        public string ItemCompanyName { get; set; }

        public string CompanyEmail { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsDelete { get; set; }
        public string FileName { get; set; }
    }

    public class DailyMailReportStatus
    {
        public int RowNo { get; set; }
        public int Id { get; set; }
        public string ItemCompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string ReportName { get; set; }
        public bool SendBySystem { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedOnString { get; set; }
        public bool SendByUser { get; set; }
        public string UserName { get; set; }
        public int SendByUserId { get; set; }
        public DateTime? SendByUserDateTime { get; set; }
        public string SendByUserDateTimeString { get; set; }
    }

    public class DailyMailReportStatusListDataModel
    {
        public List<DailyMailReportStatus> DailyMailReportStatusList { get; set; }  //= new List<CallRegistration>();

        public int RecordCount { get; set; }
    }

    public class ItemForSendCompanyReportListDataModel
    {
        public List<ItemForSendCompanyReport> ItemForSendCompanyReportList { get; set; }  //= new List<CallRegistration>();

        public int RecordCount { get; set; }
    }

    public class CallSummaryDataModel
    {
        public string fromdate { get; set; }
        public string todate { get; set; }
        public List<UserViseCallCountModel> UserViseCallCountList { get;set;}
        public List<ItemViseCallCountModel> ItemViseCallCountList { get; set; }
        public List<ServiceTypeViseCallCountModel> ServiceTypeViseCallCountList { get; set; }
        public List<CallTypeViseCallCountModel> CallTypeViseCallCountList { get; set; }
        public string UserViseCallCountListJson { get; set; }

    }

    public class UserViseCallCountModel
    {
        public string UserName { get; set; }
        public int CallCount { get; set; }

    }

    public class ItemViseCallCountModel
    {
        public string ItemName { get; set; }
        public int CallCount { get; set; }

    }

    public class ServiceTypeViseCallCountModel
    {
        public string ServiceType { get; set; }
        public int CallCount { get; set; }

    }

    public class CallTypeViseCallCountModel
    {
        public string CallType { get; set; }
        public int CallCount { get; set; }

    }

    public class AnalyticalReportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }


}