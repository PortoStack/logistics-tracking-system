export const getParcelRoutes = async (parcelId) => {
  try {
    const res = await fetch(`/Services/DbService.svc/parcel/routes/${parcelId}`, {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });
    if (!res.ok) throw new Error("Failed to fetch parcel routes");
    return await res.json();
  } catch (error) {
    console.error(error);
    throw error;
  }
};

export const getParcelRouteByDriverId = async (driverId) => {
  try {
    const res = await fetch(`/Services/DbService.svc/parcel/routes/driver/${driverId}`, {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });
    if (!res.ok) throw new Error("Failed to fetch parcel routes");
    return await res.json();
  } catch (error) {
    console.error(error);
    throw error;
  }
};

export const insertParcelRoute = async (payload) => {
  try {
    const res = await fetch("/Services/DbService.svc/parcel/routes", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });
    if (!res.ok) throw new Error("Failed to insert parcel route");
    
    const data = await res.json();
    return JSON.parse(data.InsertParcelRouteResult);
  } catch (error) {
    console.error(error);
    throw error;
  }
};