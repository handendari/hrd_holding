﻿using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mCompanyRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("CompanyRepo");

        public void InsertCompany(mCompanyModel pModel)
        {
            string SqlString = @"INSERT INTO m_company (company_code,int_company,country_code,company_name,address,postal_code,
                                                        city_name,state,phone_number,fax_number,web_address,email_address,picture,
                                                        npwp,pimpinan,pimpinan_npwp,npp,jhk,entry_date,entry_user,edit_date,edit_user)
                                 VALUES (@pCompanyCode,@pIntCompany,@pCountryCode,@pCompanyName,@pAddress,@pPostalCode,
                                         @pCityName,@pState,@pPhoneNumber,@pFaxNumber,@pWebAddress,@pEmailAddress,@pPicture,
                                         @pNpwp,@pPimpinan,@pPimpinanNpwp,@pNpp,@pJhk,@pEntryDate,@pEntryUser,@pEditDate,@pEditUser)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pCompanyCode", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pIntCompany", pModel.int_company);
                        cmd.Parameters.AddWithValue("@pCountryCode", pModel.country_code);
                        cmd.Parameters.AddWithValue("@pCompanyName", pModel.company_name);
                        cmd.Parameters.AddWithValue("@pAddress", pModel.address);
                        cmd.Parameters.AddWithValue("@pPostalCode", pModel.postal_code);
                        cmd.Parameters.AddWithValue("@pCityName", pModel.city_name);
                        cmd.Parameters.AddWithValue("@pState", pModel.state);
                        cmd.Parameters.AddWithValue("@pPhoneNumber", pModel.phone_number);
                        cmd.Parameters.AddWithValue("@pFaxNumber", pModel.fax_number);
                        cmd.Parameters.AddWithValue("@pWebAddress", pModel.web_address);
                        cmd.Parameters.AddWithValue("@pEmailAddress", pModel.email_address);
                        cmd.Parameters.AddWithValue("@pPicture", pModel.picture);
                        cmd.Parameters.AddWithValue("@pNpwp", pModel.npwp);
                        cmd.Parameters.AddWithValue("@pPimpinan", pModel.pimpinan);
                        cmd.Parameters.AddWithValue("@pPimpinanNpwp", pModel.pimpinan_npwp);
                        cmd.Parameters.AddWithValue("@pNpp", pModel.npp);
                        cmd.Parameters.AddWithValue("@pJhk", pModel.jhk);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT COMPANY SUCCESS ====>>>> Code : " + pModel.company_code + " Name : " + pModel.company_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT COMPANY FAILED", ex);
            }

        }

        public List<mCompanyModel> getCompanyList()
        {
            var vList = new List<mCompanyModel>();
            var strSQL = @"SELECT company_code,int_company,country_code,company_name,address,postal_code,
                                city_name,state,phone_number,fax_number,web_address,email_address,picture,
                                npwp,pimpinan,pimpinan_npwp,npp,jhk,entry_date,entry_user,edit_date,edit_user
                            FROM m_company";
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
                                    var m = new mCompanyModel
                                    {
                                        company_code = aa.GetInt16("company_code"),
                                        int_company = aa.GetString("int_company"),
                                        country_code = aa.GetString("country_code"),
                                        company_name = aa.GetString("country_name"),
                                        address = aa.GetString("address"),
                                        postal_code = aa.GetString("postal_code"),
                                        city_name = aa.GetString("city_name"),
                                        state = aa.GetString("state"),
                                        phone_number = aa.GetString("phone_number"),
                                        fax_number = aa.GetString("fax_number"),
                                        web_address = aa.GetString("web_address"),
                                        email_address = aa.GetString("email_address"),
                                        picture = aa.GetString("picture"),
                                        npwp = aa.GetString("npwp"),
                                        pimpinan = aa.GetString("pimpinan"),
                                        pimpinan_npwp = aa.GetString("pimpinan_npwp"),
                                        npp = aa.GetString("npp"),
                                        jhk = aa.GetDecimal("jhk"),
                                        entry_date = aa.GetDateTime("entry_date"),
                                        entry_user = aa.GetString("entry_user"),
                                        edit_date = aa.GetDateTime("edit_date"),
                                        edit_user = aa.GetString("edit_user")
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
                Log.Error(DateTime.Now + " GetCompanyList FAILED... ", ex);
            }
            return vList;
        }

        public mCompanyModel getCompanyInfo(string pCompanyCode)
        {
            var vModel = new mCompanyModel();
            var strSQL = @"SELECT company_code,int_company,country_code,company_name,address,postal_code,
                                city_name,state,phone_number,fax_number,web_address,email_address,picture,
                                npwp,pimpinan,pimpinan_npwp,npp,jhk,entry_date,entry_user,edit_date,edit_user
                           FROM m_company
                           WHERE company_code = @pCompanyCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCompanyCode", pCompanyCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.company_code = aa.GetInt16("company_code");
                                    vModel.int_company = aa.GetString("int_company");
                                    vModel.country_code = aa.GetString("country_code");
                                    vModel.company_name = aa.GetString("country_name");
                                    vModel.address = aa.GetString("address");
                                    vModel.postal_code = aa.GetString("postal_code");
                                    vModel.city_name = aa.GetString("city_name");
                                    vModel.state = aa.GetString("state");
                                    vModel.phone_number = aa.GetString("phone_number");
                                    vModel.fax_number = aa.GetString("fax_number");
                                    vModel.web_address = aa.GetString("web_address");
                                    vModel.email_address = aa.GetString("email_address");
                                    vModel.picture = aa.GetString("picture");
                                    vModel.npwp = aa.GetString("npwp");
                                    vModel.pimpinan = aa.GetString("pimpinan");
                                    vModel.pimpinan_npwp = aa.GetString("pimpinan_npwp");
                                    vModel.npp = aa.GetString("npp");
                                    vModel.jhk = aa.GetDecimal("jhk");
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
                Log.Error(DateTime.Now + " GetCompanyList Failed", ex);
            }
            return vModel;
        }

        public void UpdateCompany(mCompanyModel pModel)
        {
            string SqlString = @"UPDATE m_company 
                                        SET `int_company` = @pIntCompany,
                                            `country_code` = @pCountryCode,
                                            `company_name` = @pCompanyName,
                                            `address` = @pAddress,
                                            `postal_code` = @pPostalCode,
                                            `city_name` = @pCityName,
                                            `state` = @pState,
                                            `phone_number` = @pPhoneNumber,
                                            `fax_number` = @PFaxNumber,
                                            `web_address` = @pWebAddress,
                                            `email_address` = @pEmailAddress,
                                            `picture` = @pPicture,
                                            `npwp` = @pNpwp,
                                            `pimpinan` = @pPimpinan,
                                            `pimpinan_npwp` = @pPimpinanNpwp,
                                            `npp` = @pNpp,
                                            `jhk` = @pJhk,
                                            `entry_date` = @pEntryDate,
                                            `entry_user` = @pEntryUser,
                                            `edit_date` = @pEditDate,
                                            `edit_user` = @pEditUser
                                WHERE `company_code` = @pCompanyCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pCompanyCode", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pIntCompany", pModel.int_company);
                        cmd.Parameters.AddWithValue("@pCountryCode", pModel.country_code);
                        cmd.Parameters.AddWithValue("@pCompanyName", pModel.company_name);
                        cmd.Parameters.AddWithValue("@pAddress", pModel.address);
                        cmd.Parameters.AddWithValue("@pPostalCode", pModel.postal_code);
                        cmd.Parameters.AddWithValue("@pCityName", pModel.city_name);
                        cmd.Parameters.AddWithValue("@pState", pModel.state);
                        cmd.Parameters.AddWithValue("@pPhoneNumber", pModel.phone_number);
                        cmd.Parameters.AddWithValue("@pFaxNumber", pModel.fax_number);
                        cmd.Parameters.AddWithValue("@pWebAddress", pModel.web_address);
                        cmd.Parameters.AddWithValue("@pEmailAddress", pModel.email_address);
                        cmd.Parameters.AddWithValue("@pPicture", pModel.picture);
                        cmd.Parameters.AddWithValue("@pNpwp", pModel.npwp);
                        cmd.Parameters.AddWithValue("@pPimpinan", pModel.pimpinan);
                        cmd.Parameters.AddWithValue("@pPimpinanNpwp", pModel.pimpinan_npwp);
                        cmd.Parameters.AddWithValue("@pNpp", pModel.npp);
                        cmd.Parameters.AddWithValue("@pJhk", pModel.jhk);
                        cmd.Parameters.AddWithValue("@pEntryDate", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pEntryUser", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pEditDate", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pEditUser", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE COMPANY SUCCESS ====>>>>>> Code : " + pModel.company_code + " Name : " + pModel.company_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT COMPANY FAILED", ex);
            }

        }

        public void DeleteCompany(string pCompanyCode)
        {
            string SqlString = @"DELETE m_company WHERE company_code = @pCompanyCode";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCompanyCode", pCompanyCode);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " DELETE COMPANY SUCCESS ====>>>>>> Code : " + pCompanyCode);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE COMPANY FAILED", ex);
            }
        }

    }
}