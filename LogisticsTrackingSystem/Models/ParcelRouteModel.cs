namespace LogisticsTrackingSystem.Models
{
    public class ParcelRouteModel
    {
        public string parcel_id { get; set; }
        public int route_id { get; set; }
        public int sequence { get; set; }

        public RouteModel route { get; set; }
        public ParcelModel parcel { get; set; }
    }
}