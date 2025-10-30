import { getCustomerByPhone } from "../../Scripts/customer.service.js";
import { getLocationByContact } from "../../Scripts/location.service.js";
import { getParcels, insertParcel } from "../../Scripts/parcel.service.js";
import { statusStyle } from "./status.js";

const logout = document.getElementById("logout");
logout.addEventListener("click", () => {
  localStorage.removeItem("empName");
  localStorage.removeItem("empRole");
  localStorage.removeItem("empId");
  window.location.href = "/Views/sign-in.html";
});

document.getElementById("emp-name").innerHTML = `
  Welcome, ${localStorage.getItem("empName")}
`;

// Render Parcel Table
async function renderParcelTable() {
  const parcels = await getParcels();

  const search = document.getElementById("search-parcel");
  const searchParcel = parcels.filter((p) =>
    p.id.toLowerCase().includes(search.value.toLowerCase()) ||
    p.sender.phone.includes(search.value) ||
    p.receiver.phone.includes(search.value)
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
document.getElementById("search-parcel").addEventListener("input", renderParcelTable);

//
function setupSearch(buttonClass, prefix) {
  const button = document.querySelector(buttonClass);

  button.addEventListener("click", async () => {
    const input = document.getElementById(`${prefix}-input`);
    const phone = input.value.trim();

    if (!phone) {
      alert(`กรุณากรอกเบอร์โทร ${prefix} ก่อนค้นหา`);
      return;
    }

    const customer = await getCustomerByPhone(phone);

    if (customer) {
      document.getElementById(`${prefix}-name`).value =
        customer.name?.trim() || "";
      document.getElementById(`${prefix}-email`).value =
        customer.email?.trim() || "";
      document.getElementById(`${prefix}-phone`).value =
        customer.phone?.trim() || "";

      await renderLocList(customer.phone, prefix);
    } else {
      console.log(`ไม่พบข้อมูลลูกค้า ${prefix}`);
      alert(`ไม่พบข้อมูลลูกค้า ${prefix}`);
    }
  });
}

//
async function renderLocList(phone, prefix) {
  const locations = await getLocationByContact(phone);
  const div = document.getElementById(`${prefix}-loc-list`);

  if (!locations || locations.length === 0) {
    div.innerHTML = `<p>ไม่พบที่อยู่ของลูกค้า ${prefix}</p>`;
    return;
  }

  div.innerHTML = locations
    .map(
      (loc) => `
    <tr class="location-info">
      <td>
        <input 
          type="radio" 
          id="loc-${prefix}-${loc.id}" 
          name="${prefix}-location"
          value="${loc.id}"
          data-name="${loc.name}"
          data-address="${loc.address}"
          data-type="${loc.type}"
        >
      </td>
      <td>${loc.name}</td>
      <td>${loc.address}</td>
      <td>${loc.contact}</td>
      <td>${loc.type}</td>
    </tr>
  `
    )
    .join("");

  const radios = div.querySelectorAll(`input[name="${prefix}-location"]`);
  radios.forEach((radio) => {
    radio.addEventListener("change", () => {
      const nameInput = document.getElementById(`${prefix}-address-name`);
      const addressInput = document.getElementById(`${prefix}-address`);
      const typeInput = document.getElementById(`${prefix}-address-type`);

      nameInput.value = radio.dataset.name;
      addressInput.value = radio.dataset.address;
      typeInput.value = radio.dataset.type;
    });
  });
}

setupSearch(".search-sender", "sender");
setupSearch(".search-receiver", "receiver");

//
document
  .getElementById("submit-parcel")
  .addEventListener("click", async (event) => {
    event.preventDefault();

    // Sender
    let senderAddressName, senderAddress;
    const selectedSender = document.querySelector(
      'input[name="sender-location"]:checked'
    );
    if (selectedSender) {
      senderAddressName = selectedSender.dataset.name;
      senderAddress = selectedSender.dataset.address;
    } else {
      senderAddressName = document
        .getElementById("sender-address-name")
        .value.trim();
      senderAddress = document.getElementById("sender-address").value.trim();
    }

    // Receiver
    let receiverAddressName, receiverAddress;
    const selectedReceiver = document.querySelector(
      'input[name="receiver-location"]:checked'
    );
    if (selectedReceiver) {
      receiverAddressName = selectedReceiver.dataset.name;
      receiverAddress = selectedReceiver.dataset.address;
    } else {
      receiverAddressName = document
        .getElementById("receiver-address-name")
        .value.trim();
      receiverAddress = document
        .getElementById("receiver-address")
        .value.trim();
    }

    // ตรวจสอบว่ากรอกครบ
    if (
      !senderAddressName ||
      !senderAddress ||
      !receiverAddressName ||
      !receiverAddress
    ) {
      alert("กรุณากรอก/เลือกที่อยู่ผู้ส่งและผู้รับ");
      return;
    }

    const payload = {
      parcel_input: {
        type: document.getElementById("parcel-type").value,
        weight: document.getElementById("parcel-weight").value,
        status: "pending",
      },
      customer_input: [
        {
          name: document.getElementById("sender-name").value,
          email: document.getElementById("sender-email").value,
          phone: document.getElementById("sender-phone").value,
        },
        {
          name: document.getElementById("receiver-name").value,
          email: document.getElementById("receiver-email").value,
          phone: document.getElementById("receiver-phone").value,
        },
      ],
      location_input: [
        {
          name: document.getElementById("sender-address-name").value,
          contact: document.getElementById("sender-phone").value,
          address: document.getElementById("sender-address").value,
          type: document.getElementById("sender-address-type").value,
        },
        {
          name: document.getElementById("receiver-address-name").value,
          contact: document.getElementById("receiver-phone").value,
          address: document.getElementById("receiver-address").value,
          type: document.getElementById("receiver-address-type").value,
        },
      ],
      employee: { id: localStorage.getItem("empId") },
    };

    document.getElementById("form-panel").reset();

    const data = await insertParcel(payload);
    alert(data.message);

    window.location.href = "/Views/employee/parcel.html";
    renderParcelTable();
  });
