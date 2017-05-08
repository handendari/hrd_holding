using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mBankRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("BankRepo");

        public void InsertBank(mBankModel pModel)
        {
            string SqlString = @"INSERT INTO `m_bank`
                                        (`bank_code`,`bank_name`,`description`)
                                 VALUES (@pBankCode,@pBankName,@pDescription)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;
                       
                        cmd.Parameters.AddWithValue("@pBankCode", pModel.bank_code);
                        cmd.Parameters.AddWithValue("@pBankName", pModel.bank_name);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT BANK ====>>>> Code : " + pModel.bank_code + " Name : " + pModel.bank_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT BANK FAILED", ex);
            }

        }

        public List<mBankModel> getBankList()
        {
            var vList = new List<mBankModel>();
            var strSQL = @"SELECT `bank_code`,`bank_name`,`description` FROM m_bank";
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
                                    var m = new mBankModel
                                    {
                                        bank_code = aa.GetString("bank_code"),
                                        bank_name = aa.GetString("bank_name"),
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
                Log.Error(DateTime.Now + " GetBankList FAILED... ", ex);
            }
            return vList;
        }

        public mBankModel getBankInfo(string pBankCode)
        {
            var vModel = new mBankModel();
            var strSQL = @"SELECT `bank_code`,`bank_name`,`description` 
                           FROM m_bank
                           WHERE bank_code = @pBankCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pBankCode", pBankCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.bank_code = aa.GetString("bank_code");
                                    vModel.bank_name = aa.GetString("bank_name");
                                    vModel.description = aa.GetString("description");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetBankInfo Failed", ex);
            }
            return vModel;
        }

        public void UpdateBank(mBankModel pModel)
        {
            string SqlString = @"UPDATE `m_bank`
                                      SET `bank_name` = @pBankName,
                                          `description`= @pDescription
                                 WHERE `bank_code` = @pBankCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pBankCode", pModel.bank_code);
                        cmd.Parameters.AddWithValue("@pBankName", pModel.bank_name);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE BANK SUCCESS ====>>>>>> Code : " + pModel.bank_code + " Name : " + pModel.bank_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " UPDATE BANK FAILED", ex);
            }

        }

        public void DeleteBank(string pCode)
        {
            string SqlString = @"DELETE m_bank WHERE bank_code = @pCode";

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
                        Log.Debug(DateTime.Now + " DELETE BANK SUCCESS ====>>>>>> Code : " + pCode);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE BANK FAILED", ex);
            }

        }

    }
}