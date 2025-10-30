import { getParcelLogs } from "../../Scripts/parcel-log.service.js";
import { getParcelStat } from "../../Scripts/parcel.service.js";
import { actionStyle } from "./action.js";

const logout = document.getElementById("logout");
logout.addEventListener("click", () => {
  localStorage.removeItem("empName");
  localStorage.removeItem("empRole");
  localStorage.removeItem("empId");
  window.location.href = "/Views/sign-in.html";
});

function statusLabel(status) {
  const statusList = {
    in_transit: "In Transit",
    pending: "Pending",
    delivered: "Delivered",
    cancelled: "Cancelled",
  };

  return statusList[status];
}

function statusIcons(status) {
  const statusList = {
    in_transit: "delivery_truck_speed",
    pending: "package_2",
    delivered: "hand_package",
    cancelled: "cancel",
  };

  return statusList[status];
}

async function renderParcelStat() {
  const stats = await getParcelStat();

  const parcelStat = document.getElementById("parcel-stat");
  parcelStat.innerHTML = stats
    .map(
      (stat) => `
        <div class="Box">
          <div class="BoxContent">
            <h6>${statusLabel(stat.status)}</h6>
            <h2>${stat.count}</h2>
            <h6>MORE THAN LAST WEEK</h6>
          </div>
          <span style="font-size: 8rem;" class="material-symbols-outlined">
            ${statusIcons(stat.status)}
          </span>
        </div>
      `
    )
    .join("");
}

async function renderParcelLogs() {
  const parcelLogs = await getParcelLogs();

  const tbody = document.getElementById("parcel-logs");
  tbody.innerHTML = parcelLogs
    .map(
      (pl) => `
          <tr class="parcel-logs">
            <td>${pl.parcel_id}</td>
            <td>${pl.timestamp}</td>
            <td>${actionStyle(pl.action)}</td>
            <td>${pl.note}</td>
            <td>${pl.handled_by.name}</td>
          </tr>
      `
    )
    .join("");
}

window.addEventListener("DOMContentLoaded", renderParcelStat);
window.addEventListener("DOMContentLoaded", renderParcelLogs);
