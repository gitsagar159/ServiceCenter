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

namespace ServiceCenter.Services
{
    public class ReportService
    {
        string strQuery = "";

        private BaseDAL objBaseDAL;
        private List<SqlParameter> lstParam;

        public CallRegistrationListDataModel GetCallRegistrationReportList (int SortCol, string SortDir, int PageIndex, int PageSize, DateTime FromDate, DateTime ToDate)
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();


            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"CallRegistrationReportList";

                lstParam = new List<SqlParameter>();

                SqlParameter SortCol_Param = new SqlParameter() { ParameterName = "@SortCol", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = SortCol };
                SqlParameter SortDir_Param = new SqlParameter() { ParameterName = "@SortDir", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = SortDir };
                SqlParameter PageIndex_Param = new SqlParameter() { ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageIndex };
                SqlParameter PageSize_Param = new SqlParameter() { ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageSize };
                SqlParameter FromDate_Param = new SqlParameter() { ParameterName = "@FromDate", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = FromDate };
                SqlParameter ToDate_Param = new SqlParameter() { ParameterName = "@ToDate", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = ToDate };
                SqlParameter TotalRecordCountParam = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortCol_Param, SortDir_Param, PageIndex_Param, PageSize_Param, FromDate_Param, ToDate_Param, TotalRecordCountParam });

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


        public byte[] GetCallRegisterExportData(DateTime FromDate, DateTime ToDate)
        {
            byte[] bytes = null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"GetCallRegisterDataForExport";

                lstParam = new List<SqlParameter>();


                SqlParameter FromDate_Param = new SqlParameter() { ParameterName = "@FromDate", Value = FromDate };
                SqlParameter ToDate_Param = new SqlParameter() { ParameterName = "@ToDate", Value = ToDate };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    CallRegistration ObjCallRegistration;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        ObjCallRegistration = new CallRegistration();
                        /*
                        objCallRegistration.Oid = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        objCallRegistration.CreationDate = dtRowItem["CreationDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CreationDate"]) : (DateTime?)null;
                        objCallRegistration.CallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]) : (DateTime?)null;
                        objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                        objCallRegistration.Address = dtRowItem["Address"] != DBNull.Value ? Convert.ToString(dtRowItem["Address"]) : string.Empty;
                        objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                        objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                        objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                        objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                        objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                        objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                        objCallRegistration.ProductName = dtRowItem["ProductName"] != DBNull.Value ? Convert.ToString(dtRowItem["ProductName"]) : string.Empty;
                        */

                        ObjCallRegistration.Oid = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        ObjCallRegistration.Customer = dtRowItem["Customer"] != DBNull.Value ? Convert.ToString(dtRowItem["Customer"]) : string.Empty;
                        ObjCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;

                        ObjCallRegistration.Address = dtRowItem["Address"] != DBNull.Value ? Convert.ToString(dtRowItem["Address"]) : string.Empty;

                        ObjCallRegistration.Area = dtRowItem["AreaId"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaId"]) : string.Empty;
                        ObjCallRegistration.AreaName = dtRowItem["AreaName"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaName"]) : string.Empty;

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
                        ObjCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                        
                        ObjCallRegistration.EstConfirmDate = dtRowItem["EstConfirmDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["EstConfirmDate"]) : (DateTime?)null;
                        ObjCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;

                        ObjCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                        ObjCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;

                        ObjCallRegistration.ItemId = dtRowItem["ItemId"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemId"]) : string.Empty;
                        ObjCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;

                        ObjCallRegistration.Technician = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                        ObjCallRegistration.TechnicianName = dtRowItem["TechnicianName"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianName"]) : string.Empty;

                        ObjCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;

                        ObjCallRegistration.FaultType = dtRowItem["FaultTypeId"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultTypeId"]) : string.Empty;
                        ObjCallRegistration.FaultTypeName = dtRowItem["FaultTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultTypeName"]) : string.Empty;

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


                        objCallRegistrationListDataModel.CallRegistrationList.Add(ObjCallRegistration);

                    }
                    objCallRegistrationListDataModel.RecordCount = objCallRegistrationListDataModel.CallRegistrationList.Count;
                }

                if (objCallRegistrationListDataModel != null && objCallRegistrationListDataModel.CallRegistrationList.Count > 0)
                {

                    var memoryStream = new MemoryStream();
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var excelPackage = new ExcelPackage(memoryStream))
                    {
                        var worksheet = excelPackage.Workbook.Worksheets.Add("Call_Registration");
                        int intRow = 1;
                        worksheet.Cells[intRow, 1].Value = "Sr No.";
                        worksheet.Cells[intRow, 2].Value = "Job No.";
                        worksheet.Cells[intRow, 3].Value = "Creation Date";
                        worksheet.Cells[intRow, 4].Value = "Call Assign Date";
                        worksheet.Cells[intRow, 5].Value = "Customer Name";
                        worksheet.Cells[intRow, 6].Value = "Address";
                        worksheet.Cells[intRow, 7].Value = "Call Type";
                        worksheet.Cells[intRow, 8].Value = "Service Type";
                        worksheet.Cells[intRow, 9].Value = "Technician";
                        worksheet.Cells[intRow, 10].Value = "Mobile No";

                        worksheet.Cells[intRow, 11].Value = "Call Attn";
                        worksheet.Cells[intRow, 12].Value = "Job Done";
                        worksheet.Cells[intRow, 13].Value = "Product Name";
                        worksheet.Cells[intRow, 14].Value = "Deal.Ref/Name";
                        worksheet.Cells[intRow, 15].Value = "Company Complaint no.";
                        worksheet.Cells[intRow, 16].Value = "Estimate Date";
                        worksheet.Cells[intRow, 17].Value = "Payment";
                        worksheet.Cells[intRow, 18].Value = "Estimate Confirm Date";
                        worksheet.Cells[intRow, 19].Value = "Estimate";
                        worksheet.Cells[intRow, 20].Value = "Item Name";

                        worksheet.Cells[intRow, 21].Value = "Fault Type";
                        worksheet.Cells[intRow, 22].Value = "Fault Description";
                        worksheet.Cells[intRow, 23].Value = "Model Name";
                        worksheet.Cells[intRow, 24].Value = "Special Instuction";
                        worksheet.Cells[intRow, 25].Value = "Region For Job Done";
                        worksheet.Cells[intRow, 26].Value = "Region For Job Not Done";
                        worksheet.Cells[intRow, 27].Value = "Repeat From Tech";
                        worksheet.Cells[intRow, 28].Value = "Call Back";
                        worksheet.Cells[intRow, 29].Value = "Workshop In";
                        worksheet.Cells[intRow, 30].Value = "SMS Sent";
                        worksheet.Cells[intRow, 31].Value = "bill No.";
                        worksheet.Cells[intRow, 32].Value = "Technican Bill No.";
                        worksheet.Cells[intRow, 33].Value = "Payment Pending";
                        worksheet.Cells[intRow, 34].Value = "Purchase Date";
                        worksheet.Cells[intRow, 35].Value = "Deliverd";
                        worksheet.Cells[intRow, 36].Value = "Cancled";
                        worksheet.Cells[intRow, 37].Value = "Go After Call";
                        worksheet.Cells[intRow, 38].Value = "Part Pending";
                        worksheet.Cells[intRow, 39].Value = "Payment By";
                        worksheet.Cells[intRow, 40].Value = "Visit Charge";

                        worksheet.Cells[intRow, 1, intRow, 40].Style.Font.Bold = true;

                        intRow++;

                        int intSrNoCounter = 1;


                        foreach (CallRegistration callItem in objCallRegistrationListDataModel.CallRegistrationList)
                        {
                            worksheet.Cells[intRow, 1].Value = intSrNoCounter;

                            worksheet.Cells[intRow, 2].Value = callItem.JobNo;

                            worksheet.Cells[intRow, 3].Value = callItem.CreationDate;
                            worksheet.Cells[intRow, 3].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 4].Value = callItem.CallAssignDate;
                            worksheet.Cells[intRow, 4].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 5].Value = callItem.CustomerName;
                            worksheet.Cells[intRow, 6].Value = callItem.Address;
                            worksheet.Cells[intRow, 7].Value = callItem.CallTypeName; //CommonService.GetCallTypeNameById(callItem.CallType);
                            worksheet.Cells[intRow, 8].Value = callItem.ServTypeName; //CommonService.GetServiceTypeNameById(callItem.ServType);
                            worksheet.Cells[intRow, 9].Value = callItem.TechnicianName;
                            worksheet.Cells[intRow, 10].Value = callItem.MobileNo;
                            worksheet.Cells[intRow, 11].Value = callItem.CallAttn ? "Yes" : "No";
                            worksheet.Cells[intRow, 12].Value = callItem.JobDone ? "Yes" : "No";
                            worksheet.Cells[intRow, 13].Value = callItem.ProductName;

                            worksheet.Cells[intRow, 14].Value = callItem.DealRef;
                            worksheet.Cells[intRow, 15].Value = callItem.CompComplaintNo;
                            worksheet.Cells[intRow, 16].Value = callItem.EstimateDate;
                            worksheet.Cells[intRow, 16].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 17].Value = callItem.Payment;
                            worksheet.Cells[intRow, 18].Value = callItem.EstConfirmDate;
                            worksheet.Cells[intRow, 18].Style.Numberformat.Format = "dd/mm/yyyy";


                            worksheet.Cells[intRow, 19].Value = callItem.Estimate;
                            worksheet.Cells[intRow, 20].Value = callItem.ItemName;
                            
                            worksheet.Cells[intRow, 21].Value = callItem.FaultTypeName;
                            worksheet.Cells[intRow, 22].Value = callItem.FaultDesc;
                            worksheet.Cells[intRow, 23].Value = callItem.ModelName;
                            worksheet.Cells[intRow, 24].Value = callItem.SpInst;
                            worksheet.Cells[intRow, 25].Value = callItem.JobDoneRegion;
                            worksheet.Cells[intRow, 26].Value = callItem.JobRegion;

                            worksheet.Cells[intRow, 27].Value = callItem.RepeatFromTech ? "Yes" : "No";
                            worksheet.Cells[intRow, 28].Value = callItem.CallBack ? "Yes" : "No";
                            worksheet.Cells[intRow, 29].Value = callItem.WorkShopIN ? "Yes" : "No";
                            worksheet.Cells[intRow, 30].Value = callItem.SMSSent ? "Yes" : "No";

                            worksheet.Cells[intRow, 31].Value = callItem.BillNo;
                            worksheet.Cells[intRow, 32].Value = callItem.TechBillNo;

                            worksheet.Cells[intRow, 33].Value = callItem.PaymentPanding ? "Yes" : "No";

                            worksheet.Cells[intRow, 34].Value = callItem.PurchaseDate;
                            worksheet.Cells[intRow, 34].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 35].Value = callItem.Deliver ? "Yes" : "No";
                            worksheet.Cells[intRow, 36].Value = callItem.Canceled ? "Yes" : "No";
                            worksheet.Cells[intRow, 37].Value = callItem.GoAfterCall ? "Yes" : "No";
                            worksheet.Cells[intRow, 38].Value = callItem.PartPanding ? "Yes" : "No";
                            worksheet.Cells[intRow, 39].Value = callItem.PaymentBy;
                            worksheet.Cells[intRow, 40].Value = callItem.VisitCharge;



                            intRow++;
                            intSrNoCounter++;
                        }

                        //worksheet.Cells[intRow, 41].ToString();

                        worksheet.Cells["A1:" + worksheet.Cells[intRow, 40].ToString()].AutoFitColumns();

                        bytes = excelPackage.GetAsByteArray();
                    }

                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return bytes;
        }


        public CallRegistration GetWorkorderDetailByCallId(string CallId)
        {
            CallRegistration ObjCallRegistration = new CallRegistration();


            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"GetWorkorderPrintData";

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@CallId", CallId),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {

                    DataRow dtRowItem = ResDataTable.Rows[0];

                    ObjCallRegistration.Oid = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                    ObjCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                    ObjCallRegistration.CallDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]) : (DateTime?)null;
                    ObjCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                    ObjCallRegistration.Address = dtRowItem["Address"] != DBNull.Value ? Convert.ToString(dtRowItem["Address"]) : string.Empty;
                    ObjCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                    ObjCallRegistration.ProductName = dtRowItem["ProductName"] != DBNull.Value ? Convert.ToString(dtRowItem["ProductName"]) : string.Empty;
                    ObjCallRegistration.PurchaseDate = dtRowItem["PurchaseDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["PurchaseDate"]) : (DateTime?)null;
                    ObjCallRegistration.BillNo = dtRowItem["BillNo"] != DBNull.Value ? Convert.ToString(dtRowItem["BillNo"]) : string.Empty;
                    ObjCallRegistration.SrNo = dtRowItem["SrNo"] != DBNull.Value ? Convert.ToString(dtRowItem["SrNo"]) : string.Empty;
                    ObjCallRegistration.ModelName = dtRowItem["ModelName"] != DBNull.Value ? Convert.ToString(dtRowItem["ModelName"]) : string.Empty;
                    ObjCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                    ObjCallRegistration.TechnicianName = dtRowItem["TechnicianName"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianName"]) : string.Empty;
                    ObjCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                    ObjCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                    ObjCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                    ObjCallRegistration.JobDoneRegion = dtRowItem["JobDoneRegion"] != DBNull.Value ? Convert.ToString(dtRowItem["JobDoneRegion"]) : string.Empty;
                    ObjCallRegistration.SpInst = dtRowItem["SpInst"] != DBNull.Value ? Convert.ToString(dtRowItem["SpInst"]) : string.Empty;
                    ObjCallRegistration.CityName = dtRowItem["CityName"] != DBNull.Value ? Convert.ToString(dtRowItem["CityName"]) : string.Empty;

                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return ObjCallRegistration;
        }

    }
}