using LogisticsTrackingSystem.Models;
using System.Collections.Generic;
using System.Configuration;

namespace LogisticsTrackingSystem.Services
{
    public class DbService : IDbService
    {
        private readonly DataClassesDataContext db;

        private readonly AuthService authService;
        private readonly CustomerService customerService;
        private readonly EmployeeService employeeService;
        private readonly ParcelService parcelService;
        private readonly ParcelLogService parcelLogService;
        private readonly LocationService locationService;
        private readonly VehicleService vehicleService;
        private readonly RouteService routeService;
        private readonly ParcelRouteService parcelRouteService;

        public DbService()
        {
            var conn = ConfigurationManager
                .ConnectionStrings["LogisticsDBConnectionString"]
                .ConnectionString;
            db = new DataClassesDataContext(conn);

            authService = new AuthService(db);
            customerService = new CustomerService(db);
            employeeService = new EmployeeService(db);
            parcelService = new ParcelService(db);
            parcelLogService = new ParcelLogService(db);
            locationService = new LocationService(db);
            vehicleService = new VehicleService(db);
            routeService = new RouteService(db);
            parcelRouteService = new ParcelRouteService(db);
        }

        // Authentication services
        public string SignIn(EmployeeModel input) => authService.SignIn(input);
        public string SignUp(EmployeeModel input) => employeeService.Insert(input);

        // Customer services
        public CustomerModel[] GetCustomers() => customerService.GetAll();
        public CustomerModel GetCustomerByPhone(string phone) => customerService.GetByPhone(phone);
        public string InsertCustomer(CustomerModel input) => customerService.Insert(input);
        public string UpdateCustomer(CustomerModel input) => customerService.Update(input);
        public string DeleteCustomer(string id) => customerService.Delete(id);

        // Employee services
        public EmployeeModel[] GetEmployees() => employeeService.GetAll();
        public EmployeeModel GetEmployeeById(string id) => employeeService.GetById(id);
        public EmployeeModel[] GetEmployeeByRole(string role) => employeeService.GetByRole(role);
        public string InsertEmployee(EmployeeModel input) => employeeService.Insert(input);
        public string UpdateEmployee(EmployeeModel input) => employeeService.Update(input);
        public string DeleteEmployee(string id) => employeeService.Delete(id);

        // Parcel services
        public ParcelModel[] GetParcels() => parcelService.GetAll();
        public ParcelModel GetParcelById(string id) => parcelService.GetByID(id);
        public StatModel[] GetParcelStat() => parcelService.GetParcelStat();
        public string InsertParcel(ParcelModel parcel_input, List<CustomerModel> customer_input, List<LocationModel> location_input, EmployeeModel employee) => parcelService.Insert(parcel_input, customer_input, location_input, employee);
        public string UpdateParcel(ParcelModel input) => parcelService.Update(input);

        // Parcel Log services
        public ParcelLogModel[] GetParcelLogs() => parcelLogService.GetParcelLogs();
        public ParcelLogModel[] GetParcelTracking(string id) => parcelLogService.GetTracking(id);
        public string InsertParcelLog(ParcelLogModel input) => parcelLogService.InsertLog(input);

        // Location services
        public LocationModel[] GetLocations() => locationService.GetAll();
        public LocationModel GetLocationById(string id) => locationService.GetById(id);
        public LocationModel[] GetLocationByContact(string contact) => locationService.GetByContact(contact);
        public string InsertLocation(LocationModel input) => locationService.Insert(input);
        public string UpdateLocation(LocationModel input) => locationService.Update(input);
        public string DeleteLocation(string id) => locationService.Delete(id);

        // Route service
        public RouteModel[] GetRoutes() => routeService.GetAll();
        public RouteModel GetRouteById(string id) => routeService.GetById(id);
        public string InsertRoute(RouteModel input) => routeService.Insert(input);
        public string UpdateRoutes(RouteModel input) => routeService.Update(input);
        public string DeleteRoutes(string id) => routeService.Delete(id);

        // Vehicle services
        public VehicleModel[] GetVehicles() => vehicleService.GetAll();
        public VehicleModel GetVehicleById(string id) => vehicleService.GetById(id);
        public string InsertVehicle(VehicleModel input) => vehicleService.Insert(input);
        public string UpdateVehicle(VehicleModel input) => vehicleService.Update(input);
        public string DeleteVehicle(string id) => vehicleService.Delete(id);

        // Parcel Route services
        public ParcelRouteModel[] GetParcelRouteByParcelId(string id) => parcelRouteService.GetByParcelId(id);
        public ParcelRouteModel[] GetParcelRouteByDriverId(string id) => parcelRouteService.GetByDriverId(id);
        public string InsertParcelRoute(ParcelModel parcel_input, RouteModel route_input, EmployeeModel employee) => parcelRouteService.Insert(parcel_input, route_input, employee);
    }
}
