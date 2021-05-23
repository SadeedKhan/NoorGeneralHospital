$(document).ready(function () {
    _SpecialityDetail();

});
    //function ClearValues() {
    //    $('form#AddVisitorsForm').trigger("reset");
    //    $('.select2').val("").trigger('change .select2');
    //    $('.datepicker').datepicker('setDate', 'now');
    //}

    function _AddEditSpeciality(id) {
        $.ajax({
            type: "POST",
            url: "/Specialities/AddEditSpeciality",
            data: { Id: id },
            success: function (res) {
                console.log(res);
                $('#_SpecialityPartialView').empty().html(res);
            },
            error: function (res) {
                alert(res);
            }
        });
    }

    function _SpecialityDetail() {
        $.ajax({
            type: "Get",
            url: "/Specialities/Details",
            success: function (res) {
                $('#_SpecialityPartialView').empty().html(res);
                $("#example").dataTable();
            },
            error: function (res) {
                alert(res);
            }
        });
    }


function DeleteSpeciality(id) {
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
                url: "/Specialities/Delete",
                data: { Id: id },
                success: function (res) {
                    if (res.Code == "1") {
                        toastr.success(res.Message, 'success!')
                        _SpecialityDetail();
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
