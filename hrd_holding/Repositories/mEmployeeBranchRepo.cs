using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mEmployeeBranchRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeBranchRepo");

        public void InsertEmployeeBranch(mEmployeeBranchModel pModel)
        {
            string SqlString = @"INSERT INTO `m_employee_branch`
                                                (`nik`,`seq_no`,`company_code`,`branch_code`,`date_branch`,`flag_type`,`description`,
                                                 `entry_date`,`entry_user`,`edit_date`,`edit_user`)
                                VALUES (@pNik,@pSeqNo,@pCompanyCode,@pBranchCode,@pDateBranch,@pFlagType,@pDescription,
                                        @pEntryDate,@pEntryUser,@pEditDate,@pEditUser)";


            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pNik", pModel.nik);
                        cmd.Parameters.AddWithValue("@pSeqNo", pModel.seq_no);
                        cmd.Parameters.AddWithValue("@pCompanyCode", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pBranchCode", pModel.branch_code);
                        cmd.Parameters.AddWithValue("@pDateBranch", pModel.date_branch);
                        cmd.Parameters.AddWithValue("@pFlagType", pModel.flag_type);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT EMPLOYEE BRANCH SUCCESS ====>>>> Code : " + pModel.nik);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT EMPLOYEE BRANCH FAILED", ex);
            }

        }

        public List<mEmployeeBranchModel> getEmployeeBranchList(int pBranchCode)
        {
            var vList = new List<mEmployeeBranchModel>();
            var strSQL = @"SELECT meb.nik,emp.employee_name,meb.seq_no,meb.company_code,mc.company_name,
                                  meb.branch_code,mbo.branch_name,meb.date_branch,meb.flag_type,meb.description,
                                  meb.entry_date,meb.entry_user,meb.edit_date,meb.edit_user
                           FROM m_employee_branch meb JOIN m_empoloyee emp ON meb.nik = emp.nik
                           JOIN m_company mc ON meb.company_code = mc.company_code
                           JOIN m_brach_office mbo ON md.branch_code = mbo.branch_code
                           WHERE meb.branch_code = @pBranchCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pBranchCode", pBranchCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    var m = new mEmployeeBranchModel
                                    {
                                        nik = aa.GetString("nik"),
                                        employee_name = aa.GetString("employee_name"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        company_code = aa.GetInt16("company_code"),
                                        company_name = aa.GetString("company_name"),
                                        branch_code = aa.GetInt16("branch_code"),
                                        branch_name = aa.GetString("branch_name"),
                                        date_branch = aa.GetDateTime("date_branch"),
                                        flag_type = aa.GetInt16("flag_type"),
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
                Log.Error(DateTime.Now + " GetEmployeeBranchList FAILED... ", ex);
            }
            return vList;
        }

        public mEmployeeBranchModel getEmployeeBranchInfo(string pNik)
        {
            var vModel = new mEmployeeBranchModel();
            var strSQL = @"SELECT meb.nik,emp.employee_name,meb.seq_no,meb.company_code,mc.company_name,
                                  meb.branch_code,mbo.branch_name,meb.date_branch,meb.flag_type,meb.description,
                                  meb.entry_date,meb.entry_user,meb.edit_date,meb.edit_user
                           FROM m_employee_branch meb JOIN m_empoloyee emp ON meb.nik = emp.nik
                           JOIN m_company mc ON md.company_code = mc.company_code
                           JOIN m_brach_office mbo ON md.branch_code = mbo.branch_code
                           WHERE meb.nik = @pNik";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pNik", pNik);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.nik = aa.GetString("nik");
                                    vModel.employee_name = aa.GetString("employee_name");
                                    vModel.seq_no = aa.GetInt16("seq_no");
                                    vModel.company_code = aa.GetInt16("company_code");
                                    vModel.company_name = aa.GetString("company_name");
                                    vModel.branch_code = aa.GetInt16("branch_code");
                                    vModel.branch_name = aa.GetString("branch_name");
                                    vModel.date_branch = aa.GetDateTime("date_branch");
                                    vModel.flag_type = aa.GetInt16("flag_type");
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
                Log.Error(DateTime.Now + " GetEmployeeBranchInfo Failed", ex);
            }
            return vModel;
        }

        public void UpdateEmployeeBranch(mEmployeeBranchModel pModel)
        {
            string SqlString = @"UPDATE `m_employee_branch`
                                        SET `company_code` = @PCompanyCode,
                                            `branch_code` = @pBranchCode,
                                            `date_branch` = @pDateBranch,
                                            `flag_type` = @pFlagType,
                                            `description` = @pDescription,
                                            `entry_date` = @pEntryDate,
                                            `entry_user` = @pEntryUser,
                                            `edit_date` = @pEditDate,
                                            `edit_user` = @pEditUser
                                            WHERE `nik` = @pNik AND `seq_no` = @pSeqNo";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pNik", pModel.nik);
                        cmd.Parameters.AddWithValue("@pSeqNo", pModel.seq_no);
                        cmd.Parameters.AddWithValue("@pCompanyCode", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pBranchCode", pModel.branch_code);
                        cmd.Parameters.AddWithValue("@pDateBranch", pModel.date_branch);
                        cmd.Parameters.AddWithValue("@pFlagType", pModel.flag_type);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE EMPLOYEE BRANCH SUCCESS ====>>>>>> Code : " + pModel.nik + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " UPDATE EMPLOYEE BRANCH FAILED", ex);
            }

        }

        public void DeleteEmployeeBranch(string pCode,int pSeqNo)
        {
            string SqlString = @"DELETE m_employee_branch WHERE nik = @pCode AND seq_no = @pSeqNo";

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
                        Log.Debug(DateTime.Now + " DELETE EMPLOYEE BRANCH SUCCESS ====>>>>>> Code : " + pCode + " SeqNo : " + pSeqNo);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE EMPLOYEE BRANCH FAILED", ex);
            }

        }
    }
}