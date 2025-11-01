BEGIN TRAN

  DECLARE @SenderId INT = 1;
  DECLARE @ReceiverId INT = 2;

  DECLARE @OriginId INT = 1;
  DECLARE @DestinationId INT = 2;

  DECLARE @ParcelId VARCHAR(20) = 'ET000000010';

  -- Select sender, origin, receiver, destination before insert
  SELECT * FROM customers WHERE id IN (@SenderId, @ReceiverId);
  SELECT * FROM locations WHERE id IN (@OriginId, @DestinationId);

  -- Check exist sender and origin, insert if not exist
  IF NOT EXISTS (SELECT id FROM customers WHERE id = @SenderId)
    BEGIN
      INSERT INTO customers (name, email, phone)
      VALUES ('Test Sender', 'testS@example.com', '0987654321');

      SET @SenderId = SCOPE_IDENTITY();
    END;

  IF NOT EXISTS (SELECT id FROM locations WHERE id = @OriginId)
    BEGIN
      INSERT INTO locations (name, contact, address, type)
      VALUES ('Test Origin', 'Origin Contact', '123 Origin St', 'house');

      SET @OriginId = SCOPE_IDENTITY();
    END;

  -- Check exist receiver and destination, insert if not exist
  IF NOT EXISTS (SELECT id FROM customers WHERE id = @ReceiverId)
    BEGIN
      INSERT INTO customers (name, email, phone)
      VALUES ('Test Receiver', 'testR@example.com', '0987654322');

      SET @ReceiverId = SCOPE_IDENTITY();
    END;

  IF NOT EXISTS (SELECT id FROM locations WHERE id = @DestinationId)
    BEGIN
      INSERT INTO locations (name, contact, address, type)
      VALUES ('Test Destination', 'Destination Contact', '123 Destination St', 'house');

      SET @DestinationId = SCOPE_IDENTITY();
    END;

  -- Select sender, origin, receiver, destination after insert
  SELECT * FROM customers WHERE id IN (@SenderId, @ReceiverId);
  SELECT * FROM locations WHERE id IN (@OriginId, @DestinationId);

  -- Insert parcel
  INSERT INTO parcels (id, weight, type, status, sender_id, receiver_id, origin_id, destination_id)
  VALUES (@ParcelId, 2.5, 'ems', 'pending', @SenderId, @ReceiverId, @OriginId, @DestinationId);

  -- Select parcel after insert
  SELECT 
    p.id,
    p.weight,
    p.type,
    p.status,
    sender.name AS sender,
    receiver.name AS receiver,
    origin.name AS origin,
    destination.name AS destination
  FROM parcels p
  JOIN customers AS sender ON p.sender_id = sender.id
  JOIN customers AS receiver ON p.receiver_id = receiver.id
  JOIN locations AS origin ON p.origin_id = origin.id
  JOIN locations AS destination ON p.destination_id = destination.id
  WHERE p.id = @ParcelId;

-- COMMIT TRAN if you want to save data
ROLLBACK TRAN;