-- Drop tables
DROP TABLE IF EXISTS parcel_logs;

DROP TABLE IF EXISTS parcel_routes;

DROP TABLE IF EXISTS parcels;

DROP TABLE IF EXISTS routes;

DROP TABLE IF EXISTS vehicles;

DROP TABLE IF EXISTS customers;

DROP TABLE IF EXISTS employees;

DROP TABLE IF EXISTS locations;

-- Create Customer table
CREATE TABLE customers (
  id INT IDENTITY (1, 1),
  name VARCHAR(255) NOT NULL,
  email VARCHAR(100) UNIQUE NOT NULL,
  phone VARCHAR(20) UNIQUE NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE employees (
  id INT IDENTITY(1, 1),
  name VARCHAR(255) NOT NULL,
  email VARCHAR(100) UNIQUE NOT NULL,
  password VARCHAR(255) NOT NULL,
  phone VARCHAR(20) UNIQUE NOT NULL,
  role VARCHAR(20) NOT NULL CHECK (role IN ('employee', 'manager', 'driver')) DEFAULT 'employee',
  PRIMARY KEY (id)
);

-- Create Location table
CREATE TABLE locations (
  id INT IDENTITY (1, 1),
  name VARCHAR(100) UNIQUE NOT NULL,
  contact VARCHAR(255),
  address VARCHAR(MAX),
  type VARCHAR(20) NOT NULL CHECK(
    type IN (
      'warehouse',
      'house',
      'office',
      'pickup_point'
    )
  ),
  PRIMARY KEY (id)
);

-- Create Vehicle table
CREATE TABLE vehicles (
  id INT IDENTITY (1, 1),
  capacity INT NOT NULL,
  -- kg
  license_plate VARCHAR(20) UNIQUE NOT NULL,
  status VARCHAR(20) NOT NULL CHECK(status IN ('available', 'in_use', 'maintenance')) DEFAULT 'available',
  created_at DATETIME DEFAULT GETDATE(),
  driver_id INT NULL,
  FOREIGN KEY (driver_id) REFERENCES employees (id),
  PRIMARY KEY (id)
);

-- Create Parcel table
CREATE TABLE parcels (
  id VARCHAR(20) UNIQUE NOT NULL,
  weight DECIMAL(10, 2) NOT NULL CHECK (weight > 0),
  -- kg
  type VARCHAR(20) NOT NULL CHECK (type IN ('standard', 'ems')) DEFAULT 'standard',
  status VARCHAR(20) NOT NULL CHECK (
    status IN (
      'pending',
      'in_transit',
      'delivered',
      'cancelled'
    )
  ) DEFAULT 'pending',
  created_at DATETIME DEFAULT GETDATE(),
  sender_id INT NOT NULL,
  receiver_id INT NOT NULL,
  origin_id INT NOT NULL,
  destination_id INT NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (sender_id) REFERENCES customers (id),
  FOREIGN KEY (receiver_id) REFERENCES customers (id),
  FOREIGN KEY (origin_id) REFERENCES locations (id),
  FOREIGN KEY (destination_id) REFERENCES locations (id)
);

-- Create Route table
CREATE TABLE routes (
  id INT IDENTITY (1, 1),
  distance DECIMAL(10, 2),
  -- m
  estimated_time INT,
  -- minutes
  assigned_at DATETIME DEFAULT GETDATE(),
  status VARCHAR(20) NOT NULL CHECK (
    status IN (
      'assigned',
      'in_progress',
      'completed',
      'cancelled'
    )
  ) DEFAULT 'assigned',
  origin_id INT NOT NULL,
  destination_id INT NOT NULL,
  vehicle_id INT NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (origin_id) REFERENCES locations (id),
  FOREIGN KEY (destination_id) REFERENCES locations (id),
  FOREIGN KEY (vehicle_id) REFERENCES vehicles (id)
);

-- Create Parcel Log table
CREATE TABLE parcel_logs (
  id INT IDENTITY (1, 1),
  status VARCHAR(20) NOT NULL CHECK (
    status IN (
      'received',
      'in_transit',
      'arrived_warehouse',
      'out_for_delivery',
      'delivered',
      'failed'
    )
  ),
  note VARCHAR(MAX),
  timestamp DATETIME DEFAULT GETDATE(),
  parcel_id VARCHAR(20) NOT NULL,
  employee_id INT NULL,
  location_id INT NULL,
  route_id INT NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (parcel_id) REFERENCES parcels (id),
  FOREIGN KEY (employee_id) REFERENCES employees (id),
  FOREIGN KEY (location_id) REFERENCES locations (id),
  FOREIGN KEY (route_id) REFERENCES routes (id)
);

-- Create Parcel Route table
CREATE TABLE parcel_routes (
  parcel_id VARCHAR(20) NOT NULL,
  route_id INT NOT NULL,
  sequence INT NOT NULL,
  PRIMARY KEY (parcel_id, sequence),
  FOREIGN KEY (parcel_id) REFERENCES parcels (id),
  FOREIGN KEY (route_id) REFERENCES routes (id)
);