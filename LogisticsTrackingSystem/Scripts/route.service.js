export async function getRoutes() {
  try {
    const res = await fetch("/Services/DbService.svc/routes", {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });
    const routes = await res.json();
    return routes;
  } catch (err) {
    console.error(err);
  }
}

export async function getRouteById(id) {
  try {
    const res = await fetch(`/Services/DbService.svc/routes/${id}`, {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });
    const routes = await res.json();
    return routes;
  } catch (err) {
    console.error(err);
  }
}

export async function insertRoute(payload) {
  try {
    const res = await fetch("/Services/DbService.svc/routes", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });
    const data = await res.json();
    return JSON.parse(data.InsertRouteResult);
  } catch (err) {
    console.error(err);
  }
}

export async function updateRoute(payload) {
  try {
    const res = await fetch("/Services/DbService.svc/routes", {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });

    const data = await res.json();
    return JSON.parse(data.UpdateRouteResult);
  } catch (err) {
    console.error(err);
  }
}

export async function deleteRoute(id) {
  try {
    const res = await fetch(`/Services/DbService.svc/routes/${id}`, {
      method: "DELETE",
      headers: { "Content-Type": "application/json" },
    });

    const data = await res.json();
    return JSON.parse(data.DeleteRouteResult);
  } catch (err) {
    console.error(err);
  }
}
