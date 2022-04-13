$(document).ready(function () {
});

function MakeAnAppointment() {
    $.ajax({
        type: "Get",
        url: "/Home/MakeAnAppointment",
        success: function (res) {
            $('#_MakeAnAppointment').empty().html(res);
        },
        error: function (res) {
            alert(res);
        }
    });
}