$(document).ready(function () {
    var target = document.getElementById("customersDiv");
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
                    var alanlar = "<div class='col-sm-2  border-top border-bottom border-left'><strong>" + data[i].name + "</strong></div> <div class='col-sm-1 border-top border-bottom'><strong>" + data[i].type + "</strong></div> <div class='col-sm-1 border-top border-bottom border-right'><strong>" + data[i].quantity + "</strong></div>";
                    $("#totalDiv").append(alanlar);
                }
            },
            contentType: "application/json",
            error: function () {
            }
        });
    }

    var calculationData = [];
    $('#customersDiv .products').each(function () {
        var product = $(this);
        var name = product.find('[name=productname]').val();
        var type = product.find('select option:checked').val();
        var quantity = product.find('[name=quantityofproduct]').val();
        calculationData.push({
            name: name,
            type: type,
            quantity: quantity
        });
    });

    //console.log(calculationData);

    sendCalculationData(calculationData);
});

function getUsersBylistId() {
    totalDiv.innerHTML = "";
    var listId = $('#indexSelectedListId').val();
    var target = document.getElementById("customersDiv");
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
                                        <div class="col-sm-4">
                                                      <input type="text" class="form-control" name="productname" placeholder="ProductName" style="width:90px;" value="${data[i].products[j].productName}"  disabled>
                                                </div>
                                                <div class="col-sm-4">
                                                      <select  class="form-select form-control" name="type" style="appearance: none;margin-right:35px;width:45px">
                                                            <option value="kg">kg</option>
								                            <option value="adet">adet</option>
								                            <option value="kasa">kasa</option>
								                            <option value="bund">bund</option>
                                                      </select>
                                                </div>
                                                <div class="col-sm-4 form-inline">
                                                      <input type="text" class="form-control"  style="width:55px;margin-left:-30px;" name="quantityofproduct">
                                                      <a class="btn btn-danger btn-sm" onclick="javascript:urunSil(${data[i].products[j].id})">x</a>
                                                </div>
                                            </div>
                                           </div>
                                    `;
                }
                target.innerHTML += `<div class="form-row align-items-center">
                                        <div class="col-auto">
                                          <div class="input-group mb-2">
                                            <div class="input-group-prepend">
                                              <div class="input-group-text">@</div>
                                            </div>
                                            <input type="text" class="form-control" id="inlineFormInputGroup_${data[i].id}" disabled value="${mergedName}">
                                          </div>
                                        </div>
                                        <div class="col-auto">
                                          <a class="btn btn-warning mb-2" data-toggle="modal" data-target="#myModal" onclick="javascript:sendCustomer(${data[i].id})">Ürün Ekle</a>
                                        </div>
                                      </div>

                                        <div class="w-auto p-3" style="background-color: #eee;margin-top:-8px;margin-bottom:10px;">
                                            <div class="row">
                                                ${productLines}
                                            </div>
                                        </div>`;
                productLines = "";
            }
        },
        contentType: 'application/json',
        error: function () {
            console.log('an access problem');
        }
    });
}
