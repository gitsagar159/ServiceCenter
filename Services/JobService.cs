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
using System.Web;
using System.Web.Mvc;

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

        public CallRegistrationListDataModel GetCallRegisterListBySP(int SortCol, string SortDir, int PageIndex, int PageSize, string Keyword, int CallCategory)
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();


            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();



                strQuery = @"JobList";

                lstParam = new List<SqlParameter>();

                SqlParameter SortColParam = new SqlParameter() { ParameterName = "@SortCol", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = SortCol };
                SqlParameter SortDirParam = new SqlParameter() { ParameterName = "@SortDir", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = SortDir };
                SqlParameter PageIndexParam = new SqlParameter() { ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageIndex };
                SqlParameter PageSizeParam = new SqlParameter() { ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageSize };
                SqlParameter KeywordParam = new SqlParameter() { ParameterName = "@Keyword", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = Keyword };
                SqlParameter CallCategoryParam = new SqlParameter() { ParameterName = "@CallCategory", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = CallCategory };
                SqlParameter TotalRecordCountParam = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortColParam, SortDirParam, PageIndexParam, PageSizeParam, KeywordParam, CallCategoryParam, TotalRecordCountParam });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                TotalRecordCount = Convert.ToInt32(TotalRecordCountParam.Value);

                if (ResDataTable.Rows.Count > 0)
                {
                    CallRegistration objCallRegistration;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objCallRegistration = new CallRegistration();

                        objCallRegistration.RowNo = dtRowItem["RowNo"] != DBNull.Value ? Convert.ToInt64(dtRowItem["RowNo"]) : 0;
                        objCallRegistration.Oid = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        objCallRegistration.StringCreationDate = dtRowItem["CreationDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CreationDate"]).ToString("dd'/'MM'/'yyy") : "-";
                        objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy") : "-";
                        objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                        objCallRegistration.Address = dtRowItem["Address"] != DBNull.Value ? Convert.ToString(dtRowItem["Address"]) : string.Empty;
                        objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                        objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                        objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                        objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                        objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                        objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                        objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                        objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                        objCallRegistration.ProductName = dtRowItem["ProductName"] != DBNull.Value ? Convert.ToString(dtRowItem["ProductName"]) : string.Empty;

                        objCallRegistrationListDataModel.CallRegistrationList.Add(objCallRegistration);

                    }
                    objCallRegistrationListDataModel.RecordCount = TotalRecordCount;
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
	                            CR.Area AS AreaId,
	                            A.AreaName AS AreaName, 
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
	                            LEFT JOIN AREA A on A.Oid = CR.Area
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
                    ObjCallRegistration.Customer = dtRowItem["Customer"] != DBNull.Value ? Convert.ToString(dtRowItem["Customer"]) : string.Empty;

                    objSelect2Data.Select2Customer = new Select2() { id = ObjCallRegistration.Customer, text = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty };

                    ObjCallRegistration.Address = dtRowItem["Address"] != DBNull.Value ? Convert.ToString(dtRowItem["Address"]) : string.Empty;
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

                    objSelect2Data.Select2Technician = new Select2() { id = ObjCallRegistration.Technician, text = dtRowItem["TechnicianName"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianName"]) : string.Empty };

                    ObjCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                    ObjCallRegistration.FaultType = dtRowItem["FaultTypeId"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultTypeId"]) : string.Empty;

                    objSelect2Data.Select2FaultType = new Select2() { id = ObjCallRegistration.FaultType, text = dtRowItem["FaultTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultTypeName"]) : string.Empty };

                    ObjCallRegistration.ModelName = dtRowItem["ModelName"] != DBNull.Value ? Convert.ToString(dtRowItem["ModelName"]) : string.Empty;
                    ObjCallRegistration.SpInst = dtRowItem["SpInst"] != DBNull.Value ? Convert.ToString(dtRowItem["SpInst"]) : string.Empty;
                    ObjCallRegistration.JobDoneRegion = dtRowItem["JobDoneRegion"] != DBNull.Value ? Convert.ToString(dtRowItem["JobDoneRegion"]) : string.Empty;
                    ObjCallRegistration.ProductName = dtRowItem["ProductName"] != DBNull.Value ? Convert.ToString(dtRowItem["ProductName"]) : string.Empty;
                    ObjCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                    ObjCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
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

        [HttpPost]
        public ResponceModel InsertUpdateCallRegistration(CallRegistration objCallRegistration)
        {
            ResponceModel objResponce = new ResponceModel();

            if (objCallRegistration != null)
            {

                User UserSesionDetail = SessionService.GetUserSessionValues();


                try
                {
                    objBaseDAL = new BaseDAL();
                    string InsertQuery = @"INSERT INTO 
	                                            CallRegistration(
	                                            Oid,
	                                            CallAssignDate,
	                                            Customer,
	                                            CustomerName,
	                                            Address,
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
	                                            CreationDate
	                                            )
	                                            VALUES
	                                            (
	                                            CAST(@Oid AS UNIQUEIDENTIFIER),
	                                            @CallAssignDate,
	                                            CAST(@Customer AS UNIQUEIDENTIFIER),
	                                            @CustomerName,
	                                            @Address,
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
	                                            @CreationDate
	                                            )";



                    string UpdateQuery = @"Update 
												CallRegistration
											SET	
                                                CallAssignDate = @CallAssignDate,
                                                Customer = CAST(@Customer AS UNIQUEIDENTIFIER),
                                                CustomerName =  @CustomerName,
                                                Address = @Address,
                                                Area  = CAST(@Area AS UNIQUEIDENTIFIER),
                                                MobileNo = @MobileNo,
                                                CallDate = @CallDate,
                                                ID  = @ID ,
                                                SrNo = @SrNo,
                                                JobNo = @JobNo,
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
                                                UserName  = @UserName ,
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
                    SqlParameter UserName_Param = !string.IsNullOrEmpty(objCallRegistration.UserName) ? new SqlParameter() { ParameterName = "@UserName", Value = objCallRegistration.UserName } : new SqlParameter() { ParameterName = "@UserName", Value = DBNull.Value };
                    SqlParameter CreationDate_Param = new SqlParameter() { ParameterName = "@CreationDate", Value = objCallRegistration.CreationDate };
                    SqlParameter CompComplaintNo_Param = !string.IsNullOrEmpty(objCallRegistration.CompComplaintNo) ? new SqlParameter() { ParameterName = "@CompComplaintNo", Value = objCallRegistration.CompComplaintNo } : new SqlParameter() { ParameterName = "@CompComplaintNo", Value = DBNull.Value };
                    SqlParameter JobNo_Param = !string.IsNullOrEmpty(objCallRegistration.JobNo) ? new SqlParameter() { ParameterName = "@JobNo", Value = objCallRegistration.JobNo } : new SqlParameter() { ParameterName = "@JobNo", Value = DBNull.Value };
                    SqlParameter CallDate_Param = objCallRegistration.CallDate.HasValue ? new SqlParameter() { ParameterName = "@CallDate", Value = objCallRegistration.CallDate } : new SqlParameter() { ParameterName = "@CallDate", Value = DBNull.Value };
                    SqlParameter ItemName_Param = !string.IsNullOrEmpty(objCallRegistration.ItemName) ? new SqlParameter() { ParameterName = "@ItemName", Value = objCallRegistration.ItemName } : new SqlParameter() { ParameterName = "@ItemName", Value = DBNull.Value };
                    SqlParameter SrNo_Param = !string.IsNullOrEmpty(objCallRegistration.SrNo) ? new SqlParameter() { ParameterName = "@SrNo", Value = objCallRegistration.SrNo } : new SqlParameter() { ParameterName = "@SrNo", Value = DBNull.Value };
                    SqlParameter FaultDesc_Param = !string.IsNullOrEmpty(objCallRegistration.FaultDesc) ? new SqlParameter() { ParameterName = "@FaultDesc", Value = objCallRegistration.FaultDesc } : new SqlParameter() { ParameterName = "@FaultDesc", Value = DBNull.Value };
                    SqlParameter CallType_Param = new SqlParameter() { ParameterName = "@CallType", Value = objCallRegistration.CallType };
                    SqlParameter Customer_Param = !string.IsNullOrEmpty(objCallRegistration.Customer) ? new SqlParameter() { ParameterName = "@Customer", Value = objCallRegistration.Customer } : new SqlParameter() { ParameterName = "@Customer", Value = DBNull.Value };
                    SqlParameter CustomerName_Param = !string.IsNullOrEmpty(objCallRegistration.CustomerName) ? new SqlParameter() { ParameterName = "@CustomerName", Value = objCallRegistration.CustomerName } : new SqlParameter() { ParameterName = "@CustomerName", Value = DBNull.Value };
                    SqlParameter MobileNo_Param = !string.IsNullOrEmpty(objCallRegistration.MobileNo) ? new SqlParameter() { ParameterName = "@MobileNo", Value = objCallRegistration.MobileNo } : new SqlParameter() { ParameterName = "@MobileNo", Value = DBNull.Value };
                    SqlParameter Address_Param = !string.IsNullOrEmpty(objCallRegistration.Address) ? new SqlParameter() { ParameterName = "@Address", Value = objCallRegistration.Address } : new SqlParameter() { ParameterName = "@Address", Value = DBNull.Value };
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
                    SqlParameter Modifier_Param = !string.IsNullOrEmpty(objCallRegistration.Modifier) ? new SqlParameter() { ParameterName = "@Modifier", Value = objCallRegistration.FaultType } : new SqlParameter() { ParameterName = "@Modifier", Value = DBNull.Value };
                    SqlParameter ModifyDate_Param = objCallRegistration.ModifyDate.HasValue ? new SqlParameter() { ParameterName = "@ModifyDate", Value = objCallRegistration.FaultType } : new SqlParameter() { ParameterName = "@ModifyDate", Value = DBNull.Value };


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
                        lstParam.AddRange(new SqlParameter[]
                          {
                                new SqlParameter("@Oid", new Guid(objCallRegistration.Oid))
                          });

                        objBaseDAL.ExeccuteStoreCommand(UpdateQuery, CommandType.Text, lstParam);
                    }
                    else
                    {
                        lstParam.AddRange(new SqlParameter[]
                          {
                                new SqlParameter("@Oid", Guid.NewGuid()),
                          });

                        objBaseDAL.ExeccuteStoreCommand(InsertQuery, CommandType.Text, lstParam);
                    }

                    objResponce.Responce = true;
                    objResponce.Message = "Call Register Successfuly";


                }
                catch (Exception ex)
                {
                    objResponce = new ResponceModel() { Responce = false, Message = "Something went wrong" };
                    CommonService.WriteErrorLog(ex);
                }
            }



            return objResponce;
        }

        public JobDashboard GetDashboardData()
        {
            JobDashboard objJobDashboard = new JobDashboard();

            try
            {
                objBaseDAL = new BaseDAL();
                strQuery = @"Job_Dashboard_Data";

                lstParam = new List<SqlParameter>();

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);


                if (ResDataSet != null && ResDataSet.Tables.Count > 0)
                {
                    DataTable JobCountTable = ResDataSet.Tables[0];


                    if (JobCountTable.Rows.Count > 0)
                    {
                        DataRow dtRow = JobCountTable.Rows[0];

                        objJobDashboard.TotalJobCount = dtRow["TotalJobCount"] != DBNull.Value ? Convert.ToInt32(dtRow["TotalJobCount"]) : 0;
                        objJobDashboard.JobDoneCount = dtRow["JobDoneCount"] != DBNull.Value ? Convert.ToInt32(dtRow["JobDoneCount"]) : 0;
                        objJobDashboard.OpenJobCount = dtRow["OpenJobCount"] != DBNull.Value ? Convert.ToInt32(dtRow["OpenJobCount"]) : 0;
                        objJobDashboard.NewJobCount = dtRow["NewJobCount"] != DBNull.Value ? Convert.ToInt32(dtRow["NewJobCount"]) : 0;
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

        public List<SelectListItem> GetAreaList(string AreaIdParam = "")
        {
            List<SelectListItem> lstArea = new List<SelectListItem>();

            try
            {
                objBaseDAL = new BaseDAL();
                strQuery = @"SELECT AreaId, AreaName from AreaMaster WHERE IsActive = 1 AND IsDelete = 0 ORDER BY AreaName ASC";

                lstParam = new List<SqlParameter>();

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    SelectListItem objSelectListItem;

                    string AreaId = string.Empty;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objSelectListItem = new SelectListItem();

                        AreaId = dtRowItem["AreaId"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaId"]) : string.Empty;

                        objSelectListItem.Value = AreaId;
                        objSelectListItem.Text = dtRowItem["AreaName"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaName"]) : string.Empty;
                        objSelectListItem.Selected = AreaId == AreaIdParam ? true : false;

                        lstArea.Add(objSelectListItem);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return lstArea;
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
                    SqlParameter CityId_Param = !string.IsNullOrEmpty(ObjCustomerMaster.CityId) ? new SqlParameter() { ParameterName = "@CityId", Value = ObjCustomerMaster.CityId } : new SqlParameter() { ParameterName = "@CityId", Value = DBNull.Value };
                    SqlParameter PhoneH_Param = !string.IsNullOrEmpty(ObjCustomerMaster.PhoneH) ? new SqlParameter() { ParameterName = "@PhoneH", Value = ObjCustomerMaster.PhoneH } : new SqlParameter() { ParameterName = "@PhoneH", Value = DBNull.Value };
                    SqlParameter PhoneO_Param = !string.IsNullOrEmpty(ObjCustomerMaster.PhoneO) ? new SqlParameter() { ParameterName = "@PhoneO", Value = ObjCustomerMaster.PhoneO } : new SqlParameter() { ParameterName = "@PhoneO", Value = DBNull.Value };
                    SqlParameter MobileNo_Param = !string.IsNullOrEmpty(ObjCustomerMaster.MobileNo) ? new SqlParameter() { ParameterName = "@MobileNo", Value = ObjCustomerMaster.MobileNo } : new SqlParameter() { ParameterName = "@MobileNo", Value = DBNull.Value };
                    SqlParameter EMail_Param = !string.IsNullOrEmpty(ObjCustomerMaster.EMail) ? new SqlParameter() { ParameterName = "@EMail", Value = ObjCustomerMaster.EMail } : new SqlParameter() { ParameterName = "@EMail", Value = DBNull.Value };
                    SqlParameter OtherInfo_Param = !string.IsNullOrEmpty(ObjCustomerMaster.OtherInfo) ? new SqlParameter() { ParameterName = "@OtherInfo", Value = ObjCustomerMaster.OtherInfo } : new SqlParameter() { ParameterName = "@OtherInfo", Value = DBNull.Value };
                    SqlParameter SpDay_Param = ObjCustomerMaster.SpDay.HasValue ? new SqlParameter() { ParameterName = "@SpDay", Value = ObjCustomerMaster.SpDay } : new SqlParameter() { ParameterName = "@SpDay", Value = DBNull.Value };
                    SqlParameter LoginUserId_Param = UserSesionDetail != null ? new SqlParameter() { ParameterName = "@LoginUserId", Value = UserSesionDetail.id } : new SqlParameter() { ParameterName = "@LoginUserId", Value = DBNull.Value };
                    SqlParameter CustomerId_Param = new SqlParameter() { ParameterName = "@CustomerId", SqlDbType = SqlDbType.UniqueIdentifier, Direction = ParameterDirection.Output };

                    lstParam.AddRange(new SqlParameter[] { Oid_Param, FirstName_Param, LastName_Param, Address_Param, CityId_Param, PhoneH_Param, PhoneO_Param, MobileNo_Param, EMail_Param, OtherInfo_Param, SpDay_Param, LoginUserId_Param, CustomerId_Param });

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

            strQuery = string.Empty;

            switch (CheckboxType)
            {

                case "CallAttn":
                    strQuery = "UPDATE CallRegistration set CallAttn = @Value WHERE Oid = @CallId";
                    break;

                case "JobDone":
                    strQuery = "UPDATE CallRegistration set JobDone = @Value WHERE Oid = @CallId";
                    break;

                default:
                    strQuery = "";
                    break;
            }

            try
            {
                if(!string.IsNullOrEmpty(strQuery))
                {
                    objBaseDAL = new BaseDAL();

                    lstParam = new List<SqlParameter>();

                    lstParam.AddRange(new SqlParameter[]
                          {
                                new SqlParameter("@CallId", CallId),
                                new SqlParameter("@Value", Value),
                          });

                    objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                    objResponceModel.Responce = true;
                    objResponceModel.Message = "Checkbox value updated in database";
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

    }
}