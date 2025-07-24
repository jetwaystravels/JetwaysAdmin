// Customer currency
const countryToCurrency = {
    "India": "₹",
    "United States": "$",
    "United Kingdom": "£",
    "England": "£",
    "European Union": "€",
    "Japan": "¥",
    "Australia": "A$",
    "Canada": "C$",
    "China": "¥",
    "UAE": "AED",
    "Russia": "₽"
};
function updateCurrencyDisplay() {
    const select = document.getElementById("currencySelect");
    for (let i = 0; i < select.options.length; i++) {
        const opt = select.options[i];
        const countryName = opt.value;
        const original = opt.getAttribute('data-original');
        if (original) {
            opt.textContent = original;
        } else {
            opt.setAttribute('data-original', opt.textContent);
        }
        if (countryToCurrency[countryName]) {
            opt.textContent = `${countryName} (${countryToCurrency[countryName]})`;
        }
    }
}
window.onload = updateCurrencyDisplay;
