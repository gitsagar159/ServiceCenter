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
        public bool IsActive { get; set; }
    }

    public class AreaMasterListDataModel
    {
        public List<AreaMasterListModel> AreaMasterList { get; set; }  //= new List<CallRegistration>();

        public int RecordCount { get; set; }
    }

    #endregion

    #region Item


    public class ItemMaster
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }

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
}