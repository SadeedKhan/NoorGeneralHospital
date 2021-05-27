$(document).ready(function () {
    _locationDetail();

});
//function ClearValues() {
//    $('form#AddVisitorsForm').trigger("reset");
//    $('.select2').val("").trigger('change .select2');
//    $('.datepicker').datepicker('setDate', 'now');
//}

function _AddEditLocation(id) {
    $.ajax({
        type: "POST",
        url: "/Location/AddEditLocation",
        data: { Id: id },
        success: function (res) {
            console.log(res);
            $('#_locationPartialView').empty().html(res);
        },
        error: function (res) {
            alert(res);
        }
    });
}

function _locationDetail() {
    $.ajax({
        type: "Get",
        url: "/Location/Details",
        success: function (res) {
            $('#_locationPartialView').empty().html(res);
            $("#example").dataTable();
        },
        error: function (res) {
            alert(res);
        }
    });
}


//function DeleteLocation(id) {
//    Swal.fire({
//        title: 'Are you sure?',
//        text: "You won't be able to revert this!",
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Yes, delete it!'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                type: "Post",
//                url: "/Location/Delete",
//                data: { Id: id },
//                success: function (res) {
//                    if (res.Code == "1") {
//                        toastr.success(res.Message, 'success!')
//                        _locationDetail();
//                    }
//                    else {
//                        toastr.error(res.Message, 'error!')
//                    }
//                },
//                error: function (res) {
//                    alert(res);
//                }
//            });

//        }
//    })

//}
