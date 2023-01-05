


var counter = 0;
var index = 0;
var productPrice = 0;
var product = null;
var productVm = null;
$("#ProductList").on("change", function () {
   
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

    var productRow = '<tr><td>' + counter + '</td>'+
        '<td> <span class="productName">' + productName + '</span> <input type = "hidden" value = "' + productId + '" class="ProductId" name = "Purchase.PurchaseItems[' + index + '].ProductId" class="" ></td>' +
        '<td style="width:150px"><input type="text" value="' + productPrice+'" class="form-control rate" name="Purchase.PurchaseItems[' + index + '].Price" onkeyup="CalculateSubTotal()"></td>' +
        '###quantityinput### <input type="hidden" class="quantity" name="Purchase.PurchaseItems[' + index + '].Quantity" />' +
        '<td> <strong><span class="sub_total">0</span> Tk</strong> <input type="hidden" name="Purchase.PurchaseItems[' + index + '].SubTotal" class="subtotal_input" value="0"></td>' +
        '<td> <a onclick="RemoveProduct(this)" class="removeProduct">  <i class="fa fa-trash"></i> </a> </td>  </tr> ';

    if (productVm.subUnit != null) {
        productRow = productRow.replace("###quantityinput###", doubleUnit);
    }
    else {
        productRow = productRow.replace("###quantityinput###", singleUnit);
    }


    $("#purchaseProduct tbody").append(productRow);
    index++;
    $("#ProductList option:selected").remove();

});


function CalculateSubTotal() {
 
    var rows = $("#purchaseProduct tbody tr");
    rows.each(function () {
        debugger;
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
  
    var rows = $("#purchaseProduct tbody tr");
    var grandTotal = 0;
    rows.each(function () {
        debugger;

        grandTotal += parseFloat($(this).find(".subtotal_input").val());
        
    });

    $("#PurchaseGrandTotal").val(grandTotal);
    $("#grandTotalTxt").html($("#PurchaseGrandTotal").val());

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

$("#paymentForm").on("shown.bs.modal", function () {
    debugger;
    var grandTotal = $("#PurchaseGrandTotal").val();
    $("#totalPayable").html(grandTotal);
    $("#due_txt").html(grandTotal);
    $("#totalPayable_input").val(grandTotal);
    $("#due_input").val(grandTotal);


})


function SetPaidAmount() {
    debugger

    var grandTotal = $("#PurchaseGrandTotal").val();
    $("#pay_amount").val(grandTotal);
    $("#due_txt").html("0");
    $("#due_input").val(0);
}

$("#pay_amount").keyup(function () {
    debugger;
    var grandTotal = $("#PurchaseGrandTotal").val();
    var payAmount = $("#pay_amount").val();
    var due = parseFloat(grandTotal - payAmount);
    $("#due_txt").html(due);
    $("#due_input").val(due);

})



function SaveSupplier() {

    debugger;
    let name = $("#supplierName").val();
    let email = $("#supplierEmail").val();
    let phone = $("#supplierPhone").val();
    let address = $("#supplierAddress").val();
    let openeingReceivable = $("#openeingReceivable").val();
    let openeingPayable = $("#openeingPayable").val();

    if ($("#supplierForm").valid()) {

        var formData = $("#supplierForm").serialize();
        $.ajax({
            url: "/Suppliers/Upsert",
            type: "POST",
            data: formData,
            success: function (response) {
                debugger
                location.reload(true);
            },
            error: function (request, status, error) {
                alert(request.responseText);
            }
        });



    }

}