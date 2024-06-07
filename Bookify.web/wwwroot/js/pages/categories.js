function onClickToggleStatusBtn(item) {
    Swal.fire({
        html: `<h1>Are you Sure?</h1>`,
        icon: "info",
        buttonsStyling: false,
        showCancelButton: true,
        confirmButtonText: "Ok, got it!",
        cancelButtonText: 'Nope, cancel it',
        customClass: {
            confirmButton: "btn btn-primary",
            cancelButton: 'btn btn-danger'
        }
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            changeToggleStatus(item);
            Swal.fire("Saved!", "", "success");
        } 
    });
}

function changeToggleStatus(item) {
    var id = item.data('id');
    $.ajax({
        method: 'post',
        url: "/Categories/ToggleStatus/"+id, 
        data: {
            "__RequestVerificationToken": $('meta[name=__RequestVerificationToken]').attr("content"),
        },
        success: function(result){
            if(result.isDeleted) {
                var html = (`<span class="badge badge-light-danger fs-base p-3">
                    <i class="ki-outline ki-trash fs-5 text-danger ms-n1 me-3"></i>
                    Deleted
                </span>`)
            } else {
                 var html = (`<span class="badge badge-light-success fs-base p-3">
                    <i class="ki-outline ki-subtitle fs-5 text-success ms-n1 me-3"></i>
                    Not Deleted
                </span>`)
            }
            item.parent().parent().find("#isDeleted").html(html)
            item.parent().parent().find("#updatedOn").text(result.updatedOn)
        }
    });
}
var createModal = new bootstrap.Modal(document.getElementById('categoryModal'), {keyboard: false});
var updateRow;
function onClickNewOrUpdateCategory(type, button, id = null) {
    $.ajax({
        url: "/Categories/" +(type == 'Update' ? "UpdateAjax" : "CreateAjax") + (id != null ? "/"+id : ""),
        success: function(form){
            $("#categoryModal").find('.modal-content').html(form);
            $.validator.unobtrusive.parse($("#categoryModal"))
        },
    });
    if (type == 'Update') updateRow = $('tbody').find("tr#"+id);
}
function onSuccessForm(html) {
    showToast('Category added successfully!', 'text-white bg-success');
    createModal.hide()
    KTDatatablesExample.addRow(html);
}
function onSuccessEditForm(html) {
    showToast('Category edited successfully!', 'text-white bg-success');
    createModal.hide()
    if(updateRow !== undefined) {
        KTDatatablesExample.replaceRow(updateRow, html)
        $(updateRow).replaceWith(html);
        updateRow = undefined;
    }
}
function onModalComplete(form) {KTApp.hidePageLoading();}
function onModalStart() {KTApp.showPageLoading();}