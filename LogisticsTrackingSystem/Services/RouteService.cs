using LogisticsTrackingSystem.Models;
using System;
using System.Linq;
using Newtonsoft.Json;

namespace LogisticsTrackingSystem.Services
{
    public class RouteService
    {
        private readonly DataClassesDataContext db;

        public RouteService(DataClassesDataContext db)
        {
            this.db = db;
        }

        public RouteModel[] GetAll()
        {
            var routes = from r in db.routes
                         join lo in db.locations on r.origin_id equals lo.id
                         join ld in db.locations on r.destination_id equals ld.id
                         join v in db.vehicles on r.vehicle_id equals v.id
                         select new RouteModel
                         {
                             id = r.id,
                             origin_id = r.origin_id,
                             destination_id = r.destination_id,
                             vehicle_id = r.vehicle_id,
                             distance = (float)r.distance,
                             estimated_time = (int)r.estimated_time,
                             status = r.status,

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
                             },

                             vehicle = new VehicleModel 
                             {
                                 license_plate = v.license_plate,
                                 status = v.status,
                                 capacity = v.capacity,
                             },
                         };

            return routes.ToArray();
        }

        public RouteModel GetById(string id)
        {
            var routeId = int.Parse(id);

            var routes = from r in db.routes
                         join lo in db.locations on r.origin_id equals lo.id
                         join ld in db.locations on r.destination_id equals ld.id
                         join v in db.vehicles on r.vehicle_id equals v.id
                         where r.id == routeId
                         select new RouteModel
                         {
                             id = r.id,
                             origin_id = r.origin_id,
                             destination_id = r.destination_id,
                             vehicle_id = r.vehicle_id,
                             distance = (float)r.distance,
                             estimated_time = (int)r.estimated_time,
                             status = r.status,

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
                             },

                             vehicle = new VehicleModel
                             {
                                 license_plate = v.license_plate,
                                 status = v.status,
                                 capacity = v.capacity,
                             },
                         };

            return routes.FirstOrDefault();
        }

        public string Insert(RouteModel input)
        {
            db.Connection.Open();
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var route = new route
                {
                    origin_id = input.origin_id,
                    destination_id = input.destination_id,
                    vehicle_id = input.vehicle_id,
                    distance = (decimal) input.distance,
                    estimated_time = input.estimated_time,
                    status = "assigned",
                    assigned_at = DateTime.Now
                };

                db.routes.InsertOnSubmit(route);
                db.SubmitChanges();
                transaction.Commit();

                return JsonConvert.SerializeObject(new
                {
                    message = $"Inserted Route Id = {route.id}",
                });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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

        public string Update(RouteModel input)
        {
            db.Connection.Open();
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var route = db.routes.FirstOrDefault(r => r.id == input.id);

                route.status = input.status;

                db.SubmitChanges();
                transaction.Commit();

                return JsonConvert.SerializeObject(new
                {
                    message = $"Updated Route Id = {route.id}",
                });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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
                var routeId = int.Parse(id);

                var route = db.routes.FirstOrDefault(r => r.id == routeId);

                db.routes.DeleteOnSubmit(route);
                db.SubmitChanges();
                transaction.Commit();

                return JsonConvert.SerializeObject(new
                {
                    message = $"Deleted Route Id = {route.id}",
                });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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