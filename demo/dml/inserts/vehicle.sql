BEGIN TRAN

  -- Select vehicle before update
  SELECT * 
  FROM vehicles;

  -- insert vehicle
  INSERT INTO vehicles (license_plate, capacity, status, driver_id)
  VALUES ('BK-5973', 1800, 'available', 2);

  -- Select vehicle after update
  SELECT * 
  FROM vehicles;

-- COMMIT TRAN if you want to save data
ROLLBACK TRAN;