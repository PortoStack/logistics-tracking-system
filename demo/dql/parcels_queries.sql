-- Select all parcels
SELECT 
  p.id,
  p.type,
  p.weight,
  p.status,

  -- Sender Info
  p.sender_id,
  cs.name AS sender_name,
  cs.email AS sender_email,
  cs.phone AS sender_phone,

  -- Receiver Info
  p.receiver_id,
  cr.name AS receiver_name,
  cr.email AS receiver_email,
  cr.phone AS reciver_phonr,

  -- Origin Info
  p.origin_id,
  lo.name AS origin_name,
  lo.address AS origin_address,
  lo.contact AS origin_contact,

  -- Destination Info
  p.destination_id,
  ld.name AS destination_name,
  ld.address AS destination_address,
  ld.contact AS destination_contact
FROM parcels p
JOIN customers cs ON p.sender_id = cs.id
JOIN customers cr ON p.receiver_id = cr.id
JOIN locations lo ON p.origin_id = lo.id
JOIN locations ld ON p.destination_id = ld.id;

-- Select parcel by id
SELECT 
  p.id,
  p.type,
  p.weight,
  p.status,

  -- Sender Info
  p.sender_id,
  cs.name AS sender_name,
  cs.email AS sender_email,
  cs.phone AS sender_phone,

  -- Receiver Info
  p.receiver_id,
  cr.name AS receiver_name,
  cr.email AS receiver_email,
  cr.phone AS reciver_phonr,

  -- Origin Info
  p.origin_id,
  lo.name AS origin_name,
  lo.address AS origin_address,
  lo.contact AS origin_contact,

  -- Destination Info
  p.destination_id,
  ld.name AS destination_name,
  ld.address AS destination_address,
  ld.contact AS destination_contact
FROM parcels p
JOIN customers cs ON p.sender_id = cs.id
JOIN customers cr ON p.receiver_id = cr.id
JOIN locations lo ON p.origin_id = lo.id
JOIN locations ld ON p.destination_id = ld.id
WHERE p.id = 'ET000000001';

-- Select group status and count each status
SELECT 
  status,
  COUNT(*) AS count
FROM parcels
GROUP BY status;