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
    
    const username = usernameElement.value;
    const email = emailElement.value;
    const phone = phoneElement.value;
    const password = passwordElement.value;
    const confirmPassword = confirmPasswordElement.value;

    const matchPassword = password === confirmPassword;

    if (!matchPassword) {
      return "Password is not match";
    }

    const res = await fetch("/Service/DbService.svc/signup", {
      method: "POST",
      body: JSON.stringify({
        name: username,
        email: email,
        phone: phone,
        password: password,
      }),
    });

    const message = await res.json();

    if (!res.ok) {
      return message;
    }

    return message;
  } catch (err) {
    return err.message;
  }
});
