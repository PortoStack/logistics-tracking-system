import { getEmployeeByRole } from "../../Scripts/employee.service.js";
import {
  deleteVehicle,
  getVehicleById,
  getVehicles,
  insertVehicle,
  updateVehicle,
} from "../../Scripts/vehicle.service.js";
import { vehicleStatusStyle } from "./status.js";

const logout = document.getElementById("logout");
logout.addEventListener("click", () => {
  localStorage.removeItem("empName");
  localStorage.removeItem("empRole");
  localStorage.removeItem("empId");
  window.location.href = "/Views/sign-in.html";
});

const vehicleForm = document.querySelector(".vehicle-form");
const switchBtn = document.getElementById("switch-btn");

switchBtn.addEventListener("click", () => {
  vehicleForm.classList.toggle("active");
  document.getElementById("vehicle-form").reset();
  editingId = null;
});

async function renderDriverOptions() {
  const drivers = await getEmployeeByRole("driver");

  const driversElement = document.getElementById("driver");
  driversElement.innerHTML = drivers.map(
    (d) => `<option value="${d.id}">${d.name}</option>`
  );
}

window.addEventListener("DOMContentLoaded", renderDriverOptions);

let editingId = null;
async function handleEditVehicle(id) {
  const vehicle = await getVehicleById(id);
  editingId = id;

  document.getElementById("license-plate").value = vehicle.license_plate;
  document.getElementById("capacity").value = vehicle.capacity;
  document.getElementById("status").value = vehicle.status;
  document.getElementById("driver").value = vehicle.driver_id;
}

async function handleDeleteVehicle(id) {
  if (confirm("คุณแน่ใจหรือไม่ว่าต้องการลบรถคันนี้?")) {
    await deleteVehicle(id);
    renderVehicleTable();
  }
}

async function renderVehicleTable() {
  const vehicles = await getVehicles();

  const search = document.getElementById("search").value.trim().toLowerCase();

  const searchVehicle = vehicles.filter(
    (v) =>
      v.driver.name.toLowerCase().includes(search) ||
      v.license_plate.toLowerCase().includes(search)
  );

  const vehicleElement = document.getElementById("vehicles");
  vehicleElement.innerHTML = searchVehicle
    .map(
      (v) => `
      <tr class="vehicle">
        <td>${v.driver.name}</td>
        <td>${vehicleStatusStyle(v.status)}</td>
        <td>${v.capacity}</td>
        <td>${v.license_plate}</td>
        <td class="action-btn">
          <button class="edit-btn" data-id="${v.id}">
            <span class="material-symbols-outlined">
              edit_square
            </span>
          </button>
          <button class="delete-btn" data-id="${v.id}">
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
      handleEditVehicle(id);
      vehicleForm.classList.toggle("active");
    });
  });

  document.querySelectorAll(".delete-btn").forEach((btn) => {
    btn.addEventListener("click", (e) => {
      const id = e.currentTarget.dataset.id;
      handleDeleteVehicle(id);
    });
  });
}

window.addEventListener("DOMContentLoaded", renderVehicleTable);
document.getElementById("search").addEventListener("input", renderVehicleTable);

document
  .getElementById("assign-btn")
  .addEventListener("click", async (event) => {
    event.preventDefault();

    const licensePlate = document.getElementById("license-plate").value.trim();
    const capacity = document.getElementById("capacity").value;
    const driverId = document.getElementById("driver").value;
    const status = document.getElementById("status").value;

    if (!licensePlate || !capacity || !driverId) {
      alert("กรุณากรอกข้อมูลให้ครบ");
      return;
    }

    if (editingId) {
      const payload = {
        input: {
          id: editingId,
          license_plate: licensePlate,
          capacity: parseInt(capacity),
          driver_id: driverId,
          status: status,
        },
      };
      const data = await updateVehicle(payload);
      alert(data.message);
    } else {
      const payload = {
        input: {
          license_plate: licensePlate,
          capacity: parseInt(capacity),
          driver_id: driverId,
          status: status,
        },
      };
      const data = await insertVehicle(payload);
      alert(data.message);
    }

    document.getElementById("vehicle-form").reset();
    editingId = null;
    vehicleForm.classList.toggle("active");
    renderVehicleTable();
  });

