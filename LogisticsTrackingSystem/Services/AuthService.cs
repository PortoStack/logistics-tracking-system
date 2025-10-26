using LogisticsTrackingSystem.Models;
using System;
using System.Linq;
using System.Web;

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
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var employee = (from e in db.employees
                               where e.id == input.id
                               select new EmployeeModel()).FirstOrDefault();

                if (employee == null) return "Employee not found";
                if (employee.password != input.password) return "Incorrect password";

                HttpContext.Current.Session["EmployeeId"] = employee.id;
                HttpContext.Current.Session["EmployeeName"] = employee.name;
                HttpContext.Current.Session["EmployeeRole"] = employee.role;

                transaction.Commit();

                return "Login successful";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error: {ex.Message}";
            }
        }

        public string SignOut()
        {
            HttpContext.Current.Session.Clear();
            return "Logout successful";
        }

        public bool IsLoggedIn()
        {
            return HttpContext.Current.Session["EmployeeId"] != null;
        }

        public bool HasRole(string role)
        {
            return HttpContext.Current.Session["EmployeeRole"]?.ToString() != role;
        }
    }
}