async function loadParcels() {
  try {
    const res = await fetch("/Services/DbService.svc/parcels", {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });
    const parcels = await res.json();

    const tbody = document.getElementById("parcels");
    tbody.innerHTML = parcels.map(
        (p) => `
            <tr>
                <td>${p.id}</td>
                <td>${p.type}</td>
                <td>${p.weight}</td>
                <td>${p.status}</td>
                <td>${p.sender.name}</td>
                <td>${p.receiver.name}</td>
                <td>${p.origin.name}</td>
                <td>${p.destination.name}</td>
            </tr>
        `
      ).join("");
  } catch (err) {
    console.error(err);
  }
}

window.onload = loadParcels;
