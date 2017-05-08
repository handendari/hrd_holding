using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mDepartmentRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("DepartmentRepo");

        public void InsertDepartment(mDepartmentModel pModel)
        {
            string SqlString = @"INSERT INTO `m_department` 
                                            (`department_code`,`company_code`,`branch_code`,`int_department`,`department_name`,
                                             `description`,`entry_date`,`entry_user`,`edit_date`,`edit_user`)
                                 VALUES (@pDepartmentCode,@pCompanyCode,@pBranchCode,@pIntDepartment,@pDepartmentName,
                                         @pDescription,@pEntryDate,@pEntryUser,@pEditDate,@pEditUser)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pDepartmentCode", pModel.department_code);
                        cmd.Parameters.AddWithValue("@pCompanyCode", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pBranchCode", pModel.branch_code);
                        cmd.Parameters.AddWithValue("@pIntDepartment", pModel.int_department);
                        cmd.Parameters.AddWithValue("@pDepartmentName", pModel.department_name);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT DEPARTMENT SUCCESS ====>>>> Code : " + pModel.department_code + " Name : " + pModel.department_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT DEPARTMENT FAILED", ex);
            }

        }

        public List<mDepartmentModel> getDepartmentList(int pCompanyCode,int pBranchCode)
        {
            var vList = new List<mDepartmentModel>();
            var strSQL = @"SELECT md.department_code,md.company_code,mc.company_name,
                                  md.branch_code,mbo.branch_name, md.int_department,md.department_name,
                                  md.description,md.entry_date,md.entry_user,md.edit_date,md.edit_user
                           FROM m_department md JOIN m_company mc ON md.company_code = mc.company_code
                           JOIN m_brach_office mbo ON md.branch_code = mbo.branch_code
                           WHERE md.company_code = @pCompanyCode AND md.branch_code = @pBranchCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCompanyCode", pCompanyCode);
                        cmd.Parameters.AddWithValue("@pBranchCode", pBranchCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    var m = new mDepartmentModel
                                    {
                                        department_code = aa.GetInt16("department_code"),
                                        company_code = aa.GetInt16("company_code"),
                                        company_name = aa.GetString("company_name"),
                                        branch_code = aa.GetInt16("branch_code"),
                                        branch_name = aa.GetString("branch_name"),
                                        int_department = aa.GetString("int_department"),
                                        department_name = aa.GetString("department_name"),
                                        description = aa.GetString("description"),
                                        entry_date = aa.GetDateTime("entry_date"),
                                        entry_user = aa.GetString("entry_user"),
                                        edit_date = aa.GetDateTime("edit_date"),
                                        edit_user = aa.GetString("edit_user")
                                    };
                                    vList.Add(m);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetDepartmentList FAILED... ", ex);
            }
            return vList;
        }

        public mDepartmentModel getDepartmentInfo(string pDepartmentCode)
        {
            var vModel = new mDepartmentModel();
            var strSQL = @"SELECT md.department_code,md.company_code,mc.company_name,
                                  md.branch_code,mbo.branch_name, md.int_department,md.department_name,
                                  md.description,md.entry_date,md.entry_user,md.edit_date,md.edit_user
                           FROM m_department md JOIN m_company mc ON md.company_code = mc.company_code
                           JOIN m_brach_office mbo ON md.branch_code = mbo.branch_code
                           WHERE md.department_code = @pDepartmentCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pDepartmentCode", pDepartmentCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.department_code = aa.GetInt16("department_code");
                                    vModel.company_code = aa.GetInt16("company_code");
                                    vModel.company_name = aa.GetString("company_name");
                                    vModel.branch_code = aa.GetInt16("branch_code");
                                    vModel.branch_name = aa.GetString("branch_name");
                                    vModel.int_department = aa.GetString("int_department");
                                    vModel.department_name = aa.GetString("department_name");
                                    vModel.description = aa.GetString("description");
                                    vModel.entry_date = aa.GetDateTime("entry_date");
                                    vModel.entry_user = aa.GetString("entry_user");
                                    vModel.edit_date = aa.GetDateTime("edit_date");
                                    vModel.edit_user = aa.GetString("edit_user");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetDepartmentList Failed", ex);
            }
            return vModel;
        }

        public void UpdateDepartment(mDepartmentModel pModel)
        {
            string SqlString = @"UPDATE m_department 
                                        SET `department_code` = @pDepartmentCode,
                                            `company_code` = @pCompanyCode,
                                            `branch_code` = @pBranchCode,
                                            `int_department` = @pIntDepartment,
                                            `department_name` = @pDepartmentName,
                                            `description` = @pDescription,
                                            `entry_date` = @pEntryDate,
                                            `entry_user` = @pEntryUser,
                                            `edit_date` = @pEditDate,
                                            `edit_user` = @pEditUser
                                WHERE `department_code` = @pCompanyCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pDepartmentCode", pModel.department_code);
                        cmd.Parameters.AddWithValue("@pCompanyCode", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pBranchCode", pModel.branch_code);
                        cmd.Parameters.AddWithValue("@pIntDepartment", pModel.int_department);
                        cmd.Parameters.AddWithValue("@pDepartmentName", pModel.department_name);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE COMPANY SUCCESS ====>>>>>> Code : " + pModel.company_code + " Name : " + pModel.company_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT COMPANY FAILED", ex);
            }

        }

        public void DeleteDepartment(string pCode)
        {
            string SqlString = @"DELETE m_department WHERE department_code = @pCode";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pCode", pCode);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " DELETE DEPARTMENT SUCCESS ====>>>>>> Code : " + pCode);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE DEPARTMENT FAILED", ex);
            }
        }
    }
}