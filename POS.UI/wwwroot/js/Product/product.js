
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

                    return '<img src="'+data+'" width="50%" height="30px"/>'
                }
            },
            { "data": "code", "width": "10%" },
            { "data": "name", "width": "10%" },
            { "data": "category.name", "width": "10%" },
            { "data": "brand.name", "width": "15%" },
          
            { "data": "salePrice", "width": "10%" },
            { "data": "purchaseCost", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {

                    return '<div class="button-group">'
                        + '<div class="getViewProduct">'
                        + '  <buttontype="button" onClick="getproductDetails(' + data + ')"  class="btn btn-secondary btn-sm" data-bs-toggle="modal" data-bs-target="#productsDetails"><i class="fa-solid fa-eye"></i></button> </div>'

                        + '<div class="dropdown action-button mx-2">'
                        + '<button class="btn btn-success btn-sm dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false"><i class="fa-solid fa-gears"></i> Manage</button>'
                        + '<ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">'
                        + '<li><a  href="/Products/Upsert?Id=' + data + '" class="dropdown-item btn btn-primary"><i class="bi bi-pencil-square"></i> Edit </a></li>'
                        + '<li><a class="dropdown-item btn btn-primary" onClick="Delete(' + data + ')"><i class="bi bi-trash-fill"> </i> Delete</a> </li> </ul> </div>'

                    +'<div class="barcode-box">'
                        + '  <buttontype="button" onClick="getBarCode(' + data +')"   class="btn btn-secondary  btn-sm btn-barCode" data-bs-toggle="modal" data-bs-target="#barCode"><i class="fa-solid fa-barcode"></i></button> </div>'
                        

                      
                      


                       
                },
                "width": "110%"
            },
        ]

        
    });
}









function Delete(id) {
    
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


function GetProductById(id) {
   
    var product = null;
    $.ajax({

        url: "/Products/GetById/" + id,
        method: "GET",
        type: "json",
        async: false,

        success: function (data) {
           
            product = data.data;
           
        }


    });
    
    return product;
}

function getproductDetails(id) {

    debugger
    $("#tbl_productModal tbody tr").remove();  
    $("#ProductmodalImage span img").remove();  
    $("#staticBackdropLabel span ").remove();  
    var product = GetProductById(id);
   
          
         
           
            var imagediv = '<img src="' + product.imageUrl + '" class="product-modal-image"/>';
            var title = '<span >' + product.name + ' </span>'
            var rows = "<tr>"
                + "<td>Code</td> "
                + "<td class='prtoducttd'>" + product.code + "</td> </tr >"
                + "<tr>"
                + "<td>Category</td> "
                + "<td class='prtoducttd'>" + product.category.name + "</td> </tr >"
                + "<tr>"
                + "<td>Brand</td> "
                + "<td class='prtoducttd'>" + product.brand.name + "</td> </tr >"
                + "<tr>"
                + "<td> Price</td> "
                + "<td class='prtoducttd'>" + product.salePrice + "</td> </tr >"
                + "<tr>"
                + "<td>Cost</td> "
                + "<td class='prtoducttd'>" + product.purchaseCost + "</td> </tr >"
                + "<td>Stock</td> "
                + "<td class='prtoducttd'> </td> </tr >";
               
               
            $('#tbl_productModal tbody').append(rows); 
            $('#ProductmodalImage span ').append(imagediv); 
            $('#staticBackdropLabel ').append(title); 
        
    
    
}

function getBarCode(id) {
   
   
    var item = GetProductById(id);
   
    $("#barCodeImage  img ").remove(); 

    var Barcode =
         '<img src="/Products/GenerateBarcode?productId=' + id + '" width="100%" height="50px"/>'

    $('#barProductCode').text(item.code);
    $('#barProductTitle').text(item.name);
    $('#barProductPrice').text(item.salePrice+" Tk");

    $('#barCodeImage  ').append(Barcode);

}

function printBarCode(elem) {
    var mywindow = window.open();
    var content = document.getElementById(elem).innerHTML;
    var realContent = document.body.innerHTML;
    mywindow.document.write(content);
    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10*/
    mywindow.print();
    document.body.innerHTML = realContent;
    mywindow.close();
    return true;
}

