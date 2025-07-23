function validateLegalEntityName(input) {
    const value = input.value.trim();
    const errorSpan = document.getElementById('legalEntityNameError');
    const validPattern = /^[a-zA-Z0-9 ]+$/;
    if (value === '') {
        errorSpan.textContent = 'Legal Entity Name is required.';
        input.classList.add('is-invalid');
        return false;
    } else if (!validPattern.test(value)) {
        errorSpan.textContent = 'Only letters and numbers are allowed.';
        input.classList.add('is-invalid');
        return false;
    } else {
        errorSpan.textContent = '';
        input.classList.remove('is-invalid');
    }
}

function validateLegalEntityCode(input) {
    const codeInput = document.getElementById('legalEntityCode');
    const errorSpanCode = document.getElementById('legalEntityCodeError');
    const codeValue = codeInput.value.trim();
    const validPattern = /^[a-zA-Z0-9]+$/;
    if (codeValue === '') {
        errorSpanCode.textContent = 'Legal Entity Code is required.';
        codeInput.classList.add('is-invalid');
        return false;
    } else if (!validPattern.test(codeValue)) {
        errorSpanCode.textContent = 'Only letters and numbers are allowed.';
        codeInput.classList.add('is-invalid');
        return false;
    }
    else {
        errorSpanCode.textContent = '';
        codeInput.classList.remove('is-invalid');
    }
}

function nameValidate(input) {
    const value = input.value.trim();
    const validPattern = /^[a-zA-Z]+$/;
    let errorSpan;
    if (input.id === 'fName') {
        errorSpan = document.getElementById('firstNameError');
    } else if (input.id === 'lName') {
        errorSpan = document.getElementById('lastNameError');
    }
    if (value === '') {
        errorSpan.textContent = 'This field is required.';
        input.classList.add('is-invalid');
    } else if (!validPattern.test(value)) {
        errorSpan.textContent = 'Only alphabetic characters are allowed.';
        input.classList.add('is-invalid');
    } else {
        errorSpan.textContent = '';
        input.classList.remove('is-invalid');
    }
}


