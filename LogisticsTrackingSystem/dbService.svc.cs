using LogisticsTrackingSystem;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Configuration;

namespace LogisticsTrackingSystem
{

        
    public class dbService : IdbService
    {
        private DataClassesDataContext db;

        public dbService()
        {
            string conn = ConfigurationManager.ConnectionStrings["LogisticsDBConnectionString"].ConnectionString;
            db = new DataClassesDataContext(conn);
        }

        public CustomerObj[] customers()
        {
            var query = from c in db.customers
                        select new CustomerObj
                        {
                            id = c.id,
                            name = c.name,
                            email = c.email,
                            phone = c.phone,
                            type = c.type,
                        };

            return query.ToArray();
        }
    }
}
