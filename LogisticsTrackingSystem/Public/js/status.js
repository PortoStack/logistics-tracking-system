export function statusStyle(status) {
  const statusStyleList = {
    in_transit: `<span class="status in-transit">In Transit</span>`,
    pending: `<span class="status pending">Pending</span>`,
    delivered: `<span class="status delivered">Delivered</span>`,
    cancelled: `<span class="status cancelled">Cancelled</span>`,
  };

  return statusStyleList[status] || `<span class="status unknown">${status}</span>`;
}

export function vehicleStatusStyle(status) {
  const statusStyleList = {
    available: `<span class="status available">Available</span>`,
    in_use: `<span class="status in-use">In Use</span>`,
    maintenance: `<span class="status maintenance">Maintenance</span>`,
  };

  return statusStyleList[status] || `<span class="status unknown">${status}</span>`;
}

export function routeStatusStyle(status) {
  const statusStyleList = {
    in_progress: `<span class="status in-progress">In Progress</span>`,
    assigned: `<span class="status assigned">Assigned</span>`,
    completed: `<span class="status completed">Completed</span>`,
    cancelled: `<span class="status cancelled">Cancelled</span>`,
  };  

  return statusStyleList[status] || `<span class="status unknown">${status}</span>`;
}