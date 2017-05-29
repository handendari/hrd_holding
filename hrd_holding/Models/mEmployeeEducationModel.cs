using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class mEmployeeEducationModel
    {
        public string employee_code {get;set;}
        public string employee_name { get; set; }
        public int seq_no { get; set; }
        public DateTime? start_year { get; set; }
        public DateTime? end_year { get; set; }
        public int jenjang { get; set; }
        public string nm_jenjang { get; set; }
        public string jurusan { get; set; }
        public string school { get; set; }
        public string city { get; set; }
        public string country_code { get; set; }
        public string int_country { get; set; }
        public string country_name { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
        public DateTime? edit_date { get; set; }
        public string edit_user { get; set; }
    }
}