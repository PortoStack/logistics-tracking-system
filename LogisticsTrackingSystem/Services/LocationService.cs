using LogisticsTrackingSystem.Models;
using System;
using System.Linq;

namespace LogisticsTrackingSystem.Services
{
    public class LocationService
    {
        private readonly DataClassesDataContext db;

        public LocationService(DataClassesDataContext db)
        {
            this.db = db;
        }

        public LocationModel[] GetAll()
        {
            var locations = from l in db.locations
                            select new LocationModel
                            {
                                id = l.id,
                                name = l.name,
                                address = l.address,
                                contact = l.contact,
                                type = l.type,
                            };
            return locations.ToArray();
        }

        public string Insert(LocationModel input)
        {
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var location = new location
                {
                    name = input.name,
                    address = input.address,
                    contact = input.contact,
                    type = input.type,
                };

                db.locations.InsertOnSubmit(location);
                db.SubmitChanges();
                transaction.Commit();

                return $"Inserted Location Id = {location.id}";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error: {ex.Message}";
            }
        }

        public string Update(LocationModel input)
        {
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var location = db.locations.FirstOrDefault(l => l.id == input.id);

                location.name = input.name;
                location.address = input.address;
                location.contact = input.contact;
                location.type = input.type;

                db.SubmitChanges(); 
                transaction.Commit();

                return $"Updated Location Id = {location.id}";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error: {ex.Message}";
            }
        }
    }
}