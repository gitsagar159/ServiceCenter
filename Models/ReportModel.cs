using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    public class CallSummaryModel
    {
        public int TotalCall { get; set; }
        public int InWarranty { get; set; }
        public int OutWarranty { get; set; }
        public int Installation { get; set; }

        public int Local { get; set; }
        public int Workshop { get; set; }
        public int OutStation { get; set; }

        public int Attended { get; set; }
        public int CallBack { get; set; }

        public int RepeatFromTech { get; set; }
        public int WorkshopIn { get; set; }
        public int Delever { get; set; }
        public int Canceled { get; set; }
        public int GoAfterCall { get; set; }
        public int PartPending { get; set; }

        public int JobDone { get; set; }
        public int AttendedButNotDone { get; set; }
        public int PendingJob { get; set; }


        public decimal Estimate { get; set; }
        public decimal VisitCharge { get; set; }
        public decimal Payment { get; set; }

    }

    public class TechnicianReportData
    {
        public List<TechnicianReport> TechnicianReportList { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class TechnicianReport
    {
        public string TechnicianId { get; set; }
        public string TechnicianName { get; set; }
        public string TechnicianTypeId { get; set; }
        public decimal TechnicianPayment { get; set; }
        public decimal TechnicianVisitCharge { get; set; }
        public decimal TechnicianEstimate { get; set; }
        public decimal TechnicianExpence { get; set; }
        public decimal TotalEarning { get; set; }

        public int AssignCalls { get; set; }
        public int DoneCalls { get; set; }
        public int CallBackCalls { get; set; }
        public int CancelCalls { get; set; }
        public int PartPanding { get; set; }
        public int GoAfterCall { get; set; }
        public int WorkShopIN { get; set; }
        public int Deliver { get; set; }
        public int Ac_Service { get; set; }
        public int Workshop_Pending { get; set; }

        public TechnicianLocal Local_Calls_List { get; set; }
        public TechnicianWorkshop Workshop_Calls_List { get; set; }
        public TechnicianOutStation OutStation_Calls_List { get; set; }
        public List<TechnicianExpense> TechnicianExpenceData { get; set; }

    }

    public class TechnicianLocal
    {
        public int LocalCalls { get; set; }
        public TechnicianInWarranty Local_InWarranty_Calls { get; set; }
        public TechnicianOutWarranty Local_OutWarranty_Calls { get; set; }
        public TechnicianInstallation Local_Installation_Calls { get; set; }

    }

    public class TechnicianWorkshop
    {
        public int WorkshopCalls { get; set; }
        public TechnicianInWarranty Workshop_InWarranty_Calls { get; set; }
        public TechnicianOutWarranty Workshop_OutWarranty_Calls { get; set; }
        public TechnicianInstallation Workshop_Installation_Calls { get; set; }

    }

    public class TechnicianOutStation
    {
        public int OutStationCalls { get; set; }
        public TechnicianInWarranty OutStation_InWarranty_Calls { get; set; }
        public TechnicianOutWarranty OutStation_OutWarranty_Calls { get; set; }
        public TechnicianInstallation OutStation_Installation_Calls { get; set; }

    }

    public class TechnicianInWarranty
    {
        public int InWarrantyCalls { get; set; }
        public List<TechnicianItems> InWarrantyItems { get; set; }
        public int InWarrantyDemoCalls { get; set; }
        public int InWarrantyJobDoneCalls { get; set; }
        public int InWarrantyCancleCalls { get; set; }
        public int InWarrantyAC_ServiceCalls { get; set; }
        public int InWarrantyAttend_But_NotDoneCalls { get; set; }
        public int InWarrantyCallBackCalls { get; set; }
        public int InWarrantyCallBackRecivedCalls { get; set; }
        public int InWarrantyCallBackPendingCalls { get; set; }
        public int InWarrantyWorkshop_JobDone_But_NotDeliverCalls { get; set; }
        public int InWarrantyWorkshop_DeliverCalls { get; set; }
    }

    public class TechnicianOutWarranty
    {
        public int OutWarrantyCalls { get; set; }
        public List<TechnicianItems> OutWarrantyItems { get; set; }
        public int OutWarrantyJobDoneCalls { get; set; }
        public int OutWarrantyCancleCalls { get; set; }
        public int OutWarrantyAC_ServiceCalls { get; set; }
        public int OutWarrantyAttend_But_NotDoneCalls { get; set; }
        public int OutWarrantyCallBackCalls { get; set; }
        public int OutWarrantyCallBackRecivedCalls { get; set; }
        public int OutWarrantyCallBackPendingCalls { get; set; }
        public int OutWarrantyWorkshop_JobDone_But_NotDeliverCalls { get; set; }
        public int OutWarrantyWorkshop_DeliverCalls { get; set; }

    }

    public class TechnicianInstallation
    {
        public int InstallationCalls { get; set; }
        public List<TechnicianItems> InstallationItems { get; set; }
        public int InstallationJobDoneCalls { get; set; }
        public int InstallationCancleCalls { get; set; }
        public int InstallationAC_ServiceCalls { get; set; }
        public int InstallationAttend_But_NotDoneCalls { get; set; }
        public int InstallationCallBackCalls { get; set; }
        public int InstallationCallBackRecivedCalls { get; set; }
        public int InstallationCallBackPendingCalls { get; set; }
        public int InstallationWorkshop_JobDone_But_NotDeliverCalls { get; set; }
        public int InstallationWorkshop_DeliverCalls { get; set; }

    }

    public class TechnicianItems
    {
        public string ItemName { get; set; }

    }

    public class TechnicianExpense
    {
        public int id { get; set; }
        public string techid { get; set; }
        public int type { get; set; }
        public int amount { get; set; }
        public string date { get; set; }
    }
    public class DailyRegisterCallReportData
    {
        public DailyRegisterCallReport DailyRegisterCallReport { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class DailyRegisterCallReport
    {
        public TechnicianLocal Local_Calls_List { get; set; }
        public TechnicianWorkshop Workshop_Calls_List { get; set; }
        public TechnicianOutStation OutStation_Calls_List { get; set; }
    }

    public class PartPendingCallReport
    {
        public List<PartPendingItems> KetanPartPendingItems { get; set; }
        public List<PartPendingItems> CompanyPartPendingItems { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class PartPendingItems
    {
        public string ItemName { get; set; }
        public int ItemCount { get; set; }
        public List<CallRegistration> PartPendingCallList { get; set; }
    }

    public class TechnicianJobTimeReportData
    {
        public List<TechnicianJobTimeReport> TechnicianJobTimeReportList { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class TechnicianJobTimeReport
    {
        public string Oid { get; set; }
        public string JobNo { get; set; }
        public string CustomerName { get; set; }
        public int CallType { get; set; }
        public string CallTypeName { get; set; }
        public int ServType { get; set; }
        public string ServTypeName { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string FaultDesc { get; set; }
        public string TechnicianId { get; set; }
        public string TechnicianName { get; set; }
        public string TechType { get; set; }
        public bool JobDone { get; set; }
        public DateTime? JobStartDateTime { get; set; }
        public DateTime? JobEndDateTime { get; set; }
        public string TotalJobTime { get; set; }
    }

    public enum ReportType
    {
        TechnicianCallReport = 0,
        TechnicianPendingCallReport = 1,
        TechnicianGoAfterCallReport = 2,
        TechnicianCancleCallReport = 3,
        DailyRegisterCallReport = 4,
        PartPendingReport = 5,
    }

    public class TechnicianCallBackVSWorkshopReportData
    {
        public List<TechnicianReport> TechnicianCallBackReportList { get; set; }
        public List<TechnicianReport> TechnicianWorkshopReportList { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}