using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mSubtitleRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("SubtitleRepo");

        public ResponseModel InsertLevel(mSubtitleModel pModel)
        {
            var vResp = new ResponseModel();

            string SqlString = @"INSERT INTO `m_subtitle`
                                            (`subtitle_code`,`title_code`,`int_subtitle`,`subtitle_name`,`description`)
                                VALUES (@pSubtitleCode,@pTitleCode,@pIntSubtitle,@pSubtitleName,@pDescription)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pSubtitleCode", pModel.subtitle_code);
                        cmd.Parameters.AddWithValue("@pTitleCode", pModel.title_code);
                        cmd.Parameters.AddWithValue("@pIntSubtitle", pModel.int_subtitle);
                        cmd.Parameters.AddWithValue("@pSubtitleName", pModel.subtitle_name);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);

                        var status = cmd.ExecuteNonQuery();

                        vResp.isValid = true;
                        vResp.message = " INSERT SUBTITLE, Code : " + pModel.subtitle_code + " Name : " + pModel.subtitle_name;
                        Log.Debug(DateTime.Now + " INSERT SUBTITLE ====>>>> Code : " + pModel.subtitle_code + " Name : " + pModel.subtitle_name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " INSERT SUBTITLE, Code : " + pModel.subtitle_code + " Name : " + pModel.subtitle_name;
                Log.Error(DateTime.Now + " INSERT SUBTITLE FAILED", ex);
            }

            return vResp;
        }

        public List<mSubtitleModel> getSubtitleList(int pTitleCode)
        {
            var vList = new List<mSubtitleModel>();
            var strSQL = @"SELECT ms.subtitle_code,ms.title_code,ms.title_name, ms.int_subtitle,ms.subtitle_name,ms.description
                           FROM m_subtitle ms JOIN m_title mt ON ms.title_code = mt.title_code
                           WHERE ms.title_code = @pTitleCode";
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
                                    var m = new mSubtitleModel
                                    {
                                        subtitle_code = aa.GetInt16("subtitle_code"),
                                        title_code = aa.GetInt16("title_code"),
                                        title_name = aa.GetString("title_name"),
                                        int_subtitle = aa.GetString("int_subtitle"),
                                        subtitle_name = aa.GetString("subtitle_name"),
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
                Log.Error(DateTime.Now + " GetSubtitleList FAILED... ", ex);
            }
            return vList;
        }

        public mSubtitleModel getSubtitleInfo(string pSubtitleCode)
        {
            var vModel = new mSubtitleModel();
            var strSQL = @"SELECT ms.subtitle_code,ms.title_code,ms.title_name, ms.int_subtitle,ms.subtitle_name,ms.description
                           FROM m_subtitle ms JOIN m_title mt ON ms.title_code = mt.title_code
                           WHERE ms.subtitle_code = @pSubtitleCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pSubtitleCode", pSubtitleCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.subtitle_code = aa.GetInt16("subtitle_code");
                                    vModel.title_code = aa.GetInt16("title_code");
                                    vModel.title_name = aa.GetString("title_name");
                                    vModel.int_subtitle = aa.GetString("int_subtitle");
                                    vModel.subtitle_name = aa.GetString("subtitle_name");
                                    vModel.description = aa.GetString("description");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetSubtitleInfo Failed", ex);
            }
            return vModel;
        }

        public ResponseModel UpdateSubtitle(mSubtitleModel pModel)
        {
            var vResp = new ResponseModel();

            string SqlString = @"UPDATE `m_subtitle`
                                      SET `title_code` = @pTitleCode,
                                          `int_subtitle` = @pIntSubtitle,
                                          `subtitle_name` = @pSubtitleName,
                                          `description` = @pDescription
                                 WHERE `subtitle_code` = @pSubtitleCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pSubtitleCode", pModel.subtitle_code);
                        cmd.Parameters.AddWithValue("@pTitleCode", pModel.title_code);
                        cmd.Parameters.AddWithValue("@pIntSubtitle", pModel.int_subtitle);
                        cmd.Parameters.AddWithValue("@pSubtitleName", pModel.subtitle_name);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);

                        var status = cmd.ExecuteNonQuery();
                        vResp.isValid = true;
                        vResp.message = " UPDATE SUBTITLE SUCCESS, Code : " + pModel.subtitle_code + " Name : " + pModel.subtitle_name;
                        Log.Debug(DateTime.Now + " UPDATE SUBTITLE SUCCESS ====>>>>>> Code : " + pModel.subtitle_code + " Name : " + pModel.subtitle_name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " UPDATE SUBTITLE FAILED.....";
                Log.Error(DateTime.Now + " UPDATE SUBTITLE FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel DeleteSubtitle(string pCode)
        {
            var vResp = new ResponseModel();

            string SqlString = @"DELETE m_subtitle WHERE subtitle_code = @pCode";

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
                        vResp.message = " DELETE SUBTITLE SUCCESS, Code : " + pCode;
                        Log.Debug(DateTime.Now + " DELETE SUBTITLE SUCCESS ====>>>>>> Code : " + pCode);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " DELETE SUBTITLE FAILED......";

                Log.Error(DateTime.Now + " DELETE SUBTITLE FAILED", ex);
            }

            return vResp;
        }
    }
}