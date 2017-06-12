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

        public ResponseModel InsertEmployeeCompany(mEmployeeCompanyModel pModel)
        {
            var vResp = new ResponseModel();

            MySqlTransaction myTrans = null;
            var conn = new MySqlConnection(ConfigModel.mConn);

            try
            {
                conn.Open();
                myTrans = conn.BeginTransaction();

                var cmd = new MySqlCommand();
                var SqlString = "";

                #region INSERT EMPLOYEE COMPANY +++++++++++++++
                SqlString = @"INSERT INTO `m_employee_company`
                                    (`employee_code`,`seq_no`,`date_company`,`company_code`,`branch_code`,`department_code`,`title_code`,
                                        `subtitle_code`,`description`,`entry_date`,`entry_user`,`edit_date`,`edit_user`)
                            VALUES (@pEmployeeCode,@pSeqNo,@pDateCompany,@pCompanyCode,@pBranchCode,@pDepartmentCode,@pTitleCode,
                                    @pSubtitleCode,@pDescription,@pEntryDate,@pEntryUser,@pEditDate,@pEditUser)";

                cmd = new MySqlCommand(SqlString, conn, myTrans);
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
                cmd.ExecuteNonQuery();
                #endregion

                #region UPDATE EMPLOYEE +++++++++++++++
                SqlString = @"UPDATE m_employee set company_code = @pCompanyCode,
                                                    branch_code = @pBranchCode,
                                                    department_code = @pDepartmentCode,
                                                    title_code = @pTitleCode,
                                                    subtitle_code = @pSubtitleCode
                                  WHERE employee_code = @pEmployeeCode";
                cmd = new MySqlCommand(SqlString, conn, myTrans);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@pEmployeeCode", pModel.employee_code);
                cmd.Parameters.AddWithValue("@pCompanyCode", pModel.company_code);
                cmd.Parameters.AddWithValue("@pBranchCode", pModel.branch_code);
                cmd.Parameters.AddWithValue("@pDepartmentCode", pModel.department_code);
                cmd.Parameters.AddWithValue("@pTitleCode", pModel.title_code);
                cmd.Parameters.AddWithValue("@pSubtitleCode", pModel.subtitle_code);
                cmd.ExecuteNonQuery();
                #endregion

                myTrans.Commit();
                cmd.Dispose();

                vResp.isValid = true;
                vResp.message = " INSERT EMPLOYEE COMPANY SUCCESS, Code : " + pModel.employee_code + " Name : " + pModel.employee_name;
                Log.Debug(DateTime.Now + " INSERT EMPLOYEE COMPANY SUCCESS ====>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);
            }
            catch (Exception ex)
            {
                myTrans.Rollback();

                vResp.isValid = false;
                vResp.message = " INSERT EMPLOYEE COMPANY FAILED.....";
                Log.Error(DateTime.Now + " INSERT EMPLOYEE COMPANY FAILED", ex);
            }
            finally
            {
                conn.Close();
            }

            return vResp;
        }

        public List<mEmployeeCompanyModel> getEmployeeCompanyList(string pEmployeeCode)
        {
            Log.Error(DateTime.Now + " EMPLOYEE CODE : " + pEmployeeCode);

            var vList = new List<mEmployeeCompanyModel>();
            var strSQL = @"SELECT mec.employee_code,emp.employee_name,
                                  mec.seq_no,mec.date_company,
                                  mec.company_code,mc.int_company,mc.company_name,
                                  mec.branch_code,mbo.int_branch,mbo.branch_name,
                                  mec.department_code,md.int_department,md.department_name,
                                  mec.title_code,mt.int_title,mt.title_name,
                                  IFNULL(mec.subtitle_code,'') subtitle_code,IFNULL(mst.int_subtitle,'') int_subtitle,IFNULL(mst.subtitle_name,'') subtitle_name,
                                  IFNULL(mec.description,'') description,
                                  mec.entry_date,IFNULL(mec.entry_user,'') entry_user,
                                  mec.edit_date,IFNULL(mec.edit_user,'') edit_user
                           FROM m_employee_company mec JOIN m_employee emp ON mec.employee_code = emp.employee_code
                           JOIN m_company mc ON mec.company_code = mc.company_code
                           JOIN m_branch_office mbo ON mec.branch_code = mbo.branch_code
                           JOIN m_department md ON mec.department_code = md.department_code
                           JOIN m_title mt ON mec.title_code = mt.title_code
                           LEFT JOIN m_subtitle mst ON mec.subtitle_code = mst.subtitle_code
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
                                    Log.Error(DateTime.Now + " MASUK LOOPING ADD DATA..... ");

                                    var m = new mEmployeeCompanyModel
                                    {
                                        employee_code = aa.GetString("employee_code"),
                                        employee_name = aa.GetString("employee_name"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        date_company = (aa["date_company"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["date_company"]),
                                        company_code = aa.GetInt16("company_code"),
                                        int_company = aa.GetString("int_company"),
                                        company_name = aa.GetString("company_name"),
                                        branch_code = aa.GetInt16("branch_code"),
                                        int_branch = aa.GetString("int_branch"),
                                        branch_name = aa.GetString("branch_name"),
                                        department_code = aa.GetInt16("department_code"),
                                        int_department = aa.GetString("int_department"),
                                        department_name = aa.GetString("department_name"),
                                        title_code = aa.GetInt16("title_code"),
                                        int_title = aa.GetString("int_title"),
                                        title_name = aa.GetString("title_name"),
                                        subtitle_code = aa.GetInt16("subtitle_code"),
                                        int_subtitle = aa.GetString("int_subtitle"),
                                        subtitle_name = aa.GetString("subtitle_name"),
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
                Log.Error(DateTime.Now + " GetEmployeeCompanyList FAILED... ", ex);
            }
            Log.Error(DateTime.Now + " GetEmployeeCompanyList JmlData : " + vList.Count);
            return vList;
        }

        public mEmployeeCompanyModel getEmployeeCompanyInfo(string pEmployeeCode, int pSeqNo)
        {
            var vModel = new mEmployeeCompanyModel();
            var strSQL = @"SELECT mec.employee_code,emp.employee_name,
                                  mec.seq_no,mec.date_company,
                                  mec.company_code,mc.int_company,mc.company_name,
                                  mec.branch_code,mbo.int_branch,mbo.branch_name,
                                  mec.department_code,md.int_department,md.department_name,
                                  mec.title_code,mt.int_title,mt.title_name,
                                  IFNULL(mec.subtitle_code,'') subtitle_code,IFNULL(mst.int_subtitle,'') int_subtitle,IFNULL(mst.subtitle_name,'') subtitle_name,
                                  IFNULL(mec.description,'') description,
                                  mec.entry_date,IFNULL(mec.entry_user,'') entry_user,
                                  mec.edit_date,IFNULL(mec.edit_user,'') edit_user
                           FROM m_employee_company mec JOIN m_employee emp ON mec.employee_code = emp.employee_code
                           JOIN m_company mc ON mec.company_code = mc.company_code
                           JOIN m_branch_office mbo ON mec.branch_code = mbo.branch_code
                           JOIN m_department md ON mec.department_code = md.department_code
                           JOIN m_title mt ON mec.title_code = mt.title_code
                           LEFT JOIN m_subtitle mst ON mec.subtitle_code = mst.subtitle_code
                           WHERE mec.employee_code = @pEmployeeCode AND mec.seq_no = @pSeqNo";
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
                                    vModel.date_company = (aa["date_company"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["date_company"]);
                                    vModel.company_code = aa.GetInt16("company_code");
                                    vModel.int_company = aa.GetString("int_company");
                                    vModel.company_name = aa.GetString("company_name");
                                    vModel.branch_code = aa.GetInt16("branch_code");
                                    vModel.int_branch = aa.GetString("int_branch");
                                    vModel.branch_name = aa.GetString("branch_name");
                                    vModel.department_code = aa.GetInt16("department_code");
                                    vModel.int_department = aa.GetString("int_department");
                                    vModel.department_name = aa.GetString("department_name");
                                    vModel.title_code = aa.GetInt16("title_code");
                                    vModel.int_title = aa.GetString("int_title");
                                    vModel.title_name = aa.GetString("title_name");
                                    vModel.subtitle_code = aa.GetInt16("subtitle_code");
                                    vModel.int_subtitle = aa.GetString("int_subtitle");
                                    vModel.subtitle_name = aa.GetString("subtitle_name");
                                    vModel.description = aa.GetString("description");
                                    vModel.entry_date = (aa["entry_edit"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["entry_edit"]);
                                    vModel.entry_user = aa.GetString("nik");
                                    vModel.edit_date = (aa["edit_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["edit_date"]);
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

        public ResponseModel UpdateEmployeeCompany(mEmployeeCompanyModel pModel)
        {
            var vResp = new ResponseModel();
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
                        vResp.isValid = true;
                        vResp.message = " UPDATE EMPLOYEE COMPANY SUCCESS, Code : " + pModel.employee_code + " Name : " + pModel.employee_name;
                        Log.Debug(DateTime.Now + " UPDATE EMPLOYEE COMPANY SUCCESS ====>>>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " UPDATE EMPLOYEE COMPANY FAILED......";

                Log.Error(DateTime.Now + " UPDATE EMPLOYEE COMPANY FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel DeleteEmployeeCompany(string pEmployeeCode, int pSeqNo)
        {
            var vResp = new ResponseModel();

            var conn = new MySqlConnection(ConfigModel.mConn);
            MySqlTransaction myTrans = null;
            var cmd = new MySqlCommand();

            try
            {
                conn.Open();
                myTrans = conn.BeginTransaction();

                #region DELETE EMPLOYEE COMPANY++++++++++++++
                string SqlString = @"DELETE FROM m_employee_company WHERE employee_code = @pCode AND seq_no = @pSeqNo";
                cmd = new MySqlCommand(SqlString, conn, myTrans);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@pCode", pEmployeeCode);
                cmd.Parameters.AddWithValue("@pSeqNo", pSeqNo);
                cmd.ExecuteNonQuery();
                #endregion

                #region UPDATE EMPLOYEE +++++++++++++++
                var vSeqNo = getEmployeeCompanySeqNo(pEmployeeCode);
                var vModel = getEmployeeCompanyInfo(pEmployeeCode, vSeqNo);

                SqlString = @"UPDATE m_employee set company_code = @pCompanyCode,
                                                    branch_code = @pBranchCode,
                                                    department_code = @pDepartmentCode,
                                                    title_code = @pTitleCode,
                                                    subtitle_code = @pSubtitleCode
                                  WHERE employee_code = @pEmployeeCode";
                cmd = new MySqlCommand(SqlString, conn, myTrans);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@pEmployeeCode", pEmployeeCode);
                cmd.Parameters.AddWithValue("@pCompanyCode", vModel.company_code);
                cmd.Parameters.AddWithValue("@pBranchCode", vModel.branch_code);
                cmd.Parameters.AddWithValue("@pDepartmentCode", vModel.department_code);
                cmd.Parameters.AddWithValue("@pTitleCode", vModel.title_code);
                cmd.Parameters.AddWithValue("@pSubtitleCode", vModel.subtitle_code);
                cmd.ExecuteNonQuery();
                #endregion

                myTrans.Commit();
                cmd.Dispose();

                vResp.isValid = true;
                vResp.message = " DELETE EMPLOYEE COMPANY SUCCESS, Code : " + pEmployeeCode + " seq no : " + pSeqNo;
                Log.Debug(DateTime.Now + " DELETE EMPLOYEE COMPANY SUCCESS ====>>>>>> Code : " + pEmployeeCode + " seq no : " + pSeqNo);


            }
            catch (Exception ex)
            {
                myTrans.Rollback();

                vResp.isValid = false;
                vResp.message = " DELETE EMPLOYEE COMPANY FAILED.......";

                Log.Error(DateTime.Now + " DELETE EMPLOYEE COMPANY FAILED", ex);
            }
            finally
            {
                conn.Close();
            }

            return vResp;
        }

        public int getEmployeeCompanySeqNo(string pEmployeeCode)
        {
            var vSeqNo = 0;
            var strSQL = @"SELECT IFNULL(MAX(seq_no),0) seq_no
                           FROM m_employee_company emp
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
                                    vSeqNo = aa.GetInt16("seq_no");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetEmployeeCompanySeqNo Failed", ex);
            }
            return vSeqNo;
        }

    }
}