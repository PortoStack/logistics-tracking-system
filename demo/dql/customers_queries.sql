-- Select all customers
SELECT id, name, email, phone
FROM customers;

-- Select customer by phone
SELECT id, name, email, phone
FROM customers
WHERE phone = '0993246540';