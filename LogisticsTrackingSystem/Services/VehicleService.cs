using LogisticsTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace LogisticsTrackingSystem.Services
{
    public class VehicleService
    {
        private readonly DataClassesDataContext db;

        public VehicleService(DataClassesDataContext db)
        {
            this.db = db;
        }

        public VehicleModel[] GetAll()
        {
            var vehicles = from v in db.vehicles
                           join e in db.employees on v.driver_id equals e.id
                           select new VehicleModel
                           {
                               id = v.id,
                               status = v.status,
                               license_plate = v.license_plate,
                               capacity = v.capacity,
                               driver_id = e.id,

                               driver = new EmployeeModel
                               {
                                   id = e.id,
                                   name = e.name,
                                   email = e.email,
                               }
                           };
            return vehicles.ToArray();
        }

        public VehicleModel GetById(string id)
        {
            var vehicleId = int.Parse(id);

            var vehicles = from v in db.vehicles
                           join e in db.employees on v.driver_id equals e.id
                           where v.id == vehicleId
                           select new VehicleModel
                           {
                               id = v.id,
                               status = v.status,
                               license_plate = v.license_plate,
                               capacity = v.capacity,
                               driver_id = e.id,

                               driver = new EmployeeModel
                               {
                                   id = e.id,
                                   name = e.name,
                                   email = e.email,
                               }
                           };
            return vehicles.FirstOrDefault();
        }

        public string Insert(VehicleModel input)
        {
            db.Connection.Open();
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var vehicle = new vehicle
                {
                    status = input.status,
                    capacity = input.capacity,
                    license_plate = input.license_plate,
                    driver_id = input.driver_id,
                };

                db.vehicles.InsertOnSubmit(vehicle);
                db.SubmitChanges();

                transaction.Commit();
                return JsonConvert.SerializeObject(new
                {
                    message = $"Inserted Vehicle Id = {vehicle.id}",
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

        public string Update(VehicleModel input)
        {
            db.Connection.Open();
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var vehicle = db.vehicles.FirstOrDefault(v => v.id == input.id);
                if (vehicle == null)
                {
                    transaction.Rollback();
                    return "Vehicle not found";
                }

                vehicle.status = input.status;
                vehicle.capacity = input.capacity;
                vehicle.license_plate = input.license_plate;
                vehicle.driver_id = input.driver_id;

                db.SubmitChanges();

                transaction.Commit();
                return JsonConvert.SerializeObject(new
                {
                    message = $"Updated Vehicle Id = {vehicle.id}",
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

        public string Delete(string id)
        {
            db.Connection.Open();
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var vehicleId = int.Parse(id);

                var vehicle = db.vehicles.FirstOrDefault(v => v.id == vehicleId);
                if (vehicle == null)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        message = "Vehicle not found",
                    });
                }

                db.vehicles.DeleteOnSubmit(vehicle);
                db.SubmitChanges();

                transaction.Commit();
                return JsonConvert.SerializeObject(new
                {
                    message = $"Deleted Vehicle Id = {vehicle.id}",
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
    }
}