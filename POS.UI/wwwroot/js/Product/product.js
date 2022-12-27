
let dataTable;
$(document).ready(function () {

    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#productTBL').DataTable({

        "ajax": {
            "url": "/Products/GetAll"
        },
        "columns": [
            {
                "data": "imageUrl",
                "render": function (data, type, full, meta) {

                    return '<img src="'+data+'" width="100%" height="50px"/>'
                }
            },
            { "data": "code", "width": "15%" },
            { "data": "name", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "brand.name", "width": "15%" },
          
            { "data": "salePrice", "width": "15%" },
            { "data": "purchaseCost", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {

                    return '<div class="w-75 btn-group" role = "group">'
                        + '<a href="/Products/Upsert?Id=' + data + '" class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i> Edit</a >' +
                        '<a onClick="Delete(' + data + ')" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a></div >' +
                        '<img src="/Products/GenerateBarcode?productId='+data+'" width="100%" height="50px"/>';
                },
                "width": "15%"
            },
        ]

        //createdRow: function (row, data, index) {
        //    debugger;
        //    $.ajax({
        //        url: "/Products/GenerateBarcode",
        //        success: function (data) {
        //            debugger;
        //            $('td', row).eq(0).find("img").attr("src",data);

        //        }
        //    })
        //},
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
                url: "/Products/Delete/" + id,
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


//<div class="w-15 btn-group align-content-center" role="group">
//    <a href="/Admin/Proucts/Upsert?id=${data}" class="btn btn-primary mx-1"><i class="bi bi-pencil-square"></i>&nbsp; Edit</a>
//    <a class="btn btn-danger mx-1"><i class="bi bi-trash-fill"></i>&nbsp; Delete</a>
//</div>