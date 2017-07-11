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

        public ResponseModel InsertBranchOffice(mBranchOfficeModel pModel)
        {
            var vResp = new ResponseModel();

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
                        vResp.isValid = true;
                        vResp.message = " INSERT BRANCH OFFICE SUCCESS, Code : " + pModel.branch_code + " Name : " + pModel.branch_name;
                        Log.Debug(DateTime.Now + " INSERT BRANCH OFFICE SUCCESS ====>>>> Code : " + pModel.branch_code + " Name : " + pModel.branch_name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " INSERT BRANCH OFFICE FAILED.......";

                Log.Error(DateTime.Now + " INSERT BRANCH OFFICE FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel getBranchOfficeList(int pCompanyCode, int? pStartRow = 0, int? pRows = 0, 
            string pWhere = "", string pOrderBy = "")
        {
            var vLimit = pOrderBy + " LIMIT " + pStartRow + "," + pRows;

            var vJmlRecord = 0;
            var vList = new List<mBranchOfficeModel>();

            var strSQLCount = @"SELECT COUNT(branch_code) jml_record
                                FROM m_branch_office 
                                WHERE company_code = @pCompanyCode " + pWhere;

            var strSQL = @"SELECT mbo.branch_code,mbo.company_code,mco.int_company,mco.company_name,
                                  mbo.int_branch,mbo.country_code,mcu.int_country,mcu.country_name,mbo.branch_name,mbo.address,
                                mbo.postal_code,mbo.city_name,mbo.state,mbo.phone_number,mbo.fax_number,mbo.web_address,
                                mbo.email_address,mbo.picture,mbo.npwp,mbo.pimpinan,mbo.pimpinan_npwp,mbo.npp,IFNULL(mbo.jhk,0) jhk,
                                mbo.entry_date,IFNULL(mbo.entry_user,'') entry_user,mbo.edit_date,IFNULL(mbo.edit_user,'') edit_user
                            FROM m_branch_office mbo JOIN m_Company mco ON mbo.company_code = mco.company_code
                            JOIN m_country mcu ON mbo.country_code = mcu.country_code
                            WHERE mbo.company_code = @pCompanyCode " + pWhere + " " + vLimit;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQLCount, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pCompanyCode", pCompanyCode);

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
                        cmd.Parameters.AddWithValue("@pCompanyCode", pCompanyCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    var m = new mBranchOfficeModel
                                    {
                                        branch_code = aa.GetInt16("branch_code"),
                                        int_branch = aa.GetString("int_branch"),
                                        branch_name = aa.GetString("branch_name"),
                                        company_code = aa.GetInt16("company_code"),
                                        int_company = aa.GetString("int_company"),
                                        company_name = aa.GetString("company_name"),
                                        country_code = aa.GetString("country_code"),
                                        int_country = aa.GetString("int_country"),
                                        country_name = aa.GetString("country_name"),
                                        address = aa.GetString("address"),
                                        postal_code = aa.GetString("postal_code"),
                                        city_name = aa.GetString("city_name"),
                                        state = aa.GetString("state"),
                                        phone_number = aa.GetString("phone_number"),
                                        fax_number = aa.GetString("fax_number"),
                                        web_address = aa.GetString("web_address"),
                                        email_address = aa.GetString("email_address"),
                                        //picture = aa.GetString("picture"),
                                        npwp = aa.GetString("npwp"),
                                        pimpinan = aa.GetString("pimpinan"),
                                        pimpinan_npwp = aa.GetString("pimpinan_npwp"),
                                        npp = aa.GetString("npp"),
                                        jhk = aa.GetDecimal("jhk"),
                                        entry_date = (aa["entry_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["entry_date"]),
                                        entry_user = aa.GetString("entry_user"),
                                        edit_date = (aa["edit_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["edit_date"]),
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
                Log.Error(DateTime.Now + " GetBranchOfficeList Failed", ex);
            }

            var vResp = new ResponseModel();
            vResp.total_record = vJmlRecord;
            vResp.objResult = vList;

            return vResp;
        }

        public mBranchOfficeModel getBranchOfficeInfo(int pBranchCode)
        {
            var vModel = new mBranchOfficeModel();
            var strSQL = @"SELECT mbo.branch_code,mbo.company_code,mco.int_company,mco.company_name,
                                  mbo.int_branch,mbo.country_code,mcu.int_country,mcu.country_name,mbo.branch_name,mbo.address,
                                mbo.postal_code,mbo.city_name,mbo.state,mbo.phone_number,mbo.fax_number,mbo.web_address,
                                mbo.email_address,mbo.picture,mbo.npwp,mbo.pimpinan,mbo.pimpinan_npwp,mbo.npp,IFNULL(mbo.jhk,0) jhk,
                                mbo.entry_date,IFNULL(mbo.entry_user,'') entry_user,mbo.edit_date,IFNULL(mbo.edit_user,'') edit_user
                            FROM m_branch_office mbo JOIN m_Company mco ON mbo.company_code = mco.company_code
                            JOIN m_country mcu ON mbo.country_code = mcu.country_code
                            WHERE branch_code = @pBranchCode";
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
                                    vModel.branch_code = aa.GetInt16("branch_code");
                                    vModel.int_branch = aa.GetString("int_branch");
                                    vModel.branch_name = aa.GetString("branch_name");
                                    vModel.company_code = aa.GetInt16("company_code");
                                    vModel.int_company = aa.GetString("int_company");
                                    vModel.company_name = aa.GetString("company_name");
                                    vModel.country_code = aa.GetString("country_code");
                                    vModel.country_name = aa.GetString("country_name");
                                    vModel.address = aa.GetString("address");
                                    vModel.postal_code = aa.GetString("postal_code");
                                    vModel.city_name = aa.GetString("city_name");
                                    vModel.state = aa.GetString("state");
                                    vModel.phone_number = aa.GetString("phone_number");
                                    vModel.fax_number = aa.GetString("fax_number");
                                    vModel.web_address = aa.GetString("web_address");
                                    vModel.email_address = aa.GetString("email_address");
                                    //vModel.picture = aa.GetString("picture");
                                    vModel.npwp = aa.GetString("npwp");
                                    vModel.pimpinan = aa.GetString("pimpinan");
                                    vModel.pimpinan_npwp = aa.GetString("pimpinan_npwp");
                                    vModel.npp = aa.GetString("npp");
                                    vModel.jhk = aa.GetDecimal("jhk");
                                    vModel.entry_date = (aa["entry_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["entry_date"]);
                                    vModel.entry_user = aa.GetString("entry_user");
                                    vModel.edit_date = (aa["edit_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["edit_date"]);
                                    vModel.edit_user = aa.GetString("edit_user");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetBranchOfficeRepo Failed", ex);
            }
            return vModel;
        }

        public ResponseModel UpdateBranchOffice(mBranchOfficeModel pModel)
        {
            var vResp = new ResponseModel();
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
                        cmd.Parameters.AddWithValue("@pedit_date", pModel.edit_date);
                        cmd.Parameters.AddWithValue("@pedit_user", pModel.edit_user);

                        var status = cmd.ExecuteNonQuery();
                        vResp.isValid = true;
                        vResp.message = " UPDATE BRANCH OFFICE SUCCESS, Code : " + pModel.branch_code + " Name : " + pModel.branch_name;
                        Log.Debug(DateTime.Now + " UPDATE BRANCH OFFICE SUCCESS ====>>>>>> Code : " + pModel.branch_code + " Name : " + pModel.branch_name);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " INSERT BRANCH OFFICE FAILED........";

                Log.Error(DateTime.Now + " INSERT BRANCH OFFICE FAILED", ex);
            }

            return vResp;
        }

        public ResponseModel DeleteBranchOffice(int pBranchCode)
        {
            var vResp = new ResponseModel();

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
                        vResp.isValid = true;
                        vResp.message = " DELETE BRANCH OFFICE SUCCESS, Code : " + pBranchCode;
                        Log.Debug(DateTime.Now + " DELETE BRANCH OFFICE SUCCESS ====>>>>>> Code : " + pBranchCode);

                    }
                }
            }
            catch (Exception ex)
            {
                vResp.isValid = false;
                vResp.message = " DELETE BRANCH OFFICE FAILED........";

                Log.Error(DateTime.Now + " DELETE BRANCH OFFICE FAILED", ex);
            }

            return vResp;
        }

        public int getBranchOfficeSeqNo()
        {
            var vSeqNo = 0;
            var strSQL = @"SELECT IFNULL(MAX(branch_code),0) seq_no
                           FROM m_branch_office md";
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
                Log.Error(DateTime.Now + " GetBranchOfficeSeqNo Failed", ex);
            }
            return vSeqNo;
        }

    }
}