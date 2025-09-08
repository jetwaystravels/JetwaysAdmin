function validateLegalEntityName(input) {
    const value = input.value.trim();
    const errorSpan = document.getElementById('legalEntityNameError');
    if (value === '') {
        errorSpan.textContent = 'Legal Entity Name is required.';
        input.classList.add('is-invalid');
        return false;
    }  else {
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
function validateDeskEmail(input,e) {
    const value = input.value.trim();
    const regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    const errorSpan = document.getElementById('emailDeskError');

    if (!regex.test(value)) {
        errorSpan.textContent = 'Please enter a valid email address.';
        input.classList.add('is-invalid');
        e.preventDefault();
        return false;
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
        return false;
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
function validatePostalCode(input) {
    let value = input.value;
    const errorSpan = document.getElementById('postalCodeError');
    if (/\D/.test(value)) {
        errorSpan.textContent = 'Only numeric values are allowed.';
        input.classList.add('is-invalid');
        input.value = value.replace(/\D/g, '');
        return;
    }
    if (value.length > 6) {
        input.value = value.slice(0, 6);
    }
    if (input.value.length < 6) {
        errorSpan.textContent = 'Postal code must be exactly 6 digits.';
        input.classList.add('is-invalid');
    } else {
        errorSpan.textContent = '';
        input.classList.remove('is-invalid');
    }
}
function validateIntegrationRef(input) {
    const value = input.value.trim();
    const errorSpan = document.getElementById('integrationRefError');
    const validPattern = /^[a-zA-Z0-9]*$/; 
    if (value.length > 20) {
        errorSpan.textContent = 'Maximum length is 20 characters.';
        input.classList.add('is-invalid');
        return;
    }
    if (!validPattern.test(value)) {
        errorSpan.textContent = 'Only alphanumeric characters allowed.';
        input.classList.add('is-invalid');
    } else {
        errorSpan.textContent = '';
        input.classList.remove('is-invalid');
    }
}


function addUSer(evt) {
    var form = document.getElementById('addUserForm');
    function stopAt(el) {
        if (evt) evt.preventDefault();
        if (el) {
            el.classList.add('is-invalid');
            el.scrollIntoView({ behavior: 'smooth', block: 'center' });
            el.focus();
        }
        return false;
    }
    var eln = document.querySelector('[name="LegalEntityName"]');
    if (eln) {
        validateLegalEntityName(eln);
        if (eln.value.trim() === '') {
            var sp = document.getElementById('legalEntityNameError');
            if (sp) sp.textContent = 'Legal Entity Name is required.';
            return stopAt(eln);
        }
        if (eln.classList.contains('is-invalid')) return stopAt(eln);
    }

   var lec = document.getElementById('legalEntityCode');
    if (lec) {
        validateLegalEntityCode(lec);
        if (lec.classList.contains('is-invalid') || lec.value.trim() === '')
            return stopAt(lec);
    }
    var fpc = document.getElementById('financialpersonalcode');
    if (fpc) {
        var fpcErr = document.getElementById('FinancialpersonalcodeError');
        var fpcVal = fpc.value.trim();
        var alnum = /^[a-zA-Z0-9]+$/;
        if (fpcVal === '') {
            if (fpcErr) fpcErr.textContent = 'Financial Personal Code is required.';
            return stopAt(fpc);
        }
        if (!alnum.test(fpcVal)) {
            if (fpcErr) fpcErr.textContent = 'Only letters and numbers are allowed.';
            return stopAt(fpc);
        }
        if (fpcErr) fpcErr.textContent = '';
        fpc.classList.remove('is-invalid');
    }
    var iata = document.querySelector('[name="AssignIATAGroup"]');
    if (iata && !iata.value) {
        iata.setCustomValidity('Please select IATA Group.');
        iata.reportValidity();
        return stopAt(iata);
    } else if (iata) {
        iata.setCustomValidity('');
        iata.classList.remove('is-invalid');
    } var corp = document.getElementById('corporateCode');
    if (corp && corp.value.trim() !== '') {
        validateCorpCode(corp);
        if (corp.classList.contains('is-invalid')) return stopAt(corp);
    } var fn = document.getElementById('fName');
    if (fn) {
        nameValidate(fn);
        if (fn.classList.contains('is-invalid')) return stopAt(fn);
    }
    var ln = document.getElementById('lName');
    if (ln) {
        nameValidate(ln);
        if (ln.classList.contains('is-invalid')) return stopAt(ln);
    }
 var bEmail = document.getElementById('businessEmailInput');
    if (bEmail && bEmail.value.trim() !== '') {
        validateBusinessEmail(bEmail);
        if (bEmail.classList.contains('is-invalid')) return stopAt(bEmail);
    }

  var mobile = document.getElementById('mobileInput');
    if (mobile) {
        validateMobile(mobile);
        if (mobile.classList.contains('is-invalid')) return stopAt(mobile);
    }

    var a1 = document.getElementById('addressLine1');
    debugger;
    if (a1) {
        var v = a1.value.trim(); 
        if (v === '') {
            var a1e = document.getElementById('addressError1');
            if (a1e) a1e.textContent = 'Registered Address Line 1 is required.';
            a1.classList.add('is-invalid');         
            return stopAt(a1);                       
        } else {
            var a1e = document.getElementById('addressError1');
            if (a1e) a1e.textContent = '';
            a1.classList.remove('is-invalid');
        }
        validateAddressLine1(a1);
        if (v.length > 40) {
            return stopAt(a1);
        }
    }
    var a2 = document.getElementById('addressLine2');
    if (a2) {
        var v = a2.value.trim();
        var a2e = document.getElementById('addressError2');

        if (v === '') {
            if (a2e) a2e.textContent = 'Registered Address Line 2 is required.';
            a2.classList.add('is-invalid');    
            return stopAt(a2);
        }
        validateAddressLine2(a2);
        if (a2e && a2e.textContent) {
            a2.classList.add('is-invalid');
            return stopAt(a2);
        }
        if (a2e) a2e.textContent = '';
        a2.classList.remove('is-invalid');
    }

    // Country
    var ctry = document.getElementById('CountryId');
    if (ctry && !ctry.value) {
        if (evt) evt.preventDefault();
        ctry.setCustomValidity('Please select Country.');
        ctry.reportValidity();    
        ctry.focus();              
        return false;        
    }
    if (ctry) ctry.setCustomValidity('');

    // State
    var state = document.getElementById('StateId');
    if (state && !state.value) {
        if (evt) evt.preventDefault();
        state.setCustomValidity('Please select State.');
        state.reportValidity();
        state.focus();
        return false;
    }
    if (state) state.setCustomValidity('');

    // City
    var city = document.getElementById('CityId');
    if (city && !city.value) {
        if (evt) evt.preventDefault();
        city.setCustomValidity('Please select City.');
        city.reportValidity();
        city.focus();
        return false;
    }
    if (city) city.setCustomValidity('');

   var pc = document.getElementById('postalCodeInput');
    if (pc) {
        validatePostalCode(pc);
        if (pc.classList.contains('is-invalid')) return stopAt(pc);
    }

   var acct = document.querySelector('[name="AccountType"]');
    if (acct && !acct.value) {
        acct.setCustomValidity('Please select Account Type.');
        acct.reportValidity();
        return stopAt(acct);
    } else if (acct) {
        acct.setCustomValidity('');
        acct.classList.remove('is-invalid');
    }

    
    var curr = document.getElementById('currencySelect');
    if (curr && !curr.value) {
        if (evt) evt.preventDefault();
        curr.setCustomValidity('Please select Customer Base Currency.');
        curr.classList.add('is-invalid');   
        curr.focus();
        curr.reportValidity();             
        return false;                       
    } else if (curr) {
        curr.setCustomValidity('');
        curr.classList.remove('is-invalid');
    }
    var baseCountry = document.querySelector('[name="CustomerBaseCountry"]');
    if (baseCountry && !baseCountry.value) {
        if (evt) evt.preventDefault();
        baseCountry.setCustomValidity('Please select Customer Base Country.');
        baseCountry.classList.add('is-invalid');
        baseCountry.focus();
        baseCountry.reportValidity();
        return false;
    } else if (baseCountry) {
        baseCountry.setCustomValidity('');
        baseCountry.classList.remove('is-invalid');
    }

    var desk = document.getElementById('travelDeskEmailInput');
    if (desk) {
        validateDeskEmail(desk, evt);
        if (desk.classList.contains('is-invalid') || desk.value.trim() === '') return stopAt(desk);
        var de = document.getElementById('emailDeskError');
        if (de) de.textContent = '';
        desk.classList.remove('is-invalid');
    }
    if (form && !form.checkValidity()) {
        if (evt) evt.preventDefault();
        var firstBad = form.querySelector(':invalid');
        if (firstBad) {
            firstBad.scrollIntoView({ behavior: 'smooth', block: 'center' });
            firstBad.focus();
        }
        form.reportValidity();
        return false;
    }
    return true;
}


    (function () {
        function exclusiveRadios(nameA, nameB) {
            const groupA = document.querySelectorAll('input[name="' + nameA + '"]');
            const groupB = document.querySelectorAll('input[name="' + nameB + '"]');

            function uncheck(group) { group.forEach(r => r.checked = false); }

            groupA.forEach(r => r.addEventListener('change', () => {
                if (r.checked) uncheck(groupB);
            }));

            groupB.forEach(r => r.addEventListener('change', () => {
                if (r.checked) uncheck(groupA);
            }));
        }
  if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', () =>
            exclusiveRadios('CreateNewGDSProfile', 'UpdateExistingCustomerProfile')
        );
  } else {
        exclusiveRadios('CreateNewGDSProfile', 'UpdateExistingCustomerProfile');
  }
})();

$('#clearbutton').on('click', function (e) {
    e.preventDefault();
    $('#addUserForm')[0].reset();
});










