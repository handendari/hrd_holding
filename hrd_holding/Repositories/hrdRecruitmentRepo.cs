using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class hrdRecruitmentRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdRecruitmentRepo");

        public ResponseModel InsertRecruitment(hrdRecruitmentModel pModel)
        {
            var vResp = new ResponseModel();
            string SqlString = @"INSERT INTO hrd_recruitment 
                                            (req_id,nik,name,place_birth,date_birth,address,phone_number,hp_number,flag_gender,
                                            flag_marital_status,company_code,branch_code,department_code,title_code,status_code,
                                            dialect_group,flag_religion,flag_driving_license,flag_driving_class,physical_disability,
                                            name_employer,date_started,date_left,initial_salary,last_salary,address_employer,business_type,
                                            no_employees,position_held,reason_leave,notice_required,earliest_date,brieft_description,
                                            flag_contact,expected_salary,sports,hobbies,member_club,additional_info,flag_crime,crime_detail,
                                            flag_friend,friend_name,friend_phone,flag_company,company_state,company_dept,emergency_name,
                                            emergency_phone,flag_agree1,flag_agree2,flag_agree3,flag_type,entry_date,entry_user)
                                VALUES (@preq_id,@pnik,@pname,@pplace_birth,@pdate_birth,@paddress,@pphone_number,@php_number,@pflag_gender,
                                        @pflag_marital_status,@pcompany_code,@pbranch_code,@pdepartment_code,@ptitle_code,@pstatus_code,
                                        @pdialect_group,@pflag_religion,@pflag_driving_license,@pflag_driving_class,@pphysical_disability,
                                        @pname_employer,@pdate_started,@pdate_left,@pinitial_salary,@plast_salary,@paddress_employer,@pbusiness_type,
                                        @pno_employees,@pposition_held,@preason_leave,@pnotice_required,@pearliest_date,@pbrieft_description,
                                        @pflag_contact,@pexpected_salary,@psports,@phobbies,@pmember_club,@padditional_info,@pflag_crime,@pcrime_detail,
                                        @pflag_friend,@pfriend_name,@pfriend_phone,@pflag_company,@pcompany_state,@pcompany_dept,@pemergency_name,
                                        @pemergency_phone,@pflag_agree1,@pflag_agree2,@pflag_agree3,@pflag_type,@pentry_date,@pentry_user)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@preq_id", pModel.req_id);
                        cmd.Parameters.AddWithValue("@pnik", pModel.nik);
                        cmd.Parameters.AddWithValue("@pname", pModel.name);
                        cmd.Parameters.AddWithValue("@pplace_birth", pModel.place_birth);
                        cmd.Parameters.AddWithValue("@pdate_birth", pModel.date_birth);
                        cmd.Parameters.AddWithValue("@paddress", pModel.address);
                        cmd.Parameters.AddWithValue("@pphone_number", pModel.phone_number);
                        cmd.Parameters.AddWithValue("@php_number", pModel.hp_number);
                        cmd.Parameters.AddWithValue("@pflag_gender", pModel.flag_gender);
                        cmd.Parameters.AddWithValue("@pflag_marital_status", pModel.flag_marital_status);
                        cmd.Parameters.AddWithValue("@pcompany_code", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pbranch_code", pModel.branch_code);
                        cmd.Parameters.AddWithValue("@pdepartment_code", pModel.department_code);
                        cmd.Parameters.AddWithValue("@ptitle_code", pModel.title_code);
                        cmd.Parameters.AddWithValue("@pstatus_code", pModel.status_code);
                        cmd.Parameters.AddWithValue("@pdialect_group", pModel.dialect_group);
                        cmd.Parameters.AddWithValue("@pflag_religion", pModel.flag_religion);
                        cmd.Parameters.AddWithValue("@pflag_driving_license", pModel.flag_driving_license);
                        cmd.Parameters.AddWithValue("@pflag_driving_class", pModel.flag_driving_class);
                        cmd.Parameters.AddWithValue("@pphysical_disability", pModel.physical_disability);
                        cmd.Parameters.AddWithValue("@pname_employer", pModel.name_employer);
                        cmd.Parameters.AddWithValue("@pdate_started", pModel.date_started);
                        cmd.Parameters.AddWithValue("@pdate_left", pModel.date_left);
                        cmd.Parameters.AddWithValue("@pinitial_salary", pModel.initial_salary);
                        cmd.Parameters.AddWithValue("@plast_salary", pModel.last_salary);
                        cmd.Parameters.AddWithValue("@paddress_employer", pModel.address_employer);
                        cmd.Parameters.AddWithValue("@pbusiness_type", pModel.business_type);
                        cmd.Parameters.AddWithValue("@pno_employees", pModel.no_employees);
                        cmd.Parameters.AddWithValue("@pposition_held", pModel.position_held);
                        cmd.Parameters.AddWithValue("@preason_leave", pModel.reason_leave);
                        cmd.Parameters.AddWithValue("@pnotice_required", pModel.notice_required);
                        cmd.Parameters.AddWithValue("@pearliest_date", pModel.earliest_date);
                        cmd.Parameters.AddWithValue("@pbrieft_description", pModel.brieft_description);
                        cmd.Parameters.AddWithValue("@pflag_contact", pModel.flag_contact);
                        cmd.Parameters.AddWithValue("@pexpected_salary", pModel.expected_salary);
                        cmd.Parameters.AddWithValue("@psports", pModel.sports);
                        cmd.Parameters.AddWithValue("@phobbies", pModel.hobbies);
                        cmd.Parameters.AddWithValue("@pmember_club", pModel.member_club);
                        cmd.Parameters.AddWithValue("@padditional_info", pModel.additional_info);
                        cmd.Parameters.AddWithValue("@pflag_crime", pModel.flag_crime);
                        cmd.Parameters.AddWithValue("@pcrime_detail", pModel.crime_detail);
                        cmd.Parameters.AddWithValue("@pflag_friend", pModel.flag_friend);
                        cmd.Parameters.AddWithValue("@pfriend_name", pModel.friend_name);
                        cmd.Parameters.AddWithValue("@pfriend_phone", pModel.friend_phone);
                        cmd.Parameters.AddWithValue("@pflag_company", pModel.flag_company);
                        cmd.Parameters.AddWithValue("@pcompany_state", pModel.company_state);
                        cmd.Parameters.AddWithValue("@pcompany_dept", pModel.company_dept);
                        cmd.Parameters.AddWithValue("@pemergency_name", pModel.emergency_name);
                        cmd.Parameters.AddWithValue("@pemergency_phone", pModel.emergency_phone);
                        cmd.Parameters.AddWithValue("@pflag_agree1", pModel.flag_agree1);
                        cmd.Parameters.AddWithValue("@pflag_agree2", pModel.flag_agree2);
                        cmd.Parameters.AddWithValue("@pflag_agree3", pModel.flag_agree3);
                        cmd.Parameters.AddWithValue("@pflag_type", pModel.flag_type);
                        cmd.Parameters.AddWithValue("@pentry_date", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pentry_user", pModel.entry_user);

                        var status = cmd.ExecuteNonQuery();
                        vResp.isValid = true;
                        vResp.message = " INSERT RECRUITMENT SUCCESS.....<br/> NIK : " + pModel.nik + ", Name : " + pModel.name;
                        Log.Debug(DateTime.Now + " INSERT RECRUITMENT SUCCESS, ===>>>>> NIK : " + pModel.nik + ", Name : " + pModel.name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " INSERT RECRUITMENT FAILED........";

                Log.Error(DateTime.Now + " INSERT RECRUITMENT FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel getRecruitmentList(int pCompanyCode, int pBranchCode, int? pStartRow = 0, int? pRows = 0, string pSortField = "", string pSortDir = "", string pWhere = "")
        {

            var vOrder = "ORDER BY " + pSortField + " " + pSortDir;
            var vLimit = " LIMIT " + pStartRow + "," + pRows;
            //Log.Debug(DateTime.Now + " pWhere : " + pWhere);

            var vJmlRecord = 0;
            var vList = new List<hrdRecruitmentModel>();

            var strSQLCount = @"SELECT COUNT(hr.id) jml_record
                                FROM hrd_recruitment hr JOIN m_company mco ON hr.company_code = mco.company_code
                                JOIN m_branch_office mbo ON hr.branch_code = mbo.branch_code
                                JOIN m_department md ON hr.department_code = md.department_code
                                JOIN m_title mt ON hr.title_code = mt.title_code
                                JOIN m_emp_status ms ON hr.status_code = ms.status_code
                                WHERE hr.company_code = @pCompanyCode AND hr.branch_code = @pBranchCode " + pWhere;

            var strSQL = @"SELECT hr.id,
		                        hr.req_id,
                                hr.nik,
                                hr.name,
                                hr.place_birth,
                                hr.date_birth,
                                hr.address,
                                hr.phone_number,
                                hr.hp_number,
                                hr.flag_gender,
		                        hr.flag_marital_status,
		                        hr.company_code,mco.int_company,mco.company_name,
                                hr.branch_code,mbo.int_branch,mbo.branch_name,
                                hr.department_code,md.int_department,md.department_name,
                                hr.title_code,mt.int_title,mt.title_name,
                                hr.status_code,ms.int_status,ms.status_name,
		                        hr.dialect_group,
                                hr.flag_religion,
                                hr.flag_driving_license,
                                hr.flag_driving_class,
                                hr.physical_disability,
		                        hr.name_employer,
                                hr.date_started,
                                hr.date_left,
                                hr.initial_salary,
                                hr.last_salary,
                                hr.address_employer,
                                hr.business_type,
		                        hr.no_employees,
                                hr.position_held,
                                hr.reason_leave,
                                hr.notice_required,
                                hr.earliest_date,
                                hr.brieft_description,
		                        hr.flag_contact,
                                hr.expected_salary,
                                hr.sports,
                                hr.hobbies,
                                hr.member_club,
                                hr.additional_info,
                                hr.flag_crime,
                                hr.crime_detail,
		                        hr.flag_friend,
                                hr.friend_name,
                                hr.friend_phone,
                                hr.flag_company,
                                hr.company_state,
                                hr.company_dept,
                                hr.emergency_name,
		                        hr.emergency_phone,
                                hr.flag_agree1,
                                hr.flag_agree2,
                                hr.flag_agree3,
                                hr.flag_type,
                                hr.entry_date,
                                hr.entry_user
                        FROM hrd_recruitment hr JOIN m_company mco ON hr.company_code = mco.company_code
                        JOIN m_branch_office mbo ON hr.branch_code = mbo.branch_code
                        JOIN m_department md ON hr.department_code = md.department_code
                        JOIN m_title mt ON hr.title_code = mt.title_code
                        JOIN m_emp_status ms ON hr.status_code = ms.status_code
                        WHERE hr.company_code = @pCompanyCode AND hr.branch_code = @pBranchCode " + pWhere + " " + vOrder + " " + vLimit;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQLCount, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCompanyCode", pCompanyCode);

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
                        cmd.Parameters.AddWithValue("@pCompanyCode", pCompanyCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    var m = new hrdRecruitmentModel
                                    {
                                        id = aa.GetInt16("id"),
                                        req_id = aa.GetInt16("req_id"),
                                        nik = aa.GetString("nik"),
                                        name = aa.GetString("name"),
                                        place_birth = aa.GetString("place_birth"),
                                        date_birth = (aa["date_birth"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["date_birth"]),
                                        address = aa.GetString("address"),
                                        phone_number = aa.GetString("phone_number"),
                                        hp_number = aa.GetString("hp_number"),
                                        flag_gender = aa.GetInt16("flag_gender"),
                                        flag_marital_status = aa.GetInt16("flag_marital_status"),
                                        company_code = aa.GetInt16("company_code"),
                                        int_company = aa.GetString("int_company"),
                                        company_name = aa.GetString("company_name"),
                                        branch_code = aa.GetInt16("branch_code"),
                                        int_branch = aa.GetString("int_branch"),
                                        branch_name = aa.GetString("branch_name"),
                                        department_code = aa.GetInt16("department_code"),
                                        int_department = aa.GetString("int_department"),
                                        department_name = aa.GetString("department_name"),
                                        title_code = aa.GetInt16("title_code"),
                                        int_title = aa.GetString("int_title"),
                                        title_name = aa.GetString("title_name"),
                                        status_code = aa.GetInt16("status_code"),
                                        int_status = aa.GetString("int_status"),
                                        status_name = aa.GetString("status_name"),
                                        dialect_group = aa.GetString("dialect_group"),
                                        flag_religion = aa.GetInt16("flag_religion"),
                                        flag_driving_license = aa.GetInt16("flag_driving_license"),
                                        flag_driving_class = aa.GetInt16("flag_driving_class"),
                                        physical_disability = aa.GetString("physical_disability"),
                                        name_employer = aa.GetString("name_employer"),
                                        date_started = (aa["date_started"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["date_started"]),
                                        date_left = (aa["date_left"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["date_left"]),
                                        initial_salary = aa.GetDecimal("initial_salary"),
                                        last_salary = aa.GetDecimal("last_salary"),
                                        address_employer = aa.GetString("address_employer"),
                                        business_type = aa.GetString("business_type"),
                                        no_employees = aa.GetString("no_employees"),
                                        position_held = aa.GetString("position_held"),
                                        reason_leave = aa.GetString("reason_leave"),
                                        notice_required = aa.GetString("notice_required"),
                                        earliest_date = (aa["earliest_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["earliest_date"]),
                                        brieft_description = aa.GetString("brieft_description"),
                                        flag_contact = aa.GetInt16("flag_contact"),
                                        expected_salary = aa.GetDecimal("expected_salary"),
                                        sports = aa.GetString("sports"),
                                        hobbies = aa.GetString("hobbies"),
                                        member_club = aa.GetString("member_club"),
                                        additional_info = aa.GetString("additional_info"),
                                        flag_crime = aa.GetBoolean("flag_crime"),
                                        crime_detail = aa.GetString("crime_detail"),
                                        flag_friend = aa.GetBoolean("flag_friend"),
                                        friend_name = aa.GetString("friend_name"),
                                        friend_phone = aa.GetString("friend_phone"),
                                        flag_company = aa.GetBoolean("flag_company"),
                                        company_state = aa.GetString("company_state"),
                                        company_dept = aa.GetString("physical_disability"),
                                        emergency_name = aa.GetString("emergency_name"),
                                        emergency_phone = aa.GetString("emergency_phone"),
                                        flag_agree1 = aa.GetBoolean("flag_agree1"),
                                        flag_agree2 = aa.GetBoolean("flag_agree2"),
                                        flag_agree3 = aa.GetBoolean("flag_agree3"),
                                        flag_type = aa.GetBoolean("flag_type"),
                                        entry_date = (aa["entry_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["entry_date"]),
                                        entry_user = aa.GetString("entry_user")
                                    };
                                    vList.Add(m);
                                }
                            }
                        }
                    }
                }
                Log.Debug(DateTime.Now + " Jumlah Data Rectuitment : " + vList.Count());
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetRecruitmentList FAILED... ", ex);
            }

            var vRes = new ResponseModel();
            vRes.total_record = vJmlRecord;
            vRes.objResult = vList;

            return vRes;
        }

        public hrdRecruitmentModel getRecruitmentInfo(int pReqId)
        {
            Log.Debug(DateTime.Now + "====>>>> Recruitment Id : " + pReqId);

            var vModel = new hrdRecruitmentModel();
            var strSQL = @"SELECT hr.id,
		                        hr.req_id,
                                hr.nik,
                                hr.name,
                                hr.place_birth,
                                hr.date_birth,
                                hr.address,
                                hr.phone_number,
                                hr.hp_number,
                                hr.flag_gender,
		                        hr.flag_marital_status,
		                        hr.company_code,mco.int_company,mco.company_name,
                                hr.branch_code,mbo.int_branch,mbo.branch_name,
                                hr.department_code,md.int_department,md.department_name,
                                hr.title_code,mt.int_title,mt.title_name,
                                hr.status_code,ms.int_status,ms.status_name,
		                        hr.dialect_group,
                                hr.flag_religion,
                                hr.flag_driving_license,
                                hr.flag_driving_class,
                                hr.physical_disability,
		                        hr.name_employer,
                                hr.date_started,
                                hr.date_left,
                                hr.initial_salary,
                                hr.last_salary,
                                hr.address_employer,
                                hr.business_type,
		                        hr.no_employees,
                                hr.position_held,
                                hr.reason_leave,
                                hr.notice_required,
                                hr.earliest_date,
                                hr.brieft_description,
		                        hr.flag_contact,
                                hr.expected_salary,
                                hr.sports,
                                hr.hobbies,
                                hr.member_club,
                                hr.additional_info,
                                hr.flag_crime,
                                hr.crime_detail,
		                        hr.flag_friend,
                                hr.friend_name,
                                hr.friend_phone,
                                hr.flag_company,
                                hr.company_state,
                                hr.company_dept,
                                hr.emergency_name,
		                        hr.emergency_phone,
                                hr.flag_agree1,
                                hr.flag_agree2,
                                hr.flag_agree3,
                                hr.flag_type,
                                hr.entry_date,
                                hr.entry_user
                        FROM hrd_recruitment hr JOIN m_company mco ON hr.company_code = mco.company_code
                        JOIN m_branch_office mbo ON hr.branch_code = mbo.branch_code
                        JOIN m_department md ON hr.department_code = md.department_code
                        JOIN m_title mt ON hr.title_code = mt.title_code
                        JOIN m_emp_status ms ON hr.status_code = ms.status_code
                        WHERE hr.id = @pReqId";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pReqId", pReqId);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.id = aa.GetInt16("id");
                                    vModel.req_id = aa.GetInt16("req_id");
                                    vModel.nik = aa.GetString("nik");
                                    vModel.name = aa.GetString("name");
                                    vModel.place_birth = aa.GetString("place_birth");
                                    vModel.date_birth = (aa["date_birth"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["date_birth"]);
                                    vModel.address = aa.GetString("address");
                                    vModel.phone_number = aa.GetString("phone_number");
                                    vModel.hp_number = aa.GetString("hp_number");
                                    vModel.flag_gender = aa.GetInt16("flag_gender");
                                    vModel.flag_marital_status = aa.GetInt16("flag_marital_status");
                                    vModel.company_code = aa.GetInt16("company_code");
                                    vModel.int_company = aa.GetString("int_company");
                                    vModel.company_name = aa.GetString("company_name");
                                    vModel.branch_code = aa.GetInt16("branch_code");
                                    vModel.int_branch = aa.GetString("int_branch");
                                    vModel.branch_name = aa.GetString("branch_name");
                                    vModel.department_code = aa.GetInt16("department_code");
                                    vModel.int_department = aa.GetString("int_department");
                                    vModel.department_name = aa.GetString("department_name");
                                    vModel.title_code = aa.GetInt16("title_code");
                                    vModel.int_title = aa.GetString("int_title");
                                    vModel.title_name = aa.GetString("title_name");
                                    vModel.status_code = aa.GetInt16("status_code");
                                    vModel.int_status = aa.GetString("int_status");
                                    vModel.status_name = aa.GetString("status_name");
                                    vModel.dialect_group = aa.GetString("dialect_group");
                                    vModel.flag_religion = aa.GetInt16("flag_religion");
                                    vModel.flag_driving_license = aa.GetInt16("flag_driving_license");
                                    vModel.flag_driving_class = aa.GetInt16("flag_driving_class");
                                    vModel.physical_disability = aa.GetString("physical_disability");
                                    vModel.name_employer = aa.GetString("name_employer");
                                    vModel.date_started = (aa["date_started"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["date_started"]);
                                    vModel.date_left = (aa["date_left"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["date_left"]);
                                    vModel.initial_salary = aa.GetDecimal("initial_salary");
                                    vModel.last_salary = aa.GetDecimal("last_salary");
                                    vModel.address_employer = aa.GetString("address_employer");
                                    vModel.business_type = aa.GetString("business_type");
                                    vModel.no_employees = aa.GetString("no_employees");
                                    vModel.position_held = aa.GetString("position_held");
                                    vModel.reason_leave = aa.GetString("reason_leave");
                                    vModel.notice_required = aa.GetString("notice_required");
                                    vModel.earliest_date = (aa["earliest_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["earliest_date"]);
                                    vModel.brieft_description = aa.GetString("brieft_description");
                                    vModel.flag_contact = aa.GetInt16("flag_contact");
                                    vModel.expected_salary = aa.GetDecimal("expected_salary");
                                    vModel.sports = aa.GetString("sports");
                                    vModel.hobbies = aa.GetString("hobbies");
                                    vModel.member_club = aa.GetString("member_club");
                                    vModel.additional_info = aa.GetString("additional_info");
                                    vModel.flag_crime = aa.GetBoolean("flag_crime");
                                    vModel.crime_detail = aa.GetString("crime_detail");
                                    vModel.flag_friend = aa.GetBoolean("flag_friend");
                                    vModel.friend_name = aa.GetString("friend_name");
                                    vModel.friend_phone = aa.GetString("friend_phone");
                                    vModel.flag_company = aa.GetBoolean("flag_company");
                                    vModel.company_state = aa.GetString("company_state");
                                    vModel.company_dept = aa.GetString("physical_disability");
                                    vModel.emergency_name = aa.GetString("emergency_name");
                                    vModel.emergency_phone = aa.GetString("emergency_phone");
                                    vModel.flag_agree1 = aa.GetBoolean("flag_agree1");
                                    vModel.flag_agree2 = aa.GetBoolean("flag_agree2");
                                    vModel.flag_agree3 = aa.GetBoolean("flag_agree3");
                                    vModel.flag_type = aa.GetBoolean("flag_type");
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
                Log.Error(DateTime.Now + " GetRecruitmentInfo Failed", ex);
            }
            return vModel;
        }

        public ResponseModel UpdateRecruitment(hrdRecruitmentModel pModel)
        {
            var vResp = new ResponseModel();

            string SqlString = @"UPDATE hrd_recruitment
                                    SET req_id = @preq_id,
                                        nik = @pnik,
                                        name = @pname,
                                        place_birth = @pplace_birth,
                                        date_birth = @pdate_birth,
                                        address = @paddress,
                                        phone_number = @pphone_number,
                                        hp_number = @php_number,
                                        flag_gender = @pflag_gender,
		                                flag_marital_status = @pflag_marital_status,
		                                company_code = @pcompany_code,
                                        branch_code = @pbranch_code,
                                        department_code = @pdepartment_code,
                                        title_code = @ptitle_code,
                                        status_code = @pstatus_code,
		                                dialect_group = @pdialect_group,
                                        flag_religion = @pflag_religion,
                                        flag_driving_license = @pflag_driving_license,
                                        flag_driving_class = @pflag_driving_class,
                                        physical_disability = @pphysical_disability,
		                                name_employer = @pname_employer,
                                        date_started = @pdate_started,
                                        date_left = @pdate_left,
                                        initial_salary = @pinitial_salary,
                                        last_salary = @plast_salary,
                                        address_employer = @paddress_employer,
                                        business_type = @pbusiness_type,
		                                no_employees = @pno_employees,
                                        position_held = @pposition_held,
                                        reason_leave = @preason_leave,
                                        notice_required = @pnotice_required,
                                        earliest_date = @pearliest_date,
                                        brieft_description = @pbrieft_description,
		                                flag_contact = @pflag_contact,
                                        expected_salary = @pexpected_salary,
                                        sports = @psports,
                                        hobbies = @phobbies,
                                        member_club = @pmember_club,
                                        additional_info = @padditional_info,
                                        flag_crime = @pflag_crime,
                                        crime_detail = @pcrime_detail,
		                                flag_friend = @pflag_friend,
                                        friend_name = @pfriend_name,
                                        friend_phone = @pfriend_phone,
                                        flag_company = @pflag_company,
                                        company_state = @pcompany_state,
                                        company_dept = @pcompany_dept,
                                        emergency_name = @pemergency_name,
		                                emergency_phone = @pemergency_phone,
                                        flag_agree1 = @pflag_agree1,
                                        flag_agree2 = @pflag_agree2,
                                        flag_agree3 = @pflag_agree3,
                                        flag_type = @pflag_type
                                WHERE id = @pRecId";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pReqId", pModel.req_id);
                        cmd.Parameters.AddWithValue("@pnik", pModel.nik);
                        cmd.Parameters.AddWithValue("@pname", pModel.name);
                        cmd.Parameters.AddWithValue("@pplace_birth", pModel.place_birth);
                        cmd.Parameters.AddWithValue("@pdate_birth", pModel.date_birth);
                        cmd.Parameters.AddWithValue("@paddress", pModel.address);
                        cmd.Parameters.AddWithValue("@pphone_number", pModel.phone_number);
                        cmd.Parameters.AddWithValue("@php_number", pModel.hp_number);
                        cmd.Parameters.AddWithValue("@pflag_gender", pModel.flag_gender);
                        cmd.Parameters.AddWithValue("@pflag_marital_status", pModel.flag_marital_status);
                        cmd.Parameters.AddWithValue("@pcompany_code", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pbranch_code", pModel.branch_code);
                        cmd.Parameters.AddWithValue("@pdepartment_code", pModel.department_code);
                        cmd.Parameters.AddWithValue("@ptitle_code", pModel.title_code);
                        cmd.Parameters.AddWithValue("@pstatus_code", pModel.status_code);
                        cmd.Parameters.AddWithValue("@pdialect_group", pModel.dialect_group);
                        cmd.Parameters.AddWithValue("@pflag_religion", pModel.flag_religion);
                        cmd.Parameters.AddWithValue("@pflag_driving_license", pModel.flag_driving_license);
                        cmd.Parameters.AddWithValue("@pflag_driving_class", pModel.flag_driving_class);
                        cmd.Parameters.AddWithValue("@pphysical_disability", pModel.physical_disability);
                        cmd.Parameters.AddWithValue("@pname_employer", pModel.name_employer);
                        cmd.Parameters.AddWithValue("@pdate_started", pModel.date_started);
                        cmd.Parameters.AddWithValue("@pdate_left", pModel.date_left);
                        cmd.Parameters.AddWithValue("@pinitial_salary", pModel.initial_salary);
                        cmd.Parameters.AddWithValue("@plast_salary", pModel.last_salary);
                        cmd.Parameters.AddWithValue("@paddress_employer", pModel.address_employer);
                        cmd.Parameters.AddWithValue("@pbusiness_type", pModel.business_type);
                        cmd.Parameters.AddWithValue("@pno_employees", pModel.no_employees);
                        cmd.Parameters.AddWithValue("@pposition_held", pModel.position_held);
                        cmd.Parameters.AddWithValue("@preason_leave", pModel.reason_leave);
                        cmd.Parameters.AddWithValue("@pnotice_required", pModel.notice_required);
                        cmd.Parameters.AddWithValue("@pearliest_date", pModel.earliest_date);
                        cmd.Parameters.AddWithValue("@pbrieft_description", pModel.brieft_description);
                        cmd.Parameters.AddWithValue("@pflag_contact", pModel.flag_contact);
                        cmd.Parameters.AddWithValue("@pexpected_salary", pModel.expected_salary);
                        cmd.Parameters.AddWithValue("@psports", pModel.sports);
                        cmd.Parameters.AddWithValue("@phobbies", pModel.hobbies);
                        cmd.Parameters.AddWithValue("@pmember_club", pModel.member_club);
                        cmd.Parameters.AddWithValue("@padditional_info", pModel.additional_info);
                        cmd.Parameters.AddWithValue("@pflag_crime", pModel.flag_crime);
                        cmd.Parameters.AddWithValue("@pcrime_detail", pModel.crime_detail);
                        cmd.Parameters.AddWithValue("@pflag_friend", pModel.flag_friend);
                        cmd.Parameters.AddWithValue("@pfriend_name", pModel.friend_name);
                        cmd.Parameters.AddWithValue("@pfriend_phone", pModel.friend_phone);
                        cmd.Parameters.AddWithValue("@pflag_company", pModel.flag_company);
                        cmd.Parameters.AddWithValue("@pcompany_state", pModel.company_state);
                        cmd.Parameters.AddWithValue("@pcompany_dept", pModel.company_dept);
                        cmd.Parameters.AddWithValue("@pemergency_name", pModel.emergency_name);
                        cmd.Parameters.AddWithValue("@pemergency_phone", pModel.emergency_phone);
                        cmd.Parameters.AddWithValue("@pflag_agree1", pModel.flag_agree1);
                        cmd.Parameters.AddWithValue("@pflag_agree2", pModel.flag_agree2);
                        cmd.Parameters.AddWithValue("@pflag_agree3", pModel.flag_agree3);
                        cmd.Parameters.AddWithValue("@pflag_type", pModel.flag_type);
                        cmd.Parameters.AddWithValue("@pRecId", pModel.id);

                        var status = cmd.ExecuteNonQuery();
                        vResp.isValid = true;
                        vResp.message = " UPDATE RECRUITMENT SUCCESS.....<br/> NIK : " + pModel.nik + " Name : " + pModel.name;
                        Log.Debug(DateTime.Now + " UPDATE RECRUITMENT SUCCESS ====>>>>>> NIK : " + pModel.nik + " Name : " + pModel.name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " UPDATE RECRUITMENT FAILED.......";

                Log.Error(DateTime.Now + " UPDATE RECRUITMENT FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel DeleteRecruitment(int pRecId)
        {
            var vResp = new ResponseModel();

            string SqlString = @"DELETE hrd_recruitment
                                 WHERE id = @pReqId";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pReqId", pRecId);

                        var status = cmd.ExecuteNonQuery();
                        vResp.isValid = true;
                        vResp.message = " DELETE RECRUITMENT SUCCESS, Id : " + pRecId;
                        Log.Debug(DateTime.Now + " DELETE RECRUITMENT SUCCESS ====>>>>>> Id : " + pRecId);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " DELETE RECRUITMENT FAILED......";

                Log.Error(DateTime.Now + " DELETE RECRUITMENT FAILED", ex);
            }

            return vResp;
        }

//        public int getEmployeeSeqNo(string pEmployeeCode, string pCompanyCode)
//        {
//            var vSeqNo = 0;
//            var strSQL = @"SELECT IFNULL(MAX(seq_no),0) seqno
//                           FROM m_employee emp
//                           WHERE emp.employee_code = @pEmployeeCode AND emp.company_code = @pCompanyCode";
//            try
//            {
//                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
//                {
//                    conn.Open();
//                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
//                    {
//                        cmd.CommandType = CommandType.Text;
//                        cmd.Parameters.AddWithValue("@pEmployeeCode", pEmployeeCode);
//                        cmd.Parameters.AddWithValue("@pCompanyCode", pCompanyCode);

//                        using (MySqlDataReader aa = cmd.ExecuteReader())
//                        {
//                            if (aa.HasRows)
//                            {
//                                while (aa.Read())
//                                {
//                                    vSeqNo = aa.GetInt16("seq_no") + 1;
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Log.Error(DateTime.Now + " GetEmployeeSeqNo Failed", ex);
//            }
//            return vSeqNo;
//        }

    }
}