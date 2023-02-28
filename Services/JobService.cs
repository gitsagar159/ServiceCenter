using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using ServiceCenter.Models;
using ServiceCenter.Models.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace ServiceCenter.Services
{
    public class JobService
    {
        string strQuery = "";

        private BaseDAL objBaseDAL;
        private List<SqlParameter> lstParam;

        public List<CallRegistration> GetCallRegisterList()
        {
            List<CallRegistration> lstCallRegistration = new List<CallRegistration>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"select top 100
	                            CR.Oid,
	                            convert(varchar, CR.CreationDate, 22) AS  CreationDate,
	                            convert(varchar, CR.CallAssignDate, 22) AS  CallAssignDate,
	                            CR.CustomerName, 
	                            CR.Address,
                                CR.Pincode,
	                            CR.CallType, 
	                            CR.ServType, 
	                            T.Technician, 
	                            CR.MobileNo, 
	                            CR.CallAttn, 
	                            CR.JobDone, 
	                            CR.ProductName  
                            from 
	                            CallRegistration CR LEFT JOIN Technician T
	                            ON 	CR.Technician = T.Oid
                            Order by
	                            CR.CreationDate DESC";

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    CallRegistration objCallRegistration;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objCallRegistration = new CallRegistration();

                        objCallRegistration.Oid = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                        objCallRegistration.Address = dtRowItem["Address"] != DBNull.Value ? Convert.ToString(dtRowItem["Address"]) : string.Empty;
                        objCallRegistration.Pincode = dtRowItem["Pincode"] != DBNull.Value ? Convert.ToString(dtRowItem["Pincode"]) : string.Empty;
                        objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                        objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                        objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                        objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                        objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                        objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                        objCallRegistration.ProductName = dtRowItem["ProductName"] != DBNull.Value ? Convert.ToString(dtRowItem["ProductName"]) : string.Empty;

                        lstCallRegistration.Add(objCallRegistration);

                    }
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return lstCallRegistration;
        }

        public CallRegistrationListDataModel GetCallRegisterListBySP(int SortCol, string SortDir, int PageIndex, int PageSize, string CustomerName, int? CallType, int? ServType, string Technician, string TechnicianType, string MobileNo, string Pincode, bool? CallAttn, bool? JobDone, string JobNo, string SrNo, string CompComplaintNo, string ItemName, string ItemKeyword, bool? Deliver, bool? Canceled, bool? PartPanding, bool? IsCompComplaintNo, DateTime? FromDate, DateTime? ToDate, DateTime? CallAssignFromDate, DateTime? CallAssignToDate, bool? CallBack, bool? WorkShopIN, bool? PaymentPanding, bool? GoAfterCall, bool? RepeatFromTech, string UserName, string FaultType, string FaultDesc, string Area, DateTime? ModifyFromDate, DateTime? ModifyToDate, int CallCategory)
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();


            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();



                strQuery = @"JobList";

                lstParam = new List<SqlParameter>();

                SqlParameter SortCol_Param = new SqlParameter() { ParameterName = "@SortCol", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = SortCol };
                SqlParameter SortDir_Param = new SqlParameter() { ParameterName = "@SortDir", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = SortDir };
                SqlParameter PageIndex_Param = new SqlParameter() { ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageIndex };
                SqlParameter PageSize_Param = new SqlParameter() { ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageSize };
                SqlParameter CallCategory_Param = new SqlParameter() { ParameterName = "@CallCategory", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = CallCategory };
                SqlParameter TotalRecordCount_Param = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                SqlParameter CustomerName_Param = !string.IsNullOrEmpty(CustomerName) ? new SqlParameter() { ParameterName = "@CustomerName" , Value = CustomerName  } : new SqlParameter() { ParameterName = "@CustomerName", Value = DBNull.Value };
                SqlParameter CallType_Param = CallType.HasValue ? new SqlParameter() { ParameterName = "@CallType" , Value = CallType  } : new SqlParameter() { ParameterName = "@CallType", Value = DBNull.Value };
                SqlParameter ServType_Param = ServType.HasValue ? new SqlParameter() { ParameterName = "@ServType" , Value = ServType  } : new SqlParameter() { ParameterName = "@ServType", Value = DBNull.Value };
                SqlParameter Technician_Param = !string.IsNullOrEmpty(Technician) ? new SqlParameter() { ParameterName = "@Technician" , Value = Technician  } : new SqlParameter() { ParameterName = "@Technician", Value = DBNull.Value };
                SqlParameter TechnicianType_Param = !string.IsNullOrEmpty(TechnicianType) ? new SqlParameter() { ParameterName = "@TechnicianType", Value = TechnicianType } : new SqlParameter() { ParameterName = "@TechnicianType", Value = DBNull.Value };
                SqlParameter MobileNo_Param = !string.IsNullOrEmpty(MobileNo) ? new SqlParameter() { ParameterName = "@MobileNo" , Value = MobileNo  } : new SqlParameter() { ParameterName = "@MobileNo", Value = DBNull.Value };
                SqlParameter Pincode_Param = !string.IsNullOrEmpty(Pincode) ? new SqlParameter() { ParameterName = "@Pincode", Value = Pincode } : new SqlParameter() { ParameterName = "@Pincode", Value = DBNull.Value };
                SqlParameter CallAttn_Param = CallAttn.HasValue ? new SqlParameter() { ParameterName = "@CallAttn" , Value = CallAttn  } : new SqlParameter() { ParameterName = "@CallAttn", Value = DBNull.Value };
                SqlParameter JobDone_Param = JobDone.HasValue ? new SqlParameter() { ParameterName = "@JobDone" , Value = JobDone  } : new SqlParameter() { ParameterName = "@JobDone", Value = DBNull.Value };
                SqlParameter JobNo_Param = !string.IsNullOrEmpty(JobNo) ? new SqlParameter() { ParameterName = "@JobNo" , Value = JobNo  } : new SqlParameter() { ParameterName = "@JobNo", Value = DBNull.Value };
                SqlParameter SrNo_Param = !string.IsNullOrEmpty(SrNo) ? new SqlParameter() { ParameterName = "@SrNo", Value = SrNo } : new SqlParameter() { ParameterName = "@SrNo", Value = DBNull.Value };
                SqlParameter CompComplaintNo_Param = !string.IsNullOrEmpty(CompComplaintNo) ? new SqlParameter() { ParameterName = "@CompComplaintNo", Value = CompComplaintNo } : new SqlParameter() { ParameterName = "@CompComplaintNo", Value = DBNull.Value };
                SqlParameter ItemName_Param = !string.IsNullOrEmpty(ItemName) ? new SqlParameter() { ParameterName = "@ItemName" , Value = ItemName  } : new SqlParameter() { ParameterName = "@ItemName", Value = DBNull.Value };
                SqlParameter ItemKeyword_Param = !string.IsNullOrEmpty(ItemKeyword) ? new SqlParameter() { ParameterName = "@ItemKeyword", Value = ItemKeyword } : new SqlParameter() { ParameterName = "@ItemKeyword", Value = DBNull.Value };
                SqlParameter Deliver_Param = Deliver.HasValue ? new SqlParameter() { ParameterName = "@Deliver", Value = Deliver } : new SqlParameter() { ParameterName = "@Deliver", Value = DBNull.Value };
                SqlParameter Canceled_Param = Canceled.HasValue ? new SqlParameter() { ParameterName = "@Canceled" , Value = Canceled  } : new SqlParameter() { ParameterName = "@Canceled", Value = DBNull.Value };
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
                    JobDone_Param, JobNo_Param, SrNo_Param, CompComplaintNo_Param, ItemName_Param, ItemKeyword_Param, Deliver_Param, Canceled_Param, PartPanding_Param, GoAfterCall_Param, RepeatFromTech_Param, IsCompComplaintNo_Param, FromDate_Param, ToDate_Param, CallAssignFromDate_Param, CallAssignToDate_Param, CallBack_Param, WorkShopIN_Param, PaymentPanding_Param, UserName_Param, FaultType_Param, FaultDesc_Param, Area_Param, ModifyFromDate_Param, ModifyToDate_Param, CallCategory_Param, TotalRecordCount_Param });

                StringBuilder SBJobListSP = new StringBuilder();

                SBJobListSP.Append("DECLARE @return_value int, @RecordCount int");
                SBJobListSP.Append(Environment.NewLine);
                SBJobListSP.Append("EXEC\t@return_value = [dbo].[JobList]");

                for (int i = 0; i < lstParam.Count(); i++)
                {
                    string strText = lstParam[i].ParameterName + " = ";

                    if(lstParam[i].DbType == DbType.String)
                    {
                        strText += lstParam[i].Value != null ? !string.IsNullOrEmpty(lstParam[i].Value.ToString()) ? "'" + lstParam[i].Value.ToString() + "'" : "NULL" : "NULL";
                    }
                    else
                    {
                        strText += lstParam[i].Value != null ? !string.IsNullOrEmpty(lstParam[i].Value.ToString()) ? lstParam[i].Value.ToString() : "NULL" : "NULL";
                    }

                    SBJobListSP.Append(strText);

                    if(i != lstParam.Count() - 1)
                    {
                        SBJobListSP.Append(",");
                    }

                    SBJobListSP.Append(Environment.NewLine);
                }

                SBJobListSP.Append("SELECT @RecordCount as N'@RecordCount'");

                CommonService.WriteTraceLog(SBJobListSP.ToString());


                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                TotalRecordCount = Convert.ToInt32(TotalRecordCount_Param.Value);

                if(ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];
                    DataTable CallRegisterOidsTable = ResDataSet.Tables[1];

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
                            objCallRegistration.Pincode = dtRowItem["Pincode"] != DBNull.Value ? Convert.ToString(dtRowItem["Pincode"]) : string.Empty;
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

                    if(CallRegisterOidsTable.Rows.Count > 0)
                    {
                        DataRow dtRowItem = CallRegisterOidsTable.Rows[0];

                        objCallRegistrationListDataModel.Oids = dtRowItem["Oids"] != DBNull.Value ? Convert.ToString(dtRowItem["Oids"]) : string.Empty;
                    }
                }

                
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objCallRegistrationListDataModel;
        }

        public CallRegistration GetCallRegistrationDetailByOid(string Oid)
        {
            CallRegistration ObjCallRegistration = new CallRegistration();


            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"SELECT 
	                            CR.Oid,
	                            CR.CallAssignDate,
	                            CR.Customer,
	                            CR.CustomerName,
	                            CR.Address,
                                CR.Pincode,
	                            CR.Area AS AreaId,
	                            AM.AreaName AS AreaName, 
	                            CR.MobileNo,
	                            CR.CallDate,
	                            CR.ID, 
	                            CR.SrNo,
	                            CR.JobNo,
	                            CR.DealRef,
	                            CR.CompComplaintNo,
	                            CR.EstimateDate, 
	                            CR.Payment,
	                            CR.CallType,
	                            CR.EstConfirmDate,
	                            CR.Estimate,
	                            CR.ServType,
	                            CR.ItemName AS ItemId,
	                            IM.ItemName AS ItemName,
	                            CR.Technician AS TechnicianId,
	                            T.Technician AS TechnicianName,
	                            CR.FaultDesc,
	                            CR.FaultType AS FaultTypeId,
	                            FT.FaultType AS FaultTypeName,
	                            CR.ModelName,
	                            CR.SpInst,
	                            CR.JobDoneRegion,
                                CR.JobRegion,
	                            CR.ProductName,
	                            CR.CallAttn,
	                            CR.JobDone,
	                            CR.RepeatFromTech,
	                            CR.CallBack,
	                            CR.WorkShopIN,
	                            CR.SMSSent,
	                            CR.BillNo,
	                            CR.TechBillNo,
	                            CR.PaymentPanding,
	                            CR.PurchaseDate,
	                            CR.Deliver,
	                            CR.Canceled,
	                            CR.GoAfterCall,
	                            CR.PartPanding,
	                            CR.PaymentBy,
	                            CR.VisitCharge,
	                            CR.UserName, 
	                            CR.Modifier, 
	                            CR.CreationDate, 
	                            CR.ModifyDate
                            From 
	                            CallRegistration CR 
	                            LEFT JOIN AreaMaster AM on AM.AreaId = CR.Area
	                            LEFT JOIN ItemMaster IM on IM.ItemId = CR.ItemName
	                            LEFT JOIN Technician T on T.Oid = CR.Technician
	                            LEFT JOIN FaultType FT on FT.Oid = CR.FaultType
	                            LEFT JOIN CustomerMaster CM on CM.Oid = CR.Customer
	                            LEFT JOIN City C on C.Oid = CM.CityName
                            where
	                            CR.Oid = @Oid";

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@Oid", Oid),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {

                    DataRow dtRowItem = ResDataTable.Rows[0];

                    CallRegistrationSelect2Data objSelect2Data = new CallRegistrationSelect2Data();

                    ObjCallRegistration.Oid = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                    ObjCallRegistration.CallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]) : DateTime.Now;
                    ObjCallRegistration.CallDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]) : DateTime.Now;

                    ObjCallRegistration.Customer = dtRowItem["Customer"] != DBNull.Value ? Convert.ToString(dtRowItem["Customer"]) : string.Empty;

                    objSelect2Data.Select2Customer = new Select2() { id = ObjCallRegistration.Customer, text = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty };

                    ObjCallRegistration.Address = dtRowItem["Address"] != DBNull.Value ? Convert.ToString(dtRowItem["Address"]) : string.Empty;
                    ObjCallRegistration.Pincode = dtRowItem["Pincode"] != DBNull.Value ? Convert.ToString(dtRowItem["Pincode"]) : string.Empty;

                    ObjCallRegistration.Area = dtRowItem["AreaId"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaId"]) : string.Empty;

                    objSelect2Data.Select2Area = new Select2() { id = ObjCallRegistration.Area, text = dtRowItem["AreaName"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaName"]) : string.Empty };

                    ObjCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                    ObjCallRegistration.CallDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]) : (DateTime?)null;
                    ObjCallRegistration.ID = dtRowItem["ID"] != DBNull.Value ? Convert.ToInt64(dtRowItem["ID"]) : 0;
                    ObjCallRegistration.SrNo = dtRowItem["SrNo"] != DBNull.Value ? Convert.ToString(dtRowItem["SrNo"]) : string.Empty;
                    ObjCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                    ObjCallRegistration.DealRef = dtRowItem["DealRef"] != DBNull.Value ? Convert.ToString(dtRowItem["DealRef"]) : string.Empty;
                    ObjCallRegistration.CompComplaintNo = dtRowItem["CompComplaintNo"] != DBNull.Value ? Convert.ToString(dtRowItem["CompComplaintNo"]) : string.Empty;
                    ObjCallRegistration.EstimateDate = dtRowItem["EstimateDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["EstimateDate"]) : (DateTime?)null;
                    ObjCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                    ObjCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                    ObjCallRegistration.EstConfirmDate = dtRowItem["EstConfirmDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["EstConfirmDate"]) : (DateTime?)null;
                    ObjCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                    ObjCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                    ObjCallRegistration.ItemName = dtRowItem["ItemId"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemId"]) : string.Empty;

                    objSelect2Data.Select2Item = new Select2() { id = ObjCallRegistration.ItemName, text = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty };

                    ObjCallRegistration.Technician = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                    ObjCallRegistration.Technician_Old = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;

                    ObjCallRegistration.TechnicianName = dtRowItem["TechnicianName"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianName"]) : string.Empty;

                    objSelect2Data.Select2Technician = new Select2() { id = ObjCallRegistration.Technician, text = dtRowItem["TechnicianName"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianName"]) : string.Empty };

                    ObjCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                    ObjCallRegistration.FaultType = dtRowItem["FaultTypeId"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultTypeId"]) : string.Empty;

                    objSelect2Data.Select2FaultType = new Select2() { id = ObjCallRegistration.FaultType, text = dtRowItem["FaultTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultTypeName"]) : string.Empty };

                    ObjCallRegistration.ModelName = dtRowItem["ModelName"] != DBNull.Value ? Convert.ToString(dtRowItem["ModelName"]) : string.Empty;
                    ObjCallRegistration.SpInst = dtRowItem["SpInst"] != DBNull.Value ? Convert.ToString(dtRowItem["SpInst"]) : string.Empty;
                    ObjCallRegistration.JobDoneRegion = dtRowItem["JobDoneRegion"] != DBNull.Value ? Convert.ToString(dtRowItem["JobDoneRegion"]) : string.Empty;
                    ObjCallRegistration.JobRegion = dtRowItem["JobRegion"] != DBNull.Value ? Convert.ToString(dtRowItem["JobRegion"]) : string.Empty;
                    ObjCallRegistration.ProductName = dtRowItem["ProductName"] != DBNull.Value ? Convert.ToString(dtRowItem["ProductName"]) : string.Empty;
                    ObjCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                    ObjCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                    ObjCallRegistration.JobDone_Old = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                    ObjCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                    ObjCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                    ObjCallRegistration.WorkShopIN = dtRowItem["WorkShopIN"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["WorkShopIN"]) : false;
                    ObjCallRegistration.SMSSent = dtRowItem["SMSSent"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["SMSSent"]) : false;
                    ObjCallRegistration.BillNo = dtRowItem["BillNo"] != DBNull.Value ? Convert.ToString(dtRowItem["BillNo"]) : string.Empty;
                    ObjCallRegistration.TechBillNo = dtRowItem["TechBillNo"] != DBNull.Value ? Convert.ToString(dtRowItem["TechBillNo"]) : string.Empty;
                    ObjCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;
                    ObjCallRegistration.PurchaseDate = dtRowItem["PurchaseDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["PurchaseDate"]) : (DateTime?)null;
                    ObjCallRegistration.Deliver = dtRowItem["Deliver"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Deliver"]) : false;
                    ObjCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                    ObjCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                    ObjCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                    ObjCallRegistration.PaymentBy = dtRowItem["PaymentBy"] != DBNull.Value ? Convert.ToInt32(dtRowItem["PaymentBy"]) : 0;
                    ObjCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                    ObjCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;
                    ObjCallRegistration.Modifier = dtRowItem["Modifier"] != DBNull.Value ? Convert.ToString(dtRowItem["Modifier"]) : string.Empty;
                    ObjCallRegistration.CreationDate = dtRowItem["CreationDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CreationDate"]) : (DateTime?)null;
                    ObjCallRegistration.ModifyDate = dtRowItem["ModifyDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ModifyDate"]) : (DateTime?)null;


                    ObjCallRegistration.Select2JSON = JsonConvert.SerializeObject(objSelect2Data);

                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return ObjCallRegistration;
        }

        public CallRegistration InsertUpdateCallRegistration(CallRegistration objCallRegistration)
        {

            CommonService.WriteTraceLog("JobService_InsertUpdateCallRegistration -> objCallRegistration.CallAssignDate : " + objCallRegistration.CallAssignDate);

            CallRegistration objCallRegistrationResponce = new CallRegistration();

            if (objCallRegistration != null)
            {

                UpdatePincodeForCustomerMaster(objCallRegistration.Customer, objCallRegistration.Pincode);

                User UserSesionDetail = SessionService.GetUserSessionValues();
                bool blnNewCallSMS = false, blnTechnicianAssignSMS = false, blnJobDoneSMS = false;

                objCallRegistration.JobDoneTime = objCallRegistration.JobDone ? DateTime.Now : (DateTime?)null;

                try
                {
                    
                    objBaseDAL = new BaseDAL();
                    string InsertQuery = @"INSERT INTO 
	                                            CallRegistration(
	                                            Oid,
	                                            Customer,
	                                            CustomerName,
	                                            Address,
                                                Pincode,
	                                            Area, 
	                                            MobileNo,
	                                            CallDate,
	                                            ID, 
	                                            SrNo,
	                                            JobNo,
	                                            DealRef,
	                                            CompComplaintNo,
	                                            EstimateDate, 
	                                            Payment,
	                                            CallType,
	                                            EstConfirmDate,
	                                            Estimate,
	                                            ServType,
	                                            ItemName,
	                                            Technician,
	                                            FaultDesc,
	                                            FaultType,
	                                            ModelName,
	                                            SpInst,
	                                            JobDoneRegion,
	                                            ProductName,
	                                            CallAttn,
	                                            JobDone,
	                                            RepeatFromTech,
	                                            CallBack,
	                                            WorkShopIN,
	                                            SMSSent,
	                                            BillNo,
	                                            TechBillNo,
	                                            PaymentPanding,
	                                            PurchaseDate,
	                                            Deliver,
	                                            Canceled,
	                                            GoAfterCall,
	                                            PartPanding,
	                                            PaymentBy,
	                                            VisitCharge,
	                                            UserName, 
	                                            CreationDate,
                                                IsNew
	                                            )
	                                            VALUES
	                                            (
	                                            CAST(@Oid AS UNIQUEIDENTIFIER),
	                                            CAST(@Customer AS UNIQUEIDENTIFIER),
	                                            @CustomerName,
	                                            @Address,
                                                @Pincode,
	                                            CAST(@Area AS UNIQUEIDENTIFIER),
	                                            @MobileNo,
	                                            @CallDate,
	                                            @ID, 
	                                            @SrNo,
	                                            @JobNo,
	                                            @DealRef,
	                                            @CompComplaintNo,
	                                            @EstimateDate, 
	                                            @Payment,
	                                            @CallType,
	                                            @EstConfirmDate,
	                                            @Estimate,
	                                            @ServType,
	                                            CAST(@ItemName AS UNIQUEIDENTIFIER),
                                                CAST(@Technician AS UNIQUEIDENTIFIER),
	                                            @FaultDesc,
	                                            CAST(@FaultType AS UNIQUEIDENTIFIER),
	                                            @ModelName,
	                                            @SpInst,
	                                            @JobDoneRegion,
	                                            @ProductName,
	                                            @CallAttn,
	                                            @JobDone,
	                                            @RepeatFromTech,
	                                            @CallBack,
	                                            @WorkShopIN,
	                                            @SMSSent,
	                                            @BillNo,
	                                            @TechBillNo,
	                                            @PaymentPanding,
	                                            @PurchaseDate,
	                                            @Deliver,
	                                            @Canceled,
	                                            @GoAfterCall,
	                                            @PartPanding,
	                                            @PaymentBy,
	                                            @VisitCharge,
	                                            @UserName, 
	                                            @CreationDate,
                                                'True'
	                                            )";



                    string UpdateQuery = @"Update 
												CallRegistration
											SET	
                                                Customer = CAST(@Customer AS UNIQUEIDENTIFIER),
                                                CustomerName =  @CustomerName,
                                                Address = @Address,
                                                Pincode = @Pincode,
                                                Area  = CAST(@Area AS UNIQUEIDENTIFIER),
                                                MobileNo = @MobileNo,
                                                SrNo = @SrNo,
                                                DealRef = @DealRef,
                                                CompComplaintNo = @CompComplaintNo,
                                                EstimateDate  = @EstimateDate ,
                                                Payment = @Payment,
                                                CallType = @CallType,
                                                EstConfirmDate = @EstConfirmDate,
                                                Estimate = @Estimate,
                                                ServType = @ServType,
                                                ItemName = CAST(@ItemName AS UNIQUEIDENTIFIER),
                                                Technician = CAST(@Technician AS UNIQUEIDENTIFIER),
                                                FaultDesc = @FaultDesc,
                                                FaultType = CAST(@FaultType AS UNIQUEIDENTIFIER),
                                                ModelName = @ModelName,
                                                SpInst = @SpInst,
                                                JobDoneRegion = @JobDoneRegion,
                                                ProductName = @ProductName,
                                                CallAttn = @CallAttn,
                                                JobDone = @JobDone,
                                                RepeatFromTech = @RepeatFromTech,
                                                CallBack = @CallBack,
                                                WorkShopIN = @WorkShopIN,
                                                SMSSent = @SMSSent,
                                                BillNo = @BillNo,
                                                TechBillNo = @TechBillNo,
                                                PaymentPanding = @PaymentPanding,
                                                PurchaseDate = @PurchaseDate,
                                                Deliver = @Deliver,
                                                Canceled = @Canceled,
                                                GoAfterCall = @GoAfterCall,
                                                PartPanding = @PartPanding,
                                                PaymentBy = @PaymentBy,
                                                VisitCharge = @VisitCharge,
                                                Modifier  = @Modifier,
                                                ModifyDate = @ModifyDate
											WHERE 
												Oid = CAST(@Oid AS UNIQUEIDENTIFIER)";


                    lstParam = new List<SqlParameter>();


                    //SqlParameter Oid_Param = !string.IsNullOrEmpty(objCallRegistration.Oid) ? new SqlParameter() { ParameterName = "@Oid", Value = objCallRegistration.Oid } : new SqlParameter() { ParameterName = "@Oid", Value = DBNull.Value };
                    SqlParameter Area_Param = !string.IsNullOrEmpty(objCallRegistration.Area) ? new SqlParameter() { ParameterName = "@Area", Value = objCallRegistration.Area } : new SqlParameter() { ParameterName = "@Area", Value = DBNull.Value };
                    SqlParameter ID_Param = new SqlParameter() { ParameterName = "@ID", Value = objCallRegistration.ID };
                    SqlParameter CallAssignDate_Param = objCallRegistration.CallAssignDate.HasValue ? new SqlParameter() { ParameterName = "@CallAssignDate", Value = objCallRegistration.CallAssignDate } : new SqlParameter() { ParameterName = "@CallAssignDate", Value = DBNull.Value };
                    SqlParameter EstimateDate_Param = objCallRegistration.EstimateDate.HasValue ? new SqlParameter() { ParameterName = "@EstimateDate", Value = objCallRegistration.EstimateDate } : new SqlParameter() { ParameterName = "@EstimateDate", Value = DBNull.Value };
                    SqlParameter EstConfirmDate_Param = objCallRegistration.EstConfirmDate.HasValue ? new SqlParameter() { ParameterName = "@EstConfirmDate", Value = objCallRegistration.EstConfirmDate } : new SqlParameter() { ParameterName = "@EstConfirmDate", Value = DBNull.Value };
                    SqlParameter PaymentBy_Param = new SqlParameter() { ParameterName = "@PaymentBy", Value = objCallRegistration.PaymentBy };
                    SqlParameter CompComplaintNo_Param = !string.IsNullOrEmpty(objCallRegistration.CompComplaintNo) ? new SqlParameter() { ParameterName = "@CompComplaintNo", Value = objCallRegistration.CompComplaintNo } : new SqlParameter() { ParameterName = "@CompComplaintNo", Value = DBNull.Value };
                    SqlParameter JobNo_Param = !string.IsNullOrEmpty(objCallRegistration.JobNo) ? new SqlParameter() { ParameterName = "@JobNo", Value = objCallRegistration.JobNo } : new SqlParameter() { ParameterName = "@JobNo", Value = DBNull.Value };
                    SqlParameter CallDate_Param =  new SqlParameter() { ParameterName = "@CallDate", Value = DateTime.Now };
                    SqlParameter ItemName_Param = !string.IsNullOrEmpty(objCallRegistration.ItemName) ? new SqlParameter() { ParameterName = "@ItemName", Value = objCallRegistration.ItemName } : new SqlParameter() { ParameterName = "@ItemName", Value = DBNull.Value };
                    SqlParameter SrNo_Param = !string.IsNullOrEmpty(objCallRegistration.SrNo) ? new SqlParameter() { ParameterName = "@SrNo", Value = objCallRegistration.SrNo } : new SqlParameter() { ParameterName = "@SrNo", Value = DBNull.Value };
                    SqlParameter FaultDesc_Param = !string.IsNullOrEmpty(objCallRegistration.FaultDesc) ? new SqlParameter() { ParameterName = "@FaultDesc", Value = objCallRegistration.FaultDesc } : new SqlParameter() { ParameterName = "@FaultDesc", Value = DBNull.Value };
                    SqlParameter CallType_Param = new SqlParameter() { ParameterName = "@CallType", Value = objCallRegistration.CallType };
                    SqlParameter Customer_Param = !string.IsNullOrEmpty(objCallRegistration.Customer) ? new SqlParameter() { ParameterName = "@Customer", Value = objCallRegistration.Customer } : new SqlParameter() { ParameterName = "@Customer", Value = DBNull.Value };
                    SqlParameter CustomerName_Param = !string.IsNullOrEmpty(objCallRegistration.CustomerName) ? new SqlParameter() { ParameterName = "@CustomerName", Value = objCallRegistration.CustomerName } : new SqlParameter() { ParameterName = "@CustomerName", Value = DBNull.Value };
                    SqlParameter MobileNo_Param = !string.IsNullOrEmpty(objCallRegistration.MobileNo) ? new SqlParameter() { ParameterName = "@MobileNo", Value = objCallRegistration.MobileNo } : new SqlParameter() { ParameterName = "@MobileNo", Value = DBNull.Value };
                    SqlParameter Address_Param = !string.IsNullOrEmpty(objCallRegistration.Address) ? new SqlParameter() { ParameterName = "@Address", Value = objCallRegistration.Address } : new SqlParameter() { ParameterName = "@Address", Value = DBNull.Value };
                    SqlParameter Pincode_Param = !string.IsNullOrEmpty(objCallRegistration.Pincode) ? new SqlParameter() { ParameterName = "@Pincode", Value = objCallRegistration.Pincode } : new SqlParameter() { ParameterName = "@Pincode", Value = DBNull.Value };                    
                    SqlParameter ProductName_Param = !string.IsNullOrEmpty(objCallRegistration.ProductName) ? new SqlParameter() { ParameterName = "@ProductName", Value = objCallRegistration.ProductName } : new SqlParameter() { ParameterName = "@ProductName", Value = DBNull.Value };
                    SqlParameter ServType_Param = new SqlParameter() { ParameterName = "@ServType", Value = objCallRegistration.ServType };
                    SqlParameter SpInst_Param = !string.IsNullOrEmpty(objCallRegistration.SpInst) ? new SqlParameter() { ParameterName = "@SpInst", Value = objCallRegistration.SpInst } : new SqlParameter() { ParameterName = "@SpInst", Value = DBNull.Value };
                    SqlParameter ModelName_Param = !string.IsNullOrEmpty(objCallRegistration.ModelName) ? new SqlParameter() { ParameterName = "@ModelName", Value = objCallRegistration.ModelName } : new SqlParameter() { ParameterName = "@ModelName", Value = DBNull.Value };
                    SqlParameter PurchaseDate_Param = objCallRegistration.PurchaseDate.HasValue ? new SqlParameter() { ParameterName = "@PurchaseDate", Value = objCallRegistration.PurchaseDate } : new SqlParameter() { ParameterName = "@PurchaseDate", Value = DBNull.Value };
                    SqlParameter BillNo_Param = !string.IsNullOrEmpty(objCallRegistration.BillNo) ? new SqlParameter() { ParameterName = "@BillNo", Value = objCallRegistration.BillNo } : new SqlParameter() { ParameterName = "@BillNo", Value = DBNull.Value };
                    SqlParameter TechBillNo_Param = !string.IsNullOrEmpty(objCallRegistration.TechBillNo) ? new SqlParameter() { ParameterName = "@TechBillNo", Value = objCallRegistration.TechBillNo } : new SqlParameter() { ParameterName = "@TechBillNo", Value = DBNull.Value };
                    SqlParameter DealRef_Param = !string.IsNullOrEmpty(objCallRegistration.DealRef) ? new SqlParameter() { ParameterName = "@DealRef", Value = objCallRegistration.DealRef } : new SqlParameter() { ParameterName = "@DealRef", Value = DBNull.Value };
                    SqlParameter Technician_Param = !string.IsNullOrEmpty(objCallRegistration.Technician) ? new SqlParameter() { ParameterName = "@Technician", Value = objCallRegistration.Technician } : new SqlParameter() { ParameterName = "@Technician", Value = DBNull.Value };
                    SqlParameter CallAttn_Param = new SqlParameter() { ParameterName = "@CallAttn", Value = objCallRegistration.CallAttn };
                    SqlParameter JobDone_Param = new SqlParameter() { ParameterName = "@JobDone", Value = objCallRegistration.JobDone };
                    SqlParameter JobDoneTime_Param = objCallRegistration.JobDoneTime.HasValue ? new SqlParameter() { ParameterName = "@JobDoneTime", Value = objCallRegistration.JobDoneTime.Value } : new SqlParameter() { ParameterName = "@JobDoneTime", Value = DBNull.Value };
                    SqlParameter Deliver_Param = new SqlParameter() { ParameterName = "@Deliver", Value = objCallRegistration.Deliver };
                    SqlParameter SMSSent_Param = new SqlParameter() { ParameterName = "@SMSSent", Value = objCallRegistration.SMSSent };
                    SqlParameter Estimate_Param = new SqlParameter() { ParameterName = "@Estimate", Value = objCallRegistration.Estimate };
                    SqlParameter Payment_Param = new SqlParameter() { ParameterName = "@Payment", Value = objCallRegistration.Payment };
                    SqlParameter VisitCharge_Param = new SqlParameter() { ParameterName = "@VisitCharge", Value = objCallRegistration.VisitCharge };
                    SqlParameter CallBack_Param = new SqlParameter() { ParameterName = "@CallBack", Value = objCallRegistration.CallBack };
                    SqlParameter WorkShopIN_Param = new SqlParameter() { ParameterName = "@WorkShopIN", Value = objCallRegistration.WorkShopIN };
                    SqlParameter PartPanding_Param = new SqlParameter() { ParameterName = "@PartPanding", Value = objCallRegistration.PartPanding };
                    SqlParameter GoAfterCall_Param = new SqlParameter() { ParameterName = "@GoAfterCall", Value = objCallRegistration.GoAfterCall };
                    SqlParameter Canceled_Param = new SqlParameter() { ParameterName = "@Canceled", Value = objCallRegistration.Canceled };
                    SqlParameter PaymentPanding_Param = new SqlParameter() { ParameterName = "@PaymentPanding", Value = objCallRegistration.PaymentPanding };
                    SqlParameter RepeatFromTech_Param = new SqlParameter() { ParameterName = "@RepeatFromTech", Value = objCallRegistration.RepeatFromTech };
                    SqlParameter JobDoneRegion_Param = !string.IsNullOrEmpty(objCallRegistration.JobDoneRegion) ? new SqlParameter() { ParameterName = "@JobDoneRegion", Value = objCallRegistration.JobDoneRegion } : new SqlParameter() { ParameterName = "@JobDoneRegion", Value = DBNull.Value };
                    SqlParameter FaultType_Param = !string.IsNullOrEmpty(objCallRegistration.FaultType) ? new SqlParameter() { ParameterName = "@FaultType", Value = objCallRegistration.FaultType } : new SqlParameter() { ParameterName = "@FaultType", Value = DBNull.Value };

                    SqlParameter UserName_Param = !string.IsNullOrEmpty(objCallRegistration.UserName) ? new SqlParameter() { ParameterName = "@UserName", Value = objCallRegistration.UserName } : new SqlParameter() { ParameterName = "@UserName", Value = DBNull.Value };
                    SqlParameter CreationDate_Param = new SqlParameter() { ParameterName = "@CreationDate", Value = DateTime.Now };
                    SqlParameter Modifier_Param = !string.IsNullOrEmpty(UserSesionDetail.UserName) ? new SqlParameter() { ParameterName = "@Modifier", Value = UserSesionDetail.UserName } : new SqlParameter() { ParameterName = "@Modifier", Value = "" };
                    SqlParameter ModifyDate_Param = new SqlParameter() { ParameterName = "@ModifyDate", Value = DateTime.Now };


                    

                    lstParam.AddRange(new SqlParameter[]
                                                    {
                                                        Area_Param,
                                                        ID_Param,
                                                        CallAssignDate_Param,
                                                        EstimateDate_Param,
                                                        EstConfirmDate_Param,
                                                        PaymentBy_Param,
                                                        UserName_Param,
                                                        CreationDate_Param,
                                                        CompComplaintNo_Param,
                                                        JobNo_Param,
                                                        CallDate_Param,
                                                        ItemName_Param,
                                                        SrNo_Param,
                                                        FaultDesc_Param,
                                                        CallType_Param,
                                                        Customer_Param,
                                                        CustomerName_Param,
                                                        MobileNo_Param,
                                                        Address_Param,
                                                        Pincode_Param,
                                                        ProductName_Param,
                                                        ServType_Param,
                                                        SpInst_Param,
                                                        ModelName_Param,
                                                        PurchaseDate_Param,
                                                        BillNo_Param,
                                                        TechBillNo_Param,
                                                        DealRef_Param,
                                                        Technician_Param,
                                                        CallAttn_Param,
                                                        JobDone_Param,
                                                        Deliver_Param,
                                                        SMSSent_Param,
                                                        Estimate_Param,
                                                        Payment_Param,
                                                        VisitCharge_Param,
                                                        CallBack_Param,
                                                        WorkShopIN_Param,
                                                        PartPanding_Param,
                                                        GoAfterCall_Param,
                                                        Canceled_Param,
                                                        PaymentPanding_Param,
                                                        RepeatFromTech_Param,
                                                        JobDoneRegion_Param,
                                                        FaultType_Param,
                                                        Modifier_Param,
                                                        ModifyDate_Param
                                                    });

                    if (!string.IsNullOrEmpty(objCallRegistration.Oid))
                    {
                        objCallRegistrationResponce.Oid = objCallRegistration.Oid;
                        objCallRegistrationResponce.IsEdit = true;

                        lstParam.AddRange(new SqlParameter[]
                          {
                                new SqlParameter("@Oid", new Guid(objCallRegistration.Oid))
                          });

                        objBaseDAL.ExeccuteStoreCommand(UpdateQuery, CommandType.Text, lstParam);

                        if(objCallRegistration.Technician != objCallRegistration.Technician_Old)
                        {
                            blnTechnicianAssignSMS = true;

                            APIService objAPIService = new APIService();

                            JobDetailForSMS objJobDetailForSMS = new JobDetailForSMS() { CustomerName = objCallRegistration.CustomerName, MobileNo = objCallRegistration.MobileNo, JobNo = objCallRegistration.JobNo, TechnicianName = objCallRegistration.TechnicianName };

                            objAPIService.SendSMSForCall(JobSMSSendCategory.TechnicianAllocation, objJobDetailForSMS);
                        }

                        if (objCallRegistration.JobDone != objCallRegistration.JobDone_Old && objCallRegistration.JobDone == true)
                        {
                            blnJobDoneSMS = true;

                            APIService objAPIService = new APIService();

                            JobDetailForSMS objJobDetailForSMS = new JobDetailForSMS() { CustomerName = objCallRegistration.CustomerName, MobileNo = objCallRegistration.MobileNo, JobNo = objCallRegistration.JobNo, TechnicianName = objCallRegistration.TechnicianName };

                            objAPIService.SendSMSForCall(JobSMSSendCategory.CallDone, objJobDetailForSMS);

                        }
                    }
                    else
                    {
                        objCallRegistrationResponce.Oid = Guid.NewGuid().ToString().ToUpper();
                        objCallRegistrationResponce.IsEdit = false;

                        lstParam.AddRange(new SqlParameter[]
                          {
                                new SqlParameter("@Oid", objCallRegistrationResponce.Oid),
                          });

                        objBaseDAL.ExeccuteStoreCommand(InsertQuery, CommandType.Text, lstParam);

                        blnNewCallSMS = true;

                        JobDetailForSMS objJobDetailForSMS = new JobDetailForSMS() { CustomerName = objCallRegistration.CustomerName, MobileNo = objCallRegistration.MobileNo, JobNo = objCallRegistration.JobNo, TechnicianName = objCallRegistration.TechnicianName };

                        SendSMSForNewCall(objJobDetailForSMS);
                    }

                    objCallRegistrationResponce.Responce = true;
                    objCallRegistrationResponce.Message = "Call Register Successfuly";

                    

                }
                catch (Exception ex)
                {
                    objCallRegistrationResponce = new CallRegistration() { Responce = false, Message = "Something went wrong" };
                    CommonService.WriteErrorLog(ex);
                }
            }



            return objCallRegistrationResponce;
        }

        public JobDashboard GetDashboardData()
        {
            JobDashboard objJobDashboard = new JobDashboard();
            objJobDashboard.CurrentYearData = new MonthModel();
            objJobDashboard.PreviousYearData = new MonthModel();


            try
            {
                objBaseDAL = new BaseDAL();
                strQuery = @"Job_Dashboard_Data";

                lstParam = new List<SqlParameter>();

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);


                if (ResDataSet != null && ResDataSet.Tables.Count > 0)
                {
                    DataTable JobCountTable = ResDataSet.Tables[0];
                    DataTable CurrentYearJobDataTable = ResDataSet.Tables[1];
                    DataTable PreviousYearJobDataTable = ResDataSet.Tables[2];


                    if (JobCountTable.Rows.Count > 0)
                    {
                        DataRow dtRow = JobCountTable.Rows[0];

                        objJobDashboard.TotalJobCount = dtRow["TotalJobCount"] != DBNull.Value ? Convert.ToInt32(dtRow["TotalJobCount"]) : 0;
                        objJobDashboard.JobDoneCount = dtRow["JobDoneCount"] != DBNull.Value ? Convert.ToInt32(dtRow["JobDoneCount"]) : 0;
                        objJobDashboard.OpenJobCount = dtRow["OpenJobCount"] != DBNull.Value ? Convert.ToInt32(dtRow["OpenJobCount"]) : 0;
                        objJobDashboard.NewJobCount = dtRow["NewJobCount"] != DBNull.Value ? Convert.ToInt32(dtRow["NewJobCount"]) : 0;
                    }
                    

                    if (CurrentYearJobDataTable.Rows.Count > 0)
                    {
                        DataRow dtRow = CurrentYearJobDataTable.Rows[0];
                        
                        MonthModel objMonthModel = new MonthModel();

                        objMonthModel.January = dtRow["January"] != DBNull.Value ? Convert.ToInt32(dtRow["January"]) : 0;
                        objMonthModel.February = dtRow["February"] != DBNull.Value ? Convert.ToInt32(dtRow["February"]) : 0;
                        objMonthModel.March = dtRow["March"] != DBNull.Value ? Convert.ToInt32(dtRow["March"]) : 0;
                        objMonthModel.April = dtRow["April"] != DBNull.Value ? Convert.ToInt32(dtRow["April"]) : 0;
                        objMonthModel.May = dtRow["May"] != DBNull.Value ? Convert.ToInt32(dtRow["May"]) : 0;
                        objMonthModel.June = dtRow["June"] != DBNull.Value ? Convert.ToInt32(dtRow["June"]) : 0;
                        objMonthModel.July = dtRow["July"] != DBNull.Value ? Convert.ToInt32(dtRow["July"]) : 0;
                        objMonthModel.August = dtRow["August"] != DBNull.Value ? Convert.ToInt32(dtRow["August"]) : 0;
                        objMonthModel.September = dtRow["September"] != DBNull.Value ? Convert.ToInt32(dtRow["September"]) : 0;
                        objMonthModel.October = dtRow["October"] != DBNull.Value ? Convert.ToInt32(dtRow["October"]) : 0;
                        objMonthModel.November = dtRow["November"] != DBNull.Value ? Convert.ToInt32(dtRow["November"]) : 0;
                        objMonthModel.December = dtRow["December"] != DBNull.Value ? Convert.ToInt32(dtRow["December"]) : 0;

                        objJobDashboard.CurrentYearData = objMonthModel;
                    }


                    if (PreviousYearJobDataTable.Rows.Count > 0)
                    {
                        DataRow dtRow = PreviousYearJobDataTable.Rows[0];
                        
                        MonthModel objMonthModel = new MonthModel();

                        objMonthModel.January = dtRow["January"] != DBNull.Value ? Convert.ToInt32(dtRow["January"]) : 0;
                        objMonthModel.February = dtRow["February"] != DBNull.Value ? Convert.ToInt32(dtRow["February"]) : 0;
                        objMonthModel.March = dtRow["March"] != DBNull.Value ? Convert.ToInt32(dtRow["March"]) : 0;
                        objMonthModel.April = dtRow["April"] != DBNull.Value ? Convert.ToInt32(dtRow["April"]) : 0;
                        objMonthModel.May = dtRow["May"] != DBNull.Value ? Convert.ToInt32(dtRow["May"]) : 0;
                        objMonthModel.June = dtRow["June"] != DBNull.Value ? Convert.ToInt32(dtRow["June"]) : 0;
                        objMonthModel.July = dtRow["July"] != DBNull.Value ? Convert.ToInt32(dtRow["July"]) : 0;
                        objMonthModel.August = dtRow["August"] != DBNull.Value ? Convert.ToInt32(dtRow["August"]) : 0;
                        objMonthModel.September = dtRow["September"] != DBNull.Value ? Convert.ToInt32(dtRow["September"]) : 0;
                        objMonthModel.October = dtRow["October"] != DBNull.Value ? Convert.ToInt32(dtRow["October"]) : 0;
                        objMonthModel.November = dtRow["November"] != DBNull.Value ? Convert.ToInt32(dtRow["November"]) : 0;
                        objMonthModel.December = dtRow["December"] != DBNull.Value ? Convert.ToInt32(dtRow["December"]) : 0;

                        objJobDashboard.PreviousYearData = objMonthModel;
                    }

                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objJobDashboard;
        }

        public List<Select2> GetCustomerCodeList(string MobileNo)
        {
            List<Select2> lstCustimerCode = new List<Select2>();

            try
            {
                objBaseDAL = new BaseDAL();


                if (!string.IsNullOrEmpty(MobileNo))
                {
                    strQuery = @"SELECT 
									Oid, 
									CONCAT(CustCode,' (', FirstName,')') AS CustomerCode
								FROM
									CustomerMaster 
								WHERE 
									MobileNo LIKE '%' + @MobileNo + '%'";

                }
                else
                {
                    strQuery = @"SELECT TOP 10
								Oid, 
								CustCode AS CustomerCode
							FROM
								CustomerMaster 
							ORDER BY CreationDate DESC";
                }

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@MobileNo", MobileNo),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    Select2 objSelect2;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objSelect2 = new Select2();

                        objSelect2.id = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        objSelect2.text = dtRowItem["CustomerCode"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerCode"]) : string.Empty;

                        lstCustimerCode.Add(objSelect2);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return lstCustimerCode;
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
	                                ItemId, 
	                                ItemName 
                                FROM 
	                                ItemMaster 
                                WHERE 
	                                IsActive = 1 AND IsDelete = 0 AND ItemName LIKE '%' + @ItemName + '%'
                                ORDER BY ItemName ASC";

                }
                else
                {
                    strQuery = @"SELECT 
	                                ItemId, 
	                                ItemName 
                                FROM 
	                                ItemMaster 
                                WHERE 
	                                IsActive = 1 AND IsDelete = 0 
							    ORDER BY ItemName ASC";
                }

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@ItemName", ItemName),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    Select2 objSelect2;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objSelect2 = new Select2();

                        objSelect2.id = dtRowItem["ItemId"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemId"]) : string.Empty;
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


        public List<Select2> GetCityList(string CityName)
        {
            List<Select2> lstCity = new List<Select2>();

            try
            {
                objBaseDAL = new BaseDAL();


                if (!string.IsNullOrEmpty(CityName))
                {
                    strQuery = @"SELECT 
	                                Oid,
	                                CityName
                                FROM
	                                City 
                                WHERE
	                                CityName LIKE '%' + @CityName + '%'
                                ORDER BY CityName ASC";

                }
                else
                {
                    strQuery = @"SELECT 
	                                Oid,
	                                CityName
                                FROM
	                                City 
                                ORDER BY CityName ASC";
                }

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@CityName", CityName),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    Select2 objSelect2;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objSelect2 = new Select2();

                        objSelect2.id = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        objSelect2.text = dtRowItem["CityName"] != DBNull.Value ? Convert.ToString(dtRowItem["CityName"]) : string.Empty;

                        lstCity.Add(objSelect2);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return lstCity;
        }

        public CustomerMaster GetCustomerDetailsByCustomerId(string CustomerId)
        {
            CustomerMaster objCustomerMaster = new CustomerMaster();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"SELECT
	                            CM.Oid, 
	                            CM.CustCode, 
	                            CM.FirstName, 
	                            CM.LastName, 
	                            CM.Address,
                                CM.Pincode,
	                            CM.CityName AS CityId,
	                            C.CityName AS CityName, 
	                            CM.PhoneH, 
	                            CM.PhoneO, 
	                            CM.MobileNo, 
	                            CM.EMail,
	                            CM.OtherInfo, 
	                            CM.SpDay
                            FROM
	                            CustomerMaster CM LEFT JOIN City C 	ON CM.CityName = C.Oid
                            WHERE
	                            CM.Oid = @CustomerId";

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@CustomerId", CustomerId),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowItem = ResDataTable.Rows[0];

                    objCustomerMaster.Oid = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                    objCustomerMaster.CustCode = dtRowItem["CustCode"] != DBNull.Value ? Convert.ToInt64(dtRowItem["CustCode"]) : 0;

                    objCustomerMaster.FirstName = dtRowItem["FirstName"] != DBNull.Value ? Convert.ToString(dtRowItem["FirstName"]) : string.Empty;
                    objCustomerMaster.LastName = dtRowItem["LastName"] != DBNull.Value ? Convert.ToString(dtRowItem["LastName"]) : string.Empty;
                    objCustomerMaster.Address = dtRowItem["Address"] != DBNull.Value ? Convert.ToString(dtRowItem["Address"]) : string.Empty;
                    objCustomerMaster.Pincode = dtRowItem["Pincode"] != DBNull.Value ? Convert.ToString(dtRowItem["Pincode"]) : string.Empty;

                    objCustomerMaster.CityId = dtRowItem["CityId"] != DBNull.Value ? Convert.ToString(dtRowItem["CityId"]) : string.Empty;
                    objCustomerMaster.CityName = dtRowItem["CityName"] != DBNull.Value ? Convert.ToString(dtRowItem["CityName"]) : string.Empty;

                    objCustomerMaster.PhoneH = dtRowItem["PhoneH"] != DBNull.Value ? Convert.ToString(dtRowItem["PhoneH"]) : string.Empty;
                    objCustomerMaster.PhoneO = dtRowItem["PhoneO"] != DBNull.Value ? Convert.ToString(dtRowItem["PhoneO"]) : string.Empty;
                    objCustomerMaster.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;

                    objCustomerMaster.EMail = dtRowItem["EMail"] != DBNull.Value ? Convert.ToString(dtRowItem["EMail"]) : string.Empty;
                    objCustomerMaster.OtherInfo = dtRowItem["OtherInfo"] != DBNull.Value ? Convert.ToString(dtRowItem["OtherInfo"]) : string.Empty;
                    objCustomerMaster.SpDay = dtRowItem["SpDay"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["SpDay"]) : (DateTime?)null;

                    CustomerMasterSelect2Data objJsonDate = new CustomerMasterSelect2Data();
                    objJsonDate.Select2City = new Select2() { id = objCustomerMaster.CityId, text = objCustomerMaster.CityName };

                    objCustomerMaster.Select2JSON = JsonConvert.SerializeObject(objJsonDate);
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objCustomerMaster;
        }

        public AreaMaster GetPincodeByAreaId(string AreaId)
        {
            AreaMaster objAreaMaster = new AreaMaster();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"SELECT AreaId, AreaName, AreaPincode FROM AreaMaster WHERE AreaId = @AreaId";

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@AreaId",  AreaId),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowItem = ResDataTable.Rows[0];

                    objAreaMaster.AreaId = dtRowItem["AreaId"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaId"]) : string.Empty;
                    objAreaMaster.AreaName = dtRowItem["AreaName"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaName"]) : string.Empty;
                    objAreaMaster.AreaPincode = dtRowItem["AreaPincode"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaPincode"]) : string.Empty;

                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objAreaMaster;
        }

        public AreaMasterDD GetAreaListByPincode(string Pincode)
        {
            AreaMasterDD objAreaMasterDD = new AreaMasterDD();
            objAreaMasterDD.AreaMasterList = new List<AreaMaster>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"SELECT AreaId, AreaName FROM AreaMaster WHERE IsActive = 1 AND IsDelete = 0 AND AreaPincode = @AreaPincode";

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@AreaPincode", Pincode),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                AreaMaster objAreaMaster;

                if (ResDataTable.Rows.Count > 0)
                {
                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objAreaMaster = new AreaMaster();

                        objAreaMaster.AreaId = dtRowItem["AreaId"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaId"]) : string.Empty;
                        objAreaMaster.AreaName = dtRowItem["AreaName"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaName"]) : string.Empty;

                        objAreaMasterDD.AreaMasterList.Add(objAreaMaster);
                    }

                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objAreaMasterDD;
        }

        public List<Select2> GetFaultTypeList(string FaultType)
        {
            List<Select2> lstFaultType = new List<Select2>();

            try
            {
                objBaseDAL = new BaseDAL();


                if (!string.IsNullOrEmpty(FaultType))
                {
                    strQuery = @"SELECT 
	                                Oid,
	                                FaultType
                                FROM 
	                                FaultType
                                WHERE
	                                FaultType LIKE CONCAT('%', @FaultType,'%')
                                ORDER BY FaultType ASC";

                }
                else
                {
                    strQuery = @"SELECT 
	                                Oid,
	                                FaultType
                                FROM 
	                                FaultType
                                ORDER BY FaultType ASC";
                }

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@FaultType", FaultType),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    Select2 objSelect2;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objSelect2 = new Select2();

                        objSelect2.id = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        objSelect2.text = dtRowItem["FaultType"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultType"]) : string.Empty;

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
        public List<Select2> GetAreaList(string AreaName)
        {
            List<Select2> lstFaultType = new List<Select2>();

            try
            {
                objBaseDAL = new BaseDAL();


                if (!string.IsNullOrEmpty(AreaName))
                {
                    strQuery = @"SELECT 
	                                AreaId,
	                                AreaName
                                FROM 
	                                AreaMaster
                                WHERE
                                    IsActive = 1 AND IsDelete = 0 
                                    AND AreaName LIKE CONCAT('%', @AreaName,'%')
                                ORDER BY AreaName ASC";

                }
                else
                {
                    strQuery = @"SELECT AreaId, AreaName FROM AreaMaster WHERE IsActive = 1 AND IsDelete = 0 ORDER BY AreaName ASC";
                }

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@AreaName", AreaName),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    Select2 objSelect2;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objSelect2 = new Select2();

                        objSelect2.id = dtRowItem["AreaId"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaId"]) : string.Empty;
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

        public List<Select2> GetTechnicianList(string TechnicianName)
        {
            List<Select2> lstTechnician = new List<Select2>();

            try
            {
                objBaseDAL = new BaseDAL();


                if (!string.IsNullOrEmpty(TechnicianName))
                {
                    strQuery = @"SELECT 
	                                    Oid,
	                                    Technician
                                    FROM 
	                                    Technician
                                    WHERE
	                                    IsActive='1' AND Technician LIKE CONCAT('%', @TechnicianName,'%')
                                    ORDER BY Technician ASC";

                }
                else
                {
                    strQuery = @"SELECT 
	                                Oid,
	                                Technician
                                FROM 
	                                Technician
                                WHERE
	                                IsActive = '1'
                                ORDER BY Technician ASC";
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

                        objSelect2.id = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        objSelect2.text = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;

                        lstTechnician.Add(objSelect2);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return lstTechnician;
        }

        public List<Select2> GetTechnicianTypeList(string TechnicianTypeName)
        {
            List<Select2> lstTechnicianType = new List<Select2>();

            try
            {
                objBaseDAL = new BaseDAL();


                if (!string.IsNullOrEmpty(TechnicianTypeName))
                {
                    strQuery = @"SELECT 
	                                    Oid,
	                                    TechnicianType
                                    FROM 
	                                    TechnicianType
                                    WHERE
	                                    TechnicianType LIKE CONCAT('%', @TechnicianTypeName,'%')
                                    ORDER BY TechnicianType ASC";

                }
                else
                {
                    strQuery = @"SELECT 
	                                Oid,
	                                TechnicianType
                                FROM 
	                                TechnicianType
                                WHERE
                                ORDER BY TechnicianType ASC";
                }

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@TechnicianTypeName", TechnicianTypeName),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    Select2 objSelect2;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objSelect2 = new Select2();

                        objSelect2.id = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        objSelect2.text = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                        lstTechnicianType.Add(objSelect2);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return lstTechnicianType;
        }

        public List<Select2> GetUserList(string UserName)
        {
            List<Select2> lstUser = new List<Select2>();

            try
            {
                objBaseDAL = new BaseDAL();


                if (!string.IsNullOrEmpty(UserName))
                {
                    strQuery = @"SELECT 
	                                    DISTINCT(UserName) AS UserName 
                                    FROM 
	                                    CallRegistration 
                                    WHERE 
	                                    UserName LIKE CONCAT('%', @UserName,'%') 
                                    ORDER BY UserName ASC  ";

                }
                else
                {
                    strQuery = @"SELECT 
	                                    DISTINCT(UserName) AS UserName 
                                    FROM 
	                                    CallRegistration 
                                    ORDER BY UserName ASC";
                }

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@UserName", UserName),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    Select2 objSelect2;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objSelect2 = new Select2();

                        objSelect2.id = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;
                        objSelect2.text = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

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

        public int GetLatestIdForJobNo()
        {
            int LatestJobIdNo = 0;

            try
            {
                objBaseDAL = new BaseDAL();
                strQuery = @"SELECT ISNULL(MAX(ID), 0) AS Id FROM CallRegistration WHERE CreationDate BETWEEN CAST(CONCAT(convert(varchar, getdate(), 23), ' 00:00:00.000') AS datetime) AND  CAST(CONCAT(convert(varchar, getdate(), 23), ' 23:59:59.000') AS datetime)";

                lstParam = new List<SqlParameter>();

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowItem = ResDataTable.Rows[0];

                    LatestJobIdNo = dtRowItem["Id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["Id"]) : 0;
                    LatestJobIdNo++;
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return LatestJobIdNo;

        }


        public CustomerMaster InsertNewCustomer(CustomerMaster ObjCustomerMaster)
        {
            CustomerMaster objCustomerMasterResponce = new CustomerMaster();

            if (ObjCustomerMaster != null)
            {
                User UserSesionDetail = SessionService.GetUserSessionValues();


                try
                {
                    objBaseDAL = new BaseDAL();
                    strQuery = @"AddUpdateCustomerMasterDetail";

                    lstParam = new List<SqlParameter>();


                    SqlParameter Oid_Param = !string.IsNullOrEmpty(ObjCustomerMaster.Oid) ? new SqlParameter() { ParameterName = "@Oid", Value = ObjCustomerMaster.Oid } : new SqlParameter() { ParameterName = "@Oid", Value = DBNull.Value };
                    SqlParameter FirstName_Param = !string.IsNullOrEmpty(ObjCustomerMaster.FirstName) ? new SqlParameter() { ParameterName = "@FirstName", Value = ObjCustomerMaster.FirstName } : new SqlParameter() { ParameterName = "@FirstName", Value = DBNull.Value };
                    SqlParameter LastName_Param = !string.IsNullOrEmpty(ObjCustomerMaster.LastName) ? new SqlParameter() { ParameterName = "@LastName", Value = ObjCustomerMaster.LastName } : new SqlParameter() { ParameterName = "@LastName", Value = DBNull.Value };
                    SqlParameter Address_Param = !string.IsNullOrEmpty(ObjCustomerMaster.Address) ? new SqlParameter() { ParameterName = "@Address", Value = ObjCustomerMaster.Address } : new SqlParameter() { ParameterName = "@Address", Value = DBNull.Value };
                    SqlParameter Pincode_Param = !string.IsNullOrEmpty(ObjCustomerMaster.Pincode) ? new SqlParameter() { ParameterName = "@Pincode", Value = ObjCustomerMaster.Pincode } : new SqlParameter() { ParameterName = "@Pincode", Value = DBNull.Value };
                    SqlParameter CityId_Param = !string.IsNullOrEmpty(ObjCustomerMaster.CityName) ? new SqlParameter() { ParameterName = "@CityId", Value = ObjCustomerMaster.CityName } : new SqlParameter() { ParameterName = "@CityId", Value = DBNull.Value };
                    SqlParameter PhoneH_Param = !string.IsNullOrEmpty(ObjCustomerMaster.PhoneH) ? new SqlParameter() { ParameterName = "@PhoneH", Value = ObjCustomerMaster.PhoneH } : new SqlParameter() { ParameterName = "@PhoneH", Value = DBNull.Value };
                    SqlParameter PhoneO_Param = !string.IsNullOrEmpty(ObjCustomerMaster.PhoneO) ? new SqlParameter() { ParameterName = "@PhoneO", Value = ObjCustomerMaster.PhoneO } : new SqlParameter() { ParameterName = "@PhoneO", Value = DBNull.Value };
                    SqlParameter MobileNo_Param = !string.IsNullOrEmpty(ObjCustomerMaster.MobileNo) ? new SqlParameter() { ParameterName = "@MobileNo", Value = ObjCustomerMaster.MobileNo } : new SqlParameter() { ParameterName = "@MobileNo", Value = DBNull.Value };
                    SqlParameter EMail_Param = !string.IsNullOrEmpty(ObjCustomerMaster.EMail) ? new SqlParameter() { ParameterName = "@EMail", Value = ObjCustomerMaster.EMail } : new SqlParameter() { ParameterName = "@EMail", Value = DBNull.Value };
                    SqlParameter OtherInfo_Param = !string.IsNullOrEmpty(ObjCustomerMaster.OtherInfo) ? new SqlParameter() { ParameterName = "@OtherInfo", Value = ObjCustomerMaster.OtherInfo } : new SqlParameter() { ParameterName = "@OtherInfo", Value = DBNull.Value };
                    SqlParameter SpDay_Param = ObjCustomerMaster.SpDay.HasValue ? new SqlParameter() { ParameterName = "@SpDay", Value = ObjCustomerMaster.SpDay } : new SqlParameter() { ParameterName = "@SpDay", Value = DBNull.Value };
                    SqlParameter LoginUserId_Param = UserSesionDetail != null ? new SqlParameter() { ParameterName = "@LoginUserId", Value = UserSesionDetail.id } : new SqlParameter() { ParameterName = "@LoginUserId", Value = DBNull.Value };
                    SqlParameter CustomerId_Param = new SqlParameter() { ParameterName = "@CustomerId", SqlDbType = SqlDbType.UniqueIdentifier, Direction = ParameterDirection.Output };

                    lstParam.AddRange(new SqlParameter[] { Oid_Param, FirstName_Param, LastName_Param, Address_Param, Pincode_Param, CityId_Param, PhoneH_Param, PhoneO_Param, MobileNo_Param, EMail_Param, OtherInfo_Param, SpDay_Param, LoginUserId_Param, CustomerId_Param });

                    DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                    if (ResDataTable.Rows.Count > 0)
                    {
                        objCustomerMasterResponce = ObjCustomerMaster;

                        DataRow dtRowItem = ResDataTable.Rows[0];

                        objCustomerMasterResponce.Responce = dtRowItem["OperationSuccess"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["OperationSuccess"]) : false;
                        objCustomerMasterResponce.Message = dtRowItem["ResponceMessage"] != DBNull.Value ? Convert.ToString(dtRowItem["ResponceMessage"]) : string.Empty;
                        //objCustomerMasterResponce.Responce = dtRowItem["OprationSuceess"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["OprationSuceess"]) : false;

                        if (objCustomerMasterResponce.Responce)
                        {
                            objCustomerMasterResponce.Oid = Convert.ToString(CustomerId_Param.Value);
                        }
                    }

                }
                catch (Exception ex)
                {
                    objCustomerMasterResponce = new CustomerMaster() { Responce = false, Message = "Something went wrong" };
                    CommonService.WriteErrorLog(ex);
                }
            }



            return objCustomerMasterResponce;
        }

        public ResponceModel UpdateCheckboxValueByType(string CheckboxType, string CallId, bool Value)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();
            string UserName = UserSesionDetail != null ? UserSesionDetail.UserName : string.Empty;

            string strTechnicianIdForCall = GetTechnicianByCallId(CallId);

            if(!string.IsNullOrEmpty(strTechnicianIdForCall))
            {
                strQuery = string.Empty;
                bool blnJobDone = false;

                switch (CheckboxType)
                {

                    case "CallAttn":
                        strQuery = "UPDATE CallRegistration set CallAttn = @Value, Modifier = @UserName, ModifyDate = GETDATE() WHERE Oid = @CallId";
                        break;
                    case "JobDone":
                        strQuery = "UPDATE CallRegistration set JobDone = @Value, Modifier = @UserName, ModifyDate = GETDATE() WHERE Oid = @CallId";
                        blnJobDone = Value == true ? true : false;
                        break;
                    case "RepeatFromTech":
                        strQuery = "UPDATE CallRegistration set RepeatFromTech = @Value, Modifier = @UserName, ModifyDate = GETDATE() WHERE Oid = @CallId";
                        break;
                    case "CallBack":
                        strQuery = "UPDATE CallRegistration set CallBack = @Value, Modifier = @UserName, ModifyDate = GETDATE() WHERE Oid = @CallId";
                        break;
                    case "WorkShopIN":
                        strQuery = "UPDATE CallRegistration set WorkShopIN = @Value, Modifier = @UserName, ModifyDate = GETDATE() WHERE Oid = @CallId";
                        break;
                    case "SMSSent":
                        strQuery = "UPDATE CallRegistration set SMSSent = @Value, Modifier = @UserName, ModifyDate = GETDATE() WHERE Oid = @CallId";
                        break;
                    case "Deliver":
                        strQuery = "UPDATE CallRegistration set Deliver = @Value, Modifier = @UserName, ModifyDate = GETDATE() WHERE Oid = @CallId";
                        break;
                    case "Canceled":
                        strQuery = "UPDATE CallRegistration set Canceled = @Value, Modifier = @UserName, ModifyDate = GETDATE() WHERE Oid = @CallId";
                        break;
                    case "GoAfterCall":
                        strQuery = "UPDATE CallRegistration set GoAfterCall = @Value, Modifier = @UserName, ModifyDate = GETDATE() WHERE Oid = @CallId";
                        break;
                    case "PartPanding":
                        strQuery = "UPDATE CallRegistration set PartPanding = @Value, Modifier = @UserName, ModifyDate = GETDATE() WHERE Oid = @CallId";
                        break;

                    default:
                        strQuery = "";
                        break;
                }

                try
                {
                    if (!string.IsNullOrEmpty(strQuery))
                    {
                        objBaseDAL = new BaseDAL();

                        lstParam = new List<SqlParameter>();

                        lstParam.AddRange(new SqlParameter[]
                              {
                                new SqlParameter("@CallId", CallId),
                                new SqlParameter("@Value", Value),
                                new SqlParameter("@UserName", UserName),

                              });

                        objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                        objResponceModel.Responce = true;
                        objResponceModel.Message = "Checkbox value updated in database";
                    }

                    if (blnJobDone)
                    {

                        strQuery = @"SELECT 
	                                CM.FirstName AS CustomerName,
	                                CM.MobileNo,
                                    CR.JobNo,
                                    T.Technician AS TechnicianName
                                FROM 
	                                CustomerMaster CM 
	                                INNER JOIN CallRegistration CR ON CR.Customer = CM.Oid
                                    LEFT JOIN Technician T ON T.Oid = CR.Technician
                                WHERE 
	                                CR.Oid = @CallId";

                        lstParam = new List<SqlParameter>();

                        lstParam.AddRange(new SqlParameter[]
                              {
                                new SqlParameter("@CallId", CallId),
                              });

                        DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                        JobDetailForSMS objJobDetailForSMS = new JobDetailForSMS();

                        if (ResDataTable.Rows.Count > 0)
                        {

                            DataRow dtRowItem = ResDataTable.Rows[0];

                            objJobDetailForSMS.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objJobDetailForSMS.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objJobDetailForSMS.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objJobDetailForSMS.TechnicianName = dtRowItem["TechnicianName"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianName"]) : string.Empty;


                        }

                        APIService objAPIService = new APIService();

                        if (!string.IsNullOrEmpty(objJobDetailForSMS.CustomerName) && !string.IsNullOrEmpty(objJobDetailForSMS.MobileNo))
                        {
                            objAPIService.SendSMSForCall(JobSMSSendCategory.CallDone, objJobDetailForSMS);
                        }

                    }

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
                objResponceModel = new ResponceModel();
                objResponceModel.Responce = false;
                objResponceModel.Message = "No Technician allocated to call, Please select technicna first";
            }


            

            return objResponceModel;

        }

        public ResponceModel UpdateCompComplaintNoByCallId(string CompComplaintNo, string CallId)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();
            string UserName = UserSesionDetail != null ? UserSesionDetail.UserName : string.Empty;

            strQuery = @"UPDATE CallRegistration set CompComplaintNo = @CompComplaintNo, Modifier = @UserName, ModifyDate = GETDATE() WHERE Oid = @CallId";

            try
            {
                objBaseDAL = new BaseDAL();

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@CompComplaintNo", CompComplaintNo),
                                new SqlParameter("@CallId", CallId),
                                new SqlParameter("@UserName", UserName),
                      });

                objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                objResponceModel.Responce = true;
                objResponceModel.Message = "Comp Complaint No value updated in database";

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);

                objResponceModel = new ResponceModel();
                objResponceModel.Responce = false;
                objResponceModel.Message = "Somthing Went Wrong!";
            }

            return objResponceModel;

        }

        public ResponceModel UpdateJobDoneRegionByCallId(string JobDoneRegion, string CallId)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();
            string UserName = UserSesionDetail != null ? UserSesionDetail.UserName : string.Empty;

            strQuery = @"UPDATE CallRegistration set JobDoneRegion = @JobDoneRegion, Modifier = @UserName, ModifyDate = GETDATE() WHERE Oid = @CallId";

            try
            {
                objBaseDAL = new BaseDAL();

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@JobDoneRegion", JobDoneRegion),
                                new SqlParameter("@CallId", CallId),
                                new SqlParameter("@UserName", UserName),
                      });

                objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                objResponceModel.Responce = true;
                objResponceModel.Message = "Region For Job Not Done updated in database";

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);

                objResponceModel = new ResponceModel();
                objResponceModel.Responce = false;
                objResponceModel.Message = "Somthing Went Wrong!";
            }

            return objResponceModel;

        }

        public ResponceModel UpdateJobRegionByCallId(string JobRegion, string CallId)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();
            string UserName = UserSesionDetail != null ? UserSesionDetail.UserName : string.Empty;

            strQuery = @"UPDATE CallRegistration set JobRegion = @JobRegion, Modifier = @UserName, ModifyDate = GETDATE() WHERE Oid = @CallId";

            try
            {
                objBaseDAL = new BaseDAL();

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@JobRegion", JobRegion),
                                new SqlParameter("@CallId", CallId),
                                new SqlParameter("@UserName", UserName),
                      });

                objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                objResponceModel.Responce = true;
                objResponceModel.Message = "Region For Job Done updated in database";

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);

                objResponceModel = new ResponceModel();
                objResponceModel.Responce = false;
                objResponceModel.Message = "Somthing Went Wrong!";
            }

            return objResponceModel;

        }
        

        public BillDetailsDataModel GetBillDetailsByBillNo(string BillNo, string BillDate)
        {
            BillDetailsDataModel objBillDetailsDataModel = new BillDetailsDataModel();
            objBillDetailsDataModel.BillData = new List<BillDetails>();

            DateTime dtBillDate = DateTime.ParseExact(BillDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"GetDetailByBillNo";

                lstParam = new List<SqlParameter>();

                SqlParameter BillNo_Param = new SqlParameter() { ParameterName = "@BillNo", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = BillNo };
                SqlParameter BillDate_Param = new SqlParameter() { ParameterName = "@BillDate", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = dtBillDate };

                lstParam.AddRange(new SqlParameter[] { BillNo_Param, BillDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam, "Ketandb");


                if(ResDataSet.Tables.Count > 0)
                {

                    DataTable ResReponceDataTable = ResDataSet.Tables[0];
                    DataTable ResBillDetailsDataTable = ResDataSet.Tables.Count > 1 ? ResDataSet.Tables[1] : null;


                    if (ResBillDetailsDataTable.Rows.Count > 0)
                    {
                        BillDetails objBillDetails;

                        foreach (DataRow dtRowItem in ResBillDetailsDataTable.Rows)
                        {
                            objBillDetails = new BillDetails();

                            //CustomerId CustomerName    Address MobileNo    City PinCode ProductId ProductName ProductModel SalesId InvoiceDate

                            objBillDetails.BillNo = BillNo;
                            objBillDetails.CustomerId = dtRowItem["CustomerId"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CustomerId"]) : 0;
                            objBillDetails.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objBillDetails.Address = dtRowItem["Address"] != DBNull.Value ? Convert.ToString(dtRowItem["Address"]) : string.Empty;
                            objBillDetails.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objBillDetails.MobileNo2 = dtRowItem["MobileNo2"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo2"]) : string.Empty;
                            objBillDetails.CityName = dtRowItem["City"] != DBNull.Value ? Convert.ToString(dtRowItem["City"]) : string.Empty;
                            objBillDetails.Pincode = dtRowItem["Pincode"] != DBNull.Value ? Convert.ToString(dtRowItem["Pincode"]) : string.Empty;
                            objBillDetails.ProductId = dtRowItem["ProductId"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ProductId"]) : 0;
                            objBillDetails.ProductName = dtRowItem["ProductName"] != DBNull.Value ? Convert.ToString(dtRowItem["ProductName"]) : string.Empty;
                            objBillDetails.ProductModel = dtRowItem["ProductModel"] != DBNull.Value ? Convert.ToString(dtRowItem["ProductModel"]) : string.Empty;
                            objBillDetails.SalesId = dtRowItem["SalesId"] != DBNull.Value ? Convert.ToInt32(dtRowItem["SalesId"]) : 0;
                            objBillDetails.InvoiceDate = dtRowItem["InvoiceDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["InvoiceDate"]) : (DateTime?)null;
                            objBillDetails.StringInvoiceDate = dtRowItem["InvoiceDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["InvoiceDate"]).ToString("dd'/'MM'/'yyyy") : String.Empty;
                            objBillDetails.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : String.Empty;
                            objBillDetails.SerialNo = dtRowItem["SerialNo"] != DBNull.Value ? Convert.ToString(dtRowItem["SerialNo"]) : String.Empty;

                            CommonService.WriteTraceLog("JobService_GetBillDetailsByBillNo -> objBillDetails.InvoiceDate : " + objBillDetails.InvoiceDate);

                            string strJsonDate = JsonConvert.SerializeObject(objBillDetails);

                            objBillDetails.BillDetailJSON = strJsonDate;

                            objBillDetailsDataModel.BillData.Add(objBillDetails);

                        }
                    }
                }

                
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objBillDetailsDataModel;
        }

        public ResponceModel CreateNewCallFromBillDetails(BillDetails objBillDetails)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();

            CommonService.WriteTraceLog("JobServicee_CreateNewCallFromBillDetails -> objBillDetails.InvoiceDate : " + objBillDetails.InvoiceDate);
            CommonService.WriteTraceLog("JobServicee_CreateNewCallFromBillDetails -> objBillDetails.InvoiceDate : " + objBillDetails.StringInvoiceDate);

            DateTime dtBillDate = DateTime.ParseExact(objBillDetails.StringInvoiceDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            CommonService.WriteTraceLog("JobServicee_CreateNewCallFromBillDetails -> dtBillDate : " + dtBillDate);

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"CreateNewCallByBllNoDetails";

                lstParam = new List<SqlParameter>();

                SqlParameter BillNo_Param = new SqlParameter() { ParameterName = "@BillNo", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = objBillDetails.BillNo };
                SqlParameter BillDate_Param = new SqlParameter() { ParameterName = "@BillDate", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = dtBillDate };
                SqlParameter CustomerName_Param = new SqlParameter() { ParameterName = "@CustomerName", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = objBillDetails.CustomerName };
                SqlParameter CustomerMobileNo_Param = new SqlParameter() { ParameterName = "@CustomerMobileNo", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = objBillDetails.MobileNo };
                SqlParameter CustomerMobileNo2_Param = !string.IsNullOrEmpty(objBillDetails.MobileNo2) ? new SqlParameter() { ParameterName = "@CustomerMobileNo2", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = objBillDetails.MobileNo2 } : new SqlParameter() { ParameterName = "@CustomerMobileNo2", Value = DBNull.Value };
                SqlParameter CustomerAddress_Param = new SqlParameter() { ParameterName = "@CustomerAddress", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = objBillDetails.Address };
                SqlParameter CustomerPincode_Param = new SqlParameter() { ParameterName = "@CustomerPincode", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = objBillDetails.Pincode };
                SqlParameter CustomerCityName_Param = new SqlParameter() { ParameterName = "@CustomerCityName", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = objBillDetails.CityName };
                SqlParameter ItemName_Param = new SqlParameter() { ParameterName = "@ItemName", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = objBillDetails.ItemName };
                SqlParameter ProductName_Param = new SqlParameter() { ParameterName = "@ProductName", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = objBillDetails.ProductName };
                SqlParameter LoginUserId_Param = new SqlParameter() { ParameterName = "@LoginUserId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = UserSesionDetail.id };
                SqlParameter SerialNo_Param = new SqlParameter() { ParameterName = "@SerialNo", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = objBillDetails.SerialNo };


                lstParam.AddRange(new SqlParameter[] { BillNo_Param, BillDate_Param,  CustomerName_Param, CustomerMobileNo_Param, CustomerMobileNo2_Param, CustomerAddress_Param, CustomerPincode_Param, CustomerCityName_Param, ItemName_Param, ProductName_Param, LoginUserId_Param, SerialNo_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);


                if (ResDataSet.Tables.Count > 0)
                {

                    DataTable ResponceTable = ResDataSet.Tables[0];

                    DataRow dtRowItem = ResponceTable.Rows[0];

                    objResponceModel.Responce = dtRowItem["OperationSuccess"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["OperationSuccess"]) : false;
                    objResponceModel.Message = dtRowItem["ResponceMessage"] != DBNull.Value ? Convert.ToString(dtRowItem["ResponceMessage"]) : string.Empty;
                }

                if(objResponceModel.Responce)
                {
                    DataTable CallRegistationTable = ResDataSet.Tables[1];
                    DataRow dtRowItem = CallRegistationTable.Rows[0];

                    JobDetailForSMS objJobDetailForSMS = new JobDetailForSMS() 
                    { 
                        CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty, 
                        MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty,
                        JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty,
                    };

                    SendSMSForNewCall(objJobDetailForSMS);
                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objResponceModel;
        }

        public ResponceModel AssignMultipleCallToTechnician(string CallIds, string TechnicianId, DateTime? CallAssignDate)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"AssignMultipleCallToTechnician";

                lstParam = new List<SqlParameter>();

                SqlParameter CallIds_Param = new SqlParameter() { ParameterName = "@CallIds", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = CallIds };
                SqlParameter TechnicianId_Param = new SqlParameter() { ParameterName = "@TechnicianId", SqlDbType = SqlDbType.UniqueIdentifier, Direction = ParameterDirection.Input, Value = new Guid(TechnicianId) };
                SqlParameter CallAssignDate_Param = CallAssignDate.HasValue ? new SqlParameter() { ParameterName = "@CallAssignDate", Value = CallAssignDate.Value } : new SqlParameter() { ParameterName = "@CallAssignDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { CallIds_Param, TechnicianId_Param, CallAssignDate_Param });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowItem = ResDataTable.Rows[0];

                    objResponceModel.Responce = dtRowItem["OperationSuccess"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["OperationSuccess"]) : false;
                    objResponceModel.Message = dtRowItem["ResponceMessage"] != DBNull.Value ? Convert.ToString(dtRowItem["ResponceMessage"]) : string.Empty;
                }


                strQuery = @"GetMultipleCustomerDetailsByCallIds";

                lstParam = new List<SqlParameter>();

                SqlParameter CallIds_ForSMS_Param = new SqlParameter() { ParameterName = "@CallIds", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = CallIds };


                lstParam.AddRange(new SqlParameter[] { CallIds_ForSMS_Param});

                DataTable ResCustomerDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                List<JobDetailForSMS> lstJobDetailForSMS = new List<JobDetailForSMS>();

                if (ResCustomerDataTable.Rows.Count > 0)
                {
                    JobDetailForSMS objJobDetailForSMS;

                    foreach (DataRow dtRowItem in ResCustomerDataTable.Rows)
                    {
                        objJobDetailForSMS = new JobDetailForSMS();

                        objJobDetailForSMS.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                        objJobDetailForSMS.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                        objJobDetailForSMS.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                        objJobDetailForSMS.TechnicianName = dtRowItem["TechnicianName"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianName"]) : string.Empty;

                        lstJobDetailForSMS.Add(objJobDetailForSMS);

                    }
                }

                APIService objAPIService = new APIService();

                if(lstJobDetailForSMS.Count > 0)
                {
                    foreach (JobDetailForSMS item in lstJobDetailForSMS)
                    {
                        if (!string.IsNullOrEmpty(item.CustomerName) && !string.IsNullOrEmpty(item.MobileNo))
                        {
                            objAPIService.SendSMSForCall(JobSMSSendCategory.TechnicianAllocation, new JobDetailForSMS() { CustomerName = item.CustomerName , MobileNo = item.MobileNo, JobNo = item.JobNo, TechnicianName = item.TechnicianName });
                        }
                    }

                }
                


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objResponceModel;
        }


        public ResponceModel CreateNewCallFromOldJonNo(string OldJobNo)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();
            int LoginUserId = UserSesionDetail != null ? UserSesionDetail.id : 0;

            strQuery = @"CreatenewCallFromOldJobNo";

            try
            {
                objBaseDAL = new BaseDAL();

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@OldJobNo", OldJobNo),
                                new SqlParameter("@LoginUserId", LoginUserId),
                      });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);


                if (ResDataSet.Tables.Count > 0)
                {

                    DataTable ResponceTable = ResDataSet.Tables[0];

                    DataRow dtRowItem = ResponceTable.Rows[0];

                    objResponceModel.Responce = dtRowItem["OperationSuccess"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["OperationSuccess"]) : false;
                    objResponceModel.Message = dtRowItem["ResponceMessage"] != DBNull.Value ? Convert.ToString(dtRowItem["ResponceMessage"]) : string.Empty;
                }

                if (objResponceModel.Responce)
                {
                    DataTable CallRegistationTable = ResDataSet.Tables[1];
                    DataRow dtRowItem = CallRegistationTable.Rows[0];

                    JobDetailForSMS objJobDetailForSMS = new JobDetailForSMS()
                    {
                        CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty,
                        MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty,
                        JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty,
                    };

                    SendSMSForNewCall(objJobDetailForSMS);
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);

                objResponceModel = new ResponceModel();
                objResponceModel.Responce = false;
                objResponceModel.Message = "Somthing Went Wrong!";
            }

            return objResponceModel;

        }

        public void SendSMSForNewCall(JobDetailForSMS objJobDetailForSMS)
        {
            APIService objAPIService = new APIService();

            if(objJobDetailForSMS != null && !string.IsNullOrEmpty(objJobDetailForSMS.CustomerName) && !string.IsNullOrEmpty(objJobDetailForSMS.MobileNo) && !string.IsNullOrEmpty(objJobDetailForSMS.JobNo))
            {
                objAPIService.SendSMSForCall(JobSMSSendCategory.CallRegister, objJobDetailForSMS);
            }

        }

        public void UpdatePincodeForCustomerMaster(string CustomerId, string Pincode)
        {

            strQuery = @"UpdatePincodeForCustomerMaster";

            try
            {
                objBaseDAL = new BaseDAL();

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@CstomerId", new Guid(CustomerId)),
                                new SqlParameter("@Pincode", Pincode),
                      });

                objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
        }

        public string GetTechnicianByCallId(string CallId)
        {
            string strTechnicianId = string.Empty;

            strQuery = @"SELECT Technician FROM CallRegistration WHERE Oid = @CallId";

            try
            {
                objBaseDAL = new BaseDAL();

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@CallId", CallId),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowItem = ResDataTable.Rows[0];

                    strTechnicianId = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);

                strTechnicianId = string.Empty;
            }

            return strTechnicianId;

        }


        public ListViewDataModel<InstallationsTable> GetInstallationTableList(int SortCol, string SortDir, int PageIndex, int PageSize, string JobNo, string CustomerName, string CustomerNo, string TechnicianName, DateTime? FromDate, DateTime? ToDate)
        {
            ListViewDataModel<InstallationsTable> objListViewDataModel = new ListViewDataModel<InstallationsTable>();
            objListViewDataModel.DataList = new List<InstallationsTable>();

            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"Installation_Images_List";

                lstParam = new List<SqlParameter>();


                SqlParameter SortCol_Param = new SqlParameter() { ParameterName = "@SortCol", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = SortCol };
                SqlParameter SortDir_Param = new SqlParameter() { ParameterName = "@SortDir", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = SortDir };
                SqlParameter PageIndex_Param = new SqlParameter() { ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageIndex };
                SqlParameter PageSize_Param = new SqlParameter() { ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageSize };
                SqlParameter JobNo_Param = !string.IsNullOrEmpty(JobNo) ? new SqlParameter() { ParameterName = "@JobNo", Value = JobNo } : new SqlParameter() { ParameterName = "@JobNo", Value = DBNull.Value };
                SqlParameter CustomerName_Param = !string.IsNullOrEmpty(CustomerName) ? new SqlParameter() { ParameterName = "@CustomerName", Value = CustomerName } : new SqlParameter() { ParameterName = "@CustomerName", Value = DBNull.Value };
                SqlParameter CustomerNo_Param = !string.IsNullOrEmpty(CustomerNo) ? new SqlParameter() { ParameterName = "@CustomerNo", Value = CustomerNo } : new SqlParameter() { ParameterName = "@CustomerNo", Value = DBNull.Value };
                SqlParameter TechnicianName_Param = !string.IsNullOrEmpty(TechnicianName) ? new SqlParameter() { ParameterName = "@TechnicianName", Value = TechnicianName } : new SqlParameter() { ParameterName = "@TechnicianName", Value = DBNull.Value };
                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };
                SqlParameter TotalRecordCount_Param = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortCol_Param, SortDir_Param, PageIndex_Param, PageSize_Param, JobNo_Param, CustomerName_Param, CustomerNo_Param, TechnicianName_Param, FromDate_Param, ToDate_Param,  TotalRecordCount_Param });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                TotalRecordCount = Convert.ToInt32(TotalRecordCount_Param.Value);

                string InstallationImages = string.Empty;

                if (ResDataTable.Rows.Count > 0)
                {
                    InstallationsTable objInstallationsTable;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objInstallationsTable = new InstallationsTable();

                        objInstallationsTable.RowNo = dtRowItem["RowNo"] != DBNull.Value ? Convert.ToInt32(dtRowItem["RowNo"]) : 0;
                        objInstallationsTable.Id = dtRowItem["Id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["Id"]) : 0;
                        objInstallationsTable.JobDateTime = dtRowItem["JobDateTime"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["JobDateTime"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : string.Empty;
                        objInstallationsTable.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                        objInstallationsTable.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                        objInstallationsTable.CustomerNo = dtRowItem["CustomerNo"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerNo"]) : string.Empty;
                        //objInstallationsTable.Image = dtRowItem["Image"] != DBNull.Value ? Path.Combine(ImageFolderPath, Convert.ToString(dtRowItem["Image"])) : string.Empty;
                        InstallationImages = Convert.ToString(dtRowItem["Image"]);
                        objInstallationsTable.TechnicianName = dtRowItem["TechnicianName"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianName"]) : string.Empty;
                        objInstallationsTable.TechnicianNo = dtRowItem["TechnicianNo"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianNo"]) : string.Empty;
                        objInstallationsTable.Location = dtRowItem["Location"] != DBNull.Value ? Convert.ToString(dtRowItem["Location"]) : string.Empty;
                        objInstallationsTable.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;

                        if(!string.IsNullOrEmpty(InstallationImages))
                        {
                            InstallationImages objInstallationImages = new InstallationImages();
                            //objInstallationImages.ImageList = new List<string>();

                            try
                            {
                                objInstallationImages = JsonConvert.DeserializeObject<InstallationImages>(InstallationImages);

                                string ImageName = string.Empty;

                                if (objInstallationImages != null && objInstallationImages.ImageList.Count > 0)
                                {
                                    objInstallationsTable.ImageList = new List<string>();

                                    foreach (string imageItem in objInstallationImages.ImageList)
                                    {
                                        ImageName = imageItem;
                                        objInstallationsTable.ImageList.Add(ImageName);
                                    }
                                }
                                
                            }
                            catch (Exception ex)
                            {
                                objInstallationsTable.Image = string.Empty;
                            }
                        }
                        
                        objListViewDataModel.DataList.Add(objInstallationsTable);

                    }
                    objListViewDataModel.RecordCount = TotalRecordCount;
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objListViewDataModel;
        }

    }
}