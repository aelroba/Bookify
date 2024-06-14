"use strict";

// Class definition
var KTDatatablesExample = function () {
    // Shared variables
    var table;
    var datatable;
    var docName;
    var dtIdName;

    // Private functions
    var initDatatable = function (dtIdname, exportDocumentName) {
        dtIdname = dtIdname;
        docName = exportDocumentName;
        // Set date data order
        const tableRows = table.querySelectorAll('tbody tr');

        // Init datatable --- more info on datatables: https://datatables.net/manual/
        datatable = $(table).DataTable({
            "info": false,
            // 'order': [],
            "columnDefs": [
                  {
                        orderable: false,
                        targets: 0
                    }
            ],
            'pageLength': 10,
        });
    }
    const checkbox = (dtIdname) => {
        const e = o.querySelectorAll('tbody [type="checkbox"]');
        let c = !1,
            l = 0;
        e.forEach((e) => {
            e.checked && ((c = !0), l++);
        }),
            c ? ((r.innerHTML = l), t.classList.add("d-none"), n.classList.remove("d-none")) : (t.classList.remove("d-none"), n.classList.add("d-none"));
    };

    // Hook export buttons
    var exportButtons = (tableItem) => {
        const documentTitle = docName;
        var exportableColumns = [];
        $.each(tableItem.find('th'), function(i, v) {
            if(!v.hasAttribute('non-exportable'))
                exportableColumns.push(i)
        })
        var buttons = new $.fn.dataTable.Buttons(table, {
            buttons: [
                {
                    extend: 'copyHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exportableColumns
                    }
                },
                {
                    extend: 'excelHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exportableColumns
                    }
                },
                {
                    extend: 'csvHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exportableColumns
                    }
                },
                {
                    extend: 'pdfHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exportableColumns
                    }
                }
            ]
        }).container().appendTo($('#kt_datatable_example_buttons'));

        // Hook dropdown menu click event to datatable export buttons
        const exportButtons = document.querySelectorAll('#kt_datatable_example_export_menu [data-kt-export]');
        exportButtons.forEach(exportButton => {
            exportButton.addEventListener('click', e => {
                e.preventDefault();

                // Get clicked export value
                const exportValue = e.target.getAttribute('data-kt-export');
                const target = document.querySelector('.dt-buttons .buttons-' + exportValue);

                // Trigger click event on hidden datatable export buttons
                target.click();
            });
        });
    }

    // Search Datatable --- official docs reference: https://datatables.net/reference/api/search()
    var handleSearchDatatable = () => {
        const filterSearch = document.querySelector('[data-kt-filter="search"]');
        filterSearch.addEventListener('keyup', function (e) {
            datatable.search(e.target.value).draw();
        });
    }

    // Public methods
    return {
        init: function () {
            var dataTableIdName = $('meta[name=datatable]').attr("content");
            table = document.querySelector("#kt_datatable");

            if ( !table ) {
                return;
            }

            initDatatable(dataTableIdName, $('#'+dataTableIdName).attr('exportFileName'));
            exportButtons($('#'+dataTableIdName));
            handleSearchDatatable();
        }, 
        addRow: function(row) {
            datatable.row.add($(row)).draw();
        }, 
        replaceRow: function(oldRow, newRow) {
            datatable.row(oldRow).remove().draw();
            KTDatatablesExample.addRow(newRow);
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTDatatablesExample.init();
});