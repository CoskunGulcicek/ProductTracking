$(document).ready(function () {

});


function sendCustomer(customerId) {
    document.getElementById('modalCustomerNameTitle').innerHTML = $("#hiddenCustomerName_" + customerId).val();
    $('#modalCustomerId').val(customerId);
};


$('#kisiyeUrunEkle').click(function () {

    var eklenenCustomer = $('#modalCustomerId').val();
    var products = [];
    $("input:checkbox[name=products]:checked").each(function () {
        var val = $(this).val();
        products.push({
            name: val,
            quantity: 0,
            type: 'Ks'
        });
    });

    customerAddBasket(eklenenCustomer, products);

    $('#myModal').find("input:checked").prop('checked', false);
    $('[data-dismiss="modal"]').trigger('click');
});

function customerAddBasket(cusID, products) {


    var customer = data.customers.filter(function (customer) {
        return customer.id == cusID;
    })[0];
    var sendProds = [];
    products.forEach(function (addProd) {
        var hasProducst = false;
        (customer.basket || []).forEach(function (product) {
            hasProducst = !hasProducst && (addProd.name == product.name && addProd.type == product.type);
            if (hasProducst) {
                addProd.quantity = parseInt(addProd.quantity) + parseInt(product.quantity);
            }
        });

        sendProds.push(addProd);
    });

    customer.basket = sendProds;

    render();
}

$(document).on('change', '.inpt-quantity, .inpt-type', function (e) {
    var product = $(e.target).parents('.product');
    
    var cusID = product.parent().data('cusid');
    var name = product.find('label').data('name');
    var type = product.find('select option:checked').val();
    var quantity = product.find('[name=quantityofproduct]').val();

    var customer = data.customers.filter(function (customer) {
        return customer.id == cusID;
    })[0];

    (customer.basket || []).forEach(function (product) {
        if (name == product.name && type == product.type) {
            product.quantity = parseInt(quantity);
        }
    });
    // render();
});

$('#btnAddDealCalculate').click(function () {
    function sendCalculationData(data) {

        $.ajax({
            type: 'POST',
            url: '/Home/Add',
            data: JSON.stringify(data),
            success: function (data) {
                document.getElementById('totalDiv').innerHTML = '';
                for (var i = 0; i < data.length; i++) {
                    //data[i].name
                    //data[i].type
                    //data[i].quantity

                    var alanlar = "<div class='col-sm-2  border-top border-bottom border-left'><strong>" + data[i].name + "</strong></div> <div class='col-sm-1 border-top border-bottom'><strong>" + data[i].type + "</strong></div> <div class='col-sm-1 border-top border-bottom border-right'><strong>" + data[i].quantity + "</strong></div>";
                    $("#totalDiv").append(alanlar);
                }
            },
            contentType: 'application/json',
            error: function () {
                $('#saveModal').modal('hide');
                alertify.warning('an access problem');
            }
        });
    }

    //row_@i
    //row_@i
    //productCount
    //customerCount
    //product.Name + "_" + j

    var calculationData = [];
    var pCount = $('#productCount').val();
    var cCount = $('#customerCount').val();

    if (pCount > 0 && cCount > 0) {

        //data.customers.forEach(function (customer) {
        //    (customer.basket || []).forEach(function (item) {
        //        calculationData.push(item);
        //    });
        //});

        $('.product').each(function () {
            var product = $(this);

            var name = product.find('label').data('name');
            var type = product.find('select option:checked').val();
            var quantity = product.find('[name=quantityofproduct]').val();

            calculationData.push({
                name: name,
                type: type,
                quantity: quantity
            });
        });

    } else {
        console.log("ürün yok");
    }

    sendCalculationData(calculationData);

});

var templateHTML = $('#data-template').html();
var container = $('#content-pane');
var template;
$(function () {
    template = Handlebars.compile(templateHTML);
    render();
});

function render() {
    container.empty();
    container.append(template(data));
}


