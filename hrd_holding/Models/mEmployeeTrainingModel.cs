using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class mEmployeeTrainingModel
    {
        public string employee_code { get; set; }
        public string employee_name { get; set; }
        public int seq_no { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string material { get; set; }
        public string organizer { get; set; }
        public string place { get; set; }
        public string company { get; set; }
        public int chk_company { get; set; }
        public string value { get; set; }
        public int training_id { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
        public DateTime? edit_date { get; set; }
        public string edit_user { get; set; }
    }
}