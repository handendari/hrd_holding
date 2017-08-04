using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class hrdRecruitmentExpModel
    {
        public int id { get; set; }
        public int request_id { get; set; }
        public int recruitment_id { get; set; }
        public int seq_no { get; set; }
        public string name_employer { get; set; }
        public string business { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string position_held { get; set; }
        public decimal last_salary { get; set; }
        public string reason_leave { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
    }
}