using LogisticsTrackingSystem;
using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using LogisticsTrackingSystem.Models;

namespace LogisticsTrackingSystem
{
    [ServiceContract]
    public interface IdbService
    {
        [WebInvoke(
            Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            UriTemplate = "customers")]
        CustomerModel[] GetCustomers();

        [WebInvoke(
            Method = "POST", 
            UriTemplate = "customers", 
            ResponseFormat = WebMessageFormat.Json, 
            RequestFormat = WebMessageFormat.Json)]
        string InsertCustomer(CustomerModel input);

        [WebInvoke(
            Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            UriTemplate = "parcels")]
        ParcelModel[] GetParcels();

        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "parcels/{trackingNo}")]
        ParcelModel GetParcelByTrackingNo(string trackingNo);
    }
}
