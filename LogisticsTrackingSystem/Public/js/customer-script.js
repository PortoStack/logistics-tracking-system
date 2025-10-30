import {
  deleteCustomer,
  getCustomerByPhone,
  getCustomers,
  updateCustomer,
} from "../../Scripts/customer.service.js";

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

var editId = null;
async function handleEditCustomer(phone) {
  const customer = await getCustomerByPhone(phone);
  editId = customer.id;

  document.getElementById("name").value = customer.name;
  document.getElementById("email").value = customer.email;
  document.getElementById("phone").value = customer.phone;
}

async function handleDeleteCustomer(id) {
  if (confirm("คุณแน่ใจหรือไม่ว่าต้องการลบพนักงานคนนี้?")) {
    await deleteCustomer(id);
    renderEmployeeTable();
  }
}

async function renderCustomerTable() {
  const customers = await getCustomers();

  const search = document.getElementById("search").value.trim().toLowerCase();

  const searchCustomer = customers.filter(
    (e) =>
      e.name.toLowerCase().includes(search) ||
      e.email.toLowerCase().includes(search) ||
      e.phone.toLowerCase().includes(search)
  );

  const customerElement = document.getElementById("customers");
  customerElement.innerHTML = searchCustomer
    .map(
      (c) => `
      <tr class="vehicle">
        <td>${c.name}</td>
        <td>${c.email}</td>
        <td>${c.phone}</td>
        <td class="action-btn">
          <button class="edit-btn" data-id="${c.phone}">
            <span class="material-symbols-outlined">
              edit_square
            </span>
          </button>
          <button class="delete-btn" data-id="${c.id}">
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
      handleEditCustomer(id);
      popup.style.display = "flex";
    });
  });

  document.querySelectorAll(".delete-btn").forEach((btn) => {
    btn.addEventListener("click", (e) => {
      const id = e.currentTarget.dataset.id;
      handleDeleteCustomer(id);
    });
  });
}

window.addEventListener("DOMContentLoaded", renderCustomerTable);
document
  .getElementById("search")
  .addEventListener("input", renderCustomerTable);

document.getElementById("save-btn").addEventListener("click", async (event) => {
  event.preventDefault();

  const name = document.getElementById("name").value;
  const email = document.getElementById("email").value;
  const phone = document.getElementById("phone").value;

  if (!name || !email || !phone) {
    alert("กรุณากรอกข้อมูลให้ครบ");
    return;
  }

  const payload = {
    input: {
      id: editId,
      name: name,
      email: email,
      phone: phone,
    },
  };

  const data = await updateCustomer(payload);
  alert(data.message);

  document.querySelector(".modalContent").reset();
  popup.style.display = "none";
  editId = null;
  renderEmployeeTable();
});
