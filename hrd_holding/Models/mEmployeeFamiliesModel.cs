using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class mEmployeeFamiliesModel
    {
        public string employee_code {get;set;}
        public string employee_name { get; set; }
        public int seq_no { get; set; }
        public string name { get; set; }
        public int relationship { get; set; }
        public string nm_rel { get; set; }
        public DateTime? date_birth { get; set; }
        public int sex { get; set; }
        public string education { get; set; }
        public string employment { get; set; }
        public int chk_address { get; set; }
        public string address { get; set; }
        public DateTime? entry_date { get; set; }
        public string entry_user { get; set; }
        public DateTime? edit_date { get; set; }
        public string edit_user { get; set; }
    }
}