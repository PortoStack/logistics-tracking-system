-- Select all parcels
SELECT 
  p.id,
  p.type,
  p.weight,
  p.status,
  cs.name AS sender,
  cr.name AS receiver,
  lo.name AS origin,
  ld.name AS destination
FROM parcels p
JOIN customers cs ON p.sender_id = cs.id
JOIN customers cr ON p.receiver_id = cr.id
JOIN locations lo ON p.origin_id = lo.id
JOIN locations ld ON p.destination_id = ld.id;