-- Select all routes
SELECT
  r.id,
  r.status,
  r.distance,
  r.estimated_time,
  r.assigned_at,

  -- Origin Info
  r.origin_id,
  lo.name AS origin_name,
  lo.address AS origin_address,
  lo.contact AS origin_contact,

  -- Destination Info
  r.destination_id,
  ld.name AS destination_name,
  ld.address AS destination_address,
  ld.contact AS destination_contact,

  -- Vehicle Info
  r.vehicle_id,
  v.license_plate,
  v.status,
  v.capacity
FROM routes r
JOIN vehicles v ON r.vehicle_id = v.id
JOIN locations lo ON r.origin_id = lo.id
JOIN locations ld ON r.destination_id = ld.id;

-- Select route by id
SELECT
  r.id,
  r.status,
  r.distance,
  r.estimated_time,
  r.assigned_at,

  -- Origin Info
  r.origin_id,
  lo.name AS origin_name,
  lo.address AS origin_address,
  lo.contact AS origin_contact,

  -- Destination Info
  r.destination_id,
  ld.name AS destination_name,
  ld.address AS destination_address,
  ld.contact AS destination_contact,

  -- Vehicle Info
  r.vehicle_id,
  v.license_plate,
  v.status,
  v.capacity
FROM routes r
JOIN vehicles v ON r.vehicle_id = v.id
JOIN locations lo ON r.origin_id = lo.id
JOIN locations ld ON r.destination_id = ld.id
WHERE r.id = '1';