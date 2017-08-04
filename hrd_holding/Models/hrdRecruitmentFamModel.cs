using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class hrdRecruitmentFamModel
    {
        public int fam_id { get; set; }
        public int request_id { get; set; }
        public int recruitment_id { get; set; }
        public int seq_no { get; set; }
        public string name { get; set; }
        public int flag_relationship { get; set; }
        public string name_relationship { get; set; }
        public DateTime? date_birth { get; set; }
        public int flag_gender { get; set; }
        public string education { get; set; }
        public string occupation { get; set; }
        public string name_employer { get; set; }
        public string address { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
    }
}