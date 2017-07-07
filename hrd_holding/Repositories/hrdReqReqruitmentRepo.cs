using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class hrdReqReqruitmentRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdReqReqruitmentRepo");

        public ResponseModel InsertSubtitle(hrdReqReqruitmentModel pModel)
        {
            var vResp = new ResponseModel();

            string SqlString = @"INSERT INTO hrd_req_recruitment
                                        (id,company_code,branch_code,date_req,no_req,position_need,reason,sex,age_min,
                                         education,job_experience,english_skill,certificate,marital_status,job_title,
                                         job_purpose,responsibility,count_staff,authority,job_relationship,job_self,
                                         source_employee,work_plan,note,count_needed,request_by,flag_status,flag_approval,
                                         user_approval,entry_date,entry_user,edit_date,edit_user)
                                VALUES (@pId,@pCompanyCode,@pBranchCode,@pDateReq,@pNoReq,@pPositionNeed,@pReason,@pSex,@pAgeMin,
                                        @pEducation,@pJobExperience,@pEnglishSkill,@pCertificate,@pMaritalStatus,@pJobTitle,
                                        @pJobPurpose,@pResponsibility,@pCountStaff,@pAuthority,@pJobRelationship,@pJobSelf,
                                        @pSourceEmployee,@pWorkPlan,@pNote,@pCountNeeded,@pRequestBy,@pFlagStatus,@pFlagApproval,
                                        @pUserApproval,@pEntryDate,@pEntryUser)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pId", pModel.id);
                        cmd.Parameters.AddWithValue("@pCompanyCode", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pBranchCode",, pModel.branch_code);
                        cmd.Parameters.AddWithValue("@pDateReq", pModel.date_req);
                        cmd.Parameters.AddWithValue("@pNoReq", pModel.no_req);
                        cmd.Parameters.AddWithValue("@pPositionNeed", pModel.position_need);
                        cmd.Parameters.AddWithValue("@pReason", pModel.reason);
                        cmd.Parameters.AddWithValue("@pSex", pModel.sex);
                        cmd.Parameters.AddWithValue("@pAgeMin", pModel.age_min);
                        cmd.Parameters.AddWithValue("@pEducation", pModel.education);
                        cmd.Parameters.AddWithValue("@pJobExperience", pModel.job_experience);
                        cmd.Parameters.AddWithValue("@pEnglishSkill", pModel.english_skill);
                        cmd.Parameters.AddWithValue("@pCertificate", pModel.certificate);
                        cmd.Parameters.AddWithValue("@pMaritalStatus", pModel.marital_status);
                        cmd.Parameters.AddWithValue("@pJobTitle", pModel.job_title);
                        cmd.Parameters.AddWithValue("@pJobPurpose", pModel.job_purpose);
                        cmd.Parameters.AddWithValue("@pResponsibility", pModel.responsibility);
                        cmd.Parameters.AddWithValue("@pCountStaff", pModel.count_staff);
                        cmd.Parameters.AddWithValue("@pAuthority", pModel.authority);
                        cmd.Parameters.AddWithValue("@pJobRelationship", pModel.job_relationship);
                        cmd.Parameters.AddWithValue("@pJobSelf", pModel.job_self);
                        cmd.Parameters.AddWithValue("@pSourceEmployee", pModel.source_employee);
                        cmd.Parameters.AddWithValue("@pWorkPlan", pModel.work_plan);
                        cmd.Parameters.AddWithValue("@pNote", pModel.note);
                        cmd.Parameters.AddWithValue("@pCountNeeded", pModel.count_needed);
                        cmd.Parameters.AddWithValue("@pRequestBy", pModel.request_by);
                        cmd.Parameters.AddWithValue("@pFlagStatus", pModel.flag_status);
                        cmd.Parameters.AddWithValue("@pFlagApproval", pModel.flag_approval);
                        cmd.Parameters.AddWithValue("@pUserApproval", pModel.user_approval);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user)

                        var status = cmd.ExecuteNonQuery();

                        vResp.isValid = true;
                        vResp.message = " INSERT SUBTITLE, Code : " + pModel.subtitle_code + " Name : " + pModel.subtitle_name;
                        Log.Debug(DateTime.Now + " INSERT SUBTITLE ====>>>> Code : " + pModel.subtitle_code + " Name : " + pModel.subtitle_name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " INSERT SUBTITLE, Code : " + pModel.subtitle_code + " Name : " + pModel.subtitle_name;
                Log.Error(DateTime.Now + " INSERT SUBTITLE FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel getSubtitleList(int? pStartRow = 0, int? pRows = 0, string pWhere = "", string pOrderBy = "")
        {
            var vList = new List<mSubtitleModel>();
            var vLimit = pOrderBy + " LIMIT " + pStartRow + "," + pRows;
            var vJmlRecord = 0;

            var strSQLCount = @"SELECT COUNT(subtitle_code) jml_record
                                FROM m_subtitle " + pWhere;

            var strSQL = @"SELECT ms.subtitle_code,ms.title_code,mt.int_title,mt.title_name, ms.int_subtitle,ms.subtitle_name,ms.description
                           FROM m_subtitle ms JOIN m_title mt ON ms.title_code = mt.title_code " +
                           pWhere + " " + vLimit;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQLCount, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vJmlRecord = aa.GetInt32("jml_record");
                                }
                            }
                        }
                    }

                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    var m = new mSubtitleModel
                                    {
                                        subtitle_code = aa.GetInt16("subtitle_code"),
                                        title_code = aa.GetInt16("title_code"),
                                        int_title = aa.GetString("int_title"),
                                        title_name = aa.GetString("title_name"),
                                        int_subtitle = aa.GetString("int_subtitle"),
                                        subtitle_name = aa.GetString("subtitle_name"),
                                        description = aa.GetString("description")
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
                Log.Error(DateTime.Now + " GetSubtitleList FAILED... ", ex);
            }

            var vResp = new ResponseModel();
            vResp.total_record = vJmlRecord;
            vResp.objResult = vList;

            return vResp;

        }

        public mSubtitleModel getSubtitleInfo(int pSubtitleCode)
        {
            var vModel = new mSubtitleModel();
            var strSQL = @"SELECT ms.subtitle_code,ms.title_code,ms.title_name, ms.int_subtitle,ms.subtitle_name,ms.description
                           FROM m_subtitle ms JOIN m_title mt ON ms.title_code = mt.title_code
                           WHERE ms.subtitle_code = @pSubtitleCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pSubtitleCode", pSubtitleCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.subtitle_code = aa.GetInt16("subtitle_code");
                                    vModel.title_code = aa.GetInt16("title_code");
                                    vModel.title_name = aa.GetString("title_name");
                                    vModel.int_subtitle = aa.GetString("int_subtitle");
                                    vModel.subtitle_name = aa.GetString("subtitle_name");
                                    vModel.description = aa.GetString("description");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetSubtitleInfo Failed", ex);
            }
            return vModel;
        }

        public ResponseModel UpdateSubtitle(mSubtitleModel pModel)
        {
            var vResp = new ResponseModel();

            string SqlString = @"UPDATE `m_subtitle`
                                      SET `title_code` = @pTitleCode,
                                          `int_subtitle` = @pIntSubtitle,
                                          `subtitle_name` = @pSubtitleName,
                                          `description` = @pDescription
                                 WHERE `subtitle_code` = @pSubtitleCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pSubtitleCode", pModel.subtitle_code);
                        cmd.Parameters.AddWithValue("@pTitleCode", pModel.title_code);
                        cmd.Parameters.AddWithValue("@pIntSubtitle", pModel.int_subtitle);
                        cmd.Parameters.AddWithValue("@pSubtitleName", pModel.subtitle_name);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);

                        var status = cmd.ExecuteNonQuery();
                        vResp.isValid = true;
                        vResp.message = " UPDATE SUBTITLE SUCCESS, Code : " + pModel.subtitle_code + " Name : " + pModel.subtitle_name;
                        Log.Debug(DateTime.Now + " UPDATE SUBTITLE SUCCESS ====>>>>>> Code : " + pModel.subtitle_code + " Name : " + pModel.subtitle_name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " UPDATE SUBTITLE FAILED.....";
                Log.Error(DateTime.Now + " UPDATE SUBTITLE FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel DeleteSubtitle(int pCode)
        {
            var vResp = new ResponseModel();

            string SqlString = @"DELETE m_subtitle WHERE subtitle_code = @pCode";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCode", pCode);

                        var status = cmd.ExecuteNonQuery();

                        vResp.isValid = true;
                        vResp.message = " DELETE SUBTITLE SUCCESS, Code : " + pCode;
                        Log.Debug(DateTime.Now + " DELETE SUBTITLE SUCCESS ====>>>>>> Code : " + pCode);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " DELETE SUBTITLE FAILED......";

                Log.Error(DateTime.Now + " DELETE SUBTITLE FAILED", ex);
            }

            return vResp;
        }

        public int getSubtitleSeqNo()
        {
            var vSeqNo = 0;
            var strSQL = @"SELECT IFNULL(MAX(subtitle_code),0) seq_no FROM m_subtitle";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;

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
                Log.Error(DateTime.Now + " GetSubtitleSeqNo Failed", ex);
            }
            return vSeqNo;
        }
    }
}