


var counter = 0;
var index = 0;
var productPrice = 0;
$("#ProductList").on("change", function () {
    debugger;
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
            product = data.data;
            productPrice = product.salePrice;

        } 
    });
    var productRow = '<tr><td>' + counter + '</td>'+
        '<td> <span class="productName">' + productName + '</span> <input type = "hidden" value = "' + productId + '" class="ProductId" name = "Purchase.PurchaseItems[' + index + '].ProductId" class="" ></td>' +
        '<td style="width:150px"><input type="text" value="' + productPrice+'" class="form-control rate" name="Purchase.PurchaseItems[' + index + '].Price" onkeyup="CalculateSubTotal()"></td>' +
        '<td class="pr-3"> <div class="form-row"><label class="ml-4 mr-2">pc:</label>'+
        '<input type = "number" value = "0" class="form-control col main_qty" name = "Purchase.PurchaseItems[' + index + '].Quantity" onkeyup = "CalculateSubTotal()"></div> </td>' +
        '<td> <strong><span class="sub_total">0</span> Tk</strong> <input type="hidden" name="Purchase.PurchaseItems[' + index + '].SubTotal" class="subtotal_input" value="0"></td>' +
        '<td> <a onclick="RemoveProduct(this)" class="removeProduct">  <i class="fa fa-trash"></i> </a> </td>  </tr> ';

    $("#purchaseProduct tbody").append(productRow);
    index++;
    $("#ProductList option:selected").remove();

});


function CalculateSubTotal() {
    debugger;
    var rows = $("#purchaseProduct tbody tr");
    rows.each(function () {
        debugger;
        var price = $(this).find(".rate").val();
        var qty = $(this).find(".main_qty").val();
        $(this).find(".subtotal_input").val(price * qty);
        $(this).find(".sub_total").html($(this).find(".subtotal_input").val());

    });

    CalculateGrandTotal();

}

function CalculateGrandTotal() {
    debugger;
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
  

