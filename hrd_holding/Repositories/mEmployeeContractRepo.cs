using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mEmployeeContractRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeContractRepo");

        public void InsertEmployeeContract(mEmployeeContractModel pModel)
        {
            string SqlString = @"INSERT INTO `m_employee_contract` 
                                                (`employee_code`,`seq_no`,`start_date`,`end_date`,`no_contract`,`company`,`chk_company`,
                                                 `description`,`entry_date`,`entry_user`,`edit_date`,`edit_user`)
                                VALUES (@pEmployeeCode,@pSeqNo,@pStartDate,@pEndDate,@pNoContract,@pCompany,
                                        @pChkCompany,@pDescription,@pEntryDate,@pEntryUser,@pEditDate,@pEditUser)";


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
                        cmd.Parameters.AddWithValue("@pStartDate", pModel.start_date);
                        cmd.Parameters.AddWithValue("@pEndDate", pModel.end_date);
                        cmd.Parameters.AddWithValue("@pNoContract", pModel.no_contract);
                        cmd.Parameters.AddWithValue("@pCompany", pModel.company);
                        cmd.Parameters.AddWithValue("@pChkCompany", pModel.chk_company);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT EMPLOYEE CONTRACT SUCCESS ====>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT EMPLOYEE CONTRACT FAILED", ex);
            }

        }

        public List<mEmployeeContractModel> getEmployeeContractList(string pEmployeeCode)
        {
            var vList = new List<mEmployeeContractModel>();
            var strSQL = @"SELECT mec.employee_code,emp.employee_name,mec.seq_no,
                                  mec.start_date,mec.end_date,IFNULL(mec.no_contract,'') no_contract,
                                  IFNULL(mec.company,'') company,mec.chk_company,IFNULL(mec.description,'') description,
                                  mec.entry_date,IFNULL(mec.entry_user,'') entry_user,
                                  mec.edit_date,IFNULL(mec.edit_user,'') edit_user
                           FROM m_employee_contract mec JOIN m_employee emp ON mec.employee_code = emp.employee_code
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
                                    var m = new mEmployeeContractModel
                                    {
                                        employee_code = aa.GetString("employee_code"),
                                        employee_name = aa.GetString("employee_name"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        start_date = (aa["start_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["start_date"]),
                                        end_date = (aa["end_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["end_date"]),
                                        no_contract = aa.GetString("no_contract"),
                                        company = aa.GetString("company"),
                                        chk_company = aa.GetInt16("chk_company"),
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
                Log.Error(DateTime.Now + " GetEmployeeContractList FAILED... ", ex);
            }

            Log.Debug(DateTime.Now + " JML Employee CONTRACT List: " + vList.Count);
            return vList;
        }

        public mEmployeeContractModel getEmployeeContractInfo(string pEmployeeCode,int pSeqNo,string pNoContract)
        {
            var vModel = new mEmployeeContractModel();
            var strSQL = @"SELECT mec.employee_code,emp.employee_name,mec.seq_no,
                                  mec.start_date,mec.end_date,mec.no_contract,
                                  mec.company,mec.chk_company,mec.description,
                                  mec.entry_date,mec.entry_user,mec.edit_date,mec.edit_user
                           FROM m_employee_contract mec JOIN m_empoloyee emp ON mec.employee_code = emp.employee_code
                           WHERE mec.employee_code = @pEmployeeCode AND mec.seq_no = @pSeqNo AND mec.no_contract = @pNoContract";
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
                        cmd.Parameters.AddWithValue("@pNoContract", pNoContract);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.employee_code = aa.GetString("employee_code");
                                    vModel.employee_name = aa.GetString("employee_name");
                                    vModel.seq_no = aa.GetInt16("seq_no");
                                    vModel.start_date = aa.GetDateTime("start_date");
                                    vModel.end_date = aa.GetDateTime("end_date");
                                    vModel.no_contract = aa.GetString("no_contract");
                                    vModel.company = aa.GetString("company");
                                    vModel.chk_company = aa.GetInt16("chk_company");
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
                Log.Error(DateTime.Now + " GetEmployeeContractInfo Failed", ex);
            }
            return vModel;
        }

        public void UpdateEmployeeContract(mEmployeeContractModel pModel)
        {
            string SqlString = @"UPDATE `m_employee_contract`
                                    SET `start_date` = @pStartDate,
                                        `end_date` = @pEndDate,
                                        `company` = @pCompany,
                                        `chk_company` = @pChkCompany,
                                        `description` = @pDescription,
                                        `entry_date` = @pEntryDate,
                                        `entry_user` = @pEntryUser,
                                        `edit_date` = @pEditDate,
                                        `edit_user` = @pEditUser
                                WHERE `employee_code` = @pEmployeeCode AND `seq_no` = @pSeqNo AND `no_contract` = @pNoContract";
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
                        cmd.Parameters.AddWithValue("@pStartDate", pModel.start_date);
                        cmd.Parameters.AddWithValue("@pEndDate", pModel.end_date);
                        cmd.Parameters.AddWithValue("@pNoContract", pModel.no_contract);
                        cmd.Parameters.AddWithValue("@pCompany", pModel.company);
                        cmd.Parameters.AddWithValue("@pChkCompany", pModel.chk_company);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE EMPLOYEE CONTRACT SUCCESS ====>>>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " UPDATE EMPLOYEE CONTRACT FAILED", ex);
            }

        }

        public void DeleteEmployeeContract(string pCode,int pSeqNo,string pNoContract)
        {
            string SqlString = @"DELETE m_employee_contract WHERE employee_code = @pCode AND seq_no = @pSeqNo AND no_contract = @pNoContract";

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
                        cmd.Parameters.AddWithValue("@pNoContract", pNoContract);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " DELETE EMPLOYEE CONTRACT SUCCESS ====>>>>>> Code : " + pCode + " NoContract : " + pNoContract);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE EMPLOYEE CONTRACT FAILED", ex);
            }

        }

    }
}