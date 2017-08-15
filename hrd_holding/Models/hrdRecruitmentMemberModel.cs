using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class hrdRecruitmentMemberModel
    {
        public int id { get; set; }
        public int request_id { get; set; }
        public int recruitment_id { get; set; }
        public int seq_no { get; set; }
        public string name { get; set; }
        public DateTime? year_from { get; set; }
        public DateTime? year_to { get; set; }
        public string level { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
    }
}