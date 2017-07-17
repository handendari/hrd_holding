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
                                                (recruitment_id,seq_no,name,flag_relationship,name_relationship,date_birth,
                                                 flag_gender,education,occupation,name_employer,address,entry_date,entry_user)
                                         VALUES (@precruitment_id,@pseq_no,@pname,@pflag_relationship,@pname_relationship,@pdate_birth,
                                                 @pflag_gender,@peducation,@poccupation,@pname_employer,@paddress,@pentry_date,@pentry_user)";

                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

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

        public List<hrdRecruitmentFamModel> getRecruitmentFamilyList(int pRecruitmentId)
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
                                    {
                                        req_id = aa.GetInt16("req_id"),
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

        public hrdRecruitmentFamModel getRecruitmentFamilyInfo(int pId)
        {
            var vModel = new hrdRecruitmentFamModel();
            var strSQL = @"SELECT req_id,recruitment_id,seq_no,name,flag_relationship,name_relationship,date_birth,
                                  flag_gender,education,occupation,name_employer,address,entry_date,entry_user
                           FROM hrd_recruitment_fams hrf 
                           WHERE hrf.req_id = @pReqId";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pReqId", pId);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.req_id = aa.GetInt16("req_id");
                                    vModel.recruitment_id = aa.GetInt16("recruitment_id");
                                    vModel.seq_no = aa.GetInt16("seq_no");
                                    vModel.name = aa.GetString("name");
                                    vModel.flag_relationship = aa.GetBoolean("flag_relationship");
                                    vModel.name_relationship = aa.GetString("name_relationship");
                                    vModel.date_birth = aa.GetDateTime("date_birth");
                                    vModel.flag_gender = aa.GetBoolean("flag_gender");
                                    vModel.education = aa.GetString("education");
                                    vModel.occupation = aa.GetString("occupation");
                                    vModel.name_employer = aa.GetString("name_employer");
                                    vModel.address = aa.GetString("address");
                                    vModel.entry_date = aa.GetDateTime("entry_date");
                                    vModel.entry_user = aa.GetString("entry_user");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " getRecruitmentFamilyInfo Failed", ex);
            }
            return vModel;
        }

        public ResponseModel updateRecruitmentFamily(hrdRecruitmentFamModel pModel)
        {

            //Log.Debug(DateTime.Now + " UPDATE EMPLOYEE FAMILY ===>>> Code : " + pModel.employee_code + 
            //    " Name : " + pModel.employee_name + " Model USER : " + pModel.edit_user + 
            //    " Model Edit Date : " + pModel.edit_date);

            int vStatus = 0;
            var objHasil = new ResponseModel();

            string SqlString = @"UPDATE hrd_recruitment_fams
                                    SET name = @pname,
                                        flag_relationship = @pflag_relationship,
                                        name_relationship = @pname_relationship,
                                        date_birth = @pdate_birth,
                                        flag_gender = @pflag_gender,
                                        education = @peducation,
                                        occupation = @occupation,
                                        name_employer = @pname_employer,
                                        address = @paddress
                                WHERE req_id = @pReqId ";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pReqId", pModel.req_id);
                        cmd.Parameters.AddWithValue("@pname", pModel.name);
                        cmd.Parameters.AddWithValue("@pflag_relationship", pModel.flag_relationship);
                        cmd.Parameters.AddWithValue("@pname_relationship", pModel.name_relationship);
                        cmd.Parameters.AddWithValue("@pdate_birth", pModel.date_birth);
                        cmd.Parameters.AddWithValue("@pflag_gender", pModel.flag_gender);
                        cmd.Parameters.AddWithValue("@peducation", pModel.education);
                        cmd.Parameters.AddWithValue("@occupation", pModel.occupation);
                        cmd.Parameters.AddWithValue("@pname_employer", pModel.name_employer);
                        cmd.Parameters.AddWithValue("@paddress", pModel.address);

                        vStatus = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE Recruitment FAMILY SUCCESS....<br/> Code : " + pModel.req_id + " Name : " + pModel.name);

                        objHasil.isValid = Convert.ToBoolean(vStatus);
                        objHasil.message = " UPDATE Recruitment FAMILY SUCCESS, Code : " + pModel.req_id + " Name : " + pModel.name;
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                objHasil.message = " UPDATE RECRUITMENT FAMILY FAILED.....<br/> Code : " + pModel.req_id + " Name : " + pModel.name;

                Log.Error(DateTime.Now + " UPDATE RECRUITMENT FAMILY FAILED, Code : " + pModel.req_id + " Name : " + pModel.name, ex);
            }

            return objHasil;
        }

        public ResponseModel DeleteRecruitmentFamily(int pReqId)
        {
            var objHasil = new ResponseModel();

            var SqlString = @"DELETE FROM hrd_recruitment_fams WHERE req_id = @pReqId";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pReqId", pReqId);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " DELETE RECRUITMENT FAMILY SUCCESS.....<br/> Req Id : " + pReqId);

                        objHasil.isValid = Convert.ToBoolean(status);
                        Log.Debug(DateTime.Now + " DELETE RECRUITMENT FAMILY SUCCESS.....<br/> Req Id : " + pReqId);
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                Log.Debug(DateTime.Now + " DELETE RECRUITMENT FAMILY FAILED.....<br/> Req Id : " + pReqId);

                Log.Error(DateTime.Now + " DELETE RECRUITMENT FAMILY FAILED", ex);
            }

            return objHasil;
        }

        public int getRecruitmentFamilySeqNo(string pRecruitmentId)
        {
            //Log.Debug(DateTime.Now + "=======>>>> MASUK REPO EMPLOYEE LIST, Emp Code : " + pEmployeeCode);

            var vNo = 0;
            var strSQL = @"SELECT max(mef.seq_no) seq_no
                           FROM hrd_recruitment_fams
                           WHERE recruitment_id = @pRecruitmentId";
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
                                while (aa.Read())
                                {
                                    vNo = aa.GetInt32("seq_no");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetRecruitmentEduSeqNo FAILED... ", ex);
            }

            return vNo;
        }
    }
}