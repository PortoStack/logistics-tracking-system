using LogisticsTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogisticsTrackingSystem.Services
{
    public class ParcelService
    {
        private readonly DataClassesDataContext db;

        public ParcelService(DataClassesDataContext db)
        {
            this.db = db;
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
                                 address= ld.address,
                                 contact = ld.contact,
                             }
                          };
            return parcels.ToArray();
        }

        public ParcelModel GetByID(string id)
        {
            var parcel = from p in db.parcels
                         join c1 in db.customers on p.sender_id equals c1.id
                         join c2 in db.customers on p.receiver_id equals c2.id
                         join l1 in db.locations on p.origin_id equals l1.id
                         join l2 in db.locations on p.destination_id equals l2.id
                         where p.id == id
                         select new ParcelModel
                         {
                             id = p.id,
                             weight = (float)p.weight,
                             type = p.type,
                         };
            return parcel.FirstOrDefault();
        }

        public string Insert(ParcelModel parcel_input, List<CustomerModel> customer_input, List<LocationModel> location_input)
        {
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

                var parcel = new parcel
                {
                    id = parcel_input.id,
                    status = parcel_input.status,
                    type = parcel_input.type,
                    weight = (decimal)parcel_input.weight,
                    sender_id = sender?.id ?? customers.First().id,
                    receiver_id = receiver?.id ?? customers.Last().id,
                    origin_id = origin?.id ?? locations.First().id,
                    destination_id = destination?.id ?? locations.Last().id
                };

                db.parcels.InsertOnSubmit(parcel);
                db.SubmitChanges();

                transaction.Commit();
                return $"Inserted Parcel Id = {parcel.id}";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error: {ex.Message}";
            }
        }

        public string Update(ParcelModel input)
        {
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var parcel = db.parcels.FirstOrDefault(p => p.id == input.id);
                if (parcel == null) return "Parcel not found";

                parcel.status = input.status;

                transaction.Commit();

                return $"Updated Parcel Id = {parcel.id}";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error: {ex.Message}";
            }
        }
    }
}