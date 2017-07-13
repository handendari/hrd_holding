using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class hrdRecruitmentRefModel
    {
        public int req_id { get; set; }
        public int recruitment_id { get; set; }
        public int seq_no { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone_number { get; set; }
        public string occupation { get; set; }
        public DateTime? year_known { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
    }
}