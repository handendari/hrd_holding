using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class mBranchOfficeModel
    {
        public int branch_code {get;set;}
        public int company_code { get; set; }
        public string company_name { get; set; }
        public string int_branch { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string branch_name { get; set; }
        public string address { get; set; }
        public string postal_code { get; set; }
        public string city_name { get; set; }
        public string state { get; set; }
        public string phone_number { get; set; }
        public string fax_number { get; set; }
        public string web_address { get; set; }
        public string email_address { get; set; }
        public string picture { get; set; }
        public string npwp { get; set; }
        public string pimpinan { get; set; }
        public string pimpinan_npwp { get; set; }
        public string npp { get; set; }
        public decimal jhk { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
        public DateTime? edit_date { get; set; }
        public string edit_user { get; set; }
    }
}