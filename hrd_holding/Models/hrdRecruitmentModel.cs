﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class hrdRecruitmentModel
    {
        public int id { get; set; }
        public int req_id { get; set; }
        public string nik { get; set; }
        public string name { get; set; }
        public string place_birth { get; set; }
        public DateTime? date_birth { get; set; }
        public string address { get; set; }
        public string phone_number { get; set; }
        public string hp_number { get; set; }
        public int flag_gender { get; set; }
        public int flag_marital_status { get; set; }
        
        public int company_code { get; set; }
        public string int_company { get; set; }
        public string company_name { get; set; }

        public int branch_code { get; set; }
        public string int_branch { get; set; }
        public string branch_name { get; set; }

        public int? department_code { get; set; }
        public string int_department { get; set; }
        public string department_name { get; set; }

        public int? title_code { get; set; }
        public string int_title { get; set; }
        public string title_name { get; set; }

        public int status_code { get; set; }
        public string int_status { get; set; }
        public string status_name { get; set; }

        //`picture` blob,
        public string dialect_group { get; set; }
        public int flag_religion { get; set; }
        public int flag_driving_license { get; set; }
        public int flag_driving_class { get; set; }
        public string physical_disability { get; set; }
        public string name_employer { get; set; }
        public DateTime? date_started { get; set; }
        public DateTime? date_left { get; set; }
        public decimal initial_salary { get; set; }
        public decimal last_salary { get; set; }
        public string address_employer { get; set; }
        public string business_type { get; set; }
        public string no_employees { get; set; }
        public string position_held { get; set; }
        public string reason_leave { get; set; }
        public string notice_required { get; set; }
        public DateTime? earliest_date { get; set; }
        public string brieft_description { get; set; }
        public int flag_contact { get; set; }
        public decimal expected_salary { get; set; }
        public string sports { get; set; }
        public string hobbies { get; set; }
        public string member_club { get; set; }
        public string additional_info { get; set; }
        public Boolean flag_crime { get; set; }
        public string crime_detail { get; set; }
        public Boolean flag_friend { get; set; }
        public string friend_name { get; set; }
        public string friend_phone { get; set; }
        public Boolean flag_company { get; set; }
        public string company_state { get; set; }
        public string company_dept { get; set; }
        public string emergency_name { get; set; }
        public string emergency_phone { get; set; }
        public Boolean flag_agree1 { get; set; }
        public Boolean flag_agree2 { get; set; }
        public Boolean flag_agree3 { get; set; }
        public Boolean flag_type { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }

    }

    public class RecruitmentModel_All
    {
        public hrdRecruitmentModel recModel { get; set; }
        public List<hrdRecruitmentEduModel> listEdu { get; set; }
        public List<hrdRecruitmentExpModel> listExp { get; set; }
        public List<hrdRecruitmentFamModel> listFams { get; set; }
        public List<hrdRecruitmentMemberModel> listMem { get; set; }
        public List<hrdRecruitmentRefModel> listRef { get; set; }
        public List<hrdRecruitmentSkillModel> listSkill { get; set; }
    }
}