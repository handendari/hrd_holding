using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mEmployeeRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeRepo");

        public void InsertEmployee(mEmployeeModel pModel)
        {
            string SqlString = @"INSERT INTO `m_employee`
                                        (`employee_code`,`seq_no`,`nik`,`nip`,`employee_name`,`employee_nick_name`,`company_code`,`branch_code`,
                                         `department_code`,`division_code`,`title_code`,`subtitle_code`,`level_code`,`status_code`,`flag_shiftable`,
                                         `flag_transport`,`place_birth`,`date_birth`,`sex`,`religion`,`marital_status`,`no_of_children`,`emp_address`,
                                         `npwp`,`kode_pajak`,`npwp_method`,`npwp_registered_date`,`npwp_address`,`no_jamsostek`,`jstk_registered_date`,
                                         `bank_code`,`bank_account`,`bank_acc_name`,`start_working`,`appointment_date`,`phone_number`,`hp_number`,
                                         `email`,`country_code`,`identity_number`,`last_education`,`last_employment`,`description`,`flag_active`,
                                         `end_working`,`reason`,`picture`,`salary_type`,`tgl_mutasi`,`flag_managerial`,`spv_code`,`note1`,`note2`,
                                         `note3`,`entry_date`,`entry_user`,`edit_date`,`edit_user`)
                                VALUES (@pEmployeeCode,@pSeqNo,@pNik,@pNip,@pEmployeeName,@pEmployeeNickName,@pCompanyCode,@pBranchCode,
                                        @pDepartmentCode,@pDivisionCode,@pTitleCode,@pSubtitleCode,@pLevelCode,@pStatusCode,@pFlagShiftable,
                                        @pFlagTransport,@pPlaceBirth,@pDateBirth,@pSex,@pReligion,@pMaritalStatus,@pNoOfChildren,@pEmpAddress,
                                        @pNpwp,@pKodePajak,@pNpwpMethod,@pNpwpRegisteredDate,@pNpwpAddress,@pNoJamsostek,@pJstkRegisteredDate,
                                        @pBankCode,@pBankAccount,@pBankAccName,@pStartWorking,@pAppointmentDate,@pPhoneNumber,@pHpNumber,
                                        @pEmail,@pCountryCode,@pIdentityNumber,@pLastEducation,@pLastEmployment,@pDescription,@pFlagActive,
                                        @pEndWorking,@pReason,@pPicture,@pSalaryType,@pTglMutasi,@pFlagManagerial,@pSpvCode,@pNote1,@pNote2,
                                        @pNote3,@pEntryDate,@pEntryUser,@pEditDate,@pEditUser)";

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
                        cmd.Parameters.AddWithValue("@pNik", pModel.nik);
                        cmd.Parameters.AddWithValue("@pNip", pModel.nip);
                        cmd.Parameters.AddWithValue("@pEmployeeName", pModel.employee_name);
                        cmd.Parameters.AddWithValue("@pEmployeeNickName", pModel.employee_nick_name);
                        cmd.Parameters.AddWithValue("@pCompanyCode", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pBranchCode", pModel.branch_code);
                        cmd.Parameters.AddWithValue("@pDepartmentCode", pModel.department_code);
                        cmd.Parameters.AddWithValue("@pDivisionCode", pModel.division_code);
                        cmd.Parameters.AddWithValue("@pTitleCode", pModel.title_code);
                        cmd.Parameters.AddWithValue("@pSubtitleCode", pModel.subtitle_code);
                        cmd.Parameters.AddWithValue("@pLevelCode", pModel.level_code);
                        cmd.Parameters.AddWithValue("@pStatusCode", pModel.status_code);
                        cmd.Parameters.AddWithValue("@pFlagShiftable", pModel.flag_shiftable);
                        cmd.Parameters.AddWithValue("@pFlagTransport", pModel.flag_transport);
                        cmd.Parameters.AddWithValue("@pPlaceBirth", pModel.place_birth);
                        cmd.Parameters.AddWithValue("@pDateBirth", pModel.date_birth);
                        cmd.Parameters.AddWithValue("@pSex", pModel.sex);
                        cmd.Parameters.AddWithValue("@pReligion", pModel.religion);
                        cmd.Parameters.AddWithValue("@pMaritalStatus", pModel.marital_status);
                        cmd.Parameters.AddWithValue("@pNoOfChildren", pModel.no_of_children);
                        cmd.Parameters.AddWithValue("@pEmpAddress", pModel.emp_address);
                        cmd.Parameters.AddWithValue("@pNpwp", pModel.npwp);
                        cmd.Parameters.AddWithValue("@pKodePajak", pModel.kode_pajak);
                        cmd.Parameters.AddWithValue("@pNpwpMethod", pModel.npwp_method);
                        cmd.Parameters.AddWithValue("@pNpwpRegisteredDate", pModel.npwp_registered_date);
                        cmd.Parameters.AddWithValue("@pNpwpAddress", pModel.npwp_address);
                        cmd.Parameters.AddWithValue("@pNoJamsostek", pModel.no_jamsostek);
                        cmd.Parameters.AddWithValue("@pJstkRegisteredDate", pModel.jstk_registered_date);
                        cmd.Parameters.AddWithValue("@pBankCode", pModel.bank_code);
                        cmd.Parameters.AddWithValue("@pBankAccount", pModel.bank_account);
                        cmd.Parameters.AddWithValue("@pBankAccName", pModel.bank_acc_name);
                        cmd.Parameters.AddWithValue("@pStartWorking", pModel.start_working);
                        cmd.Parameters.AddWithValue("@pAppointmentDate", pModel.appointment_date);
                        cmd.Parameters.AddWithValue("@pPhoneNumber", pModel.phone_number);
                        cmd.Parameters.AddWithValue("@pHpNumber", pModel.hp_number);
                        cmd.Parameters.AddWithValue("@pEmail", pModel.email);
                        cmd.Parameters.AddWithValue("@pCountryCode", pModel.country_code);
                        cmd.Parameters.AddWithValue("@pIdentityNumber", pModel.identity_number);
                        cmd.Parameters.AddWithValue("@pLastEducation", pModel.last_education);
                        cmd.Parameters.AddWithValue("@pLastEmployment", pModel.last_employment);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pFlagActive", pModel.flag_active);
                        cmd.Parameters.AddWithValue("@pEndWorking", pModel.end_working);
                        cmd.Parameters.AddWithValue("@pReason", pModel.reason);
                        cmd.Parameters.AddWithValue("@pPicture", pModel.picture);
                        cmd.Parameters.AddWithValue("@pSalaryType", pModel.salary_type);
                        cmd.Parameters.AddWithValue("@pTglMutasi", pModel.tgl_mutasi);
                        cmd.Parameters.AddWithValue("@pFlagManagerial", pModel.flag_managerial);
                        cmd.Parameters.AddWithValue("@pSpvCode", pModel.spv_code);
                        cmd.Parameters.AddWithValue("@pNote1", pModel.note1);
                        cmd.Parameters.AddWithValue("@pNote2", pModel.note2);
                        cmd.Parameters.AddWithValue("@pNote3", pModel.note3);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT EMPLOYEE SUCCESS ====>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT EMPLOYEE FAILED", ex);
            }

        }

        public ResponseModel getEmployeeList_Lama(int pCompanyCode, int? pStartRow=0, int? pRows=0, string pSortField = "", string pSortDir = "", string pWhere = "")
        {
            
            var vLimit = "ORDER BY " + pSortField + " " + pSortDir + " LIMIT " + pStartRow + "," + pRows;
            Log.Debug(DateTime.Now + " pWhere : " + pWhere);

            var vJmlRecord = 0;
            var vList = new List<mEmployeeModel>();
            
            var strSQLCount = @"SELECT COUNT(emp.employee_code) jml_record
                           FROM m_employee emp JOIN m_company mc ON emp.company_code = mc.company_code
                           JOIN m_branch_office mbo ON emp.branch_code = mbo.branch_code
                           JOIN m_department md ON emp.department_code = md.department_code
                           JOIN m_title mt ON emp.title_code = mt.title_code
                           JOIN m_country mco ON emp.country_code = mco.country_code
                           JOIN m_emp_status mes ON emp.status_code = mes.status_code
                           WHERE emp.company_code = @pCompanyCode ";

            var strSQL = @"SELECT emp.employee_code,emp.seq_no,emp.nik,emp.nip,emp.employee_name,emp.employee_nick_name,
                                  emp.company_code,mc.company_name,
                                  emp.branch_code,mbo.branch_name,
                                  emp.department_code,md.department_name,
                                  emp.division_code,
                                  emp.title_code,mt.title_name,
                                  emp.subtitle_code,IFNULL(ms.subtitle_name,'') subtitle_name,
                                  emp.level_code,ml.level_name,
                                  emp.status_code,mes.status_name,
                                  emp.flag_shiftable,
                                  emp.flag_transport,emp.place_birth,emp.date_birth,emp.sex,emp.religion,emp.marital_status,
                                  emp.no_of_children,emp.emp_address,
                                  emp.npwp,emp.kode_pajak,emp.npwp_method,emp.npwp_registered_date,emp.npwp_address,emp.no_jamsostek,
                                  emp.jstk_registered_date,
                                  emp.bank_code,mba.bank_name,
                                  emp.bank_account,emp.bank_acc_name,emp.start_working,emp.appointment_date,
                                  emp.phone_number,emp.hp_number,emp.email,
                                  emp.country_code,mco.country_name,
                                  emp.identity_number,emp.last_education,
                                  emp.last_employment,emp.description,emp.flag_active,
                                  emp.end_working,emp.reason,emp.picture,emp.salary_type,emp.tgl_mutasi,emp.flag_managerial,
                                  emp.spv_code,
                                  emp.note1,emp.note2,emp.note3,
                                  emp.entry_date,emp.entry_user,emp.edit_date,IFNULL(emp.edit_user,'') edit_user
                           FROM m_employee emp JOIN m_company mc ON emp.company_code = mc.company_code
                           JOIN m_branch_office mbo ON emp.branch_code = mbo.branch_code
                           JOIN m_department md ON emp.department_code = md.department_code
                           JOIN m_title mt ON emp.title_code = mt.title_code
                           JOIN m_country mco ON emp.country_code = mco.country_code
                           JOIN m_emp_status mes ON emp.status_code = mes.status_code
                           LEFT JOIN m_subtitle ms ON emp.subtitle_code = ms.subtitle_code
                           LEFT JOIN m_level ml ON emp.level_code = ml.level_code
                           LEFT JOIN m_bank mba ON emp.bank_code = mba.bank_code
                           WHERE emp.company_code = @pCompanyCode " + vLimit;
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
                                    var m = new mEmployeeModel
                                    {
                                        employee_code = aa.GetString("employee_code"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        nik = aa.GetString("nik"),
                                        nip = aa.GetString("nip"),
                                        employee_name = aa.GetString("employee_name"),
                                        employee_nick_name = aa.GetString("employee_nick_name"),
                                        company_code = aa.GetString("company_code"),
                                        company_name = aa.GetString("company_name"),
                                        branch_code = aa.GetString("branch_code"),
                                        branch_name = aa.GetString("branch_name"),
                                        department_code = aa.GetString("department_code"),
                                        department_name = aa.GetString("department_name"),
                                        division_code = aa.GetString("division_code"),
                                        title_code = aa.GetString("title_code"),
                                        title_name = aa.GetString("title_name"),
                                        subtitle_code = aa.GetString("subtitle_code"),
                                        subtitle_name = aa.GetString("subtitle_name"),
                                        level_code = aa.GetString("level_code"),
                                        level_name = aa.GetString("level_name"),
                                        status_code = aa.GetString("status_code"),
                                        status_name = aa.GetString("status_name"),
                                        flag_shiftable = aa.GetInt16("flag_shiftable"),
                                        flag_transport = aa.GetInt16("flag_transport"),
                                        place_birth = aa.GetString("place_birth"),
                                        date_birth = (aa["date_birth"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["date_birth"]),                                        
                                        sex = aa.GetInt16("sex"),
                                        religion = aa.GetInt16("religion"),
                                        marital_status = aa.GetInt16("marital_status"),
                                        no_of_children = aa.GetInt16("no_of_children"),
                                        emp_address = aa.GetString("emp_address"),
                                        npwp = aa.GetString("npwp"),
                                        kode_pajak = aa.GetString("kode_pajak"),
                                        npwp_method = aa.GetInt16("npwp_method"),
                                        npwp_registered_date = (aa["npwp_registered_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["npwp_registered_date"]),
                                        npwp_address = aa.GetString("npwp_address"),
                                        no_jamsostek = aa.GetString("no_jamsostek"),
                                        jstk_registered_date = (aa["jstk_registered_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["jstk_registered_date"]),
                                        bank_code = aa.GetString("bank_code"),
                                        bank_account = aa.GetString("bank_account"),
                                        bank_acc_name = aa.GetString("bank_acc_name"),
                                        start_working = (aa["start_working"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["start_working"]),
                                        appointment_date = (aa["appointment_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["appointment_date"]),
                                        phone_number = aa.GetString("phone_number"),
                                        hp_number = aa.GetString("hp_number"),
                                        email = aa.GetString("email"),
                                        country_code = aa.GetString("country_code"),
                                        country_name = aa.GetString("country_name"),
                                        identity_number = aa.GetString("identity_number"),
                                        last_education = aa.GetString("last_education"),
                                        last_employment = aa.GetString("last_employment"),
                                        description = aa.GetString("description"),
                                        flag_active = aa.GetInt16("flag_active"),
                                        end_working = (aa["end_working"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["end_working"]),
                                        reason = aa.GetString("reason"),
                                        picture = aa.GetString("picture"),
                                        salary_type = aa.GetInt16("salary_type"),
                                        tgl_mutasi = (aa["tgl_mutasi"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["tgl_mutasi"]),
                                        flag_managerial = aa.GetInt16("flag_managerial"),
                                        spv_code = aa.GetString("spv_code"),
                                        ////spv_name = aa.GetString("spv_name"),
                                        note1 = aa.GetString("note1"),
                                        note2 = aa.GetString("note2"),
                                        note3 = aa.GetString("note3"),
                                        entry_date = (aa["entry_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["entry_date"]),
                                        entry_user = aa.GetString("entry_user"),
                                        edit_date = (aa["edit_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["edit_date"]),
                                        edit_user = aa.GetString("edit_user")
                                    };
                                    vList.Add(m);
                                }
                            }
                        }
                    }
                }
                Log.Debug(DateTime.Now + " Jumlah Data Employee : " + vList.Count());
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetEmployeeList FAILED... ", ex);
            }

            var vRes = new ResponseModel();
            vRes.total_record = vJmlRecord;
            vRes.objResult = vList;
            
            return vRes;
        }

        public ResponseModel getEmployeeList(int pCompanyCode, int? pStartRow = 0, int? pRows = 0, string pWhere = "", string pOrderBy = "")
        {

            var vLimit = pWhere + pOrderBy + " LIMIT " + pStartRow + "," + pRows;

            var vJmlRecord = 0;
            var vList = new List<mEmployeeModel>();

            var strSQLCount = @"SELECT COUNT(emp.employee_code) jml_record
                           FROM m_employee emp JOIN m_company mc ON emp.company_code = mc.company_code
                           JOIN m_branch_office mbo ON emp.branch_code = mbo.branch_code
                           JOIN m_department md ON emp.department_code = md.department_code
                           JOIN m_title mt ON emp.title_code = mt.title_code
                           JOIN m_country mco ON emp.country_code = mco.country_code
                           JOIN m_emp_status mes ON emp.status_code = mes.status_code
                           WHERE emp.company_code = @pCompanyCode " + pWhere;
            
            Log.Debug(DateTime.Now + " strSQLCount : " + strSQLCount);

            var strSQL = @"SELECT emp.employee_code,emp.seq_no,emp.nik,emp.nip,emp.employee_name,emp.employee_nick_name,
                                  emp.company_code,mc.company_name,
                                  emp.branch_code,mbo.branch_name,
                                  emp.department_code,md.department_name,
                                  emp.division_code,
                                  emp.title_code,mt.title_name,
                                  emp.subtitle_code,IFNULL(ms.subtitle_name,'') subtitle_name,
                                  emp.level_code,ml.level_name,
                                  emp.status_code,mes.status_name,
                                  emp.flag_shiftable,
                                  emp.flag_transport,emp.place_birth,emp.date_birth,emp.sex,emp.religion,emp.marital_status,
                                  emp.no_of_children,emp.emp_address,
                                  emp.npwp,emp.kode_pajak,emp.npwp_method,emp.npwp_registered_date,emp.npwp_address,emp.no_jamsostek,
                                  emp.jstk_registered_date,
                                  emp.bank_code,mba.bank_name,
                                  emp.bank_account,emp.bank_acc_name,emp.start_working,emp.appointment_date,
                                  emp.phone_number,emp.hp_number,emp.email,
                                  emp.country_code,mco.country_name,
                                  emp.identity_number,emp.last_education,
                                  emp.last_employment,emp.description,emp.flag_active,
                                  emp.end_working,emp.reason,emp.picture,emp.salary_type,emp.tgl_mutasi,emp.flag_managerial,
                                  emp.spv_code,
                                  emp.note1,emp.note2,emp.note3,
                                  emp.entry_date,emp.entry_user,emp.edit_date,IFNULL(emp.edit_user,'') edit_user
                           FROM m_employee emp JOIN m_company mc ON emp.company_code = mc.company_code
                           JOIN m_branch_office mbo ON emp.branch_code = mbo.branch_code
                           JOIN m_department md ON emp.department_code = md.department_code
                           JOIN m_title mt ON emp.title_code = mt.title_code
                           JOIN m_country mco ON emp.country_code = mco.country_code
                           JOIN m_emp_status mes ON emp.status_code = mes.status_code
                           LEFT JOIN m_subtitle ms ON emp.subtitle_code = ms.subtitle_code
                           LEFT JOIN m_level ml ON emp.level_code = ml.level_code
                           LEFT JOIN m_bank mba ON emp.bank_code = mba.bank_code
                           WHERE emp.company_code = @pCompanyCode " + vLimit;

            Log.Debug(DateTime.Now + " strSQL : " + strSQL);

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
                                    var m = new mEmployeeModel
                                    {
                                        employee_code = aa.GetString("employee_code"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        nik = aa.GetString("nik"),
                                        nip = aa.GetString("nip"),
                                        employee_name = aa.GetString("employee_name"),
                                        employee_nick_name = aa.GetString("employee_nick_name"),
                                        company_code = aa.GetString("company_code"),
                                        company_name = aa.GetString("company_name"),
                                        branch_code = aa.GetString("branch_code"),
                                        branch_name = aa.GetString("branch_name"),
                                        department_code = aa.GetString("department_code"),
                                        department_name = aa.GetString("department_name"),
                                        division_code = aa.GetString("division_code"),
                                        title_code = aa.GetString("title_code"),
                                        title_name = aa.GetString("title_name"),
                                        subtitle_code = aa.GetString("subtitle_code"),
                                        subtitle_name = aa.GetString("subtitle_name"),
                                        level_code = aa.GetString("level_code"),
                                        level_name = aa.GetString("level_name"),
                                        status_code = aa.GetString("status_code"),
                                        status_name = aa.GetString("status_name"),
                                        flag_shiftable = aa.GetInt16("flag_shiftable"),
                                        flag_transport = aa.GetInt16("flag_transport"),
                                        place_birth = aa.GetString("place_birth"),
                                        date_birth = (aa["date_birth"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["date_birth"]),
                                        sex = aa.GetInt16("sex"),
                                        religion = aa.GetInt16("religion"),
                                        marital_status = aa.GetInt16("marital_status"),
                                        no_of_children = aa.GetInt16("no_of_children"),
                                        emp_address = aa.GetString("emp_address"),
                                        npwp = aa.GetString("npwp"),
                                        kode_pajak = aa.GetString("kode_pajak"),
                                        npwp_method = aa.GetInt16("npwp_method"),
                                        npwp_registered_date = (aa["npwp_registered_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["npwp_registered_date"]),
                                        npwp_address = aa.GetString("npwp_address"),
                                        no_jamsostek = aa.GetString("no_jamsostek"),
                                        jstk_registered_date = (aa["jstk_registered_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["jstk_registered_date"]),
                                        bank_code = aa.GetString("bank_code"),
                                        bank_account = aa.GetString("bank_account"),
                                        bank_acc_name = aa.GetString("bank_acc_name"),
                                        start_working = (aa["start_working"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["start_working"]),
                                        appointment_date = (aa["appointment_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["appointment_date"]),
                                        phone_number = aa.GetString("phone_number"),
                                        hp_number = aa.GetString("hp_number"),
                                        email = aa.GetString("email"),
                                        country_code = aa.GetString("country_code"),
                                        country_name = aa.GetString("country_name"),
                                        identity_number = aa.GetString("identity_number"),
                                        last_education = aa.GetString("last_education"),
                                        last_employment = aa.GetString("last_employment"),
                                        description = aa.GetString("description"),
                                        flag_active = aa.GetInt16("flag_active"),
                                        end_working = (aa["end_working"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["end_working"]),
                                        reason = aa.GetString("reason"),
                                        picture = aa.GetString("picture"),
                                        salary_type = aa.GetInt16("salary_type"),
                                        tgl_mutasi = (aa["tgl_mutasi"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["tgl_mutasi"]),
                                        flag_managerial = aa.GetInt16("flag_managerial"),
                                        spv_code = aa.GetString("spv_code"),
                                        ////spv_name = aa.GetString("spv_name"),
                                        note1 = aa.GetString("note1"),
                                        note2 = aa.GetString("note2"),
                                        note3 = aa.GetString("note3"),
                                        entry_date = (aa["entry_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["entry_date"]),
                                        entry_user = aa.GetString("entry_user"),
                                        edit_date = (aa["edit_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["edit_date"]),
                                        edit_user = aa.GetString("edit_user")
                                    };
                                    vList.Add(m);
                                }
                            }
                        }
                    }
                }
                Log.Debug(DateTime.Now + " Jumlah Data Employee : " + vList.Count());
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetEmployeeList FAILED... ", ex);
            }

            var vRes = new ResponseModel();
            vRes.total_record = vJmlRecord;
            vRes.objResult = vList;

            return vRes;
        }

        public mEmployeeModel getEmployeeInfo(string pEmployeeCode, int pSeqNo)
        {
            var vModel = new mEmployeeModel();
            var strSQL = @"SELECT emp.employee_code,emp.seq_no,IFNULL(emp.nik,'') nik,IFNULL(emp.nip,'') nip,emp.employee_name,emp.employee_nick_name,
                                  emp.company_code,mc.int_company,mc.company_name,
                                  emp.branch_code,mbo.int_branch,mbo.branch_name,
                                  emp.department_code,md.int_department,md.department_name,
                                  IFNULL(emp.division_code,'') division_code,
                                  emp.title_code,mt.int_title,mt.title_name,
                                  IFNULL(emp.subtitle_code,'') subtitle_code,IFNULL(ms.int_subtitle,'') int_subtitle,IFNULL(ms.subtitle_name,'') subtitle_name,
                                  emp.level_code,ml.int_level,ml.level_name,
                                  emp.status_code,mes.int_status,mes.status_name,
                                  emp.flag_shiftable,
                                  emp.flag_transport,emp.place_birth,emp.date_birth,emp.sex,emp.religion,emp.marital_status,
                                  emp.no_of_children,emp.emp_address,
                                  IFNULL(emp.npwp,'') npwp,IFNULL(emp.kode_pajak,'') kode_pajak,IFNULL(emp.npwp_method,0) npwp_method,
                                  emp.npwp_registered_date,emp.npwp_address,emp.no_jamsostek,
                                  emp.jstk_registered_date,
                                  emp.bank_code,mba.bank_name,
                                  emp.bank_account,emp.bank_acc_name,emp.start_working,emp.appointment_date,
                                  emp.phone_number,emp.hp_number,emp.email,
                                  emp.country_code,mco.int_country,mco.country_name,
                                  IFNULL(emp.identity_number,'') identity_number,IFNULL(emp.last_education,'') last_education,
                                  IFNULL(emp.last_employment,'') last_employment,IFNULL(emp.description,'') description,emp.flag_active,
                                  emp.end_working,IFNULL(emp.reason,'') reason,
                                  '' picture,emp.salary_type,
                                  emp.tgl_mutasi,emp.flag_managerial,emp.spv_code,
                                  emp.note1,emp.note2,emp.note3,emp.entry_date,emp.entry_user,emp.edit_date,
                                  IFNULL(emp.edit_user,'') edit_user
                           FROM m_employee emp JOIN m_company mc ON emp.company_code = mc.company_code
                           JOIN m_branch_office mbo ON emp.branch_code = mbo.branch_code
                           JOIN m_department md ON emp.department_code = md.department_code
                           JOIN m_title mt ON emp.title_code = mt.title_code
                           JOIN m_country mco ON emp.country_code = mco.country_code
                           JOIN m_emp_status mes ON emp.status_code = mes.status_code
                           LEFT JOIN m_subtitle ms ON emp.subtitle_code = ms.subtitle_code
                           LEFT JOIN m_level ml ON emp.level_code = ml.level_code
                           LEFT JOIN m_bank mba ON emp.bank_code = mba.bank_code
                           WHERE emp.employee_code = @pEmployeeCode AND emp.seq_no = @pSeqNo";
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
                                    vModel.seq_no = aa.GetInt16("seq_no");
                                    vModel.nik = aa.GetString("nik");
                                    vModel.nip = aa.GetString("nip");
                                    vModel.employee_name = aa.GetString("employee_name");
                                    vModel.employee_nick_name = aa.GetString("employee_nick_name");
                                    vModel.company_code = aa.GetString("company_code");
                                    vModel.int_company = aa.GetString("int_company");
                                    vModel.company_name = aa.GetString("company_name");
                                    vModel.branch_code = aa.GetString("branch_code");
                                    vModel.int_branch = aa.GetString("int_branch");
                                    vModel.branch_name = aa.GetString("branch_name");
                                    vModel.department_code = aa.GetString("department_code");
                                    vModel.int_department = aa.GetString("int_department");
                                    vModel.department_name = aa.GetString("department_name");
                                    vModel.division_code = aa.GetString("division_code");
                                    vModel.title_code = aa.GetString("title_code");
                                    vModel.int_title = aa.GetString("int_title");
                                    vModel.title_name = aa.GetString("title_name");
                                    vModel.subtitle_code = aa.GetString("subtitle_code");
                                    vModel.int_subtitle = aa.GetString("int_subtitle");
                                    vModel.subtitle_name = aa.GetString("subtitle_name");
                                    vModel.level_code = aa.GetString("level_code");
                                    vModel.int_level = aa.GetString("int_level");
                                    vModel.level_name = aa.GetString("level_name");
                                    vModel.status_code = aa.GetString("status_code");
                                    vModel.int_status = aa.GetString("int_status");
                                    vModel.status_name = aa.GetString("status_name");
                                    vModel.flag_shiftable = aa.GetInt16("flag_shiftable");
                                    vModel.flag_transport = aa.GetInt16("flag_transport");
                                    vModel.place_birth = aa.GetString("place_birth");
                                    vModel.date_birth = (aa["date_birth"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["date_birth"]);
                                    vModel.sex = aa.GetInt16("sex");
                                    vModel.religion = aa.GetInt16("religion");
                                    vModel.marital_status = aa.GetInt16("marital_status");
                                    vModel.no_of_children = aa.GetInt16("no_of_children");
                                    vModel.emp_address = aa.GetString("emp_address");
                                    vModel.npwp = aa.GetString("npwp");
                                    vModel.kode_pajak = aa.GetString("kode_pajak");
                                    vModel.npwp_method = aa.GetInt16("npwp_method");
                                    vModel.npwp_registered_date = (aa["npwp_registered_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["npwp_registered_date"]);
                                    vModel.npwp_address = aa.GetString("npwp_address");
                                    vModel.no_jamsostek = aa.GetString("no_jamsostek");
                                    vModel.jstk_registered_date = (aa["jstk_registered_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["jstk_registered_date"]);
                                    vModel.bank_code = aa.GetString("bank_code");
                                    vModel.bank_name = aa.GetString("bank_name");
                                    vModel.bank_account = aa.GetString("bank_account");
                                    vModel.bank_acc_name = aa.GetString("bank_acc_name");
                                    vModel.start_working = (aa["start_working"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["start_working"]);
                                    vModel.appointment_date = (aa["appointment_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["appointment_date"]);
                                    vModel.phone_number = aa.GetString("phone_number");
                                    vModel.hp_number = aa.GetString("hp_number");
                                    vModel.email = aa.GetString("email");
                                    vModel.country_code = aa.GetString("country_code");
                                    vModel.int_country = aa.GetString("int_country");
                                    vModel.country_name = aa.GetString("country_name");
                                    vModel.identity_number = aa.GetString("identity_number");
                                    vModel.last_education = aa.GetString("last_education");
                                    vModel.last_employment = aa.GetString("last_employment");
                                    vModel.description = aa.GetString("description");
                                    vModel.flag_active = aa.GetInt16("flag_active");
                                    vModel.end_working = (aa["end_working"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["end_working"]);
                                    vModel.reason = aa.GetString("reason");
                                    vModel.picture = aa.GetString("picture");
                                    vModel.salary_type = aa.GetInt16("salary_type");
                                    vModel.tgl_mutasi = (aa["tgl_mutasi"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["tgl_mutasi"]);
                                    vModel.flag_managerial = aa.GetInt16("flag_managerial");
                                    vModel.spv_code = aa.GetString("spv_code");
                                    //vModel.spv_name = aa.GetString("spv_name");
                                    vModel.note1 = aa.GetString("note1");
                                    vModel.note2 = aa.GetString("note2");
                                    vModel.note3 = aa.GetString("note3");
                                    vModel.entry_date = (aa["entry_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["entry_date"]);
                                    vModel.entry_user = aa.GetString("entry_user");
                                    vModel.edit_date = (aa["edit_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["edit_date"]);
                                    vModel.edit_user = aa.GetString("edit_user");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetEmployeeInfo Failed", ex);
            }
            return vModel;
        }

        public void UpdateEmployee(mEmployeeModel pModel)
        {
            string SqlString = @"UPDATE `m_employee`
                                     SET `nik` = @pNik,
                                        `nip` = @pNip,
                                        `employee_name` = @pEmployeeName,
                                        `employee_nick_name` = @pEmployeeNickName,
                                        `company_code` = @pCompanyCode,
                                        `branch_code` = @pBranchCode,
                                         `department_code` = @pDepartmentCode,
                                        `division_code` = @pDivisionCode,
                                        `title_code` = @pTitleCode,
                                        `subtitle_code` = @pSubtitleCode,
                                        `level_code` = @pLevelCode,
                                        `status_code` = @pStatusCode,
                                        `flag_shiftable` = @pFlagShiftable,
                                         `flag_transport` = @pFlagTransport,
                                        `place_birth` = @pPlaceBirth,
                                        `date_birth` = @pDateBirth,
                                        `sex` = @pSex,
                                        `religion` = @pReligion,
                                        `marital_status` = @pMaritalStatus,
                                        `no_of_children` = @pNoOfChildren,
                                        `emp_address` = @pEmpAddress,
                                         `npwp` = @pNpwp,
                                        `kode_pajak` = @pKodePajak,
                                        `npwp_method` = @pNpwpMethod,
                                        `npwp_registered_date` = @pNpwpRegisteredDate,
                                        `npwp_address` = @pNpwpAddress,
                                        `no_jamsostek` = @pNoJamsostek,
                                        `jstk_registered_date` = @pJstkRegisteredDate,
                                         `bank_code` = @pBankCode,
                                        `bank_account` = @pBankAccount,
                                        `bank_acc_name` = @pBankAccName,
                                        `start_working` = @pStartWorking,
                                        `appointment_date` = @pAppointmentDate,
                                        `phone_number` = @pPhoneNumber,
                                        `hp_number` = @pHpNumber,
                                         `email` = @pEmail,
                                        `country_code` = @pCountryCode,
                                        `identity_number` = @pIdentityNumber,
                                        `last_education` = @pLastEducation,
                                        `last_employment` = @pLastEmployment,
                                        `description` = @pDescription,
                                        `flag_active` = @pFlagActive,
                                         `end_working` = @pEndWorking,
                                        `reason` = @pReason,
                                        `picture` = @pPicture,
                                        `salary_type` = @pSalaryType,
                                        `tgl_mutasi` = @pTglMutasi,
                                        `flag_managerial` = @pFlagManagerial,
                                        `spv_code` = @pSpvCode,
                                        `note1` = @pNote1,
                                        `note2` = @pNote2,
                                         `note3` = @pNote3,
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
                        cmd.Parameters.AddWithValue("@pNik", pModel.nik);
                        cmd.Parameters.AddWithValue("@pNip", pModel.nip);
                        cmd.Parameters.AddWithValue("@pEmployeeName", pModel.employee_name);
                        cmd.Parameters.AddWithValue("@pEmployeeNickName", pModel.employee_nick_name);
                        cmd.Parameters.AddWithValue("@pCompanyCode", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pBranchCode", pModel.branch_code);
                        cmd.Parameters.AddWithValue("@pDepartmentCode", pModel.department_code);
                        cmd.Parameters.AddWithValue("@pDivisionCode", pModel.division_code);
                        cmd.Parameters.AddWithValue("@pTitleCode", pModel.title_code);
                        cmd.Parameters.AddWithValue("@pSubtitleCode", pModel.subtitle_code);
                        cmd.Parameters.AddWithValue("@pLevelCode", pModel.level_code);
                        cmd.Parameters.AddWithValue("@pStatusCode", pModel.status_code);
                        cmd.Parameters.AddWithValue("@pFlagShiftable", pModel.flag_shiftable);
                        cmd.Parameters.AddWithValue("@pFlagTransport", pModel.flag_transport);
                        cmd.Parameters.AddWithValue("@pPlaceBirth", pModel.place_birth);
                        cmd.Parameters.AddWithValue("@pDateBirth", pModel.date_birth);
                        cmd.Parameters.AddWithValue("@pSex", pModel.sex);
                        cmd.Parameters.AddWithValue("@pReligion", pModel.religion);
                        cmd.Parameters.AddWithValue("@pMaritalStatus", pModel.marital_status);
                        cmd.Parameters.AddWithValue("@pNoOfChildren", pModel.no_of_children);
                        cmd.Parameters.AddWithValue("@pEmpAddress", pModel.emp_address);
                        cmd.Parameters.AddWithValue("@pNpwp", pModel.npwp);
                        cmd.Parameters.AddWithValue("@pKodePajak", pModel.kode_pajak);
                        cmd.Parameters.AddWithValue("@pNpwpMethod", pModel.npwp_method);
                        cmd.Parameters.AddWithValue("@pNpwpRegisteredDate", pModel.npwp_registered_date);
                        cmd.Parameters.AddWithValue("@pNpwpAddress", pModel.npwp_address);
                        cmd.Parameters.AddWithValue("@pNoJamsostek", pModel.no_jamsostek);
                        cmd.Parameters.AddWithValue("@pJstkRegisteredDate", pModel.jstk_registered_date);
                        cmd.Parameters.AddWithValue("@pBankCode", pModel.bank_code);
                        cmd.Parameters.AddWithValue("@pBankAccount", pModel.bank_account);
                        cmd.Parameters.AddWithValue("@pBankAccName", pModel.bank_acc_name);
                        cmd.Parameters.AddWithValue("@pStartWorking", pModel.start_working);
                        cmd.Parameters.AddWithValue("@pAppointmentDate", pModel.appointment_date);
                        cmd.Parameters.AddWithValue("@pPhoneNumber", pModel.phone_number);
                        cmd.Parameters.AddWithValue("@pHpNumber", pModel.hp_number);
                        cmd.Parameters.AddWithValue("@pEmail", pModel.email);
                        cmd.Parameters.AddWithValue("@pCountryCode", pModel.country_code);
                        cmd.Parameters.AddWithValue("@pIdentityNumber", pModel.identity_number);
                        cmd.Parameters.AddWithValue("@pLastEducation", pModel.last_education);
                        cmd.Parameters.AddWithValue("@pLastEmployment", pModel.last_employment);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pFlagActive", pModel.flag_active);
                        cmd.Parameters.AddWithValue("@pEndWorking", pModel.end_working);
                        cmd.Parameters.AddWithValue("@pReason", pModel.reason);
                        cmd.Parameters.AddWithValue("@pPicture", pModel.picture);
                        cmd.Parameters.AddWithValue("@pSalaryType", pModel.salary_type);
                        cmd.Parameters.AddWithValue("@pTglMutasi", pModel.tgl_mutasi);
                        cmd.Parameters.AddWithValue("@pFlagManagerial", pModel.flag_managerial);
                        cmd.Parameters.AddWithValue("@pSpvCode", pModel.spv_code);
                        cmd.Parameters.AddWithValue("@pNote1", pModel.note1);
                        cmd.Parameters.AddWithValue("@pNote2", pModel.note2);
                        cmd.Parameters.AddWithValue("@pNote3", pModel.note3);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE EMPLOYEE SUCCESS ====>>>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " UPDATE EMPLOYEE FAILED", ex);
            }

        }

        public void DeleteEmployee(mEmployeeModel pModel)
        {
            string SqlString = @"UPDATE m_employee 
                                    SET `flag_active` = @pFlagActive,
                                        `end_working` = @pEndWorking,
                                        `reason` = @pReason,
                                        `edit_date` = @pEditDate,
                                        `edit_user` = @pEditUser

                                 WHERE employee_code = @pCode AND seq_no = @pSeqNo";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCode", pModel.employee_code);
                        cmd.Parameters.AddWithValue("@pSeqNo", pModel.seq_no);
                        cmd.Parameters.AddWithValue("@pFlagActive", pModel.flag_active);
                        cmd.Parameters.AddWithValue("@pEndWorking", pModel.end_working);
                        cmd.Parameters.AddWithValue("@pReason", pModel.reason);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " DELETE EMPLOYEE SUCCESS ====>>>>>> Employee Code : " + pModel.employee_code + " Employee Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE EMPLOYEE FAILED", ex);
            }

        }

        public int getEmployeeSeqNo(string pEmployeeCode,string pCompanyCode)
        {
            var vSeqNo = 0;
            var strSQL = @"SELECT MAX(seqno) seqno
                           FROM m_empoloyee emp
                           WHERE emp.employee_code = @pEmployeeCode AND emp.company_code = @pCompanyCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pEmployeeCode", pEmployeeCode);
                        cmd.Parameters.AddWithValue("@pCompanyCode", pCompanyCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vSeqNo = aa.GetInt16("seq_no") + 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetEmployeeSeqNo Failed", ex);
            }
            return vSeqNo;
        }
    }
}