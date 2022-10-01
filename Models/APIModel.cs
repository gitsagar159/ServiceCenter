using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceCenter.Models
{
    public class APIModel
    {
    }

    public class SandesWalaSendSMSModel
    {
        public string url { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string sender { get; set; }
        public string to { get; set; }
        public string message { get; set; }
        public string reqid { get; set; }
        public string format { get; set; }
        public string route_id { get; set; }
        //public string Template_ID { get; set; }


    }

    public enum JobSMSSendCategory
    {
        //JOB REGISTER , TECH.ALLOT AND JOB DONE
        CallRegister,
        TechnicianAllocation,
        CallDone
    }
    
    public class JobDetailForSMS
    {
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public string JobNo { get; set; }
        public string TechnicianName { get; set; }
    }
}