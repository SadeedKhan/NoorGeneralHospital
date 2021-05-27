$(document).ready(function () {
    _AppointmentDetail();

    //Begin Form Insert
    $('form').submit(function (e) {
        e.preventDefault();
        var isValid = $(this).validate().form();
        if (!isValid) return false;
        else {
            $.ajax({
                url: '/Appointment/RejectAppointment',
                data: $(this).serialize(),
                type: 'POST',
                beforeSend: function () {
                },
                complete: function () {
                },
                success: function (res) {
                    if (res.Code == "1") {
                        toastr.success(res.Message, 'success!');
                        _AppointmentDetail();
                        $('#reject_appointment').modal('hide');
                    }
                    else {
                        toastr.error(res.Message, 'error!');
                    }
                },
                error: function (jqXHR, error, errorThrown) {
                    toastr.error('Sorry Try again', 'error!');
                },
            });
        }
    });
});


function _AddEditAppointment(id) {
    $.ajax({
        type: "POST",
        url: "/Appointment/AddEditAppointment",
        data: { Id: id },
        success: function (res) {
            $('#_AppointmentPartialView').empty().html(res);
        },
        error: function (res) {
            alert(res);
        }
    });
}

function _AppointmentDetail() {
    $.ajax({
        type: "Get",
        url: "/Appointment/Details",
        success: function (res) {
            $('#_AppointmentPartialView').empty().html(res);
            $("#example").dataTable();
        },
        error: function (res) {
            alert(res);
        }
    });
}


function RejectAppointment(id) {
    $.ajax({
        type: "POST",
        url: "/Appointment/RejectAppointmentPartialView",
        data: { Id: id },
        success: function (res) {
            $('#_rejectappointmentmodel').empty().html(res);
        },
        error: function (res) {
            alert(res);
        }
    });
}

function AttendAppointment(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Accept it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "Post",
                url: "/Appointment/AttendAppointment",
                data: { Id: id },
                success: function (res) {
                    if (res.Code == "1") {
                        toastr.success(res.Message, 'success!')
                        _AppointmentDetail();
                    }
                    else {
                        toastr.error(res.Message, 'error!')
                    }
                },
                error: function (res) {
                    alert(res);
                }
            });

        }
    })

}

function AcceptAppointment(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Accept it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "Post",
                url: "/Appointment/AcceptAppointment",
                data: { Id: id },
                success: function (res) {
                    if (res.Code == "1") {
                        toastr.success(res.Message, 'success!')
                        _AppointmentDetail();
                    }
                    else {
                        toastr.error(res.Message, 'error!')
                    }
                },
                error: function (res) {
                    alert(res);
                }
            });

        }
    })

}

function DeleteDoctor(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "Post",
                url: "/Appointment/Delete",
                data: { Id: id },
                success: function (res) {
                    if (res.Code == "1") {
                        toastr.success(res.Message, 'success!')
                        _DoctorDetail();
                    }
                    else {
                        toastr.error(res.Message, 'error!')
                    }
                },
                error: function (res) {
                    alert(res);
                }
            });

        }
    })

}
