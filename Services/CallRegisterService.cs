using OfficeOpenXml;
using ServiceCenter.Models.Data;
using ServiceCenter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace ServiceCenter.Services
{
    public class CallRegisterService
    {
        string strQuery = "";

        private BaseDAL objBaseDAL;
        private List<SqlParameter> lstParam;

        public byte[] GetCallRegisterDataForCmpanyReport(DateTime FromDate, DateTime ToDate, string CompanyName, int CallCategory, ref StringBuilder sbEmailTrace)
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

                    sbEmailTrace.AppendLine("Company Name : " + CompanyName + "  - " + ResDataTable.Rows.Count.ToString());

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
                        ObjCallRegistration.SrNo = dtRowItem["SrNo"] != DBNull.Value ? Convert.ToString(dtRowItem["SrNo"]) : string.Empty;
                        ObjCallRegistration.JobNo = dtRowItem["JobNo"] != DBNull.Value ? Convert.ToString(dtRowItem["JobNo"]) : string.Empty;
                        ObjCallRegistration.DealRef = dtRowItem["DealRef"] != DBNull.Value ? Convert.ToString(dtRowItem["DealRef"]) : string.Empty;
                        ObjCallRegistration.CompComplaintNo = dtRowItem["CompComplaintNo"] != DBNull.Value ? Convert.ToString(dtRowItem["CompComplaintNo"]) : string.Empty;
                        ObjCallRegistration.EstimateDate = dtRowItem["EstimateDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["EstimateDate"]).ToShortDateString() : string.Empty;
                        ObjCallRegistration.Payment = dtRowItem["Payment"] != DBNull.Value ? Convert.ToDecimal(dtRowItem["Payment"]) : 0;
                        ObjCallRegistration.CallType = dtRowItem["CallType"] != DBNull.Value ? Convert.ToInt32(dtRowItem["CallType"]) : 0;
                        ObjCallRegistration.CallTypeName = dtRowItem["CallTypeName"] != DBNull.Value ? Convert.ToString(dtRowItem["CallTypeName"]) : string.Empty;
                        ObjCallRegistration.EstConfirmDate = dtRowItem["EstConfirmDate"] != DBNull.Value ? Convert.ToDateTime(dtRowItem["EstConfirmDate"]).ToShortDateString() : string.Empty;
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


        public List<ItemForSendCompanyReport> GenerateCompanyViseReport(DateTime FromDate, DateTime ToDate, ref StringBuilder sbEmailTrace)
        {

            sbEmailTrace.AppendLine(Environment.NewLine);

            GmailService objGmailService = new GmailService();

            List<ItemForSendCompanyReport> lstCompanyReportList = new List<ItemForSendCompanyReport>();

            DailyMailReportStatus objDailyMailReportStatus;

            try
            {
                lstCompanyReportList = GetItemListForCompanyReport();


                if (lstCompanyReportList != null && lstCompanyReportList.Count > 0)
                {

                    foreach (ItemForSendCompanyReport reportItem in lstCompanyReportList)
                    {
                        try
                        {
                            byte[] FileData = null;

                            objDailyMailReportStatus = new DailyMailReportStatus();

                            string strFileName = reportItem.ItemCompanyName + "_Call_Report_" + DateTime.Now.ToString("dd'_'MM'_'yyyy'_'hhmmss") + ".xlsx";

                            objDailyMailReportStatus.ItemCompanyName = reportItem.ItemCompanyName;
                            objDailyMailReportStatus.ReportName = strFileName;

                            //string ReportFolderPath = configuration.GetValue<string>("FolderPath:ReportFolderPath"); 
                            string ReportFolderPath = ConfigurationManager.AppSettings["CallRegisterIntoCompanyReportFolder"].ToString();

                            //string ReportDir = "E:\\Projects\\Ketan\\ServieCenter\\Source\\ServiceCenter\\Content\\Reports\\" + reportItem.ItemCompanyName;

                            string ReportDir = ReportFolderPath + "\\" + reportItem.ItemCompanyName;

                            // If directory does not exist, create it
                            if (!Directory.Exists(ReportDir))
                            {
                                Directory.CreateDirectory(ReportDir);
                            }

                            string ReportFileDir = ReportDir + "\\" + strFileName;
                            //string ReportFileDir = ReportDir + "\\" + strFileName;

                            FileData = GetCallRegisterDataForCmpanyReport(FromDate, ToDate, reportItem.ItemCompanyName, 0, ref sbEmailTrace);

                            if (FileData != null)
                            {
                                sbEmailTrace.AppendLine("Report Name: " + ReportFileDir);

                                File.WriteAllBytes(ReportFileDir, FileData);

                                objDailyMailReportStatus.CreatedOn = DateTime.Now;
                                objDailyMailReportStatus.CompanyEmail = reportItem.CompanyEmail;

                                string strToEmail = reportItem.CompanyEmail;
                                //string strToEmail = "mail4dudhaiya@gmail.com";

                                string strSubject = "DEMO / INSTALLATION CALL REGISTRATION DATE : " + DateTime.Now.ToString("dd/MM/yyyy");

                                string strBody = @"Dear Sir / Madam, <br />
                                                
                                                Please find the attachment<br />
                                                
                                                Regiser a call and reply ASAP.<br />

                                                Thanks";



                                bool sendMail = false;

                                //sendMail = objEmailService.Sendmail(strToEmail, strBody, strSubject, ReportFileDir);
                                sendMail = objGmailService.SendMail(strToEmail, strBody, strSubject, ReportFileDir);


                                objDailyMailReportStatus.SendBySystem = sendMail;

                                sbEmailTrace.AppendLine("Mail Sent: " + sendMail);

                                sbEmailTrace.AppendLine("---------------------------------------------------------------------------------------------");
                                sbEmailTrace.AppendLine(Environment.NewLine);

                                InsertRecordInToDailyMailReportStatus(objDailyMailReportStatus);

                                Thread.Sleep(60000);


                                //bool sendMail = objEmailService.Sendmail("dsagar159@gmail.com", strBody, strSubject, ReportFileDir);

                            }

                        }
                        catch (Exception ex)
                        {
                            CommonService.WriteErrorLog(ex);
                        }
                    }

                }




            }
            catch (Exception ex)
            {

                CommonService.WriteErrorLog(ex);
            }


            return lstCompanyReportList;


        }

        public void InsertRecordInToDailyMailReportStatus(DailyMailReportStatus objDailyMailReportStatus)
        {
            if (objDailyMailReportStatus != null)
            {
                try
                {

                    objBaseDAL = new BaseDAL();

                    strQuery = @"INSERT INTO DailyMailReportStatus (ItemCompanyName, CompanyEmail, ReportName, SendBySystem, CreatedOn) VALUES(@ItemCompanyName, @CompanyEmail, @ReportName, @SendBySystem, @CreatedOn) ";

                    lstParam = new List<SqlParameter>();

                    SqlParameter ItemCompanyName_Param = !string.IsNullOrEmpty(objDailyMailReportStatus.ItemCompanyName) ? new SqlParameter() { ParameterName = "@ItemCompanyName", Value = objDailyMailReportStatus.ItemCompanyName } : new SqlParameter() { ParameterName = "@ItemCompanyName", Value = DBNull.Value };
                    SqlParameter CompanyEmail_Param = !string.IsNullOrEmpty(objDailyMailReportStatus.CompanyEmail) ? new SqlParameter() { ParameterName = "@CompanyEmail", Value = objDailyMailReportStatus.CompanyEmail } : new SqlParameter() { ParameterName = "@CompanyEmail", Value = DBNull.Value };
                    SqlParameter ReportName_Param = !string.IsNullOrEmpty(objDailyMailReportStatus.ReportName) ? new SqlParameter() { ParameterName = "@ReportName", Value = objDailyMailReportStatus.ReportName } : new SqlParameter() { ParameterName = "@ReportName", Value = DBNull.Value };
                    SqlParameter SendBySystem_Param = new SqlParameter() { ParameterName = "@SendBySystem", Value = objDailyMailReportStatus.SendBySystem };
                    SqlParameter CreatedOn_Param = objDailyMailReportStatus.CreatedOn.HasValue ? new SqlParameter() { ParameterName = "@CreatedOn", Value = objDailyMailReportStatus.CreatedOn } : new SqlParameter() { ParameterName = "@CreatedOn", Value = DBNull.Value };


                    lstParam.AddRange(new SqlParameter[] { ItemCompanyName_Param, CompanyEmail_Param, ReportName_Param, SendBySystem_Param, CreatedOn_Param });

                    objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                }
                catch (Exception ex)
                {
                    CommonService.WriteErrorLog(ex);
                }

            }

        }
    }
}