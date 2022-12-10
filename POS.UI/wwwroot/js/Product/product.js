
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
            { "data": "name", "width": "15%" },
            { "data": "code", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "unit.name", "width": "15%" },
            { "data": "salePrice", "width": "15%" },
            { "data": "purchaseCost", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {

                    return `
                     <div class="w-15 btn-group align-content-center" role="group">
                        <a href="/Products/Upsert?id=${data}" class="btn btn-primary mx-1">
                            <i class="bi bi-pencil-square"></i>&nbsp; Edit</a>

                        <a class="btn btn-danger mx-1"><i class="bi bi-trash-fill"></i>&nbsp; Delete</a>
                        </div>
                            `
                },
                "width": "15%"
            },
        ]
    });
}


//<div class="w-15 btn-group align-content-center" role="group">
//    <a href="/Admin/Proucts/Upsert?id=${data}" class="btn btn-primary mx-1"><i class="bi bi-pencil-square"></i>&nbsp; Edit</a>
//    <a class="btn btn-danger mx-1"><i class="bi bi-trash-fill"></i>&nbsp; Delete</a>
//</div>