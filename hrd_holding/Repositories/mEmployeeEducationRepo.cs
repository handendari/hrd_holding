﻿using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mEmployeeEducationRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeEducationRepo");

        public void InsertEmployeeEducation(mEmployeeEducationModel pModel)
        {
            string SqlString = @"INSERT INTO `m_employee_edu`
                                            (`employee_code`,`seq_no`,`start_year`,`end_year`,`jenjang`,`nm_jenjang`,`jurusan`,`school`,
                                             `city`,`country_code`,`entry_date`,`entry_user`,`edit_date`,`edit_user`)
                                VALUES (@pEmployeeCode,@pSeqNo,@PStartYear,@pEndYear,@pJenjang,@pNmJenjang,@pJurusan,@pSchool,
                                        @pCity,@pCountryCode,@pEntryDate,@pEntryUser,@pEditDate,@pEditUser)";


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
                        cmd.Parameters.AddWithValue("@PStartYear", pModel.start_year);
                        cmd.Parameters.AddWithValue("@pEndYear", pModel.end_year);
                        cmd.Parameters.AddWithValue("@pJenjang", pModel.jenjang);
                        cmd.Parameters.AddWithValue("@pNmJenjang", pModel.nm_jenjang);
                        cmd.Parameters.AddWithValue("@pJurusan", pModel.jurusan);
                        cmd.Parameters.AddWithValue("@pSchool", pModel.school);
                        cmd.Parameters.AddWithValue("@pCity", pModel.city);
                        cmd.Parameters.AddWithValue("@pCountryCode", pModel.country_code);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT EMPLOYEE EDUCATION SUCCESS ====>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT EMPLOYEE EDUCATION FAILED", ex);
            }

        }

        public List<mEmployeeEducationModel> getEmployeeEducationList(int pEmployeeCode)
        {
            var vList = new List<mEmployeeEducationModel>();
            var strSQL = @"SELECT mee.employee_code,emp.employee_name,mee.seq_no,mee.start_year,mee.end_year,
                                  mee.jenjang,mee.nm_jenjang,mee.jurusan,mee.school,mee.city,
                                  mee.country_code,mc.country_name,
                                  mee.entry_date,mee.entry_user,mee.edit_date,mee.edit_user
                           FROM m_employee_edu mee JOIN m_empoloyee emp ON mee.employee_code = emp.employee_code
                           LEFT JOIN m_country mc ON mee.country_code = mc.country_code
                           WHERE mee.employee_code = @pEmployeeCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pEmployeeCode", pEmployeeCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    var m = new mEmployeeEducationModel
                                    {
                                        employee_code = aa.GetString("employee_code"),
                                        employee_name = aa.GetString("employee_name"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        start_year = aa.GetDateTime("start_year"),
                                        end_year = aa.GetDateTime("end_year"),
                                        jenjang = aa.GetInt16("jenjang"),
                                        nm_jenjang = aa.GetString("nm_jenjang"),
                                        jurusan = aa.GetString("jurusan"),
                                        school = aa.GetString("school"),
                                        city = aa.GetString("city"),
                                        country_code = aa.GetString("country_code"),
                                        country_name = aa.GetString("country_name"),
                                        entry_date = aa.GetDateTime("entry_date"),
                                        entry_user = aa.GetString("entry_user"),
                                        edit_date = aa.GetDateTime("edit_date"),
                                        edit_user = aa.GetString("edit_user"),
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
                Log.Error(DateTime.Now + " GetEmployeeEducationList FAILED... ", ex);
            }
            return vList;
        }

        public mEmployeeEducationModel getEmployeeEducationInfo(string pEmployeeCode, int pSeqNo)
        {
            var vModel = new mEmployeeEducationModel();
            var strSQL = @"SELECT mee.employee_code,emp.employee_name,mee.seq_no,mee.start_year,mee.end_year,
                                  mee.jenjang,mee.nm_jenjang,mee.jurusan,mee.school,mee.city,
                                  mee.country_code,mc.country_name,
                                  mee.entry_date,mee.entry_user,mee.edit_date,mee.edit_user
                           FROM m_employee_edu mee JOIN m_empoloyee emp ON mee.employee_code = emp.employee_code
                           LEFT JOIN m_country mc ON mee.country_code = mc.country_code
                           WHERE mee.employee_code = @pEmployeeCode AND mee.seq_no = @pSeqNo";
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
                                    vModel.employee_name = aa.GetString("employee_name");
                                    vModel.seq_no = aa.GetInt16("seq_no");
                                    vModel.start_year = aa.GetDateTime("start_year");
                                    vModel.end_year = aa.GetDateTime("end_year");
                                    vModel.jenjang = aa.GetInt16("jenjang");
                                    vModel.nm_jenjang = aa.GetString("nm_jenjang");
                                    vModel.jurusan = aa.GetString("jurusan");
                                    vModel.school = aa.GetString("school");
                                    vModel.city = aa.GetString("city");
                                    vModel.country_code = aa.GetString("country_code");
                                    vModel.country_name = aa.GetString("country_name");
                                    vModel.entry_date = aa.GetDateTime("entry_date");
                                    vModel.entry_user = aa.GetString("entry_user");
                                    vModel.edit_date = aa.GetDateTime("edit_date");
                                    vModel.edit_user = aa.GetString("edit_user");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetEmployeeEducationInfo Failed", ex);
            }
            return vModel;
        }

        public void UpdateEmployeeEducation(mEmployeeEducationModel pModel)
        {
            string SqlString = @"UPDATE `m_employee_edu`
                                    SET `start_year` = @pStartYear,
                                        `end_year` = @pEndYear,
                                        `jenjang` = @pJenjang,
                                        `nm_jenjang` = @pNmJenjang,
                                        `jurusan` = @pJurusan,
                                        `school` = @pSchool,
                                        `city` = @pCity,
                                        `country_code` = @pCountryCode,
                                        `entry_date` = @pEntryDate,
                                        `entry_user` = @pEntryUser,
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
                        cmd.Parameters.AddWithValue("@PStartYear", pModel.start_year);
                        cmd.Parameters.AddWithValue("@pEndYear", pModel.end_year);
                        cmd.Parameters.AddWithValue("@pJenjang", pModel.jenjang);
                        cmd.Parameters.AddWithValue("@pNmJenjang", pModel.nm_jenjang);
                        cmd.Parameters.AddWithValue("@pJurusan", pModel.jurusan);
                        cmd.Parameters.AddWithValue("@pSchool", pModel.school);
                        cmd.Parameters.AddWithValue("@pCity", pModel.city);
                        cmd.Parameters.AddWithValue("@pCountryCode", pModel.country_code);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE EMPLOYEE EDUCATION SUCCESS ====>>>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " UPDATE EMPLOYEE EDUCATION FAILED", ex);
            }

        }

        public void DeleteEmployeeEducation(string pCode, int pSeqNo)
        {
            string SqlString = @"DELETE m_employee_education WHERE employee_code = @pCode AND seq_no = @pSeqNo";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCode", pCode);
                        cmd.Parameters.AddWithValue("@pSeqNo", pSeqNo);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " DELETE EMPLOYEE EDUCATION SUCCESS ====>>>>>> Code : " + pCode + " NoSeq : " + pSeqNo);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE EMPLOYEE EDUCATION FAILED", ex);
            }

        }
    }
}