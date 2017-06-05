using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mEmployeeExperienceRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeExperienceRepo");

        public ResponseModel InsertEmployeeExperience(mEmployeeExperienceModel pModel)
        {
            var vResp = new ResponseModel();

            string SqlString = @"INSERT INTO `m_employee_exp`
                                            (`employee_code`,`seq_no`,`start_working`,`end_working`,`company_name`,`usaha`,
                                             `department_name`,`last_title`,`last_salary`,`reason_stop_working`,`description`,
                                             `entry_date`,`entry_user`,`edit_date`,`edit_user`)
                                VALUES (@pEmployeeCode,@pSeqNo,@pStartWorking,@pEndWorking,@pCompanyName,@pUsaha,
                                        @pDepartmentName,@pLastTitle,@pLastSalary,@pReasonStopWorking,@pDescription,
                                        @pEntryDate,@pEntryUser,@pEditDate,@pEditUser)";


            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pEmployeeCode", pModel.employee_code);
                        cmd.Parameters.AddWithValue("@pSeqNo", pModel.seq_no);
                        cmd.Parameters.AddWithValue("@pStartWorking", pModel.start_working);
                        cmd.Parameters.AddWithValue("@pEndWorking", pModel.end_working);
                        cmd.Parameters.AddWithValue("@pCompanyName", pModel.company_name);
                        cmd.Parameters.AddWithValue("@pUsaha", pModel.usaha);
                        cmd.Parameters.AddWithValue("@pDepartmentName", pModel.department_name);
                        cmd.Parameters.AddWithValue("@pLastTitle", pModel.last_title);
                        cmd.Parameters.AddWithValue("@pLastSalary", pModel.last_salary);
                        cmd.Parameters.AddWithValue("@pReasonStopWorking", pModel.reason_stop_working);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        vResp.isValid = true;
                        vResp.message = " INSERT EMPLOYEE EXPERIENCE SUCCESS, Code : " + pModel.employee_code + " Name : " + pModel.employee_name;
                        Log.Debug(DateTime.Now + " INSERT EMPLOYEE EXPERIENCE SUCCESS ====>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = "INSERT EMPLOYEE EXPERIENCE FAILED....";
                Log.Error(DateTime.Now + " INSERT EMPLOYEE EXPERIENCE FAILED", ex);
            }

            return vResp;
        }

        public List<mEmployeeExperienceModel> getEmployeeExperienceList(string pEmployeeCode)
        {
            var vList = new List<mEmployeeExperienceModel>();
            var strSQL = @"SELECT mee.employee_code,emp.employee_name,mee.seq_no,mee.start_working,mee.end_working,
                                  IFNULL(mee.company_name,'') company_name,IFNULL(mee.usaha,'') usaha,
                                  IFNULL(mee.department_name,'') department_name,IFNULL(mee.last_title,'') last_title,
                                  IFNULL(mee.last_salary,0) last_salary,IFNULL(mee.reason_stop_working,'') reason_stop_working,
                                  IFNULL(mee.description,'') description,
                                  mee.entry_date,IFNULL(mee.entry_user,'') entry_user,
                                  mee.edit_date,IFNULL(mee.edit_user,'') edit_user
                           FROM m_employee_exp mee JOIN m_employee emp ON mee.employee_code = emp.employee_code
                           WHERE mee.employee_code = @pEmployeeCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pEmployeeCode", pEmployeeCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    var m = new mEmployeeExperienceModel
                                    {
                                        employee_code = aa.GetString("employee_code"),
                                        employee_name = aa.GetString("employee_name"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        start_working = (aa["start_working"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["start_working"]),
                                        end_working = (aa["end_working"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["end_working"]),
                                        company_name = aa.GetString("company_name"),
                                        usaha = aa.GetString("usaha"),
                                        department_name = aa.GetString("department_name"),
                                        last_title = aa.GetString("last_title"),
                                        last_salary = aa.GetDecimal("last_salary"),
                                        reason_stop_working = aa.GetString("reason_stop_working"),
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
                Log.Error(DateTime.Now + " GetEmployeeExperienceList FAILED... ", ex);
            }
            Log.Debug(DateTime.Now + " JML Employee EXPERIENCE List: " + vList.Count);
            return vList;
        }

        public mEmployeeExperienceModel getEmployeeExperienceInfo(string pEmployeeCode, int pSeqNo)
        {
            var vModel = new mEmployeeExperienceModel();
            var strSQL = @"SELECT mee.employee_code,emp.employee_name,mee.seq_no,mee.start_working,mee.end_working,mee.company_name,mee.usaha,
                                  mee.department_name,mee.last_title,mee.last_salary,mee.reason_stop_working,mee.description,
                                  mee.entry_date,mee.entry_user,mee.edit_date,mee.edit_user
                           FROM m_employee_exp mee JOIN m_empoloyee emp ON mee.employee_code = emp.employee_code
                           WHERE mee.employee_code = @pEmployeeCode AND mee.seq_no = @pSeqNo";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pEmployeeCode", pEmployeeCode);
                        cmd.Parameters.AddWithValue("@pSeqNo", pSeqNo);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.employee_code = aa.GetString("employee_code");
                                    vModel.employee_name = aa.GetString("employee_name");
                                    vModel.seq_no = aa.GetInt16("seq_no");
                                    vModel.start_working = aa.GetDateTime("start_working");
                                    vModel.end_working = aa.GetDateTime("end_working");
                                    vModel.company_name = aa.GetString("company_name");
                                    vModel.usaha = aa.GetString("usaha");
                                    vModel.department_name = aa.GetString("department_name");
                                    vModel.last_title = aa.GetString("last_title");
                                    vModel.last_salary = aa.GetDecimal("last_salary");
                                    vModel.reason_stop_working = aa.GetString("reason_stop_working");
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
                Log.Error(DateTime.Now + " GetEmployeeExperienceInfo Failed", ex);
            }
            return vModel;
        }

        public ResponseModel UpdateEmployeeExperience(mEmployeeExperienceModel pModel)
        {
            var vResp = new ResponseModel();

            string SqlString = @"UPDATE `m_employee_exp`
                                    SET `start_working` = @pStartWorking,
                                        `end_working` = @pEndWorking,
                                        `company_name` = @pCompanyName,
                                        `usaha` = @pUsaha,
                                        `department_name` = @pDepartmentName,
                                        `last_title` = @pLastTitle,
                                        `last_salary` = @pLastSalary,
                                        `reason_stop_working` = @pReasonStopWorking,
                                        `description` = @pDescription,
                                        `entry_date` = @pEntryDate,
                                        `entry_user` = @pEntryUser,
                                        `edit_date` = @pEditDate,
                                        `edit_user` = @pEditUser
                                WHERE `employee_code` = @pEmployeeCode AND `seq_no` = @pSeqNo";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pEmployeeCode", pModel.employee_code);
                        cmd.Parameters.AddWithValue("@pSeqNo", pModel.seq_no);
                        cmd.Parameters.AddWithValue("@pStartWorking", pModel.start_working);
                        cmd.Parameters.AddWithValue("@pEndWorking", pModel.end_working);
                        cmd.Parameters.AddWithValue("@pCompanyName", pModel.company_name);
                        cmd.Parameters.AddWithValue("@pUsaha", pModel.usaha);
                        cmd.Parameters.AddWithValue("@pDepartmentName", pModel.department_name);
                        cmd.Parameters.AddWithValue("@pLastTitle", pModel.last_title);
                        cmd.Parameters.AddWithValue("@pLastSalary", pModel.last_salary);
                        cmd.Parameters.AddWithValue("@pReasonStopWorking", pModel.reason_stop_working);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        vResp.isValid = true;
                        vResp.message = " UPDATE EMPLOYEE EXPERIENCE SUCCESS, Code : " + pModel.employee_code + " Name : " + pModel.employee_name;
                        Log.Debug(DateTime.Now + " UPDATE EMPLOYEE EXPERIENCE SUCCESS ====>>>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = "UPDATE EMPLOYEE EXPERIENCE FAILED.....";
                Log.Error(DateTime.Now + " UPDATE EMPLOYEE EXPERIENCE FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel DeleteEmployeeExperience(string pCode, int pSeqNo)
        {
            var vResp = new ResponseModel();

            string SqlString = @"DELETE FROM m_employee_exp WHERE employee_code = @pCode AND seq_no = @pSeqNo";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCode", pCode);
                        cmd.Parameters.AddWithValue("@pSeqNo", pSeqNo);

                        var status = cmd.ExecuteNonQuery();
                        vResp.isValid = true;
                        vResp.message = " DELETE EMPLOYEE EXPERIENCE SUCCESS, Employee Code : " + pCode + " NoSeq : " + pSeqNo;

                        Log.Debug(DateTime.Now + " DELETE EMPLOYEE EXPERIENCE SUCCESS ====>>>>>> Employee Code : " + pCode + " NoSeq : " + pSeqNo);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = "DELETE EMPLOYEE EXPERIENCE FAILED.....";
                Log.Error(DateTime.Now + " DELETE EMPLOYEE EXPERIENCE FAILED", ex);
            }

            return vResp;
        }

        public int getEmployeeExperienceSeqNo(string pEmployeeCode)
        {
            var vSeqNo = 0;
            var strSQL = @"SELECT IFNULL(MAX(seq_no),0) seq_no
                           FROM m_employee_exp emp
                           WHERE emp.employee_code = @pEmployeeCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pEmployeeCode", pEmployeeCode);

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
                Log.Error(DateTime.Now + " GetEmployeeSeqNo Failed", ex);
            }
            return vSeqNo;
        }
    }
}