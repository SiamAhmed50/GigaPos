var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#brandTbl').DataTable({
        "ajax": {
            "url": "/Brands/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "description", "width": "15%" },
            
            {
                "data": "logoUrl",
                "render": function (data, type, full, meta) {

                    return '<img class="table-logo-image" src="'+data+'" />'
                    
                }
            },
            {
                "title": "Count Products",
                "render": function (data, type, full, meta) {

                    return ' ';
                }
            },
           
            {
                "data": "id",
                "render": function (data, type, full, meta) {

                    return '<div class="dropdown action-button mx-2">'
                        + '<button class="btn btn-success btn-sm dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false"><i class="fa-solid fa-gears"></i> Manage</button>'
                        + '<ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">'
                        + '<li><a  href="/Brands/Upsert?Id=' + data + '" class="dropdown-item btn btn-primary"><i class="bi bi-pencil-square"></i> Edit </a></li>'
                        + '<li><a class="dropdown-item btn btn-primary" onClick="Delete(' + data + ')"><i class="bi bi-trash-fill"> </i> Delete</a> </li> </ul> </div>'
                }
            },

        ],

        

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
                url: "/Brands/Delete/" + id,
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