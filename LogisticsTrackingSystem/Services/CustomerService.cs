using LogisticsTrackingSystem.Models;
using System;
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
                           };
            return customer.ToArray();
        }

        public string Insert(CustomerModel input)
        {
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
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error: {ex.Message}";
            }
        }

        public string Update(CustomerModel input)
        {
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
                var customer = db.customers.FirstOrDefault(c => c.id == id);
                if (customer == null) return "Customer not found";

                db.customers.DeleteOnSubmit(customer);
                db.SubmitChanges();
                transaction.Commit();

                return $"Deleted Customer Id = {customer.id}";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error: {ex.Message}";
            }
        }
    }
}