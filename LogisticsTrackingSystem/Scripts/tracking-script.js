function revealPanels() {
  document.querySelector(".Panel02").style.display = "flex";
  document.querySelector(".Panel03").style.display = "flex";
}

let value;
const input = document.getElementById("TrackingBxTx");

input.addEventListener("change", function (event) {
  value = event.target.value;
});

const form = document.getElementById("TrackingForm");

form.addEventListener("submit", async function (event) {
  event.preventDefault();
  if (!value) {
    alert("Not value");
  }

  try {
    const res = await fetch(
      `/Services/DbService.svc/tracking/${encodeURIComponent(value)}`,
      {
        method: "GET",
        headers: { "Content-Type": "application/json" },
      }
    );

    if (!res.ok) {
      alert("Not found parcel");
    }

    const parcelLogs = await res.json();
    console.log(parcelLogs);

    revealPanels();
  } catch (err) {
    console.log(err);
  }
});
