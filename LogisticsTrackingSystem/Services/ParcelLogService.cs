using LogisticsTrackingSystem.Models;
using System.Linq;

namespace LogisticsTrackingSystem.Services
{
    public class ParcelLogService
    {
        private readonly DataClassesDataContext db;

        public ParcelLogService(DataClassesDataContext db)
        {
            this.db = db;
        }

        public ParcelLogModel[] GetTracking(string id)
        {
            var parcelLog = from pl in db.parcel_logs
                            join e in db.employees on pl.employee_id equals e.id into emp
                            from e in emp.DefaultIfEmpty()

                            join ld in db.locations on pl.location_id equals ld.id into locDes
                            from ld in locDes.DefaultIfEmpty()

                            join pr in db.parcel_routes on new { pl.parcel_id, pl.route_id }
                                                equals new { pr.parcel_id, route_id = pr.route_id } 
                                                into parcelRou
                            from pr in parcelRou.DefaultIfEmpty()

                            join r in db.routes on pl.route_id equals r.id into rou
                            from r in rou.DefaultIfEmpty()

                            join lo in db.locations on r.origin_id equals lo.id into locOri
                            from lo in locOri.DefaultIfEmpty()

                            where pl.parcel_id == id
                            orderby pr.sequence descending, pl.timestamp descending
                            select new ParcelLogModel
                            {
                                id = pl.id,
                                timestamp = pl.timestamp.Value.ToString(),
                                action = pl.status,
                                note = pl.note,
                                location_id = pl.location_id,
                                employee_id = pl.employee_id,
                                route_id = pl.route_id,
                                sequence = pr.sequence,

                                current_location = new LocationModel
                                {
                                    name = ld.name,
                                    address = ld.address,
                                    contact = ld.contact,
                                },
                                handled_by = new EmployeeModel
                                {
                                    name = e.name,
                                    email = e.email,
                                    role = e.role,
                                },
                                route = new RouteModel
                                {
                                    status = r.status,
                                    distance = (float)r.distance,
                                    estimated_time = (int)r.estimated_time,
                                }
                            };
            return parcelLog.ToArray();
        }
    }
}