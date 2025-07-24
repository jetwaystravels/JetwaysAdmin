//Date Validation

function getTodayDate() {
    const today = new Date();
    return today.toISOString().split('T')[0]; 
}
function restrictActivationDate() {
    const activDateInput = document.getElementById("activadate");
    activDateInput.setAttribute("min", getTodayDate());
    const deactInput = document.getElementById("deactivatedate");
    if (activDateInput.value) {
        deactInput.setAttribute("min", activDateInput.value);
    }
}
function setMinDeactivationDate() {
    const activDate = document.getElementById("activadate").value;
    const deactInput = document.getElementById("deactivatedate");

    if (activDate) {
        deactInput.setAttribute("min", activDate);
        if (deactInput.value && deactInput.value < activDate) {
            alert("Deactivation date cannot be before Activation date.");
            deactInput.value = '';
        }
    }
}
function restrictDeactivationDate() {
    const activDate = document.getElementById("activadate").value;
    const deactInput = document.getElementById("deactivatedate");

    if (activDate) {
        deactInput.setAttribute("min", activDate);

        if (deactInput.value && deactInput.value < activDate) {
            alert("Deactivation date cannot be before Activation date.");
            deactInput.value = '';
        }
    }
}

