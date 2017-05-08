using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class mCityModel
    {
      public string city_code {get;set;}
      public string city_name { get; set; }
      public DateTime? entry_date { get; set; }
      public int deleted { get; set; }
      public DateTime? delete_date { get; set; }
    }
}