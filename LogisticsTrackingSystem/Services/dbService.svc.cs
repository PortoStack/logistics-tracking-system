using LogisticsTrackingSystem.Models;
using System.Configuration;

namespace LogisticsTrackingSystem.Services
{
    public class dbService : IdbService
    {
        private readonly DataClassesDataContext db;
        private readonly CustomerService customerService;
        private readonly ParcelService parcelService;

        public dbService()
        {
            string conn = ConfigurationManager.ConnectionStrings["LogisticsDBConnectionString"].ConnectionString;
            db = new DataClassesDataContext(conn);

            customerService = new CustomerService(db);
            parcelService = new ParcelService(db);
        }

        // Customer endpoints
        public CustomerModel[] GetCustomers() => customerService.GetAll();
        public string InsertCustomer(CustomerModel input) => customerService.Insert(input);
        public string UpdateCustomer(CustomerModel input) => customerService.Update(input);

        // Parcel endpoints
        public ParcelModel[] GetParcels() => parcelService.GetAll();

        public ParcelModel GetParcelByTrackingNo(string trackingNo) => parcelService.GetByTrackingNo(trackingNo);
    }
}
