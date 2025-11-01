-- Select all parcel routes by parcel id
SELECT 
  pr.parcel_id,
  pr.sequence,
  
  -- Route Info
  pr.route_id,
  r.distance,
  r.estimated_time,

  -- Origin Info
  r.origin_id,
  lo.name AS origin_name,
  lo.address AS origin_address,
  lo.contact AS origin_contact,

  -- Destination Info
  r.destination_id,
  ld.name AS destinaion_name,
  ld.address AS destination_address,
  ld.contact AS destination_contact,

  -- Vehicle Info
  r.vehicle_id,
  v.license_plate,
  v.capacity,
  v.status
FROM parcel_routes pr
JOIN routes r ON pr.route_id = r.id
JOIN locations lo ON r.origin_id = lo.id
JOIN locations ld ON r.destination_id = ld.id
JOIN vehicles v ON r.vehicle_id = v.id
WHERE pr.parcel_id = 'ST276846630'
ORDER BY pr.sequence ASC;

-- Select parcel routes by id
SELECT 
  pr.sequence,

  -- Parcel Info
  pr.parcel_id,
  p.weight,
  p.status AS parcel_status,
  p.type AS parcel_type,

  
  -- Route Info
  pr.route_id,
  r.distance,
  r.estimated_time,
  r.status AS route_status,

  -- Origin Info
  r.origin_id,
  lo.name AS origin_name,
  lo.address AS origin_address,
  lo.contact AS origin_contact,

  -- Destination Info
  r.destination_id,
  ld.name AS destinaion_name,
  ld.address AS destination_address,
  ld.contact AS destination_contact,

  -- Vehicle Info
  r.vehicle_id,
  v.license_plate,
  v.capacity,
  v.status AS vehicle_status
FROM parcel_routes pr
JOIN parcels p ON pr.parcel_id = p.id
JOIN routes r ON pr.route_id = r.id
JOIN locations lo ON r.origin_id = lo.id
JOIN locations ld ON r.destination_id = ld.id
JOIN vehicles v ON r.vehicle_id = v.id
WHERE v.driver_id = '4' AND p.status = 'in_transit' AND r.status != 'completed'
ORDER BY pr.sequence ASC;