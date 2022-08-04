using ServiceCenter.Models;
using ServiceCenter.Models.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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

        public AreaMasterListDataModel GetAreaList(int SortCol, string SortDir, int PageIndex, int PageSize, string Keyword)
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
                SqlParameter KeywordParam = new SqlParameter() { ParameterName = "@Keyword", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = Keyword };
                SqlParameter TotalRecordCountParam = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortColParam, SortDirParam, PageIndexParam, PageSizeParam, KeywordParam, TotalRecordCountParam });

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
        public ResponceModel InsertUpdateItem(string ItemId, string ItemName)
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


                SqlParameter ItemNameParam = new SqlParameter() { ParameterName = "@ItemName", Value = ItemName };
                SqlParameter LoginUserIdParam = new SqlParameter() { ParameterName = "@LoginUserId", Value = UserSesionDetail.id };

                lstParam.AddRange(new SqlParameter[] { ItemIdParam, ItemNameParam, LoginUserIdParam });

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

        public ItemMasterListDataModel GetItemList(int SortCol, string SortDir, int PageIndex, int PageSize, string Keyword)
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
                SqlParameter KeywordParam = new SqlParameter() { ParameterName = "@Keyword", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = Keyword };
                SqlParameter TotalRecordCountParam = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortColParam, SortDirParam, PageIndexParam, PageSizeParam, KeywordParam, TotalRecordCountParam });

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
    }
}