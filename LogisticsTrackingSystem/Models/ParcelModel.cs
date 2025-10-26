namespace LogisticsTrackingSystem.Models
{
    public class ParcelModel
    {
        public string id { get; set; }
        public string status { get; set; }
        public float weight { get; set; }
        public string type { get; set; }
        public int sender_id { get; set; }
        public int receiver_id { get; set; }
        public int origin_id { get; set; }
        public int destination_id { get; set; }

        public CustomerModel sender { get; set; }
        public CustomerModel receiver { get; set; }
        public LocationModel origin { get; set; }
        public LocationModel destination { get; set; }
    }
}