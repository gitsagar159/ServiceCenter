using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceCenter.Models
{
    public class CallRegistrationModel
    {
    }

    public class CallRegistrationListSearchOption
    {
        public string Keyword { get; set; } 
        public int CallCategory { get; set; }
    }

    public class CallRegistration: ResponceModel
    {
        public long RowNo { get; set; }
        public string Oid { get; set; }
        public string IsNew { get; set; }
        public bool CallDeleted { get; set; }
        public string Area { get; set; }
        public string AreaName { get; set; }
        public long ID { get; set; }
        public string RecipientType { get; set; }
        public DateTime? JobDoneTime { get; set; }
        public string StringJobDoneTime { get; set; }
        public DateTime? CallAssignDate { get; set; }
        public string StringCallAssignDate { get; set; }
        public string GodownName { get; set; }
        public string Godown { get; set; }
        public string DealerName { get; set; }
        public bool HomeDelivery { get; set; }
        public bool Display { get; set; }
        public string ExchangeProduct { get; set; }
        public bool PendingExchange { get; set; }
        public string EstimateDate { get; set; }
        public string EstConfirmDate { get; set; }
        public int PaymentBy { get; set; }
        public string UserName { get; set; }
        public string Modifier { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreationDate { get; set; }
        public string StringCreationDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyDateString { get; set; }
        public string CompComplaintNo { get; set; }
        public string JobNo { get; set; }
        public DateTime? CallDate { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string SrNo { get; set; }
        public string FaultDesc { get; set; }
        public int CallType { get; set; }
        public string CallTypeName { get; set; }
        public string Customer { get; set; }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string ProductName { get; set; }
        public int ServType { get; set; }
        public string ServTypeName { get; set; }
        public string SpInst { get; set; }
        public string ModelName { get; set; }
        public string ItemGroup { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string StringPurchaseDate { get; set; }
        public string BillNo { get; set; }
        public string ShowRoom { get; set; }
        public string TechBillNo { get; set; }
        public string DealRef { get; set; }
        public string TechnicianId { get; set; }
        public string Technician { get; set; }
        public string Technician_Old { get; set; }
        public string TechnicianName { get; set; }
        public string TechnicianType { get; set; }

        public string TechType { get; set; }

        public float Amount { get; set; }
        public string SIGN { get; set; }
        public string Warranty { get; set; }
        public bool CallAttn { get; set; }
        public bool JobDone { get; set; }
        public bool JobDone_Old { get; set; }
        public bool Deliver { get; set; }
        public bool SMSSent { get; set; }
        public decimal Estimate { get; set; }
        public float DelQty { get; set; }
        public string Salesman { get; set; }
        public string Driver { get; set; }
        public string VehicleNo { get; set; }
        public decimal Payment { get; set; }
        public decimal VisitCharge { get; set; }
        public decimal Rent { get; set; }
        public bool CallBack { get; set; }
        public bool WorkShopIN { get; set; }
        public bool PartPanding { get; set; }
        public bool GoAfterCall { get; set; }
        public bool Canceled { get; set; }
        public bool PaymentPanding { get; set; }
        public bool AMC { get; set; }
        public bool Repeat { get; set; }
        public bool RepeatFromTech { get; set; }
        public string JobRegion { get; set; }
        public string JobDoneRegion { get; set; }
        public string FaultType { get; set; }
        public string FaultTypeName { get; set; }
        public string PaymentByCheque { get; set; }
        public string fault_name7 { get; set; }

        public DateTime? CallAttn_DateTime { get; set; }
        public DateTime? Deliver_DateTime { get; set; }
        public DateTime? SMSSent_DateTime { get; set; }
        public DateTime? CallBack_DateTime { get; set; }
        public DateTime? WorkShopIN_DateTime { get; set; }
        public DateTime? GoAfterCall_DateTime { get; set; }
        public DateTime? Canceled_DateTime { get; set; }
        public bool AC_Service { get; set; }
        public int FreeServiceCardType { get; set; }
        public string FreeServiceCardNo { get; set; }
        public string PartId { get; set; }
        public decimal PartPrice { get; set; }

        public string CityName { get; set; }
        public SelectList ServiceTypeDD { get; set; }
        public SelectList CallTypeDD { get; set; }
        public SelectList TechnicianDD { get; set; }
        public SelectList AreaDD { get; set; }
        public SelectList FaultTypeDD { get; set; }
        public SelectList PaymentByDD { get; set; }
        public SelectList FreeServiceCardTypeDD { get; set; }

        public string Select2JSON { get; set; }

        public string PhoneH { get; set; }
        public string PhoneO { get; set; }
        public bool IsEdit { get; set; }

        public int CallAssingToJobDoneTime { get; set; }
    }

    public class CallRegistrationSelect2Data
    {
        public Select2 Select2Area { get; set; }
        public Select2 Select2Item { get; set; }
        public Select2 Select2Technician { get; set; }
        public Select2 Select2FaultType { get; set; }
        public Select2 Select2Customer { get; set; }
        public Select2 Select2Part { get; set; }
    }

    public class CallRegistrationListDataModel
    {
        public List<CallRegistration> CallRegistrationList { get; set; }  //= new List<CallRegistration>();

        public int RecordCount { get; set; }

        public string Oids { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }

    }

    public class CallBackReceivedOrPendingModel
    {
        public List<CallRegistration> CallBackList { get; set; }
        public List<CallRegistration> WorkshopInList { get; set; }
        public List<CallRegistration> DeliverList { get; set; }
        public List<CallRegistration> PendingList { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

    }

    public class ResultList<T>
    {
        public List<T> items { get; set; }
        public int total_count { get; set; }
    }

    public class Select2
    {
        public string id { get; set; }
        public string text { get; set; }
    }

    public class CustomerCodeDD
    {
        public List<Select2> Text { get; set; }
    }


    public class CustomerMaster : ResponceModel
    {
        public string Oid { get; set; }
        public string OldOid { get; set; }
        public long CustCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string PhoneH { get; set; }
        public string PhoneO { get; set; }
        public string MobileNo { get; set; }
        public string EMail { get; set; }
        public string OtherInfo { get; set; }
        public DateTime? SpDay { get; set; }
        public string UserName { get; set; }
        public string Modifier { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int OptimisticLockField { get; set; }
        public int GCRecord { get; set; }

        public string Select2JSON { get; set; }

    }

    public class CustomerMasterSelect2Data
    {
        public Select2 Select2City { get; set; }
    }
    public class JqueryDatatableParam
    {
        public string sEcho { get; set; }
        public string sSearch { get; set; }
        public int iDisplayLength { get; set; }
        public int iDisplayStart { get; set; }
        public int iColumns { get; set; }
        public int iSortCol_0 { get; set; }
        public string sSortDir_0 { get; set; }
        public int iSortingCols { get; set; }
        public string sColumns { get; set; }
        public int CallCategory { get; set; }
    }

    public class BillDetails
    {
        //CustomerId CustomerName    Address MobileNo    City PinCode ProductId ProductName ProductModel SalesId InvoiceDate
        public string BillNo { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string MobileNo2 { get; set; }
        public string CityName { get; set; }
        public string Pincode { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductModel { get; set; }
        public int SalesId { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string StringInvoiceDate { get; set; }
        public string ItemName { get; set; }
        public string SerialNo { get; set; }

        public string BillDetailJSON { get; set; }


    }

    public class BillDetailsDataModel
    {
        public List<BillDetails> BillData { get; set; }
    }


    public class InstallationsTable
    {
        public int RowNo { get; set; }
        public int Id { get; set; }
        public string JobNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNo { get; set; }
        public string Image { get; set; }
        public List<string> ImageList { get; set; }
        public string TechnicianName { get; set; }
        public string TechnicianNo { get; set; }
        public string JobDate { get; set; }
        public string JobTime { get; set; }
        public string JobDateTime { get; set; }
        public string Location { get; set; }
        public string ServTypeName { get; set; }
    }

    public class ListViewDataModel<T>
    {
        public List<T> DataList { get; set; }  //= new List<CallRegistration>();

        public int RecordCount { get; set; }
    }

    public class InstallationImages
    {
        public List<string> ImageList { get; set; }  //= new List<CallRegistration>();
    }
}
