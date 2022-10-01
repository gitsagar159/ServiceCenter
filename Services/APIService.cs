using ServiceCenter.Models;
using ServiceCenter.Models.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.Text;
using System.Reflection;

namespace ServiceCenter.Services
{
    public class APIService
    {
        string strQuery = "";

        private BaseDAL objBaseDAL;
        private List<SqlParameter> lstParam;

        public SandesWalaSendSMSModel BindSendSMSAPIParam()
        {

            SandesWalaSendSMSModel objSandesWalaSendSMSModel = new SandesWalaSendSMSModel();

            objSandesWalaSendSMSModel.url = ConfigurationManager.AppSettings["SW_URL"].ToString();
            objSandesWalaSendSMSModel.username = ConfigurationManager.AppSettings["SW_UserName"].ToString();
            objSandesWalaSendSMSModel.password = ConfigurationManager.AppSettings["SW_Password"].ToString();
            objSandesWalaSendSMSModel.sender = ConfigurationManager.AppSettings["SW_Sender"].ToString();
            objSandesWalaSendSMSModel.reqid = ConfigurationManager.AppSettings["SW_ReqId"].ToString();
            objSandesWalaSendSMSModel.format = ConfigurationManager.AppSettings["SW_Format"].ToString();
            objSandesWalaSendSMSModel.route_id = ConfigurationManager.AppSettings["SW_RouteId"].ToString();

            return objSandesWalaSendSMSModel;
        }

        public void SendSMSForCall(JobSMSSendCategory SMSCategory, JobDetailForSMS JobDetails)
        {
            SandesWalaSendSMSModel objSandesWalaSendSMSModel = BindSendSMSAPIParam();

            string strSMSText = string.Empty, CustomerName = string.Empty, MobileNo = string.Empty, JobNo = string.Empty, TechnicianName = string.Empty;

            if (JobDetails != null)
            {
                CustomerName = JobDetails.CustomerName;
                MobileNo = JobDetails.MobileNo;
                JobNo = JobDetails.JobNo;
                TechnicianName = JobDetails.TechnicianName;
            }

            switch (SMSCategory)
            {

                case JobSMSSendCategory.CallRegister:
                    strSMSText = CustomerName + ", Your service call has been register. Your service call No is :" + JobNo;
                    break;
                case JobSMSSendCategory.TechnicianAllocation:
                    strSMSText = CustomerName + ", Tecnician(" + TechnicianName + ") Allocated to your service call:" + JobNo;
                    break;
                case JobSMSSendCategory.CallDone:
                    strSMSText = CustomerName + ", Your "+ JobNo + " service call is done";
                    break;
                default:
                    strSMSText = "";
                    break;
            }

            objSandesWalaSendSMSModel.message = strSMSText;

            objSandesWalaSendSMSModel.to = MobileNo;


            try
            {

                StringBuilder SBAPIUrl = new StringBuilder();
                SBAPIUrl.Append(objSandesWalaSendSMSModel.url + "?");

                Type type = objSandesWalaSendSMSModel.GetType();
                PropertyInfo[] props = type.GetProperties();
                
                foreach (var prop in props)
                {
                    if(prop.Name != "url")
                    {
                        SBAPIUrl.Append("&" + prop.Name + "=" + prop.GetValue(objSandesWalaSendSMSModel));
                    }
                    
                }

                //var client = new RestClient("http://sendsms.sandeshwala.com/API/WebSMS/Http/v1.0a/index.php?username=KETANE&password=darshan@2019&sender=KETANE&to=919327142646&message=Mr. Sagar Your call has been register.&reqid=1&format=text&route_id=205");
                var client = new RestClient(SBAPIUrl.ToString());
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Cookie", "PHPSESSID=jg6lvjq6pvh05lgiq18de7b7i1");
                RestResponse response = (RestResponse)client.Execute(request);
                Console.WriteLine(response.Content);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }






    }
}