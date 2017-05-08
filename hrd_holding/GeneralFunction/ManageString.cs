using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace hrd_holding.GeneralFunction
{
    public class ManageString
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("ManageString");

        public RijndaelManaged GetRijndaelManaged(String secretKey)
        {
            var keyBytes = new byte[16];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));

            return new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = keyBytes
            };
        }

        public byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor()
                .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        public byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor()
                .TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }

        /// <summary>
        /// Encrypts plaintext using AES 128bit key and a Chain Block Cipher and returns a base64 encoded string
        /// </summary>
        /// <param name="plainText">Plain text to encrypt</param>
        /// <param name="key">Secret key</param>
        /// <returns>Base64 encoded string</returns>
        public String Encrypt(String plainText, String key)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(Encrypt(plainBytes, GetRijndaelManaged(key)));
        }

        /// <summary>
        /// Decrypts a base64 encoded string using the given key (AES 128bit key and a Chain Block Cipher)
        /// </summary>
        /// <param name="encryptedText">Base64 Encoded String</param>
        /// <param name="key">Secret Key</param>
        /// <returns>Decrypted String</returns>
        public String Decrypt(String encryptedText, String key)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            return Encoding.UTF8.GetString(Decrypt(encryptedBytes, GetRijndaelManaged(key)));
        }


        /// <summary>
        /// Generate Where Clause On jqGrid
        /// </summary>
        public string ConstructWhereJqGrid(string filters)
        {
            string strWhere = "";
            string strOpr = "";
            string strAwal = "";
            string strAkhir = "";
            string strField = "";

            if (filters.Length > 0)
            {
                dynamic json = JObject.Parse(filters);
                JArray jsonArray = json.rules;

                if (json.rules.Count > 0)
                {
                    for (int i = 0; i < jsonArray.Count; i++)
                    {
                        dynamic isifilter = JObject.Parse(jsonArray[i].ToString());
                        switch ((string)isifilter.op)
                        {
                            case "eq":
                                strOpr = " = ";
                                strAwal = "'";
                                strAkhir = "'";
                                break;
                            case "ne":
                                strOpr = " <> ";
                                strAwal = "'";
                                strAkhir = "'";
                                break;
                            case "lt":
                                strOpr = " < ";
                                strAwal = "'";
                                strAkhir = "'";
                                break;
                            case "le":
                                strOpr = " <= ";
                                strAwal = "'";
                                strAkhir = "'";
                                break;
                            case "gt":
                                strOpr = " > ";
                                strAwal = "'";
                                strAkhir = "'";
                                break;
                            case "ge":
                                strOpr = " >= ";
                                strAwal = "'";
                                strAkhir = "'";
                                break;
                            case "bw":
                            case "ew":
                            case "cn":
                                strOpr = " LIKE ";
                                strAwal = "'%";
                                strAkhir = "%'";
                                break;
                            case "bn":
                            case "en":
                            case "nc":
                                strOpr = " NOT LIKE ";
                                strAwal = "'%";
                                strAkhir = "%'";
                                break;
                            case "in":
                                strOpr = " IN ";
                                strAwal = "'";
                                strAkhir = "'";
                                break;
                            case "ni":
                                strOpr = " NOT IN ";
                                strAwal = "'";
                                strAkhir = "'";
                                break;
                            case "nu":
                                strOpr = " IS NULL ";
                                strAwal = "";
                                strAkhir = "";
                                break;
                            case "nn":
                                strOpr = "IS NOT NULL";
                                strAwal = "";
                                strAkhir = "";
                                break;
                            default:
                                strOpr = "";
                                strAwal = "";
                                strAkhir = "";
                                break;
                        }

                        if (isifilter.field.ToString().ToUpper().Contains("DATE") || isifilter.field.ToString().ToUpper().Contains("EDON"))
                        {
                            strField = "convert(date," + isifilter.field + ")";
                            strOpr = " = ";
                            strAwal = "'";
                            strAkhir = "'";
                        }
                        else
                        {
                            strField = isifilter.field;
                        }

                        if (i == 0)
                        {
                            strWhere = strField + strOpr + strAwal + isifilter.data + strAkhir;
                        }
                        else
                        {
                            strWhere = strWhere + " AND " + strField + strOpr + strAwal + isifilter.data + strAkhir;
                        }
                    }
                    //strWhere = " WHERE " + strWhere;
                }
            }
            return strWhere;
        }

        public string ConstructWhere(Boolean pWithWhere, int pFilterCount, List<string> pFilterValue, List<string> pFilterCondition,
            List<string> pFilterDataField, List<string> pFilterOperator)
        {
            var where = "";
            if (pFilterCount > 0)
            {

                where = pWithWhere ? " WHERE (" : " AND (";

                String tmpdatafield = "";
                String tmpfilteroperator = "";
                for (int i = 0; i < pFilterCount; i++)
                {
                    String filtervalue = pFilterValue[i].ToString();
                    String filtercondition = pFilterCondition[i].ToString();
                    String filterdatafield = pFilterDataField[i].ToString();
                    filterdatafield = filterdatafield.Replace("([^A-Za-z0-9])", "");
                    String filteroperator = pFilterOperator[i].ToString();

                    if (tmpdatafield.Equals(""))
                    {
                        tmpdatafield = filterdatafield;
                    }
                    else if (!tmpdatafield.Equals(filterdatafield))
                    {
                        where += ") AND (";
                    }
                    else if (tmpdatafield.Equals(filterdatafield))
                    {
                        if (tmpfilteroperator.Equals("0"))
                        {
                            where += " AND ";
                        }
                        else where += " OR ";
                    }

                    // build the "WHERE" clause depending on the filter's condition, value and datafield.
                    switch (filtercondition)
                    {
                        case "CONTAINS":
                            where += " " + filterdatafield + " LIKE '%" + filtervalue + "%'";
                            break;
                        case "CONTAINS_CASE_SENSITIVE":
                            where += " " + filterdatafield + " LIKE BINARY '%" + filtervalue + "%'";
                            break;
                        case "DOES_NOT_CONTAIN":
                            where += " " + filterdatafield + " NOT LIKE '%" + filtervalue + "%'";
                            break;
                        case "DOES_NOT_CONTAIN_CASE_SENSITIVE":
                            where += " " + filterdatafield + " NOT LIKE BINARY '%" + filtervalue + "%'";
                            break;
                        case "EQUAL":
                            where += " " + filterdatafield + " = '" + filtervalue + "'";
                            break;
                        case "EQUAL_CASE_SENSITIVE":
                            where += " " + filterdatafield + " LIKE BINARY '" + filtervalue + "'";
                            break;
                        case "NOT_EQUAL":
                            where += " " + filterdatafield + " NOT LIKE '" + filtervalue + "'";
                            break;
                        case "NOT_EQUAL_CASE_SENSITIVE":
                            where += " " + filterdatafield + " NOT LIKE BINARY '" + filtervalue + "'";
                            break;
                        case "GREATER_THAN":
                            where += " " + filterdatafield + " > '" + filtervalue + "'";
                            break;
                        case "LESS_THAN":
                            where += " " + filterdatafield + " < '" + filtervalue + "'";
                            break;
                        case "GREATER_THAN_OR_EQUAL":
                            where += " " + filterdatafield + " >= '" + filtervalue + "'";
                            break;
                        case "LESS_THAN_OR_EQUAL":
                            where += " " + filterdatafield + " <= '" + filtervalue + "'";
                            break;
                        case "STARTS_WITH":
                            where += " " + filterdatafield + " LIKE '" + filtervalue + "%'";
                            break;
                        case "STARTS_WITH_CASE_SENSITIVE":
                            where += " " + filterdatafield + " LIKE BINARY '" + filtervalue + "%'";
                            break;
                        case "ENDS_WITH":
                            where += " " + filterdatafield + " LIKE '%" + filtervalue + "'";
                            break;
                        case "ENDS_WITH_CASE_SENSITIVE":
                            where += " " + filterdatafield + " LIKE BINARY '%" + filtervalue + "'";
                            break;
                        case "NULL":
                            where += " " + filterdatafield + " IS NULL";
                            break;
                        case "NOT_NULL":
                            where += " " + filterdatafield + " IS NOT NULL";
                            break;
                    }

                    if (i == pFilterCount - 1)
                    {
                        where += ")";
                    }

                    tmpfilteroperator = filteroperator;
                    tmpdatafield = filterdatafield;
                }
            }
            return where;
        }

        public string ConstructOrderBy(string pSortField,string pOrder)
        {
            String orderby = "";
            String sortdatafield = pSortField;
            String sortorder = pOrder;
            if (sortdatafield != null && sortorder != null && (sortorder.Equals("asc") || sortorder.Equals("desc")))
            {
                sortdatafield = sortdatafield.Replace("([^A-Za-z0-9])", "");

                orderby = " ORDER BY " + sortdatafield + " " + sortorder;
            }

            return orderby;
        }

        public int ConstructPageSize(object pPageSize)
        {
            int pagesize = 1000;
            if (null != pPageSize)
            {
                try
                {
                    pagesize = Convert.ToInt32(pPageSize);
                }
                catch (FormatException nfe) { }
                {
                }
            }
            return pagesize;
        }

        public int ConstructPageNum(object pPageNum)
        {
            int pagenum = 0;
            if (null != pPageNum)
            {
                try
                {
                    pagenum = Convert.ToInt32(pPageNum);
                }
                catch (FormatException nfe) { }
                {
                }
            }

            return pagenum;
        }

    }
}