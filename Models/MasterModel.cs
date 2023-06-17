using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceCenter.Models
{
    public class MasterModel
    {
    }

    #region Area

    public class AreaMaster
    {
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public string AreaPincode { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }

    public class AreaMasterListModel
    {
        public int RowNo { get; set; }
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public string AreaPincode { get; set; }
        public bool IsActive { get; set; }
    }

    public class AreaMasterListDataModel
    {
        public List<AreaMasterListModel> AreaMasterList { get; set; }  //= new List<CallRegistration>();

        public int RecordCount { get; set; }
    }

    public class AreaMasterDD
    {
        public List<AreaMaster> AreaMasterList { get; set; }
    }

    #endregion

    #region Item


    public class ItemMaster
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemKeyword { get; set; }
        public string TechnicianId { get; set; }
        public string Technician { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }

    public class ItemMasterListModel
    {
        public int RowNo { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }

        public string ItemKeyword { get; set; }
        public string Technician { get; set; }
        public bool IsActive { get; set; }
    }

    public class ItemMasterListDataModel
    {
        public List<ItemMasterListModel> ItemMasterList { get; set; }  //= new List<CallRegistration>();

        public int RecordCount { get; set; }
    }


    #endregion

    #region Technician

    public class TechnicianMaster : ResponceModel
    {
        public long RowNo { get; set; }
        public string Oid { get; set; }
        public string OldOid { get; set; }
        public bool IsActive { get; set; }
        public string Technician { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string CityName { get; set; }
        public string L1 { get; set; }
        public string L2 { get; set; }
        public string PhoneH { get; set; }
        public string PhoneO { get; set; }
        public string Mobile { get; set; }
        public string TechType { get; set; }
        public string TechTypeName { get; set; }
        public string JobType { get; set; }
        public string UserName { get; set; }
        public string Modifier { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int OptimisticLockField { get; set; }
        public int GCRecord { get; set; }
        public string locationtime { get; set; }
        public string device_token { get; set; }
        public string Select2JSON { get; set; }
        public SelectList TechnicianTypeDD { get; set; }
        public string Password { get; set; }
    }


    public class TechnicianListDataModel
    {
        public List<TechnicianMaster> TechnicianList { get; set; }

        public int RecordCount { get; set; }
    }

    public class TechnicianMasterSelect2Data
    {
        public Select2 Select2City { get; set; }
    }

    public class BillData
    {
        public string BillDate { get; set; }
        public string BillNo { get; set; }
    }


    public class TechnicianAttendance
    {
        public int RowNo { get; set; }
        public int attendance_id { get; set; }
        public string technician_name { get; set; }
        public string technician_mno { get; set; }
        public bool technician_present { get; set; }
        public bool start_day { get; set; }
        public string technician_img { get; set; }
        public string present_location { get; set; }
        public bool technician_absent { get; set; }
        public string absent_reason { get; set; }
        public string absent_time { get; set; }
        public bool technician_tranning { get; set; }
        public string location { get; set; }
        public string attendance_date { get; set; }
        public string attendance_time { get; set; }
        public bool lunch_start { get; set; }
        public string lunch_img { get; set; }
        public string lunch_starttime { get; set; }
        public string lunch_start_location { get; set; }
        public bool lunch_end { get; set; }
        public string lunch_endimg { get; set; }
        public string lunch_endtime { get; set; }
        public string lunch_end_location { get; set; }
        public bool end { get; set; }
        public string end_img { get; set; }
        public string endtime { get; set; }
        public string end_day_location { get; set; }
        public string locationde { get; set; }
        public string role { get; set; }
    }

    public class TechnicianAttendanceListDataModel
    {
        public List<TechnicianAttendance> TechnicianAttendanceList { get; set; }

        public int RecordCount { get; set; }
    }

    #endregion

    #region IP Address

    public class clsIPAddress
    {
        public int RowNo { get; set; }
        public int Id { get; set; }
        public string IP { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class IPAddressMasterListDataModel
    {
        public List<clsIPAddress> IPAddressMasterList { get; set; }

        public int RecordCount { get; set; }
    }

    #endregion


    #region Part


    public class PartMaster
    {
        public string PartId { get; set; }
        public string PartName { get; set; }
        public string PartKeyword { get; set; }
        public string Company { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }

    public class PartMasterListModel
    {
        public int RowNo { get; set; }
        public string PartId { get; set; }
        public string PartName { get; set; }
        public string PartKeyword { get; set; }
        public string Company { get; set; }
        public bool IsActive { get; set; }
    }

    public class PartMasterListDataModel
    {
        public List<PartMasterListModel> PartMasterList { get; set; }  //= new List<CallRegistration>();

        public int RecordCount { get; set; }
    }


    #endregion
}