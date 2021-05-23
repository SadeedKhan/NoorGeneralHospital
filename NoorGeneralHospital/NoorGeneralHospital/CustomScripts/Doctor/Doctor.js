$(document).ready(function () {
    _DoctorDetail();
});
    //function ClearValues() {
    //    $('form#AddVisitorsForm').trigger("reset");
    //    $('.select2').val("").trigger('change .select2');
    //    $('.datepicker').datepicker('setDate', 'now');
    //}

function _AddEditDoctor(id) {
        $.ajax({
            type: "POST",
            url: "/Doctor/AddEditDoctor",
            data: { Id: id },
            success: function (res) {
                $('#_DoctorPartialView').empty().html(res);
                $("#example").dataTable();
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

function _DoctorDetail() {
        $.ajax({
            type: "Get",
            url: "/Doctor/Details",
            success: function (res) {
                $('#_DoctorPartialView').empty().html(res);
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
                url: "/Doctor/Delete",
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
