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
        return false;
    } else if (!validPattern.test(value)) {
        errorSpan.textContent = 'Only alphabetic characters are allowed.';
        input.classList.add('is-invalid');
        return false;
    } else {
        errorSpan.textContent = '';
        input.classList.remove('is-invalid');
    }
}
function validateCorpCode(input) {
    const corpCode = document.getElementById('corporateCode');
    const errorSpanCorpCode = document.getElementById('CorporateCodeError');
    const corpCodeValue = corpCode.value.trim();
    const validPattern = /^[a-zA-Z0-9]+$/;
    if (corpCodeValue === '') {
        errorSpanCorpCode.textContent = 'Corporate Code is required.';
        corpCode.classList.add('is-invalid');
        return false;
    } else if (!validPattern.test(corpCodeValue)) {
        errorSpanCorpCode.textContent = 'Only letters and numbers are allowed.';
        corpCode.classList.add('is-invalid');
        return false;
    } else {
        errorSpanCorpCode.textContent = '';
        corpCode.classList.remove('is-invalid');
    }
}
function validateBusinessEmail(input) {
    const value = input.value.trim();
    const regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    const errorSpan = document.getElementById('emailBusinesError');

    if (!regex.test(value)) {
        errorSpan.textContent = 'Please enter a valid email address.';
        input.classList.add('is-invalid');
    } else {
        errorSpan.textContent = '';
        input.classList.remove('is-invalid');
    }
}
function validateDeskEmail(input) {
    const value = input.value.trim();
    const regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    const errorSpan = document.getElementById('emailDeskError');

    if (!regex.test(value)) {
        errorSpan.textContent = 'Please enter a valid email address.';
        input.classList.add('is-invalid');
    } else {
        errorSpan.textContent = '';
        input.classList.remove('is-invalid');
    }
}
function validateMobile(input) {
    input.value = input.value.replace(/[^0-9]/g, '');
    const value = input.value;
    const errorSpan = document.getElementById('mobileError');
    if (value.length !== 10) {
        errorSpan.textContent = 'Mobile number must be exactly 10 digits.';
        input.classList.add('is-invalid');
    } else {
        errorSpan.textContent = '';
        input.classList.remove('is-invalid');
    }
}
function validateAddressLine1(input) {
    const maxLength = 40;
    const errorSpan = document.getElementById('addressError1');

    if (input.value.length > maxLength) {
        input.value = input.value.slice(0, maxLength);
    }

    if (input.value.length === maxLength) {
        errorSpan.textContent = 'Maximum 40 characters allowed.';
    } else {
        errorSpan.textContent = '';
    }
}
function validateAddressLine2(input) {
    const maxLength = 40;
    const errorSpan = document.getElementById('addressError2');

    if (input.value.length > maxLength) {
        input.value = input.value.slice(0, maxLength);
    }

    if (input.value.length === maxLength) {
        errorSpan.textContent = 'Maximum 40 characters allowed.';
    } else {
        errorSpan.textContent = '';
    }
}






