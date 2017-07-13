using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class hrdRecruitmentSkill
    {
        public int req_id { get; set; }
        public int recruitment_id { get; set; }
        public int seq_no { get; set; }
        public string skill { get; set; }
        public Boolean flag_level { get; set; }
        public string name_level { get; set; }
        public string description { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
    }
}