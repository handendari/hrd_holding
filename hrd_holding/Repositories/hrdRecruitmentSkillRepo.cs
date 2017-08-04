using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class hrdRecruitmentSkillRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdRecruitmentSkillRepo");

        public ResponseModel InsertRecruitmentSkill(hrdRecruitmentSkillModel pModel)
        {
            var objHasil = new ResponseModel();
            var vStatus = 0;
            var vNo = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    string SqlString = @"INSERT INTO hrd_recruitment_skill
                                                (request_id,recruitment_id,seq_no,skill,flag_level,name_level,
                                                 description,entry_date,entry_user)
                                         VALUES (@prequest_id,@precruitment_id,@pseq_no,@pskill,@pflag_level,@pname_level,
                                                 @pdescription,@pentry_date,@pentry_user)";

                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@prequest_id", pModel.request_id);
                        cmd.Parameters.AddWithValue("@precruitment_id", pModel.recruitment_id);
                        cmd.Parameters.AddWithValue("@pseq_no", pModel.seq_no);
                        cmd.Parameters.AddWithValue("@pskill", pModel.skill);
                        cmd.Parameters.AddWithValue("@pflag_level", pModel.flag_level);
                        cmd.Parameters.AddWithValue("@pname_level", pModel.name_level);
                        cmd.Parameters.AddWithValue("@pdescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pentry_date", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pentry_user", pModel.entry_user);

                        vStatus = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT RECRUITMENT SKILL SUCCESS....<br/> REQUEST ID : " + pModel.request_id + " Name : " + pModel.skill);

                        objHasil.isValid = Convert.ToBoolean(vStatus);
                        objHasil.message = " INSERT RECRUITMENT SKILL SUCCESS ====>>>> REQUEST ID : " + pModel.request_id + " Name : " + pModel.skill;
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                objHasil.message = " INSERT RECRUITMENT SKILL FAILED, REQUEST ID : " + pModel.request_id + " Name : " + pModel.skill;

                Log.Error(DateTime.Now + " INSERT RECRUITMENT SKILL FAILED", ex);
            }

            return objHasil;
        }

        public List<hrdRecruitmentSkillModel> getRecruitmentSkillList(int pRequestId)
        {
            //Log.Debug(DateTime.Now + "=======>>>> MASUK REPO EMPLOYEE LIST, Emp Code : " + pEmployeeCode);

            var vList = new List<hrdRecruitmentSkillModel>();
            var strSQL = @"SELECT id,request_id,recruitment_id,seq_no,skill,flag_level,name_level,description,entry_date,entry_user
                           FROM hrd_recruitment_skill  hre
                           WHERE hre.request_id = @pRequestId";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pRequestId", pRequestId);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {

                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {

                                    //Log.Debug(DateTime.Now + "=======>>>> MASUK LOOPING HASIL QUERY EMP FAMILY KE : " + i++);

                                    var m = new hrdRecruitmentSkillModel
                                    {
                                        id = aa.GetInt16("id"),
                                        request_id = aa.GetInt16("request_id"),
                                        recruitment_id = aa.GetInt16("recruitment_id"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        skill = aa.GetString("skill"),
                                        flag_level = aa.GetInt16("flag_level"),
                                        name_level = aa.GetString("name_level"),
                                        description = aa.GetString("description"),
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
                Log.Error(DateTime.Now + " getRecruitmentSkillList FAILED... ", ex);
            }

            //Log.Debug(DateTime.Now + "=======>>>> Jml DATA getRecruitmentEdu LIST : " + vList.Count);
            return vList;
        }

        public hrdRecruitmentSkillModel getRecruitmentSkillInfo(int pId)
        {
            var vModel = new hrdRecruitmentSkillModel();
            var strSQL = @"SELECT id,request_id,recruitment_id,seq_no,skill,flag_level,name_level,description,entry_date,entry_user
                           FROM hrd_recruitment_skill hre
                           WHERE hre.id = @pSkillId";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pSkillId", pId);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.id = aa.GetInt16("id");
                                    vModel.request_id = aa.GetInt16("req_id");
                                    vModel.recruitment_id = aa.GetInt16("recruitment_id");
                                    vModel.seq_no = aa.GetInt16("seq_no");
                                    vModel.skill = aa.GetString("entry_user");
                                    vModel.flag_level = aa.GetInt16("flag_level");
                                    vModel.name_level = aa.GetString("name_level");
                                    vModel.description = aa.GetString("description");
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
                Log.Error(DateTime.Now + " getRecruitmentSkillInfo Failed", ex);
            }
            return vModel;
        }

        public ResponseModel updateRecruitmentSkill(hrdRecruitmentSkillModel pModel)
        {

            //Log.Debug(DateTime.Now + " UPDATE EMPLOYEE FAMILY ===>>> Code : " + pModel.employee_code + 
            //    " Name : " + pModel.employee_name + " Model USER : " + pModel.edit_user + 
            //    " Model Edit Date : " + pModel.edit_date);

            int vStatus = 0;
            var objHasil = new ResponseModel();

            string SqlString = @"UPDATE hrd_recruitment_skill
                                    SET request_id = @prequest_id,
                                        recruitment_id = @precruitment_id,
                                        skill = @pskill,
                                        flag_level = @pflag_level,
                                        name_level = @pname_level,
                                        description = @pdescription
                                WHERE id = @pId ";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pId", pModel.id);
                        cmd.Parameters.AddWithValue("@prequest_Id", pModel.request_id);
                        cmd.Parameters.AddWithValue("@precruitment_id", pModel.recruitment_id);
                        cmd.Parameters.AddWithValue("@pskill", pModel.skill);
                        cmd.Parameters.AddWithValue("@pflag_level", pModel.flag_level);
                        cmd.Parameters.AddWithValue("@pname_level", pModel.name_level);
                        cmd.Parameters.AddWithValue("@pdescription", pModel.description);

                        vStatus = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE Recruitment SKILL SUCCESS....<br/> Code : " + pModel.request_id + " Name : " + pModel.skill);

                        objHasil.isValid = Convert.ToBoolean(vStatus);
                        objHasil.message = " UPDATE Recruitment SKILL SUCCESS, Code : " + pModel.request_id + " Name : " + pModel.skill;
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                objHasil.message = " UPDATE RECRUITMENT SKILL FAILED.....<br/> Code : " + pModel.request_id + " Name : " + pModel.skill;

                Log.Error(DateTime.Now + " UPDATE RECRUITMENT SKILL FAILED, Code : " + pModel.request_id + " Name : " + pModel.skill, ex);
            }

            return objHasil;
        }

        public ResponseModel DeleteRecruitmentSkill(int pId)
        {
            var objHasil = new ResponseModel();

            var SqlString = @"DELETE FROM hrd_recruitment_skill WHERE id = @pId";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pId", pId);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " DELETE RECRUITMENT SKILL SUCCESS.....<br/> Req Id : " + pId);

                        objHasil.isValid = Convert.ToBoolean(status);
                        Log.Debug(DateTime.Now + " DELETE RECRUITMENT SKILL SUCCESS.....<br/> Req Id : " + pId);
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                Log.Debug(DateTime.Now + " DELETE RECRUITMENT SKILL FAILED.....<br/> Req Id : " + pId);

                Log.Error(DateTime.Now + " DELETE RECRUITMENT SKILL FAILED", ex);
            }

            return objHasil;
        }

        public int getRecruitmentSkillSeqNo(int pRequestId)
        {
            //Log.Debug(DateTime.Now + "=======>>>> MASUK REPO EMPLOYEE LIST, Emp Code : " + pEmployeeCode);

            var vNo = 0;
            var strSQL = @"SELECT max(seq_no) seq_no
                           FROM hrd_recruitment_skill
                           WHERE request_id = @pRequestId";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pRequestId", pRequestId);

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
                Log.Error(DateTime.Now + " GetRecruitmentSkillSeqNo FAILED... ", ex);
            }

            return vNo;
        }
    }
}