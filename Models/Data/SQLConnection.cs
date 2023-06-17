using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServiceCenter.Models.Data
{
    public class SQLConnection
    {
        static string _staticConnectionString;

        public static SqlConnection StaticSqlConnection(string DBName = "ketan2020")
        {
            SqlConnection staticConnection = new SqlConnection();
            staticConnection.ConnectionString = StaticConnectionString(DBName);
            return staticConnection;

        }


        public static string StaticConnectionString(string DBName)
        {

            if (!string.IsNullOrEmpty(_staticConnectionString))
                return _staticConnectionString;

            string con = ConfigurationManager.ConnectionStrings["KetanServices"].ConnectionString;

            switch (DBName)
            {
                case "ketan2020":
                    con = ConfigurationManager.ConnectionStrings["KetanServices"].ConnectionString;
                    break;
                case "Ketandb":
                    con = ConfigurationManager.ConnectionStrings["KetanERP"].ConnectionString;
                    break;
                case "ketan2015_GoDaddy":
                    con = ConfigurationManager.ConnectionStrings["OldKetanServices"].ConnectionString;
                    break;
                default:
                    con = ConfigurationManager.ConnectionStrings["KetanServices"].ConnectionString;
                    break;
            }

            //string con = DBName == "ketan2020" ? ConfigurationManager.ConnectionStrings["KetanServices"].ConnectionString : DBName == "Ketandb" ? ConfigurationManager.ConnectionStrings["KetanERP"].ConnectionString : string.Empty;

            return con;

        }

        /*
         * 
        public static SqlConnection StaticSqlConnection
        {
            get
            {

                SqlConnection staticConnection = new SqlConnection();
                staticConnection.ConnectionString = StaticConnectionString;
                return staticConnection;
            }
        }
        

        public static string StaticConnectionString
        {
            set { _staticConnectionString = value; }
            get
            {
                if (!string.IsNullOrEmpty(_staticConnectionString))
                    return _staticConnectionString;

                string con = ConfigurationManager.ConnectionStrings["KetanServices"].ConnectionString;

                return con;
            }
        }

        */
    }
}