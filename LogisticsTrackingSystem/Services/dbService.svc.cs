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
        }

        // Authentication services
        public string SignIn(EmployeeModel input) => authService.SignIn(input);
        public string SignUp(EmployeeModel input) => employeeService.Insert(input);
        public string SignOut() => authService.SignOut();

        // Customer services
        public CustomerModel[] GetCustomers() => customerService.GetAll();
        public string InsertCustomer(CustomerModel input) => customerService.Insert(input);
        public string UpdateCustomer(CustomerModel input) => customerService.Update(input);
        public string DeleteCustomer(int id) => customerService.Delete(id); 

        // Employee services
        public EmployeeModel[] GetEmployees() => employeeService.GetAll();
        public EmployeeModel GetEmployeeById(string id) => employeeService.GetById(id);
        public string InsertEmployee(EmployeeModel input) => employeeService.Insert(input);
        public string UpdateEmployee(EmployeeModel input) => employeeService.Update(input);
        public string DeleteEmployee(int id) => employeeService.Delete(id);

        // Parcel services
        public ParcelModel[] GetParcels() => parcelService.GetAll();
        public ParcelModel GetParcelById(string id) => parcelService.GetByID(id);
        public string InsertParcel(ParcelModel parcel_input, List<CustomerModel> customer_input, List<LocationModel> location_input) => parcelService.Insert(parcel_input, customer_input, location_input);

        // Parcel Log services
        public ParcelLogModel[] GetParcelTracking(string id) => parcelLogService.GetTracking(id);
    }
}
