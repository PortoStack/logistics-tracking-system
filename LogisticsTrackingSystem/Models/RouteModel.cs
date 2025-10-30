namespace LogisticsTrackingSystem.Models
{
    public class RouteModel
    {
        public int id { get; set; }
        public float distance { get; set; }
        public string status { get; set; }
        public int estimated_time { get; set; }
        public int origin_id { get; set; }
        public int destination_id { get; set; }
        public int vehicle_id { get; set; }

        public LocationModel origin { get; set; }
        public LocationModel destination { get; set; }
        public VehicleModel vehicle { get; set; }
    }
}