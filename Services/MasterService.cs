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
        public ResponceModel InsertUpdateArea(AreaMaster objAreaMaster)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();

            if(objAreaMaster != null && !string.IsNullOrEmpty(objAreaMaster.AreaName) && !string.IsNullOrEmpty(objAreaMaster.AreaPincode))
            {

                try
                {
                    objBaseDAL = new BaseDAL();

                    strQuery = @"InsertUpdateAreaDetail";

                    lstParam = new List<SqlParameter>();

                    SqlParameter AreaIdParam = new SqlParameter();

                    if (!string.IsNullOrEmpty(objAreaMaster.AreaId))
                    {
                        AreaIdParam = new SqlParameter() { ParameterName = "@AreaId", Value = objAreaMaster.AreaId.ToUpper() };
                    }
                    else
                    {
                        AreaIdParam = new SqlParameter() { ParameterName = "@AreaId", Value = DBNull.Value };
                    }


                    SqlParameter AreaNameParam = new SqlParameter() { ParameterName = "@AreaName", Value = objAreaMaster.AreaName };
                    SqlParameter AreaPincodeParam = new SqlParameter() { ParameterName = "@AreaPincode", Value = objAreaMaster.AreaPincode };
                    SqlParameter LoginUserIdParam = new SqlParameter() { ParameterName = "@LoginUserId", Value = UserSesionDetail.id };

                    lstParam.AddRange(new SqlParameter[] { AreaIdParam, AreaNameParam, AreaPincodeParam, LoginUserIdParam });

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

            }
            else
            {
                objResponceModel = new ResponceModel();

                objResponceModel.Message = "Please Enter proper data";
                objResponceModel.Responce = false;

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

        public AreaMasterListDataModel GetAreaList(int SortCol, string SortDir, int PageIndex, int PageSize, string AreaName, string AreaPincode)
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
                SqlParameter AreaNameParam = !string.IsNullOrEmpty(AreaName) ? new SqlParameter() { ParameterName = "@AreaName", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = AreaName } : new SqlParameter() { ParameterName = "@AreaName", Value = DBNull.Value }; ;
                SqlParameter AreaPincodeParam = !string.IsNullOrEmpty(AreaPincode) ? new SqlParameter() { ParameterName = "@AreaPincode", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = AreaPincode } : new SqlParameter() { ParameterName = "@AreaPincode", Value = DBNull.Value }; ;
                SqlParameter TotalRecordCountParam = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortColParam, SortDirParam, PageIndexParam, PageSizeParam, AreaNameParam, AreaPincodeParam, TotalRecordCountParam });

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
                        objAreaMaster.AreaPincode = dtRowItem["AreaPincode"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaPincode"]) : string.Empty;
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
        public ResponceModel InsertUpdateItem(ItemMaster objItemMaster)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"InsertUpdateItemDetail";

                lstParam = new List<SqlParameter>();

                SqlParameter ItemIdParam = new SqlParameter();

                if (!string.IsNullOrEmpty(objItemMaster.ItemId))
                {
                    ItemIdParam = new SqlParameter() { ParameterName = "@ItemId", Value = objItemMaster.ItemId.ToUpper() };
                }
                else
                {
                    ItemIdParam = new SqlParameter() { ParameterName = "@ItemId", Value = DBNull.Value };
                }

                SqlParameter TechnicianIdParam = !string.IsNullOrEmpty(objItemMaster.TechnicianId) ? new SqlParameter() { ParameterName = "@TechnicianId", Value = objItemMaster.TechnicianId.ToUpper() } : new SqlParameter() { ParameterName = "@TechnicianId", Value = DBNull.Value };
                SqlParameter ItemNameParam = new SqlParameter() { ParameterName = "@ItemName", Value = objItemMaster.ItemName };
                SqlParameter ItemKeywordParam = new SqlParameter() { ParameterName = "@ItemKeyword", Value = objItemMaster.ItemKeyword };
                SqlParameter LoginUserIdParam = new SqlParameter() { ParameterName = "@LoginUserId", Value = UserSesionDetail.id };

                lstParam.AddRange(new SqlParameter[] { ItemIdParam, ItemNameParam, ItemKeywordParam, TechnicianIdParam, LoginUserIdParam });

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
                strQuery = @"select ItemId, ItemName, ItemKeyword from ItemMaster WHERE ItemId = @ItemId";

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
                    objItemMaster.ItemKeyword = dtRowItem["ItemKeyword"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemKeyword"]) : string.Empty;
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objItemMaster;
        }

        public ItemMasterListDataModel GetItemList(int SortCol, string SortDir, int PageIndex, int PageSize, string ItemName, string ItemKeyword)
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
                SqlParameter ItemKeywordParam = !string.IsNullOrEmpty(ItemKeyword) ? new SqlParameter() { ParameterName = "@ItemKeyword", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = ItemKeyword } : new SqlParameter() { ParameterName = "@ItemKeyword", Value = DBNull.Value };
                SqlParameter TotalRecordCountParam = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortColParam, SortDirParam, PageIndexParam, PageSizeParam, ItemNameParam, ItemKeywordParam, TotalRecordCountParam });

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
                        objItemMaster.ItemKeyword = dtRowItem["ItemKeyword"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemKeyword"]) : string.Empty;
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
                                TT.TechnicianType AS TechTypeName,
                                T.JobType,
                                T.OptimisticLockField,
                                T.GCRecord,
                                T.locationtime,
                                T.device_token,
                                T.UserName,
                                T.Modifier,
                                T.CreationDate,
                                T.ModifyDate,
                                R.[password]
                            From	
                                Technician T LEFT JOIN City C ON T.City = C.Oid
                                LEFT JOIN TechnicianType TT ON TT.Oid = T.TechType
                                LEFT JOIN register R ON R.Technician = T.Oid
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
                    objTechnicianMaster.Password = dtRowItem["password"] != DBNull.Value ? Convert.ToString(dtRowItem["password"]) : string.Empty;

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
                    SqlParameter TechnicianLoginUserName_Param = new SqlParameter() { ParameterName = "@TechnicianLoginUserName", SqlDbType = SqlDbType.VarChar, Value = ObjTechnician.Mobile };
                    SqlParameter Password_Param = new SqlParameter() { ParameterName = "@Password", SqlDbType = SqlDbType.VarChar, Value = ObjTechnician.Password };

                    lstParam.AddRange(new SqlParameter[] { Oid_Param, Technician_Param, IsActive_Param, Address_Param, City_Param, L1_Param, L2_Param, PhoneH_Param, PhoneO_Param, Mobile_Param, TechType_Param , JobType_Param, OptimisticLockField_Param, GCRecord_Param, locationtime_Param, device_token_Param, LoginUserId_Param, TechnicianId_Param, TechnicianLoginUserName_Param, Password_Param });

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

        public ResponceModel UpdateTechnicianStatusById(string TechnicianId, bool Status)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();
            string Modifier = UserSesionDetail != null ? UserSesionDetail.UserName : "";

            bool IsActive = !Status;

            if (!string.IsNullOrEmpty(TechnicianId))
            {

                try
                {
                    strQuery = "UPDATE Technician SET IsActive = @IsActive, Modifier = @Modifier, ModifyDate = GETDATE() WHERE Oid = @TechnicianId";


                    objBaseDAL = new BaseDAL();

                    lstParam = new List<SqlParameter>();

                    lstParam.AddRange(new SqlParameter[]
                          {
                                new SqlParameter("@TechnicianId", TechnicianId.ToUpper()),
                                new SqlParameter("@IsActive", IsActive),
                                new SqlParameter("@Modifier", Modifier),
                          });

                    objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                    objResponceModel.Responce = true;
                    objResponceModel.Message = "Technician Detail Updated";


                }
                catch (Exception ex)
                {
                    CommonService.WriteErrorLog(ex);

                    objResponceModel = new ResponceModel();
                    objResponceModel.Responce = false;
                    objResponceModel.Message = "Somthing Went Wrong!";
                }

            }
            else
            {
                objResponceModel.Responce = false;
                objResponceModel.Message = "No Technician found!";
            }

            return objResponceModel;

        }


        public TechnicianAttendanceListDataModel TechnicianAttendanceList(int SortCol, string SortDir, int PageIndex, int PageSize, string TechnicianName, DateTime? FromDate, DateTime? ToDate)
        {
            TechnicianAttendanceListDataModel objTechnicianAttendanceListDataModel = new TechnicianAttendanceListDataModel();
            objTechnicianAttendanceListDataModel.TechnicianAttendanceList = new List<TechnicianAttendance>();


            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"TechnicianAttendance";

                lstParam = new List<SqlParameter>();

                SqlParameter SortCol_Param = new SqlParameter() { ParameterName = "@SortCol", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = SortCol };
                SqlParameter SortDir_Param = new SqlParameter() { ParameterName = "@SortDir", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = SortDir };
                SqlParameter PageIndex_Param = new SqlParameter() { ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageIndex };
                SqlParameter PageSize_Param = new SqlParameter() { ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageSize };
                SqlParameter TechnicianName_Param = new SqlParameter() { ParameterName = "@TechnicianName", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = TechnicianName };
                SqlParameter TotalRecordCount_Param = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { SortCol_Param, SortDir_Param, PageIndex_Param, PageSize_Param, TechnicianName_Param, FromDate_Param, ToDate_Param, TotalRecordCount_Param });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                TotalRecordCount = Convert.ToInt32(TotalRecordCount_Param.Value);

                if (ResDataTable.Rows.Count > 0)
                {
                    TechnicianAttendance objTechnicianAttendance;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objTechnicianAttendance = new TechnicianAttendance();

                        objTechnicianAttendance.RowNo = dtRowItem["RowNo"] != DBNull.Value ? Convert.ToInt32(dtRowItem["RowNo"]) : 0;
                        objTechnicianAttendance.technician_name = dtRowItem["technician_name"] != DBNull.Value ? Convert.ToString(dtRowItem["technician_name"]) : string.Empty;
                        objTechnicianAttendance.attendance_date = dtRowItem["attendance_date"] != DBNull.Value ? Convert.ToString(dtRowItem["attendance_date"]) : string.Empty;
                        objTechnicianAttendance.present_location = dtRowItem["present_location"] != DBNull.Value ? Convert.ToString(dtRowItem["present_location"]) : string.Empty;

                        objTechnicianAttendance.technician_tranning = dtRowItem["technician_tranning"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["technician_tranning"]) : false;

                        objTechnicianAttendance.lunch_starttime = dtRowItem["lunch_starttime"] != DBNull.Value ? Convert.ToString(dtRowItem["lunch_starttime"]) : string.Empty;

                        objTechnicianAttendance.lunch_endtime = dtRowItem["lunch_endtime"] != DBNull.Value ? Convert.ToString(dtRowItem["lunch_endtime"]) : string.Empty;
                        objTechnicianAttendance.endtime = dtRowItem["endtime"] != DBNull.Value ? Convert.ToString(dtRowItem["endtime"]) : string.Empty;
                        

                        objTechnicianAttendanceListDataModel.TechnicianAttendanceList.Add(objTechnicianAttendance);

                    }
                    objTechnicianAttendanceListDataModel.RecordCount = TotalRecordCount;
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objTechnicianAttendanceListDataModel;
        }


        public List<Select2> GetTechnicianNameList(string TechnicianName)
        {
            List<Select2> lstUser = new List<Select2>();

            try
            {
                objBaseDAL = new BaseDAL();


                if (!string.IsNullOrEmpty(TechnicianName))
                {
                    strQuery = @"SELECT 
	                                    DISTINCT(technician_name) AS technician_name 
                                    FROM 
	                                    attendance_tbl 
                                    WHERE 
	                                    technician_name LIKE CONCAT('%', @TechnicianName,'%') 
                                    ORDER BY technician_name ASC  ";

                }
                else
                {
                    strQuery = @"SELECT 
	                                    DISTINCT(technician_name) AS technician_name 
                                    FROM 
	                                    attendance_tbl 
                                    ORDER BY technician_name ASC";
                }

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@TechnicianName", TechnicianName),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    Select2 objSelect2;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objSelect2 = new Select2();

                        objSelect2.id = dtRowItem["technician_name"] != DBNull.Value ? Convert.ToString(dtRowItem["technician_name"]) : string.Empty;
                        objSelect2.text = dtRowItem["technician_name"] != DBNull.Value ? Convert.ToString(dtRowItem["technician_name"]) : string.Empty;

                        lstUser.Add(objSelect2);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return lstUser;
        }

        #endregion

        #region IP Address


        public IPAddressMasterListDataModel GetIPAddressList(int SortCol, string SortDir, int PageIndex, int PageSize, string IPAddress)
        {
            IPAddressMasterListDataModel objIPAddressListDataModel = new IPAddressMasterListDataModel();
            objIPAddressListDataModel.IPAddressMasterList = new List<clsIPAddress>();

            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"IPAddressList";

                lstParam = new List<SqlParameter>();


                SqlParameter SortCol_Param = new SqlParameter() { ParameterName = "@SortCol", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = SortCol };
                SqlParameter SortDir_Param = new SqlParameter() { ParameterName = "@SortDir", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = SortDir };
                SqlParameter PageIndex_Param = new SqlParameter() { ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageIndex };
                SqlParameter PageSize_Param = new SqlParameter() { ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageSize };
                SqlParameter IPAddress_Param = new SqlParameter() { ParameterName = "@IPAddress", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = IPAddress };
                SqlParameter TotalRecordCount_Param = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortCol_Param, SortDir_Param, PageIndex_Param, PageSize_Param, IPAddress_Param, TotalRecordCount_Param });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                TotalRecordCount = Convert.ToInt32(TotalRecordCount_Param.Value);

                if (ResDataTable.Rows.Count > 0)
                {
                    clsIPAddress objIPAddress;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objIPAddress = new clsIPAddress();

                        objIPAddress.RowNo = dtRowItem["RowNo"] != DBNull.Value ? Convert.ToInt32(dtRowItem["RowNo"]) : 0;
                        objIPAddress.Id = dtRowItem["Id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["Id"]) : 0;
                        objIPAddress.IP = dtRowItem["IP"] != DBNull.Value ? Convert.ToString(dtRowItem["IP"]) : string.Empty;
                        objIPAddress.IsActive = dtRowItem["IsActive"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["IsActive"]) : false;

                        objIPAddressListDataModel.IPAddressMasterList.Add(objIPAddress);

                    }
                    objIPAddressListDataModel.RecordCount = TotalRecordCount;
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objIPAddressListDataModel;
        }


        public clsIPAddress GetIPAddressDetails(string IPAddressId)
        {
            clsIPAddress objIPAddress = new clsIPAddress();

            try
            {
                objBaseDAL = new BaseDAL();
                strQuery = @"select Id, IP from IPAddress WHERE Id = @IPAddressId";

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                         {
                                new SqlParameter("@IPAddressId", IPAddressId),
                         });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowItem = ResDataTable.Rows[0];

                    objIPAddress.Id = dtRowItem["Id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["Id"]) : 0;
                    objIPAddress.IP = dtRowItem["IP"] != DBNull.Value ? Convert.ToString(dtRowItem["IP"]) : string.Empty;
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objIPAddress;
        }


        public ResponceModel InsertUpdateIPAddress(int IPAddressId, string IPAddress)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"InsertUpdateIPAddress";

                lstParam = new List<SqlParameter>();

                SqlParameter ItemIdParam = new SqlParameter();

                if (IPAddressId > 0)
                {
                    ItemIdParam = new SqlParameter() { ParameterName = "@IPAddressId", Value = IPAddressId };
                }
                else
                {
                    ItemIdParam = new SqlParameter() { ParameterName = "@IPAddressId", Value = DBNull.Value };
                }

                SqlParameter IPAddressId_Param = IPAddressId > 0 ? new SqlParameter() { ParameterName = "@IPAddressId", Value = IPAddressId } : new SqlParameter() { ParameterName = "@IPAddressId", Value = DBNull.Value };
                SqlParameter IPAddress_Param = new SqlParameter() { ParameterName = "@IPAddress", Value = IPAddress };
                SqlParameter LoginUserId_Param = new SqlParameter() { ParameterName = "@LoginUserId", Value = UserSesionDetail.id };

                lstParam.AddRange(new SqlParameter[] { IPAddressId_Param, IPAddress_Param, LoginUserId_Param });

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

        public ResponceModel DeleteIPAddressById(int IPaddressId)
        {

            ResponceModel objResponceModel;

            User UserSesionDetail = SessionService.GetUserSessionValues();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"Update IPAddress set IsDelete = 1 WHERE Id = @IPaddressId";

                lstParam = new List<SqlParameter>();

                SqlParameter ItemIdParam = new SqlParameter();

                if (IPaddressId > 0)
                {
                    ItemIdParam = new SqlParameter() { ParameterName = "@IPaddressId", Value = IPaddressId };
                }
                else
                {
                    ItemIdParam = new SqlParameter() { ParameterName = "@IPaddressId", Value = DBNull.Value };
                }

                lstParam.AddRange(new SqlParameter[] { ItemIdParam });

                objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                objResponceModel = new ResponceModel();

                objResponceModel.Message = "IP Address Deleted Succesfuly";
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

        #region Part
        public ResponceModel InsertUpdatePart(PartMaster objPartMaster)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"InsertUpdatePartDetail";

                lstParam = new List<SqlParameter>();

                SqlParameter PartId_Param = new SqlParameter();

                if (!string.IsNullOrEmpty(objPartMaster.PartId))
                {
                    PartId_Param = new SqlParameter() { ParameterName = "@PartId", Value = objPartMaster.PartId.ToUpper() };
                }
                else
                {
                    PartId_Param = new SqlParameter() { ParameterName = "@PartId", Value = DBNull.Value };
                }

                SqlParameter Company_Param = !string.IsNullOrEmpty(objPartMaster.Company) ? new SqlParameter() { ParameterName = "@Company", Value = objPartMaster.Company } : new SqlParameter() { ParameterName = "@TechnicianId", Value = DBNull.Value };
                SqlParameter PartName_Param = new SqlParameter() { ParameterName = "@PartName", Value = objPartMaster.PartName };
                SqlParameter PartKeyword_Param = new SqlParameter() { ParameterName = "@PartKeyword", Value = objPartMaster.PartKeyword };
                SqlParameter LoginUserId_Param = new SqlParameter() { ParameterName = "@LoginUserId", Value = UserSesionDetail.id };

                lstParam.AddRange(new SqlParameter[] { PartId_Param, PartName_Param, PartKeyword_Param, Company_Param, LoginUserId_Param });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);


                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowPart = ResDataTable.Rows[0];

                    objResponceModel.Message = dtRowPart["ResponceMesage"] != DBNull.Value ? Convert.ToString(dtRowPart["ResponceMesage"]) : string.Empty;
                    objResponceModel.Responce = dtRowPart["OprationSuceess"] != DBNull.Value ? Convert.ToBoolean(dtRowPart["OprationSuceess"]) : false;

                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objResponceModel;
        }

        public PartMaster GetPartDetails(string PartId)
        {
            PartMaster objPartMaster = new PartMaster();

            try
            {
                objBaseDAL = new BaseDAL();
                strQuery = @"select PartId, PartName, PartKeyword, Company from PartMaster WHERE PartId = @PartId";

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                         {
                                new SqlParameter("@PartId", new Guid(PartId)),
                         });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowPart = ResDataTable.Rows[0];

                    objPartMaster.PartId = dtRowPart["PartId"] != DBNull.Value ? Convert.ToString(dtRowPart["PartId"]) : string.Empty;
                    objPartMaster.PartName = dtRowPart["PartName"] != DBNull.Value ? Convert.ToString(dtRowPart["PartName"]) : string.Empty;
                    objPartMaster.PartKeyword = dtRowPart["PartKeyword"] != DBNull.Value ? Convert.ToString(dtRowPart["PartKeyword"]) : string.Empty;
                    objPartMaster.Company = dtRowPart["Company"] != DBNull.Value ? Convert.ToString(dtRowPart["Company"]) : string.Empty;
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objPartMaster;
        }

        public PartMasterListDataModel GetPartList(int SortCol, string SortDir, int PageIndex, int PageSize, string PartName, string PartKeyword)
        {
            PartMasterListDataModel objPartMasterListDataModel = new PartMasterListDataModel();
            objPartMasterListDataModel.PartMasterList = new List<PartMasterListModel>();

            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"PartList";

                lstParam = new List<SqlParameter>();


                SqlParameter SortColParam = new SqlParameter() { ParameterName = "@SortCol", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = SortCol };
                SqlParameter SortDirParam = new SqlParameter() { ParameterName = "@SortDir", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = SortDir };
                SqlParameter PageIndexParam = new SqlParameter() { ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageIndex };
                SqlParameter PageSizeParam = new SqlParameter() { ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageSize };
                SqlParameter PartNameParam = new SqlParameter() { ParameterName = "@PartName", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = PartName };
                SqlParameter PartKeywordParam = !string.IsNullOrEmpty(PartKeyword) ? new SqlParameter() { ParameterName = "@PartKeyword", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = PartKeyword } : new SqlParameter() { ParameterName = "@PartKeyword", Value = DBNull.Value };
                SqlParameter TotalRecordCountParam = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortColParam, SortDirParam, PageIndexParam, PageSizeParam, PartNameParam, PartKeywordParam, TotalRecordCountParam });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                TotalRecordCount = Convert.ToInt32(TotalRecordCountParam.Value);

                if (ResDataTable.Rows.Count > 0)
                {
                    PartMasterListModel objPartMaster;

                    foreach (DataRow dtRowPart in ResDataTable.Rows)
                    {
                        objPartMaster = new PartMasterListModel();

                        objPartMaster.RowNo = dtRowPart["RowNo"] != DBNull.Value ? Convert.ToInt32(dtRowPart["RowNo"]) : 0;
                        objPartMaster.PartId = dtRowPart["PartId"] != DBNull.Value ? Convert.ToString(dtRowPart["PartId"]) : string.Empty;
                        objPartMaster.PartName = dtRowPart["PartName"] != DBNull.Value ? Convert.ToString(dtRowPart["PartName"]) : string.Empty;
                        objPartMaster.PartKeyword = dtRowPart["PartKeyword"] != DBNull.Value ? Convert.ToString(dtRowPart["PartKeyword"]) : string.Empty;
                        objPartMaster.IsActive = dtRowPart["IsActive"] != DBNull.Value ? Convert.ToBoolean(dtRowPart["IsActive"]) : false;
                        objPartMaster.Company = dtRowPart["Company"] != DBNull.Value ? Convert.ToString(dtRowPart["Company"]) : string.Empty;

                        objPartMasterListDataModel.PartMasterList.Add(objPartMaster);

                    }
                    objPartMasterListDataModel.RecordCount = TotalRecordCount;
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objPartMasterListDataModel;
        }

        public ResponceModel DeletePartById(string PartId)
        {

            ResponceModel objResponceModel;

            User UserSesionDetail = SessionService.GetUserSessionValues();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"Update PartMaster set IsDelete = 1 WHERE PartId = @PartId";

                lstParam = new List<SqlParameter>();

                SqlParameter PartIdParam = new SqlParameter();

                if (!string.IsNullOrEmpty(PartId))
                {
                    PartIdParam = new SqlParameter() { ParameterName = "@PartId", Value = PartId.ToUpper() };
                }
                else
                {
                    PartIdParam = new SqlParameter() { ParameterName = "@PartId", Value = DBNull.Value };
                }

                lstParam.AddRange(new SqlParameter[] { PartIdParam });

                objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                objResponceModel = new ResponceModel();

                objResponceModel.Message = "Part Name Deleted Succesfuly";
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

        public ResponceModel UpdatePartStatusById(string PartId, bool Status)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();
            int Modifier = UserSesionDetail != null ? UserSesionDetail.id : 1;

            bool IsActive = !Status;

            if(!string.IsNullOrEmpty(PartId))
            {

                try
                {
                    strQuery = "UPDATE PartMaster SET IsActive = @IsActive, ModifiedBy = @Modifier, ModifiedDate = GETDATE() WHERE PartId = @PartId";


                    objBaseDAL = new BaseDAL();

                    lstParam = new List<SqlParameter>();

                    lstParam.AddRange(new SqlParameter[]
                          {
                                new SqlParameter("@PartId", PartId.ToUpper()),
                                new SqlParameter("@IsActive", IsActive),
                                new SqlParameter("@Modifier", Modifier),
                          });

                    objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                    objResponceModel.Responce = true;
                    objResponceModel.Message = "Part Detail Updated";


                }
                catch (Exception ex)
                {
                    CommonService.WriteErrorLog(ex);

                    objResponceModel = new ResponceModel();
                    objResponceModel.Responce = false;
                    objResponceModel.Message = "Somthing Went Wrong!";
                }

            }
            else
            {
                objResponceModel.Responce = false;
                objResponceModel.Message = "No Part found!";
            }

            return objResponceModel;

        }

        #endregion
    }
}