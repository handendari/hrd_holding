using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mTitleRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("TitleRepo");

        public void InsertLevel(mTitleModel pModel)
        {
            string SqlString = @"INSERT INTO `m_title`
                                            (`title_code`,`int_title`,`title_name`,`description`)
                                VALUES (@pTitleCode,@pIntTitle,@pTitleName,@pDescription)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pTitleCode", pModel.title_code);
                        cmd.Parameters.AddWithValue("@pIntTitle", pModel.int_title);
                        cmd.Parameters.AddWithValue("@pTitleName", pModel.title_name);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT TITLE ====>>>> Code : " + pModel.title_code + " Name : " + pModel.title_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT TITLE FAILED", ex);
            }

        }

        public List<mTitleModel> getTitleList()
        {
            var vList = new List<mTitleModel>();
            var strSQL = @"SELECT `title_code`,`int_title`,`title_name`,`description`
                           FROM m_title";
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
                                    var m = new mTitleModel
                                    {
                                        title_code = aa.GetInt16("title_code"),
                                        int_title = aa.GetString("int_title"),
                                        title_name = aa.GetString("title_name"),
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
                Log.Error(DateTime.Now + " GetTitleList FAILED... ", ex);
            }
            return vList;
        }

        public mTitleModel getTitleInfo(string pTitleCode)
        {
            var vModel = new mTitleModel();
            var strSQL = @"SELECT `title_code`,`int_title`,`title_name`,`description`
                           FROM m_title
                           WHERE title_code = @pTitleCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pTitleCode", pTitleCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.title_code = aa.GetInt16("title_code");
                                    vModel.int_title = aa.GetString("int_title");
                                    vModel.title_name = aa.GetString("title_name");
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

        public void UpdateTitle(mTitleModel pModel)
        {
            string SqlString = @"UPDATE `m_title`
                                    SET `int_title` = @pIntTitle,
                                        `title_name` = @pTitleName,
                                        `description` = @pDescription
                                 WHERE `title_code` = @pTitleCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pTitleCode", pModel.title_code);
                        cmd.Parameters.AddWithValue("@pIntTitle", pModel.int_title);
                        cmd.Parameters.AddWithValue("@pTitleName", pModel.title_name);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE TITLE SUCCESS ====>>>>>> Code : " + pModel.title_code + " Name : " + pModel.title_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " UPDATE TITLE FAILED", ex);
            }

        }

        public void DeleteTitle(string pCode)
        {
            string SqlString = @"DELETE m_title WHERE title_code = @pCode";

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
                        Log.Debug(DateTime.Now + " DELETE TITLE SUCCESS ====>>>>>> Code : " + pCode);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE TITLE FAILED", ex);
            }
        }
    }
}