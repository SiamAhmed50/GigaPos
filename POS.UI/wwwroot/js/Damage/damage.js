var dataTable;
$(document).ready(function () {
    loadDataTable();
});


$("#ProductList").on("change", function () {
    debugger
    $(".damage-qty").show();
    var sd = $(this).text().replace(/[^0-9]/gi, '');
    var qty = parseInt(sd, 10);
   
    $("#qty").val(qty);
});


function loadDataTable() {
    dataTable = $('#DamageTbl').DataTable({
        "autoWidth": true,
        "ajax": {
            "url": "/Damages/GetAll"
        },
        "columns": [
            { "data": "id"},
            { "data": "createdDate" },
            { "data": "productId" },
            { "data": "quantity" },
            {
                "data": "id",
                "width": "40%",
                "render": function (data, type, full, meta) {
                    return '<div class="dropdown action-button mx-2">'
                        + '<button class="btn btn-success btn-sm dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false"><i class="fa-solid fa-gears"></i> Manage</button>'
                        + '<ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">'
                        + '<li><a class="dropdown-item btn btn-primary" onClick="Delete(' + data + ')"><i class="bi bi-trash-fill"> </i> Delete</a> </li> </ul> </div>'
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
                url: "/Damages/Delete/" + id,
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