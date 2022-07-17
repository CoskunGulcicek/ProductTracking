

function getLists() {
    $.ajax({
        url: '/List/GetAllLists',
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


function deleteList(id) {
    $.ajax({
        url: '/List/Delete?id=' + id,
        type: 'POST',
        success: function (response) {
            location.reload();
        },
        error: function () {
            alert("hata");
        }
    });
};
