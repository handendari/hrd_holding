using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mEmployeeFamilyRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeFamilyRepo");

        public ResponseModel InsertEmployeeFamily(mEmployeeFamiliesModel pModel)
        {
            var objHasil = new ResponseModel();
            var vStatus = 0;
            var vNo = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();

                    var strSQL = @"SELECT max(mef.seq_no) seq_no
                           FROM m_employee_fams mef JOIN m_employee emp ON mef.employee_code = emp.employee_code
                           WHERE mef.employee_code = @pEmployeeCode";

                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pEmployeeCode", pModel.employee_code);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {

                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vNo = aa.GetInt32("seq_no") + 1;
                                }
                            }
                        }
                    }

                    string SqlString = @"INSERT INTO `m_employee_fams`
                                            (`employee_code`,`seq_no`,`name`,`relationship`,`nm_rel`,`date_birth`,`sex`,`education`,
                                             `employment`,`chk_address`,`address`,`entry_date`,`entry_user`)
                                VALUES (@pEmployeeCode,@pSeqNo,@pName,@pRelationship,@pNmRel,@pDateBirth,@pSex,@pEducation,
                                        @pEmployment,@pChkAddress,@pAddress,@pEntryDate,@pEntryUser)";

                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pEmployeeCode", pModel.employee_code);
                        cmd.Parameters.AddWithValue("@pSeqNo", vNo);
                        cmd.Parameters.AddWithValue("@pName", pModel.name);
                        cmd.Parameters.AddWithValue("@pRelationship", pModel.relationship);
                        cmd.Parameters.AddWithValue("@pNmRel", pModel.nm_rel);
                        cmd.Parameters.AddWithValue("@pDateBirth", pModel.date_birth);
                        cmd.Parameters.AddWithValue("@pSex", pModel.sex);
                        cmd.Parameters.AddWithValue("@pEducation", pModel.education);
                        cmd.Parameters.AddWithValue("@pEmployment", pModel.employment);
                        cmd.Parameters.AddWithValue("@pChkAddress", pModel.chk_address);
                        cmd.Parameters.AddWithValue("@pAddress", pModel.address);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);

                        vStatus = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT EMPLOYEE FAMILY SUCCESS ====>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                        objHasil.isValid = Convert.ToBoolean(vStatus);
                        objHasil.message = " INSERT EMPLOYEE FAMILY SUCCESS ====>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name;
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                objHasil.message = " INSERT EMPLOYEE FAMILY FAILED, Code : " + pModel.employee_code + " Name : " + pModel.employee_name;

                Log.Error(DateTime.Now + " INSERT EMPLOYEE FAMILY FAILED", ex);
            }

            return objHasil;
        }

        public List<mEmployeeFamiliesModel> getEmployeeFamilyList(string pEmployeeCode)
        {
            //Log.Debug(DateTime.Now + "=======>>>> MASUK REPO EMPLOYEE LIST, Emp Code : " + pEmployeeCode);

            var vList = new List<mEmployeeFamiliesModel>();
            var strSQL = @"SELECT mef.employee_code,IFNULL(emp.employee_name,'') employee_name,mef.seq_no,IFNULL(mef.name,'') name,mef.relationship,
                                  mef.nm_rel,mef.date_birth,mef.sex,IFNULL(mef.education,'') education,IFNULL(mef.employment,'') employment,
                                  mef.chk_address,IFNULL(mef.address,'') address,mef.entry_date,IFNULL(mef.entry_user,'') entry_user,
                                  mef.edit_date,IFNULL(mef.edit_user,'') edit_user
                           FROM m_employee_fams mef JOIN m_employee emp ON mef.employee_code = emp.employee_code
                           WHERE mef.employee_code = @pEmployeeCode";
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
                                var i = 0;
                                while (aa.Read())
                                {

                                    //Log.Debug(DateTime.Now + "=======>>>> MASUK LOOPING HASIL QUERY EMP FAMILY KE : " + i++);

                                    var m = new mEmployeeFamiliesModel
                                    {
                                        employee_code = aa.GetString("employee_code"),
                                        employee_name = aa.GetString("employee_name"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        name = aa.GetString("name"),
                                        relationship = aa.GetInt16("relationship"),
                                        nm_rel = aa.GetString("nm_rel"),
                                        date_birth = (aa["date_birth"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["date_birth"]),
                                        sex = aa.GetInt16("sex"),
                                        education = aa.GetString("education"),
                                        employment = aa.GetString("employment"),
                                        chk_address = aa.GetInt16("chk_address"),
                                        address = aa.GetString("address"),
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
                Log.Error(DateTime.Now + " GetEmployeeFamilyList FAILED... ", ex);
            }

            Log.Debug(DateTime.Now + "=======>>>> Jml DATA EMPLOYEE LIST : " + vList.Count);
            return vList;
        }

        public mEmployeeFamiliesModel getEmployeeFamilyInfo(string pEmployeeCode, int pSeqNo)
        {
            var vModel = new mEmployeeFamiliesModel();
            var strSQL = @"SELECT mef.employee_code,emp.employee_name,mef.seq_no,mef.name,mef.relationship,
                                  mef.nm_rel,mef.date_birth,mef.sex,mef.education,mef.employment,mef.chk_address,
                                  mef.address,mef.entry_date,mef.entry_user,mef.edit_date,mef.edit_user
                           FROM m_employee_fams mef JOIN m_empoloyee emp ON mef.employee_code = emp.employee_code
                           WHERE mef.employee_code = @pEmployeeCode AND mef.seq_no = @pSeqNo";
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
                                    vModel.name = aa.GetString("name");
                                    vModel.relationship = aa.GetInt16("relationship");
                                    vModel.nm_rel = aa.GetString("nm_rel");
                                    vModel.date_birth = aa.GetDateTime("date_birth");
                                    vModel.sex = aa.GetInt16("sex");
                                    vModel.education = aa.GetString("education");
                                    vModel.employment = aa.GetString("employment");
                                    vModel.chk_address = aa.GetInt16("chk_address");
                                    vModel.address = aa.GetString("address");
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
                Log.Error(DateTime.Now + " GetEmployeeFamilyInfo Failed", ex);
            }
            return vModel;
        }

        public ResponseModel UpdateEmployeeFamily(mEmployeeFamiliesModel pModel)
        {

            Log.Debug(DateTime.Now + " UPDATE EMPLOYEE FAMILY ===>>> Code : " + pModel.employee_code + 
                " Name : " + pModel.employee_name + " Model USER : " + pModel.edit_user + 
                " Model Edit Date : " + pModel.edit_date);

            int vStatus = 0;
            var objHasil = new ResponseModel();

            string SqlString = @"UPDATE `m_employee_fams`
                                    SET `name` = @pName,
                                        `relationship` = @pRelationship,
                                        `nm_rel` = @pNmRel,
                                        `date_birth` = @pDateBirth,
                                        `sex` = @pSex,
                                        `education` = @pEducation,
                                        `employment` = @pEmployment,
                                        `chk_address` = @pChkAddress,
                                        `address` = @pAddress,
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
                        cmd.Parameters.AddWithValue("@pName", pModel.name);
                        cmd.Parameters.AddWithValue("@pRelationship", pModel.relationship);
                        cmd.Parameters.AddWithValue("@pNmRel", pModel.nm_rel);
                        cmd.Parameters.AddWithValue("@pDateBirth", pModel.date_birth);
                        cmd.Parameters.AddWithValue("@pSex", pModel.sex);
                        cmd.Parameters.AddWithValue("@pEducation", pModel.education);
                        cmd.Parameters.AddWithValue("@pEmployment", pModel.employment);
                        cmd.Parameters.AddWithValue("@pChkAddress", pModel.chk_address);
                        cmd.Parameters.AddWithValue("@pAddress", pModel.address);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user); 

                        vStatus = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE EMPLOYEE FAMILY SUCCESS ====>>>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                        objHasil.isValid = Convert.ToBoolean(vStatus);
                        objHasil.message = " UPDATE EMPLOYEE FAMILY SUCCESS, Code : " + pModel.employee_code + " Name : " + pModel.employee_name;
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                objHasil.message = " UPDATE EMPLOYEE FAMILY FAILED, Code : " + pModel.employee_code + " Name : " + pModel.employee_name;

                Log.Error(DateTime.Now + " UPDATE EMPLOYEE FAMILY FAILED", ex);
            }

            return objHasil;
        }

        public ResponseModel DeleteEmployeeFamily(string pCode, int pSeqNo)
        {
            var objHasil = new ResponseModel();

            var SqlString = @"DELETE FROM m_employee_fams WHERE employee_code = @pCode AND seq_no = @pSeqNo";

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
                        Log.Debug(DateTime.Now + " DELETE EMPLOYEE FAMILY SUCCESS ====>>>>>> Employee Code : " + pCode + " NoSeq : " + pSeqNo);

                        objHasil.isValid = Convert.ToBoolean(status);
                        objHasil.message = " DELETE EMPLOYEE FAMILY SUCCESS ====>>>>>> Employee Code : " + pCode + " NoSeq : " + pSeqNo;
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                objHasil.message = "DELETE EMPLOYEE FAMILY FAILED, Employee Code : " + pCode + " NoSeq : " + pSeqNo;

                Log.Error(DateTime.Now + " DELETE EMPLOYEE FAMILY FAILED", ex);
            }

            return objHasil;
        }

        public int getEmployeeFamilySeqNo(string pEmployeeCode)
        {
            //Log.Debug(DateTime.Now + "=======>>>> MASUK REPO EMPLOYEE LIST, Emp Code : " + pEmployeeCode);

            var vNo = 0;
            var strSQL = @"SELECT max(mef.seq_no) seq_no
                           FROM m_employee_fams mef JOIN m_employee emp ON mef.employee_code = emp.employee_code
                           WHERE mef.employee_code = @pEmployeeCode";
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
                                    vNo = aa.GetInt32("seq_no") + 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetEmployeeFamilyList FAILED... ", ex);
            }

            return vNo;
        }
    }
}