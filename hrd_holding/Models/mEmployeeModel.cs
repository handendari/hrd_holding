using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class mEmployeeModel
    {
        public string employee_code {get;set;}
        public int seq_no { get; set; }
        public string nik { get; set; }
        public string nip { get; set; }
        public string employee_name { get; set; }
        public string employee_nick_name { get; set; }
        public string company_code { get; set; }
        public string int_company { get; set; }
        public string company_name { get; set; }
        public string branch_code { get; set; }
        public string int_branch { get; set; }
        public string branch_name { get; set; }
        public string department_code { get; set; }
        public string int_department { get; set; }
        public string department_name { get; set; }
        public string division_code { get; set; }
        public string division_name { get; set; }
        public string title_code { get; set; }
        public string int_title { get; set; }
        public string title_name { get; set; }
        public string subtitle_code { get; set; }
        public string int_subtitle { get; set; }
        public string subtitle_name { get; set; }
        public string level_code { get; set; }
        public string int_level { get; set; }
        public string level_name { get; set; }
        public string status_code { get; set; }
        public string int_status { get; set; }
        public string status_name { get; set; }
        public int flag_shiftable { get; set; }
        public int flag_transport { get; set; }
        public string place_birth { get; set; }
        public System.Nullable<DateTime> date_birth { get; set; }
        public int sex { get; set; }
        public int religion { get; set; }
        public int marital_status { get; set; }
        public int no_of_children { get; set; }
        public string emp_address { get; set; }
        public string npwp { get; set; }
        public string kode_pajak { get; set; }
        public int npwp_method { get; set; }
        public DateTime? npwp_registered_date { get; set; }
        public string npwp_address { get; set; }
        public string no_jamsostek { get; set; }
        public DateTime? jstk_registered_date { get; set; }
        public string bank_code { get; set; }
        public string int_bank { get; set; }
        public string bank_name { get; set; }
        public string bank_account { get; set; }
        public string bank_acc_name { get; set; }
        public DateTime? start_working { get; set; }
        public DateTime? appointment_date { get; set; }
        public string phone_number { get; set; }
        public string hp_number { get; set; }
        public string email { get; set; }
        public string country_code { get; set; }
        public string int_country { get; set; }
        public string country_name { get; set; }
        public string identity_number { get; set; }
        public string last_education { get; set; }
        public string last_employment { get; set; }
        public string description { get; set; }
        public int flag_active { get; set; }
        public DateTime? end_working { get; set; }
        public string reason { get; set; }
        public string picture { get; set; }
        public int salary_type { get; set; }
        public DateTime? tgl_mutasi { get; set; }
        public int flag_managerial { get; set; }
        public string spv_code { get; set; }
        public string spv_nik { get; set; }
        public string spv_name { get; set; }
        public string note1 { get; set; }
        public string note2 { get; set; }
        public string note3 { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
        public DateTime? edit_date { get; set; }
        public string edit_user { get; set; }
    }

    public class EmployeeModelAll
    {
        public mEmployeeModel empModel { get; set; }
        public List<mEmployeeContractModel> listContract { get; set; }
        public List<mEmployeeEducationModel> listEducation { get; set; }
        public List<mEmployeeExperienceModel> listExperience { get; set; }
        public List<mEmployeeFamiliesModel> listFamily { get; set; }
        public List<mEmployeeSkillModel> listSkill { get; set; }
        public List<mEmployeeTrainingModel> listTrain { get; set; }
    }
}