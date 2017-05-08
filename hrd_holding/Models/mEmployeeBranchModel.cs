using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class mEmployeeBranchModel
    {
        public string nik {get;set;}
        public string employee_name { get; set; }
        public int seq_no { get; set; }
        public int company_code { get; set; }
        public string company_name { get; set; }
        public int branch_code { get; set; }
        public string branch_name { get; set; }
        public DateTime? date_branch { get; set; }
        public int flag_type { get; set; }
        public string description { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
        public DateTime? edit_date { get; set; }
        public string edit_user { get; set; }
    }
}