

// Select elements
const toastElement = document.getElementById('kt_docs_toast_toggle');

// Get toast instance --- more info: https://getbootstrap.com/docs/5.1/components/toasts/#getinstance
const toast = bootstrap.Toast.getOrCreateInstance(toastElement);
function showToast(content =null, cssClasses = null, title = null, date = null) {
    if(title != null)
        $("#kt_docs_toast_toggle").find('#title').text(title)
    if(cssClasses != null)
        $("#kt_docs_toast_toggle").find('#body').addClass(cssClasses)
    if(date != null)
        $("#kt_docs_toast_toggle").find('#date').text(date)
    if(content != null)
        $("#kt_docs_toast_toggle").find('#body').html(content)
    toast.show();
}
$(document).ready(function (){
    var successMsg = $("#successMsg").text();
    if(successMsg != '') showToast(successMsg, 'text-white bg-success')
    var errorMsg = $("#errorMsg").text();
    if(errorMsg != '') showToast(errorMsg, 'text-white bg-danger')

});


$.validator.setDefaults({

    highlight: function (element) {
        $(element).addClass("is-invalid").removeClass("is-valid");
        $(element.form).find("[data-valmsg-for=" + element.id + "]").addClass("invalid-feedback");
    },
    unhighlight: function (element) {
        $(element).addClass("is-valid").removeClass("is-invalid");
        $(element.form).find("[data-valmsg-for=" + element.id + "]").removeClass("invalid-feedback");
    },
});