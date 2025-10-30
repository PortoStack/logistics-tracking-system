import { insertParcelLog } from "../../Scripts/parcel-log.service.js";
import { getParcelRouteByDriverId } from "../../Scripts/parcel-route.service.js";

const logout = document.getElementById("logout");
logout.addEventListener("click", () => {
  localStorage.removeItem("empName");
  localStorage.removeItem("empRole");
  localStorage.removeItem("empId");
  window.location.href = "/Views/sign-in.html";
});

document.getElementById("driver-name").innerHTML = `
  Welcome, ${localStorage.getItem("empName")}
`;

// Fetch assigned parcels for the driver
const driverId = localStorage.getItem("empId");

async function renderParcelTable() {
  const parcels = await getParcelRouteByDriverId(driverId);

  const filterParcels = parcels.filter((p) => p.parcel.status === "in_transit" && p.route.status !== "completed");

  console.log(filterParcels)

  const search = document.getElementById("search-parcel");
  const searchParcel = filterParcels.filter((p) =>
    p.parcel_id.toLowerCase().includes(search.value.toLowerCase())
  );

  const tbody = document.getElementById("tracking-info");
  tbody.innerHTML = searchParcel
    .map(
      (p) => `
            <div class="parcel">
                <h1>Parcel ID: ${p.parcel_id}</h1>
                <span>From: ${
                  p.route.origin.name + " | " + p.route.origin.address
                }</span>
                <span>To: ${
                  p.route.destination.name + " | " + p.route.destination.address
                }</span>
                <div class="action-btn">
                  <button 
                    class="cancel-btn" 
                    data-id="${p.parcel_id}"
                    data-route="${p.route_id}"
                    data-location="${p.route.destination_id}"
                  >
                    Cancel
                  </button>
                  <button 
                    class="complete-btn" 
                    data-id="${p.parcel_id}" 
                    data-status="${
                      p.parcel.destination_id === p.route.destination_id
                        ? "delivered"
                        : "arrived_warehouse"
                    }"
                    data-route="${p.route_id}"
                    data-location="${p.route.destination_id}"
                  >${
                    p.parcel.destination_id === p.route.destination_id
                      ? "Delivered"
                      : "Arrived Warehouse"
                  }
                  </button>
                </div>
            </div>
        `
    )
    .join("");

  document.querySelectorAll(".complete-btn").forEach((btn) => {
    btn.addEventListener("click", (e) => {
      const parcelId = e.currentTarget.dataset.id;
      const routeId = e.currentTarget.dataset.route;
      const status = e.currentTarget.dataset.status;
      const locationId = e.currentTarget.dataset.location;
      handleInsertLog(parcelId, routeId, status, locationId);
    });
  });

  document.querySelectorAll(".cancel-btn").forEach((btn) => {
    btn.addEventListener("click", (e) => {
      const parcelId = e.currentTarget.dataset.id;
      const routeId = e.currentTarget.dataset.route;
      const locationId = e.currentTarget.dataset.location;
      handleCancelParcel(parcelId, routeId, locationId);
    });
  });
}

window.addEventListener("DOMContentLoaded", renderParcelTable);
document
  .getElementById("search-parcel")
  .addEventListener("input", renderParcelTable);

async function handleInsertLog(parcelId, routeId, status, locationId, driverId) {
  const confirmMsg = {
    arrived_warehouse: "ยืนยันว่าพัสดุถึงคลังแล้ว?",
    delivered: "ยืนยันส่งสำเร็จหรือไม่?",
    failed: "ยืนยันยกเลิกพัสดุหรือไม่?",
  };

  if (!confirm(confirmMsg[status] || "Are you sure?")) return;

  const payload = {
    input: {
      parcel_id: parcelId,
      route_id: routeId,
      action: status,
      note: getNoteByStatus(status),
      employee_id: driverId,
      location_id: locationId,
    },
  };

  try {
    const data = await insertParcelLog(payload);
    alert(data.message || "Status updated successfully");
    await renderParcelTable();
  } catch (err) {
    console.error("❌ Failed to update log:", err);
    alert("เกิดข้อผิดพลาดในการอัปเดตสถานะพัสดุ");
  }
}

function getNoteByStatus(status) {
  switch (status) {
    case "arrived_warehouse":
      return "Parcel arrived at warehouse.";
    case "delivered":
      return "Parcel delivered successfully.";
    case "failed":
      return "Delivery failed.";
    default:
      return "";
  }
}
