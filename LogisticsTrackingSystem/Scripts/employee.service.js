export async function getEmployees() {
  try {
    const res = await fetch("/Services/DbService.svc/employees", {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });

    const employees = await res.json();
    return employees;
  } catch (err) {
    console.error(err);
  }
}

export async function getEmployeeById(id) {
  try {
    const res = await fetch(`/Services/DbService.svc/employees/id/${id}`, {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });
    const employee = await res.json();
    return employee;
  } catch (err) {
    console.error(err);
  }
}

export async function getEmployeeByRole(role) {
  try {
    const res = await fetch(`/Services/DbService.svc/employees/role/${role}`, {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });
    const employees = await res.json();
    return employees;
  } catch (err) {
    console.error(err);
  }
}

export async function updateEmployee(payload) {
  try {
    const res = await fetch(`/Services/DbService.svc/employees`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });
    const data = await res.json();
    return JSON.parse(data.UpdateEmployeeResult);
  } catch (err) {
    console.error(err);
  }
}

export async function deleteEmployee(id) {
  try {
    const res = await fetch(`/Services/DbService.svc/employees/${id}`, {
      method: "DELETE",
      headers: { "Content-Type": "application/json" },
    });
    const data = await res.json();
    return data;
  } catch (err) {
    console.error(err);
  }
}
