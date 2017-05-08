using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class mEmployeeSkillModel
    {
        public string employee_code {get;set;}
        public string employee_name { get; set; }
        public int seq_no { get; set; }
        public string skill { get; set; }
        public int level { get; set; }
        public string nm_level { get; set; }
        public int flag_skill { get; set; }
        public string description { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
        public DateTime? edit_date { get; set; }
        public string edit_user { get; set; }
    }
}