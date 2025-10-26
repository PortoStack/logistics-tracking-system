using LogisticsTrackingSystem.Models;
using System;
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
            var employee = from e in db.employees
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

        public string Insert(EmployeeModel input)
        {
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
                };

                db.employees.InsertOnSubmit(employee);
                db.SubmitChanges();
                transaction.Commit();

                return $"Inserted Employee Id = {employee.id}";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error: {ex.Message}";
            }
        }

        public string Update(EmployeeModel input)
        {
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var employee = db.employees.FirstOrDefault(e => e.id == input.id);
                if (employee == null) return "Employee not found";

                employee.name = input.name;
                employee.email = input.email;
                employee.phone = input.phone;
                employee.role = input.role;

                db.SubmitChanges();
                transaction.Commit();

                return $"Updated Employee Id = {employee.id}";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error: {ex.Message}";
            }
        }

        public string Delete(int id)
        {
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var employee = db.employees.FirstOrDefault(e => e.id == id);
                if (employee == null) return "Employee not found";

                db.employees.DeleteOnSubmit(employee);
                db.SubmitChanges();
                transaction.Commit();

                return $"Deleted Employee Id = {employee.id}";
            }
            catch (Exception ex) { 
                transaction.Rollback();
                return $"Error: {ex.Message}";
            }
        }
    }
}