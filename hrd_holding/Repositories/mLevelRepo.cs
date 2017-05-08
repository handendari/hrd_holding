using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mLevelRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("LevelRepo");

        public void InsertLevel(mLevelModel pModel)
        {
            string SqlString = @"INSERT INTO `m_level`
                                        (`level_code`,`int_level`,`date_entry`,`date_edit`,`description`,`user_entry`,`user_edit`,`level_name`)
                                 VALUES (@pLevelCode,@pIntLevel,@pDateEntry,@pDateEdit,@pDescription,@pUserEntry,@pUserEdit,@pLevelName)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pLevelCode", pModel.level_code);
                        cmd.Parameters.AddWithValue("@pIntLevel", pModel.int_level);
                        cmd.Parameters.AddWithValue("@pDateEntry", pModel.date_entry);
                        cmd.Parameters.AddWithValue("@pDateEdit", pModel.date_edit);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pUserEntry", pModel.user_entry);
                        cmd.Parameters.AddWithValue("@pUserEdit", pModel.user_edit);
                        cmd.Parameters.AddWithValue("@pLevelName", pModel.level_name);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT LEVEL ====>>>> Code : " + pModel.level_code + " Name : " + pModel.level_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT LEVEL FAILED", ex);
            }

        }

        public List<mLevelModel> getLevelList()
        {
            var vList = new List<mLevelModel>();
            var strSQL = @"SELECT `level_code`,`int_level`,`level_name` ,`description`,`date_entry`,`user_entry`,`date_edit`,`user_edit`
                           FROM m_level";
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
                                    var m = new mLevelModel
                                    {
                                        level_code = aa.GetInt16("level_code"),
                                        int_level = aa.GetString("int_level"),
                                        level_name = aa.GetString("level_name"),
                                        description = aa.GetString("description"),
                                        date_entry = aa.GetDateTime("date_entry"),
                                        user_entry = aa.GetString("user_entry"),
                                        date_edit = aa.GetDateTime("date_edit"),
                                        user_edit = aa.GetString("user_edit")
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
                Log.Error(DateTime.Now + " GetLevelList FAILED... ", ex);
            }
            return vList;
        }

        public mLevelModel getLevelInfo(string pLevelCode)
        {
            var vModel = new mLevelModel();
            var strSQL = @"SELECT `level_code`,`int_level`,`level_name` ,`description`,`date_entry`,`user_entry`,`date_edit`,`user_edit`
                           FROM m_level
                           WHERE level_code = @pLevelCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pLevelCode", pLevelCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.level_code = aa.GetInt16("level_code");
                                    vModel.int_level = aa.GetString("int_level");
                                    vModel.level_name = aa.GetString("level_name");
                                    vModel.description = aa.GetString("description");
                                    vModel.date_entry = aa.GetDateTime("date_entry");
                                    vModel.user_entry = aa.GetString("user_entry");
                                    vModel.date_edit = aa.GetDateTime("date_edit");
                                    vModel.user_edit = aa.GetString("user_edit");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetLevelInfo Failed", ex);
            }
            return vModel;
        }

        public void UpdateLevel(mLevelModel pModel)
        {
            string SqlString = @"UPDATE `m_level`
                                       SET `int_level` = @pIntLevel,
                                           `date_entry` = @pDateEntry,
                                           `date_edit` = @pDateEdit,
                                           `description` = @pDescription,
                                           `user_entry` = @pUserEntry,
                                           `user_edit` = @pUserEdit,
                                           `level_name` = @pLevelName
                                 WHERE `level_code` = @pLevelCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pLevelCode", pModel.level_code);
                        cmd.Parameters.AddWithValue("@pIntLevel", pModel.int_level);
                        cmd.Parameters.AddWithValue("@pDateEntry", pModel.date_entry);
                        cmd.Parameters.AddWithValue("@pDateEdit", pModel.date_edit);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);
                        cmd.Parameters.AddWithValue("@pUserEntry", pModel.user_entry);
                        cmd.Parameters.AddWithValue("@pUserEdit", pModel.user_edit);
                        cmd.Parameters.AddWithValue("@pLevelName", pModel.level_name);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE LEVEL SUCCESS ====>>>>>> Code : " + pModel.level_code + " Name : " + pModel.level_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " UPDATE LEVEL FAILED", ex);
            }

        }

        public void DeleteLevel(string pCode)
        {
            string SqlString = @"DELETE m_level WHERE level_code = @pCode";

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
                        Log.Debug(DateTime.Now + " DELETE LEVEL SUCCESS ====>>>>>> Code : " + pCode);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE LEVEL FAILED", ex);
            }

        }
    }
}