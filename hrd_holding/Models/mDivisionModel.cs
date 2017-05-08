using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class mDivisionModel
    {
        public int division_code {get;set;}
        public string company_code { get; set; }
        public string company_name { get; set; }
        public string branch_code { get; set; }
        public string branch_name { get; set; }
        public string department_code { get; set; }
        public string department_name { get; set; }
        public string int_division { get; set; }
        public string division_name { get; set; }
        public string description { get; set; }
    }
}