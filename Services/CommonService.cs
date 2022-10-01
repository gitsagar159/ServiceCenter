using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ServiceCenter.Services
{
    public static class CommonService
    {
        public static DateTime GetDateWithoutMilliseconds(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
        }

        public static string FormatDateTime(string inputDate)
        {
            //2022-01-15 11:38:29.657000
            //return inputDate.ToString("yyyy'-'MM'-'dd hh':'mm':'ss'.'fff");

            //DateTime dtFormatedDate = DateTime.ParseExact(inputDate, "yyyy'-'MM'-'dd hh':'mm':'ss'.'fff", CultureInfo.InvariantCulture);

            return Convert.ToDateTime(inputDate).ToString("yyyy'-'MM'-'dd hh':'mm':'ss'.'ffffff");
            //dtFormatedDate.ToString();
        }

        public static string GetCurrentDateOnly()
        {
            return DateTime.Now.ToString("yyyy'-'MM'-'dd");
        }

        public static string GetCurrentTimeOnly()
        {
            return DateTime.Now.ToString("hh':'mm':'ss");
        }

        public static void WriteTraceLog(string strTrace)
        {
            
            string TraceLogFolderPath = HttpContext.Current.Server.MapPath("~/Content/TraceLog");
            

            if (!Directory.Exists(TraceLogFolderPath))
            {
                Directory.CreateDirectory(TraceLogFolderPath);
            }

            string TraceFileName = "TraceLogLog_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt";

            string TraceFilePath = Path.Combine(TraceLogFolderPath, TraceFileName);

            if (!File.Exists(TraceFilePath))
            {
                using (FileStream fs = File.Create(TraceFilePath))
                {

                }

                // write file
                WriteTraceInFile(TraceFilePath, strTrace);
            }
            else
            {
                // write file
                WriteTraceInFile(TraceFilePath, strTrace);
            }

        }

        public static void WriteErrorLog(Exception ex)
        {
            //string ErrorLogFolderPath = Application.CommonAppDataPath;
            string ErrorLogFolderPath = HttpContext.Current.Server.MapPath("~/Content/ErrorLog");
            //string ErrorLogFolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"Content\ErrorLog");
            //string ErrorLogFolderPath = @"E:\Projects\BooksAppV2\BooksAppV2\Content\ErrorLog\";



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

                // write file
                WriteExceptioninInFile(ErrorFilePath, ex);
            }
            else
            {
                // write file
                WriteExceptioninInFile(ErrorFilePath, ex);
            }

        }

        private static void WriteTraceInFile(string FilePath, string strTrace)
        {
            using (StreamWriter writer = new StreamWriter(FilePath, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();

                if(!string.IsNullOrEmpty(strTrace))
                {
                    writer.WriteLine(strTrace);
                }
            }
        }

        private static void WriteExceptioninInFile(string FilePath, Exception ex)
        {
            using (StreamWriter writer = new StreamWriter(FilePath, true))
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

        public static string GetCallTypeNameById(int CallTypeId)
        {
            string strCallTypeName = "";

            switch (CallTypeId)
            {
                case 0:
                    strCallTypeName = "Local";
                    break;
                case 1:
                    strCallTypeName = "Workshop";
                    break;
                case 2:
                    strCallTypeName = "Out-Station";
                    break;



            }

            return strCallTypeName;
        }

        public static string GetServiceTypeNameById(int ServiceTypeId)
        {
            string strServiceTypeName = "";

            switch (ServiceTypeId)
            {
                case 0:
                    strServiceTypeName = "In-Warranty";
                    break;
                case 1:
                    strServiceTypeName = "Out-Warranty";
                    break;
                case 2:
                    strServiceTypeName = "Installation";
                    break;

            }

            return strServiceTypeName;
        }
    }
}