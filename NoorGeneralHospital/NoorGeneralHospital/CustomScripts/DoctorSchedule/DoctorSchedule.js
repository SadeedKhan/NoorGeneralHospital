$(document).ready(function () {
    _DoctorScheduleDetail();
});
//function ClearValues() {
//    $('form#AddVisitorsForm').trigger("reset");
//    $('.select2').val("").trigger('change .select2');
//    $('.datepicker').datepicker('setDate', 'now');
//}

function _AddEditDoctorSchedule(id) {
    $.ajax({
        type: "POST",
        url: "/DoctorSchedule/AddEditDoctorSchedule",
        data: { Id: id },
        success: function (res) {
            $('#_DoctorSchedulePartialView').empty().html(res);
        },
        error: function (res) {
            alert(res);
        }
    });
}

function _DoctorScheduleDetail() {
    $.ajax({
        type: "Get",
        url: "/DoctorSchedule/Details",
        success: function (res) {
            $('#_DoctorSchedulePartialView').empty().html(res);
        },
        error: function (res) {
            alert(res);
        }
    });
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
                url: "/DoctorSchedule/Delete",
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
