-- Select all parcel logs
SELECT 
  pl.id,
  pl.parcel_id,
  pl.timestamp,
  pl.status AS action,
  pl.note,
  COALESCE(pr.sequence, 0) AS sequence,

  -- Current Location Info
  pl.location_id AS current_location,
  l.name AS current_location_name,
  l.address AS current_location_address,
  l.contact AS current_location_contact,

  -- Employee Info
  pl.employee_id,
  e.name AS handled_by,
  e.email AS handled_by_email,
  e.role AS handled_by_role,

  -- Route Info
  pl.route_id,
  r.status,
  r.distance,
  r.estimated_time
FROM parcel_logs pl
LEFT JOIN employees e ON pl.employee_id = e.id
LEFT JOIN locations l ON pl.location_id = l.id
LEFT JOIN parcel_routes pr ON pl.route_id = pr.route_id AND pl.parcel_id = pr.parcel_id
LEFT JOIN routes r ON pl.route_id = r.id
ORDER BY pl.timestamp DESC;

-- Select parcel log by parcel id
SELECT 
  pl.id,
  pl.parcel_id,
  pl.timestamp,
  pl.status AS action,
  pl.note,
  COALESCE(pr.sequence, 0) AS sequence,

  -- Current Location Info
  pl.location_id AS current_location,
  l.name AS current_location_name,
  l.address AS current_location_address,
  l.contact AS current_location_contact,

  -- Employee Info
  pl.employee_id,
  e.name AS handled_by,
  e.email AS handled_by_email,
  e.role AS handled_by_role,

  -- Route Info
  pl.route_id,
  r.status,
  r.distance,
  r.estimated_time
FROM parcel_logs pl
LEFT JOIN employees e ON pl.employee_id = e.id
LEFT JOIN locations l ON pl.location_id = l.id
LEFT JOIN parcel_routes pr ON pl.route_id = pr.route_id AND pl.parcel_id = pr.parcel_id
LEFT JOIN routes r ON pl.route_id = r.id
WHERE pl.parcel_id = 'ET000000001'
ORDER BY pr.sequence DESC, pl.timestamp DESC;