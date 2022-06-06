

function getCumtomers() {
    $.ajax({
        url: '/Customer/GetAllCustomers',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);
        },
        error: function () {
            alert('an access problem');
        }
    });
};


function deleteCustomer(id) {
    $.ajax({
        url: '/Customer/Delete?id=' + id,
        type: 'POST',
        success: function (response) {
            location.reload();
        },
        error: function () {
            alert("hata");
        }
    });
};
