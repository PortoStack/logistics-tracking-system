-- Select all vehicles
SELECT
  v.id,
  v.license_plate,
  v.capacity,
  v.status,

  -- Driver Info
  v.driver_id,
  e.name AS driver_name,
  e.email AS driver_email
FROM vehicles v
JOIN employees e ON driver_id = e.id;

-- Select vehicle by id
SELECT
  v.id,
  v.license_plate,
  v.capacity,
  v.status,

  -- Driver Info
  v.driver_id,
  e.name AS driver_name,
  e.email AS driver_email
FROM vehicles v
JOIN employees e ON driver_id = e.id
WHERE v.id = '1';