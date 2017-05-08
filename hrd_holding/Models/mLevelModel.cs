using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Models
{
    public class mLevelModel
    {
          public int level_code {get;set;}
          public string int_level {get;set;}
          public DateTime? date_entry {get;set;}
          public DateTime? date_edit {get;set;}
          public string description {get;set;}
          public string user_entry {get;set;}
          public string user_edit {get;set;}
          public string level_name { get; set; }
    }
}