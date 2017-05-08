using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mBranchOfficeRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("BranchOfficeRepo");

        public void InsertCountry(mBranchOfficeModel pModel)
        {
            string SqlString = @"INSERT INTO m_branch_office (branch_code,company_code,int_branch,country_code,branch_name,address,
                                                        postal_code,city_name,state,phone_number,fax_number,web_address,
                                                        email_address,picture,npwp,pimpinan,pimpinan_npwp,npp,jhk,
                                                        entry_date,entry_user,edit_date,edit_user)
                                            VALUES (@pbranch_code,@pcompany_code,@pint_branch,@pcountry_code,@pbranch_name,@paddress,
                                                    @ppostal_code,@pcity_name,@pstate,@pphone_number,@pfax_number,@pweb_address,
                                                    @pemail_address,@ppicture,@pnpwp,@ppimpinan,@ppimpinan_npwp,@pnpp,@pjhk,
                                                    @pentry_date,@pentry_user,@pedit_date,@pedit_user)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pbranch_code", pModel.branch_code);
                        cmd.Parameters.AddWithValue("@pcompany_code", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pint_branch", pModel.int_branch);
                        cmd.Parameters.AddWithValue("@pcountry_code", pModel.country_code);
                        cmd.Parameters.AddWithValue("@pbranch_name", pModel.branch_name);
                        cmd.Parameters.AddWithValue("@paddress", pModel.address);
                        cmd.Parameters.AddWithValue("@ppostal_code", pModel.postal_code);
                        cmd.Parameters.AddWithValue("@pcity_name", pModel.city_name);
                        cmd.Parameters.AddWithValue("@pstate", pModel.state);
                        cmd.Parameters.AddWithValue("@pphone_number", pModel.phone_number);
                        cmd.Parameters.AddWithValue("@pfax_number", pModel.fax_number);
                        cmd.Parameters.AddWithValue("@pweb_address", pModel.web_address);
                        cmd.Parameters.AddWithValue("@pemail_address", pModel.email_address);
                        cmd.Parameters.AddWithValue("@ppicture", pModel.picture);
                        cmd.Parameters.AddWithValue("@pnpwp", pModel.npwp);
                        cmd.Parameters.AddWithValue("@ppimpinan", pModel.pimpinan);
                        cmd.Parameters.AddWithValue("@ppimpinan_npwp", pModel.pimpinan_npwp);
                        cmd.Parameters.AddWithValue("@pnpp", pModel.npp);
                        cmd.Parameters.AddWithValue("@pjhk", pModel.jhk);
                        cmd.Parameters.AddWithValue("@pentry_date", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pentry_user", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pedit_date", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pedit_user", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT BRANCH OFFICE SUCCESS ====>>>> Code : " + pModel.branch_code + " Name : " + pModel.branch_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT BRANCH OFFICE FAILED", ex);
            }
        }

        public List<mBranchOfficeModel> getBranchOfficeList(string pCompanyCode)
        {
            var vList = new List<mBranchOfficeModel>();
            var strSQL = @"SELECT branch_code,company_code,company_name,int_branch,country_code,country_name,branch_name,address,
                                postal_code,city_name,state,phone_number,fax_number,web_address,
                                email_address,picture,npwp,pimpinan,pimpinan_npwp,npp,jhk,
                                entry_date,entry_user,edit_date,edit_user
                            FROM m_branch_office WHERE company_code = @pCompanyCode";
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
                                    var m = new mBranchOfficeModel
                                    {
                                        branch_code = aa.GetInt16(0),
                                        company_code = aa.GetInt16(1),
                                        company_name = aa.GetString(2),
                                        int_branch = aa.GetString(3),
                                        country_code = aa.GetString(4),
                                        country_name = aa.GetString(5),
                                        branch_name = aa.GetString(6),
                                        address = aa.GetString(7),
                                        postal_code = aa.GetString(8),
                                        city_name = aa.GetString(9),
                                        state = aa.GetString(10),
                                        phone_number = aa.GetString(11),
                                        fax_number = aa.GetString(12),
                                        web_address = aa.GetString(13),
                                        email_address = aa.GetString(14),
                                        picture = aa.GetString(15),
                                        npwp = aa.GetString(16),
                                        pimpinan = aa.GetString(17),
                                        pimpinan_npwp = aa.GetString(18),
                                        npp = aa.GetString(19),
                                        jhk = aa.GetDecimal(20),
                                        entry_date = aa.GetDateTime(21),
                                        entry_user = aa.GetString(22),
                                        edit_date = aa.GetDateTime(23),
                                        edit_user = aa.GetString(24)
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
                Log.Error(DateTime.Now + " GetBranchOfficeList Failed", ex);
            }
            return vList;
        }

        public mBranchOfficeModel getBranchOfficeInfo(string pBranchCode)
        {
            var vModel = new mBranchOfficeModel();
            var strSQL = @"SELECT branch_code,company_code,company_name,int_branch,country_code,country_name,branch_name,address,
                                postal_code,city_name,state,phone_number,fax_number,web_address,
                                email_address,picture,npwp,pimpinan,pimpinan_npwp,npp,jhk,
                                entry_date,entry_user,edit_date,edit_user
                            FROM m_branch_office WHERE branch_code = @pBranchCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pBranchCode", pBranchCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.branch_code = aa.GetInt16(0);
                                    vModel.company_code = aa.GetInt16(1);
                                    vModel.company_name = aa.GetString(2);
                                    vModel.int_branch = aa.GetString(3);
                                    vModel.country_code = aa.GetString(4);
                                    vModel.country_name = aa.GetString(5);
                                    vModel.branch_name = aa.GetString(6);
                                    vModel.address = aa.GetString(7);
                                    vModel.postal_code = aa.GetString(8);
                                    vModel.city_name = aa.GetString(9);
                                    vModel.state = aa.GetString(10);
                                    vModel.phone_number = aa.GetString(11);
                                    vModel.fax_number = aa.GetString(12);
                                    vModel.web_address = aa.GetString(13);
                                    vModel.email_address = aa.GetString(14);
                                    vModel.picture = aa.GetString(15);
                                    vModel.npwp = aa.GetString(16);
                                    vModel.pimpinan = aa.GetString(17);
                                    vModel.pimpinan_npwp = aa.GetString(18);
                                    vModel.npp = aa.GetString(19);
                                    vModel.jhk = aa.GetDecimal(20);
                                    vModel.entry_date = aa.GetDateTime(21);
                                    vModel.entry_user = aa.GetString(22);
                                    vModel.edit_date = aa.GetDateTime(23);
                                    vModel.edit_user = aa.GetString(24);
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

        public void UpdateBranchOffice(mBranchOfficeModel pModel)
        {
            string SqlString = @"UPDATE m_branch_office 
                                 SET company_code = @pcompany_code,
                                    int_branch = @pint_branch,
                                    country_code = @pcountry_code,
                                    branch_name = @pbranch_name,
                                    address = @paddress,
                                    postal_code = @ppostal_code,
                                    city_name = @pcity_name,
                                    state = @pstate,
                                    phone_number = @pphone_number,
                                    fax_number = @pfax_number,
                                    web_address = @pweb_address,
                                    email_address = @pemail_address,
                                    picture = @ppicture,
                                    npwp = @pnpwp,
                                    pimpinan = @ppimpinan,
                                    pimpinan_npwp = @ppimpinan_npwp,
                                    npp = @pnpp,
                                    jhk = @pjhk,
                                    entry_date = @pentry_date,
                                    entry_user = @pentry_user,
                                    edit_date = @pedit_date,
                                    edit_user = @pedit_user
                                WHERE branch_code = @pbranch_code";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pbranch_code", pModel.branch_code);
                        cmd.Parameters.AddWithValue("@pcompany_code", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pint_branch", pModel.int_branch);
                        cmd.Parameters.AddWithValue("@pcountry_code", pModel.country_code);
                        cmd.Parameters.AddWithValue("@pbranch_name", pModel.branch_name);
                        cmd.Parameters.AddWithValue("@paddress", pModel.address);
                        cmd.Parameters.AddWithValue("@ppostal_code", pModel.postal_code);
                        cmd.Parameters.AddWithValue("@pcity_name", pModel.city_name);
                        cmd.Parameters.AddWithValue("@pstate", pModel.state);
                        cmd.Parameters.AddWithValue("@pphone_number", pModel.phone_number);
                        cmd.Parameters.AddWithValue("@pfax_number", pModel.fax_number);
                        cmd.Parameters.AddWithValue("@pweb_address", pModel.web_address);
                        cmd.Parameters.AddWithValue("@pemail_address", pModel.email_address);
                        cmd.Parameters.AddWithValue("@ppicture", pModel.picture);
                        cmd.Parameters.AddWithValue("@pnpwp", pModel.npwp);
                        cmd.Parameters.AddWithValue("@ppimpinan", pModel.pimpinan);
                        cmd.Parameters.AddWithValue("@ppimpinan_npwp", pModel.pimpinan_npwp);
                        cmd.Parameters.AddWithValue("@pnpp", pModel.npp);
                        cmd.Parameters.AddWithValue("@pjhk", pModel.jhk);
                        cmd.Parameters.AddWithValue("@pentry_date", pModel.entry_date);
                        cmd.Parameters.AddWithValue("@pentry_user", pModel.entry_user);
                        cmd.Parameters.AddWithValue("@pedit_date", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pedit_user", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE BRANCH OFFICE SUCCESS ====>>>>>> Code : " + pModel.branch_code + " Name : " + pModel.branch_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT BRANCH OFFICE FAILED", ex);
            }
        }

        public void DeleteBranchOffice(string pBranchCode)
        {
            string SqlString = @"DELETE m_branch_office WHERE branch_code = @pBranchCode";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pBranchCode", pBranchCode);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " DELETE BRANCH OFFICE SUCCESS ====>>>>>> Code : " + pBranchCode);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " DELETE BRANCH OFFICE FAILED", ex);
            }
        }
    }
}