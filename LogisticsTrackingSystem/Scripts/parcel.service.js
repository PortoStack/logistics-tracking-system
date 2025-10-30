export async function getParcels() {
  try {
    const res = await fetch("/Services/DbService.svc/parcels", {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });
    const parcels = await res.json();
    return parcels;
  } catch (err) {
    console.error(err);
  }
}

export async function getParcelById(id) {
  try {
    const res = await fetch(`/Services/DbService.svc/parcels/${id}`, {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });
    const parcel = await res.json();
    return parcel;
  } catch (err) {
    console.error(err);
  }
}

export async function getParcelStat() {
  try {
    const res = await fetch("/Services/DbService.svc/parcels/stat", {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });
    const stat = await res.json();
    return stat;
  } catch (err) {
    console.error(err);
  }
}

export async function insertParcel(payload) {
  try {
    const res = await fetch("/Services/DbService.svc/parcels", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });

    const data = await res.json();
    return JSON.parse(data.InsertParcelResult);
  } catch (err) {
    console.error(err);
  }
}

export async function updateParcel(payload) {
  try {
    const res = await fetch("/Services/DbService.svc/parcels", {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });

    const data = await res.json();
    return JSON.parse(data.UpdateParcelResult);
  } catch (err) {
    console.error(err);
  }
}
