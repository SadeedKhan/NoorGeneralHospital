$(document).ready(function () {
    _DashboardAppointmentDetails();
});

function _DashboardAppointmentDetails() {
    $.ajax({
        type: "Get",
        url: "/Dashboard/DashboardAppointmentDetails",
        success: function (res) {
            $('#_DashboardAppointmentDetailsPartialView').empty().html(res);
            _DashboardDoctorsDetails();
        },
        error: function (res) {
            alert(res);
        }
    });
}

function _DashboardDoctorsDetails() {
    $.ajax({
        type: "Get",
        url: "/Dashboard/DashboardDoctorsDetails",
        success: function (res) {
            $('#_DashboardDoctorsDetailsPartialView').empty().html(res);
        },
        error: function (res) {
            alert(res);
        }
    });
}

function DoctorProfile(id) {
    $.ajax({
        type: "POST",
        url: "/Doctor/DoctorProfile",
        data: { Id: id },
        success: function (res) {
            debugger
            $('#_DoctorPartialView').empty().html(res);
        },
        error: function (res) {
            alert(res);
        }
    });
}
