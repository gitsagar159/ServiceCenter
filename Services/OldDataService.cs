using ServiceCenter.Models;
using ServiceCenter.Models.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ServiceCenter.Services
{
    public class OldDataService
    {
        string strQuery = "";

        private BaseDAL objBaseDAL;
        private List<SqlParameter> lstParam;
        private string DBName = "ketan2015_GoDaddy";

        #region Job

        public CallRegistrationListDataModel GetCallRegisterListBySP(int SortCol, string SortDir, int PageIndex, int PageSize, string CustomerName, int? CallType, int? ServType, string Technician, string TechnicianType, string MobileNo, string Pincode, bool? CallAttn, bool? JobDone, string JobNo, string SrNo, string CompComplaintNo, string ItemName, string ItemKeyword, bool? Deliver, bool? Canceled, bool? PartPanding, bool? IsCompComplaintNo, DateTime? FromDate, DateTime? ToDate, DateTime? CallAssignFromDate, DateTime? CallAssignToDate, bool? CallBack, bool? WorkShopIN, bool? PaymentPanding, bool? GoAfterCall, bool? RepeatFromTech, string UserName, string FaultType, string FaultDesc, string Area, DateTime? ModifyFromDate, DateTime? ModifyToDate)
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();


            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();



                strQuery = @"JobList_2015";

                lstParam = new List<SqlParameter>();

                SqlParameter SortCol_Param = new SqlParameter() { ParameterName = "@SortCol", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = SortCol };
                SqlParameter SortDir_Param = new SqlParameter() { ParameterName = "@SortDir", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = SortDir };
                SqlParameter PageIndex_Param = new SqlParameter() { ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageIndex };
                SqlParameter PageSize_Param = new SqlParameter() { ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageSize };
                SqlParameter TotalRecordCount_Param = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                SqlParameter CustomerName_Param = !string.IsNullOrEmpty(CustomerName) ? new SqlParameter() { ParameterName = "@CustomerName", Value = CustomerName } : new SqlParameter() { ParameterName = "@CustomerName", Value = DBNull.Value };
                SqlParameter CallType_Param = CallType.HasValue ? new SqlParameter() { ParameterName = "@CallType", Value = CallType } : new SqlParameter() { ParameterName = "@CallType", Value = DBNull.Value };
                SqlParameter ServType_Param = ServType.HasValue ? new SqlParameter() { ParameterName = "@ServType", Value = ServType } : new SqlParameter() { ParameterName = "@ServType", Value = DBNull.Value };
                SqlParameter Technician_Param = !string.IsNullOrEmpty(Technician) ? new SqlParameter() { ParameterName = "@Technician", Value = Technician } : new SqlParameter() { ParameterName = "@Technician", Value = DBNull.Value };
                SqlParameter TechnicianType_Param = !string.IsNullOrEmpty(TechnicianType) ? new SqlParameter() { ParameterName = "@TechnicianType", Value = TechnicianType } : new SqlParameter() { ParameterName = "@TechnicianType", Value = DBNull.Value };
                SqlParameter MobileNo_Param = !string.IsNullOrEmpty(MobileNo) ? new SqlParameter() { ParameterName = "@MobileNo", Value = MobileNo } : new SqlParameter() { ParameterName = "@MobileNo", Value = DBNull.Value };
                SqlParameter Pincode_Param = !string.IsNullOrEmpty(Pincode) ? new SqlParameter() { ParameterName = "@Pincode", Value = Pincode } : new SqlParameter() { ParameterName = "@Pincode", Value = DBNull.Value };
                SqlParameter CallAttn_Param = CallAttn.HasValue ? new SqlParameter() { ParameterName = "@CallAttn", Value = CallAttn } : new SqlParameter() { ParameterName = "@CallAttn", Value = DBNull.Value };
                SqlParameter JobDone_Param = JobDone.HasValue ? new SqlParameter() { ParameterName = "@JobDone", Value = JobDone } : new SqlParameter() { ParameterName = "@JobDone", Value = DBNull.Value };
                SqlParameter JobNo_Param = !string.IsNullOrEmpty(JobNo) ? new SqlParameter() { ParameterName = "@JobNo", Value = JobNo } : new SqlParameter() { ParameterName = "@JobNo", Value = DBNull.Value };
                SqlParameter SrNo_Param = !string.IsNullOrEmpty(SrNo) ? new SqlParameter() { ParameterName = "@SrNo", Value = SrNo } : new SqlParameter() { ParameterName = "@SrNo", Value = DBNull.Value };
                SqlParameter CompComplaintNo_Param = !string.IsNullOrEmpty(CompComplaintNo) ? new SqlParameter() { ParameterName = "@CompComplaintNo", Value = CompComplaintNo } : new SqlParameter() { ParameterName = "@CompComplaintNo", Value = DBNull.Value };
                SqlParameter ItemName_Param = !string.IsNullOrEmpty(ItemName) ? new SqlParameter() { ParameterName = "@ItemName", Value = ItemName } : new SqlParameter() { ParameterName = "@ItemName", Value = DBNull.Value };
                SqlParameter ItemKeyword_Param = !string.IsNullOrEmpty(ItemKeyword) ? new SqlParameter() { ParameterName = "@ItemKeyword", Value = ItemKeyword } : new SqlParameter() { ParameterName = "@ItemKeyword", Value = DBNull.Value };
                SqlParameter Deliver_Param = Deliver.HasValue ? new SqlParameter() { ParameterName = "@Deliver", Value = Deliver } : new SqlParameter() { ParameterName = "@Deliver", Value = DBNull.Value };
                SqlParameter Canceled_Param = Canceled.HasValue ? new SqlParameter() { ParameterName = "@Canceled", Value = Canceled } : new SqlParameter() { ParameterName = "@Canceled", Value = DBNull.Value };
                SqlParameter PartPanding_Param = PartPanding.HasValue ? new SqlParameter() { ParameterName = "@PartPanding", Value = PartPanding } : new SqlParameter() { ParameterName = "@PartPanding", Value = DBNull.Value };
                SqlParameter GoAfterCall_Param = GoAfterCall.HasValue ? new SqlParameter() { ParameterName = "@GoAfterCall", Value = GoAfterCall } : new SqlParameter() { ParameterName = "@GoAfterCall", Value = DBNull.Value };
                SqlParameter RepeatFromTech_Param = RepeatFromTech.HasValue ? new SqlParameter() { ParameterName = "@RepeatFromTech", Value = RepeatFromTech } : new SqlParameter() { ParameterName = "@RepeatFromTech", Value = DBNull.Value };

                SqlParameter IsCompComplaintNo_Param = IsCompComplaintNo.HasValue ? new SqlParameter() { ParameterName = "@IsCompComplaintNo", Value = IsCompComplaintNo } : new SqlParameter() { ParameterName = "@IsCompComplaintNo", Value = DBNull.Value };
                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                SqlParameter CallAssignFromDate_Param = CallAssignFromDate.HasValue ? new SqlParameter() { ParameterName = "@CallAssignFromDate", Value = CallAssignFromDate } : new SqlParameter() { ParameterName = "@CallAssignFromDate", Value = DBNull.Value };
                SqlParameter CallAssignToDate_Param = CallAssignToDate.HasValue ? new SqlParameter() { ParameterName = "@CallAssignToDate", Value = CallAssignToDate } : new SqlParameter() { ParameterName = "@CallAssignToDate", Value = DBNull.Value };
                SqlParameter CallBack_Param = CallBack.HasValue ? new SqlParameter() { ParameterName = "@CallBack", Value = CallBack } : new SqlParameter() { ParameterName = "@CallBack", Value = DBNull.Value };
                SqlParameter WorkShopIN_Param = WorkShopIN.HasValue ? new SqlParameter() { ParameterName = "@WorkShopIN", Value = WorkShopIN } : new SqlParameter() { ParameterName = "@WorkShopIN", Value = DBNull.Value };
                SqlParameter PaymentPanding_Param = PaymentPanding.HasValue ? new SqlParameter() { ParameterName = "@PaymentPanding", Value = PaymentPanding } : new SqlParameter() { ParameterName = "@PaymentPanding", Value = DBNull.Value };

                SqlParameter UserName_Param = !string.IsNullOrEmpty(UserName) ? new SqlParameter() { ParameterName = "@UserName", Value = UserName } : new SqlParameter() { ParameterName = "@UserName", Value = DBNull.Value };

                SqlParameter FaultType_Param = !string.IsNullOrEmpty(FaultType) ? new SqlParameter() { ParameterName = "@FaultType", Value = FaultType } : new SqlParameter() { ParameterName = "@FaultType", Value = DBNull.Value };
                SqlParameter FaultDesc_Param = !string.IsNullOrEmpty(FaultDesc) ? new SqlParameter() { ParameterName = "@FaultDesc", Value = FaultDesc } : new SqlParameter() { ParameterName = "@FaultDesc", Value = DBNull.Value };
                SqlParameter Area_Param = !string.IsNullOrEmpty(Area) ? new SqlParameter() { ParameterName = "@Area", Value = Area } : new SqlParameter() { ParameterName = "@Area", Value = DBNull.Value };

                SqlParameter ModifyFromDate_Param = ModifyFromDate.HasValue ? new SqlParameter() { ParameterName = "@ModifyFromDate", Value = ModifyFromDate } : new SqlParameter() { ParameterName = "@ModifyFromDate", Value = DBNull.Value };
                SqlParameter ModifyToDate_Param = ModifyToDate.HasValue ? new SqlParameter() { ParameterName = "@ModifyToDate", Value = ModifyToDate } : new SqlParameter() { ParameterName = "@ModifyToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { SortCol_Param, SortDir_Param, PageIndex_Param, PageSize_Param, CustomerName_Param, CallType_Param, ServType_Param, Technician_Param, TechnicianType_Param, MobileNo_Param, Pincode_Param, CallAttn_Param,
                    JobDone_Param, JobNo_Param, SrNo_Param, CompComplaintNo_Param, ItemName_Param, ItemKeyword_Param, Deliver_Param, Canceled_Param, PartPanding_Param, GoAfterCall_Param, RepeatFromTech_Param, IsCompComplaintNo_Param, FromDate_Param, ToDate_Param, CallAssignFromDate_Param, CallAssignToDate_Param, CallBack_Param, WorkShopIN_Param, PaymentPanding_Param, UserName_Param, FaultType_Param, FaultDesc_Param, Area_Param, ModifyFromDate_Param, ModifyToDate_Param, TotalRecordCount_Param });

                StringBuilder SBJobListSP = new StringBuilder();

                SBJobListSP.Append("DECLARE @return_value int, @RecordCount int");
                SBJobListSP.Append(Environment.NewLine);
                SBJobListSP.Append("EXEC\t@return_value = [dbo].[JobList]");

                for (int i = 0; i < lstParam.Count(); i++)
                {
                    string strText = lstParam[i].ParameterName + " = ";

                    if (lstParam[i].DbType == DbType.String)
                    {
                        strText += lstParam[i].Value != null ? !string.IsNullOrEmpty(lstParam[i].Value.ToString()) ? "'" + lstParam[i].Value.ToString() + "'" : "NULL" : "NULL";
                    }
                    else
                    {
                        strText += lstParam[i].Value != null ? !string.IsNullOrEmpty(lstParam[i].Value.ToString()) ? lstParam[i].Value.ToString() : "NULL" : "NULL";
                    }

                    SBJobListSP.Append(strText);

                    if (i != lstParam.Count() - 1)
                    {
                        SBJobListSP.Append(",");
                    }

                    SBJobListSP.Append(Environment.NewLine);
                }

                SBJobListSP.Append("SELECT @RecordCount as N'@RecordCount'");

                CommonService.WriteTraceLog(SBJobListSP.ToString());


                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam, DBName);

                TotalRecordCount = Convert.ToInt32(TotalRecordCount_Param.Value);

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();

                            objCallRegistration.RowNo = dtRowItem["RowNo"] != DBNull.Value ? Convert.ToInt64(dtRowItem["RowNo"]) : 0;
                            objCallRegistration.Oid = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CreationDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CreationDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CompComplaintNo = dtRowItem["CompComplaintNo"] != DBNull.Value ? Convert.ToString(dtRowItem["CompComplaintNo"]) : string.Empty;
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.Address = dtRowItem["Address"] != DBNull.Value ? Convert.ToString(dtRowItem["Address"]) : string.Empty;
                            //objCallRegistration.Pincode = dtRowItem["Pincode"] != DBNull.Value ? Convert.ToString(dtRowItem["Pincode"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.ProductName = dtRowItem["ProductName"] != DBNull.Value ? Convert.ToString(dtRowItem["ProductName"]) : string.Empty;
                            objCallRegistration.SrNo = dtRowItem["SrNo"] != DBNull.Value ? Convert.ToString(dtRowItem["SrNo"]) : string.Empty;

                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.SpInst = dtRowItem["SpInst"] != DBNull.Value ? Convert.ToString(dtRowItem["SpInst"]) : string.Empty;
                            objCallRegistration.JobRegion = dtRowItem["JobRegion"] != DBNull.Value ? Convert.ToString(dtRowItem["JobRegion"]) : string.Empty;
                            objCallRegistration.JobDoneRegion = dtRowItem["JobDoneRegion"] != DBNull.Value ? Convert.ToString(dtRowItem["JobDoneRegion"]) : string.Empty;

                            objCallRegistration.ItemId = dtRowItem["ItemId"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemId"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.ModelName = dtRowItem["ModelName"] != DBNull.Value ? Convert.ToString(dtRowItem["ModelName"]) : string.Empty;
                            objCallRegistration.PurchaseDate = dtRowItem["PurchaseDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["PurchaseDate"]) : (DateTime?)null;
                            objCallRegistration.StringPurchaseDate = dtRowItem["PurchaseDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["PurchaseDate"]).ToString("dd'/'MM'/'yyy") : "-";

                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.WorkShopIN = dtRowItem["WorkShopIN"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["WorkShopIN"]) : false;
                            objCallRegistration.SMSSent = dtRowItem["SMSSent"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["SMSSent"]) : false;
                            objCallRegistration.Deliver = dtRowItem["Deliver"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Deliver"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;

                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;

                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;
                            objCallRegistration.IsNew = dtRowItem["IsNew"] != DBNull.Value ? Convert.ToString(dtRowItem["IsNew"]) : string.Empty;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;
                            objCallRegistration.ModifyDateString = dtRowItem["ModifyDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ModifyDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";

                            objCallRegistrationListDataModel.CallRegistrationList.Add(objCallRegistration);

                        }
                        objCallRegistrationListDataModel.RecordCount = TotalRecordCount;
                    }
                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objCallRegistrationListDataModel;
        }


        #endregion

        #region Area

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

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam, DBName);

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

        #endregion

        #region Item
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

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam, DBName);

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

        #endregion

        #region Select2 Dropdown

        public List<Select2> GetAreaList(string AreaName)
        {
            List<Select2> lstFaultType = new List<Select2>();

            try
            {
                objBaseDAL = new BaseDAL();


                if (!string.IsNullOrEmpty(AreaName))
                {
                    strQuery = @"SELECT 
	                                Oid,
	                                AreaName
                                FROM 
	                                Area
                                WHERE
                                    AreaName LIKE CONCAT('%', @AreaName,'%')
                                ORDER BY AreaName ASC";

                }
                else
                {
                    strQuery = @"SELECT Oid, AreaName FROM Area ORDER BY AreaName ASC";
                }

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@AreaName", AreaName),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam, DBName);

                if (ResDataTable.Rows.Count > 0)
                {
                    Select2 objSelect2;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objSelect2 = new Select2();

                        objSelect2.id = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        objSelect2.text = dtRowItem["AreaName"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaName"]) : string.Empty;

                        lstFaultType.Add(objSelect2);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return lstFaultType;
        }

        public List<Select2> GetItemList(string ItemName)
        {
            List<Select2> lstItem = new List<Select2>();

            try
            {
                objBaseDAL = new BaseDAL();


                if (!string.IsNullOrEmpty(ItemName))
                {
                    strQuery = @"SELECT 
	                                Oid, 
	                                ItemName 
                                FROM 
	                                Item 
                                WHERE 
	                                ItemName LIKE '%' + @ItemName + '%'
                                ORDER BY ItemName ASC";

                }
                else
                {
                    strQuery = @"SELECT 
	                                Oid, 
	                                ItemName 
                                FROM 
	                                Item
							    ORDER BY ItemName ASC";
                }

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@ItemName", ItemName),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam, DBName);

                if (ResDataTable.Rows.Count > 0)
                {
                    Select2 objSelect2;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objSelect2 = new Select2();

                        objSelect2.id = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        objSelect2.text = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;

                        lstItem.Add(objSelect2);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return lstItem;
        }

        public List<Select2> GetCompanyList(string CompanyName)
        {
            List<Select2> lstItem = new List<Select2>();

            try
            {
                objBaseDAL = new BaseDAL();


                if (!string.IsNullOrEmpty(CompanyName))
                {
                    strQuery = @"SELECT 
	                                Oid, 
	                                CompanyName 
                                FROM 
	                                Company 
                                WHERE 
	                                CompanyName LIKE '%' + @CompanyName + '%'
                                ORDER BY CompanyName ASC";

                }
                else
                {
                    strQuery = @"SELECT 
	                                Oid, 
	                                CompanyName 
                                FROM 
	                                Company
							    ORDER BY CompanyName ASC";
                }

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@CompanyName", CompanyName),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam, DBName);

                if (ResDataTable.Rows.Count > 0)
                {
                    Select2 objSelect2;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objSelect2 = new Select2();

                        objSelect2.id = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        objSelect2.text = dtRowItem["CompanyName"] != DBNull.Value ? Convert.ToString(dtRowItem["CompanyName"]) : string.Empty;

                        lstItem.Add(objSelect2);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return lstItem;
        }

        public List<Select2> GetBrandList(string BrandName)
        {
            List<Select2> lstItem = new List<Select2>();

            try
            {
                objBaseDAL = new BaseDAL();


                if (!string.IsNullOrEmpty(BrandName))
                {
                    strQuery = @"SELECT 
	                                Oid, 
	                                Brand 
                                FROM 
	                                Brand 
                                WHERE 
	                               Brand LIKE '%' + @BrandName + '%'
                                ORDER BY Brand ASC";

                }
                else
                {
                    strQuery = @"SELECT 
	                                Oid, 
	                                Brand 
                                FROM 
	                                Brand
							    ORDER BY Brand ASC";
                }

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@BrandName", BrandName),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam, DBName);

                if (ResDataTable.Rows.Count > 0)
                {
                    Select2 objSelect2;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objSelect2 = new Select2();

                        objSelect2.id = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        objSelect2.text = dtRowItem["Brand"] != DBNull.Value ? Convert.ToString(dtRowItem["Brand"]) : string.Empty;

                        lstItem.Add(objSelect2);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return lstItem;
        }

        #endregion
    }
}