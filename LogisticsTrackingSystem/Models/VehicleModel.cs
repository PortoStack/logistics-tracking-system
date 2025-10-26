using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogisticsTrackingSystem.Models
{
    public class VehicleModel
    {
        public int id { get; set; }
        public string license_plate { get; set; } 
        public string status { get; set; }
        public int capacity { get; set; }
        public int driver_id { get; set; }

        public EmployeeModel driver { get; set; }
    }
}