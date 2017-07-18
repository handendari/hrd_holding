using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class hrdRecruitmentMemberRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdRecruitmentMemberRepo");

        public ResponseModel InsertRecruitmentMember(hrdRecruitmentMemberModel pModel)
        {
            var objHasil = new ResponseModel();
            var vStatus = 0;
            var vNo = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    string SqlString = @"INSERT INTO hrd_recruitment_membership
                                                (recruitment_id,seq_no,name,year_from,year_to,level,entry_date,entry_user)
                                         VALUES (@precruitment_id,@pseq_no,@pname,@pyear_from,@pyear_to,@plevel,@pentry_date,@pentry_user)";

                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@preq_id", pModel.req_id);
                        cmd.Parameters.AddWithValue("@precruitment_id", pModel.recruitment_id);
                        cmd.Parameters.AddWithValue("@pseq_no", pModel.seq_no);
                        cmd.Parameters.AddWithValue("@pname", pModel.name);
                        cmd.Parameters.AddWithValue("@pyear_from", pModel.year_from);
                        cmd.Parameters.AddWithValue("@pyear_to", pModel.year_to);
                        cmd.Parameters.AddWithValue("@plevel", pModel.level);
                        cmd.Parameters.AddWithValue("@pentry_date", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pentry_user", pModel.entry_user);

                        vStatus = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT RECRUITMENT MEMBER SUCCESS....<br/> REQUEST ID : " + pModel.req_id + " Name : " + pModel.name);

                        objHasil.isValid = Convert.ToBoolean(vStatus);
                        objHasil.message = " INSERT RECRUITMENT MEMBER SUCCESS ====>>>> REQUEST ID : " + pModel.req_id + " Name : " + pModel.name;
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                objHasil.message = " INSERT RECRUITMENT MEMBER FAILED, REQUEST ID : " + pModel.req_id + " Name : " + pModel.name;

                Log.Error(DateTime.Now + " INSERT RECRUITMENT MEMBER FAILED", ex);
            }

            return objHasil;
        }

        public List<hrdRecruitmentMemberModel> getRecruitmentMemberList(int pRecruitmentId)
        {
            //Log.Debug(DateTime.Now + "=======>>>> MASUK REPO EMPLOYEE LIST, Emp Code : " + pEmployeeCode);

            var vList = new List<hrdRecruitmentMemberModel>();
            var strSQL = @"SELECT req_id,recruitment_id,seq_no,name,year_from,year_to,level,entry_date,entry_user
                           FROM hrd_recruitment_membership
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

                                    //Log.Debug(DateTime.Now + "=======>>>> MASUK LOOPING HASIL QUERY EMP FAMILY KE : " + i++);

                                    var m = new hrdRecruitmentMemberModel
                                    {
                                        req_id = aa.GetInt16("req_id"),
                                        recruitment_id = aa.GetInt16("recruitment_id"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        name = aa.GetString("name"),
                                        year_from = (aa["year_from"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["year_from"]),
                                        year_to = (aa["year_to"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["year_to"]),
                                        level = aa.GetString("level"),
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
                Log.Error(DateTime.Now + " getRecruitmentMemberList FAILED... ", ex);
            }

            //Log.Debug(DateTime.Now + "=======>>>> Jml DATA getRecruitmentEdu LIST : " + vList.Count);
            return vList;
        }

        public hrdRecruitmentMemberModel getRecruitmentMemberInfo(int pId)
        {
            var vModel = new hrdRecruitmentMemberModel();
            var strSQL = @"SELECT req_id,recruitment_id,seq_no,name,year_from,year_to,level,entry_date,entry_user
                           FROM hrd_recruitment_membership
                           WHERE req_id = @pReqId";
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
                                    vModel.year_from = (aa["year_from"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["year_from"]);
                                    vModel.year_to = (aa["year_to"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["year_to"]);
                                    vModel.level = aa.GetString("level");
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
                Log.Error(DateTime.Now + " getRecruitmentMemberInfo Failed", ex);
            }
            return vModel;
        }

        public ResponseModel updateRecruitmentMember(hrdRecruitmentMemberModel pModel)
        {

            //Log.Debug(DateTime.Now + " UPDATE EMPLOYEE FAMILY ===>>> Code : " + pModel.employee_code + 
            //    " Name : " + pModel.employee_name + " Model USER : " + pModel.edit_user + 
            //    " Model Edit Date : " + pModel.edit_date);

            int vStatus = 0;
            var objHasil = new ResponseModel();

            string SqlString = @"UPDATE hrd_recruitment_membership
                                    SET name = @pname,
                                        year_from = @pyear_from,
                                        year_to = @pyear_to,
                                        level = @plevel
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
                        cmd.Parameters.AddWithValue("@pyear_from", pModel.year_from);
                        cmd.Parameters.AddWithValue("@pyear_to", pModel.year_to);
                        cmd.Parameters.AddWithValue("@plevel", pModel.level);

                        vStatus = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE Recruitment MEMBER SUCCESS....<br/> Code : " + pModel.req_id + " Name : " + pModel.name);

                        objHasil.isValid = Convert.ToBoolean(vStatus);
                        objHasil.message = " UPDATE Recruitment MEMBER SUCCESS, Code : " + pModel.req_id + " Name : " + pModel.name;
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                objHasil.message = " UPDATE RECRUITMENT MEMBER FAILED.....<br/> Code : " + pModel.req_id + " Name : " + pModel.name;

                Log.Error(DateTime.Now + " UPDATE RECRUITMENT MEMBER FAILED, Code : " + pModel.req_id + " Name : " + pModel.name, ex);
            }

            return objHasil;
        }

        public ResponseModel DeleteRecruitmentMember(int pReqId)
        {
            var objHasil = new ResponseModel();

            var SqlString = @"DELETE FROM hrd_recruitment_membership WHERE req_id = @pReqId";

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
                        Log.Debug(DateTime.Now + " DELETE RECRUITMENT MEMBER SUCCESS.....<br/> Req Id : " + pReqId);

                        objHasil.isValid = Convert.ToBoolean(status);
                        Log.Debug(DateTime.Now + " DELETE RECRUITMENT MEMBER SUCCESS.....<br/> Req Id : " + pReqId);
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                Log.Debug(DateTime.Now + " DELETE RECRUITMENT MEMBER FAILED.....<br/> Req Id : " + pReqId);

                Log.Error(DateTime.Now + " DELETE RECRUITMENT MEMBER FAILED", ex);
            }

            return objHasil;
        }

        public int getRecruitmentMemberSeqNo(int pRecruitmentId)
        {
            //Log.Debug(DateTime.Now + "=======>>>> MASUK REPO EMPLOYEE LIST, Emp Code : " + pEmployeeCode);

            var vNo = 0;
            var strSQL = @"SELECT max(mef.seq_no) seq_no
                           FROM hrd_recruitment_membership
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
                Log.Error(DateTime.Now + " GetRecruitmentMemberSeqNo FAILED... ", ex);
            }

            return vNo;
        }
    }
}