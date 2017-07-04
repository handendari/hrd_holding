using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class mEmployeeStatusModel
    {
        public int status_code {get;set;}
        public string int_status { get; set; }
        public string status_name { get; set; }
        public int flag_period { get; set; }
        public string kode_pajak { get; set; }
        public string nama_pajak { get; set; }
        public string description { get; set; }
    }
}