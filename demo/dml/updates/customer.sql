-- Update customer
BEGIN TRAN
DECLARE @CustomerId INT = 1;
  -- Select customer before update
  SELECT * 
  FROM customers 
  WHERE id = @CustomerId;

  UPDATE customers
  SET
    name = 'Test Sql',
    email = 'test@example.com'
  WHERE id = @CustomerId;

  -- Select customer after update
  SELECT * 
  FROM customers 
  WHERE id = @CustomerId;

-- COMMIT TRAN if you want to save data
ROLLBACK TRAN; 