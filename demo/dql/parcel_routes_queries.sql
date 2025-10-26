-- Select all parcel routes
SELECT 
  pr.parcel_id,
  pr.sequence,
  lo.name AS origin,
  ld.name AS destinaion
FROM parcel_routes pr
JOIN routes r ON pr.route_id = r.id
JOIN locations lo ON r.origin_id = lo.id
JOIN locations ld ON r.destination_id = ld.id
GROUP BY pr.parcel_id, pr.sequence, lo.name, ld.name
ORDER BY pr.sequence DESC;

-- Select parcel routes by id

