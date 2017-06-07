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

        public ResponseModel InsertDepartment(mDepartmentModel pModel)
        {
            var vResp = new ResponseModel();
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
                        vResp.isValid = true;
                        vResp.message = " INSERT DEPARTMENT SUCCESS, Code : " + pModel.department_code + " Name : " + pModel.department_name;
                        Log.Debug(DateTime.Now + " INSERT DEPARTMENT SUCCESS ====>>>> Code : " + pModel.department_code + " Name : " + pModel.department_name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " INSERT DEPARTMENT FAILED.....";
                Log.Error(DateTime.Now + " INSERT DEPARTMENT FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel getDepartmentList(int pCompanyCode, int pBranchCode, 
                                               int? pStartRow = 0, int? pRows = 0, string pWhere = "", string pOrderBy = "")
        {
            var vLimit = pOrderBy + " LIMIT " + pStartRow + "," + pRows;

            var vJmlRecord = 0;
            var vList = new List<mDepartmentModel>();          

            var strSQLCount = @"SELECT COUNT(department_code) jml_record
                                FROM m_department 
                                WHERE company_code = @pCompanyCode AND branch_code = @pBranchCode " + pWhere;

            var strSQL = @"SELECT md.department_code,md.company_code,mc.company_name,
                                  md.branch_code,mbo.branch_name, md.int_department,md.department_name,
                                  md.description,md.entry_date,IFNULL(md.entry_user,'') entry_user,
                                  md.edit_date,IFNULL(md.edit_user,'') edit_user
                           FROM m_department md JOIN m_company mc ON md.company_code = mc.company_code
                           JOIN m_branch_office mbo ON md.branch_code = mbo.branch_code
                           WHERE md.company_code = @pCompanyCode AND md.branch_code = @pBranchCode " +
                           pWhere + " " + vLimit;

            Log.Debug(DateTime.Now + "+======>>>> strSQL : " + strSQL);

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQLCount, conn))
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
                                    vJmlRecord = aa.GetInt32("jml_record");
                                }
                            }
                        }
                    }

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
                                        entry_date = (aa["entry_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["entry_date"]),
                                        entry_user = aa.GetString("entry_user"),
                                        edit_date = (aa["edit_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["edit_date"]),
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

            var vRes = new ResponseModel();
            vRes.total_record = vJmlRecord;
            vRes.objResult = vList;

            return vRes;
        }

        public mDepartmentModel getDepartmentInfo(int pDepartmentCode)
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

        public ResponseModel UpdateDepartment(mDepartmentModel pModel)
        {
            var vResp = new ResponseModel();
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
                        vResp.isValid = true;
                        vResp.message = " UPDATE DEPARTMENT SUCCESS, Code : " + pModel.company_code + " Name : " + pModel.company_name;
                        Log.Debug(DateTime.Now + " UPDATE DEPARTMENT SUCCESS ====>>>>>> Code : " + pModel.company_code + " Name : " + pModel.company_name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " INSERT DEPARTMENT FAILED.....";
                Log.Error(DateTime.Now + " INSERT DEPARTMENT FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel DeleteDepartment(int pCode)
        {
            var vResp = new ResponseModel();

            string SqlString = @"DELETE FROM m_department WHERE department_code = @pCode";

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
                        vResp.isValid = true;
                        vResp.message = " DELETE DEPARTMENT SUCCESS, Code : " + pCode;
                        Log.Debug(DateTime.Now + " DELETE DEPARTMENT SUCCESS ====>>>>>> Code : " + pCode);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " DELETE DEPARTMENT FAILED......";
                Log.Error(DateTime.Now + " DELETE DEPARTMENT FAILED", ex);
            }

            return vResp;
        }

        public int getDepartmentSeqNo()
        {
            var vSeqNo = 0;
            var strSQL = @"SELECT IFNULL(MAX(department_code),0) seq_no
                           FROM m_department md";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vSeqNo = aa.GetInt16("seq_no") + 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetDepartmentSeqNo Failed", ex);
            }
            return vSeqNo;
        }
    }
}