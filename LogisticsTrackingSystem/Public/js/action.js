export function actionStyle(action) {
  const actionStyleList = {
    received: `<span class="status received">Received</span>`,
    in_transit: `<span class="status in-transit">In Transit</span>`,
    arrived_warehouse: `<span class="status arrived-warehouse">Arrived Warehouse</span>`,
    out_for_delivery: `<span class="status out-for-delivery">Out for Delivery</span>`,
    delivered: `<span class="status delivered">Delivered</span>`,
    failed: `<span class="status failed">Failed</span>`,
  };

  return actionStyleList[action] || `<span class="status unknown">${action}</span>`;
}
