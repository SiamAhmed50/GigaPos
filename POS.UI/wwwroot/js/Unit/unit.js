var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#unitTBL').DataTable({
        "ajax": {
            "url": "/Units/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "relatedBy", "width": "15%" },
            { "data": "relatedSign", "width": "15%" },
            
            {
                "data": "id",
                "render": function (data, type, full, meta) {
                    return '<div class="w-75 btn-group" role = "group">'
                        + '<a href="/Units/Upsert?Id=' + data + '" class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i> Edit</a >' +
                        '<a onClick="Delete(' + data + ')" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a></div > '
                }
            },

        ]

    });
}

function Delete(id) {
    debugger;
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
                url: "/Units/Delete/" + id,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}

$(".unitChange").change(function () {
    debugger
    let Unitvalue = $(this).text();
    var text = $(".unitChange option:selected").text();
    $('.unit-count').show();
    $('#unitValue').text(text);




});

$('.opratorChange').change(function () {
    debugger
    let existOperatorValue = $('#operatorValue').text();
    let existUnitValue = $('#unitValue').text();
    if (existUnitValue == "") {
        let opratorVal = $(this).val();
        if (opratorVal == "") {
            $('.unit-count').show();
            $('#operatorValue').text("1select Unit");

        }
        if (opratorVal = "*") {
            $('.unit-count').show();
            $('#operatorValue').text("1select Unit*")
        }

    }
    if (existUnitValue != "") {

        let opratorVal = $(this).val();
        if (opratorVal == "") {
            $('.unit-count').show();
            $('#operatorValue').text("")
        }
        if (opratorVal == "*") {
            $('#existUnitValue').text("")
            $('.unit-count').show();
            $('#operatorValue').text("*")
        }
    }
});

$('.relatedValue').keyup(function () {
    alert('sdhfshdf');
})