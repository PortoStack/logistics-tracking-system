using LogisticsTrackingSystem.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace LogisticsTrackingSystem.Services
{
    public class AuthService
    {
        private readonly DataClassesDataContext db;

        public AuthService(DataClassesDataContext db)
        {
            this.db = db;
        }

        public string SignIn(EmployeeModel input)
        {
            try
            {
                var employee = (from e in db.employees
                                where e.email == input.email
                                select new EmployeeModel
                                { 
                                    name = e.name,
                                    role = e.role,
                                    password = e.password,
                                    id = e.id,
                                }).FirstOrDefault();

                if (employee == null) return "Employee not found";
                if (employee.password != input.password) return "Incorrect password";

                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                     message = "Login successful",
                     role = employee.role,
                     name = employee.name,
                     id = employee.id,
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    message = ex.Message,
                });
            }
        }
    }
}