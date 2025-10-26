using LogisticsTrackingSystem.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LogisticsTrackingSystem
{
    [ServiceContract]
    public interface IDbService
    {
        // Authentication endpoints
        [WebInvoke(Method = "POST", UriTemplate = "signin", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string SignIn(EmployeeModel input);

        [WebInvoke(Method = "POST", UriTemplate = "signup", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string SignUp(EmployeeModel input);

        [WebInvoke(Method = "POST", UriTemplate = "signout", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string SignOut();

        // Customer endpoints
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "customers")]
        CustomerModel[] GetCustomers();

        [WebInvoke(Method = "POST", UriTemplate = "customers", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string InsertCustomer(CustomerModel input);

        [WebInvoke(Method = "PUT", UriTemplate = "customers", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string UpdateCustomer(CustomerModel input);

        [WebInvoke(Method = "DELETE", UriTemplate = "customers", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string DeleteCustomer(int id);

        // Employee endpoints
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "employees")]
        EmployeeModel[] GetEmployees();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "employees/{id}")]
        EmployeeModel GetEmployeeById(string id);

        [WebInvoke(Method = "POST", UriTemplate = "employees", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string InsertEmployee(EmployeeModel input);

        [WebInvoke(Method = "PUT", UriTemplate = "employees", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string UpdateEmployee(EmployeeModel input);

        [WebInvoke(Method = "DELETE", UriTemplate = "employees", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string DeleteEmployee(int id);

        // Parcel endpoints
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "parcels")]
        ParcelModel[] GetParcels();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "parcels/{id}")]
        ParcelModel GetParcelById(string id);

        [WebInvoke(Method = "POST", UriTemplate = "parcels", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string InsertParcel(ParcelModel parcel_input, List<CustomerModel> customer_input, List<LocationModel> location_input);

        // Parcel Log endpoints
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "tracking/{id}")]
        ParcelLogModel[] GetParcelTracking(string id);
    }
}
