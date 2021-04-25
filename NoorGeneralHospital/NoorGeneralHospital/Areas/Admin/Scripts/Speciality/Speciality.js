function _Main() {
    _SpecialityDetail(0);

    $("form").submit(function (e) {
        e.preventDefault();
        var Valid = $(this).validate().form();
        if (Valid) {
            try {
                $.ajax({
                    type: "POST",
                    url: "/Speciality/SaveUpdateSpeciality",
                    data: $('form').serialize(),
                    beforeSend: function () {
                    },
                    complete: function () {
                        ClearValues();
                        StopLoader();
                    },
                    success: function (data) {
                        if (res.code == "1") {
                            ClearValues();
                            toastr.success(res.message, 'success!');
                        }
                        else
                        {
                            toastr.error(res.message, 'error!');
                        }
                    },
                    error: function (jqXHR, error, errorThrown) {
                        toastr.error('Sorry Try again', 'error!');
                    },
                });
            }
            catch (error) {
                toastr.info("there is no record found of this type!", "info");
            }
        }
    });

    function ClearValues() {
        $('form#AddVisitorsForm').trigger("reset");
        $('.select2').val("").trigger('change .select2');
        $('.datepicker').datepicker('setDate', 'now');
    }

    function _AddEditSpeciality(id) {
        $.ajax({
            type: "Get",
            url: "/Speciality/_AddEditSpeciality",
            data: { Id: id },
            success: function (res) {
                console.log(res);
                $('#_AddEditSpeciality').empty().html(res);
            },
            error: function (res) {
                alert(res);
            }
        });
    }

    function _SpecialityDetail(id) {
        $.ajax({
            type: "Get",
            url: "/Speciality/_SpecialityDetail",
            data: { Id: id },
            success: function (res) {
                console.log(res);
                $('#_SpecialityDetail').empty().html(res);
            },
            error: function (res) {
                alert(res);
            }
        });
    }
   
}
_Main();