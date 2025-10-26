using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogisticsTrackingSystem.Models
{
    public class EmployeeModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string role { get; set; }
    }
}