import { getRouteById, getRoutes, insertRoute, updateRoute } from "../../Scripts/route.service.js";
import { getLocations } from "../../Scripts/location.service.js";
import { routeStatusStyle, statusStyle } from "./status.js";
import { getVehicles } from "../../Scripts/vehicle.service.js";
import { getParcels } from "../../Scripts/parcel.service.js";
import { getParcelRoutes, insertParcelRoute } from "../../Scripts/parcel-route.service.js";

const logout = document.getElementById("logout");
logout.addEventListener("click", () => {
  localStorage.removeItem("empName");
  localStorage.removeItem("empRole");
  localStorage.removeItem("empId");
  window.location.href = "/Views/sign-in.html";
});

const routeForm = document.querySelector(".route-form");
const switchBtn = document.getElementById("switch-btn");

switchBtn.addEventListener("click", () => {
  routeForm.classList.toggle("active");
  document.getElementById("route-form").reset();
  editingId = null;
});

async function renderVehicleOptions() {
  const vehicles = await getVehicles();

  const vehiclesElement = document.getElementById("vehicle");
  vehiclesElement.innerHTML = vehicles.map(
    (v) => `<option value="${v.id}">${v.license_plate}</option>`
  );
}
window.addEventListener("DOMContentLoaded", renderVehicleOptions);

async function renderLocationOptions() {
  const locations = await getLocations();

  const originElement = document.getElementById("origin");
  const destinationElement = document.getElementById("destination");

  originElement.innerHTML = locations.map(
    (l) => `<option value="${l.id}">${l.name + " - " + l.address}</option>`
  );

  destinationElement.innerHTML = locations.map(
    (l) => `<option value="${l.id}">${l.name + " - " + l.address}</option>`
  );
}
window.addEventListener("DOMContentLoaded", renderLocationOptions);

const popup = document.getElementById("popupModal");
const closeModal = document.getElementById("close-btn");

closeModal.addEventListener("click", () => {
  popup.style.display = "none";
});

let editingId = null;
async function handleEditRoute(id) {
  const route = await getRouteById(id);
  editingId = id;

  document.getElementById("vehicle").value = route.vehicle_id;
  document.getElementById("distance").value = route.distance;
  document.getElementById("estimated-time").value = route.estimated_time;
  document.getElementById("origin").value = route.origin_id;
  document.getElementById("destination").value = route.destination_id;
}

async function handleDeleteRoute(id) {
  if (confirm("คุณแน่ใจหรือไม่ว่าต้องการลบรถคันนี้?")) {
    await deleteRoute(id);
    renderRouteTable();
  }
}

async function renderRouteTable() {
  const routes = await getRoutes();

  const search = document
    .getElementById("route-search")
    .value.trim()
    .toLowerCase();

  const searchRoute = routes.filter(
    (r) =>
      r.vehicle.license_plate.toLowerCase().includes(search) ||
      r.origin.address.toLowerCase().includes(search) ||
      r.destination.address.toLowerCase().includes(search)
  );

  const routeElement = document.getElementById("routes");
  routeElement.innerHTML = searchRoute
    .map(
      (r) => `
      <tr class="route">
        <td>${r.vehicle.license_plate}</td>
        <td>${r.vehicle.capacity}</td>
        <td>${r.distance / 1000 + "km"}</td>
        <td>${
          r.estimated_time < 60
            ? r.estimated_time + "m"
            : r.estimated_time / 60 + "hr"
        }</td>
        <td>${routeStatusStyle(r.status)}</td>
        <td>${r.origin.address}</td>
        <td>${r.destination.address}</td>
        <td class="action-btn">
          <button class="edit-btn" data-id="${r.id}">
            <span class="material-symbols-outlined">
              edit_square
            </span>
          </button>
          <button class="delete-btn" data-id="${r.id}">
            <span class="material-symbols-outlined">
              delete_forever
            </span>
          </button>
        </td>
      </tr>
    `
    )
    .join("");

  document.querySelectorAll(".edit-btn").forEach((btn) => {
    btn.addEventListener("click", (e) => {
      const id = e.currentTarget.dataset.id;
      handleEditRoute(id);
      routeForm.classList.toggle("active");
    });
  });

  document.querySelectorAll(".delete-btn").forEach((btn) => {
    btn.addEventListener("click", (e) => {
      const id = e.currentTarget.dataset.id;
      handleDeleteRoute(id);
    });
  });
}

window.addEventListener("DOMContentLoaded", renderRouteTable);
document
  .getElementById("route-search")
  .addEventListener("input", renderRouteTable);

document.getElementById("save-btn").addEventListener("click", async (event) => {
  event.preventDefault();

  const distance = document.getElementById("distance").value;
  const estimatedTime = document.getElementById("estimated-time").value;
  const vehicleId = document.getElementById("vehicle").value;
  const originId = document.getElementById("origin").value;
  const destinationId = document.getElementById("destination").value;

  if (!distance || !estimatedTime) {
    alert("กรุณากรอกข้อมูลให้ครบ");
    return;
  }

  if (editingId) {
    const payload = {
      input: {
        id: editingId,
        distance: parseInt(distance),
        estimated_time: parseInt(estimatedTime),
      },
    };
    const data = await updateRoute(payload);
    alert(data.message);
  } else {
    const payload = {
      input: {
        distance: parseInt(distance),
        estimated_time: parseInt(estimatedTime),
        vehicle_id: vehicleId,
        origin_id: originId,
        destination_id: destinationId,
      },
    };
    const data = await insertRoute(payload);
    alert(data.message);
  }

  document.getElementById("route-form").reset();
  editingId = null;
  routeForm.classList.toggle("active");
  renderRouteTable();
});

// Add Parcel Route Assignment
let parcelId = null;
async function handleAddParcelRoute(id) {
  const parcelRoute = await getParcelRoutes(id);
  parcelId = id;

  document.querySelector(".parcel-route-list").innerHTML = parcelRoute
    .map(
      (pr) => `
        <tr class="parcel-route-item">
          <td>${pr.sequence}</td>
          <td>${pr.route.origin.name + " | " +pr.route.origin.address}</td>
          <td>${pr.route.destination.name + " | " +pr.route.destination.address}</td>
          <td>${pr.route.vehicle.license_plate}</td>
        </tr>
      `
    )
    .join("");
}

// Parcel Route Assignment
async function renderParcelTable() {
  const parcels = await getParcels();

  const search = document
    .getElementById("parcel-search")
    .value.trim()
    .toLowerCase();

  const searchParcel = parcels.filter(
    (p) =>
      p.id.toLowerCase().includes(search) ||
      p.sender.name.toLowerCase().includes(search) ||
      p.receiver.name.toLowerCase().includes(search)
  );

  const tbody = document.getElementById("parcels");
  tbody.innerHTML = searchParcel
    .map(
      (p) => `
            <tr class="parcel">
                <td>${p.id}</td>
                <td>${statusStyle(p.status)}</td>
                <td>${p.origin.name}</td>
                <td>${p.destination.name}</td>
                <td class="action-btn">
                  <button class="detail-btn" data-id="${p.id}">
                    <span class="material-symbols-outlined">
                      assignment
                    </span>
                  </button>
                </td>
            </tr>
        `
    )
    .join("");

  document.querySelectorAll(".detail-btn").forEach((btn) => {
    btn.addEventListener("click", (e) => {
      const id = e.currentTarget.dataset.id;
      popup.style.display = "flex";
      handleAddParcelRoute(id);
    });
  });
}

window.addEventListener("DOMContentLoaded", renderParcelTable);
document
  .getElementById("parcel-search")
  .addEventListener("input", renderParcelTable);

// Route list
async function renderRouteOptions() {
  const routes = await getRoutes();

  const routeElement = document.getElementById("route-options");
  routeElement.innerHTML = routes
    .map(
      (r) => `
        <option value="${r.id}">${r.vehicle.license_plate} - ${r.origin.name + " | " + r.origin.address} to -> ${r.destination.name + " | " +  r.destination.address}</option>
    `
    )
    .join("");
}
window.addEventListener("DOMContentLoaded", renderRouteOptions);

document
  .getElementById("assign-route")
  .addEventListener("click", async (event) => {
    event.preventDefault();

    const routeId = document.getElementById("route-options").value;

    if (!parcelId || !routeId) {
      alert("กรุณาเลือกเส้นทาง");
      return;
    }

    const payload = {
      parcel_input: {
        id: parcelId,
      },
      route_input: {
        id: routeId,
      },
      employee: {
        id: localStorage.getItem("empId"),
      }
    };

    const data = await insertParcelRoute(payload);
    alert(data.message);

    popup.style.display = "none";
    renderParcelTable();
  });
