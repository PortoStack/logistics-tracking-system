async function loadParcels() {
    try {
        const res = await fetch("/Services/dbService.svc/parcels");
        const parcels = await res.json();
        console.log(parcels);

        const tbody = document.getElementById("parcels");
        tbody.innerHTML = "";

        parcels.forEach(p => {
            const tr = document.createElement("tr");

            const tdTracking = document.createElement("td");
            tdTracking.textContent = p.trackingNo;
            tr.appendChild(tdTracking);

            const tdType = document.createElement("td");
            tdType.textContent = p.type;
            tr.appendChild(tdType);

            const tdWeight = document.createElement("td");
            tdWeight.textContent = p.weight;
            tr.appendChild(tdWeight);

            const tdStatus = document.createElement("td");
            tdStatus.textContent = p.status;
            tr.appendChild(tdStatus);

            tbody.appendChild(tr);
        });
    } catch (err) {
        console.error(err);
    }
}

window.onload = loadParcels;