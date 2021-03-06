﻿using hrd_holding.Models;
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

        public ResponseModel InsertRequest(hrdReqRecruitmentModel pModel)
        {
            var vResp = new ResponseModel();

            string SqlString = @"INSERT INTO hrd_req_recruitment
                                        (id,company_code,branch_code,date_req,no_req,position_need,reason,sex,age_min,
                                         education,job_experience,english_skill,certificate,marital_status,job_title,
                                         job_purpose,responsibility,count_staff,authority,job_relationship,job_self,
                                         source_employee,work_plan,note,count_needed,request_by,flag_status,flag_approval,
                                         user_approval,entry_date,entry_user)
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
                        cmd.Parameters.AddWithValue("@pBranchCode",pModel.branch_code);
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
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);

                        var status = cmd.ExecuteNonQuery();

                        vResp.isValid = true;
                        vResp.message = " INSERT REQUEST SUCCESS....<br/> No Req : " + pModel.no_req ;
                        Log.Debug(DateTime.Now + " INSERT REQUEST SUCCESS ====>>>> Code : " + pModel.no_req);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " INSERT REQUEST FAILED... <br/> No Req : " + pModel.no_req;
                Log.Error(DateTime.Now + " INSERT REQUEST FAILED...", ex);
            }

            return vResp;
        }

        public ResponseModel getRequestList(int pCompanyCode,int? pBranchCode,int pFlagStatus, int? pStartRow = 0, int? pRows = 0, string pWhere = "", string pOrderBy = "")
        {
            var vList = new List<hrdReqRecruitmentModel>();
            var vLimit = " LIMIT " + pStartRow + "," + pRows;
            var vJmlRecord = 0;

            var strSQLCount = @"SELECT COUNT(id) jml_record
                                FROM hrd_req_recruitment hr JOIN m_company mco ON hr.company_code = mco.company_code
                                JOIN m_branch_office mbo ON hr.branch_code = mbo.branch_code 
                                WHERE hr.company_code = @pCompanyCode AND hr.branch_code = @pBranchCode 
                                AND IFNULL(hr.flag_status,0) = @pFlagStatus " + pWhere;

            var strSQL = @"SELECT hr.id,
                                  IFNULL(hr.company_code,0) company_code,IFNULL(mco.int_company,'') int_company,IFNULL(mco.company_name,'') company_name,
	                              IFNULL(hr.branch_code,0) branch_code,IFNULL(mbo.int_branch,'') int_branch,IFNULL(mbo.branch_name,'') branch_name,
	                              hr.date_req,IFNULL(hr.no_req,'') no_req,IFNULL(hr.position_need,'') position_need,IFNULL(hr.reason,'') reason,
                                  IFNULL(hr.sex,0) sex,IFNULL(hr.age_min,0) age_min,IFNULL(hr.education,'') education,
                                  IFNULL(hr.job_experience,'') job_experience,IFNULL(hr.english_skill,'') english_skill,
                                  IFNULL(hr.certificate,'') certificate,IFNULL(hr.marital_status,0) marital_status,
                                  IFNULL(hr.job_title,'') job_title,IFNULL(hr.job_purpose,'') job_purpose,IFNULL(hr.responsibility,'') responsibility,
                                  IFNULL(hr.count_staff,0) count_staff,IFNULL(hr.authority,'') authority,
                                  IFNULL(hr.job_relationship,'') job_relationship,IFNULL(hr.job_self,'') job_self,
                                  IFNULL(hr.source_employee,0) source_employee,
                                  CASE WHEN IFNULL(hr.source_employee,0) = 0 THEN 'INTERNAL' ELSE 'EXTERNAL' END source_employee2,
                                  hr.work_plan,IFNULL(hr.note,'') note,
                                  IFNULL(hr.count_needed,0) count_needed,IFNULL(hr.request_by,'') request_by,
                                  IFNULL(hr.flag_status,0) flag_status,IFNULL(hr.flag_approval,0) flag_approval,IFNULL(hr.user_approval,'') user_approval,
	                              hr.entry_date,IFNULL(hr.entry_user,'') entry_user
                           FROM hrd_req_recruitment hr JOIN m_company mco ON hr.company_code = mco.company_code
                           JOIN m_branch_office mbo ON hr.branch_code = mbo.branch_code 
                           WHERE hr.company_code = @pCompanyCode AND hr.branch_code = @pBranchCode AND IFNULL(hr.flag_status,0) = @pFlagStatus " + 
                           pWhere + " " + pOrderBy + " " + vLimit;

            //Log.Debug(DateTime.Now + "====>>>> strSQL : " + strSQL);
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQLCount, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCompanyCode", pCompanyCode);
                        cmd.Parameters.AddWithValue("@pBranchCode", pBranchCode);
                        cmd.Parameters.AddWithValue("@pFlagStatus", pFlagStatus);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                //Log.Debug(DateTime.Now + "====>>>> aa MASUK HASROWS, Company : " + pCompanyCode + 
                                //    ", Branch : " + pBranchCode + ", FlagStatus : " + pFlagStatus +"<<<<=====");

                                while (aa.Read())
                                {
                                    vJmlRecord = aa.GetInt32("jml_record");

                                    //Log.Debug(DateTime.Now + "====>>>> aa MASUK READ (" + vJmlRecord + ")<<<<=====");
                                }
                            }
                        }
                    }

                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCompanyCode", pCompanyCode);
                        cmd.Parameters.AddWithValue("@pBranchCode", pBranchCode);
                        cmd.Parameters.AddWithValue("@pFlagStatus", pFlagStatus);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    var m = new hrdReqRecruitmentModel
                                    {
                                        id = aa.GetInt16("id"),
                                        company_code = aa.GetInt16("company_code"),
                                        int_company = aa.GetString("int_company"),
                                        company_name = aa.GetString("company_name"),
	                                    branch_code = aa.GetInt16("branch_code"),
                                        int_branch = aa.GetString("int_branch"),
                                        branch_name = aa.GetString("branch_name"),
                                        date_req = (aa["date_req"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["date_req"]),
                                        no_req = aa.GetString("no_req"),
                                        position_need = aa.GetString("position_need"),
                                        reason = aa.GetString("reason"),
                                        sex = aa.GetInt16("sex"),
                                        age_min = aa.GetInt16("age_min"),
                                        education = aa.GetInt16("education"),
                                        job_experience = aa.GetString("job_experience"),
                                        english_skill = aa.GetString("english_skill"),
                                        certificate = aa.GetString("certificate"),
                                        marital_status = aa.GetInt16("marital_status"),
                                        job_title = aa.GetString("job_title"),
                                        job_purpose = aa.GetString("job_purpose"),
                                        responsibility = aa.GetString("responsibility"),
                                        count_staff = aa.GetInt16("count_staff"),
                                        authority = aa.GetString("authority"),
                                        job_relationship = aa.GetString("job_relationship"),
                                        job_self = aa.GetString("job_self"),
                                        source_employee2 = aa.GetString("source_employee2"),
                                        work_plan = (aa["work_plan"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["work_plan"]),
                                        note = aa.GetString("note"),
                                        count_needed = aa.GetInt16("count_needed"),
                                        request_by = aa.GetString("request_by"),
                                        flag_status = aa.GetInt16("flag_status"),
                                        flag_approval = aa.GetInt16("flag_approval"),
                                        user_approval = aa.GetString("user_approval"),
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
                Log.Error(DateTime.Now + " GetRequestList FAILED... ", ex);
            }

            //Log.Debug(DateTime.Now + "====>>>> vJmlRecord : " + vJmlRecord + " JUMLAH DATA: " + vList.Count);

            var vResp = new ResponseModel();
            vResp.total_record = vJmlRecord;
            vResp.objResult = vList;

            return vResp;

        }

        public hrdReqRecruitmentModel getRequestInfo(int pReqCode)
        {
            var vModel = new hrdReqRecruitmentModel();
            var strSQL = @"SELECT hr.id,hr.company_code,mco.int_company,mco.company_name,
	                              hr.branch_code,mbo.int_branch,mbo.branch_name,
	                              hr.date_req,hr.no_req,hr.position_need,hr.reason,
                                  IFNULL(hr.sex,0) sex,IFNULL(hr.age_min,0) age_min,IFNULL(hr.education,0) education,
                                  IFNULL(hr.job_experience,'') job_experience,
                                  IFNULL(hr.english_skill,'') english_skill,IFNULL(hr.certificate,'') certificate,
                                  IFNULL(hr.marital_status,0) marital_status,
                                  IFNULL(hr.job_title,'') job_title,IFNULL(hr.job_purpose,'') job_purpose,
                                  IFNULL(hr.responsibility,'') responsibility,IFNULL(hr.count_staff,0) count_staff,
                                  IFNULL(hr.authority,'') authority,IFNULL(hr.job_relationship,'') job_relationship,
                                  IFNULL(hr.job_self,'') job_self,IFNULL(hr.source_employee,0) source_employee,
                                  hr.work_plan,IFNULL(hr.note,'') note,
                                  IFNULL(hr.count_needed,0) count_needed,IFNULL(hr.request_by,'') request_by,
                                  IFNULL(hr.flag_status,0) flag_status,IFNULL(hr.flag_approval,0) flag_approval,
                                  IFNULL(hr.user_approval,'') user_approval,
	                              hr.entry_date,IFNULL(hr.entry_user,'') entry_user
                           FROM hrd_req_recruitment hr JOIN m_company mco ON hr.company_code = mco.company_code
                           JOIN m_branch_office mbo ON hr.branch_code = mbo.branch_code 
                           WHERE hr.id = @pReqId";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pReqId", pReqCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.id = aa.GetInt16("id");
                                    vModel.company_code = aa.GetInt16("company_code");
                                    vModel.int_company = aa.GetString("int_company");
                                    vModel.company_name = aa.GetString("company_name");
                                    vModel.branch_code = aa.GetInt16("branch_code");
                                    vModel.int_branch = aa.GetString("int_branch");
                                    vModel.branch_name = aa.GetString("branch_name");
                                    vModel.date_req = (aa["date_req"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["date_req"]);
                                    vModel.no_req = aa.GetString("no_req");
                                    vModel.position_need = aa.GetString("position_need");
                                    vModel.reason = aa.GetString("reason");
                                    vModel.sex = aa.GetInt16("sex");
                                    vModel.age_min = aa.GetInt16("age_min");
                                    vModel.education = aa.GetInt16("education");
                                    vModel.job_experience = aa.GetString("job_experience");
                                    vModel.english_skill = aa.GetString("english_skill");
                                    vModel.certificate = aa.GetString("certificate");
                                    vModel.marital_status = aa.GetInt16("marital_status");
                                    vModel.job_title = aa.GetString("job_title");
                                    vModel.job_purpose = aa.GetString("job_purpose");
                                    vModel.responsibility = aa.GetString("responsibility");
                                    vModel.count_staff = aa.GetInt16("count_staff");
                                    vModel.authority = aa.GetString("authority");
                                    vModel.job_relationship = aa.GetString("job_relationship");
                                    vModel.job_self = aa.GetString("job_self");
                                    vModel.source_employee = aa.GetInt16("source_employee");
                                    vModel.work_plan = (aa["work_plan"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["work_plan"]);
                                    vModel.note = aa.GetString("note");
                                    vModel.count_needed = aa.GetInt16("count_needed");
                                    vModel.request_by = aa.GetString("request_by");
                                    vModel.flag_status = aa.GetInt16("flag_status");
                                    vModel.flag_approval = aa.GetInt16("flag_approval");
                                    vModel.user_approval = aa.GetString("user_approval");
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
                Log.Error(DateTime.Now + " GetRequestInfo Failed", ex);
            }
            return vModel;
        }

        public ResponseModel UpdateRequest(hrdReqRecruitmentModel pModel)
        {
            var vResp = new ResponseModel();

            string SqlString = @"UPDATE hrd_req_recruitment
                                    SET company_code = @pcompany_code,
	                                    branch_code = @pbranch_code,
	                                    date_req = @pdate_req,
                                        no_req = @pno_req,
                                        position_need = @pposition_need,
                                        reason = @preason,
                                        sex = @psex,
                                        age_min = @page_min,
                                        education = @peducation,
                                        job_experience = @pjob_experience,
                                        english_skill = @penglish_skill,
                                        certificate = @pcertificate,
                                        marital_status = @pmarital_status,
                                        job_title = @pjob_title,
                                        job_purpose = @pjob_purpose,
                                        responsibility = @presponsibility,
                                        count_staff = @pcount_staff,
                                        authority = @pauthority,
                                        job_relationship = @pjob_relationship,
                                        job_self = @pjob_self,
                                        source_employee = @psource_employee,
                                        work_plan = @pwork_plan,
                                        note = @pnote,
                                        count_needed = @pcount_needed,
                                        request_by = @prequest_by,
                                        flag_status = @pflag_status,
                                        flag_approval = @pflag_approval,
                                        user_approval = @puser_approval,
	                                    edit_date = @pedit_date,
                                        edit_user = @pedit_user
                                 WHERE id = @pReqId";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pcompany_code", pModel.company_code);
	                    cmd.Parameters.AddWithValue("@pbranch_code", pModel.branch_code);
	                    cmd.Parameters.AddWithValue("@pdate_req", pModel.date_req);
                        cmd.Parameters.AddWithValue("@pno_req", pModel.no_req);
                        cmd.Parameters.AddWithValue("@pposition_need", pModel.position_need);
                        cmd.Parameters.AddWithValue("@preason", pModel.reason);
                        cmd.Parameters.AddWithValue("@psex", pModel.sex);
                        cmd.Parameters.AddWithValue("@page_min", pModel.age_min);
                        cmd.Parameters.AddWithValue("@peducation", pModel.education);
                        cmd.Parameters.AddWithValue("@pjob_experience", pModel.job_experience);
                        cmd.Parameters.AddWithValue("@penglish_skill", pModel.english_skill);
                        cmd.Parameters.AddWithValue("@pcertificate", pModel.certificate);
                        cmd.Parameters.AddWithValue("@pmarital_status", pModel.marital_status);
                        cmd.Parameters.AddWithValue("@pjob_title", pModel.job_title);
                        cmd.Parameters.AddWithValue("@pjob_purpose", pModel.job_purpose);
                        cmd.Parameters.AddWithValue("@presponsibility", pModel.responsibility);
                        cmd.Parameters.AddWithValue("@pcount_staff", pModel.count_staff);
                        cmd.Parameters.AddWithValue("@pauthority", pModel.authority);
                        cmd.Parameters.AddWithValue("@pjob_relationship", pModel.job_relationship);
                        cmd.Parameters.AddWithValue("@pjob_self", pModel.job_self);
                        cmd.Parameters.AddWithValue("@psource_employee", pModel.source_employee);
                        cmd.Parameters.AddWithValue("@pwork_plan", pModel.work_plan);
                        cmd.Parameters.AddWithValue("@pnote", pModel.note);
                        cmd.Parameters.AddWithValue("@pcount_needed", pModel.count_needed);
                        cmd.Parameters.AddWithValue("@prequest_by", pModel.request_by);
                        cmd.Parameters.AddWithValue("@pflag_status", pModel.flag_status);
                        cmd.Parameters.AddWithValue("@pflag_approval", pModel.flag_approval);
                        cmd.Parameters.AddWithValue("@puser_approval", pModel.user_approval);
	                    cmd.Parameters.AddWithValue("@pedit_date", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pedit_user", pModel.edit_user);
                        cmd.Parameters.AddWithValue("@pReqId", pModel.id);

                        var status = cmd.ExecuteNonQuery();
                        vResp.isValid = true;
                        vResp.message = " UPDATE REQUEST SUCCESS...<br/> Code : " + pModel.id + ", No Req : " + pModel.no_req;
                        Log.Debug(DateTime.Now + " UPDATE REQUEST SUCCESS ====>>>>>> Code : " + pModel.id + " No Req : " + pModel.no_req);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " UPDATE REQUEST RECRUITMENT FAILED.....";
                Log.Error(DateTime.Now + " UPDATE REQUEST FAILED ", ex);
            }

            return vResp;
        }

        public ResponseModel DeleteRequset(int pCode)
        {
            var vResp = new ResponseModel();

            string SqlString = @"DELETE hrd_req_recruitment WHERE id = @pCode";

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
                        vResp.message = " DELETE REQUEST REQRUITMENT SUCCESS, ID : " + pCode;
                        Log.Debug(DateTime.Now + " DELETE REQUEST RECRUITMENT SUCCESS ====>>>>>> ID : " + pCode);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " DELETE REQUEST RECRUITMENT FAILED......";

                Log.Error(DateTime.Now + " DELETE REQUEST RECRUITMENT FAILED", ex);
            }

            return vResp;
        }

        public int getRequestSeqNo()
        {
            var vSeqNo = 0;
            var strSQL = @"SELECT IFNULL(MAX(id),0) seq_no FROM hrd_req_recruitment";
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
                Log.Error(DateTime.Now + " GetRequestSeqNo Failed", ex);
            }
            return vSeqNo;
        }
    }
}