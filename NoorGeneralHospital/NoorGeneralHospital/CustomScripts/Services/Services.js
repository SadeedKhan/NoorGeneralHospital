$(document).ready(function () {
    _ServicesDetail();
});
//function ClearValues() {
//    $('form#AddVisitorsForm').trigger("reset");
//    $('.select2').val("").trigger('change .select2');
//    $('.datepicker').datepicker('setDate', 'now');
//}

function _AddEditServices(id) {
    $.ajax({
        type: "POST",
        url: "/Services/AddEditServices",
        data: { Id: id },
        success: function (res) {
            $('#_ServicesPartialView').empty().html(res);
            $("#ServiceTable").dataTable();
        },
        error: function (res) {
            alert(res);
        }
    });
}

function _ServicesDetail() {
    $.ajax({
        type: "Get",
        url: "/Services/Details",
        success: function (res) {
            $('#_ServicesPartialView').empty().html(res);
            $("#ServiceTable").dataTable();
        },
        error: function (res) {
            alert(res);
        }
    });
}


function DeleteServices(id) {
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
                url: "/Services/Delete",
                data: { Id: id },
                success: function (res) {
                    if (res.Code == "1") {
                        toastr.success(res.Message, 'success!')
                        _ServicesDetail();
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
