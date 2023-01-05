

//Add POS
var counter = 0;
var index = 0;
var productPrice = 0;
var product = null;
var productVm = null;





$("#ProductList").on("change", function () {

    debugger

    var productId = $("#ProductList option:selected").val();
    var productName = $("#ProductList option:selected").text();
    counter++;
    $.ajax({

        url: "/Products/GetById/" + productId,
        method: "GET",
        type: "json",
        async: false,

        success: function (data) {
            debugger;
            productVm = data.data;
            product = data.data.product;
            productPrice = product.salePrice;

        }
    });
    debugger;
    var singleUnit = '<td class="pr-3"> <div class="form-row"><label class="ml-4 mr-2">###unitname###:</label>' +
        '<input type = "number" value = "0" class="form-control col main_qty" name ="" data-relatedby="' + product.unit.relatedBy + '" onkeyup = "CalculateSubTotal()"></div> </td>';

    var doubleUnit = '<td class="pr-3"> <div class="form-row"><label class="ml-4 mr-2">###unitname###:</label>' +
        '<input type = "number" value = "0" class="form-control col main_qty" name ="" data-relatedby="' + product.unit.relatedBy + '" onkeyup = "CalculateSubTotal()">' +
        '<label class="ml-4 mr-2">###subunitname###:</label><input type = "number" value = "0" class="form-control col sub_qty" name = "" onkeyup = "CalculateSubTotal()"> </div> </td>';
    if (productVm.subUnit != null) {
        doubleUnit = doubleUnit.replace("###unitname###", product.unit.name).replace("###subunitname###", productVm.subUnit.name);
    }
    else {
        singleUnit = singleUnit.replace("###unitname###", product.unit.name);
    }

    var productRow = 
        '<tr> <td> <span class="productName">' + productName + '</span> <input type = "hidden" value = "' + productId + '" class="ProductId" name = "Purchase.PurchaseItems[' + index + '].ProductId" class="" ></td>' +
        '###quantityinput### <input type="hidden" class="quantity" name="Purchase.PurchaseItems[' + index + '].Quantity" />' +
        '<td style="width:150px"><input type="text" value="' + productPrice + '" class="form-control rate" name="Purchase.PurchaseItems[' + index + '].Price" onkeyup="CalculateSubTotal()"></td>' +
       
        '<td> <strong><span class="sub_total">0</span> Tk</strong> <input type="hidden" name="Purchase.PurchaseItems[' + index + '].SubTotal" class="subtotal_input" value="0"></td>' +
        '<td> <a onclick="RemoveProduct(this)" class="removeProduct">  <i class="fa fa-trash"></i> </a> </td>  </tr> ';

    if (productVm.subUnit != null) {
        productRow = productRow.replace("###quantityinput###", doubleUnit);
    }
    else {
        productRow = productRow.replace("###quantityinput###", singleUnit);
    }


    $("#SellProduct tbody").append(productRow);
    index++;
    $("#ProductList option:selected").remove();

});

function CalculateSubTotal() {

    var rows = $("#SellProduct tbody tr");
    rows.each(function () {
       
        var main_qty = 0;
        var sub_qty = 0;
        var tot_qty = 0;
        var relatedBy = 0;
        var price = parseFloat($(this).find(".rate").val()).toFixed(2);
        main_qty = parseInt($(this).find(".main_qty").val());
        relatedBy = parseInt($(this).find(".main_qty").attr("data-relatedby"));
        if ($(this).find(".sub_qty").length > 0) {
            sub_qty = parseInt($(this).find(".sub_qty").val());

            $(this).find(".subtotal_input").val(parseFloat((price * main_qty) + ((price / relatedBy) * sub_qty)).toFixed(2));
            tot_qty = (main_qty * relatedBy) + sub_qty;
        }
        else {

            $(this).find(".subtotal_input").val(price * main_qty);
            tot_qty = (main_qty * relatedBy) + sub_qty;
        }

        $(this).find(".quantity").val(tot_qty);

        $(this).find(".sub_total").html($(this).find(".subtotal_input").val());

    });

    CalculateGrandTotal();

}

function CalculateGrandTotal() {

    var rows = $("#SellProduct tbody tr");
    var grandTotal = 0;
    rows.each(function () {
       

        grandTotal += parseFloat($(this).find(".subtotal_input").val());

    });

    $("#PurchaseGrandTotal").val(grandTotal);
    $("#grandTotalTxt").html($("#PurchaseGrandTotal").val());

}
function CalculateTotalQty() {
    debugger
    var rows = $("#SellProduct tbody tr");
    var TotalQty = 0;
    rows.each(function () {
        debugger;

        TotalQty += parseFloat($(this).find(".quantity").val());

    });

    $("#sellTotalQty").val(grandTotal);
    $("#TotalQtyTxt").html($("#sellTotalQty").val());

}

function RemoveProduct(elem) {
    debugger;
    var row = $(elem).parent().parent();
    var productName = row.find(".productName").text();
    var productId = row.find(".ProductId").val();
    var data = {
        id: productId,
        text: productName
    };

    var newOption = new Option(data.text, data.id, false, false);
    row.remove();
    $("#ProductList").append(newOption);
}


$(document).ready(function () {
    debugger;

    getProducts();
    //Pagination JS
    //how much items per page to show
    var show_per_page = 9;
    //getting the amount of elements inside pagingBox div
    var number_of_items = $('#pagingBox').children().length;
    //calculate the number of pages we are going to have
    var number_of_pages = Math.ceil(number_of_items / show_per_page);

    //set the value of our hidden input fields
    $('#current_page').val(0);
    $('#show_per_page').val(show_per_page);

    var navigation_html = '<li class="previous_link page-item " onclick="previous()" aria-label="« Previous"><span class="page-link" aria-hidden="true" >‹</span></li >';
    var current_link = 0;
    while (number_of_pages > current_link) {

        navigation_html += '<li class="page-item page_link" onclick="go_to_page(' + current_link + ')" aria-current="page" longdesc="' + current_link + '">' +
            '<span class="page-link" > ' + (current_link + 1) + '</span ></li > ';
        current_link++;
    }

    navigation_html += '<li class="page-item" ><a class="page-link" href="javascript:next()" rel="next" aria-label="Next »" >›</a ></li >';

    $('#page_navigation').html(navigation_html);

    //add active_page class to the first page link
    $('#page_navigation .page_link:first').addClass('active_page');

    //hide all the elements inside pagingBox div
    $('#pagingBox').children().css('display', 'none');

    //and show the first n (show_per_page) elements
    $('#pagingBox').children().slice(0, show_per_page).css('display', 'block');


  

});



//Pagination JS

function previous() {

    new_page = parseInt($('#current_page').val()) - 1;
    //if there is an item before the current active link run the function
    if ($('.active_page').prev('.page_link').length == true) {
        go_to_page(new_page);
    }

}

function next() {
    new_page = parseInt($('#current_page').val()) + 1;
    //if there is an item after the current active link run the function
    if ($('.active_page').next('.page_link').length == true) {
        go_to_page(new_page);
    }

}
function go_to_page(page_num) {
    debugger;
    //get the number of items shown per page
    var show_per_page = parseInt($('#show_per_page').val());

    //get the element number where to start the slice from
    start_from = page_num * show_per_page;

    //get the element number where to end the slice
    end_on = start_from + show_per_page;

    //hide all children elements of pagingBox div, get specific items and show them
    $('#pagingBox').children().css('display', 'none').slice(start_from, end_on).css('display', 'block');

    /*get the page link that has longdesc attribute of the current page and add active_page class to it
    and remove that class from previously active page link*/
    $('.page_link[longdesc=' + page_num + ']').addClass('active_page').siblings('.active_page').removeClass('active_page');

    //update the current page input field
    $('#current_page').val(page_num);
}

function getProducts(searchFilter, categoryId) {
    debugger;

  


    $.ajax({

        url: "/Pos/GetProducts?searchFilter=" + searchFilter + "&categoryId=" + categoryId,
        method: "GET",
        type: "json",
        async: false,

        success: function (data) {
            debugger;
            products = data.data;
            $("#pagingBox").html("");
            for (var i = 0; i < products.length; i++) {
                var productDiv = '<div class="col-sm-4 col-md-4"><div class=" product text-center m-2 product" ><img src="###imgurl###" class="align-self-start img-thumbnail" alt="###productnamealt###" style="width:80px" data-pagespeed-url-hash="31082695"><br><span>###productname### - ###productcode###</span> <br> <small class="font-weight-bold">###productprice###</small> Tk<br><small class="stock">Stock : ###stock### pc</small> </div></div>';

                productDiv = productDiv.replace("###imgurl###", products[i].imageUrl).replace("###productnamealt###", products[i].name).replace("###productname###", products[i].name).replace("###productcode###", products[i].code).replace("###productprice###", products[i].salePrice).replace("###stock###", products[i].stock);
                $("#pagingBox").append(productDiv);
            }

        }


    });

}

function searchProduct() {
    debugger;
    var filter = $("#searchInput").val();
    getProducts(filter, null);

}

function getAllProduct() {
   
    var product = null;
    $.ajax({

        url: "/Products/GetAll/",
        method: "GET",
        type: "json",
        async: false,

        success: function (data) {
           
            product = data.data;

        }

    });

    return product;

}