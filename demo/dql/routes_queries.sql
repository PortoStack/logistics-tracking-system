-- Select all routes
SELECT
  r.id,
  r.status,
  r.distance,
  r.estimated_time,
  r.assigned_at,
  lo.name AS origin,
  ld.name AS destination,
  e.name
FROM routes r
JOIN vehicles v ON r.vehicle_id = v.id
JOIN employees e ON v.driver_id = e.id
JOIN locations lo ON r.origin_id = lo.id
JOIN locations ld ON r.destination_id = ld.id;