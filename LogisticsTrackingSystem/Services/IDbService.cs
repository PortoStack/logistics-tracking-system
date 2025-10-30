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

        // Customer endpoints
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "customers")]
        CustomerModel[] GetCustomers();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "customers/{phone}")]
        CustomerModel GetCustomerByPhone(string phone);

        [WebInvoke(Method = "POST", UriTemplate = "customers", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string InsertCustomer(CustomerModel input);

        [WebInvoke(Method = "PUT", UriTemplate = "customers", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateCustomer(CustomerModel input);

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "customers/{id}")]
        string DeleteCustomer(string id);

        // Employee endpoints
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "employees")]
        EmployeeModel[] GetEmployees();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "employees/id/{id}")]
        EmployeeModel GetEmployeeById(string id);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "employees/role/{role}")]
        EmployeeModel[] GetEmployeeByRole(string role);

        [WebInvoke(Method = "POST", UriTemplate = "employees", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string InsertEmployee(EmployeeModel input);

        [WebInvoke(Method = "PUT", UriTemplate = "employees", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateEmployee(EmployeeModel input);

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "employees/{id}")]
        string DeleteEmployee(string id);

        // Parcel endpoints
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "parcels")]
        ParcelModel[] GetParcels();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "parcels/stat")]
        StatModel[] GetParcelStat();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "parcels/{id}")]
        ParcelModel GetParcelById(string id);

        [WebInvoke(Method = "POST", UriTemplate = "parcels", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string InsertParcel(ParcelModel parcel_input, List<CustomerModel> customer_input, List<LocationModel> location_input, EmployeeModel employee);

        [WebInvoke(Method = "PUT", UriTemplate = "parcels", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateParcel(ParcelModel input);

        // Parcel Log endpoints
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "parcel/logs")]
        ParcelLogModel[] GetParcelLogs();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "tracking/{id}")]
        ParcelLogModel[] GetParcelTracking(string id);

        [WebInvoke(Method = "POST", UriTemplate = "parcel/logs", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string InsertParcelLog(ParcelLogModel input);

        // Parcel Route endpoints
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "parcel/routes/{id}")]
        ParcelRouteModel[] GetParcelRouteByParcelId(string id);
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "parcel/routes/driver/{id}")]
        ParcelRouteModel[] GetParcelRouteByDriverId(string id);

        [WebInvoke(Method = "POST", UriTemplate = "parcel/routes", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string InsertParcelRoute(ParcelModel parcel_input, RouteModel route_input, EmployeeModel employee);

        // Route endpoints
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "routes")]
        RouteModel[] GetRoutes();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "routes/{id}")]
        RouteModel GetRouteById(string id);

        [WebInvoke(Method = "POST", UriTemplate = "routes", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string InsertRoute(RouteModel input);

        [WebInvoke(Method = "PUT", UriTemplate = "routes", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateRoutes(RouteModel input);

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "routes/{id}")]
        string DeleteRoutes(string id);

        // Vehicle endpoints
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "vehicles")]
        VehicleModel[] GetVehicles();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "vehicles/{id}")]
        VehicleModel GetVehicleById(string id);

        [WebInvoke(Method = "POST", UriTemplate = "vehicles", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string InsertVehicle(VehicleModel input);

        [WebInvoke(Method = "PUT", UriTemplate = "vehicles", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateVehicle(VehicleModel input);

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "vehicles/{id}")]
        string DeleteVehicle(string id);

        // Location endpoints
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "locations")]
        LocationModel[] GetLocations();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "locations/id/{id}")]
        LocationModel GetLocationById(string id);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "locations/contact/{contact}")]
        LocationModel[] GetLocationByContact(string contact);

        [WebInvoke(Method = "POST", UriTemplate = "locations", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string InsertLocation(LocationModel input);

        [WebInvoke(Method = "PUT", UriTemplate = "locations", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateLocation(LocationModel input);

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "locations/{id}")]
        string DeleteLocation(string id);
    }
}
