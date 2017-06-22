using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class mDepartmentModel
    {
        public int department_code {get;set;}
        public int company_code { get; set; }
        public string int_company { get; set; }
        public string company_name { get; set; }
        public int branch_code { get; set; }
        public string int_branch { get; set; }
        public string branch_name { get; set; }
        public string int_department { get; set; }
        public string department_name { get; set; }
        public string description { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
        public DateTime? edit_date { get; set; }
        public string edit_user { get; set; }
    }
}