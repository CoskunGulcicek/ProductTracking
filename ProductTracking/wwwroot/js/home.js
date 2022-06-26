$(document).ready(function () {

});


function sendCustomer(data) {
    alert(data);
};

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
        for (let i = 1; i <= cCount; i++) {
            for (let j = 1; j <= pCount; j++) {
                var Name = document.getElementById('name_' + i + '_' + j).textContent;
                var Type = $('#type_' + i + '_' + j).val();
                var Quantity = $('#quantity_' + i + '_' + j).val();
                calculationData.push({ Name, Type, Quantity });
            }
        }
    } else {
        console.log("ürün yok");
    }

    sendCalculationData(calculationData);

});
