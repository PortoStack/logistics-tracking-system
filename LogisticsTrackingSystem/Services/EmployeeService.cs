using LogisticsTrackingSystem.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace LogisticsTrackingSystem.Services
{
    public class EmployeeService
    {
        private readonly DataClassesDataContext db;

        public EmployeeService(DataClassesDataContext db)
        {
            this.db = db;
        }

        public EmployeeModel[] GetAll()
        {
            var employees = from e in db.employees
                            select new EmployeeModel
                            {
                                id = e.id,
                                name = e.name,
                                email = e.email,
                                phone = e.phone,
                                role = e.role,
                            };
            return employees.ToArray();
        }

        public EmployeeModel GetById(string id)
        {
            var empId = int.Parse(id);

            var employee = from e in db.employees
                           where e.id == empId
                           select new EmployeeModel
                           {
                               id = (int)e.id,
                               name = e.name,
                               email = e.email,
                               phone = e.phone,
                               role = e.role,
                           };
            return employee.FirstOrDefault();
        }

        public EmployeeModel[] GetByRole(string role)
        {
            var employee = from e in db.employees
                           where e.role == role
                           select new EmployeeModel
                           {
                               id = (int)e.id,
                               name = e.name,
                               email = e.email,
                               phone = e.phone,
                               role = e.role,
                           };
            return employee.ToArray();
        }

        public string Insert(EmployeeModel input)
        {
            db.Connection.Open();
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var employee = new employee
                {
                    name = input.name,
                    phone = input.phone,
                    email = input.email,
                    password = input.password,
                    role = "employee",
                };

                db.employees.InsertOnSubmit(employee);
                db.SubmitChanges();


                transaction.Commit();
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    message = $"Inserted Employee Id = {employee.id}",
                });
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                transaction.Rollback();
                Debug.WriteLine("SqlException: " + ex.Message);
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    message = $"Errror: {ex.Message}",
                });
            }
            finally 
            { 
                db.Connection.Close(); 
            }
        }

        public string Update(EmployeeModel input)
        {
            db.Connection.Open();
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var employee = db.employees.FirstOrDefault(e => e.id == input.id);
                if (employee == null)
                {
                    transaction.Rollback();
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        message = "Employee not found",
                    });
                }

                employee.name = input.name;
                employee.email = input.email;
                employee.phone = input.phone;
                employee.role = input.role;

                db.SubmitChanges();

                transaction.Commit();
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    message = $"Updated Employee Id = {employee.id}",
                });
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                transaction.Rollback();
                Debug.WriteLine("SqlException: " + ex.Message);
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    message = $"Errror: {ex.Message}",
                });
            }
            finally
            {
                db.Connection.Close();
            }
        }

        public string Delete(string id)
        {
            db.Connection.Open();
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var empId = int.Parse(id);

                var employee = db.employees.FirstOrDefault(e => e.id == empId);
                if (employee == null) return "Employee not found";

                db.employees.DeleteOnSubmit(employee);
                db.SubmitChanges();

                transaction.Commit();
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    message = $"Deleted Employee Id = {employee.id}",
                });
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                transaction.Rollback();
                Debug.WriteLine("SqlException: " + ex.Message);
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    message = $"Errror: {ex.Message}",
                });
            }
            finally
            {
                db.Connection.Close();
            }
        }
    }
}