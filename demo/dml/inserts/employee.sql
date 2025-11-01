BEGIN TRAN

  -- Select employee before update
  SELECT * 
  FROM employees;

  -- insert employee
  INSERT INTO employees (name, email, password, phone, role)
  VALUES ('Chanachon Holactie', 'chanachon@example.com', '159753', '0820985463', 'manager');

  -- Select employee after update
  SELECT * 
  FROM employees;

-- COMMIT TRAN if you want to save data
ROLLBACK TRAN;