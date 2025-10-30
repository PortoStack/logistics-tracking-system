-- Customers
INSERT INTO customers (name, email, phone)
VALUES
  ('John Sender', 'john.sender@example.com', '0810000001'),
  ('Mary Receiver', 'mary.receiver@example.com', '0810000002'),
  ('Alice Walker', 'alice.walker@example.com', '0810000003'),
  ('Bob Smith', 'bob.smith@example.com', '0810000004'),
  ('Tom Johnson', 'tom.johnson@example.com', '0810000005'),
  ('Lucy Brown', 'lucy.brown@example.com', '0810000006');
  
-- Employees
INSERT INTO employees (name, email, password, phone, role)
VALUES
  ('Emma Employee', 'emma.emp@example.com', '123456', '0820000001', 'employee'),
  ('Michael Manager', 'michael.manager@example.com', '123456', '0820000002', 'manager'),
  ('David Driver', 'david.driver@example.com', '123456', '0820000003', 'driver'),
  ('James Driver', 'james.driver@example.com', '123456', '0820000004', 'driver'),
  ('Sophia Staff', 'sophia.staff@example.com', '123456', '0820000005', 'employee');

-- Locations
INSERT INTO locations (name, contact, address, type)
VALUES
  ('Bangkok Warehouse', 'Mr. Somchai', '12 Sukhumvit Rd, Bangkok', 'warehouse'),
  ('Chiang Mai Hub', 'Ms. Waranya', '88 Chang Phueak Rd, Chiang Mai', 'warehouse'),
  ('Phuket Delivery Center', 'Mr. Tavee', '77 Patong Rd, Phuket', 'pickup_point'),
  ('Origin John', '0810000001', '10 Moo 5, Sukhothai', 'house'),
  ('Destination Mary', '0810000002', '22 Sathorn Rd, Bangkok', 'house'),
  ('Origin Alice', '0810000003', '55 Nimman Rd, Chiang Mai', 'house'),
  ('Destination Lucy', '0810000006', '44 Beach Rd, Phuket', 'house');

-- Vehicles
INSERT INTO vehicles (capacity, license_plate, status, driver_id)
VALUES
  (2000, 'BK-1234', 'available', 3),
  (1800, 'CM-5678', 'in_use', 4),
  (2500, 'PK-1122', 'maintenance', NULL),
  (1500, 'BK-5566', 'available', 3),
  (2200, 'CM-9988', 'available', 4);

-- Routes
INSERT INTO routes (distance, estimated_time, status, origin_id, destination_id, vehicle_id)
VALUES
  (690000.00, 480, 'in_progress', 2, 1, 2),  
  (870000.00, 600, 'assigned', 1, 3, 1),     
  (50000.00, 60, 'assigned', 1, 5, 4),       
  (40000.00, 55, 'completed', 2, 6, 5),      
  (15000.00, 30, 'assigned', 3, 7, 1);       

-- Parcels
INSERT INTO parcels (id, weight, type, status, sender_id, receiver_id, origin_id, destination_id)
VALUES
  ('ET000000001', 2.50, 'ems', 'in_transit', 1, 2, 4, 5),
  ('ST000000001', 1.20, 'standard', 'pending', 3, 4, 6, 5),
  ('ET000000002', 5.00, 'ems', 'delivered', 1, 6, 4, 7),
  ('ST000000002', 0.75, 'standard', 'cancelled', 5, 2, 4, 5),
  ('ET000000003', 3.40, 'ems', 'in_transit', 3, 6, 6, 7),
  ('ST000000003', 4.50, 'standard', 'pending', 1, 4, 4, 5),
  ('ET000000004', 1.80, 'ems', 'in_transit', 3, 2, 6, 5),
  ('ST000000004', 2.30, 'standard', 'delivered', 5, 6, 4, 7),
  ('ST000000005', 1.10, 'standard', 'in_transit', 1, 4, 4, 5),
  ('ET000000005', 6.50, 'ems', 'pending', 3, 6, 6, 7);

-- Parcel Routes
INSERT INTO parcel_routes (parcel_id, route_id, sequence)
VALUES
  ('ET000000001', 1, 1),
  ('ET000000001', 3, 2),
  ('ST000000001', 4, 1),
  ('ET000000002', 5, 1),
  ('ST000000002', 3, 1),
  ('ET000000003', 1, 1),
  ('ST000000003', 3, 1),
  ('ET000000004', 4, 1),
  ('ST000000004', 5, 1),
  ('ST000000005', 3, 1);

-- Parcel Logs
-- Parcel Logs (แก้ให้มี route_id)
INSERT INTO parcel_logs (status, note, parcel_id, employee_id, location_id, route_id)
VALUES
  ('received', 'Parcel received from customer', 'ET000000001', 1, 4, 1),
  ('in_transit', 'Left Chiang Mai hub to Bangkok', 'ET000000001', 3, 2, 1),
  ('arrived_warehouse', 'Arrived at Bangkok warehouse', 'ET000000001', 3, 1, 1),
  ('out_for_delivery', 'Out for delivery to receiver', 'ET000000001', 4, 1, 3),
  ('delivered', 'Delivered successfully to Mary', 'ET000000001', 4, 5, 3),

  ('received', 'Parcel accepted from Alice', 'ST000000001', 1, 6, 4),
  ('in_transit', 'In transit to Bangkok', 'ST000000001', 3, 2, 4),
  ('failed', 'Delivery failed due to wrong address', 'ST000000001', 4, 5, 4),

  ('received', 'Received from John', 'ET000000002', 5, 4, 5),
  ('delivered', 'Delivered successfully to Lucy', 'ET000000002', 3, 7, 5);

