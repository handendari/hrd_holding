using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mEmployeeSkillRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeSkillRepo");

        public void InsertEmployeeSkill(mEmployeeSkillModel pModel)
        {
            string SqlString = @"INSERT INTO `m_employee_skill`
                                            (`employee_code`,`seq_no`,`skill`,`level`,`nm_level`,`flag_skill`,`description`,`entry_date`,
                                             `entry_user`,`edit_date`,`edit_user`)
                                 VALUES (@pEmployeeCode,@pSeqNo,@pSkill,@pLevel,@pNmLevel,@pFlagSkill,@pDescription,
                                         @pEntryDate,@pEntryUser,@pEditDate,@pEditUser)";


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
                        cmd.Parameters.AddWithValue("@pSkill", pModel.skill);
                        cmd.Parameters.AddWithValue("@pLevel", pModel.level);
                        cmd.Parameters.AddWithValue("@pNmLevel", pModel.nm_level);
                        cmd.Parameters.AddWithValue("@pFlagSkill", pModel.flag_skill);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT EMPLOYEE SKILL ====>>>> Code : " + pModel.employee_code);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT EMPLOYEE SKILL FAILED", ex);
            }

        }

        public List<mEmployeeSkillModel> getEmployeeSkillList(int pEmployeeCode)
        {
            var vList = new List<mEmployeeSkillModel>();
            var strSQL = @"SELECT mes.employee_code,emp.employee_name,mes.seq_no,mes.skill,mes.level,mes.nm_level,mes.flag_skill,
                                  mes.description,mes.entry_date,mes.entry_user,mes.edit_date,mes.edit_user
                           FROM m_employee_skill mes JOIN m_empoloyee emp ON mes.employee_code = emp.employee_code
                           WHERE mes.employee_code = @pEmployeeCode";
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
                                    var m = new mEmployeeSkillModel
                                    {
                                        employee_code = aa.GetString("employee_code"),
                                        employee_name = aa.GetString("employee_name"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        skill = aa.GetString("skill"),
                                        level = aa.GetInt16("level"),
                                        nm_level = aa.GetString("nm_level"),
                                        flag_skill = aa.GetInt16("flag_skill"),
                                        description = aa.GetString("description"),
                                        entry_date = aa.GetDateTime("entry_date"),
                                        entry_user = aa.GetString("entry_user"),
                                        edit_date = aa.GetDateTime("edit_date"),
                                        edit_user = aa.GetString("edit_user")
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
                Log.Error(DateTime.Now + " GetEmployeeSkillList FAILED... ", ex);
            }
            return vList;
        }

        public mEmployeeSkillModel getEmployeeSkillInfo(string pEmployeeCode, int pSeqNo)
        {
            var vModel = new mEmployeeSkillModel();
            var strSQL = @"SELECT mes.employee_code,emp.employee_name,mes.seq_no,mes.skill,mes.level,mes.nm_level,mes.flag_skill,
                                  mes.description,mes.entry_date,mes.entry_user,mes.edit_date,mes.edit_user
                           FROM m_employee_skill mes JOIN m_empoloyee emp ON mes.employee_code = emp.employee_code
                           WHERE mes.employee_code = @pEmployeeCode AND mes.seq_no = @pSeqNo";
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
                                    vModel.skill = aa.GetString("skill");
                                    vModel.level = aa.GetInt16("level");
                                    vModel.nm_level = aa.GetString("nm_level");
                                    vModel.flag_skill = aa.GetInt16("flag_skill");
                                    vModel.description = aa.GetString("description");
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
                Log.Error(DateTime.Now + " GetEmployeeSkillInfo Failed", ex);
            }
            return vModel;
        }

        public void UpdateEmployeeSkill(mEmployeeSkillModel pModel)
        {
            string SqlString = @"UPDATE `m_employee_skill`
                                      SET `skill` = @pSkill,
                                          `level` = @pLevel,
                                          `nm_level` = @pNmLevel,
                                          `flag_skill` = @pFlagSkill,
                                          `description` = @pDescription,
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
                        cmd.Parameters.AddWithValue("@pSkill", pModel.skill);
                        cmd.Parameters.AddWithValue("@pLevel", pModel.level);
                        cmd.Parameters.AddWithValue("@pNmLevel", pModel.nm_level);
                        cmd.Parameters.AddWithValue("@pFlagSkill", pModel.flag_skill);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE EMPLOYEE SKILL SUCCESS ====>>>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " UPDATE EMPLOYEE BRANCH FAILED", ex);
            }

        }

        public void DeleteEmployeeSkill(string pCode, int pSeqNo)
        {
            string SqlString = @"DELETE m_employee_Skill WHERE employee_code = @pCode AND seq_no = @pSeqNo";

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
                        Log.Debug(DateTime.Now + " DELETE EMPLOYEE SKILL SUCCESS ====>>>>>> Code : " + pCode + " SeqNo : " + pSeqNo);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE EMPLOYEE SKILL FAILED", ex);
            }

        }

    }
}