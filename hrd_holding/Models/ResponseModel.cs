using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class ResponseModel
    {
        public bool isValid { get; set; }
        public string message { get; set; }

        public int total_record { get; set; }
        public int total_page { get; set; }

        public object objResult { get; set; }
    }
}