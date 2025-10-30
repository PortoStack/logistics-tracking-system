import {
  getParcelById,
  getParcels,
  updateParcel,
} from "../../Scripts/parcel.service.js";
import { statusStyle } from "./status.js";

const logout = document.getElementById("logout");
logout.addEventListener("click", () => {
  localStorage.removeItem("empName");
  localStorage.removeItem("empRole");
  localStorage.removeItem("empId");
  window.location.href = "/Views/sign-in.html";
});

const popup = document.getElementById("popupModal");
const closeModal = document.getElementById("close-btn");

closeModal.addEventListener("click", () => {
  popup.style.display = "none";
});

async function handleEditParcel(id) {
  popup.style.display = "flex";
  const parcel = await getParcelById(id);

  const parcelId = document.getElementById("parcel-id");
  const parcelWeight = document.getElementById("parcel-weight");
  const parcelStatus = document.getElementById("parcel-status");
  const parcelOrigin = document.getElementById("parcel-origin");
  const parcelDestination = document.getElementById("parcel-destination");

  parcelId.value = parcel.id;
  parcelWeight.value = parcel.weight;
  parcelStatus.value = parcel.status;
  parcelOrigin.value = parcel.origin.name;
  parcelDestination.value = parcel.destination.name;
}

async function renderParcelTable() {
  const parcels = await getParcels();

  const search = document.getElementById("search").value.trim().toLowerCase();

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
                <td>${p.type}</td>
                <td>${p.weight}</td>
                <td>${statusStyle(p.status)}</td>
                <td>${p.sender.name}</td>
                <td>${p.receiver.name}</td>
                <td class="action-btn">
                  <button class="edit-btn" data-id="${p.id}">
                    <span class="material-symbols-outlined">
                      edit_square
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
      handleEditParcel(id);
    });
  });
}

window.addEventListener("DOMContentLoaded", renderParcelTable);
document.getElementById("search").addEventListener("input", renderParcelTable);

document
  .getElementById("submit-parcel")
  .addEventListener("click", async (event) => {
    event.preventDefault();

    const payload = {
      input: {
        id: document.getElementById("parcel-id").value,
        status: document.getElementById("parcel-status").value,
      },
    };

    const data = await updateParcel(payload);
    alert(data.message);

    popup.style.display = "none";
    renderParcelTable();
  });
