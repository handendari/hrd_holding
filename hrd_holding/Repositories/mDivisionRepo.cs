using hrd_holding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace hrd_holding.Repositories
{
    public class mDivisionRepo
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("DivisionRepo");

        public void InsertDivision(mDivisionModel pModel)
        {
            string SqlString = @"INSERT INTO `m_division` 
                                            (`division_code`,`company_code`,`branch_code`,`department_code`,`int_division`,`division_name`,`description`)
                                 VALUES (@pDivisionCode,@pCompanyCode,@pBranchCode,@pDepartmentCode,@pIntDivision,@pDivisionName,@pDescription)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pDivisionCode", pModel.division_code);
                        cmd.Parameters.AddWithValue("@pCompanyCode", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pBranchCode", pModel.branch_code);
                        cmd.Parameters.AddWithValue("@pDepartmentCode", pModel.department_code);
                        cmd.Parameters.AddWithValue("@pIntDivision", pModel.int_division);
                        cmd.Parameters.AddWithValue("@pDivisionName", pModel.division_name);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " INSERT DIVISION SUCCESS ====>>>> Code : " + pModel.division_code + " Name : " + pModel.division_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " INSERT DIVISION FAILED", ex);
            }

        }

        public List<mDivisionModel> getDivisionList(int pDepartmentCode)
        {
            var vList = new List<mDivisionModel>();
            var strSQL = @"SELECT md.division_code,md.company_code,md.branch_code,md.department_code,md.int_division,
                                  md.division_name,md.description
                           FROM m_division md JOIN m_company mc ON md.company_code = mc.company_code
                           JOIN m_brach_office mbo ON md.branch_code = mbo.branch_code
                           JOIN m_department mdp ON md.department_code = mdp.department_code
                           WHERE md.Department_code = @pDepartmentCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pDepartmentCode", pDepartmentCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    var m = new mDivisionModel
                                    {
                                        division_code = aa.GetInt16("division_code"),
                                        department_code = aa.GetString("department_code"),
                                        department_name = aa.GetString("department_name"),
                                        company_code = aa.GetString("company_code"),
                                        company_name = aa.GetString("company_name"),
                                        branch_code = aa.GetString("branch_code"),
                                        branch_name = aa.GetString("branch_name"),
                                        int_division = aa.GetString("int_division"),
                                        division_name = aa.GetString("division_division"),
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
                Log.Error(DateTime.Now + " GetDivisionList FAILED... ", ex);
            }
            return vList;
        }

        public mDivisionModel getDivisionInfo(string pDivisionCode)
        {
            var vModel = new mDivisionModel();
            var strSQL = @"SELECT md.division_code,md.company_code,md.branch_code,md.department_code,md.int_division,
                                  md.division_name,md.description
                           FROM m_division md JOIN m_company mc ON md.company_code = mc.company_code
                           JOIN m_brach_office mbo ON md.branch_code = mbo.branch_code
                           JOIN m_department mdp ON md.department_code = mdp.department_code
                           WHERE md.division_code = @pDivisionCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@pDivisionCode", pDivisionCode);

                        using (MySqlDataReader aa = cmd.ExecuteReader())
                        {
                            if (aa.HasRows)
                            {
                                while (aa.Read())
                                {
                                    vModel.division_code = aa.GetInt16("division_code");
                                    vModel.department_code = aa.GetString("department_code");
                                    vModel.department_name = aa.GetString("department_name");
                                    vModel.company_code = aa.GetString("company_code");
                                    vModel.company_name = aa.GetString("company_name");
                                    vModel.branch_code = aa.GetString("branch_code");
                                    vModel.branch_name = aa.GetString("branch_name");
                                    vModel.int_division = aa.GetString("int_division");
                                    vModel.division_name = aa.GetString("division_division");
                                    vModel.description = aa.GetString("description");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " GetDivisionList Failed", ex);
            }
            return vModel;
        }

        public void UpdateDivision(mDivisionModel pModel)
        {
            string SqlString = @"UPDATE m_division
                                      SET `company_code` = @pCompanyCode,
                                          `branch_code` = @pBranchCode,
                                          `department_code` = @pDepartmentCode,
                                          `int_division` = @pIntDivision,
                                          `division_name` = @pDivisionName,
                                          `description` = @pDescription
                                WHERE `division_code` = @pDivisionCode";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigModel.mConn))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(SqlString, conn))
                    {

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@pDivisionCode", pModel.division_code);
                        cmd.Parameters.AddWithValue("@pCompanyCode", pModel.company_code);
                        cmd.Parameters.AddWithValue("@pBranchCode", pModel.branch_code);
                        cmd.Parameters.AddWithValue("@pDepartmentCode", pModel.department_code);
                        cmd.Parameters.AddWithValue("@pIntDivision", pModel.int_division);
                        cmd.Parameters.AddWithValue("@pDivisionName", pModel.division_name);
                        cmd.Parameters.AddWithValue("@pDescription", pModel.description);

                        var status = cmd.ExecuteNonQuery();
                        Log.Debug(DateTime.Now + " UPDATE DIVISION SUCCESS ====>>>>>> Code : " + pModel.division_code + " Name : " + pModel.division_name);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now + " UPDATE DIVISION FAILED", ex);
            }

        }

        public void DeleteDivision(string pCode)
        {
            string SqlString = @"DELETE m_division WHERE division_code = @pCode";

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
                        Log.Debug(DateTime.Now + " DELETE DIVISION SUCCESS ====>>>>>> Code : " + pCode);

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