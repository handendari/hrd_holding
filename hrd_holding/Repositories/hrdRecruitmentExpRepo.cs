using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class hrdRecruitmentExpRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdRecruitmentExpRepo");

        public ResponseModel InsertRecruitmentExp(hrdRecruitmentExpModel pModel)
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
                                                (recruitment_id,seq_no,name_employer,business,start_date,end_date,
                                                 position_held,last_salary,reason_leave,entry_date,entry_user)
                                        VALUES (@precruitment_id,@pseq_no,@pname_employer,@pbusiness,@pstart_date,@pend_date,
                                                 @pposition_held,@plast_salary,@preason_leave,@pentry_date,@pentry_user)";

                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@precruitment_id", pModel.recruitment_id);
                        cmd.Parameters.AddWithValue("@pseq_no", pModel.seq_no);
                        cmd.Parameters.AddWithValue("@pname_employer", pModel.name_employer);
                        cmd.Parameters.AddWithValue("@pbusiness", pModel.business);
                        cmd.Parameters.AddWithValue("@pstart_date", pModel.start_date);
                        cmd.Parameters.AddWithValue("@pend_date", pModel.end_date);
                        cmd.Parameters.AddWithValue("@pposition_held", pModel.position_held);
                        cmd.Parameters.AddWithValue("@plast_salary", pModel.last_salary);
                        cmd.Parameters.AddWithValue("@preason_leave", pModel.reason_leave);
                        cmd.Parameters.AddWithValue("@pentry_date", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pentry_user", pModel.entry_user);

                        vStatus = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT RECRUITMENT EXPERIENCE SUCCESS....<br/> REQUEST ID : " + pModel.req_id + " Name : " + pModel.name_employer);

                        objHasil.isValid = Convert.ToBoolean(vStatus);
                        objHasil.message = " INSERT RECRUITMENT EXPERIENCE SUCCESS ====>>>> REQUEST ID : " + pModel.req_id + " Name : " + pModel.name_employer;
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                objHasil.message = " INSERT RECRUITMENT EXPERIENCE FAILED, REQUEST ID : " + pModel.req_id + " Name : " + pModel.name_employer;

                Log.Error(DateTime.Now + " INSERT RECRUITMENT EXPERIENCE FAILED", ex);
            }

            return objHasil;
        }

        public List<hrdRecruitmentExpModel> getRecruitmentExpList(int pRecruitmentId)
        {
            //Log.Debug(DateTime.Now + "=======>>>> MASUK REPO EMPLOYEE LIST, Emp Code : " + pEmployeeCode);

            var vList = new List<hrdRecruitmentExpModel>();
            var strSQL = @"SELECT req_id,recruitment_id,seq_no,name_employer,business,start_date,end_date,
                                  position_held,last_salary,reason_leave,entry_date,entry_user
                           FROM hrd_recruitment_exp  hre
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

                                    var m = new hrdRecruitmentExpModel
                                    {
                                        req_id = aa.GetInt16("req_id"),
                                        recruitment_id = aa.GetInt16("recruitment_id"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        name_employer = aa.GetString("name_employer"),
                                        business = aa.GetString("business"),
                                        start_date = (aa["start_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["start_date"]),
                                        end_date = (aa["end_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["end_date"]),
                                        position_held = aa.GetString("position_held"),
                                        last_salary = aa.GetDecimal("last_salary"),
                                        reason_leave = aa.GetString("reason_leave"),
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
                Log.Error(DateTime.Now + " getRecruitmentExpList FAILED... ", ex);
            }

            //Log.Debug(DateTime.Now + "=======>>>> Jml DATA getRecruitmentEdu LIST : " + vList.Count);
            return vList;
        }

        public hrdRecruitmentExpModel getRecruitmentExpInfo(int pId)
        {
            var vModel = new hrdRecruitmentExpModel();
            var strSQL = @"SELECT req_id,recruitment_id,seq_no,name_employer,business,start_date,end_date,
                                  position_held,last_salary,reason_leave,entry_date,entry_user
                           FROM hrd_recruitment_exp hre
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
                                    vModel.name_employer = aa.GetString("name_employer");
                                    vModel.business = aa.GetString("business");
                                    vModel.start_date = (aa["start_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["start_date"]);
                                    vModel.end_date = (aa["end_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["end_date"]);
                                    vModel.position_held = aa.GetString("position_held");
                                    vModel.last_salary = aa.GetDecimal("last_salary");
                                    vModel.reason_leave = aa.GetString("reason_leave");
                                    vModel.entry_date = (aa["entry_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["entry_date"]);
                                    vModel.entry_user = aa.GetString("entry_user");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " getRecruitmentExpInfo Failed", ex);
            }
            return vModel;
        }

        public ResponseModel updateRecruitmentExp(hrdRecruitmentExpModel pModel)
        {

            //Log.Debug(DateTime.Now + " UPDATE EMPLOYEE FAMILY ===>>> Code : " + pModel.employee_code + 
            //    " Name : " + pModel.employee_name + " Model USER : " + pModel.edit_user + 
            //    " Model Edit Date : " + pModel.edit_date);

            int vStatus = 0;
            var objHasil = new ResponseModel();

            string SqlString = @"UPDATE hrd_recruitment_exp
                                    SET name_employer = @pname_employer,
                                        business = @pbusiness,
                                        start_date = @pstart_date,
                                        end_date = @pend_date,
                                        position_held = @pposition_held,
                                        last_salary = @plast_salary,
                                        reason_leave = @preason_leave
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
                        cmd.Parameters.AddWithValue("@pname_employer", pModel.name_employer);
                        cmd.Parameters.AddWithValue("@pbusiness", pModel.business);
                        cmd.Parameters.AddWithValue("@pstart_date", pModel.start_date);
                        cmd.Parameters.AddWithValue("@pend_date", pModel.end_date);
                        cmd.Parameters.AddWithValue("@pposition_held", pModel.position_held);
                        cmd.Parameters.AddWithValue("@plast_salary", pModel.last_salary);
                        cmd.Parameters.AddWithValue("@preason_leave", pModel.reason_leave);

                        vStatus = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE Recruitment EXPERIENCE SUCCESS....<br/> Code : " + pModel.req_id + " Name : " + pModel.name_employer);

                        objHasil.isValid = Convert.ToBoolean(vStatus);
                        objHasil.message = " UPDATE Recruitment EXPERIENCE SUCCESS, Code : " + pModel.req_id + " Name : " + pModel.name_employer;
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                objHasil.message = " UPDATE RECRUITMENT EXPERIENCE FAILED.....<br/> Code : " + pModel.req_id + " Name : " + pModel.name_employer;

                Log.Error(DateTime.Now + " UPDATE RECRUITMENT EXPERIENCE FAILED, Code : " + pModel.req_id + " Name : " + pModel.name_employer, ex);
            }

            return objHasil;
        }

        public ResponseModel DeleteRecruitmentExp(int pReqId)
        {
            var objHasil = new ResponseModel();

            var SqlString = @"DELETE FROM hrd_recruitment_exp WHERE req_id = @pReqId";

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
                        Log.Debug(DateTime.Now + " DELETE RECRUITMENT EXPERIENCE SUCCESS.....<br/> Req Id : " + pReqId);

                        objHasil.isValid = Convert.ToBoolean(status);
                        Log.Debug(DateTime.Now + " DELETE RECRUITMENT EXPERIENCE SUCCESS.....<br/> Req Id : " + pReqId);
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                Log.Debug(DateTime.Now + " DELETE RECRUITMENT EXPERIENCE FAILED.....<br/> Req Id : " + pReqId);

                Log.Error(DateTime.Now + " DELETE RECRUITMENT EXPERIENCE FAILED", ex);
            }

            return objHasil;
        }

        public int getRecruitmentExpSeqNo(string pRecruitmentId)
        {
            //Log.Debug(DateTime.Now + "=======>>>> MASUK REPO EMPLOYEE LIST, Emp Code : " + pEmployeeCode);

            var vNo = 0;
            var strSQL = @"SELECT max(mef.seq_no) seq_no
                           FROM hrd_recruitment_exp
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
                Log.Error(DateTime.Now + " GetRecruitmentExpSeqNo FAILED... ", ex);
            }

            return vNo;
        }
    }
}