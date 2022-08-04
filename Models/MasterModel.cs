using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public bool IsActive { get; set; }
    }

    public class ItemMasterListDataModel
    {
        public List<ItemMasterListModel> ItemMasterList { get; set; }  //= new List<CallRegistration>();

        public int RecordCount { get; set; }
    }


    #endregion

    #region Technician
    #endregion
}