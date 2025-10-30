export async function getLocations() {
  try {
    const res = await fetch(`/Services/DbService.svc/locations`, {
      method: "GET",
      headers: { "Content-Type": "application/json" }
    });

    const locations = await res.json();
    return locations;
  } catch (err) {
    console.error(err);
  }
}

export async function getLocationById(id) {
  try {
    const res = await fetch(`/Services/DbService.svc/locations/id/${id}`, {
      method: "GET",
      headers: { "Content-Type": "application/json" }
    });

    const location = await res.json();
    return location;
  } catch (err) {
    console.error(err);
  }
}

export async function getLocationByContact(contact) {
  try {
    const res = await fetch(`/Services/DbService.svc/locations/contact/${contact}`, {
      method: "GET",
      headers: { "Content-Type": "application/json" }
    });

    const locations = await res.json();
    return locations;
  } catch (err) {
    console.error(err);
  }
}

export async function insertLocation(payload) {
  try {
    const res = await fetch(`/Services/DbService.svc/locations`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });

    const locations = await res.json();
    return locations;
  } catch (err) {
    console.error(err);
  }
}

export async function updateLocation(payload) {
  try {
    const res = await fetch(`/Services/DbService.svc/locations`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });

    const locations = await res.json();
    return locations;
  } catch (err) {
    console.error(err);
  }
}

export async function deleteLocation(id) {
  try {
    const res = await fetch(`/Services/DbService.svc/locations/${id}`, {
      method: "DELETE",
      headers: { "Content-Type": "application/json" }
    });

    const locations = await res.json();
    return locations;
  } catch (err) {
    console.error(err);
  }
}