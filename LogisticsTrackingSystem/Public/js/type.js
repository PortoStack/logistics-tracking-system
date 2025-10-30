export function locationTypeStyle(type) {
  const typeStyleList = {
    warehouse: `<span class="type warehouse">Warehouse</span>`,
    house: `<span class="type house">House</span>`,
    office: `<span class="type office">Office</span>`,
    pickup_point: `<span class="type pickup-point">Pickup Point</span>`,
  };

  return typeStyleList[type] || `<span class="type unknown">${type}</span>`;
}