using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class hrdRecruitmentRefRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdRecruitmentRefRepo");

        public ResponseModel InsertRecruitmentRef(hrdRecruitmentRefModel pModel)
        {
            var objHasil = new ResponseModel();
            var vStatus = 0;
            var vNo = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    string SqlString = @"INSERT INTO hrd_recruitment_reference
                                                (recruitment_id,seq_no,name,address,phone_number,occupation,year_known,entry_date,entry_user)
                                         VALUES(@precruitment_id,@pseq_no,@pname,@paddress,@pphone_number,@poccupation,@pyear_known,@pentry_date,@pentry_user)";

                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@precruitment_id", pModel.recruitment_id);
                        cmd.Parameters.AddWithValue("@pseq_no", pModel.seq_no);
                        cmd.Parameters.AddWithValue("@pname", pModel.name);
                        cmd.Parameters.AddWithValue("@paddress", pModel.address);
                        cmd.Parameters.AddWithValue("@pphone_number", pModel.phone_number);
                        cmd.Parameters.AddWithValue("@poccupation", pModel.occupation);
                        cmd.Parameters.AddWithValue("@pyear_known", pModel.year_known);
                        cmd.Parameters.AddWithValue("@pentry_date", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pentry_user", pModel.entry_user);

                        vStatus = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT RECRUITMENT REFERENCE SUCCESS....<br/> REQUEST ID : " + pModel.req_id + " Name : " + pModel.name);

                        objHasil.isValid = Convert.ToBoolean(vStatus);
                        objHasil.message = " INSERT RECRUITMENT REFERENCE SUCCESS ====>>>> REQUEST ID : " + pModel.req_id + " Name : " + pModel.name;
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                objHasil.message = " INSERT RECRUITMENT REFERENCE FAILED, REQUEST ID : " + pModel.req_id + " Name : " + pModel.name;

                Log.Error(DateTime.Now + " INSERT RECRUITMENT REFERENCE FAILED", ex);
            }

            return objHasil;
        }

        public List<hrdRecruitmentRefModel> getRecruitmentRefList(int pRecruitmentId)
        {
            //Log.Debug(DateTime.Now + "=======>>>> MASUK REPO EMPLOYEE LIST, Emp Code : " + pEmployeeCode);

            var vList = new List<hrdRecruitmentRefModel>();
            var strSQL = @"SELECT req_id,recruitment_id,seq_no,name,address,phone_number,occupation,year_known,entry_date,entry_user
                           FROM hrd_recruitment_reference
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

                                    var m = new hrdRecruitmentRefModel
                                    {
                                        req_id = aa.GetInt16("req_id"),
                                        recruitment_id = aa.GetInt16("recruitment_id"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        name = aa.GetString("school"),
                                        address = aa.GetString("school"),
                                        phone_number = aa.GetString("school"),
                                        occupation = aa.GetString("school"),
                                        year_known = (aa["year_known"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["year_known"]),
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
                Log.Error(DateTime.Now + " getRecruitmentRefList FAILED... ", ex);
            }

            //Log.Debug(DateTime.Now + "=======>>>> Jml DATA getRecruitmentEdu LIST : " + vList.Count);
            return vList;
        }

        public hrdRecruitmentRefModel getRecruitmentRefInfo(int pId)
        {
            var vModel = new hrdRecruitmentRefModel();

            var strSQL = @"SELECT req_id,recruitment_id,seq_no,name,address,phone_number,occupation,year_known,entry_date,entry_user
                           FROM hrd_recruitment_reference
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
                                    vModel.name = aa.GetString("school");
                                    vModel.address = aa.GetString("school");
                                    vModel.phone_number = aa.GetString("school");
                                    vModel.occupation = aa.GetString("school");
                                    vModel.year_known = (aa["year_known"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["year_known"]);
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
                Log.Error(DateTime.Now + " getRecruitmentRefInfo Failed", ex);
            }
            return vModel;
        }

        public ResponseModel updateRecruitmentRef(hrdRecruitmentRefModel pModel)
        {

            //Log.Debug(DateTime.Now + " UPDATE EMPLOYEE FAMILY ===>>> Code : " + pModel.employee_code + 
            //    " Name : " + pModel.employee_name + " Model USER : " + pModel.edit_user + 
            //    " Model Edit Date : " + pModel.edit_date);

            int vStatus = 0;
            var objHasil = new ResponseModel();

            string SqlString = @"UPDATE hrd_recruitment_reference
                                    SET name = @pname,
                                        address = @paddress,
                                        phone_number = @pphone_number,
                                        occupation = @poccupation,
                                        year_known = @pyear_known
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
                        cmd.Parameters.AddWithValue("@paddress", pModel.address);
                        cmd.Parameters.AddWithValue("@pphone_number", pModel.phone_number);
                        cmd.Parameters.AddWithValue("@poccupation", pModel.occupation);
                        cmd.Parameters.AddWithValue("@pyear_known", pModel.year_known);

                        vStatus = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE Recruitment REFERENCE SUCCESS....<br/> Code : " + pModel.req_id + " Name : " + pModel.name);

                        objHasil.isValid = Convert.ToBoolean(vStatus);
                        objHasil.message = " UPDATE Recruitment REFERENCE SUCCESS, Code : " + pModel.req_id + " Name : " + pModel.name;
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                objHasil.message = " UPDATE RECRUITMENT REFERENCE FAILED.....<br/> Code : " + pModel.req_id + " Name : " + pModel.name;

                Log.Error(DateTime.Now + " UPDATE RECRUITMENT REFERENCE FAILED, Code : " + pModel.req_id + " Name : " + pModel.name, ex);
            }

            return objHasil;
        }

        public ResponseModel DeleteRecruitmentRef(int pReqId)
        {
            var objHasil = new ResponseModel();

            var SqlString = @"DELETE FROM hrd_recruitment_reference WHERE req_id = @pReqId";

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
                        Log.Debug(DateTime.Now + " DELETE RECRUITMENT REFERENCE SUCCESS.....<br/> Req Id : " + pReqId);

                        objHasil.isValid = Convert.ToBoolean(status);
                        Log.Debug(DateTime.Now + " DELETE RECRUITMENT REFERENCE SUCCESS.....<br/> Req Id : " + pReqId);
                    }
                }
            }
            catch (Exception ex)
            {
                objHasil.isValid = false;
                Log.Debug(DateTime.Now + " DELETE RECRUITMENT REFERENCE FAILED.....<br/> Req Id : " + pReqId);

                Log.Error(DateTime.Now + " DELETE RECRUITMENT REFERENCE FAILED", ex);
            }

            return objHasil;
        }

        public int getRecruitmentRefSeqNo(int pRecruitmentId)
        {
            //Log.Debug(DateTime.Now + "=======>>>> MASUK REPO EMPLOYEE LIST, Emp Code : " + pEmployeeCode);

            var vNo = 0;
            var strSQL = @"SELECT max(mef.seq_no) seq_no
                           FROM hrd_recruitment_reference
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
                Log.Error(DateTime.Now + " GetRecruitmentRefSeqNo FAILED... ", ex);
            }

            return vNo;
        }
    }
}