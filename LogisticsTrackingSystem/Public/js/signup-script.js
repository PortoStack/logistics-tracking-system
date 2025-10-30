// Sign Up Form
const signUpForm = document.getElementById("sign-up-form");

const usernameElement = document.getElementById("username");
const emailElement = document.getElementById("email");
const phoneElement = document.getElementById("phone");
const passwordElement = document.getElementById("password");
const confirmPasswordElement = document.getElementById("confirmPassword");

signUpForm.addEventListener("submit", async function (event) {
  event.preventDefault();

  try {
    const username = usernameElement.value.trim();
    const email = emailElement.value.trim();
    const phone = phoneElement.value.trim();
    const password = passwordElement.value;
    const confirmPassword = confirmPasswordElement.value;

    if (!username || !email || !phone || !password) {
      alert("Please fill in all fields.");
      return;
    }

    if (password !== confirmPassword) {
      alert("Passwords do not match!");
      return;
    }

    const res = await fetch("/Services/DbService.svc/signup", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        name: username,
        phone: phone,
        email: email,
        password: password,
      }),
    });

    if (!res.ok) {
      alert("Sign up failed!");
      return;
    }

    alert("Sign up successful!");
    signUpForm.reset();
  } catch (err) {
    console.error(err);
    alert("Error: " + err.message);
  }
});
