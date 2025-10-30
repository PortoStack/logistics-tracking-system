export async function getParcelLogs() {
  try {
    const res = await fetch("/Services/DbService.svc/parcel/logs", {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });

    const parcelLogs = await res.json();
    return parcelLogs;
  } catch (err) {
    console.error(err);
  }
}

export async function insertParcelLog(payload) {
  try {
    const res = await fetch("/Services/DbService.svc/parcel/logs", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });

    if (!res.ok) {
      throw new Error("Failed to insert parcel log");
    }

    const data = await res.json();
    return JSON.parse(data.InsertParcelLogResult);
  } catch (err) {
    console.error(err);
  }
}