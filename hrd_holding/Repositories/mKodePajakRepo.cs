using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mKodePajakRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("KodePajakRepo");

        public ResponseModel InsertKodePajak(mKodePajakModel pModel)
        {
            var vResp = new ResponseModel();

            string SqlString = @"INSERT INTO m_kode_pajak
                                            (kode_pajak,flag_status,description)
                                VALUES (@pKode,@pFlagStatus,@pDescription)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pKode", pModel.kode_pajak);
                        cmd.Parameters.AddWithValue("@pFlagStatus", pModel.flag_status);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);

                        var status = cmd.ExecuteNonQuery();
                        vResp.isValid = true;
                        vResp.message = " INSERT KODE PAJAK, Code : " + pModel.kode_pajak + " Name : " + pModel.description;
                        Log.Debug(DateTime.Now + " INSERT KODE PAJAK ====>>>> Code : " + pModel.kode_pajak + " Name : " + pModel.description);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " INSERT KODE PAJAK FAILED.....";
                Log.Error(DateTime.Now + " INSERT  KODE PAJAK FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel getKodePajakList(int? pStartRow = 0, int? pRows = 0, string pWhere = "", string pOrderBy = "")
        {

            var vList = new List<mKodePajakModel>();

            var vLimit = pOrderBy + " LIMIT " + pStartRow + "," + pRows;
            var vJmlRecord = 0;

            var strSQLCount = @"SELECT COUNT(kode_pajak) jml_record
                                FROM m_kode_pajak " + pWhere;

            var strSQL = @"SELECT kode_pajak,flag_status,description
                           FROM m_kode_pajak " + pWhere + " " + vLimit;
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
                                    var m = new mKodePajakModel
                                    {
                                        kode_pajak = aa.GetString("kode_pajak"),
                                        flag_status = aa.GetInt16("flag_status"),
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
                Log.Error(DateTime.Now + " GetKodePajakList FAILED... ", ex);
            }

            var vResp = new ResponseModel();
            vResp.total_record = vJmlRecord;
            vResp.objResult = vList;

            return vResp;
        }

        public mKodePajakModel getKodePajakInfo(string pCode)
        {
            var vModel = new mKodePajakModel();
            var strSQL = @"SELECT kode_pajak,flag_status,IFNULL(description,'') description
                           FROM m_kode_pajak
                           WHERE kode_pajak = @pCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCode", pCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.kode_pajak = aa.GetString("title_code");
                                    vModel.flag_status = aa.GetInt16("flag_status");
                                    vModel.description = aa.GetString("description");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetTitleInfo Failed", ex);
            }
            return vModel;
        }

        public ResponseModel UpdateKodePajak(mKodePajakModel pModel)
        {
            var vResp = new ResponseModel();

            string SqlString = @"UPDATE m_kode_pajak
                                    SET flag_status = @pFlagStatus,
                                        description = @pDescription
                                 WHERE kode_pajak = @pCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pCode", pModel.kode_pajak);
                        cmd.Parameters.AddWithValue("@pFlagStatus", pModel.flag_status);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);

                        var status = cmd.ExecuteNonQuery();
                        vResp.isValid = true;
                        vResp.message = " UPDATE KODE PAJAK SUCCESS, Code : " + pModel.kode_pajak + " Name : " + pModel.description;
                        Log.Debug(DateTime.Now + " UPDATE KODE PAJAK SUCCESS ====>>>>>> Code : " + pModel.kode_pajak + " Name : " + pModel.description);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " UPDATE KODE PAJAK FAILED.....";
                Log.Error(DateTime.Now + " UPDATE KODE PAJAK FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel DeleteKodePajak(int pCode)
        {
            var vResp = new ResponseModel();
            string SqlString = @"DELETE FROM m_kode_pajak WHERE kode_pajak = @pCode";

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
                        vResp.message = " DELETE KODE PAJAK SUCCESS, Code : " + pCode;

                        Log.Debug(DateTime.Now + " DELETE KODE PAJAK SUCCESS ====>>>>>> Code : " + pCode);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " DELETE KODE PAJAK FAILED......";

                Log.Error(DateTime.Now + " DELETE PAJAK FAILED", ex);
            }

            return vResp;
        }

    }
}