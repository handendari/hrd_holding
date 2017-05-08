using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mEmployeeGradeRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeGradeRepo");

        public void InsertEmployeeGrade(mEmployeeGradeModel pModel)
        {
            string SqlString = @"INSERT INTO `m_employee_grade`
                                            (`employee_code`,`seq_no`,`date_grade`,`grade_code`,`description`,`entry_date`,`entry_user`,`edit_date`,`edit_user`)
                                     VALUES (@pEmployeeCode,@pSeqNo,@pDateGrade,@pGradeCode,@pDescription,@pEntryDate,@pEntryUser,@pEditDate,@pEditUser)";


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
                        cmd.Parameters.AddWithValue("@pDateGrade", pModel.date_grade);
                        cmd.Parameters.AddWithValue("@pGradeCode", pModel.grade_code);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT EMPLOYEE GRADE SUCCESS ====>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT EMPLOYEE GRADE FAILED", ex);
            }

        }

        public List<mEmployeeGradeModel> getEmployeeGradeList(int pEmployeeCode)
        {
            var vList = new List<mEmployeeGradeModel>();
            var strSQL = @"SELECT meg.employee_code,emp.employee_name,meg.seq_no,meg.date_grade,meg.grade_code,
                                  meg.description,meg.entry_date,meg.entry_user,meg.edit_date,meg.edit_user
                           FROM m_employee_grade meg JOIN m_empoloyee emp ON meg.employee_code = emp.employee_code
                           WHERE meg.employee_code = @pEmployeeCode";
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
                                    var m = new mEmployeeGradeModel
                                    {
                                        employee_code = aa.GetString("employee_code"),
                                        employee_name = aa.GetString("employee_name"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        date_grade = aa.GetDateTime("date_grade"),
                                        grade_code = aa.GetString("grade_code"),
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
                Log.Error(DateTime.Now + " GetEmployeeGradeList FAILED... ", ex);
            }
            return vList;
        }

        public mEmployeeGradeModel getEmployeeGradeInfo(string pEmployeeCode, int pSeqNo)
        {
            var vModel = new mEmployeeGradeModel();
            var strSQL = @"SELECT meg.employee_code,emp.employee_name,meg.seq_no,meg.date_grade,meg.grade_code,
                                  meg.description,meg.entry_date,meg.entry_user,meg.edit_date,meg.edit_user
                           FROM m_employee_grade meg JOIN m_empoloyee emp ON meg.employee_code = emp.employee_code
                           WHERE meg.employee_code = @pEmployeeCode AND meg.seq_no = @pSeqNo";
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
                                    vModel.date_grade = aa.GetDateTime("date_grade");
                                    vModel.grade_code = aa.GetString("grade_code");
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
                Log.Error(DateTime.Now + " GetEmployeeGradeInfo Failed", ex);
            }
            return vModel;
        }

        public void UpdateEmployeeGrade(mEmployeeGradeModel pModel)
        {
            string SqlString = @"UPDATE `m_employee_grade`
                                    SET `date_grade` = @pDateGrade,
                                        `grade_code` = @pGradeCode,
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
                        cmd.Parameters.AddWithValue("@pDateGrade", pModel.date_grade);
                        cmd.Parameters.AddWithValue("@pGradeCode", pModel.grade_code);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE EMPLOYEE GRADE SUCCESS ====>>>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " UPDATE EMPLOYEE GRADE FAILED", ex);
            }

        }

        public void DeleteEmployeeGrade(string pCode, int pSeqNo)
        {
            string SqlString = @"DELETE m_employee_grade WHERE employee_code = @pCode AND seq_no = @pSeqNo";

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
                        Log.Debug(DateTime.Now + " DELETE EMPLOYEE GRADE SUCCESS ====>>>>>> Employee Code : " + pCode + " NoSeq : " + pSeqNo);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE EMPLOYEE GRADE FAILED", ex);
            }

        }
    }
}