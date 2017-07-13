using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class hrdRecruitmentFamRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdRecruitmentFamRepo");

        public ResponseModel InsertRecruitmentFamily(hrdRecruitmentFamModel pModel)
        {
            var objHasil = new ResponseModel();
            var vStatus = 0;
            var vNo = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    string SqlString = @"INSERT INTO hrd_recruitment_fams
                                                (req_id,recruitment_id,seq_no,name,flag_relationship,name_relationship,date_birth,
                                                 flag_gender,education,occupation,name_employer,address,entry_date,entry_user)
                                         VALUES (@preq_id,@precruitment_id,@pseq_no,@pname,@pflag_relationship,@pname_relationship,@pdate_birth,
                                                 @pflag_gender,@peducation,@poccupation,@pname_employer,@paddress,@pentry_date,@pentry_user)";

                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@preq_id", pModel.req_id);
                        cmd.Parameters.AddWithValue("@precruitment_id", pModel.recruitment_id);
                        cmd.Parameters.AddWithValue("@pseq_no", pModel.seq_no);
                        cmd.Parameters.AddWithValue("@pname", pModel.name);
                        cmd.Parameters.AddWithValue("@pflag_relationship", pModel.flag_relationship);
                        cmd.Parameters.AddWithValue("@pname_relationship", pModel.name_relationship);
                        cmd.Parameters.AddWithValue("@pdate_birth", pModel.date_birth);
                        cmd.Parameters.AddWithValue("@pflag_gender", pModel.flag_gender);
                        cmd.Parameters.AddWithValue("@peducation", pModel.education);
                        cmd.Parameters.AddWithValue("@poccupation", pModel.occupation);
                        cmd.Parameters.AddWithValue("@pname_employer", pModel.name_employer);
                        cmd.Parameters.AddWithValue("@paddress", pModel.address);
                        cmd.Parameters.AddWithValue("@pentry_date", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pentry_user", pModel.entry_user);

                        vStatus = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT RECRUITMENT FAMILY SUCCESS....<br/> REQUEST ID : " + pModel.req_id + " Name : " + pModel.name);

                        objHasil.isValid = Convert.ToBoolean(vStatus);
                        objHasil.message = " INSERT RECRUITMENT FAMILY SUCCESS ====>>>> REQUEST ID : " + pModel.req_id + " Name : " + pModel.name;
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                objHasil.message = " INSERT RECRUITMENT FAMILY FAILED, REQUEST ID : " + pModel.req_id + " Name : " + pModel.name;

                Log.Error(DateTime.Now + " INSERT RECRUITMENT FAMILY FAILED", ex);
            }

            return objHasil;
        }

        public List<hrdRecruitmentFamModel> getRecruitmentFamilyList(string pRecruitmentId)
        {
            //Log.Debug(DateTime.Now + "=======>>>> MASUK REPO EMPLOYEE LIST, Emp Code : " + pEmployeeCode);

            var vList = new List<hrdRecruitmentFamModel>();
            var strSQL = @"SELECT req_id,recruitment_id,seq_no,name,flag_relationship,name_relationship,date_birth,
                                  flag_gender,education,occupation,name_employer,address,entry_date,entry_user
                           FROM hrd_recruitment_fams  hrf
                           WHERE hrf.recruitment_id = @pRecruitmentId";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pRecruitmentId", pRecruitmentId);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {

                            if (aa.HasRows)
                            {
                                var i = 0;
                                while (aa.Read())
                                {

                                    //Log.Debug(DateTime.Now + "=======>>>> MASUK LOOPING HASIL QUERY EMP FAMILY KE : " + i++);

                                    var m = new hrdRecruitmentFamModel
                                    {   req_id = aa.GetInt16("req_id"),
                                        recruitment_id = aa.GetInt16("recruitment_id"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        name = aa.GetString("name"),
                                        flag_relationship = aa.GetBoolean("flag_relationship"),
                                        name_relationship = aa.GetString("name_relationship"),
                                        date_birth = (aa["date_birth"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["date_birth"]),
                                        flag_gender = aa.GetBoolean("flag_gender"),
                                        education = aa.GetString("education"),
                                        occupation = aa.GetString("occupation"),
                                        name_employer = aa.GetString("name_employer"),
                                        address = aa.GetString("address"),
                                        entry_date = (aa["entry_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["entry_date"]),
                                        entry_user = aa.GetString("entry_user")
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
                Log.Error(DateTime.Now + " getRecruitmentFamilyList FAILED... ", ex);
            }

            Log.Debug(DateTime.Now + "=======>>>> Jml DATA getRecruitmentFamily LIST : " + vList.Count);
            return vList;
        }

        public mEmployeeFamiliesModel getRecruitmentFamilyInfo(string pId)
        {
            var vModel = new mEmployeeFamiliesModel();
            var strSQL = @"SELECT req_id,recruitment_id,seq_no,name,flag_relationship,name_relationship,date_birth,
                                  flag_gender,education,occupation,name_employer,address,entry_date,entry_user
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