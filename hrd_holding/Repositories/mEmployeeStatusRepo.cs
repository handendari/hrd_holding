using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mEmployeeStatusRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeStatusRepo");

        public void InsertEmployeeStatus(mEmployeeStatusModel pModel)
        {
            string SqlString = @"INSERT INTO `m_emp_status`
                                            (`status_code`,`int_status`,`status_name`,`flag_period`,`kode_pajak`,`description`)
                                 VALUES (@pStatusCode,@pIntStatus,@pStatusName,@pFlagPeriod,@pKodePajak,@pDescription)";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pStatusCode", pModel.status_code);
                        cmd.Parameters.AddWithValue("@pIntStatus", pModel.int_status);
                        cmd.Parameters.AddWithValue("@pStatusName", pModel.status_name);
                        cmd.Parameters.AddWithValue("@pFlagPeriod", pModel.flag_period);
                        cmd.Parameters.AddWithValue("@pKodePajak", pModel.kode_pajak);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT EMPLOYEE STATUS ====>>>> Code : " + pModel.status_code + " Name : " + pModel.status_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT EMPLOYEE STATUS FAILED", ex);
            }

        }

        public List<mEmployeeStatusModel> getEmployeeStatusList()
        {
            var vList = new List<mEmployeeStatusModel>();
            var strSQL = @"SELECT `status_code`,`int_status`,`status_name`,`flag_period`,`kode_pajak`,`description`
                           FROM m_emp_status";
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
                                    var m = new mEmployeeStatusModel
                                    {
                                        status_code = aa.GetInt16("status_code"),
                                        int_status = aa.GetString("int_status"),
                                        status_name = aa.GetString("status_name"),
                                        flag_period = aa.GetInt16("flag_period"),
                                        kode_pajak = aa.GetString("kode_pajak"),
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
                Log.Error(DateTime.Now + " GetEmployeeStatusList FAILED... ", ex);
            }
            return vList;
        }

        public mEmployeeStatusModel getEmployeeStatusInfo(string pStatusCode)
        {
            var vModel = new mEmployeeStatusModel();
            var strSQL = @"SELECT status_code,int_status,status_name,flag_period,kode_pajak,description
                           FROM m_emp_status
                           WHERE status_code = @pStatusCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pStatusCode", pStatusCode);
                        
                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.status_code = aa.GetInt16("status_code");
                                    vModel.int_status = aa.GetString("int_status");
                                    vModel.status_name = aa.GetString("status_name");
                                    vModel.flag_period = aa.GetInt16("flag_period");
                                    vModel.kode_pajak = aa.GetString("kode_pajak");
                                    vModel.description = aa.GetString("description");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetEmployeeStatusInfo Failed", ex);
            }
            return vModel;
        }

        public void UpdateEmployeeStatus(mEmployeeStatusModel pModel)
        {
            string SqlString = @"UPDATE `m_emp_status`
                                       SET `int_status` = @pIntStatus,
                                           `status_name` = @pStatusName,
                                           `flag_period` = @pFlagPeriod,
                                           `kode_pajak` = @pKodePajak,
                                           `description` = @pDescription
                                 WHERE `status_code` = @pStatusCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pStatusCode", pModel.status_code);
                        cmd.Parameters.AddWithValue("@pIntStatus", pModel.int_status);
                        cmd.Parameters.AddWithValue("@pStatusName", pModel.status_name);
                        cmd.Parameters.AddWithValue("@pFlagPeriod", pModel.flag_period);
                        cmd.Parameters.AddWithValue("@pKodePajak", pModel.kode_pajak);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE EMPLOYEE STATUS SUCCESS ====>>>>>> Code : " + pModel.status_code + " Name : " + pModel.status_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " UPDATE EMPLOYEE STATUS FAILED", ex);
            }

        }

        public void DeleteEmployeeStatus(string pCode)
        {
            string SqlString = @"DELETE m_emp_Status WHERE status_code = @pCode";

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
                        Log.Debug(DateTime.Now + " DELETE EMPLOYEE STATUS SUCCESS ====>>>>>> Code : " + pCode);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE EMPLOYEE STATUS FAILED", ex);
            }

        }

    }
}