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
            {
                "title": "Related Unit Name",
                "render": function (data, type, full, meta) {
                    
                    var relatedUnit = full['relatedUnit'];
                    if (relatedUnit != null) {
                        var relatedUnitName = relatedUnit.name;
                        return '<span>'   + relatedUnitName + '</span > '
                    }
                    else {
                        return '';
                    }



                    
                }
            },
            { "data": "operator", "width": "15%" },
            { "data": "relatedBy", "width": "15%" },

            {
                "title":"Result",
                "render": function (data, type, full, meta) {
                    debugger;
                    var relatedUnit = full['relatedUnit'];
                    if (relatedUnit != null) {
                        var relatedUnitName = relatedUnit.name;
                        return '<span>' + full['name'] + ' = ' + +'1 ' + relatedUnitName + ' ' + full['operator'] + ' ' + full['relatedBy'] + '</span > '
                    }
                    else {
                        return '';
                    }
                    
                   

                    return 'Test';
                }
            },

            {
                "data": "id",
                "render": function (data, type, full, meta) {
                    return '<div class="dropdown action-button mx-2">'
                        + '<button class="btn btn-success btn-sm dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false"><i class="fa-solid fa-gears"></i> Manage</button>'
                        + '<ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">'
                        + '<li><a  href="/Units/Upsert?Id=' + data + '" class="dropdown-item btn btn-primary"><i class="bi bi-pencil-square"></i> Edit </a></li>'
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