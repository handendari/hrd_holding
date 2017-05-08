using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mEmployeeCompanyRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeCompanyRepo");

        public void InsertEmployeeCompany(mEmployeeCompanyModel pModel)
        {
            string SqlString = @"INSERT INTO `m_employee_company`
                                        (`employee_code`,`seq_no`,`date_company`,`company_code`,`branch_code`,`department_code`,`title_code`,
                                         `subtitle_code`,`description`,`entry_date`,`entry_user`,`edit_date`,`edit_user`)
                                VALUES (@pEmployeeCode,@pSeqNo,@pDateCompany,@pCompanyCode,@pBranchCode,@pDepartmentCode,@pTitleCode,
                                        @pSubtitleCode,@pDescription,@pEntryDate,@pEntryUser,@pEditDate,@pEditUser)";


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
                        cmd.Parameters.AddWithValue("@pDateCompany", pModel.date_company);
                        cmd.Parameters.AddWithValue("@pCompanyCode", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pBranchCode", pModel.branch_code);
                        cmd.Parameters.AddWithValue("@pDepartmentCode", pModel.department_code);
                        cmd.Parameters.AddWithValue("@pTitleCode", pModel.title_code);
                        cmd.Parameters.AddWithValue("@pSubtitleCode", pModel.subtitle_code);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT EMPLOYEE COMPANY SUCCESS ====>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT EMPLOYEE COMPANY FAILED", ex);
            }

        }

        public List<mEmployeeCompanyModel> getEmployeeCompanyList(int pDepartmentCode)
        {
            var vList = new List<mEmployeeCompanyModel>();
            var strSQL = @"SELECT mec.employee_code,emp.employee_name,
                                  mec.seq_no,mec.date_company,mec.company_code,mc.company_name,mec.branch_code,mbc.branch_name,
                                  mec.department_code,md.department_name,mec.title_code,mt.title_name,
                                  mec.subtitle_code,mst.substitle_name,mec.description,mec.entry_date,mec.entry_user,
                                  mec.edit_date,mec.edit_user
                           FROM m_employee_company mec JOIN m_empoloyee emp ON mec.employee_code = emp.employee_code
                           JOIN m_company mc ON md.company_code = mc.company_code
                           JOIN m_brach_office mbo ON md.branch_code = mbo.branch_code
                           JOIN m_department md mec.department_code = md.department_code
                           JOIN m_title mt ON mec.title_code = mt.title_code
                           LEFT JOIN m_substitle mst ON mec.subtitle_code = mst.subtitle_code
                           WHERE mec.department_code = @pDepartmentCode";
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
                                    var m = new mEmployeeCompanyModel
                                    {
                                        employee_code = aa.GetString("employee_code"),
                                        employee_name = aa.GetString("employee_name"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        date_company = aa.GetDateTime("date_company"),
                                        company_code = aa.GetInt16("company_code"),
                                        company_name = aa.GetString("company_name"),
                                        branch_code = aa.GetInt16("branch_code"),
                                        branch_name = aa.GetString("branch_name"),
                                        department_code = aa.GetInt16("department_code"),
                                        department_name = aa.GetString("department_name"),
                                        title_code = aa.GetInt16("title_code"),
                                        title_name = aa.GetString("title_name"),
                                        subtitle_code = aa.GetInt16("subtitle_code"),
                                        subtitle_name = aa.GetString("subtitle_name"),
                                        description = aa.GetString("description"),
                                        entry_date = aa.GetDateTime("entry_date"),
                                        entry_user = aa.GetString("nik"),
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
                Log.Error(DateTime.Now + " GetEmployeeCompanyList FAILED... ", ex);
            }
            return vList;
        }

        public mEmployeeCompanyModel getEmployeeCompanyInfo(string pEmployeeCode)
        {
            var vModel = new mEmployeeCompanyModel();
            var strSQL = @"SELECT mec.employee_code,emp.employee_name,
                                  mec.seq_no,mec.date_company,mec.company_code,mc.company_name,mec.branch_code,mbc.branch_name,
                                  mec.department_code,md.department_name,mec.title_code,mt.title_name,
                                  mec.subtitle_code,mst.substitle_name,mec.description,mec.entry_date,mec.entry_user,
                                  mec.edit_date,mec.edit_user
                           FROM m_employee_company mec JOIN m_empoloyee emp ON mec.employee_code = emp.employee_code
                           JOIN m_company mc ON md.company_code = mc.company_code
                           JOIN m_brach_office mbo ON md.branch_code = mbo.branch_code
                           JOIN m_department md mec.department_code = md.department_code
                           JOIN m_title mt ON mec.title_code = mt.title_code
                           LEFT JOIN m_substitle mst ON mec.subtitle_code = mst.subtitle_code
                           WHERE mec.employee_code = @pEmployeeCode";
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
                                    vModel.employee_code = aa.GetString("employee_code");
                                    vModel.employee_name = aa.GetString("employee_name");
                                    vModel.seq_no = aa.GetInt16("seq_no");
                                    vModel.date_company = aa.GetDateTime("date_company");
                                    vModel.company_code = aa.GetInt16("company_code");
                                    vModel.company_name = aa.GetString("company_name");
                                    vModel.branch_code = aa.GetInt16("branch_code");
                                    vModel.branch_name = aa.GetString("branch_name");
                                    vModel.department_code = aa.GetInt16("department_code");
                                    vModel.department_name = aa.GetString("department_name");
                                    vModel.title_code = aa.GetInt16("title_code");
                                    vModel.title_name = aa.GetString("title_name");
                                    vModel.subtitle_code = aa.GetInt16("subtitle_code");
                                    vModel.subtitle_name = aa.GetString("subtitle_name");
                                    vModel.description = aa.GetString("description");
                                    vModel.entry_date = aa.GetDateTime("entry_date");
                                    vModel.entry_user = aa.GetString("nik");
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
                Log.Error(DateTime.Now + " GetEmployeeCompanyInfo Failed", ex);
            }
            return vModel;
        }

        public void UpdateEmployeeCompany(mEmployeeCompanyModel pModel)
        {
            string SqlString = @"UPDATE `m_employee_company`
                                    SET `seq_no` = @pSeqNo,
                                        `date_company` = @pDateCompany,
                                        `company_code` = @pCompanyCode,
                                        `branch_code` = @pBranchCode,
                                        `department_code` = @pDepartmentCode,
                                        `title_code` = @pTitleCode,
                                        `subtitle_code` = @pSubtitleCode,
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
                        cmd.Parameters.AddWithValue("@pDateCompany", pModel.date_company);
                        cmd.Parameters.AddWithValue("@pCompanyCode", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pBranchCode", pModel.branch_code);
                        cmd.Parameters.AddWithValue("@pDepartmentCode", pModel.department_code);
                        cmd.Parameters.AddWithValue("@pTitleCode", pModel.title_code);
                        cmd.Parameters.AddWithValue("@pSubtitleCode", pModel.subtitle_code);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE EMPLOYEE COMPANY SUCCESS ====>>>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " UPDATE EMPLOYEE COMPANY FAILED", ex);
            }

        }

        public void DeleteEmployeeCompany(string pCode,int pSeqNo)
        {
            string SqlString = @"DELETE m_employee_company WHERE employee_code = @pCode AND seq_no = @pSeqNo";

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
                        Log.Debug(DateTime.Now + " DELETE EMPLOYEE COMPANY SUCCESS ====>>>>>> Code : " + pCode + " seq no : " + pSeqNo);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE EMPLOYEE COMPANY FAILED", ex);
            }

        }
    }
}