using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class hrdRecruitmentEduRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdRecruitmentEduRepo");

        public ResponseModel InsertRecruitmentEdu(hrdRecruitmentEduModel pModel)
        {
            var objHasil = new ResponseModel();
            var vStatus = 0;
            var vNo = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    string SqlString = @"INSERT INTO hrd_recruitment_edu
                                                (recruitment_id,seq_no,start_year,end_year,flag_achieved,name_achieved,
                                                 school,entry_date,entry_user)
                                        VALUES (@precruitment_id,@pseq_no,@pstart_year,@pend_year,@pflag_achieved,@pname_achieved,
                                                @pschool,@pentry_date,@pentry_user)";

                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@precruitment_id", pModel.recruitment_id);
                        cmd.Parameters.AddWithValue("@pseq_no", pModel.seq_no);
                        cmd.Parameters.AddWithValue("@pstart_year", pModel.start_year);
                        cmd.Parameters.AddWithValue("@pend_year", pModel.end_year);
                        cmd.Parameters.AddWithValue("@pflag_achieved", pModel.flag_achieved);
                        cmd.Parameters.AddWithValue("@pname_achieved", pModel.name_achieved);
                        cmd.Parameters.AddWithValue("@pschool", pModel.school);
                        cmd.Parameters.AddWithValue("@pentry_date", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pentry_user", pModel.entry_user);

                        vStatus = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT RECRUITMENT EDU SUCCESS....<br/> REQUEST ID : " + pModel.req_id + " Name : " + pModel.name_achieved);

                        objHasil.isValid = Convert.ToBoolean(vStatus);
                        objHasil.message = " INSERT RECRUITMENT EDU SUCCESS ====>>>> REQUEST ID : " + pModel.req_id + " Name : " + pModel.name_achieved;
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                objHasil.message = " INSERT RECRUITMENT EDU FAILED, REQUEST ID : " + pModel.req_id + " Name : " + pModel.name_achieved;

                Log.Error(DateTime.Now + " INSERT RECRUITMENT EDU FAILED", ex);
            }

            return objHasil;
        }

        public List<hrdRecruitmentEduModel> getRecruitmentEduList(int pRecruitmentId)
        {
            //Log.Debug(DateTime.Now + "=======>>>> MASUK REPO EMPLOYEE LIST, Emp Code : " + pEmployeeCode);

            var vList = new List<hrdRecruitmentEduModel>();
            var strSQL = @"SELECT req_id,recruitment_id,seq_no,start_year,end_year,flag_achieved,name_achieved,
                                  school,entry_date,entry_user
                           FROM hrd_recruitment_edu  hre
                           WHERE hre.recruitment_id = @pRecruitmentId";
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

                                    //Log.Debug(DateTime.Now + "=======>>>> MASUK LOOPING HASIL QUERY EMP FAMILY KE : " + i++);

                                    var m = new hrdRecruitmentEduModel
                                    {
                                        req_id = aa.GetInt16("req_id"),
                                        recruitment_id = aa.GetInt16("recruitment_id"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        start_year = (aa["start_year"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["start_year"]),
                                        end_year = (aa["end_year"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["end_year"]),
                                        flag_achieved = aa.GetBoolean("flag_achieved"),
                                        name_achieved = aa.GetString("name_achieved"),
                                        school = aa.GetString("school"),
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
                Log.Error(DateTime.Now + " getRecruitmentEduList FAILED... ", ex);
            }

            //Log.Debug(DateTime.Now + "=======>>>> Jml DATA getRecruitmentEdu LIST : " + vList.Count);
            return vList;
        }

        public hrdRecruitmentEduModel getRecruitmentEduInfo(int pId)
        {
            var vModel = new hrdRecruitmentEduModel();
            var strSQL = @"SELECT req_id,recruitment_id,seq_no,start_year,end_year,flag_achieved,name_achieved,
                                  school,entry_date,entry_user
                           FROM hrd_recruitment_edu hre
                           WHERE hre.req_id = @pReqId";
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
                                    vModel.start_year = (aa["start_year"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["start_year"]);
                                    vModel.end_year = (aa["end_year"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["end_year"]);
                                    vModel.flag_achieved = aa.GetBoolean("flag_achieved");
                                    vModel.name_achieved = aa.GetString("name_achieved");
                                    vModel.school = aa.GetString("school");
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
                Log.Error(DateTime.Now + " getRecruitmentEduInfo Failed", ex);
            }
            return vModel;
        }

        public ResponseModel updateRecruitmentEdu(hrdRecruitmentEduModel pModel)
        {

            //Log.Debug(DateTime.Now + " UPDATE EMPLOYEE FAMILY ===>>> Code : " + pModel.employee_code + 
            //    " Name : " + pModel.employee_name + " Model USER : " + pModel.edit_user + 
            //    " Model Edit Date : " + pModel.edit_date);

            int vStatus = 0;
            var objHasil = new ResponseModel();

            string SqlString = @"UPDATE hrd_recruitment_edu
                                    SET start_year = @pstart_year,
                                        end_year = @pend_year,
                                        flag_achieved = @pflag_achieved,
                                        name_achieved = @pname_achieved,
                                        school = @pschool
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
                        cmd.Parameters.AddWithValue("@pstart_year", pModel.start_year);
                        cmd.Parameters.AddWithValue("@pend_year", pModel.end_year);
                        cmd.Parameters.AddWithValue("@pflag_achieved", pModel.flag_achieved);
                        cmd.Parameters.AddWithValue("@pname_achieved", pModel.name_achieved);
                        cmd.Parameters.AddWithValue("@pschool", pModel.school);

                        vStatus = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE Recruitment EDUCATION SUCCESS....<br/> Code : " + pModel.req_id + " Name : " + pModel.name_achieved);

                        objHasil.isValid = Convert.ToBoolean(vStatus);
                        objHasil.message = " UPDATE Recruitment EDUCATION SUCCESS, Code : " + pModel.req_id + " Name : " + pModel.name_achieved;
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                objHasil.message = " UPDATE RECRUITMENT EDUCATION FAILED.....<br/> Code : " + pModel.req_id + " Name : " + pModel.name_achieved;

                Log.Error(DateTime.Now + " UPDATE RECRUITMENT EDUCATION FAILED, Code : " + pModel.req_id + " Name : " + pModel.name_achieved, ex);
            }

            return objHasil;
        }

        public ResponseModel DeleteRecruitmentEdu(int pReqId)
        {
            var objHasil = new ResponseModel();

            var SqlString = @"DELETE FROM hrd_recruitment_edu WHERE req_id = @pReqId";

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
                        Log.Debug(DateTime.Now + " DELETE RECRUITMENT EDUCATION SUCCESS.....<br/> Req Id : " + pReqId);

                        objHasil.isValid = Convert.ToBoolean(status);
                        Log.Debug(DateTime.Now + " DELETE RECRUITMENT EDUCATION SUCCESS.....<br/> Req Id : " + pReqId);
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                Log.Debug(DateTime.Now + " DELETE RECRUITMENT EDUCATION FAILED.....<br/> Req Id : " + pReqId);

                Log.Error(DateTime.Now + " DELETE RECRUITMENT EDUCATION FAILED", ex);
            }

            return objHasil;
        }

        public int getRecruitmentEduSeqNo(int pRecruitmentId)
        {
            //Log.Debug(DateTime.Now + "=======>>>> MASUK REPO EMPLOYEE LIST, Emp Code : " + pEmployeeCode);

            var vNo = 0;
            var strSQL = @"SELECT max(mef.seq_no) seq_no
                           FROM hrd_recruitment_edu
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