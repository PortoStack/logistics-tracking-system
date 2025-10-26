-- Select all employees
SELECT id, name, email, password, phone, role
FROM employees;

-- Select employee by id
SELECT id, name, email, password, phone, role
FROM employees
WHERE id = 1;