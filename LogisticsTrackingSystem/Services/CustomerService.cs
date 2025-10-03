using LogisticsTrackingSystem.Models;
using System;
using System.Linq;

namespace LogisticsTrackingSystem.Services
{
    public class CustomerService
    {
        private DataClassesDataContext db;

        public CustomerService(DataClassesDataContext context)
        {
            db = context;
        }

        public CustomerModel[] GetAll()
        {
            return (from c in db.customers
                    select new CustomerModel
                    {
                        id = c.id,
                        name = c.name,
                        email = c.email,
                        phone = c.phone,
                        type = c.type
                    }).ToArray();
        }

        public string Insert(CustomerModel input)
        {
            try
            {
                // insert logic here
                
                return "ok";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "error: " + ex.Message;
            }
        }

        public string Update(CustomerModel input)
        {
            try
            {
                var customer = db.customers.SingleOrDefault(c => c.id == input.id);
                if (customer == null) return "customer not found";

                // update logic here

                return "ok";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "error: " + ex.Message;
            }
        }
    }
}
