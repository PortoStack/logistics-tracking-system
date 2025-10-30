namespace LogisticsTrackingSystem.Models
{
    public class ParcelLogModel
    {
        public int id { get; set; }
        public string parcel_id { get; set; }
        public string timestamp { get; set; }
        public string note { get; set; }
        public string action { get; set; }
        public int? location_id { get; set; }
        public int? employee_id { get; set; }
        public int? route_id { get; set; }
        public int? sequence { get; set; }

        public LocationModel current_location { get; set; }
        public EmployeeModel handled_by { get; set; }
        public RouteModel route { get; set; }
    }
}