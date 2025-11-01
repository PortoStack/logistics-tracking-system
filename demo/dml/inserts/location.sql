BEGIN TRAN

  -- Select location before update
  SELECT * 
  FROM locations;

  -- insert location
  INSERT INTO locations (name, contact, address, type)
  VALUES ('NU Express', '0899599338', '99, Moo 9, Tha Pho, Mueang Phitsanulok, Phitsanulok, 65000', 'warehouse');

  -- Select location after update
  SELECT * 
  FROM locations;

-- COMMIT TRAN if you want to save data
ROLLBACK TRAN;