
    $(document).ready(function () {
        $("input[type=checkbox][id^=appStatusCheckbox_]").change(function () {
            var supplierId = $(this).attr("id").split("_")[1];
            var form = $("#appStatusForm_" + supplierId);
            $.ajax({
                url: "/OrganizationStructure/UpdateSupplierAppStatus",
                type: "POST",
                data: form.serialize(),
                success: function (response) {
                    //alert("Status updated successfully:", response);
                },
                error: function (xhr, status, error) {
                    console.error("Error updating status:", error);
                    alert("Something went wrong!");
                }
            });
        });
    });


    $(function () {
        $(".select").select2();
    });
    flatpickr(".date-main", {
        dateFormat: "Y-m-d",
        minDate: "today",
        allowInput: true,
        changeMonth: true,
        changeYear: true
    });

    $(document).ready(function () {
        $(document).on("click", ".add-btn", function () {
            let parentTab = $(this).closest(".tab-pane");
            parentTab.find(".default-content").hide();
            parentTab.find(".supplier-content").show();
            $(this).hide();
        });
        $(document).on("click", ".sup-bx", function () {
            let parentTab = $(this).closest(".tab-pane");
            parentTab.find(".default-content, .add-btn").hide();
            parentTab.find(".new-spplier").show();
        });
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            let targetTab = $($(e.target).attr("href"));
            targetTab.find(".default-content").show();
            targetTab.find(".new-spplier, .supplier-content").hide();
            targetTab.find(".add-btn").show();
        });
        $('.nav-link').on("click", function () {
            let targetTab = $($(this).attr("href"));
            targetTab.find(".default-content").show();
            targetTab.find(".new-spplier, .supplier-content").hide();
            targetTab.find(".add-btn").show();
        });
    });


    (function () {
        const BACKDROP = '#dealCodeBackdrop';
        const OPEN_BTN = '#openDealCodeBtn';
        const CLOSE_BTN = '#dealCodeClose';
        const CANCEL_BTN = '#dealCodeCancel';

        function showPopup() {
            document.querySelector(BACKDROP)?.classList.add('show-popup-office');
        }
        function hidePopup() {
            document.querySelector(BACKDROP)?.classList.remove('show-popup-office');
        }

        document.addEventListener('click', function (e) {
            if (e.target.closest(OPEN_BTN)) {
                showPopup();
                return;
            }
            if (e.target.closest(CLOSE_BTN) || e.target.closest(CANCEL_BTN)) {
                hidePopup();
                return;
            }
            const backdrop = document.querySelector(BACKDROP);
            if (backdrop && e.target === backdrop) {
                hidePopup();
            }
        });
        document.addEventListener('keydown', (e) => { if (e.key === 'Escape') hidePopup(); });
    })();

    $(document).ready(function () {
        let lastTab = '#Organization';
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            lastTab = $(e.target).attr('href');
        });
        $('#addOfficeModal').on('shown.bs.modal', function () {
            if (lastTab) {
                $('a[href="' + lastTab + '"]').tab('show');
            }
        });

        // Optional: Reset to first tab if you want fresh start every time
        // $('#addOfficeModal').on('hidden.bs.modal', function () {
        //     lastTab = '#Organization';
        // });
    });


    async function navigateToCredential(el) {
        const { supplierid, legalcode, legalname, idlegal } = el.dataset;
        const idLegalVal = idlegal || document.querySelector('input[name="IdLegal"]')?.value || '';
        const params = new URLSearchParams({
            SupplierId: supplierid,
            IdLegal: idLegalVal,
            LegalEntityCode: legalcode,
            LegalEntityName: legalname
        });
        const url = '/OrganizationStructure/GetSupplierCredential?' + params.toString();
        const container = document.getElementById('credentialModalBody');
        if (!container) {
            window.location.href = url;
            return;
        }
        container.innerHTML = '<div class="p-4 text-center">Loading…</div>';
        try {
            const resp = await fetch(url, { credentials: 'same-origin' });
            if (!resp.ok) throw new Error('HTTP ' + resp.status);
            const html = await resp.text();
            container.innerHTML = html;
            const parentTab = el.closest('.tab-pane');
            if (parentTab) {
                parentTab.querySelector('.default-content')?.classList.add('d-none');
                parentTab.querySelector('.add-btn')?.classList.add('d-none');
                const panel = parentTab.querySelector('.new-spplier');
                if (panel) panel.style.display = '';
            }
        } catch (e) {
            console.error(e);
            container.innerHTML = '<div class="alert alert-danger m-3">Failed to load credentials.</div>';
        }
    }

    $(document).on('click', '.remove-btn', function () {
        $(this).closest('.tag').remove();
    });


function toggleDropdown(event) {
    event.stopPropagation();
    let dropdown = event.currentTarget.closest('.dropdown').querySelector('.dropdown-list');
    document.querySelectorAll('.dropdown-list').forEach(list => {
        if (list !== dropdown) list.classList.remove('active');
    });
    dropdown.classList.toggle('active');
}

function selectItem(event) {
    let clicked = event.currentTarget;
    let displayName = clicked.innerText.trim();            
    let value = clicked.dataset.userId || displayName;     
    let dropdown = clicked.closest('.dropdown');
    let field = dropdown.dataset.field;
    let selectedContainer = dropdown.querySelector('.selected-items');

    
    if (!selectedContainer.querySelector(`[data-value="${value}"]`)) {
        let itemElem = document.createElement('div');
        itemElem.classList.add('selected-item');
        itemElem.dataset.value = value;
        itemElem.innerHTML = `
            ${displayName} 
            <span class="remove-x" onclick="removeItem(this, '${value}', '${field}')">×</span>
        `;
        selectedContainer.appendChild(itemElem);
        updateHiddenField(field, dropdown);
    }

    clicked.closest('.dropdown-list').classList.remove('active');
}

function removeItem(span, value, field) {
    let dropdown = span.closest('.dropdown');
    let selectedContainer = dropdown.querySelector('.selected-items');

    selectedContainer.querySelectorAll('.selected-item').forEach(elem => {
        if (elem.dataset.value === value) {
            elem.remove();
        }
    });

    updateHiddenField(field, dropdown);
}

function updateHiddenField(field, dropdown) {
    let selectedValues = Array.from(dropdown.querySelectorAll('.selected-item'))
        .map(item => item.dataset.value)
        .join(',');
    document.getElementById(field).value = selectedValues;
}


document.addEventListener('click', function (event) {
    document.querySelectorAll('.dropdown-list').forEach(list => {
        if (!list.closest('.dropdown').contains(event.target)) {
            list.classList.remove('active');
        }
    });
});


$(document).on('click', '.dropdown-item', function (e) {
    selectItem(e);
});



    let nonHOValue = document.getElementById("legalEntityCode")?.value || "";
    document.getElementById("legalEntityCode")?.addEventListener("input", function () {
        if (document.getElementById("financialType")?.value !== "HO") {
            nonHOValue = this.value;
        }
    });
    document.getElementById("financialType").addEventListener("change", function () {
        const selectedValue = this.value;
        const parentCode = document.getElementById("parentLegalEntityCode")?.value || "";
        const legalEntityInput = document.getElementById("legalEntityCode");
        if (!legalEntityInput) return;
        if (selectedValue === "HO") {
            if (!nonHOValue) nonHOValue = legalEntityInput.value;
            legalEntityInput.value = parentCode;
            legalEntityInput.readOnly = true;
        } else {
            legalEntityInput.readOnly = false;
            legalEntityInput.value = nonHOValue;
        }
    });
    document.addEventListener("DOMContentLoaded", function () {
        const ft = document.getElementById("financialType");
        if (ft) ft.dispatchEvent(new Event("change"));
    });

    (function () {
        // Inline SVGs (encoded on the fly so we don't need external assets)
        const svgOpen = `<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'>
                    <path d='M1 12s4-7 11-7 11 7 11 7-4 7-11 7S1 12 1 12z' fill='none' stroke='currentColor' stroke-width='1.8' stroke-linecap='round' stroke-linejoin='round'/>
                    <circle cx='12' cy='12' r='3.2' fill='none' stroke='currentColor' stroke-width='1.8'/>
                  </svg>`;

        const svgClosed = `<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'>
                    <path d='M1 12s4-7 11-7c2.8 0 5.4 1.2 7.4 3' fill='none' stroke='currentColor' stroke-width='1.8' stroke-linecap='round'/>
                    <path d='M23 12s-4 7-11 7c-2.8 0-5.4-1.2-7.4-3' fill='none' stroke='currentColor' stroke-width='1.8' stroke-linecap='round'/>
                    <path d='M3 3L21 21' fill='none' stroke='currentColor' stroke-width='1.8' stroke-linecap='round'/>
                  </svg>`;

        function svgToUrl(svg) {
            const encoded = encodeURIComponent(svg)
                .replace(/'/g, "%27").replace(/"/g, "%22");
            return `url("data:image/svg+xml;utf8,${encoded}")`;
        }

        function setIcon(input, visible) {
            const sz = +input.dataset.eyeSize || 20;
            const pad = +input.dataset.eyePad || 10;
            input.style.backgroundImage = visible ? svgToUrl(svgOpen) : svgToUrl(svgClosed);
            input.style.backgroundPosition = `right ${pad}px center`;
            input.style.backgroundSize = `${sz}px ${sz}px`;
            input.dataset.visible = visible ? "1" : "0";
            input.setAttribute("title", visible ? "Hide password" : "Show password");
            input.setAttribute("aria-label", visible ? "Password (visible)" : "Password (hidden)");
        }

        function toggleVisibility(input) {
            const wasText = input.type === "text";
            const start = input.selectionStart, end = input.selectionEnd, scroll = input.scrollLeft;

            input.type = wasText ? "password" : "text";
            setIcon(input, !wasText);
            try { input.setSelectionRange(start, end); } catch { }
            input.scrollLeft = scroll;
            input.focus();
        }

        function clickedEye(input, evt) {
            const rect = input.getBoundingClientRect();
            const sz = (+input.dataset.eyeSize || 20);
            const pad = (+input.dataset.eyePad || 10);
            const iconWidth = sz;
            // clickable area: [right - (iconWidth + pad*2), right]
            const x = (evt.touches ? evt.touches[0].clientX : evt.clientX);
            return x >= rect.right - (iconWidth + pad * 2) && x <= rect.right;
        }

        function init(input) {
            // Initialize correct icon for current type
            setIcon(input, input.type === "text");

            // Click / touch toggle when tapping the eye area
            const handler = (evt) => {
                if (clickedEye(input, evt)) {
                    evt.preventDefault();
                    evt.stopPropagation();
                    toggleVisibility(input);
                }
            };
            input.addEventListener("mousedown", handler);
            input.addEventListener("touchstart", handler, { passive: false });
            input.addEventListener("keydown", (e) => {
                if ((e.ctrlKey || e.metaKey) && e.shiftKey && (e.key === "P" || e.key === "p")) {
                    e.preventDefault();
                    toggleVisibility(input);
                }
            });
        }
        function boot() {
            document.querySelectorAll('input[data-eye="true"]').forEach(init);
        }

        if (document.readyState === "loading") {
            document.addEventListener("DOMContentLoaded", boot);
        } else {
            boot();
        }
    })();




