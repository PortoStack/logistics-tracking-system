using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogisticsTrackingSystem.Models
{
    public class LocationModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public string type { get; set; }
    }
}