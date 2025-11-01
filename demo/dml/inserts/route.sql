BEGIN TRAN

  -- Select route before update
  SELECT * 
  FROM routes;

  -- insert route
  INSERT INTO routes ()
  VALUES ();

  -- Select route after update
  SELECT * 
  FROM routes;

-- COMMIT TRAN if you want to save data
ROLLBACK TRAN;