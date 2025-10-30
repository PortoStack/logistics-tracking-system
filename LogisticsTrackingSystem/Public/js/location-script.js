import {
  getLocationById,
  getLocations,
  insertLocation,
  updateLocation,
  deleteLocation,
} from "../../Scripts/location.service.js";
import { locationTypeStyle } from "./type.js";

const logout = document.getElementById("logout");
logout.addEventListener("click", () => {
  localStorage.removeItem("empName");
  localStorage.removeItem("empRole");
  localStorage.removeItem("empId");
  window.location.href = "/Views/sign-in.html";
});

const locationForm = document.querySelector(".location-form");
const switchBtn = document.getElementById("switch-btn");

switchBtn.addEventListener("click", () => {
  locationForm.classList.toggle("active");
  document.getElementById("location-form").reset();
  editingId = null;
});

let editingId = null;
async function handleEditLocation(id) {
  const location = await getLocationById(id);
  editingId = location.id;

  document.getElementById("name").value = location.name;
  document.getElementById("address").value = location.address;
  document.getElementById("contact").value = location.contact;
  document.getElementById("type").value = location.type;
}

async function handleDeleteLocation(id) {
  if (confirm("คุณแน่ใจหรือไม่ว่าต้องการลบรถคันนี้?")) {
    await deleteLocation(id);
    renderLocationTable();
  }
}

async function renderLocationTable() {
  const locations = await getLocations();

  const search = document.getElementById("search").value.trim().toLowerCase();

  const searchLocation = locations.filter(
    (loc) =>
      loc.name.toLowerCase().includes(search) ||
      loc.address.toLowerCase().includes(search)
  );

  const locationElement = document.getElementById("locations");
  locationElement.innerHTML = searchLocation
    .map(
      (loc) => `
      <tr class="location">
        <td>${loc.name}</td>
        <td>${loc.address}</td>
        <td>${loc.contact}</td>
        <td>${locationTypeStyle(loc.type)}</td>
        <td class="action-btn">
          <button class="edit-btn" data-id="${loc.id}">
            <span class="material-symbols-outlined">
              edit_square
            </span>
          </button>
          <button class="delete-btn" data-id="${loc.id}">
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
      handleEditLocation(id);
      locationForm.classList.toggle("active");
    });
  });

  document.querySelectorAll(".delete-btn").forEach((btn) => {
    btn.addEventListener("click", (e) => {
      const id = e.currentTarget.dataset.id;
      handleDeleteLocation(id);
    });
  });
}

window.addEventListener("DOMContentLoaded", renderLocationTable);
document
  .getElementById("search")
  .addEventListener("input", renderLocationTable);

document.getElementById("save-btn").addEventListener("click", async (event) => {
  event.preventDefault();

  const name = document.getElementById("name").value;
  const address = document.getElementById("address").value;
  const contact = document.getElementById("contact").value;
  const type = document.getElementById("type").value;

  if (!name || !address || !contact) {
    alert("กรุณากรอกข้อมูลให้ครบ");
    return;
  }

  if (editingId) {
    const payload = {
      input: {
        id: editingId,
        name: name,
        address: address,
        contact: contact,
        type: type,
      },
    };
    const data = await updateLocation(payload);
    // alert(data.message);
  } else {
    const payload = {
      input: {
        name: name,
        address: address,
        contact: contact,
        type: type,
      },
    };
    const data = await insertLocation(payload);
    // alert(data.message);
  }

  document.getElementById("location-form").reset();
  editingId = null;
  locationForm.classList.toggle("active");
  renderLocationTable();
});
