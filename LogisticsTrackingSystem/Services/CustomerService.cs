using LogisticsTrackingSystem.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace LogisticsTrackingSystem.Services
{
    public class CustomerService
    {
        private DataClassesDataContext db;
        public CustomerService(DataClassesDataContext db)
        {
            this.db = db;
        }

        public CustomerModel[] GetAll()
        {
            var customer = from c in db.customers
                           select new CustomerModel
                           {
                               id = c.id,
                               name = c.name,
                               email = c.email,
                               phone = c.phone,
                           };
            return customer.ToArray();
        }

        public CustomerModel GetByPhone(string phone)
        {
            var customer = from c in db.customers
                           where c.phone == phone
                           select new CustomerModel
                           {
                               id = c.id,
                               name = c.name,
                               email = c.email,
                               phone = c.phone,
                           };
            return customer.FirstOrDefault();
        }

        public string Insert(CustomerModel input)
        {
            db.Connection.Open();
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var customer = new customer
                {
                    name = input.name,
                    email = input.email,
                    phone = input.phone,
                };

                db.customers.InsertOnSubmit(customer);
                db.SubmitChanges();

                transaction.Commit();
                return $"Inserted Customer Id = {customer.id}";
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                transaction.Rollback();
                Debug.WriteLine("SqlException: " + ex.Message);
                return $"Errror: {ex.Message}";
            }
            finally
            {
                db.Connection.Close();
            }
        }

        public string Update(CustomerModel input)
        {
            db.Connection.Open();
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var customer = db.customers.FirstOrDefault(c => c.id == input.id);
                if (customer == null) return "Customer not found";

                customer.name = input.name;
                customer.email = input.email;
                customer.phone = input.phone;

                db.SubmitChanges();

                transaction.Commit();
                return $"Updated Customer Id = {customer.id}";
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                transaction.Rollback();
                Debug.WriteLine("SqlException: " + ex.Message);
                return $"Errror: {ex.Message}";
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
                var customerId = int.Parse(id);

                var customer = db.customers.FirstOrDefault(c => c.id == customerId);
                if (customer == null) return "Customer not found";

                db.customers.DeleteOnSubmit(customer);
                db.SubmitChanges();

                transaction.Commit();
                return $"Deleted Customer Id = {customer.id}";
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                transaction.Rollback();
                Debug.WriteLine("SqlException: " + ex.Message);
                return $"Errror: {ex.Message}";
            }
            finally
            {
                db.Connection.Close();
            }
        }
    }
}