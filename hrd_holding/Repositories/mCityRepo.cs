using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mCityRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("CityRepo");

        public void InsertCity(mCityModel pModel)
        {
            string SqlString = @"INSERT INTO m_city (city_code,city_name,entry_date,deleted,delete_date)
                                            VALUES (@pCityCode,@pCityName,@pEntryDate,@pDeleted,@pDeleteDate)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pCityCode", pModel.city_code);
                        cmd.Parameters.AddWithValue("@pCityName", pModel.city_name);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pDeleted", pModel.deleted);
                        cmd.Parameters.AddWithValue("@pDeleteDate", pModel.delete_date);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT CITY SUCCESS ====>>>> Code : " + pModel.city_code + " Name : " + pModel.city_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT CITY FAILED", ex);
            }
        }

        public List<mCityModel> getCityList()
        {
            var vList = new List<mCityModel>();
            var strSQL = @"SELECT city_code,city_name,entry_date,deleted,delete_date
                            FROM m_city";
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
                                    var m = new mCityModel
                                    {
                                        city_code = aa.GetString("city_code"),
                                        city_name = aa.GetString("city_name"),
                                        entry_date = aa.GetDateTime("entry_date"),
                                        deleted = aa.GetInt16("deleted"),
                                        delete_date = aa.GetDateTime("delete_date")
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
                Log.Error(DateTime.Now + " GET CITY LIST FAILED... ", ex);
            }
            return vList;
        }

        public mCityModel getCityInfo(string pCityCode)
        {
            var vModel = new mCityModel();
            var strSQL = @"SELECT city_code,city_name,entry_date,deleted,delete_date
                           FROM m_city 
                           WHERE city_code = @pCityCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCityCode", pCityCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.city_code = aa.GetString("city_code");
                                    vModel.city_name = aa.GetString("city_name");
                                    vModel.entry_date = aa.GetDateTime("entry_date");
                                    vModel.deleted = aa.GetInt16("deleted");
                                    vModel.delete_date = aa.GetDateTime("delete_date");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetCityList Failed", ex);
            }
            return vModel;
        }

        public void UpdateCity(mCityModel pModel)
        {
            string SqlString = @"UPDATE m_city SET city_name = @pCityName,entry_date = @pEntryDate,deleted = @pDeleted,delete_date = @pDeleteDate
                                WHERE city_code = @pCityCode";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pCityCode", pModel.city_code);
                        cmd.Parameters.AddWithValue("@pCityName", pModel.city_name);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pDeleted", pModel.deleted);
                        cmd.Parameters.AddWithValue("@pDeleteDate", pModel.delete_date);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE CITY SUCCESS ====>>>>>> Code : " + pModel.city_code + " Name : " + pModel.city_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT CITY FAILED", ex);
            }
        }

        public void DeleteCity(string pCityCode)
        {
            string SqlString = @"DELETE m_city WHERE city_code = @pCityCode";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCityCode", pCityCode);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " DELETE CITY SUCCESS ====>>>>>> Code : " + pCityCode);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE CITY FAILED", ex);
            }
        }
    }
}