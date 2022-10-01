using Newtonsoft.Json;
using ServiceCenter.Models;
using ServiceCenter.Models.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceCenter.Services
{
    public class MasterService
    {
        string strQuery = "";

        private BaseDAL objBaseDAL;
        private List<SqlParameter> lstParam;

        #region Area
        public ResponceModel InsertUpdateArea(string AreaId, string AreaName)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"InsertUpdateAreaDetail";

                lstParam = new List<SqlParameter>();

                SqlParameter AreaIdParam = new SqlParameter();

                if (!string.IsNullOrEmpty(AreaId))
                {
                    AreaIdParam = new SqlParameter() { ParameterName = "@AreaId", Value = AreaId.ToUpper() };
                }
                else
                {
                    AreaIdParam = new SqlParameter() { ParameterName = "@AreaId", Value = DBNull.Value };
                }

                
                SqlParameter AreaNameParam = new SqlParameter() { ParameterName = "@AreaName", Value = AreaName };
                SqlParameter LoginUserIdParam = new SqlParameter() { ParameterName = "@LoginUserId", Value = UserSesionDetail.id };

                lstParam.AddRange(new SqlParameter[] { AreaIdParam, AreaNameParam, LoginUserIdParam });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);


                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowItem = ResDataTable.Rows[0];

                    objResponceModel.Message = dtRowItem["ResponceMesage"] != DBNull.Value ? Convert.ToString(dtRowItem["ResponceMesage"]) : string.Empty;
                    objResponceModel.Responce = dtRowItem["OprationSuceess"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["OprationSuceess"]) : false;

                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objResponceModel;
        }

        public AreaMaster GetAreaDetails(string AreaId)
        {
            AreaMaster objAreaMaster = new AreaMaster();

            try
            {
                objBaseDAL = new BaseDAL();
                strQuery = @"select AreaId, AreaName from AreaMaster WHERE AreaId = @AreaId";

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                         {
                                new SqlParameter("@AreaId", new Guid(AreaId)),
                         });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowItem = ResDataTable.Rows[0];

                    objAreaMaster.AreaId = dtRowItem["AreaId"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaId"]) : string.Empty;
                    objAreaMaster.AreaName = dtRowItem["AreaName"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaName"]) : string.Empty;
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objAreaMaster;
        }

        [HttpPost]
        public AreaMasterListDataModel GetAreaList(int SortCol, string SortDir, int PageIndex, int PageSize, string AreaName)
        {
            AreaMasterListDataModel objAreaMasterListDataModel = new AreaMasterListDataModel();
            objAreaMasterListDataModel.AreaMasterList = new List<AreaMasterListModel>();

            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"AreaList";

                lstParam = new List<SqlParameter>();


                SqlParameter SortColParam = new SqlParameter() { ParameterName = "@SortCol", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = SortCol };
                SqlParameter SortDirParam = new SqlParameter() { ParameterName = "@SortDir", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = SortDir };
                SqlParameter PageIndexParam = new SqlParameter() { ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageIndex };
                SqlParameter PageSizeParam = new SqlParameter() { ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageSize };
                SqlParameter AreaNameParam = new SqlParameter() { ParameterName = "@AreaName", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = AreaName };
                SqlParameter TotalRecordCountParam = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortColParam, SortDirParam, PageIndexParam, PageSizeParam, AreaNameParam, TotalRecordCountParam });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                TotalRecordCount = Convert.ToInt32(TotalRecordCountParam.Value);

                if (ResDataTable.Rows.Count > 0)
                {
                    AreaMasterListModel objAreaMaster;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objAreaMaster = new AreaMasterListModel();

                        objAreaMaster.RowNo = dtRowItem["RowNo"] != DBNull.Value ? Convert.ToInt32(dtRowItem["RowNo"]) : 0;
                        objAreaMaster.AreaId = dtRowItem["AreaId"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaId"]) : string.Empty;
                        objAreaMaster.AreaName = dtRowItem["AreaName"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaName"]) : string.Empty;
                        objAreaMaster.IsActive = dtRowItem["IsActive"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["IsActive"]) : false;

                        objAreaMasterListDataModel.AreaMasterList.Add(objAreaMaster);

                    }
                    objAreaMasterListDataModel.RecordCount = TotalRecordCount;
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objAreaMasterListDataModel;
        }

        public ResponceModel DeleteAreaById(string AreaId)
        {

            ResponceModel objResponceModel;

            User UserSesionDetail = SessionService.GetUserSessionValues();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"Update AreaMaster set IsDelete = 1 WHERE AreaId = @AreaId";

                lstParam = new List<SqlParameter>();

                SqlParameter AreaIdParam = new SqlParameter();

                if (!string.IsNullOrEmpty(AreaId))
                {
                    AreaIdParam = new SqlParameter() { ParameterName = "@AreaId", Value = AreaId.ToUpper() };
                }
                else
                {
                    AreaIdParam = new SqlParameter() { ParameterName = "@AreaId", Value = DBNull.Value };
                }

                lstParam.AddRange(new SqlParameter[] { AreaIdParam });

                objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                objResponceModel = new ResponceModel();

                objResponceModel.Message = "Area Name Deleted Succesfuly";
                objResponceModel.Responce = true;


            }
            catch (Exception ex)
            {
                objResponceModel = new ResponceModel();

                objResponceModel.Message = "Somthing Went Wrong";
                objResponceModel.Responce = false;

                CommonService.WriteErrorLog(ex);
            }

            return objResponceModel;

        }

        #endregion

        #region Item
        public ResponceModel InsertUpdateItem(string ItemId, string ItemName, string TechnicianId)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"InsertUpdateItemDetail";

                lstParam = new List<SqlParameter>();

                SqlParameter ItemIdParam = new SqlParameter();

                if (!string.IsNullOrEmpty(ItemId))
                {
                    ItemIdParam = new SqlParameter() { ParameterName = "@ItemId", Value = ItemId.ToUpper() };
                }
                else
                {
                    ItemIdParam = new SqlParameter() { ParameterName = "@ItemId", Value = DBNull.Value };
                }

                SqlParameter TechnicianIdParam = !string.IsNullOrEmpty(TechnicianId) ? new SqlParameter() { ParameterName = "@TechnicianId", Value =  TechnicianId.ToUpper() } : new SqlParameter() { ParameterName = "@TechnicianId", Value = DBNull.Value };
                SqlParameter ItemNameParam = new SqlParameter() { ParameterName = "@ItemName", Value = ItemName };
                SqlParameter LoginUserIdParam = new SqlParameter() { ParameterName = "@LoginUserId", Value = UserSesionDetail.id };

                lstParam.AddRange(new SqlParameter[] { ItemIdParam, ItemNameParam, TechnicianIdParam, LoginUserIdParam });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);


                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowItem = ResDataTable.Rows[0];

                    objResponceModel.Message = dtRowItem["ResponceMesage"] != DBNull.Value ? Convert.ToString(dtRowItem["ResponceMesage"]) : string.Empty;
                    objResponceModel.Responce = dtRowItem["OprationSuceess"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["OprationSuceess"]) : false;

                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objResponceModel;
        }

        public ItemMaster GetItemDetails(string ItemId)
        {
            ItemMaster objItemMaster = new ItemMaster();

            try
            {
                objBaseDAL = new BaseDAL();
                strQuery = @"select ItemId, ItemName from ItemMaster WHERE ItemId = @ItemId";

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                         {
                                new SqlParameter("@ItemId", new Guid(ItemId)),
                         });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowItem = ResDataTable.Rows[0];

                    objItemMaster.ItemId = dtRowItem["ItemId"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemId"]) : string.Empty;
                    objItemMaster.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objItemMaster;
        }

        public ItemMasterListDataModel GetItemList(int SortCol, string SortDir, int PageIndex, int PageSize, string ItemName)
        {
            ItemMasterListDataModel objItemMasterListDataModel = new ItemMasterListDataModel();
            objItemMasterListDataModel.ItemMasterList = new List<ItemMasterListModel>();

            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"ItemList";

                lstParam = new List<SqlParameter>();


                SqlParameter SortColParam = new SqlParameter() { ParameterName = "@SortCol", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = SortCol };
                SqlParameter SortDirParam = new SqlParameter() { ParameterName = "@SortDir", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = SortDir };
                SqlParameter PageIndexParam = new SqlParameter() { ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageIndex };
                SqlParameter PageSizeParam = new SqlParameter() { ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageSize };
                SqlParameter ItemNameParam = new SqlParameter() { ParameterName = "@ItemName", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = ItemName };
                SqlParameter TotalRecordCountParam = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortColParam, SortDirParam, PageIndexParam, PageSizeParam, ItemNameParam, TotalRecordCountParam });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                TotalRecordCount = Convert.ToInt32(TotalRecordCountParam.Value);

                if (ResDataTable.Rows.Count > 0)
                {
                    ItemMasterListModel objItemMaster;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objItemMaster = new ItemMasterListModel();

                        objItemMaster.RowNo = dtRowItem["RowNo"] != DBNull.Value ? Convert.ToInt32(dtRowItem["RowNo"]) : 0;
                        objItemMaster.ItemId = dtRowItem["ItemId"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemId"]) : string.Empty;
                        objItemMaster.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                        objItemMaster.IsActive = dtRowItem["IsActive"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["IsActive"]) : false;
                        objItemMaster.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;

                        objItemMasterListDataModel.ItemMasterList.Add(objItemMaster);

                    }
                    objItemMasterListDataModel.RecordCount = TotalRecordCount;
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objItemMasterListDataModel;
        }

        public ResponceModel DeleteItemById(string ItemId)
        {

            ResponceModel objResponceModel;

            User UserSesionDetail = SessionService.GetUserSessionValues();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"Update ItemMaster set IsDelete = 1 WHERE ItemId = @ItemId";

                lstParam = new List<SqlParameter>();

                SqlParameter ItemIdParam = new SqlParameter();

                if (!string.IsNullOrEmpty(ItemId))
                {
                    ItemIdParam = new SqlParameter() { ParameterName = "@ItemId", Value = ItemId.ToUpper() };
                }
                else
                {
                    ItemIdParam = new SqlParameter() { ParameterName = "@ItemId", Value = DBNull.Value };
                }

                lstParam.AddRange(new SqlParameter[] { ItemIdParam });

                objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                objResponceModel = new ResponceModel();

                objResponceModel.Message = "Item Name Deleted Succesfuly";
                objResponceModel.Responce = true;


            }
            catch (Exception ex)
            {
                objResponceModel = new ResponceModel();

                objResponceModel.Message = "Somthing Went Wrong";
                objResponceModel.Responce = false;

                CommonService.WriteErrorLog(ex);
            }

            return objResponceModel;

        }

        #endregion

        #region Technician


        public TechnicianListDataModel GetTechnicianList(int SortCol, string SortDir, int PageIndex, int PageSize, string TechnicianName, string MobileNo)
        {
            TechnicianListDataModel objTechnicianListDataModel = new TechnicianListDataModel();
            objTechnicianListDataModel.TechnicianList = new List<TechnicianMaster>();


            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"TechnicianList";

                lstParam = new List<SqlParameter>();

                SqlParameter SortColParam = new SqlParameter() { ParameterName = "@SortCol", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = SortCol };
                SqlParameter SortDirParam = new SqlParameter() { ParameterName = "@SortDir", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = SortDir };
                SqlParameter PageIndexParam = new SqlParameter() { ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageIndex };
                SqlParameter PageSizeParam = new SqlParameter() { ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageSize };
                SqlParameter TechnicianNameParam = new SqlParameter() { ParameterName = "@TechnicianName", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = TechnicianName };
                SqlParameter MobileNoParam = new SqlParameter() { ParameterName = "@MobileNo", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = MobileNo };
                SqlParameter TotalRecordCountParam = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortColParam, SortDirParam, PageIndexParam, PageSizeParam, TechnicianNameParam, MobileNoParam, TotalRecordCountParam });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                TotalRecordCount = Convert.ToInt32(TotalRecordCountParam.Value);

                if (ResDataTable.Rows.Count > 0)
                {
                    TechnicianMaster objTechnicianMaster;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objTechnicianMaster = new TechnicianMaster();

                        objTechnicianMaster.RowNo = dtRowItem["RowNo"] != DBNull.Value ? Convert.ToInt64(dtRowItem["RowNo"]) : 0;
                        objTechnicianMaster.Oid = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        objTechnicianMaster.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                        objTechnicianMaster.IsActive = dtRowItem["IsActive"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["IsActive"]): false;
                        objTechnicianMaster.City = dtRowItem["City"] != DBNull.Value ? Convert.ToString(dtRowItem["City"]) : string.Empty;
                        objTechnicianMaster.CityName = dtRowItem["CityName"] != DBNull.Value ? Convert.ToString(dtRowItem["CityName"]) : string.Empty;
                        objTechnicianMaster.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                        objTechnicianMaster.TechTypeName = dtRowItem["TechTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["TechTypeName"]) : string.Empty;
                        objTechnicianMaster.PhoneH = dtRowItem["PhoneH"] != DBNull.Value ? Convert.ToString(dtRowItem["PhoneH"]) : string.Empty;
                        objTechnicianMaster.PhoneO = dtRowItem["PhoneO"] != DBNull.Value ? Convert.ToString(dtRowItem["PhoneO"]) : string.Empty;
                        objTechnicianMaster.Mobile = dtRowItem["Mobile"] != DBNull.Value ? Convert.ToString(dtRowItem["Mobile"]) : string.Empty;

                        objTechnicianListDataModel.TechnicianList.Add(objTechnicianMaster);

                    }
                    objTechnicianListDataModel.RecordCount = TotalRecordCount;
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objTechnicianListDataModel;
        }


        public TechnicianMaster GetTechnicianDetailById(string TechnicianId)
        {
            TechnicianMaster objTechnicianMaster = new TechnicianMaster();

            try
            {
                objBaseDAL = new BaseDAL();
                strQuery = @"SELECT
	                            T.Oid,
	                            T.Technician,
                                T.Address,
	                            T.IsActive,
	                            T.City,
	                            C.CityName,
	                            T.PhoneH,
	                            T.PhoneO,
	                            T.Mobile,
	                            T.L1,
	                            T.L2,
	                            T.TechType,
	                            T.JobType,
	                            T.OptimisticLockField,
	                            T.GCRecord,
	                            T.locationtime,
	                            T.device_token,
	                            T.UserName,
	                            T.Modifier,
	                            T.CreationDate,
	                            T.ModifyDate
                            From	
	                            Technician T LEFT JOIN City C ON T.City = C.Oid
                            WHERE 
	                            T.Oid = @TechnicianId";

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                         {
                                new SqlParameter("@TechnicianId", new Guid(TechnicianId)),
                         });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowItem = ResDataTable.Rows[0];

                    objTechnicianMaster.Oid = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                    objTechnicianMaster.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                    objTechnicianMaster.Address = dtRowItem["Address"] != DBNull.Value ? Convert.ToString(dtRowItem["Address"]) : string.Empty;

                    objTechnicianMaster.IsActive = dtRowItem["IsActive"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["IsActive"]) : false;
                    objTechnicianMaster.City = dtRowItem["City"] != DBNull.Value ? Convert.ToString(dtRowItem["City"]) : string.Empty;
                    objTechnicianMaster.CityName = dtRowItem["CityName"] != DBNull.Value ? Convert.ToString(dtRowItem["CityName"]) : string.Empty;
                    objTechnicianMaster.PhoneH = dtRowItem["PhoneH"] != DBNull.Value ? Convert.ToString(dtRowItem["PhoneH"]) : string.Empty;
                    objTechnicianMaster.PhoneO = dtRowItem["PhoneO"] != DBNull.Value ? Convert.ToString(dtRowItem["PhoneO"]) : string.Empty;
                    objTechnicianMaster.Mobile = dtRowItem["Mobile"] != DBNull.Value ? Convert.ToString(dtRowItem["Mobile"]) : string.Empty;
                    objTechnicianMaster.L1 = dtRowItem["L1"] != DBNull.Value ? Convert.ToString(dtRowItem["L1"]) : string.Empty;
                    objTechnicianMaster.L2 = dtRowItem["L2"] != DBNull.Value ? Convert.ToString(dtRowItem["L2"]) : string.Empty;
                    objTechnicianMaster.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                    objTechnicianMaster.TechTypeName = dtRowItem["TechTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["TechTypeName"]) : string.Empty;
                    objTechnicianMaster.JobType = dtRowItem["JobType"] != DBNull.Value ? Convert.ToString(dtRowItem["JobType"]) : string.Empty;
                    objTechnicianMaster.OptimisticLockField = dtRowItem["OptimisticLockField"] != DBNull.Value ? Convert.ToInt32(dtRowItem["OptimisticLockField"]) : 0;

                    objTechnicianMaster.GCRecord = dtRowItem["GCRecord"] != DBNull.Value ? Convert.ToInt32(dtRowItem["GCRecord"]) : 0;
                    objTechnicianMaster.locationtime = dtRowItem["locationtime"] != DBNull.Value ? Convert.ToString(dtRowItem["locationtime"]) : string.Empty;
                    objTechnicianMaster.device_token = dtRowItem["device_token"] != DBNull.Value ? Convert.ToString(dtRowItem["device_token"]) : string.Empty;
                    objTechnicianMaster.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;
                    objTechnicianMaster.Modifier = dtRowItem["Modifier"] != DBNull.Value ? Convert.ToString(dtRowItem["Modifier"]) : string.Empty;
                    objTechnicianMaster.CreationDate = dtRowItem["CreationDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CreationDate"]) : (DateTime?)null;
                    objTechnicianMaster.ModifyDate = dtRowItem["ModifyDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ModifyDate"]) : (DateTime?)null;

                    TechnicianMasterSelect2Data objTechnicianMasterSelect2Data = new TechnicianMasterSelect2Data();
                    objTechnicianMasterSelect2Data.Select2City = new Select2() { id = objTechnicianMaster.City, text = objTechnicianMaster.CityName };

                    objTechnicianMaster.Select2JSON = JsonConvert.SerializeObject(objTechnicianMasterSelect2Data);
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objTechnicianMaster;

        }



        public TechnicianMaster AdUpdateTechnicianDetail(TechnicianMaster ObjTechnician)
        {
            TechnicianMaster ObjTechnicianResponce = new TechnicianMaster();

            if (ObjTechnician != null)
            {
                User UserSesionDetail = SessionService.GetUserSessionValues();


                try
                {
                    objBaseDAL = new BaseDAL();
                    strQuery = @"AddUpdateTechnicianDetail";

                    lstParam = new List<SqlParameter>();


                    SqlParameter Oid_Param = !string.IsNullOrEmpty(ObjTechnician.Oid) ? new SqlParameter() { ParameterName = "@Oid", Value = ObjTechnician.Oid } : new SqlParameter() { ParameterName = "@Oid", Value = DBNull.Value };
                    SqlParameter Technician_Param = !string.IsNullOrEmpty(ObjTechnician.Technician) ? new SqlParameter() { ParameterName = "@Technician", Value = ObjTechnician.Technician } : new SqlParameter() { ParameterName = "@Technician", Value = DBNull.Value };
                    SqlParameter IsActive_Param = new SqlParameter() { ParameterName = "@IsActive", Value = ObjTechnician.IsActive };
                    SqlParameter Address_Param = !string.IsNullOrEmpty(ObjTechnician.Address) ? new SqlParameter() { ParameterName = "@Address", Value = ObjTechnician.Address } : new SqlParameter() { ParameterName = "@Address", Value = DBNull.Value };
                    SqlParameter City_Param = !string.IsNullOrEmpty(ObjTechnician.City) ? new SqlParameter() { ParameterName = "@City", Value = ObjTechnician.City } : new SqlParameter() { ParameterName = "@City", Value = DBNull.Value };
                    SqlParameter L1_Param = !string.IsNullOrEmpty(ObjTechnician.L1) ? new SqlParameter() { ParameterName = "@L1", Value = ObjTechnician.L1 } : new SqlParameter() { ParameterName = "@L1", Value = DBNull.Value };
                    SqlParameter L2_Param = !string.IsNullOrEmpty(ObjTechnician.L2) ? new SqlParameter() { ParameterName = "@L2", Value = ObjTechnician.L2 } : new SqlParameter() { ParameterName = "@L2", Value = DBNull.Value };
                    SqlParameter PhoneH_Param = !string.IsNullOrEmpty(ObjTechnician.PhoneH) ? new SqlParameter() { ParameterName = "@PhoneH", Value = ObjTechnician.PhoneH } : new SqlParameter() { ParameterName = "@PhoneH", Value = DBNull.Value };
                    SqlParameter PhoneO_Param = !string.IsNullOrEmpty(ObjTechnician.PhoneO) ? new SqlParameter() { ParameterName = "@PhoneO", Value = ObjTechnician.PhoneO } : new SqlParameter() { ParameterName = "@PhoneO", Value = DBNull.Value };
                    SqlParameter Mobile_Param = !string.IsNullOrEmpty(ObjTechnician.Mobile) ? new SqlParameter() { ParameterName = "@Mobile", Value = ObjTechnician.Mobile } : new SqlParameter() { ParameterName = "@Mobile", Value = DBNull.Value };
                    SqlParameter TechType_Param = !string.IsNullOrEmpty(ObjTechnician.TechType) ? new SqlParameter() { ParameterName = "@TechType", Value = ObjTechnician.TechType } : new SqlParameter() { ParameterName = "@TechType", Value = DBNull.Value };
                    SqlParameter JobType_Param = !string.IsNullOrEmpty(ObjTechnician.JobType) ? new SqlParameter() { ParameterName = "@JobType", Value = ObjTechnician.JobType } : new SqlParameter() { ParameterName = "@JobType", Value = DBNull.Value };
                    SqlParameter OptimisticLockField_Param = new SqlParameter() { ParameterName = "@OptimisticLockField", Value = ObjTechnician.OptimisticLockField };
                    SqlParameter GCRecord_Param = new SqlParameter() { ParameterName = "@GCRecord", Value = ObjTechnician.GCRecord };
                    SqlParameter locationtime_Param = !string.IsNullOrEmpty(ObjTechnician.locationtime) ? new SqlParameter() { ParameterName = "@locationtime", Value = ObjTechnician.locationtime } : new SqlParameter() { ParameterName = "@locationtime", Value = DBNull.Value };
                    SqlParameter device_token_Param = !string.IsNullOrEmpty(ObjTechnician.device_token) ? new SqlParameter() { ParameterName = "@device_token", Value = ObjTechnician.device_token } : new SqlParameter() { ParameterName = "@device_token", Value = DBNull.Value };
                    SqlParameter LoginUserId_Param = UserSesionDetail != null  ? new SqlParameter() { ParameterName = "@LoginUserId", Value = UserSesionDetail.id } : new SqlParameter() { ParameterName = "@LoginUserId", Value = 1 };
                    SqlParameter TechnicianId_Param = new SqlParameter() { ParameterName = "@TechnicianId", SqlDbType = SqlDbType.UniqueIdentifier, Direction = ParameterDirection.Output };

                    lstParam.AddRange(new SqlParameter[] { Oid_Param, Technician_Param, IsActive_Param, Address_Param, City_Param, L1_Param, L2_Param, PhoneH_Param, PhoneO_Param, Mobile_Param, TechType_Param , JobType_Param, OptimisticLockField_Param, GCRecord_Param, locationtime_Param, device_token_Param, LoginUserId_Param, TechnicianId_Param });

                    DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                    if (ResDataTable.Rows.Count > 0)
                    {
                        ObjTechnicianResponce = new TechnicianMaster();

                        DataRow dtRowItem = ResDataTable.Rows[0];

                        ObjTechnicianResponce.Responce = dtRowItem["OperationSuccess"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["OperationSuccess"]) : false;
                        ObjTechnicianResponce.Message = dtRowItem["ResponceMessage"] != DBNull.Value ? Convert.ToString(dtRowItem["ResponceMessage"]) : string.Empty;
                    }

                }
                catch (Exception ex)
                {
                    ObjTechnicianResponce = new TechnicianMaster() { Responce = false, Message = "Something went wrong" };
                    CommonService.WriteErrorLog(ex);
                }
            }

            return ObjTechnicianResponce;
        }

        public List<SelectListItem> GetTechnicianTypeList(string TechnicianTypeIdParam = "")
        {
            List<SelectListItem> lstTechnicianType = new List<SelectListItem>();

            try
            {
                objBaseDAL = new BaseDAL();
                strQuery = @"select Oid, TechnicianType From TechnicianType ORDER BY TechnicianType ASC";

                lstParam = new List<SqlParameter>();

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    SelectListItem objSelectListItem;

                    string TechnicianTypeId = string.Empty;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objSelectListItem = new SelectListItem();

                        TechnicianTypeId = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;

                        objSelectListItem.Value = TechnicianTypeId;
                        objSelectListItem.Text = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;
                        objSelectListItem.Selected = TechnicianTypeId == TechnicianTypeIdParam ? true : false;

                        lstTechnicianType.Add(objSelectListItem);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return lstTechnicianType;
        }

        #endregion
    }
}