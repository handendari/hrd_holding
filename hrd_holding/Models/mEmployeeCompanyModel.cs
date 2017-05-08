using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class mEmployeeCompanyModel
    {
        public string employee_code {get;set;}
        public string employee_name { get; set; }
        public int seq_no { get; set; }
        public DateTime? date_company { get; set; }
        public int company_code { get; set; }
        public string company_name { get; set; }
        public int branch_code { get; set; }
        public string branch_name { get; set; }
        public int department_code { get; set; }
        public string department_name { get; set; }
        public int title_code { get; set; }
        public string title_name { get; set; }
        public int subtitle_code { get; set; }
        public string subtitle_name { get; set; }
        public string description { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
        public DateTime? edit_date { get; set; }
        public string edit_user { get; set; }
    }
}