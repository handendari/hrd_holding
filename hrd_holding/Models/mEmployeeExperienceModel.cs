using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class mEmployeeExperienceModel
    {
        public string employee_code {get;set;}
        public string employee_name { get; set; }
        public int seq_no { get; set; }
        public DateTime? start_working { get; set; }
        public DateTime? end_working { get; set; }
        public string company_name { get; set; }
        public string usaha { get; set; }
        public string department_name { get; set; }
        public string last_title { get; set; }
        public decimal last_salary { get; set; }
        public string reason_stop_working { get; set; }
        public string description { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
        public DateTime? edit_date { get; set; }
        public string edit_user { get; set; }
    }
}