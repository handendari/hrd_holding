using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class hrdRecruitmentEduModel
    {
        public int req_id { get; set; }
        public int recruitment_id { get; set; }
        public int seq_no { get; set; }
        public DateTime? start_year { get; set; }
        public DateTime? end_year { get; set; }
        public Boolean flag_achieved { get; set; }
        public string name_achieved { get; set; }
        public string school { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }

    }
}