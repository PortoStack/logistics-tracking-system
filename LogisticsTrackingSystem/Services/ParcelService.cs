using LogisticsTrackingSystem.Models;
using System;
using System.Linq;

namespace LogisticsTrackingSystem.Services
{
    public class ParcelService
    {
        private DataClassesDataContext db;

        public ParcelService(DataClassesDataContext context)
        {
            db = context;
        }

        public ParcelModel[] GetAll()
        {
            try
            {
                var query = from p in db.parcels
                            join w1 in db.warehouses on p.origin_id equals w1.id
                            join w2 in db.warehouses on p.destination_id equals w2.id
                            join c1 in db.customers on p.sender_id equals c1.id
                            join c2 in db.customers on p.receiver_id equals c2.id
                            select new ParcelModel
                            {
                                trackingNo = p.tracking_no,
                                type = p.type,
                                weight = (float)p.weight,
                                status = p.status,
                                sender = c1.name,
                                receiver = c2.name,
                                origin = w1.name,
                                destination = w2.name
                            };

                return query.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ParcelModel[0];
            }
        }
        public ParcelModel GetByTrackingNo(string trackingNo)
        {
            try
            {
                var query = from p in db.parcels
                            join w1 in db.warehouses on p.origin_id equals w1.id
                            join w2 in db.warehouses on p.destination_id equals w2.id
                            join c1 in db.customers on p.sender_id equals c1.id
                            join c2 in db.customers on p.receiver_id equals c2.id
                            where p.tracking_no == trackingNo
                            select new ParcelModel
                            {
                                trackingNo = p.tracking_no,
                                type = p.type,
                                weight = (float)p.weight,
                                status = p.status,
                                sender = c1.name,
                                receiver = c2.name,
                                origin = w1.name,
                                destination = w2.name
                            };

                return query.ToArray()[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ParcelModel();
            }
        }
    }
}
