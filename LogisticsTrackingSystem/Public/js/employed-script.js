import {
  deleteEmployee,
  getEmployeeById,
  getEmployees,
  updateEmployee,
} from "../../Scripts/employee.service.js";

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
async function handleEditEmployee(id) {
  const employee = await getEmployeeById(id);
  editId = id;

  document.getElementById("name").value = employee.name;
  document.getElementById("email").value = employee.email;
  document.getElementById("phone").value = employee.phone;
  document.getElementById("role").value = employee.role;
}

async function handleDeleteEmployee(id) {
  if (confirm("คุณแน่ใจหรือไม่ว่าต้องการลบพนักงานคนนี้?")) {
    await deleteEmployee(id);
    renderEmployeeTable();
  }
}

async function renderEmployeeTable() {
  const employees = await getEmployees();

  const search = document.getElementById("search").value.trim().toLowerCase();

  const searchEmployee = employees.filter(
    (e) =>
      e.name.toLowerCase().includes(search) ||
      e.email.toLowerCase().includes(search) ||
      e.phone.toLowerCase().includes(search)
  );

  const employeeElement = document.getElementById("employees");
  employeeElement.innerHTML = searchEmployee
    .map(
      (e) => `
      <tr class="vehicle">
        <td>${e.name}</td>
        <td>${e.role}</td>
        <td>${e.email}</td>
        <td>${e.phone}</td>
        <td class="action-btn">
          <button class="edit-btn" data-id="${e.id}">
            <span class="material-symbols-outlined">
              edit_square
            </span>
          </button>
          <button class="delete-btn" data-id="${e.id}">
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
      handleEditEmployee(id);
      popup.style.display = "flex";
    });
  });

  document.querySelectorAll(".delete-btn").forEach((btn) => {
    btn.addEventListener("click", (e) => {
      const id = e.currentTarget.dataset.id;
      handleDeleteEmployee(id);
    });
  });
}

window.addEventListener("DOMContentLoaded", renderEmployeeTable);
document.getElementById("search").addEventListener("input", renderEmployeeTable);

document
  .getElementById("save-btn")
  .addEventListener("click", async (event) => {
    event.preventDefault();

    const name = document.getElementById("name").value;
    const email = document.getElementById("email").value;
    const phone = document.getElementById("phone").value;
    const role = document.getElementById("role").value;

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
        role: role,
      },
    };

    const data = await updateEmployee(payload);
    alert(data.message);

    document.querySelector(".modalContent").reset();
    popup.style.display = "none";
    editId = null;
    renderEmployeeTable();
  });
