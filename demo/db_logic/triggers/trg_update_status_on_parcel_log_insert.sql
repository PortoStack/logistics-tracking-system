DROP TRIGGER IF EXISTS trg_update_status_on_parcel_log_insert;
GO

CREATE TRIGGER trg_update_status_on_parcel_log_insert
ON parcel_logs
AFTER INSERT
AS
BEGIN
  SET NOCOUNT ON;

  -- Update parcel status based on log entry
  UPDATE p
  SET p.status = 
      CASE i.status
        WHEN 'arrived_warehouse' THEN 'pending'
        WHEN 'failed' THEN 'cancelled'
        WHEN 'delivered' THEN 'delivered'
        ELSE p.status
      END
  FROM parcels p
  JOIN inserted i ON p.id = i.parcel_id;

  -- Update route status based on parcel log entry
  UPDATE r
  SET r.status =
      CASE i.status
        WHEN 'arrived_warehouse' THEN 'completed'
        WHEN 'failed' THEN 'cancelled'
        WHEN 'delivered' THEN 'completed'
        ELSE r.status
      END
  FROM routes r
  JOIN inserted i ON r.id = i.route_id;

  UPDATE v
  SET v.status = 'available'
  FROM vehicles v
  JOIN routes r ON r.vehicle_id = v.id
  JOIN inserted i ON r.id = i.route_id;
END;