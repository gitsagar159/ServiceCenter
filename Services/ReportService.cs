using OfficeOpenXml;
using OfficeOpenXml.Table;
using Org.BouncyCastle.Asn1.Ocsp;
using ServiceCenter.Models;
using ServiceCenter.Models.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.WebSockets;

namespace ServiceCenter.Services
{
    public class ReportService
    {
        string strQuery = "";

        private BaseDAL objBaseDAL;
        private List<SqlParameter> lstParam;

        public CallRegistrationListDataModel GetCallRegistrationReportList(int SortCol, string SortDir, int PageIndex, int PageSize, DateTime FromDate, DateTime ToDate, int CallCategory)
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
                SqlParameter CallCategory_Param = new SqlParameter() { ParameterName = "@CallCategory", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = CallCategory };
                SqlParameter TotalRecordCountParam = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortCol_Param, SortDir_Param, PageIndex_Param, PageSize_Param, FromDate_Param, ToDate_Param, CallCategory_Param, TotalRecordCountParam });

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
                        objCallRegistration.IsNew = dtRowItem["IsNew"] != DBNull.Value ? Convert.ToString(dtRowItem["IsNew"]) : string.Empty;

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


        public byte[] GetCallRegisterExportData(DateTime FromDate, DateTime ToDate, int CallCategory)
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
                SqlParameter CallCategory_Param = new SqlParameter() { ParameterName = "@CallCategory", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = CallCategory };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param, CallCategory_Param });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    CallRegistration ObjCallRegistration;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        ObjCallRegistration = new CallRegistration();

                        ObjCallRegistration.Oid = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        ObjCallRegistration.CreationDate = dtRowItem["CreationDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CreationDate"]) : (DateTime?)null;
                        ObjCallRegistration.CallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]) : (DateTime?)null;

                        ObjCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                        ObjCallRegistration.Address = dtRowItem["Address"] != DBNull.Value ? Convert.ToString(dtRowItem["Address"]) : string.Empty;
                        ObjCallRegistration.Pincode = dtRowItem["Pincode"] != DBNull.Value ? Convert.ToString(dtRowItem["Pincode"]) : string.Empty;
                        ObjCallRegistration.AreaName = dtRowItem["AreaName"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaName"]) : string.Empty;
                        ObjCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                        ObjCallRegistration.CallDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]) : (DateTime?)null;
                        //ObjCallRegistration.ID = dtRowItem["ID"] != DBNull.Value ? Convert.ToInt64(dtRowItem["ID"]) : 0;
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
                        //ObjCallRegistration.Technician = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                        ObjCallRegistration.TechnicianName = dtRowItem["TechnicianName"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianName"]) : string.Empty;
                        ObjCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;
                        ObjCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                        ObjCallRegistration.FaultType = dtRowItem["FaultTypeId"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultTypeId"]) : string.Empty;
                        ObjCallRegistration.FaultTypeName = dtRowItem["FaultTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultTypeName"]) : string.Empty;
                        ObjCallRegistration.ModelName = dtRowItem["ModelName"] != DBNull.Value ? Convert.ToString(dtRowItem["ModelName"]) : string.Empty;
                        ObjCallRegistration.SpInst = dtRowItem["SpInst"] != DBNull.Value ? Convert.ToString(dtRowItem["SpInst"]) : string.Empty;
                        ObjCallRegistration.JobDoneRegion = dtRowItem["JobDoneRegion"] != DBNull.Value ? Convert.ToString(dtRowItem["JobDoneRegion"]) : string.Empty;
                        ObjCallRegistration.ProductName = dtRowItem["ProductName"] != DBNull.Value ? Convert.ToString(dtRowItem["ProductName"]) : string.Empty;
                        ObjCallRegistration.SrNo = dtRowItem["SrNo"] != DBNull.Value ? Convert.ToString(dtRowItem["SrNo"]) : string.Empty;
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
                        worksheet.Cells[intRow, 7].Value = "Pincode";

                        worksheet.Cells[intRow, 8].Value = "Call Type";
                        worksheet.Cells[intRow, 9].Value = "Service Type";
                        worksheet.Cells[intRow, 10].Value = "Technician Type";
                        worksheet.Cells[intRow, 11].Value = "Technician";
                        worksheet.Cells[intRow, 12].Value = "Mobile No";
                        worksheet.Cells[intRow, 13].Value = "Call Attn";
                        worksheet.Cells[intRow, 14].Value = "Job Done";
                        worksheet.Cells[intRow, 15].Value = "Product Name";
                        worksheet.Cells[intRow, 16].Value = "Deal.Ref/Name";
                        worksheet.Cells[intRow, 17].Value = "Company Complaint no.";
                        worksheet.Cells[intRow, 18].Value = "Estimate Date";
                        worksheet.Cells[intRow, 19].Value = "Payment";
                        worksheet.Cells[intRow, 20].Value = "Estimate Confirm Date";
                        worksheet.Cells[intRow, 21].Value = "Estimate";
                        worksheet.Cells[intRow, 22].Value = "Item Name";
                        worksheet.Cells[intRow, 23].Value = "Fault Type";
                        worksheet.Cells[intRow, 24].Value = "Fault Description";
                        worksheet.Cells[intRow, 25].Value = "Model Name";
                        worksheet.Cells[intRow, 26].Value = "Special Instuction";
                        worksheet.Cells[intRow, 27].Value = "Region For Job Done";
                        worksheet.Cells[intRow, 28].Value = "Region For Job Not Done";
                        worksheet.Cells[intRow, 29].Value = "Repeat From Tech";
                        worksheet.Cells[intRow, 30].Value = "Call Back";
                        worksheet.Cells[intRow, 31].Value = "Workshop In";
                        worksheet.Cells[intRow, 32].Value = "SMS Sent";
                        worksheet.Cells[intRow, 33].Value = "bill No.";
                        worksheet.Cells[intRow, 34].Value = "Technican Bill No.";
                        worksheet.Cells[intRow, 35].Value = "Payment Pending";
                        worksheet.Cells[intRow, 36].Value = "Purchase Date";
                        worksheet.Cells[intRow, 37].Value = "Deliverd";
                        worksheet.Cells[intRow, 38].Value = "Cancled";
                        worksheet.Cells[intRow, 39].Value = "Go After Call";
                        worksheet.Cells[intRow, 40].Value = "Part Pending";
                        worksheet.Cells[intRow, 41].Value = "Payment By";
                        worksheet.Cells[intRow, 42].Value = "Visit Charge";
                        worksheet.Cells[intRow, 43].Value = "Serial No.";

                        worksheet.Cells[intRow, 1, intRow, 42].Style.Font.Bold = true;

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
                            worksheet.Cells[intRow, 7].Value = callItem.Pincode;

                            worksheet.Cells[intRow, 8].Value = callItem.CallTypeName; //CommonService.GetCallTypeNameById(callItem.CallType);
                            worksheet.Cells[intRow, 9].Value = callItem.ServTypeName; //CommonService.GetServiceTypeNameById(callItem.ServType);
                            worksheet.Cells[intRow, 10].Value = callItem.TechnicianType;
                            worksheet.Cells[intRow, 11].Value = callItem.TechnicianName;
                            worksheet.Cells[intRow, 12].Value = callItem.MobileNo;
                            worksheet.Cells[intRow, 13].Value = callItem.CallAttn ? "Yes" : "No";
                            worksheet.Cells[intRow, 14].Value = callItem.JobDone ? "Yes" : "No";
                            worksheet.Cells[intRow, 15].Value = callItem.ProductName;
                            worksheet.Cells[intRow, 16].Value = callItem.DealRef;
                            worksheet.Cells[intRow, 17].Value = callItem.CompComplaintNo;
                            worksheet.Cells[intRow, 18].Value = callItem.EstimateDate;

                            worksheet.Cells[intRow, 18].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 19].Value = callItem.Payment;
                            worksheet.Cells[intRow, 20].Value = callItem.EstConfirmDate;

                            worksheet.Cells[intRow, 20].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 21].Value = callItem.Estimate;
                            worksheet.Cells[intRow, 22].Value = callItem.ItemName;
                            worksheet.Cells[intRow, 23].Value = callItem.FaultTypeName;
                            worksheet.Cells[intRow, 24].Value = callItem.FaultDesc;
                            worksheet.Cells[intRow, 25].Value = callItem.ModelName;
                            worksheet.Cells[intRow, 26].Value = callItem.SpInst;
                            worksheet.Cells[intRow, 27].Value = callItem.JobDoneRegion;
                            worksheet.Cells[intRow, 28].Value = callItem.JobRegion;
                            worksheet.Cells[intRow, 29].Value = callItem.RepeatFromTech ? "Yes" : "No";
                            worksheet.Cells[intRow, 30].Value = callItem.CallBack ? "Yes" : "No";
                            worksheet.Cells[intRow, 31].Value = callItem.WorkShopIN ? "Yes" : "No";
                            worksheet.Cells[intRow, 32].Value = callItem.SMSSent ? "Yes" : "No";
                            worksheet.Cells[intRow, 33].Value = callItem.BillNo;
                            worksheet.Cells[intRow, 34].Value = callItem.TechBillNo;
                            worksheet.Cells[intRow, 35].Value = callItem.PaymentPanding ? "Yes" : "No";
                            worksheet.Cells[intRow, 36].Value = callItem.PurchaseDate;

                            worksheet.Cells[intRow, 36].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 37].Value = callItem.Deliver ? "Yes" : "No";
                            worksheet.Cells[intRow, 38].Value = callItem.Canceled ? "Yes" : "No";
                            worksheet.Cells[intRow, 39].Value = callItem.GoAfterCall ? "Yes" : "No";
                            worksheet.Cells[intRow, 40].Value = callItem.PartPanding ? "Yes" : "No";
                            worksheet.Cells[intRow, 41].Value = callItem.PaymentBy;
                            worksheet.Cells[intRow, 42].Value = callItem.VisitCharge;
                            worksheet.Cells[intRow, 43].Value = callItem.SrNo;



                            intRow++;
                            intSrNoCounter++;
                        }

                        //worksheet.Cells[intRow, 41].ToString();

                        worksheet.Cells["A1:" + worksheet.Cells[intRow, 43].ToString()].AutoFitColumns();

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

        public byte[] GetCallRegisterExportForTechnician(string CallIds)
        {
            byte[] bytes = null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"ExportCallListForTechnician";

                lstParam = new List<SqlParameter>();


                
                SqlParameter CallIds_Param = new SqlParameter() { ParameterName = "@CallIds", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = CallIds };

                lstParam.AddRange(new SqlParameter[] { CallIds_Param });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    CallRegistration ObjCallRegistration;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        ObjCallRegistration = new CallRegistration();

                        ObjCallRegistration.Oid = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        ObjCallRegistration.CreationDate = dtRowItem["CreationDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CreationDate"]) : (DateTime?)null;
                        ObjCallRegistration.CallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]) : (DateTime?)null;

                        ObjCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                        ObjCallRegistration.Address = dtRowItem["Address"] != DBNull.Value ? Convert.ToString(dtRowItem["Address"]) : string.Empty;
                        ObjCallRegistration.Pincode = dtRowItem["Pincode"] != DBNull.Value ? Convert.ToString(dtRowItem["Pincode"]) : string.Empty;
                        //ObjCallRegistration.Area = dtRowItem["AreaId"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaId"]) : string.Empty;
                        ObjCallRegistration.AreaName = dtRowItem["AreaName"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaName"]) : string.Empty;
                        ObjCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                        ObjCallRegistration.CallDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]) : (DateTime?)null;
                        //ObjCallRegistration.ID = dtRowItem["ID"] != DBNull.Value ? Convert.ToInt64(dtRowItem["ID"]) : 0;
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
                        //ObjCallRegistration.Technician = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                        ObjCallRegistration.TechnicianName = dtRowItem["TechnicianName"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianName"]) : string.Empty;
                        ObjCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;
                        ObjCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                        ObjCallRegistration.FaultType = dtRowItem["FaultTypeId"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultTypeId"]) : string.Empty;
                        ObjCallRegistration.FaultTypeName = dtRowItem["FaultTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultTypeName"]) : string.Empty;
                        ObjCallRegistration.ModelName = dtRowItem["ModelName"] != DBNull.Value ? Convert.ToString(dtRowItem["ModelName"]) : string.Empty;
                        ObjCallRegistration.SpInst = dtRowItem["SpInst"] != DBNull.Value ? Convert.ToString(dtRowItem["SpInst"]) : string.Empty;
                        ObjCallRegistration.JobDoneRegion = dtRowItem["JobDoneRegion"] != DBNull.Value ? Convert.ToString(dtRowItem["JobDoneRegion"]) : string.Empty;
                        ObjCallRegistration.ProductName = dtRowItem["ProductName"] != DBNull.Value ? Convert.ToString(dtRowItem["ProductName"]) : string.Empty;
                        ObjCallRegistration.SrNo = dtRowItem["SrNo"] != DBNull.Value ? Convert.ToString(dtRowItem["SrNo"]) : string.Empty;
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
                        worksheet.Cells[intRow, 7].Value = "Pincode";

                        worksheet.Cells[intRow, 8].Value = "Call Type";
                        worksheet.Cells[intRow, 9].Value = "Service Type";
                        worksheet.Cells[intRow, 10].Value = "Technician Type";
                        worksheet.Cells[intRow, 11].Value = "Technician";
                        worksheet.Cells[intRow, 12].Value = "Mobile No";
                        worksheet.Cells[intRow, 13].Value = "Call Attn";
                        worksheet.Cells[intRow, 14].Value = "Job Done";
                        worksheet.Cells[intRow, 15].Value = "Product Name";
                        worksheet.Cells[intRow, 16].Value = "Deal.Ref/Name";
                        worksheet.Cells[intRow, 17].Value = "Company Complaint no.";
                        worksheet.Cells[intRow, 18].Value = "Estimate Date";
                        worksheet.Cells[intRow, 19].Value = "Payment";
                        worksheet.Cells[intRow, 20].Value = "Estimate Confirm Date";
                        worksheet.Cells[intRow, 21].Value = "Estimate";
                        worksheet.Cells[intRow, 22].Value = "Item Name";
                        worksheet.Cells[intRow, 23].Value = "Fault Type";
                        worksheet.Cells[intRow, 24].Value = "Fault Description";
                        worksheet.Cells[intRow, 25].Value = "Model Name";
                        worksheet.Cells[intRow, 26].Value = "Special Instuction";
                        worksheet.Cells[intRow, 27].Value = "Region For Job Done";
                        worksheet.Cells[intRow, 28].Value = "Region For Job Not Done";
                        worksheet.Cells[intRow, 29].Value = "Repeat From Tech";
                        worksheet.Cells[intRow, 30].Value = "Call Back";
                        worksheet.Cells[intRow, 31].Value = "Workshop In";
                        worksheet.Cells[intRow, 32].Value = "SMS Sent";
                        worksheet.Cells[intRow, 33].Value = "bill No.";
                        worksheet.Cells[intRow, 34].Value = "Technican Bill No.";
                        worksheet.Cells[intRow, 35].Value = "Payment Pending";
                        worksheet.Cells[intRow, 36].Value = "Purchase Date";
                        worksheet.Cells[intRow, 37].Value = "Deliverd";
                        worksheet.Cells[intRow, 38].Value = "Cancled";
                        worksheet.Cells[intRow, 39].Value = "Go After Call";
                        worksheet.Cells[intRow, 40].Value = "Part Pending";
                        worksheet.Cells[intRow, 41].Value = "Payment By";
                        worksheet.Cells[intRow, 42].Value = "Visit Charge";
                        worksheet.Cells[intRow, 43].Value = "Serial No.";

                        worksheet.Cells[intRow, 1, intRow, 42].Style.Font.Bold = true;

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
                            worksheet.Cells[intRow, 7].Value = callItem.Pincode;

                            worksheet.Cells[intRow, 8].Value = callItem.CallTypeName; //CommonService.GetCallTypeNameById(callItem.CallType);
                            worksheet.Cells[intRow, 9].Value = callItem.ServTypeName; //CommonService.GetServiceTypeNameById(callItem.ServType);
                            worksheet.Cells[intRow, 10].Value = callItem.TechnicianType;
                            worksheet.Cells[intRow, 11].Value = callItem.TechnicianName;
                            worksheet.Cells[intRow, 12].Value = callItem.MobileNo;
                            worksheet.Cells[intRow, 13].Value = callItem.CallAttn ? "Yes" : "No";
                            worksheet.Cells[intRow, 14].Value = callItem.JobDone ? "Yes" : "No";
                            worksheet.Cells[intRow, 15].Value = callItem.ProductName;
                            worksheet.Cells[intRow, 16].Value = callItem.DealRef;
                            worksheet.Cells[intRow, 17].Value = callItem.CompComplaintNo;
                            worksheet.Cells[intRow, 18].Value = callItem.EstimateDate;

                            worksheet.Cells[intRow, 18].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 19].Value = callItem.Payment;
                            worksheet.Cells[intRow, 20].Value = callItem.EstConfirmDate;

                            worksheet.Cells[intRow, 20].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 21].Value = callItem.Estimate;
                            worksheet.Cells[intRow, 22].Value = callItem.ItemName;
                            worksheet.Cells[intRow, 23].Value = callItem.FaultTypeName;
                            worksheet.Cells[intRow, 24].Value = callItem.FaultDesc;
                            worksheet.Cells[intRow, 25].Value = callItem.ModelName;
                            worksheet.Cells[intRow, 26].Value = callItem.SpInst;
                            worksheet.Cells[intRow, 27].Value = callItem.JobDoneRegion;
                            worksheet.Cells[intRow, 28].Value = callItem.JobRegion;
                            worksheet.Cells[intRow, 29].Value = callItem.RepeatFromTech ? "Yes" : "No";
                            worksheet.Cells[intRow, 30].Value = callItem.CallBack ? "Yes" : "No";
                            worksheet.Cells[intRow, 31].Value = callItem.WorkShopIN ? "Yes" : "No";
                            worksheet.Cells[intRow, 32].Value = callItem.SMSSent ? "Yes" : "No";
                            worksheet.Cells[intRow, 33].Value = callItem.BillNo;
                            worksheet.Cells[intRow, 34].Value = callItem.TechBillNo;
                            worksheet.Cells[intRow, 35].Value = callItem.PaymentPanding ? "Yes" : "No";
                            worksheet.Cells[intRow, 36].Value = callItem.PurchaseDate;

                            worksheet.Cells[intRow, 36].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 37].Value = callItem.Deliver ? "Yes" : "No";
                            worksheet.Cells[intRow, 38].Value = callItem.Canceled ? "Yes" : "No";
                            worksheet.Cells[intRow, 39].Value = callItem.GoAfterCall ? "Yes" : "No";
                            worksheet.Cells[intRow, 40].Value = callItem.PartPanding ? "Yes" : "No";
                            worksheet.Cells[intRow, 41].Value = callItem.PaymentBy;
                            worksheet.Cells[intRow, 42].Value = callItem.VisitCharge;
                            worksheet.Cells[intRow, 43].Value = callItem.SrNo;



                            intRow++;
                            intSrNoCounter++;
                        }

                        //worksheet.Cells[intRow, 41].ToString();

                        worksheet.Cells["A1:" + worksheet.Cells[intRow, 43].ToString()].AutoFitColumns();

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
                    ObjCallRegistration.Pincode = dtRowItem["Pincode"] != DBNull.Value ? Convert.ToString(dtRowItem["Pincode"]) : string.Empty;
                    ObjCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                    ObjCallRegistration.PhoneH = dtRowItem["PhoneH"] != DBNull.Value ? Convert.ToString(dtRowItem["PhoneH"]) : string.Empty;
                    ObjCallRegistration.PhoneO = dtRowItem["PhoneO"] != DBNull.Value ? Convert.ToString(dtRowItem["PhoneO"]) : string.Empty;

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

        public List<ItemForSendCompanyReport> GetItemListForCompanyReport()
        {
            List<ItemForSendCompanyReport> lstItemList = new List<ItemForSendCompanyReport>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"SELECT Id, ItemCompanyName, CompanyEmail FROM ItemForSendCompanyReport WHERE IsDelete = 0 ";

                lstParam = new List<SqlParameter>();

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    ItemForSendCompanyReport objItemForSendCompanyReport;


                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objItemForSendCompanyReport = new ItemForSendCompanyReport();

                        objItemForSendCompanyReport.Id = dtRowItem["Id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["Id"]) : 0;
                        objItemForSendCompanyReport.ItemCompanyName = dtRowItem["ItemCompanyName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemCompanyName"]) : string.Empty;
                        objItemForSendCompanyReport.CompanyEmail = dtRowItem["CompanyEmail"] != DBNull.Value ? Convert.ToString(dtRowItem["CompanyEmail"]) : string.Empty;

                        lstItemList.Add(objItemForSendCompanyReport);
                    }

                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return lstItemList; 
        }

        public byte[] GetCallRegisterDataForCmpanyReport(DateTime FromDate, DateTime ToDate, string CompanyName, int CallCategory)
        {
            byte[] bytes = null;

            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"GetCallRegisterDataForCompanyReport";

                lstParam = new List<SqlParameter>();


                SqlParameter FromDate_Param = new SqlParameter() { ParameterName = "@FromDate", Value = FromDate };
                SqlParameter ToDate_Param = new SqlParameter() { ParameterName = "@ToDate", Value = ToDate };
                SqlParameter CompanyName_Param = new SqlParameter() { ParameterName = "@ItemCompanyName", Value = CompanyName };
                SqlParameter CallCategory_Param = new SqlParameter() { ParameterName = "@CallCategory", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = CallCategory };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param, CompanyName_Param, CallCategory_Param });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    CallRegistration ObjCallRegistration;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        ObjCallRegistration = new CallRegistration();

                        ObjCallRegistration.Oid = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                        ObjCallRegistration.CreationDate = dtRowItem["CreationDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CreationDate"]) : (DateTime?)null;
                        ObjCallRegistration.CallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]) : (DateTime?)null;

                        ObjCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                        ObjCallRegistration.Address = dtRowItem["Address"] != DBNull.Value ? Convert.ToString(dtRowItem["Address"]) : string.Empty;
                        ObjCallRegistration.Pincode = dtRowItem["Pincode"] != DBNull.Value ? Convert.ToString(dtRowItem["Pincode"]) : string.Empty;
                        ObjCallRegistration.AreaName = dtRowItem["AreaName"] != DBNull.Value ? Convert.ToString(dtRowItem["AreaName"]) : string.Empty;
                        ObjCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                        ObjCallRegistration.CallDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]) : (DateTime?)null;
                        //ObjCallRegistration.ID = dtRowItem["ID"] != DBNull.Value ? Convert.ToInt64(dtRowItem["ID"]) : 0;
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
                        //ObjCallRegistration.Technician = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                        ObjCallRegistration.TechnicianName = dtRowItem["TechnicianName"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianName"]) : string.Empty;
                        ObjCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;
                        ObjCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                        ObjCallRegistration.FaultType = dtRowItem["FaultTypeId"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultTypeId"]) : string.Empty;
                        ObjCallRegistration.FaultTypeName = dtRowItem["FaultTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultTypeName"]) : string.Empty;
                        ObjCallRegistration.ModelName = dtRowItem["ModelName"] != DBNull.Value ? Convert.ToString(dtRowItem["ModelName"]) : string.Empty;
                        ObjCallRegistration.SpInst = dtRowItem["SpInst"] != DBNull.Value ? Convert.ToString(dtRowItem["SpInst"]) : string.Empty;
                        ObjCallRegistration.JobDoneRegion = dtRowItem["JobDoneRegion"] != DBNull.Value ? Convert.ToString(dtRowItem["JobDoneRegion"]) : string.Empty;
                        ObjCallRegistration.ProductName = dtRowItem["ProductName"] != DBNull.Value ? Convert.ToString(dtRowItem["ProductName"]) : string.Empty;
                        ObjCallRegistration.SrNo = dtRowItem["SrNo"] != DBNull.Value ? Convert.ToString(dtRowItem["SrNo"]) : string.Empty;
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
                        worksheet.Cells[intRow, 7].Value = "Pincode";

                        worksheet.Cells[intRow, 8].Value = "Call Type";
                        worksheet.Cells[intRow, 9].Value = "Service Type";
                        worksheet.Cells[intRow, 10].Value = "Technician Type";
                        worksheet.Cells[intRow, 11].Value = "Technician";
                        worksheet.Cells[intRow, 12].Value = "Mobile No";
                        worksheet.Cells[intRow, 13].Value = "Call Attn";
                        worksheet.Cells[intRow, 14].Value = "Job Done";
                        worksheet.Cells[intRow, 15].Value = "Product Name";
                        worksheet.Cells[intRow, 16].Value = "Deal.Ref/Name";
                        worksheet.Cells[intRow, 17].Value = "Company Complaint no.";
                        worksheet.Cells[intRow, 18].Value = "Estimate Date";
                        worksheet.Cells[intRow, 19].Value = "Payment";
                        worksheet.Cells[intRow, 20].Value = "Estimate Confirm Date";
                        worksheet.Cells[intRow, 21].Value = "Estimate";
                        worksheet.Cells[intRow, 22].Value = "Item Name";
                        worksheet.Cells[intRow, 23].Value = "Fault Type";
                        worksheet.Cells[intRow, 24].Value = "Fault Description";
                        worksheet.Cells[intRow, 25].Value = "Model Name";
                        worksheet.Cells[intRow, 26].Value = "Special Instuction";
                        worksheet.Cells[intRow, 27].Value = "Region For Job Done";
                        worksheet.Cells[intRow, 28].Value = "Region For Job Not Done";
                        worksheet.Cells[intRow, 29].Value = "Repeat From Tech";
                        worksheet.Cells[intRow, 30].Value = "Call Back";
                        worksheet.Cells[intRow, 31].Value = "Workshop In";
                        worksheet.Cells[intRow, 32].Value = "SMS Sent";
                        worksheet.Cells[intRow, 33].Value = "bill No.";
                        worksheet.Cells[intRow, 34].Value = "Technican Bill No.";
                        worksheet.Cells[intRow, 35].Value = "Payment Pending";
                        worksheet.Cells[intRow, 36].Value = "Purchase Date";
                        worksheet.Cells[intRow, 37].Value = "Deliverd";
                        worksheet.Cells[intRow, 38].Value = "Cancled";
                        worksheet.Cells[intRow, 39].Value = "Go After Call";
                        worksheet.Cells[intRow, 40].Value = "Part Pending";
                        worksheet.Cells[intRow, 41].Value = "Payment By";
                        worksheet.Cells[intRow, 42].Value = "Visit Charge";
                        worksheet.Cells[intRow, 43].Value = "Serial No.";

                        worksheet.Cells[intRow, 1, intRow, 42].Style.Font.Bold = true;

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
                            worksheet.Cells[intRow, 7].Value = callItem.Pincode;

                            worksheet.Cells[intRow, 8].Value = callItem.CallTypeName; //CommonService.GetCallTypeNameById(callItem.CallType);
                            worksheet.Cells[intRow, 9].Value = callItem.ServTypeName; //CommonService.GetServiceTypeNameById(callItem.ServType);
                            worksheet.Cells[intRow, 10].Value = callItem.TechnicianType;
                            worksheet.Cells[intRow, 11].Value = callItem.TechnicianName;
                            worksheet.Cells[intRow, 12].Value = callItem.MobileNo;
                            worksheet.Cells[intRow, 13].Value = callItem.CallAttn ? "Yes" : "No";
                            worksheet.Cells[intRow, 14].Value = callItem.JobDone ? "Yes" : "No";
                            worksheet.Cells[intRow, 15].Value = callItem.ProductName;
                            worksheet.Cells[intRow, 16].Value = callItem.DealRef;
                            worksheet.Cells[intRow, 17].Value = callItem.CompComplaintNo;
                            worksheet.Cells[intRow, 18].Value = callItem.EstimateDate;

                            worksheet.Cells[intRow, 18].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 19].Value = callItem.Payment;
                            worksheet.Cells[intRow, 20].Value = callItem.EstConfirmDate;

                            worksheet.Cells[intRow, 20].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 21].Value = callItem.Estimate;
                            worksheet.Cells[intRow, 22].Value = callItem.ItemName;
                            worksheet.Cells[intRow, 23].Value = callItem.FaultTypeName;
                            worksheet.Cells[intRow, 24].Value = callItem.FaultDesc;
                            worksheet.Cells[intRow, 25].Value = callItem.ModelName;
                            worksheet.Cells[intRow, 26].Value = callItem.SpInst;
                            worksheet.Cells[intRow, 27].Value = callItem.JobDoneRegion;
                            worksheet.Cells[intRow, 28].Value = callItem.JobRegion;
                            worksheet.Cells[intRow, 29].Value = callItem.RepeatFromTech ? "Yes" : "No";
                            worksheet.Cells[intRow, 30].Value = callItem.CallBack ? "Yes" : "No";
                            worksheet.Cells[intRow, 31].Value = callItem.WorkShopIN ? "Yes" : "No";
                            worksheet.Cells[intRow, 32].Value = callItem.SMSSent ? "Yes" : "No";
                            worksheet.Cells[intRow, 33].Value = callItem.BillNo;
                            worksheet.Cells[intRow, 34].Value = callItem.TechBillNo;
                            worksheet.Cells[intRow, 35].Value = callItem.PaymentPanding ? "Yes" : "No";
                            worksheet.Cells[intRow, 36].Value = callItem.PurchaseDate;

                            worksheet.Cells[intRow, 36].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 37].Value = callItem.Deliver ? "Yes" : "No";
                            worksheet.Cells[intRow, 38].Value = callItem.Canceled ? "Yes" : "No";
                            worksheet.Cells[intRow, 39].Value = callItem.GoAfterCall ? "Yes" : "No";
                            worksheet.Cells[intRow, 40].Value = callItem.PartPanding ? "Yes" : "No";
                            worksheet.Cells[intRow, 41].Value = callItem.PaymentBy;
                            worksheet.Cells[intRow, 42].Value = callItem.VisitCharge;
                            worksheet.Cells[intRow, 43].Value = callItem.SrNo;



                            intRow++;
                            intSrNoCounter++;
                        }

                        //worksheet.Cells[intRow, 41].ToString();

                        worksheet.Cells["A1:" + worksheet.Cells[intRow, 43].ToString()].AutoFitColumns();

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


        public List<ItemForSendCompanyReport> GenerateCompanyViseReport(DateTime FromDate, DateTime ToDate)
        {

            EmailService objEmailService = new EmailService();
            List<ItemForSendCompanyReport> lstCompanyReportList = new List<ItemForSendCompanyReport>();


            try
            {
                lstCompanyReportList = GetItemListForCompanyReport();


                if(lstCompanyReportList != null && lstCompanyReportList.Count > 0)
                {

                    foreach(ItemForSendCompanyReport reportItem in lstCompanyReportList)
                    {

                        byte[] FileData = null;
                        string strFileName = reportItem.ItemCompanyName + "_Call_Report_" + DateTime.Now.ToString("dd'_'MM'_'yyyy'_'hhmmss") + ".xlsx";

                        string ReportDir = HttpContext.Current.Server.MapPath("~/Content/Reports/" + reportItem.ItemCompanyName);

                        //string ReportDir = ConfigurationManager.AppSettings["CompanyReportPath"].ToString() + "\\" + reportItem.ItemCompanyName;

                        // If directory does not exist, create it
                        if (!Directory.Exists(ReportDir))
                        {
                            Directory.CreateDirectory(ReportDir);
                        }

                        string ReportFileDir = HttpContext.Current.Server.MapPath("~/Content/Reports/" + reportItem.ItemCompanyName + "/" + strFileName);
                        //string ReportFileDir = ReportDir + "\\" + strFileName;

                        FileData = GetCallRegisterDataForCmpanyReport(FromDate, ToDate, reportItem.ItemCompanyName, 0);

                        if (FileData != null)
                        {
                            File.WriteAllBytes(ReportFileDir, FileData);

                            string strToEmail = reportItem.CompanyEmail;
                            string strSubject = "DEMO / INSTALLATION CALL REGISTRATION DATE : " + DateTime.Now.ToString("dd/MM/yyyy");

                            string strBody = @"Dear Sit / Madam
                                                
                                                Please find the attachment
                                                
                                                Regiser a call and reply ASAP.

                                                Thanks";

                            //objEmailService.Sendmail("dsagar159@gmail.com", strBody, strSubject, ReportFileDir);
                            objEmailService.Sendmail(strToEmail, strBody, strSubject, ReportFileDir);

                        }

                    }

                }

                


            }
            catch (Exception ex)
            {

                //string ErrorLogFolderPath = ConfigurationManager.AppSettings["ErrorLogFolderPath"].ToString();
                string ErrorLogFolderPath = HttpContext.Current.Server.MapPath("~/Content/ErrorLog");


                if (!Directory.Exists(ErrorLogFolderPath))
                {
                    Directory.CreateDirectory(ErrorLogFolderPath);
                }

                string ErrorFileName = "ErrorLog_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt";

                string ErrorFilePath = Path.Combine(ErrorLogFolderPath, ErrorFileName);

                if (!File.Exists(ErrorFilePath))
                {
                    using (FileStream fs = File.Create(ErrorFilePath))
                    {

                    }

                    using (StreamWriter writer = new StreamWriter(ErrorFilePath, true))
                    {
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("Date : " + DateTime.Now.ToString());
                        writer.WriteLine();

                        while (ex != null)
                        {
                            writer.WriteLine(ex.GetType().FullName);
                            writer.WriteLine("Message : " + ex.Message);
                            writer.WriteLine("StackTrace : " + ex.StackTrace);

                            ex = ex.InnerException;
                        }
                    }
                }
                else
                {
                    // write file
                    using (StreamWriter writer = new StreamWriter(ErrorFilePath, true))
                    {
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("Date : " + DateTime.Now.ToString());
                        writer.WriteLine();

                        while (ex != null)
                        {
                            writer.WriteLine(ex.GetType().FullName);
                            writer.WriteLine("Message : " + ex.Message);
                            writer.WriteLine("StackTrace : " + ex.StackTrace);

                            ex = ex.InnerException;
                        }
                    }
                }
            }


            return lstCompanyReportList;


        }


        public DailyMailReportStatusListDataModel GetDailyReportMailList(int SortCol, string SortDir, int PageIndex, int PageSize, string CompanyName, DateTime? FromDate, DateTime? ToDate)
        {
            DailyMailReportStatusListDataModel objDailyMailReportStatusListDataModel = new DailyMailReportStatusListDataModel();
            objDailyMailReportStatusListDataModel.DailyMailReportStatusList = new List<DailyMailReportStatus>();

            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"DailyMailReportStatusList";

                lstParam = new List<SqlParameter>();


                SqlParameter SortCol_Param = new SqlParameter() { ParameterName = "@SortCol", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = SortCol };
                SqlParameter SortDir_Param = new SqlParameter() { ParameterName = "@SortDir", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = SortDir };
                SqlParameter PageIndex_Param = new SqlParameter() { ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageIndex };
                SqlParameter PageSize_Param = new SqlParameter() { ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageSize };
                SqlParameter CompanyName_Param = new SqlParameter() { ParameterName = "@CompanyName", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = CompanyName };
                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                SqlParameter TotalRecordCount_Param = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortCol_Param, SortDir_Param, PageIndex_Param, PageSize_Param, CompanyName_Param, FromDate_Param, ToDate_Param, TotalRecordCount_Param });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                TotalRecordCount = Convert.ToInt32(TotalRecordCount_Param.Value);

                if (ResDataTable.Rows.Count > 0)
                {
                    DailyMailReportStatus objDailyMailReportStatus;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objDailyMailReportStatus = new DailyMailReportStatus();

                        objDailyMailReportStatus.RowNo = dtRowItem["RowNo"] != DBNull.Value ? Convert.ToInt32(dtRowItem["RowNo"]) : 0;
                        objDailyMailReportStatus.Id = dtRowItem["Id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["Id"]) : 0;
                        objDailyMailReportStatus.ItemCompanyName = dtRowItem["ItemCompanyName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemCompanyName"]) : string.Empty;
                        objDailyMailReportStatus.CompanyEmail = dtRowItem["CompanyEmail"] != DBNull.Value ? Convert.ToString(dtRowItem["CompanyEmail"]) : string.Empty;
                        objDailyMailReportStatus.ReportName = dtRowItem["ReportName"] != DBNull.Value ? Convert.ToString(dtRowItem["ReportName"]) : string.Empty;
                        objDailyMailReportStatus.SendBySystem = dtRowItem["SendBySystem"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["SendBySystem"]) : false;
                        objDailyMailReportStatus.CreatedOnString = dtRowItem["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CreatedOn"]).ToString("dd'/'MM'/'yyyy'_'HH':'mm':'ss") : string.Empty;
                        objDailyMailReportStatus.SendByUser = dtRowItem["SendByUser"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["SendByUser"]) : false;
                        objDailyMailReportStatus.SendByUserId = dtRowItem["SendByUserId"] != DBNull.Value ? Convert.ToInt32(dtRowItem["SendByUserId"]) : 0;
                        objDailyMailReportStatus.SendByUserDateTimeString = dtRowItem["SendByUserDateTime"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["SendByUserDateTime"]).ToString("dd'/'MM'/'yyyy'_'HH':'mm':'ss") : string.Empty;
                        

                        objDailyMailReportStatusListDataModel.DailyMailReportStatusList.Add(objDailyMailReportStatus);

                    }
                    objDailyMailReportStatusListDataModel.RecordCount = TotalRecordCount;
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objDailyMailReportStatusListDataModel;
        }

        #region CompanyEmail
        public ResponceModel InsertUpdateCompanyEmail(int Id, string ItemCompanyName, string CompanyEmail)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"InsertUpdateCompanyEmailDetail";

                lstParam = new List<SqlParameter>();

                SqlParameter Id_Param = new SqlParameter() { ParameterName = "@Id", Value = Id };
                SqlParameter ItemCompanyName_Param = new SqlParameter() { ParameterName = "@ItemCompanyName", Value = ItemCompanyName };
                SqlParameter CompanyEmail_Param = new SqlParameter() { ParameterName = "@CompanyEmail", Value = CompanyEmail };
                SqlParameter LoginUserIdParam = new SqlParameter() { ParameterName = "@LoginUserId", Value = UserSesionDetail.id };

                lstParam.AddRange(new SqlParameter[] { Id_Param, ItemCompanyName_Param, CompanyEmail_Param, LoginUserIdParam });

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

        public ItemForSendCompanyReport GetCompanyEmailDetails(int Id)
        {
            ItemForSendCompanyReport objCompanyEmail = new ItemForSendCompanyReport();

            try
            {
                objBaseDAL = new BaseDAL();
                strQuery = @"SELECT Id, ItemCompanyName, CompanyEmail FROM ItemForSendCompanyReport WHERE Id = @Id";

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                         {
                                new SqlParameter("@Id", Id),
                         });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowItem = ResDataTable.Rows[0];

                    objCompanyEmail.Id = dtRowItem["Id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["Id"]) : 0;
                    objCompanyEmail.ItemCompanyName = dtRowItem["ItemCompanyName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemCompanyName"]) : string.Empty;
                    objCompanyEmail.CompanyEmail = dtRowItem["CompanyEmail"] != DBNull.Value ? Convert.ToString(dtRowItem["CompanyEmail"]) : string.Empty;
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objCompanyEmail;
        }

        public ItemForSendCompanyReportListDataModel GetCompanyEmailList(int SortCol, string SortDir, int PageIndex, int PageSize, string CompanyName, string CompanyEmail)
        {
            ItemForSendCompanyReportListDataModel objCompanyEmailListDataModel = new ItemForSendCompanyReportListDataModel();
            objCompanyEmailListDataModel.ItemForSendCompanyReportList = new List<ItemForSendCompanyReport>();

            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"ItemForSendCompanyReportList";

                lstParam = new List<SqlParameter>();


                SqlParameter SortCol_Param = new SqlParameter() { ParameterName = "@SortCol", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = SortCol };
                SqlParameter SortDir_Param = new SqlParameter() { ParameterName = "@SortDir", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = SortDir };
                SqlParameter PageIndex_Param = new SqlParameter() { ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageIndex };
                SqlParameter PageSize_Param = new SqlParameter() { ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageSize };
                SqlParameter CompanyName_Param = new SqlParameter() { ParameterName = "@ItemCompanyName", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = CompanyName };
                SqlParameter CompanyEmail_Param = new SqlParameter() { ParameterName = "@CompanyEmail", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = CompanyEmail };
                SqlParameter TotalRecordCount_Param = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortCol_Param, SortDir_Param, PageIndex_Param, PageSize_Param, CompanyName_Param, CompanyEmail_Param, TotalRecordCount_Param });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                TotalRecordCount = Convert.ToInt32(TotalRecordCount_Param.Value);

                if (ResDataTable.Rows.Count > 0)
                {
                    ItemForSendCompanyReport objCompanyEmail;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objCompanyEmail = new ItemForSendCompanyReport();

                        objCompanyEmail.RowNo = dtRowItem["RowNo"] != DBNull.Value ? Convert.ToInt32(dtRowItem["RowNo"]) : 0;
                        objCompanyEmail.Id = dtRowItem["Id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["Id"]) : 0;
                        objCompanyEmail.ItemCompanyName = dtRowItem["ItemCompanyName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemCompanyName"]) : string.Empty;
                        objCompanyEmail.CompanyEmail = dtRowItem["CompanyEmail"] != DBNull.Value ? Convert.ToString(dtRowItem["CompanyEmail"]) : string.Empty;

                        objCompanyEmailListDataModel.ItemForSendCompanyReportList.Add(objCompanyEmail);

                    }
                    objCompanyEmailListDataModel.RecordCount = TotalRecordCount;
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objCompanyEmailListDataModel;
        }

        public ResponceModel DeleteCompanyEmailById(int Id)
        {

            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();

            if(Id > 0)
            {
                try
                {
                    objBaseDAL = new BaseDAL();

                    strQuery = @"UPDATE ItemForSendCompanyReport SET IsDelete = 1 WHERE Id = @Id";

                    lstParam = new List<SqlParameter>();

                    SqlParameter Id_Param = new SqlParameter() { ParameterName = "@Id", Value = Id };

                    lstParam.AddRange(new SqlParameter[] { Id_Param });

                    objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                    objResponceModel.Message = "Comapny Email Deleted Succesfuly";
                    objResponceModel.Responce = true;


                }
                catch (Exception ex)
                {
                    objResponceModel.Message = "Somthing Went Wrong";
                    objResponceModel.Responce = false;

                    CommonService.WriteErrorLog(ex);
                }

            }

            return objResponceModel;

        }

        #endregion

    }
}