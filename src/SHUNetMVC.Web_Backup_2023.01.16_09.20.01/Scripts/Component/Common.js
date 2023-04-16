function showToast(message) {
    $("#toastSuccess .toast-body").text(message)
    $("#toastSuccess").toast('show');
}

function showToastError(message) {
    $("#toastError .toast-body").text(message)
    $("#toastError").toast('show');
}

