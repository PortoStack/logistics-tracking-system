using LogisticsTrackingSystem.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
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

        public LocationModel GetById(string id)
        {
            var locationId = int.Parse(id);

            var locations = from l in db.locations
                            where l.id == locationId
                            select new LocationModel
                            {
                                id = l.id,
                                name = l.name,
                                address = l.address,
                                contact = l.contact,
                                type = l.type,
                            };
            return locations.FirstOrDefault();
        }

        public LocationModel[] GetByContact(string contact)
        {
            var locations = from l in db.locations
                            where l.contact == contact
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
            db.Connection.Open();
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

        public string Update(LocationModel input)
        {
            db.Connection.Open();
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
                var locationId = int.Parse(id);

                var location = db.locations.FirstOrDefault(l => l.id == locationId);
                if (location == null)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        message = "Location not found",
                    });
                }

                db.locations.DeleteOnSubmit(location);
                db.SubmitChanges();
                transaction.Commit();

                return $"Updated Location Id = {location.id}";
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