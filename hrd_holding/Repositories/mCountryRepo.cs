using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mCountryRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("CountryRepo");

        public void InsertCountry(mCountryModel pModel)
        {
            string SqlString = @"INSERT INTO m_country (country_code,int_country, int_code,country_name,description)
                                            VALUES (@pCountryCode,@pIntCountry,@pIntCode,@pCountryName,@pDesc)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pCountryCode", pModel.country_code);
                        cmd.Parameters.AddWithValue("@pIntCountry", pModel.int_country);
                        cmd.Parameters.AddWithValue("@pIntCode", pModel.int_code);
                        cmd.Parameters.AddWithValue("@pCountryName", pModel.country_name);
                        cmd.Parameters.AddWithValue("@pDesc", pModel.description);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT COUNTRY SUCCESS ====>>>> Code : " + pModel.country_code + " Name : " + pModel.country_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT COUNTRY FAILED", ex);
            }
        }

        public ResponseModel getCountryList(int? pStartRow = 0, int? pRows = 0, string pWhere = "", string pOrderBy = "")
        {
            var vLimit = pWhere + pOrderBy + " LIMIT " + pStartRow + "," + pRows;

            var vJmlRecord = 0;
            var vList = new List<mCountryModel>();

            var strSQLCount = @"SELECT COUNT(country_code) jml_record
                                FROM m_country " + pWhere;

            var strSQL = @"SELECT country_code,int_country, int_code,country_name,description
                            FROM m_country " + vLimit;
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
                                    var m = new mCountryModel
                                    {
                                        country_code = aa.GetInt16(0),
                                        int_country = aa.GetString(1),
                                        int_code = aa.GetInt16(2),
                                        country_name = aa.GetString(3),
                                        description = aa.GetString(4)
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
                Log.Error(DateTime.Now + " GetCountryList Failed",ex);
            }

            var vRes = new ResponseModel();
            vRes.total_record = vJmlRecord;
            vRes.objResult = vList;

            return vRes;
        }

        public mCountryModel getCountryInfo(string pCountryCode)
        {
            var vModel = new mCountryModel();
            var strSQL = @"SELECT country_code,int_country, int_code,country_name,description
                           FROM m_country 
                           WHERE country_code = @pCountryCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCountryCode", pCountryCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.country_code = aa.GetInt16(0);
                                    vModel.int_country = aa.GetString(1);
                                    vModel.int_code = aa.GetInt16(2);
                                    vModel.country_name = aa.GetString(3);
                                    vModel.description = aa.GetString(4);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetCountryList Failed", ex);
            }
            return vModel;
        }

        public void UpdateCountry(mCountryModel pModel)
        {
            string SqlString = @"UPDATE m_country SET int_country = @pIntCountry, int_code = @pIntCode,
                                                      country_name = @pCountryName,description = @pDesc
                                WHERE country_code = @pCountryCode";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pCountryCode", pModel.country_code);
                        cmd.Parameters.AddWithValue("@pIntCountry", pModel.int_country);
                        cmd.Parameters.AddWithValue("@pIntCode", pModel.int_code);
                        cmd.Parameters.AddWithValue("@pCountryName", pModel.country_name);
                        cmd.Parameters.AddWithValue("@pDesc", pModel.description);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE COUNTRY SUCCESS ====>>>>>> Code : " + pModel.country_code + " Name : " + pModel.country_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT COUNTRY FAILED", ex);
            }
        }

        public void DeleteCountry(string pCountryCode)
        {
            string SqlString = @"DELETE m_country WHERE country_code = @pCountryCode";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCountryCode", pCountryCode);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " DELETE COUNTRY SUCCESS ====>>>>>> Code : " + pCountryCode);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE COUNTRY FAILED", ex);
            }
        }
    }
}