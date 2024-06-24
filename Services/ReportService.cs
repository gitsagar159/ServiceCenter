using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using Org.BouncyCastle.Asn1.Ocsp;
using RestSharp.Serializers;
using ServiceCenter.Models;
using ServiceCenter.Models.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
                        objCallRegistration.ModifyDateString = dtRowItem["ModifyDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ModifyDate"]).ToString("dd'/'MM'/'yyy") : "-";
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
                        ObjCallRegistration.ModifyDate = dtRowItem["ModifyDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ModifyDate"]) : (DateTime?)null;
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
                        ObjCallRegistration.EstimateDate = dtRowItem["EstimateDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["EstimateDate"]).ToString("dd'/'MM'/'yyyy") : string.Empty;
                        ObjCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                        ObjCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                        ObjCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                        ObjCallRegistration.EstConfirmDate = dtRowItem["EstConfirmDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["EstConfirmDate"]).ToString("dd'/'MM'/'yyyy") : string.Empty;
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
                        worksheet.Cells[intRow, 5].Value = "Modify Date";
                        worksheet.Cells[intRow, 6].Value = "Customer Name";
                        worksheet.Cells[intRow, 7].Value = "Address";
                        worksheet.Cells[intRow, 8].Value = "Pincode";

                        worksheet.Cells[intRow, 9].Value = "Call Type";
                        worksheet.Cells[intRow, 10].Value = "Service Type";
                        worksheet.Cells[intRow, 11].Value = "Technician Type";
                        worksheet.Cells[intRow, 12].Value = "Technician";
                        worksheet.Cells[intRow, 13].Value = "Mobile No";
                        worksheet.Cells[intRow, 14].Value = "Call Attn";
                        worksheet.Cells[intRow, 15].Value = "Job Done";
                        worksheet.Cells[intRow, 16].Value = "Product Name";
                        worksheet.Cells[intRow, 17].Value = "Deal.Ref/Name";
                        worksheet.Cells[intRow, 18].Value = "Company Complaint no.";
                        worksheet.Cells[intRow, 19].Value = "Estimate Date";
                        worksheet.Cells[intRow, 20].Value = "Payment";
                        worksheet.Cells[intRow, 21].Value = "Estimate Confirm Date";
                        worksheet.Cells[intRow, 22].Value = "Estimate";
                        worksheet.Cells[intRow, 23].Value = "Item Name";
                        worksheet.Cells[intRow, 24].Value = "Fault Type";
                        worksheet.Cells[intRow, 25].Value = "Fault Description";
                        worksheet.Cells[intRow, 26].Value = "Model Name";
                        worksheet.Cells[intRow, 27].Value = "Special Instuction";
                        worksheet.Cells[intRow, 28].Value = "Region For Job Done";
                        worksheet.Cells[intRow, 29].Value = "Region For Job Not Done";
                        worksheet.Cells[intRow, 30].Value = "Repeat From Tech";
                        worksheet.Cells[intRow, 31].Value = "Call Back";
                        worksheet.Cells[intRow, 32].Value = "Workshop In";
                        worksheet.Cells[intRow, 33].Value = "SMS Sent";
                        worksheet.Cells[intRow, 34].Value = "bill No.";
                        worksheet.Cells[intRow, 35].Value = "Technican Bill No.";
                        worksheet.Cells[intRow, 36].Value = "Payment Pending";
                        worksheet.Cells[intRow, 37].Value = "Purchase Date";
                        worksheet.Cells[intRow, 38].Value = "Deliverd";
                        worksheet.Cells[intRow, 39].Value = "Cancled";
                        worksheet.Cells[intRow, 40].Value = "Go After Call";
                        worksheet.Cells[intRow, 41].Value = "Part Pending";
                        worksheet.Cells[intRow, 42].Value = "Payment By";
                        worksheet.Cells[intRow, 43].Value = "Visit Charge";
                        worksheet.Cells[intRow, 44].Value = "Serial No.";

                        worksheet.Cells[intRow, 1, intRow, 44].Style.Font.Bold = true;

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

                            worksheet.Cells[intRow, 5].Value = callItem.ModifyDate;
                            worksheet.Cells[intRow, 5].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 6].Value = callItem.CustomerName;
                            worksheet.Cells[intRow, 7].Value = callItem.Address;
                            worksheet.Cells[intRow, 8].Value = callItem.Pincode;

                            worksheet.Cells[intRow, 9].Value = callItem.CallTypeName; //CommonService.GetCallTypeNameById(callItem.CallType);
                            worksheet.Cells[intRow, 10].Value = callItem.ServTypeName; //CommonService.GetServiceTypeNameById(callItem.ServType);
                            worksheet.Cells[intRow, 11].Value = callItem.TechnicianType;
                            worksheet.Cells[intRow, 12].Value = callItem.TechnicianName;
                            worksheet.Cells[intRow, 13].Value = callItem.MobileNo;
                            worksheet.Cells[intRow, 14].Value = callItem.CallAttn ? "Yes" : "No";
                            worksheet.Cells[intRow, 15].Value = callItem.JobDone ? "Yes" : "No";
                            worksheet.Cells[intRow, 16].Value = callItem.ProductName;
                            worksheet.Cells[intRow, 17].Value = callItem.DealRef;
                            worksheet.Cells[intRow, 18].Value = callItem.CompComplaintNo;
                            
                            worksheet.Cells[intRow, 19].Value = callItem.EstimateDate;
                            worksheet.Cells[intRow, 19].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 20].Value = callItem.Payment;
                            
                            worksheet.Cells[intRow, 21].Value = callItem.EstConfirmDate;
                            worksheet.Cells[intRow, 21].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 22].Value = callItem.Estimate;
                            worksheet.Cells[intRow, 23].Value = callItem.ItemName;
                            worksheet.Cells[intRow, 24].Value = callItem.FaultTypeName;
                            worksheet.Cells[intRow, 25].Value = callItem.FaultDesc;
                            worksheet.Cells[intRow, 26].Value = callItem.ModelName;
                            worksheet.Cells[intRow, 27].Value = callItem.SpInst;
                            worksheet.Cells[intRow, 28].Value = callItem.JobDoneRegion;
                            worksheet.Cells[intRow, 29].Value = callItem.JobRegion;
                            worksheet.Cells[intRow, 30].Value = callItem.RepeatFromTech ? "Yes" : "No";
                            worksheet.Cells[intRow, 31].Value = callItem.CallBack ? "Yes" : "No";
                            worksheet.Cells[intRow, 32].Value = callItem.WorkShopIN ? "Yes" : "No";
                            worksheet.Cells[intRow, 33].Value = callItem.SMSSent ? "Yes" : "No";
                            worksheet.Cells[intRow, 34].Value = callItem.BillNo;
                            worksheet.Cells[intRow, 35].Value = callItem.TechBillNo;
                            worksheet.Cells[intRow, 36].Value = callItem.PaymentPanding ? "Yes" : "No";
                            
                            worksheet.Cells[intRow, 37].Value = callItem.PurchaseDate;
                            worksheet.Cells[intRow, 37].Style.Numberformat.Format = "dd/mm/yyyy";

                            worksheet.Cells[intRow, 38].Value = callItem.Deliver ? "Yes" : "No";
                            worksheet.Cells[intRow, 39].Value = callItem.Canceled ? "Yes" : "No";
                            worksheet.Cells[intRow, 40].Value = callItem.GoAfterCall ? "Yes" : "No";
                            worksheet.Cells[intRow, 41].Value = callItem.PartPanding ? "Yes" : "No";
                            worksheet.Cells[intRow, 42].Value = callItem.PaymentBy;
                            worksheet.Cells[intRow, 43].Value = callItem.VisitCharge;
                            worksheet.Cells[intRow, 44].Value = callItem.SrNo;



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
                        ObjCallRegistration.EstimateDate = dtRowItem["EstimateDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["EstimateDate"]).ToString("dd'/'MM'/'yyyy") : string.Empty;
                        ObjCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                        ObjCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                        ObjCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                        ObjCallRegistration.EstConfirmDate = dtRowItem["EstConfirmDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["EstConfirmDate"]).ToString("dd'/'MM'/'yyyy") : string.Empty;
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
                        ObjCallRegistration.EstimateDate = dtRowItem["EstimateDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["EstimateDate"]).ToString("dd'/'MM'/'yyyy") : string.Empty;
                        ObjCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                        ObjCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                        ObjCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                        ObjCallRegistration.EstConfirmDate = dtRowItem["EstConfirmDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["EstConfirmDate"]).ToString("dd'/'MM'/'yyyy") : string.Empty;
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
                        objDailyMailReportStatus.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;
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

        /*
        public CallRegistrationListDataModel PendingCallReport(DateTime? FromDate, DateTime? ToDate)
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();


            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"PendingCallReport";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                            objCallRegistrationListDataModel.CallRegistrationList.Add(objCallRegistration);

                        }
                        objCallRegistrationListDataModel.RecordCount = TotalRecordCount;
                    }

                    if(ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objCallRegistrationListDataModel.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objCallRegistrationListDataModel.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }
                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objCallRegistrationListDataModel;
        }
        */

        public TechnicianReportData PendingCallReport(DateTime? FromDate, DateTime? ToDate)
        {
            TechnicianReportData objTechnicianReportData = new TechnicianReportData();
            objTechnicianReportData.TechnicianReportList = new List<TechnicianReport>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"PendingCallReport";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                List<CallRegistration> CallRegistrationList = new List<CallRegistration>();

                List<TechnicianExpense> TechnicianExpenseList = new List<TechnicianExpense>();
                TechnicianExpense objTechnicianExpense;

                TechnicianReport objTechnicianReport = new TechnicianReport();

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;
                            objCallRegistration.AC_Service = dtRowItem["AC_Service"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["AC_Service"]) : false;

                            CallRegistrationList.Add(objCallRegistration);

                        }

                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];

                        foreach (DataRow dtRowItem in CallDatesTable.Rows)
                        {
                            objTechnicianExpense = new TechnicianExpense();

                            objTechnicianExpense.id = dtRowItem["id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["id"]) : 0;
                            objTechnicianExpense.techid = dtRowItem["techid"] != DBNull.Value ? Convert.ToString(dtRowItem["techid"]) : String.Empty;
                            objTechnicianExpense.type = dtRowItem["type"] != DBNull.Value ? Convert.ToInt32(dtRowItem["type"]) : 0;
                            objTechnicianExpense.amount = dtRowItem["amount"] != DBNull.Value ? Convert.ToInt32(dtRowItem["amount"]) : 0;
                            objTechnicianExpense.date = dtRowItem["date"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["date"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : String.Empty;

                            TechnicianExpenseList.Add(objTechnicianExpense);
                        }
                    }

                    if (ResDataSet.Tables.Count > 2)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[2];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objTechnicianReportData.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objTechnicianReportData.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }


                    if (CallRegistrationList.Count > 0)
                    {
                        List<TechnicianItems> ItemList;

                        var CallListGroupByTechnician = CallRegistrationList.GroupBy(i => i.TechnicianId)
                                            .Select(s => new { TechnicianId = s.Key, CallItem = s.ToList() })
                                            .ToList();

                        if (CallListGroupByTechnician.Count > 0)
                        {
                            foreach (var groupCallItem in CallListGroupByTechnician)
                            {

                                objTechnicianReport = new TechnicianReport();



                                objTechnicianReport.TechnicianId = groupCallItem.TechnicianId;
                                objTechnicianReport.TechnicianName = groupCallItem.CallItem.FirstOrDefault().Technician;
                                objTechnicianReport.TechnicianTypeId = groupCallItem.CallItem.FirstOrDefault().TechType;
                                objTechnicianReport.TechnicianPayment = groupCallItem.CallItem.Sum(p => p.Payment);
                                objTechnicianReport.TechnicianVisitCharge = groupCallItem.CallItem.Sum(p => p.VisitCharge);
                                objTechnicianReport.TechnicianEstimate = groupCallItem.CallItem.Sum(p => p.Estimate);

                                objTechnicianReport.TechnicianExpenceData = new List<TechnicianExpense>();
                                objTechnicianReport.TechnicianExpenceData = TechnicianExpenseList.Count > 0 ? TechnicianExpenseList.Where(e => e.techid == objTechnicianReport.TechnicianId).ToList() : new List<TechnicianExpense>();

                                objTechnicianReport.TechnicianExpence = objTechnicianReport.TechnicianExpenceData.Count > 0 ? objTechnicianReport.TechnicianExpenceData.Sum(x => x.amount) : 0;
                                objTechnicianReport.TotalEarning = objTechnicianReport.TechnicianPayment - objTechnicianReport.TechnicianExpence;

                                objTechnicianReport.AssignCalls = groupCallItem.CallItem.Count;
                                objTechnicianReport.DoneCalls = groupCallItem.CallItem.Where(x => x.JobDone == true && x.Canceled == false).ToList().Count;
                                objTechnicianReport.CancelCalls = groupCallItem.CallItem.Where(x => x.Canceled == true).ToList().Count;
                                objTechnicianReport.Ac_Service = groupCallItem.CallItem.Where(x => x.AC_Service == true).ToList().Count;

                                objTechnicianReport.CallBackCalls = groupCallItem.CallItem.Where(x => x.CallBack == true).ToList().Count;
                                objTechnicianReport.WorkShopIN = groupCallItem.CallItem.Where(x => x.WorkShopIN == true).ToList().Count;
                                objTechnicianReport.Workshop_Pending = objTechnicianReport.CallBackCalls - objTechnicianReport.WorkShopIN;

                                objTechnicianReport.Local_Calls_List = new TechnicianLocal();

                                objTechnicianReport.Local_Calls_List.LocalCalls = groupCallItem.CallItem.Where(l => l.CallType == 0).ToList().Count;

                                // Local Calls Data -> In-Warranty
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // Local Calls Data -> Out-Warranty

                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // Local Calls Data -> Installation

                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }



                                objTechnicianReport.Workshop_Calls_List = new TechnicianWorkshop();
                                objTechnicianReport.Workshop_Calls_List.WorkshopCalls = groupCallItem.CallItem.Where(l => l.CallType == 1).ToList().Count;

                                //Workshop Calls -> Out-Warranty

                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0).ToList().Count;

                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.CallAttn == true && l.JobDone == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyWorkshop_JobDone_But_NotDeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.Deliver == true && l.JobDone == true).ToList().Count;

                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // Workshop Calls Data -> Out-Warranty

                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1).ToList().Count;

                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.CallAttn == true && l.JobDone == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyWorkshop_JobDone_But_NotDeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.Deliver == true && l.JobDone == true).ToList().Count;


                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // Workshop Calls Data -> Installation

                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2).ToList().Count;

                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2 && l.CallAttn == true && l.JobDone == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationWorkshop_JobDone_But_NotDeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2 && l.Deliver == true && l.JobDone == true).ToList().Count;

                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }




                                objTechnicianReport.OutStation_Calls_List = new TechnicianOutStation();
                                objTechnicianReport.OutStation_Calls_List.OutStationCalls = groupCallItem.CallItem.Where(l => l.CallType == 2).ToList().Count;

                                //OutStation Calls -> Out-Warranty

                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // OutStation Calls Data -> Out-Warranty

                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // OutStation Calls Data -> Installation

                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                objTechnicianReportData.TechnicianReportList.Add(objTechnicianReport);

                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objTechnicianReportData;
        }

        public CustomerPendingPaymentReportData CustomerPendingPaymentReport(DateTime? FromDate, DateTime? ToDate)
        {
            CustomerPendingPaymentReportData objCustomerPendingPaymentReportData = new CustomerPendingPaymentReportData();
            objCustomerPendingPaymentReportData.CustomerList = new List<CallRegistration>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"CustomerPaymentPendingReport";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;
                            objCallRegistration.AC_Service = dtRowItem["AC_Service"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["AC_Service"]) : false;

                            objCustomerPendingPaymentReportData.CustomerList.Add(objCallRegistration);

                        }

                    }


                }

                if (ResDataSet.Tables.Count > 1)
                {
                    DataTable CallDatesTable = ResDataSet.Tables[1];
                    DataRow dtRowItem = CallDatesTable.Rows[0];

                    objCustomerPendingPaymentReportData.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    objCustomerPendingPaymentReportData.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objCustomerPendingPaymentReportData;
        }

        public CallRegistrationListDataModel PendingCallListByTechnicianId(DateTime? FromDate, DateTime? ToDate, string TechnicianId)
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();


            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"PendingCallByTechnicianId";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };
                SqlParameter TechnicianId_Param = !string.IsNullOrEmpty(TechnicianId) ? new SqlParameter() { ParameterName = "@TechnicianId", Value = TechnicianId } : new SqlParameter() { ParameterName = "@TechnicianId", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param, TechnicianId_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                            objCallRegistrationListDataModel.CallRegistrationList.Add(objCallRegistration);

                        }
                        objCallRegistrationListDataModel.RecordCount = TotalRecordCount;
                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objCallRegistrationListDataModel.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objCallRegistrationListDataModel.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }
                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objCallRegistrationListDataModel;
        }
        public CallRegistrationListDataModel CallDetails(DateTime? FromDate, DateTime? ToDate, string TechnicianId, int ReportType)
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"CallDetails";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };
                SqlParameter ReportType_Param =  new SqlParameter() { ParameterName = "@ReportType", Value = ReportType };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param, ReportType_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                List<CallRegistration> lstCallRegistration = new List<CallRegistration>();

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                       
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();

                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                            lstCallRegistration.Add(objCallRegistration);

                        }
                    }
                }

                if(lstCallRegistration.Count > 0)
                {
                    objCallRegistrationListDataModel.CallRegistrationList = lstCallRegistration.Where(x=>x.TechnicianId == TechnicianId).ToList();
                    objCallRegistrationListDataModel.RecordCount = lstCallRegistration.Where(x => x.TechnicianId == TechnicianId).ToList().Count;
                }

                objCallRegistrationListDataModel.FromDate = FromDate.HasValue ? FromDate.Value.ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                objCallRegistrationListDataModel.ToDate = ToDate.HasValue ? ToDate.Value.ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objCallRegistrationListDataModel;
        }

        public TechnicianReportData TechnicianCallSummaryReport(DateTime? FromDate, DateTime? ToDate)
        {
            TechnicianReportData objTechnicianReportData = new TechnicianReportData();
            objTechnicianReportData.TechnicianReportList = new List<TechnicianReport>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"DailyTechnicianCallReport";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                List<CallRegistration> CallRegistrationList = new List<CallRegistration>();

                List<TechnicianExpense> TechnicianExpenseList = new List<TechnicianExpense>();
                TechnicianExpense objTechnicianExpense;

                TechnicianReport objTechnicianReport = new TechnicianReport();

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;
                            objCallRegistration.AC_Service = dtRowItem["AC_Service"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["AC_Service"]) : false;

                            CallRegistrationList.Add(objCallRegistration);

                        }
                        
                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];

                        foreach (DataRow dtRowItem in CallDatesTable.Rows)
                        {
                            objTechnicianExpense = new TechnicianExpense();

                            objTechnicianExpense.id = dtRowItem["id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["id"]) : 0;
                            objTechnicianExpense.techid = dtRowItem["techid"] != DBNull.Value ? Convert.ToString(dtRowItem["techid"]) : String.Empty;
                            objTechnicianExpense.type = dtRowItem["type"] != DBNull.Value ? Convert.ToInt32(dtRowItem["type"]) : 0;
                            objTechnicianExpense.amount = dtRowItem["amount"] != DBNull.Value ? Convert.ToInt32(dtRowItem["amount"]) : 0;
                            objTechnicianExpense.date = dtRowItem["date"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["date"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : String.Empty;

                            TechnicianExpenseList.Add(objTechnicianExpense);
                        }
                    }

                    if (ResDataSet.Tables.Count > 2)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[2];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objTechnicianReportData.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objTechnicianReportData.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }


                    if(CallRegistrationList.Count > 0)
                    {
                        List<TechnicianItems> ItemList;

                        var CallListGroupByTechnician = CallRegistrationList.GroupBy(i => i.TechnicianId)
                                            .Select(s => new { TechnicianId = s.Key, CallItem = s.ToList() })
                                            .ToList();

                        if(CallListGroupByTechnician.Count > 0)
                        {
                            foreach (var groupCallItem in CallListGroupByTechnician)
                            {

                                objTechnicianReport = new TechnicianReport();

                                

                                objTechnicianReport.TechnicianId = groupCallItem.TechnicianId;
                                objTechnicianReport.TechnicianName = groupCallItem.CallItem.FirstOrDefault().Technician;
                                objTechnicianReport.TechnicianTypeId = groupCallItem.CallItem.FirstOrDefault().TechType;
                                objTechnicianReport.TechnicianPayment = groupCallItem.CallItem.Sum(p=>p.Payment);
                                objTechnicianReport.TechnicianVisitCharge = groupCallItem.CallItem.Sum(p => p.VisitCharge);
                                objTechnicianReport.TechnicianEstimate = groupCallItem.CallItem.Sum(p => p.Estimate);

                                objTechnicianReport.TechnicianExpenceData = new List<TechnicianExpense>();
                                objTechnicianReport.TechnicianExpenceData = TechnicianExpenseList.Count > 0 ? TechnicianExpenseList.Where(e => e.techid == objTechnicianReport.TechnicianId).ToList() : new List<TechnicianExpense>();

                                objTechnicianReport.TechnicianExpence = objTechnicianReport.TechnicianExpenceData.Count > 0 ? objTechnicianReport.TechnicianExpenceData.Sum(x => x.amount) : 0;
                                objTechnicianReport.TotalEarning = objTechnicianReport.TechnicianPayment - objTechnicianReport.TechnicianExpence;

                                objTechnicianReport.AssignCalls = groupCallItem.CallItem.Count;
                                objTechnicianReport.DoneCalls = groupCallItem.CallItem.Where(x=>x.JobDone == true && x.Canceled == false).ToList().Count;
                                objTechnicianReport.CancelCalls = groupCallItem.CallItem.Where(x => x.Canceled == true).ToList().Count;
                                objTechnicianReport.Ac_Service = groupCallItem.CallItem.Where(x => x.AC_Service == true).ToList().Count;

                                objTechnicianReport.CallBackCalls = groupCallItem.CallItem.Where(x => x.CallBack == true).ToList().Count;
                                objTechnicianReport.WorkShopIN = groupCallItem.CallItem.Where(x => x.WorkShopIN == true).ToList().Count;
                                objTechnicianReport.Workshop_Pending = objTechnicianReport.CallBackCalls - objTechnicianReport.WorkShopIN;

                                objTechnicianReport.Local_Calls_List = new TechnicianLocal();

                                objTechnicianReport.Local_Calls_List.LocalCalls = groupCallItem.CallItem.Where(l => l.CallType == 0).ToList().Count;

                                // Local Calls Data -> In-Warranty
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.CallAttn == true && l.JobDone == false).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyDemoCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.FaultDesc.ToLower().Trim() == "demo").ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // Local Calls Data -> Out-Warranty

                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // Local Calls Data -> Installation

                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                

                                objTechnicianReport.Workshop_Calls_List = new TechnicianWorkshop();
                                objTechnicianReport.Workshop_Calls_List.WorkshopCalls = groupCallItem.CallItem.Where(l => l.CallType == 1).ToList().Count;

                                //Workshop Calls -> Out-Warranty

                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0).ToList().Count;

                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.CallAttn == true && l.JobDone == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyWorkshop_JobDone_But_NotDeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.Deliver == true && l.JobDone == true).ToList().Count;

                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // Workshop Calls Data -> Out-Warranty

                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1).ToList().Count;
                                
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.CallAttn == true && l.JobDone == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyWorkshop_JobDone_But_NotDeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.Deliver == true && l.JobDone == true).ToList().Count;


                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // Workshop Calls Data -> Installation

                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2).ToList().Count;

                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2 && l.CallAttn == true && l.JobDone == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationWorkshop_JobDone_But_NotDeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2 && l.Deliver == true && l.JobDone == true).ToList().Count;

                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }




                                objTechnicianReport.OutStation_Calls_List = new TechnicianOutStation();
                                objTechnicianReport.OutStation_Calls_List.OutStationCalls = groupCallItem.CallItem.Where(l => l.CallType == 2).ToList().Count;

                                //OutStation Calls -> Out-Warranty

                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // OutStation Calls Data -> Out-Warranty

                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // OutStation Calls Data -> Installation

                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                objTechnicianReportData.TechnicianReportList.Add(objTechnicianReport);

                            }
                        }

                        
                    }




                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objTechnicianReportData;
        }

        public DailyRegisterCallReportData DailyCallRegister(DateTime? FromDate, DateTime? ToDate)
        {
            DailyRegisterCallReportData objDailyRegisterCallReportData = new DailyRegisterCallReportData();
            objDailyRegisterCallReportData.DailyRegisterCallReport = new DailyRegisterCallReport();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"DailyCallRegisterReport";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                List<CallRegistration> CallRegistrationList = new List<CallRegistration>();

                DailyRegisterCallReport objDailyRegisterCallReport = new DailyRegisterCallReport();

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();

                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                            CallRegistrationList.Add(objCallRegistration);

                        }

                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objDailyRegisterCallReportData.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objDailyRegisterCallReportData.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }


                    if (CallRegistrationList.Count > 0)
                    {
                        List<TechnicianItems> ItemList;

                        objDailyRegisterCallReport = new DailyRegisterCallReport();

                        objDailyRegisterCallReport.Local_Calls_List = new TechnicianLocal();

                        objDailyRegisterCallReport.Local_Calls_List.LocalCalls = CallRegistrationList.Where(l => l.CallType == 0).ToList().Count;

                        // Local Calls Data -> In-Warranty
                        objDailyRegisterCallReport.Local_Calls_List.Local_InWarranty_Calls = new TechnicianInWarranty();
                        objDailyRegisterCallReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyCalls = CallRegistrationList.Where(l => l.CallType == 0 && l.ServType == 0).ToList().Count;
                        objDailyRegisterCallReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();

                        foreach (var item in CallRegistrationList.Where(l => l.CallType == 0 && l.ServType == 0).ToList())
                        {
                            objDailyRegisterCallReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                        }

                        // Local Calls Data -> Out-Warranty

                        objDailyRegisterCallReport.Local_Calls_List.Local_OutWarranty_Calls = new TechnicianOutWarranty();
                        objDailyRegisterCallReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyCalls = CallRegistrationList.Where(l => l.CallType == 0 && l.ServType == 1).ToList().Count;
                        objDailyRegisterCallReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                        foreach (var item in CallRegistrationList.Where(l => l.CallType == 0 && l.ServType == 1).ToList())
                        {
                            objDailyRegisterCallReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                        }


                        // Local Calls Data -> Installation

                        objDailyRegisterCallReport.Local_Calls_List.Local_Installation_Calls = new TechnicianInstallation();
                        objDailyRegisterCallReport.Local_Calls_List.Local_Installation_Calls.InstallationCalls = CallRegistrationList.Where(l => l.CallType == 0 && l.ServType == 2).ToList().Count;
                        objDailyRegisterCallReport.Local_Calls_List.Local_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                        foreach (var item in CallRegistrationList.Where(l => l.CallType == 0 && l.ServType == 2).ToList())
                        {
                            objDailyRegisterCallReport.Local_Calls_List.Local_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                        }



                        objDailyRegisterCallReport.Workshop_Calls_List = new TechnicianWorkshop();
                        objDailyRegisterCallReport.Workshop_Calls_List.WorkshopCalls = CallRegistrationList.Where(l => l.CallType == 1).ToList().Count;

                        //Workshop Calls -> Out-Warranty

                        objDailyRegisterCallReport.Workshop_Calls_List.Workshop_InWarranty_Calls = new TechnicianInWarranty();
                        objDailyRegisterCallReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyCalls = CallRegistrationList.Where(l => l.CallType == 1 && l.ServType == 0).ToList().Count;
                        objDailyRegisterCallReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                        foreach (var item in CallRegistrationList.Where(l => l.CallType == 1 && l.ServType == 0).ToList())
                        {
                            objDailyRegisterCallReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                        }

                        // Workshop Calls Data -> Out-Warranty

                        objDailyRegisterCallReport.Workshop_Calls_List.Workshop_OutWarranty_Calls = new TechnicianOutWarranty();
                        objDailyRegisterCallReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyCalls = CallRegistrationList.Where(l => l.CallType == 1 && l.ServType == 1).ToList().Count;
                        objDailyRegisterCallReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                        foreach (var item in CallRegistrationList.Where(l => l.CallType == 1 && l.ServType == 1).ToList())
                        {
                            objDailyRegisterCallReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                        }


                        // Workshop Calls Data -> Installation

                        objDailyRegisterCallReport.Workshop_Calls_List.Workshop_Installation_Calls = new TechnicianInstallation();
                        objDailyRegisterCallReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationCalls = CallRegistrationList.Where(l => l.CallType == 1 && l.ServType == 2).ToList().Count;
                        objDailyRegisterCallReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                        foreach (var item in CallRegistrationList.Where(l => l.CallType == 1 && l.ServType == 2).ToList())
                        {
                            objDailyRegisterCallReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                        }




                        objDailyRegisterCallReport.OutStation_Calls_List = new TechnicianOutStation();
                        objDailyRegisterCallReport.OutStation_Calls_List.OutStationCalls = CallRegistrationList.Where(l => l.CallType == 2).ToList().Count;

                        //OutStation Calls -> Out-Warranty

                        objDailyRegisterCallReport.OutStation_Calls_List.OutStation_InWarranty_Calls = new TechnicianInWarranty();
                        objDailyRegisterCallReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyCalls = CallRegistrationList.Where(l => l.CallType == 2 && l.ServType == 0).ToList().Count;
                        objDailyRegisterCallReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                        foreach (var item in CallRegistrationList.Where(l => l.CallType == 2 && l.ServType == 0).ToList())
                        {
                            objDailyRegisterCallReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                        }

                        // OutStation Calls Data -> Out-Warranty

                        objDailyRegisterCallReport.OutStation_Calls_List.OutStation_OutWarranty_Calls = new TechnicianOutWarranty();
                        objDailyRegisterCallReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyCalls = CallRegistrationList.Where(l => l.CallType == 2 && l.ServType == 1).ToList().Count;
                        objDailyRegisterCallReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                        foreach (var item in CallRegistrationList.Where(l => l.CallType == 2 && l.ServType == 1).ToList())
                        {
                            objDailyRegisterCallReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                        }


                        // OutStation Calls Data -> Installation

                        objDailyRegisterCallReport.OutStation_Calls_List.OutStation_Installation_Calls = new TechnicianInstallation();
                        objDailyRegisterCallReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationCalls = CallRegistrationList.Where(l => l.CallType == 2 && l.ServType == 2).ToList().Count;
                        objDailyRegisterCallReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                        foreach (var item in CallRegistrationList.Where(l => l.CallType == 2 && l.ServType == 2).ToList())
                        {
                            objDailyRegisterCallReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                        }


                        objDailyRegisterCallReportData.DailyRegisterCallReport = objDailyRegisterCallReport;


                    }




                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objDailyRegisterCallReportData;
        }


        public TechnicianReportData WorkshopPendingWorkReport(DateTime? FromDate, DateTime? ToDate)
        {
            TechnicianReportData objTechnicianReportData = new TechnicianReportData();
            objTechnicianReportData.TechnicianReportList = new List<TechnicianReport>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"WorkshopPendingReport";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                List<CallRegistration> CallRegistrationList = new List<CallRegistration>();

                List<TechnicianExpense> TechnicianExpenseList = new List<TechnicianExpense>();

                TechnicianReport objTechnicianReport = new TechnicianReport();

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;
                            objCallRegistration.WorkShopIN = dtRowItem["WorkShopIN"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["WorkShopIN"]) : false;
                            objCallRegistration.Deliver = dtRowItem["Deliver"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Deliver"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                            CallRegistrationList.Add(objCallRegistration);

                        }

                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objTechnicianReportData.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objTechnicianReportData.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }


                    if (CallRegistrationList.Count > 0)
                    {
                        List<TechnicianItems> ItemList;

                        var CallListGroupByTechnician = CallRegistrationList.GroupBy(i => i.TechnicianId)
                                            .Select(s => new { TechnicianId = s.Key, CallItem = s.ToList() })
                                            .ToList();

                        if (CallListGroupByTechnician.Count > 0)
                        {
                            foreach (var groupCallItem in CallListGroupByTechnician)
                            {

                                objTechnicianReport = new TechnicianReport();



                                objTechnicianReport.TechnicianId = groupCallItem.TechnicianId;
                                objTechnicianReport.TechnicianName = groupCallItem.CallItem.FirstOrDefault().Technician;
                                objTechnicianReport.TechnicianTypeId = groupCallItem.CallItem.FirstOrDefault().TechType;
                                objTechnicianReport.TechnicianPayment = groupCallItem.CallItem.Sum(p => p.Payment);

                                objTechnicianReport.TechnicianExpenceData = new List<TechnicianExpense>();
                                objTechnicianReport.TechnicianExpenceData = TechnicianExpenseList.Count > 0 ? TechnicianExpenseList.Where(e => e.techid == objTechnicianReport.TechnicianId).ToList() : new List<TechnicianExpense>();

                                objTechnicianReport.TechnicianExpence = objTechnicianReport.TechnicianExpenceData.Count > 0 ? objTechnicianReport.TechnicianExpenceData.Sum(x => x.amount) : 0;
                                objTechnicianReport.TotalEarning = objTechnicianReport.TechnicianPayment - objTechnicianReport.TechnicianExpence;

                                objTechnicianReport.AssignCalls = groupCallItem.CallItem.Count;
                                objTechnicianReport.DoneCalls = groupCallItem.CallItem.Where(x => x.JobDone == true).ToList().Count;
                                objTechnicianReport.CancelCalls = groupCallItem.CallItem.Where(x => x.Canceled == true).ToList().Count;
                                objTechnicianReport.CallBackCalls = groupCallItem.CallItem.Where(x => x.CallBack == true).ToList().Count;
                                objTechnicianReport.WorkShopIN = groupCallItem.CallItem.Where(x => x.WorkShopIN == true).ToList().Count;


                                objTechnicianReport.Local_Calls_List = new TechnicianLocal();

                                objTechnicianReport.Local_Calls_List.LocalCalls = groupCallItem.CallItem.Where(l => l.CallType == 0).ToList().Count;

                                // Local Calls Data -> In-Warranty
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // Local Calls Data -> Out-Warranty

                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // Local Calls Data -> Installation

                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }



                                objTechnicianReport.Workshop_Calls_List = new TechnicianWorkshop();
                                objTechnicianReport.Workshop_Calls_List.WorkshopCalls = groupCallItem.CallItem.Where(l => l.CallType == 1).ToList().Count;

                                //Workshop Calls -> Out-Warranty

                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // Workshop Calls Data -> Out-Warranty

                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // Workshop Calls Data -> Installation

                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }




                                objTechnicianReport.OutStation_Calls_List = new TechnicianOutStation();
                                objTechnicianReport.OutStation_Calls_List.OutStationCalls = groupCallItem.CallItem.Where(l => l.CallType == 2).ToList().Count;

                                //OutStation Calls -> Out-Warranty

                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // OutStation Calls Data -> Out-Warranty

                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // OutStation Calls Data -> Installation

                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                objTechnicianReportData.TechnicianReportList.Add(objTechnicianReport);

                            }
                        }


                    }




                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objTechnicianReportData;
        }

        public CallRegistrationListDataModel WorkshopInOutCallReportDaily(DateTime? FromDate, DateTime? ToDate)
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();


            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"DailyWorkshopInOutReport";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.WorkShopIN = dtRowItem["WorkShopIN"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["WorkShopIN"]) : false;
                            objCallRegistration.Deliver = dtRowItem["Deliver"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Deliver"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                            objCallRegistrationListDataModel.CallRegistrationList.Add(objCallRegistration);

                        }
                        objCallRegistrationListDataModel.RecordCount = TotalRecordCount;
                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objCallRegistrationListDataModel.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objCallRegistrationListDataModel.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }
                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objCallRegistrationListDataModel;
        }

        public CallRegistrationListDataModel WorkshopInOutCallListByTechnicianId(DateTime? FromDate, DateTime? ToDate, string TechnicianId)
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();


            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"WorkshopInOutCallListByTechnicianId";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };
                SqlParameter TechnicianId_Param = !string.IsNullOrEmpty(TechnicianId) ? new SqlParameter() { ParameterName = "@TechnicianId", Value = TechnicianId } : new SqlParameter() { ParameterName = "@TechnicianId", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param, TechnicianId_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.WorkShopIN = dtRowItem["WorkShopIN"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["WorkShopIN"]) : false;
                            objCallRegistration.Deliver = dtRowItem["Deliver"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Deliver"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                            objCallRegistrationListDataModel.CallRegistrationList.Add(objCallRegistration);

                        }
                        objCallRegistrationListDataModel.RecordCount = TotalRecordCount;
                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objCallRegistrationListDataModel.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objCallRegistrationListDataModel.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }
                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objCallRegistrationListDataModel;
        }
        public CallRegistrationListDataModel ServiceTypeReportDaily(DateTime? FromDate, DateTime? ToDate)
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();


            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"DailyServiceTypeReport";

                lstParam = new List<SqlParameter>();


                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();

                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;


                            objCallRegistrationListDataModel.CallRegistrationList.Add(objCallRegistration);

                        }
                        objCallRegistrationListDataModel.RecordCount = TotalRecordCount;
                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objCallRegistrationListDataModel.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objCallRegistrationListDataModel.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }
                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objCallRegistrationListDataModel;
        }
        public PartPendingCallReport PartPendingCallReport(DateTime? FromDate, DateTime? ToDate)
        {
            PartPendingCallReport objPartPendingCallReport = new PartPendingCallReport();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"PartPendingCallReport";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                List<CallRegistration> CallRegistrationList = new List<CallRegistration>();

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;
                            objCallRegistration.CompComplaintNo = dtRowItem["CompComplaintNo"] != DBNull.Value ? Convert.ToString(dtRowItem["CompComplaintNo"]) : string.Empty;
                            objCallRegistration.ModifyDateString = dtRowItem["ModifyDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ModifyDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";

                            CallRegistrationList.Add(objCallRegistration);

                        }
                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objPartPendingCallReport.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objPartPendingCallReport.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }

                    if(CallRegistrationList.Count > 0)
                    {
                        objPartPendingCallReport.KetanPartPendingItems = new List<PartPendingItems>();

                        

                        foreach (var item in CallRegistrationList.Where(x => x.TechType == "f44522c2-be0c-420a-a380-25946ec7a1cb").ToList().GroupBy(i => i.ItemName)
                                            .Select(s => new { ItemName = s.Key, ItemNameList = s.ToList() })
                                            .ToList())
                        {
                            objPartPendingCallReport.KetanPartPendingItems.Add(new PartPendingItems() { ItemName = item.ItemName, ItemCount = item.ItemNameList.Count, PartPendingCallList = item.ItemNameList });
                        }

                        

                        objPartPendingCallReport.CompanyPartPendingItems = new List<PartPendingItems>();

                        foreach (var item in CallRegistrationList.Where(x => x.TechType == "167e36ac-a5be-44d8-be93-f3a229b9025a").ToList().GroupBy(i => i.ItemName)
                                            .Select(s => new { ItemName = s.Key, ItemNameList = s.ToList() })
                                            .ToList())
                        {
                            objPartPendingCallReport.KetanPartPendingItems.Add(new PartPendingItems() { ItemName = item.ItemName, ItemCount = item.ItemNameList.Count, PartPendingCallList = item.ItemNameList });
                        }

                    }

                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objPartPendingCallReport;
        }

        public CallRegistrationListDataModel PaymentPendingReportDaily(DateTime? FromDate, DateTime? ToDate)
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();


            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"AllPaymentPendingCalls";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                            objCallRegistrationListDataModel.CallRegistrationList.Add(objCallRegistration);

                        }
                        objCallRegistrationListDataModel.RecordCount = TotalRecordCount;
                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objCallRegistrationListDataModel.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objCallRegistrationListDataModel.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }
                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objCallRegistrationListDataModel;
        }
        public CallRegistrationListDataModel RepeatFromTechReportDaily(DateTime? FromDate, DateTime? ToDate)
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();

            try
            {
                CallRegistrationListDataModel objDailyCallListForReport = GetDailyCallListForReport(FromDate, ToDate);

                List<CallRegistration> lstCallRegistration = objDailyCallListForReport != null && objDailyCallListForReport.CallRegistrationList.Count > 0 ? objDailyCallListForReport.CallRegistrationList : new List<CallRegistration>();

                if (lstCallRegistration != null && lstCallRegistration.Count > 0)
                {
                    objCallRegistrationListDataModel.CallRegistrationList = lstCallRegistration.Where(x => x.Technician != string.Empty && x.RepeatFromTech == true).ToList();
                    objCallRegistrationListDataModel.RecordCount = lstCallRegistration.Where(x => x.Technician != string.Empty && x.RepeatFromTech == true).ToList().Count();
                }

                objCallRegistrationListDataModel.FromDate = objDailyCallListForReport.FromDate;
                objCallRegistrationListDataModel.ToDate = objDailyCallListForReport.ToDate;




            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objCallRegistrationListDataModel;
        }
        public TechnicianReportData CancelCallReportDaily(DateTime? FromDate, DateTime? ToDate)
        {
            TechnicianReportData objTechnicianReportData = new TechnicianReportData();
            objTechnicianReportData.TechnicianReportList = new List<TechnicianReport>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"CancleCallReport";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                List<CallRegistration> CallRegistrationList = new List<CallRegistration>();

                List<TechnicianExpense> TechnicianExpenseList = new List<TechnicianExpense>();

                TechnicianReport objTechnicianReport = new TechnicianReport();

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;
                            objCallRegistration.WorkShopIN = dtRowItem["WorkShopIN"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["WorkShopIN"]) : false;
                            objCallRegistration.Deliver = dtRowItem["Deliver"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Deliver"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                            CallRegistrationList.Add(objCallRegistration);

                        }

                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objTechnicianReportData.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objTechnicianReportData.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }


                    if (CallRegistrationList.Count > 0)
                    {
                        List<TechnicianItems> ItemList;

                        var CallListGroupByTechnician = CallRegistrationList.GroupBy(i => i.TechnicianId)
                                            .Select(s => new { TechnicianId = s.Key, CallItem = s.ToList() })
                                            .ToList();

                        if (CallListGroupByTechnician.Count > 0)
                        {
                            foreach (var groupCallItem in CallListGroupByTechnician)
                            {

                                objTechnicianReport = new TechnicianReport();



                                objTechnicianReport.TechnicianId = groupCallItem.TechnicianId;
                                objTechnicianReport.TechnicianName = groupCallItem.CallItem.FirstOrDefault().Technician;
                                objTechnicianReport.TechnicianTypeId = groupCallItem.CallItem.FirstOrDefault().TechType;
                                objTechnicianReport.TechnicianPayment = groupCallItem.CallItem.Sum(p => p.Payment);

                                objTechnicianReport.TechnicianExpenceData = new List<TechnicianExpense>();
                                objTechnicianReport.TechnicianExpenceData = TechnicianExpenseList.Count > 0 ? TechnicianExpenseList.Where(e => e.techid == objTechnicianReport.TechnicianId).ToList() : new List<TechnicianExpense>();

                                objTechnicianReport.TechnicianExpence = objTechnicianReport.TechnicianExpenceData.Count > 0 ? objTechnicianReport.TechnicianExpenceData.Sum(x => x.amount) : 0;
                                objTechnicianReport.TotalEarning = objTechnicianReport.TechnicianPayment - objTechnicianReport.TechnicianExpence;

                                objTechnicianReport.AssignCalls = groupCallItem.CallItem.Count;
                                objTechnicianReport.DoneCalls = groupCallItem.CallItem.Where(x => x.JobDone == true).ToList().Count;
                                objTechnicianReport.CancelCalls = groupCallItem.CallItem.Where(x => x.Canceled == true).ToList().Count;
                                objTechnicianReport.CallBackCalls = groupCallItem.CallItem.Where(x => x.CallBack == true).ToList().Count;
                                objTechnicianReport.WorkShopIN = groupCallItem.CallItem.Where(x => x.WorkShopIN == true).ToList().Count;


                                objTechnicianReport.Local_Calls_List = new TechnicianLocal();

                                objTechnicianReport.Local_Calls_List.LocalCalls = groupCallItem.CallItem.Where(l => l.CallType == 0).ToList().Count;

                                // Local Calls Data -> In-Warranty
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // Local Calls Data -> Out-Warranty

                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // Local Calls Data -> Installation

                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }



                                objTechnicianReport.Workshop_Calls_List = new TechnicianWorkshop();
                                objTechnicianReport.Workshop_Calls_List.WorkshopCalls = groupCallItem.CallItem.Where(l => l.CallType == 1).ToList().Count;

                                //Workshop Calls -> Out-Warranty

                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // Workshop Calls Data -> Out-Warranty

                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // Workshop Calls Data -> Installation

                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }




                                objTechnicianReport.OutStation_Calls_List = new TechnicianOutStation();
                                objTechnicianReport.OutStation_Calls_List.OutStationCalls = groupCallItem.CallItem.Where(l => l.CallType == 2).ToList().Count;

                                //OutStation Calls -> Out-Warranty

                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // OutStation Calls Data -> Out-Warranty

                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // OutStation Calls Data -> Installation

                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                objTechnicianReportData.TechnicianReportList.Add(objTechnicianReport);

                            }
                        }


                    }




                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objTechnicianReportData;
        }
        public TechnicianReportData GoAfterCallReportDaily(DateTime? FromDate, DateTime? ToDate)
        {
            TechnicianReportData objTechnicianReportData = new TechnicianReportData();
            objTechnicianReportData.TechnicianReportList = new List<TechnicianReport>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"GoAfterCallReport";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                List<CallRegistration> CallRegistrationList = new List<CallRegistration>();

                List<TechnicianExpense> TechnicianExpenseList = new List<TechnicianExpense>();

                TechnicianReport objTechnicianReport = new TechnicianReport();

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;
                            objCallRegistration.WorkShopIN = dtRowItem["WorkShopIN"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["WorkShopIN"]) : false;
                            objCallRegistration.Deliver = dtRowItem["Deliver"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Deliver"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                            CallRegistrationList.Add(objCallRegistration);

                        }

                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objTechnicianReportData.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objTechnicianReportData.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }


                    if (CallRegistrationList.Count > 0)
                    {
                        List<TechnicianItems> ItemList;

                        var CallListGroupByTechnician = CallRegistrationList.GroupBy(i => i.TechnicianId)
                                            .Select(s => new { TechnicianId = s.Key, CallItem = s.ToList() })
                                            .ToList();

                        if (CallListGroupByTechnician.Count > 0)
                        {
                            foreach (var groupCallItem in CallListGroupByTechnician)
                            {

                                objTechnicianReport = new TechnicianReport();



                                objTechnicianReport.TechnicianId = groupCallItem.TechnicianId;
                                objTechnicianReport.TechnicianName = groupCallItem.CallItem.FirstOrDefault().Technician;
                                objTechnicianReport.TechnicianTypeId = groupCallItem.CallItem.FirstOrDefault().TechType;
                                objTechnicianReport.TechnicianPayment = groupCallItem.CallItem.Sum(p => p.Payment);

                                objTechnicianReport.TechnicianExpenceData = new List<TechnicianExpense>();
                                objTechnicianReport.TechnicianExpenceData = TechnicianExpenseList.Count > 0 ? TechnicianExpenseList.Where(e => e.techid == objTechnicianReport.TechnicianId).ToList() : new List<TechnicianExpense>();

                                objTechnicianReport.TechnicianExpence = objTechnicianReport.TechnicianExpenceData.Count > 0 ? objTechnicianReport.TechnicianExpenceData.Sum(x => x.amount) : 0;
                                objTechnicianReport.TotalEarning = objTechnicianReport.TechnicianPayment - objTechnicianReport.TechnicianExpence;

                                objTechnicianReport.AssignCalls = groupCallItem.CallItem.Count;
                                objTechnicianReport.DoneCalls = groupCallItem.CallItem.Where(x => x.JobDone == true).ToList().Count;
                                objTechnicianReport.CancelCalls = groupCallItem.CallItem.Where(x => x.Canceled == true).ToList().Count;
                                objTechnicianReport.CallBackCalls = groupCallItem.CallItem.Where(x => x.CallBack == true).ToList().Count;
                                objTechnicianReport.GoAfterCall = groupCallItem.CallItem.Where(x => x.GoAfterCall == true).ToList().Count;


                                objTechnicianReport.Local_Calls_List = new TechnicianLocal();

                                objTechnicianReport.Local_Calls_List.LocalCalls = groupCallItem.CallItem.Where(l => l.CallType == 0).ToList().Count;

                                // Local Calls Data -> In-Warranty
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // Local Calls Data -> Out-Warranty

                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // Local Calls Data -> Installation

                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }



                                objTechnicianReport.Workshop_Calls_List = new TechnicianWorkshop();
                                objTechnicianReport.Workshop_Calls_List.WorkshopCalls = groupCallItem.CallItem.Where(l => l.CallType == 1).ToList().Count;

                                //Workshop Calls -> Out-Warranty

                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // Workshop Calls Data -> Out-Warranty

                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // Workshop Calls Data -> Installation

                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }




                                objTechnicianReport.OutStation_Calls_List = new TechnicianOutStation();
                                objTechnicianReport.OutStation_Calls_List.OutStationCalls = groupCallItem.CallItem.Where(l => l.CallType == 2).ToList().Count;

                                //OutStation Calls -> Out-Warranty

                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // OutStation Calls Data -> Out-Warranty

                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // OutStation Calls Data -> Installation

                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                objTechnicianReportData.TechnicianReportList.Add(objTechnicianReport);

                            }
                        }


                    }




                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objTechnicianReportData;
        }
        public CallRegistrationListDataModel CollationReportDaily(DateTime? FromDate, DateTime? ToDate)
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();

            try
            {
                CallRegistrationListDataModel objDailyCallListForReport = GetDailyCallListForReport(FromDate, ToDate);

                List<CallRegistration> lstCallRegistration = objDailyCallListForReport != null && objDailyCallListForReport.CallRegistrationList.Count > 0 ? objDailyCallListForReport.CallRegistrationList : new List<CallRegistration>();


                if (lstCallRegistration != null && lstCallRegistration.Count > 0)
                {
                    objCallRegistrationListDataModel.CallRegistrationList = lstCallRegistration.Where(x =>x.Technician != string.Empty && (x.Payment != 0 || x.Estimate != 0 || x.VisitCharge != 0)).ToList();
                    objCallRegistrationListDataModel.RecordCount = lstCallRegistration.Where(x => x.Technician != string.Empty && (x.Payment != 0 || x.Estimate != 0 || x.VisitCharge != 0)).ToList().Count();
                }

                objCallRegistrationListDataModel.FromDate = objDailyCallListForReport.FromDate;
                objCallRegistrationListDataModel.ToDate = objDailyCallListForReport.ToDate;

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objCallRegistrationListDataModel;
        }
        public TechnicianJobTimeReportData TechnicianJobTimeReport(DateTime? FromDate, DateTime? ToDate)
        {
            TechnicianJobTimeReportData objTechnicianJobTimeReportData = new TechnicianJobTimeReportData();
            objTechnicianJobTimeReportData.TechnicianJobTimeReportList = new List<TechnicianJobTimeReport>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"TechnicianJobTimeReport";

                lstParam = new List<SqlParameter>();


                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        TechnicianJobTimeReport objTechnicianJobTimeReport;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objTechnicianJobTimeReport = new TechnicianJobTimeReport();


                            objTechnicianJobTimeReport.Oid = dtRowItem["Oid"] != DBNull.Value ? Convert.ToString(dtRowItem["Oid"]) : string.Empty;
                            objTechnicianJobTimeReport.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objTechnicianJobTimeReport.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objTechnicianJobTimeReport.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objTechnicianJobTimeReport.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objTechnicianJobTimeReport.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objTechnicianJobTimeReport.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objTechnicianJobTimeReport.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objTechnicianJobTimeReport.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objTechnicianJobTimeReport.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objTechnicianJobTimeReport.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objTechnicianJobTimeReport.TechnicianName = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objTechnicianJobTimeReport.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objTechnicianJobTimeReport.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objTechnicianJobTimeReport.JobStartDateTime = dtRowItem["JobStartDateTime"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["JobStartDateTime"]) : (DateTime?)null;
                            objTechnicianJobTimeReport.JobEndDateTime = dtRowItem["JobEndDateTime"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["JobEndDateTime"]) : (DateTime?)null;
                            objTechnicianJobTimeReport.TotalJobTime = dtRowItem["TotalJobTime"] != DBNull.Value ? Convert.ToString(dtRowItem["TotalJobTime"]) : "-";


                            objTechnicianJobTimeReportData.TechnicianJobTimeReportList.Add(objTechnicianJobTimeReport);

                        }
                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objTechnicianJobTimeReportData.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objTechnicianJobTimeReportData.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }
                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objTechnicianJobTimeReportData;
        }
        public CallBackReceivedOrPendingModel CallbackReceivedOrPendingReportDaily(DateTime? FromDate, DateTime? ToDate)
        {
            CallBackReceivedOrPendingModel objCallBackReceivedOrPendingModel = new CallBackReceivedOrPendingModel();
            objCallBackReceivedOrPendingModel.CallBackList = new List<CallRegistration>();
            objCallBackReceivedOrPendingModel.WorkshopInList = new List<CallRegistration>();
            objCallBackReceivedOrPendingModel.DeliverList = new List<CallRegistration>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"CallBackReceivedAndWorkshopInOut";

                lstParam = new List<SqlParameter>();


                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallBackTable = ResDataSet.Tables[0];
                    DataTable WorkshopInTable = ResDataSet.Tables[1];
                    DataTable DeliverTable = ResDataSet.Tables[2];
                    CallRegistration objCallRegistration;

                    if (CallBackTable.Rows.Count > 0)
                    {
                        foreach (DataRow dtRowItem in CallBackTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();

                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;


                            objCallBackReceivedOrPendingModel.CallBackList.Add(objCallRegistration);

                        }
                    }

                    if (WorkshopInTable.Rows.Count > 0)
                    {
                        foreach (DataRow dtRowItem in WorkshopInTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();

                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.WorkShopIN = dtRowItem["WorkShopIN"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["WorkShopIN"]) : false;
                            objCallRegistration.Deliver = dtRowItem["Deliver"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Deliver"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;


                            objCallBackReceivedOrPendingModel.WorkshopInList.Add(objCallRegistration);

                        }
                    }

                    if (DeliverTable.Rows.Count > 0)
                    {
                        foreach (DataRow dtRowItem in DeliverTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();

                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.WorkShopIN = dtRowItem["WorkShopIN"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["WorkShopIN"]) : false;
                            objCallRegistration.Deliver = dtRowItem["Deliver"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Deliver"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;


                            objCallBackReceivedOrPendingModel.DeliverList.Add(objCallRegistration);

                        }
                    }


                    List<CallRegistration> WorkshopInAndDeliverList = objCallBackReceivedOrPendingModel.WorkshopInList;
                    WorkshopInAndDeliverList.AddRange(objCallBackReceivedOrPendingModel.DeliverList);


                    objCallBackReceivedOrPendingModel.PendingList = objCallBackReceivedOrPendingModel.CallBackList.Except(WorkshopInAndDeliverList).ToList();


                    if (ResDataSet.Tables.Count > 2)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[3];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objCallBackReceivedOrPendingModel.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objCallBackReceivedOrPendingModel.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }
                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objCallBackReceivedOrPendingModel;
        }
        public CallSummaryDataModel CallSummaryReport(DateTime? FromDate, DateTime? ToDate)
        {
            CallSummaryDataModel objCallSummaryDataModel = new CallSummaryDataModel();
            objCallSummaryDataModel.UserViseCallCountList = new List<UserViseCallCountModel>();
            objCallSummaryDataModel.ItemViseCallCountList = new List<ItemViseCallCountModel>();
            objCallSummaryDataModel.ServiceTypeViseCallCountList = new List<ServiceTypeViseCallCountModel>();
            objCallSummaryDataModel.CallTypeViseCallCountList = new List<CallTypeViseCallCountModel>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"CallSummaryReport";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataSet.Tables.Count > 0)
                {

                    DataTable DatesTable = ResDataSet.Tables[0];
                    DataTable UserViseCallCountTable = ResDataSet.Tables[1];
                    DataTable ItemViseCallCountTable = ResDataSet.Tables[2];
                    DataTable ServiceTypeViseCallCountTable = ResDataSet.Tables[3];
                    DataTable CallTypeViseCallCountTable = ResDataSet.Tables[4];

                    if (DatesTable.Rows.Count > 0)
                    {

                        DataRow dtRowItem = DatesTable.Rows[0];

                        objCallSummaryDataModel.fromdate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : string.Empty; 
                        objCallSummaryDataModel.todate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : string.Empty;
                    }

                    if (UserViseCallCountTable.Rows.Count > 0)
                    {
                        UserViseCallCountModel objUserViseCallCountModel;

                        foreach (DataRow dtRowItem in UserViseCallCountTable.Rows)
                        {
                            objUserViseCallCountModel = new UserViseCallCountModel();

                            objUserViseCallCountModel.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;
                            objUserViseCallCountModel.CallCount = dtRowItem["CallCount"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallCount"]) : 0;

                            objCallSummaryDataModel.UserViseCallCountList.Add(objUserViseCallCountModel);

                        }

                        objCallSummaryDataModel.UserViseCallCountListJson = objCallSummaryDataModel.UserViseCallCountList.Count > 0 ? JsonConvert.SerializeObject(objCallSummaryDataModel.UserViseCallCountList) : string.Empty;
                    }

                    if (ItemViseCallCountTable.Rows.Count > 0)
                    {
                        ItemViseCallCountModel objItemViseCallCountModel;

                        foreach (DataRow dtRowItem in ItemViseCallCountTable.Rows)
                        {
                            objItemViseCallCountModel = new ItemViseCallCountModel();

                            objItemViseCallCountModel.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objItemViseCallCountModel.CallCount = dtRowItem["CallCount"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallCount"]) : 0;

                            objCallSummaryDataModel.ItemViseCallCountList.Add(objItemViseCallCountModel);

                        }
                    }

                    if (ServiceTypeViseCallCountTable.Rows.Count > 0)
                    {
                        ServiceTypeViseCallCountModel objServiceTypeViseCallCountModel;

                        foreach (DataRow dtRowItem in ServiceTypeViseCallCountTable.Rows)
                        {
                            objServiceTypeViseCallCountModel = new ServiceTypeViseCallCountModel();

                            objServiceTypeViseCallCountModel.ServiceType = dtRowItem["ServiceType"] != DBNull.Value ? Convert.ToString(dtRowItem["ServiceType"]) : string.Empty;
                            objServiceTypeViseCallCountModel.CallCount = dtRowItem["CallCount"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallCount"]) : 0;

                            objCallSummaryDataModel.ServiceTypeViseCallCountList.Add(objServiceTypeViseCallCountModel);

                        }
                    }

                    if (CallTypeViseCallCountTable.Rows.Count > 0)
                    {
                        CallTypeViseCallCountModel objCallTypeViseCallCountModel;

                        foreach (DataRow dtRowItem in CallTypeViseCallCountTable.Rows)
                        {
                            objCallTypeViseCallCountModel = new CallTypeViseCallCountModel();

                            objCallTypeViseCallCountModel.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToString(dtRowItem["CallType"]) : string.Empty;
                            objCallTypeViseCallCountModel.CallCount = dtRowItem["CallCount"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallCount"]) : 0;

                            objCallSummaryDataModel.CallTypeViseCallCountList.Add(objCallTypeViseCallCountModel);

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objCallSummaryDataModel;
        }
        public CallRegistrationListDataModel GetDailyCallListForReport(DateTime? FromDate, DateTime? ToDate)
        {
            CallRegistrationListDataModel objCallRegistrationListDataModel = new CallRegistrationListDataModel();
            objCallRegistrationListDataModel.CallRegistrationList = new List<CallRegistration>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"DailyCallListForReport";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                            objCallRegistrationListDataModel.CallRegistrationList.Add(objCallRegistration);

                        }
                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objCallRegistrationListDataModel.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objCallRegistrationListDataModel.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }

                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objCallRegistrationListDataModel;
        }

        public TechnicianReportData CallBackReceivedAndWorkshopInOut(DateTime? FromDate, DateTime? ToDate)
        {
            TechnicianReportData objTechnicianReportData = new TechnicianReportData();
            objTechnicianReportData.TechnicianReportList = new List<TechnicianReport>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"CallBackReceivedAndWorkshopInOut";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                List<CallRegistration> CallBackList = new List<CallRegistration>();
                List<CallRegistration> CallBackWorkshoInList = new List<CallRegistration>();
                List<CallRegistration> CallBackWorkshopPendingList = new List<CallRegistration>();

                List<TechnicianExpense> TechnicianExpenseList = new List<TechnicianExpense>();
                TechnicianExpense objTechnicianExpense;

                TechnicianReport objTechnicianReport = new TechnicianReport();

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                            CallBackList.Add(objCallRegistration);

                        }

                    }


                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallBackWorkshopPinding = ResDataSet.Tables[1];

                        if (CallBackWorkshopPinding.Rows.Count > 0)
                        {
                            CallRegistration objCallRegistration;

                            foreach (DataRow dtRowItem in CallBackWorkshopPinding.Rows)
                            {
                                objCallRegistration = new CallRegistration();


                                objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                                objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                                objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                                objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                                objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                                objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                                objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                                objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                                objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                                objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                                objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                                objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                                objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                                objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                                objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                                objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                                objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                                objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                                objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                                objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                                objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                                objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                                objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                                objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                                objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                                objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                                objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                                CallBackWorkshoInList.Add(objCallRegistration);

                            }

                        }
                    }

                    if (ResDataSet.Tables.Count > 2)
                    {
                        DataTable CallBackWorkshopIn = ResDataSet.Tables[2];

                        if (CallBackWorkshopIn.Rows.Count > 0)
                        {
                            CallRegistration objCallRegistration;

                            foreach (DataRow dtRowItem in CallBackWorkshopIn.Rows)
                            {
                                objCallRegistration = new CallRegistration();


                                objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                                objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                                objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                                objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                                objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                                objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                                objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                                objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                                objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                                objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                                objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                                objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                                objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                                objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                                objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                                objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                                objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                                objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                                objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                                objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                                objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                                objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                                objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                                objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                                objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                                objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                                objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                                CallBackWorkshopPendingList.Add(objCallRegistration);

                            }

                        }
                    }

                    if (ResDataSet.Tables.Count > 3)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[3];

                        foreach (DataRow dtRowItem in CallDatesTable.Rows)
                        {
                            objTechnicianExpense = new TechnicianExpense();

                            objTechnicianExpense.id = dtRowItem["id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["id"]) : 0;
                            objTechnicianExpense.techid = dtRowItem["techid"] != DBNull.Value ? Convert.ToString(dtRowItem["techid"]) : String.Empty;
                            objTechnicianExpense.type = dtRowItem["type"] != DBNull.Value ? Convert.ToInt32(dtRowItem["type"]) : 0;
                            objTechnicianExpense.amount = dtRowItem["amount"] != DBNull.Value ? Convert.ToInt32(dtRowItem["amount"]) : 0;
                            objTechnicianExpense.date = dtRowItem["date"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["date"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : String.Empty;

                            TechnicianExpenseList.Add(objTechnicianExpense);
                        }
                    }

                    if (ResDataSet.Tables.Count > 4)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[4];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objTechnicianReportData.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objTechnicianReportData.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }


                    if (CallBackList.Count > 0)
                    {
                        List<TechnicianItems> ItemList;

                        var CallListGroupByTechnician = CallBackList.GroupBy(i => i.TechnicianId)
                                            .Select(s => new { TechnicianId = s.Key, CallItem = s.ToList() })
                                            .ToList();

                        if (CallListGroupByTechnician.Count > 0)
                        {
                            foreach (var groupCallItem in CallListGroupByTechnician)
                            {

                                objTechnicianReport = new TechnicianReport();


                                objTechnicianReport.TechnicianId = groupCallItem.TechnicianId;
                                objTechnicianReport.TechnicianName = groupCallItem.CallItem.FirstOrDefault().Technician;
                                objTechnicianReport.TechnicianTypeId = groupCallItem.CallItem.FirstOrDefault().TechType;
                                objTechnicianReport.TechnicianPayment = groupCallItem.CallItem.Sum(p => p.Payment);
                                objTechnicianReport.TechnicianVisitCharge = groupCallItem.CallItem.Sum(p => p.VisitCharge);
                                objTechnicianReport.TechnicianEstimate = groupCallItem.CallItem.Sum(p => p.Estimate);

                                objTechnicianReport.TechnicianExpenceData = new List<TechnicianExpense>();
                                objTechnicianReport.TechnicianExpenceData = TechnicianExpenseList.Count > 0 ? TechnicianExpenseList.Where(e => e.techid == objTechnicianReport.TechnicianId).ToList() : new List<TechnicianExpense>();

                                objTechnicianReport.TechnicianExpence = objTechnicianReport.TechnicianExpenceData.Count > 0 ? objTechnicianReport.TechnicianExpenceData.Sum(x => x.amount) : 0;
                                objTechnicianReport.TotalEarning = objTechnicianReport.TechnicianPayment - objTechnicianReport.TechnicianExpence;

                                //objTechnicianReport.AssignCalls = groupCallItem.CallItem.Count;
                                //objTechnicianReport.DoneCalls = groupCallItem.CallItem.Where(x => x.JobDone == true && x.Canceled == false).ToList().Count;
                                objTechnicianReport.CancelCalls = groupCallItem.CallItem.Where(x => x.Canceled == true).ToList().Count;
                                //objTechnicianReport.Ac_Service = groupCallItem.CallItem.Where(x => x.AC_Service == true).ToList().Count;

                                objTechnicianReport.CallBackCalls = groupCallItem.CallItem.Where(x => x.CallBack == true).ToList().Count;
                                objTechnicianReport.WorkShopIN = groupCallItem.CallItem.Where(x => x.WorkShopIN == true).ToList().Count;
                                objTechnicianReport.Workshop_Pending = objTechnicianReport.CallBackCalls - objTechnicianReport.WorkShopIN;

                                /*
                                objTechnicianReport.Local_Calls_List = new TechnicianLocal();

                                objTechnicianReport.Local_Calls_List.LocalCalls = groupCallItem.CallItem.Where(l => l.CallType == 0).ToList().Count;

                                // Local Calls Data -> In-Warranty
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.CallAttn == true && l.JobDone == false).ToList().Count;
                                
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyCallBackCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.CallBack == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyCallBackRecivedCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.CallBack == true && l.WorkShopIN == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyCallBackPendingCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.CallBack == true && l.WorkShopIN == false).ToList().Count;

                                //objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyWorkshop_JobDone_But_NotDeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.CallAttn == true && l.JobDone == false).ToList().Count;
                                //objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyWorkshop_DeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0 && l.CallAttn == true && l.JobDone == false).ToList().Count;


                                objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // Local Calls Data -> Out-Warranty

                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyCallBackCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1 && l.CallBack == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyCallBackRecivedCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1 && l.CallBack == true && l.WorkShopIN == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyCallBackPendingCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1 && l.CallBack == true && l.WorkShopIN == false).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // Local Calls Data -> Installation

                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2).ToList().Count;

                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2 && l.CallAttn == true && l.JobDone == false).ToList().Count;


                                objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 0 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.Local_Calls_List.Local_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                */

                                objTechnicianReport.Workshop_Calls_List = new TechnicianWorkshop();
                                objTechnicianReport.Workshop_Calls_List.WorkshopCalls = groupCallItem.CallItem.Where(l => l.CallType == 1).ToList().Count;

                                //Workshop Calls -> Out-Warranty

                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0).ToList().Count;

                                //objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                //objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.Canceled == true).ToList().Count;
                                //objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.AC_Service == true).ToList().Count;
                                //objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyCallBackCalls = groupCallItem.CallItem.Where(l => l.ServType == 0 && l.CallBack == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyCallBackRecivedCalls = CallBackWorkshoInList.Where(l => l.TechnicianId == groupCallItem.TechnicianId && l.ServType == 0).ToList().Count();
                                    //groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.CallBack == true && l.WorkShopIN == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyCallBackPendingCalls = CallBackWorkshopPendingList.Where(l => l.TechnicianId == groupCallItem.TechnicianId && l.ServType == 0).ToList().Count();
                                //groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.CallBack == true && l.WorkShopIN == false).ToList().Count;
                                //objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyWorkshop_JobDone_But_NotDeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.Deliver == false && l.JobDone == true).ToList().Count;
                                //objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyWorkshop_DeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.Deliver == true).ToList().Count;
                                /*
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }
                                */
                                // Workshop Calls Data -> Out-Warranty

                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1).ToList().Count;

                                //objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                //objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.Canceled == true).ToList().Count;
                                //objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.AC_Service == true).ToList().Count;
                                //objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.CallAttn == true && l.JobDone == false).ToList().Count;
                                //objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyWorkshop_JobDone_But_NotDeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.Deliver == true && l.JobDone == true).ToList().Count;


                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyCallBackCalls = groupCallItem.CallItem.Where(l => l.ServType == 1 && l.CallBack == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyCallBackRecivedCalls = CallBackWorkshoInList.Where(l => l.TechnicianId == groupCallItem.TechnicianId && l.ServType == 1).ToList().Count();
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyCallBackPendingCalls = CallBackWorkshopPendingList.Where(l => l.TechnicianId == groupCallItem.TechnicianId && l.ServType == 1).ToList().Count();
                                //groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.CallBack == true && l.WorkShopIN == true).ToList().Count;

                                //groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.CallBack == true && l.WorkShopIN == false).ToList().Count;
                                //objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyWorkshop_JobDone_But_NotDeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.Deliver == false && l.JobDone == true).ToList().Count;
                                //objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyWorkshop_DeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.Deliver == true).ToList().Count;

                                /*
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }
                                */

                                /*
                                // Workshop Calls Data -> Installation

                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2).ToList().Count;

                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2 && l.CallAttn == true && l.JobDone == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationWorkshop_JobDone_But_NotDeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2 && l.Deliver == true && l.JobDone == true).ToList().Count;

                                objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.Workshop_Calls_List.Workshop_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                */


                                /*

                                objTechnicianReport.OutStation_Calls_List = new TechnicianOutStation();
                                objTechnicianReport.OutStation_Calls_List.OutStationCalls = groupCallItem.CallItem.Where(l => l.CallType == 2).ToList().Count;

                                //OutStation Calls -> Out-Warranty

                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyCallBackCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0 && l.CallBack == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyCallBackRecivedCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0 && l.CallBack == true && l.WorkShopIN == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyCallBackPendingCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0 && l.CallBack == true && l.WorkShopIN == false).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 0).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_InWarranty_Calls.InWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                // OutStation Calls Data -> Out-Warranty

                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyCallBackCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1 && l.CallBack == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyCallBackRecivedCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1 && l.CallBack == true && l.WorkShopIN == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyCallBackPendingCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1 && l.CallBack == true && l.WorkShopIN == false).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 1).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_OutWarranty_Calls.OutWarrantyItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }


                                // OutStation Calls Data -> Installation

                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls = new TechnicianInstallation();
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2 && l.JobDone == true && l.Canceled == false).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationAC_ServiceCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2 && l.AC_Service == true).ToList().Count;
                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationAttend_But_NotDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2 && l.CallAttn == true && l.JobDone == false).ToList().Count;

                                objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationItems = new List<TechnicianItems>();
                                foreach (var item in groupCallItem.CallItem.Where(l => l.CallType == 2 && l.ServType == 2).ToList())
                                {
                                    objTechnicianReport.OutStation_Calls_List.OutStation_Installation_Calls.InstallationItems.Add(new TechnicianItems() { ItemName = item.ItemName });
                                }

                                */
                                objTechnicianReportData.TechnicianReportList.Add(objTechnicianReport);

                            }
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objTechnicianReportData;
        }


        public TechnicianReportData WorkshopReport(DateTime? FromDate, DateTime? ToDate)
        {
            TechnicianReportData objTechnicianReportData = new TechnicianReportData();
            objTechnicianReportData.TechnicianReportList = new List<TechnicianReport>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"WorkshopReport";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                List<CallRegistration> WorkshopCallList = new List<CallRegistration>();

                List<TechnicianExpense> TechnicianExpenseList = new List<TechnicianExpense>();
                TechnicianExpense objTechnicianExpense;

                TechnicianReport objTechnicianReport = new TechnicianReport();

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                            WorkshopCallList.Add(objCallRegistration);

                        }

                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];

                        foreach (DataRow dtRowItem in CallDatesTable.Rows)
                        {
                            objTechnicianExpense = new TechnicianExpense();

                            objTechnicianExpense.id = dtRowItem["id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["id"]) : 0;
                            objTechnicianExpense.techid = dtRowItem["techid"] != DBNull.Value ? Convert.ToString(dtRowItem["techid"]) : String.Empty;
                            objTechnicianExpense.type = dtRowItem["type"] != DBNull.Value ? Convert.ToInt32(dtRowItem["type"]) : 0;
                            objTechnicianExpense.amount = dtRowItem["amount"] != DBNull.Value ? Convert.ToInt32(dtRowItem["amount"]) : 0;
                            objTechnicianExpense.date = dtRowItem["date"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["date"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : String.Empty;

                            TechnicianExpenseList.Add(objTechnicianExpense);
                        }
                    }

                    if (ResDataSet.Tables.Count > 2)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[2];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objTechnicianReportData.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objTechnicianReportData.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }


                    if (WorkshopCallList.Count > 0)
                    {
                        List<TechnicianItems> ItemList;

                        var CallListGroupByTechnician = WorkshopCallList.GroupBy(i => i.TechnicianId)
                                            .Select(s => new { TechnicianId = s.Key, CallItem = s.ToList() })
                                            .ToList();

                        if (CallListGroupByTechnician.Count > 0)
                        {
                            foreach (var groupCallItem in CallListGroupByTechnician)
                            {

                                objTechnicianReport = new TechnicianReport();


                                objTechnicianReport.TechnicianId = groupCallItem.TechnicianId;
                                objTechnicianReport.TechnicianName = groupCallItem.CallItem.FirstOrDefault().Technician;
                                objTechnicianReport.TechnicianTypeId = groupCallItem.CallItem.FirstOrDefault().TechType;
                                objTechnicianReport.TechnicianPayment = groupCallItem.CallItem.Sum(p => p.Payment);
                                objTechnicianReport.TechnicianVisitCharge = groupCallItem.CallItem.Sum(p => p.VisitCharge);
                                objTechnicianReport.TechnicianEstimate = groupCallItem.CallItem.Sum(p => p.Estimate);

                                objTechnicianReport.TechnicianExpenceData = new List<TechnicianExpense>();
                                objTechnicianReport.TechnicianExpenceData = TechnicianExpenseList.Count > 0 ? TechnicianExpenseList.Where(e => e.techid == objTechnicianReport.TechnicianId).ToList() : new List<TechnicianExpense>();

                                objTechnicianReport.TechnicianExpence = objTechnicianReport.TechnicianExpenceData.Count > 0 ? objTechnicianReport.TechnicianExpenceData.Sum(x => x.amount) : 0;
                                objTechnicianReport.TotalEarning = objTechnicianReport.TechnicianPayment - objTechnicianReport.TechnicianExpence;

                                objTechnicianReport.AssignCalls = groupCallItem.CallItem.Count;
                                objTechnicianReport.DoneCalls = groupCallItem.CallItem.Where(x => x.JobDone == true && x.Canceled == false).ToList().Count;
                                objTechnicianReport.CancelCalls = groupCallItem.CallItem.Where(x => x.Canceled == true).ToList().Count;



                                objTechnicianReport.Workshop_Calls_List = new TechnicianWorkshop();
                                objTechnicianReport.Workshop_Calls_List.WorkshopCalls = groupCallItem.CallItem.Where(l => l.CallType == 1).ToList().Count;

                                //Workshop Calls -> In-Warranty

                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls = new TechnicianInWarranty();
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0).ToList().Count;

                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.JobDone == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyWorkshop_JobDone_But_NotDeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.JobDone == true && l.Deliver == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_InWarranty_Calls.InWarrantyWorkshop_DeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 0 && l.JobDone == true && l.Deliver == true).ToList().Count;

                                //Workshop Calls -> Out-Warranty

                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls = new TechnicianOutWarranty();
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1).ToList().Count;

                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyCancleCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.Canceled == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyJobDoneCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.JobDone == true).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyWorkshop_JobDone_But_NotDeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.JobDone == true && l.Deliver == false).ToList().Count;
                                objTechnicianReport.Workshop_Calls_List.Workshop_OutWarranty_Calls.OutWarrantyWorkshop_DeliverCalls = groupCallItem.CallItem.Where(l => l.CallType == 1 && l.ServType == 1 && l.JobDone == true && l.Deliver == true).ToList().Count;

                                objTechnicianReportData.TechnicianReportList.Add(objTechnicianReport);

                            }
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objTechnicianReportData;
        }

        public TechnicianReportData TechnicianReportIncomeVSExpence(DateTime? FromDate, DateTime? ToDate)
        {
            TechnicianReportData objTechnicianReportData = new TechnicianReportData();
            objTechnicianReportData.TechnicianReportList = new List<TechnicianReport>();

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"DailyTechnicianCallReport";

                lstParam = new List<SqlParameter>();

                SqlParameter FromDate_Param = FromDate.HasValue ? new SqlParameter() { ParameterName = "@FromDate", Value = FromDate } : new SqlParameter() { ParameterName = "@FromDate", Value = DBNull.Value };
                SqlParameter ToDate_Param = ToDate.HasValue ? new SqlParameter() { ParameterName = "@ToDate", Value = ToDate } : new SqlParameter() { ParameterName = "@ToDate", Value = DBNull.Value };

                lstParam.AddRange(new SqlParameter[] { FromDate_Param, ToDate_Param });

                DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                List<CallRegistration> CallRegistrationList = new List<CallRegistration>();

                List<TechnicianExpense> TechnicianExpenseList = new List<TechnicianExpense>();
                TechnicianExpense objTechnicianExpense;

                TechnicianReport objTechnicianReport = new TechnicianReport();

                if (ResDataSet.Tables.Count > 0)
                {
                    DataTable CallRegisterListTable = ResDataSet.Tables[0];

                    if (CallRegisterListTable.Rows.Count > 0)
                    {
                        CallRegistration objCallRegistration;

                        foreach (DataRow dtRowItem in CallRegisterListTable.Rows)
                        {
                            objCallRegistration = new CallRegistration();


                            objCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                            objCallRegistration.StringCreationDate = dtRowItem["CallDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.StringCallAssignDate = dtRowItem["CallAssignDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CallAssignDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                            objCallRegistration.CustomerName = dtRowItem["CustomerName"] != DBNull.Value ? Convert.ToString(dtRowItem["CustomerName"]) : string.Empty;
                            objCallRegistration.MobileNo = dtRowItem["MobileNo"] != DBNull.Value ? Convert.ToString(dtRowItem["MobileNo"]) : string.Empty;
                            objCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                            objCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                            objCallRegistration.ServType = dtRowItem["ServType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["ServType"]) : 0;
                            objCallRegistration.ServTypeName = dtRowItem["ServTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["ServTypeName"]) : string.Empty;
                            objCallRegistration.ItemName = dtRowItem["ItemName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemName"]) : string.Empty;
                            objCallRegistration.FaultDesc = dtRowItem["FaultDesc"] != DBNull.Value ? Convert.ToString(dtRowItem["FaultDesc"]) : string.Empty;
                            objCallRegistration.TechnicianId = dtRowItem["TechnicianId"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianId"]) : string.Empty;
                            objCallRegistration.Technician = dtRowItem["Technician"] != DBNull.Value ? Convert.ToString(dtRowItem["Technician"]) : string.Empty;
                            objCallRegistration.TechType = dtRowItem["TechType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechType"]) : string.Empty;
                            objCallRegistration.TechnicianType = dtRowItem["TechnicianType"] != DBNull.Value ? Convert.ToString(dtRowItem["TechnicianType"]) : string.Empty;

                            objCallRegistration.CallAttn = dtRowItem["CallAttn"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallAttn"]) : false;
                            objCallRegistration.PartPanding = dtRowItem["PartPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PartPanding"]) : false;
                            objCallRegistration.JobDone = dtRowItem["JobDone"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["JobDone"]) : false;
                            objCallRegistration.CallBack = dtRowItem["CallBack"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["CallBack"]) : false;
                            objCallRegistration.GoAfterCall = dtRowItem["GoAfterCall"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["GoAfterCall"]) : false;
                            objCallRegistration.Canceled = dtRowItem["Canceled"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["Canceled"]) : false;
                            objCallRegistration.RepeatFromTech = dtRowItem["RepeatFromTech"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["RepeatFromTech"]) : false;
                            objCallRegistration.PaymentPanding = dtRowItem["PaymentPanding"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PaymentPanding"]) : false;

                            objCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                            objCallRegistration.VisitCharge = dtRowItem["VisitCharge"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["VisitCharge"]) : 0;
                            objCallRegistration.Estimate = dtRowItem["Estimate"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Estimate"]) : 0;
                            objCallRegistration.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;
                            objCallRegistration.AC_Service = dtRowItem["AC_Service"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["AC_Service"]) : false;

                            CallRegistrationList.Add(objCallRegistration);

                        }

                    }

                    if (ResDataSet.Tables.Count > 1)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[1];

                        foreach (DataRow dtRowItem in CallDatesTable.Rows)
                        {
                            objTechnicianExpense = new TechnicianExpense();

                            objTechnicianExpense.id = dtRowItem["id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["id"]) : 0;
                            objTechnicianExpense.techid = dtRowItem["techid"] != DBNull.Value ? Convert.ToString(dtRowItem["techid"]) : String.Empty;
                            objTechnicianExpense.type = dtRowItem["type"] != DBNull.Value ? Convert.ToInt32(dtRowItem["type"]) : 0;
                            objTechnicianExpense.amount = dtRowItem["amount"] != DBNull.Value ? Convert.ToInt32(dtRowItem["amount"]) : 0;
                            objTechnicianExpense.date = dtRowItem["date"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["date"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : String.Empty;

                            TechnicianExpenseList.Add(objTechnicianExpense);
                        }
                    }

                    if (ResDataSet.Tables.Count > 2)
                    {
                        DataTable CallDatesTable = ResDataSet.Tables[2];
                        DataRow dtRowItem = CallDatesTable.Rows[0];

                        objTechnicianReportData.FromDate = dtRowItem["FromDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["FromDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                        objTechnicianReportData.ToDate = dtRowItem["ToDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["ToDate"]).ToString("dd'/'MM'/'yyy hh':'mm':'ss tt") : "-";
                    }


                    if (CallRegistrationList.Count > 0)
                    {
                        var CallListGroupByTechnician = CallRegistrationList.GroupBy(i => i.TechnicianId)
                                            .Select(s => new { TechnicianId = s.Key, CallItem = s.ToList() })
                                            .ToList();

                        if (CallListGroupByTechnician.Count > 0)
                        {
                            foreach (var groupCallItem in CallListGroupByTechnician)
                            {

                                objTechnicianReport = new TechnicianReport();



                                objTechnicianReport.TechnicianId = groupCallItem.TechnicianId;
                                objTechnicianReport.TechnicianName = groupCallItem.CallItem.FirstOrDefault().Technician;
                                objTechnicianReport.TechnicianTypeId = groupCallItem.CallItem.FirstOrDefault().TechType;
                                objTechnicianReport.TechnicianPayment = groupCallItem.CallItem.Sum(p => p.Payment);
                                objTechnicianReport.TechnicianVisitCharge = groupCallItem.CallItem.Sum(p => p.VisitCharge);
                                objTechnicianReport.TechnicianEstimate = groupCallItem.CallItem.Sum(p => p.Estimate);

                                objTechnicianReport.TechnicianExpenceData = new List<TechnicianExpense>();
                                objTechnicianReport.TechnicianExpenceData = TechnicianExpenseList.Count > 0 ? TechnicianExpenseList.Where(e => e.techid == objTechnicianReport.TechnicianId).ToList() : new List<TechnicianExpense>();

                                objTechnicianReport.TechnicianExpence = objTechnicianReport.TechnicianExpenceData.Count > 0 ? objTechnicianReport.TechnicianExpenceData.Sum(x => x.amount) : 0;
                                objTechnicianReport.TotalEarning = objTechnicianReport.TechnicianPayment - objTechnicianReport.TechnicianExpence;

                                objTechnicianReport.AssignCalls = groupCallItem.CallItem.Count;
                                objTechnicianReport.DoneCalls = groupCallItem.CallItem.Where(x => x.JobDone == true && x.Canceled == false).ToList().Count;
                                objTechnicianReport.CancelCalls = groupCallItem.CallItem.Where(x => x.Canceled == true).ToList().Count;
                                objTechnicianReport.Ac_Service = groupCallItem.CallItem.Where(x => x.AC_Service == true).ToList().Count;

                                objTechnicianReport.CallBackCalls = groupCallItem.CallItem.Where(x => x.CallBack == true).ToList().Count;
                                objTechnicianReport.WorkShopIN = groupCallItem.CallItem.Where(x => x.WorkShopIN == true).ToList().Count;
                                objTechnicianReport.Workshop_Pending = objTechnicianReport.CallBackCalls - objTechnicianReport.WorkShopIN;


                                objTechnicianReport.TechnicianInWarrantyCalls = groupCallItem.CallItem.Where(l => l.ServType == 0).ToList().Count;
                                objTechnicianReport.TechnicianOutWarrantyCalls = groupCallItem.CallItem.Where(l => l.ServType == 1).ToList().Count;

                                objTechnicianReportData.TechnicianReportList.Add(objTechnicianReport);

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objTechnicianReportData;
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

        public bool SendCallForRegisterIntoCompany(int ReportId)
        {
            bool blnEmailSend = false;

            DailyMailReportStatus objDailyMailReportStatus = new DailyMailReportStatus();

            try
            {
                objBaseDAL = new BaseDAL();
                strQuery = @"SELECT
	                            A.Id,
	                            A.ItemCompanyName,
	                            A.CompanyEmail,
	                            A.ReportName,
	                            A.SendBySystem,
	                            A.CreatedOn,
	                            A.SendByUser,
	                            A.SendByUserId,
	                            U.UserName,
	                            A.SendByUserDateTime
                            From	
	                            DailyMailReportStatus A
	                            LEFT JOIN users U ON U.id = A.SendByUserId
                            WHERE
	                            A.Id = @ReportId";

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                         {
                                new SqlParameter("@ReportId", ReportId),
                         });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowItem = ResDataTable.Rows[0];

                    objDailyMailReportStatus.Id = dtRowItem["Id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["Id"]) : 0;
                    objDailyMailReportStatus.ItemCompanyName = dtRowItem["ItemCompanyName"] != DBNull.Value ? Convert.ToString(dtRowItem["ItemCompanyName"]) : string.Empty;
                    objDailyMailReportStatus.CompanyEmail = dtRowItem["CompanyEmail"] != DBNull.Value ? Convert.ToString(dtRowItem["CompanyEmail"]) : string.Empty;
                    objDailyMailReportStatus.ReportName = dtRowItem["ReportName"] != DBNull.Value ? Convert.ToString(dtRowItem["ReportName"]) : string.Empty;
                    objDailyMailReportStatus.SendBySystem = dtRowItem["SendBySystem"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["SendBySystem"]) : false;
                    objDailyMailReportStatus.CreatedOnString = dtRowItem["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["CreatedOn"]).ToString("dd'/'MM'/'yyyy'_'HH':'mm':'ss") : string.Empty;
                    objDailyMailReportStatus.SendByUser = dtRowItem["SendByUser"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["SendByUser"]) : false;
                    objDailyMailReportStatus.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;
                    objDailyMailReportStatus.SendByUserId = dtRowItem["SendByUserId"] != DBNull.Value ? Convert.ToInt32(dtRowItem["SendByUserId"]) : 0;
                    objDailyMailReportStatus.SendByUserDateTimeString = dtRowItem["SendByUserDateTime"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["SendByUserDateTime"]).ToString("dd'/'MM'/'yyyy'_'HH':'mm':'ss") : string.Empty;
                }

                string ReportFolderPath = ConfigurationManager.AppSettings["CallRegisterIntoCompanyReportFolder"].ToString();

                string ReportFileName = ReportFolderPath + "\\" + objDailyMailReportStatus.ItemCompanyName + "\\" + objDailyMailReportStatus.ReportName;

                string strToEmail = objDailyMailReportStatus.CompanyEmail;

                string strSubject = "DEMO / INSTALLATION CALL REGISTRATION DATE : " + objDailyMailReportStatus.CreatedOnString;

                StringBuilder SBEmailBody = new StringBuilder();

                SBEmailBody.Append("Dear Sir / Madam");
                SBEmailBody.Append(Environment.NewLine);
                SBEmailBody.Append("Please find the attachment");
                SBEmailBody.Append(Environment.NewLine);
                SBEmailBody.Append("Regiser a call and reply ASAP.");
                SBEmailBody.Append(Environment.NewLine);
                SBEmailBody.Append("Thanks");


                string strBody = SBEmailBody.ToString();

                GmailService objGmailService = new GmailService();

                blnEmailSend = objGmailService.SendMail(strToEmail, strBody, strSubject, ReportFileName);

                if(blnEmailSend)
                {
                    User UserSesionDetail = SessionService.GetUserSessionValues();

                    strQuery = @"UPDATE DailyMailReportStatus SET SendByUser = 1, SendByUserId = @LoginUserId, SendByUserDateTime = GETDATE() WHERE Id = @ReportId";

                    lstParam = new List<SqlParameter>();

                    lstParam.AddRange(new SqlParameter[]
                             {
                                new SqlParameter("@ReportId", ReportId),
                                new SqlParameter("@LoginUserId", UserSesionDetail != null ? UserSesionDetail.id : 0),
                             });

                    objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                }


            }
            catch (Exception ex)
            {
                blnEmailSend = false;
                CommonService.WriteErrorLog(ex);
            }

            return blnEmailSend;

        }

        #endregion

    }
}