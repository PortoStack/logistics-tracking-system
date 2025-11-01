DROP TRIGGER IF EXISTS trg_update_status_on_route_assign;
GO

CREATE TRIGGER trg_update_status_on_route_assign
ON parcel_routes
AFTER INSERT
AS
BEGIN
  SET NOCOUNT ON;

  -- Insert log entries
  INSERT INTO parcel_logs (status, note, parcel_id, route_id, location_id, employee_id)
  SELECT 
    CASE
      WHEN r.destination_id = p.destination_id THEN 'out_for_delivery'
      ELSE 'in_transit'
    END AS status,
    CASE 
      WHEN r.destination_id = p.destination_id THEN 'Parcel is out for delivery'
      ELSE 'Parcel is in transit'
    END AS note,
    i.parcel_id,
    i.route_id,
    l.id,
    e.id AS employee_id
  FROM inserted i
  JOIN routes r ON i.route_id = r.id
  JOIN parcels p ON i.parcel_id = p.id
  JOIN vehicles v ON r.vehicle_id = v.id
  JOIN locations l ON r.origin_id = l.id
  JOIN employees e ON v.driver_id = e.id;

  -- Update parcel status
  UPDATE p
  SET p.status = 'in_transit'
  FROM parcels p
  JOIN inserted i ON p.id = i.parcel_id
  JOIN routes r ON i.route_id = r.id;

  -- Update route status
  UPDATE r
  SET r.status = 'in_progress'
  FROM routes r
  JOIN inserted i ON r.id = i.route_id;

  -- Update vehicle status 
  UPDATE v
  SET v.status = 'in_use'
  FROM vehicles v
  JOIN routes r ON r.vehicle_id = v.id
  JOIN inserted i ON r.id = i.route_id;
END;
