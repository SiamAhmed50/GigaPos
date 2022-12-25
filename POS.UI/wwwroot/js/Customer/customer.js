var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#CustomerTbl').DataTable({
        "autoWidth": true,
        "ajax": {

            "url": "/Customers/GetAll"
        },
        "columns": [
            { "data": "1", "width": "30%" },
            { "data": "name", "width": "30%" },
            { "data": "email", "width": "30%" },
            { "data": "phone", "width": "30%" },
            { "data": "address", "width": "30%" },
            { "data": "openingReceivable", "width": "30%" },
            { "data": "openingPayable", "width": "30%" },
            { "data": "", "width": "30%" },
            { "data": "", "width": "30%" },
            { "data": "", "width": "30%" },
           
            //{
            //    "data": "id",
            //    "width": "40%",
            //    "render": function (data, type, full, meta) {
            //        return '<div class=" btn-group" role = "group">'
            //            + '<a href="/Categories/Edit?Id=' + data + '" class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i> Edit</a >' +
            //            '<a onClick="Delete(' + data + ')" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a></div > '
            //    }
            //},

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
                url: "/Customers/Delete/" + id,
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