using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServiceCenter.Models.Data
{
    public class BaseDAL : SQLConnection
    {
        public DataTable GetResultDataTable(string strQuery, CommandType SqlcommandType, List<SqlParameter> lstParams, string DBName = "ketan2020")
        {
            var staticConnection = StaticSqlConnection(DBName);

            var command = new SqlCommand
            {
                CommandText = strQuery,
                CommandType = SqlcommandType,
                Connection = staticConnection,
            };

            if (lstParams != null)
                command.Parameters.AddRange(lstParams.ToArray());


            var objDataAdapter = new SqlDataAdapter();
            var dt = new DataTable();
            try
            {
                command.CommandTimeout = 60;
                staticConnection.Open();
                objDataAdapter.SelectCommand = command;
                objDataAdapter.Fill(dt);
                staticConnection.Close();
                return dt;
            }
            catch
            {
                throw;
            }
            finally
            {
                dt.Dispose();
                objDataAdapter.Dispose();
                staticConnection.Close();
                command.Dispose();
            }
        }

        public DataSet GetResultDataSet(string strQuery, CommandType SqlcommandType, List<SqlParameter> lstParams, string DBName = "ketan2020")
        {
            var staticConnection = StaticSqlConnection(DBName);

            var command = new SqlCommand
            {
                CommandText = strQuery,
                CommandType = SqlcommandType,
                Connection = staticConnection,
            };

            if (lstParams != null)
                command.Parameters.AddRange(lstParams.ToArray());


            var objDataAdapter = new SqlDataAdapter();
            var ds = new DataSet();
            try
            {
                staticConnection.Open();
                objDataAdapter.SelectCommand = command;
                objDataAdapter.Fill(ds);
                staticConnection.Close();
                return ds;
            }
            catch
            {
                throw;
            }
            finally
            {
                ds.Dispose();
                objDataAdapter.Dispose();
                staticConnection.Close();
                command.Dispose();
            }
        }

        public int ExeccuteStoreCommand(string strQuery, CommandType SqlcommandType, List<SqlParameter> lstParams, bool blnLastId = false, string DBName = "ketan2020")
        {
            int intLastId = 0;

            var staticConnection = StaticSqlConnection(DBName);

            var command = new SqlCommand
            {
                CommandText = strQuery,
                CommandType = SqlcommandType,
                Connection = staticConnection,
            };

            if (lstParams != null)
                command.Parameters.AddRange(lstParams.ToArray());

            try
            {
                staticConnection.Open();
                command.ExecuteNonQuery();

                if (blnLastId)
                {
                    string stQuery2 = "Select @@Identity";
                    command.CommandText = stQuery2;
                    intLastId = (int)command.ExecuteScalar();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                staticConnection.Close();
                command.Dispose();
            }

            return intLastId;
        }
    }
}