export async function getCustomers() {
  try {
    const res = await fetch("/Services/DbService.svc/customers", {
      method: "GET",
      headers: { "Content-Type": "application'json" },
    });

    const customers = await res.json();
    return customers;
  } catch (err) {
    console.error(err);
  }
}

export async function getCustomerByPhone(phone) {
  try {
    const res = await fetch(`/Services/DbService.svc/customers/${phone}`, {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });

    const customer = await res.json();
    return customer;
  } catch (err) {
    console.error(err);
  }
}

export async function updateCustomer(payload) {
  try {
    const res = await fetch(`/Services/DbService.svc/customers`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });

    const data = await res.json();
    return data;
  } catch (err) {
    console.error(err);
  }
}

export async function deleteCustomer(id) {
  try {
    const res = await fetch(`/Services/DbService.svc/customers/${id}`, {
      method: "DELETE",
      headers: { "Content-Type": "application/json" },
    });

    const data = await res.json();
    return data;
  } catch (err) {
    console.error(err);
  }
}