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

            if (objUserLoginModel != null && !string.IsNullOrEmpty(objUserLoginModel.UserName) && !string.IsNullOrEmpty(objUserLoginModel.Password))
            {
                objBaseDAL = new BaseDAL();

                try
                {
                    objBaseDAL = new BaseDAL();
                    strQuery = "UserLogin";
                    lstParam = new List<SqlParameter>();

                    lstParam.AddRange(new SqlParameter[]
                          {
                                new SqlParameter("@name", objUserLoginModel.UserName),
                                new SqlParameter("@password", objUserLoginModel.Password),
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
                                objUser.FirstName = dtRow["FirstName"] != DBNull.Value ? Convert.ToString(dtRow["FirstName"]) : string.Empty;
                                objUser.LastName = dtRow["LastName"] != DBNull.Value ? Convert.ToString(dtRow["LastName"]) : string.Empty;
                                objUser.UserName = dtRow["UserName"] != DBNull.Value ? Convert.ToString(dtRow["UserName"]) : string.Empty;
                                objUser.Email = dtRow["Email"] != DBNull.Value ? Convert.ToString(dtRow["Email"]) : string.Empty;
                                objUser.Mobile = dtRow["Mobile"] != DBNull.Value ? Convert.ToString(dtRow["Mobile"]) : string.Empty;
                                objUser.Role = dtRow["Role"] != DBNull.Value ? Convert.ToInt32(dtRow["Role"]) : 0;
                                objUser.Image = dtRow["Image"] != DBNull.Value ? Convert.ToString(dtRow["Image"]) : string.Empty;
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

        public ResponceModel AddUpdateUser(User Objusers)
        {
            ResponceModel objResponceModel = new ResponceModel();

            if (Objusers != null)
            {
                User UserSesionDetail = SessionService.GetUserSessionValues();

                try
                {
                    objBaseDAL = new BaseDAL();
                    strQuery = @"AddUpdateUserDetail";

                    lstParam = new List<SqlParameter>();


                    SqlParameter id_Param = new SqlParameter() { ParameterName = "@id", Value = Objusers.id, SqlDbType = SqlDbType.Int };
                    SqlParameter FirstName_Param = !string.IsNullOrEmpty(Objusers.FirstName) ? new SqlParameter() { ParameterName = "@FirstName", Value = Objusers.FirstName, SqlDbType = SqlDbType.VarChar } : new SqlParameter() { ParameterName = "@FirstName", Value = DBNull.Value };
                    SqlParameter LastName_Param = !string.IsNullOrEmpty(Objusers.LastName) ? new SqlParameter() { ParameterName = "@LastName", Value = Objusers.LastName, SqlDbType = SqlDbType.VarChar } : new SqlParameter() { ParameterName = "@LastName", Value = DBNull.Value };
                    SqlParameter UserName_Param = !string.IsNullOrEmpty(Objusers.UserName) ? new SqlParameter() { ParameterName = "@UserName", Value = Objusers.UserName, SqlDbType = SqlDbType.VarChar } : new SqlParameter() { ParameterName = "@UserName", Value = DBNull.Value };
                    SqlParameter IsActive_Param = new SqlParameter() { ParameterName = "@IsActive", Value = Objusers.IsActive, SqlDbType = SqlDbType.Bit };
                    SqlParameter Email_Param = !string.IsNullOrEmpty(Objusers.Email) ? new SqlParameter() { ParameterName = "@Email", Value = Objusers.Email, SqlDbType = SqlDbType.VarChar } : new SqlParameter() { ParameterName = "@Email", Value = DBNull.Value };
                    SqlParameter Mobile_Param = !string.IsNullOrEmpty(Objusers.Mobile) ? new SqlParameter() { ParameterName = "@Mobile", Value = Objusers.Mobile, SqlDbType = SqlDbType.VarChar } : new SqlParameter() { ParameterName = "@Mobile", Value = DBNull.Value };
                    SqlParameter APIKey_Param = !string.IsNullOrEmpty(Objusers.APIKey) ? new SqlParameter() { ParameterName = "@APIKey", Value = Objusers.APIKey, SqlDbType = SqlDbType.VarChar } : new SqlParameter() { ParameterName = "@APIKey", Value = DBNull.Value };
                    SqlParameter Password_Param = !string.IsNullOrEmpty(Objusers.Password) ? new SqlParameter() { ParameterName = "@Password", Value = Objusers.Password, SqlDbType = SqlDbType.VarChar } : new SqlParameter() { ParameterName = "@Password", Value = DBNull.Value };
                    SqlParameter Image_Param = !string.IsNullOrEmpty(Objusers.Image) ? new SqlParameter() { ParameterName = "@Image", Value = Objusers.Image, SqlDbType = SqlDbType.NVarChar } : new SqlParameter() { ParameterName = "@Image", Value = DBNull.Value };
                    SqlParameter Role_Param = new SqlParameter() { ParameterName = "@UserRole", Value = Objusers.Role, SqlDbType = SqlDbType.Int };
                    SqlParameter LoginUserId_Param = UserSesionDetail != null ? new SqlParameter() { ParameterName = "@LoginUserId", Value = UserSesionDetail.id, SqlDbType = SqlDbType.Int } : new SqlParameter() { ParameterName = "@LoginUserId", Value = 1 };
                    SqlParameter UserId_Param = new SqlParameter() { ParameterName = "@UserId", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int };


                    lstParam.AddRange(new SqlParameter[] { id_Param, FirstName_Param, LastName_Param, UserName_Param, Email_Param, Mobile_Param, APIKey_Param, IsActive_Param, Password_Param, Role_Param, Image_Param, LoginUserId_Param, UserId_Param });

                    DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                    if (ResDataTable.Rows.Count > 0)
                    {
                        objResponceModel = new ResponceModel();

                        DataRow dtRowItem = ResDataTable.Rows[0];

                        objResponceModel.Responce = dtRowItem["OperationSuccess"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["OperationSuccess"]) : false;
                        objResponceModel.Message = dtRowItem["ResponceMessage"] != DBNull.Value ? Convert.ToString(dtRowItem["ResponceMessage"]) : string.Empty;
                    }

                }
                catch (Exception ex)
                {
                    objResponceModel = new ResponceModel() { Responce = false, Message = "Something went wrong" };
                    CommonService.WriteErrorLog(ex);
                }
            }

            return objResponceModel;
        }

        public User GetUserDetailById(int UserId)
        {
            User objUser = new User();

            try
            {
                objBaseDAL = new BaseDAL();
                strQuery = @"SELECT id, FirstName, LastName, UserName, Email, Mobile, APIKey, Password, Role, Image FROM users Where Id = @UserId";

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                         {
                                new SqlParameter("@UserId", UserId),
                         });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    DataRow dtRowItem = ResDataTable.Rows[0];

                    objUser.id = dtRowItem["id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["id"]) : 0;
                    objUser.FirstName = dtRowItem["FirstName"] != DBNull.Value ? Convert.ToString(dtRowItem["FirstName"]) : string.Empty;
                    objUser.LastName = dtRowItem["LastName"] != DBNull.Value ? Convert.ToString(dtRowItem["LastName"]) : string.Empty;
                    objUser.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;
                    objUser.Password = dtRowItem["Password"] != DBNull.Value ? Convert.ToString(dtRowItem["Password"]) : string.Empty;
                    objUser.Image = dtRowItem["Image"] != DBNull.Value ? Convert.ToString(dtRowItem["Image"]) : string.Empty;
                    objUser.Role = dtRowItem["Role"] != DBNull.Value ? Convert.ToInt32(dtRowItem["Role"]) : 0;
                    
                    objUser.Mobile = dtRowItem["Mobile"] != DBNull.Value ? Convert.ToString(dtRowItem["Mobile"]) : string.Empty;
                    objUser.Email = dtRowItem["Email"] != DBNull.Value ? Convert.ToString(dtRowItem["Email"]) : string.Empty;
                    objUser.APIKey = dtRowItem["APIKey"] != DBNull.Value ? Convert.ToString(dtRowItem["APIKey"]) : string.Empty;

                }
                

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return objUser;
        }


        public UserListDataModel GetUserList(int SortCol, string SortDir, int PageIndex, int PageSize, string UserName, string MobileNo)
        {
            UserListDataModel objUserListDataModel = new UserListDataModel();
            objUserListDataModel.UserList = new List<User>();

            int TotalRecordCount = 0;

            try
            {
                objBaseDAL = new BaseDAL();

                strQuery = @"UserList";

                lstParam = new List<SqlParameter>();

                SqlParameter SortCol_Param = new SqlParameter() { ParameterName = "@SortCol", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = SortCol };
                SqlParameter SortDir_Param = new SqlParameter() { ParameterName = "@SortDir", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = SortDir };
                SqlParameter PageIndex_Param = new SqlParameter() { ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageIndex };
                SqlParameter PageSize_Param = new SqlParameter() { ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = PageSize };
                SqlParameter UserName_Param = new SqlParameter() { ParameterName = "@UserName", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = UserName };
                SqlParameter MobileNo_Param = new SqlParameter() { ParameterName = "@MobileNo", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = MobileNo };
                SqlParameter TotalRecordCount_Param = new SqlParameter() { ParameterName = "@RecordCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                lstParam.AddRange(new SqlParameter[] { SortCol_Param, SortDir_Param, PageIndex_Param, PageSize_Param, UserName_Param, MobileNo_Param, TotalRecordCount_Param });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.StoredProcedure, lstParam);

                TotalRecordCount = Convert.ToInt32(TotalRecordCount_Param.Value);

                if (ResDataTable.Rows.Count > 0)
                {
                    User objUser;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objUser = new User();

                        objUser.RowNo = dtRowItem["RowNo"] != DBNull.Value ? Convert.ToInt32(dtRowItem["RowNo"]) : 0;
                        objUser.id = dtRowItem["id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["id"]) : 0;
                        objUser.FirstName = dtRowItem["FirstName"] != DBNull.Value ? Convert.ToString(dtRowItem["FirstName"]) : string.Empty;
                        objUser.LastName = dtRowItem["LastName"] != DBNull.Value ? Convert.ToString(dtRowItem["LastName"]) : string.Empty;
                        objUser.UserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;
                        objUser.Role = dtRowItem["Role"] != DBNull.Value ? Convert.ToInt32(dtRowItem["Role"]) : 0;
                        objUser.Mobile = dtRowItem["Mobile"] != DBNull.Value ? Convert.ToString(dtRowItem["Mobile"]) : string.Empty;
                        objUser.Email = dtRowItem["Email"] != DBNull.Value ? Convert.ToString(dtRowItem["Email"]) : string.Empty;
                        objUser.IsActive = dtRowItem["IsActive"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["IsActive"]) : false;


                        var userRole = (UserRole)objUser.Role;
                        objUser.StringRole = userRole.ToString();

                        objUserListDataModel.UserList.Add(objUser);

                    }
                    objUserListDataModel.RecordCount = TotalRecordCount;
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }
            return objUserListDataModel;
        }
    }
}