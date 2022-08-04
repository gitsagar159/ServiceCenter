using ServiceCenter.Models;
using ServiceCenter.Models.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServiceCenter.Services
{
    public class UserService
    {
        string strQuery = "";

        private BaseDAL objBaseDAL;
        private List<SqlParameter> lstParam;

        public User UserLogin(UserLoginModel objUserLoginModel)
        {

            User objUser = new User();

            if (objUserLoginModel != null && !string.IsNullOrEmpty(objUserLoginModel.name) && !string.IsNullOrEmpty(objUserLoginModel.password))
            {
                objBaseDAL = new BaseDAL();

                try
                {
                    objBaseDAL = new BaseDAL();
                    strQuery = "UserLogin";
                    lstParam = new List<SqlParameter>();

                    lstParam.AddRange(new SqlParameter[]
                          {
                                new SqlParameter("@name", objUserLoginModel.name),
                                new SqlParameter("@password", objUserLoginModel.password),
                          });

                    DataSet ResDataSet = objBaseDAL.GetResultDataSet(strQuery, CommandType.StoredProcedure, lstParam);

                    if(ResDataSet.Tables.Count > 0)
                    {

                        DataTable ResponceDataTable = ResDataSet.Tables[0];

                        if(ResponceDataTable != null && ResponceDataTable.Rows.Count > 0)
                        {
                            DataRow dtRow = ResponceDataTable.Rows[0];

                            objUser.Responce = dtRow["Responce"] != DBNull.Value ? Convert.ToBoolean(dtRow["Responce"]) : false;
                            objUser.Message = dtRow["Message"] != DBNull.Value ? Convert.ToString(dtRow["Message"]) : string.Empty;

                        }

                        if(objUser.Responce)
                        {
                            DataTable UserDataTable = ResDataSet.Tables[1];

                            if (UserDataTable.Rows.Count > 0)
                            {
                                DataRow dtRow = UserDataTable.Rows[0];

                                objUser.id = dtRow["id"] != DBNull.Value ? Convert.ToInt32(dtRow["id"]) : 0;
                                objUser.name = dtRow["name"] != DBNull.Value ? Convert.ToString(dtRow["name"]) : string.Empty;
                                objUser.email = dtRow["email"] != DBNull.Value ? Convert.ToString(dtRow["email"]) : string.Empty;
                                objUser.mobile = dtRow["mobile"] != DBNull.Value ? Convert.ToString(dtRow["mobile"]) : string.Empty;
                                objUser.role = dtRow["role"] != DBNull.Value ? Convert.ToString(dtRow["role"]) : string.Empty;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    objUser = new User();
                    objUser.Responce = false;
                    objUser.Message = "Somthing went wrong";
                    CommonService.WriteErrorLog(ex);
                }

            }

            return objUser;

        }
    }
}