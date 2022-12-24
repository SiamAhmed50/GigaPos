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
            { "data": "relatedUnit.name", "width": "15%" },
            { "data": "operator", "width": "15%" },
            { "data": "relatedBy", "width": "15%" },

            {
                "title":"Result",
                "render": function (data, type, row, meta) {
                   // return '<span>' + row.name + ' = ' + +'1 ' + row.relatedUnit.name + ' ' + row.operator + ' ' + row.relatedBy+ '</span > '

                    return 'Test';
                }
            },

            {
                "data": "id",
                "render": function (data, type,row, full, meta) {
                    return '<div class="w-75 btn-group" role = "group">'
                        + '<a href="/Units/Upsert?Id=' + row.id + '" class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i> Edit</a >' +
                        '<a onClick="Delete(' + row.id + ')" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a></div > '
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

$("#Unit_RelatedUnitId").change(function () {
    debugger;
    UnitCount();

});

$('#Unit_Operator').change(function () {
    debugger;
    UnitCount();
});
$('#Unit_RelatedBy').keyup(function () {
    debugger;
    UnitCount();
});
function UnitCount() {
    debugger;
    var unitName = $("#Unit_Name").val();
    var relatedBy = $("#Unit_RelatedBy").val();
    var relatedUnit = $("#Unit_RelatedUnitId option:selected").text();
    var relatedUnitId = $("#Unit_RelatedUnitId option:selected").val();
    var operator = $("#Unit_Operator option:selected").val();
    $('.unit-count label').html("1 " + unitName + "=" + (relatedUnitId != "" ? relatedUnit : "") + " " + operator + " " + (relatedBy > 0 ? relatedBy : ""));
    $('.unit-count').show();

}