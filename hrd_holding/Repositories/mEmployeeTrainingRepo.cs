﻿using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mEmployeeTrainingRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeTrainingRepo");

        public ResponseModel InsertEmployeeTraining(mEmployeeTrainingModel pModel)
        {
            var vResp = new ResponseModel();

            string SqlString = @"INSERT INTO `m_employee_training`
                                         (`employee_code`,`seq_no`,`start_date`,`end_date`,`material`,`organizer`,`place`,`company`,
                                          `chk_company`,`value`,`training_id`,`entry_date`,`entry_user`,`edit_date`,`edit_user`)
                                 VALUES (@pEmployeeCode,@pSeqNo,@pStartDate,@pEndDate,@pMaterial,@pOrganizer,@pPlace,@pCompany,
                                         @pChkCompany,@pValue,@pTrainingId,@pEntryDate,@pEntryUser,@pEditDate,@pEditUser)";

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
                        cmd.Parameters.AddWithValue("@pStartDate", pModel.start_date);
                        cmd.Parameters.AddWithValue("@pEndDate", pModel.end_date);
                        cmd.Parameters.AddWithValue("@pMaterial", pModel.material);
                        cmd.Parameters.AddWithValue("@pOrganizer", pModel.organizer);
                        cmd.Parameters.AddWithValue("@pPlace", pModel.place);
                        cmd.Parameters.AddWithValue("@pCompany", pModel.company);
                        cmd.Parameters.AddWithValue("@pChkCompany", pModel.chk_company);
                        cmd.Parameters.AddWithValue("@pValue", pModel.value);
                        cmd.Parameters.AddWithValue("@pTrainingId", pModel.training_id);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        vResp.isValid = true;
                        vResp.message = " INSERT EMPLOYEE TRAINING, Code : " + pModel.employee_code + " Name : " + pModel.employee_name;
                        Log.Debug(DateTime.Now + " INSERT EMPLOYEE TRAINING ====>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " INSERT EMPLOYEE TRAINING FAILED.....";
                Log.Error(DateTime.Now + " INSERT EMPLOYEE TRAINING FAILED", ex);
            }

            return vResp;
        }

        public List<mEmployeeTrainingModel> getEmployeeTrainingList(string pEmployeeCode)
        {
            var vList = new List<mEmployeeTrainingModel>();
            var strSQL = @"SELECT met.employee_code,emp.employee_name,met.seq_no,met.start_date,met.end_date,
                                  IFNULL(met.material,'') material,IFNULL(met.organizer,'') organizer,IFNULL(met.place,'') place,
                                  IFNULL(met.company,'') company,IFNULL(met.chk_company,0) chk_company,
                                  IFNULL(met.`value`,'') value,IFNULL(met.training_id,0) training_id,
                                  met.entry_date,IFNULL(met.entry_user,'') entry_user,
                                  met.edit_date,IFNULL(met.edit_user,'') edit_user
                           FROM m_employee_training met JOIN m_employee emp ON met.employee_code = emp.employee_code
                           WHERE met.employee_code = @pEmployeeCode";
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
                                    var m = new mEmployeeTrainingModel
                                    {
                                        employee_code = aa.GetString("employee_code"),
                                        employee_name = aa.GetString("employee_name"),
                                        seq_no = aa.GetInt16("seq_no"),
                                        start_date = (aa["start_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["start_date"]),
                                        end_date = (aa["end_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["end_date"]),
                                        material = aa.GetString("material"),
                                        organizer = aa.GetString("organizer"),
                                        place = aa.GetString("place"),
                                        company = aa.GetString("company"),
                                        chk_company = aa.GetInt16("chk_company"),
                                        value = aa.GetString("value"),
                                        training_id = aa.GetInt16("training_id"),
                                        entry_date = (aa["entry_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["entry_date"]),
                                        entry_user = aa.GetString("entry_user"),
                                        edit_date = (aa["edit_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["edit_date"]),
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
                Log.Error(DateTime.Now + " GetEmployeeTrainingList FAILED... ", ex);
            }

            Log.Debug(DateTime.Now + " JML Employee Training List: " + vList.Count);
            return vList;
        }

        public mEmployeeTrainingModel getEmployeeTrainingInfo(string pEmployeeCode,int pSeqNo)
        {
            var vModel = new mEmployeeTrainingModel();
            var strSQL = @"SELECT met.employee_code,emp.employee_name,met.seq_no,met.start_date,met.end_date,met.material,met.organizer,met.place,
                                  met.company,met.chk_company,met.value,met.training_id,met.entry_date,met.entry_user,met.edit_date,met.edit_user
                           FROM m_employee_training met JOIN m_employee emp ON met.employee_code = emp.employee_code
                           WHERE met.employee_code = @pEmployeeCode AND met.seq_no = @pSeqNo";
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
                                    vModel.start_date = aa.GetDateTime("start_date");
                                    vModel.end_date = aa.GetDateTime("end_date");
                                    vModel.material = aa.GetString("material");
                                    vModel.organizer = aa.GetString("organizer");
                                    vModel.place = aa.GetString("place");
                                    vModel.company = aa.GetString("company");
                                    vModel.chk_company = aa.GetInt16("chk_company");
                                    vModel.value = aa.GetString("value");
                                    vModel.training_id = aa.GetInt16("training_id");
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
                Log.Error(DateTime.Now + " GetEmployeeTrainingInfo Failed", ex);
            }
            return vModel;
        }

        public ResponseModel UpdateEmployeeTraining(mEmployeeTrainingModel pModel)
        {
            var vResp = new ResponseModel();
            string SqlString = @"UPDATE `m_employee_training`
                                      SET `start_date` = @pStartDate,
                                          `end_date` = @pEndDate,
                                          `material` = @pMaterial,
                                          `organizer` = @pOrganizer,
                                          `place` = @pPlace,
                                          `company` = @pCompany,
                                          `chk_company` = @pChkCompany,
                                          `value` = @pValue,
                                          `training_id` = @pTrainingId,
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
                        cmd.Parameters.AddWithValue("@pStartDate", pModel.start_date);
                        cmd.Parameters.AddWithValue("@pEndDate", pModel.end_date);
                        cmd.Parameters.AddWithValue("@pMaterial", pModel.material);
                        cmd.Parameters.AddWithValue("@pOrganizer", pModel.organizer);
                        cmd.Parameters.AddWithValue("@pPlace", pModel.place);
                        cmd.Parameters.AddWithValue("@pCompany", pModel.company);
                        cmd.Parameters.AddWithValue("@pChkCompany", pModel.chk_company);
                        cmd.Parameters.AddWithValue("@pValue", pModel.value);
                        cmd.Parameters.AddWithValue("@pTrainingId", pModel.training_id);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        vResp.isValid = true;
                        vResp.message = " UPDATE EMPLOYEE TRAINING SUCCESS, Code : " + pModel.employee_code + " Name : " + pModel.employee_name;
                        Log.Debug(DateTime.Now + " UPDATE EMPLOYEE TRAINING SUCCESS ====>>>>>> Code : " + pModel.employee_code + " Name : " + pModel.employee_name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " UPDATE EMPLOYEE TRAINING FAILED.....";
                Log.Error(DateTime.Now + " UPDATE EMPLOYEE TRAINING FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel DeleteEmployeeTraining(string pCode, int pSeqNo)
        {
            var vResp = new ResponseModel();

            string SqlString = @"DELETE FROM m_employee_training WHERE employee_code = @pCode AND seq_no = @pSeqNo";

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
                        vResp.isValid = true;
                        vResp.message = " DELETE EMPLOYEE TRAINING SUCCESS, Code : " + pCode;
                        Log.Debug(DateTime.Now + " DELETE EMPLOYEE TRAINING SUCCESS ====>>>>>> Code : " + pCode);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " DELETE EMPLOYEE TRAINING FAILED.....";
                Log.Error(DateTime.Now + " DELETE EMPLOYEE TRAINING FAILED", ex);
            }

            return vResp;
        }

        public int getEmployeeTrainingSeqNo(string pEmployeeCode)
        {
            var vSeqNo = 0;
            var strSQL = @"SELECT IFNULL(MAX(seq_no),0) seq_no
                           FROM m_employee_training emp
                           WHERE emp.employee_code = @pEmployeeCode";
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
                                    vSeqNo = aa.GetInt16("seq_no") + 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetEmployeeTrainingSeqNo Failed", ex);
            }
            return vSeqNo;
        }
    }
}