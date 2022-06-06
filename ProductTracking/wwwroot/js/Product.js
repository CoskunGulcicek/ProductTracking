

function getProducts() {
    $.ajax({
        url: '/Product/GetAllProducts',
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


function deleteProduct(id) {
    $.ajax({
        url: '/Product/Delete?id=' + id,
        type: 'POST',
        success: function (response) {
            location.reload();
        },
        error: function () {
            alert("hata");
        }
    });
};
