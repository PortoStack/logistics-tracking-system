// Sign In Form
const signInForm = document.getElementById("sign-in-form");

const emailElement = document.getElementById("email");
const passwordElement = document.getElementById("password");

signInForm.addEventListener("submit", async (event) => {
  event.preventDefault();

  try {
    const email = emailElement.value.trim();
    const password = passwordElement.value;

    if (!email || !password) {
      alert("Please fill in all fields.");
      return;
    }

    const res = await fetch("/Services/DbService.svc/signin", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email: email,
        password: password,
      }),
    });

    const data = await res.json();
    console.log(data)
    const employee = JSON.parse(data);

    if (!res.ok) {
      alert("Sign in failed!");
      return;
    }

    if (res.ok) {
      localStorage.setItem("empRole", employee.role);
      localStorage.setItem("empName", employee.name);
      localStorage.setItem("empId", employee.id);
    }

    alert(employee.message);
    signInForm.reset();

    if (employee.role === "manager") {
      window.location.href = "/Views/manager/dashboard.html";
      return;
    }

    if (employee.role === "employee") {
      window.location.href = "/Views/employee/parcel.html";
      return;
    }

    if (employee.role === "driver") {
      window.location.href = "/Views/driver/tracking.html";
      return;
    }

    window.location.href = "/Views/tracking.html";
  } catch (err) {
    console.error(err);
    alert("Error: " + err.message);
  }
});
