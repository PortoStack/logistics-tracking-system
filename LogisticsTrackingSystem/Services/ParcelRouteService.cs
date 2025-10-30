using LogisticsTrackingSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogisticsTrackingSystem.Services
{
    public class ParcelRouteService
    {
        private readonly DataClassesDataContext db;

        public ParcelRouteService(DataClassesDataContext db)
        {
            this.db = db;
        }

        public ParcelRouteModel[] GetByParcelId(string id)
        {
            var parcelRoute = from pr in db.parcel_routes
                              join r in db.routes on pr.route_id equals r.id
                              join lo in db.locations on r.origin_id equals lo.id
                              join ld in db.locations on r.destination_id equals ld.id
                              join v in db.vehicles on r.vehicle_id equals v.id
                              where pr.parcel_id == id
                              orderby pr.sequence ascending
                              select new ParcelRouteModel
                              {
                                  parcel_id = pr.parcel_id,
                                  route_id = pr.route_id,
                                  sequence = pr.sequence,

                                  route = new RouteModel
                                  {
                                      distance = (float) r.distance,
                                      estimated_time = (int) r.estimated_time,
                                      vehicle_id = r.vehicle_id,
                                      origin_id = r.origin_id,
                                      destination_id = r.destination_id,

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
                                          capacity = v.capacity,
                                          status = v.status,
                                      }
                                  }
                              };
            return parcelRoute.ToArray();
        }

        public ParcelRouteModel[] GetByDriverId(string id)
        {
            var driverId = int.Parse(id);

            var parcelRoute = from pr in db.parcel_routes
                              join r in db.routes on pr.route_id equals r.id
                              join p in db.parcels on pr.parcel_id equals p.id
                              join lo in db.locations on r.origin_id equals lo.id
                              join ld in db.locations on r.destination_id equals ld.id
                              join v in db.vehicles on r.vehicle_id equals v.id
                              where v.driver_id == driverId
                              orderby pr.sequence ascending
                              select new ParcelRouteModel
                              {
                                  parcel_id = pr.parcel_id,
                                  route_id = pr.route_id,
                                  sequence = pr.sequence,

                                  parcel = new ParcelModel
                                  {
                                      id = p.id,
                                      weight = (float) p.weight,
                                      type = p.type,
                                      status = p.status,
                                      origin_id = p.origin_id,
                                      destination_id = p.destination_id,
                                  },

                                  route = new RouteModel
                                  {
                                      distance = (float)r.distance,
                                      status = r.status,
                                      estimated_time = (int)r.estimated_time,
                                      vehicle_id = r.vehicle_id,
                                      origin_id = r.origin_id,
                                      destination_id = r.destination_id,

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
                                          capacity = v.capacity,
                                          status = v.status,
                                      }
                                  }
                              };
            return parcelRoute.ToArray();
        }

        public string Insert(ParcelModel parcel_input, RouteModel route_input, EmployeeModel employee)
        {
            db.Connection.Open();
            var transaction = db.Connection.BeginTransaction();
            db.Transaction = transaction;

            try
            {
                var parcel = db.parcels.FirstOrDefault(p => p.id == parcel_input.id);
                if (parcel == null) throw new Exception("Parcel not found");

                int newSequence = db.parcel_routes
                                    .Where(pr => pr.parcel_id == parcel_input.id)
                                    .Max(pr => (int?)pr.sequence) ?? 0;
                newSequence += 1;

                var parcelRoute = new parcel_route
                {
                    parcel_id = parcel_input.id,
                    route_id = route_input.id,
                    sequence = newSequence
                };
                db.parcel_routes.InsertOnSubmit(parcelRoute);

                db.SubmitChanges();
                transaction.Commit();

                return JsonConvert.SerializeObject(new
                {
                    message = $"Parcel {parcel_input.id} assigned to {route_input} routes"
                });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return JsonConvert.SerializeObject(new { message = ex.Message });
            }
            finally
            {
                db.Connection.Close();
            }
        }

    }
}