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

                    return '<img src="'+data+'" width="40%" height="50px"/>'
                    
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

                    return '<div class="w-75 btn-group" role = "group">'
                        + '<a href="/Brands/Upsert?Id=' + data + '" class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i> Edit</a >' +
                        '<a onClick="Delete(' + data + ')" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a></div > '
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