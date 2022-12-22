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
    let labelExistValue = $('#subUnitValue').text();
    var text = $(".unitChange option:selected").text();
    if (labelExistValue=="") {
        $('.unit-count').show();
        $('#subUnitValue').text("1" + text);
    }
    if (labelExistValue != "") {
        $('.unit-count').show();
        $('#subUnitValue').text(labelExistValue + text);
    }
   
     
   
    
   
});

$('.operatorChange').change(function () {

    debugger
    let labelExistValue = $('#subUnitValue').text();
  /*  let existVal = labelValue.val();*/
    var operatorValue = $(this).val();

    if (labelExistValue == "")
    {
        
        $('.unit-count').show();
        $('#subUnitValue').text("1Select Unit" + operatorValue);
    }

    if (labelExistValue != "") {
        if (operatorValue=="") {
            $('.unit-count').show();
            $('#subUnitValue').text("1+"+labelExistValue + operatorValue);
        }
        if (operatorValue != "") {
            $('.unit-count').show();
            $('#subUnitValue').text("1+" + labelExistValue + operatorValue);
        }
       
    }



    //if (operatorValue =="") {
       
    //    if (labelExistValue=="") {
    //        $('.unit-count').show();

    //        $('#subUnitValue').text("1Select Unit");
    //    }
    //    if (labelExistValue != "") {
    //        $('.unit-count').show();

    //        $('#subUnitValue').text("1" + labelExistValue);
    //    }
    //}
    //if (operatorValue != "") {
       

    //    if (labelExistValue == "") {
    //        $('.unit-count').show();
    //        $('#subUnitValue').text("1Select Unit" + operatorValue);
    //    }
    //    if (labelExistValue != "") {
    //        $('.unit-count').show();

    //        $('#subUnitValue').text("1" + labelExistValue + operatorValue);
    //    }
    //}
     
   
    
});