using LogisticsTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace LogisticsTrackingSystem.Services
{
    public class ParcelService
    {
        private readonly DataClassesDataContext db;

        public ParcelService(DataClassesDataContext db)
        {
            this.db = db;
        }

        private string GenerateParcelId(string type)
        {
            string prefix = type.ToLower() == "standard" ? "ST" : "ET";

            Random rnd = new Random();
            int number = rnd.Next(0, 999999999);
            string numberStr = number.ToString("D9");

            return prefix + numberStr;
        }

        public ParcelModel[] GetAll()
        {
            var parcels = from p in db.parcels
                          join cs in db.customers on p.sender_id equals cs.id
                          join cr in db.customers on p.receiver_id equals cr.id
                          join lo in db.locations on p.origin_id equals lo.id
                          join ld in db.locations on p.destination_id equals ld.id
                          select new ParcelModel
                          {
                              id = p.id,
                              weight = (float)p.weight,
                              status = p.status,
                              type = p.type,
                              sender_id = p.sender_id,
                              receiver_id = p.receiver_id,
                              origin_id = p.origin_id,
                              destination_id = p.destination_id,

                              sender = new CustomerModel
                              {
                                  name = cs.name,
                                  email = cs.email,
                                  phone = cs.phone,
                              },
                              receiver = new CustomerModel
                              {
                                  name = cr.name,
                                  email = cr.email,
                                  phone = cr.phone,
                              },

                              origin = new LocationModel
                              {
                                  name = lo.name,
                                  address = lo.address,
                                  contact = lo.contact,
                              },
                              destination = new LocationModel
                              {
                                  name = ld.name,
                                  address = ld.address,
                                  contact = ld.contact,
                              }
                          };
            return parcels.ToArray();
        }

        public ParcelModel GetByID(string id)
        {
            var parcel = from p in db.parcels
                         join cs in db.customers on p.sender_id equals cs.id
                         join cr in db.customers on p.receiver_id equals cr.id
                         join lo in db.locations on p.origin_id equals lo.id
                         join ld in db.locations on p.destination_id equals ld.id
                         where p.id == id
                         select new ParcelModel
                         {
                             id = p.id,
                             weight = (float)p.weight,
                             status = p.status,
                             type = p.type,
                             sender_id = p.sender_id,
                             receiver_id = p.receiver_id,
                             origin_id = p.origin_id,
                             destination_id = p.destination_id,

                             sender = new CustomerModel
                             {
                                 name = cs.name,
                                 email = cs.email,
                                 phone = cs.phone,
                             },
                             receiver = new CustomerModel
                             {
                                 name = cr.name,
                                 email = cr.email,
                                 phone = cr.phone,
                             },

                             origin = new LocationModel
                             {
                                 name = lo.name,
                                 address = lo.address,
                                 contact = lo.contact,
                                 type = lo.type,
                             },
                             destination = new LocationModel
                             {
                                 name = ld.name,
                                 address = ld.address,
                                 contact = ld.contact,
                                 type = ld.type,
                             }
                         };
            return parcel.FirstOrDefault();
        }

        public StatModel[] GetParcelStat()
        {
            var stat = from p in db.parcels
                       group p by p.status into g
                       select new StatModel
                       {
                           status = g.Key,
                           count = g.Count(),
                       };
            return stat.ToArray();
        }

        public string Insert(ParcelModel parcel_input, List<CustomerModel> customer_input, List<LocationModel> location_input, EmployeeModel employee)
        {
            db.Connection.Open();
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var customers = new List<customer>();
                var locations = new List<location>();

                foreach (var customer in customer_input)
                {
                    var existingCustomer = db.customers
                        .FirstOrDefault(c => c.email == customer.email || c.phone == customer.phone);

                    if (existingCustomer != null)
                    {
                        customers.Add(existingCustomer);
                    }
                    else
                    {
                        var newCustomer = new customer
                        {
                            name = customer.name,
                            email = customer.email,
                            phone = customer.phone,
                        };

                        db.customers.InsertOnSubmit(newCustomer);
                        customers.Add(newCustomer);
                    }
                }

                foreach (var location in location_input)
                {
                    var existingLocation = db.locations
                        .FirstOrDefault(loc => loc.address == location.address && loc.type == location.type);

                    if (existingLocation != null)
                    {
                        locations.Add(existingLocation);
                    }
                    else
                    {
                        var newLocation = new location
                        {
                            name = location.name,
                            address = location.address,
                            contact = location.contact,
                            type = location.type,
                        };

                        db.locations.InsertOnSubmit(newLocation);
                        locations.Add(newLocation);
                    }
                }

                db.SubmitChanges();

                var sender = customers.FirstOrDefault(cs => cs.id == parcel_input.sender_id);
                var receiver = customers.FirstOrDefault(cr => cr.id == parcel_input.receiver_id);
                var origin = locations.FirstOrDefault(lo => lo.id == parcel_input.origin_id);
                var destination = locations.FirstOrDefault(ld => ld.id == parcel_input.destination_id);

                string newId;
                do
                {
                    newId = GenerateParcelId(parcel_input.type);
                } while (db.parcels.Any(p => p.id == newId));

                var parcel = new parcel
                {
                    id = newId,
                    status = parcel_input.status,
                    type = parcel_input.type,
                    weight = (decimal)parcel_input.weight,
                    sender_id = sender?.id ?? customers.First().id,
                    receiver_id = receiver?.id ?? customers.Last().id,
                    origin_id = origin?.id ?? locations.First().id,
                    destination_id = destination?.id ?? locations.Last().id
                };

                db.parcels.InsertOnSubmit(parcel);

                var parcelLog = new parcel_log
                {
                    parcel_id = newId,
                    status = "received",
                    note = "Receive parcel into the system",
                    location_id = locations.First().id,
                    timestamp = DateTime.Now,
                    employee_id = employee.id,
                };

                db.parcel_logs.InsertOnSubmit(parcelLog);
                db.SubmitChanges();

                transaction.Commit();
                return JsonConvert.SerializeObject(new
                {
                    message = $"Inserted Parcel Id = {parcel.id}",
                });
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                transaction.Rollback();
                Debug.WriteLine("SqlException: " + ex.Message);
                return JsonConvert.SerializeObject(new
                {
                    message = ex.Message,
                });
            }
            finally
            {
                db.Connection.Close();
            }
        }

        public string Update(ParcelModel input)
        {
            db.Connection.Open();
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var parcel = db.parcels.FirstOrDefault(p => p.id == input.id);
                if (parcel == null)
                {
                    transaction.Rollback();
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        message = "Parcel not found",
                    });
                }

                parcel.status = input.status;

                db.SubmitChanges();

                transaction.Commit();
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    message = $"Updated Parcel Id = {parcel.id}",
                });
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                transaction.Rollback();
                Debug.WriteLine("SqlException: " + ex.Message);
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    message = ex.Message,
                });
            }
            finally
            {
                db.Connection.Close();
            }
        }
    }
}