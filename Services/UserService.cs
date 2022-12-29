using ServiceCenter.Models;
using ServiceCenter.Models.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;

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
            string strIpAddress = GetIpAddress();

            string LoginSessionId = string.Empty;

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
                                new SqlParameter("@ipaddress", strIpAddress),

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

                                LoginActivity objLoginActivity = new LoginActivity();

                                objLoginActivity.UserName = objUser.UserName;
                                objLoginActivity.IPAddress = strIpAddress;
                                objLoginActivity.LoginTime = DateTime.Now;

                                LoginSessionId = RegisterLoginSession(objLoginActivity);

                                objUser.LoginSessionId = LoginSessionId;
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


        public List<string> GetAllWindowsUserNames()
        {
            List<string> lstWindowsUsers = new List<string>();

            try
            {
                objBaseDAL = new BaseDAL();
                strQuery = @"SELECT LOWER(UserName) as UserName FROM WindowsUsers WHERE IsActive = 1 AND IsDelete = 0";

                lstParam = new List<SqlParameter>();

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    string strWindowsUserName = string.Empty;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        strWindowsUserName = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                        if(!string.IsNullOrEmpty(strWindowsUserName))
                        {
                            lstWindowsUsers.Add(strWindowsUserName);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return lstWindowsUsers;
        }

        public bool WindowsUserAuthentication()
        {
            bool IsValidUSer = false;

            string strCurrentWindowsUser = HttpContext.Current.User.Identity.Name;

            List<string> lstWindowsUsers = GetAllWindowsUserNames();

            if(lstWindowsUsers != null && lstWindowsUsers.Count > 0 && !string.IsNullOrEmpty(strCurrentWindowsUser))
            {
                IsValidUSer = lstWindowsUsers.Contains(strCurrentWindowsUser.ToLower());
            }

            return IsValidUSer;

        }

        public void EnterWinowsUserNameInDB()
        {
            bool IsUserExist = false;

            string strCurrentWindowsUser = HttpContext.Current.User.Identity.Name;

            List<string> lstWindowsUsers = GetAllWindowsUserNames();

            if (lstWindowsUsers != null && lstWindowsUsers.Count > 0 && !string.IsNullOrEmpty(strCurrentWindowsUser))
            {
                IsUserExist = lstWindowsUsers.Contains(strCurrentWindowsUser.ToLower());
            }

            if(!IsUserExist)
            {
                try
                {
                    objBaseDAL = new BaseDAL();
                    strQuery = @"INSERT INTO WindowsUsers(UserName, IsActive, CreatedOn, CreatedBy, UpdatedOn, UpdatedBy) VALUES(@UserName, @IsActive, @CreatedOn, @CreatedBy, @UpdatedOn, @UpdatedBy)";

                    lstParam = new List<SqlParameter>();


                    
                    SqlParameter UserName_Param = !string.IsNullOrEmpty(strCurrentWindowsUser) ? new SqlParameter() { ParameterName = "@UserName", Value = strCurrentWindowsUser, SqlDbType = SqlDbType.VarChar } : new SqlParameter() { ParameterName = "@UserName", Value = DBNull.Value };
                    SqlParameter IsActive_Param = new SqlParameter() { ParameterName = "@IsActive", Value = true };
                    SqlParameter CreatedOn_Param =  new SqlParameter() { ParameterName = "@CreatedOn", Value = DateTime.Now };
                    SqlParameter CreatedBy_Param = new SqlParameter() { ParameterName = "@CreatedBy", Value = 1 };
                    SqlParameter UpdatedOn_Param = new SqlParameter() { ParameterName = "@UpdatedOn", Value = DateTime.Now };
                    SqlParameter UpdatedBy_Param = new SqlParameter() { ParameterName = "@UpdatedBy", Value = 1 };

                    lstParam.AddRange(new SqlParameter[] { UserName_Param, IsActive_Param, CreatedOn_Param, CreatedBy_Param, UpdatedOn_Param, UpdatedBy_Param});

                    objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                }
                catch (Exception ex)
                {
                    CommonService.WriteErrorLog(ex);
                }

            }

        }
        public string GetIpAddress()
        {
            string strIPAddress = string.Empty;

            //var host = Dns.GetHostEntry(Dns.GetHostName());

            //string LocalIP = host.AddressList[2].AddressFamily == AddressFamily.InterNetwork ?  host.AddressList[2].ToString() : String.Empty;

            strIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(strIPAddress))
            {
                strIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }   


            return strIPAddress;
        }


        public String RegisterLoginSession(LoginActivity objLoginActivity)
        {
            string LoginSessionId = string.Empty;

            try
            {
                objBaseDAL = new BaseDAL();
                strQuery = @"INSERT INTO LoginActivity(LoginSessionId, UserName, LoginTime, IPAddress) VALUES(@LoginSessionId, @UserName, @LoginTime, @IPAddress)";

                Guid id = Guid.NewGuid();

                LoginSessionId = id.ToString().ToUpper();

                objLoginActivity.LoginSessionId = LoginSessionId;

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                         {
                                new SqlParameter("@LoginSessionId", objLoginActivity.LoginSessionId),
                                new SqlParameter("@UserName", objLoginActivity.UserName),
                                new SqlParameter("@LoginTime", objLoginActivity.LoginTime),
                                new SqlParameter("@IPAddress", objLoginActivity.IPAddress),
                         });

                objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                
            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return LoginSessionId;
        }

        public void RegisterLogoutDetail(string LoginSessionId)
        {
            if(!string.IsNullOrEmpty(LoginSessionId))
            {
                try
                {
                    objBaseDAL = new BaseDAL();
                    strQuery = @"UPDATE LoginActivity SET LogoutTime = GETDATE() WHERE LoginSessionId = @LoginSessionId";

                    lstParam = new List<SqlParameter>();

                    lstParam.AddRange(new SqlParameter[]
                             {
                                new SqlParameter("@LoginSessionId", LoginSessionId),
                             });

                    objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                }
                catch (Exception ex)
                {
                    CommonService.WriteErrorLog(ex);
                }
            }
        }

        public UserPageRightsList GetUserPagesRightsListByUserId(int LoginUserId)
        {
            UserPageRightsList objUserPageRightsList = new UserPageRightsList();
            objUserPageRightsList.lstUserPageRights = new List<UserPageRights>();

            if (LoginUserId > 0)
            {
                try
                {
                    objBaseDAL = new BaseDAL();
                    strQuery = @"SELECT 
	                                    P.PageName, UPR.PageCode, UPR.ListRights, UPR.AddRights, UPR.EditRights, UPR.DeleteRights, UPR.PrintRights, UPR.ExportRights 
                                    FROM 
	                                    UserPageRights UPR LEFT JOIN Pages P ON P.PageCode = UPR.PageCode
                                    WHERE UserId = @LoginUserId";

                    lstParam = new List<SqlParameter>();

                    lstParam.AddRange(new SqlParameter[]
                             {
                                new SqlParameter("@LoginUserId", LoginUserId),
                             });

                    DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                    if (ResDataTable.Rows.Count > 0)
                    {
                        UserPageRights objUserPageRights;

                        foreach (DataRow dtRowItem in ResDataTable.Rows)
                        {
                            objUserPageRights = new UserPageRights();


                            //objUserPageRights.Id = dtRowItem["Id"] != DBNull.Value ? Convert.ToInt32(dtRowItem["Id"]) : 0;
                            objUserPageRights.PageName = dtRowItem["PageName"] != DBNull.Value ? Convert.ToString(dtRowItem["PageName"]) : string.Empty;
                            objUserPageRights.PageCode = dtRowItem["PageCode"] != DBNull.Value ? Convert.ToString(dtRowItem["PageCode"]) : string.Empty;
                            objUserPageRights.UserId = LoginUserId;
                            objUserPageRights.ListRights = dtRowItem["ListRights"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["ListRights"]) : false;
                            objUserPageRights.AddRights = dtRowItem["AddRights"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["AddRights"]) : false;
                            objUserPageRights.EditRights = dtRowItem["EditRights"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["EditRights"]) : false;
                            objUserPageRights.DeleteRights = dtRowItem["DeleteRights"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["DeleteRights"]) : false;
                            objUserPageRights.PrintRights = dtRowItem["PrintRights"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["PrintRights"]) : false;
                            objUserPageRights.ExportRights = dtRowItem["ExportRights"] != DBNull.Value ? Convert.ToBoolean(dtRowItem["ExportRights"]) : false;

                            objUserPageRightsList.lstUserPageRights.Add(objUserPageRights);

                        }
                    }

                }
                catch (Exception ex)
                {
                    CommonService.WriteErrorLog(ex);
                }

                
            }
            return objUserPageRightsList;
        }


        public ResponceModel UpdateUserPageRightsByPageCodeAndUserId(string PageCode, int UserId, bool CheckboxValue, string CheckboxType)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();
            string UserName = UserSesionDetail != null ? UserSesionDetail.UserName : string.Empty;

            strQuery = string.Empty;

            switch (CheckboxType)
            {
                case "ListRights":
                    strQuery = "UPDATE UserPageRights SET ListRights = @CheckboxValue WHERE PageCode = @PageCode AND UserId = @UserId";
                    break;
                case "AddRights":
                    strQuery = "UPDATE UserPageRights SET AddRights = @CheckboxValue WHERE PageCode = @PageCode AND UserId = @UserId";
                    break;
                case "EditRights":
                    strQuery = "UPDATE UserPageRights SET EditRights = @CheckboxValue WHERE PageCode = @PageCode AND UserId = @UserId";
                    break;
                case "DeleteRights":
                    strQuery = "UPDATE UserPageRights SET DeleteRights = @CheckboxValue WHERE PageCode = @PageCode AND UserId = @UserId";
                    break;
                case "PrintRights":
                    strQuery = "UPDATE UserPageRights SET PrintRights = @CheckboxValue WHERE PageCode = @PageCode AND UserId = @UserId";
                    break;
                case "ExportRights":
                    strQuery = "UPDATE UserPageRights SET ExportRights = @CheckboxValue WHERE PageCode = @PageCode AND UserId = @UserId";
                    break;
                default:
                    strQuery = "";
                    break;
            }

            try
            {
                if (!string.IsNullOrEmpty(strQuery))
                {
                    objBaseDAL = new BaseDAL();

                    lstParam = new List<SqlParameter>();

                    lstParam.AddRange(new SqlParameter[]
                          {
                                new SqlParameter("@UserId", UserId),
                                new SqlParameter("@PageCode", PageCode),
                                new SqlParameter("@CheckboxValue", CheckboxValue),

                          });

                    objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                    objResponceModel.Responce = true;
                    objResponceModel.Message = "Page Rights Updated";
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);

                objResponceModel = new ResponceModel();
                objResponceModel.Responce = false;
                objResponceModel.Message = "Somthing Went Wrong!";
            }

            return objResponceModel;

        }

        public ResponceModel UpdateAllUserPageRightsByUserId(int UserId, bool CheckboxValue, string CheckboxType)
        {
            ResponceModel objResponceModel = new ResponceModel();

            User UserSesionDetail = SessionService.GetUserSessionValues();
            string UserName = UserSesionDetail != null ? UserSesionDetail.UserName : string.Empty;

            strQuery = string.Empty;

            switch (CheckboxType)
            {
                case "ListRights":
                    strQuery = "UPDATE UserPageRights SET ListRights = @CheckboxValue WHERE UserId = @UserId";
                    break;
                case "AddRights":
                    strQuery = "UPDATE UserPageRights SET AddRights = @CheckboxValue WHERE UserId = @UserId";
                    break;
                case "EditRights":
                    strQuery = "UPDATE UserPageRights SET EditRights = @CheckboxValue WHERE UserId = @UserId";
                    break;
                case "DeleteRights":
                    strQuery = "UPDATE UserPageRights SET DeleteRights = @CheckboxValue WHERE UserId = @UserId";
                    break;
                case "PrintRights":
                    strQuery = "UPDATE UserPageRights SET PrintRights = @CheckboxValue WHERE UserId = @UserId";
                    break;
                case "ExportRights":
                    strQuery = "UPDATE UserPageRights SET ExportRights = @CheckboxValue WHERE UserId = @UserId";
                    break;
                default:
                    strQuery = "";
                    break;
            }

            try
            {
                if (!string.IsNullOrEmpty(strQuery))
                {
                    objBaseDAL = new BaseDAL();

                    lstParam = new List<SqlParameter>();

                    lstParam.AddRange(new SqlParameter[]
                          {
                                new SqlParameter("@UserId", UserId),
                                new SqlParameter("@CheckboxValue", CheckboxValue),

                          });

                    objBaseDAL.ExeccuteStoreCommand(strQuery, CommandType.Text, lstParam);

                    objResponceModel.Responce = true;
                    objResponceModel.Message = "Page Rights Updated";
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);

                objResponceModel = new ResponceModel();
                objResponceModel.Responce = false;
                objResponceModel.Message = "Somthing Went Wrong!";
            }

            return objResponceModel;

        }

        public List<Select2> GetUserList(string UserName)
        {
            List<Select2> lstUser = new List<Select2>();

            try
            {
                objBaseDAL = new BaseDAL();


                if (!string.IsNullOrEmpty(UserName))
                {
                    strQuery = @"SELECT 
                                        id,
	                                    UserName
                                    FROM 
	                                    users 
                                    WHERE 
	                                    UserName LIKE CONCAT('%', @UserName,'%') 
                                    ORDER BY UserName ASC  ";

                }
                else
                {
                    strQuery = @"SELECT 
                                        id,
	                                    UserName
                                    FROM 
	                                    users 
                                    ORDER BY UserName ASC ";
                }

                lstParam = new List<SqlParameter>();

                lstParam.AddRange(new SqlParameter[]
                      {
                                new SqlParameter("@UserName", UserName),
                      });

                DataTable ResDataTable = objBaseDAL.GetResultDataTable(strQuery, CommandType.Text, lstParam);

                if (ResDataTable.Rows.Count > 0)
                {
                    Select2 objSelect2;

                    foreach (DataRow dtRowItem in ResDataTable.Rows)
                    {
                        objSelect2 = new Select2();

                        objSelect2.id = dtRowItem["id"] != DBNull.Value ? Convert.ToString(dtRowItem["id"]) : string.Empty;
                        objSelect2.text = dtRowItem["UserName"] != DBNull.Value ? Convert.ToString(dtRowItem["UserName"]) : string.Empty;

                        lstUser.Add(objSelect2);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonService.WriteErrorLog(ex);
            }

            return lstUser;
        }

    }
}