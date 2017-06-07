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

        public ResponseModel InsertEmployeeStatus(mEmployeeStatusModel pModel)
        {
            var vResp = new ResponseModel();

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
                        vResp.isValid = true;
                        vResp.message = " INSERT EMPLOYEE STATUS, Code : " + pModel.status_code + " Name : " + pModel.status_name;
                        Log.Debug(DateTime.Now + " INSERT EMPLOYEE STATUS ====>>>> Code : " + pModel.status_code + " Name : " + pModel.status_name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " INSERT EMPLOYEE STATUS FAILED......";

                Log.Error(DateTime.Now + " INSERT EMPLOYEE STATUS FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel getEmployeeStatusList(int? pStartRow = 0, int? pRows = 0, string pWhere = "", string pOrderBy = "")
        {
            var vList = new List<mEmployeeStatusModel>();
            var vLimit = pOrderBy + " LIMIT " + pStartRow + "," + pRows;

            var vJmlRecord = 0;

            var strSQLCount = @"SELECT COUNT(status_code) jml_record
                                FROM m_emp_status " + pWhere;

            var strSQL = @"SELECT `status_code`,`int_status`,`status_name`,`flag_period`,`kode_pajak`,`description`
                           FROM m_emp_status " + pWhere + " " + vLimit;
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

            var vResp = new ResponseModel();
            vResp.total_record = vJmlRecord;
            vResp.objResult = vList;

            return vResp;
        }

        public mEmployeeStatusModel getEmployeeStatusInfo(int pStatusCode)
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

        public ResponseModel UpdateEmployeeStatus(mEmployeeStatusModel pModel)
        {
            var vResp = new ResponseModel();

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
                        vResp.isValid = true;
                        vResp.message = " UPDATE EMPLOYEE STATUS SUCCESS, Code : " + pModel.status_code + " Name : " + pModel.status_name;
                        Log.Debug(DateTime.Now + " UPDATE EMPLOYEE STATUS SUCCESS ====>>>>>> Code : " + pModel.status_code + " Name : " + pModel.status_name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " UPDATE EMPLOYEE STATUS FAILED.....";
                Log.Error(DateTime.Now + " UPDATE EMPLOYEE STATUS FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel DeleteEmployeeStatus(int pCode)
        {
            var vResp = new ResponseModel();
            string SqlString = @"DELETE FROM m_emp_Status WHERE status_code = @pCode";

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
                        vResp.message = " DELETE EMPLOYEE STATUS SUCCESS, Code : " + pCode;
                        Log.Debug(DateTime.Now + " DELETE EMPLOYEE STATUS SUCCESS ====>>>>>> Code : " + pCode);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " DELETE EMPLOYEE STATUS FAILED....";
                Log.Error(DateTime.Now + " DELETE EMPLOYEE STATUS FAILED", ex);
            }
            return vResp;
        }

        public int getStatusSeqNo()
        {
            var vSeqNo = 0;
            var strSQL = @"SELECT IFNULL(MAX(status_code),0) seq_no
                           FROM m_emp_status emp";
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
                                    vSeqNo = aa.GetInt16("seq_no") + 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetStatusSeqNo Failed", ex);
            }
            return vSeqNo;
        }

    }
}