-- Select all vehicles
SELECT
  v.id,
  v.license_plate,
  v.capacity,
  v.status,
  e.name AS driver
FROM vehicles v
JOIN employees e ON driver_id = e.id;