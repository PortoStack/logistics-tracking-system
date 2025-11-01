-- Select all locations
SELECT id, name, type, address, contact
FROM locations;

-- Select location by id
SELECT id, name, type, address, contact
FROM locations
WHERE id = '1';

-- Select all locations
SELECT id, name, type, address, contact
FROM locations
WHERE contact = '0993246540';