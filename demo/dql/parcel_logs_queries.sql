-- Select all parcel logs
SELECT 
  pl.id,
  pl.status,
  pl.note,
  pl.timestamp,
  pl.parcel_id,
  e.name AS handle_by
FROM parcel_logs pl
LEFT JOIN employees e ON pl.employee_id = e.id

-- Select parcel by id
SELECT
    pl.timestamp,
    pl.status AS action,
    pl.note,
    ld.name AS current_location,
    e.name AS handled_by,
    r.distance,
    r.estimated_time,
    pr.sequence
FROM parcel_logs pl
LEFT JOIN employees e ON pl.employee_id = e.id
LEFT JOIN locations ld ON pl.location_id = ld.id
LEFT JOIN parcel_routes pr ON pl.parcel_id = pr.parcel_id AND pr.route_id = pl.route_id
LEFT JOIN routes r ON pl.route_id = r.id
LEFT JOIN locations lo ON r.origin_id = lo.id
WHERE pl.parcel_id = 'ET000000001'
ORDER BY pr.sequence DESC, pl.timestamp DESC;