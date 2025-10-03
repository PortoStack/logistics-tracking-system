using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogisticsTrackingSystem.Models
{
    public class ParcelModel
    {
        public string trackingNo;
        public string type;
        public float weight;
        public string status;
        public string sender;
        public string receiver;
        public string origin;
        public string destination;
    }
}