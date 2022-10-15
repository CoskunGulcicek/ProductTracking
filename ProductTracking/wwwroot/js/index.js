$(document).ready(function () {
    var target = document.getElementById("accordionPanelsStayOpenExample");
    target.innerHTML = "";
    getUsersBylistId();
});


function sendCustomer(customerId) {
    document.getElementById('modalCustomerNameTitle').innerHTML = $("#inlineFormInputGroup_" + customerId).val();
    $('#modalCustomerId').val(customerId);
};

$('#kisiyeUrunEkle').click(function () {

    var eklenenCustomer = $('#modalCustomerId').val();
    var customerproducts = [];
    $("input:checkbox[name=products]:checked").each(function () {
        var pid = $(this).val();
        customerproducts.push({
            ProductId: parseInt(pid),
            CustomerId: parseInt(eklenenCustomer)
        });
    });

    customerAddBasket(customerproducts);
    $('#myModal').find("input:checked").prop('checked', false);
    $('[data-dismiss="modal"]').trigger('click');
});

$('#listeyeCustomerEkle').click(function () {

    var lstId = $('#indexSelectedListId').val();
    var listcustomer = [];
    $("input:checkbox[name=products]:checked").each(function () {
        var cusId = $(this).val();
        listcustomer.push({
            ListId: parseInt(lstId),
            CustomerId: parseInt(cusId)
        });
    });

    ListCustomerAdd(listcustomer);
    $('#listCustomerModal').find("input:checked").prop('checked', false);
    $('[data-dismiss="modal"]').trigger('click');
});

function ListCustomerAdd(listCustomers) {
    $.ajax({
        type: 'POST',
        url: '/ListCustomer/Add',
        data: JSON.stringify(listCustomers),
        success: function (data) {//JSON.stringify(customerproducts),
            location.reload();
        },
        contentType: 'application/json',
        error: function () {
            alert('an access problem');
        }
    });
}



function urunSil(id) {
    $.ajax({
        url: '/CustomerProduct/Delete?id=' + id,
        type: 'POST',
        success: function (response) {
            location.reload();
        },
        error: function () {
            alert("hata");
        }
    });
};


function customerAddBasket(customerproducts) {
    $.ajax({
        type: 'POST',
        url: '/Product/AddProductToCustomers',
        data: JSON.stringify(customerproducts),
        success: function (data) {//JSON.stringify(customerproducts),
            location.reload();
        },
        contentType: 'application/json',
        error: function () {
            alert('an access problem');
        }
    });
}

$('#calculate').click(function () {
    function sendCalculationData(data) {
        $.ajax({
            type: 'POST',
            url: '/Home/Add',
            data: JSON.stringify(data),
            success: function (data) {
                document.getElementById('totalDiv').innerHTML = '';
                for (var i = 0; i < data.length; i++) {
                    /* var alanlar = "<div class='col-sm-2  border-top border-bottom border-left'><strong>" + data[i].name + "</strong></div> <div class='col-sm-1 border-top border-bottom'><strong>" + data[i].type + "</strong></div> <div class='col-sm-1 border-top border-bottom border-right'><strong>" + data[i].quantity + "</strong></div>";*/
                    var alanlar = "<div class='col-sm-2  border-top border-bottom border-left' style='font-size:1.1em;'><strong>" + data[i].name + "</strong></div> <div class='col-sm-1 border-top border-bottom border-right' style='font-size:1.1em;'><strong>" + data[i].quantity + "</strong></div>";
                    $("#totalDiv").append(alanlar);
                }
            },
            contentType: "application/json",
            error: function () {
            }
        });
    }

    var calculationData = [];
    $('#accordionPanelsStayOpenExample .products').each(function () {
        var product = $(this);
        var quantityDoluMu = product.find('[name=quantityofproduct]').val();
        if (quantityDoluMu != "") {
            var name = product.find('[name=productname]').val();
            var type = "kg"//product.find('select option:checked').val();
            var quantity = product.find('[name=quantityofproduct]').val();
            var cusId = product.find('[name=userId]').val();
            var prodId = product.find('[name=productId]').val();
            calculationData.push({
                name: name,
                type: type,
                quantity: quantity,
                cusId: cusId,
                prodId: prodId
            });
        }
    });
    //console.log(calculationData);
    if (calculationData.length > 0) {
        sendCalculationData(calculationData);
    }
});

function getUsersBylistId() {
    totalDiv.innerHTML = "";
    var listId = $('#indexSelectedListId').val();
    var target = document.getElementById("accordionPanelsStayOpenExample");
    target.innerHTML = "";
    $.ajax({
        type: 'GET',
        url: '/Customer/GetByListId?id=' + listId,
        success: function (data) {
            var sayi = 0;
            for (var i = 0; i < data.length; i++) {
                var mergedName = data[i].name + " " + data[i].surName;
                var productLines = "";
                for (var j = 0; j < data[i].products.length; j++) {
                    productLines += `<div class="products col-sm-3">
                                        <div class="row">
                                            <div class="col-sm-8">
                                                          <input type="text" class="form-control" style="font-size:1em;" name="productname" placeholder="ProductName"  value="${data[i].products[j].productName}"  disabled>
                                                          <input type="hidden" name="userId" id="userId" value="${data[i].id}">
                                                          <input type="hidden" name="productId" id="productId" value="${data[i].products[j].productId}">
                                                    </div>
                                                    <div class="col-sm-4 form-inline">
                                                          <input type="text" class="form-control"  style="width:55px;margin-left:-30px;font-size:1em;" name="quantityofproduct" value="${data[i].products[j].quantity}">
                                                          <a class="btn btn-danger btn-sm" style="opacity:0.5;" onclick="javascript:urunSil(${data[i].products[j].id})">x</a>
                                                    </div>
                                            </div>
                                        </div>`;
                }
                target.innerHTML += `
                        <div class="accordion-item">
                                  <a class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseTwo_${i}" aria-expanded="true" aria-controls="panelsStayOpen-collapseTwo_${i}">
                                    ${mergedName}
                                  </a>
                                  <a class="btn btn-warning btn-sm btn_urun_ekle" data-toggle="modal" data-target="#myModal" onclick="javascript:sendCustomer(${data[i].id})" style="font-size:0.8em;">Ürün Ekle</a>
                                  <a class="btn btn-danger btn-sm btn_listeden_sil" onclick="javascript:deleteCustomerFromList(${data[i].id})" style="font-size:0.8em;">Listeden Sil</a>
                                
                                <div id="panelsStayOpen-collapseTwo_${i}" class="accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingTwo" style="">
                                  <div class="accordion-body">
                                    
                                            <div class="row">
                                    ${productLines}
                                    </div>
                                </div>
                         </div>

                                    `;
                productLines = "";
            }
        },
        contentType: 'application/json',
        error: function () {
            console.log('an access problem');
        }
    });
}

$('#ilgiliListeyiBosalt').click(function () {
    var lstId = $('#indexSelectedListId').val();
    $.ajax({
        url: '/List/UrunleriSifirla?id=' + lstId,
        type: 'POST',
        success: function (response) {
            location.reload();
        },
        error: function () {
            alert("hata");
        }
    });
});

function deleteCustomerFromList(id) {
    var lstId = $('#indexSelectedListId').val();
    var listId = parseInt(lstId);
    $.ajax({
        url: '/listCustomer/DeleteFromlist?id=' + id + '&listId=' + listId,
        type: 'POST',
        success: function (response) {
            location.reload();
        },
        error: function () {
            alert("hata");
        }
    });
};