export async function getVehicles() {
  try {
    const res = await fetch("/Services/DbService.svc/vehicles", {
      method: "GET",
      headers: { "Content-Type": "application/json" }
    });

    const vehicles = await res.json();
    return vehicles;
  } catch (err) {
    console.error(err);
  }
}

export async function getVehicleById(id) {
  try {
    const res = await fetch(`/Services/DbService.svc/vehicles/${id}`, {
      method: "GET",
      headers: { "Content-Type": "application/json" }
    });

    const vehicle = await res.json();
    return vehicle;
  } catch (err) {
    console.error(err);
  }
}

export async function insertVehicle(payload) {
  try {
    const res = await fetch("/Services/DbService.svc/vehicles", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });

    const data = await res.json();
    return JSON.parse(data.InsertVehicleResult);
  } catch (err) {
    console.error(err);
  }
}

export async function updateVehicle(payload) {
  try {
    const res = await fetch("/Services/DbService.svc/vehicles", {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });

    const data = await res.json();
    return JSON.parse(data.UpdateVehicleResult);
  } catch (err) {
    console.error(err);
  }
}

export async function deleteVehicle(id) {
  try {
    const res = await fetch(`/Services/DbService.svc/vehicles/${id}`, {
      method: "DELETE",
      headers: { "Content-Type": "application/json" },
    });

    const data = await res.json();
    return data;
  } catch (err) {
    console.error(err);
  }
}