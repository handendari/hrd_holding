using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class mEmployeeGradeModel
    {
        public string employee_code {get;set;}
        public string employee_name { get; set; }
        public int seq_no { get; set; }
        public DateTime? date_grade { get; set; }
        public string grade_code { get; set; }
        public string description { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
        public DateTime? edit_date { get; set; }
        public string edit_user { get; set; }
    }
}