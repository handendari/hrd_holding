using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class hrdReqReqruitmentModel
    {
        public int id {get;set;}
        public int company_code {get;set;}
        public string int_company { get; set; }
        public string company_name { get; set; }
        public int branch_code {get;set;}
        public string int_branch { get; set; }
        public string branch_name { get; set; }
        public DateTime? date_req {get;set;}
        public string no_req {get;set;}
        public string position_need {get;set;}
        public string reason {get;set;}
        public int? sex {get;set;}
        public int? age_min {get;set;}
        public int? education {get;set;}
        public string job_experience {get;set;}
        public string english_skill {get;set;}
        public string certificate {get;set;}
        public int? marital_status {get;set;}
        public string job_title {get;set;}
        public string job_purpose {get;set;}
        public string responsibility {get;set;}
        public int? count_staff {get;set;}
        public string authority {get;set;}
        public string job_relationship {get;set;}
        public string job_self {get;set;}
        public int? source_employee {get;set;}
        public DateTime? work_plan {get;set;}
        public string note {get;set;}
        public int? count_needed {get;set;}
        public string request_by {get;set;}
        public int? flag_status {get;set;}
        public int? flag_approval {get;set;}
        public string user_approval {get;set;}
        public DateTime? entry_date {get;set;}
        public string entry_user {get;set;}
        public DateTime? edit_date {get;set;}
        public string edit_user {get;set;}
    }
}